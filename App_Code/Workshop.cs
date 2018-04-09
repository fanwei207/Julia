using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using Excel;
//using System.Runtime.InteropServices;
//using Microsoft.Office.Tools.Excel;
using APPWS;

namespace WSExcelHelper 
{
    ///   <summary> 
    /// 功能说明：套用模板输出Excel
    ///   </summary> 
    public class WSExcelHelper
    {
        protected string templetFile = null;
        protected string outputFile = null;
        protected object missing = Missing.Value;

        ///   <summary> 
        /// 构造函数，需指定模板文件和输出文件完整路径
        ///   </summary> 
        ///   <param name="templetFilePath"> Excel模板文件路径 </param> 
        ///   <param name="outputFilePath"> 输出Excel文件路径 </param> 
        public WSExcelHelper(string templetFilePath, string outputFilePath)
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
        /// 输出次品分析
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        public void FailureAnalysis(string _strDate, string _strDate1, string _strSite, string _strCC, string _strLine, string _strPart, string _txtCC, string _txtLine)
        {
            if (_strDate == null || _strDate.Trim() == "")
                return;

            if (_strSite == null || _strSite.Trim() == "")
                return;
                
            string sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(2);
            workSheet.Name = sheetPrefixName;

            for (int i = 1; i < 50; i++)
            {
                workSheet.Cells[i, 1] = "";
                workSheet.Cells[i, 2] = "";
            }
            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            //输出头部信息
            //workSheet.Cells[2, 2] = sdi.Customer;
            //workSheet.Cells[2, 10] = sdi.Verfication;
            //workSheet.Cells[3, 10] = sdi.ShipDate;
            //workSheet.Cells[4, 2] = sdi.Harbor;
            //workSheet.Cells[4, 11] = "T/T";
            //workSheet.Cells[5, 10] = sdi.ShipNo;

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[6, 10], workSheet.Cells[6, 11]).Locked = true;

            WS myws = new WS();   

            //输出明细信息
            int pos = 1;

            string strPlant = "";
            string strPlantCode = "";
            string strCC = "";
            string strLine = "";
            string strLineName = "";
            Decimal qty = 0;
            Decimal qty_bad = 0;
            string strStatus = "";

            Decimal total = 0;

            IDataReader reader = null;
            reader = (IDataReader)myws.SelectFailureData(_strDate, _strDate1, _strSite, _strCC, _strLine, _strPart);
            while (reader.Read())
            {
                if (strStatus != "" && strStatus != reader[6].ToString().Trim())
                    {
                        if (strStatus.Trim() != "正品")
                        {
                            if (qty != 0)
                            {
                                workSheet.Cells[pos, 1] = qty_bad / qty * 100;
                            }
                            else
                            {
                                workSheet.Cells[pos, 1] = qty_bad ;
                            }
                            workSheet.Cells[pos, 2] = strStatus;
                            total = total + qty_bad;
                            pos = pos + 1;
                        }

                        strPlant = "";
                        strCC = "";
                        strStatus = "";
                        qty_bad = 0;
                    }

                    strPlantCode = reader[0].ToString().Trim();
                    strPlant = reader[1].ToString().Trim();
                    strCC = reader[2].ToString().Trim();
                    strLine = reader[3].ToString().Trim();
                    strLineName = reader[4].ToString().Trim();
                    strStatus = reader[6].ToString().Trim();
                    qty_bad = qty_bad + Convert.ToDecimal(reader[5]);
                    qty = Convert.ToDecimal(reader[7]);
            }

            reader.Close();

            if (strStatus != "")
            {
                if (strStatus.Trim() != "正品")
                {
                    if (qty != 0)
                    {
                        workSheet.Cells[pos, 1] = qty_bad / qty * 100;
                    }
                    else
                    {
                        workSheet.Cells[pos, 1] = qty_bad;
                    }

                    workSheet.Cells[pos, 2] = strStatus;
                    total = total + qty_bad;
                    pos = pos + 1;
                }
            }

            Excel.Chart xlChart = (Excel.Chart)workBook.Charts.get_Item(1);

            if (qty != 0)
            {
                xlChart.ChartTitle.Text = _txtCC + " " + _txtLine + " " + _strPart + " " + _strDate + "-" + _strDate1 + " 次品原因分析  流量:" + Convert.ToString(Math.Round(qty, 0)) + "  次品率:" + Convert.ToString(Math.Round(total / qty * 100, 2)) + "%";
            }
            else
            {
                xlChart.ChartTitle.Text = _txtCC + " " + _txtLine + " " + _strPart + " " + _strDate + "-" + _strDate1 + " 次品原因分析  流量:" + Convert.ToString(Math.Round(qty, 0)) + "  次品数:" + Convert.ToString(Math.Round(total, 2));
            }

