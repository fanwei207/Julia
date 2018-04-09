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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_list_slow4s : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate1.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
            txtDate2.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            BindGridView();
        }
    }

    protected override void BindGridView()
    {
        try
        {
            string strSql = "sp_wo2_selectWorkOrderHoursList2";

            SqlParameter[] sqlParam = new SqlParameter[8];
            sqlParam[0] = new SqlParameter("@date1", txtDate1.Text.Trim());
            sqlParam[1] = new SqlParameter("@date2", txtDate2.Text.Trim());
            sqlParam[2] = new SqlParameter("@part", txtPart.Text.Trim());
            sqlParam[3] = new SqlParameter("@nbr", txtNbr.Text.Trim());
            sqlParam[4] = new SqlParameter("@lot", txtLot.Text.Trim());
            sqlParam[5] = new SqlParameter("@isClosed", chkIsClosed.Checked);
            sqlParam[6] = new SqlParameter("@isReported", chkIsReported.Checked);
            sqlParam[7] = new SqlParameter("@cc", txtCostCenter.Text.Trim());

            DataTable table = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            gvSummary.DataSource = table;
            gvSummary.DataBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtDate1.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('日期 必须指定一个起始日期！');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('日期 格式不正确！');";
                return;
            }
        }

        if (txtDate2.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('日期 必须指定一个起始日期！');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('日期 格式不正确！');";
                return;
            }
        }

        BindGridView();
    }
    protected void gvSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSummary.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string str = "date1=" + txtDate1.Text.Trim() + "&date2=" + txtDate2.Text.Trim() + "&part=" + txtPart.Text.Trim() + "&cc=" + txtCostCenter.Text.Trim()
                        + "&nbr=" + txtNbr.Text.Trim() + "&lot=" + txtLot.Text.Trim() + "&isClosed=" + chkIsClosed.Checked.ToString() + "&isReported=" + chkIsReported.Checked.ToString();
        ltlAlert.Text = "window.open('wo2_list_slow4sExcel.aspx?" + str + "','_blank')";
    }
    protected void gvSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Details")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            this.Redirect("wo2_list_detail4.aspx?ty=slow&s=" + gvSummary.DataKeys[index].Values["wo_site"].ToString() + "&id=" + gvSummary.DataKeys[index].Values["wo_lot"].ToString() + "&pi=" + gvSummary.DataKeys[index].Values["ro_tool"].ToString() + "&cls=" + chkIsClosed.Checked.ToString());
        }
    }
}
