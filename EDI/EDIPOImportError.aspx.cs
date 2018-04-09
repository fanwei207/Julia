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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;

public partial class EDIPOImportError : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = GetTempError(Session["uID"].ToString());

            drawExcel("<b>日期</b>", "<b>客户代码</b>", "<b>港口</b>", "<b>TCP订单号</b>", "<b>客户订单号</b>", "<b>SW1</b>", "<b>SW2</b>"
                , "<b>截止日期</b>", "<b>序号</b>", "<b>SZX销售订单</b>", "<b>ATL销售订单</b>", "<b>QAD号编码</b>", "<b>产品型号</b>", "<b>订购数量(套)</b>"
                , "<b>数量(只)</b>", "<b>制地</b>", "<b>备注</b>", "<b>采购价格</b>", "<b>留样</b>", "<b>样品</b>","<b>承诺发货日期</b>", "<b>错误信息</b>");

            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                drawExcel(ds.Tables[0].Rows[n]["et_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_cust"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_port"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_tcp_po"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_cust_po"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_sw1"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_sw2"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_due_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_line"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_szx_so"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_atl_so"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_qad"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_item"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_qty_ord"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_qty_ord1"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_site"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_rmks"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_price"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_sample"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_IsSample"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_promisedDeliveryDate"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_errMsg"].ToString().Trim());
            }

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=errMsg.xls");
        }
    }

    protected DataSet GetTempError(string uID)
    {
        try
        {
            string strSql = "sp_edi_selectTempError";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@uID", uID);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            return null;
        }
    }

    private void drawExcel(params string[] str)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        foreach (string s in str)
        {
            TableCell cell = new TableCell();
            cell.Text = s.Trim();

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"\d{10,}");

            if (reg.IsMatch(cell.Text))
            {
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            }
            row.Cells.Add(cell);
        }

        exl.Rows.Add(row);
    }


}
