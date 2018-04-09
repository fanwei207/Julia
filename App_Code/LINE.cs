using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using CommClass;


namespace LINEPRO
{
    /// <summary>
    /// Summary description for LINE
    /// </summary>
    public class LINE
    {
        adamClass adam = new adamClass();
        String _strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

        public LINE()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region 采样点

        public DataSet SelectCostCenter(int plantid)
        {
            try
            {
                string strName = "sp_LN_SelectCostCenter";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@plantid", plantid);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectSamplesByLines(int plantid, int lines)
        {
            try
            {
                string strName = "sp_LN_SelectSamplesByLines";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@lines", lines);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectSamples(int plantid, string name)
        {
            try
            {
                string strName = "sp_LN_SelectSamples";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@name", name);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public int InsertSamples(int plantid, string name,bool last, int uID)
        {
            try
            {
                string strName = "sp_LN_InsertSamples";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@name", name);
                param[2] = new SqlParameter("@last", last);
                param[3] = new SqlParameter("@uID", uID);
                param[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[4].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public int UpdateSamples(int plantid, int id, string name, bool last, int uID)
        {
            try
            {
                string strName = "sp_LN_UpdateSamples";
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@name", name);
                param[3] = new SqlParameter("@last", last);
                param[4] = new SqlParameter("@uID", uID);
                param[5] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[5].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteSamples(int plantid, int id)
        {
            try
            {
                string strName = "sp_LN_DeleteSamples";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@id", id);

                int nRet = SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 原因

        public DataSet SelectReasons(int plantid, string name)
        {
            try
            {
                string strName = "sp_LN_SelectReasons";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@name", name);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public int InsertReasons(int plantid, string name, int uID)
        {
            try
            {
                string strName = "sp_LN_InsertReasons";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@name", name);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[3].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public int UpdateReasons(int plantid, int id, string name, int uID)
        {
            try
            {
                string strName = "sp_LN_UpdateReasons";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@name", name);
                param[3] = new SqlParameter("@uID", uID);
                param[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[4].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteReasons(int plantid, int id)
        {
            try
            {
                string strName = "sp_LN_DeleteReasons";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@id", id);

                int nRet = SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 采样点和原因的关系

        public DataSet SelectSamplesRelations(int plantid, int sID,int lID)
        {
            try
            {
                string strName = "sp_LN_SelectSamplesRelations";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@sID", sID);
                param[2] = new SqlParameter("@lID", lID);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateSamplesRelations(int plantid, int sID,int lID, string insert, string delete,string sortid,string sort, int uID)
        {
            try
            {
                string strName = "sp_LN_UpdateSamplesRelations";
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@sID", sID);
                param[2] = new SqlParameter("@lID", lID);
                param[3] = new SqlParameter("@insert", insert);
                param[4] = new SqlParameter("@delete", delete);
                param[5] = new SqlParameter("@sortid", sortid);
                param[6] = new SqlParameter("@sort", sort);
                param[7] = new SqlParameter("@uID", uID);

                int nRet = SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 生产线和采样点的关系

        public DataSet SelectLines(int plantid, int cc)
        {
            try
            {
                string strName = "sp_LN_SelectLines";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@cc", cc);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectLinesRelations(int plantid, int ID)
        {
            try
            {
                string strName = "sp_LN_SelectLinesRelations";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@ID", ID);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateLinesRelations(int plantid, int lID, string insert, string delete, int uID)
        {
            try
            {
                string strName = "sp_LN_UpdateLinesRelations";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@plantid", plantid);
                param[1] = new SqlParameter("@lID", lID);
                param[2] = new SqlParameter("@insert", insert);
                param[3] = new SqlParameter("@delete", delete);
                param[4] = new SqlParameter("@uID", uID);

                int nRet = SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 流水线
        public static DataSet getAccessCC(string plantCode)
        {
            SqlParameter param = new SqlParameter("@plantCode", plantCode);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_line_getAccessCC", param);
        }

        public static DataSet getCCList(string plantCode,string ccid,string ccName,string lnName)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@plantCode", plantCode);
            param[1] = new SqlParameter("@ccid", ccid);
            param[2] = new SqlParameter("@ccname", ccName);
            param[3] = new SqlParameter("@lnName", lnName);
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_line_getCCList", param);
        }

        public static string insertLine(string ccId,string ccName,string lnName,string plantCode)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ls_line_cc", ccId);
            param[1] = new SqlParameter("@ls_line_ccname", ccName);
            param[2] = new SqlParameter("@ls_line_name", lnName);
            param[3] = new SqlParameter("@ls_line_plant", plantCode);

           return Convert.ToString(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_line_insertLine", param));

        }

        public static void deleteLine(string lineid,string plantCode)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@lineid", lineid);
            param[1] = new SqlParameter("@plantCode", plantCode);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_line_deleteLine", param);
        }

        public static DataSet getOneLine(string lineid,string plantCode)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@lineid", lineid);
            param[1] = new SqlParameter("@plantCode", plantCode);
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_line_getOneLine", param);
        }

        public static string updateLine(string ccId, string ccName, string lnName, string plantCode,string lineid)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@lineid", lineid);
            param[1] = new SqlParameter("@ccid", ccId);
            param[2] = new SqlParameter("@ccname", ccName);
            param[3] = new SqlParameter("@lnname", lnName);
            param[4] = new SqlParameter("@ls_line_plant", plantCode);
            return Convert.ToString(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_line_updateLine", param));

        }

        #endregion

    }
}
