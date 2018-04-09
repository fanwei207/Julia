using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using Excel;
//using System.Runtime.InteropServices;
//using Microsoft.Office.Tools.Excel;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using QADSID;
using APPWS;
//using NPOI.HSSF.UserModel;
//using NPOI.HPSF;
//using NPOI.POIFS.FileSystem;
//using NPOI.HSSF.Util;
//using System.Globalization;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.SS.UserModel;
using NPOI.SS;  

namespace ExcelHelper
{
    ///   <summary> 
    /// 功能说明：套用模板输出Excel
    ///   </summary> 
    public class ExcelHelper
    {
        protected string templetFile = null;
        protected string outputFile = null;
        protected object missing = Missing.Value;
        //protected CultureInfo culture = new CultureInfo("en-US");
        //private HSSFWorkbook workbook;
        adamClass adam = new adamClass();

        ///   <summary> 
        /// 构造函数，需指定模板文件和输出文件完整路径
        ///   </summary> 
        ///   <param name="templetFilePath"> Excel模板文件路径 </param> 
        ///   <param name="outputFilePath"> 输出Excel文件路径 </param> 
        public ExcelHelper(string templetFilePath, string outputFilePath)
        {
            if (templetFilePath == null)
                throw new Exception(" Excel模板文件路径不能为空！ ");

            if (outputFilePath == null)
                throw new Exception(" 输出Excel文件路径不能为空！ ");

            if (!File.Exists(templetFilePath))
                throw new Exception(" 指定路径的Excel模板文件不存在！ ");

            this.templetFile = templetFilePath;
            this.outputFile = outputFilePath;

            try
            {
                Process[] myProcesses;
                myProcesses = Process.GetProcessesByName("Excel");

                foreach (Process myProcess in myProcesses)
                {
                    myProcess.Kill();
                }
            }
            catch { }
            finally
            {
                KillProcess("Excel");
            }
        }

        /// <summary>
        /// 获取WorkSheet数量
        /// </summary>
        /// <param name="rowCount">记录总行数</param>
        /// <param name="rows">每WorkSheet行数</param>
        /// <returns>返回WorkSheet数量</returns>
        private int GetSheetCount(int rowCount, int rows)
        {
            int n = rowCount % rows;         // 余数

            if (n == 0)
                return rowCount / rows;
            else
                return Convert.ToInt32(rowCount / rows) + 1;
        }

