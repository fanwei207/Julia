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
using System.IO;
using QCProgress;

public partial class QC_qc_product_lum_det : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected string FormType
    {
        get
        {
            if (Request.QueryString["type"] != null)
            {
                return Request.QueryString["type"];
            }
            else
            {
                return "";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblNbr.Text = Request.QueryString["nbr"].ToString();
            lblLot.Text = Request.QueryString["lot"].ToString();
            lblRcvd.Text = Request.QueryString["rcvd"].ToString();
            lblPart.Text = Request.QueryString["part"].ToString();
            if (FormType == "read")
            {
                btnDelete.Visible = false;
            }

            ogv.GridViewDataBind(gvReport, oqc.GetProductLumFlax(lblNbr.Text.Trim(), lblLot.Text.Trim(), Convert.ToBoolean(Request.QueryString["tcp"].ToString())));
        }
        else
            ogv.ResetGridView(gvReport);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        if (!Convert.ToBoolean(Request.QueryString["tcp"].ToString()))
        {
            if (FormType == "read")
            {
                this.Response.Redirect("qc_product.aspx?type=read&wolot=" + Request.QueryString["lot"]);
            }
            else
            {
                this.Response.Redirect("qc_product.aspx");
            }
        }
        else
        {
            if (FormType == "read")
            {
                this.Response.Redirect("qc_product_tcp.aspx?type=read&wolot=" + Request.QueryString["lot"]);
            }
            else
            {
                this.Response.Redirect("qc_product_tcp.aspx");
            }
        }

}
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFloder = Server.MapPath("/docs/qc_incoming_flux.xls");
        string strImport = "qc_incoming_flux_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.ProductFluxExport(lblNbr.Text.Trim(), lblLot.Text.Trim(), Convert.ToBoolean(Request.QueryString["tcp"].ToString()));


        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        FuncErrType errtype = FuncErrType.²Ù×÷³É¹¦;
        string strMsg = "";

        foreach (GridViewRow row in gvReport.Rows)
        {
            CheckBox chk = (CheckBox)row.Cells[0].FindControl("chk");

            if (chk.Checked)
            {
                int id = int.Parse(gvReport.DataKeys[row.RowIndex].Value.ToString());
                errtype = oqc.DeleteProdcutLumFlax(id, ref strMsg);
            }
        }

        ogv.GridViewDataBind(gvReport, oqc.GetProductLumFlax(lblNbr.Text.Trim(), lblLot.Text.Trim(), Convert.ToBoolean(Request.QueryString["tcp"].ToString())));
    }
}
