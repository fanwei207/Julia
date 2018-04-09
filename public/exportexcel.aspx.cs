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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

public partial class public_exportexcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EXTitle"] == null || Session["EXSQL"] == null)
        {
            Response.Redirect("exportExcelClose.aspx", true);
        }
        else
        {
            String str = Session["EXHeader"].ToString();
            int ind = str.IndexOf("^~^");
            if (ind > -1) 
            {
                str = str.Substring(0, ind);
                Response.Redirect(str, true);
            }

            ToExcel();
        }
    }

    /// <summary>
    /// 拼接SQL语句的导出Excel通用方法(NPOI方法)
    /// </summary>
    /// <param name="dsnx">chk.dsn0()或chk.dsnx()</param>
    /// <param name="EXTitle"></param>
    /// <param name="EXSQL"></param>
    /// <param name="fullDateFormat">（将被取消）日期格式：yyyy-MM-dd HH:mm:ss还是yyyy-MM-dd</param>
    /// 

    public void ToExcel()
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

        IList<ExcelTitle> ItemList = new List<ExcelTitle>();

        string str = Session["EXTitle"].ToString();
        int total = 0;
        int ind = 0;
        while (str.Length > 0)
        {
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                total = total + 1;
                str = "";
                break;
            }
            total = total + 1;
            str = str.Substring(ind + 2);
        }
        str = Session["EXTitle"].ToString();

        for (int i = 0; i <= total - 1; i++)
        {
            ExcelTitle item = new ExcelTitle();
            int width = 100 * 6000 / 164;
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                ind = str.IndexOf("L~");
                if (ind > -1)
                {
                    str = str.Substring(2);
                }

                ind = str.IndexOf("^");
                if (ind == -1)
                {
                    item.Name = str.Substring(2);
                    item.Width = width;
                }
                else
                {
                    item.Name = str.Substring(ind + 1);
                    item.Width = Convert.ToInt32(str.Substring(0, ind)) * 6000 / 164;
                }
                str = "";
                break;
            }
            else
            {
                item.Name = str.Substring(0, ind);
                item.Width = width;
                str = str.Substring(ind + 2);

                ind = item.Name.IndexOf("L~");
                if (ind > -1)
                {
                    item.Name = item.Name.Substring(2);
                }

                ind = item.Name.IndexOf("^");
                if (ind > -1)
                {
                    item.Width = Convert.ToInt32(item.Name.Substring(0, ind)) * 6000 / 164;
                    item.Name = item.Name.Substring(ind + 1);
                }
            }

            item.Name = item.Name.Replace("<b>", "").Replace("</b>", "");
            ItemList.Add(item);
        }

        adamClass chk = new adamClass();
        string sqlStr = Session["EXSQL"].ToString(); 
        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, sqlStr).Tables[0];

        //头栏样式
        ICellStyle styleHeader = workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        styleHeader.SetFont(fontHeader);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(0);

        foreach (ExcelTitle item in ItemList)
        {
            sheet.SetColumnWidth(ItemList.IndexOf(item), item.Width);

            ICell cell = rowHeader.CreateCell(ItemList.IndexOf(item));
            cell.CellStyle = styleHeader;
            cell.SetCellValue(item.Name);

            switch (dt.Columns[ItemList.IndexOf(item)].DataType.ToString())
            {
                case "System.DateTime":
                    ICellStyle s1 = workbook.CreateCellStyle();

                    s1.Alignment = HorizontalAlignment.Center;

                    s1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s1.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s1.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s1.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s1.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f1 = workbook.CreateFont();
                    f1.FontHeightInPoints = 9;
                    s1.SetFont(f1);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s1);

                    break;
                case "System.Int16":
                    ICellStyle s2 = workbook.CreateCellStyle();

                    s2.Alignment = HorizontalAlignment.Right;

                    s2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s2.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s2.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s2.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s2.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f2 = workbook.CreateFont();
                    f2.FontHeightInPoints = 9;
                    s2.SetFont(f2);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s2);

                    break;
                case "System.Int32":
                    ICellStyle s3 = workbook.CreateCellStyle();

                    s3.Alignment = HorizontalAlignment.Right;

                    s3.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s3.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s3.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s3.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s3.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s3.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s3.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s3.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f3 = workbook.CreateFont();
                    f3.FontHeightInPoints = 9;
                    s3.SetFont(f3);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s3);

                    break;
                case "System.Int64":
                    ICellStyle s4 = workbook.CreateCellStyle();

                    s4.Alignment = HorizontalAlignment.Right;

                    s4.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s4.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s4.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s4.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s4.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s4.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s4.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s4.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f4 = workbook.CreateFont();
                    f4.FontHeightInPoints = 9;
                    s4.SetFont(f4);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s4);

                    break;
                case "System.Decimal":
                    ICellStyle s5 = workbook.CreateCellStyle();

                    s5.Alignment = HorizontalAlignment.Right;

                    s5.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s5.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s5.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s5.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s5.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s5.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s5.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s5.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f5 = workbook.CreateFont();
                    f5.FontHeightInPoints = 9;
                    s5.SetFont(f5);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s5);

                    break;
                case "System.Double":
                    ICellStyle s6 = workbook.CreateCellStyle();

                    s6.Alignment = HorizontalAlignment.Right;

                    s6.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s6.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s6.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s6.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s6.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s6.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s6.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s6.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f6 = workbook.CreateFont();
                    f6.FontHeightInPoints = 9;
                    s6.SetFont(f6);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s6);

                    break;
                case "System.Boolean":
                    ICellStyle s7 = workbook.CreateCellStyle();

                    s7.Alignment = HorizontalAlignment.Center;

                    s7.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s7.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s7.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s7.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s7.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s7.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s7.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s7.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f7 = workbook.CreateFont();
                    f7.FontHeightInPoints = 9;
                    s7.SetFont(f7);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s7);

                    break;
                case "System.String":
                    ICellStyle s8 = workbook.CreateCellStyle();

                    s8.Alignment = HorizontalAlignment.Left;

                    s8.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    s8.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s8.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    s8.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s8.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    s8.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    s8.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    s8.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                    IFont f8 = workbook.CreateFont();
                    f8.FontHeightInPoints = 9;
                    s8.SetFont(f8);

                    sheet.SetDefaultColumnStyle(ItemList.IndexOf(item), s8);

                    break;
            }
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row = sheet.CreateRow(i + 1);

            for (int j = 1; j <= total; j++)
            {
                ICell cell = row.CreateCell(j - 1);

                if (dt.Columns.Count == total)
                {
                    int _col1 = j - 1;

                    switch (dt.Columns[_col1].DataType.ToString())
                    {
                        case "System.DateTime":

                            if (dt.Rows[i].IsNull(_col1))
                            {
                                cell.SetCellValue("");
                            }
                            else
                            {
                                //有可能是空值，所以要用Try
                                try
                                {
                                    DateTime _dt1 = Convert.ToDateTime(dt.Rows[i][_col1]);

                                    if (_dt1.Hour == 0 && _dt1.Minute == 0 && _dt1.Second == 0)
                                    {
                                        cell.SetCellValue(String.Format("{0:yyyy-MM-dd}", _dt1));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(String.Format("{0:yyyy-MM-dd HH:mm:ss}", _dt1));
                                    }
                                }
                                catch
                                {
                                    cell.SetCellValue("");
                                }
                            }

                            break;
                        case "System.Int16":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col1) ? 0 : Convert.ToInt16(dt.Rows[i][_col1]));

                            break;
                        case "System.Int32":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col1) ? 0 : Convert.ToInt32(dt.Rows[i][_col1]));

                            break;
                        case "System.Int64":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col1) ? 0 : Convert.ToInt64(dt.Rows[i][_col1]));

                            break;
                        case "System.Decimal":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col1) ? 0.0 : Convert.ToDouble(dt.Rows[i][_col1]));

                            break;
                        case "System.Double":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col1) ? 0 : Convert.ToDouble(dt.Rows[i][_col1]));

                            break;
                        case "System.Boolean":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col1) ? false : Convert.ToBoolean(dt.Rows[i][_col1]));

                            break;
                        case "System.String":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col1) ? "" : dt.Rows[i][_col1].ToString());

                            break;
                    }
                }
                else if (dt.Columns.Count > total)
                {
                    int _col2 = j - 1 + (dt.Columns.Count - total);

                    switch (dt.Columns[_col2].DataType.ToString())
                    {
                        case "System.DateTime":

                            if (dt.Rows[i].IsNull(_col2))
                            {
                                cell.SetCellValue("");
                            }
                            else
                            {
                                //有可能是空值，所以要用Try
                                try
                                {
                                    DateTime _dt1 = Convert.ToDateTime(dt.Rows[i][_col2]);

                                    if (_dt1.Hour == 0 && _dt1.Minute == 0 && _dt1.Second == 0)
                                    {
                                        cell.SetCellValue(String.Format("{0:yyyy-MM-dd}", _dt1));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(String.Format("{0:yyyy-MM-dd HH:mm:ss}", _dt1));
                                    }
                                }
                                catch
                                {
                                    cell.SetCellValue("");
                                }
                            }

                            break;
                        case "System.Int16":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col2) ? 0 : Convert.ToInt16(dt.Rows[i][_col2]));

                            break;
                        case "System.Int32":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col2) ? 0 : Convert.ToInt32(dt.Rows[i][_col2]));

                            break;
                        case "System.Int64":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col2) ? 0 : Convert.ToInt64(dt.Rows[i][_col2]));

                            break;
                        case "System.Decimal":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col2) ? 0.0 : Convert.ToDouble(dt.Rows[i][_col2]));

                            break;
                        case "System.Double":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col2) ? 0 : Convert.ToDouble(dt.Rows[i][_col2]));

                            break;
                        case "System.Boolean":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col2) ? false : Convert.ToBoolean(dt.Rows[i][_col2]));

                            break;
                        case "System.String":
                            cell.SetCellValue(dt.Rows[i].IsNull(_col2) ? "" : dt.Rows[i][_col2].ToString());

                            break;
                    }
                }
            }
        }

        dt.Reset();

        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);
            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");
    }

}

public class ExcelTitle
{
    private string _name;
    /// <summary>
    /// 名称
    /// </summary>
    public string Name
    {
        get
        {
            return this._name;
        }
        set
        {
            this._name = value;
        }
    }

    private int _width;
    /// <summary>
    /// 宽度
    /// </summary>
    public int Width
    {
        get
        {
            return this._width;
        }
        set
        {
            this._width = value;
        }
    }

    public ExcelTitle(string name, int width)
    {
        this.Name = name;
        this.Width = width;
    }

    public ExcelTitle()
    {

    }
}

