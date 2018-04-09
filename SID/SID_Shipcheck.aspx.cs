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
using QADSID;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Data;
using System.Text;
using System.Text;
using System.Data.SqlClient;


public partial class SID_SID_ShipX : BasePage
{
    SID sid = new SID();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            //验货日期维护
            if (!this.Security["550010023"].isValid)
            {
                gvShip.Columns[17].Visible = false;
            }
            if (!this.Security["550010024"].isValid)
            {
                gvShip.Columns[18].Visible = false;
            }
            if (!this.Security["550010025"].isValid)
            {
                gvShip.Columns[16].Visible = false;
            }
            BindData();


        }
    }

    protected void BindData()
    {
        String strRad = "";

        if (rad1.Checked)
        {
            strRad = "1";
        }
        if (rad2.Checked)
        {
            strRad = "2";
        }

        DataTable dt = GetShip(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtdate1.Text.Trim(), txtdate2.Text.Trim(), strRad, txtdomain.Text.Trim(), txtInspectDate.Text.Trim(), txtInspectSite.Text.Trim(), txtOutDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtInspectDate2.Text.Trim(), txtOutDate2.Text.Trim(), txt_createdate1.Text.Trim(), txt_createdate2.Text.Trim());
        
        gvShip.DataSource = dt;
        gvShip.DataBind();


    }

    public DataTable GetShip(String PK, String Nbr, string stardate, string enddata, string red, string domain, string InspectDate, string InspectSite, string OutDate, string shipto, string site, string InspectDate2, string OutDate2, string CreateDate1, string CreateDate2)
    {
        string strSQL = "sp_SID_shipX";
        SqlParameter[] parm = new SqlParameter[15];
        parm[0] = new SqlParameter("@pk", PK);
        parm[1] = new SqlParameter("@nbr", Nbr);
        parm[2] = new SqlParameter("@stardate", stardate);
        parm[3] = new SqlParameter("@enddata", enddata);
        parm[4] = new SqlParameter("@Rad", red);
        parm[5] = new SqlParameter("@domain", domain);
        parm[6] = new SqlParameter("@InspectDate", InspectDate);
        parm[7] = new SqlParameter("@InspectSite", InspectSite);
        parm[8] = new SqlParameter("@OutDate", OutDate);
        parm[9] = new SqlParameter("@shipto", shipto);
        parm[10] = new SqlParameter("@site", site);
        parm[11] = new SqlParameter("@InspectDate2", InspectDate2);
        parm[12] = new SqlParameter("@OutDate2", OutDate2);
        parm[13] = new SqlParameter("@CreateDate1", CreateDate1);
        parm[14] = new SqlParameter("@CreateDate2", CreateDate2);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
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
            Response.Redirect("SID_ShipcheckDet.aspx?DID=" + Server.UrlEncode(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + rad2.Checked.ToString() + "&rm=" + DateTime.Now);
        }
        else if (e.CommandName == "Confirm1")
        {
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvShip.DataKeys[index].Values["ID"].ToString();
            ConfirmShipInfo(id, Session["uID"].ToString(),"ORG1");
            BindData();
        }
        else if (e.CommandName == "Confirm2")
        {
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvShip.DataKeys[index].Values["ID"].ToString();
          
            ConfirmShipInfo(id, Session["uID"].ToString(), "ORG2");
            BindData();
        }
         else if (e.CommandName == "Confirm3")
         {
             int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
             string id = gvShip.DataKeys[index].Values["ID"].ToString();
             ConfirmShipInfo(id, Session["uID"].ToString(), "ORG3");
             BindData();
         }
    }

    public void ConfirmShipInfo(String id, string uID, string ORG)
        {
          string  strSQL = "sp_SID_ConfirmShipcheckdate";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@id", id);
            parm[1] = new SqlParameter("@UID", uID);
            parm[2] = new SqlParameter("@ORG", ORG);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }
       
    protected void gvShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShip.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void rad1_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rad2_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btn_exportexcel_Click(object sender, EventArgs e)
    {
        String strRad = "";

        if (rad1.Checked)
        {
            strRad = "1";
        }
        if (rad2.Checked)
        {
            strRad = "2";
        }
        DataTable dt = sid.SidExportExcel1(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtOutDate2.Text.Trim(), "", "", txtdate1.Text.Trim(), txtdate2.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), strRad, txt_createdate1.Text.Trim(), txt_createdate2.Text.Trim());
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "80^<b>出运单号</b>~^110^<b>系统货运单号</b>~^70^<b>出运日期</b>~^120^<b>出厂日期</b>~^80^<b>装箱地点</b>~^110^<b>序号</b>~^110^<b>物料编码</b>~^200^<b>客户物料</b>~^70^<b>出运套数</b>~^70^<b>出运只数</b>~^70^<b>箱数</b>~^150^<b>备注</b>~^100^<b>销售单号</b>~^40^<b>行号</b>~^100^<b>ATL订单号</b>~^100^<b>TCP订单号</b>~^120^<b>完工日期</b>~^120^<b>货物抵达日期</b>~^";
        this.ExportExcel(title, dt, false,ExcelVersion.Excel2003);
    }

    public DataTable GetShipMax(String PK, String Nbr, string stardate, string enddata, string red, string domain, string InspectDate, string InspectSite, string OutDate, string shipto, string site, string InspectDate2, string OutDate2, string CreateDate1, string CreateDate2)
    {
        string strSQL = "sp_SID_shipXMax";
        SqlParameter[] parm = new SqlParameter[15];
        parm[0] = new SqlParameter("@pk", PK);
        parm[1] = new SqlParameter("@nbr", Nbr);
        parm[2] = new SqlParameter("@stardate", stardate);
        parm[3] = new SqlParameter("@enddata", enddata);
        parm[4] = new SqlParameter("@Rad", red);
        parm[5] = new SqlParameter("@domain", domain);
        parm[6] = new SqlParameter("@InspectDate", InspectDate);
        parm[7] = new SqlParameter("@InspectSite", InspectSite);
        parm[8] = new SqlParameter("@OutDate", OutDate);
        parm[9] = new SqlParameter("@shipto", shipto);
        parm[10] = new SqlParameter("@site", site);
        parm[11] = new SqlParameter("@InspectDate2", InspectDate2);
        parm[12] = new SqlParameter("@OutDate2", OutDate2);
        parm[13] = new SqlParameter("@CreateDate1", CreateDate1);
        parm[14] = new SqlParameter("@CreateDate2", CreateDate2);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    }

    protected void btn_exportexcelbytotal_Click(object sender, EventArgs e)
    {
        String strRad = "";

        if (rad1.Checked)
        {
            strRad = "1";
        }
        if (rad2.Checked)
        {
            strRad = "2";
        }

        DataTable dt = GetShipMax(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtdate1.Text.Trim(), txtdate2.Text.Trim(), strRad, txtdomain.Text.Trim(), txtInspectDate.Text.Trim(), txtInspectSite.Text.Trim(), txtOutDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtInspectDate2.Text.Trim(), txtOutDate2.Text.Trim(), txt_createdate1.Text.Trim(), txt_createdate2.Text.Trim());
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "80^<b>出运单号</b>~^110^<b>系统货运单号</b>~^120^<b>出厂日期</b>~^70^<b>运输方式</b>~^70^<b>箱子型号</b>~^120^<b>出运日期</b>~^120^<b>运往</b>~^80^<b>装箱地点</b>~^80^<b>域</b>~^60^<b>QC放行</b>~^120^<b>完工日期</b>~^120^<b>货物抵达日期</b>~^";
        this.ExportExcel(title, dt, false);
    }


    public DataTable GetShipWo(String PK, String Nbr, string stardate, string enddata, string red, string domain, string InspectDate, string InspectSite, string OutDate, string shipto, string site, string InspectDate2, string OutDate2, string CreateDate1, string CreateDate2)
    {
        string strSQL = "sp_sid_selecteshipexcelwo";
        SqlParameter[] parm = new SqlParameter[15];
        parm[0] = new SqlParameter("@pk", PK);
        parm[1] = new SqlParameter("@nbr", Nbr);
        parm[2] = new SqlParameter("@stardate", stardate);
        parm[3] = new SqlParameter("@enddata", enddata);
        parm[4] = new SqlParameter("@Rad", red);
        parm[5] = new SqlParameter("@domain", domain);
        parm[6] = new SqlParameter("@InspectDate", InspectDate);
        parm[7] = new SqlParameter("@InspectSite", InspectSite);
        parm[8] = new SqlParameter("@OutDate", OutDate);
        parm[9] = new SqlParameter("@shipto", shipto);
        parm[10] = new SqlParameter("@site", site);
        parm[11] = new SqlParameter("@InspectDate2", InspectDate2);
        parm[12] = new SqlParameter("@OutDate2", OutDate2);
        parm[13] = new SqlParameter("@CreateDate1", CreateDate1);
        parm[14] = new SqlParameter("@CreateDate2", CreateDate2);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    }


    protected void btn_exportexcelbywo_Click(object sender, EventArgs e)
    {
        String strRad = "";

        if (rad1.Checked)
        {
            strRad = "1";
        }
        if (rad2.Checked)
        {
            strRad = "2";
        }

        DataTable dt = GetShipWo(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtdate1.Text.Trim(), txtdate2.Text.Trim(), strRad, txtdomain.Text.Trim(), txtInspectDate.Text.Trim(), txtInspectSite.Text.Trim(), txtOutDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtInspectDate2.Text.Trim(), txtOutDate2.Text.Trim(), txt_createdate1.Text.Trim(), txt_createdate2.Text.Trim());
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "80^<b>出运单号</b>~^110^<b>系统货运单号</b>~^70^<b>出运日期</b>~^120^<b>出厂日期</b>~^80^<b>装箱地点</b>~^110^<b>物料编码</b>~^200^<b>客户物料</b>~^70^<b>出运套数</b>~^70^<b>出运只数</b>~^70^<b>箱数</b>~^150^<b>备注</b>~^100^<b>销售单号</b>~^40^<b>行号</b>~^100^<b>ATL订单号</b>~^100^<b>TCP订单号</b>~^80^<b>加工单号</b>~^80^<b>ID号</b>~^120^<b>下达日期</b>~^80^<b>工单状态</b>~^80^<b>成本中心</b>~^";
        this.ExportExcel(title, dt, false);
    }
}