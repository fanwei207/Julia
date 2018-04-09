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

public partial class QC_qc_flux_det :  BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("100105140", "±à¼­ÐÅÏ¢È¨ÏÞ");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();

            btnClose.Attributes.Add("onclick", "window.close();");

            checkAuth();
        }
    }

    private void checkAuth()
    {
        if (!this.Security["100105140"].isValid)
        {

            gvBasisInfo.Columns[2].Visible = false;
        }
    }

    private void GridViewBind()
    {
        int ID = int.Parse(Request.QueryString["id"].ToString());

        DataTable table = oqc.SelectFluxDetail(ID);

        ogv.GridViewDataBind(gvReport, table);

        gvBasisInfoBind();


        table.Dispose();
    }

    private void gvBasisInfoBind()
    {
        DataTable dt = oqc.selectQcFluxBasisList(Request.QueryString["id"].ToString());
        if (dt.Rows.Count == 0)
        {
            gvBasisInfo.Visible = false;
        }
        else
        {
            gvBasisInfo.Visible = true;
            gvBasisInfo.DataSource = dt;
            gvBasisInfo.DataBind();
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                id = gvReport.PageIndex * 20 + id;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
    }
    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReport.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void gvReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = int.Parse(gvReport.DataKeys[e.RowIndex].Value.ToString());

        if (oqc.DeleteFluxDetail(ID))
            ltlAlert.Text = "alert('É¾³ý³É¹¦');";
        else
            ltlAlert.Text = "alert('É¾³ýÊ§°Ü');";

        GridViewBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFloder = Server.MapPath("../docs/qc_incoming_flux.xls");
        string strImport = "qc_incoming_flux_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.SampleFluxExport(int.Parse(Request.QueryString["id"].ToString()));


        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void gvBasisInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBasisInfo.PageIndex = e.NewPageIndex;
        gvBasisInfoBind();
    }
    protected void gvBasisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnView")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        
        }
        if (e.CommandName == "lkbtndelete")
        {
            if (oqc.updateQCFluxBasis(e.CommandArgument.ToString(),Session["uID"].ToString(),Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('É¾³ý³É¹¦£¡');";
                gvBasisInfoBind();

            }
            else
            {

                ltlAlert.Text = "alert('É¾³ýÊ§°Ü£¡');";
            }
        }
    }
}
