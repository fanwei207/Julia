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
            ltlAlert.Text = "alert('工单号和ID号不能同时为空!')";
        }
        else
        {
            string nbr = CheckWoInfo(txtWorkOrder.Text.Trim(), txtID.Text.Trim(), Convert.ToInt32(Session["PlantCode"].ToString()));

            if (nbr == string.Empty)
            {
                ltlAlert.Text = "alert('加工单不存在!')";
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
            ltlAlert.Text = "alert('加工单不能为空!')";
            return;
        }

        if (txtID.Text.Length == 0)
        {
            ltlAlert.Text = "alert('ID不能为空!')";
            return;
        }

        if (ddlProcInput.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择工序!')";
            return;
        }

        if (ddlPostionInput.SelectedValue.ToString() == "0")
        {
            ltlAlert.Text = "alert('请选择岗位!')";
            return;
        }

        if (txtUserNo.Text.Length == 0)
        {
            ltlAlert.Text = "alert('工号不能为空!')";
            return;
        }

        if (txtEffecDate.Text.Length == 0)
        {
            ltlAlert.Text = "alert('生效日期不能为空!')";
            return;
        }
        else
        {
            try
            {
                dateFormat = Convert.ToDateTime(txtEffecDate.Text.Trim());

                if (dateFormat > DateTime.Now)
                {
                    ltlAlert.Text = "alert('生效日期不能超过当天!')";
                    return;
                }
                else if (!CheckFinIsOpen(Convert.ToDateTime(txtEffecDate.Text.Trim()).Year, Convert.ToDateTime(txtEffecDate.Text.Trim()).Month))
                {
                    if (!CheckWoIsHrClosed(txtWorkOrder.Text, txtID.Text))
                    {
                        ltlAlert.Text = "alert('该生效日期工资已被财务冻结，不能操作!!')";
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期非法!')";
                return;
            }
        }

        if (txtQty.Text.Length == 0)
        {
            ltlAlert.Text = "alert('数量不能为空!')";
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
                ltlAlert.Text = "alert('数量非法!')";
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
                    ltlAlert.Text = "alert('该员工已经存在!')";
                    return;
                }
                else if (retValue == -1)
                {
                    ltlAlert.Text = "alert('添加数据失败，请联系管理员!');";
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
                ltlAlert.Text = "alert('该员工不存在或者已经离职!')";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('暂不能汇报! 请至【条码系统】完成以下步骤：【线路板/裸灯发料】或【毛管发料】!')";
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bValid = true;
        int chkNum = 0;

        if (ddlProcInput.SelectedIndex == 0)
        {
            this.Alert("保存时，必须选择一道工序！");
            return;
        }

        if (txtEffecDate.Text.Length == 0)
        {
            ltlAlert.Text = "alert('请填写生效日期,以防跨天保存,不是该生效日期的不参与保存!')";
            return;
        }
        else
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtEffecDate.Text.Trim());

                if (dateFormat > DateTime.Now)
                {
                    ltlAlert.Text = "alert('生效日期不能超过当天!')";
                    return;
                }
                else if (!CheckFinIsOpen(Convert.ToDateTime(txtEffecDate.Text.Trim()).Year, Convert.ToDateTime(txtEffecDate.Text.Trim()).Month))
                {
                    if (!CheckWoIsHrClosed(txtWorkOrder.Text, txtID.Text))
                    {
                        ltlAlert.Text = "alert('该生效日期工资已被财务冻结，不能操作!!')";
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期非法!')";
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
                    string ordertype = "员工:" + row.Cells[8].Text.Trim() + "汇报数量非法!";
                    ltlAlert.Text = "alert('" + ordertype + "');";
                    break;
                }
            }
            else
            {
                bValid = false;
                string ordertype = "员工:" + row.Cells[8].Text.Trim() + "汇报数量不能为空!";
                ltlAlert.Text = "alert('" + ordertype + "');";
                break;
            }
        }

        if (chkNum == 0)
        {
            bValid = false;
            ltlAlert.Text = "alert('请选择需要参与保存且与生效日期匹配的人员!');";
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
                        ltlAlert.Text = "alert('保存失败,请联系管理员!');";
                        return;
                    }
                    else
                    {
                        ltlAlert.Text = "alert('保存失败,请联系管理员!');";
                        return;
                    }
                }
            }
            else
            {
                ltlAlert.Text = "alert('保存失败,请联系管理员!');";
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
            ltlAlert.Text = "alert('请选择需要清空的人员!');";
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
                    ltlAlert.Text = "alert('清空失败,请联系管理员!');";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('清空失败,请联系管理员!');";
                    return;
                }
            }
        }
        else if (nRet == 0)
        {
            ltlAlert.Text = "alert('清空失败,请联系管理员!');";
            return;
        }
        else if(nRet == 2)
        {
            ltlAlert.Text = "alert('该生效日期工资已被财务冻结，不能操作!!')";
            return;
        }
    }

    protected void btnAvg_Click(object sender, EventArgs e)
    {
        if (ddlProcInput.SelectedIndex == 0)
        {
            this.Alert("平均分配时，必须选择一道工序！");
            return;
        }

        if (txtQty.Text.Length == 0)
        {
            ltlAlert.Text = "alert('数量不能为空!')";
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
                ltlAlert.Text = "alert('数量非法!')";
                return;
            }
        }

        if (txtEffecDate.Text.Length == 0)
        {
            ltlAlert.Text = "alert('请填写生效日期,以防跨天分配,不是该生效日期的不参与平均分配!')";
            return;
        }
        else
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtEffecDate.Text.Trim());

                if (dateFormat > DateTime.Now)
                {
                    ltlAlert.Text = "alert('生效日期不能超过当天!')";
                    return;
                }
                else if (!CheckFinIsOpen(Convert.ToDateTime(txtEffecDate.Text.Trim()).Year, Convert.ToDateTime(txtEffecDate.Text.Trim()).Month))
                {
                    if (!CheckWoIsHrClosed(txtWorkOrder.Text, txtID.Text))
                    {
                        ltlAlert.Text = "alert('该生效日期工资已被财务冻结，不能操作!!')";
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期非法!')";
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
            ltlAlert.Text = "alert('请选择需要参与平均分配且与生效日期匹配的人员!');";
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
                    ltlAlert.Text = "alert('平均分配失败A,请联系管理员!');";
                    return;
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('平均分配失败B,请联系管理员!');";
            return;
        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (txtWorkOrder.Text.Trim() == string.Empty)
        {
            this.Alert("删除时，工单不能为空！");
            return;
        }

        if (txtID.Text.Trim() == string.Empty)
        {
            this.Alert("删除时，ID号不能为空！");
            return;
        }

        if (txtEffecDate.Text.Trim() == string.Empty)
        {
            this.Alert("删除时，生效日期不能为空！");
            return;
        }

        if (ddlProcInput.SelectedIndex == 0)
        {
            this.Alert("删除时，请选择一道工序！");
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
            ltlAlert.Text = "alert('请选择需要删除的人员!');";
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
                    ltlAlert.Text = "alert('删除失败,请联系管理员!');";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('删除失败,请联系管理员!');";
                    return;
                }
            }
        }
        else if (nRet == 0)
        {
            ltlAlert.Text = "alert('删除失败,请联系管理员!');";
            return;
        }
        else if (nRet == 2)
        {
            ltlAlert.Text = "alert('该生效日期工资已被财务冻结，不能操作!!')";
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
                    ltlAlert.Text = "alert('该生效日期工资已被财务冻结，不能操作!!')";
                    return;
                }
            }

            if (DeleteWoUserAfterOffLine(id, effdate, Convert.ToInt32(Session["uID"].ToString())))
            {
                BindGridView();
            }
            else
            {
                ltlAlert.Text = "alert('删除数据失败，请联系管理员!');";
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

        lblSum.Text = "当前共" + dt.Rows.Count.ToString() + "条记录,汇报数量之和为" + sumQty.ToString();
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
            #region 创建存放数据源的表procOutput
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
                        ltlAlert.Text = "alert('该生效日期工资已被财务冻结，不能操作!!')";
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
                        ltlAlert.Text = "alert('删除失败,请联系管理员!');";
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
            #region 创建存放数据源的表procOutput
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
                        ltlAlert.Text = "alert('保存失败,请联系管理员!');";
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
            #region 创建存放数据源的表procOutput
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
                        ltlAlert.Text = "alert('平均分配失败,请联系管理员!');";
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
    /// 检查工单是否结算
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
