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
using System.Data.SqlClient;
using adamFuncs;

public partial class pcb_export : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = GetData();
        drawExcel("<b>修改日期</b>", "<b>线路板</b>", "<b>版本</b>", "<b>绿油网</b>", "<b>钢网</b>", "<b>铜网</b>", "<b>改板内容</b>", "<b>设备确认</b>", "<b>车间确认</b>", "<b>备注</b>");


        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                drawExcel(ds.Tables[0].Rows[n][1].ToString().Trim(), ds.Tables[0].Rows[n][2].ToString().Trim(), ds.Tables[0].Rows[n][3].ToString().Trim(), ds.Tables[0].Rows[n][4].ToString().Trim(), ds.Tables[0].Rows[n][5].ToString().Trim()
                                , ds.Tables[0].Rows[n][6].ToString().Trim(), ds.Tables[0].Rows[n][7].ToString().Trim() , ds.Tables[0].Rows[n][8].ToString().Trim(), ds.Tables[0].Rows[n][9].ToString().Trim(), ds.Tables[0].Rows[n][10].ToString().Trim());
            }

            ds.Reset();
        }


        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("content-disposition", "attachment; filename=EdiQAD850.xls");
    }

    private DataSet GetData()
    {
        string strSql = "sp_PVK_selectPcbVersionKeys";

        SqlParameter[] sqlParam = new SqlParameter[10];
        sqlParam[0] = new SqlParameter("@date", Request.QueryString["date"].ToString());
        sqlParam[1] = new SqlParameter("@model", Request.QueryString["model"].ToString());
        sqlParam[2] = new SqlParameter("@version", Request.QueryString["version"].ToString());
        sqlParam[3] = new SqlParameter("@green", Request.QueryString["green"].ToString());
        sqlParam[4] = new SqlParameter("@steel", Request.QueryString["steel"].ToString());
        sqlParam[5] = new SqlParameter("@copper", Request.QueryString["copper"].ToString());
        sqlParam[6] = new SqlParameter("@content", Request.QueryString["content"].ToString());
        sqlParam[7] = new SqlParameter("@equ", Request.QueryString["equ"].ToString());
        sqlParam[8] = new SqlParameter("@workshop", Request.QueryString["workshop"].ToString());
        sqlParam[9] = new SqlParameter("@rmks", Request.QueryString["rmks"].ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    private void drawExcel(string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9, string str10)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.Text = str1.Trim();
        cell.Width = new Unit(88);
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-m-d");
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
        cell.Width = new Unit(140);
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

        exl.Rows.Add(row);
    }
}