            //Excel.Chart xlChart = (Excel.Chart)workBook.Charts.Add(
            //                       Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //Excel.Range chartRage = workSheet.get_Range("A1:A" + Convert.ToString(pos - 1), "B1:B" + Convert.ToString(pos - 1));

            //xlChart.ChartWizard(chartRage, Excel.XlChartType.xlLineMarkers,
            //    Missing.Value, Excel.XlRowCol.xlColumns, 0, 0, true,
            //    "次品分析", "分类", "比例", Missing.Value);

            //////保存图表
            ////xlBook.SaveAs(Application.StartupPath + "\\图表.xls", Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            ////    Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value);




           ////保护单元格
           //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;


            //输出Total:
            //workSheet.Cells[pos, 10] = "Total:";
            //workSheet.Cells[pos, 11] = "USD";

            //Total处划线
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

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
        /// 输出10分钟次品流量
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        public void FailureAnalysis10(string _strDate, string _strUser, string _txtCC, string _txtLine, string _strPart)
        {
            if (_strUser == null || _strUser.Trim() == "")
                return;

            string sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(2);
            workSheet.Name = sheetPrefixName;

            for (int i = 1; i < 150; i++)
            {
                workSheet.Cells[i, 1] = "";
            }
            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            //输出头部信息
            //workSheet.Cells[2, 2] = sdi.Customer;
            //workSheet.Cells[2, 10] = sdi.Verfication;
            //workSheet.Cells[3, 10] = sdi.ShipDate;
            //workSheet.Cells[4, 2] = sdi.Harbor;
            //workSheet.Cells[4, 11] = "T/T";
            //workSheet.Cells[5, 10] = sdi.ShipNo;

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[6, 10], workSheet.Cells[6, 11]).Locked = true;

            WS myws = new WS();

            //输出明细信息
            int pos = 1;

            IDataReader reader = null;
            reader = (IDataReader)myws.SelectFailureData10(_strUser);
            while (reader.Read())
            {
                workSheet.Cells[pos, 1] = reader[1].ToString().Trim();
                pos = pos + 1;
            }

            reader.Close();


            Excel.Chart xlChart = (Excel.Chart)workBook.Charts.get_Item(1);

            xlChart.ChartTitle.Text = _txtCC + " " + _txtLine + " " + _strPart + " " + _strDate + " 15分钟次品流量";

            //Excel.Chart xlChart = (Excel.Chart)workBook.Charts.Add(
            //                       Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //Excel.Range chartRage = workSheet.get_Range("A1:A" + Convert.ToString(pos - 1), "B1:B" + Convert.ToString(pos - 1));

            //xlChart.ChartWizard(chartRage, Excel.XlChartType.xlLineMarkers,
            //    Missing.Value, Excel.XlRowCol.xlColumns, 0, 0, true,
            //    "次品分析", "分类", "比例", Missing.Value);

            //////保存图表
            ////xlBook.SaveAs(Application.StartupPath + "\\图表.xls", Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            ////    Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value);




            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;


            //输出Total:
            //workSheet.Cells[pos, 10] = "Total:";
            //workSheet.Cells[pos, 11] = "USD";

            //Total处划线
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

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
        /// 输出10分钟正品流量
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        public void GoodAnalysis10(string _strDate, string _strUser, string _txtCC, string _txtLine, string _strPart)
        {
            if (_strUser == null || _strUser.Trim() == "")
                return;

            string sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(2);
            workSheet.Name = sheetPrefixName;

            for (int i = 1; i < 150; i++)
            {
                workSheet.Cells[i, 1] = "";
            }
            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            //输出头部信息
            //workSheet.Cells[2, 2] = sdi.Customer;
            //workSheet.Cells[2, 10] = sdi.Verfication;
            //workSheet.Cells[3, 10] = sdi.ShipDate;
            //workSheet.Cells[4, 2] = sdi.Harbor;
            //workSheet.Cells[4, 11] = "T/T";
            //workSheet.Cells[5, 10] = sdi.ShipNo;

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[6, 10], workSheet.Cells[6, 11]).Locked = true;

            WS myws = new WS();

            //输出明细信息
            int pos = 1;

            IDataReader reader = null;
            reader = (IDataReader)myws.SelectGoodData10(_strUser);
            while (reader.Read())
            {
                workSheet.Cells[pos, 1] = reader[1].ToString().Trim();
                pos = pos + 1;

            }

            reader.Close();


            Excel.Chart xlChart = (Excel.Chart)workBook.Charts.get_Item(1);

