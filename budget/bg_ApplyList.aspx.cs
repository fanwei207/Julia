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
using adamFuncs;
using BudgetProcess;

public partial class bg_ApplyList : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        { 
            txtYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strDept = txtDept.Text.Trim();
        string strUser = txtApplicant.Text.Trim();
        string strKeyword = txtKeyword.Text.Trim();
        string strPlantID = Convert.ToString(Session["plantCode"]);
        string strYear = txtYear.Text.Trim();
        string strMonth = ddlMonth.SelectedValue.ToString();
        long isShowMe = chkShowMyApply.Checked ? Convert.ToInt64(Session["uID"]) : 0;
        int isAll = chkShowAll.Checked ? 1 : 0;

        gvApply.DataSource = Budget.getApplyList(strDept, strUser, strKeyword, strPlantID, strYear, strMonth, isShowMe, isAll).Tables[0];
        gvApply.DataBind();
    }

    protected void gvApply_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApply.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvApply_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvApply_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strApplyID = string.Empty;

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strApplyID = gvApply.DataKeys[intRow].Value.ToString();
            ltlAlert.Text = "var w=window.open('/budget/bg_ApplyDetail.aspx?aid=" + strApplyID +"&rm=" + DateTime.Now;
            ltlAlert.Text += "','ApplyInfo','menubar=no,scrollbars=yes,resizable=no,width=800,height=500,top=0,left=0'); w.focus();";
        }
        if (e.CommandName.ToString() == "Evaluate")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strApplyID = gvApply.DataKeys[intRow].Value.ToString();
            Response.Redirect("/budget/bg_ApplyEvaluate.aspx?aid=" + strApplyID + "&rm=" + DateTime.Now.ToString(), true);
        }
        if (e.CommandName.ToString() == "Actual")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strApplyID = gvApply.DataKeys[intRow].Value.ToString();
            Response.Redirect("/budget/bg_ApplyActual.aspx?aid=" + strApplyID + "&rm=" + DateTime.Now.ToString(), true);
        }

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        gvApply.PageIndex = 0;
        BindData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Budget/bg_ApplyAdd.aspx", true);
    }

    protected void chkShowMyApply_CheckedChanged(object sender, EventArgs e)
    {
        gvApply.PageIndex = 0;
        BindData();
    }

    protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
    {
        gvApply.PageIndex = 0;
        BindData();
    }
    
}
