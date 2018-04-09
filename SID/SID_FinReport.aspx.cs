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
using QADSID;

public partial class SID_FinReport : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStart.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            txtEnd.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strStart = txtStart.Text.Trim();
        string strEnd = txtEnd.Text.Trim();

        gvFin.DataSource = sid.SelectFinanceReport(strStart, strEnd);
        gvFin.DataBind();

        Session["EXTitle3"] = "50^<b>Flag</b>~^70^<b>Domain</b>~^100^<b>Invoice</b>~^100^<b>Eff Date</b>~^90^<b>Bill To</b>~^90^<b>Sell To</b>~^90^<b>Ship To</b>~^50^<b>Curr</b>~^120^<b>ATL Amount</b>~^120^<b>TCP Amount</b>~^100^<b>Tax No</b>~^100^<b>Invoice2</b>~^";
        Session["EXSQL3"] = sid.SelectFinanceReportExcel(strStart, strEnd);
        Session["EXTitle"] = "50^<b>Flag</b>~^70^<b>Domain</b>~^100^<b>Invoice</b>~^100^<b>Eff Date</b>~^90^<b>Bill To</b>~^90^<b>Sell To</b>~^90^<b>Ship To</b>~^50^<b>Curr</b>~^120^<b>ATL Amount</b>~^120^<b>TCP Amount</b>~^100^<b>Tax No</b>~^100^<b>Invoice2</b>~^";
        Session["EXSQL"] = sid.SelectFinanceReportExcel(strStart, strEnd);
        Session["EXHeader"] = "/public/exportexcel3.aspx^~^";
        Session["EXHeader3"] = "";
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFin_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvFin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFin.PageIndex = e.NewPageIndex;
        BindData();
    }
}
