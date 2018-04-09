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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Stocking
/// </summary>
/// 
namespace Stockingbase
{
    public class Stocking
    {
        adamClass adam = new adamClass();
        public Stocking()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool InsertBatchTemp(string uID, string uName)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@uID", uID);
                sqlParam[1] = new SqlParameter("@uName", uName);
                sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
                sqlParam[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_SK_insertStockingSubmit", sqlParam);

                return Convert.ToBoolean(sqlParam[2].Value);
            }
            catch
            {
                return false;
            }
        }
        public bool InsertBatchTemporder(string uID, string uName)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@uID", uID);
                sqlParam[1] = new SqlParameter("@uName", uName);
                sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
                sqlParam[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_SK_insertStockingSubmitorder", sqlParam);

                return Convert.ToBoolean(sqlParam[2].Value);
            }
            catch
            {
                return false;
            }
        }
        public bool submitstocking(string nbr, string createby)
        {
            try
            {
                string strSql = "sp_SK_StockingSubmit";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@nbr", nbr);
                sqlParam[1] = new SqlParameter("@Uid", createby);
                SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        public bool submitstockingcancel(string nbr, string createby,string remark)
        {
            try
            {
                string strSql = "sp_SK_StockingSubmitcancel";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@nbr", nbr);
                sqlParam[1] = new SqlParameter("@Uid", createby);
                sqlParam[2] = new SqlParameter("@remark", remark);
                SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        public DataTable SelectStockingSubmit(string strnbr, string strvent, string strpart, string strQAD, string status)
        {
            try
            {
                string strSql = "sp_SK_selectStocking";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@nbr", strnbr);
                sqlParam[1] = new SqlParameter("@vent", strvent);
                sqlParam[2] = new SqlParameter("@part", strpart);
                sqlParam[3] = new SqlParameter("@QAD", strQAD);
                sqlParam[4] = new SqlParameter("@status", status);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];


            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public DataTable SelectStockingSubmitorder(string strnbr, string strvent, string strpart, string strQAD, string status)
        {
            try
            {
                string strSql = "sp_SK_selectStockingorder";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@nbr", strnbr);
                sqlParam[1] = new SqlParameter("@vent", strvent);
                sqlParam[2] = new SqlParameter("@part", strpart);
                sqlParam[3] = new SqlParameter("@QAD", strQAD);
                sqlParam[4] = new SqlParameter("@status", status);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];


            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        public DataTable SelectStockingdelay(string strnbr, string strvent, string strpart, string strQAD, string status)
        {
            try
            {
                string strSql = "sp_SK_selectStockingdelay";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@nbr", strnbr);
                sqlParam[1] = new SqlParameter("@vent", strvent);
                sqlParam[2] = new SqlParameter("@part", strpart);
                sqlParam[3] = new SqlParameter("@QAD", strQAD);
                sqlParam[4] = new SqlParameter("@status", status);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];


            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        public DataTable SelectStockingSubmitorderexcel(string strnbr, string strvent, string strpart, string strQAD, string status,string uid)
        {
            try
            {
                string strSql = "sp_SK_selectStockingorderexcel";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@nbr", strnbr);
                sqlParam[1] = new SqlParameter("@vent", strvent);
                sqlParam[2] = new SqlParameter("@part", strpart);
                sqlParam[3] = new SqlParameter("@QAD", strQAD);
                sqlParam[4] = new SqlParameter("@status", status);
                sqlParam[5] = new SqlParameter("@uid", uid);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];


            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        public bool ClearTemp(int uID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@uID", uID);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_SK_clearStockingSubmitTemp", param);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool updatestocking(string nbr, string txtponbr, string txtpolinr)
        {
            try
            {
                string strSql = "sp_SK_updateStockingPonbr";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@nbr", nbr);
                sqlParam[1] = new SqlParameter("@ponbr", txtponbr);
                sqlParam[2] = new SqlParameter("@poline", txtpolinr);
                SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public bool updatestockingorder(string nbr, string txtponbr, string txtpolinr)
        {
            try
            {
                string strSql = "sp_SK_updateStockingordernbr";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@nbr", nbr);
                sqlParam[1] = new SqlParameter("@ponbr", txtponbr);
                sqlParam[2] = new SqlParameter("@poline", txtpolinr);
                SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        public DataTable SelectStockingSubmitcaigou(string strnbr, string strvent, string strpart, string strQAD, string status)
        {
            try
            {
                string strSql = "sp_SK_selectStockingcaigou";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@nbr", strnbr);
                sqlParam[1] = new SqlParameter("@vent", strvent);
                sqlParam[2] = new SqlParameter("@part", strpart);
                sqlParam[3] = new SqlParameter("@QAD", strQAD);
                sqlParam[4] = new SqlParameter("@status", status);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];


            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
    }
}