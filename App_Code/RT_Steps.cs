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

namespace RT_Workflow
{
    /// <summary>
    /// Summary description for RT_Steps
    /// </summary>
    public class RT_Steps
    {
        adamClass adam = new adamClass();
        String _strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_RDW"];
        public RT_Steps()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Steps Maintenance

        public DataSet SelectSteps(int mid)
        {
            try
            {
                string strName = "sp_RT_SelectSteps";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@mid", mid);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectDetSteps(int mid, int uID)
        {
            try
            {
                string strName = "sp_RT_SelectDetSteps";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public int InsertSteps(int mid, string step, string missions, string stddate, string enddate, int uID)
        {
            try
            {
                string strName = "sp_RT_InsertSteps";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@step", step);
                param[2] = new SqlParameter("@missions", missions);
                param[3] = new SqlParameter("@stddate", stddate);
                param[4] = new SqlParameter("@enddate", enddate);
                param[5] = new SqlParameter("@uID", uID);
                param[6] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[6].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public bool InsertDetSteps(int mid, int sid, int uID)
        {
            try
            {
                string strName = "sp_RT_InsertDetSteps";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@sid", sid);
                param[2] = new SqlParameter("@uID", uID);

                int nRet = SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                if (nRet < 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public int UpdateSteps(int mid, int id, string step, string missions, string stddate, string enddate, int uID)
        {
            try
            {
                string strName = "sp_RT_UpdateSteps";
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@step", step);
                param[3] = new SqlParameter("@missions", missions);
                param[4] = new SqlParameter("@stddate", stddate);
                param[5] = new SqlParameter("@enddate", enddate);
                param[6] = new SqlParameter("@uID", uID);
                param[7] = new SqlParameter("@returnVal", SqlDbType.Int);
                param[7].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(_strConn, CommandType.StoredProcedure, strName, param);

                return int.Parse(param[7].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteSteps(int mid, int id)
        {
            try
            {
                string strName = "sp_RT_DeleteSteps";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mid", mid);
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

        public bool StepsUp(int mid, string org, string up)
        {
            try
            {
                string strName = "sp_RT_StepsUp";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@org", org);
                param[2] = new SqlParameter("@up", up);

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

        public bool StepsDown(int mid, string org, string down)
        {
            try
            {
                string strName = "sp_RT_StepsDown";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@org", org);
                param[2] = new SqlParameter("@down", down);

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

        public DataSet SelectReviewers(int plant, int dept, int org, int id, int type)
        {
            try
            {
                string strName = "sp_RT_SelectReviewers";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@plant", plant);
                param[1] = new SqlParameter("@dept", dept);
                param[2] = new SqlParameter("@org", org);
                param[3] = new SqlParameter("@id", id);
                param[4] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public DataSet SelectReviewersAll(int plant, int id, int type)
        {
            try
            {
                string strName = "sp_RT_SelectReviewersAll";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@plant", plant);
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(_strConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateReviewers(int id, int type, string reviewers, string reviewernames, int uID)
        {
            try
            {
                string strName = "sp_RT_UpdateReviewers";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@type", type);
                param[2] = new SqlParameter("@reviewers", reviewers);
                param[3] = new SqlParameter("@reviewernames", reviewernames);
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

        public bool DeleteReviewers(int id, int type, int uID)
        {
            try
            {
                string strName = "sp_RT_DeleteReviewers";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@type", type);
                param[2] = new SqlParameter("@uID", uID);

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
    }
}
