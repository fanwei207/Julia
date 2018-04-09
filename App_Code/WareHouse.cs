using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using adamFuncs;
using CommClass;
using System.Data.OleDb;
using System.Text.RegularExpressions;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Data;
using System.Text;
using System.Text;


namespace WHInfo
{
    /// <summary>
    /// Summary description for WH
    /// </summary>
    public class WareHouse
    {
        adamClass adam = new adamClass();
        String strSQL = "";

        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public WareHouse()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region 计划外入出库

        /// <summary>
        /// 入账退回操作
        /// </summary>
        public int ReturnAccounts(string uID, string domain, string nbr, string lot, string type, string id, string remarks)
        {
            strSQL = "sp_wh_ReturnAccounts";
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@domain", domain);
            parm[1] = new SqlParameter("@nbr", nbr);
            parm[2] = new SqlParameter("@lot", lot);
            parm[3] = new SqlParameter("@type", type);
            parm[4] = new SqlParameter("@id", id);
            parm[5] = new SqlParameter("@remarks", remarks);
            parm[6] = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }
        /// <summary>
        /// 入账退回操作
        /// </summary>
        public int ReturnAccountsWaintCount(string uID, string domain, string type)
        {
            strSQL = "sp_wh_ReturnWaitCount";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@domain", domain);
            parm[1] = new SqlParameter("@type", type);
            parm[2] = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// 计划外出库判断物料号是否存在
        /// </summary>
        public int UnPlssCheckPart(string part, string domain)
        {
            strSQL = "sp_wh_selectCheckPart";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@part", part);
            parm[1] = new SqlParameter("@domain", domain);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }
        /// <summary>
        /// 计划外出库判断库位是否存在
        /// </summary>
        public int UnPlssCheckLoc(string loc, string domain, string site)
        {
            strSQL = "sp_wh_selectCheckLoc";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@loc", loc);
            parm[1] = new SqlParameter("@domain", domain);
            parm[2] = new SqlParameter("@site", site);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// 计划外出库获取单位
        /// </summary>
        public SqlDataReader UnPlssGetUM(string domain)
        {
            strSQL = "sp_wh_selectUnplssUm";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@domain", domain);
            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }

        /// <summary>
        /// 计划外出库获取单位
        /// </summary>
        public DataTable GetUnplssAppInfos(string AppNo)
        {
            strSQL = "sp_wh_selectUnplssAppInfos";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }

        /// <summary>
        /// 计划外出库获取单位
        /// </summary>
        public DataTable GetPartDescInfos(string AppNo)
        {
            strSQL = "sp_wh_selectPartDescInfos";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }

        /// <summary>
        /// 计划外入,出库提交
        /// </summary>
        public int SubmitApp(string AppNo, string PlantCode, string Types)
        {
            strSQL = "sp_wh_SubmitAppInfos";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            parm[1] = new SqlParameter("@PlantCode", PlantCode);
            parm[2] = new SqlParameter("@Types", Types);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// 计划外出库获取单位
        /// </summary>
        public DataTable GetUNPHaveAppInfos(string AppNo, string Applyer, string DepartmentID, string Site, string Type, string CheckType, string ProjType, string Startdate, string Enddate,string Nbr,string ID)
        {
            strSQL = "sp_wh_selectUNPHaveAppInfos";
            SqlParameter[] parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            parm[1] = new SqlParameter("@Applyer", Applyer);
            parm[2] = new SqlParameter("@DepartmentID", DepartmentID);
            parm[3] = new SqlParameter("@Site", Site);
            parm[4] = new SqlParameter("@Type", Type);
            parm[5] = new SqlParameter("@CheckType", CheckType);
            parm[6] = new SqlParameter("@ProjType", ProjType);
            parm[7] = new SqlParameter("@Startdate", Startdate);
            parm[8] = new SqlParameter("@Enddate", Enddate);
            parm[9] = new SqlParameter("@Nbr", Nbr);
            parm[10] = new SqlParameter("@ID", ID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }

        /// <summary>
        /// 计划外申请删除
        /// </summary>
        public int DeleteNUPApp(string AppNo)
        {
            strSQL = "sp_wh_DeleteUnpApp";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void createUNPPlss(string stroutFile, string DepartmentID, string DepartmentName, string Site, string No, string ProjTypeID, string ProjTypeName, string Date, string Applyer, int uid)
        {
            System.Data.DataTable dt = GetUnplssAppInfos(No);//generationInquiry(IMID, out company, out createdDate, out createByName, uid);
            string excelname = System.DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
            string strFile = stroutFile;
            string filePath = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
            MemoryStream ms = UNPPlssRenderDataTableToExcel(dt, filePath, uid, DepartmentID, DepartmentName, Site, No, ProjTypeID, ProjTypeName, Date, Applyer) as MemoryStream;
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            ms = null;
            fs = null;
        }

        public Stream UNPPlssRenderDataTableToExcel(System.Data.DataTable SourceTable, string stroutFile, int uid, string DepartmentID, string DepartmentName, string Site, string No, string ProjTypeID, string ProjTypeName, string Date, string Applyer)
        {
            MemoryStream ms = new MemoryStream();
            FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/wh_exceunpiss.xls"), FileMode.Open, FileAccess.Read);
            NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");//workbook.CreateSheet();
            NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(17);//sheet.GetRow(0);
            file.Close();


            //输出头部信息
            #region 输出头部信息
            sheet.GetRow(0).GetCell(0).SetCellValue("计划外出库单申请");
            sheet.GetRow(1).GetCell(0).SetCellValue("上海强凌电子有限公司");
            sheet.GetRow(2).GetCell(0).SetCellValue("上海松江泗径镇望东南路139号");
            sheet.GetRow(3).GetCell(0).SetCellValue("领用部门：" + DepartmentName + "(" + DepartmentID + ")");
            sheet.GetRow(3).GetCell(3).SetCellValue("地点：" + Site);
            sheet.GetRow(3).GetCell(6).SetCellValue("单据号：" + No);
            sheet.GetRow(4).GetCell(0).SetCellValue("项目类型：" + ProjTypeName + "(" + ProjTypeID + ")");
            sheet.GetRow(4).GetCell(6).SetCellValue("出库日期：" + Date);

            //输出明细


            //设置10列宽为100
            sheet.SetColumnWidth(10, 250);
            //sheet.SetColumnWidth(12, 100);
            //加标题
            sheet.GetRow(5).GetCell(0).SetCellValue("序号");
            sheet.GetRow(5).GetCell(1).SetCellValue("物料编码");
            sheet.GetRow(5).GetCell(2).SetCellValue("品名 规格 型号");
            sheet.GetRow(5).GetCell(3).SetCellValue("单位");
            sheet.GetRow(5).GetCell(4).SetCellValue("库位");
            sheet.GetRow(5).GetCell(5).SetCellValue("需求数量");
            sheet.GetRow(5).GetCell(6).SetCellValue("实发数量");
            sheet.GetRow(5).GetCell(7).SetCellValue("备注");

            //明细起始行
            int rowIndex = 6;

            //ICellStyle style1 = hssfworkbook.CreateCellStyle();
            //style1.WrapText = true;
            ICellStyle style2 = hssfworkbook.CreateCellStyle();
            style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thick;



            foreach (DataRow row in SourceTable.Rows)
            {

                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);
                ICellStyle style = hssfworkbook.CreateCellStyle();
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;


                ICell cell = dataRow.CreateCell(0);
                cell.SetCellValue(row["wh_rowNum"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(1);
                cell.SetCellValue(row["wh_part"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(2);
                cell.SetCellValue(row["wh_desc1"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(3);
                cell.SetCellValue(row["wh_pt_um"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(4);
                cell.SetCellValue(row["wh_pt_loc"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(5);
                cell.SetCellValue(row["wh_demandQty"].ToString());
                cell.CellStyle = style;


                cell = dataRow.CreateCell(6);
                cell.SetCellValue(row["wh_actualQty"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(7);
                cell.SetCellValue(row["wh_remark"].ToString());
                cell.CellStyle = style;

                rowIndex++;
            }

            //for (int i = sheet.LastRowNum; i > SourceTable.Rows.Count + 12; i--)
            //{
            //    sheet.ShiftRows(i, i + 1, -1);

            //}

            #endregion

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void createUNPrct(string stroutFile, string DepartmentID, string DepartmentName, string Site, string No, string ProjTypeID, string ProjTypeName, string Date, string Applyer, int uid)
        {
            System.Data.DataTable dt = GetUnplssAppInfos(No);//generationInquiry(IMID, out company, out createdDate, out createByName, uid);
            string excelname = System.DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
            string strFile = stroutFile;
            string filePath = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
            MemoryStream ms = UNPrctRenderDataTableToExcel(dt, filePath, uid, DepartmentID, DepartmentName, Site, No, ProjTypeID, ProjTypeName, Date, Applyer) as MemoryStream;
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            ms = null;
            fs = null;
        }

        public Stream UNPrctRenderDataTableToExcel(System.Data.DataTable SourceTable, string stroutFile, int uid, string DepartmentID, string DepartmentName, string Site, string No, string ProjTypeID, string ProjTypeName, string Date, string Applyer)
        {
            MemoryStream ms = new MemoryStream();
            FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/wh_exceunpiss.xls"), FileMode.Open, FileAccess.Read);
            NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");//workbook.CreateSheet();
            NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(17);//sheet.GetRow(0);
            file.Close();


            //输出头部信息
            #region 输出头部信息
            sheet.GetRow(0).GetCell(0).SetCellValue("计划外入库单申请");
            sheet.GetRow(1).GetCell(0).SetCellValue("上海强凌电子有限公司");
            sheet.GetRow(2).GetCell(0).SetCellValue("上海松江泗径镇望东南路139号");
            sheet.GetRow(3).GetCell(0).SetCellValue("领用部门：" + DepartmentName + "(" + DepartmentID + ")");
            sheet.GetRow(3).GetCell(3).SetCellValue("地点：" + Site);
            sheet.GetRow(3).GetCell(6).SetCellValue("单据号：" + No);
            sheet.GetRow(4).GetCell(0).SetCellValue("项目类型：" + ProjTypeName + "(" + ProjTypeID + ")");
            sheet.GetRow(4).GetCell(6).SetCellValue("入库日期：" + Date);

            //输出明细


            //设置10列宽为100
            sheet.SetColumnWidth(10, 250);
            //sheet.SetColumnWidth(12, 100);
            //加标题
            sheet.GetRow(5).GetCell(0).SetCellValue("序号");
            sheet.GetRow(5).GetCell(1).SetCellValue("物料编码");
            sheet.GetRow(5).GetCell(2).SetCellValue("品名 规格 型号");
            sheet.GetRow(5).GetCell(3).SetCellValue("单位");
            sheet.GetRow(5).GetCell(4).SetCellValue("库位");
            sheet.GetRow(5).GetCell(5).SetCellValue("应收数量");
            sheet.GetRow(5).GetCell(6).SetCellValue("实收数量");
            sheet.GetRow(5).GetCell(7).SetCellValue("备注");

            //明细起始行
            int rowIndex = 6;

            //ICellStyle style1 = hssfworkbook.CreateCellStyle();
            //style1.WrapText = true;
            ICellStyle style2 = hssfworkbook.CreateCellStyle();
            style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thick;



            foreach (DataRow row in SourceTable.Rows)
            {

                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);
                ICellStyle style = hssfworkbook.CreateCellStyle();
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;


                ICell cell = dataRow.CreateCell(0);
                cell.SetCellValue(row["wh_rowNum"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(1);
                cell.SetCellValue(row["wh_part"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(2);
                cell.SetCellValue(row["wh_desc1"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(3);
                cell.SetCellValue(row["wh_pt_um"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(4);
                cell.SetCellValue(row["wh_pt_loc"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(5);
                cell.SetCellValue(row["wh_demandQty"].ToString());
                cell.CellStyle = style;


                cell = dataRow.CreateCell(6);
                cell.SetCellValue(row["wh_actualQty"].ToString());
                cell.CellStyle = style;

                cell = dataRow.CreateCell(7);
                cell.SetCellValue(row["wh_remark"].ToString());
                cell.CellStyle = style;

                rowIndex++;
            }

            //for (int i = sheet.LastRowNum; i > SourceTable.Rows.Count + 12; i--)
            //{
            //    sheet.ShiftRows(i, i + 1, -1);

            //}

            #endregion

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


        #endregion


        /// <summary>
        /// 计划外出入库获取明细
        /// </summary>
        public DataTable GetUnpISSInfo(string AppNo)
        {
            strSQL = "sp_wh_selectUNPISSUNcheckInfos";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }

        /// <summary>
        /// 计划外出入库获取明细
        /// </summary>
        public DataTable GetUNPUNAccessInfo(string AppNo)
        {
            strSQL = "sp_wh_selectUNPUNAccessInfos";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }

        /// <summary>
        /// 计划外出入库头栏
        /// </summary>
        public DataTable GetUnpMstrInfo(string AppNo)
        {
            strSQL = "sp_wh_selectUNPMstrInfos";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@AppNo", AppNo);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }

        #region

        /// <summary>
        /// 未处理记录
        /// </summary>
        public DataTable GetUnfinished(string Type, int plantcode)
        {
            strSQL = "sp_wh_selectunfinished";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@Type", Type);
            parm[1] = new SqlParameter("@plantcode", plantcode);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns>部门ID，部门名称</returns>
        public DataTable GetDepartment(int intSiteID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_Department";
                SqlParameter parm = new SqlParameter("@siteID", intSiteID);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        #endregion


        #region Wh_Compare

        /// <summary>
        /// 判断物料号是否存在更改未完成
        /// </summary>
        /// <returns></returns>
        public DataTable GetDate(String Nbr, string Lot, string stardate, string enddata, string domain, string type, int checktype)
        {
            string strSQL = "sp_wh_selectdatacompare";
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@nbr", Nbr);
            parm[1] = new SqlParameter("@lot", Lot);
            parm[2] = new SqlParameter("@stardate", stardate);
            parm[3] = new SqlParameter("@enddata", enddata);
            parm[4] = new SqlParameter("@domain", domain);
            parm[5] = new SqlParameter("@type", type);
            parm[6] = new SqlParameter("@checktype", Convert.ToBoolean(checktype));

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }
        /// <summary>
        ///  根据人删除CIMLOAD临时数据
        /// </summary>
        /// <param name="iApplyUserId">申请人id</param>
        /// <param name="strApplyUserName">申请人姓名</param>
        /// <param name="iApplyDeptId">申请人所在部门id</param>
        /// <param name="strApplyDeptName">申请人所在部门</param>
        /// <param name="strApplyReason">申请理由</param>
        /// <returns>新建权限申请的ID号</returns>
        //public static int newAccessApplyInfo(string Father_Bom, string Son_Bom, DateTime StartTime, DateTime EndTime, string Toson_Bom, DateTime TostartTime, DateTime ToendTime, string CreateBy, string ApplyReason)
        public int DeleteCimloadId(int uid, int PlantCode)
        {

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uid", uid);
            param[1] = new SqlParameter("@PlantCode", PlantCode);
            param[2] = new SqlParameter("@returnVar", SqlDbType.Int);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_wh_deleteCimloadId", param);
            return Convert.ToInt32(param[2].Value);
        }


        /// <summary>
        ///  获取CIMLOAD明细
        /// </summary>
        /// <param name="iApplyUserId">申请人id</param>
        /// <param name="strApplyUserName">申请人姓名</param>
        /// <param name="iApplyDeptId">申请人所在部门id</param>
        /// <param name="strApplyDeptName">申请人所在部门</param>
        /// <param name="strApplyReason">申请理由</param>
        /// <returns>新建权限申请的ID号</returns>
        //public static int newAccessApplyInfo(string Father_Bom, string Son_Bom, DateTime StartTime, DateTime EndTime, string Toson_Bom, DateTime TostartTime, DateTime ToendTime, string CreateBy, string ApplyReason)
        public int GetCimloadId(string domain, string nbr, string lot, string line, string loc, string part, string serial, string wh_type, int uid, int PlantCode)
        {

            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@wh_domain", domain);
            param[1] = new SqlParameter("@wh_nbr", nbr);
            param[2] = new SqlParameter("@wh_lot", lot);
            param[3] = new SqlParameter("@wh_line", line);
            param[4] = new SqlParameter("@wh_loc", loc);
            param[5] = new SqlParameter("@wh_part", part);
            param[6] = new SqlParameter("@wh_serial", serial);
            param[7] = new SqlParameter("@wh_type", wh_type);
            param[8] = new SqlParameter("@uid", uid);
            param[9] = new SqlParameter("@PlantCode", PlantCode);
            param[10] = new SqlParameter("@returnVar", SqlDbType.Int);
            param[10].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_wh_insertCimloadId", param);
            return Convert.ToInt32(param[10].Value);
        }


        public DataTable GetWhCimload(int uID, string ps_nbr, string ps_lot)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@ps_nbr", ps_nbr);
            param[2] = new SqlParameter("@ps_lot", ps_lot);

            DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Wh_cimload", param).Tables[0];
            return dt;
        }


        #endregion


    }

}