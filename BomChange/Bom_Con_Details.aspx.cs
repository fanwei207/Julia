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


public partial class Bom_Con_Details : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["DID"])))
            {
                ltlAlert.Text = "alert('无所要查询的数据！');";
            }
            else
            {
                DataBind();
            }
        }
    }

    protected void DataBind()
    {
        //DataTable dt = Bom_AccessApply.GetBomCheckHaveDetails(Convert.ToString(Request.QueryString["DID"]), 1); //int.Parse(Request.QueryString["RAD"].ToString()));
        DataTable dt = Bom_AccessApply.GetBomCheckHaveDetails(Convert.ToString(Request.QueryString["DID"]), Convert.ToInt32(Session["PlantCode"])); //int.Parse(Request.QueryString["RAD"].ToString()));
        gv_HaveCheckInfo.DataSource = dt;
        gv_HaveCheckInfo.DataBind();
    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("close ", " <script> window.opener=null;window.top.close() </script> ");
    }

    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gv_HaveCheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
            ltlAlert.Text = "window.open('conn_detail_list.aspx?mid=" + Server.UrlEncode(gv_HaveCheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') ";

            //Response.Redirect("Bom_Change.aspx?DID=" + Server.UrlEncode(gv_HaveCheckInfo.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + Convert.ToInt32(Session["PlantCode"]));// + "&rm=" + DateTime.Now);
        }
    }

    protected void gvShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_HaveCheckInfo.PageIndex = e.NewPageIndex;
    }

    protected void gvShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
        }

    }
}
