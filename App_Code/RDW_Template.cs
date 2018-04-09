using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

/// <summary>
/// Summary description for RDW_Template
/// </summary>
public class RDW_Template
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
	public RDW_Template()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet SelectReviewersAll(int plant, int id, int type)
    {
        try
        {
            string strName = "sp_RDW_SelectReviewersAll";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@plant", plant);
            param[1] = new SqlParameter("@id", id);
            param[2] = new SqlParameter("@type", type);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }

        public bool DeleteReviewers(int id, int type, int uID)
        {
            try
            {
                string strName = "sp_RDW_DeleteReviewers";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@type", type);
                param[2] = new SqlParameter("@uID", uID);

                int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

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
        public bool UpdateReviewers(int id, int type, string reviewers, string reviewernames, int uID)
        {
            //try
            //{
            string strName = "sp_RDW_UpdateReviewers";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@type", type);
            param[2] = new SqlParameter("@reviewers", reviewers);
            param[3] = new SqlParameter("@reviewernames", reviewernames);
            param[4] = new SqlParameter("@uID", uID);

            int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (nRet > -1)
                return true;
            else
                return false;
        }
}