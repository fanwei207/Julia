using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class part_NPart_PartVendDocView : BasePage
{

    PC_price pc = new PC_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.gvbind();
            lbQAD.Text =  HttpUtility.UrlDecode(Request["NPartQAD"].ToString());
            lbVendor.Text = HttpUtility.UrlDecode(Request["NPartVendor"].ToString());
        
        }
    }

    private void gvbind()
    {
        string QAD = HttpUtility.UrlDecode(Request["NPartQAD"].ToString()); ;
        string vendor = HttpUtility.UrlDecode(Request["NPartVendor"].ToString());
        gvDet.DataSource = pc.selectDocByPartAndVendor(QAD, vendor);
        gvDet.DataBind();
    }
    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            int level = 0;
            int.TryParse( gvDet.DataKeys[e.Row.RowIndex].Values["Level"].ToString(),out level);

            if (level >= 0)
            {
                string path = gvDet.DataKeys[e.Row.RowIndex].Values["virPath"].ToString();
                string filename = gvDet.DataKeys[e.Row.RowIndex].Values["filename"].ToString();
                string typeid = gvDet.DataKeys[e.Row.RowIndex].Values["typeid"].ToString();
                string id = gvDet.DataKeys[e.Row.RowIndex].Values["id"].ToString();
                string cateid = gvDet.DataKeys[e.Row.RowIndex].Values["cateid"].ToString();
                if (string.IsNullOrEmpty(path))
                {


                    ((LinkButton)e.Row.FindControl("lkbview")).CommandArgument = "/TecDocs/" + typeid + "/" + cateid + "/" + filename;
                }
                else
                {

                    ((LinkButton)e.Row.FindControl("lkbview")).CommandArgument = path + filename;
                }
                int hiscnt = 0;
                int.TryParse(gvDet.DataKeys[e.Row.RowIndex].Values["hiscnt"].ToString(), out hiscnt);
                if (hiscnt > 0)
                {
                    string name = gvDet.DataKeys[e.Row.RowIndex].Values["name"].ToString().Trim();

                    ((LinkButton)e.Row.FindControl("lkbPvar")).CommandArgument = "/part/NPart_QADDocHistByDocID.aspx?code=" + Server.UrlEncode(name) + "&typeid=" + typeid + "&cateid=" + cateid + "&id=" + id
                        + "&NPartQAD=" + Request["NPartQAD"].ToString() + "&NPartVendor=" + Request["NPartVendor"].ToString();

                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lkbPvar")).Text = "";
                }
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lkbPvar")).Text = "";
                ((LinkButton)e.Row.FindControl("lkbview")).Text = "";
            }

            
        }
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
        if (e.CommandName == "old")
        {
             Response.Redirect(e.CommandArgument.ToString());
            //ltlAlert.Text = " $.window('review', '70%', '80%',"+e.CommandArgument.ToString() +", '', true);";
        
        }

        
    }
}