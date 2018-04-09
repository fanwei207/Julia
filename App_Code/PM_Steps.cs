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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PM
{
    /// <summary>
    /// Summary description for RD_WorkFlow
    /// </summary>
    public class PM_Steps
    {
        adamClass adam = new adamClass();
        String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_PM"];

        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public PM_Steps()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Steps Maintenance
        /// <summary>
        /// ��ò���
        /// </summary>
        /// <param name="mid">PM_MstrID</param>
        /// <returns>DataSet</returns>
        public DataSet SelectSteps(long mid)
        {
            try
            {
                string strSql = "sp_PM_SelectSteps";
                SqlParameter param = new SqlParameter("@mid", mid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// �����ϸ����
        /// </summary>
        /// <param name="mid">PM_MstrID</param>
        /// <param name="uID">Session["uID"]</param>
        /// <returns>DataSet</returns>
        public DataSet SelectDetSteps(long mid, long uID)
        {
            try
            {
                string strSql = "sp_PM_SelectDetSteps";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���Ӳ���
        /// </summary>
        /// <param name="mid">PM_MstrID</param>
        /// <param name="strName">������</param>
        /// <param name="strDesc">����˵��</param>
        /// <param name="strStart">��ʼ����</param>
        /// <param name="strEnd">��������</param>
        /// <param name="uID">Session["uID"]</param>
        /// <returns></returns>
        public int InsertSteps(long mid, string strName, string strDesc, string strStart, string strEnd, long uID)
        {
            try
            {
                string strSql = "sp_PM_InsertSteps";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@name", strName);
                param[2] = new SqlParameter("@desc", strDesc);
                param[3] = new SqlParameter("@start", strStart);
                param[4] = new SqlParameter("@end", strEnd);
                param[5] = new SqlParameter("@uID", uID);
                param[6] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

                return Convert.ToInt32(param[6].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// ������ϸ����
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="did"></param>
        /// <param name="uID"></param>
        /// <returns>True/False</returns>
        public bool InsertDetSteps(long mid, long sid, long uID)
        {
            try
            {
                string strSql = "sp_PM_InsertDetSteps";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@sid", sid);
                param[2] = new SqlParameter("@uid", uID);

                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));

                if (nRet < 0) return false;
                else return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ���²���
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="id">����ID</param>
        /// <param name="strName">��������</param>
        /// <param name="strDesc">����˵��</param>
        /// <param name="strStart">��ʼ����</param>
        /// <param name="strEnd">��������</param>
        /// <param name="uID">Session["uID"]</param>
        /// <returns></returns>
        public int UpdateSteps(long mid, long id, string strName, string strDesc, string strStart, string strEnd, long uID)
        {
            try
            {
                string strSql = "sp_PM_UpdateSteps";
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@name", strName);
                param[3] = new SqlParameter("@desc", strDesc);
                param[4] = new SqlParameter("@start", strStart);
                param[5] = new SqlParameter("@end", strEnd);
                param[6] = new SqlParameter("@uID", uID);
                param[7] = new SqlParameter("@ret", SqlDbType.Int);
                param[7].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

                return Convert.ToInt32(param[7].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="id">����ID</param>
        /// <returns>True/False</returns>
        public bool DeleteSteps(long mid, long id)
        {
            try
            {
                string strSql = "sp_PM_DeleteSteps";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@id", id);

                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));

                if (nRet == 0) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="org"></param>
        /// <param name="up"></param>
        /// <returns>True/False</returns>
        public bool StepsUp(long mid, string org, string up)
        {
            try
            {
                string strSql = "sp_PM_StepsUp";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@org", org);
                param[2] = new SqlParameter("@up", up);

                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));

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

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="org"></param>
        /// <param name="down"></param>
        /// <returns>True/False</returns>
        public bool StepsDown(long mid, string org, string down)
        {
            try
            {
                string strSql = "sp_PM_StepsDown";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@org", org);
                param[2] = new SqlParameter("@down", down);

                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));

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

        /// <summary>
        /// �����Ա
        /// </summary>
        /// <param name="plant">������˾</param>
        /// <param name="dept">��������</param>
        /// <param name="org">������֯</param>
        /// <param name="id"></param>
        /// <param name="type">����</param>
        /// <returns>DataSet</returns>
        public DataSet SelectReviewers(int plant, int dept, int org, long id, int type) 
        {
            try
            {
                string strSql = "sp_PM_SelectReviewers";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@plant", plant);
                param[1] = new SqlParameter("@dept", dept);
                param[2] = new SqlParameter("@org", org);
                param[3] = new SqlParameter("@id", id);
                param[4] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���������Ա
        /// </summary>
        /// <param name="plant">������˾</param>
        /// <param name="id"></param>
        /// <param name="type">����</param>
        /// <returns>DataSet</returns>
        public DataSet SelectReviewersAll(int plant, long id, int type)
        {
            try
            {
                string strSql = "sp_PM_SelectReviewersAll";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@plant", plant);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ������Ա
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">����</param>
        /// <param name="reviewers">��ԱID</param>
        /// <param name="reviewernames">��Ա����</param>
        /// <param name="uID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool UpdateReviewers(long id, int type, string reviewers, string reviewernames, long uID)
        {
            try
            {
                string strSql = "sp_PM_UpdateReviewers";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@type", type);
                param[2] = new SqlParameter("@reviewers", reviewers);
                param[3] = new SqlParameter("@reviewernames", reviewernames);
                param[4] = new SqlParameter("@uID", uID);

                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));

                if (nRet == 0) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �����Ա
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">����</param>
        /// <param name="uID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool DeleteReviewers(long id, int type, long uID)
        {
            try
            {
                string strSql = "sp_PM_DeleteReviewers";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@type", type);
                param[2] = new SqlParameter("@uID", uID);

                int nRet = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));

                if (nRet == 0) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}