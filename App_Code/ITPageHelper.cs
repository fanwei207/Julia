using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Collections;

namespace IT
{
    public class PageMakerHelper : System.Web.UI.Page
    {
        static string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];

        /// <summary>
        /// 隐藏构造方法.
        /// </summary>
        public PageMakerHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// 获取Page Mastr主记录
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static DataTable GetPageMstr(string pageID)
        {
            try
            {
                string strName = "sp_page_selectPageMstr";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取字段的明细配置
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static DataTable GetPageDet(string pageID, string colName)
        {
            try
            {
                string strName = "sp_page_selectPageDet";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@pageID", pageID);
                param[1] = new SqlParameter("@colName", colName);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetPageDet(string pageID)
        {
            try
            {
                string strName = "sp_page_getPageDet";
                SqlParameter param = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取查询字段（最多只支持10个查询条件）
        /// </summary>
        public static DataTable GetQueryFields(string pageID)
        {
            try
            {
                string strName = "sp_page_selectQueryFields";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 明细表头栏处显示字段
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static DataTable GetHeaderFields(string pageID)
        {
            try
            {
                string strName = "sp_page_selectHeaderFields";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取导出字段
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExportFields(string pageID)
        {
            try
            {
                string strName = "sp_page_selectExportFields";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取关键字段（最多只支持5个关键字组合）
        /// </summary>
        public static string[] GetPrimaryKeyFields(string pageID)
        {
            ArrayList arrList = new ArrayList();
            try
            {
                string strName = "sp_page_selectPrimaryKeyFields";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];

                if (table != null || table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        arrList.Add(row["name"]);
                    }
                }
            }
            catch
            {
                ;
            }
            return arrList.ToArray(typeof(string)) as string[];
        }
        /// <summary>
        /// 获取外键列表
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static DataTable GetForeignKeyFields(string parentID, string referID)
        {
            try
            {
                string strName = "sp_page_selectForeignKeyFields";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@parentID", parentID);
                param[1] = new SqlParameter("@referID", referID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取字段格式
        /// </summary>
        /// <returns></returns>
        public static string GetFieldData(string pageID, string field, string colName)
        {
            try
            {
                string strName = "sp_page_selectFieldData";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@pageID", pageID);
                param[1] = new SqlParameter("@field", field);
                param[2] = new SqlParameter("@colName", colName);

                return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strName, param).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取新增字段
        /// </summary>
        public static DataTable GetAddableFields(string pageID)
        {
            try
            {
                string strName = "sp_page_selectAddableFields";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取待生成文件列表
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static DataTable GetFileList(string pageID)
        {
            try
            {
                string strName = "sp_page_selectFileList";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取可导入字段信息
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public static DataTable GetImportField(string pageid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageid", pageid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_page_selectImportFields", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static bool ClearImportTemp(string pageid,string uid)
        {
            SqlParameter[] pram = new SqlParameter[2];
            pram[0] = new SqlParameter("@pageid", pageid);
            pram[1] = new SqlParameter("@uid", uid);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_page_clearImportTemp", pram));
        }
        /// <summary>
        /// 验证Excel表中的数据是否正确
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public static bool ExcelValidate(string pageid,string uid)
        {
            SqlParameter []param = new SqlParameter[2];
            param[0] = new SqlParameter("@pageID", pageid);
            param[1] = new SqlParameter("@uid", uid);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_page_excelValidate", param));
        }
        /// <summary>
        /// 将Excel中正确的数据的导入到目标表中
        /// </summary>
        /// <param name="pageid"></param>
        /// <param name="uid"></param>
        public static bool ExcelToTable(string pageid,string uid)
        { 
            SqlParameter [] param = new SqlParameter[3];
            param[0] = new SqlParameter("@pageid",pageid);
            param[1] = new SqlParameter("@uid", uid);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_page_insertExToTable", param);

            return Convert.ToBoolean(param[2].Value);
        }
        /// <summary>
        /// 调用用户自定义存储过程将Excel中正确的数据的导入到目标表中
        /// </summary>
        /// <param name="pageid"></param>
        /// <param name="uid"></param>
        public static bool ExcelToTableBySelf(string pageid, string uid,string importProc)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@pageid", pageid);
            param[1] = new SqlParameter("@uid", uid);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, importProc, param);
            return Convert.ToBoolean(param[2].Value);
        }
        /// <summary>
        /// 获取有错误记录的Excel数据
        /// </summary>
        /// <param name="pageid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable GetExcelError(string pageid,string uid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@pageid", pageid);
                param[1] = new SqlParameter("@uid", uid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_page_GetExcelError", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 配置表的详细信息
        /// </summary>
        public static bool ConfigurePageDet(string db,string table,string uID,string uName)
        {
            string str = "sp_page_configurePageDet";

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@db",db);
            param[1] = new SqlParameter("@table",table);
            param[2] = new SqlParameter("@uID",uID);
            param[3] = new SqlParameter("@uName",uName);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,str,param));
        }
        /// <summary>
        /// 判断Det表中是否存在记录
        /// </summary>
        public static bool ExistsTableDet(string db,string table)
        {
            string str = "sp_page_existsTableDet";

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@db",db);
            param[1] = new SqlParameter("@table",table);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,str,param));
        }

        public static DataTable GetTableColDet(string db, string table,string status)
        {
            string str = "sp_page_getTableColDet";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@db", db);
            param[1] = new SqlParameter("@table", table);
            param[2] = new SqlParameter("@status", status);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, str, param).Tables[0];
        }
        public static DataSet ConfigTableColDet(string db, string table, string status)
        {
            string str = "sp_page_getTableColDet";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@db", db);
            param[1] = new SqlParameter("@table", table);
            param[2] = new SqlParameter("@status", status);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, str, param);
        }

        public static DataTable GetEditableFields(string pageID)
        {
            try
            {
                string strName = "sp_page_selectEditableFields";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@pageID", pageID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static string GetPageID(string db,string table)
        { 
            string str = "sp_page_getPageID";

            SqlParameter []param = new SqlParameter[2];
            param[0] = new SqlParameter("@db",db);
            param[1] = new SqlParameter("@table",table);

            return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
        }
        public static bool UpdatePageDet(string db, string table)
        {
            string str = "sp_page_UpdatePageDet";

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@db",db);
            param[1] = new SqlParameter("@table", table);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,str,param));
        }
        public static bool DeletePageDetTemp(string db, string table, string uid)
        {
            string str = "sp_page_deletePageDetTemp";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@db",db);
            param[1] = new SqlParameter("@table",table);
            param[2] = new SqlParameter("@uid", uid);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,str,param));
        }
        public static void saveDetProc(string pageID, string saveProc, string validateProc, string delProc, string importProc, string editProc, string uID, string uName)
        {
            string str = "sp_page_saveDetProc";

            SqlParameter []param = new SqlParameter[8];
            param[0] = new SqlParameter("@pageID",pageID);
            param[1] = new SqlParameter("@saveProc",saveProc);
            param[2] = new SqlParameter("@validateProc", validateProc);
            param[3] = new SqlParameter("@delProc", delProc);
            param[4] = new SqlParameter("@importProc", importProc);
            param[5] = new SqlParameter("@editProc", editProc);
            param[6] = new SqlParameter("@uID", uID);
            param[7] = new SqlParameter("@uName", uName);

            SqlHelper.ExecuteNonQuery(strConn,CommandType.StoredProcedure,str,param);
        }
        /// <summary>
        /// 转换默认值
        /// </summary>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public string SwitchDefaultValue(string defaultVal)
        {
            string _val = defaultVal.ToLower();
            switch (_val)
            {
                case "{uid}": _val = Session["uID"].ToString(); break;
                case "{uname}": _val = Session["uName"].ToString(); break;
                case "{plantcode}": _val = Session["PlantCode"].ToString(); break;
                case "{today}": _val = DateTime.Now.ToString("yyyy-MM-dd"); break;
                case "{prevweek}": _val = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); break;
                case "{nextweek}": _val = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"); break;
                case "{prevmonth}": _val = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"); break;
                case "{nextmonth}": _val = DateTime.Now.AddMonths(1).ToString("yyyy-MM-01"); break;
                case "{pk0}": _val = System.Web.HttpContext.Current.Request["pk0"]; break;
                case "{pk1}": _val = System.Web.HttpContext.Current.Request["pk1"]; break;
                case "{pk2}": _val = System.Web.HttpContext.Current.Request["pk2"]; break;
                case "{pk3}": _val = System.Web.HttpContext.Current.Request["pk3"]; break;
                case "{pk4}": _val = System.Web.HttpContext.Current.Request["pk4"]; break;
                case "{fk0}": _val = System.Web.HttpContext.Current.Request["fk0"]; break;
                case "{fk1}": _val = System.Web.HttpContext.Current.Request["fk1"]; break;
                case "{fk2}": _val = System.Web.HttpContext.Current.Request["fk2"]; break;
                case "{fk3}": _val = System.Web.HttpContext.Current.Request["fk3"]; break;
                case "{fk4}": _val = System.Web.HttpContext.Current.Request["fk4"]; break;
                default: _val = ""; break;
            }
            return _val;
        }
    }
}