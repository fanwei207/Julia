using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Supplier_Supplier_IsNotActiveList : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvList.DataSource = bind(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
            gvList.DataBind();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        gvList.DataSource = bind(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
        gvList.DataBind();
    }
    private DataTable bind(string supplier,string supplierName, string startDate,string endDate)
    {
        string str = "sp_supplier_selectNotActiveSupplierInfo";

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@supplier", supplier);
        param[1] = new SqlParameter("@supplierName", supplierName);
        param[2] = new SqlParameter("@startDate", startDate);
        param[3] = new SqlParameter("@endDate", endDate);


        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dtdown = exportExcelFromPage(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
        StringBuilder title = new StringBuilder("");

        foreach (DataColumn dc in dtdown.Columns)
        {
            title.Append("100^<b>");
            title.Append(dc.ColumnName);
            title.Append("</b>~^");
        }


        if (dtdown != null && dtdown.Rows.Count > 0)
        {
            ExportExcel(title.ToString(), dtdown, false);
        }
    }
    private DataTable exportExcelFromPage(string supplier, string supplierName, string startDate, string endDate)
    {
        string str = "sp_supplier_exportNotActiveInfo";

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@supplier", supplier);
        param[1] = new SqlParameter("@supplierName", supplierName);
        param[2] = new SqlParameter("@startDate", startDate);
        param[3] = new SqlParameter("@endDate", endDate);


        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        gvList.DataSource = bind(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
        gvList.DataBind();
    }
}