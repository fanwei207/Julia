using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QCProgress;

public partial class QC_qc_productSupplierBasis : BasePage
{
    QC oqc = new QC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbOrder.Text = Request.QueryString["order"].ToString();
            lbLine.Text = Request.QueryString["line"].ToString();
            lbPart.Text = Request.QueryString["part"].ToString();
            lbguest.Text = Request.QueryString["guest"].ToString();
            this.Bind();
        }
    }

    private void Bind()
    {
        string order =  lbOrder.Text;
        string part = lbPart.Text;

        gvlist.DataSource = oqc.selectSuppBasisList(order, part);
        gvlist.DataBind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("qc_product_out.aspx");
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        this.Bind();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "download")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = gvrow.RowIndex;
            string[] param = e.CommandArgument.ToString().Split(',');
            string vend = param[0].ToString();
            string docid = param[1].ToString();
            string path = param[2].ToString();
            string status = param[3].ToString();
            string url = string.Empty;
            if (status == "False")
            {
                if (string.IsNullOrEmpty(path))
                {
                    url = "/TecDocs/Supplier/" + vend + "/" + docid;
                }
                else
                {
                    url = path + docid;
                }
            }
            else
            {
                url = path + gvlist.Rows[index].Cells[2].Text;
            }
            ltlAlert.Text = "var w=window.open('" + url + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
           
        }
    }
}