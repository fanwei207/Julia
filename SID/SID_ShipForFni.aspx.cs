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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using QADSIDFNI;


public partial class SID_ShipForFni : BasePage
{
    SIDFNI sid = new SIDFNI();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            //验货日期维护
            if (this.Security["550000010"].isValid)
            {
                /*
                    如果该用户即有出运单维护权限，也有验货日期的权限，则前者将会被覆盖，既，即便已报关的处于可编辑状态，但也无法保存头信息，而只能进行验货设置
                 */

                //不是管理员的，出运单维护的功能也禁用掉
                if (Convert.ToInt32(Session["uID"]) != 13)
                {
                    gvShip.Columns[15].Visible = false;
                }
            }
            gvShip.Columns[13].Visible = false;
            gvShip.Columns[21].Visible = false;
            gvShip.Columns[22].Visible = false;
            //if (!this.Security["550000006"].isValid)
            //{
            //    gvShip.Columns[22].Visible = false;
            //}
            txtcreated.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
            txtcreated1.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            BindData();
        }
    }

    protected void BindData()
    {
        String strRad = "";

        strRad = "1";


       // DataTable dt = sid.GetShip();
       DataTable dt = sid.GetShip(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtcreated.Text.Trim(), txtcreated1.Text.Trim(),strRad);

        //if (dt.Rows.Count == 0)
        //{
        //    dt.Rows.Add(dt.NewRow());

        //}
        gvShip.DataSource = dt;
        gvShip.DataBind();


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        BindData();

        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = ""; 
        txtVia.Text = "";
        txtCtype.Text  = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";

    }

    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
          if (gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
          {
              ltlAlert.Text = "alert('此行是空行！');";
              return;
          }

          Response.Redirect("SID_shipdetailForFNI.aspx?DID=" + Server.UrlEncode(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + 2 + "&rm=" + DateTime.Now);
        }
        else if (e.CommandName.ToString() == "Del1")
        {
            if (this.Security["550000002"].isValid)
            {
                ltlAlert.Text = "alert('没有删除的权限！');";
                return;
            }


            Int32 IErr = 0;

            IErr = sid.DelShipData(Convert.ToString(Session["uID"]), gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim());
            if (IErr < 0)
            {
                ltlAlert.Text = "alert('删除失败！');";
                return;
            }
            else
            {
                BindData();
            }
        }
        else if (e.CommandName.ToString() == "Edit1")
        {
            if (this.Security["550000002"].isValid)
            {
                ltlAlert.Text = "alert('没有维护的权限！');";
                return;
            }

            int index = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
            txtPK.Text = gvShip.Rows[index].Cells[0].Text.Trim();
            txtPKref.Text = gvShip.Rows[index].Cells[1].Text.Trim();
            
            if (gvShip.Rows[index].Cells[1].Text.Trim() == "&nbsp;")
            {
                txtPKref.Text = "";
            }

            txtnbr.Text = gvShip.Rows[index].Cells[2].Text.Trim();
            txtVia.Text = gvShip.Rows[index].Cells[3].Text.Trim();
            txtCtype.Text = Server.HtmlDecode( gvShip.Rows[index].Cells[4].Text.Trim());
            txtsite.Text = gvShip.Rows[index].Cells[5].Text.Trim();

            txtShipDate.Text = gvShip.Rows[index].Cells[6].Text.Trim();
            if (gvShip.Rows[index].Cells[6].Text.Trim() == "&nbsp;")
            {
                txtShipDate.Text = "";
            }

            txtOutDate.Text = gvShip.Rows[index].Cells[7].Text.Trim();
            if (gvShip.Rows[index].Cells[7].Text.Trim() == "&nbsp;")
            {
                txtOutDate.Text = "";
            }

            txtdomain.Text = gvShip.Rows[index].Cells[16].Text.Trim();
            txtshipto.Text = gvShip.Rows[index].Cells[17].Text.Trim();
            if (gvShip.Rows[index].Cells[16].Text.Trim() == "&nbsp;")
            {
                txtshipto.Text = "";
            }
        
        }
        else if (e.CommandName == "Confirm1")
        {
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ORG1", Session["uID"].ToString());

            BindData();
        }
        else if (e.CommandName == "Confirm2")
        {
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ORG2", Session["uID"].ToString());

            BindData();
        }
        else if (e.CommandName == "Confirm3")
        {
            string isqcstatus = "";
            int index = ((GridViewRow)((Button)e.CommandSource).Parent.Parent).RowIndex;
            if (gvShip.Rows[index].Cells[22].Text.Trim() == "放行")
            {
                isqcstatus = "NQC";
            }
            else
            {
                isqcstatus = "ISQC";
            }
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), isqcstatus, Session["uID"].ToString());

            BindData();
        }
    }

    protected void gvShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShip.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.Security["550000003"].isValid)
            {
                e.Row.Cells[21].Enabled = false;
            }

            if (this.Security["550001000"].isValid)
            {
                e.Row.Cells[22].Enabled = false;
            }
            //Add By WLW 2014-05-12
            if (!this.Security["550000005"].isValid)
            {
                e.Row.Cells[22].Enabled = false;
            }
            if (!this.Security["550000004"].isValid)
            {
                e.Row.Cells[21].Enabled = false;
            }

        }

    }

    protected void btn_exportexcel_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(txtnbr.Text))
        //{
        //    this.Alert("请输入出运单号！");
        //    return;
        //}
        String strRad = "";

        strRad = "1";

        DataTable dt = sid.SidExportExcel(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtcreated.Text.Trim(), txtcreated1.Text.Trim(), strRad);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "80^<b>出运单号</b>~^110^<b>系统货运单号</b>~^70^<b>出运日期</b>~^120^<b>出厂日期</b>~^80^<b>装箱地点</b>~^110^<b>序号</b>~^110^<b>物料编码</b>~^200^<b>客户物料</b>~^70^<b>出运套数</b>~^70^<b>出运只数</b>~^70^<b>箱数</b>~^150^<b>备注</b>~^100^<b>销售单号</b>~^40^<b>行号</b>~^100^<b>ATL订单号</b>~^100^<b>TCP订单号</b>~^120^<b>完工日期</b>~^120^<b>货物抵达日期</b>~^";
        this.ExportExcel(title, dt, false);
    }
    protected void btn_exportexcelByTemp_Click(object sender, EventArgs e)
    {
        string strFile = string.Empty;
        SIDExcel.SIDExcel excel = null;
        //导出EXCEL

        //定义参数
        string strShipNo = txtnbr.Text.Trim();

        //strFile = "SID_Cust_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        strFile = "SID_" + strShipNo + ".xls";

        excel = new SIDExcel.SIDExcel(Server.MapPath("/docs/SID_Ship_Det.xls"), Server.MapPath("../Excel/") + strFile);
        //excel.NEWCustInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicSeal);
        excel.SidDetToExcelNewByNPOI("出运单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

    }

}
