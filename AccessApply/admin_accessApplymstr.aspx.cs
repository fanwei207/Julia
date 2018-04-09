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

public partial class AccessApply_admin_accessApplymstr : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["aamId"]))
            {
                txtApplyId.Text = Request.QueryString["aamId"].ToString();
            }
            BindData();
        }
    }

    private void BindData()
    {
        int iApplyId;
        if (txtApplyId.Text.ToString() == string.Empty)
        {
            iApplyId = 0;
        }
        else
        {
            iApplyId = Convert.ToInt32(txtApplyId.Text);
        }
        string strApplyName = txtApplyName.Text.ToString();

        bool idispayOnlyToApprove;

        if (chkb_displayToApprove.Checked == true)
        {
            idispayOnlyToApprove = true;
        }
        else
        {
            idispayOnlyToApprove = false;
        }

        int iCurrentUserId = Convert.ToInt32(Session["uID"]);
        DataSet ds = admin_AccessApply.getAccessApplyMstr(iApplyId, strApplyName, idispayOnlyToApprove, iCurrentUserId);

        gvAccessPro.DataSource = ds;
        gvAccessPro.DataBind();

    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin_accessApplyModule.aspx", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtApplyId.Text.ToString() != string.Empty)
        {
            try
            {
                Convert.ToInt32(txtApplyId.Text.ToString());
            }
            catch
            {
                ltlAlert.Text = "alert('申请序号应该是数字，请确认查询')";
                return;
            }
        }

        BindData();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lnkEdit")).Style.Add("font-weight", "normal");
            ((LinkButton)e.Row.FindControl("lnkDelete")).Style.Add("font-weight", "normal");
            ((LinkButton)e.Row.FindControl("lnkFirstApp")).Style.Add("font-weight", "normal");

            if (gvAccessPro.DataKeys[e.Row.RowIndex][1].ToString() == Convert.ToString(Session["uID"]) && e.Row.Cells[10].Text.ToString() == "&nbsp;"
                  && (gvAccessPro.DataKeys[e.Row.RowIndex][3].ToString() == string.Empty || Convert.ToInt32(gvAccessPro.DataKeys[e.Row.RowIndex][3].ToString()) == 1))
            {
                ((LinkButton)e.Row.FindControl("lnkEdit")).Enabled = true;
                ((LinkButton)e.Row.FindControl("lnkDelete")).Enabled = true;
            }

            if (e.Row.Cells[10].Text.ToString() != "&nbsp;")
            {
                ((LinkButton)e.Row.FindControl("lnkFirstApp")).Text = "已审批";

            }
            else
            {
                if (gvAccessPro.DataKeys[e.Row.RowIndex][2].ToString() == Convert.ToString(Session["uID"]) && e.Row.Cells[10].Text.ToString() == "&nbsp;")
                {
                    ((LinkButton)e.Row.FindControl("lnkFirstApp")).Enabled = true;
                }
            }
        }
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvAccessPro.EditIndex = e.NewEditIndex;

        BindData();
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int iApplyId = Convert.ToInt32(gvAccessPro.DataKeys[e.RowIndex][0].ToString());
        admin_AccessApply.deleteAccessApplymstr(iApplyId);
        admin_AccessApply.deleteAccessApplyDetail(iApplyId);

        BindData();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccessPro.PageIndex = e.NewPageIndex;
        this.BindData();
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAccessPro.EditIndex = -1;
        BindData();
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("admin_accessApplyModule.aspx?aamId=" + e.CommandArgument.ToString() + "&islook=no&istartApprove=no", true);
        }
        if (e.CommandName == "lookApplyProcess")
        {
            Response.Redirect("admin_accessApproveProcess.aspx?aamId=" + e.CommandArgument.ToString() + "&islook=yes&istartApprove=no", true);
        }
        if (e.CommandName == "startApprove")
        {
            Response.Redirect("admin_accessApproveProcess.aspx?aamId=" + e.CommandArgument.ToString() + "&islook=no&istartApprove=yes&approveId=", true);
        }
    }
    protected void chkb_displayToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void chkb_displayUserToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
