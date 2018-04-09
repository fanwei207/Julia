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

public partial class PM_HeaderList : BasePage
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
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@plantCode", dropCompany.SelectedValue);
            param[1] = new SqlParameter("@userNo", txtUserNo.Text.Trim());
            param[2] = new SqlParameter("@userName", txtUserName.Text.Trim());
            param[3] = new SqlParameter("@apprDate", txtApprDate.Text.Trim());
            param[4] = new SqlParameter("@isHandle", isHandle.Checked);
            param[5] = new SqlParameter("@uID", Session["uID"]);

            gv.DataSource = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_userApprove_selectUserApproveList", param);
            gv.DataBind();
            if (isHandle.Checked == false)
            {
                gv.Columns[10].Visible = false;

            }
            else
            {
                gv.Columns[10].Visible = true;
            }
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtApprDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtApprDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }

        BindData();
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myHandle")
        {
            Response.Redirect("userApproveHandel.aspx?mid=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "myDetail")
        {
            ltlAlert.Text = "window.open('UserApproveDetailList.aspx?mid=" +  e.CommandArgument.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') ";
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
            Response.Redirect("userApporveResume.aspx?userNo=" + userNo + "&userName=" + userName + "&deptName=" + deptName + "&roleName=" + workName + "&plantID=" + plantID + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton linkresume = (LinkButton)e.Row.Cells[5].FindControl("linkresume");
            if (gv.DataKeys[e.Row.RowIndex].Values["ResumePath"].ToString() == "")
            {
                linkresume.Text = "";
            }
            if (gv.DataKeys[e.Row.RowIndex].Values["isCreator"].ToString() == "是")
            {
                e.Row.Cells[10].Enabled = false;
            }

        }
    }
    protected void isHandle_CheckedChanged(object sender, EventArgs e)
    {
        if (txtApprDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtApprDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }

        BindData();
    }
}
