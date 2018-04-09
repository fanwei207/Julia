using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Configuration;

/// <summary>
/// Summary description for Rec_Emial
/// </summary>
public class Rec_Emial
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
	public Rec_Emial()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet SelectEmployees(int plant, int department, bool isleave,string userName)
    {
        try
        {
            string sql = "sp_Rec_SelectEmployees";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@plantCode", plant);
            param[1] = new SqlParameter("@departmentID", department);
            param[2] = new SqlParameter("@isleave", Convert.ToInt32(isleave));
            param[3] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        }
        catch
        {
            return null;
        }
    }
    public DataSet SelectOriginalEml(int id,string type)
    {
        string sql = "sp_Rec_SelectOriginalEmls";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@type", type);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
    }
    public bool DeleteAllEmails(string id, string type,int uID)
    {
        try
        {
            string sql = "sp_Rec_DelAllEmails";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@type", type);
            param[2] = new SqlParameter("@modifyby", uID);
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

            return Convert.ToBoolean(param[3].Value);
        }
        catch
        {
            return false;
        }

    }
    public bool UpdateEmialbyType(string id, string type, string emls, int uID)
    {
        string sql = "sp_Rec_updateEmialbyType";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@type", type);
        param[2] = new SqlParameter("@modifyby", uID);
        param[3] = new SqlParameter("@emls", emls);
        param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[4].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

        return Convert.ToBoolean(param[4].Value);
    }
    /// <summary>
    /// 判断是否含中文字符
    /// </summary>
    /// <param name="input"></param>
    /// <returns>True含中文字符 False不含</returns>
    public bool CheckStringChinese(string input)
    {
        foreach (var c in input.ToCharArray())
        {
            if (c >= 0x4e00 && c <= 0x9fbb)
            {
                return true;
            }
        }
        return false;
    }
}