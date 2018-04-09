using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class upcProgram_barcodeconfig : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_selectBarCodeConfig";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@desc", txtDesc.Text.Trim());
            sqlParam[1] = new SqlParameter("@code", txtCode.Text.Trim());

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

            dgCode.DataSource = ds;
            dgCode.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (btnsearch.Text == "Search")
        {
            txtCode.ReadOnly = false;

            btnAdd.Text = "Add";

            dgCode.SelectedIndex = -1;
        }
        else
        {
            txtDesc.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtMax.Text = string.Empty;
            txtMid.Text = string.Empty;
            txtMin.Text = string.Empty;
            btnAdd.Text = "Add";
            btnsearch.Text = "Search";
            txtCode.ReadOnly = false;

            dgCode.SelectedIndex = -1;
        }
        BindData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtCode.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Code不能为空!');";
            return;
        }
        else
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!regex.IsMatch(txtCode.Text))
            {
                ltlAlert.Text = "alert('Code必须是6位数字!');";
                return;
            }
        }

        if (txtMax.Text.Trim() != string.Empty)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!regex.IsMatch(txtMax.Text))
            {
                ltlAlert.Text = "alert('Max必须是数字!');";
                return;
            }
        }

        if (txtMid.Text.Trim() != string.Empty)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!regex.IsMatch(txtMid.Text))
            {
                ltlAlert.Text = "alert('Mid必须是数字!');";
                return;
            }
        }

        if (txtMin.Text.Trim() != string.Empty)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!regex.IsMatch(txtMin.Text))
            {
                ltlAlert.Text = "alert('Min必须是数字!');";
                return;
            }
        }

        if (btnAdd.Text == "Add")
        {
            try
            {
                string strSql = "JDE_Data.dbo.sp_insertBarCodeConfig";

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@desc", txtDesc.Text.Trim());
                sqlParam[1] = new SqlParameter("@code", txtCode.Text.Trim());
                sqlParam[2] = new SqlParameter("@max", txtMax.Text.Trim());
                sqlParam[3] = new SqlParameter("@mid", txtMid.Text.Trim());
                sqlParam[4] = new SqlParameter("@min", txtMin.Text.Trim());
                sqlParam[5] = new SqlParameter("@uID", Session["uID"].ToString());

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                ;
            }
        }
        else
        {
            try
            {
                string strSql = "JDE_Data.dbo.sp_updateBarCodeConfig";

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@desc", txtDesc.Text.Trim());
                sqlParam[1] = new SqlParameter("@code", txtCode.Text.Trim());
                sqlParam[2] = new SqlParameter("@max", txtMax.Text.Trim());
                sqlParam[3] = new SqlParameter("@mid", txtMid.Text.Trim());
                sqlParam[4] = new SqlParameter("@min", txtMin.Text.Trim());
                sqlParam[5] = new SqlParameter("@uID", Session["uID"].ToString());

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                ;
            }
        }

        txtDesc.Text = string.Empty;
        txtCode.Text = string.Empty;
        txtMax.Text = string.Empty;
        txtMid.Text = string.Empty;
        txtMin.Text = string.Empty;
        btnAdd.Text = "Add";
        btnsearch.Text = "Search";
        txtCode.ReadOnly = false;

        dgCode.SelectedIndex = -1;

        BindData();
    }
    protected void dgCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgCode.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    protected void dgCode_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string strSql = "JDE_Data.dbo.sp_deleteBarCodeConfig";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@code", dgCode.Items[e.Item.ItemIndex].Cells[1].Text.Trim());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ;
        }

        txtDesc.Text = string.Empty;
        txtCode.Text = string.Empty;
        txtMax.Text = string.Empty;
        txtMid.Text = string.Empty;
        txtMin.Text = string.Empty;
        btnAdd.Text = "Add";
        txtCode.ReadOnly = false;

        dgCode.SelectedIndex = -1;
        btnsearch.Text = "Search";

        BindData();
    }
    protected void dgCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDesc.Text = dgCode.Items[dgCode.SelectedIndex].Cells[0].Text.ToString();
        txtCode.Text = dgCode.Items[dgCode.SelectedIndex].Cells[1].Text.ToString();
        txtMax.Text = dgCode.Items[dgCode.SelectedIndex].Cells[2].Text.ToString();
        txtMid.Text = dgCode.Items[dgCode.SelectedIndex].Cells[3].Text.ToString();
        txtMin.Text = dgCode.Items[dgCode.SelectedIndex].Cells[4].Text.ToString();

        if (txtDesc.Text == "&nbsp;")
            txtDesc.Text = string.Empty;

        if (txtMax.Text == "&nbsp;")
            txtMax.Text = string.Empty;

        if (txtMid.Text == "&nbsp;")
            txtMid.Text = string.Empty;

        if (txtMin.Text == "&nbsp;")
            txtMin.Text = string.Empty;

        txtCode.ReadOnly = true;

        btnAdd.Text = "Save";
        btnsearch.Text = "Cancel";
    }
}
