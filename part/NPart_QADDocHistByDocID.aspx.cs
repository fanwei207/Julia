using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class part_NPart_QADDocHistByDocID : BasePage
{
    PC_price pc = new PC_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label2.Text = "Document Name: " + Server.UrlDecode(Request["code"].ToString());
            this.bind();
        }
    }

    private void bind()
    {

        string code = Server.UrlDecode(Request["code"].ToString());
        string typeid = Request["typeID"].ToString();
        string cateid = Request["cateid"].ToString();
        gvDet.DataSource = pc.selectQADDocHistByDocID(code, typeid, cateid);
        gvDet.DataBind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {

        Response.Redirect("/part/NPart_PartVendDocView.aspx?NPartQAD=" + Request["NPartQAD"].ToString() + "&NPartVendor=" + Request["NPartVendor"].ToString());
    }
    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string path = gvDet.DataKeys[e.Row.RowIndex].Values["Path"].ToString();
            string filename = gvDet.DataKeys[e.Row.RowIndex].Values["filename"].ToString();
            if (gvDet.DataKeys[e.Row.RowIndex].Values["filepath"].ToString().Length > 0)
            {
                if (string.IsNullOrEmpty(path))
                {


                    ((LinkButton)e.Row.FindControl("lkbview")).CommandArgument = "/TecDocs/" + Request["typeID"].ToString() + "/" + Request["cateid"].ToString() + "/" + filename;
                }
                else
                {

                    ((LinkButton)e.Row.FindControl("lkbview")).CommandArgument = path + filename;
                }

            }
            else
            {
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
    }
}