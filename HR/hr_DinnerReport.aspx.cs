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
using Wage;

public partial class hr_DinnerReport : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

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
        int Plant = Convert.ToInt32(Session["PlantCode"]);

        gvDinner.DataSource = hr.SelectDinnerInfo(Year, Month, Plant);
        gvDinner.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDinner_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvDinner_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strDate = string.Empty;

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strDate = gvDinner.DataKeys[intRow].Value.ToString();
            Response.Redirect("/hr/hr_DinnerReportDetail.aspx?date=" + strDate + "&rm=" + DateTime.Now.ToString(), true);
        }
    }

    protected void gvDinner_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDinner.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
}
