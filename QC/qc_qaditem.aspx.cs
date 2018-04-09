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
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class qc_qaditem : BasePage
{
    adamClass adam = new adamClass();
    QC oqc = new QC();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void BindGrid()
    {
        DataTable dt = GetQadItems();

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.dg.DataSource = dt;
            this.dg.DataBind();
            int columnCount = dg.Rows[0].Cells.Count;
            dg.Rows[0].Cells.Clear();
            dg.Rows[0].Cells.Add(new TableCell());
            dg.Rows[0].Cells[0].ColumnSpan = columnCount;
            dg.Rows[0].Cells[0].Text = "没有记录";
            dg.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

            btnExport.Enabled = false;
        }
        else
        {
            btnExport.Enabled = true;

            this.dg.DataSource = dt;
            this.dg.DataBind();
        }
    }

    protected DataTable GetQadItems()
    {
        try
        {
            string strSql = "sp_QC_selectQadItems";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@item", txtItem.Text);
            sqlParam[1] = new SqlParameter("@part", txtPart.Text);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
    protected void dg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg.PageIndex = e.NewPageIndex;

        BindGrid();
    }
    protected void dg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Attributes["class"] = "OmittedWord";
        }
    }
}
