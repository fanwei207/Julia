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

public partial class QC_qc_product_unfinished : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));

            txtDate2.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

            GridViewBind();
        }
    }

    private void GridViewBind()
    {

        DataSet _dataset = oqc.GetProductUnfinished(txtOrder.Text.Trim(),
                                            txtID.Text.Trim(),
                                            txtDate.Text.Trim(),
                                            txtDate2.Text.Trim(),
                                            int.Parse(Session["PlantCode"].ToString()));

        ogv.GridViewDataBind(gvProduct, _dataset.Tables[0]);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
}
