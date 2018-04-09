using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QC_qc_tracking : BasePage
{
    private QCProgress.QCTracking helper = new QCProgress.QCTracking();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindData();
        }
    }

    private void BindData()
    {
        string nbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string woNbr1 = txtWo1.Text.Trim();
        string woNbr2 = txtWo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        gvlist.DataSource = helper.SelectWoAndPoFromSo(nbr, part, woNbr1, woNbr2, orderDate1, orderDate2);
        gvlist.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }
}