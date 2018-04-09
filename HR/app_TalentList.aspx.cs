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
public partial class HR_app_TalentList : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
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
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("app_TalentAddList.aspx");
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        BindData();
    }
}