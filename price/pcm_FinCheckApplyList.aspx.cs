using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_FinCheckApplyList : BasePage
{
    private PCM_FinCheckApply helper = new PCM_FinCheckApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string itemCode = txtItemCode.Text.Trim();
        string part = txtPart.Text.Trim();
        string applyDate = txtApplyDate.Text.Trim();
        bool pendingToAppr = chkb_displayToApprove.Checked;
        string userId = Session["uID"].ToString();
        string roleId = Session["uRole"].ToString();
   

        gv.DataSource = helper.GetApplyList(itemCode, part, applyDate, pendingToAppr, userId, roleId);
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
            Response.Redirect("pcm_FinCheckApply.aspx?Id=" + id, true);
        }
    }
    protected void chkb_displayToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}