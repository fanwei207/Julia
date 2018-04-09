using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using Excel;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using ExcelHelper;
using System.Drawing;
using QCProgress;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.XSSF.UserModel;

/// <summary>
/// Summary description for RDW_Excel
/// </summary>
public class RDW_Excel
{
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
   
    private string _templetFile;
    private string _outputFile;

    public  RDW_Excel(string templetFilePath, string outputFilePath)
    {
        _templetFile = templetFilePath;
        _outputFile = outputFilePath;
    }

    public  RDW_Excel()
    {
        _templetFile = string.Empty;
        _outputFile = string.Empty;
    }
    public void ProjectTracking(string prod, string code, string status, string category, string startDate, string region ,int plantCode,string mstrId)
    {
        //读取模板路径
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new XSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);

        //输出明细
        string strSql = "sp_RDW_SelectProjectByTemplate";
        SqlParameter[] parm = new SqlParameter[9];
        parm[0] = new SqlParameter("@prod", prod);
        parm[1] = new SqlParameter("@prodcode", code);
        parm[2] = new SqlParameter("@start", startDate);
        parm[3] = new SqlParameter("@status", status);
        parm[4] = new SqlParameter("@cateid", category);
        parm[5] = new SqlParameter("@region", region);
        parm[6] = new SqlParameter("@plantCode", plantCode);
        parm[7] = new SqlParameter("@mstrId", mstrId);
        DataSet ds_data = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm);

        int nCols = ds_data.Tables[0].Columns.Count;
        int nRows = 2;
        int ExcelColStart = 9;

        ICellStyle style = workbook.CreateCellStyle();
        style.FillForegroundColor = HSSFColor.Red.Index;
        style.FillPattern = FillPattern.SolidForeground;

        ICellStyle styleComplete = workbook.CreateCellStyle();
        styleComplete.FillForegroundColor = HSSFColor.LightGreen.Index;
        styleComplete.FillPattern = FillPattern.SolidForeground;
        styleComplete.WrapText = true;

        ICellStyle stringStylte = workbook.CreateCellStyle();
        stringStylte.Alignment = HorizontalAlignment.Left;
        stringStylte.WrapText = true;
        workSheet.SetDefaultColumnStyle(3, stringStylte);
        workSheet.SetDefaultColumnStyle(1, stringStylte);
        workSheet.SetDefaultColumnStyle(2, stringStylte);
        workSheet.SetDefaultColumnStyle(4, stringStylte);
        workSheet.SetDefaultColumnStyle(5, stringStylte);
        workSheet.SetDefaultColumnStyle(6, stringStylte);
        workSheet.SetDefaultColumnStyle(7, stringStylte);
        workSheet.SetDefaultColumnStyle(8, stringStylte);
        foreach (DataRow row in ds_data.Tables[0].Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue("");
            iRow.CreateCell(1).SetCellValue(row[1]);

            iRow.CreateCell(2).SetCellValue(row[2]);
            iRow.CreateCell(3).SetCellValue(row[3]);
            iRow.CreateCell(4).SetCellValue(row[4]);
            iRow.CreateCell(5).SetCellValue(row[5]);
            iRow.CreateCell(6).SetCellValue(row[6]);
            iRow.CreateCell(7).SetCellValue(row[7]);
            iRow.CreateCell(8).SetCellValue(row[8]);
            ExcelColStart = 9;
            for (int j = 11; j < nCols; j++, ExcelColStart++)
            {
                iRow.HeightInPoints = 2 * workSheet.DefaultRowHeight / 20;
                if (row[j].ToString().IndexOf("EXPIRE") > 0)
                {
                    iRow.CreateCell(ExcelColStart).SetCellValue(row[j].ToString().Replace("EXPIRE", ""));
                    iRow.GetCell(ExcelColStart).CellStyle = style;

                }
                else if (row[j].ToString().Length > 9)
                {
                    iRow.CreateCell(ExcelColStart).SetCellValue(row[j]);
                    iRow.GetCell(ExcelColStart).CellStyle = styleComplete;
                }
                else
                    iRow.CreateCell(ExcelColStart).SetCellValue(row[j]);
            }
            iRow.CreateCell(ExcelColStart).SetCellValue(row[10]);

            nRows++;
        }
        // 输出Excel文件并退出 
        try
        {
            using (Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(localFile);
                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    public void ProjectTrackings(string prod, string code, string status, string category, string startDate, string region, int plantCode, string mstrId, int lineint,string lampType)
    {
        //读取模板路径
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new XSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);

        //输出明细
        string strSql = "sp_RDW_SelectProjectByTemplates";
        SqlParameter[] parm = new SqlParameter[10];
        parm[0] = new SqlParameter("@prod", prod);
        parm[1] = new SqlParameter("@prodcode", code);
        parm[2] = new SqlParameter("@start", startDate);
        parm[3] = new SqlParameter("@status", status);
        parm[4] = new SqlParameter("@cateid", category);
        parm[5] = new SqlParameter("@region", region);
        parm[6] = new SqlParameter("@plantCode", plantCode);
        parm[7] = new SqlParameter("@mstrId", mstrId);
        parm[8] = new SqlParameter("@LampType", lampType);
        parm[9] = new SqlParameter("@Line", lineint);
        DataSet ds_data = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm);

        int nCols = ds_data.Tables[0].Columns.Count;
        int nRows = 2;
        int ExcelColStart = 7;

        ICellStyle style = workbook.CreateCellStyle();
        style.FillForegroundColor = HSSFColor.Red.Index;
        style.FillPattern = FillPattern.SolidForeground;
        style.WrapText = true;

        ICellStyle styleComplete = workbook.CreateCellStyle();
        styleComplete.FillForegroundColor = HSSFColor.LightGreen.Index;
        styleComplete.FillPattern = FillPattern.SolidForeground;
        styleComplete.WrapText = true;

        ICellStyle styles = workbook.CreateCellStyle();
        styles.FillForegroundColor = HSSFColor.White.Index;
        styles.FillPattern = FillPattern.SolidForeground;
        styles.WrapText = true;
        foreach (DataRow row in ds_data.Tables[0].Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue("");
            iRow.CreateCell(1).SetCellValue(row[0]);

            iRow.CreateCell(2).SetCellValue(row[1]);
            iRow.CreateCell(3).SetCellValue(row[2]);
            iRow.CreateCell(4).SetCellValue(row[3]);
            iRow.CreateCell(5).SetCellValue(row[4]);


            iRow.CreateCell(6).SetCellValue(row[5]);
            iRow.CreateCell(7).SetCellValue(row[6]);
            iRow.CreateCell(8).SetCellValue(row[7]);
            iRow.CreateCell(9).SetCellValue(row[8]);
            iRow.CreateCell(10).SetCellValue(row[9]);


            iRow.CreateCell(11).SetCellValue(row[10]);
            iRow.CreateCell(12).SetCellValue(row[11]);
            iRow.CreateCell(13).SetCellValue(row[12]);
            iRow.CreateCell(14).SetCellValue(row[13]);
            iRow.CreateCell(15).SetCellValue(row[14]);

            iRow.CreateCell(16).SetCellValue(row[15]);
            iRow.CreateCell(17).SetCellValue(row[16]);
            iRow.CreateCell(18).SetCellValue(row[17]);
            iRow.CreateCell(19).SetCellValue(row[18]);
            iRow.CreateCell(20).SetCellValue(row[19]);

            iRow.CreateCell(21).SetCellValue(row[20]);
            iRow.CreateCell(22).SetCellValue(row[21]);
            iRow.CreateCell(23).SetCellValue(row[22]);
            iRow.CreateCell(24).SetCellValue(row[23]);
            iRow.CreateCell(25).SetCellValue(row[24]);

            iRow.CreateCell(26).SetCellValue(row[25]);
            iRow.CreateCell(27).SetCellValue(row[26]);
            iRow.CreateCell(28).SetCellValue(row[27]);
            iRow.CreateCell(29).SetCellValue(row[28]);
            iRow.CreateCell(30).SetCellValue(row[29]);
            ExcelColStart = 31;
            for (int j = 30; j < nCols; j++, ExcelColStart++)
            {
                if (row[j].ToString().IndexOf("EXPIRE") > 0)
                {
                    iRow.CreateCell(ExcelColStart).SetCellValue(row[j].ToString().Replace("EXPIRE", ""));
                    iRow.GetCell(ExcelColStart).CellStyle = style;
                    iRow.HeightInPoints = 2 * workSheet.DefaultRowHeight / 20;
                }
                else if (row[j].ToString().Length > 9)
                {
                    iRow.CreateCell(ExcelColStart).SetCellValue(row[j]);
                    iRow.GetCell(ExcelColStart).CellStyle = styleComplete;
                    iRow.HeightInPoints = 2 * workSheet.DefaultRowHeight / 20;
                }
                else
                {
                    iRow.CreateCell(ExcelColStart).SetCellValue(row[j]);
                    iRow.GetCell(ExcelColStart).CellStyle = styles;
                    iRow.HeightInPoints = 2 * workSheet.DefaultRowHeight / 20;
                }
            }
            nRows++;
        }
        // 输出Excel文件并退出 
        try
        {
            using (Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(localFile);
                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }

}