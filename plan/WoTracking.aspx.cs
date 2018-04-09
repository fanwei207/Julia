using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_WoTracking : System.Web.UI.Page
{
    private edi.OrderTracking helper = new edi.OrderTracking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string soNbr = Request.QueryString["soNbr"];
        string part = Request.QueryString["part"];
        string line = Request.QueryString["line"];

        gvlist.DataSource = helper.GetWoList(soNbr, part, line);
        gvlist.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }
}