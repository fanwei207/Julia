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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;

public partial class QAD_qad_bom_associatedDocsReport : BasePage
{
    adamClass chk = new adamClass();
    string begin = string.Empty;
    string end = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txbBeginDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            txbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            BindData();
        }
    }

    protected void BindData()
    {
        txbQad_bak.Text = txbQad.Text;
        txbBeginDate_bak.Text = txbBeginDate.Text;
        txbEndDate_bak.Text = txbEndDate.Text;

        DataTable dt = GetBomDocuments(txbQad.Text.Trim(), txbBeginDate.Text.Trim(), txbEndDate.Text.Trim());
        this.gvBOM.DataSource = dt;
        this.gvBOM.DataBind();
        this.gvBOM.PageIndex = 0;
        txbResultsCount.Text = (dt == null) ? "0" : dt.Rows.Count.ToString();
    }

    protected DataTable GetBomDocuments(string strQad, string strBeginDate, string strEndDate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@beginDate", strBeginDate);
            param[1] = new SqlParameter("@endDate", strEndDate);
            param[2] = new SqlParameter("@qad", strQad);
            param[3] = new SqlParameter("@hasDocs", chkHasDocs.Checked);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_qad_selectBomDocuments", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void gvBOM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvBOM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBOM.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if ((txbBeginDate.Text.Trim() != string.Empty && !IsDate(txbBeginDate.Text.Trim())) || (txbEndDate.Text.Trim() != string.Empty && !IsDate(txbEndDate.Text.Trim())))
        {
            ltlAlert.Text = "alert('出运日期格式不正确！')";
            return;
        }
        BindData();
    }

    protected bool IsDate(string strDate)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(strDate);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (txbResultsCount.Text == "0")
        {
            ltlAlert.Text = "alert('没有可导出的数据！')";
            return;
        }

        ltlAlert.Text = "window.open('/QAD/qad_bom_exportreports.aspx?qad=" + txbQad_bak.Text.Trim() + "&beginDate=" + txbBeginDate_bak.Text.Trim() + "&endDate=" + txbEndDate_bak.Text.Trim() + "&hasDocs=" + chkHasDocs.Checked.ToString() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
    }

    protected void chkHasDocs_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
