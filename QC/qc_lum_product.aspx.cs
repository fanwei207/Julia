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

public partial class qc_lum_product : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate1.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1"));
        }
        else
            ogv.ResetGridView(gvReport);
    }
    protected void bindGridView()
    {
        ogv.GridViewDataBind(gvReport, oqc.GetLumFlaxProduct(txtDate1.Text, txtDate2.Text
                                                                    ,txtNbr1.Text, txtNbr2.Text
                                                                    ,txtLot1.Text, txtLot2.Text
                                                                    ,txtPart1.Text, txtPart2.Text).Tables[0]);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFloder = Server.MapPath("/docs/qc_incoming_flux.xlsx");
        string strImport = "qc_incoming_flux_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.ProductFluxExport(txtDate1.Text, txtDate2.Text
                                    , txtNbr1.Text, txtNbr2.Text
                                    , txtLot1.Text, txtLot2.Text
                                    , txtPart1.Text, txtPart2.Text);


        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d1 = Convert.ToDateTime(txtDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('日期格式不正确!');";
                return;
            }
        }

        if (txtDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d2 = Convert.ToDateTime(txtDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('日期格式不正确!');";
                return;
            }
        }

        bindGridView();
    }
    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReport.PageIndex = e.NewPageIndex;

        bindGridView();
    }
}
