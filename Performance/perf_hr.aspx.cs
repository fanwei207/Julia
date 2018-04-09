using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;
using adamFuncs;

public partial class Performance_perf_hr : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           // BindGridView();
            txtend.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txtstart.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-3).ToString("yyyy-MM-dd");
        }
    }
    protected override void BindGridView()
    {
        try
        {
            string strName = "";
            if (ddldept.SelectedValue == "0")
            {
                strName = "sp_perf_selectxianzhang";
            }
            else if (ddldept.SelectedValue == "1")
            {
                strName = "sp_perf_selectbuzhang";
            }
            else if (ddldept.SelectedValue == "2")
            {
                strName = "sp_perf_selectzhongceng";
            }
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@plantcode", dropPlant.SelectedValue.ToString());
            param[1] = new SqlParameter("@startdate", txtstart.Text);
            param[2] = new SqlParameter("@enddate", txtend.Text);
            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        string strName = "";
        if (ddldept.SelectedValue == "0")
        {
            strName = "sp_perf_selectxianzhang";
        }
        else if (ddldept.SelectedValue == "1")
        {
            strName = "sp_perf_selectbuzhang";
        }
        else if (ddldept.SelectedValue == "2")
        {
            strName = "sp_perf_selectzhongceng";
        }
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@plantcode", dropPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@startdate", txtstart.Text);
        param[2] = new SqlParameter("@enddate", txtend.Text);
        DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param);
        DataTable dtExcel = ds.Tables[0];
        string title = "100^<b>部门/产线</b>~^<b>姓名</b>~^<b>总扣分</b>~^<b>部门人数</b>~^<b>处罚</b>~^";
        this.ExportExcel(title, dtExcel, false);
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}