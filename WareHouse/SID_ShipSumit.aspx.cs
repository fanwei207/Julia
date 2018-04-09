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


public partial class SID_ShipSumit : BasePage
{
    SID sid = new SID();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            //验货日期维护
            //if (!this.Security["550010023"].isValid)
            //{
            //    gvorder.Columns[17].Visible = false;
            //}
            //if (!this.Security["550010024"].isValid)
            //{
            //    gvorder.Columns[18].Visible = false;
            //}
            //if (!this.Security["550010025"].isValid)
            //{
            //    gvorder.Columns[16].Visible = false;
            //}
            ddl_type.Items.Add(new ListItem("请选择一个类型","0"));
            BindData();


        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvorder.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvorder.Rows[i].FindControl("chk_Select");
            if (chkAll.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
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

        DataTable dt = GetDate(txt_nbr.Text.Trim(), txt_startdate.Text.Trim(), txt_endate.Text.Trim(), strRad, txt_domain.Text.Trim(), ddl_type.SelectedValue);

        gvorder.DataSource = dt;
        gvorder.DataBind();
    }

    public DataTable GetDate(String Nbr, string stardate, string enddata, string red, string domain, string type)
    {
        string strSQL = "sp_wh_selectdata";
        SqlParameter[] parm = new SqlParameter[6];
        parm[0] = new SqlParameter("@nbr", Nbr);
        parm[1] = new SqlParameter("@stardate", stardate);
        parm[2] = new SqlParameter("@enddata", enddata);
        parm[3] = new SqlParameter("@Rad", red);
        parm[4] = new SqlParameter("@domain", domain);
        parm[5] = new SqlParameter("@type", type);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvorder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gvorder.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
            Response.Redirect("SID_ShipSumitDet.aspx?DID=" + Server.UrlEncode(gvorder.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + rad2.Checked.ToString() + "&rm=" + DateTime.Now);
        }
        else if (e.CommandName == "Confirm1")
        {
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvorder.DataKeys[index].Values["ID"].ToString();
            ConfirmShipInfo(id, Session["uID"].ToString(),"ORG1");
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
       
    protected void gvorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvorder.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
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
        DataTable dt = null;//sid.SidExportExcel1(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(),txtOutDate2.Text.Trim(), "", "", txtdate1.Text.Trim(), txtdate2.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), strRad);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "80^<b>出运单号</b>~^110^<b>系统货运单号</b>~^70^<b>出运日期</b>~^120^<b>出厂日期</b>~^80^<b>装箱地点</b>~^110^<b>序号</b>~^110^<b>物料编码</b>~^200^<b>客户物料</b>~^70^<b>出运套数</b>~^70^<b>出运只数</b>~^70^<b>箱数</b>~^150^<b>备注</b>~^100^<b>销售单号</b>~^40^<b>行号</b>~^100^<b>ATL订单号</b>~^100^<b>TCP订单号</b>~^120^<b>完工日期</b>~^120^<b>货物抵达日期</b>~^";
        this.ExportExcel(title, dt, false);
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

        DataTable dt = null;//GetShipMax(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtdate1.Text.Trim(), txtdate2.Text.Trim(), strRad, txtdomain.Text.Trim(), txtInspectDate.Text.Trim(), txtInspectSite.Text.Trim(), txtOutDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtInspectDate2.Text.Trim(), txtOutDate2.Text.Trim());
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "80^<b>出运单号</b>~^110^<b>系统货运单号</b>~^120^<b>出厂日期</b>~^70^<b>运输方式</b>~^70^<b>箱子型号</b>~^120^<b>出运日期</b>~^120^<b>运往</b>~^80^<b>装箱地点</b>~^80^<b>域</b>~^60^<b>QC放行</b>~^120^<b>完工日期</b>~^120^<b>货物抵达日期</b>~^";
        this.ExportExcel(title, dt, false);
    }

    public DataTable GetShipWo(String PK, String Nbr, string stardate, string enddata, string red, string domain, string InspectDate, string InspectSite, string OutDate, string shipto, string site, string InspectDate2, string OutDate2)
    {
        string strSQL = "sp_sid_selecteshipexcelwo";
        SqlParameter[] parm = new SqlParameter[13];
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
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    }
}