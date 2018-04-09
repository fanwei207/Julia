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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;


public partial class Bom_Inv : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
                DataBind();
        }
    }

    protected void DataBind()
    {
        DataTable dt = Bom_AccessApply.GetBomInv(Request.QueryString["bom"], Request.QueryString["tobom"], 1);
        if (dt.Rows.Count > 0)
        {
            gv_inv.DataSource = dt;
            gv_inv.DataBind();
        }
        else
        {
            Page.RegisterStartupScript("close ", " <script> window.opener=null;window.top.close() </script> ");
            ltlAlert.Text = "alert('物料无库存信息')";
        }
    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("close ", " <script> window.opener=null;window.top.close() </script> ");
    }

    protected void gv_inv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void gv_inv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_inv.PageIndex = e.NewPageIndex;
    }

    protected void gv_inv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
        }

    }
}
