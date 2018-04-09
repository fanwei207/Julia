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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Collections.Generic;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;

/// <summary>
/// Summary description for Prod
/// </summary>

namespace ProdApp
{ 
    public class Prod
    {
        String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
	    public Prod()
	    {
		    //
		    // TODO: Add constructor logic here
		    //

	    }
        public SqlDataReader GetAppDet(string no)
        {
            SqlParameter param = new SqlParameter("@no", no);

            return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_prod_GetAppDet", param);
        }
        public DataTable ExportExcel(String projectname, String prodname, String pcb, String status, String createdate, String plandate, String enddate, String overdate)
        {
            try
            {
                string strSQL = "sp_prod_selectProdExportExcel";
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@projectname", projectname);
                parm[1] = new SqlParameter("@prodname", prodname);
                parm[2] = new SqlParameter("@pcb", pcb);
                parm[3] = new SqlParameter("@status", status);
                parm[4] = new SqlParameter("@createdate", createdate);
                parm[5] = new SqlParameter("@plandate", plandate);
                parm[6] = new SqlParameter("@enddate", enddate);
                parm[7] = new SqlParameter("@overdate", overdate);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSQL, parm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}