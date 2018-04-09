using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
/// <summary>
/// Summary description for Hr_coat
/// </summary>
public class Hr_coat
{
	public Hr_coat()
	{


	}
    //
    // TODO: Add constructor logic here
    //
    adamClass adam = new adamClass();
    public DataTable GetAppName(string userno, string plantcode)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@userno", userno);
            sqlParam[1] = new SqlParameter("@plantcode", plantcode);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_hr_GetAppName", sqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetCoatTypes()
    {
        try
        {
            SqlParameter[] sqlParam1 = new SqlParameter[0];
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_hr_selectcoattype", sqlParam1).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetDepartments()
    {
        try
        {
            SqlParameter[] sqlParam1 = new SqlParameter[0];
            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_selectcoatDepartmens", sqlParam1).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable GetCoatDetails(string userno, string usrname,string deparment, string coattype, bool isleave, int plantid)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@userno", userno);
            sqlParam[1] = new SqlParameter("@usrname", usrname);
            sqlParam[2] = new SqlParameter("@deparment", deparment);
            sqlParam[3] = new SqlParameter("@coattype", coattype);
            sqlParam[4] = new SqlParameter("@isleave", isleave);
            sqlParam[5] = new SqlParameter("@plantid", plantid);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_GetCoatDetails", sqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    /// <summary>
    /// 数据添加
    /// </summary>
    /// <param name="strShipNo"></param>
    /// <returns>数据添加</returns>
    /// 
    public bool CoatInfoAdd(string userno, DateTime appdate, string apptype, int appcount, int userid, int plantid)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@userno", userno);
            sqlParam[1] = new SqlParameter("@appdate", appdate);
            sqlParam[2] = new SqlParameter("@apptype", apptype);
            sqlParam[3] = new SqlParameter("@appcount", appcount);
            sqlParam[4] = new SqlParameter("@userid", userid);
            sqlParam[5] = new SqlParameter("@plantid", plantid);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_coatInfoAdd", sqlParam));
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CoatInfoDelete(string userUniformDetailID)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@userUniformDetailID", userUniformDetailID);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_coatInfoDelete", sqlParam));
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}