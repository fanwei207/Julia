using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.IO;

public partial class plan_soque_track : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = selectSoquesTracking();
        gv.DataSource = dt;
        gv.DataBind();
        SpanSingleRow(gv);

    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:

                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();

                #region 重建表头
                //第一行 表头
                tcHeader.Add(new TableHeaderCell());
                //tcHeader[0].Attributes.Add("bgcolor", "DarkSeaBlue"); 
                tcHeader[0].Text = "SO Info";

                tcHeader.Add(new TableHeaderCell());
                //tcHeader[1].Attributes.Add("bgcolor", "DarkSeaBlue");
                tcHeader[1].Attributes.Add("colspan", "5");  //合并第一行的6列
                tcHeader[1].Text = "EDI Info";

                tcHeader.Add(new TableHeaderCell());
                //tcHeader[2].Attributes.Add("bgcolor", "DarkSeaBlue");
                tcHeader[2].Attributes.Add("colspan", "4");  //合并第一行的6列
                tcHeader[2].Text = "Details</th></tr><tr>";

                //第二行表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;text-align:center;");
                tcHeader[3].Font.Size = 8;
                tcHeader[3].Width = 100;
                tcHeader[3].Text = "SO";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[4].Font.Size = 8;
                tcHeader[4].Width = 100;
                tcHeader[4].Text = "Order";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[5].Font.Size = 8;
                tcHeader[5].Width = 30;
                tcHeader[5].Text = "Line";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[6].Font.Size = 8;
                tcHeader[6].Width = 150;
                tcHeader[6].Text = "Cust Part";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[7].Font.Size = 8;
                tcHeader[7].Width = 100;
                tcHeader[7].Text = "QAD";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[8].Font.Size = 8;
                tcHeader[8].Width = 100;
                tcHeader[8].Text = "Remarks";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[9].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[9].Font.Size = 8;
                tcHeader[9].Width = 150;
                tcHeader[9].Text = "Detail";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[10].Font.Size = 8;
                tcHeader[10].Width = 100;
                tcHeader[10].Text = "User Name";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[11].Font.Size = 8;
                tcHeader[11].Width = 80;
                tcHeader[11].Text = "Comfirm Date";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Attributes.Add("style", "background:url(../images/bg_gv.gif) repeat-x;color:black;fontsize:9px;height:27px;border:1px solid #b8d2f0;font-weight:bold;text-align:center;");
                tcHeader[12].Font.Size = 8;
                tcHeader[12].Width = 80;
                tcHeader[12].Text = "Request Date";
                #endregion

                break;
        }
    }

    protected DataTable selectSoquesTracking()
    {
        try
        {
            string strName = "sp_plan_selectSoqueTracking";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@soStart", txtSoStart.Text.Trim());
            param[1] = new SqlParameter("@soEnd", txtSoEnd.Text.Trim());
            param[2] = new SqlParameter("@nbrStart", Request.QueryString["poNbr"] == null ? txtPoNbrStart.Text.Trim() : Request.QueryString["poNbr"].ToString());
            param[3] = new SqlParameter("@nbrEnd", Request.QueryString["poNbr"] == null ? txtPoNbrStartEnd.Text.Trim() : Request.QueryString["poNbr"].ToString());
            param[4] = new SqlParameter("@qadStart", txtQADStart.Text.Trim());
            param[5] = new SqlParameter("@qadEnd", txtQADEnd.Text.Trim());
            param[6] = new SqlParameter("@line", Request.QueryString["line"] == null ? 0 : Convert.ToInt32(Request.QueryString["line"].ToString()));


            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strName, param).Tables[0];

        }
        catch
        {
            return null;
        }

    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    public static void SpanSingleRow(GridView gv)
    {
        string strSo = string.Empty;
        string strNbr = string.Empty;
        string strLine = string.Empty;
        string strCode = string.Empty;
        string strQad = string.Empty;
        string strRemarks = string.Empty;

        foreach (GridViewRow row in gv.Rows)
        {
          

            //edi
            if (strNbr != row.Cells[1].Text)
            {
                strNbr = row.Cells[1].Text;
            }
            else
            {
                row.Cells[1].Text = string.Empty;
            }

            //so
            if (strSo != row.Cells[0].Text + strNbr)
            {
                strSo = row.Cells[0].Text + strNbr;
            }
            else
            {
                row.Cells[0].Text = string.Empty;
            }

            //line
            if (strLine != strNbr + ";" + row.Cells[2].Text)
            {
                strLine = strNbr + ";" + row.Cells[2].Text;
            }
            else
            {
                row.Cells[2].Text = string.Empty;
            }
            //CustCode
            if (strCode != strLine + ";" + row.Cells[3].Text)
            {
                strCode = strLine + ";" + row.Cells[3].Text;
            }
            else
            {
                row.Cells[3].Text = string.Empty;
            }
            //QAD
            if (strQad != strCode + ";" + row.Cells[4].Text)
            {
                strQad = strCode + ";" + row.Cells[4].Text;
            }
            else
            {
                row.Cells[4].Text = string.Empty;
            }

            //Remarks
            if (strRemarks != strNbr + ";" + row.Cells[5].Text)
            {
                strRemarks = strNbr + ";" + row.Cells[5].Text;
            }
            else
            {
                row.Cells[5].Text = string.Empty;
            }



        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");
        sheet.SetColumnWidth(0, 20*256);
        sheet.SetColumnWidth(1, 20 * 256);
        sheet.SetColumnWidth(2, 5 * 256);
        sheet.SetColumnWidth(3, 20 * 256);
        sheet.SetColumnWidth(4, 20 * 256);
        sheet.SetColumnWidth(5, 25 * 256);
        sheet.SetColumnWidth(6, 10 * 256);
        sheet.SetColumnWidth(7, 20 * 256);
        sheet.SetColumnWidth(8, 20 * 256);
        sheet.SetColumnWidth(9, 20 * 256);
        sheet.SetColumnWidth(9, 30 * 256);
        ICell cell;
        //标题栏样式
        IFont fontTitle = workbook.CreateFont();
        fontTitle.FontHeightInPoints = 16;
        fontTitle.Boldweight = 600;
        ICellStyle styleTitle = workbook.CreateCellStyle();
        styleTitle.Alignment = HorizontalAlignment.Center;//居中对齐
        styleTitle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleTitle.FillPattern = FillPattern.SolidForeground;
        styleTitle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleTitle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleTitle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleTitle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleTitle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleTitle.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleTitle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleTitle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleTitle.SetFont(fontTitle);
        //表头样式
        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = 12;
        fontHeader.Boldweight = 600;
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
        styleHeader.SetFont(fontHeader);

        //内容样式
        ICellStyle styleBody = workbook.CreateCellStyle();
        styleBody.Alignment = HorizontalAlignment.Center;//居中对齐
        IFont fontBody = workbook.CreateFont();
        fontBody.FontHeightInPoints = 10;
        fontBody.Boldweight = 600;
        styleBody.SetFont(fontBody);

        //创建标题栏
        IRow Title = sheet.CreateRow(0);
        cell = Title.CreateCell(0);
        cell.SetCellValue("");
        SetCellRangeAddress(sheet, 0, 0, 0, 9);
        cell.CellStyle = styleTitle;

        //创建第一行表头
        IRow rowHeader = sheet.CreateRow(1);
        cell = rowHeader.CreateCell(0);
        cell.SetCellValue("SO Info");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(1);
        cell.SetCellValue("EDI Info");
        SetCellRangeAddress(sheet, 1, 1, 1, 4);
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(5);
        cell.SetCellValue("Details");
        SetCellRangeAddress(sheet, 1, 1, 5, 9);
        cell.CellStyle = styleHeader;
        //创建第二行表头
        rowHeader = sheet.CreateRow(2);
        cell = rowHeader.CreateCell(0);
        cell.SetCellValue("SO");
        cell.CellStyle = styleHeader;
        //sheet.SetColumnWidth(0,100);
        cell = rowHeader.CreateCell(1);
        cell.SetCellValue("Order");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(2);
        cell.SetCellValue("Line");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(3);
        cell.SetCellValue("Cust Part");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(4);
        cell.SetCellValue("QAD");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(5);
        cell.SetCellValue("Detail");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(6);
        cell.SetCellValue("User Name");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(7);
        cell.SetCellValue("Comfirm Date");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(8);
        cell.SetCellValue("Due Date");
        cell.CellStyle = styleHeader;
        cell = rowHeader.CreateCell(9);
        cell.SetCellValue("Remarks");
        cell.CellStyle = styleHeader;
        //写数据
        SetDetailsValue(sheet, 10, selectSoquesTracking(), 3, false);

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

    /// <summary>
    /// 合并单元格
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="rowstart"></param>
    /// <param name="rowend"></param>
    /// <param name="colstart"></param>
    /// <param name="colend"></param>
    protected void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
    {
        CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
        sheet.AddMergedRegion(cellRangeAddress);
        ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellRangeAddress, NPOI.SS.UserModel.BorderStyle.Thin, NPOI.HSSF.Util.HSSFColor.Black.Index);
    }

    /// <summary>
    /// 写数据
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="total"></param>
    /// <param name="dt"></param>
    /// <param name="startRowIndex"></param>
    /// <param name="fullDateFormat"></param>
    private void SetDetailsValue(ISheet sheet, int total, DataTable dt, int startRowIndex, bool fullDateFormat = false)
    {
        if (dt.Columns.Count >= total)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + startRowIndex);

                for (int j = 1; j <= total; j++)
                {
                    ICell cell = row.CreateCell(j - 1);
                    int _col1 = j - 1 + (dt.Columns.Count - total);
                    //cell.SetCellValue(dt.Rows[i][_col1].ToString());
                    if (i == 0)
                    {
                        cell.SetCellValue(dt.Rows[i][_col1].ToString());
                    }
                    else
                    {
                        string secStr;
                        string firStr;
                        if (_col1 == 0 || _col1 == 2)
                        {
                            secStr = dt.Rows[i][_col1].ToString() + dt.Rows[i][1].ToString();
                            firStr = dt.Rows[i - 1][_col1].ToString() + dt.Rows[i - 1][1].ToString();
                        }
                        else if (_col1 == 3)
                        {
                            secStr = dt.Rows[i][1].ToString() + dt.Rows[i][2].ToString() + dt.Rows[i][3].ToString();
                            firStr = dt.Rows[i - 1][1].ToString() + dt.Rows[i - 1][2].ToString() + dt.Rows[i - 1][3].ToString();
                        }
                        else if (_col1 == 4)
                        {
                            secStr = dt.Rows[i][1].ToString() + dt.Rows[i][2].ToString() + dt.Rows[i][3].ToString() + dt.Rows[i][4].ToString();
                            firStr = dt.Rows[i - 1][1].ToString() + dt.Rows[i - 1][2].ToString() + dt.Rows[i - 1][3].ToString() + dt.Rows[i - 1][4].ToString();
                        }
                        else if (_col1 == 1)
                        {
                            secStr = dt.Rows[i][1].ToString();
                            firStr = dt.Rows[i - 1][1].ToString();
                        }
                        else
                        {
                            secStr = "secStr";
                            firStr = "firStr";
                        }
                        if (string.Compare(secStr, firStr) == 0)
                        {
                            cell.SetCellValue("");
                            //SetCellRangeAddress(sheet, i + startRowIndex - 1, i + startRowIndex, _col1, _col1);
                        }
                        else
                        {
                            cell.SetCellValue(dt.Rows[i][_col1].ToString());
                        }
                    }


                }

            }
        }
    }

    protected void bntSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}