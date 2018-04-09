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
using QADSID;

public partial class SID_HtsList : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strItem = txtItem.Text.Trim();
        string strHts = txtHts.Text.Trim();

        gvHtsList.DataSource = sid.SelectHtsList(strItem, strHts);
        gvHtsList.DataBind();
    }

    protected void gvHtsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHtsList.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHtsList_PreRender(object sender, EventArgs e)
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strItem = txtItem.Text.Trim();
        string strHts = txtHts.Text.Trim();

        DataTable dt = sid.SelectHtsListTb(strItem, strHts);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "160^<b>TCP Item</b>~^160^<b>HTS编号</b>~^280^<b>HTS描述</b>~^";
        this.ExportExcel(title, dt, false, ExcelVersion.Excel2003);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
