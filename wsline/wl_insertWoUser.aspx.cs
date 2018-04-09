using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wsline_wl_insertWoUser : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtWorkOrder.Text.Length == 0 && txtID.Text.Length == 0)
        {
            ltlAlert.Text = "alert('�����ź�ID�Ų���ͬʱΪ��!')";
        }
        else
        {
            string nbr = CheckWoInfo(txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(Session["PlantCode"].ToString()));

            if (nbr == string.Empty)
            {
                ltlAlert.Text = "alert('�ӹ���������!')";
            }
            else
            {
                if (txtWorkOrder.Text.Length == 0)
                {
                    txtWorkOrder.Text = nbr;
                }
                else
                {
                    txtID.Text = nbr;
                }
            }

            BindWoProcess();
            BindWoPostion();
            BindGridView();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        decimal intFormat = 0;
        DateTime dateFormat = DateTime.Now;
        if (txtWorkOrder.Text.Length == 0)
        {
            ltlAlert.Text = "alert('�ӹ�������Ϊ��!')";
            return;
        }

        if (txtID.Text.Length == 0)
        {
            ltlAlert.Text = "alert('ID����Ϊ��!')";
            return;
        }

        if (ddlProcInput.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('��ѡ����!')";
            return;
        }

        if (ddlPostionInput.SelectedValue.ToString() == "0")
        {
            ltlAlert.Text = "alert('��ѡ���λ!')";
            return;
        }

        if (txtUserNo.Text.Length == 0)
        {
            ltlAlert.Text = "alert('���Ų���Ϊ��!')";
            return;
        }

        if (txtEffecDate.Text.Length == 0)
        {
            ltlAlert.Text = "alert('��Ч���ڲ���Ϊ��!')";
            return;
        }
        else
        {
            try
            {
                dateFormat = Convert.ToDateTime(txtEffecDate.Text.Trim());

                if (dateFormat > DateTime.Now)
                {
                    ltlAlert.Text = "alert('��Ч���ڲ��ܳ�������!')";
                    return;
                }
                else if (!CheckFinIsOpen(Convert.ToDateTime(txtEffecDate.Text.Trim()).Year, Convert.ToDateTime(txtEffecDate.Text.Trim()).Month))
                {
                    if (!CheckWoIsHrClosed(txtWorkOrder.Text, txtID.Text))
                    {
                        ltlAlert.Text = "alert('����Ч���ڹ����ѱ����񶳽ᣬ���ܲ���!!')";
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('��Ч���ڷǷ�!')";
                return;
            }
        }

        if (txtQty.Text.Length == 0)
        {
            ltlAlert.Text = "alert('��������Ϊ��!')";
            return;
        }
        else
        {
            try
            {
                intFormat = Convert.ToDecimal(txtQty.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('�����Ƿ�!')";
                return;
            }
        }

        if (CheckWoIsReady(txtWorkOrder.Text.Trim(), txtID.Text.Trim()))
        {
            if (CheckUsers(txtUserNo.Text.Trim(), Convert.ToInt32(Session["PlantCode"].ToString())))
            {
                int len = ddlProcInput.SelectedItem.Text.Trim().Length;
                int index = ddlProcInput.SelectedItem.Text.Trim().IndexOf(")");

                string procName = ddlProcInput.SelectedItem.Text.Trim().Substring(index + 1, len - index - 1);

                string[] param = ddlPostionInput.SelectedItem.Text.Trim().Replace("--", "-").Split('-');
                string posion = param[0];
                string postName = param[1];
                string postProportion = param[2];


                int retValue = AddWoUserAfterOffLine(txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(ddlProcInput.SelectedValue.ToString()), procName, 0,
                    Convert.ToInt32(ddlPostionInput.SelectedValue.ToString()), postName, Convert.ToDecimal(postProportion), txtUserNo.Text.Trim(),
                    Convert.ToInt32(Session["uID"].ToString()), Convert.ToDecimal(txtQty.Text.Trim()), Convert.ToInt32(Session["PlantCode"].ToString()), txtEffecDate.Text.Trim());

                if (retValue == 0)
                {
                    ltlAlert.Text = "alert('��Ա���Ѿ�����!')";
                    return;
                }
                else if (retValue == -1)
                {
                    ltlAlert.Text = "alert('�������ʧ�ܣ�����ϵ����Ա!');";
                    return;
                }
                else if (retValue == 1)
                {
                    txtUserNo.Text = string.Empty;
                    BindGridView();
                }
            }
            else
            {
                ltlAlert.Text = "alert('��Ա�������ڻ����Ѿ���ְ!')";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('�ݲ��ܻ㱨! ����������ϵͳ��������²��裺����·��/��Ʒ��ϡ���ë�ܷ��ϡ�!')";
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bValid = true;
        int chkNum = 0;

        if (ddlProcInput.SelectedIndex == 0)
        {
            this.Alert("����ʱ������ѡ��һ������");
            return;
        }

        if (txtEffecDate.Text.Length == 0)
        {
            ltlAlert.Text = "alert('����д��Ч����,�Է����챣��,���Ǹ���Ч���ڵĲ����뱣��!')";
            return;
        }
        else
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtEffecDate.Text.Trim());

                if (dateFormat > DateTime.Now)
                {
                    ltlAlert.Text = "alert('��Ч���ڲ��ܳ�������!')";
                    return;
                }
                else if (!CheckFinIsOpen(Convert.ToDateTime(txtEffecDate.Text.Trim()).Year, Convert.ToDateTime(txtEffecDate.Text.Trim()).Month))
                {
                    if (!CheckWoIsHrClosed(txtWorkOrder.Text, txtID.Text))
                    {
                        ltlAlert.Text = "alert('����Ч���ڹ����ѱ����񶳽ᣬ���ܲ���!!')";
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('��Ч���ڷǷ�!')";
                return;
            }
        }

        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox chkUsers = (CheckBox)row.FindControl("chkUsers");
            TextBox txtWo2_line_comp = (TextBox)row.FindControl("txt_wo2_line_comp");

            if (chkUsers.Checked && (row.Cells[12].Text.Trim() == txtEffecDate.Text.Trim() || row.Cells[12].Text.Trim() == "&nbsp;"))
            {
                chkNum = chkNum + 1;
            }

            if (txtWo2_line_comp.Text.Trim() != string.Empty)
            {
                try
                {
                    Decimal qty = Convert.ToDecimal(txtWo2_line_comp.Text.Trim());
                }
                catch
                {
                    bValid = false;
                    string ordertype = "Ա��:" + row.Cells[8].Text.Trim() + "�㱨�����Ƿ�!";
                    ltlAlert.Text = "alert('" + ordertype + "');";
                    break;
                }
            }
            else
            {
                bValid = false;
                string ordertype = "Ա��:" + row.Cells[8].Text.Trim() + "�㱨��������Ϊ��!";
                ltlAlert.Text = "alert('" + ordertype + "');";
                break;
            }
        }

        if (chkNum == 0)
        {
            bValid = false;
            ltlAlert.Text = "alert('��ѡ����Ҫ���뱣��������Ч����ƥ�����Ա!');";
            return;
        }

        if (bValid)
        {
            if (UpdateWoReporterBatchSave())
            {
                if (ReportBatch(Convert.ToInt32(Session["uID"].ToString()), txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(ddlProcInput.SelectedItem.Value), txtEffecDate.Text.Trim()))
                {
                    BindGridView();
                }
                else
                {
                    if (DeleteWorkOrderReportUpdate(Convert.ToInt32(Session["uID"].ToString())))
                    {
                        ltlAlert.Text = "alert('����ʧ��,����ϵ����Ա!');";
                        return;
                    }
                    else
                    {
                        ltlAlert.Text = "alert('����ʧ��,����ϵ����Ա!');";
                        return;
                    }
                }
            }
            else
            {
                ltlAlert.Text = "alert('����ʧ��,����ϵ����Ա!');";
                return;
            }
        }
    }

    protected void txtClear_Click(object sender, EventArgs e)
    {
        int chkNum = 0;
        string idGroup = string.Empty;
        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox chkUsers = (CheckBox)row.FindControl("chkUsers");

            if (chkUsers.Checked)
            {
                chkNum = chkNum + 1;
            }
        }

        if (chkNum == 0)
        {
            ltlAlert.Text = "alert('��ѡ����Ҫ��յ���Ա!');";
            return;
        }

        int nRet = DeleteWoReporterBatch();
        if (nRet == 1)
        {
            if (ClearWoUserAfterOffLineBatch(Convert.ToInt32(Session["uID"].ToString())))
            {
                BindGridView();
            }
            else
            {
                if (DeleteWorkOrderEnterID(Convert.ToInt32(Session["uID"].ToString())))
                {
                    ltlAlert.Text = "alert('���ʧ��,����ϵ����Ա!');";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('���ʧ��,����ϵ����Ա!');";
                    return;
                }
            }
        }
        else if (nRet == 0)
        {
            ltlAlert.Text = "alert('���ʧ��,����ϵ����Ա!');";
            return;
        }
        else if(nRet == 2)
        {
            ltlAlert.Text = "alert('����Ч���ڹ����ѱ����񶳽ᣬ���ܲ���!!')";
            return;
        }
    }

    protected void btnAvg_Click(object sender, EventArgs e)
    {
        if (ddlProcInput.SelectedIndex == 0)
        {
            this.Alert("ƽ������ʱ������ѡ��һ������");
            return;
        }

        if (txtQty.Text.Length == 0)
        {
            ltlAlert.Text = "alert('��������Ϊ��!')";
            return;
        }
        else
        {
            try
            {
                Decimal decFormat = Convert.ToDecimal(txtQty.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('�����Ƿ�!')";
                return;
            }
        }

        if (txtEffecDate.Text.Length == 0)
        {
            ltlAlert.Text = "alert('����д��Ч����,�Է��������,���Ǹ���Ч���ڵĲ�����ƽ������!')";
            return;
        }
        else
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtEffecDate.Text.Trim());

                if (dateFormat > DateTime.Now)
                {
                    ltlAlert.Text = "alert('��Ч���ڲ��ܳ�������!')";
                    return;
                }
                else if (!CheckFinIsOpen(Convert.ToDateTime(txtEffecDate.Text.Trim()).Year, Convert.ToDateTime(txtEffecDate.Text.Trim()).Month))
                {
                    if (!CheckWoIsHrClosed(txtWorkOrder.Text, txtID.Text))
                    {
                        ltlAlert.Text = "alert('����Ч���ڹ����ѱ����񶳽ᣬ���ܲ���!!')";
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('��Ч���ڷǷ�!')";
                return;
            }
        }

        int chkNum = 0;
        decimal avgQty = 0;

        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox chkUsers = (CheckBox)row.FindControl("chkUsers");

            if (chkUsers.Checked && (row.Cells[12].Text.Trim() == txtEffecDate.Text.Trim() || row.Cells[12].Text.Trim() == "&nbsp;"))
            {
                chkNum = chkNum + 1;
            }
        }

        if (chkNum == 0)
        {
            ltlAlert.Text = "alert('��ѡ����Ҫ����ƽ������������Ч����ƥ�����Ա!');";
            return;
        }

        avgQty = Convert.ToDecimal(txtQty.Text.Trim()) / chkNum;

        if (UpdateWoReporterBatchAvg(avgQty))
        {
            if (ReportBatch(Convert.ToInt32(Session["uID"].ToString()), txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(ddlProcInput.SelectedItem.Value), txtEffecDate.Text.Trim()))
            {
                BindGridView();
            }
            else
            {
                if (!DeleteWorkOrderReportUpdate(Convert.ToInt32(Session["uID"].ToString())))
                {
                    ltlAlert.Text = "alert('ƽ������ʧ��A,����ϵ����Ա!');";
                    return;
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('ƽ������ʧ��B,����ϵ����Ա!');";
            return;
        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (txtWorkOrder.Text.Trim() == string.Empty)
        {
            this.Alert("ɾ��ʱ����������Ϊ�գ�");
            return;
        }

        if (txtID.Text.Trim() == string.Empty)
        {
            this.Alert("ɾ��ʱ��ID�Ų���Ϊ�գ�");
            return;
        }

        if (txtEffecDate.Text.Trim() == string.Empty)
        {
            this.Alert("ɾ��ʱ����Ч���ڲ���Ϊ�գ�");
            return;
        }

        if (ddlProcInput.SelectedIndex == 0)
        {
            this.Alert("ɾ��ʱ����ѡ��һ������");
            return;
        }

        int chkNum = 0;
        string idGroup = string.Empty;
        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox chkUsers = (CheckBox)row.FindControl("chkUsers");

            if (chkUsers.Checked)
            {
                chkNum = chkNum + 1;
            }
        }

        if (chkNum == 0)
        {
            ltlAlert.Text = "alert('��ѡ����Ҫɾ������Ա!');";
            return;
        }

        int nRet = DeleteWoReporterBatch();

        if(nRet == 1)
        {
            if (DeleteWoUserAfterOffLineBatch(Convert.ToInt32(Session["uID"].ToString())))
            {
                BindGridView();
            }
            else
            {
                if (DeleteWorkOrderEnterID(Convert.ToInt32(Session["uID"].ToString())))
                {
                    ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
                    return;
                }
            }
        }
        else if (nRet == 0)
        {
            ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
            return;
        }
        else if (nRet == 2)
        {
            ltlAlert.Text = "alert('����Ч���ڹ����ѱ����񶳽ᣬ���ܲ���!!')";
            return;
        }
    }

    protected void ddlProcInput_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindWoPostion();
        BindGridView();
    }

    protected void ddlPostionInput_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!Convert.ToBoolean(gvList.DataKeys[e.Row.RowIndex].Values["wo2_isBarCodesys"]))
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            int id = Convert.ToInt32(gvList.DataKeys[index].Values["wo2_id"].ToString());
            string effdate = gvList.DataKeys[index].Values["wo2_effDate"].ToString();
            if (effdate == "")
            {
                effdate = "1990-01-01";
            }
            if (!CheckFinIsOpen(Convert.ToDateTime(effdate).Year, Convert.ToDateTime(effdate).Month))
            {
                if (!CheckWoIsHrClosed(txtWorkOrder.Text, txtID.Text))
                {
                    ltlAlert.Text = "alert('����Ч���ڹ����ѱ����񶳽ᣬ���ܲ���!!')";
                    return;
                }
            }

            if (DeleteWoUserAfterOffLine(id, effdate, Convert.ToInt32(Session["uID"].ToString())))
            {
                BindGridView();
            }
            else
            {
                ltlAlert.Text = "alert('ɾ������ʧ�ܣ�����ϵ����Ա!');";
                return;
            }
        }
    }

    private string CheckWoInfo(string nbr, string lot, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@lot", lot);
        param[2] = new SqlParameter("@plantCode", plantCode);

        return Convert.ToString(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_CheckWoInfo", param));
    }

    private void BindWoProcess()
    {
        DataTable dt = GetWoProcess(txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(Session["PlantCode"].ToString()));

        ddlProcInput.DataSource = dt;
        ddlProcInput.DataBind();
        ddlProcInput.Items.Insert(0, new ListItem("----", "0"));
    }

    private void BindWoPostion()
    {
        ddlPostionInput.Items.Clear();
        DataTable dt = GetWoPostion(txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(ddlProcInput.SelectedValue.ToString()));

        ddlPostionInput.DataSource = dt;
        ddlPostionInput.DataBind();
        ddlPostionInput.Items.Insert(0, new ListItem("----", "0"));
    }

    private void BindGridView()
    {
        DataTable dt = GetWoUserAfterOffLine(txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(ddlProcInput.SelectedValue.ToString()), 0, Convert.ToInt32(ddlPostionInput.SelectedValue.ToString()), txtEffecDate.Text.Trim(), txtUserNo.Text.Trim());

        gvList.DataSource = dt;
        gvList.DataBind();

        decimal sumQty = 0;
        foreach (DataRow row in dt.Rows)
        {
            sumQty = sumQty + Convert.ToDecimal(row["wo2_line_comp"].ToString());
        }

        lblSum.Text = "��ǰ��" + dt.Rows.Count.ToString() + "����¼,�㱨����֮��Ϊ" + sumQty.ToString();
    }

    private DataTable GetWoProcess(string nbr, string lot, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@lot", lot);
        param[2] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectProcessesManul", param).Tables[0];
    }

    private DataTable GetWoPostion(string nbr, string lot, int process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@lot", lot);
        param[2] = new SqlParameter("@process", process);

        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectPostionManul", param).Tables[0];
    }

    private DataTable GetWoUserAfterOffLine(string nbr, string lot, int process, int group, int postion, string effdate, string userNo)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@lot", lot);
        param[2] = new SqlParameter("@process", process);
        param[3] = new SqlParameter("@group", group);
        param[4] = new SqlParameter("@postion", postion);
        param[5] = new SqlParameter("@effdate", effdate);
        param[6] = new SqlParameter("@userNo", userNo);

        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_selectWoUserAfterOffLine", param).Tables[0];
    }

    private int AddWoUserAfterOffLine(string nbr, string lot, int process, string procName, int group, int postion, string postName, decimal postProportion, string userNo, int createdBy, decimal qty, int plantCode, string effdate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@lot", lot);
            param[2] = new SqlParameter("@process", process);
            param[3] = new SqlParameter("@procName", procName);
            param[4] = new SqlParameter("@group", group);
            param[5] = new SqlParameter("@posion", postion);
            param[6] = new SqlParameter("@postName", postName);
            param[7] = new SqlParameter("@postProportion", postProportion);
            param[8] = new SqlParameter("@uNo", userNo);
            param[9] = new SqlParameter("@createdBy", createdBy);
            param[10] = new SqlParameter("@qty", qty);
            param[11] = new SqlParameter("@plantCode", plantCode);
            param[12] = new SqlParameter("@effdate", effdate);
            param[13] = new SqlParameter("@retValue", DbType.Int32);
            param[13].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_insertWoUserAfterOffLine", param);
            return Convert.ToInt32(param[13].Value);
        }
        catch
        {
            return 0;
        }
    }

    private bool CheckUsers(string userNo, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@userNo", userNo);
        param[1] = new SqlParameter("@plantCode", plantCode);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_checkUsers", param));
    }

    private bool DeleteWoUserAfterOffLine(int id, string effdate, int createdBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@wo2_id", id);
            param[1] = new SqlParameter("@wo2_effdate", effdate);
            param[2] = new SqlParameter("@wo2_his_createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_deleteWoUserAfterOffLine", param));
        }
        catch
        {
            return false;
        }
    }

    private int DeleteWoReporterBatch()
    {
        if (DeleteWorkOrderEnterID(Convert.ToInt32(Session["uID"].ToString())))
        {
            #region �����������Դ�ı�procOutput
            DataTable procOutput = new DataTable("reporterOutput");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "wo2_id";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "wo2_effdate";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "wo2_createdBy";
            procOutput.Columns.Add(column);
            #endregion

            foreach (GridViewRow row1 in gvList.Rows)
            {
                CheckBox chkUsers = (CheckBox)row1.FindControl("chkUsers");
                if (chkUsers.Checked)
                {
                    DataRow procOutputRow = procOutput.NewRow();
                    procOutputRow["wo2_id"] = Convert.ToInt32(gvList.DataKeys[row1.RowIndex].Values["wo2_id"].ToString());

                    string wo2_effdate = string.Empty;

                    if (row1.Cells[12].Text.Trim() == "&nbsp;")
                    {
                        wo2_effdate = "1990-01-01";
                    }
                    else if (!CheckFinIsOpen(Convert.ToDateTime(row1.Cells[12].Text.Trim()).Year, Convert.ToDateTime(row1.Cells[12].Text.Trim()).Month))
                    {
                        ltlAlert.Text = "alert('����Ч���ڹ����ѱ����񶳽ᣬ���ܲ���!!')";
                        return 2;
                    }
                    else
                    {
                        wo2_effdate = row1.Cells[12].Text.Trim();
                    }

                    procOutputRow["wo2_effdate"] = wo2_effdate;
                    procOutputRow["wo2_createdBy"] = Convert.ToInt32(Session["uID"].ToString());
                    procOutput.Rows.Add(procOutputRow);
                }
            }

            if (procOutput != null && procOutput.Rows.Count > 0)
            {
                using (SqlBulkCopy bulckCopy = new SqlBulkCopy(chk.dsnx(), SqlBulkCopyOptions.UseInternalTransaction))
                {
                    bulckCopy.DestinationTableName = "wo2_WorkOrderEnterID";
                    bulckCopy.ColumnMappings.Add("wo2_id", "wo2_id");
                    bulckCopy.ColumnMappings.Add("wo2_effdate", "wo2_effdate");
                    bulckCopy.ColumnMappings.Add("wo2_createdBy", "wo2_createdBy");

                    try
                    {
                        bulckCopy.WriteToServer(procOutput);
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
                        return 0;
                    }
                    finally
                    {
                        procOutput.Dispose();
                    }
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    private bool UpdateWoReporterBatchSave()
    {
        if (DeleteWorkOrderReportUpdate(Convert.ToInt32(Session["uID"].ToString())))
        {
            #region �����������Դ�ı�procOutput
            DataTable procOutput = new DataTable("reporterOutput");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "wo2_id";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "wo2_effdate";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "wo2_line_comp";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "wo2_reportedBy";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "wo2_reportedName";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "wo2_reportedDate";
            procOutput.Columns.Add(column);
            #endregion

            foreach (GridViewRow row1 in gvList.Rows)
            {
                CheckBox chkUsers = (CheckBox)row1.FindControl("chkUsers");
                TextBox txt_wo2_line_comp = (TextBox)row1.FindControl("txt_wo2_line_comp");

                if (chkUsers.Checked && (row1.Cells[12].Text.Trim() == txtEffecDate.Text.Trim() || row1.Cells[12].Text.Trim() == "&nbsp;"))
                {
                    DataRow procOutputRow = procOutput.NewRow();
                    procOutputRow["wo2_id"] = Convert.ToInt32(gvList.DataKeys[row1.RowIndex].Values["wo2_id"].ToString());

                    string wo2_effdate = string.Empty;

                    if (row1.Cells[12].Text.Trim() == "&nbsp;")
                    {
                        wo2_effdate = "1990-01-01";
                    }
                    else
                    {
                        wo2_effdate = row1.Cells[12].Text.Trim();
                    }

                    procOutputRow["wo2_effdate"] = wo2_effdate;
                    procOutputRow["wo2_line_comp"] = Convert.ToDecimal(txt_wo2_line_comp.Text.Trim());
                    procOutputRow["wo2_reportedBy"] = Convert.ToInt32(Session["uID"].ToString());
                    procOutputRow["wo2_reportedName"] = Session["uName"].ToString();
                    procOutputRow["wo2_reportedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    procOutput.Rows.Add(procOutputRow);
                }
            }

            if (procOutput != null && procOutput.Rows.Count > 0)
            {
                using (SqlBulkCopy bulckCopy = new SqlBulkCopy(chk.dsnx(), SqlBulkCopyOptions.UseInternalTransaction))
                {
                    bulckCopy.DestinationTableName = "wo2_WorkOrderReportUpdate";
                    bulckCopy.ColumnMappings.Add("wo2_id", "wo2_id");
                    bulckCopy.ColumnMappings.Add("wo2_effdate", "wo2_effdate");
                    bulckCopy.ColumnMappings.Add("wo2_line_comp", "wo2_line_comp");
                    bulckCopy.ColumnMappings.Add("wo2_reportedBy", "wo2_reportedBy");
                    bulckCopy.ColumnMappings.Add("wo2_reportedName", "wo2_reportedName");
                    bulckCopy.ColumnMappings.Add("wo2_reportedDate", "wo2_reportedDate");

                    try
                    {
                        bulckCopy.WriteToServer(procOutput);
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('����ʧ��,����ϵ����Ա!');";
                        return false;
                    }
                    finally
                    {
                        procOutput.Dispose();
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    private bool UpdateWoReporterBatchAvg(decimal qty)
    {
        if (DeleteWorkOrderReportUpdate(Convert.ToInt32(Session["uID"].ToString())))
        {
            #region �����������Դ�ı�procOutput
            DataTable procOutput = new DataTable("reporterOutput");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "wo2_id";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "wo2_effdate";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "wo2_line_comp";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "wo2_reportedBy";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "wo2_reportedName";
            procOutput.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "wo2_reportedDate";
            procOutput.Columns.Add(column);
            #endregion

            foreach (GridViewRow row1 in gvList.Rows)
            {
                CheckBox chkUsers = (CheckBox)row1.FindControl("chkUsers");

                if (chkUsers.Checked && (row1.Cells[12].Text.Trim() == txtEffecDate.Text.Trim() || row1.Cells[12].Text.Trim() == "&nbsp;"))
                {
                    DataRow procOutputRow = procOutput.NewRow();
                    procOutputRow["wo2_id"] = Convert.ToInt32(gvList.DataKeys[row1.RowIndex].Values["wo2_id"].ToString());

                    string wo2_effdate = string.Empty;

                    if (row1.Cells[12].Text.Trim() == "&nbsp;")
                    {
                        wo2_effdate = "1990-01-01";
                    }
                    else
                    {
                        wo2_effdate = row1.Cells[12].Text.Trim();
                    }

                    procOutputRow["wo2_effdate"] = wo2_effdate;
                    procOutputRow["wo2_line_comp"] = qty;
                    procOutputRow["wo2_reportedBy"] = Convert.ToInt32(Session["uID"].ToString());
                    procOutputRow["wo2_reportedName"] = Session["uName"].ToString();
                    procOutputRow["wo2_reportedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    procOutput.Rows.Add(procOutputRow);
                }
            }

            if (procOutput != null && procOutput.Rows.Count > 0)
            {
                using (SqlBulkCopy bulckCopy = new SqlBulkCopy(chk.dsnx(), SqlBulkCopyOptions.UseInternalTransaction))
                {
                    bulckCopy.DestinationTableName = "wo2_WorkOrderReportUpdate";
                    bulckCopy.ColumnMappings.Add("wo2_id", "wo2_id");
                    bulckCopy.ColumnMappings.Add("wo2_effdate", "wo2_effdate");
                    bulckCopy.ColumnMappings.Add("wo2_line_comp", "wo2_line_comp");
                    bulckCopy.ColumnMappings.Add("wo2_reportedBy", "wo2_reportedBy");
                    bulckCopy.ColumnMappings.Add("wo2_reportedName", "wo2_reportedName");
                    bulckCopy.ColumnMappings.Add("wo2_reportedDate", "wo2_reportedDate");

                    try
                    {
                        bulckCopy.WriteToServer(procOutput);
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('ƽ������ʧ��,����ϵ����Ա!');";
                        return false;
                    }
                    finally
                    {
                        procOutput.Dispose();
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    private bool ReportBatch(int createdBy, string nbr, string lot, int process, string effdate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@createdBy", createdBy);
            param[1] = new SqlParameter("@nbr", nbr);
            param[2] = new SqlParameter("@lot", lot);
            param[3] = new SqlParameter("@par_process", process);
            param[4] = new SqlParameter("@effdate", effdate);

            SqlConnection conn = new SqlConnection(chk.dsnx());
            conn.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandTimeout = 240;
            cm.CommandText = "sp_wo2_updateWoReporterBatchManual3";
            cm.Connection = conn;

            cm.Parameters.AddRange(param);

            bool bValid = Convert.ToBoolean(cm.ExecuteScalar());

            conn.Close();

            return bValid;
        }
        catch
        {
            return false;
        }
    }

    private bool DeleteWorkOrderEnterID(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_deleteWorkOrderEnterID", param));
        }
        catch
        {
            return false;
        }
    }

    private bool DeleteWorkOrderReportUpdate(int reportedBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@reportedBy", reportedBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_deleteWorkOrderReportUpdate", param));
        }
        catch
        {
            return false;
        }
    }

    private bool DeleteWoUserAfterOffLineBatch(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            SqlConnection conn = new SqlConnection(chk.dsnx());
            conn.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandTimeout = 240;
            cm.CommandText = "sp_wo2_deleteWoUserAfterOffLineBatch1";
            cm.Connection = conn;

            cm.Parameters.Add(param);

            bool bValid = Convert.ToBoolean(cm.ExecuteScalar());

            conn.Close();

            return bValid;
        }
        catch
        {
            return false;
        }
    }

    private bool ClearWoUserAfterOffLineBatch(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);
            //return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, "sp_clearWoUserAfterOffLineBatch", param));

            SqlConnection conn = new SqlConnection(chk.dsnx());
            conn.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandTimeout = 240;
            cm.CommandText = "sp_clearWoUserAfterOffLineBatch";
            cm.Connection = conn;

            cm.Parameters.Add(param);

            bool bValid = Convert.ToBoolean(cm.ExecuteScalar());

            conn.Close();

            return bValid;
        }
        catch
        {
            return false;
        }
    }

    private bool CheckWoIsReady(string nbr, string lot)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@lot", lot);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_checkWoIsReady", param));
        }
        catch
        {
            return false;
        }
    }

    private bool CheckFinIsOpen(int wo2_year, int wo2_month)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@wo2_year", wo2_year);
            param[1] = new SqlParameter("@wo2_month", wo2_month);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_checkFinIsOpen", param));
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// ��鹤���Ƿ����
    /// </summary>
    /// <param name="nbr"></param>
    /// <param name="lot"></param>
    /// <returns></returns>
    private bool CheckWoIsHrClosed(string nbr, string lot)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@lot", lot);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_wo2_checkFinIsHrClosed", param);
            return Convert.ToBoolean(param[2].Value);
        }
        catch
        {
            return false;
        }
    }
}
