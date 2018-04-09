using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_selectBasis : System.Web.UI.Page
{
    PCM_price pc = new PCM_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["PQDetID"] != null)
            {
                
                bind();
            }
        }

    }
    protected void gvBasisInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBasisInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    private void bind()
    {

        string IMID = null;

        gvBasisInfo.DataSource = pc.selectBasis(HttpUtility.UrlDecode(Request.QueryString["PQDetID"]), out IMID);

        gvBasisInfo.DataBind();

        lbIMID.Text = IMID;
    }
   
    protected void gvBasisInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvBasisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnView")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }
}