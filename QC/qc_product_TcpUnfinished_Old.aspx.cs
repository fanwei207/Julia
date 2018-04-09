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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;

public partial class QC_qc_product_unfinished_Old : BasePage
{
    adamClass adam = new adamClass();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now.Year + "-" + DateTime.Now.Month + "-01");

            txtDate2.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

            GridViewBind();
        }
    }

    private void GridViewBind()
    {

        DataTable dt = GetProductTcpUnfinished(txtOrder.Text.Trim(),
                                            txtID.Text.Trim(),
                                            txtDate.Text.Trim(),
                                            txtDate2.Text.Trim(),
                                            chkIsChecked.Checked);

        ogv.GridViewDataBind(gvProduct, dt);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtDate.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('入库日期格式不正确!')";
            }
        }

        if (txtDate2.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('入库日期格式不正确!')";
            }
        }

        GridViewBind();
    }

    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        GridViewBind();
    }

    protected DataTable GetProductTcpUnfinished(string nbr, string lot, string date1, string date2, bool uncheck)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@lot", lot);
        param[2] = new SqlParameter("@date1", date1);
        param[3] = new SqlParameter("@date2", date2);
        param[4] = new SqlParameter("@unCheck", uncheck);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_QC_selectProductTcpUnfinished", param).Tables[0];
    }

    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "check")
        {
            Response.Redirect("qc_product.aspx?ID=TCP&TID=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now, true);
        }
    }

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text.Trim() == "0")
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (txtDate.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('入库日期格式不正确!')";
                return;
            }
        }

        if (txtDate2.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('入库日期格式不正确!')";
                return;
            }
        }

        ltlAlert.Text = "window.open('qc_product_TcpUnfinishedExcel.aspx?nbr=" + txtOrder.Text.Trim() + "&lot=" + txtID.Text.Trim() + "&d1=" + txtDate.Text.Trim() + "&d2=" + txtDate2.Text.Trim() + "&uc=" + chkIsChecked.Checked + "', '_blank');";
    }
}
