using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class wo2_wo2_IncomingList : BasePage
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.BarCodeSys"];
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        gvbind();
    }

    private void gvbind()
    {


        gvDet.DataSource = selectInfo();
        gvDet.DataBind();

    }

    private DataTable selectInfo()
    {

        DateTime actFromTime = new DateTime();
        DateTime actToTime = new DateTime();

        DateTime offFromTime = new DateTime();
        DateTime offToTime = new DateTime();


        string woNbr = txtNbr.Text.Trim();
        string woLot = txtLot.Text.Trim();
        string part = txtPart.Text.Trim();
        string vender = txtVender.Text.Trim();
        string venderName = txtVenderName.Text.Trim();

        string strFrom = txtActDateFrom.Text.Trim();
        string strTo = txtActDateTo.Text.Trim();

        string strOffFrom = txtOffBegin.Text.Trim();
        string strOffTo = txtOffEnd.Text.Trim();


        if (!DateTime.TryParse(strFrom, out actFromTime) && !strFrom.Equals(string.Empty))
        {
            Alert("上线日期的起始日期不是日期格式！");
            return null;
            
        }
        if (!DateTime.TryParse(strTo, out actToTime) && !strTo.Equals(string.Empty))
        {
            Alert("上线日期的截止日期不是日期格式！");
            return null;
        }
        if (!DateTime.TryParse(strOffFrom, out offFromTime) && !strOffFrom.Equals(string.Empty))
        {
            Alert("下线日期的起始日期不是日期格式！");
            return null;

        }
        if (!DateTime.TryParse(strOffTo, out offToTime) && !strOffTo.Equals(string.Empty))
        {
            Alert("下线日期的截止日期不是日期格式！");
            return null;
        }


        string sqlstr = "sp_wo_selectIncomingList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@woNbr",woNbr)
            , new SqlParameter("@woLot",woLot)
            , new SqlParameter("@part",part)
            , new SqlParameter("@vender",vender)
            , new SqlParameter("@venderName",venderName)
            , new SqlParameter("@fromTime",strFrom)
            , new SqlParameter("@toTime",strTo)
            , new SqlParameter("@strOffFrom",strOffFrom)
            , new SqlParameter("@strOffTo",strOffTo)
        
        };

        return  SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }


    protected void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDet.PageIndex = e.NewPageIndex;
        gvbind();
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "det")
        {
            int indexint = Convert.ToInt32(e.CommandArgument.ToString());

            string nbrID = gvDet.DataKeys[indexint].Values["wo_IncomingID"].ToString();
            string nbr = gvDet.DataKeys[indexint].Values["wo_nbr"].ToString();
            string lot= gvDet.DataKeys[indexint].Values["wo_lot"].ToString();
            string qad = gvDet.DataKeys[indexint].Values["wo_QAD"].ToString();
            string vender = gvDet.DataKeys[indexint].Values["wo_vender"].ToString();
            string venderName = gvDet.DataKeys[indexint].Values["ad_name"].ToString();
            string checkNum = gvDet.DataKeys[indexint].Values["wo_checkNum"].ToString();


            ltlAlert.Text = "$.window('明细',1000,800,'/wo2/wo2_IncomingDet.aspx?nbrID=" + nbrID + "&nbr=" + nbr + "&lot=" + lot
                + "&qad=" + qad + "&vender=" + vender + "&venderName=" + venderName + "&checkNum=" + checkNum + "');";
        }
    }
    protected void benExcel_Click(object sender, EventArgs e)
    {
        DateTime actFromTime = new DateTime();
        DateTime actToTime = new DateTime();

        DateTime offFromTime = new DateTime();
        DateTime offToTime = new DateTime();

        string woNbr = txtNbr.Text.Trim();
        string woLot = txtLot.Text.Trim();
        string part = txtPart.Text.Trim();
        string vender = txtVender.Text.Trim();
        string venderName = txtVenderName.Text.Trim();

        string strFrom = txtActDateFrom.Text.Trim();
        string strTo = txtActDateTo.Text.Trim();

        string strOffFrom = txtOffBegin.Text.Trim();
        string strOffTo = txtOffEnd.Text.Trim();

        if (!DateTime.TryParse(strFrom, out actFromTime) && !strFrom.Equals(string.Empty))
        {
            Alert("上线日期的起始日期不是日期格式！");
            return;

        }
        if (!DateTime.TryParse(strTo, out actToTime) && !strTo.Equals(string.Empty))
        {
            Alert("上线日期的截止日期不是日期格式！");
            return;

        }
        if (!DateTime.TryParse(strOffFrom, out offFromTime) && !strOffFrom.Equals(string.Empty))
        {
            Alert("下线日期的起始日期不是日期格式！");
            return ;

        }
        if (!DateTime.TryParse(strOffTo, out offToTime) && !strOffTo.Equals(string.Empty))
        {
            Alert("下线日期的截止日期不是日期格式！");
            return ;
        }

        string sqlstr = "sp_wo_selectIncomingExcel";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@woNbr",woNbr)
            , new SqlParameter("@woLot",woLot)
            , new SqlParameter("@part",part)
            , new SqlParameter("@vender",vender)
            , new SqlParameter("@venderName",venderName)
            , new SqlParameter("@fromTime",strFrom)
            , new SqlParameter("@toTime",strTo)
            , new SqlParameter("@strOffFrom",strOffFrom)
            , new SqlParameter("@strOffTo",strOffTo)
        
        };

        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];


        string tatle = "100^<b>工单号</b>~^100^<b>ID号</b>~^120^<b>QAD号</b>~^100^<b>供应商</b>~^"
                        + "200^<b>供应商名称</b>~^100^<b>部门</b>~^100^<b>产线</b>~^100^<b>检验数量</b>~^100^<b>不合格数量</b>~^100^<b>缺陷率（%）</b>~^"
                        + "100^<b>上线日期</b>~^100^<b>下线日期</b>~^100^<b>批次</b>~^100^<b>批次数量</b>~^100^<b>缺陷名</b>~^100^<b>缺陷数</b>~^100^<b>详情</b>~^";


        this.ExportExcel(tatle, dt, true, 12, "wo_nbr", "wo_lot", "wo_QAD", "wo_vender");

    }
}