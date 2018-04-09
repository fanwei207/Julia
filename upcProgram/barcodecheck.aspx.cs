using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class upcProgram_barcodecheck : BasePage 
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtUpc.Text.Trim() == string.Empty)
            return;

        DataTable table = getBarCode().Tables[0];

        if (table.Rows.Count == 0)
        {
            txtDesc.Text = string.Empty;
            txtNumber.Text = string.Empty;
            txtIpi.Text = string.Empty;
            txtMpi.Text = string.Empty;

            ltlAlert.Text = "alert('ÌõÂë²»´æÔÚ!');";
            return;
        }
        else
        {
            txtDesc.Text = table.Rows[0][2].ToString();
            txtNumber.Text = table.Rows[0][0].ToString();
            txtIpi.Text = table.Rows[0][3].ToString();
            txtMpi.Text = table.Rows[0][4].ToString();
        }

        table.Dispose();
    }
    protected void txtUpc_TextChanged(object sender, EventArgs e)
    {
        this.btnsearch_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtUpc.Text = string.Empty;
        txtDesc.Text = string.Empty;
        txtNumber.Text = string.Empty;
        txtIpi.Text = string.Empty;
        txtMpi.Text = string.Empty;
    }
}
