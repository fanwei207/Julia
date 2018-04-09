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

public partial class SID_SID_DeclarationInfo : BasePage
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
        string strShipNo = txtShipNo.Text.Trim();

        gvSID.DataSource = sid.SelectDeclarationInfo(strShipNo);
        gvSID.DataBind();

        Session["EXTitle"] = "120^<b>报关发票号</b>~^120^<b>税务发票号</b>~^120^<b>发票日期</b>~^140^<b>出口核销单号</b>~^60^<b>系列</b>~^400^<b>商品名称</b>~^120^<b>商品代码</b>~^120^<b>数量</b>~^60^<b>单价</b>~^120^<b>金额</b>~^";
        Session["EXSQL"] = sid.SelectDeclarationInfoExcel(strShipNo);
        Session["EXHeader"] = "/SID/ExportSIDDeclarationInfo.aspx^~^";
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSID_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
