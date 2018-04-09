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
using System.IO;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class HR_app_ImportTalentList : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCompany.Text = Request.QueryString["company"];
            txtDepartment.Text = Request.QueryString["department"];
            txtProcess.Text = Request.QueryString["process"];
            txtPlantcode.Text = Request.QueryString["plantCode"];
            txtDepartmentID.Text = Request.QueryString["departmentID"];
            txtProcessID.Text = Request.QueryString["processID"];

            txtCompany.Visible = false;
            txtDepartment.Visible = false;
            txtProcess.Visible = false;
            txtPlantcode.Visible = false;
            txtDepartmentID.Visible = false;
            txtProcessID.Visible = false;
            BindEducation();
            BindData();
        }
    }
    /// <summary>
    /// 绑定学历列表
    /// </summary>
    private void BindEducation()
    {
        DataTable dt = GetAllEdu();
        ddlEdu.DataSource = dt;
        ddlEdu.DataBind();
        ddlEdu.Items.Insert(0, new ListItem("-学历-", "0"));
    }
    /// <summary>
    /// 获取所有学历
    /// </summary>
    private DataTable GetAllEdu()
    {
        string sql = "select systemCodeID,systemCodeName  from tcpc0..systemCode where systemCodeTypeID = 4";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private void BindData()
    {
        DataTable dt = GetTalentList(ddlTalentList.SelectedValue.ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable GetTalentList(string TalentListID)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@TalentListID", TalentListID);
        //param[1] = new SqlParameter("@department", department);
        //param[2] = new SqlParameter("@process", process);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetTalentList", param).Tables[0];
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownLoad")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string filePath = gv.DataKeys[index].Values["fpath"].ToString();
            try
            {
                filePath = Server.MapPath(filePath);
                filePath = filePath.Replace("\\", "/");
            }
            catch (Exception)
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        BindData();
    }
    protected void btnImport_Click(object sender, System.EventArgs e)
    {
        int chkNum = 0;
        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chkUsers = (CheckBox)row.FindControl("chkUsers");
            if (chkUsers.Checked)
            {
                chkNum = chkNum + 1;
            }
        }
        if (chkNum == 0)
        {
            ltlAlert.Text = "alert('请选择需要导入的人员!');";
            return;
        }
        if (ImportTalent(chkNum))
        {
            ltlAlert.Text = "alert('导入成功!');";
        }
    }
    private bool ImportTalent(int chkNum)
    {
        int AppRecruitmentNum = chkNum;
        #region 创建临时表
        DataTable procOutput = new DataTable("Talentemp");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "userName";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sex";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "sexID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "education";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "educationID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "graduateSchool";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "professional";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "place";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "placeID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fname";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fpath";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "birthday";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "email";
        procOutput.Columns.Add(column);



        

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "company";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "plantcode";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "department";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "departmentID";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "process";
        procOutput.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "processID";
        procOutput.Columns.Add(column);
        #endregion

        foreach (GridViewRow row1 in gv.Rows)
        {
            CheckBox chkUsers = (CheckBox)row1.FindControl("chkUsers");

            if (chkUsers.Checked)
            {
                DataRow procOutputRow = procOutput.NewRow();

                procOutputRow["userName"] = gv.DataKeys[row1.RowIndex].Values["userName"].ToString();
                procOutputRow["sex"] = gv.DataKeys[row1.RowIndex].Values["sex"].ToString();
                procOutputRow["sexID"] = Convert.ToInt32(gv.DataKeys[row1.RowIndex].Values["sexID"].ToString());
                procOutputRow["education"] = gv.DataKeys[row1.RowIndex].Values["education"].ToString();
                procOutputRow["educationID"] = Convert.ToInt32(gv.DataKeys[row1.RowIndex].Values["educationID"].ToString());
                procOutputRow["graduateSchool"] = gv.DataKeys[row1.RowIndex].Values["graduateSchool"].ToString();
                procOutputRow["professional"] = gv.DataKeys[row1.RowIndex].Values["professional"].ToString();
                procOutputRow["place"] = gv.DataKeys[row1.RowIndex].Values["place"].ToString();
                procOutputRow["placeID"] = Convert.ToInt32(gv.DataKeys[row1.RowIndex].Values["placeID"].ToString());
                procOutputRow["fname"] = gv.DataKeys[row1.RowIndex].Values["fname"].ToString();
                procOutputRow["fpath"] = gv.DataKeys[row1.RowIndex].Values["fpath"].ToString();
                procOutputRow["birthday"] = gv.DataKeys[row1.RowIndex].Values["birthday"].ToString();
                procOutputRow["email"] = gv.DataKeys[row1.RowIndex].Values["email"].ToString();

                procOutputRow["company"] = txtCompany.Text;
                procOutputRow["plantcode"] = Convert.ToInt32(txtPlantcode.Text);
                procOutputRow["department"] = txtDepartment.Text;
                procOutputRow["departmentID"] = Convert.ToInt32(txtDepartmentID.Text);
                procOutputRow["process"] = txtProcess.Text;
                procOutputRow["processID"] = Convert.ToInt32(txtProcessID.Text);
                procOutput.Rows.Add(procOutputRow);
            }
        }
        if (procOutput != null && procOutput.Rows.Count > 0)
        {
            using (SqlBulkCopy bulckCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
            {
                bulckCopy.DestinationTableName = "UsersInformation";
                bulckCopy.ColumnMappings.Add("userName", "userName");
                bulckCopy.ColumnMappings.Add("sex", "sex");
                bulckCopy.ColumnMappings.Add("sexID", "sexID");
                bulckCopy.ColumnMappings.Add("education", "education");
                bulckCopy.ColumnMappings.Add("educationID", "educationID");
                bulckCopy.ColumnMappings.Add("graduateSchool", "graduateSchool");
                bulckCopy.ColumnMappings.Add("professional", "professional");
                bulckCopy.ColumnMappings.Add("place", "place");
                bulckCopy.ColumnMappings.Add("placeID", "placeID");
                bulckCopy.ColumnMappings.Add("fname", "fname");
                bulckCopy.ColumnMappings.Add("fpath", "fpath");
                bulckCopy.ColumnMappings.Add("birthday", "birthday");
                bulckCopy.ColumnMappings.Add("email", "email");

                bulckCopy.ColumnMappings.Add("company", "company");
                bulckCopy.ColumnMappings.Add("plantcode", "plantcode");
                bulckCopy.ColumnMappings.Add("department", "department");
                bulckCopy.ColumnMappings.Add("departmentID", "departmentID");
                bulckCopy.ColumnMappings.Add("process", "process");
                bulckCopy.ColumnMappings.Add("processID", "processID");

                try
                {
                    bulckCopy.WriteToServer(procOutput);
                    if (updateAppRecruitmentNum(AppRecruitmentNum,Request.QueryString["company"], Request.QueryString["department"], Request.QueryString["process"]))
                    {
                        ltlAlert.Text = "alert('保存成功!');";
                    }
                    //if (DeleteTalent())
                    //{
                    //    ltlAlert.Text = "alert('保存成功!');";
                    //}
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
    private bool updateAppRecruitmentNum(int AppRecruitmentNum,string company,string department,string process)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@AppRecruitmentNum", AppRecruitmentNum);
        param[1] = new SqlParameter("@company", company);
        param[2] = new SqlParameter("@department", department);
        param[3] = new SqlParameter("@process", process);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_updateAppRecruitmentNum", param));
    }
    /// <summary>
    /// 删除人才表导出的记录
    /// </summary>
    /// <param name="reportedBy"></param>
    /// <returns></returns>
    private bool DeleteTalent(int reportedBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@reportedBy", reportedBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_deleteWorkOrderReportUpdate", param));
        }
        catch
        {
            return false;
        }
    }
    protected void btnBack_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("app_ResumeList.aspx?App_Company=" + txtCompany.Text + "&App_department=" + txtDepartment.Text + "&App_Process=" + txtProcess.Text + "&App_plantCode=" + txtPlantcode.Text + "&App_departmentID=" + txtDepartmentID.Text + "&App_ProcID=" + txtProcessID.Text);
    }
}