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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
public partial class EDI_SampleShow : BasePage
{
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
        string strnbr = txtNbr.Text.Trim().Replace("*", "%");
        string strdate = txtdate.Text.Trim();

        gvSID.DataSource = SelectStockingSubmit(strnbr, strdate, ddlstatus.SelectedValue);
        gvSID.DataBind();
    }
    public DataTable SelectStockingSubmit(string strnbr, string strdate, string status)
    {
        try
        {
            string strSql = "sp_edi_getEdiSampleShow";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@order", strnbr);
            sqlParam[1] = new SqlParameter("@date", strdate);
           
            sqlParam[2] = new SqlParameter("@status", status);
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];


        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvSID.Rows[i].FindControl("chk_Select");
            if (cb.Enabled == true)
            {
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
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //定义参数
        //string time = DateTime.Now.ToFileTime().ToString();
        string strRet = chkSelect();
        string struID = Convert.ToString(Session["uID"]);
        string[] strsid;

        if (strRet.Length != 0)
        {
            strRet = strRet.Substring(0, strRet.Length - 1);




            strsid = strRet.Split(',');
            foreach (var item in strsid)
            {
                submitstocking(item, Session["uID"].ToString());
            }

            //Response.Redirect("SID_UpPicture.aspx?from=new&shipid=" + time + "&sid=" + strRet + "&rt=" + DateTime.Now.ToFileTime().ToString());

        }
        BindData();
    }
    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvSID.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvSID.DataKeys[i].Value.ToString() + ",";
            }
        }
        return strSelect;
    }
    public bool submitstocking(string nbr, string createby)
    {
        try
        {
            string strSql = "sp_EDI_EDISampleSubmit";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@id", nbr);
            SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);
            return true;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string strnbr = txtNbr.Text.Trim().Replace("*", "%");
        string strdate = txtdate.Text.Trim();
        string EXTitle = "<b>接收日期</b>~^<b>进QAD时间</b>~^<b>客户代码</b>~^<b>客户名称</b>~^<b>港口</b>~^<b>运输方式</b>~^<b>客户订单号</b>~^<b>SW1</b>~^<b>SW2</b>~^<b>序号</b>~^<b>QAD订单号</b>~^<b>QAD号编码</b>~^<b>描述</b>~^<b>产品型号</b>~^<b>订购数量(套)</b>~^<b>数量(只)</b>~^<b>制地</b>~^<b>备注</b>~^<b>TCP客户订单号</b>~^<b>价格</b>~^<b>价格*0.91</b>~^<b>裸灯QAD号</b>~^<b>描述</b>~^<b>处理意见</b>~^<b>订单操作域</b>~^<b>收货人地址</b>~^<b>审批结果</b>~^<b>工单号</b>~^<b>创建人</b>~^<b>是否确认</b>~^<b>是否样品</b>~^";
        DataSet ds = getExcelData(strnbr, strdate, ddlstatus.SelectedValue);
        this.ExportExcel(EXTitle, ds.Tables[0], false);
    }
    public static DataSet getExcelData(string strnbr, string strdate, string status)
    {
        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@order", strnbr);
        sqlParam[1] = new SqlParameter("@date", strdate);
        sqlParam[2] = new SqlParameter("@status", status);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getExcelDataSample", sqlParam);
        return ds;
    }
    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gvSID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = e.Row.RowIndex;
            if (((Label)e.Row.FindControl("lbl_appvResult")).Text == string.Empty)
            {
                ((CheckBox)e.Row.FindControl("chk_Select")).Enabled = false;
            }
          

        }
    }
}