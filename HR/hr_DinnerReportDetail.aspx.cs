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
using System.IO;
using adamFuncs;
using Wage;

public partial class hr_DinnerReportDetail : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["date"] == null)
            {
                lblDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }

            lblDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Request.QueryString["date"]));

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        DateTime date = Convert.ToDateTime(Request.QueryString["date"]);
        int Plant = Convert.ToInt32(Session["PlantCode"]);
        bool isChk = chkAll.Checked; 
        string strUserNo = txtUserNo.Text.Trim();

        gvDinnerDetail.DataSource = strUserNo.Length == 0 ? hr.SelectDinnerDetailInfo(date, Plant) : hr.SelectDinnerDetailUserInfo(date, strUserNo, Plant, isChk);
        gvDinnerDetail.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDinnerDetail_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvDinnerDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDinnerDetail.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/hr/hr_DinnerReport.aspx?rm=" + DateTime.Now.ToString(), true);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('/HR/hr_DinnerReportDetailExportExcel.aspx?date=" + Convert.ToDateTime(Request.QueryString["date"]) + "&Plant=" + Session["PlantCode"].ToString() + "&isChk=" + chkAll.Checked + "&strUserNo=" + txtUserNo.Text.Trim() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
    }
}