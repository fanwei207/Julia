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
        DataTable dt = Bom_AccessApply.GetBomOnInv(Request.QueryString["bom"], Request.QueryString["tobom"], 1);
        if (dt.Rows.Count > 0)
        {
            gv_oninv.DataSource = dt;
            gv_oninv.DataBind();
        }
        else
        {
            //Response.Redirect("~/BomChange/Bom_Change.aspx");
            Page.RegisterStartupScript("close ", " <script> window.opener=null;window.top.close() </script> ");
            ltlAlert.Text = "alert('物料无在途信息')";
        }
    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("close ", " <script> window.opener=null;window.top.close() </script> ");
    }

    protected void gv_oninv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void gv_oninv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_oninv.PageIndex = e.NewPageIndex;
    }

    protected void gv_oninv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
        }

    }
}
