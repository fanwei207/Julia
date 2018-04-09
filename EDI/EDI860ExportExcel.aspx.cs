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

public partial class EDI_EDI860ExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = getEdiData.export860Excel(Request.QueryString["stdDate"].ToString(), Request.QueryString["endDate"].ToString());
        Int32 n, m;
        drawExcel("<b>采购单号</b>", "<b>客户订单号</b>", "<b>行号</b>", "<b>客户零件号</b>", "<b>订单数量</b>",
                            "<b>价格</b>", "<b>单位</b>", "<b>需求日期</b>", "<b>截止日期</b>", "<b>备注</b>", "<b>修改要求</b>", "<b>修改日期</b>"
                            );
        for (n = 0; n < ds.Tables[0].Rows.Count; n++)
        {
            drawExcel(ds.Tables[0].Rows[n][0].ToString().Trim(), ds.Tables[0].Rows[n][1].ToString().Trim(), ds.Tables[0].Rows[n][2].ToString().Trim(), ds.Tables[0].Rows[n][3].ToString().Trim(),
                            ds.Tables[0].Rows[n][4].ToString().Trim(), ds.Tables[0].Rows[n][5].ToString().Trim(), ds.Tables[0].Rows[n][6].ToString().Trim()
                            , ds.Tables[0].Rows[n][7].ToString().Trim(), ds.Tables[0].Rows[n][8].ToString().Trim(), ds.Tables[0].Rows[n][10].ToString().Trim(), ds.Tables[0].Rows[n][9].ToString().Trim()
                            , ds.Tables[0].Rows[n]["edi_860_date"].ToString().Trim());
        }

        ds.Reset();

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.AppendHeader("content-disposition", "attachment; filename=Edi860.xls");
    }

    private void drawExcel(string str1, string str2, string str3, string str4, string str5,
                              string str6, string str7, string str8, string str9, string str10, string str11, string str12
                              )
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.Text = str1.Trim();
        cell.Width = new Unit(100);
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str2.Trim();
        cell.Width = new Unit(120);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str3.Trim();
        cell.Width = new Unit(40);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str4.Trim();
        cell.Width = new Unit(180);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str5.Trim();
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str6.Trim();
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(90);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str7.Trim();
        cell.Width = new Unit(40);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str8.Trim();
        cell.Width = new Unit(70);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str9.Trim();
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(70);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str10.Trim();
        cell.Width = new Unit(150);
        row.Cells.Add(cell);

        cell = new TableCell();
        if (str10.Trim() == "A")
        {
            cell.Text = "添加";
        }
        else if (str10.Trim() == "C")
        {
            cell.Text = "修改";
        }
        else if (str10.Trim() == "D")
        {
            cell.Text = "删除";
        }
        else
        {
            cell.Text = str11.Trim();
        }
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(110);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str12.Trim();
        cell.Width = new Unit(70);
        row.Cells.Add(cell);

        exl.Rows.Add(row);
    }
}
