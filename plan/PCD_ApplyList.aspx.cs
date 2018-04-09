using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_PCD_ApplyList : BasePage
{
    private edi.PCDApply helper = new edi.PCDApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string poNbr = txtPoNbr.Text.Trim();
        string custPart = txtCustPart.Text.Trim();
        string part = txtPart.Text.Trim();
        string applyDate = txtApplyDate.Text.Trim();
        bool pendingToAppr = chkb_displayToApprove.Checked;
        string userId = Session["uID"].ToString();
        string roleId = Session["uRole"].ToString();

        gv.DataSource = helper.GetApplyList(poNbr, custPart, part, applyDate, pendingToAppr, userId, roleId);
        gv.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "look")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string id = gv.DataKeys[index].Values["ApplyId"].ToString();
            string poNbr = gv.DataKeys[index].Values["poNbr"].ToString();
            Response.Redirect("PCD_Apply.aspx?Id=" + id + "&poNbr=" + poNbr, true);
        }
    }
    protected void chkb_displayToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv.DataKeys[e.Row.RowIndex].Values["ApplyBy"].ToString() == Convert.ToString(Session["uID"]))
            {
                ((LinkButton)e.Row.FindControl("lnkDelete")).Enabled = true;
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string applyId = gv.DataKeys[e.RowIndex]["ApplyId"].ToString();
        int result = helper.DeletePCDApply(applyId);
        switch (result)
        {
      
            case 1: ltlAlert.Text = "alert('删除成功！');";
                BindData();
                break;
            case 0: ltlAlert.Text = "alert('删除失败！');";
                break;
            case -1: ltlAlert.Text = "alert('此申请已有审批记录，不能删除')";
                break;
        }
    }
}