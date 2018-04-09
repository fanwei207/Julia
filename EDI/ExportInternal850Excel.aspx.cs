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

public partial class EDI_ExportInternal850Excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        Int32 n, m;
        //if (Convert.ToString(Request["filter"]).Length==0 )
        if (Request.QueryString["filter"] == null)
        {
            ds = getEdiData.get850QADExcelDataInternal(Session["uID"].ToString().Trim(), Session["PlantCode"].ToString().Trim());
            drawExcel("<b>采购订单号</b>", "<b>采购员</b>", "<b>截止日期</b>", "<b>运输方式</b>",
                    "<b>行号</b>", "<b>物料号</b>", "<b>单位</b>", "<b>订购数量</b>", "<b>单价</b>",
                    "<b>行需求日期</b>", "<b>行截止日期</b>", "<b>地点</b>", "<b>联系人</b>", "<b>备注</b>", "");
        }
        else
        {
            string date = string.Empty;
            if (Request.QueryString["date"] != null)
            {
                date = Request.QueryString["date"].ToString();
            }


            ds = getEdiData.get850QADExcelDataInternal(Request["filter"].ToString().Trim(), Session["PlantCode"].ToString().Trim(), Request["contact"].ToString().Trim(), Session["uName"].ToString().Trim(), date);
            drawExcel("<b>采购订单号</b>", "<b>联系人</b>", "<b>截止日期</b>", "<b>运输方式</b>",
                    "<b>行号</b>", "<b>物料号</b>", "<b>单位</b>", "<b>订购数量</b>", "<b>单价</b>",
                    "<b>行需求日期</b>", "<b>行截止日期</b>", "<b>订单域</b>", "<b>采购员</b>", "<b>备注</b>", "<b>QAD销售订单</b>");
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                drawExcel(ds.Tables[0].Rows[n][0].ToString().Trim(), ds.Tables[0].Rows[n][2].ToString().Trim(), ds.Tables[0].Rows[n][1].ToString().Trim(), ds.Tables[0].Rows[n][3].ToString().Trim(),
                                ds.Tables[0].Rows[n][4].ToString().Trim(), ds.Tables[0].Rows[n][5].ToString().Trim(), ds.Tables[0].Rows[n][6].ToString().Trim()
                                , ds.Tables[0].Rows[n][7].ToString().Trim(), ds.Tables[0].Rows[n][8].ToString().Trim(), ds.Tables[0].Rows[n][9].ToString().Trim()
                                , ds.Tables[0].Rows[n][10].ToString().Trim(), ds.Tables[0].Rows[n][11].ToString().Trim(), ds.Tables[0].Rows[n][12].ToString().Trim(), ds.Tables[0].Rows[n][13].ToString().Trim(), ds.Tables[0].Rows[n][14].ToString().Trim());
            }

            ds.Reset();
        }


        Response.ContentType = "application/vnd.ms-excel";
        //Response.Charset = "utf-8";
        //Response.HeaderEncoding = System.Text.Encoding.UTF8;
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("content-disposition", "attachment; filename=EdiQAD850.xls");
    }

    private void drawExcel(string str1, string str2, string str3, string str4, string str5,
                              string str6, string str7, string str8, string str9, string str10,
                              string str11, string str12, string str13, string str14, string str15)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.Text = str1.Trim();
        cell.Width = new Unit(88);
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str2.Trim();
        cell.Width = new Unit(70);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str3.Trim();
        cell.Width = new Unit(90);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str4.Trim();
        cell.Width = new Unit(70);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str5.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(50);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str6.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(120);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str7.Trim();
        cell.Width = new Unit(40);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str8.Trim();
        cell.Width = new Unit(90);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str9.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(90);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str10.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(90);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str11.Trim();
        cell.Width = new Unit(90);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str12.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(70);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str13.Trim();
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str14.Trim();
        cell.Width = new Unit(150);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str15.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        exl.Rows.Add(row);
    }
}
