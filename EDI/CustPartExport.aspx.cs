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
using System.ComponentModel;
using adamFuncs;
using CommClass;

public partial class CustPartExport : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        string cust = Request.QueryString["cust"];
        string part = Request.QueryString["part"];
        string qad = Request.QueryString["qad"];
        string stdDate = Request.QueryString["stdDate"];
        string endDate = Request.QueryString["endDate"];

        DataSet ds = GetCustPartExport(cust, part, qad, stdDate, endDate);
        drawExcel("<b>客户/货物发往</b>", "<b>客户物料</b>", "<b>物料号</b>", "<b>生效日期</b>", "<b>截止日期</b>", "<b>说明</b>", "<b>显示客户物料</b>");
        int n;
        for (n = 0; n < ds.Tables[0].Rows.Count; n++)
        {
            drawExcel(ds.Tables[0].Rows[n]["cp_cust"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cp_cust_part"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cp_part"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cp_start_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cp_end_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cp_comment"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cp_cust_partd"].ToString().Trim());
        }

        ds.Reset();

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.AppendHeader("content-disposition", "attachment; filename= cp.xls");
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

    private DataSet GetCustPartExport(string cust, string part, string qad, string stdDate, string endDate)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@cust", cust);
        param[1] = new SqlParameter("@part", part);
        param[2] = new SqlParameter("@qad", qad);
        param[3] = new SqlParameter("@stdDate", stdDate);
        param[4] = new SqlParameter("@endDate", endDate);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectCustPartExport", param);
        return ds;
    }
}
