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
public class PurResult
{
	public PurResult()
	{
        
	}


    private String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    #region //版本信息

    public int CheckVersionExists(string versionname, string startdate, string enddate)
    {
        string strName = "sp_purresult_CheckVersionExists";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@versionname", versionname);
        param[1] = new SqlParameter("@startdate", Convert.ToDateTime(startdate));
        if (enddate != "")
        {
            param[2] = new SqlParameter("@enddate", Convert.ToDateTime(enddate));
        }
        else
        {
            param[2] = new SqlParameter("@enddate", enddate);
        }
        int i = (int)SqlHelper.ExecuteScalar(strConn, strName, param);
        return i;
    }

    public bool SavVersion(string versionname, string startdate, string enddate, string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_SaveVersion";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@versionname", versionname);
            param[1] = new SqlParameter("@startdate", Convert.ToDateTime(startdate));
            if (enddate != "")
            {
                param[2] = new SqlParameter("@enddate", Convert.ToDateTime(enddate));
            }
            else
            {
                param[2] = new SqlParameter("@enddate", enddate);

            }
            param[3] = new SqlParameter("@uID", uID);
            param[4] = new SqlParameter("@uName", uName);
            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public DataTable GetVersionList(string versionid)
    {
        try
        {
            string strName = "sp_purresult_selectVersionList";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@versionid", versionid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public bool DeleteVersion(string versionid,string uID, string uName)
    {
        string strName = "sp_purresult_deleteVerion";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@id", versionid);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[3].Value);
    }

    public int GetVersionValue(string versionid, string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_StartVersionTotalValue";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@versionid", versionid);
            param[1] = new SqlParameter("@uID", uID);
            param[2] = new SqlParameter("@uName", uName);
            return Convert.ToInt16(SqlHelper.ExecuteScalar(strConn, strName, param));
        }
        catch
        {
            return 0;
        }
    }

