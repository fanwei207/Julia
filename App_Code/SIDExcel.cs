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
using QADSID;
using APPWS;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.SS.UserModel;
using NPOI.SS;

namespace SIDExcel
{
    ///   <summary> 
    /// 功能说明：套用模板输出Excel
    ///   </summary> 
    public class SIDExcel
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
        public SIDExcel(string templetFilePath, string outputFilePath)
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
        /// 出运头栏
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回单证报关汇总对象列表</returns>
        public System.Data.DataTable Selectshipmstr(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectShipMstr";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 出运明细
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回单证报关汇总对象列表</returns>
        public System.Data.DataTable SelectshipDet(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectShipDet";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 出运明细
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回单证报关汇总对象列表</returns>
        public System.Data.DataTable SelectshipDetZQZ(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectShipDetZQZ";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// NPOI新版本输出ATL发票:Excel
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="stddate"></param>
        /// <param name="enddate"></param>
        /// 
        public void SidDetToExcelNewByNPOI(string sheetPrefixName, string ShipNo, int uid, string uName, int plantcode)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(1);

            SID sid = new SID();
            //定义参数

            //当前行
            int curR = 1;


            #region //输出头部信息

            //输出头部信息

            System.Data.DataTable SidMstr = Selectshipmstr(ShipNo);

            //输出头部信息
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;


            workSheet.GetRow(curR + 0).GetCell(0).SetCellValue("出运单号:");
            workSheet.GetRow(curR + 0).GetCell(2).SetCellValue(SidMstr.Rows[0]["sid_nbr"].ToString());//.Replace(" ",""));
            if (SidMstr.Rows[0]["sid_nbr"].ToString() != SidMstr.Rows[0]["sid_nbr1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                    workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 0).GetCell(3).SetCellValue("系统货运单号:");
            workSheet.GetRow(curR + 0).GetCell(4).SetCellValue(SidMstr.Rows[0]["sid_pk"].ToString());
            if (SidMstr.Rows[0]["sid_pk"].ToString() != SidMstr.Rows[0]["sid_pk1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 0).GetCell(4).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 0).GetCell(7).SetCellValue("出厂日期:");
            workSheet.GetRow(curR + 0).GetCell(9).SetCellValue(SidMstr.Rows[0]["sid_outdate"].ToString());
            if (SidMstr.Rows[0]["sid_outdate"].ToString() != SidMstr.Rows[0]["sid_outdate1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 0).GetCell(9).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 0).GetCell(13).SetCellValue("版本:"+SidMstr.Rows[0]["sid_var"].ToString());
            workSheet.GetRow(curR + 1).GetCell(0).SetCellValue("运输方式:");
            workSheet.GetRow(curR + 1).GetCell(2).SetCellValue(SidMstr.Rows[0]["sid_via"].ToString());
            if (SidMstr.Rows[0]["sid_via"].ToString() != SidMstr.Rows[0]["sid_via1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 1).GetCell(2).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 1).GetCell(3).SetCellValue("集箱箱型:");
            workSheet.GetRow(curR + 1).GetCell(5).SetCellValue(SidMstr.Rows[0]["sid_Ctype"].ToString());
            if (SidMstr.Rows[0]["sid_Ctype"].ToString() != SidMstr.Rows[0]["sid_Ctype1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 1).GetCell(5).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 1).GetCell(7).SetCellValue("出运日期:");
            workSheet.GetRow(curR + 1).GetCell(9).SetCellValue(SidMstr.Rows[0]["sid_shipdate"].ToString());
            if (SidMstr.Rows[0]["sid_shipdate"].ToString() != SidMstr.Rows[0]["sid_shipdate1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 1).GetCell(9).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 1).GetCell(13).SetCellValue("操作者:" + uName);

            workSheet.GetRow(curR + 2).GetCell(0).SetCellValue("运往:");
            workSheet.GetRow(curR + 2).GetCell(2).SetCellValue(SidMstr.Rows[0]["sid_shipto"].ToString());
            if (SidMstr.Rows[0]["sid_shipto"].ToString() != SidMstr.Rows[0]["sid_shipto1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 2).GetCell(2).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 2).GetCell(7).SetCellValue("装箱地点:");
            workSheet.GetRow(curR + 2).GetCell(9).SetCellValue(SidMstr.Rows[0]["sid_site"].ToString());
            if (SidMstr.Rows[0]["sid_site"].ToString() != SidMstr.Rows[0]["sid_site1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 2).GetCell(9).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 2).GetCell(11).SetCellValue(SidMstr.Rows[0]["sid_domain"].ToString());
            if (SidMstr.Rows[0]["sid_domain"].ToString() != SidMstr.Rows[0]["sid_domain1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 2).GetCell(11).CellStyle = cellStyle;
            }

            workSheet.GetRow(curR + 3).GetCell(0).SetCellValue("总箱数:");
            workSheet.GetRow(curR + 3).GetCell(2).SetCellValue(SidMstr.Rows[0]["box"].ToString());
            //if (SidMstr.Rows[0]["box"].ToString() != SidMstr.Rows[0]["box1"].ToString())
            //{
            //    workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            //}
            workSheet.GetRow(curR + 3).GetCell(3).SetCellValue("总重量:");
            workSheet.GetRow(curR + 3).GetCell(5).SetCellValue(SidMstr.Rows[0]["weight"].ToString());
            //if (SidMstr.Rows[0]["weight"].ToString() != SidMstr.Rows[0]["weight1"].ToString())
            //{
            //    workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            //}
            workSheet.GetRow(curR + 3).GetCell(7).SetCellValue("总体积:");
            workSheet.GetRow(curR + 3).GetCell(9).SetCellValue(SidMstr.Rows[0]["volume"].ToString());
            //if (SidMstr.Rows[0]["volume"].ToString() != SidMstr.Rows[0]["volume1"].ToString())
            //{
            //    workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            //}


            workSheet.GetRow(curR + 4).GetCell(0).SetCellValue("序号:");
            workSheet.GetRow(curR + 4).GetCell(1).SetCellValue("系列:");
            workSheet.GetRow(curR + 4).GetCell(2).SetCellValue("物料编码:");
            workSheet.GetRow(curR + 4).GetCell(3).SetCellValue("客户物料:");
            workSheet.GetRow(curR + 4).GetCell(4).SetCellValue("出运套数:");
            workSheet.GetRow(curR + 4).GetCell(5).SetCellValue("出运只数:");
            workSheet.GetRow(curR + 4).GetCell(6).SetCellValue("箱数:");
            workSheet.GetRow(curR + 4).GetCell(7).SetCellValue("件数:");
            workSheet.GetRow(curR + 4).GetCell(8).SetCellValue("商检号:");
            workSheet.GetRow(curR + 4).GetCell(9).SetCellValue("备注:");
            workSheet.GetRow(curR + 4).GetCell(10).SetCellValue("销售单号:");
            workSheet.GetRow(curR + 4).GetCell(11).SetCellValue("行号:");
            workSheet.GetRow(curR + 4).GetCell(12).SetCellValue("批序号/加工单:");
            workSheet.GetRow(curR + 4).GetCell(13).SetCellValue("TCP订单号:");
            workSheet.GetRow(curR + 4).GetCell(14).SetCellValue("重量:");
            workSheet.GetRow(curR + 4).GetCell(15).SetCellValue("体积:");
            workSheet.GetRow(curR + 4).GetCell(16).SetCellValue("客户订单号");
            workSheet.GetRow(curR + 4).GetCell(17).SetCellValue("ATL订单号:");
            workSheet.GetRow(curR + 4).GetCell(18).SetCellValue("Shipment:");
            workSheet.GetRow(curR + 4).GetCell(19).SetCellValue("QAD订单号:");
            workSheet.GetRow(curR + 4).GetCell(20).SetCellValue("QAD行号:");
            //workSheet.GetRow(curR + 4).GetCell(16).SetCellValue("每箱标号:");
            //workSheet.GetRow(curR + 4).GetCell(17).SetCellValue("SKU:");
            //workSheet.GetRow(curR + 4).GetCell(18).SetCellValue("描述:");
            //ICellStyle cellStyle = workbook.CreateCellStyle();
            //cellStyle.FillPattern = FillPattern.SolidForeground;
            //cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            //workSheet.GetRow(curR + 4).GetCell(1).CellStyle = cellStyle;

            #endregion

            #region //输出明细信息

            //输出明细信息
            System.Data.DataTable SidDet = SelectshipDet(ShipNo); ;//packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate, checkpricedate2);

            //int nCols = PackingDet.Columns.Count;

            foreach (DataRow row in SidDet.Rows)
            {
                ICellStyle styleP2 = workbook.CreateCellStyle();

                styleP2.WrapText = true;
                styleP2.Alignment = HorizontalAlignment.Left;
                NPOI.SS.UserModel.IFont fontP2 = workbook.CreateFont();
                fontP2.FontHeightInPoints = 10;
                styleP2.SetFont(fontP2);
                IRow iRowP2 = workSheet.GetRow(curR + 5);

                iRowP2.GetCell(0).SetCellValue(row["sid_rowno"].ToString());
                iRowP2.GetCell(1).SetCellValue(row["sid_sno"].ToString());
                if (row["sid_sno"].ToString() != row["sid_sno1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(1).CellStyle = cellStyle;
                }
                iRowP2.GetCell(2).SetCellValue(row["SID_qad"].ToString());
                if (row["SID_qad"].ToString() != row["SID_qad1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(2).CellStyle = cellStyle;
                }
                iRowP2.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                if (row["sid_cust_part"].ToString() != row["sid_cust_part1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(3).CellStyle = cellStyle;
                }
                iRowP2.GetCell(4).SetCellValue(row["sid_qty_set"].ToString());
                if (row["sid_qty_set"].ToString() != row["sid_qty_set1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(4).CellStyle = cellStyle;
                }
                iRowP2.GetCell(5).SetCellValue(row["sid_qty_pcs"].ToString());
                if (row["sid_qty_pcs"].ToString() != row["sid_qty_pcs1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(5).CellStyle = cellStyle;
                }
                iRowP2.GetCell(6).SetCellValue(row["sid_qty_box"].ToString());
                if (row["sid_qty_box"].ToString() != row["sid_qty_box1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(6).CellStyle = cellStyle;
                }
                iRowP2.GetCell(9).SetCellValue(row["sid_memo"].ToString());
                if (row["sid_memo"].ToString() != row["sid_memo1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(9).CellStyle = cellStyle;
                }
                iRowP2.GetCell(10).SetCellValue(row["sid_so_nbr"].ToString());
                if (row["sid_so_nbr"].ToString() != row["sid_so_nbr1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(10).CellStyle = cellStyle;
                }
                iRowP2.GetCell(11).SetCellValue(row["sid_so_line"].ToString());
                if (row["sid_so_line"].ToString() != row["sid_so_line1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(11).CellStyle = cellStyle;
                }
                iRowP2.GetCell(13).SetCellValue(row["sid_po"].ToString());
                if (row["sid_po"].ToString() != row["sid_po1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(13).CellStyle = cellStyle;
                }
                iRowP2.GetCell(14).SetCellValue(row["sid_weight"].ToString());
                if (row["sid_weight"].ToString() != row["sid_weight1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(14).CellStyle = cellStyle;
                }
                iRowP2.GetCell(15).SetCellValue(row["sid_volume"].ToString());
                if (row["sid_volume"].ToString() != row["sid_volume1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(15).CellStyle = cellStyle;
                }
                iRowP2.GetCell(16).SetCellValue(row["sid_fob"].ToString());
                if (row["sid_fob"].ToString() != row["sid_fob1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(16).CellStyle = cellStyle;
                }
                iRowP2.GetCell(17).SetCellValue(row["sid_atl"].ToString());
                if (row["sid_atl"].ToString() != row["sid_atl1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(17).CellStyle = cellStyle;
                }
                iRowP2.GetCell(19).SetCellValue(row["sid_qadpo"].ToString());
                if (row["sid_qadpo"].ToString() != row["sid_qadpo1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(19).CellStyle = cellStyle;
                }
                iRowP2.GetCell(20).SetCellValue(row["sid_qadline"].ToString());
                if (row["sid_qadline"].ToString() != row["sid_qadline1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(20).CellStyle = cellStyle;
                }
                //iRowP2.GetCell(18).SetCellValue(row["sid_cust_partdesc"].ToString());
                //if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                //{
                //    iRowP2.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                //}
                curR = curR + 1;
            }


            ICellStyle cellStyleLCL = workbook.CreateCellStyle();
            cellStyleLCL.FillPattern = FillPattern.SolidForeground;
            cellStyleLCL.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            cellStyleLCL.Alignment = HorizontalAlignment.Center;
            cellStyleLCL.VerticalAlignment = VerticalAlignment.Center;
            cellStyleLCL.BorderBottom = BorderStyle.Thin;
            cellStyleLCL.BorderLeft = BorderStyle.Thin;
            cellStyleLCL.BorderRight = BorderStyle.Thin;
            cellStyleLCL.BorderTop = BorderStyle.Thin;
            cellStyleLCL.BottomBorderColor = HSSFColor.Black.Index;
            cellStyleLCL.LeftBorderColor = HSSFColor.Black.Index;
            cellStyleLCL.RightBorderColor = HSSFColor.Black.Index;
            cellStyleLCL.TopBorderColor = HSSFColor.Black.Index;  


            if (!string.IsNullOrEmpty(SidMstr.Rows[0]["sid_lcl"].ToString().Trim()))
            {
                NPOI.SS.Util.CellRangeAddress m_region = new NPOI.SS.Util.CellRangeAddress(curR + 8, curR + 10, 10, 16);
                workSheet.AddMergedRegion(m_region);
                IRow iRowP3 = workSheet.GetRow(curR + 8);
                iRowP3.GetCell(10).SetCellValue(SidMstr.Rows[0]["sid_lcl"].ToString().Trim());
                iRowP3.GetCell(10).CellStyle = cellStyleLCL;
                //workSheet.AddMergedRegion(new HSSFCellRangeAddress(10, curR + 8, 5, 3));
                //workSheet.AddMergedRegion(new HSSFCellRangeAddress(0, 0, 0, 10));
                //HSSFCellRangeAddress m_region = new HSSFCellRangeAddress(10, curR + 8, 5, 3));

            }
            #endregion


            #region //输出尾栏

            //输出尾栏

            //ICellStyle styleFoot = workbook.CreateCellStyle();

            //styleFoot.Alignment = HorizontalAlignment.Right;
            //NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            //fontFoot.FontHeightInPoints = 10;
            //styleFoot.SetFont(fontFoot);
            //workSheet.CreateRow(curR + 49).CreateCell(3).SetCellValue("Country of Origin:  China");
            //workSheet.CreateRow(curR + 51).CreateCell(3).SetCellValue("SHANG HAI QIANG LING");
            //workSheet.CreateRow(curR + 52).CreateCell(3).SetCellValue("ELECTRONIC  CO., LTD");

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
        /// 
        public void SidDetZQZToExcelNewByNPOI(string sheetPrefixName, string ShipNo, int uid, string uName, int plantcode)
        {
            //读取模板路径
            FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new HSSFWorkbook(templetFile);
            ISheet workSheet = workbook.GetSheetAt(0);
            ISheet detailSheet = workbook.GetSheetAt(1);

            SID sid = new SID();
            //定义参数

            //当前行
            int curR = 1;


            #region //输出头部信息

            //输出头部信息

            System.Data.DataTable SidMstr = Selectshipmstr(ShipNo);

            //输出头部信息
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;


            workSheet.GetRow(curR + 0).GetCell(0).SetCellValue("出运单号:");
            workSheet.GetRow(curR + 0).GetCell(2).SetCellValue(SidMstr.Rows[0]["sid_nbr"].ToString());//.Replace(" ",""));
            if (SidMstr.Rows[0]["sid_nbr"].ToString() != SidMstr.Rows[0]["sid_nbr1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 0).GetCell(3).SetCellValue("系统货运单号:");
            workSheet.GetRow(curR + 0).GetCell(4).SetCellValue(SidMstr.Rows[0]["sid_pk"].ToString());
            if (SidMstr.Rows[0]["sid_pk"].ToString() != SidMstr.Rows[0]["sid_pk1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 0).GetCell(4).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 0).GetCell(7).SetCellValue("出厂日期:");
            workSheet.GetRow(curR + 0).GetCell(9).SetCellValue(SidMstr.Rows[0]["sid_outdate"].ToString());
            if (SidMstr.Rows[0]["sid_outdate"].ToString() != SidMstr.Rows[0]["sid_outdate1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 0).GetCell(9).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 0).GetCell(16).SetCellValue("版本:" + SidMstr.Rows[0]["sid_var"].ToString());
            workSheet.GetRow(curR + 1).GetCell(0).SetCellValue("运输方式:");
            workSheet.GetRow(curR + 1).GetCell(2).SetCellValue(SidMstr.Rows[0]["sid_via"].ToString());
            if (SidMstr.Rows[0]["sid_via"].ToString() != SidMstr.Rows[0]["sid_via1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 1).GetCell(2).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 1).GetCell(3).SetCellValue("集箱箱型:");
            workSheet.GetRow(curR + 1).GetCell(5).SetCellValue(SidMstr.Rows[0]["sid_Ctype"].ToString());
            if (SidMstr.Rows[0]["sid_Ctype"].ToString() != SidMstr.Rows[0]["sid_Ctype1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 1).GetCell(5).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 1).GetCell(7).SetCellValue("出运日期:");
            workSheet.GetRow(curR + 1).GetCell(9).SetCellValue(SidMstr.Rows[0]["sid_shipdate"].ToString());
            if (SidMstr.Rows[0]["sid_shipdate"].ToString() != SidMstr.Rows[0]["sid_shipdate1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 1).GetCell(9).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 1).GetCell(16).SetCellValue("操作者:" + uName);

            workSheet.GetRow(curR + 2).GetCell(0).SetCellValue("运往:");
            workSheet.GetRow(curR + 2).GetCell(2).SetCellValue(SidMstr.Rows[0]["sid_shipto"].ToString());
            if (SidMstr.Rows[0]["sid_shipto"].ToString() != SidMstr.Rows[0]["sid_shipto1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 2).GetCell(2).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 2).GetCell(7).SetCellValue("装箱地点:");
            workSheet.GetRow(curR + 2).GetCell(9).SetCellValue(SidMstr.Rows[0]["sid_site"].ToString());
            if (SidMstr.Rows[0]["sid_site"].ToString() != SidMstr.Rows[0]["sid_site1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 2).GetCell(9).CellStyle = cellStyle;
            }
            workSheet.GetRow(curR + 2).GetCell(11).SetCellValue(SidMstr.Rows[0]["sid_domain"].ToString());
            if (SidMstr.Rows[0]["sid_domain"].ToString() != SidMstr.Rows[0]["sid_domain1"].ToString() && !string.IsNullOrEmpty(SidMstr.Rows[0]["sid_nbr1"].ToString()))
            {
                workSheet.GetRow(curR + 2).GetCell(11).CellStyle = cellStyle;
            }

            workSheet.GetRow(curR + 3).GetCell(0).SetCellValue("总箱数:");
            workSheet.GetRow(curR + 3).GetCell(2).SetCellValue(SidMstr.Rows[0]["box"].ToString());
            //if (SidMstr.Rows[0]["box"].ToString() != SidMstr.Rows[0]["box1"].ToString())
            //{
            //    workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            //}
            workSheet.GetRow(curR + 3).GetCell(3).SetCellValue("总重量:");
            workSheet.GetRow(curR + 3).GetCell(5).SetCellValue(SidMstr.Rows[0]["weight"].ToString());
            //if (SidMstr.Rows[0]["weight"].ToString() != SidMstr.Rows[0]["weight1"].ToString())
            //{
            //    workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            //}
            workSheet.GetRow(curR + 3).GetCell(7).SetCellValue("总体积:");
            workSheet.GetRow(curR + 3).GetCell(9).SetCellValue(SidMstr.Rows[0]["volume"].ToString());
            //if (SidMstr.Rows[0]["volume"].ToString() != SidMstr.Rows[0]["volume1"].ToString())
            //{
            //    workSheet.GetRow(curR + 0).GetCell(2).CellStyle = cellStyle;
            //}


            workSheet.GetRow(curR + 4).GetCell(0).SetCellValue("序号:");
            workSheet.GetRow(curR + 4).GetCell(1).SetCellValue("系列:");
            workSheet.GetRow(curR + 4).GetCell(2).SetCellValue("物料编码:");
            workSheet.GetRow(curR + 4).GetCell(3).SetCellValue("客户物料:");
            workSheet.GetRow(curR + 4).GetCell(4).SetCellValue("出运套数:");
            workSheet.GetRow(curR + 4).GetCell(5).SetCellValue("出运只数:");
            workSheet.GetRow(curR + 4).GetCell(6).SetCellValue("箱数:");
            workSheet.GetRow(curR + 4).GetCell(7).SetCellValue("件数:");
            workSheet.GetRow(curR + 4).GetCell(8).SetCellValue("商检号:");
            workSheet.GetRow(curR + 4).GetCell(9).SetCellValue("备注:");
            workSheet.GetRow(curR + 4).GetCell(10).SetCellValue("销售单号1:");
            workSheet.GetRow(curR + 4).GetCell(11).SetCellValue("行号1:");
            workSheet.GetRow(curR + 4).GetCell(12).SetCellValue("销售单号2:");
            workSheet.GetRow(curR + 4).GetCell(13).SetCellValue("行号2:");
            workSheet.GetRow(curR + 4).GetCell(14).SetCellValue("制地");
            workSheet.GetRow(curR + 4).GetCell(15).SetCellValue("批序号/加工单:");
            workSheet.GetRow(curR + 4).GetCell(16).SetCellValue("TCP订单号:");
            workSheet.GetRow(curR + 4).GetCell(17).SetCellValue("重量:");
            workSheet.GetRow(curR + 4).GetCell(18).SetCellValue("体积:");
            workSheet.GetRow(curR + 4).GetCell(19).SetCellValue("客户订单号");
            workSheet.GetRow(curR + 4).GetCell(20).SetCellValue("ATL订单号:");
            workSheet.GetRow(curR + 4).GetCell(21).SetCellValue("Shipment:");
            workSheet.GetRow(curR + 4).GetCell(22).SetCellValue("QAD订单号:");
            workSheet.GetRow(curR + 4).GetCell(23).SetCellValue("QAD行号:");
            //workSheet.GetRow(curR + 4).GetCell(16).SetCellValue("每箱标号:");
            //workSheet.GetRow(curR + 4).GetCell(17).SetCellValue("SKU:");
            //workSheet.GetRow(curR + 4).GetCell(18).SetCellValue("描述:");
            //ICellStyle cellStyle = workbook.CreateCellStyle();
            //cellStyle.FillPattern = FillPattern.SolidForeground;
            //cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            //workSheet.GetRow(curR + 4).GetCell(1).CellStyle = cellStyle;

            #endregion

            #region //输出明细信息

            //输出明细信息
            System.Data.DataTable SidDet = SelectshipDetZQZ(ShipNo); ;//packing.SelectInvoiceDetailsInfo(nbr, uid, plantcode, checkpricedate, checkpricedate2);

            //int nCols = PackingDet.Columns.Count;

            foreach (DataRow row in SidDet.Rows)
            {
                ICellStyle styleP2 = workbook.CreateCellStyle();

                styleP2.WrapText = true;
                styleP2.Alignment = HorizontalAlignment.Left;
                NPOI.SS.UserModel.IFont fontP2 = workbook.CreateFont();
                fontP2.FontHeightInPoints = 10;
                styleP2.SetFont(fontP2);
                IRow iRowP2 = workSheet.GetRow(curR + 5);

                iRowP2.GetCell(0).SetCellValue(row["sid_rowno"].ToString());
                iRowP2.GetCell(1).SetCellValue(row["sid_sno"].ToString());
                if (row["sid_sno"].ToString() != row["sid_sno1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(1).CellStyle = cellStyle;
                }
                iRowP2.GetCell(2).SetCellValue(row["SID_qad"].ToString());
                if (row["SID_qad"].ToString() != row["SID_qad1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(2).CellStyle = cellStyle;
                }
                iRowP2.GetCell(3).SetCellValue(row["sid_cust_part"].ToString());
                if (row["sid_cust_part"].ToString() != row["sid_cust_part1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(3).CellStyle = cellStyle;
                }
                iRowP2.GetCell(4).SetCellValue(row["sid_qty_set"].ToString());
                if (row["sid_qty_set"].ToString() != row["sid_qty_set1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(4).CellStyle = cellStyle;
                }
                iRowP2.GetCell(5).SetCellValue(row["sid_qty_pcs"].ToString());
                if (row["sid_qty_pcs"].ToString() != row["sid_qty_pcs1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(5).CellStyle = cellStyle;
                }
                iRowP2.GetCell(6).SetCellValue(row["sid_qty_box"].ToString());
                if (row["sid_qty_box"].ToString() != row["sid_qty_box1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(6).CellStyle = cellStyle;
                }
                iRowP2.GetCell(9).SetCellValue(row["sid_memo"].ToString());
                if (row["sid_memo"].ToString() != row["sid_memo1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(9).CellStyle = cellStyle;
                }
                iRowP2.GetCell(10).SetCellValue(row["sid_so_nbr"].ToString());
                if (row["sid_so_nbr"].ToString() != row["sid_so_nbr1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(10).CellStyle = cellStyle;
                }
                iRowP2.GetCell(11).SetCellValue(row["sid_so_line"].ToString());
                if (row["sid_so_line"].ToString() != row["sid_so_line1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(11).CellStyle = cellStyle;
                }
                iRowP2.GetCell(12).SetCellValue(row["sod__qadc03"].ToString());
                iRowP2.GetCell(13).SetCellValue(row["sod__qadi01"].ToString());
                iRowP2.GetCell(14).SetCellValue(row["wo_site"].ToString());
                iRowP2.GetCell(16).SetCellValue(row["sid_po"].ToString());
                if (row["sid_po"].ToString() != row["sid_po1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(16).CellStyle = cellStyle;
                }
                iRowP2.GetCell(17).SetCellValue(row["sid_weight"].ToString());
                if (row["sid_weight"].ToString() != row["sid_weight1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(18).CellStyle = cellStyle;
                }
                iRowP2.GetCell(18).SetCellValue(row["sid_volume"].ToString());
                if (row["sid_volume"].ToString() != row["sid_volume1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(18).CellStyle = cellStyle;
                }
                iRowP2.GetCell(19).SetCellValue(row["sid_fob"].ToString());
                if (row["sid_fob"].ToString() != row["sid_fob1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(19).CellStyle = cellStyle;
                }
                iRowP2.GetCell(20).SetCellValue(row["sid_atl"].ToString());
                if (row["sid_atl"].ToString() != row["sid_atl1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(20).CellStyle = cellStyle;
                }
                iRowP2.GetCell(22).SetCellValue(row["sid_qadpo"].ToString());
                if (row["sid_qadpo"].ToString() != row["sid_qadpo1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(22).CellStyle = cellStyle;
                }
                iRowP2.GetCell(23).SetCellValue(row["sid_qadline"].ToString());
                if (row["sid_qadline"].ToString() != row["sid_qadline1"].ToString() && !string.IsNullOrEmpty(row["SID_qad1"].ToString()))
                {
                    iRowP2.GetCell(23).CellStyle = cellStyle;
                }
                //iRowP2.GetCell(18).SetCellValue(row["sid_cust_partdesc"].ToString());
                //if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                //{
                //    iRowP2.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                //}
                curR = curR + 1;
            }


            ICellStyle cellStyleLCL = workbook.CreateCellStyle();
            cellStyleLCL.FillPattern = FillPattern.SolidForeground;
            cellStyleLCL.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            cellStyleLCL.Alignment = HorizontalAlignment.Center;
            cellStyleLCL.VerticalAlignment = VerticalAlignment.Center;
            cellStyleLCL.BorderBottom = BorderStyle.Thin;
            cellStyleLCL.BorderLeft = BorderStyle.Thin;
            cellStyleLCL.BorderRight = BorderStyle.Thin;
            cellStyleLCL.BorderTop = BorderStyle.Thin;
            cellStyleLCL.BottomBorderColor = HSSFColor.Black.Index;
            cellStyleLCL.LeftBorderColor = HSSFColor.Black.Index;
            cellStyleLCL.RightBorderColor = HSSFColor.Black.Index;
            cellStyleLCL.TopBorderColor = HSSFColor.Black.Index;


            if (!string.IsNullOrEmpty(SidMstr.Rows[0]["sid_lcl"].ToString().Trim()))
            {
                NPOI.SS.Util.CellRangeAddress m_region = new NPOI.SS.Util.CellRangeAddress(curR + 8, curR + 10, 10, 16);
                workSheet.AddMergedRegion(m_region);
                IRow iRowP3 = workSheet.GetRow(curR + 8);
                iRowP3.GetCell(10).SetCellValue(SidMstr.Rows[0]["sid_lcl"].ToString().Trim());
                iRowP3.GetCell(10).CellStyle = cellStyleLCL;
                //workSheet.AddMergedRegion(new HSSFCellRangeAddress(10, curR + 8, 5, 3));
                //workSheet.AddMergedRegion(new HSSFCellRangeAddress(0, 0, 0, 10));
                //HSSFCellRangeAddress m_region = new HSSFCellRangeAddress(10, curR + 8, 5, 3));

            }
            #endregion


            #region //输出尾栏

            //输出尾栏

            //ICellStyle styleFoot = workbook.CreateCellStyle();

            //styleFoot.Alignment = HorizontalAlignment.Right;
            //NPOI.SS.UserModel.IFont fontFoot = workbook.CreateFont();
            //fontFoot.FontHeightInPoints = 10;
            //styleFoot.SetFont(fontFoot);
            //workSheet.CreateRow(curR + 49).CreateCell(3).SetCellValue("Country of Origin:  China");
            //workSheet.CreateRow(curR + 51).CreateCell(3).SetCellValue("SHANG HAI QIANG LING");
            //workSheet.CreateRow(curR + 52).CreateCell(3).SetCellValue("ELECTRONIC  CO., LTD");

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

        public System.Data.DataTable GetSidNbrVar(string nbr)
        {
            try
            {
                //string strSql = "sp_sid_SelectSidNbrVar";
                //SqlParameter[] sqlParam = new SqlParameter[1];
                //sqlParam[0] = new SqlParameter("@nbr", nbr);
                //return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
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