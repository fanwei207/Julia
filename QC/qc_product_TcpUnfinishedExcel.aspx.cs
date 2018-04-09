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
using QCProgress;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;

public partial class QC_qc_product_TcpUnfinishedExcel : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        string nbr = Request.QueryString["nbr"].ToString().Trim();
        string lot = Request.QueryString["lot"].ToString().Trim();
        string date1 = Request.QueryString["d1"].ToString().Trim();
        string date2 = Request.QueryString["d2"].ToString().Trim();
        bool uncheck = Convert.ToBoolean(Request.QueryString["uc"].ToString().Trim());

        DataTable dt = GetProductTcpUnfinished(nbr, lot, date1, date2, uncheck);

        drawExcel("<b>加工单</b>", "<b>ID</b>", "<b>物料号</b>", "<b>入库日期</b>", "<b>工单数量</b>", "<b>完工数量</b>", "<b>成本中心</b>", "<b>地点</b>", "<b>状态</b>");

        for (int n = 0; n < dt.Rows.Count; n++)
        {
            drawExcel(dt.Rows[n]["prd_nbr"].ToString().Trim(), dt.Rows[n]["prd_lot"].ToString().Trim(), dt.Rows[n]["prd_part"].ToString().Trim(), 
                      dt.Rows[n]["prd_checkdate"].ToString().Trim(), dt.Rows[n]["prd_qty1"].ToString().Trim(), dt.Rows[n]["prd_qty2"].ToString().Trim(), 
                      dt.Rows[n]["prd_cc"].ToString().Trim(), dt.Rows[n]["prd_site"].ToString().Trim(), dt.Rows[n]["prd_status"].ToString().Trim());
        }

        dt.Reset();
        Response.ContentType = "application/excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.AddHeader("content-disposition", "attachment; filename=excel.xls");
    }

    protected DataTable GetProductTcpUnfinished(string nbr, string lot, string date1, string date2, bool uncheck)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@lot", lot);
        param[2] = new SqlParameter("@date1", date1);
        param[3] = new SqlParameter("@date2", date2);
        param[4] = new SqlParameter("@unCheck", uncheck);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_QC_selectProductTcpUnfinished", param).Tables[0];
    }

    private void drawExcel(string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        //加工单
        TableCell cell = new TableCell();
        cell.Text = str1.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        //ID
        cell = new TableCell();
        cell.Text = str2.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        //物料号
        cell = new TableCell();
        cell.Text = str3.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(120);
        row.Cells.Add(cell);

        //入库日期
        cell = new TableCell();
        cell.Text = str4.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-MM-dd");
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        //工单数量
        cell = new TableCell();
        cell.Text = str5.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        //完工数量
        cell = new TableCell();
        cell.Text = str6.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        //成本中心
        cell = new TableCell();
        cell.Text = str7.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        //地点
        cell = new TableCell();
        cell.Text = str8.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        //状态
        cell = new TableCell();
        cell.Text = str9.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        qc_excel.Rows.Add(row);
    }
}
