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
using System.Text;

public partial class part_chk_exportPartDaily : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        string generateDate = string.Empty;
        string loc = Request.QueryString["loc"];
        string checkedDate = Request.QueryString["checkedDate"];
        string checkedDateend = Request.QueryString["checkedDateend"];
        string checkedName = Request.QueryString["checkedName"];
        string keepedName = Request.QueryString["keepedName"];
        DataTable dt = GetPartDaily(loc, checkedDate, checkedDateend);
        if (dt != null)
        {
           // generateDate = dt.Rows[0]["createdDate"].ToString();
            DrawExcelHeader("物料库存日盘点报表", loc, checkedDate, checkedName, keepedName);
            DrawExcelContent("序号", "地点", "库位", "QAD号", "老部件号", "批次", "系统库存", "盘点库存", "差异原因", "描述", true);
          
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DrawExcelContent(dt.Rows[i]["rowNo1"].ToString(), dt.Rows[i]["site"].ToString(), dt.Rows[i]["loc"].ToString(), dt.Rows[i]["part"].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["lot"].ToString(), dt.Rows[i]["sysQty"].ToString(), dt.Rows[i]["relQty"].ToString(), dt.Rows[i]["diff"].ToString(), dt.Rows[i]["descs"].ToString(), false);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Write("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=gb2312\"/>");
            Response.AppendHeader("content-disposition", "attachment; filename= PartListDaily.xls");
        }
    }

    protected DataTable GetPartDaily(string loc, string checkedDate, string checkedDateend)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@loc", loc);
            param[1] = new SqlParameter("@checkedDate", checkedDate);
            param[2] = new SqlParameter("@checkedDateEnd", checkedDateend);
            return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_selectPartDailyEx", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void DrawExcelHeader(string headerName, string generateDate, string checkedDate, string checkedName, string keepedName)
    {
        TableRow row = new TableRow();
        row.BorderWidth = new Unit(50);
        row.Font.Size = 20;

        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = headerName;
        cell.BackColor = System.Drawing.Color.Yellow;
        cell.ColumnSpan = 9;
        row.Cells.Add(cell);
        excelTable.Rows.Add(row);
        /////////////////////////////////////////////////////////////////////
        row = new TableRow();
        row.BorderWidth = new Unit(20);
        row.Font.Size = 10;

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = "仓库：" + generateDate;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
       // cell.BackColor = System.Drawing.Color.Yellow;
        cell.ColumnSpan = 2;
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = "注意：在导入盘点数据前，系统库存将不会显示。";
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.ColumnSpan = 7;
        row.Cells.Add(cell);
        excelTable.Rows.Add(row);
        excelTable.Rows.Add(row);
        /////////////////////////////////////////////////////////////////////
        row = new TableRow();
        row.BorderWidth = new Unit(20);
        row.Font.Size = 10;

        //cell = new TableCell();
        //cell.HorizontalAlign = HorizontalAlign.Center;
        //cell.Text = "盘点日期：" + checkedDate;
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        //cell.BackColor = System.Drawing.Color.Yellow;
        //cell.ColumnSpan = 2;
        //cell.Width = new Unit(200);
        //row.Cells.Add(cell);
        //excelTable.Rows.Add(row);

        //cell = new TableCell();
        //cell.HorizontalAlign = HorizontalAlign.Center;
        //cell.Text = "财务：" + checkedName;
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        //cell.BackColor = System.Drawing.Color.Yellow;
        //cell.Width = new Unit(100);
        //row.Cells.Add(cell);
        //excelTable.Rows.Add(row);

        //cell = new TableCell();
        //cell.HorizontalAlign = HorizontalAlign.Center;
        //cell.Text = string.Empty;
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        //cell.Width = new Unit(100);
        //row.Cells.Add(cell);
        //excelTable.Rows.Add(row);

        //cell = new TableCell();
        //cell.HorizontalAlign = HorizontalAlign.Center;
        //cell.Text = "仓库：" + keepedName;
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        //cell.BackColor = System.Drawing.Color.Yellow;
        //cell.Width = new Unit(100);
        //row.Cells.Add(cell);

        //cell = new TableCell();
        //cell.HorizontalAlign = HorizontalAlign.Center;
        //cell.Text = string.Empty;
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        //cell.ColumnSpan = 5;
        //cell.Width = new Unit(100);
        //row.Cells.Add(cell);
        //excelTable.Rows.Add(row);

    }

    protected void DrawExcelContent(string id, string site, string loc, string qad, string code, string lot, string sysQty, string relQty, string diff, string desc, bool isHeader)
    {
        bool HasDiff = false;
        if (relQty != string.Empty && sysQty != relQty)
        {
            HasDiff = true;
        }
        TableRow row = new TableRow();
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = id;
        cell.Width = new Unit(50);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = site;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = loc;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(150);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = qad;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(150);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = code;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(200);
        row.Cells.Add(cell);

        //cell = new TableCell();
        //cell.HorizontalAlign = HorizontalAlign.Center;
        //cell.Text = lot;
        //cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        //cell.Width = new Unit(150);
        //row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = sysQty;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.BackColor = !isHeader && HasDiff ? System.Drawing.Color.Red : cell.BackColor;
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = relQty;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.BackColor = !isHeader && HasDiff ? System.Drawing.Color.Red : cell.BackColor;
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = diff;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(150);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = desc;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(250);
        row.Cells.Add(cell);

        excelTable.Rows.Add(row);
    }
}
