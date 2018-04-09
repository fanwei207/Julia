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

public partial class soque_list_export : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        string cust = Request.QueryString["cust"];
        string nbr = Request.QueryString["nbr"];
        string part = Request.QueryString["part"];
        string crt = Request.QueryString["crt"];
        string close = Request.QueryString["close"].ToString();

        string steps = string.Empty;
        int cnt = 0;

        DataSet ds = GetSoQueListExport(nbr, part, cust, crt, close);

        steps = ds.Tables[0].Rows[0]["Steps"].ToString();
        cnt = Convert.ToInt32(ds.Tables[0].Rows[0]["Cnt"]);

        drawHeader("2~<b>Nbr#</b>, 2~<b>Line</b>, 2~<b>Cust</b>, 2~<b>Cust Part</b>, 2~<b>QAD Part</b>, 2~<b>Ord Date</b>, 2~<b>Ord Qty</b>, 2~<b>Ship Date</b>, 2~<b>Status</b>, 2~<b>Create By</b>, 2~<b>Create Date</b>,2~<b>Close By</b>, 2~<b>Close Date</b>,2~<b>Remarks</b>," + steps);

        steps = string.Empty;

        for (int i = 0; i < cnt; i++)
        {
            if (i > 0)
            {
                steps = steps + ",";
            }

            steps += "<b>Responsible Person</b>, <b>Confirm Date</b>";
        }

        drawHeader(steps);

        foreach (DataRow row in ds.Tables[1].Rows)
        {
            string data = string.Empty;

            for (int col = 0; col < ds.Tables[1].Columns.Count; col++)
            {
                data += row[col].ToString() + ";";
            }

            drawExcel(data);
        }

        ds.Reset();

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.AppendHeader("content-disposition", "attachment; filename= ppu.xls");
    }

    /// <summary>
    /// 画表头，允许跨行、跨列
    /// </summary>
    /// <param name="str"></param>
    private void drawHeader(string param)
    {
        /*
         * "3~<b>订单</b>~2":
         * 第一个数字：跨行
         * 第二个数字：跨列
         */

        string[] str = param.Split(',');

        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        int r = 0;//跨行
        int c = 0;//跨列

        foreach (string s in str)
        {
            r = 0;
            c = 0;

            TableCell cell = new TableCell();
            
            //有跨行
            if (s.Contains("~<b>"))
            {
                r = Convert.ToInt32(s.Substring(0, s.IndexOf("~<b>")));
            }
            //有跨列
            if (s.Contains("</b>~"))
            {
                c = Convert.ToInt32(s.Substring(s.IndexOf("</b>~") + 5));
            }

            cell.Text = s.Substring(s.IndexOf("<b>"), s.IndexOf("</b>") - s.IndexOf("<b>") + 4);

            if (r > 0)
            {
                cell.RowSpan = r;
            }

            if (c > 0)
            {
                cell.ColumnSpan = c;
            }

            row.Cells.Add(cell);
        }

        exl.Rows.Add(row);
    }

    private void drawExcel(string param)
    {
        param = param.Substring(0, param.Length - 1);
        string[] str = param.Split(';');

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

    private DataSet GetSoQueListExport(string nbr, string part, string cust, string createdBy, string notComp)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@part", part);
        param[2] = new SqlParameter("@cust", cust);
        param[3] = new SqlParameter("@createdBy", createdBy);
        param[4] = new SqlParameter("@notComp", notComp);

        DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_exportSoQueList", param);
        return ds;
    }
}