            xlChart.ChartTitle.Text = _txtCC + " " + _txtLine + " " + _strPart + " " + _strDate + " 15分钟正品流量";

            //Excel.Chart xlChart = (Excel.Chart)workBook.Charts.Add(
            //                       Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //Excel.Range chartRage = workSheet.get_Range("A1:A" + Convert.ToString(pos - 1), "B1:B" + Convert.ToString(pos - 1));

            //xlChart.ChartWizard(chartRage, Excel.XlChartType.xlLineMarkers,
            //    Missing.Value, Excel.XlRowCol.xlColumns, 0, 0, true,
            //    "次品分析", "分类", "比例", Missing.Value);

            //////保存图表
            ////xlBook.SaveAs(Application.StartupPath + "\\图表.xls", Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            ////    Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value);




            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;


            //输出Total:
            //workSheet.Cells[pos, 10] = "Total:";
            //workSheet.Cells[pos, 11] = "USD";

            //Total处划线
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

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
        /// 输出完工入库工时和考勤工时比较
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>  
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        public void WOCompAtten(string _userID, string _year, string _month, string _strSite, string _strCC, string _strTitle)
        {
            if (_userID == null || _userID.Trim() == "")
                return;

            string sheetPrefixName = " Sheet1";

            // 创建一个Application对象并使其可见 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // 打开模板文件，得到WorkBook对象 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // 得到WorkSheet对象 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(2);
            workSheet.Name = sheetPrefixName;

            for (int i = 1; i < 32; i++)
            {
                workSheet.Cells[i, 1] = "";
                workSheet.Cells[i, 2] = "";
            }
            ////取消整个工作表保护
            //workSheet.Cells.Locked = false;

            //输出头部信息
            //workSheet.Cells[2, 2] = sdi.Customer;
            //workSheet.Cells[2, 10] = sdi.Verfication;
            //workSheet.Cells[3, 10] = sdi.ShipDate;
            //workSheet.Cells[4, 2] = sdi.Harbor;
            //workSheet.Cells[4, 11] = "T/T";
            //workSheet.Cells[5, 10] = sdi.ShipNo;

            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[6, 10], workSheet.Cells[6, 11]).Locked = true;

            WS myws = new WS();

            //输出明细信息
            int pos = 1;

            Decimal total1 = 0;
            Decimal total2 = 0;


            IDataReader reader = null;
            reader = (IDataReader)myws.SelectWOCompAttenData(_userID, _year, _month, _strSite, _strCC);
            while (reader.Read())
            {
                try
                {
                    if (Convert.ToDecimal(reader[5]) != 0)
                    {
                        workSheet.Cells[pos, 1] = Convert.ToDecimal(reader[3]) / Convert.ToDecimal(reader[5]) * 100;
                    }
                    else
                    {
                        workSheet.Cells[pos, 1] = 0;

                    }
                    workSheet.Cells[pos, 2] = Convert.ToDateTime(reader[1]);

                    total1 = total1 + Convert.ToDecimal(reader[3]);
                    total2 = total2 + Convert.ToDecimal(reader[5]);
                }
                catch
                {

                }

                pos = pos + 1;
            }

            reader.Close();

            Excel.Chart xlChart = (Excel.Chart)workBook.Charts.get_Item(1);

            if (total2 !=0)
            {
                xlChart.ChartTitle.Text = _strTitle + "  月平均： " + Convert.ToString(Math.Round(total1 / total2 * 100, 2)) + "%";
            }
            else
            {
                xlChart.ChartTitle.Text = _strTitle ;
            }

            //Excel.Chart xlChart = (Excel.Chart)workBook.Charts.Add(
            //                       Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //Excel.Range chartRage = workSheet.get_Range("A1:A" + Convert.ToString(pos - 1), "B1:B" + Convert.ToString(pos - 1));

            //xlChart.ChartWizard(chartRage, Excel.XlChartType.xlLineMarkers,
            //    Missing.Value, Excel.XlRowCol.xlColumns, 0, 0, true,
            //    "次品分析", "分类", "比例", Missing.Value);

            //////保存图表
            ////xlBook.SaveAs(Application.StartupPath + "\\图表.xls", Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            ////    Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value,
            ////    Missing.Value, Missing.Value, Missing.Value);




            ////保护单元格
            //workSheet.Cells.get_Range(workSheet.Cells[pos, 2], workSheet.Cells[pos, 12]).Locked = true;


            //输出Total:
            //workSheet.Cells[pos, 10] = "Total:";
            //workSheet.Cells[pos, 11] = "USD";

            //Total处划线
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            //workSheet.get_Range(workSheet.Cells[pos, 11], workSheet.Cells[pos, 12]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

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


    }
}
