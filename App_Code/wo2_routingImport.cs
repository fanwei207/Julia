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
using System.Data.OleDb;
using System.Text.RegularExpressions;


namespace WO2_RI
{ 

/// <summary>
/// Summary description for wo2_routingImport
/// </summary>
public class WO2_routingImport
{
    adamClass adam = new adamClass();
    String strSQL = "";

	public WO2_routingImport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Delete the temp routing import data
    /// </summary>
    public void DelRoutingTemp(Int32 uID)
    {
        strSQL = "sp_wo2_DelRoutingTemp";
        SqlParameter parm = new SqlParameter("@uID", uID);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
    }

    public bool ImportRouting(Int32 uID)
    {
        strSQL = "sp_wo2_ImportRouting1";
        SqlParameter parm = new SqlParameter("@uID", uID);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
    }

    public bool ImportRoutingAppv(Int32 uID)
    {
        strSQL = "sp_wo2_ImportRoutingAppv";
        SqlParameter parm = new SqlParameter("@uID", uID);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
    }

    public void InsertTempRouting(String routing, String mop, Decimal run,Int32 uID)
    {
        strSQL = "sp_wo2_InsertTempRouting";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@routing", routing);
        parm[2] = new SqlParameter("@mop", mop);
        parm[3] = new SqlParameter("@run", run);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
    }

    public Int32 IsMop(String mop)
    {
        strSQL = "sp_wo2_IsMop";
        SqlParameter parm = new SqlParameter("@mop", mop);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
    }

    public DataTable GetRouting(String Routing,String Mop)
    {
        strSQL = "sp_wo2_GetRouting";
        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@Routing", Routing);
        parm[1] = new SqlParameter("@mop", Mop);
        return SqlHelper.ExecuteDataset(adam.dsn0(),CommandType.StoredProcedure,strSQL,parm).Tables[0];
    
    }

    public Int32 DelRouting(Int32 uID,Int32 ID)
    {
        strSQL = "sp_wo2_DelRouting";
        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@ID", ID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
    
    }

    public Int32 EditRouting(Int32 uID, Int32 ID,String routing,String mop,Decimal run)
    {
        strSQL = "sp_wo2_EditRouting";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@ID", ID);
        parm[2] = new SqlParameter("@routing",routing);
        parm[3] = new SqlParameter("@mop", mop);
        parm[4] = new SqlParameter("@run", run);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm)); 
    }

    public Int32 InsertRouting(Int32 uID,String routing, String mop, Decimal run)
    {
        strSQL = "sp_wo2_InsertRouting";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@routing", routing);
        parm[2] = new SqlParameter("@mop", mop);
        parm[3] = new SqlParameter("@run", run);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
    }

    public bool IsNumber(string str)
    {
        if (str == null || str == "") return false;
        Regex objNotNumberPattern = new Regex("[^0-9.-]");
        Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
        String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
        String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
        Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

        return !objNotNumberPattern.IsMatch(str) &&
        !objTwoDotPattern.IsMatch(str) &&
        !objTwoMinusPattern.IsMatch(str) &&
        objNumberPattern.IsMatch(str);
    }

    public int CheckRoutingTempError(int uID)
    {
        string sqlstr = "sp_wo2_CheckRoutingTempError";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(),CommandType.StoredProcedure,sqlstr,param));
    }

    public int CheckRoutingTempCounts(int uID)
    {
        string sqlstr = "sp_wo2_CheckRoutingTempCounts";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

}
}