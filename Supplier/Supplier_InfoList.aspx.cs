using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;

public partial class Supplier_Supplier_InfoList : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvList.DataSource = bind(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            gvList.DataBind();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {


        gvList.DataSource = bind(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
        gvList.DataBind();
    }


    private DataTable bind(string supplier, string supplierName, string uID, string uName)
    {
        string str = "sp_supplier_selectSupplierInfo";

        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@supplier", supplier);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@supplierName", supplierName);


        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }


    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        gvList.DataSource = bind(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
        gvList.DataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dtdown = exportExcelFromPage(txtSupplier.Text.Trim(), txtSupplierName.Text.Trim());
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

    private DataTable exportExcelFromPage(string supplierNo, string supplierName)
    {
        string str = "sp_supplier_exportExcelFromPage";

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@supplier", supplierNo);

        param[1] = new SqlParameter("@supplierName", supplierName);


        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbModifiy")
        {
            string supplierNo = e.CommandArgument.ToString();
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;

            string supplierID = gvList.DataKeys[index].Values["supplierID"].ToString();

            Response.Redirect("Supplier_InfoDet.aspx?supplierNo=" + supplierNo + "&supplierID=" + supplierID);
        }
        if (e.CommandName == "view")
        {
            string supplierNo = e.CommandArgument.ToString();
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;

            string supplierID = gvList.DataKeys[index].Values["supplierID"].ToString();
            Response.Redirect("Supplier_InfoView.aspx?supplierNo=" + supplierNo + "&supplierID=" + supplierID + "&pc_mstr=0");
        }
    }


}