        /// <summary>
        /// 终止Excel
        /// </summary>
        /// <param name="processName"></param>
        private void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex
            }
        }

        /// <summary>
        /// 输出Szx发票
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPO"></param>
        public void SzxInvToExcel(string sheetPrefixName, string strShipNo, bool isPO)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);
                        
            workSheet.Cells[2, 2] = sdi.Customer;
            workSheet.Cells[2, 10] = sdi.Verfication;
            workSheet.Cells[3, 10] = sdi.ShipDate;
            workSheet.Cells[4, 2] = sdi.Harbor;
            workSheet.Cells[4, 11] = "T/T";
            workSheet.Cells[5, 10] = sdi.ShipNo;

            if (isPO)
            {
                workSheet.Cells[6, 10] = "PO#";
                if (sdi.FOB == null || sdi.FOB == "")
                {
                    workSheet.Cells[6, 11] = sdi.PO;
                }
                else
                {
                    workSheet.Cells[6, 11] = sdi.FOB;
                }
                
                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[6, 10], workSheet.Cells[6, 11]).Locked = true;
            }

            //输出明细信息
            int pos = 9;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 2] = strCode[1];
                workSheet.Cells[pos, 6] = "PCS";
                workSheet.Cells[pos, 8] = string.Format("{0:#0}",reader["QtyPcs"]);
                workSheet.Cells[pos, 9] = "USD";
                workSheet.Cells[pos, 10] = string.Format("{0:#0.00}", reader["AvgPrice"]);
                workSheet.Cells[pos, 11] = "USD";
                workSheet.Cells[pos, 12] = string.Format("{0:#0.00}", reader["Amount"]);

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.Cells[pos, 10] = "Total:";
            workSheet.Cells[pos, 11] = "USD";

            reader = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (reader.Read())
            {
                workSheet.Cells[pos, 12] = string.Format("{0:#0.00}", reader["Amount"]);
            }
            reader.Close();
            reader.Dispose();

            //Total处划线
            workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 10], workSheet.Cells[pos, 12]).Locked = true;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();
            
            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// NPOI新版本输出Szx发票
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void SzxInvToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isPO)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(1).GetCell(1).SetCellValue(sdi.Customer);
            workSheet.GetRow(1).GetCell(9).SetCellValue(sdi.Verfication);
            workSheet.GetRow(2).GetCell(9).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());
            workSheet.GetRow(3).GetCell(1).SetCellValue(sdi.Harbor);
            workSheet.GetRow(3).GetCell(10).SetCellValue("T/T");
            workSheet.GetRow(4).GetCell(9).SetCellValue(sdi.ShipNo);

            if (isPO)
            {
                workSheet.GetRow(5).GetCell(9).SetCellValue("PO#");
                if (sdi.FOB == null || sdi.FOB == "")
                {
                    workSheet.GetRow(5).GetCell(10).SetCellValue(sdi.PO);
                }
                else
                {
                    workSheet.GetRow(5).GetCell(10).SetCellValue(sdi.FOB);
                }
            }

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息

            int pos = 8;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.GetRow(pos).GetCell(1).SetCellValue(strCode[1]);
                workSheet.GetRow(pos).GetCell(5).SetCellValue("PCS");
                workSheet.GetRow(pos).GetCell(7).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPcs"])));
                workSheet.GetRow(pos).GetCell(8).SetCellValue("USD");
                workSheet.GetRow(pos).GetCell(9).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["AvgPrice"])));
                workSheet.GetRow(pos).GetCell(10).SetCellValue("USD");
                //workSheet.GetRow(pos).GetCell(11).SetCellValue(string.Format("{0:#0.00}", reader["Amount"]));
                workSheet.GetRow(pos).GetCell(11).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Amount"])));

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.GetRow(pos).GetCell(9).SetCellValue("Total:");
            workSheet.GetRow(pos).GetCell(10).SetCellValue("USD");

            reader = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (reader.Read())
            {
                workSheet.GetRow(pos).GetCell(11).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Amount"])));
            }
            reader.Close();
            reader.Dispose();

            //Total处划线

            for (int i = 10; i < 12; i++)
            {
                ICell cell = workSheet.GetRow(pos - 1).GetCell(i);
                HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                Style.BorderBottom = BorderStyle.Thin;
                cell.CellStyle = Style;
            }

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出Zql发票
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        public void ZqlInvToExcel(string sheetPrefixName, string strShipNo)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.Cells[2, 2] = sdi.Customer;
            workSheet.Cells[2, 10] = sdi.Verfication;
            workSheet.Cells[3, 10] = sdi.ShipDate;
            workSheet.Cells[4, 2] = sdi.Harbor;
            workSheet.Cells[4, 11] = "T/T";
            workSheet.Cells[5, 10] = sdi.ShipNo;

            //if (isPO)
            //{
            //    workSheet.Cells[6, 10] = "PO#";
            //    if (sdi.FOB == null || sdi.FOB == "")
            //    {
            //        workSheet.Cells[6, 11] = sdi.PO;
            //    }
            //    else
            //    {
            //        workSheet.Cells[6, 11] = sdi.FOB;
            //    }

            //    ////保护单元格
            //    //workSheet.Cells.get_Range(workSheet.Cells[6, 10], workSheet.Cells[6, 11]).Locked = true;
            //}

            //输出明细信息
            int pos = 9;
            decimal amount = 0.0M;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 2] = strCode[1];
                workSheet.Cells[pos, 6] = "PCS";
                workSheet.Cells[pos, 8] = string.Format("{0:#0}", reader["QtyPcs"]);
                workSheet.Cells[pos, 9] = "USD";
                workSheet.Cells[pos, 10] = string.Format("{0:#0.000000}", reader["AvgPrice"]);
                workSheet.Cells[pos, 11] = "USD";
                //workSheet.Cells[pos, 12] = string.Format("{0:#0.00}", reader["Amount"]);
                workSheet.Cells[pos, 12] = string.Format("{0:#0.000000}", Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"])));
                amount += Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"]));

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.Cells[pos, 10] = "Total:";
            workSheet.Cells[pos, 11] = "USD";
            workSheet.Cells[pos, 12] = string.Format("{0:#0.000000}", amount);

            //reader = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            //while (reader.Read())
            //{
            //    workSheet.Cells[pos, 12] = string.Format("{0:#0.00}", reader["Amount"]);
            //}
            //reader.Close();
            //reader.Dispose();

            //Total处划线
            workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 10], workSheet.Cells[pos, 12]).Locked = true;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// NPOI新版本输出Zql发票
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void ZqlInvToExcelByNPOI(string sheetPrefixName, string strShipNo)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(1).GetCell(1).SetCellValue(sdi.Customer);
            workSheet.GetRow(1).GetCell(9).SetCellValue(sdi.Verfication);
            workSheet.GetRow(2).GetCell(9).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());
            workSheet.GetRow(3).GetCell(1).SetCellValue(sdi.Harbor);
            workSheet.GetRow(3).GetCell(10).SetCellValue("T/T");
            workSheet.GetRow(4).GetCell(9).SetCellValue(sdi.ShipNo);

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息

            int pos = 8;
            decimal amount = 0.0M;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.GetRow(pos).GetCell(1).SetCellValue(strCode[1]);
                workSheet.GetRow(pos).GetCell(5).SetCellValue("PCS");
                workSheet.GetRow(pos).GetCell(7).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPcs"])));
                workSheet.GetRow(pos).GetCell(8).SetCellValue("USD");
                workSheet.GetRow(pos).GetCell(9).SetCellValue(double.Parse(string.Format("{0:#0.000000}", reader["AvgPrice"])));
                workSheet.GetRow(pos).GetCell(10).SetCellValue("USD");
                //workSheet.Cells[pos, 12] = string.Format("{0:#0.00}", reader["Amount"]);
                workSheet.GetRow(pos).GetCell(11).SetCellValue(double.Parse(string.Format("{0:#0.000000}", Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"])))));
                amount += Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"]));

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.GetRow(pos).GetCell(9).SetCellValue("Total:");
            workSheet.GetRow(pos).GetCell(10).SetCellValue("USD");
            workSheet.GetRow(pos).GetCell(11).SetCellValue(double.Parse(string.Format("{0:#0.000000}", amount)));

            //Total处划线

            for (int i = 10; i < 12; i++)
            {
                ICell cell = workSheet.GetRow(pos - 1).CreateCell(i);
                HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                Style.BorderBottom = BorderStyle.Thin;
                cell.CellStyle = Style;
            }

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出Szx装箱单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        /// <param name="isNotM3"></param>
        public void SzxPklToExcel(string sheetPrefixName, string strShipNo, bool isPkgs, bool isNotM3)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();
            
            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.Cells[15, 2] = sdi.ShipNo;
            workSheet.Cells[17, 2] = sdi.ShipDate;
            workSheet.Cells[18, 2] = sdi.Customer;
            workSheet.Cells[18, 11] = "T/T";
            workSheet.Cells[23, 2] = sdi.Harbor;

            //输出明细信息
            int pos = 29;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 2] = strCode[1];
                workSheet.Cells[pos, 3] = string.Format("{0:#0}", reader["QtyPcs"]);
                workSheet.Cells[pos, 4] = "PCS";
                if (isPkgs)
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyPkgs"]);
                    workSheet.Cells[pos, 6] = "PKGS";
                }
                else
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyBox"]);
                    workSheet.Cells[pos, 6] = "CTNS";
                }
                workSheet.Cells[pos, 7] = string.Format("{0:#0.00}", reader["Weight"]);
                workSheet.Cells[pos, 8] = "KGS";
                workSheet.Cells[pos, 9] = string.Format("{0:#0.00}", reader["Net"]);
                workSheet.Cells[pos, 10] = "KGS";

                if (!isNotM3)
                {
                    workSheet.Cells[pos, 11] = string.Format("{0:#0.00}", reader["Volume"]);
                    workSheet.Cells[pos, 12] = "M3";
                }

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.Cells[pos, 3] = "Total:";

            reader = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (reader.Read())
            {
                if (isPkgs)
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyPkgs"]);
                    workSheet.Cells[pos, 6] = "PKGS";
                }
                else
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyBox"]);
                    workSheet.Cells[pos, 6] = "CTNS";
                }
                workSheet.Cells[pos, 7] = string.Format("{0:#0.00}", reader["Weight"]);
                workSheet.Cells[pos, 8] = "KGS";
                workSheet.Cells[pos, 9] = string.Format("{0:#0.00}", reader["Net"]);
                workSheet.Cells[pos, 10] = "KGS";

                if (!isNotM3)
                {
                    workSheet.Cells[pos, 11] = string.Format("{0:#0.00}", reader["Volume"]);
                    workSheet.Cells[pos, 12] = "M3";
                }
            }
            reader.Close();
            reader.Dispose();

            //Total处划线
            if (!isNotM3)
            {
                workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            }
            else
            {
                workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 10]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 10]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            }

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 12]).Locked = true;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();
            ////workSheet.Cells[1, 1] = strRnd;

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// NPOI新版本输出Szx装箱单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void SzxPklToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs, bool isNotM3)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(14).GetCell(1).SetCellValue(sdi.ShipNo);
            workSheet.GetRow(16).GetCell(1).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());

            workSheet.CreateRow(17).CreateCell(1).SetCellValue(sdi.Customer);
            workSheet.GetRow(17).CreateCell(10).SetCellValue("T/T");
            workSheet.GetRow(22).CreateCell(1).SetCellValue(sdi.Harbor);

            #endregion

            #region //输出明细信息

            //输出明细信息
            int pos = 28;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.CreateRow(pos).CreateCell(1).SetCellValue(strCode[1]);
                workSheet.GetRow(pos).CreateCell(2).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPcs"])));
                workSheet.GetRow(pos).CreateCell(3).SetCellValue("PCS");
                if (isPkgs)
                {
                    workSheet.GetRow(pos).CreateCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPkgs"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("PKGS");
                }
                else
                {
                    workSheet.GetRow(pos).CreateCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyBox"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("CTNS");
                }
                workSheet.GetRow(pos).CreateCell(6).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Weight"])));
                workSheet.GetRow(pos).CreateCell(7).SetCellValue("KGS");
                workSheet.GetRow(pos).CreateCell(8).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Net"])));
                workSheet.GetRow(pos).CreateCell(9).SetCellValue("KGS");

                if (!isNotM3)
                {
                    workSheet.GetRow(pos).CreateCell(10).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Volume"])));
                    workSheet.GetRow(pos).CreateCell(11).SetCellValue("M3");
                }

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.CreateRow(pos).CreateCell(2).SetCellValue("Total:");

            reader = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (reader.Read())
            {
                if (isPkgs)
                {
                    workSheet.GetRow(pos).CreateCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPkgs"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("PKGS");
                }
                else
                {
                    workSheet.GetRow(pos).CreateCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyBox"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("CTNS");
                }
                workSheet.GetRow(pos).CreateCell(6).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Weight"])));
                workSheet.GetRow(pos).CreateCell(7).SetCellValue("KGS");
                workSheet.GetRow(pos).CreateCell(8).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Net"])));
                workSheet.GetRow(pos).CreateCell(9).SetCellValue("KGS");

                if (!isNotM3)
                {
                    workSheet.GetRow(pos).CreateCell(10).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Volume"])));
                    workSheet.GetRow(pos).CreateCell(11).SetCellValue("M3");
                }
            }
            reader.Close();
            reader.Dispose();

            //Total处划线
            if (!isNotM3)
            {
                for (int i = 2; i < 12; i++)
                {
                    ICell cell = workSheet.CreateRow(pos - 1).CreateCell(i);
                    HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                    Style.BorderBottom = BorderStyle.Thin;
                    cell.CellStyle = Style;
                }
            }
            else
            {
                for (int i = 2; i < 10; i++)
                {
                    ICell cell = workSheet.GetRow(pos - 1).GetCell(i);
                    HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                    Style.BorderBottom = BorderStyle.Thin;
                    cell.CellStyle = Style;
                }
            }

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出Zql装箱单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isNotM3"></param>
        public void ZqlPklToExcel(string sheetPrefixName, string strShipNo, bool isNotM3, bool isNotPkgs)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();
            
            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.Cells[11, 2] = sdi.ShipNo;
            workSheet.Cells[13, 2] = sdi.ShipDate;
            workSheet.Cells[15, 2] = sdi.Customer;
            workSheet.Cells[19, 2] = sdi.Harbor;

            //输出明细信息
            workSheet.Cells[25, 7] = "G/W";
            workSheet.Cells[25, 9] = "N/W";
            if (!isNotM3)
            {
                workSheet.Cells[25, 11] = "M3";
            }

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[25, 7], workSheet.Cells[25, 11]).Locked = true;

            int pos = 26;
            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 2] = strCode[1];
                workSheet.Cells[pos, 3] = string.Format("{0:#0}", reader["QtyPcs"]);
                workSheet.Cells[pos, 4] = "PCS";
                if (!isNotPkgs)
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyPkgs"]);
                    workSheet.Cells[pos, 6] = "PKGS";
                    workSheet.Cells[pos + 1, 5] = "(" + string.Format("{0:#0}", reader["QtyBox"]);
                    workSheet.Cells[pos + 1, 6] = "CTNS)";
                }
                else
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyBox"]);
                    workSheet.Cells[pos, 6] = "CTNS";
                }
                workSheet.Cells[pos, 7] = string.Format("{0:#0.00}", reader["Weight"]);
                workSheet.Cells[pos, 8] = "KGS";
                workSheet.Cells[pos, 9] = string.Format("{0:#0.00}", reader["Net"]);
                workSheet.Cells[pos, 10] = "KGS";

                if (!isNotM3)
                {
                    workSheet.Cells[pos, 11] = string.Format("{0:#0.00}", reader["Volume"]);
                    workSheet.Cells[pos, 12] = "M3";
                }

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;

                if (!isNotPkgs) pos += 2;
                else pos += 1;
            }
            reader.Close();

            //输出Total:
            //workSheet.Cells[pos, 3] = "Total:";

            reader = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (reader.Read())
            {
                if (!isNotPkgs)
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyPkgs"]);
                    workSheet.Cells[pos, 6] = "PKGS";
                    workSheet.Cells[pos + 1, 5] = "(" + string.Format("{0:#0}", reader["QtyBox"]);
                    workSheet.Cells[pos + 1, 6] = "CTNS)";
                }
                else
                {
                    workSheet.Cells[pos, 5] = string.Format("{0:#0}", reader["QtyBox"]);
                    workSheet.Cells[pos, 6] = "CTNS";
                }
                workSheet.Cells[pos, 7] = string.Format("{0:#0.00}", reader["Weight"]);
                workSheet.Cells[pos, 8] = "KGS";
                workSheet.Cells[pos, 9] = string.Format("{0:#0.00}", reader["Net"]);
                workSheet.Cells[pos, 10] = "KGS";

                if (!isNotM3)
                {
                    workSheet.Cells[pos, 11] = string.Format("{0:#0.00}", reader["Volume"]);
                    workSheet.Cells[pos, 12] = "M3";
                }
            }
            reader.Close();
            reader.Dispose();

            //Total处划线
            if (!isNotM3)
            {
                workSheet.get_Range(workSheet.Cells[pos, 5], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                workSheet.get_Range(workSheet.Cells[pos, 5], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            }
            else
            {
                workSheet.get_Range(workSheet.Cells[pos, 5], workSheet.Cells[pos, 10]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                workSheet.get_Range(workSheet.Cells[pos, 5], workSheet.Cells[pos, 10]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            }

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 5], workSheet.Cells[pos, 12]).Locked = true;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// NPOI新版本输出Zql装箱单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void ZqlPklToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isNotM3, bool isNotPkgs)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(10).GetCell(1).SetCellValue(sdi.ShipNo);
            workSheet.GetRow(12).GetCell(1).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());
            workSheet.GetRow(14).GetCell(1).SetCellValue(sdi.Customer);
            workSheet.GetRow(18).GetCell(1).SetCellValue(sdi.Harbor);


            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息


            workSheet.GetRow(24).GetCell(6).SetCellValue("G/W");
            workSheet.GetRow(24).GetCell(8).SetCellValue("N/W");
            if (!isNotM3)
            {
                workSheet.GetRow(24).GetCell(10).SetCellValue("M3");
            }

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[25, 7], workSheet.Cells[25, 11]).Locked = true;

            int pos = 25;
            IDataReader reader = null;
            reader = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.CreateRow(pos).CreateCell(1).SetCellValue(strCode[1]);
                workSheet.GetRow(pos).CreateCell(2).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPcs"])));
                workSheet.GetRow(pos).CreateCell(3).SetCellValue("PCS");
                if (!isNotPkgs)
                {
                    workSheet.GetRow(pos).CreateCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPkgs"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("PKGS");
                    workSheet.CreateRow(pos + 1).CreateCell(4).SetCellValue("(" + int.Parse(string.Format("{0:#0}", reader["QtyBox"])));
                    workSheet.GetRow(pos + 1).CreateCell(5).SetCellValue("CTNS)");
                }
                else
                {
                    workSheet.GetRow(pos).CreateCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyBox"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("CTNS");
                }
                workSheet.GetRow(pos).CreateCell(6).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Weight"])));
                workSheet.GetRow(pos).CreateCell(7).SetCellValue("KGS");
                workSheet.GetRow(pos).CreateCell(8).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Net"])));
                workSheet.GetRow(pos).CreateCell(9).SetCellValue("KGS");

                if (!isNotM3)
                {
                    workSheet.GetRow(pos).CreateCell(10).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Volume"])));
                    workSheet.GetRow(pos).CreateCell(11).SetCellValue("M3");
                }

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;

                if (!isNotPkgs) pos += 2;
                else pos += 1;
            }
            reader.Close();

            //输出Total:
            //workSheet.Cells[pos, 3] = "Total:";

            reader = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (reader.Read())
            {
                if (!isNotPkgs)
                {
                    workSheet.CreateRow(pos).CreateCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPkgs"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("PKGS");
                    workSheet.CreateRow(pos + 1).CreateCell(4).SetCellValue("(" + int.Parse(string.Format("{0:#0}", reader["QtyBox"])));
                    workSheet.CreateRow(pos + 1).CreateCell(5).SetCellValue("CTNS)");
                }
                else
                {
                    workSheet.GetRow(pos).GetCell(4).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyBox"])));
                    workSheet.GetRow(pos).CreateCell(5).SetCellValue("CTNS");
                }
                workSheet.GetRow(pos).CreateCell(6).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Weight"])));
                workSheet.GetRow(pos).CreateCell(7).SetCellValue("KGS");
                workSheet.GetRow(pos).CreateCell(8).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Net"])));
                workSheet.GetRow(pos).CreateCell(9).SetCellValue("KGS");

                if (!isNotM3)
                {
                    workSheet.GetRow(pos).CreateCell(10).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Volume"])));
                    workSheet.GetRow(pos).CreateCell(11).SetCellValue("M3");
                }
            }
            reader.Close();
            reader.Dispose();

            //Total处划线
            if (!isNotM3)
            {
                for (int i = 4; i < 12; i++)
                {
                    ICell cell = null;
                    if (i < 6)
                    {
                        cell = workSheet.GetRow(pos - 1).GetCell(i);
                        HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                        Style.BorderBottom = BorderStyle.Thin;
                        Style.Alignment = HorizontalAlignment.Right;
                        cell.CellStyle = Style;
                    }
                    else
                    {
                        cell = workSheet.GetRow(pos).GetCell(i);
                        HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                        Style.BorderTop = BorderStyle.Thin;
                        cell.CellStyle = Style;
                    }
                }
            }
            else
            {
                for (int i = 4; i < 10; i++)
                {
                    ICell cell = null;
                    if (i < 6)
                    {
                        cell = workSheet.GetRow(pos - 1).GetCell(i);
                        HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                        Style.BorderBottom = BorderStyle.Thin;
                        Style.Alignment = HorizontalAlignment.Right;
                        cell.CellStyle = Style;
                    }
                    else
                    {
                        cell = workSheet.GetRow(pos - 1).CreateCell(i);
                        HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                        Style.BorderBottom = BorderStyle.Thin;
                        cell.CellStyle = Style;
                    }
                }
            }

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出报关单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void DeclarationToExcel(string sheetPrefixName, string strShipNo, bool isPkgs)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //定义参数
            //当前页
            int curP = 0; 
            //每页条数
            int cntP = 5;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 46;
            //当前行
            int curR = 20;

            if (cntT < 0)
                return;

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);
            IDataReader readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);

            //输出头部信息
            workSheet.Cells[rowP * curP + 2, 2] = "SHANGHAI";
            workSheet.Cells[rowP * curP + 2, 7] = sdi.ShipDate;
            workSheet.Cells[rowP * curP + 4, 5] = sdi.ShipVia;
            workSheet.Cells[rowP * curP + 4, 1] = "3118941529";
            workSheet.Cells[rowP * curP + 4, 7] = sdi.Conveyance;
            workSheet.Cells[rowP * curP + 4, 10] = sdi.BL;
            workSheet.Cells[rowP * curP + 5, 1] = "上海强凌电子有限公司";
            workSheet.Cells[rowP * curP + 6, 5] = sdi.Trade;
            workSheet.Cells[rowP * curP + 6, 12] = "T/T";
            workSheet.Cells[rowP * curP + 9, 5] = sdi.Country;
            workSheet.Cells[rowP * curP + 9, 8] = sdi.Harbor;
            workSheet.Cells[rowP * curP + 9, 11] = "SONGJIANG";
            workSheet.Cells[rowP * curP + 11, 1] = sdi.ShipNo;
            workSheet.Cells[rowP * curP + 11, 3] = "FOB";

            while (readerH.Read())
            {
                if (isPkgs)
                {
                    workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyPkgs"]);
                    workSheet.Cells[rowP * curP + 13, 7] = "PKGS";
                }
                else
                {
                    workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyBox"]);
                    workSheet.Cells[rowP * curP + 13, 7] = "CTNS";
                }
                workSheet.Cells[rowP * curP + 13, 10] = string.Format("{0:#0.00}", readerH["Weight"]);
                workSheet.Cells[rowP * curP + 13, 12] = string.Format("{0:#0.00}", readerH["Net"]);

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
            }
            readerH.Close();

            //输出明细信息
            IDataReader readerD = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (readerD.Read())
            {
                if (cnt >= 5)
                {
                    cnt = 0;
                    curP += 1;
                    curR = 20;

                    //输出头部信息
                    workSheet.Cells[rowP * curP + 2, 2] = "SHANGHAI";
                    workSheet.Cells[rowP * curP + 2, 7] = sdi.ShipDate;
                    workSheet.Cells[rowP * curP + 4, 5] = sdi.ShipVia;
                    workSheet.Cells[rowP * curP + 4, 1] = "3118941529";
                    workSheet.Cells[rowP * curP + 4, 7] = sdi.Conveyance;
                    workSheet.Cells[rowP * curP + 4, 10] = sdi.BL;
                    workSheet.Cells[rowP * curP + 5, 1] = "上海强凌电子有限公司";
                    workSheet.Cells[rowP * curP + 6, 5] = sdi.Trade;
                    workSheet.Cells[rowP * curP + 6, 12] = "T/T";
                    workSheet.Cells[rowP * curP + 9, 5] = sdi.Country;
                    workSheet.Cells[rowP * curP + 9, 8] = sdi.Harbor;
                    workSheet.Cells[rowP * curP + 9, 11] = "SHANGHAI";
                    workSheet.Cells[rowP * curP + 11, 1] = sdi.Verfication;
                    workSheet.Cells[rowP * curP + 11, 3] = "FOB";

                    readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
                    while (readerH.Read())
                    {
                        if (isPkgs)
                        {
                            workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyPkgs"]);
                            workSheet.Cells[rowP * curP + 13, 7] = "PKGS";
                        }
                        else
                        {
                            workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyBox"]);
                            workSheet.Cells[rowP * curP + 13, 7] = "CTNS";
                        }
                        workSheet.Cells[rowP * curP + 13, 10] = string.Format("{0:#0.00}", readerH["Weight"]);
                        workSheet.Cells[rowP * curP + 13, 12] = string.Format("{0:#0.00}", readerH["Net"]);

                        ////保护单元格
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                    }
                    readerH.Close();
                }

                string[] strCode = readerD["SCode"].ToString().Split(',');
                workSheet.Cells[rowP * curP + curR, 1] = strCode[2];
                workSheet.Cells[rowP * curP + curR, 2] = strCode[1];
                workSheet.Cells[rowP * curP + curR + 1, 2] = strCode[0];
                workSheet.Cells[rowP * curP + curR, 5] = string.Format("{0:#0}", readerD["QtyPcs"]);
                workSheet.Cells[rowP * curP + curR, 6] = "PCS/只";
                //workSheet.Cells[rowP * curP + curR, 7] = "USD";
                workSheet.Cells[rowP * curP + curR, 8] = string.Format("{0:#0.00}", readerD["AvgPrice"]);
                workSheet.Cells[rowP * curP + curR, 9] = "USD";
                workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#0.00}", readerD["Amount"]);
                workSheet.Cells[rowP * curP + curR, 11] = "USD";

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 1], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR + 1, 2], workSheet.Cells[rowP * curP + curR + 1, 2]).Locked = true;

                cnt += 1;
                curR += 2;
            }
            readerD.Close();
            readerD.Dispose();

            curR += 1;

            //输出Total:
            workSheet.Cells[rowP * curP + curR, 9] = "Total:";
            //workSheet.Cells[rowP * curP + curR, 9] = "USD";

            readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (readerH.Read())
            {
                workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#,###,##0.00}", readerH["Amount"]);
            }
            workSheet.Cells[rowP * curP + curR, 11] = "USD";
            readerH.Close();
            readerH.Dispose();

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 8], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// NPOI新版本输出报关单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void DeclarationToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 5;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 46;
            //当前行
            int curR = 21;

            if (cntT < 0)
                return;

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);
            IDataReader readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);

            //输出头部信息
            workSheet.GetRow(rowP * curP + 1).CreateCell(1).SetCellValue("SHANGHAI");
            workSheet.GetRow(rowP * curP + 1).GetCell(6).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());
            workSheet.GetRow(rowP * curP + 3).GetCell(0).SetCellValue("3118941529");
            workSheet.GetRow(rowP * curP + 3).GetCell(4).SetCellValue(sdi.ShipVia);
            //workSheet.GetRow(rowP * curP + 3).GetCell(6).SetCellValue(sdi.Conveyance);
            workSheet.GetRow(rowP * curP + 3).GetCell(9).SetCellValue(sdi.BL);
            workSheet.GetRow(rowP * curP + 4).GetCell(0).SetCellValue("上海强凌电子有限公司");
            workSheet.GetRow(rowP * curP + 5).GetCell(4).SetCellValue(sdi.Trade);
            workSheet.GetRow(rowP * curP + 5).GetCell(9).SetCellValue("(T/T)" + sdi.Conveyance);
            workSheet.GetRow(rowP * curP + 8).CreateCell(1).SetCellValue(sdi.Country);
            workSheet.GetRow(rowP * curP + 8).GetCell(4).SetCellValue(sdi.Country);
            workSheet.GetRow(rowP * curP + 8).GetCell(7).SetCellValue(sdi.Harbor);
            workSheet.GetRow(rowP * curP + 8).GetCell(10).SetCellValue("SONGJIANG");
            workSheet.GetRow(rowP * curP + 10).GetCell(0).SetCellValue(sdi.ShipNo);
            workSheet.GetRow(rowP * curP + 10).GetCell(2).SetCellValue("FOB");

            while (readerH.Read())
            {
                if (isPkgs)
                {
                    workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyPkgs"]));
                    workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("PKGS");
                }
                else
                {
                    workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyBox"]));
                    workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("CTNS");
                }
                workSheet.GetRow(rowP * curP + 12).GetCell(9).SetCellValue(string.Format("{0:#0.00}", readerH["Weight"]));
                workSheet.GetRow(rowP * curP + 12).GetCell(11).SetCellValue(string.Format("{0:#0.00}", readerH["Net"]));
            }
            readerH.Close();

            workSheet.GetRow(rowP * curP + 17).CreateCell(1).SetCellValue("原产国:   中国");

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息

            ICellStyle cellstyle = workbook.CreateCellStyle();

            //styleP2.WrapText = true;
            cellstyle.Alignment = HorizontalAlignment.Right;

            IDataReader readerD = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (readerD.Read())
            {
                if (cnt >= 5)
                {
                    cnt = 0;
                    curP += 1;
                    //curR = 19;

                    //输出头部信息
                    workSheet.GetRow(rowP * curP + 2).CreateCell(1).SetCellValue("SHANGHAI");
                    workSheet.GetRow(rowP * curP + 2).CreateCell(6).SetCellValue(sdi.ShipDate);
                    workSheet.GetRow(rowP * curP + 4).CreateCell(4).SetCellValue(sdi.ShipVia);
                    workSheet.GetRow(rowP * curP + 4).CreateCell(0).SetCellValue("3118941529");
                    workSheet.GetRow(rowP * curP + 4).CreateCell(6).SetCellValue(sdi.Conveyance);
                    workSheet.GetRow(rowP * curP + 4).CreateCell(9).SetCellValue(sdi.BL);
                    workSheet.GetRow(rowP * curP + 5).CreateCell(0).SetCellValue("上海强凌电子有限公司");
                    workSheet.GetRow(rowP * curP + 6).CreateCell(4).SetCellValue(sdi.Trade);
                    workSheet.GetRow(rowP * curP + 6).CreateCell(11).SetCellValue("T/T");
                    workSheet.GetRow(rowP * curP + 9).CreateCell(4).SetCellValue(sdi.Country);
                    workSheet.GetRow(rowP * curP + 9).CreateCell(7).SetCellValue(sdi.Harbor);
                    workSheet.GetRow(rowP * curP + 9).CreateCell(10).SetCellValue("SHANGHAI");
                    workSheet.GetRow(rowP * curP + 11).CreateCell(0).SetCellValue(sdi.Verfication);
                    workSheet.GetRow(rowP * curP + 11).CreateCell(2).SetCellValue("FOB");

                    readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
                    while (readerH.Read())
                    {
                        if (isPkgs)
                        {
                            workSheet.GetRow(rowP * curP + 13).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyPkgs"]));
                            workSheet.GetRow(rowP * curP + 13).GetCell(6).SetCellValue("PKGS");
                        }
                        else
                        {
                            workSheet.GetRow(rowP * curP + 13).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyBox"]));
                            workSheet.GetRow(rowP * curP + 13).GetCell(6).SetCellValue("CTNS");
                        }
                        workSheet.GetRow(rowP * curP + 13).GetCell(9).SetCellValue(string.Format("{0:#0.00}", readerH["Weight"]));
                        workSheet.GetRow(rowP * curP + 13).GetCell(11).SetCellValue(string.Format("{0:#0.00}", readerH["Net"]));

                        ////保护单元格
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                    }
                    readerH.Close();
                }

                string[] strCode = readerD["SCode"].ToString().Split(',');
                workSheet.CreateRow(rowP * curP + curR).CreateCell(0).SetCellValue(strCode[2]);
                workSheet.GetRow(rowP * curP + curR).CreateCell(1).SetCellValue(strCode[1]);
                workSheet.CreateRow(rowP * curP + curR + 1).CreateCell(1).SetCellValue(strCode[0]);
                workSheet.GetRow(rowP * curP + curR).CreateCell(4).SetCellValue(string.Format("{0:#0}", readerD["QtyPcs"]));
                workSheet.GetRow(rowP * curP + curR).CreateCell(5).SetCellValue("PCS/只");
                //workSheet.GetRow(rowP * curP + curR, 7] = "USD";
                workSheet.GetRow(rowP * curP + curR).CreateCell(7).SetCellValue(string.Format("{0:#0.00}", readerD["AvgPrice"]));
                workSheet.GetRow(rowP * curP + curR).CreateCell(8).SetCellValue("USD");
                workSheet.GetRow(rowP * curP + curR).CreateCell(9).SetCellValue(string.Format("{0:#0.00}", readerD["Amount"]));
                workSheet.GetRow(rowP * curP + curR).CreateCell(10).SetCellValue("USD");

                workSheet.GetRow(rowP * curP + curR).GetCell(0).CellStyle = cellstyle;
                workSheet.GetRow(rowP * curP + curR).GetCell(4).CellStyle = cellstyle;
                workSheet.GetRow(rowP * curP + curR).GetCell(7).CellStyle = cellstyle;
                workSheet.GetRow(rowP * curP + curR).GetCell(9).CellStyle = cellstyle;

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 1], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR + 1, 2], workSheet.Cells[rowP * curP + curR + 1, 2]).Locked = true;

                cnt += 1;
                curR += 2;
            }
            readerD.Close();
            readerD.Dispose();

            curR += 1;

            //输出Total:
            workSheet.CreateRow(rowP * curP + curR).CreateCell(8).SetCellValue("Total:");
            //workSheet.Cells[rowP * curP + curR, 9] = "USD";

            readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (readerH.Read())
            {
                workSheet.GetRow(rowP * curP + curR).CreateCell(9).SetCellValue(string.Format("{0:#,###,##0.00}", readerH["Amount"]));
            }
            workSheet.GetRow(rowP * curP + curR).CreateCell(10).SetCellValue("USD");
            readerH.Close();
            readerH.Dispose();

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出检疫单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void QuarantineToExcel(string sheetPrefixName, string strShipNo, bool isPkgs)
        {
            //定义变量
            string strSCode = string.Empty;
            string strSNo = string.Empty;
            string strPCS = string.Empty;
            bool havedata = false;

            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.Cells[5, 7] = sdi.Contact;
            
            //输出明细信息
            int pos = 8;
            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 2] = reader["QA"].ToString();
                workSheet.Cells[pos, 3] = strCode[0];
                strSCode += strCode[0] + "/";
                strSNo += strCode[2] + "/";
                workSheet.Cells[pos, 6] = string.Format("{0:#0}", reader["QtyPcs"]);
                strPCS += string.Format("{0:#0}",reader["QtyPcs"]) + "PCS/";
                workSheet.Cells[pos, 7] = "只";

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 7]).Locked = true;
                havedata = true;
                pos += 1;
            }
            reader.Close();

            //输出汇总
            if (havedata)
            {
                workSheet.Cells[4, 3] = strSCode.Substring(0, strSCode.Length - 1);
                workSheet.Cells[4, 7] = strSNo.Substring(0, strSNo.Length - 1);
            }
            Excel.Range range = null;
            range = workSheet.get_Range(workSheet.Cells[4, 3], workSheet.Cells[4, 5]);
            range.MergeCells = true;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.ShrinkToFit = true;
            //range.Locked = true;

            range = workSheet.get_Range(workSheet.Cells[4, 7], workSheet.Cells[4, 8]);
            range.MergeCells = true;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.ShrinkToFit = true;
            //range.Locked = true;

            //workSheet.get_Range(workSheet.Cells[4, 3], workSheet.Cells[4, 5]).Merge(mValue);
            //workSheet.get_Range(workSheet.Cells[4, 3], mValue).Value2 = strSCode.Substring(0, strSCode.Length - 1);
            //workSheet.Cells[4, 3] = strSCode.Substring(0, strSCode.Length - 1);
            //workSheet.Cells[4, 7] = strSNo.Substring(0, strSNo.Length - 1);

            //保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[4, 3], mValue).Locked = true;
            //workSheet.Cells.get_Range(workSheet.Cells[4, 7], mValue).Locked = true;

            reader = (IDataReader)sid.SelectDeclarationNBPA(strShipNo);
            while (reader.Read())
            {
                if (isPkgs)
                {
                    strPCS = string.Format("{0:#0}", reader["QtyPkgs"]) + "PKGS/" + string.Format("{0:#0.00}", reader["Net"]) + "KGS/" + strPCS;
                }
                else
                {
                    strPCS = string.Format("{0:#0}", reader["QtyBox"]) + "CTNS/" + string.Format("{0:#0.00}", reader["Net"]) + "KGS/" + strPCS;
                }
            }
            reader.Close();
            reader.Dispose();

            workSheet.Cells[5, 3] = strPCS.Substring(0, strPCS.Length - 1);
            range = workSheet.get_Range(workSheet.Cells[5, 3], workSheet.Cells[5, 6]);
            range.MergeCells = true;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.ShrinkToFit = true;
            //range.Locked = true;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }
        }

        /// <summary>
        /// NPOI新版本输出ZQL报关单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void QuarantineToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            //定义变量
            string strSCode = string.Empty;
            string strSNo = string.Empty;
            string strPCS = string.Empty;
            bool havedata = false;

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(4).GetCell(6).SetCellValue(sdi.Contact);

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息

            int pos = 7;
            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.GetRow(pos).GetCell(1).SetCellValue(reader["QA"].ToString());
                workSheet.GetRow(pos).GetCell(2).SetCellValue(strCode[0]);
                strSCode += strCode[0] + "/";
                strSNo += strCode[2] + "/";
                workSheet.GetRow(pos).GetCell(5).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPcs"])));
                strPCS += string.Format("{0:#0}", reader["QtyPcs"]) + "PCS/";
                workSheet.GetRow(pos).GetCell(6).SetCellValue("只");

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 7]).Locked = true;
                havedata = true;
                pos += 1;
            }
            reader.Close();

            //输出汇总
            if (havedata)
            {
                workSheet.GetRow(3).GetCell(2).SetCellValue(strSCode.Substring(0, strSCode.Length - 1));
                workSheet.GetRow(3).GetCell(6).SetCellValue(strSNo.Substring(0, strSNo.Length - 1));
            }

            reader = (IDataReader)sid.SelectDeclarationNBPA(strShipNo);
            while (reader.Read())
            {
                if (isPkgs)
                {
                    strPCS = string.Format("{0:#0}", reader["QtyPkgs"]) + "PKGS/" + string.Format("{0:#0.00}", reader["Net"]) + "KGS/" + strPCS;
                }
                else
                {
                    strPCS = string.Format("{0:#0}", reader["QtyBox"]) + "CTNS/" + string.Format("{0:#0.00}", reader["Net"]) + "KGS/" + strPCS;
                }
            }
            reader.Close();
            reader.Dispose();

            workSheet.GetRow(4).GetCell(2).SetCellValue(strPCS.Substring(0, strPCS.Length - 1));

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出Szx订单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        public void SzxOrderToExcel(string sheetPrefixName, string strShipNo)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.Cells[3, 3] = sdi.Contact;
            workSheet.Cells[3, 7] = sdi.ShipDate;
            workSheet.Cells[7, 2] = sdi.Country;

            //workSheet.Cells[5, 1] = "SHANGHAI ZHENXIN ELECTRONIC ENGINEERING CO ,LTD";
            //workSheet.Cells[6, 1] = "139 WANGDONG SOUTH RD,(S)SI JING SONGJIANG";

            //输出明细信息
            int pos = 12;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 1] = string.Format("{0:#0}", reader["QtyPcs"]);
                workSheet.Cells[pos, 2] = "PCS";
                workSheet.Cells[pos, 3] = strCode[1];
                workSheet.Cells[pos, 6] = string.Format("{0:#0.00}", reader["AvgPrice"]);
                workSheet.Cells[pos, 7] = "USD";
                workSheet.Cells[pos, 8] = string.Format("{0:#0.00}", reader["Amount"]);
                workSheet.Cells[pos, 9] = "USD";

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 1], workSheet.Cells[pos, 9]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.Cells[pos, 7] = "Total:";
            workSheet.Cells[pos, 9] = "USD";

            reader = (IDataReader)sid.SelectDeclarationNBPA(strShipNo);
            while (reader.Read())
            {
                workSheet.Cells[pos, 8] = string.Format("{0:#0.00}", reader["Amount"]);
            }
            reader.Close();
            reader.Dispose();

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 8]).Locked = true;

            pos += 3;
            workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 3]).UnMerge();
            workSheet.Cells[pos, 6] = sdi.Customer;

            pos += 3;
            workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 3]).UnMerge();
            workSheet.Cells[pos, 6] = "AUTHORIZDE BY";
            workSheet.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 9]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            workSheet.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 9]).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// NPOI新版本输出Szx订单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void SzxOrderToExcelByNPOI(string sheetPrefixName, string strShipNo)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(2).GetCell(2).SetCellValue(sdi.Contact);
            workSheet.GetRow(2).GetCell(6).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());
            workSheet.GetRow(6).GetCell(1).SetCellValue(sdi.Country);

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息

            int pos = 11;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.GetRow(pos).GetCell(0).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPcs"])));
                workSheet.GetRow(pos).GetCell(1).SetCellValue("PCS");
                workSheet.GetRow(pos).GetCell(2).SetCellValue(strCode[1]);
                workSheet.GetRow(pos).GetCell(5).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["AvgPrice"])));
                workSheet.GetRow(pos).GetCell(6).SetCellValue("USD");
                workSheet.GetRow(pos).GetCell(7).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Amount"])));
                workSheet.GetRow(pos).GetCell(8).SetCellValue("USD");

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 1], workSheet.Cells[pos, 9]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.GetRow(pos).GetCell(6).SetCellValue("Total:");
            workSheet.GetRow(pos).GetCell(8).SetCellValue("USD");

            reader = (IDataReader)sid.SelectDeclarationNBPA(strShipNo);
            while (reader.Read())
            {
                workSheet.GetRow(pos).GetCell(7).SetCellValue(double.Parse(string.Format("{0:#0.00}", reader["Amount"])));
            }
            reader.Close();
            reader.Dispose();

            pos += 3;

            workSheet.GetRow(pos).CreateCell(0).SetCellValue(sdi.Customer);

            workSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(pos, pos, 0, 5));
            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont styleFoot1 = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 12;
            fontFoot.FontName = "Times New Roman";
            styleFoot.SetFont(fontFoot);
            workSheet.GetRow(pos).GetCell(0).CellStyle = styleFoot;

            pos += 3;
            workSheet.GetRow(pos).GetCell(1).SetCellValue("AUTHORIZDE BY");
            workSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(pos, pos, 1, 5));
            workSheet.GetRow(pos).GetCell(1).CellStyle = styleFoot;
            for (int i = 7; i < 9; i++)
            {
                ICell cell = workSheet.GetRow(pos).GetCell(i);
                //HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                ICellStyle Style = workbook.CreateCellStyle();

                Style.BorderBottom = BorderStyle.Thin;
                //cell.CellStyle = Style;
            }

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出Zql订单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        public void ZqlOrderToExcel(string sheetPrefixName, string strShipNo)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.Cells[3, 3] = sdi.Contact;
            workSheet.Cells[3, 7] = sdi.ShipDate;
            workSheet.Cells[7, 2] = sdi.Country;

            //workSheet.Cells[5, 1] = "ZHENJIANG  QIANG LING ILLUMINATE CO., LTD";
            //workSheet.Cells[6, 1] = "NO.200 XUE FU ROAD ZHENJIANG ,JIANGSU ";

            //输出明细信息
            int pos = 12;
            decimal amount = 0.0M;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 1] = string.Format("{0:#0}", reader["QtyPcs"]);
                workSheet.Cells[pos, 2] = "PCS";
                workSheet.Cells[pos, 3] = strCode[1];
                workSheet.Cells[pos, 6] = string.Format("{0:#0.000000}", reader["AvgPrice"]);
                workSheet.Cells[pos, 7] = "USD";
                //workSheet.Cells[pos, 8] = string.Format("{0:#0.00}", reader["Amount"]);
                workSheet.Cells[pos, 8] = string.Format("{0:#0.000000}", Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"])));
                amount += Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"]));
                workSheet.Cells[pos, 9] = "USD";

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[pos, 1], workSheet.Cells[pos, 9]).Locked = true;

                pos += 1;
            }
            reader.Close();

            pos += 1;

            //输出Total:
            workSheet.Cells[pos, 7] = "Total:";
            workSheet.Cells[pos, 9] = "USD";
            workSheet.Cells[pos, 8] = string.Format("{0:#0.000000}", amount);

            //reader = (IDataReader)sid.SelectDeclarationNBPA(strShipNo);
            //while (reader.Read())
            //{
            //    workSheet.Cells[pos, 8] = string.Format("{0:#0.00}", reader["Amount"]);
            //}
            //reader.Close();
            //reader.Dispose();

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 8]).Locked = true;

            pos += 3;
            workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 3]).MergeCells = false;
            workSheet.Cells[pos, 6] = sdi.Customer;

            pos += 3;
            workSheet.get_Range(workSheet.Cells[pos, 3], workSheet.Cells[pos, 3]).MergeCells = false;
            workSheet.Cells[pos, 6] = "AUTHORIZDE BY";
            workSheet.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 9]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            workSheet.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 9]).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }
        }

        /// <summary>
        /// NPOI新版本输出Szx订单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void ZqlOrderToExcelByNPOI(string sheetPrefixName, string strShipNo)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(2).GetCell(2).SetCellValue(sdi.Contact);
            workSheet.GetRow(2).GetCell(6).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());
            workSheet.GetRow(6).GetCell(1).SetCellValue(sdi.Country);

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息


            int pos = 11;
            decimal amount = 0.0M;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.GetRow(pos).GetCell(0).SetCellValue(int.Parse(string.Format("{0:#0}", reader["QtyPcs"])));
                workSheet.GetRow(pos).GetCell(1).SetCellValue("PCS");
                workSheet.GetRow(pos).GetCell(2).SetCellValue(strCode[1]);
                workSheet.GetRow(pos).GetCell(5).SetCellValue(double.Parse(string.Format("{0:#0.000000}", reader["AvgPrice"])));
                workSheet.GetRow(pos).GetCell(6).SetCellValue("USD");

                string AvgPriceAmount = string.Format("{0:#0.000000}", Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"])));
                workSheet.GetRow(pos).GetCell(7).SetCellValue(double.Parse(AvgPriceAmount));
                amount += Convert.ToInt32(reader["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", reader["AvgPrice"]));
                workSheet.GetRow(pos).GetCell(8).SetCellValue("USD");

                pos += 1;
            }
            reader.Close();
            reader.Dispose();

            pos += 1;

            //输出Total:
            workSheet.GetRow(pos).GetCell(6).SetCellValue("Total:");
            workSheet.GetRow(pos).GetCell(8).SetCellValue("USD");
            workSheet.GetRow(pos).GetCell(7).SetCellValue(double.Parse(string.Format("{0:#0.000000}", amount)));

            pos += 3;
            workSheet.GetRow(pos).GetCell(0).SetCellValue(sdi.Customer);
            workSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(pos, pos, 0, 5));
            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont styleFoot1 = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 12;
            fontFoot.FontName = "Times New Roman";
            styleFoot.SetFont(fontFoot);
            workSheet.GetRow(pos).GetCell(0).CellStyle = styleFoot;

            pos += 3;
            workSheet.GetRow(pos).GetCell(1).SetCellValue("AUTHORIZDE BY");

            workSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(pos, pos, 1, 5));
            workSheet.GetRow(pos).GetCell(1).CellStyle = styleFoot;

            //添加边框
            for (int i = 7; i < 9; i++)
            {
                ICell cell = workSheet.GetRow(pos).GetCell(i);
                HSSFCellStyle Style = workbook.CreateCellStyle() as HSSFCellStyle;
                Style.BorderBottom = BorderStyle.Thin;
                cell.CellStyle = Style;
            }

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出九城
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        public void NineCityToExcel(string sheetPrefixName, string strShipNo, bool isNotPkgs)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.Cells[5, 2] = sdi.Customer;

            //输出明细信息
            int pos = 10;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.Cells[pos, 2] = strCode[0];
                workSheet.Cells[pos, 4] = strCode[2];
                workSheet.Cells[pos, 5] = "上海市";
                workSheet.Cells[pos, 6] = string.Format("{0:#0}", reader["QtyPcs"]);
                workSheet.Cells[pos, 7] = "只";
                workSheet.Cells[pos, 8] = string.Format("{0:#0.00}", reader["Amount"]);
                workSheet.Cells[pos, 9] = "美元";
                workSheet.Cells[pos, 10] = string.Format("{0:#0}", reader["QtyBox"]) + "CTNS";
                if (!isNotPkgs)
                {
                    workSheet.Cells[pos + 1, 10] = string.Format("{0:#0}", reader["QtyPkgs"]) + "PKGS";

                    ////保护单元格
                    //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos + 1, 10]).Locked = true;

                    pos += 2;
                }
                else
                {
                    ////保护单元格
                    //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 10]).Locked = true;

                    pos += 1;
                }
                
            }
            reader.Close();

            pos += 2;

            //输出贸易方式:
            workSheet.Cells[pos, 8] = sdi.Trade;
            workSheet.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 8]).HorizontalAlignment = XlHAlign.xlHAlignLeft;

            pos += 3;
            workSheet.Cells[pos, 6] = sdi.Country;
            workSheet.get_Range(workSheet.Cells[pos, 6], workSheet.Cells[pos, 6]).HorizontalAlignment = XlHAlign.xlHAlignLeft;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }
        }

        /// <summary>
        /// NPOI新版本输出ZQL报关单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void NineCityToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isNotPkgs)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);

            workSheet.GetRow(4).GetCell(1).SetCellValue(sdi.Customer);

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息

            int pos = 9;

            IDataReader reader = null;
            reader = (IDataReader)sid.SelectQuarantine(strShipNo);
            while (reader.Read())
            {
                string[] strCode = reader["SCode"].ToString().Split(',');
                workSheet.CreateRow(pos).CreateCell(1).SetCellValue(strCode[0]);
                workSheet.GetRow(pos).CreateCell(3).SetCellValue(strCode[2]);
                workSheet.GetRow(pos).CreateCell(4).SetCellValue("上海市");
                workSheet.GetRow(pos).CreateCell(5).SetCellValue(string.Format("{0:#0}", reader["QtyPcs"]));
                workSheet.GetRow(pos).CreateCell(6).SetCellValue("只");
                workSheet.GetRow(pos).CreateCell(7).SetCellValue(string.Format("{0:#0.00}", reader["Amount"]));
                workSheet.GetRow(pos).CreateCell(8).SetCellValue("美元");
                workSheet.GetRow(pos).CreateCell(9).SetCellValue(string.Format("{0:#0}", reader["QtyBox"]) + "CTNS");
                if (!isNotPkgs)
                {
                    workSheet.CreateRow(pos + 1).CreateCell(9).SetCellValue(string.Format("{0:#0}", reader["QtyPkgs"]) + "PKGS");

                    ////保护单元格
                    //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos + 1, 10]).Locked = true);

                    pos += 2;
                }
                else
                {
                    ////保护单元格
                    //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 10]).Locked = true;

                    pos += 1;
                }

            }
            reader.Close();

            pos += 2;

            //输出贸易方式:
            workSheet.CreateRow(pos).CreateCell(7).SetCellValue(sdi.Trade);
            //workSheet.get_Range(workSheet.Cells[pos, 8], workSheet.Cells[pos, 8]).HorizontalAlignment = XlHAlign.xlHAlignLeft;

            pos += 3;
            workSheet.CreateRow(pos).CreateCell(5).SetCellValue(sdi.Country);
            //workSheet.get_Range(workSheet.Cells[pos, 6], workSheet.Cells[pos, 6]).HorizontalAlignment = XlHAlign.xlHAlignLeft;

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 输出单证报关差异
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        public void FinDiffToExcel(string sheetPrefixName, string strSql)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            //输出明细信息
            int pos = 2;

            //定义
            string strDeclarationNo = string.Empty;

            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strSql);
            while (reader.Read())
            {
                workSheet.Cells[pos, 1] = reader["DocumentNo"].ToString();
                workSheet.Cells[pos, 2] = string.Format("{0:yyyy-MM-dd}", reader["DocumentShipDate"]);
                workSheet.Cells[pos, 3] = reader["DeclarationNo"].ToString();
                workSheet.Cells[pos, 4] = string.Format("{0:yyyy-MM-dd}", reader["DeclarationShipDate"]);
                workSheet.Cells[pos, 5] = string.Format("{0:#0.00}", reader["DocumentAmount"]);
                workSheet.Cells[pos, 6] = string.Format("{0:#0.00}", reader["DeclarationAmount"]);
                workSheet.Cells[pos, 7] = string.Format("{0:#0.00}", reader["DiffAmount"]);

                if (strDeclarationNo == reader["DeclarationNo"].ToString())
                {
                    workSheet.Cells[pos, 6] = "0.00";
                    workSheet.Cells[pos, 7] = "0.00";
                }

                strDeclarationNo = reader["DeclarationNo"].ToString();

                pos += 1;
            }
            reader.Close();

            //输出合计:
            workSheet.Cells[pos, 5] = "=Sum(E2:E" + Convert.ToString(pos - 1) + ")";
            workSheet.Cells[pos, 6] = "=Sum(F2:F" + Convert.ToString(pos - 1) + ")";
            workSheet.Cells[pos, 7] = "=Sum(G2:G" + Convert.ToString(pos - 1) + ")";

            // 输出Excel文件并退出 
            try
            {
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }
        }

        /// <summary>
        /// 输出报关发票信息
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        public void DeclarationInfoToExcel(string sheetPrefixName, string strSql)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            //输出明细信息
            int pos = 2;

            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strSql);
            while (reader.Read())
            {
                workSheet.Cells[pos, 1] = reader["ShipNo"].ToString();
                workSheet.Cells[pos, 2] = reader["Tax"].ToString();
                workSheet.Cells[pos, 3] = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                workSheet.Cells[pos, 4] = reader["Verfication"].ToString();
                workSheet.Cells[pos, 5] = reader["SNO"].ToString();
                workSheet.Cells[pos, 6] = reader["SCode"].ToString();
                workSheet.Cells[pos, 7] = reader["Code"].ToString();
                workSheet.Cells[pos, 8] = string.Format("{0:#0}", reader["QtyPcs"]);
                workSheet.Cells[pos, 9] = string.Format("{0:#0.00}", reader["AvgPrice"]);
                workSheet.Cells[pos, 10] = string.Format("{0:#0.00}", reader["Amount"]);

                pos += 1;
            }
            reader.Close();

            // 输出Excel文件并退出 
            try
            {
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }
        }

        /// <summary>
        /// 输出发票信息汇总
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        public void DocumentInfoToExcel(string sheetPrefixName, string strSql)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            //输出明细信息
            int pos = 8;
            int startpos = 8;

            //定义发票号
            string strInvNo = string.Empty;
            int i = 1;

            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strSql);
            while (reader.Read())
            {
                if (strInvNo != reader["Invoice"].ToString())
                {
                    if (strInvNo != "")
                    {
                        workSheet.Cells[pos, 4] = "合计";
                        workSheet.Cells[pos, 14] = "=Sum(N" + Convert.ToString(startpos) + ":N" + Convert.ToString(pos - 1) + ")";
                        workSheet.Cells[pos, 16] = "=Sum(P" + Convert.ToString(startpos) + ":P" + Convert.ToString(pos - 1) + ")";
                        pos += 2;
                    }

                    strInvNo = reader["Invoice"].ToString();

                    startpos = pos;
                    workSheet.Cells[pos, 2] = i;
                    workSheet.Cells[pos, 6] = reader["BL"].ToString().Trim();
                    workSheet.Cells[pos, 7] = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                    workSheet.Cells[pos, 8] = reader["Commencement"].ToString().Trim();
                    workSheet.Cells[pos, 17] = reader["Invoice"].ToString().Trim();

                    i += 1;
                }

                workSheet.Cells[pos, 3] = reader["ItemNo"].ToString().Trim();
                workSheet.Cells[pos, 4] = reader["ItemDesc"].ToString().Trim();
                workSheet.Cells[pos, 5] = reader["OrderNo"].ToString().Trim();
                workSheet.Cells[pos, 10] = string.Format("{0:#0}", reader["Qty"]);
                workSheet.Cells[pos, 11] = string.Format("{0:#0}", reader["Sets_Qty"]);
                workSheet.Cells[pos, 12] = string.Format("{0:#0}", reader["SID_Ptype"]);
                workSheet.Cells[pos, 13] = string.Format("{0:#0.00000}", reader["ATLPrice"]);
                workSheet.Cells[pos, 14] = string.Format("{0:#0.00000}", reader["ATLAmount"]);
                workSheet.Cells[pos, 15] = string.Format("{0:#0.00000}", reader["TCPPrice"]);
                workSheet.Cells[pos, 16] = string.Format("{0:#0.00000}", reader["TCPAmount"]);
                workSheet.Cells[pos, 18] = reader["QAD"].ToString().Trim();
                workSheet.Cells[pos, 19] = reader["IsCabin"].ToString().Trim();

                pos += 1;
            }
            reader.Close();

            workSheet.Cells[pos, 4] = "合计";
            workSheet.Cells[pos, 14] = "=Sum(N" + Convert.ToString(startpos) + ":N" + Convert.ToString(pos - 1) + ")";
            workSheet.Cells[pos, 16] = "=Sum(P" + Convert.ToString(startpos) + ":P" + Convert.ToString(pos - 1) + ")";


            // 输出Excel文件并退出 
            try
            {
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }
        }

        /// <summary>
        /// 输出ZQL报关单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void ZqlDeclarationToExcel(string sheetPrefixName, string strShipNo, bool isPkgs)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 5;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 46;
            //当前行
            int curR = 20;
            //总价
            decimal amount = 0.0M;

            if (cntT < 0)
                return;

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);
            IDataReader readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);

            //输出头部信息
            workSheet.Cells[rowP * curP + 2, 2] = "SHANGHAI";
            workSheet.Cells[rowP * curP + 2, 7] = sdi.ShipDate;
            workSheet.Cells[rowP * curP + 4, 5] = sdi.ShipVia;
            workSheet.Cells[rowP * curP + 4, 1] = "3211940085";
            workSheet.Cells[rowP * curP + 4, 7] = sdi.Conveyance;
            workSheet.Cells[rowP * curP + 4, 10] = sdi.BL;
            workSheet.Cells[rowP * curP + 5, 1] = "镇江强灵照明有限公司";
            workSheet.Cells[rowP * curP + 6, 5] = sdi.Trade;
            workSheet.Cells[rowP * curP + 6, 12] = "T/T";
            workSheet.Cells[rowP * curP + 9, 5] = sdi.Country;
            workSheet.Cells[rowP * curP + 9, 8] = sdi.Harbor;
            workSheet.Cells[rowP * curP + 9, 11] = "SHANGHAI";
            workSheet.Cells[rowP * curP + 11, 1] = sdi.Verfication;
            workSheet.Cells[rowP * curP + 11, 3] = "FOB";

            while (readerH.Read())
            {
                if (isPkgs)
                {
                    workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyPkgs"]);
                    workSheet.Cells[rowP * curP + 13, 7] = "PKGS";
                }
                else
                {
                    workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyBox"]);
                    workSheet.Cells[rowP * curP + 13, 7] = "CTNS";
                }
                workSheet.Cells[rowP * curP + 13, 10] = string.Format("{0:#0.00}", readerH["Weight"]);
                workSheet.Cells[rowP * curP + 13, 12] = string.Format("{0:#0.00}", readerH["Net"]);

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
            }
            readerH.Close();

            //输出明细信息
            IDataReader readerD = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (readerD.Read())
            {
                if (cnt >= 5)
                {
                    cnt = 0;
                    curP += 1;
                    curR = 20;

                    //输出头部信息
                    workSheet.Cells[rowP * curP + 2, 2] = "SHANGHAI";
                    workSheet.Cells[rowP * curP + 2, 7] = sdi.ShipDate;
                    workSheet.Cells[rowP * curP + 4, 5] = sdi.ShipVia;
                    workSheet.Cells[rowP * curP + 4, 7] = sdi.Conveyance;
                    workSheet.Cells[rowP * curP + 4, 10] = sdi.BL;
                    workSheet.Cells[rowP * curP + 6, 5] = sdi.Trade;
                    workSheet.Cells[rowP * curP + 6, 12] = "T/T";
                    workSheet.Cells[rowP * curP + 9, 5] = sdi.Country;
                    workSheet.Cells[rowP * curP + 9, 8] = sdi.Harbor;
                    workSheet.Cells[rowP * curP + 9, 11] = "SHANGHAI";
                    workSheet.Cells[rowP * curP + 11, 1] = sdi.Verfication;
                    workSheet.Cells[rowP * curP + 11, 3] = "FOB";

                    readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
                    while (readerH.Read())
                    {
                        if (isPkgs)
                        {
                            workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyPkgs"]);
                            workSheet.Cells[rowP * curP + 13, 7] = "PKGS";
                        }
                        else
                        {
                            workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyBox"]);
                            workSheet.Cells[rowP * curP + 13, 7] = "CTNS";
                        }
                        workSheet.Cells[rowP * curP + 13, 10] = string.Format("{0:#0.00}", readerH["Weight"]);
                        workSheet.Cells[rowP * curP + 13, 12] = string.Format("{0:#0.00}", readerH["Net"]);

                        ////保护单元格
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                    }
                    readerH.Close();
                }

                string[] strCode = readerD["SCode"].ToString().Split(',');
                workSheet.Cells[rowP * curP + curR, 1] = strCode[2];
                workSheet.Cells[rowP * curP + curR, 2] = strCode[1];
                workSheet.Cells[rowP * curP + curR + 1, 2] = strCode[0];
                workSheet.Cells[rowP * curP + curR, 5] = string.Format("{0:#0}", readerD["QtyPcs"]);
                workSheet.Cells[rowP * curP + curR, 6] = "PCS/只";
                workSheet.Cells[rowP * curP + curR, 7] = "USD";
                workSheet.Cells[rowP * curP + curR, 8] = string.Format("{0:#0.000000}", readerD["AvgPrice"]);
                workSheet.Cells[rowP * curP + curR, 9] = "USD";
                //workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#0.00}", readerD["Amount"]);
                workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#0.000000}", Convert.ToInt32(readerD["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", readerD["AvgPrice"])));
                amount += Convert.ToInt32(readerD["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", readerD["AvgPrice"]));

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 1], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR + 1, 2], workSheet.Cells[rowP * curP + curR + 1, 2]).Locked = true;

                cnt += 1;
                curR += 2;
            }
            readerD.Close();
            readerD.Dispose();

            curR += 1;

            //输出Total:
            workSheet.Cells[rowP * curP + curR, 8] = "Total:";
            workSheet.Cells[rowP * curP + curR, 9] = "USD";
            workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#,###,##0.000000}", amount);

            //readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            //while (readerH.Read())
            //{
            //    workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#,###,##0.00}", readerH["Amount"]);
            //}
            //readerH.Close();
            //readerH.Dispose();

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 8], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;

            ////定义随机数
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// NPOI新版本输出ZQL报关单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void ZqlDeclarationToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(0);

            SID sid = new SID();

            #region //输出头部信息

            //输出头部信息

            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 5;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 46;
            //当前行
            int curR = 20;
            //总价
            decimal amount = 0.0M;

            if (cntT < 0)
                return;

            //输出头部信息
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);
            IDataReader readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);

            //输出头部信息
            workSheet.GetRow(rowP * curP + 1).CreateCell(1).SetCellValue("SHANGHAI");
            workSheet.GetRow(rowP * curP + 1).GetCell(6).SetCellValue(Convert.ToDateTime(sdi.ShipDate).ToString("MMMM. dd. yyyy", new System.Globalization.CultureInfo("en-us")).ToString());
            workSheet.GetRow(rowP * curP + 3).GetCell(4).SetCellValue(sdi.ShipVia);
            workSheet.GetRow(rowP * curP + 3).GetCell(0).SetCellValue("3211940085");
            workSheet.GetRow(rowP * curP + 3).GetCell(6).SetCellValue(sdi.Conveyance);
            workSheet.GetRow(rowP * curP + 3).GetCell(9).SetCellValue(sdi.BL);
            workSheet.GetRow(rowP * curP + 4).GetCell(0).SetCellValue("镇江强灵照明有限公司");
            workSheet.GetRow(rowP * curP + 5).GetCell(4).SetCellValue(sdi.Trade);
            workSheet.GetRow(rowP * curP + 5).GetCell(11).SetCellValue("T/T");
            workSheet.GetRow(rowP * curP + 8).CreateCell(1).SetCellValue(sdi.Country);
            workSheet.GetRow(rowP * curP + 8).GetCell(4).SetCellValue(sdi.Country);
            workSheet.GetRow(rowP * curP + 8).GetCell(7).SetCellValue(sdi.Harbor);
            workSheet.GetRow(rowP * curP + 8).GetCell(10).SetCellValue("SHANGHAI");
            workSheet.GetRow(rowP * curP + 10).GetCell(0).SetCellValue(sdi.Verfication);
            workSheet.GetRow(rowP * curP + 10).GetCell(2).SetCellValue("FOB");

            while (readerH.Read())
            {
                if (isPkgs)
                {
                    workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyPkgs"]));
                    workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("PKGS");
                }
                else
                {
                    workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyBox"]));
                    workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("CTNS");
                }
                workSheet.GetRow(rowP * curP + 12).GetCell(9).SetCellValue(string.Format("{0:#0.00}", readerH["Weight"]));
                workSheet.GetRow(rowP * curP + 12).GetCell(11).SetCellValue(string.Format("{0:#0.00}", readerH["Net"]));

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
            }
            readerH.Close();
            readerH.Dispose();
            workSheet.GetRow(rowP * curP + 17).CreateCell(1).SetCellValue("原产国:   中国");

            #endregion

            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            #endregion

            #region //输出明细信息

            //输出明细信息

            IDataReader readerD = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (readerD.Read())
            {
                if (cnt >= 5)
                {
                    cnt = 0;
                    curP += 1;
                    //curR = 19;

                    //输出头部信息
                    workSheet.GetRow(rowP * curP + 1).CreateCell(1).SetCellValue("SHANGHAI");
                    workSheet.GetRow(rowP * curP + 1).GetCell(6).SetCellValue(sdi.ShipDate);
                    workSheet.GetRow(rowP * curP + 3).GetCell(4).SetCellValue(sdi.ShipVia);
                    workSheet.GetRow(rowP * curP + 3).GetCell(6).SetCellValue(sdi.Conveyance);
                    workSheet.GetRow(rowP * curP + 3).GetCell(9).SetCellValue(sdi.BL);
                    workSheet.GetRow(rowP * curP + 5).GetCell(4).SetCellValue(sdi.Trade);
                    workSheet.GetRow(rowP * curP + 5).GetCell(11).SetCellValue("T/T");
                    workSheet.GetRow(rowP * curP + 8).GetCell(4).SetCellValue(sdi.Country);
                    workSheet.GetRow(rowP * curP + 8).GetCell(7).SetCellValue(sdi.Harbor);
                    workSheet.GetRow(rowP * curP + 8).GetCell(10).SetCellValue("SHANGHAI");
                    workSheet.GetRow(rowP * curP + 10).GetCell(0).SetCellValue(sdi.Verfication);
                    workSheet.GetRow(rowP * curP + 10).GetCell(2).SetCellValue("FOB");

                    readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
                    while (readerH.Read())
                    {
                        if (isPkgs)
                        {
                            workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyPkgs"]));
                            workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("PKGS");
                        }
                        else
                        {
                            workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue(string.Format("{0:#0}", readerH["QtyBox"]));
                            workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("CTNS");
                        }
                        workSheet.GetRow(rowP * curP + 12).GetCell(9).SetCellValue(double.Parse(string.Format("{0:#0.00}", readerH["Weight"])));
                        workSheet.GetRow(rowP * curP + 12).GetCell(11).SetCellValue(double.Parse(string.Format("{0:#0.00}", readerH["Net"])));

                        ////保护单元格
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                    }
                    readerH.Close();
                }

                ICellStyle styleP2 = workbook.CreateCellStyle();

                //styleP2.WrapText = true;
                styleP2.Alignment = HorizontalAlignment.Right;

                string[] strCode = readerD["SCode"].ToString().Split(',');
                workSheet.GetRow(rowP * curP + curR).CreateCell(0).SetCellValue(strCode[2]);
                workSheet.GetRow(rowP * curP + curR).GetCell(0).CellStyle = styleP2;
                workSheet.GetRow(rowP * curP + curR).CreateCell(1).SetCellValue(strCode[1]);
                //workSheet.GetRow(rowP * curP + curR).GetCell(1).CellStyle = styleP2;
                workSheet.GetRow(rowP * curP + curR + 1).CreateCell(1).SetCellValue(strCode[0]);
                //workSheet.GetRow(rowP * curP + curR + 1).GetCell(1).CellStyle = styleP2;
                workSheet.GetRow(rowP * curP + curR).CreateCell(4).SetCellValue(string.Format("{0:#0}", readerD["QtyPcs"]));
                workSheet.GetRow(rowP * curP + curR).GetCell(4).CellStyle = styleP2;
                workSheet.GetRow(rowP * curP + curR).CreateCell(5).SetCellValue("PCS/只");
                workSheet.GetRow(rowP * curP + curR).CreateCell(6).SetCellValue("USD");
                workSheet.GetRow(rowP * curP + curR).GetCell(7).SetCellValue(string.Format("{0:#0.000000}", readerD["AvgPrice"]));
                workSheet.GetRow(rowP * curP + curR).CreateCell(8).SetCellValue("USD");
                //workSheet.GetRow(rowP * curP + curR, 10] = string.Format("{0:#0.00}", readerD["Amount"]);
                workSheet.GetRow(rowP * curP + curR).GetCell(9).SetCellValue(double.Parse(string.Format("{0:#0.000000}", Convert.ToInt32(readerD["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", readerD["AvgPrice"])))));
                amount += Convert.ToInt32(readerD["QtyPcs"]) * Convert.ToDecimal(string.Format("{0:#0.000000}", readerD["AvgPrice"]));

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 1], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR + 1, 2], workSheet.Cells[rowP * curP + curR + 1, 2]).Locked = true;

                cnt += 1;
                curR += 2;
            }
            readerD.Close();
            readerD.Dispose();

            curR += 1;

            //输出Total:
            workSheet.GetRow(rowP * curP + curR).GetCell(7).SetCellValue("Total:");
            workSheet.GetRow(rowP * curP + curR).CreateCell(8).SetCellValue("USD");
            workSheet.GetRow(rowP * curP + curR).GetCell(9).SetCellValue(double.Parse(string.Format("{0:#,###,##0.000000}", amount)));

            #endregion

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);

                    Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                    localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                    localFile.Dispose();

                    ms.Flush();
                    ms.Position = 0;

                    workSheet = null;
                    detailSheet = null;
                    workbook = null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToExcel(string sheetPrefixName)
        {
            DateTime beforeTime;
            DateTime afterTime;

            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            beforeTime = DateTime.Now;
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;
            afterTime = DateTime.Now;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;
            workSheet.Select(true);
            workSheet.Cells.Locked = false;

            //定义随机数
            Random rnd = new Random(1);

            for (int i = 0; i < 10; i++)
            {
                workSheet.Cells[i + 2, 3] = string.Format("{0:#0}", 1000000 + rnd.NextDouble());
                workSheet.Cells[i + 2, 4] = string.Format("{0:#0.00}", 1000000 + rnd.NextDouble());
                workSheet.Cells.get_Range(workSheet.Cells[i + 2, 3], workSheet.Cells[i + 2, 3]).Locked = true;
            }

            workSheet.Protect("1234567890", false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // 输出Excel文件并退出 
            try
            {
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        //private void WriteToFile()
        //{
        //    //Write the stream data of workbook to the root directory
        //    FileStream file = new FileStream(outputFile, FileMode.Create);
        //    workbook.Write(file);
        //    file.Close();
        //}

        //private void InitializeWorkbook()
        //{
        //    //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
        //    //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
        //    FileStream file = new FileStream(templetFile, FileMode.Open, FileAccess.Read);

        //    workbook = new HSSFWorkbook(file);
        //}
      }
}
