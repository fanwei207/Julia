using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace edi
{
    /// <summary>
    /// Summary description for OrderTracking
    /// </summary>
    public class OrderTracking
    {
        private string connStr = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];

        public DataTable GetOrderTracking(string po1, string po2, string dateFrom, string dateTo, string region, string customer, string status, string type)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@po1", po1);
            param[1] = new SqlParameter("@po2", po2);
            param[2] = new SqlParameter("@ordDate1", dateFrom);
            param[3] = new SqlParameter("@ordDate2", dateTo);
            param[4] = new SqlParameter("@region", region);
            param[5] = new SqlParameter("@customer", customer);
            param[6] = new SqlParameter("@status", status);
            param[7] = new SqlParameter("@type", type);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectOrderTrackingNew", param).Tables[0];
        }


        public DataTable GetOrderTrackingWithWo(string po1, string po2, string dateFrom, string dateTo, string region, string customer, string status, string type)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@po1", po1);
            param[1] = new SqlParameter("@po2", po2);
            param[2] = new SqlParameter("@ordDate1", dateFrom);
            param[3] = new SqlParameter("@ordDate2", dateTo);
            param[4] = new SqlParameter("@region", region);
            param[5] = new SqlParameter("@customer", customer);
            param[6] = new SqlParameter("@status", status);
            param[7] = new SqlParameter("@type", type);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectOrderTrackingWithWo", param).Tables[0];
        }

        public DataTable GetWoList(string soNbr, string part, string line)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@soNbr", soNbr);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@line", line);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectWo", param).Tables[0];
        }

        public DataTable GetRegions(string uID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "Qad_Data.dbo.sp_cm_selectCmRegion", param).Tables[0];
        }

        public void RefreshOrderTracking()
        {
            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_job_OrderTracking");
        }
    }
}