using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class upcProgram_barcodeedit : BasePage
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
            sqlParam[3] = new SqlParameter("@chk", chk.Checked);

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

        if (btnSearch.Text == "Search")
        {
            btnAdd.Text = "Add";
            txtUpc.ReadOnly = false;

            dgCode.SelectedIndex = -1;
        }
        else
        {
            txtUpc.ReadOnly = false;
            txtNumber.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtUpc.Text = string.Empty;
            txtMpi.Text = string.Empty;
            txtIpi.Text = string.Empty;

            btnAdd.Text = "Add";
            btnSearch.Text = "Search";

            dgCode.SelectedIndex = -1;
        }

        dgCode.CurrentPageIndex = 0;

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
    protected void dgCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNumber.Text = dgCode.SelectedItem.Cells[0].Text;
        txtDesc.Text = dgCode.SelectedItem.Cells[1].Text;
        txtUpc.Text = dgCode.SelectedItem.Cells[2].Text;
        txtMpi.Text = dgCode.SelectedItem.Cells[4].Text;
        txtIpi.Text = dgCode.SelectedItem.Cells[3].Text;

        btnAdd.Text = "Save";
        btnSearch.Text = "Cancel";
        txtUpc.ReadOnly = true;

        //BindData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
        if (!regex.IsMatch(txtUpc.Text.Trim()))
        {
            ltlAlert.Text = "alert('UPC必须是数字!');";
            return;
        }

        if (txtNumber.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Item Number不能为空!');";
            return;
        }

        int nRet = 0;
        nRet = InsertBarCode(txtNumber.Text.Trim(), txtDesc.Text.Trim(), txtUpc.Text.Trim(), Session["uID"].ToString(), btnAdd.Text);

        if (nRet == 0)
        {
            ltlAlert.Text = "alert('该Item Number与UPC的外销条形码已经存在!')";
        }
        else if (nRet == 1)
        {
            ltlAlert.Text = "alert('所输入的UPC不合法!')";
        }
        else if (nRet == -1)
        {
            ltlAlert.Text = "alert('添加外销条形码失败，请联系管理员!')";
        }
        else
        {
            txtUpc.ReadOnly = false;
            txtNumber.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtMpi.Text = string.Empty;
            txtIpi.Text = string.Empty;

            btnAdd.Text = "Add";

            btnSearch.Text = "Search";

            dgCode.SelectedIndex = -1;

            BindData();
        }
    }
    protected void dgCode_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_deleteBarCode";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@upc", dgCode.Items[e.Item.ItemIndex].Cells[2].Text.Trim());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ;
        }

        txtUpc.ReadOnly = false;
        txtUpc.Text = string.Empty;
        txtNumber.Text = string.Empty;
        txtDesc.Text = string.Empty;
        txtMpi.Text = string.Empty;
        txtIpi.Text = string.Empty;

        btnAdd.Text = "Add";

        btnSearch.Text = "Search";

        dgCode.SelectedIndex = -1;

        BindData();
    }

    private int InsertBarCode(string item, string desc, string upc, string uID, string cmd)
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_insertBarCode";

            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@item", txtNumber.Text.Trim());
            sqlParam[1] = new SqlParameter("@desc", txtDesc.Text.Trim());
            sqlParam[2] = new SqlParameter("@upc", txtUpc.Text.Trim());
            sqlParam[3] = new SqlParameter("@uid", Session["uID"].ToString());
            sqlParam[4] = new SqlParameter("@cmd", btnAdd.Text);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
        }
        catch
        {
            return -1;
        }
    }
}
