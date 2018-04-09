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

public partial class Admin_userApproveNewList : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@plantCode", Session["plantCode"].ToString());
            param[1] = new SqlParameter("@userNo", txtUserNo.Text.Trim());
            param[2] = new SqlParameter("@userName", txtUserName.Text.Trim());
            param[3] = new SqlParameter("@status", ddlStatus.SelectedItem.Text);
            gv.DataSource = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_userApprove_selectUserInfo", param);
            gv.DataBind();
        }
        catch
        {
            this.Alert("数据获取失败！请联系管理员！");
        }
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text == "已通过" || e.Row.Cells[5].Text == "已申请")
            {
                e.Row.Cells[6].Enabled = false;
            }
            LinkButton linkresume = (LinkButton)e.Row.Cells[4].FindControl("linkresume");
            if (gv.DataKeys[e.Row.RowIndex].Values["ResumePath"].ToString() == "")
            {
                linkresume.Text = "";
            }
        }

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myApply")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string aa = param[1].ToString();
            Response.Redirect("userApproveNew.aspx?userNo=" + param[0].ToString() +"&userName=" +param[1].ToString());
        }
        if (e.CommandName == "myResume")
        {

            ltlAlert.Text = "window.open('" + e.CommandArgument.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') ";

        }
        if (e.CommandName == "myUpload")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string userNo = param[0].ToString();
            string userName = param[1].ToString();
            string deptName = param[2].ToString();
            string workName = param[3].ToString();
            string plantID = param[4].ToString();
            Response.Redirect("userApporveResume.aspx?userNo=" + userNo + "&userName=" + userName + "&deptName=" + deptName + "&roleName=" + workName + "&plantID=" + plantID +"&type=1&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}