    #endregion
    //版本列表
    #region
    public DataTable GetTypeList(string versionid, string proid, string typename, string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_selectVersionLis";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@versionid", versionid);
            param[1] = new SqlParameter("@proid", proid);
            param[2] = new SqlParameter("@typename", typename);
            param[3] = new SqlParameter("@uID", uID);
            param[4] = new SqlParameter("@uName", uName);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetTypeListAdd(string versionid, string proid, string typename)
    {
        try
        {
            string strName = "sp_purresult_selectVersionLisAdd";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@versionid", versionid);
            param[1] = new SqlParameter("@proid", proid);
            param[2] = new SqlParameter("@typename", typename);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetTypeListById(string versionid)
    {
        try
        {
            string strName = "sp_purresult_SelectVersionListByID";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@versionid", versionid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public int InsertVersionList(string typeid, string versionid, string typename,string maxvalue,string sysvalue,bool checkok,string uID, string uName)
    {
        string strName = "sp_purresult_InsertVersionList";
        SqlParameter[] param = new SqlParameter[8];
        param[0] = new SqlParameter("@typeid", typeid);
        param[1] = new SqlParameter("@typename", typename);
        param[2] = new SqlParameter("@versionid", versionid);
        param[3] = new SqlParameter("@maxvalue", maxvalue);
        param[4] = new SqlParameter("@sysvalue", sysvalue);
        param[5] = new SqlParameter("@checkok", checkok);
        param[6] = new SqlParameter("@uID", uID);
        param[7] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }
    public int DeleteVersionList(string versionid, string uID, string uName)
    {
        string strName = "sp_purresult_InsertVersionList";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@versionid", versionid);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }

    #endregion

    #region //供应商信息维护
    public int SaveCustPartType(string CustCode, string PartType, string uID, string uName)
    {
        string strName = "sp_purresult_SaveCustPartType";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@CustCode", CustCode);
        param[1] = new SqlParameter("@PartType", PartType);
        param[2] = new SqlParameter("@uID", uID);
        param[3] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }

    public int UpdateCustPartType(string CustCode, string PartType, string uID, string uName)
    {
        string strName = "sp_purresult_UpdateCustPartType";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@CustCode", CustCode);
        param[1] = new SqlParameter("@PartType", PartType);
        param[2] = new SqlParameter("@uID", uID);
        param[3] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }

    public DataTable GetCustPartTypeList(string CustCode)
    {
        try
        {
            string strName = "sp_purresult_selectCustPartTypeList";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CustCode", CustCode);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public bool DeleteCustPartType(string CustCode, string uID, string uName)
    {
        string strName = "sp_purresult_deleteCustPartType";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@CustCode", CustCode);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[3].Value);
    }


    #endregion


    #region //项目维护
    public DataTable GetProList(string proname)
    {
        try
        {
            string strName = "sp_purresult_selectProList";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@proname", proname);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public int SavPro(string proname, string proScore, string uID, string uName)
    {
        string strName = "sp_purresult_SavePro";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@proname", proname);
        param[1] = new SqlParameter("@proScore", proScore);
        param[2] = new SqlParameter("@uID", uID);
        param[3] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }
    public bool DeletePro(string versionid, string uID, string uName)
    {
        string strName = "sp_purresult_deletePro";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@id", versionid);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[3].Value);
    }
    public bool DeleteProCheck(string typeid)
    {
        string strName = "Pro";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@typeid", typeid);
        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[1].Value);
    }
    #endregion
    #region //类别维护
    public DataTable GetPro()
    {
        try
        {
            string strName = "sp_purresult_selectType";
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetProByTypeid(string typeid)
    {
        try
        {
            string strName = "sp_purresult_selectTypeByTypeid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@typeid", typeid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetTypeByTypeid(string typeid)
    {
        try
        {
            string strName = "sp_purresult_selectTypeById";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@typeid", typeid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetTypeList(string proid, string typename)
    {
        try
        {
            string strName = "sp_purresult_selectTypeLis";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@proid", proid);
            param[1] = new SqlParameter("@typename", typename);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public bool SavTypeCheckScore(string proid, string score, string typename)
    {
        string strName = "sp_purresult_SavTypeCheckScore";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@proid", proid);
        param[1] = new SqlParameter("@score", score);
        param[2] = new SqlParameter("@typename", typename);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[3].Value);
    }

    public int SavType(string proid, string proname, string typename, string score, string sysvalue, string uID, string uName)
    {
        string strName = "sp_purresult_SaveType";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@proid", proid);
        param[1] = new SqlParameter("@proname", proname);
        param[2] = new SqlParameter("@typename", typename);
        param[3] = new SqlParameter("@score", Convert.ToDecimal(score));
        param[4] = new SqlParameter("@sysvalue", sysvalue);
        param[5] = new SqlParameter("@uID", uID);
        param[6] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }

    public int UpateType(string typeid, string typename, string uID, string uName)
    {
        string strName = "sp_purresult_UpdateType";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@typeid", typeid);
        param[1] = new SqlParameter("@typename", typename);
        param[2] = new SqlParameter("@uID", uID);
        param[3] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }


    public bool DeleteTypeCheck(string typeid)
    {
        string strName = "sp_purresult_DeleteTypeCheck";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@typeid", typeid);
        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[1].Value);
    }
    public bool DeleteTypeVersion(string typeid, string uID, string uName)
    {
        string strName = "sp_purresult_deleteResulVersionList";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@typeid", typeid);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[3].Value);
    }
    public bool DeleteType(string typeid, string uID, string uName)
    {
        string strName = "sp_purresult_deleteType";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@typeid", typeid);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[3].Value);
    }

    #endregion
    

    #region //类别允许值维护

    public int SavTypeValueName(string proid, string proname, string typeid,string typename, string valuename, string uID, string uName)
    {
        string strName = "sp_purresult_SavTypeValueName";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@proid", proid);
        param[1] = new SqlParameter("@proname", proname);
        param[2] = new SqlParameter("@typeid", typeid);
        param[3] = new SqlParameter("@typename", typename);
        param[4] = new SqlParameter("@valuename", valuename);
        param[5] = new SqlParameter("@uID", uID);
        param[6] = new SqlParameter("@uName", uName);
        int i = (int)(SqlHelper.ExecuteScalar(strConn, strName, param));
        return i;
    }
    public DataTable GetTypeValueNameList(string proid, string typeid)
    {
        try
        {
            string strName = "sp_purresult_selectTypeValueNameList";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@proid", proid);
            param[1] = new SqlParameter("@typeid", typeid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public bool DeleteTypeValueName(string pur_id, string uID, string uName)
    {
        string strName = "sp_purresult_DeleteTypeValueName";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@pur_id", pur_id);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        return Convert.ToBoolean(param[3].Value);
    }


    #endregion

    public bool SavePurTypeList(string proid, string proname, string typeid, string typename, string valueStartOperation, int valuefrom, string valueoperation, int valueto, decimal valuescore, bool sysvalue, string uID, string uName, int version, string valuetext, bool valuetype)
    {
        try
        {
            string strName = "sp_purresult_SaveResult";
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@proid", proid);
            param[1] = new SqlParameter("@proname", proname);
            param[2] = new SqlParameter("@typeid", typeid);
            param[3] = new SqlParameter("@typename", typename);
            param[4] = new SqlParameter("@valueStartOperation", valueStartOperation);
            param[5] = new SqlParameter("@valuefrom", valuefrom);
            param[6] = new SqlParameter("@valueoperation", valueoperation);
            param[7] = new SqlParameter("@valueto", valueto);
            param[8] = new SqlParameter("@valuescore", valuescore);
            param[9] = new SqlParameter("@sysvalue", sysvalue);
            param[10] = new SqlParameter("@uID", uID);
            param[11] = new SqlParameter("@uName", uName);
            param[12] = new SqlParameter("@version", version);
            param[13] = new SqlParameter("@valuetext", valuetext);
            param[14] = new SqlParameter("@valuetype", valuetype);
            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }
    public int CheckPurListExists(string proid, string proname, string typeid, string typename, int valuefrom, string valueStartOperation, string valueoperation, int valueto, int version, string valuetext, bool valuetype)
    {
        string strName = "sp_purresult_CheckResultExists";
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@proid", proid);
        param[1] = new SqlParameter("@proname", proname);
        param[2] = new SqlParameter("@typeid", typeid);
        param[3] = new SqlParameter("@typename", typename);
        param[4] = new SqlParameter("@valuefrom", valuefrom);
        param[5] = new SqlParameter("@valueStartOperation", valueStartOperation);
        param[6] = new SqlParameter("@valueoperation", valueoperation);
        param[7] = new SqlParameter("@valueto", valueto);
        param[8] = new SqlParameter("@version", version);
        param[9] = new SqlParameter("@valuetext", valuetext);
        param[10] = new SqlParameter("@valuetype", valuetype);
        int i = (int)SqlHelper.ExecuteScalar(strConn, strName, param);
        return i;
    }
    public int CheckScoreValue(string proid, string proname, string typeid, string typename, int valuefrom, string valueoperation, int valueto, int valuescore,int version)
    {
        string strName = "sp_purresult_CheckScoreValue";
        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@proid", proid);
        param[1] = new SqlParameter("@proname", proname);
        param[2] = new SqlParameter("@typeid", typeid);
        param[3] = new SqlParameter("@typename", typename);
        param[4] = new SqlParameter("@valuefrom", valuefrom);
        param[5] = new SqlParameter("@valueoperation", valueoperation);
        param[6] = new SqlParameter("@valueto", valueto);
        param[7] = new SqlParameter("@valuescore", valuescore);
        param[8] = new SqlParameter("@version", version);
        int i = (int)SqlHelper.ExecuteScalar(strConn, strName, param);
        return i;
    }
    public int GetMaxScoreValue(string proid, string proname, string typeid, string typename, int valuefrom, string valueoperation, int valueto, decimal valuescore, int version, bool valuetype)
    {
        string strName = "sp_purresult_GetMaxScoreValue";
        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@proid", proid);
        param[1] = new SqlParameter("@proname", proname);
        param[2] = new SqlParameter("@typeid", typeid);
        param[3] = new SqlParameter("@typename", typename);
        param[4] = new SqlParameter("@valuefrom", valuefrom);
        param[5] = new SqlParameter("@valueoperation", valueoperation);
        param[6] = new SqlParameter("@valueto", valueto);
        param[7] = new SqlParameter("@valuescore", valuescore);
        param[8] = new SqlParameter("@version", version);
        int i = (int)SqlHelper.ExecuteScalar(strConn, strName, param);
        return i;
    }
    public DataTable GetVerdorList(string vendor, string pro)
    {
        try
        {
            string strName = "sp_purresult_selectVendorList";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@proid", pro);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetValueCheckList(string proid, string typeid)
    {
        try
        {
            string strName = "sp_purresult_selectValueCheckList";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@proid", proid);
            param[1] = new SqlParameter("@typeid", typeid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public int PurResultValueCheckExists(string vendor, string vendorname, string proid, string proname, string typeid, string typename, int valuescore, string mounth, string uID, string uName)
    {
        int result = 0;
        try
        {
            string strName = "sp_purresult_PurResultValueCheckExists";
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@vendorname", vendorname);
            param[2] = new SqlParameter("@proid", proid);
            param[3] = new SqlParameter("@proname", proname);
            param[4] = new SqlParameter("@typeid", typeid);
            param[5] = new SqlParameter("@typename", typename);
            param[6] = new SqlParameter("@valuescore", valuescore);
            param[7] = new SqlParameter("@mounth", mounth);
            param[8] = new SqlParameter("@uID", uID);
            param[9] = new SqlParameter("@uName", uName);
            result =  (int)SqlHelper.ExecuteScalar(strConn, strName, param);
        }
        catch
        {
            result = - 1;
        }
        return result;
    }

    public string SavePurResultValueCheckValueName(string vendor, string vendorname, decimal lotPassRate, int fackBackRate, int _8dRate, int _8dReturnRate, int _8dUnusualRate, int _8dCustComplainRate, int _8dLineComplainRate, decimal deliveryRate, decimal orderCheckRate
                                    , string consignRate, string overRate, int finRate, string priceRate, string docRate, decimal samplePassRate, decimal sampleTimeRate, string year, string mounth, string uID, string uName
                                    , bool qualityPower, bool purchasePower, bool technologyPower)
    {
        string re = "";
        try
        {
            string strName = "sp_purresult_InsertResultValueCheck";
            SqlParameter[] param = new SqlParameter[25];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@vendorname", vendorname);
            param[2] = new SqlParameter("@lotPassRate", lotPassRate);
            param[3] = new SqlParameter("@fackBackRate", fackBackRate);
            param[4] = new SqlParameter("@_8dRate", _8dRate);
            param[5] = new SqlParameter("@deliveryRate", deliveryRate);
            param[6] = new SqlParameter("@orderCheckRate", orderCheckRate);

            param[7] = new SqlParameter("@consignRate", consignRate);
            param[8] = new SqlParameter("@overRate", overRate);
            param[9] = new SqlParameter("@finRate", finRate);
            param[10] = new SqlParameter("@priceRate", priceRate);
            param[11] = new SqlParameter("@docRate", docRate);
            param[12] = new SqlParameter("@samplePassRate", samplePassRate);
            param[13] = new SqlParameter("@sampleTimeRate", sampleTimeRate);
            param[14] = new SqlParameter("@year", year);
            param[15] = new SqlParameter("@mounth", mounth);
            param[16] = new SqlParameter("@uID", uID);
            param[17] = new SqlParameter("@uName", uName);
            param[18] = new SqlParameter("@_8dReturnRate", _8dReturnRate);
            param[19] = new SqlParameter("@_8dUnusualRate", _8dUnusualRate);
            param[20] = new SqlParameter("@_8dCustComplainRate", _8dCustComplainRate);
            param[21] = new SqlParameter("@_8dLineComplainRate", _8dLineComplainRate);
            param[22] = new SqlParameter("@qualityPower", qualityPower);
            param[23] = new SqlParameter("@purchasePower", purchasePower);
            param[24] = new SqlParameter("@technologyPower", technologyPower);

            re = (string)SqlHelper.ExecuteScalar(strConn, strName, param);
        }
        catch
        {
            return "值判断错误";
        }
        return re;
    }


    public bool SavePurResultValueByList(string vendor, string vendorname, decimal lotPassRate, int fackBackRate, int _8dRate, int _8dReturnRate, int _8dUnusualRate, int _8dCustComplainRate, int _8dLineComplainRate, decimal deliveryRate, decimal orderCheckRate
                                        , string consignRate, string overRate, int finRate, string priceRate, string docRate, decimal samplePassRate, decimal sampleTimeRate, string year, string mounth, string uID, string uName, string remark)
    {
        try
        {
            string strName = "sp_purresult_InsertResultValue";
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@vendorname", vendorname);
            param[2] = new SqlParameter("@lotPassRate", lotPassRate);
            param[3] = new SqlParameter("@fackBackRate", fackBackRate);
            param[4] = new SqlParameter("@_8dRate", _8dRate);
            param[5] = new SqlParameter("@deliveryRate", deliveryRate);
            param[6] = new SqlParameter("@orderCheckRate", orderCheckRate);

            param[7] = new SqlParameter("@consignRate", consignRate);
            param[8] = new SqlParameter("@overRate", overRate);
            param[9] = new SqlParameter("@finRate", finRate);
            param[10] = new SqlParameter("@priceRate", priceRate);
            param[11] = new SqlParameter("@docRate", docRate);
            param[12] = new SqlParameter("@samplePassRate", samplePassRate);
            param[13] = new SqlParameter("@sampleTimeRate", sampleTimeRate);
            param[14] = new SqlParameter("@year", year);
            param[15] = new SqlParameter("@mounth", mounth);
            param[16] = new SqlParameter("@uID", uID);
            param[17] = new SqlParameter("@uName", uName);
            param[18] = new SqlParameter("@_8dReturnRate", _8dReturnRate);
            param[19] = new SqlParameter("@_8dUnusualRate", _8dUnusualRate);
            param[20] = new SqlParameter("@_8dCustComplainRate", _8dCustComplainRate);
            param[21] = new SqlParameter("@_8dLineComplainRate", _8dLineComplainRate);
            param[22] = new SqlParameter("@remark", remark);

            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }
    public bool SavePurResultValueByListByTemp(string vendor, string vendorname, decimal lotPassRate, int fackBackRate, int _8dRate, int _8dReturnRate, int _8dUnusualRate, int _8dCustComplainRate, int _8dLineComplainRate, decimal deliveryRate, decimal orderCheckRate
                                    , string consignRate, string overRate, int finRate, string priceRate, string docRate, decimal samplePassRate, decimal sampleTimeRate, string year, string mounth, string uID, string uName, string remark, bool qualityPower, bool purchasePower, bool technologyPower)
    {
        try
        {
            string strName = "sp_purresult_InsertResultValueByTemp";
            SqlParameter[] param = new SqlParameter[26];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@vendorname", vendorname);
            param[2] = new SqlParameter("@lotPassRate", lotPassRate);
            param[3] = new SqlParameter("@fackBackRate", fackBackRate);
            param[4] = new SqlParameter("@_8dRate", _8dRate);
            param[5] = new SqlParameter("@deliveryRate", deliveryRate);
            param[6] = new SqlParameter("@orderCheckRate", orderCheckRate);

            param[7] = new SqlParameter("@consignRate", consignRate);
            param[8] = new SqlParameter("@overRate", overRate);
            param[9] = new SqlParameter("@finRate", finRate);
            param[10] = new SqlParameter("@priceRate", priceRate);
            param[11] = new SqlParameter("@docRate", docRate);
            param[12] = new SqlParameter("@samplePassRate", samplePassRate);
            param[13] = new SqlParameter("@sampleTimeRate", sampleTimeRate);
            param[14] = new SqlParameter("@year", year);
            param[15] = new SqlParameter("@mounth", mounth);
            param[16] = new SqlParameter("@uID", uID);
            param[17] = new SqlParameter("@uName", uName);
            param[18] = new SqlParameter("@_8dReturnRate", _8dReturnRate);
            param[19] = new SqlParameter("@_8dUnusualRate", _8dUnusualRate);
            param[20] = new SqlParameter("@_8dCustComplainRate", _8dCustComplainRate);
            param[21] = new SqlParameter("@_8dLineComplainRate", _8dLineComplainRate);
            param[22] = new SqlParameter("@remark", remark);
            param[23] = new SqlParameter("@qualityPower", qualityPower);
            param[24] = new SqlParameter("@purchasePower", purchasePower);
            param[25] = new SqlParameter("@technologyPower", technologyPower);

            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }
    public bool deletePurResultValueByListTemp(string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_deleteResultValueTemp";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@uName", uName);
            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool SavePurResultValue(string vendor, string vendorname, string proid, string proname, string typeid, string typename, int valuescore, int valueType, string mounth, string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_InsertResultValue";
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@vendorname", vendorname);
            param[2] = new SqlParameter("@proid", proid);
            param[3] = new SqlParameter("@proname", proname);
            param[4] = new SqlParameter("@typeid", typeid);
            param[5] = new SqlParameter("@typename", typename);
            param[6] = new SqlParameter("@valuescore", valuescore);
            param[7] = new SqlParameter("@valueType", valueType);
            param[8] = new SqlParameter("@mounth", mounth);
            param[9] = new SqlParameter("@uID", uID);
            param[10] = new SqlParameter("@uName", uName);
            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool CheckVendExists(string vendor)
    {
        try
        {
            string strName = "sp_purresult_CheckVendExists";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@vendor", vendor);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strName, param));
        }
        catch
        {
            return false ;
        }
    }
    public DataTable CerateVendorResult(string vendor, string year, string mounth)
    {
        try
        {
            string strName = "sp_purresult_CerateVendorResult";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@year", year);
            param[2] = new SqlParameter("@mounth", mounth);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable CerateVendorResultExport(string vendor, string year, string mounth)
    {
        try
        {
            string strName = "sp_purresult_CerateVendorResultExport";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@year", year);
            param[2] = new SqlParameter("@mounth", mounth);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetVendorResultList(string vendor, string proid, string typeid)
    {
        try
        {
            string strName = "sp_purresult_selectVendorResultList";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@proid", proid);
            param[2] = new SqlParameter("@typeid", typeid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public bool CheckVendPass(string vend, string year, string mounth, bool pass, string uID)
    {
        try
        {
            string strName = "sp_purresult_CheckVendPass";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@vendor", vend);
            param[1] = new SqlParameter("@year", year);
            param[2] = new SqlParameter("@mounth", mounth);
            param[3] = new SqlParameter("@pass", pass);
            param[4] = new SqlParameter("@uID", uID);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strName, param));
        }
        catch
        {
            return false;
        }
    }
    public DataTable GetVendorResultReportList(string vendor, string year, string mounth, int pass, int uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_selectVendorResultReportList";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@vendor", vendor);
            param[1] = new SqlParameter("@year", year);
            param[2] = new SqlParameter("@mounth", mounth);
            param[3] = new SqlParameter("@pass", pass);
            param[4] = new SqlParameter("@uID", uID);
            param[5] = new SqlParameter("@uName", uName);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected string templetFile = null;
    protected string outputFile = null;
    protected object missing = Missing.Value;

    ///   <summary> 
    /// 构造函数，需指定模板文件和输出文件完整路径
    ///   </summary> 
    ///   <param name="templetFilePath"> Excel模板文件路径 </param> 
    ///   <param name="outputFilePath"> 输出Excel文件路径 </param> 
    public string PurResult1(string templetFilePath, string outputFilePath)
    {
        //if (templetFilePath == null)
        //    //throw new Exception(" Excel模板文件路径不能为空！ ");

        //if (outputFilePath == null)
        //    //throw new Exception(" 输出Excel文件路径不能为空！ ");

        //if (!File.Exists(templetFilePath))
            //throw new Exception(" 指定路径的Excel模板文件不存在！ ");

        this.templetFile = templetFilePath;
        this.outputFile = outputFilePath;

        try
        {
            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName("Excel");

            foreach (Process myProcess in myProcesses)
            {
                myProcess.Kill();
            }
        }
        catch {
            return "";
        }
        finally
        {
            KillProcess("Excel");
        }
        return "";
    }

    /// <summary>
    /// 终止Excel
    /// </summary>
    /// <param name="processName"></param>
    private void KillProcess(string processName)
    {
        System.Diagnostics.Process myproc = new System.Diagnostics.Process();
        //得到所有打开的进程
        try
        {
            foreach (Process thisproc in Process.GetProcessesByName(processName))
            {
                if (!thisproc.CloseMainWindow())
                {
                    thisproc.Kill();
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex
        }
    }


    /// <summary>
    /// NPOI新版本输出ATL发票:Excel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void ATLInvoiceToExcelNewByNPOI(string sheetPrefixName, string vendor, string year, string mounth, int pass, int uid, int plantcode)
    {
        //读取模板路径
        FileStream templetFile = new FileStream(this.templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        ISheet detailSheet = workbook.GetSheetAt(1);


        //输出计数
        int cnt = 0;
        //每页行数
        int rowP = 53;
        //当前行
        int curR = 4;


        #region //输出明细信息

        //输出明细信息
        //DataSet _dataset = SelectDailyIncoming(uID, stddate, enddate);
        System.Data.DataTable VendorReportList = GetVendorResultReportList(vendor, year, mounth, pass, uid, "");


        int nCols = VendorReportList.Columns.Count;
        int nRows = 3;
        int cntP = 1;
        int curP = 1;

        foreach (DataRow row in VendorReportList.Rows)
        {
                curP = 1;


                ICellStyle style = workbook.CreateCellStyle();

                style.WrapText = true;
                style.Alignment = HorizontalAlignment.Left;
                NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                font.FontHeightInPoints = 10;
                style.SetFont(font);
                IRow iRow = workSheet.GetRow(curR);

                iRow.GetCell(0).SetCellValue(row["prh_vend"].ToString());
                iRow.GetCell(1).SetCellValue(row["ad_name"].ToString());
                iRow.GetCell(2).SetCellValue(row["pur_PartType"].ToString());
                iRow.GetCell(3).SetCellValue("");
                iRow.GetCell(4).SetCellValue(row["pur_lotPassRate"].ToString());
                iRow.GetCell(5).SetCellValue(row["pur_fackBackRate"]);
                //iRow.GetCell(6).SetCellValue(row["pur_8dRate"]);
                iRow.GetCell(6).SetCellValue(row["pur_8dReturnRate"]);
                iRow.GetCell(7).SetCellValue(row["pur_8dUnusualRate"]);
                iRow.GetCell(8).SetCellValue(row["pur_8dCustComplainRate"]);
                iRow.GetCell(9).SetCellValue(row["pur_8dLineComplainRate"]);

                iRow.GetCell(10).SetCellValue(row["pur_deliveryRate"].ToString());
                iRow.GetCell(11).SetCellValue(row["pur_orderCheckRate"].ToString());
                //iRow.GetCell(12).SetCellValue(row["pur_consignRate"]);
                
                iRow.GetCell(12).SetCellValue(row["pur_finRate"]);
                iRow.GetCell(13).SetCellValue(row["pur_overRate"]);
                iRow.GetCell(14).SetCellValue(row["pur_PriceRate"]);

                iRow.GetCell(15).SetCellValue(row["pur_docRate"].ToString());
                iRow.GetCell(16).SetCellValue(row["pur_SamplePassRate"].ToString());
                iRow.GetCell(17).SetCellValue(row["pur_sampleTimeRate"].ToString());

                iRow.GetCell(18).SetCellValue(row["pur_lotPassScore"].ToString());
                iRow.GetCell(19).SetCellValue(row["pur_fackBackScore"].ToString());
                //iRow.GetCell(21).SetCellValue(row["pur_8dScore"].ToString());
                iRow.GetCell(20).SetCellValue(row["pur_8dReturnScore"].ToString());
                iRow.GetCell(21).SetCellValue(row["pur_8dUnusualScore"].ToString());
                iRow.GetCell(22).SetCellValue(row["pur_8dCustComplainScore"].ToString());
                iRow.GetCell(23).SetCellValue(row["pur_8dLineComplainScore"].ToString());

                iRow.GetCell(24).SetCellValue(row["pur_deliveryScore"].ToString());
                iRow.GetCell(25).SetCellValue(row["pur_orderCheckScore"].ToString());
                //iRow.GetCell(26).SetCellValue(row["pur_consignScore"].ToString());
                iRow.GetCell(26).SetCellValue(row["pur_finScore"].ToString());
                iRow.GetCell(27).SetCellValue(row["pur_overScore"].ToString());
                iRow.GetCell(28).SetCellValue(row["pur_priceScore"].ToString());

                iRow.GetCell(29).SetCellValue(row["pur_docScore"].ToString());
                iRow.GetCell(30).SetCellValue(row["pur_samplePassScore"].ToString());
                iRow.GetCell(31).SetCellValue(row["pur_sampleTimeScore"].ToString());

                iRow.GetCell(32).SetCellValue(row["ValueTotal"].ToString());
                iRow.GetCell(33).SetCellValue(row["ValueGrade"].ToString());
                iRow.GetCell(34).SetCellValue(row["pur_Remark"].ToString());

                //if (!string.IsNullOrEmpty(row["sid_qty_pcs"].ToString().Trim()))
                //{
                //    iRow.GetCell(5).SetCellValue(int.Parse(row["sid_qty_pcs"].ToString().Trim()));
                //}
                //iRow.GetCell(6).SetCellValue(row["sid_qty_unit"].ToString());
                //if (!string.IsNullOrEmpty(row["SID_price1"].ToString().Trim()))
                //{
                //    iRow.GetCell(7).SetCellValue(double.Parse(row["SID_price1"].ToString().Trim()));
                //}
                //iRow.GetCell(8).SetCellValue(row["SID_currency"].ToString());
                //if (!string.IsNullOrEmpty(row["amount1"].ToString().Trim()))
                //{
                //    iRow.GetCell(9).SetCellValue(double.Parse(row["amount1"].ToString().Trim()));
                //}
                //iRow.GetCell(10).SetCellValue(row["SID_currency1"].ToString());

                cnt++;
                curR++;
  
        }

        #endregion

        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this.outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                detailSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public int StartVersionCheckValue(string versionid, string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_StartVersionCheckValue";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@versionid", versionid);
            param[1] = new SqlParameter("@uID", uID);
            param[2] = new SqlParameter("@uName", uName);
            return Convert.ToInt16(SqlHelper.ExecuteScalar(strConn, strName, param));
        }
        catch
        {
            return 0;
        }
    }
    public bool StartVersionCheck(string versionid, string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_StartVersionCheck";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@versionid", versionid);
            param[1] = new SqlParameter("@uID", uID);
            param[2] = new SqlParameter("@uName", uName);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, strName, param));
        }
        catch
        {
            return false;
        }
    }


    public bool StartVersion(string versionid, bool flag, string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_StartVersion";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@versionid", versionid);
            param[1] = new SqlParameter("@flag", flag);
            param[2] = new SqlParameter("@uID", uID);
            param[3] = new SqlParameter("@uName", uName);
            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool TechnolobyUpdate(string uID, string uName)
    {
        try
        {
            string strName = "sp_purresult_TechnolobyUpdate";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@uName", uName);
            SqlHelper.ExecuteNonQuery(strConn, strName, param);
        }
        catch
        {
            return false;
        }
        return true;
    }
}