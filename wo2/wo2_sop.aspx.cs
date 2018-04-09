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

public partial class wo2_wo2_sop : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblProCode.Text = Request.QueryString["id"].ToString();
            lblProName.Text = Request.QueryString["n"].ToString();

            Session["sop_orderby"] = "wo2_sop_proc";
            Session["sop_direction"] = "ASC";

            DataGridBind();
        }
    }

    private void DataGridBind()
    {
        try
        {
            string strSql = "sp_wo2_selectSopProc";

            SqlParameter[] parmArray = new SqlParameter[5];
            parmArray[0] = new SqlParameter("@mopid", Request.QueryString["id"].ToString());
            parmArray[1] = new SqlParameter("@proccode", txtProcCode.Text.Trim());
            parmArray[2] = new SqlParameter("@procname", txtProcName.Text.Trim());
            parmArray[3] = new SqlParameter("@orderby", Session["sop_orderby"]);
            parmArray[4] = new SqlParameter("@direction", Session["sop_direction"]);

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            dgSopProc.DataSource = ds;
            dgSopProc.DataBind();

            ds.Dispose();
        }
        catch
        {
            ;
        }
    }

    protected void dgSopProc_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgSopProc.CurrentPageIndex = e.NewPageIndex;

        DataGridBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtProcCode.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工位代码不能为空!');";
            return;
        }
        else
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,}$");
            if (!regex.IsMatch(txtProcCode.Text.Trim()))
            {
                ltlAlert.Text = "alert('工位代码必须是一串数字序列!');";
                return;
            }
        }

        if (txtProcName.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工位名称不能为空!');";
            return;
        }

        if (txtRate.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工位系数不能为空!');";
            return;
        }
        else
        {
            try
            {
                Decimal d = Convert.ToDecimal(txtRate.Text.Trim());

                if (d <= 0)
                {
                    ltlAlert.Text = "alert('工位系数必须大于0!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('工位系数可以是整数也可以是小数!');";
                return;
            }
        }

        try
        {
            string strSql = "sp_wo2_insertSopProc";

            SqlParameter[] parmArray = new SqlParameter[5];
            parmArray[0] = new SqlParameter("@mopcode", Request.QueryString["id"].ToString());
            parmArray[1] = new SqlParameter("@proccode", txtProcCode.Text.Trim());
            parmArray[2] = new SqlParameter("@procname", txtProcName.Text.Trim());
            parmArray[3] = new SqlParameter("@rate", txtRate.Text.Trim());
            parmArray[4] = new SqlParameter("@uID", Session["uID"].ToString().Trim());

            Boolean bRet = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

            if (!bRet)
            {
                txtProcCode.Text = String.Empty;
                txtProcName.Text = String.Empty;
                txtRate.Text = String.Empty;

                DataGridBind();
            }
            else
                ltlAlert.Text = "alert('工位代码已经存在!');Form1.txtProcCode.select();";
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
                ltlAlert.Text = "alert('工位代码必须是一串数字序列');";
                txtProcCode.Text = String.Empty;
            }
        }

        DataGridBind();
    }

    protected void dgSopProc_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string strSql = "sp_wo2_deleteSopProc";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@id", dgSopProc.Items[e.Item.ItemIndex].Cells[7].Text);

            int nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

            txtProcCode.Text = String.Empty;
            txtProcName.Text = String.Empty;
            txtRate.Text = String.Empty;

            DataGridBind();
        }
        catch
        {
            ;
        }
    }

    protected void dgSopProc_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        Session["sop_orderby"] = e.SortExpression;

        if (Session["sop_direction"] == "ASC")
            Session["sop_direction"] = "DESC";
        else
            Session["sop_direction"] = "ASC";

        DataGridBind();
    }

    protected void dgSopProc_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgSopProc.EditItemIndex = e.Item.ItemIndex;

        DataGridBind();
    }

    protected void dgSopProc_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgSopProc.EditItemIndex = -1;

        DataGridBind();
    }

    protected void dgSopProc_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox txtName = (TextBox)e.Item.Cells[1].FindControl("txtName");
        TextBox tRate = (TextBox)e.Item.Cells[1].FindControl("txtRate");

        if (txtName.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工位名称不能为空!');";
            return;
        }

        if (tRate.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('工位系数不能为空!');";
            return;
        }
        else
        {
            try
            {
                Decimal d = Convert.ToDecimal(tRate.Text.Trim());

                if (d <= 0)
                {
                    ltlAlert.Text = "alert('工位系数必须大于0!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('工位系数可以是整数也可以是小数!');";
                return;
            }
        }

        try
        {
            string strSql = "sp_wo2_updateSopProc";

            SqlParameter[] parmArray = new SqlParameter[4];
            parmArray[0] = new SqlParameter("@sopid", e.Item.Cells[7].Text.Trim());
            parmArray[1] = new SqlParameter("@procname", txtName.Text.Trim());
            parmArray[2] = new SqlParameter("@procrate", tRate.Text.Trim());
            parmArray[3] = new SqlParameter("@uID", Session["uID"].ToString().Trim());

            int nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

            dgSopProc.EditItemIndex = -1;

            DataGridBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("/wo2/wo2_mop.aspx?rt=" + DateTime.Now.ToString());
    }
}
