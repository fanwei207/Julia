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
using QADSID;

public partial class product_SID_SynStage856Detail : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblInvNoVal.Text = Request.QueryString["InvNo"].ToString().Trim();
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = sid.GetPartDataFromSID(Request.QueryString["InvNo"].ToString());

        gvSynStageList.DataSource = dt;
        gvSynStageList.DataBind();
    }

    protected void gvSynStageList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSynStageList.PageIndex = e.NewPageIndex;
        BindData();
    }
}
