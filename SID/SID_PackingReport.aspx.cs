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

public partial class SID_PackingList1 : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
    PackingReport pack = new PackingReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        
        string strPK = txtSysPKNo.Text.Trim();
        //string strRef = txtSysPKRef.Text.Trim();
        string strNbr = txtShipNo.Text.Trim();
        //string strDomain = txtDomain.Text.Trim();

        //Add By Shanzm 2011.02.14
        string strShipDate1 = txtShipDate1.Text.Trim();
        string strShipDate2 = txtShipDate2.Text.Trim();



        string pk = txtSysPKNo.Text.Trim();
        string nbr = txtShipNo.Text.Trim();
        string shipdate1 = txtShipDate1.Text.ToString();
        string shipdate2 = txtShipDate2.Text.ToString();
        string cust = txt_cust.Text.Trim();
        string lastcust = txt_lastcust.Text.Trim();
        string bldate1 = txt_bldate1.Text.ToString();
        string bldate2 = txt_bldate2.Text.ToString();
        string packingdate1 = txt_packingdate1.Text.ToString();
        string packingdate2 = txt_packingdate2.Text.ToString();

        //DataTable dt = pack.PackingExport(pk, nbr, shipdate1, shipdate2, cust, lastcust, bldate1, bldate2);

        gvPacking.DataSource = pack.PackingExport(pk, nbr, shipdate1, shipdate2, cust, lastcust, bldate1, bldate2, packingdate1, packingdate2);//pack.SelectSIDList(strPK, "", strNbr, strDomain, strShipDate1, strShipDate2);
        gvPacking.DataBind();
    }


    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPacking_PreRender(object sender, EventArgs e)
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
        if (txtShipDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d1 = Convert.ToDateTime(txtShipDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('出运日期格式不正确！');";
                return;
            }
        }

        if (txtShipDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d2 = Convert.ToDateTime(txtShipDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('出运日期格式不正确！');";
                return;
            }
        }

        BindData();
    }

    protected void gvPacking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPacking.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvPacking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Trim() == string.Empty)
            {
                e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(e.Row.Cells[5].Text));
            }
        }
    }

    protected void gvPacking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gvPacking.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
            Response.Redirect("/SID/SID_PackingDoc.aspx?type=temp", true);
            //Response.Redirect("SID_PackingDoc.aspx?DID=" + Server.UrlEncode(gvPacking.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&rm=" + DateTime.Now);
        }
    }

    protected void btn_packingexport_Click(object sender, EventArgs e)
    {
        string pk = txtSysPKNo.Text.Trim();
        string nbr = txtShipNo.Text.Trim();
        string shipdate1 = txtShipDate1.Text.ToString();
        string shipdate2 = txtShipDate2.Text.ToString();
        string cust = txt_cust.Text.Trim();
        string lastcust = txt_lastcust.Text.Trim();
        string bldate1 = txt_bldate1.Text.ToString();
        string bldate2 = txt_bldate2.Text.ToString();
        string packingdate1 = txt_packingdate1.Text.ToString();
        string packingdate2 = txt_packingdate2.Text.ToString();

        DataTable dt = pack.PackingExport(pk, nbr, shipdate1, shipdate2, cust, lastcust, bldate1, bldate2, packingdate1, packingdate2);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "80^<b>出运单号</b>~^110^<b>系统货运单号</b>~^70^<b>出厂日期</b>~^70^<b>出运日期~^70^<b>发票日期</b>~^100^<b>装箱地点</b>~^110^<b>物料编码</b>~^200^<b>客户物料</b>~^70^<b>出运套数</b>~^70^<b>出运只数</b>~^100^<b>销售单号</b>~^40^<b>行号</b>~^100^<b>ATL订单号</b>~^100^<b>TCP订单号</b>~^100^<b>客户订单号</b>~^40^<b>单位</b>~^70^<b>系统单位</b>~^70^<b>定价日期</b>~^70^<b>Price0</b>~^70^<b>Price1</b>~^70^<b>Amt1</b>~^70^<b>Price2</b>~^70^<b>Amt2</b>~^70^<b>Price3</b>~^70^<b>Amt3</b>~^80^<b>发票号</b>~^100^<b>客户</b>~^150^<b>客户名称</b>~^100^<b>最终客户</b>~^150^<b>最终客户名称</b>~^90^<b>发票打印日期</b>~^40^<b>域</b>~^80^<b>提单日期</b>~^";
        this.ExportExcel(title, dt, false);
    }
}
