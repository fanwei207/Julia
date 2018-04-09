using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;


namespace wo
{
    /// <summary>
    /// Summary description for Wod
    /// </summary>
    public class WodPartLack
    {
        private string connStr = ConfigurationManager.AppSettings["SqlConn.QAD_DATA"];
        public WodPartLack()
        {
            
        }

        public DataTable GetPartLack(string nbr, string dateFrom, string dateTo, string qad, string lack, string site, string domain)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@fromDate", dateFrom);
            param[2] = new SqlParameter("@toDate", dateTo);
            param[3] = new SqlParameter("@qad", qad);
            param[4] = new SqlParameter("@lack", lack);
            param[5] = new SqlParameter("@site", site);
            param[6] = new SqlParameter("@domain", domain);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_wod_selectPartLack", param).Tables[0];
        }

        public DataTable GetPartLackReport(string nbr, string dateFrom, string dateTo, string qad, string lack, string site, string domain)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@fromDate", dateFrom);
            param[2] = new SqlParameter("@toDate", dateTo);
            param[3] = new SqlParameter("@qad", qad);
            param[4] = new SqlParameter("@lack", lack);
            param[5] = new SqlParameter("@site", site);
            param[6] = new SqlParameter("@domain", domain);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_wod_selectPartLackReport", param).Tables[0];
        }

        public DataTable GetPodLoc(string id)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_wod_selectPodLoc", param).Tables[0];
        }

        public DataTable GetPadLocSurplus(string qad, string site)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@part", qad);
            param[1] = new SqlParameter("@site", site);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_wod_selectPodLocSurplus", param).Tables[0];

        }

        
    }
}