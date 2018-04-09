using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Contracts;
using Microsoft.ApplicationBlocks.Data;
using System.Runtime.InteropServices;
using ASPSimply;

namespace Services
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProjectService : IProjectService
    {
        private readonly string strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];

        public Tuple<string, string, string, string> GetHeader(string name, string code)
        {
            Tuple<string, string, string, string> result = new Tuple<string, string, string, string>("", "", "", "");
            try
            {
                string strSql = "sp_RDW_SelectRdwHeaderByCS";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@ProdCode", code);
                sqlParam[1] = new SqlParameter("@Project", name);

                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                if (reader.Read())
                {
                    string desc = reader["ProdDesc"].ToString();
                    string startDate = string.Format("{0:yyyy-MM-dd}", reader["StartDate"]);
                    string endDate = string.Format("{0:yyyy-MM-dd}", reader["EndDate"]);
                    string id = reader["ID"].ToString();
                    result = new Tuple<string, string, string, string>(desc, startDate, endDate, id);
                }
                reader.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public DataTable GetSteps(string project, string prodcode)
        {

            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@project", project);
                param[1] = new SqlParameter("@prodcode", prodcode);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_RDW_SelectRdwStepTypeByCS", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetStepById(string id)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDetailEditByCS";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@stepid", id);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SelectRDWDetailDocs(string strDID,string strUid)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDetailDocsbyCS";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUid);
                DataSet dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool UploadFile(string fileName, string newFileName,string uID, string uName, string stepID)
        {
            try
            {
                string strSql = "sp_RDW_UploadFileByCS";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@fileName", fileName);
                sqlParam[1] = new SqlParameter("@newFileName", newFileName);
                sqlParam[2] = new SqlParameter("@uID", uID);
                sqlParam[3] = new SqlParameter("@uName", uName);
                sqlParam[4] = new SqlParameter("@stepID", stepID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteFile(string strDocID)
        {
            try
            {
                string strSql = "sp_RDW_DeleteRdwDetailDocs";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@docid", strDocID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary> 
        /// 判断是否可以完成明细 
        /// </summary> 
        /// <param name="strDID"></param> 
        /// <param name="strUID"></param> 
        /// <returns></returns> 
        public bool CheckFinishRDWDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_FinishRDWDetailCheckByCS";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex; 
                return false;
            }
        }

    }

    public class BaseInfo : IBaseInfo
    {
        public string GetFtpDocumentServerAddress()
        {
            return ConfigurationSettings.AppSettings["FtpDocServerAddress"];
        }

        public string GetFtpDocumentServerFtpUserName()
        {
            return ConfigurationSettings.AppSettings["FtpDocServerUserName"];
        }

        public string GetFtpDocumentServerPassword()
        {
            return ConfigurationSettings.AppSettings["FtpDocServerPassword"];
        }
    }

    public class LoginService : ILoginService
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public string EncryptPWD(string strInput, [Optional, DefaultParameterValue("www.fengxinsoftware.com")] string sKey)
        {
            CryptoASPX oaspx = new CryptoASPX();
            CryptoASPX oaspx2 = oaspx;
            oaspx2.CryptoType = "DES";
            oaspx2.Key = sKey;
            oaspx2.SourceString = strInput;
            oaspx2.ConvertHEX = true;
            string str2 = oaspx2.EncryptString();
            oaspx2.SourceString = str2;
            oaspx2 = null;
            oaspx = null;
            return str2;
        }

        /// <summary>
        /// 登陆验证1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        public DataSet Login(string loginName, string userPwd, string domain = "szx")
        {
            String Conn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@loginName", loginName);
                param[1] = new SqlParameter("@userPwd", userPwd);
                param[2] = new SqlParameter("@domain", domain);

                DataSet dt = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, "tcpc0.dbo.sp_bcs_loginUser", param);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetCompanies()
        {
            String Conn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

            try
            {
                DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, "BarCodeSys.dbo.sp_selectCompanies").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
