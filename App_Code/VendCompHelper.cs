using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for VendComp
/// </summary>
public class VendCompHelper
{
    public static String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];

    private VendCompHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 获取罚款科目
    /// </summary>
    /// <returns></returns>
    public static DataTable GetCategory()
    {
        try
        {
            string strSql = "sp_vc_selectCategory";

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 获取罚款科目
    /// </summary>
    /// <returns></returns>
    public static DataTable GetVender(string vender)
    {
        try
        {
            string strSql = "sp_vc_selectVender";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@vender", vender);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 获取主记录
    /// </summary>
    /// <returns></returns>
    public static DataTable GetMstrByID(string id)
    {
        try
        {
            string strSql = "sp_vc_selectMstrByID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@vc_id", id);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 邮件发送时，记录下邮件发送信息
    /// </summary>
    /// <param name="mstrID"></param>
    /// <param name="uID"></param>
    /// <param name="uName"></param>
    /// <returns></returns>
    public static bool MarkEmailSended(string mstrID, string uID, string uName)
    {
        try
        {
            string strName = "sp_vc_markEmailSended";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@mstrID", mstrID);
            param[1] = new SqlParameter("@uID", uID);
            param[2] = new SqlParameter("@uName", uName);
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[3].Value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="vc_plant"></param>
    /// <param name="vc_vend"></param>
    /// <param name="vc_catename"></param>
    /// <param name="vc_amount"></param>
    /// <param name="vc_rate"></param>
    /// <param name="vc_submitter"></param>
    /// <param name="vc_remark"></param>
    /// <param name="vc_date"></param>
    /// <param name="loginName"></param>
    /// <param name="userNameC"></param>
    /// <param name="email"></param>
    /// <param name="vc_FactoryName"></param>
    /// <returns></returns>
    public static int InsertMstr(string vc_plant, string vc_site, string vc_vend, string vc_catename, string vc_amount, string vc_rate
            , string vc_submitter, string vc_remark, string vc_date, string loginName, string userNameC, string email, string vc_FactoryName)
    {
        try
        {
            string strName = "sp_vc_insertMstr1";
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@vc_plant", vc_plant);
            param[1] = new SqlParameter("@vc_site", vc_site);
            param[2] = new SqlParameter("@vc_vend", vc_vend);
            param[3] = new SqlParameter("@vc_catename", vc_catename);
            param[4] = new SqlParameter("@vc_amount", vc_amount);
            param[5] = new SqlParameter("@vc_rate", vc_rate);
            param[6] = new SqlParameter("@vc_submitter", vc_submitter);
            param[7] = new SqlParameter("@vc_remark", vc_remark);
            param[8] = new SqlParameter("@vc_date", vc_date);
            param[9] = new SqlParameter("@loginName", loginName);
            param[10] = new SqlParameter("@userNameC", userNameC);
            param[11] = new SqlParameter("@email", email);
            param[12] = new SqlParameter("@vc_FactoryName", vc_FactoryName);
            param[13] = new SqlParameter("@retValue", SqlDbType.Int);
            param[13].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToInt32(param[13].Value);
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public static bool UpdateMstr(string vc_id, string vc_amount, string vc_rate, string vc_remark
            , string userNameC, string email, string vc_FactoryName, string uID, string uName)
    {
        try
        {
            string strName = "sp_vc_updateMstr";
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@vc_id", vc_id);
            param[1] = new SqlParameter("@vc_amount", vc_amount);
            param[2] = new SqlParameter("@vc_rate", vc_rate);
            param[3] = new SqlParameter("@vc_remark", vc_remark);
            param[4] = new SqlParameter("@userNameC", userNameC);
            param[5] = new SqlParameter("@email", email);
            param[6] = new SqlParameter("@vc_FactoryName", vc_FactoryName);
            param[7] = new SqlParameter("@uID", uID);
            param[8] = new SqlParameter("@uName", uName);
            param[9] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[9].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[9].Value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    /// <summary>
    /// 获取记录关联文档
    /// </summary>
    /// <param name="mstrID"></param>
    /// <returns></returns>
    public static DataTable GetDocuments(string mstrID)
    {
        try
        {
            string strSql = "sp_vc_selectDoc";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@mstrID", mstrID);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public static int CancleItem(int vc_id)
    {
        string sql = "sp_vc_deletemstr";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@vc_id", vc_id);
        param[1] = new SqlParameter("@reValue", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

        return Convert.ToInt32(param[1].Value);
    }

    public static int ApproveORreject(int vc_id, int result ,string reason ,int uID,string uName)
    {
        string sql = "sp_vc_ApproveORreject";
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@vc_id", vc_id);
            param[1] = new SqlParameter("@reValue", SqlDbType.Int);
            param[1].Direction = ParameterDirection.Output;
            param[2] = new SqlParameter("@result", result);
            param[3] = new SqlParameter("@reason", reason);
            param[4] = new SqlParameter("@uID", uID);
            param[5] = new SqlParameter("@uName", uName);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

            return Convert.ToInt32(param[1].Value);
        }
        catch
        {
            return 0;
        }
        
    }

    public static string GetEmail(int vc_id)
    {
        string sql = "sp_vc_GetEmail";
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@vc_id", vc_id);

            return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, param).ToString();
        }
        catch
        {
            return "";
        }

    }
}