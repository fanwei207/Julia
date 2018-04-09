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


/// <summary>
/// Summary description for MF
/// </summary>
namespace MF
{
    public class MFHelper
    {
        static string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
        public MFHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// 创建新的FM主题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Decription"></param>
        /// <param name="Authorize"></param>
        /// <param name="KeyWords"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool insertMFmstr(string title, string Decription, string Authorize, string KeyWords, string uID, string uName)
        {

            try
            {
                string strName = "sp_MF_insertMFMstr";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@title", title);
                param[1] = new SqlParameter("@Decription", Decription);
                param[2] = new SqlParameter("@Authorize", Authorize);
                param[3] = new SqlParameter("@KeyWords", KeyWords);
                param[4] = new SqlParameter("@uID", uID);
                param[5] = new SqlParameter("@uName", uName);
                param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[6].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Keywords"></param>
        /// <returns></returns>
        public static DataTable selectMFmstr(string status, string Keywords)
        {
            try
            {
                string strName = "sp_MF_selectMFmstr";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@status", status);
                param[1] = new SqlParameter("@Keywords", Keywords);


                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];

             
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable selectMFdet(string parenid, string status, string uID,string admin )
        {
            try
            {
                string strName = "sp_MF_selectMFdet";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@parenid", parenid);
                param[1] = new SqlParameter("@status", status);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@admin", admin);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool setvisit(string id, string uID, string uName)
        {
            try
            {
                string strName = "sp_MF_updateMFmstrVisit";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool saveMFdet(string id, string uID, string uName)
        {
            try
            {
                string strName = "sp_MF_updateMFdetsave";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool insertMFdet(string parentid, string title, string depart, string Decription, string uID, string uName)
        {

            try
            {
                string strName = "sp_MF_insertMFdet";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@parentid", parentid);
                param[1] = new SqlParameter("@title", title);
                param[2] = new SqlParameter("@depart", depart);
                param[3] = new SqlParameter("@Decription", Decription);
                param[4] = new SqlParameter("@uID", uID);
                param[5] = new SqlParameter("@uName", uName);
                param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[6].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static SqlDataReader selectMFmstrone(string id)
        {
            try
            {
                string strName = "sp_MF_selectMFmstr";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);
               


                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static SqlDataReader selectMFdetone(string id)
        {
            try
            {
                string strName = "sp_MF_selectMFdetone";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);



                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool updateMFdet(string parentid, string title, string depart, string Decription, string uID, string uName, string step)
        {

            try
            {
                string strName = "sp_MF_updateMFdet";
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@parentid", parentid.Trim());
                param[1] = new SqlParameter("@title", title);
                param[2] = new SqlParameter("@depart", depart);
                param[3] = new SqlParameter("@Decription", Decription);
                param[4] = new SqlParameter("@uID", uID);
                param[5] = new SqlParameter("@uName", uName);
                param[6] = new SqlParameter("@step", step);
                param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[7].Direction = ParameterDirection.Output;
             
                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[7].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable selectMFdetReview(string parenid, string id)
        {
            try
            {
                string strName = "sp_MF_selectMFdetReview";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@parenid", parenid);
                param[1] = new SqlParameter("@id", id);
              

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];


            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}