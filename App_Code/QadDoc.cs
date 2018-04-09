using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS;  


/// <summary>
/// Summary description for PurResult
/// </summary>
public class QadDoc
{
    public QadDoc()
	{
        
	}

    private String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_qaddoc"];
    #region

    public int SaveSchema(string Schemaname, string uID, string uName)
    {
        string strName = "sp_qaddoc_SaveSchema";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Schemaname", Schemaname);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }

    public DataTable GetSchemaList(string Schemaname)
    {
        try
        {
            string strName = "sp_qaddoc_selectSchemaList";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Schemaname", Schemaname);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public int DeleteSchema(string Schemaid, string uID, string uName)
    {
        string strName = "sp_qaddoc_DeleteSchema";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Schemaid", Schemaid);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }

    public int UpateSchema(string Schemaid, string Schemaname, string uID, string uName)
    {
        string strName = "sp_qaddoc_UpateSchema";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@Schemaid", Schemaid);
        param[1] = new SqlParameter("@Schemaname", Schemaname);
        param[2] = new SqlParameter("@uID", uID);
        param[3] = new SqlParameter("@uName", uName);

        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }

    #endregion

}