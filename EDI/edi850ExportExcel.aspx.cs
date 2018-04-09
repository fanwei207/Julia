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

public partial class EDI_edi850ExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string date = string.Empty;
        if (Request.QueryString["date"] != null)
        {
            date = Request.QueryString["date"].ToString();
        }

        DataSet ds = getEdiData.getExcelData(date, Session["PlantCode"].ToString());
        Int32 n, m;
        drawExcel("<b>日期</b>", "<b>客户代码</b>", "<b>港口</b>", "<b>运输方式</b>", "<b>客户订单号</b>", "<b>SW1</b>", "<b>SW2</b>",
                            "<b>截止日期</b>", "<b>序号</b>", "<b>QAD订单号</b>", "<b>QAD号编码</b>", "<b>描述</b>", "<b>产品型号</b>",
                            "<b>订购数量(套)</b>", "<b>数量(只)</b>", "<b>域</b>", "<b>销售单地点</b>", "<b>制地</b>", "<b>备注</b>", "<b>TCP客户订单号</b>", "<b>价格</b>", "<b>裸灯QAD号</b>", "<b>描述</b>", "<b>处理意见</b>", "<b>订单操作域</b>", "<b>收货人地址</b>", "<b>审批结果</b>");
        for (n = 0; n < ds.Tables[0].Rows.Count; n++)
        {
            string _partNbr = ds.Tables[0].Rows[n]["partNbr"].ToString().Trim();
            if (_partNbr.Length > 0)
            {
                if (_partNbr.Substring(0, 1) == "0")
                {
                    _partNbr = "'" + _partNbr;
                }
            }

            drawExcel(ds.Tables[0].Rows[n]["PoRecDate"].ToString().Trim(), ds.Tables[0].Rows[n]["CustName"].ToString().Trim(), ds.Tables[0].Rows[n]["rmk"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["shipVia"].ToString().Trim(), ds.Tables[0].Rows[n]["poNbr"].ToString().Trim(), ds.Tables[0].Rows[n]["reqDate"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["dueDate"].ToString().Trim(), ds.Tables[0].Rows[n]["dueDate"].ToString().Trim(), ds.Tables[0].Rows[n]["poLine"].ToString().Trim(), "", ds.Tables[0].Rows[n]["qadPart"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["Desc1"].ToString().Trim()
                        , _partNbr, ds.Tables[0].Rows[n]["ordQty"].ToString().Trim(), ds.Tables[0].Rows[n]["QtyPer"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["domain"].ToString().Trim(), ds.Tables[0].Rows[n]["site"].ToString(),ds.Tables[0].Rows[n]["wo_site"].ToString().Trim(), "", ds.Tables[0].Rows[n]["fob"].ToString().Trim(), ds.Tables[0].Rows[n]["price"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["ps_comp"].ToString().Trim(), ds.Tables[0].Rows[n]["Desc2"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["errMsg"].ToString().Trim(), ds.Tables[0].Rows[n]["mpo_domain"].ToString().Trim(), ds.Tables[0].Rows[n]["det_rmks"].ToString(), ds.Tables[0].Rows[n]["appvResult"].ToString());
        }

        ds.Reset();

        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("content-disposition", "attachment; filename=Edi850.xls");
    }



    private void drawExcel(string str1, string str2, string str3, string str18,string str4, string str5,
                                      string str6, string str7, string str8, string str9, string str10, string strDesc1,
                                      string str11, string str12, string str13, string strDomain, string str14, string strWoSite, string str15, string str16
            , string str17, string str19, string strDesc2, string str20, string str21, string str22, string strAppResult)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.Text = str1.Trim();
        cell.Width = new Unit(55);
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str2.Trim();
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str3.Trim();
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str18.Trim();
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str4.Trim();
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str5.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(50);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str6.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(50);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str7.Trim();
        cell.Width = new Unit(70);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str8.Trim();
        cell.Width = new Unit(35);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str9.Trim();
        cell.Width = new Unit(75);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str10.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(105);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = strDesc1.Trim();
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str11.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str12.Trim();
        cell.Width = new Unit(95);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str13.Trim();
        cell.Width = new Unit(65);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = strDomain.Trim();
        cell.Width = new Unit(40);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str14.Trim();
        cell.Width = new Unit(40);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = strWoSite.Trim();
        cell.Width = new Unit(40);
        row.Cells.Add(cell);

        cell = new TableCell();
        if (str10.Trim() == "")
        {
            str15 = "客户物料不存在或者无QAD号";
        }
        cell.Text = str15.Trim();
        cell.Width = new Unit(180);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str16.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str17.Trim();
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str19.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(105);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = strDesc2.Trim();
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str20.Trim();
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str21.Trim();
        cell.Width = new Unit(80);
        row.Cells.Add(cell);
        exl.Rows.Add(row);

        cell = new TableCell();
        cell.Text = str22.Trim();
        cell.Width = new Unit(300);
        row.Cells.Add(cell);
        exl.Rows.Add(row);

        cell = new TableCell();
        cell.Text = strAppResult.Trim();
        cell.Width = new Unit(300);
        row.Cells.Add(cell);
        exl.Rows.Add(row);
    }
}
