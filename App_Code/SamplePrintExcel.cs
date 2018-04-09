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
using System.IO;
using NPOI.SS;

namespace SamplePrint
{
    ///   <summary> 
    /// 功能说明：套用模板输出Excel
    ///   </summary> 
    public class SamplePrintExecl
    {
        protected string templetFile = null;
        protected string outputFile = null;
        protected object missing = Missing.Value;
        //protected CultureInfo culture = new CultureInfo("en-US");
        //private HSSFWorkbook workbook;
        adamClass adam = new adamClass();
        SID_Packing packing = new SID_Packing();

        ///   <summary> 
        /// 构造函数，需指定模板文件和输出文件完整路径
        ///   </summary> 
        ///   <param name="templetFilePath"> Excel模板文件路径 </param> 
        ///   <param name="outputFilePath"> 输出Excel文件路径 </param> 
        public SamplePrintExecl(string templetFilePath, string outputFilePath)
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
        /// 老版本输出ATL发票
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void ATLInvoiceToExcel(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate,string PicLogo, string PicSeal)
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
            int cntP = 2;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 18;

            if (cntT < 0)
                return;

            //输出头部信息

            #region

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

            //if (DT.Rows.Count <= 0)
            //{
            //    //this.Alert("此出运单号信息不全！");
            //    return;
            //}

            //输出头部信息
            workSheet.Cells[rowP * curP + 3, 6] = "上  海  强  凌  电  子  有  限  公  司";

            workSheet.Cells[rowP * curP + 5, 2] = "SHANGHAI QIANG LING";
            workSheet.Cells[rowP * curP + 5, 6] = "出  口  专  用";

            workSheet.Cells[rowP * curP + 6, 2] = "ELECTRONIC CO., LTD";
            workSheet.Cells[rowP * curP + 6, 6] = "FOR  EXPORT  ONLY";

            workSheet.Cells[rowP * curP + 8, 2] = "INVOICE";
            workSheet.Cells[rowP * curP + 8, 6] = "发  票";

            workSheet.Cells[rowP * curP + 10, 2] = "HU SONG GUO S HUI WAI (97) ZI NO. 80";
            workSheet.Cells[rowP * curP + 10, 6] = "沪 松 国 税 外 (97) 字 第 80 号";
            workSheet.Cells[rowP * curP + 12, 2] = "TO:";
            workSheet.Cells[rowP * curP + 12, 4] = "AURORA TECHNOLOGIES LIMITED";
            workSheet.Cells[rowP * curP + 12, 9] = "NO:";

            workSheet.Cells[rowP * curP + 13, 4] = "ADDRESS:UNITS 1601-3 TAI RUNG BUILDING";

            workSheet.Cells[rowP * curP + 14, 4] = "8 FLEMING ROAD";
            workSheet.Cells[rowP * curP + 14, 9] = "DATE:";

            workSheet.Cells[rowP * curP + 15, 4] = "HONG KONG";

            workSheet.Cells[rowP * curP + 16, 2] = "SHIP TO:";
            workSheet.Cells[rowP * curP + 16, 9] = "L/C NO.:";

