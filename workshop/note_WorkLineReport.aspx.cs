using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;

public partial class note_WorkLineReport : BasePage
{
    adamClass chk = new adamClass();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetNoteDuty();
            BindDept(Convert.ToString(Session["uID"]));
            try
            {
                dropDept.SelectedIndex = -1;
                dropDept.Items.FindByValue(Session["deptID"].ToString()).Selected = true;
            }
            catch
            {
                ;
            }
            //工段是基于部门筛选的
            BindWorkShop(dropDept.SelectedValue);
            try
            {
                dropWorkShop.SelectedIndex = -1;
                dropWorkShop.Items.FindByValue(Session["workshopID"].ToString()).Selected = true;
            }
            catch
            {
                ;
            }

            BindWorkLogInfo();
        }

    }

    private void BindDept(string userid)
    {
        dropDept.Items.Clear();
        dropDept.Items.Add("--请选择一个车间--");
        try
        {

            string str = Convert.ToString(Session["plantcode"]);
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
            SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_note_selectWorkHomeGroup", param);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dropDept.Items.Add(new ListItem(reader["name"].ToString(), reader["departmentID"].ToString()));
                }
                reader.Close();
            }
        }
        catch { }
        
    }

    private void BindWorkShop(string deptID)
    {
        dropWorkShop.Items.Clear();
        dropWorkShop.Items.Add("--请选择一个产线--");
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@detpID", deptID);
            param[1] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
            SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_note_selectWorkshop", param);
            while (reader.Read())
            {
                dropWorkShop.Items.Add(new ListItem(reader["name"].ToString(), reader["workshopID"].ToString()));
            }
            reader.Close();
        }
        catch { }
    }

    private void BindWorkLogInfo()
    {
        string _date = DateTime.Now.ToString("yyyy-MM-dd");
        string _leader = Session["uID"].ToString();
        string _woNbr = Request.QueryString["wonbr"] == null ? "" : Request.QueryString["wonbr"];
        string _woLot = Request.QueryString["wolot"] == null ? "" : Request.QueryString["wolot"];
        string _duty = dropDuty.SelectedValue;

        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@duty", _duty);
            param[1] = new SqlParameter("@date", _date);
            param[2] = new SqlParameter("@leader", _leader);
            param[3] = new SqlParameter("@wonbr", _woNbr);
            param[4] = new SqlParameter("@wolot", _woLot);

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_note_selectWorkLogInfo", param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                hidMstrID.Value = ds.Tables[0].Rows[0]["note_id"].ToString();
            }
            else
            {
                hidMstrID.Value = "0";
            }

            gvWorkLog.DataSource = ds.Tables[1];
            gvWorkLog.DataBind();
        }
        catch
        {
            this.Alert("数据库操作失败！请联系管理员！");
        }
    }

    private void GetNoteDuty()
    {
        try
        {
            string strName = "sp_note_selectNoteDuty";
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName);
            dropDuty.DataSource = ds;
            dropDuty.DataBind();

            dropDuty.Items.Insert(0, new ListItem("--", "0"));
        }
        catch
        {
            ;
        }
    }

    protected void gvWorkLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = e.Row.FindControl("chk_Select") as CheckBox;
            chk.Checked = Convert.ToBoolean(gvWorkLog.DataKeys[e.Row.RowIndex].Values["ntd_sel"]);
        }
    }

    protected void gvWorkLog_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Confirm1")
        {
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvWorkLog.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvWorkLog.Rows[i].FindControl("chk_Select");
            if (chkAll.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }
    }
    protected void ddl_workline_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindWorkLogInfo();

    }
    protected void ddl_workhome_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropDept.Text.Trim().Length > 0 && dropDept.SelectedIndex != 0)
        {
            BindWorkShop(dropDept.SelectedValue);
            BindWorkLogInfo();

        }
    }
    protected void btn_Save1_Click(object sender, EventArgs e)
    {
        if (dropDept.SelectedIndex == 0)
        {
            this.Alert("请先选择一个车间！");
            return;
        }

        if (dropWorkShop.SelectedIndex == 0)
        {
            this.Alert("请先选择一个产线！");
            return;
        }

        if (dropDuty.SelectedIndex == 0)
        {
            this.Alert("请先选择一项职责！");
            return;
        }

        int WorkShopID = 0;
        int WorkLineID = 0;
        string WorkShop = "";
        string WorkLine = "";

        WorkShop = dropDept.SelectedItem.Text;
        WorkLine = dropWorkShop.SelectedItem.Text;
        WorkShopID = Convert.ToInt32(dropDept.SelectedValue);
        WorkLineID = Convert.ToInt32(dropWorkShop.SelectedValue);

        try
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@note_id", hidMstrID.Value);
            param[1] = new SqlParameter("@note_date", DateTime.Now.ToString("yyyy-MM-dd"));
            param[2] = new SqlParameter("@note_leaderID", Session["uID"].ToString());
            param[3] = new SqlParameter("@note_leaderName", Session["uName"].ToString());
            param[4] = new SqlParameter("@note_deptID", dropDept.SelectedValue);
            param[5] = new SqlParameter("@note_deptName", dropDept.SelectedItem.Text);
            param[6] = new SqlParameter("@note_workshopID", dropWorkShop.SelectedValue);
            param[7] = new SqlParameter("@note_workshopName", dropWorkShop.SelectedItem.Text);
            param[8] = new SqlParameter("@note_lineID", 0);
            param[9] = new SqlParameter("@note_line", "");
            param[10] = new SqlParameter("@note_nbr", Request.QueryString["wonbr"] == null ? "" : Request.QueryString["wonbr"]);
            param[11] = new SqlParameter("@note_lot", Request.QueryString["wolot"] == null ? "" : Request.QueryString["wolot"]);
            param[12] = new SqlParameter("@note_rmks", txtRemark.Text);
            param[13] = new SqlParameter("@dutyID", dropDuty.SelectedValue);
            param[14] = new SqlParameter("@uID", Session["uID"].ToString());
            param[15] = new SqlParameter("@uName", Session["uName"].ToString());
            param[16] = new SqlParameter("@retValue", SqlDbType.Int);
            param[16].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_note_insertNoteMstr", param);

            if (Convert.ToInt32(param[16].Value) <= 0)
            {
                this.Alert("头栏保存失败1！请联系管理员！");
                return;
            }

            hidMstrID.Value = param[16].Value.ToString();
        }
        catch
        {
            this.Alert("头栏保存失败2！请联系管理员！");
            return;
        }

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", Convert.ToString(Session["uID"]));
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_note_DeleteWorkLogTemp", param);
        }
        catch
        {
            this.Alert("删除临时表失败！请联系管理员！");
            return;
        }

        #region 创建存放数据源的表procOutput
        DataTable procOutput = new DataTable("reporterOutput");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "note_id";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ntt_duty_id";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ntt_id";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ntd_id";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ntd_remarks";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "ntd_sel";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "note_rmks";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "uID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "uName";
        procOutput.Columns.Add(column);

        #endregion

        #region 为数据源表procOutput赋值

        foreach (GridViewRow gvRow in gvWorkLog.Rows)
        {
            int i = gvRow.RowIndex;

            CheckBox chkSel = (CheckBox)gvRow.FindControl("chk_Select");
            row = procOutput.NewRow();

            row["note_id"] = hidMstrID.Value;
            row["ntt_duty_id"] = dropDuty.SelectedValue;
            row["ntt_id"] = gvWorkLog.DataKeys[gvRow.RowIndex].Values["ntt_id"].ToString();
            if (gvWorkLog.DataKeys[gvRow.RowIndex].Values["ntd_id"].ToString() != "")
            {
                row["ntd_id"] = gvWorkLog.DataKeys[gvRow.RowIndex].Values["ntd_id"].ToString();
            }
            row["ntd_remarks"] = gvWorkLog.DataKeys[gvRow.RowIndex].Values["ntt_desc"].ToString();
            row["ntd_sel"] = chkSel.Checked;

            row["uID"] = Session["uID"].ToString();
            row["uName"] = Session["uName"].ToString();
            procOutput.Rows.Add(row);
        }

        #endregion

        #region 将明细导入临时表
        if (procOutput != null && procOutput.Rows.Count > 0)
        {
            using (SqlBulkCopy bulckCopy = new SqlBulkCopy(strConn))
            {
                bulckCopy.DestinationTableName = "BarCodeSys" + ".dbo.note_det_temp";
                bulckCopy.ColumnMappings.Add("note_id", "note_id");
                bulckCopy.ColumnMappings.Add("ntt_duty_id", "ntt_duty_id");
                bulckCopy.ColumnMappings.Add("ntt_id", "ntt_id");
                bulckCopy.ColumnMappings.Add("ntd_id", "ntd_id");
                bulckCopy.ColumnMappings.Add("ntd_remarks", "ntd_remarks");
                bulckCopy.ColumnMappings.Add("ntd_sel", "ntd_sel");

                bulckCopy.ColumnMappings.Add("uID", "uID");
                bulckCopy.ColumnMappings.Add("uName", "uName");

                try
                {
                    bulckCopy.WriteToServer(procOutput);

                    #region 导入到数据库
                    try
                    {
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@note_id", hidMstrID.Value);
                        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
                        param[2] = new SqlParameter("@retValue", SqlDbType.Int);
                        param[2].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_note_batchNoteMstr", param);

                        if (Convert.ToBoolean(param[2].Value))
                        {
                            this.Alert("保存成功！");
                        }
                        else
                        {
                            this.Alert("保存明细失败！请联系管理员！");
                            return;
                        }
                    }
                    catch
                    { 
                        
                    }

                    BindWorkLogInfo();

                    #endregion
                }
                catch
                {
                    this.Alert("同步明细数据失败！");
                    return;
                }
                finally
                {
                    procOutput.Dispose();
                }
            }
        }
        #endregion
    }
    protected void ddl_duty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dropDuty_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindWorkLogInfo();
    }
}