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
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class partinfo_part_calculation_exportexcel : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = getExportExcelData(int.Parse(Session["uID"].ToString()));
        Int32 n, m;
        drawExcel("<b>序号</b>", "<b>物料号</b>", "<b>物料数量</b>", "<b>描述1</b>", "<b>描述2</b>", "<b>单套箱数</b>",
                  "<b>单套重量</b>", "<b>单套体积</b>", "<b>合计箱数</b>", "<b>合计重量</b>", "<b>合计体积</b>");

        for (n = 0; n < dt.Rows.Count; n++)
        {
            drawExcel(dt.Rows[n]["id"].ToString().Trim(), dt.Rows[n]["pt_part"].ToString().Trim(), dt.Rows[n]["pt_number"].ToString().Trim(), dt.Rows[n]["pt_desc1"].ToString().Trim(), dt.Rows[n]["pt_desc2"].ToString().Trim(),
                       dt.Rows[n]["pt_unit_size"].ToString().Trim(), dt.Rows[n]["pt_uint_net_wt"].ToString().Trim(), dt.Rows[n]["pt_uint_net_wt"].ToString().Trim(),
                       dt.Rows[n]["pt_sum_size"].ToString().Trim(), dt.Rows[n]["pt_sum_ship_wt"].ToString().Trim(), dt.Rows[n]["pt_sum_net_wt"].ToString().Trim());
        }

        dt.Reset();

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.AppendHeader("content-disposition", "attachment; filename= part_calculation.xls");
    }
    /// <summary>
    /// 获取零件属性
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public DataTable getExportExcelData(int userID)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userID", userID);
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_part_selectRepPartCalculation", param).Tables[0];
        return dt;
    }

    private void drawExcel(string str1, string str2, string str3, string str4, string str5,
                           string str6, string str7, string str8, string str9, string str10, string str11)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.Text = str1.Trim();
        cell.Width = new Unit(50);
        cell.HorizontalAlign = HorizontalAlign.Center;
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str2.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Width = new Unit(110);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str3.Trim();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str4.Trim();
        cell.HorizontalAlign = HorizontalAlign.Left;
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str5.Trim();
        cell.HorizontalAlign = HorizontalAlign.Left;
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str6.Trim();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str7.Trim();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str8.Trim();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str9.Trim();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str10.Trim();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str11.Trim();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Width = new Unit(80);
        row.Cells.Add(cell);

        part_exl.Rows.Add(row);
    }
}