            workSheet.Cells[rowP * curP + 17, 2] = "NO";
            workSheet.Cells[rowP * curP + 17, 3] = "PO#";
            workSheet.Cells[rowP * curP + 17, 4] = "PART";
            workSheet.Cells[rowP * curP + 17, 5] = "DESCRIPTION";
            workSheet.Cells[rowP * curP + 17, 6] = "QTY";
            workSheet.Cells[rowP * curP + 17, 7] = "UNIT";
            workSheet.Cells[rowP * curP + 17, 8] = "UNIT PRICE";
            workSheet.Cells[rowP * curP + 17, 9] = "CURRENCY";
            workSheet.Cells[rowP * curP + 17, 10] = "AMOUNT";
            workSheet.Cells[rowP * curP + 17, 11] = "CURRENCY";

            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 17, 2], workSheet.Cells[rowP * curP + 17, 11]).Borders.LineStyle = 1;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 17, 2], workSheet.Cells[rowP * curP + 17, 11]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 17, 2], workSheet.Cells[rowP * curP + 17, 11]).Borders.get_Item(XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlMedium;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 17, 2], workSheet.Cells[rowP * curP + 17, 11]).Borders.get_Item(XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlMedium;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 17, 2], workSheet.Cells[rowP * curP + 17, 11]).Borders.get_Item(XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlMedium;

            //string picpath = Server.MapPath(@"D://TCP-File//Julia//images//Seal.jpg");
            //workSheet.Shapes.AddPicture(PicSeal, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)280, (float)680, (float)55, (float)55);



            if (DT.Rows.Count > 0)
            {
                workSheet.Cells[rowP * curP + 12, 10] = DT.Rows[0]["SID_nbr"].ToString();
                workSheet.Cells[rowP * curP + 14, 10] = DT.Rows[0]["SID_shipdate"].ToString();
                workSheet.Cells[rowP * curP + 16, 4] = DT.Rows[0]["SID_shipto"].ToString();
                workSheet.Cells[rowP * curP + 16, 10] = DT.Rows[0]["SID_lcno"].ToString();

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
            }

            workSheet.Cells[rowP * curP + 50, 4] = "Country of Origin:  China";

            workSheet.Cells[rowP * curP + 52, 4] = "SHANG HAI QIANG LING";
            workSheet.Cells[rowP * curP + 53, 4] = "ELECTRONIC  CO., LTD";
            workSheet.Cells[rowP * curP + 54, 5] = "'----------------------";
            workSheet.Cells[rowP * curP + 55, 5] = "AUTHORIZED.SIGNATURE";
            workSheet.Cells[rowP * curP + 55, 9] = "NO.139 WANG DONG ROAD (S.)";
            workSheet.Cells[rowP * curP + 56, 3] = "地址:";
            workSheet.Cells[rowP * curP + 56, 4] = "上海松江泗泾望东南路139号      邮编:201601";
            workSheet.Cells[rowP * curP + 56, 9] = " SJ JING SONG JIANG";
            workSheet.Cells[rowP * curP + 57, 3] = "电话:";
            workSheet.Cells[rowP * curP + 57, 4] = "021-57619108";
            workSheet.Cells[rowP * curP + 57, 5] = "传真:021-57619961";
            workSheet.Cells[rowP * curP + 57, 9] = "SHANG HAI 201601 ,CHINA";
            workSheet.Cells[rowP * curP + 58, 9] = "TEL: 021-57619108";
            workSheet.Cells[rowP * curP + 59, 9] = "FAX:021-57619961";

            #endregion

            //输出明细信息


            System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);
            if (PackingDet.Rows.Count > 0)
            {

                for (int i = 0; i < PackingDet.Rows.Count; i++)
                {
                    int str = i / 2;
                    if (cnt >= 30)
                    {
                        cnt = 0;
                        curP += 1;
                        curR =  29;

                        workSheet.Cells[rowP * curP + 14, 6] = "上  海  强  凌  电  子  有  限  公  司";

                        workSheet.Cells[rowP * curP + 16, 2] = "SHANGHAI QAING LING";
                        workSheet.Cells[rowP * curP + 16, 6] = "出  口  专  用";

                        workSheet.Cells[rowP * curP + 17, 2] = "ELECTRONIC CO., LTD";
                        workSheet.Cells[rowP * curP + 17, 6] = "FOR  EXPORT  ONLY";

                        workSheet.Cells[rowP * curP + 19, 2] = "INVICE";
                        workSheet.Cells[rowP * curP + 19, 6] = "发  票";

                        workSheet.Cells[rowP * curP + 21, 2] = "HU SONG GUO S HUI WAI (97) ZI NO. 80";
                        workSheet.Cells[rowP * curP + 21, 6] = "沪 松 国 税 外 (97) 字 第 80 号";
                        workSheet.Cells[rowP * curP + 23, 2] = "TO:";
                        workSheet.Cells[rowP * curP + 23, 4] = "TECHNICAL CONSUMER PRODUCTS, INC";
                        workSheet.Cells[rowP * curP + 23, 9] = "NO:";

                        workSheet.Cells[rowP * curP + 24, 4] = "325 CAMPUS DRIVE AURORA, OHIO 44202";
                        workSheet.Cells[rowP * curP + 24, 9] = "DATE:";

                        workSheet.Cells[rowP * curP + 25, 4] = "U.S.A.";

                        workSheet.Cells[rowP * curP + 2, 2] = "SHIP TO:";
                        workSheet.Cells[rowP * curP + 27, 9] = "L/C NO.:";
                        if (DT.Rows.Count > 0)
                        {
                            workSheet.Cells[rowP * curP + 23, 10] = DT.Rows[0]["SID_nbr"].ToString();
                            workSheet.Cells[rowP * curP + 25, 10] = DT.Rows[0]["SID_shipdate"].ToString();
                            workSheet.Cells[rowP * curP + 27, 4] = DT.Rows[0]["SID_shipto"].ToString();
                            workSheet.Cells[rowP * curP + 27, 10] = DT.Rows[0]["SID_lcno"].ToString();

                            ////保护单元格
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                        }

                        workSheet.Cells[rowP * curP + 28, 2] = "NO";
                        workSheet.Cells[rowP * curP + 28, 3] = "PO#";
                        workSheet.Cells[rowP * curP + 28, 4] = "PART";
                        workSheet.Cells[rowP * curP + 28, 5] = "DESCRIPTION";
                        workSheet.Cells[rowP * curP + 28, 6] = "QTY";
                        workSheet.Cells[rowP * curP + 28, 7] = "UNIT";
                        workSheet.Cells[rowP * curP + 28, 8] = "UNIT PRICE";
                        workSheet.Cells[rowP * curP + 28, 9] = "CURRENCY";
                        workSheet.Cells[rowP * curP + 28, 10] = "AMOUNT";
                        workSheet.Cells[rowP * curP + 28, 11] = "CURRENCY";

                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 28, 2], workSheet.Cells[rowP * curP + 28, 11]).Borders.LineStyle = 1;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 28, 2], workSheet.Cells[rowP * curP + 28, 11]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 28, 2], workSheet.Cells[rowP * curP + 28, 11]).Borders.get_Item(XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlMedium;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 28, 2], workSheet.Cells[rowP * curP + 28, 11]).Borders.get_Item(XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlMedium;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 28, 2], workSheet.Cells[rowP * curP + 28, 11]).Borders.get_Item(XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlMedium;

                        //string picpath = Server.MapPath(@"D://TCP-File//Julia//images//Seal.jpg");
                        //workSheet.Shapes.AddPicture(PicLogo, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)280, (float)680, (float)55, (float)55);

                        //workSheet.Shapes.AddPicture(PicSeal, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)280, (float)680, (float)55, (float)55);

                        workSheet.Cells[rowP * curP + 55, 4] = "Country of Origin:  China";

                        workSheet.Cells[rowP * curP + 64, 4] = "SHANG HAI QIANG LING";
                        workSheet.Cells[rowP * curP + 65, 4] = "ELECTRONIC  CO., LTD";
                        workSheet.Cells[rowP * curP + 66, 5] = "'----------------------";
                        workSheet.Cells[rowP * curP + 67, 5] = "AUTHORIZED.SIGNATURE";
                        workSheet.Cells[rowP * curP + 67, 9] = "NO.139 WANG DONG ROAD (S.)";
                        workSheet.Cells[rowP * curP + 68, 3] = "地址:";
                        workSheet.Cells[rowP * curP + 68, 4] = "上海松江泗泾望东南路139号      邮编:201601";
                        workSheet.Cells[rowP * curP + 68, 9] = " SJ JING SONG JIANG";
                        workSheet.Cells[rowP * curP + 69, 3] = "电话:";
                        workSheet.Cells[rowP * curP + 69, 4] = "021-57619108";
                        workSheet.Cells[rowP * curP + 69, 5] = "传真:021-57619961";
                        workSheet.Cells[rowP * curP + 69, 9] = "SHANG HAI 201601 ,CHINA";
                        workSheet.Cells[rowP * curP + 70, 9] = "TEL: 021-57619108";
                        workSheet.Cells[rowP * curP + 71, 9] = "FAX:021-57619961";
                    }
                    else
                    {
                        Excel.Range range = app.get_Range(workSheet.Cells[rowP * curP + 60, 1], workSheet.Cells[rowP * curP + 200, 1]);
                        range.Select();
                        range.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        //range.get_Item(1);
                        Excel.Range range1 = app.get_Range(workSheet.Cells[rowP * curP + 1, 1], workSheet.Cells[rowP * curP + 1, 1]);
                        range1.Select();
                    }

                    workSheet.Cells[rowP * curP + curR, 2] = PackingDet.Rows[i]["sid_so_line"].ToString();
                    workSheet.Cells[rowP * curP + curR, 3] = PackingDet.Rows[i]["SID_PO"].ToString();
                    workSheet.Cells[rowP * curP + curR, 4] = PackingDet.Rows[i]["sid_cust_part"].ToString();
                    workSheet.Cells[rowP * curP + curR, 5] = PackingDet.Rows[i]["sid_cust_partdesc"].ToString();
                    workSheet.Cells[rowP * curP + curR, 6] = PackingDet.Rows[i]["sid_qty_pcs"].ToString();
                    workSheet.Cells[rowP * curP + curR, 7] = PackingDet.Rows[i]["sid_qty_unit"].ToString();
                    workSheet.Cells[rowP * curP + curR, 8] = PackingDet.Rows[i]["SID_price1"].ToString();
                    workSheet.Cells[rowP * curP + curR, 9] = PackingDet.Rows[i]["SID_currency"].ToString();
                    workSheet.Cells[rowP * curP + curR, 10] = PackingDet.Rows[i]["amount1"].ToString();
                    workSheet.Cells[rowP * curP + curR, 11] = PackingDet.Rows[i]["SID_currency1"].ToString();

                    cnt += 1;
                    curR += 1;
                }

            }

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
        /// NPOI新版本输出ATL发票:Excel
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="stddate"></param>
        /// <param name="enddate"></param>
        public void ATLInvoiceToExcelNewByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate, string PicLogo, string PicSeal)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(1);

            SID sid = new SID();
            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 29;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 17;

            if (cntT < 0)
                return;

            #region //输出头部信息

            //输出头部信息

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

            //输出头部信息

            workSheet.GetRow(rowP * curP + 2).GetCell(5).SetCellValue("上  海  强  凌  电  子  有  限  公  司");
            workSheet.GetRow(rowP * curP + 4).GetCell(1).SetCellValue("SHANGHAI QIANG LING");
            workSheet.GetRow(rowP * curP + 4).GetCell(5).SetCellValue("出  口  专  用");

            workSheet.GetRow(rowP * curP + 5).GetCell(1).SetCellValue("ELECTRONIC CO., LTD");
            workSheet.GetRow(rowP * curP + 5).GetCell(5).SetCellValue("FOR  EXPORT  ONLY");

            workSheet.GetRow(rowP * curP + 7).GetCell(1).SetCellValue("INVOICE");
            workSheet.GetRow(rowP * curP + 7).GetCell(5).SetCellValue("发  票");

            workSheet.GetRow(rowP * curP + 9).GetCell(1).SetCellValue("HU SONG GUO S HUI WAI (97) ZI NO. 80");
            workSheet.GetRow(rowP * curP + 9).GetCell(5).SetCellValue("沪 松 国 税 外 (97) 字 第 80 号");

            workSheet.GetRow(rowP * curP + 11).GetCell(1).SetCellValue("TO:");
            workSheet.GetRow(rowP * curP + 11).GetCell(3).SetCellValue("AURORA TECHNOLOGIES LIMITED");
            workSheet.GetRow(rowP * curP + 11).GetCell(8).SetCellValue("NO:");

            workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue("ADDRESS:UNITS 1601-3 TAI RUNG BUILDING");

            workSheet.GetRow(rowP * curP + 13).GetCell(3).SetCellValue("8 FLEMING ROAD");
            workSheet.GetRow(rowP * curP + 13).GetCell(8).SetCellValue("DATE:");

            workSheet.GetRow(rowP * curP + 14).GetCell(3).SetCellValue("HONG KONG");

            workSheet.GetRow(rowP * curP + 15).GetCell(1).SetCellValue("SHIP TO:");
            workSheet.GetRow(rowP * curP + 15).GetCell(8).SetCellValue("L/C NO.:");

            workSheet.GetRow(rowP * curP + 16).GetCell(1).SetCellValue("NO");
            workSheet.GetRow(rowP * curP + 16).GetCell(2).SetCellValue("PO#");
            workSheet.GetRow(rowP * curP + 16).GetCell(3).SetCellValue("PART");
            workSheet.GetRow(rowP * curP + 16).GetCell(4).SetCellValue("DESCRIPTION");
            workSheet.GetRow(rowP * curP + 16).GetCell(5).SetCellValue("QTY");
            workSheet.GetRow(rowP * curP + 16).GetCell(6).SetCellValue("UNIT");
            workSheet.GetRow(rowP * curP + 16).GetCell(7).SetCellValue("UNIT PRICE");
            workSheet.GetRow(rowP * curP + 16).GetCell(8).SetCellValue("CURRENCY");
            workSheet.GetRow(rowP * curP + 16).GetCell(9).SetCellValue("AMOUNT");
            workSheet.GetRow(rowP * curP + 16).GetCell(10).SetCellValue("CURRENCY");
            if (DT.Rows.Count > 0)
            {
                workSheet.GetRow(rowP * curP + 11).CreateCell(9).SetCellValue(DT.Rows[0]["SID_nbr"].ToString());
                workSheet.GetRow(rowP * curP + 13).GetCell(9).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                workSheet.GetRow(rowP * curP + 15).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
                workSheet.GetRow(rowP * curP + 15).GetCell(9).SetCellValue(DT.Rows[0]["SID_lcno"].ToString());
            }


            #endregion


            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);
            workSheet.CreateRow(rowP * curP + 49).CreateCell(3).SetCellValue("Country of Origin:  China");
            workSheet.CreateRow(rowP * curP + 51).CreateCell(3).SetCellValue("SHANG HAI QIANG LING");
            workSheet.CreateRow(rowP * curP + 52).CreateCell(3).SetCellValue("ELECTRONIC  CO., LTD");
            for (int i = 53; i <= 58; i++)
            {
                IRow iRowFoot = workSheet.CreateRow(rowP * curP + i);
                if (i == 53)
                {
                    iRowFoot.CreateCell(4).SetCellValue("----------------------");
                    iRowFoot.GetCell(4).CellStyle = styleFoot;
                }
                if (i == 54)
                {
                    iRowFoot.CreateCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                    iRowFoot.GetCell(4).CellStyle = styleFoot;
                    iRowFoot.CreateCell(8).SetCellValue("NO.139 WANG DONG ROAD (S.)");
                }
                if (i == 55)
                {
                    iRowFoot.CreateCell(2).SetCellValue("地址:");
                    iRowFoot.CreateCell(3).SetCellValue("上海松江泗泾望东南路139号      邮编:201601");
                    iRowFoot.CreateCell(8).SetCellValue(" SJ JING SONG JIANG");
                }
                if (i == 56)
                {
                    iRowFoot.CreateCell(2).SetCellValue("电话:");
                    iRowFoot.CreateCell(3).SetCellValue("021-57619108");
                    iRowFoot.CreateCell(4).SetCellValue("传真:021-57619961");
                    iRowFoot.CreateCell(8).SetCellValue("SHANG HAI 201601 ,CHINA");
                }
            }
            workSheet.CreateRow(rowP * curP + 57).CreateCell(8).SetCellValue("TEL: 021-57619108");
            workSheet.CreateRow(rowP * curP + 58).CreateCell(8).SetCellValue("FAX:021-57619961");

            #endregion

            #region //输出明细信息

            //输出明细信息
            //DataSet _dataset = SelectDailyIncoming(uID, stddate, enddate);
            System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);


            int nCols = PackingDet.Columns.Count;
            int nRows = 3;

            foreach (DataRow row in PackingDet.Rows)
            {
                if (cnt > cntP)
                {
                    curP = 1;

                    //输出头部信息
                    if (cnt == cntP + 1)
                    {
                        workSheet.GetRow(rowP * curP + 13).GetCell(5).SetCellValue("上  海  强  凌  电  子  有  限  公  司");
                        workSheet.GetRow(rowP * curP + 15).GetCell(1).SetCellValue("SHANGHAI QIANG LING");
                        workSheet.GetRow(rowP * curP + 15).GetCell(5).SetCellValue("出  口  专  用");

                        workSheet.GetRow(rowP * curP + 16).GetCell(1).SetCellValue("ELECTRONIC CO., LTD");
                        workSheet.GetRow(rowP * curP + 16).GetCell(5).SetCellValue("FOR  EXPORT  ONLY");

                        workSheet.GetRow(rowP * curP + 18).GetCell(1).SetCellValue("INVOICE");
                        workSheet.GetRow(rowP * curP + 18).GetCell(5).SetCellValue("发  票");

                        workSheet.GetRow(rowP * curP + 20).GetCell(1).SetCellValue("HU SONG GUO S HUI WAI (97) ZI NO. 80");
                        workSheet.GetRow(rowP * curP + 20).GetCell(5).SetCellValue("沪 松 国 税 外 (97) 字 第 80 号");

                        workSheet.GetRow(rowP * curP + 22).GetCell(1).SetCellValue("TO:");
                        workSheet.GetRow(rowP * curP + 22).GetCell(3).SetCellValue("AURORA TECHNOLOGIES LIMITED");
                        workSheet.GetRow(rowP * curP + 22).GetCell(8).SetCellValue("NO:");

                        workSheet.GetRow(rowP * curP + 23).GetCell(3).SetCellValue("ADDRESS:UNITS 1601-3 TAI RUNG BUILDING");

                        workSheet.GetRow(rowP * curP + 24).GetCell(3).SetCellValue("8 FLEMING ROAD");
                        workSheet.GetRow(rowP * curP + 24).GetCell(8).SetCellValue("DATE:");

                        workSheet.GetRow(rowP * curP + 25).GetCell(3).SetCellValue("HONG KONG");

                        workSheet.GetRow(rowP * curP + 26).GetCell(1).SetCellValue("SHIP TO:");
                        workSheet.GetRow(rowP * curP + 26).GetCell(8).SetCellValue("L/C NO.:");

                        workSheet.GetRow(rowP * curP + 27).GetCell(1).SetCellValue("NO");
                        workSheet.GetRow(rowP * curP + 27).GetCell(2).SetCellValue("PO#");
                        workSheet.GetRow(rowP * curP + 27).GetCell(3).SetCellValue("PART");
                        workSheet.GetRow(rowP * curP + 27).GetCell(4).SetCellValue("DESCRIPTION");
                        workSheet.GetRow(rowP * curP + 27).GetCell(5).SetCellValue("QTY");
                        workSheet.GetRow(rowP * curP + 27).GetCell(6).SetCellValue("UNIT");
                        workSheet.GetRow(rowP * curP + 27).GetCell(7).SetCellValue("UNIT PRICE");
                        workSheet.GetRow(rowP * curP + 27).GetCell(8).SetCellValue("CURRENCY");
                        workSheet.GetRow(rowP * curP + 27).GetCell(9).SetCellValue("AMOUNT");
                        workSheet.GetRow(rowP * curP + 27).GetCell(10).SetCellValue("CURRENCY");
                        if (DT.Rows.Count > 0)
                        {
                            workSheet.GetRow(rowP * curP + 22).GetCell(9).SetCellValue(DT.Rows[0]["SID_nbr"].ToString());
                            workSheet.GetRow(rowP * curP + 24).GetCell(9).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                            workSheet.GetRow(rowP * curP + 26).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
                            workSheet.GetRow(rowP * curP + 26).GetCell(9).SetCellValue(DT.Rows[0]["SID_lcno"].ToString());
                        }
                    }

                    curR = 51 + cnt;
                    ICellStyle styleP2 = workbook.CreateCellStyle();

                    styleP2.WrapText = true;
                    styleP2.Alignment = HorizontalAlignment.Left;
                    NPOI.SS.UserModel.IFont fontP2 = workbook.CreateFont();
                    fontP2.FontHeightInPoints = 10;
                    styleP2.SetFont(fontP2);
                    IRow iRowP2 = workSheet.GetRow(curR);

                    iRowP2.GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRowP2.GetCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRowP2.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRowP2.GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRowP2.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    iRowP2.GetCell(6).SetCellValue(row["sid_qty_unit"].ToString());
                    if (!string.IsNullOrEmpty(row["SID_price1"].ToString().Trim()))
                    {
                        iRowP2.GetCell(7).SetCellValue(double.Parse(row["SID_price1"].ToString().Trim()));
                    }
                    iRowP2.GetCell(8).SetCellValue(row["SID_currency"].ToString());
                    if (!string.IsNullOrEmpty(row["amount1"].ToString().Trim()))
                    {
                        iRowP2.GetCell(9).SetCellValue(double.Parse(row["amount1"].ToString().Trim()));
                    }
                    iRowP2.GetCell(10).SetCellValue(row["SID_currency1"].ToString());
                    cnt++;
                }
                else
                {
                    ICellStyle style = workbook.CreateCellStyle();

                    style.WrapText = true;
                    style.Alignment = HorizontalAlignment.Left;
                    NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    style.SetFont(font);
                    IRow iRow = workSheet.GetRow(curR);

                    iRow.GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRow.GetCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRow.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRow.GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRow.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    iRow.GetCell(6).SetCellValue(row["sid_qty_unit"].ToString());
                    if (!string.IsNullOrEmpty(row["SID_price1"].ToString().Trim()))
                    {
                        iRow.GetCell(7).SetCellValue(double.Parse(row["SID_price1"].ToString().Trim()));
                    }
                    iRow.GetCell(8).SetCellValue(row["SID_currency"].ToString());
                    if (!string.IsNullOrEmpty(row["amount1"].ToString().Trim()))
                    {
                        iRow.GetCell(9).SetCellValue(double.Parse(row["amount1"].ToString().Trim()));
                    }
                    iRow.GetCell(10).SetCellValue(row["SID_currency1"].ToString());

                    cnt++;
                    curR++;
                }
            }

            if (PackingDet.Rows.Count < cntP+2)
            {
                for (int i = 60; i < 130; i++)
                {
                    IRow iRowP2 = workSheet.CreateRow(i);
                    workSheet.RemoveRow(iRowP2);
                }
            }
            else
            {
                #region //输出尾栏

                //输出尾栏

                ICellStyle styleFoot2 = workbook.CreateCellStyle();
                rowP = rowP + 13;
                styleFoot.Alignment = HorizontalAlignment.Right;
                NPOI.SS.UserModel.IFont fontFoot2 = workbook.CreateFont();
                fontFoot.FontHeightInPoints = 10;
                styleFoot.SetFont(fontFoot);
                workSheet.CreateRow(rowP * curP + 49).CreateCell(3).SetCellValue("Country of Origin:  China");
                workSheet.CreateRow(rowP * curP + 51).CreateCell(3).SetCellValue("SHANG HAI QIANG LING");
                workSheet.CreateRow(rowP * curP + 52).CreateCell(3).SetCellValue("ELECTRONIC  CO., LTD");
                for (int i = 53; i <= 58; i++)
                {
                    IRow iRowFoot = workSheet.CreateRow(rowP * curP + i);
                    if (i == 53)
                    {
                        iRowFoot.CreateCell(4).SetCellValue("----------------------");
                        iRowFoot.GetCell(4).CellStyle = styleFoot;
                    }
                    if (i == 54)
                    {
                        iRowFoot.CreateCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                        iRowFoot.GetCell(4).CellStyle = styleFoot;
                        iRowFoot.CreateCell(8).SetCellValue("NO.139 WANG DONG ROAD (S.)");
                    }
                    if (i == 55)
                    {
                        iRowFoot.CreateCell(2).SetCellValue("地址:");
                        iRowFoot.CreateCell(3).SetCellValue("上海松江泗泾望东南路139号      邮编:201601");
                        iRowFoot.CreateCell(8).SetCellValue(" SJ JING SONG JIANG");
                    }
                    if (i == 56)
                    {
                        iRowFoot.CreateCell(2).SetCellValue("电话:");
                        iRowFoot.CreateCell(3).SetCellValue("021-57619108");
                        iRowFoot.CreateCell(4).SetCellValue("传真:021-57619961");
                        iRowFoot.CreateCell(8).SetCellValue("SHANG HAI 201601 ,CHINA");
                    }
                }
                workSheet.CreateRow(rowP * curP + 57).CreateCell(8).SetCellValue("TEL: 021-57619108");
                workSheet.CreateRow(rowP * curP + 58).CreateCell(8).SetCellValue("FAX:021-57619961");

                #endregion
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
        /// NPOI新版本输出ATL发票:Excel
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="stddate"></param>
        /// <param name="enddate"></param>
        public void ATLInvoiceToExcelNewPageByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate, string PicLogo, string PicSeal)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(1);

            SID sid = new SID();
            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 29;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 17;

            if (cntT < 0)
                return;

            #region //输出头部信息

            //输出头部信息

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

            //输出头部信息

            workSheet.GetRow(rowP * curP + 2).GetCell(5).SetCellValue("上  海  强  凌  电  子  有  限  公  司");
            workSheet.GetRow(rowP * curP + 4).GetCell(1).SetCellValue("SHANGHAI QIANG LING");
            workSheet.GetRow(rowP * curP + 4).GetCell(5).SetCellValue("出  口  专  用");

            workSheet.GetRow(rowP * curP + 5).GetCell(1).SetCellValue("ELECTRONIC CO., LTD");
            workSheet.GetRow(rowP * curP + 5).GetCell(5).SetCellValue("FOR  EXPORT  ONLY");

            workSheet.GetRow(rowP * curP + 7).GetCell(1).SetCellValue("INVOICE");
            workSheet.GetRow(rowP * curP + 7).GetCell(5).SetCellValue("发  票");

            workSheet.GetRow(rowP * curP + 9).GetCell(1).SetCellValue("HU SONG GUO S HUI WAI (97) ZI NO. 80");
            workSheet.GetRow(rowP * curP + 9).GetCell(5).SetCellValue("沪 松 国 税 外 (97) 字 第 80 号");

            workSheet.GetRow(rowP * curP + 11).GetCell(1).SetCellValue("TO:");
            workSheet.GetRow(rowP * curP + 11).GetCell(3).SetCellValue("AURORA TECHNOLOGIES LIMITED");
            workSheet.GetRow(rowP * curP + 11).CreateCell(8).SetCellValue("NO:");

            workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue("ADDRESS:UNITS 1601-3 TAI RUNG BUILDING");

            workSheet.GetRow(rowP * curP + 13).GetCell(3).SetCellValue("8 FLEMING ROAD");
            workSheet.GetRow(rowP * curP + 13).CreateCell(8).SetCellValue("DATE:");

            workSheet.GetRow(rowP * curP + 14).GetCell(3).SetCellValue("HONG KONG");

            workSheet.GetRow(rowP * curP + 15).GetCell(1).SetCellValue("SHIP TO:");
            workSheet.GetRow(rowP * curP + 15).CreateCell(8).SetCellValue("L/C NO.:");

            workSheet.GetRow(rowP * curP + 16).GetCell(1).SetCellValue("NO");
            workSheet.GetRow(rowP * curP + 16).GetCell(2).SetCellValue("PO#");
            workSheet.GetRow(rowP * curP + 16).GetCell(3).SetCellValue("PART");
            workSheet.GetRow(rowP * curP + 16).GetCell(4).SetCellValue("DESCRIPTION");
            workSheet.GetRow(rowP * curP + 16).CreateCell(5).SetCellValue("QTY");
            workSheet.GetRow(rowP * curP + 16).CreateCell(6).SetCellValue("UNIT");
            workSheet.GetRow(rowP * curP + 16).CreateCell(7).SetCellValue("UNIT PRICE");
            workSheet.GetRow(rowP * curP + 16).CreateCell(8).SetCellValue("CURRENCY");
            workSheet.GetRow(rowP * curP + 16).CreateCell(9).SetCellValue("AMOUNT");
            workSheet.GetRow(rowP * curP + 16).CreateCell(10).SetCellValue("CURRENCY");
            if (DT.Rows.Count > 0)
            {
                workSheet.GetRow(rowP * curP + 11).CreateCell(9).SetCellValue(DT.Rows[0]["SID_nbr"].ToString());
                workSheet.GetRow(rowP * curP + 13).GetCell(9).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                workSheet.GetRow(rowP * curP + 15).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
                workSheet.GetRow(rowP * curP + 15).GetCell(9).SetCellValue(DT.Rows[0]["SID_lcno"].ToString());
            }


            #endregion


            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);
            workSheet.CreateRow(rowP * curP + 49).CreateCell(3).SetCellValue("Country of Origin:  China");
            workSheet.CreateRow(rowP * curP + 51).CreateCell(3).SetCellValue("SHANG HAI QIANG LING");
            workSheet.CreateRow(rowP * curP + 52).CreateCell(3).SetCellValue("ELECTRONIC  CO., LTD");
            for (int i = 53; i <= 58; i++)
            {
                IRow iRowFoot = workSheet.CreateRow(rowP * curP + i);
                if (i == 53)
                {
                    iRowFoot.CreateCell(4).SetCellValue("----------------------");
                    iRowFoot.GetCell(4).CellStyle = styleFoot;
                }
                if (i == 54)
                {
                    iRowFoot.CreateCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                    iRowFoot.GetCell(4).CellStyle = styleFoot;
                    iRowFoot.CreateCell(8).SetCellValue("NO.139 WANG DONG ROAD (S.)");
                }
                if (i == 55)
                {
                    iRowFoot.CreateCell(2).SetCellValue("地址:");
                    iRowFoot.CreateCell(3).SetCellValue("上海松江泗泾望东南路139号      邮编:201601");
                    iRowFoot.CreateCell(8).SetCellValue(" SJ JING SONG JIANG");
                }
                if (i == 56)
                {
                    iRowFoot.CreateCell(2).SetCellValue("电话:");
                    iRowFoot.CreateCell(3).SetCellValue("021-57619108");
                    iRowFoot.CreateCell(4).SetCellValue("传真:021-57619961");
                    iRowFoot.CreateCell(8).SetCellValue("SHANG HAI 201601 ,CHINA");
                }
            }
            workSheet.CreateRow(rowP * curP + 57).CreateCell(8).SetCellValue("TEL: 021-57619108");
            workSheet.CreateRow(rowP * curP + 58).CreateCell(8).SetCellValue("FAX:021-57619961");

            #endregion

            #region //输出明细信息

            //输出明细信息
            //DataSet _dataset = SelectDailyIncoming(uID, stddate, enddate);
            System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);


            int nCols = PackingDet.Columns.Count;
            int nRows = 3;
            curP = 1;
            foreach (DataRow row in PackingDet.Rows)
            {
                if (cnt > cntP)
                {
                    

                    //输出头部信息
                    //if (cnt == cntP + 1)
                    if (cnt == (cntP + 1) * curP)
                    {
                        workSheet.GetRow(rowP * curP + 13).GetCell(5).SetCellValue("上  海  强  凌  电  子  有  限  公  司");
                        workSheet.GetRow(rowP * curP + 15).GetCell(1).SetCellValue("SHANGHAI QIANG LING");
                        workSheet.GetRow(rowP * curP + 15).GetCell(5).SetCellValue("出  口  专  用");

                        workSheet.GetRow(rowP * curP + 16).GetCell(1).SetCellValue("ELECTRONIC CO., LTD");
                        workSheet.GetRow(rowP * curP + 16).GetCell(5).SetCellValue("FOR  EXPORT  ONLY");

                        workSheet.GetRow(rowP * curP + 18).GetCell(1).SetCellValue("INVOICE");
                        workSheet.GetRow(rowP * curP + 18).GetCell(5).SetCellValue("发  票");

                        workSheet.GetRow(rowP * curP + 20).GetCell(1).SetCellValue("HU SONG GUO S HUI WAI (97) ZI NO. 80");
                        workSheet.GetRow(rowP * curP + 20).GetCell(5).SetCellValue("沪 松 国 税 外 (97) 字 第 80 号");

                        workSheet.GetRow(rowP * curP + 22).CreateCell(1).SetCellValue("TO:");
                        workSheet.GetRow(rowP * curP + 22).GetCell(3).SetCellValue("AURORA TECHNOLOGIES LIMITED");
                        workSheet.GetRow(rowP * curP + 22).CreateCell(8).SetCellValue("NO:");

                        workSheet.GetRow(rowP * curP + 23).GetCell(3).SetCellValue("ADDRESS:UNITS 1601-3 TAI RUNG BUILDING");

                        workSheet.GetRow(rowP * curP + 24).GetCell(3).SetCellValue("8 FLEMING ROAD");
                        workSheet.GetRow(rowP * curP + 24).CreateCell(8).SetCellValue("DATE:");

                        workSheet.GetRow(rowP * curP + 25).CreateCell(3).SetCellValue("HONG KONG");

                        workSheet.GetRow(rowP * curP + 26).CreateCell(1).SetCellValue("SHIP TO:");
                        workSheet.GetRow(rowP * curP + 26).CreateCell(8).SetCellValue("L/C NO.:");

                        workSheet.GetRow(rowP * curP + 27).GetCell(1).SetCellValue("NO");
                        workSheet.GetRow(rowP * curP + 27).GetCell(2).SetCellValue("PO#");
                        workSheet.GetRow(rowP * curP + 27).GetCell(3).SetCellValue("PART");
                        workSheet.GetRow(rowP * curP + 27).GetCell(4).SetCellValue("DESCRIPTION");
                        workSheet.GetRow(rowP * curP + 27).GetCell(5).SetCellValue("QTY");
                        workSheet.GetRow(rowP * curP + 27).GetCell(6).SetCellValue("UNIT");
                        workSheet.GetRow(rowP * curP + 27).GetCell(7).SetCellValue("UNIT PRICE");
                        workSheet.GetRow(rowP * curP + 27).GetCell(8).SetCellValue("CURRENCY");
                        workSheet.GetRow(rowP * curP + 27).GetCell(9).SetCellValue("AMOUNT");
                        workSheet.GetRow(rowP * curP + 27).GetCell(10).SetCellValue("CURRENCY");
                        if (DT.Rows.Count > 0)
                        {
                            workSheet.GetRow(rowP * curP + 22).GetCell(9).SetCellValue(DT.Rows[0]["SID_nbr"].ToString());
                            workSheet.GetRow(rowP * curP + 24).GetCell(9).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                            workSheet.GetRow(rowP * curP + 26).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
                            workSheet.GetRow(rowP * curP + 26).GetCell(9).SetCellValue(DT.Rows[0]["SID_lcno"].ToString());
                        }
                        curP = curP + 1;
                    }

                    //curR = 51 + cnt;
                    curR = 44 + cnt;
                    if (curP == 2)
                    {
                        //curR = 44 + 76 - 53 + cnt;
                    }
                    if (curP == 3)
                    {
                        curR = 44 + 76 - 53 + cnt;
                    }
                    if (curP == 4)
                    {
                        curR = 44 + 76 - 30 + cnt;
                    }

                    ICellStyle styleP2 = workbook.CreateCellStyle();

                    styleP2.WrapText = true;
                    styleP2.Alignment = HorizontalAlignment.Left;
                    NPOI.SS.UserModel.IFont fontP2 = workbook.CreateFont();
                    fontP2.FontHeightInPoints = 10;
                    styleP2.SetFont(fontP2);
                    IRow iRowP2 = workSheet.GetRow(curR);

                    iRowP2.GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRowP2.GetCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRowP2.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRowP2.GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRowP2.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    iRowP2.GetCell(6).SetCellValue(row["sid_qty_unit"].ToString());
                    if (!string.IsNullOrEmpty(row["SID_price1"].ToString().Trim()))
                    {
                        iRowP2.GetCell(7).SetCellValue(double.Parse(row["SID_price1"].ToString().Trim()));
                    }
                    iRowP2.GetCell(8).SetCellValue(row["SID_currency"].ToString());
                    if (!string.IsNullOrEmpty(row["amount1"].ToString().Trim()))
                    {
                        iRowP2.GetCell(9).SetCellValue(double.Parse(row["amount1"].ToString().Trim()));
                    }
                    iRowP2.GetCell(10).SetCellValue(row["SID_currency1"].ToString());
                    cnt++;
                }
                else
                {
                    ICellStyle style = workbook.CreateCellStyle();

                    style.WrapText = true;
                    style.Alignment = HorizontalAlignment.Left;
                    NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    style.SetFont(font);
                    IRow iRow = workSheet.GetRow(curR);

                    iRow.CreateCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRow.CreateCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRow.CreateCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRow.CreateCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRow.CreateCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    iRow.CreateCell(6).SetCellValue(row["sid_qty_unit"].ToString());
                    if (!string.IsNullOrEmpty(row["SID_price1"].ToString().Trim()))
                    {
                        iRow.CreateCell(7).SetCellValue(double.Parse(row["SID_price1"].ToString().Trim()));
                    }
                    iRow.CreateCell(8).SetCellValue(row["SID_currency"].ToString());
                    if (!string.IsNullOrEmpty(row["amount1"].ToString().Trim()))
                    {
                        iRow.CreateCell(9).SetCellValue(double.Parse(row["amount1"].ToString().Trim()));
                    }
                    iRow.CreateCell(10).SetCellValue(row["SID_currency1"].ToString());

                    cnt++;
                    curR++;
                }
            }

            if (PackingDet.Rows.Count < cntP + 2)
            {
                for (int i = 60; i < 130; i++)
                {
                    IRow iRowP2 = workSheet.CreateRow(i);
                    workSheet.RemoveRow(iRowP2);
                }
            }
            else
            {
                #region //输出尾栏

                //输出尾栏

                ICellStyle styleFoot2 = workbook.CreateCellStyle();
                rowP = rowP + 13;
                styleFoot.Alignment = HorizontalAlignment.Right;
                NPOI.SS.UserModel.IFont fontFoot2 = workbook.CreateFont();
                fontFoot.FontHeightInPoints = 10;
                styleFoot.SetFont(fontFoot);
                //workSheet.CreateRow(rowP * curP + 49).CreateCell(3).SetCellValue("Country of Origin:  China");
                //workSheet.CreateRow(rowP * curP + 51).CreateCell(3).SetCellValue("SHANG HAI QIANG LING");
                //workSheet.CreateRow(rowP * curP + 52).CreateCell(3).SetCellValue("ELECTRONIC  CO., LTD");
                //for (int i = 53; i <= 58; i++)
                //{
                //    IRow iRowFoot = workSheet.CreateRow(rowP * curP + i);
                //    if (i == 53)
                //    {
                //        iRowFoot.CreateCell(4).SetCellValue("----------------------");
                //        iRowFoot.GetCell(4).CellStyle = styleFoot;
                //    }
                //    if (i == 54)
                //    {
                //        iRowFoot.CreateCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                //        iRowFoot.GetCell(4).CellStyle = styleFoot;
                //        iRowFoot.CreateCell(8).SetCellValue("NO.139 WANG DONG ROAD (S.)");
                //    }
                //    if (i == 55)
                //    {
                //        iRowFoot.CreateCell(2).SetCellValue("地址:");
                //        iRowFoot.CreateCell(3).SetCellValue("上海松江泗泾望东南路139号      邮编:201601");
                //        iRowFoot.CreateCell(8).SetCellValue(" SJ JING SONG JIANG");
                //    }
                //    if (i == 56)
                //    {
                //        iRowFoot.CreateCell(2).SetCellValue("电话:");
                //        iRowFoot.CreateCell(3).SetCellValue("021-57619108");
                //        iRowFoot.CreateCell(4).SetCellValue("传真:021-57619961");
                //        iRowFoot.CreateCell(8).SetCellValue("SHANG HAI 201601 ,CHINA");
                //    }
                //}
                //workSheet.CreateRow(rowP * curP + 57).CreateCell(8).SetCellValue("TEL: 021-57619108");
                //workSheet.CreateRow(rowP * curP + 58).CreateCell(8).SetCellValue("FAX:021-57619961");

                #endregion
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
        /// 老版本输出TCP发票
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void TCPInvoiceToExcel(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate)
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
            int cntP = 2;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 18;

            if (cntT < 0)
                return;

            //输出头部信息

            #region

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

            string boxno = "";
            string bl = "";
            if (DT.Rows.Count > 0)
            {
                boxno = DT.Rows[0]["sid_boxno"].ToString();
                bl = DT.Rows[0]["SID_bl"].ToString();
            }
            //if (DT.Rows.Count <= 0)
            //{
            //    //this.Alert("此出运单号信息不全！");
            //    return;
            //}

            //输出头部信息
            workSheet.Cells[rowP * curP + 3, 6] = "上  海  强  凌  电  子  有  限  公  司";

            workSheet.Cells[rowP * curP + 5, 2] = "SHANGHAI QAING LING";
            workSheet.Cells[rowP * curP + 5, 6] = "出  口  专  用";

            workSheet.Cells[rowP * curP + 6, 2] = "ELECTRONIC CO., LTD";
            workSheet.Cells[rowP * curP + 6, 6] = "FOR  EXPORT  ONLY";

            workSheet.Cells[rowP * curP + 8, 2] = "INVOICE";
            workSheet.Cells[rowP * curP + 8, 6] = "发  票";

            workSheet.Cells[rowP * curP + 10, 2] = "HU SONG GUO S HUI WAI (97) ZI NO. 80";
            workSheet.Cells[rowP * curP + 10, 6] = "沪 松 国 税 外 (97) 字 第 80 号";
            workSheet.Cells[rowP * curP + 12, 2] = "TO:";
            workSheet.Cells[rowP * curP + 12, 4] = "TECHNICAL CONSUMER PRODUCTS, INC";
            workSheet.Cells[rowP * curP + 12, 9] = "NO:";

            workSheet.Cells[rowP * curP + 13, 4] = "325 CAMPUS DRIVE AURORA, OHIO 44202";

            workSheet.Cells[rowP * curP + 14, 4] = "U.S.A.";
            workSheet.Cells[rowP * curP + 14, 9] = "DATE:";

            workSheet.Cells[rowP * curP + 16, 2] = "SHIP TO:";
            workSheet.Cells[rowP * curP + 16, 9] = "L/C NO.:";

            workSheet.Cells[rowP * curP + 17, 2] = "NO";
            workSheet.Cells[rowP * curP + 17, 3] = "PO#";
            workSheet.Cells[rowP * curP + 17, 4] = "PART";
            workSheet.Cells[rowP * curP + 17, 5] = "DESCRIPTION";
            workSheet.Cells[rowP * curP + 17, 6] = "QTY";
            workSheet.Cells[rowP * curP + 17, 7] = "UNIT";
            workSheet.Cells[rowP * curP + 17, 8] = "UNIT PRICE";
            workSheet.Cells[rowP * curP + 17, 9] = "CURRENCY";
            workSheet.Cells[rowP * curP + 17, 10] = "AMOUNT";
            workSheet.Cells[rowP * curP + 17, 11] = "CURRENCY";

            if (DT.Rows.Count > 0)
            {
                workSheet.Cells[rowP * curP + 12, 10] = DT.Rows[0]["SID_nbr"].ToString();
                workSheet.Cells[rowP * curP + 14, 10] = DT.Rows[0]["SID_shipdate"].ToString();
                workSheet.Cells[rowP * curP + 16, 4] = DT.Rows[0]["SID_shipto"].ToString();
                workSheet.Cells[rowP * curP + 16, 10] = DT.Rows[0]["SID_lcno"].ToString();

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
            }

            if (!string.IsNullOrEmpty(boxno))
            {
                workSheet.Cells[rowP * curP + 33, 4] = "CONTAINER NO" + boxno;
            }
           workSheet.Cells[rowP * curP + 34, 4] = "Country of Origin:  China";

            workSheet.Cells[rowP * curP + 43, 4] = "SHANG HAI QIANG LING";
            workSheet.Cells[rowP * curP + 44, 4] = "ELECTRONIC  CO., LTD";
            workSheet.Cells[rowP * curP + 45, 5] = "'----------------------";
            workSheet.Cells[rowP * curP + 46, 5] = "AUTHORIZED.SIGNATURE";
            workSheet.Cells[rowP * curP + 46, 9] = "NO.139 WANG DONG ROAD (S.)";
            workSheet.Cells[rowP * curP + 47, 3] = "地址:";
            workSheet.Cells[rowP * curP + 47, 4] = "上海松江泗泾望东南路139号      邮编:201601";
            workSheet.Cells[rowP * curP + 47, 9] = " SJ JING SONG JIANG";
            workSheet.Cells[rowP * curP + 48, 3] = "电话:";
            workSheet.Cells[rowP * curP + 48, 4] = "021-57619108";
            workSheet.Cells[rowP * curP + 48, 5] = "传真:021-57619961";
            workSheet.Cells[rowP * curP + 48, 9] = "SHANG HAI 201601 ,CHINA";
            workSheet.Cells[rowP * curP + 49, 9] = "TEL: 021-57619108";
            workSheet.Cells[rowP * curP + 50, 9] = "FAX:021-57619961";

            #endregion

            //输出明细信息


            System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);
            if (PackingDet.Rows.Count > 0)
            {

                for (int i = 0; i < PackingDet.Rows.Count; i++)
                {
                    int str = i / 2;
                    if (cnt >= 15)
                    {
                        cnt = 0;
                        curP += 1;
                        curR = 19;

                        workSheet.Cells[rowP * curP + 4, 6] = "上  海  强  凌  电  子  有  限  公  司";

                        workSheet.Cells[rowP * curP + 6, 2] = "SHANGHAI QIANG LING";
                        workSheet.Cells[rowP * curP + 6, 6] = "出  口  专  用";

                        workSheet.Cells[rowP * curP + 7, 2] = "ELECTRONIC CO., LTD";
                        workSheet.Cells[rowP * curP + 7, 6] = "FOR  EXPORT  ONLY";

                        workSheet.Cells[rowP * curP + 9, 2] = "INVICE";
                        workSheet.Cells[rowP * curP + 9, 6] = "发  票";

                        workSheet.Cells[rowP * curP + 11, 2] = "HU SONG GUO S HUI WAI (97) ZI NO. 80";
                        workSheet.Cells[rowP * curP + 11, 6] = "沪 松 国 税 外 (97) 字 第 80 号";
                        workSheet.Cells[rowP * curP + 13, 2] = "TO:";
                        workSheet.Cells[rowP * curP + 13, 4] = "TECHNICAL CONSUMER PRODUCTS, INC";
                        workSheet.Cells[rowP * curP + 13, 9] = "NO:";

                        workSheet.Cells[rowP * curP + 14, 4] = "325 CAMPUS DRIVE AURORA, OHIO 44202";
                        workSheet.Cells[rowP * curP + 14, 9] = "DATE:";

                        workSheet.Cells[rowP * curP + 15, 4] = "U.S.A.";

                        workSheet.Cells[rowP * curP + 17, 2] = "SHIP TO:";
                        workSheet.Cells[rowP * curP + 17, 9] = "L/C NO.:";
                        if (DT.Rows.Count > 0)
                        {
                            workSheet.Cells[rowP * curP + 13, 10] = DT.Rows[0]["SID_nbr"].ToString();
                            workSheet.Cells[rowP * curP + 15, 10] = DT.Rows[0]["SID_shipdate"].ToString();
                            workSheet.Cells[rowP * curP + 17, 4] = DT.Rows[0]["SID_shipto"].ToString();
                            workSheet.Cells[rowP * curP + 17, 10] = DT.Rows[0]["SID_lcno"].ToString();

                            ////保护单元格
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                        }

                        workSheet.Cells[rowP * curP + 18, 2] = "NO";
                        workSheet.Cells[rowP * curP + 18, 3] = "PO#";
                        workSheet.Cells[rowP * curP + 18, 4] = "PART";
                        workSheet.Cells[rowP * curP + 18, 5] = "DESCRIPTION";
                        workSheet.Cells[rowP * curP + 18, 6] = "QTY";
                        workSheet.Cells[rowP * curP + 18, 7] = "UNIT";
                        workSheet.Cells[rowP * curP + 18, 8] = "UNIT PRICE";
                        workSheet.Cells[rowP * curP + 18, 9] = "CURRENCY";
                        workSheet.Cells[rowP * curP + 18, 10] = "AMOUNT";
                        workSheet.Cells[rowP * curP + 18, 11] = "CURRENCY";

                        workSheet.Cells[rowP * curP + 35, 4] = "Country of Origin:  China";

                        workSheet.Cells[rowP * curP + 44, 4] = "SHANG HAI QIANG LING";
                        workSheet.Cells[rowP * curP + 45, 4] = "ELECTRONIC  CO., LTD";
                        workSheet.Cells[rowP * curP + 46, 5] = "'----------------------";
                        workSheet.Cells[rowP * curP + 47, 5] = "AUTHORIZED.SIGNATURE";
                        workSheet.Cells[rowP * curP + 47, 9] = "NO.139 WANG DONG ROAD (S.)";
                        workSheet.Cells[rowP * curP + 48, 3] = "地址:";
                        workSheet.Cells[rowP * curP + 48, 4] = "上海松江泗泾望东南路139号      邮编:201601";
                        workSheet.Cells[rowP * curP + 48, 9] = " SJ JING SONG JIANG";
                        workSheet.Cells[rowP * curP + 49, 3] = "电话:";
                        workSheet.Cells[rowP * curP + 49, 4] = "021-57619108";
                        workSheet.Cells[rowP * curP + 49, 5] = "传真:021-57619961";
                        workSheet.Cells[rowP * curP + 49, 9] = "SHANG HAI 201601 ,CHINA";
                        workSheet.Cells[rowP * curP + 50, 9] = "TEL: 021-57619108";
                        workSheet.Cells[rowP * curP + 51, 9] = "FAX:021-57619961";
                    }

                    workSheet.Cells[rowP * curP + curR, 2] = PackingDet.Rows[i]["sid_so_line"].ToString();
                    workSheet.Cells[rowP * curP + curR, 3] = PackingDet.Rows[i]["SID_PO"].ToString();
                    workSheet.Cells[rowP * curP + curR, 4] = PackingDet.Rows[i]["sid_cust_part"].ToString();
                    workSheet.Cells[rowP * curP + curR, 5] = PackingDet.Rows[i]["sid_cust_partdesc"].ToString();
                    workSheet.Cells[rowP * curP + curR, 6] = PackingDet.Rows[i]["sid_qty_pcs"].ToString();
                    workSheet.Cells[rowP * curP + curR, 7] = PackingDet.Rows[i]["sid_qty_unit"].ToString();
                    workSheet.Cells[rowP * curP + curR, 8] = PackingDet.Rows[i]["SID_price2"].ToString();
                    workSheet.Cells[rowP * curP + curR, 9] = PackingDet.Rows[i]["SID_currency"].ToString();
                    workSheet.Cells[rowP * curP + curR, 10] = PackingDet.Rows[i]["amount2"].ToString();
                    workSheet.Cells[rowP * curP + curR, 11] = PackingDet.Rows[i]["SID_currency1"].ToString();

                    cnt += 1;
                    curR += 1;
                }

            }

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
        /// 新版本输出TCP发票  使用COM+
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void NEWTCPInvoiceToExcel(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate, string PicSeal)
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
            int cntP = 2;
            //总条数
            //int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            //int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 14;

            //if (cntT < 0)
            //    return;

            //输出头部信息

            #region

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

            string boxno = "";
            string bl = "";
            if (DT.Rows.Count > 0)
            {
                boxno = DT.Rows[0]["sid_boxno"].ToString();
                bl = DT.Rows[0]["SID_bl"].ToString();
            }
            //if (DT.Rows.Count <= 0)
            //{
            //    //this.Alert("此出运单号信息不全！");
            //    return;
            //}

            //输出头部信息
            //workSheet.Cells[rowP * curP + 2, 2] = "TCP";
            workSheet.Cells[rowP * curP + 2, 2] = "AURORA TECHNOLOGIES LIMITED";
            // by wang li wei 抬头有TCP更改为ALT  2014-11-12
            //workSheet.Cells[rowP * curP + 3, 2] = "";
            //workSheet.Cells[rowP * curP + 4, 5] = "";
            workSheet.Cells[rowP * curP + 6, 2] = "COMMERCIAL INVOICE";
            workSheet.Cells[rowP * curP + 8, 2] = "TO:";
            workSheet.Cells[rowP * curP + 8, 4] = "TECHNICAL CONSUMER PRODUCTS, INC";
            workSheet.Cells[rowP * curP + 8, 6] = "INV NO:";

            workSheet.Cells[rowP * curP + 9, 4] = "325 CAMPUS DRIVE AURORA, OHIO 44202";

            workSheet.Cells[rowP * curP + 10, 4] = "U.S.A.";
            workSheet.Cells[rowP * curP + 10, 6] = "DATE:";

            workSheet.Cells[rowP * curP + 12, 2] = "SHIP TO:";
            workSheet.Cells[rowP * curP + 12, 6] = "Country of Origin:  China";

            workSheet.Cells[rowP * curP + 13, 2] = "NO";
            workSheet.Cells[rowP * curP + 13, 3] = "PO#";
            workSheet.Cells[rowP * curP + 13, 4] = "PART";
            workSheet.Cells[rowP * curP + 13, 5] = "DESCRIPTION";
            workSheet.Cells[rowP * curP + 13, 6] = "QTY UNITS";
            workSheet.Cells[rowP * curP + 13, 7] = "UNIT PRICE" + "  USD";
            workSheet.Cells[rowP * curP + 13, 8] = "TOTAL  " + "  " + "  USD";

            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 2], workSheet.Cells[rowP * curP + 13, 8]).Borders.LineStyle = 1;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 2], workSheet.Cells[rowP * curP + 13, 8]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 2], workSheet.Cells[rowP * curP + 13, 8]).Borders.get_Item(XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlMedium;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 2], workSheet.Cells[rowP * curP + 13, 8]).Borders.get_Item(XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlMedium;
            workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 2], workSheet.Cells[rowP * curP + 13, 8]).Borders.get_Item(XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlMedium;

            //string picpath = Server.MapPath(@"D://TCP-File//Julia//images//Seal.jpg");
            //workSheet.Shapes.AddPicture(PicSeal, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)280, (float)680, (float)55, (float)55);
            //workSheet.Shapes.AddPicture(@"D:\\TCP-File\\Julia\\images\\Seal.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)280, (float)680, (float)55, (float)55);
          
            //workSheet.Range.Delete(workSheet.Cells[rowP * curP + 56,2]);

            //workBook.ActiveSheet.r

            if (DT.Rows.Count > 0)
            {
                workSheet.Cells[rowP * curP + 8, 7] = DT.Rows[0]["SID_nbr"].ToString().Trim() + " "+"  Page" + (curP + 1) ;
                workSheet.Cells[rowP * curP + 10, 7] = DT.Rows[0]["SID_shipdate"].ToString();
                workSheet.Cells[rowP * curP + 12, 4] = DT.Rows[0]["SID_shipto"].ToString();

                ////保护单元格
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
            }

            if (!string.IsNullOrEmpty(boxno))
            {
                workSheet.Cells[rowP * curP + 46, 4] = "CONTAINER NO:" + "  " + boxno;
            }

            workSheet.Cells[rowP * curP + 52, 3] = "SHANG HAI QIANG LING";
            workSheet.Cells[rowP * curP + 53, 3] = "ELECTRONIC  CO., LTD";
            workSheet.Cells[rowP * curP + 54, 5] = "'----------------------";
            workSheet.Cells[rowP * curP + 55, 5] = "AUTHORIZED.SIGNATURE";

            #endregion

            //输出明细信息
            #region

            System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);
            if (PackingDet.Rows.Count > 0)
            {

                for (int i = 0; i < PackingDet.Rows.Count; i++)
                {
                    int str = i / 2;
                    if (cnt >= 30)
                    {
                        cnt = 0;
                        curP += 1;
                        curR = 22;
                        workSheet.Cells[rowP * curP + 10, 2] = "AURORA TECHNOLOGIES LIMITED";
                        //workSheet.Cells[rowP * curP + 3, 2] = "ADD: NO.139 WANG DONG ROAD (S.)  SJ JING SONG JIANG SHANG HAI 201601 ,CHINA";
                        //workSheet.Cells[rowP * curP + 4, 5] = "TEL: 021-57619108 FAX:021-57619961";
                        workSheet.Cells[rowP * curP + 14, 2] = "COMMERCIAL INVOICE";
                        workSheet.Cells[rowP * curP + 16, 2] = "TO:";
                        workSheet.Cells[rowP * curP + 16, 4] = "TECHNICAL CONSUMER PRODUCTS, INC";
                        workSheet.Cells[rowP * curP + 16, 6] = "INV NO:";

                        workSheet.Cells[rowP * curP + 17, 4] = "325 CAMPUS DRIVE AURORA, OHIO 44202";

                        workSheet.Cells[rowP * curP + 18, 4] = "U.S.A.";
                        workSheet.Cells[rowP * curP + 18, 6] = "DATE:";

                        workSheet.Cells[rowP * curP + 20, 2] = "SHIP TO:";
                        workSheet.Cells[rowP * curP + 20, 6] = "Country of Origin:  China";

                        workSheet.Cells[rowP * curP + 21, 2] = "NO";
                        workSheet.Cells[rowP * curP + 21, 3] = "PO#";
                        workSheet.Cells[rowP * curP + 21, 4] = "PART";
                        workSheet.Cells[rowP * curP + 21, 5] = "DESCRIPTION";
                        workSheet.Cells[rowP * curP + 21, 6] = "QTY UNITS";
                        workSheet.Cells[rowP * curP + 21, 7] = "UNIT PRICE" + "  USD";
                        workSheet.Cells[rowP * curP + 21, 8] = "TOTAL  " + "  " + "  USD";

                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 21, 2], workSheet.Cells[rowP * curP + 21, 8]).Borders.LineStyle = 1;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 21, 2], workSheet.Cells[rowP * curP + 21, 8]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 21, 2], workSheet.Cells[rowP * curP + 21, 8]).Borders.get_Item(XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlMedium;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 21, 2], workSheet.Cells[rowP * curP + 21, 8]).Borders.get_Item(XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlMedium;
                        workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 21, 2], workSheet.Cells[rowP * curP + 21, 8]).Borders.get_Item(XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlMedium;

                        //string picpath = Server.MapPath(@"D://TCP-File//Julia//images//Seal.jpg");
                        //workSheet.Shapes.AddPicture(PicSeal, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)280, (float)680, (float)55, (float)55);


                        if (DT.Rows.Count > 0)
                        {
                            workSheet.Cells[rowP * curP + 16, 7] = DT.Rows[0]["SID_nbr"].ToString().Trim() + "  " + " Page" + (curP + 1);
                            workSheet.Cells[rowP * curP + 18, 7] = DT.Rows[0]["SID_shipdate"].ToString();
                            workSheet.Cells[rowP * curP + 20, 4] = DT.Rows[0]["SID_shipto"].ToString();

                            ////保护单元格
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                        }

                        //workSheet.Cells[rowP * curP + 13, 2] = "NO";
                        //workSheet.Cells[rowP * curP + 13, 3] = "PO#";
                        //workSheet.Cells[rowP * curP + 13, 4] = "PART";
                        //workSheet.Cells[rowP * curP + 13, 5] = "DESCRIPTION";
                        //workSheet.Cells[rowP * curP + 13, 6] = "QTY " + "  " + "  SETS";
                        //workSheet.Cells[rowP * curP + 13, 7] = "UNIT PRICE" + "  USD";
                        //workSheet.Cells[rowP * curP + 13, 8] = "AMOUNT  " + "  " + "  USD";

                        if (!string.IsNullOrEmpty(boxno))
                        {
                            workSheet.Cells[rowP * curP + 46, 4] = "CONTAINER NO:" + "  " + boxno;
                        }

                        workSheet.Cells[rowP * curP + 60, 3] = "SHANG HAI QIANG LING";
                        workSheet.Cells[rowP * curP + 61, 3] = "ELECTRONIC  CO., LTD";
                        workSheet.Cells[rowP * curP + 62, 5] = "'----------------------";
                        workSheet.Cells[rowP * curP + 63, 5] = "AUTHORIZED.SIGNATURE";
                    }
                    else
                    {
                        Excel.Range range = app.get_Range(workSheet.Cells[rowP * curP + 60, 1], workSheet.Cells[rowP * curP + 200, 1]);
                        range.Select();
                        range.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        //range.get_Item(1);
                        Excel.Range range1 = app.get_Range(workSheet.Cells[rowP * curP + 1, 1], workSheet.Cells[rowP * curP + 1, 1]);
                        range1.Select();
                    }

                    workSheet.Cells[rowP * curP + curR, 2] = PackingDet.Rows[i]["sid_so_line"].ToString();
                    workSheet.Cells[rowP * curP + curR, 3] = PackingDet.Rows[i]["SID_PO"].ToString();
                    workSheet.Cells[rowP * curP + curR, 4] = PackingDet.Rows[i]["sid_cust_part"].ToString();
                    workSheet.Cells[rowP * curP + curR, 5] = PackingDet.Rows[i]["sid_cust_partdesc"].ToString();
                    workSheet.Cells[rowP * curP + curR, 6] = PackingDet.Rows[i]["sid_qty_pcs"].ToString();
                    workSheet.Cells[rowP * curP + curR, 7] = PackingDet.Rows[i]["SID_price2"].ToString();
                    workSheet.Cells[rowP * curP + curR, 8] = PackingDet.Rows[i]["amount2"].ToString();


                    cnt += 1;
                    curR += 1;
                }

            }
            #endregion
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
        /// NPOI新版本输出TCP发票  使用COM+
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void NEWTCPInvoiceToExcelByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate, string PicSeal)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(1);

            SID sid = new SID();
            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 29;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 13;

            if (cntT < 0)
                return;

            #region //输出头部信息

            //输出头部信息

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

            string boxno = "";
            string bl = "";
            if (DT.Rows.Count > 0)
            {
                boxno = DT.Rows[0]["sid_boxno"].ToString();
                bl = DT.Rows[0]["SID_bl"].ToString();
            }
            //输出头部信息

            workSheet.GetRow(rowP * curP + 1).GetCell(1).SetCellValue("AURORA TECHNOLOGIES LIMITED");
            // by wang li wei 抬头有TCP更改为ALT  2014-11-12
            //workSheet.Cells[rowP * curP + 3, 2] = "";
            //workSheet.Cells[rowP * curP + 4, 5] = "";
            workSheet.GetRow(rowP * curP + 5).GetCell(1).SetCellValue("COMMERCIAL INVOICE");
            workSheet.GetRow(rowP * curP + 7).GetCell(1).SetCellValue("TO:");
            workSheet.GetRow(rowP * curP + 7).GetCell(3).SetCellValue("TECHNICAL CONSUMER PRODUCTS, INC");
            workSheet.GetRow(rowP * curP + 7).GetCell(5).SetCellValue("INV NO:");

            workSheet.GetRow(rowP * curP + 8).GetCell(3).SetCellValue("325 CAMPUS DRIVE AURORA, OHIO 44202");

            workSheet.GetRow(rowP * curP + 9).GetCell(3).SetCellValue("U.S.A.");
            workSheet.GetRow(rowP * curP + 9).GetCell(5).SetCellValue("DATE:");

            workSheet.GetRow(rowP * curP + 11).GetCell(1).SetCellValue("SHIP TO:");
            workSheet.GetRow(rowP * curP + 11).GetCell(5).SetCellValue("Country of Origin:  China");

            workSheet.GetRow(rowP * curP + 12).GetCell(1).SetCellValue("NO");
            workSheet.GetRow(rowP * curP + 12).GetCell(2).SetCellValue("PO#");
            workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue("PART");
            workSheet.GetRow(rowP * curP + 12).GetCell(4).SetCellValue("DESCRIPTION");
            workSheet.GetRow(rowP * curP + 12).GetCell(5).SetCellValue("QTY UNITS");
            workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("UNIT PRICE" + "  USD");
            workSheet.GetRow(rowP * curP + 12).GetCell(7).SetCellValue("TOTAL  " + "  " + "  USD");
            if (DT.Rows.Count > 0)
            {
                workSheet.GetRow(rowP * curP + 7).GetCell(6).SetCellValue(DT.Rows[0]["SID_nbr"].ToString().Trim() + " " + "  Page" + (curP + 1));
                workSheet.GetRow(rowP * curP + 9).GetCell(6).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                workSheet.GetRow(rowP * curP + 11).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
            }

            #endregion


            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            if (!string.IsNullOrEmpty(boxno))
            {
                workSheet.GetRow(rowP * curP + 45).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
            }

            workSheet.GetRow(rowP * curP + 51).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
            workSheet.GetRow(rowP * curP + 52).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
            workSheet.GetRow(rowP * curP + 53).GetCell(4).SetCellValue("'----------------------");
            workSheet.GetRow(rowP * curP + 54).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");


            #endregion

            #region //输出明细信息

            //输出明细信息
            //DataSet _dataset = SelectDailyIncoming(uID, stddate, enddate);
            System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);


            int nCols = PackingDet.Columns.Count;
            int nRows = 3;

            foreach (DataRow row in PackingDet.Rows)
            {
                if (cnt > cntP)
                {
                    curP = 1;

                    //输出头部信息
                    if (cnt == cntP + 1)
                    {
                        workSheet.GetRow(rowP * curP +9).GetCell(1).SetCellValue("AURORA TECHNOLOGIES LIMITED");
                        // by wang li wei 抬头有TCP更改为ALT  2014-11-12
                        //workSheet.Cells[rowP * curP + 3, 2] = "";
                        //workSheet.Cells[rowP * curP + 4, 5] = "";
                        workSheet.GetRow(rowP * curP + 13).GetCell(1).SetCellValue("COMMERCIAL INVOICE");
                        workSheet.GetRow(rowP * curP + 15).GetCell(1).SetCellValue("TO:");
                        workSheet.GetRow(rowP * curP + 15).GetCell(3).SetCellValue("TECHNICAL CONSUMER PRODUCTS, INC");
                        workSheet.GetRow(rowP * curP + 15).GetCell(5).SetCellValue("INV NO:");

                        workSheet.GetRow(rowP * curP + 16).GetCell(3).SetCellValue("325 CAMPUS DRIVE AURORA, OHIO 44202");

                        workSheet.GetRow(rowP * curP + 17).GetCell(3).SetCellValue("U.S.A.");
                        workSheet.GetRow(rowP * curP + 17).GetCell(5).SetCellValue("DATE:");

                        workSheet.GetRow(rowP * curP + 19).GetCell(1).SetCellValue("SHIP TO:");
                        workSheet.GetRow(rowP * curP + 19).GetCell(5).SetCellValue("Country of Origin:  China");

                        workSheet.GetRow(rowP * curP + 20).GetCell(1).SetCellValue("NO");
                        workSheet.GetRow(rowP * curP + 20).GetCell(2).SetCellValue("PO#");
                        workSheet.GetRow(rowP * curP + 20).GetCell(3).SetCellValue("PART");
                        workSheet.GetRow(rowP * curP + 20).GetCell(4).SetCellValue("DESCRIPTION");
                        workSheet.GetRow(rowP * curP + 20).GetCell(5).SetCellValue("QTY UNITS");
                        workSheet.GetRow(rowP * curP + 20).GetCell(6).SetCellValue("UNIT PRICE" + "  USD");
                        workSheet.GetRow(rowP * curP + 20).GetCell(7).SetCellValue("TOTAL  " + "  " + "  USD");
                        if (DT.Rows.Count > 0)
                        {
                            workSheet.GetRow(rowP * curP + 15).GetCell(6).SetCellValue(DT.Rows[0]["SID_nbr"].ToString().Trim() + " " + "  Page" + (curP + 1));
                            workSheet.GetRow(rowP * curP + 17).GetCell(6).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                            workSheet.GetRow(rowP * curP + 19).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
                        }

                        //workSheet.GetRow(rowP * curP + 60).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                        //workSheet.GetRow(rowP * curP + 61).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                        //workSheet.GetRow(rowP * curP + 62).GetCell(4).SetCellValue("'----------------------");
                        //workSheet.GetRow(rowP * curP + 63).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                    }

                    curR = 44 + cnt;
                    ICellStyle styleP2 = workbook.CreateCellStyle();

                    styleP2.WrapText = true;
                    styleP2.Alignment = HorizontalAlignment.Left;
                    NPOI.SS.UserModel.IFont fontP2 = workbook.CreateFont();
                    fontP2.FontHeightInPoints = 10;
                    styleP2.SetFont(fontP2);
                    IRow iRowP2 = workSheet.GetRow(curR);

                    iRowP2.GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRowP2.GetCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRowP2.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRowP2.GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    iRowP2.GetCell(4).CellStyle = styleP2;
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRowP2.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["SID_price2"].ToString().Trim()))
                    {
                        iRowP2.GetCell(6).SetCellValue(double.Parse(row["SID_price2"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["amount2"].ToString().Trim()))
                    {
                        iRowP2.GetCell(7).SetCellValue(double.Parse(row["amount2"].ToString().Trim()));
                    }
                    cnt++;
                }
                else
                {
                    ICellStyle style = workbook.CreateCellStyle();
                    
                    //style.WrapText = true;
                    style.Alignment = HorizontalAlignment.Right;
                    NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 12;
                    style.SetFont(font);
                    style.DataFormat = HSSFDataFormat.GetBuiltinFormat("$#,##0.00");
                    IRow iRow = workSheet.GetRow(curR);

                    iRow.GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRow.CreateCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRow.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRow.GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRow.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["SID_price2"].ToString().Trim()))
                    {
                        iRow.GetCell(6).SetCellValue(double.Parse(row["SID_price2"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["amount2"].ToString().Trim()))
                    {
                        iRow.GetCell(7).SetCellValue(double.Parse(row["amount2"].ToString().Trim()));
                    }

                    cnt++;
                    curR++;
                }
            }

            if (PackingDet.Rows.Count < cntP + 2)
            {
                for (int i = 60; i < 130; i++)
                {
                    IRow iRowP2 = workSheet.CreateRow(i);
                    workSheet.RemoveRow(iRowP2);
                }
            }
            else
            {
                #region //输出尾栏

                //输出尾栏

                ICellStyle styleFoot2 = workbook.CreateCellStyle();
                rowP = rowP + 13;
                styleFoot.Alignment = HorizontalAlignment.Right;
                NPOI.SS.UserModel.IFont fontFoot2 = workbook.CreateFont();
                fontFoot.FontHeightInPoints = 10;
                styleFoot.SetFont(fontFoot);
                if (!string.IsNullOrEmpty(boxno))
                {
                    workSheet.GetRow(rowP * curP + 40).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
                }

                workSheet.GetRow(rowP * curP + 46).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                workSheet.GetRow(rowP * curP + 47).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                workSheet.GetRow(rowP * curP + 48).GetCell(4).SetCellValue("'----------------------");
                workSheet.GetRow(rowP * curP + 49).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");

                #endregion
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
        /// NPOI新版本输出TCP发票  使用COM+
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void NEWTCPInvoiceToExcelPageByNPOI(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate, string PicSeal)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(1);

            SID sid = new SID();
            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 29;
            //总条数
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 13;

            if (cntT < 0)
                return;

            #region //输出头部信息

            //输出头部信息

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

            string boxno = "";
            string bl = "";
            if (DT.Rows.Count > 0)
            {
                boxno = DT.Rows[0]["sid_boxno"].ToString();
                bl = DT.Rows[0]["SID_bl"].ToString();
            }
            //输出头部信息

           workSheet.GetRow(rowP * curP + 1).GetCell(1).SetCellValue("AURORA TECHNOLOGIES LIMITED");
            // by wang li wei 抬头有TCP更改为ALT  2014-11-12
            //workSheet.Cells[rowP * curP + 3, 2] = "";
            //workSheet.Cells[rowP * curP + 4, 5] = "";
            workSheet.GetRow(rowP * curP + 5).GetCell(1).SetCellValue("COMMERCIAL INVOICE");
            workSheet.GetRow(rowP * curP + 7).GetCell(1).SetCellValue("TO:");
            workSheet.GetRow(rowP * curP + 7).GetCell(3).SetCellValue("TECHNICAL CONSUMER PRODUCTS, INC");
            workSheet.GetRow(rowP * curP + 7).GetCell(5).SetCellValue("INV NO:");

            workSheet.GetRow(rowP * curP + 8).GetCell(3).SetCellValue("325 CAMPUS DRIVE AURORA, OHIO 44202");

            workSheet.GetRow(rowP * curP + 9).GetCell(3).SetCellValue("U.S.A.");
            workSheet.GetRow(rowP * curP + 9).GetCell(5).SetCellValue("DATE:");

            workSheet.GetRow(rowP * curP + 11).GetCell(1).SetCellValue("SHIP TO:");
            workSheet.GetRow(rowP * curP + 11).GetCell(5).SetCellValue("Country of Origin:  China");

            workSheet.GetRow(rowP * curP + 12).GetCell(1).SetCellValue("NO");
            workSheet.GetRow(rowP * curP + 12).GetCell(2).SetCellValue("PO#");
            workSheet.GetRow(rowP * curP + 12).GetCell(3).SetCellValue("PART");
            workSheet.GetRow(rowP * curP + 12).GetCell(4).SetCellValue("DESCRIPTION");
            workSheet.GetRow(rowP * curP + 12).GetCell(5).SetCellValue("QTY UNITS");
            workSheet.GetRow(rowP * curP + 12).GetCell(6).SetCellValue("UNIT PRICE" + "  USD");
            workSheet.GetRow(rowP * curP + 12).GetCell(7).SetCellValue("TOTAL  " + "  " + "  USD");
            if (DT.Rows.Count > 0)
            {
                workSheet.GetRow(rowP * curP + 7).GetCell(6).SetCellValue(DT.Rows[0]["SID_nbr"].ToString().Trim() + " " + "  Page" + (curP + 1));
                workSheet.GetRow(rowP * curP + 9).GetCell(6).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                workSheet.GetRow(rowP * curP + 11).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
            }

            #endregion


            #region //输出尾栏

            //输出尾栏

            ICellStyle styleFoot = workbook.CreateCellStyle();

            styleFoot.Alignment = HorizontalAlignment.Right;
            NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            fontFoot.FontHeightInPoints = 10;
            styleFoot.SetFont(fontFoot);

            if (!string.IsNullOrEmpty(boxno))
            {
                workSheet.GetRow(rowP * curP + 45).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
            }

            workSheet.GetRow(rowP * curP + 51).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
            workSheet.GetRow(rowP * curP + 52).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
            workSheet.GetRow(rowP * curP + 53).GetCell(4).SetCellValue("'----------------------");
            workSheet.GetRow(rowP * curP + 54).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");


            #endregion

            #region //输出明细信息

            //输出明细信息
            //DataSet _dataset = SelectDailyIncoming(uID, stddate, enddate);
            System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);


            int nCols = PackingDet.Columns.Count;
            int nRows = 3;
            curP = 1;
            foreach (DataRow row in PackingDet.Rows)
            {
                if (cnt > cntP)
                {
                    //输出头部信息
                    if (cnt == (cntP + 1) * curP)
                    {

                        workSheet.GetRow(rowP * curP + 9).GetCell(1).SetCellValue("AURORA TECHNOLOGIES LIMITED");
                        // by wang li wei 抬头有TCP更改为ALT  2014-11-12
                        //workSheet.Cells[rowP * curP + 3, 2] = "";
                        //workSheet.Cells[rowP * curP + 4, 5] = "";
                        workSheet.GetRow(rowP * curP + 13).GetCell(1).SetCellValue("COMMERCIAL INVOICE");
                        workSheet.GetRow(rowP * curP + 15).GetCell(1).SetCellValue("TO:");
                        workSheet.GetRow(rowP * curP + 15).GetCell(3).SetCellValue("TECHNICAL CONSUMER PRODUCTS, INC");
                        workSheet.GetRow(rowP * curP + 15).GetCell(5).SetCellValue("INV NO:");

                        workSheet.GetRow(rowP * curP + 16).GetCell(3).SetCellValue("325 CAMPUS DRIVE AURORA, OHIO 44202");

                        workSheet.GetRow(rowP * curP + 17).GetCell(3).SetCellValue("U.S.A.");
                        workSheet.GetRow(rowP * curP + 17).GetCell(5).SetCellValue("DATE:");

                        workSheet.GetRow(rowP * curP + 19).GetCell(1).SetCellValue("SHIP TO:");
                        workSheet.GetRow(rowP * curP + 19).GetCell(5).SetCellValue("Country of Origin:  China");

                        workSheet.GetRow(rowP * curP + 20).GetCell(1).SetCellValue("NO");
                        workSheet.GetRow(rowP * curP + 20).GetCell(2).SetCellValue("PO#");
                        workSheet.GetRow(rowP * curP + 20).GetCell(3).SetCellValue("PART");
                        workSheet.GetRow(rowP * curP + 20).GetCell(4).SetCellValue("DESCRIPTION");
                        workSheet.GetRow(rowP * curP + 20).GetCell(5).SetCellValue("QTY UNITS");
                        workSheet.GetRow(rowP * curP + 20).GetCell(6).SetCellValue("UNIT PRICE" + "  USD");
                        workSheet.GetRow(rowP * curP + 20).GetCell(7).SetCellValue("TOTAL  " + "  " + "  USD");
                        if (DT.Rows.Count > 0)
                        {
                            workSheet.GetRow(rowP * curP + 15).GetCell(6).SetCellValue(DT.Rows[0]["SID_nbr"].ToString().Trim() + " " + "  Page" + (curP + 1));
                            workSheet.GetRow(rowP * curP + 17).GetCell(6).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                            workSheet.GetRow(rowP * curP + 19).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
                        }

                        //workSheet.GetRow(rowP * curP + 60).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                        //workSheet.GetRow(rowP * curP + 61).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                        //workSheet.GetRow(rowP * curP + 62).GetCell(4).SetCellValue("'----------------------");
                        //workSheet.GetRow(rowP * curP + 63).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                        curP = curP + 1;
                    }

                    curR = 44 + cnt;
                    if (curP == 2)
                    {
                        //curR = 44 + 76 - 53 + cnt;
                        if (!string.IsNullOrEmpty(boxno))
                        {
                            workSheet.GetRow(rowP * curP  - 1).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
                        }

                        workSheet.GetRow(rowP * curP + 2).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                        workSheet.GetRow(rowP * curP + 3).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                        workSheet.GetRow(rowP * curP + 4).GetCell(4).SetCellValue("'----------------------");
                        workSheet.GetRow(rowP * curP + 5).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                    }
                    if (curP == 3)
                    {
                        curR = 44 + 76 - 53 + cnt;
                        if (!string.IsNullOrEmpty(boxno))
                        {
                            workSheet.GetRow(rowP * curP - 1).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
                        }
                        workSheet.GetRow(rowP * curP + 2).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                        workSheet.GetRow(rowP * curP + 3).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                        workSheet.GetRow(rowP * curP + 4).GetCell(4).SetCellValue("'----------------------");
                        workSheet.GetRow(rowP * curP + 5).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                    }
                    if (curP == 4)
                    {
                        curR = 44 + 76 - 30 + cnt;
                        if (!string.IsNullOrEmpty(boxno))
                        {
                            workSheet.GetRow(rowP * curP - 1).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
                        }

                        workSheet.GetRow(rowP * curP + 2).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                        workSheet.GetRow(rowP * curP + 3).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                        workSheet.GetRow(rowP * curP + 4).GetCell(4).SetCellValue("'----------------------");
                        workSheet.GetRow(rowP * curP + 5).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
                    }
                    ICellStyle styleP2 = workbook.CreateCellStyle();

                    styleP2.WrapText = true;
                    styleP2.Alignment = HorizontalAlignment.Left;
                    NPOI.SS.UserModel.IFont fontP2 = workbook.CreateFont();
                    fontP2.FontHeightInPoints = 10;
                    styleP2.SetFont(fontP2);
                    IRow iRowP2 = workSheet.GetRow(curR);

                    iRowP2.CreateCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRowP2.CreateCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRowP2.CreateCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRowP2.CreateCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    iRowP2.CreateCell(4).CellStyle = styleP2;
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRowP2.CreateCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["SID_price2"].ToString().Trim()))
                    {
                        iRowP2.CreateCell(6).SetCellValue(double.Parse(row["SID_price2"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["amount2"].ToString().Trim()))
                    {
                        iRowP2.CreateCell(7).SetCellValue(double.Parse(row["amount2"].ToString().Trim()));
                    }
                    cnt++;
                }
                else
                {
                    ICellStyle style = workbook.CreateCellStyle();

                    //style.WrapText = true;
                    style.Alignment = HorizontalAlignment.Right;
                    NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 12;
                    style.SetFont(font);
                    style.DataFormat = HSSFDataFormat.GetBuiltinFormat("$#,##0.00");
                    IRow iRow = workSheet.GetRow(curR);

                    iRow.GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    iRow.CreateCell(2).SetCellValue(row["SID_PO"].ToString());
                    iRow.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    iRow.GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                    {
                        iRow.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["SID_price2"].ToString().Trim()))
                    {
                        iRow.GetCell(6).SetCellValue(double.Parse(row["SID_price2"].ToString().Trim()));
                    }
                    if (!string.IsNullOrEmpty(row["amount2"].ToString().Trim()))
                    {
                        iRow.GetCell(7).SetCellValue(double.Parse(row["amount2"].ToString().Trim()));
                    }

                    cnt++;
                    curR++;
                }
            }

            if (PackingDet.Rows.Count < cntP + 2)
            {
                for (int i = 60; i < 130; i++)
                {
                    IRow iRowP2 = workSheet.CreateRow(i);
                    workSheet.RemoveRow(iRowP2);
                }
            }
            else
            {
                #region //输出尾栏

                //输出尾栏

                //ICellStyle styleFoot2 = workbook.CreateCellStyle();
                //rowP = rowP + 13;
                //styleFoot.Alignment = HorizontalAlignment.Right;
                //NPOI.SS.UserModel.IFont fontFoot2 = workbook.CreateFont();
                //fontFoot.FontHeightInPoints = 10;
                //styleFoot.SetFont(fontFoot);
                //if (!string.IsNullOrEmpty(boxno))
                //{
                //    workSheet.GetRow(rowP * curP + 40).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
                //}

                //workSheet.GetRow(rowP * curP + 46).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                //workSheet.GetRow(rowP * curP + 47).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                //workSheet.GetRow(rowP * curP + 48).GetCell(4).SetCellValue("'----------------------");
                //workSheet.GetRow(rowP * curP + 49).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");

                #endregion
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
        /// 新版本输出TCP发票 使用NPOI
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void NEWTCPInvoiceToExcel1(string sheetPrefixName, string strShipNo, bool isPkgs, int uid, int plantcode, string checkpricedate, string PicSeal, string stroutFile)
        {
            //System.Data.DataTable PackingDet = packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate);
            System.Data.DataTable dt = packing.SelectInvoiceDetailsInfo("TCP14997", uid, plantcode, checkpricedate);
            string excelname = System.DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
            string strFile = stroutFile;
            string filePath = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
            MemoryStream ms = RenderDataTableToExcel(dt, PicSeal, stroutFile, uid) as MemoryStream;
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            ms = null;
            fs = null;
        }

        public Stream RenderDataTableToExcel(System.Data.DataTable SourceTable, string PicSeal, string stroutFile, int uid)
        {
            MemoryStream ms = new MemoryStream();
            FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/SID_Cust_NewInvoice-NPOI.xls"), FileMode.Open, FileAccess.Read);
            NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");//workbook.CreateSheet();
            NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(17);//sheet.CreateRow(0);
            file.Close();

            //输出头部信息
            #region

            System.Data.DataTable nbr1 = packing.GetPackingInfo(uid);
            string nbr = nbr1.Rows[0]["sid_fob"].ToString();
            string rcvd = nbr1.Rows[0]["sid_fob"].ToString();
            System.Data.DataTable DT = packing.SelectPackingListInfo1(nbr);
            string boxno = "";
            string bl = "";
            if (DT.Rows.Count > 0)
            {
                boxno = DT.Rows[0]["sid_boxno"].ToString();
                bl = DT.Rows[0]["SID_bl"].ToString();
            }
            //if (DT.Rows.Count <= 0)
            //{
            //    //this.Alert("此出运单号信息不全！");
            //    return;
            //}

            //输出头部信息
            sheet.GetRow(1).GetCell(1).SetCellValue("AURORA TECHNOLOGIES LIMITED");
            sheet.GetRow(5).GetCell(1).SetCellValue("COMMERCIAL INVOICE");
            sheet.GetRow(7).GetCell(1).SetCellValue("TO:");
            sheet.GetRow(7).GetCell(3).SetCellValue("TECHNICAL CONSUMER PRODUCTS, INC");
            sheet.GetRow(7).GetCell(5).SetCellValue("INV NO:");
            sheet.GetRow(8).GetCell(3).SetCellValue("325 CAMPUS DRIVE AURORA, OHIO 44202");
            sheet.GetRow(9).GetCell(3).SetCellValue("U.S.A.");
            sheet.GetRow(9).GetCell(5).SetCellValue("DATE:");
            sheet.GetRow(11).GetCell(1).SetCellValue("SHIP TO:");
            sheet.GetRow(11).GetCell(5).SetCellValue("Country of Origin:  China");
            if (DT.Rows.Count > 0)
            {
                string page = DT.Rows[0]["SID_nbr"].ToString().Trim() + " " + "Page1";
                sheet.GetRow(7).GetCell(6).SetCellValue(page);
                sheet.GetRow(9).GetCell(6).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                sheet.GetRow(11).GetCell(3).SetCellValue( DT.Rows[0]["SID_shipto"].ToString());
            }
            if (!string.IsNullOrEmpty(boxno))
            {
                sheet.GetRow(45).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
            }
            sheet.GetRow(51).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
            sheet.GetRow(52).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
            sheet.GetRow(53).GetCell(4).SetCellValue("----------------------");
            sheet.GetRow(54).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");

    #endregion

            //输出明细

            //设置7列宽为100
            sheet.SetColumnWidth(8, 250);
            //sheet.SetColumnWidth(12, 100);
            //加标题
            sheet.GetRow(12).GetCell(1).SetCellValue("NO");
            sheet.GetRow(12).GetCell(2).SetCellValue("PO#");
            sheet.GetRow(12).GetCell(3).SetCellValue("PART");
            sheet.GetRow(12).GetCell(4).SetCellValue("DESCRIPTION");
            sheet.GetRow(12).GetCell(5).SetCellValue("QTY " +  "  UNITS");
            sheet.GetRow(12).GetCell(6).SetCellValue("UNIT PRICE" + "  USD");
            sheet.GetRow(12).GetCell(7).SetCellValue("TOTAL" + "  " + "  USD");

            //明细起始行
            int rowIndex = 13;
            //每页显示行数
            int pagerows = 30;
            //页码
            int pagenum = 0;

            //页码间隔数
            int pagerowhead = 61;
            //页码间隔数
            int pagerow = 60;

            bool showimg = true;
            int rows = rowIndex;
            foreach (DataRow row in SourceTable.Rows)
            {
                NPOI.SS.UserModel.IRow dataRow = sheet.GetRow(rowIndex);
                dataRow.Height = 300;
                //列高50
                sheet.GetRow(rowIndex).Height = 300;

                if (rowIndex < pagerows + rows) //打印第一页
                {
                    sheet.GetRow(rowIndex).GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    sheet.GetRow(rowIndex).GetCell(2).SetCellValue(row["SID_PO"].ToString());
                    sheet.GetRow(rowIndex).GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    sheet.GetRow(rowIndex).GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    sheet.GetRow(rowIndex).GetCell(5).SetCellValue(row["sid_qty_pcs"].ToString());
                    sheet.GetRow(rowIndex).GetCell(6).SetCellValue(row["SID_price2"].ToString());
                    sheet.GetRow(rowIndex).GetCell(7).SetCellValue(row["amount2"].ToString());
                    string picurl = PicSeal;// row["p_url"].ToString();  //图片存储路径
                    if (row.Table.Rows.Count > 0 && showimg)
                    {
                        AddPieChart(sheet, hssfworkbook, PicSeal, 51, 4, PicSeal);
                        showimg = false;
                    }
                }
                else     //打印第二页
                {
                    //输出明细
                    sheet.GetRow(rowIndex + pagerow).GetCell(1).SetCellValue(row["sid_so_line"].ToString());
                    sheet.GetRow(rowIndex + pagerow).GetCell(2).SetCellValue(row["SID_PO"].ToString());
                    sheet.GetRow(rowIndex + pagerow).GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                    sheet.GetRow(rowIndex + pagerow).GetCell(4).SetCellValue(row["sid_cust_partdesc"].ToString());
                    sheet.GetRow(rowIndex + pagerow).GetCell(5).SetCellValue(row["sid_qty_pcs"].ToString());
                    sheet.GetRow(rowIndex + pagerow).GetCell(6).SetCellValue(row["SID_price2"].ToString());
                    sheet.GetRow(rowIndex + pagerow).GetCell(7).SetCellValue(row["amount2"].ToString());

                    if (rowIndex == pagerows + rows)
                    {
                        showimg = true;
                    }

                    string picurl = PicSeal;// row["p_url"].ToString();  //图片存储路径
                    if (row.Table.Rows.Count > 0 && showimg)
                    {
                        AddPieChart(sheet, hssfworkbook, PicSeal, 112, 4, PicSeal);
                        showimg = false;
                    }
                }
                rowIndex++;
            }
            if (SourceTable.Rows.Count < pagerows)
            {
                for (int i = sheet.LastRowNum; i > 56; i--)
                {
                    sheet.ShiftRows(i, i + 1, -1);
                }
            }
            else
            {
                //输出头部信息
                sheet.GetRow(1 + pagerowhead).GetCell(1).SetCellValue("AURORA TECHNOLOGIES LIMITED");
                sheet.GetRow(5 + pagerowhead).GetCell(1).SetCellValue("COMMERCIAL INVOICE");
                sheet.GetRow(7 + pagerowhead).GetCell(1).SetCellValue("TO:");
                sheet.GetRow(7 + pagerowhead).GetCell(3).SetCellValue("TECHNICAL CONSUMER PRODUCTS, INC");
                sheet.GetRow(7 + pagerowhead).GetCell(5).SetCellValue("INV NO:");
                sheet.GetRow(8 + pagerowhead).GetCell(3).SetCellValue("325 CAMPUS DRIVE AURORA, OHIO 44202");
                sheet.GetRow(9 + pagerowhead).GetCell(3).SetCellValue("U.S.A.");
                sheet.GetRow(9 + pagerowhead).GetCell(5).SetCellValue("DATE:");
                sheet.GetRow(11 + pagerowhead).GetCell(1).SetCellValue("SHIP TO:");
                sheet.GetRow(11 + pagerowhead).GetCell(5).SetCellValue("Country of Origin:  China");
                if (DT.Rows.Count > 0)
                {
                    string page = DT.Rows[0]["SID_nbr"].ToString().Trim() + " " + "Page2";
                    sheet.GetRow(7 + pagerowhead).GetCell(6).SetCellValue(page);
                    sheet.GetRow(9 + pagerowhead).GetCell(6).SetCellValue(DT.Rows[0]["SID_shipdate"].ToString());
                    sheet.GetRow(11 + pagerowhead).GetCell(3).SetCellValue(DT.Rows[0]["SID_shipto"].ToString());
                }
                if (!string.IsNullOrEmpty(boxno))
                {
                    sheet.GetRow(45 + pagerowhead).GetCell(3).SetCellValue("CONTAINER NO:" + "  " + boxno);
                }

                //加标题
                sheet.GetRow(12 + pagerowhead).GetCell(1).SetCellValue("NO");
                sheet.GetRow(12 + pagerowhead).GetCell(2).SetCellValue("PO#");
                sheet.GetRow(12 + pagerowhead).GetCell(3).SetCellValue("PART");
                sheet.GetRow(12 + pagerowhead).GetCell(4).SetCellValue("DESCRIPTION");
                sheet.GetRow(12 + pagerowhead).GetCell(5).SetCellValue("QTY " + "  UNITS");
                sheet.GetRow(12 + pagerowhead).GetCell(6).SetCellValue("UNIT PRICE" + "  USD");
                sheet.GetRow(12 + pagerowhead).GetCell(7).SetCellValue("TOTAL" + "  " + "  USD");

                sheet.GetRow(51 + pagerowhead).GetCell(2).SetCellValue("SHANG HAI QIANG LING");
                sheet.GetRow(52 + pagerowhead).GetCell(2).SetCellValue("ELECTRONIC  CO., LTD");
                sheet.GetRow(53 + pagerowhead).GetCell(4).SetCellValue("----------------------");
                sheet.GetRow(54 + pagerowhead).GetCell(4).SetCellValue("AUTHORIZED.SIGNATURE");
            }
            //sheet.RemoveRow(sheet.GetRow(71));
            //Excel.Range range = app.get_Range(workSheet.Cells[rowP * curP + 60, 1], workSheet.Cells[rowP * curP + 200, 1]);
            //range.Select();
            //range.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
            //range.get_Item(1);
            //Excel.Range range1 = //app.get_Range(workSheet.Cells[rowP * curP + 1, 1], workSheet.Cells[rowP * curP + 1, 1]);


            hssfworkbook.Write(ms);
            //workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            //workbook = null;
            hssfworkbook = null;
            return ms;
        }

        private void AddPieChart(NPOI.SS.UserModel.ISheet sheet, NPOI.HSSF.UserModel.HSSFWorkbook workbook, string fileurl, int row, int col, string PicSeal)
        {
            try
            {
                string path = PicSeal;//Server.MapPath("../images/SY_Seal.jpg"); //Server.MapPath("~/html/");
                if (fileurl.Contains("/"))
                {
                    path += fileurl.Substring(fileurl.IndexOf('/'));
                }
                string FileName = path;
                byte[] bytes = System.IO.File.ReadAllBytes(FileName);

                if (!string.IsNullOrEmpty(FileName))
                {
                    int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
                    HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                    //HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 100, 50, col, row, col + 1, row + 1);
                    //##处理照片位置，【图片左上角为（col, row）第row+1行col+1列，右下角为（ col +1, row +1）第 col +1+1行row +1+1列，宽为100，高为100

                    HSSFClientAnchor anchor = new HSSFClientAnchor(20, 20, 10, 20, col, row, col+1, row + 3);
                    HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);

                    pict.Resize();//这句话一定不要，这是用图片原始大小来显示
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// NPOI新版本输出ATL装箱单
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void SamplePrintToExcel(string sheetPrefixName,int uid,int plantcode,string wolist, string woNbr,string part, string relDateFrom, string relDateTo)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            //ISheet detailSheet = workbook.GetSheetAt(1);

            SID sid = new SID();
            //定义参数
            //当前页
            int curP = 0;
            //每页条数
            int cntP = 29;
            //总条数
            int cntT = 0;//Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //总页数
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //输出计数
            int cnt = 0;
            //每页行数
            int rowP = 53;
            //当前行
            int curR = 5;

            if (cntT < 0)
                return;

            #region //输出明细信息

            //输出明细信息
            wo.Wo_ActualRelease helper = new wo.Wo_ActualRelease();
            System.Data.DataTable PackingDet = helper.GetWoActRelPrint(wolist, woNbr, part, relDateFrom, relDateTo);//packing.SelectPackingDetailsInfo(nbr, uid);

            int nCols = PackingDet.Columns.Count;
            int nRows = 3;
            curP = 1;

            foreach (DataRow row in PackingDet.Rows)
            {
                for (int j = 0; j < 2; j++)
                {
                    ICellStyle style = workbook.CreateCellStyle();

                    style.WrapText = true;
                    style.Alignment = HorizontalAlignment.Left;
                    NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    style.SetFont(font);
                    IRow iRow = workSheet.GetRow(curR - 2);
                    if (!string.IsNullOrEmpty(row["wo_plandate"].ToString()))
                    {
                        iRow.GetCell(1).SetCellValue("计划日期: " + Convert.ToDateTime(row["wo_rel_date_act"].ToString()).Year + " 年" + Convert.ToDateTime(row["wo_rel_date_act"].ToString()).Month + " 月" + Convert.ToDateTime(row["wo_rel_date_act"].ToString()).Day + "  日 ");
                    }
                    IRow iRow1 = workSheet.GetRow(curR);
                    iRow1.GetCell(1).SetCellValue("产品型号：" + row["code"].ToString());
                    iRow1.GetCell(3).SetCellValue("批量：" + row["wo_qty_ord"].ToString() + "pcs");
                    iRow1.GetCell(4).SetCellValue("QAD：" + row["wo_part"].ToString());
                    //iRow.GetCell(7).CellStyle = style;
                    IRow iRow2 = workSheet.GetRow(curR + 1);
                    iRow2.GetCell(1).SetCellValue("客户：" + row["ad_name"].ToString() + " 加工单号：" + row["wo_nbr"].ToString());
                    iRow2.GetCell(3).SetCellValue("ID：" + row["wo_lot"].ToString());
                    iRow2.GetCell(4).SetCellValue("线路板号：");


                    IRow iRow3 = workSheet.GetRow(curR + 4);
                    NPOI.SS.UserModel.IFont font4 = workbook.CreateFont();
                    font4.FontHeightInPoints = 18;
                    //font4.Underline = FontUnderlineType.Single;//下划线
                    font4.FontName = "Wingdings 2";

                    HSSFRichTextString TubeTrack = new HSSFRichTextString("灯管跟踪：0");
                    TubeTrack.ApplyFont(5, 6, font4);
                    iRow3.GetCell(3).SetCellValue(TubeTrack);

                    HSSFRichTextString Status = new HSSFRichTextString("0");
                    Status.ApplyFont(0, 1, font4);
                    iRow3.GetCell(2).SetCellValue(Status);

                    HSSFRichTextString Pack = new HSSFRichTextString("待包装：0");
                    Pack.ApplyFont(4, 5, font4);
                    iRow3.GetCell(4).SetCellValue(Pack);

                    HSSFRichTextString ComponentsTrack = new HSSFRichTextString("元器件跟踪：0");
                    ComponentsTrack.ApplyFont(6, 7, font4);
                    iRow3.GetCell(5).SetCellValue(ComponentsTrack);

                    IRow iRow4 = workSheet.GetRow(curR + 6);
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    NPOI.SS.UserModel.IFont font3 = workbook.CreateFont();
                    font3.FontHeightInPoints = 18;
                    font3.Underline = FontUnderlineType.Single;
                    //font3.IsItalic = true;   //斜体
                    cellStyle.SetFont(font3);

                    iRow4.GetCell(1).SetCellValue("留样数：  " + "              ");
                    iRow4.GetCell(2).SetCellValue("失效数：  " + "              ");
                    iRow4.GetCell(3).SetCellValue("入库数：  " + "              ");
                    iRow4.GetCell(4).SetCellValue("其它1：   " + "              ");
                    iRow4.GetCell(1).CellStyle = cellStyle;
                    iRow4.GetCell(2).CellStyle = cellStyle;
                    iRow4.GetCell(3).CellStyle = cellStyle;
                    iRow4.GetCell(4).CellStyle = cellStyle;
                    cnt++;
                    curR = curR + 18;
                }
            }

            if (PackingDet.Rows.Count < cntP + 2)
            {
                int deletecurR = 0;
                if (PackingDet.Rows.Count < 2)
                {
                    deletecurR = 18+18;
                }
                else
                {
                    deletecurR = curR - 9+18;
                }
                for (int i = deletecurR; i < 500; i++)
                {
                    IRow iRowP2 = workSheet.CreateRow(i);
                    workSheet.RemoveRow(iRowP2);
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
                    //detailSheet = null;
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

      }
}
