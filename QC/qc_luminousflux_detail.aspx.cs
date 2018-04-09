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
using QCProgress;

public partial class QC_qc_luminousflux_detail : BasePage
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
            lblPage.Text = Request.QueryString["page"].ToString().Trim();
            lblGroup.Text = Request.QueryString["group"].ToString().Trim();

            DataTable table = oqc.GetReportSnapByGroup(lblPage.Text.Trim(), lblGroup.Text.Trim());

            lblReceiver.Text = table.Rows[0][1].ToString();
            lblOrder.Text = table.Rows[0][2].ToString();
            lblLine.Text = table.Rows[0][3].ToString();
            lblPart.Text = table.Rows[0][4].ToString();
            lblRcvd.Text = table.Rows[0][6].ToString();

            //bind gridview
            ogv.GridViewDataBind(gvReport, oqc.GetLumFlax(lblLine.Text.Trim(), lblReceiver.Text.Trim()));
        }
        else
            ogv.ResetGridView(gvReport);
    }
    protected void gvReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        FuncErrType errtype = FuncErrType.操作成功;
        string strMsg = "";

        int id = int.Parse(gvReport.DataKeys[e.RowIndex].Value.ToString());
        errtype = oqc.DeleteLumFlax(id, ref strMsg);

        if (errtype != FuncErrType.操作成功) 
        {
            ltlAlert.Text = "alert('" + errtype .ToString()+ "');";
            return;
        }

        ogv.GridViewDataBind(gvReport, oqc.GetLumFlax(lblLine.Text.Trim(),lblReceiver.Text.Trim()));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["page"].ToString() == "100103110")
        {
            if (FormType == "read")
            {
                this.Response.Redirect(string.Format("qc_report_complete.aspx?type=read&ponbr={0}&receiver={1}&line={2}&part={3}", Request.QueryString["ponbr"], Request.QueryString["receiver"], Request.QueryString["line"], Request.QueryString["part"]));
            }
            else
            {
                Response.Redirect("qc_report_complete.aspx");
            }
        }

        else
            Response.Redirect("qc_report.aspx");
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFloder = Server.MapPath("/docs/qc_incoming_flux.xls");
        string strImport = "qc_incoming_flux_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.IncongmingFluxExport(lblLine.Text.Trim(),lblReceiver.Text.Trim());


        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
}
