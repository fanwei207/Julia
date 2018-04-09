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

public partial class SID_PackListDetail : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mi"] == null)
            {
                Response.Redirect("/SID/SID_PackList.aspx?rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                BindData();
            }
        }
    }

    protected void BindData()
    {

        //定义参数
        string strMI = Convert.ToString(Request.QueryString["mi"]);
        string strShipDate = Convert.ToString(Request.QueryString["shipdate"]);

        SID_Stage856 stage = sid.SelectPackList(strMI);

        lblInvNoValue.Text = stage.MasterInvoice.ToString();
        lblDestValue.Text = stage.ShipDest.ToString();
        //lblShipDateValue.Text = stage.ShipDate.ToString();
        lblShipDateValue.Text = strShipDate;


        gvPackDetail.DataSource = sid.SelectPackDetail(strMI, strShipDate);
        gvPackDetail.DataBind();
    }

    protected void gvPackDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPackDetail.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPackDetail_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_PackList.aspx?rm=" + DateTime.Now.ToString(), true);
    }
}
