using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

/// <summary>
/// Summary description for NPOIHelper
/// </summary> 
public class NPOIHelper
{
    public DataTable GetExcelContents(string excelPath)
    {
        if (File.Exists(excelPath))
        {
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            FileStream fileStream = new FileStream(excelPath, FileMode.Open);
            IWorkbook workbook = new HSSFWorkbook(fileStream);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);

            DataTable table = new DataTable();
            //获取sheet的首行
            IRow headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum + 1;

            for (int i = (sheet.FirstRowNum + 1); i < rowCount; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    table.Rows.Add(dataRow);
                }
                catch
                {
                    continue;
                }
            }

            workbook = null;
            sheet = null;

            return table;
        }
        else
        {
            return null;
        }
    }
}

public static class NPOIExtension
{
    public static void SetCellValue(this ICell cell, object value, bool fullDateFormat = false, string dateFormat = "")
    {
        if (value == null)
        {
            cell.SetCellValue("");
        }
        else
        {
            string type = value.GetType().FullName;
            switch (type)
            {
                case "System.DateTime":
                    if (dateFormat == "")
                    {
                        if (fullDateFormat)
                        {
                            cell.SetCellValue(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            cell.SetCellValue(((DateTime)value).ToString("yyyy-MM-dd"));
                        }
                    }
                    else
                    {
                        cell.SetCellValue(((DateTime)value).ToString(dateFormat));
                    }
                    break;
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Decimal":
                case "System.Double":
                    cell.SetCellValue(double.Parse(value.ToString()));
                    break;
                case "System.Boolean":
                    cell.SetCellValue((bool)value);
                    break;
                default:
                    cell.SetCellValue(value.ToString());
                    break;
            }
        }
    }
}