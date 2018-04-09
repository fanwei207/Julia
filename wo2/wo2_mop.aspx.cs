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
using System.Collections.Generic;
using System.Data.Odbc;

public partial class wo2_wo2_mop : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["mop_orderby"] = "wo2_mop_proc";
            Session["mop_direction"] = "ASC";

            DataGridBind();
        }
    }

    private void DataGridBind()
    {
        try
        {
            string strSql = "sp_wo2_selectMopProc";

            SqlParameter[] parmArray = new SqlParameter[4];
            parmArray[0] = new SqlParameter("@proccode", txtProcCode.Text.Trim());
            parmArray[1] = new SqlParameter("@procname", txtProcName.Text.Trim());
            parmArray[2] = new SqlParameter("@orderby", Session["mop_orderby"]);
            parmArray[3] = new SqlParameter("@direction", Session["mop_direction"]);

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            dgMopProc.DataSource = ds;
            dgMopProc.DataBind();

            ds.Dispose();
        }
        catch
        {
            ;
        }
    }

    protected void dgMopProc_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgMopProc.CurrentPageIndex = e.NewPageIndex;

        DataGridBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtProcCode.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工序代码不能为空!');";
            return;
        }
        else
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,}$");
            if (!regex.IsMatch(txtProcCode.Text.Trim()))
            {
                ltlAlert.Text = "alert('工序代码必须是一串数字序列!');";
                return;
            }
        }

        if (txtProcName.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工序名称不能为空!');";
            return;
        }

        try
        {
            string strSql = "sp_wo2_insertMopProc";

            SqlParameter[] parmArray = new SqlParameter[3];
            parmArray[0] = new SqlParameter("@proccode", txtProcCode.Text.Trim());
            parmArray[1] = new SqlParameter("@procname", txtProcName.Text.Trim());
            parmArray[2] = new SqlParameter("@uID", Session["uID"].ToString().Trim());

            Boolean bRet = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

            if (!bRet)
            {
                txtProcName.Text = String.Empty;

                DataGridBind();
            }
            else
                ltlAlert.Text = "alert('工序代码已经存在!');Form1.txtProcCode.select();";
        }
        catch
        {
            ;
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (txtProcCode.Text.Trim() != String.Empty)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,}$");
            if (!regex.IsMatch(txtProcCode.Text.Trim()))
            {
                ltlAlert.Text = "alert('工序代码必须是一串数字序列');";
                return;
            }
        }

        DataGridBind();
    }
    protected void dgMopProc_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string strSql = "sp_wo2_deleteMopProc";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@proccode", dgMopProc.Items[e.Item.ItemIndex].Cells[0].Text);

            Boolean bRet = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

            if (bRet)
                ltlAlert.Text = "alert('该工序已被别处引用，不允许删除');";

            txtProcCode.Text = String.Empty;
            txtProcName.Text = String.Empty;

            DataGridBind();
        }
        catch
        {
            ;
        }
    }
    protected void dgMopProc_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        Session["mop_orderby"] = e.SortExpression;

        if (Session["mop_direction"] == "ASC")
            Session["mop_direction"] = "DESC";
        else
            Session["mop_direction"] = "ASC";

        DataGridBind();
    }

    protected void dgMopProc_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgMopProc.EditItemIndex = e.Item.ItemIndex;

        DataGridBind();
    }
    protected void dgMopProc_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgMopProc.EditItemIndex = -1;

        DataGridBind();
    }
    protected void dgMopProc_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox txtName = (TextBox)e.Item.Cells[1].FindControl("txtName");

        if (txtName.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工序名称不能为空!');";
            return;
        }

        try
        {
            string strSql = "sp_wo2_updateMopProc";

            SqlParameter[] parmArray = new SqlParameter[3];
            parmArray[0] = new SqlParameter("@proccode", e.Item.Cells[0].Text.Trim());
            parmArray[1] = new SqlParameter("@procname", txtName.Text.Trim());
            parmArray[2] = new SqlParameter("@uID", Session["uID"].ToString().Trim());

            int nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

            dgMopProc.EditItemIndex = -1;

            DataGridBind();
        }
        catch
        {
            ;
        }
    }

    protected void dgMopProc_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "mySop")
        {
            Response.Redirect("wo2_sop.aspx?id=" + e.Item.Cells[0].Text + "&n=" + Server.UrlEncode(((Label)e.Item.Cells[1].FindControl("lblName")).Text));
        }
    }
}
