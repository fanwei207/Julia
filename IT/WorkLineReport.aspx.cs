using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using adamFuncs;
using QADSID;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;

public partial class IT_WorkLineReport : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetNoteDuty();
            BindWorkHomeGroup(Convert.ToString(Session["uID"]));
            BindcmbWorkLineGroup("");
            BindWorkHomeGroup(0);
            gvWorkLog.Columns[2].Visible = false;
        }

    }

    private void BindWorkHomeGroup(string userid)
    {
        ddl_workhome.Items.Clear();
        ddl_workhome.Items.Add("--请选择一个车间--");
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
                    ddl_workhome.Items.Add(reader["departmentID"].ToString() + "-" + reader["name"].ToString());
                }
                reader.Close();
            }
        }
        catch { }
        ddl_workhome.SelectedIndex = 0;
    }

    private void BindcmbWorkLineGroup(string WorkHome)
    {
        ddl_workline.Items.Clear();
        ddl_workline.Items.Add("--请选择一个产线--");
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@WorkHome", WorkHome);
            param[1] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
            SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_note_selectcmbWorkLineGroup", param);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_workline.Items.Add(reader["id"].ToString() + "-" + reader["name"].ToString());
                }
                reader.Close();
            }
        }
        catch { }
        ddl_workline.SelectedIndex = 0;
    }

    private void BindWorkHomeGroup(int duty)
    {
        int index1 = ddl_workhome.Text.ToString().IndexOf("-");
        int index2 = ddl_workline.Text.ToString().IndexOf("-");
        string WorkHome = "";
        string WorkLine = "";
        string OrderNo = "";

        WorkHome = ddl_workhome.Text.ToString().Trim().Substring(0, index1);
        WorkLine = ddl_workline.Text.ToString().Trim().Substring(0, index2);

        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@WorkLogDate", "");
        param[1] = new SqlParameter("@UserId", Convert.ToString(Session["uID"]));
        param[2] = new SqlParameter("@duty", duty);
        param[3] = new SqlParameter("@WorkHome", WorkHome);
        param[4] = new SqlParameter("@WorkLine", WorkLine);
        param[5] = new SqlParameter("@OrderNbr", OrderNo);
        param[6] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_note_selectWorkLogInfo", param).Tables[0];
        gvWorkLog.DataSource = dt;
        gvWorkLog.DataBind();
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
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //}
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

    }
    protected void ddl_workhome_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_workhome.Text.Trim().Length > 0 && ddl_workhome.SelectedIndex != 0)
        {
            int index = ddl_workhome.Text.ToString().IndexOf("-");
            BindcmbWorkLineGroup(ddl_workhome.Text.ToString().Trim().Substring(0, index));

        }
    }
    protected void btn_Save1_Click(object sender, EventArgs e)
    {

        if (ddl_workhome.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请先选择一个车间！');";
            return;
        }
        if (ddl_workline.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请先选择一个产线！');";
            return;
        }

        int index1 = ddl_workhome.Text.ToString().IndexOf("-") +1;
        
        int index2 = ddl_workline.Text.ToString().IndexOf("-") +1;
        int index3 = ddl_workhome.Text.Length;
        int index4 = ddl_workline.Text.Length;

        string WorkHome = "";
        string WorkLine = "";

        WorkHome = ddl_workhome.Text.ToString().Trim().Substring(index1, index3 - index1);
        WorkLine = ddl_workline.Text.ToString().Trim().Substring(index2, index4 - index2);

        if (txt_remark.Text.Length > 100)
        {
            ltlAlert.Text = "alert('备注需要100字符内，请重新编辑备注！');";
            return;
        }

        SqlParameter[] param1 = new SqlParameter[1];
        param1[0] = new SqlParameter("@userid", Convert.ToString(Session["uID"]));
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_note_DeleteWorkLogTemp", param1);

        #region 创建存放数据源的表procOutput
        DataTable procOutput = new DataTable("reporterOutput");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ntd_temp_id";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "note_workshop";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "note_rmks";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "note_createBy";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "note_createName";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "note_createDate";
        procOutput.Columns.Add(column);

        #endregion

        bool HaveErrorData = false;
        int cnt = 0;

        #region 为数据源表procOutput赋值

        foreach (GridViewRow row1 in gvWorkLog.Rows)
        {
            int i = row1.RowIndex;

            CheckBox chkUsers1 = (CheckBox)row1.FindControl("chk_Select");
            if (chkUsers1.Checked)
            {
                cnt++;
                row = procOutput.NewRow();

                string str1 = row1.Cells[0].Text;
                string str2 = row1.Cells[1].Text;
                row["ntd_temp_id"] = row1.Cells[2].Text;
                row["note_workshop"] = row1.Cells[4].Text;
                row["note_rmks"] = row1.Cells[5].Text;

                row["note_createBy"] = Convert.ToString(Session["uID"]);
                row["note_createName"] = Convert.ToString(Session["uName"]);
                row["note_createDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                procOutput.Rows.Add(row);
            }
        }

        if (cnt == 0)
        {
            ltlAlert.Text = "alert('未选择汇报项，请选择汇报项后再做保存！');";
            return;
        }

        #endregion

        #region 导入到数据库临时表
        if (HaveErrorData)
        {
            ltlAlert.Text = "alert('车间及产线不能为空！');";
            return;
            if (procOutput != null)
            {
                procOutput.Dispose();
            }
            return;
        }
        else
        {
            if (procOutput != null && procOutput.Rows.Count > 0)
            {
                using (SqlBulkCopy bulckCopy = new SqlBulkCopy(strConn))
                {
                    bulckCopy.DestinationTableName = "BarCodeSys" + ".dbo.note_Mstr_Temp";
                    bulckCopy.ColumnMappings.Add("ntd_temp_id", "ntd_temp_id");
                    bulckCopy.ColumnMappings.Add("note_workshop", "note_workshop");
                    bulckCopy.ColumnMappings.Add("note_rmks", "note_rmks");

                    bulckCopy.ColumnMappings.Add("note_createBy", "note_createBy");
                    bulckCopy.ColumnMappings.Add("note_createName", "note_createName");
                    bulckCopy.ColumnMappings.Add("note_createDate", "note_createDate");

                    try
                    {
                        bulckCopy.WriteToServer(procOutput);

                        #region 导入到数据库

                        SqlParameter[] param = new SqlParameter[6];
                        param[0] = new SqlParameter("@workhome", WorkHome);
                        param[1] = new SqlParameter("@workline", WorkLine);
                        param[2] = new SqlParameter("@UserId", Convert.ToString(Session["uID"]));
                        param[3] = new SqlParameter("@orderno", "");
                        param[4] = new SqlParameter("@remark", txt_remark.Text);
                        param[5] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_note_InsertWorkLog", param);
                        ltlAlert.Text = "alert('日志保存成功！Log Save Success.');";
                        return;

                        #endregion
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('下线汇报失败！\nFail to write to server.');";
                        return;
                    }
                    finally
                    {
                        procOutput.Dispose();
                    }
                }
            }
            else
            {
                return;
            }
        }
        #endregion
    }
    protected void ddl_duty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dropDuty_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindWorkHomeGroup(dropDuty.SelectedIndex);
    }
}