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

public partial class SID_DeclarationReport : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        int Year = Convert.ToInt32(txtYear.Text.Trim());
        int Month = Convert.ToInt32(ddlMonth.SelectedValue);
        bool noZero = chkNoZero.Checked;
        bool isSum = chkIsSum.Checked;

        if (!isSum)
        {
            gvDeclaration.Visible = true;
            gvDeclaration.DataSource = sid.SelectDeclarationReport(Year, Month, noZero, isSum);
            gvDeclaration.DataBind();
            gvDeclarationSum.Visible = false;
            gvDeclarationSum.DataSource = null;

            Session["EXTitle"] = "140^<b>报关发票前缀</b>~^120^<b>报关发票号</b>~^140^<b>报关发票日期</b>~^140^<b>报关发票金额</b>~^";
            Session["EXSQL"] = sid.SelectDeclarationReportExcel(Year, Month, noZero, isSum);
            Session["EXHeader"] = "";
        }
        else
        {
            gvDeclarationSum.Visible = true;
            gvDeclarationSum.DataSource = sid.SelectDeclarationReport(Year, Month, noZero, isSum);
            gvDeclarationSum.DataBind();
            gvDeclaration.Visible = false;
            gvDeclaration.DataSource = null;

            Session["EXTitle"] = "140^<b>报关发票前缀</b>~^140^<b>报关发票金额</b>~^120^<b>报关发票票数</b>~^";
            Session["EXSQL"] = sid.SelectDeclarationReportExcel(Year, Month, noZero, isSum);
            Session["EXHeader"] = "";
        }
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDeclaration_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDeclarationSum_PreRender(object sender, EventArgs e)
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

    protected void gvDeclaration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeclaration.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvDeclarationSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeclarationSum.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void chkNoZero_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void chkIsSum_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
