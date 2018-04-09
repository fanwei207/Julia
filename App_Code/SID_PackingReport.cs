using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using adamFuncs;
using CommClass;
using System.Data.OleDb;
using System.Text.RegularExpressions;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Text;
using System.Reflection;
using System.Diagnostics;



namespace QADSID
{
    /// <summary>
    /// Summary description for Packing
    /// </summary>
    public class PackingReport
    {
        adamClass adam = new adamClass();
        String strSQL = "";

        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public PackingReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// delete ImportError table
        /// </summary>
        public void DelImportError(Int32 uID)
        {
            strSQL = " Delete From ImportError Where userID='" + uID + "'";
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);

        }



        /// <summary>
        /// 装箱单打印详情查询
        /// </summary>
        /// <param name="Pi_Cust"></param>
        /// <param name="Pi_QAD"></param>
        /// <param name="Pi_ShipTo"></param>
        /// <returns></returns>
        public DataTable PackingExport(string pk, string nbr, string shipdate1, string shipdate2, string cust, string lastcust, string bldate1, string bldate2, string packingdate1, string packingdate2)
        {
            try
            {
                string strSQL = "sp_sid_selectprintpackinglist";
                SqlParameter[] parm = new SqlParameter[10];
                parm[0] = new SqlParameter("@pk", pk);
                parm[1] = new SqlParameter("@nbr", nbr);
                parm[2] = new SqlParameter("@shipdate1", shipdate1);
                parm[3] = new SqlParameter("@shipdate2", shipdate2);
                parm[4] = new SqlParameter("@cust", cust);
                parm[5] = new SqlParameter("@lastcust", lastcust);
                parm[6] = new SqlParameter("@bldate1", bldate1);
                parm[7] = new SqlParameter("@bldate2", bldate2);
                parm[8] = new SqlParameter("@packingdate1", packingdate1);
                parm[9] = new SqlParameter("@packingdate2", packingdate2);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// 装箱单打印详情查询
        /// </summary>
        /// <param name="Pi_Cust"></param>
        /// <param name="Pi_QAD"></param>
        /// <param name="Pi_ShipTo"></param>
        /// <returns></returns>
        public DataTable PackingExportForFin(string pk, string nbr, string shipdate1, string shipdate2, string cust, string lastcust, string bldate1, string bldate2, Int32  type)
        {
            try
            {
                string strSQL = "sp_sid_selectprintpackingForFinList";
                SqlParameter[] parm = new SqlParameter[9];
                parm[0] = new SqlParameter("@pk", pk);
                parm[1] = new SqlParameter("@nbr", nbr);
                parm[2] = new SqlParameter("@shipdate1", shipdate1);
                parm[3] = new SqlParameter("@shipdate2", shipdate2);
                parm[4] = new SqlParameter("@cust", cust);
                parm[5] = new SqlParameter("@lastcust", lastcust);
                parm[6] = new SqlParameter("@bldate1", bldate1);
                parm[7] = new SqlParameter("@bldate2", bldate2);
                parm[8] = new SqlParameter("@types", type);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }

        }


    }
}