using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class atl_Barcode : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected DataSet getBarCode()
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_selectBarCode";

            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@item", txtNumber.Text.Trim());
            sqlParam[1] = new SqlParameter("@upc", txtUpc.Text.Trim());
            sqlParam[2] = new SqlParameter("@desc", txtDesc.Text.Trim());
            sqlParam[3] = new SqlParameter("@chk", false);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            return null;
        }
    }

    protected void BindData()
    {
        dgCode.DataSource = getBarCode();
        dgCode.DataBind();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void dgCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgCode.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder strb = new System.Text.StringBuilder("SKU,UPC,InnerPackI2of5,MasterPackI2of5\r\n");
        foreach (DataRow row in getBarCode().Tables[0].Rows)
        {
            strb.Append(row[0].ToString() + "," + row[1].ToString() + "," + row[3].ToString() + "," + row[4].ToString() + "\r\n");
        }

        Response.Clear();
        Response.Buffer = false;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=I2of5 Numbers for SZX.csv");
        Response.ContentType = "text/plain";
        this.EnableViewState = false;
        Response.Write(strb.ToString());
        Response.End();
    }
}
