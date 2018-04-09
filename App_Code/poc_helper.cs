using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for poc_helper
/// </summary>
public class poc_helper
{

    adamClass helper = new adamClass();

    string connstr = admClass.getConnectString("SqlConn.Conn_edi");
	public poc_helper()
	{
		
	}

    public DataTable BindChannel(string plantCode)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@plantCode", plantCode);

            return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, "sp_edi_selectChannel", param).Tables[0];

            
        }
        catch
        {
           
            return null;
        }

    }

    public  DataTable BindShipTo(string cust,string plantCode)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cust_code", cust);
            param[1] = new SqlParameter("@plantCode", plantCode);

            return  SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, "sp_edi_selectShipTo", param).Tables[0];

            
        }
        catch
        {
            
            return null;
        }

    }



    public DataSet selectEDIPoHrd(string poc_id, string poc_PoNbr,string uID,string uName)
    {
        

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", poc_id)
            , new SqlParameter("@poc_PoNbr" ,poc_PoNbr)
             , new SqlParameter("@uID" ,uID)
              , new SqlParameter("@uName" ,uName)
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, "sp_poc_selectEDIPoHrd", param);
    }

    public DataSet selectEDIPoDet(string poc_id, string poc_PoNbr, string uID, string uName)
    {
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", poc_id)
            , new SqlParameter("@poc_PoNbr" ,poc_PoNbr)
             , new SqlParameter("@uID" ,uID)
              , new SqlParameter("@uName" ,uName)
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, "sp_poc_selectEDIPoDet", param);
    }

    public SqlDataReader selectPocDetByID(string pocd_id)
    {
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@pocd_id", pocd_id)
           
        };

        return SqlHelper.ExecuteReader(connstr, CommandType.StoredProcedure, "sp_poc_selectPocDetByID", param);
    }

    public bool deletePocDetByID(string pocd_id)
    {
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@pocd_id", pocd_id)
           
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, "sp_poc_deletePocDetByID", param));
    }

    public bool saveReason(string poc_id, string reason, string uID, string uName, string ponbr)
    {
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", poc_id)
            , new SqlParameter("@reason" ,reason)
             , new SqlParameter("@uID" ,uID)
              , new SqlParameter("@uName" ,uName)
            , new SqlParameter("@poNbr",ponbr)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, "sp_poc_saveReason", param));
    }

    public string  saveLine(string poc_id, string line, string custPart, string SKU, string QAD, string qty,
        string price, string um, string reqDate, string dueDate, string remark, string uID, string uName, string ponbr
        ,string desc)
    {
        string sqlstr = "sp_poc_saveLine";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",poc_id)
            , new SqlParameter("@line",line)
            , new SqlParameter("@custPart" , custPart)
            , new SqlParameter("@SKU" , SKU)
            , new SqlParameter("@QAD" , QAD)
            , new SqlParameter("@qty", qty)
            , new SqlParameter("@price",price)
            , new SqlParameter("@um", um)
            , new SqlParameter("@reqDate", reqDate)
            , new SqlParameter("@dueDate",dueDate)
            , new SqlParameter("@remark",remark)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
            , new SqlParameter("@poNbr",ponbr)
            , new SqlParameter("@desc",desc)
        };

        return SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public DataTable selectPoDocByID(string poc_id, string poNbr)
    {
        string sqlstr = "sp_poc_selectPoDocByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",poc_id)
            , new SqlParameter("@poNbr",poNbr)
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool uploadManualPo(string pod_id, string fileName, string filePate, string uID, string uName)
    {
        string sqlstr = "sp_poc_uploadManualPo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@pod_id",pod_id)
            , new SqlParameter("@fileName", fileName)
            , new SqlParameter("@filePate",filePate)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
            
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, sqlstr, param));
    }

    public bool deletePoDoc(string id)
    {
        string sqlstr = "sp_poc_deletePoDoc";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@id",id)
            
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, sqlstr, param));
    }

    public bool commitDet(string poc_id,string uID,string uName)
    {
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", poc_id)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, "sp_poc_commitDet", param));
    }

    public DataTable selectPOChangeMstr(string poNbr, string pocCode, string pocStarDate, string pocEndDate, string status,string waiting,string uID)
    {
        string sqlstr = "sp_poc_selectPOChangeMstr";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poNbr",poNbr)
            , new SqlParameter("@pocCode",pocCode)
            , new SqlParameter("@pocStarDate",pocStarDate)
            , new SqlParameter("@pocEndDate",pocEndDate)
            , new SqlParameter("@status",status)
            , new SqlParameter("@waiting" , waiting)
            , new SqlParameter("@uID",uID)
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool ClosePoApply(string poc_id, string uID, string uName)
    {
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", poc_id)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, "sp_poc_ClosePoApply", param));
    }

    public DataTable GetPocEffect(string poc_id)
    {
        string sqlstr = "sp_poc_selectPocEffect";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",poc_id)
            
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }



    public DataTable GetEffectDetail(string pocId, string _effectID)
    {
        string sqlstr = "sp_poc_selectEffectDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",pocId)
            , new SqlParameter("@effectID",_effectID)
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable GetEffectUser(string _effectID)
    {
        string sqlstr = "sp_poc_selectEffectUser";

        SqlParameter[] param = new SqlParameter[]{
          
            new SqlParameter("@effectID",_effectID)
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public SqlDataReader selectEdIPoMstrByID(string poc_id)
    {
        string sqlstr = "sp_poc_selectEdIPoMstrByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",poc_id)
            
        };

        return SqlHelper.ExecuteReader(connstr, CommandType.StoredProcedure, sqlstr, param);
    }

    public SqlDataReader selectEDIAttaByID(string poc_id)
    {
        string sqlstr = "sp_poc_selectEDIAttaByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",poc_id)
            
        };

        return SqlHelper.ExecuteReader(connstr, CommandType.StoredProcedure, sqlstr, param);
    }

    public bool uploadAtta(string pocID, string fileName, string filePate, string type, string uID, string uName)
    {
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", pocID)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
            , new SqlParameter("@type",type)
            , new SqlParameter("@fileName",fileName)
            , new SqlParameter("@filePate",filePate)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, "sp_poc_uploadAtta", param));
    }

    public int enterButton(string type, string flag, string pocID, string uID, string uName, string leaveMsg, out string to, out string copy, out string subject, out string body)
    {
        string sqlstr = "sp_poc_insertOpinion";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", pocID)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
            , new SqlParameter("@type",type)
            , new SqlParameter("@flag",flag)
            , new SqlParameter("@leaveMsg",leaveMsg)
            , new SqlParameter("@to",SqlDbType.NVarChar,500)
            , new SqlParameter("@copy",SqlDbType.NVarChar,500)
            , new SqlParameter("@subject",SqlDbType.NVarChar,500)
            , new SqlParameter("@body",SqlDbType.NVarChar,2000)
        };

        param[6].Direction = ParameterDirection.Output;
        param[7].Direction = ParameterDirection.Output;
        param[8].Direction = ParameterDirection.Output;
        param[9].Direction = ParameterDirection.Output;

        int success = Convert.ToInt32(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, sqlstr, param));

        to = param[6].Value.ToString();
        copy = param[7].Value.ToString();
        subject = param[8].Value.ToString();
        body = param[9].Value.ToString();

        return success;
    }

    public DataTable selectEDIeffectMstr(string uID)
    {
        string sqlstr = "sp_poc_selectEDIeffectMstr";

        SqlParameter param = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public int insertIntoEffect(string poc_id, string effectID, string agree, string msg, string uID, string uName, out string effectDetId, out string to, out string copy, out string subject, out string body)
    {
        string sqlstr = "sp_poc_insertEffectOpinion";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", poc_id)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
            , new SqlParameter("@effectID",effectID)
            , new SqlParameter("@agree",agree)
            , new SqlParameter("@msg",msg)
            , new SqlParameter("@effectDetId",SqlDbType.NVarChar,200)
            , new SqlParameter("@to",SqlDbType.NVarChar,500)
            , new SqlParameter("@copy",SqlDbType.NVarChar,500)
            , new SqlParameter("@subject",SqlDbType.NVarChar,500)
            , new SqlParameter("@body",SqlDbType.NVarChar,2000)
        };
        param[6].Direction = ParameterDirection.Output;
        param[10].Direction = ParameterDirection.Output;
        param[7].Direction = ParameterDirection.Output;
        param[8].Direction = ParameterDirection.Output;
        param[9].Direction = ParameterDirection.Output;
        int flag = Convert.ToInt32(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, sqlstr, param));

        effectDetId = param[6].Value.ToString();
        to = param[7].Value.ToString();
        copy = param[8].Value.ToString();
        subject = param[9].Value.ToString();
        body = param[10].Value.ToString();
        return flag;
    }

    public bool uploadEffect(string pocID, string effectDetId, string fileName, string filePate, string uID, string uName)
    {
        string sqlstr = "sp_poc_uploadEffect";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", pocID)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
            , new SqlParameter("@effectDetId",effectDetId)
            , new SqlParameter("@fileName",fileName)
            , new SqlParameter("@filePate",filePate)
            
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, sqlstr, param));
    }

    public bool deleteEDIPoLine(string poLine, string pocID, string uID, string uName)
    {
        string sqlstr = "sp_poc_deleteEDIPoLine";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id", pocID)
            , new SqlParameter("@uID" ,uID)
            , new SqlParameter("@uName" ,uName)
            , new SqlParameter("@poLine",poLine)
          
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(connstr, CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectPoDocModifiedByID(string pocID)
    {
        string sqlstr = "sp_poc_selectPoDocModifiedByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",pocID)
            
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataSet selectEDIPoDetModified(string pocID, string poNbr)
    {
        string sqlstr = "sp_poc_selectEDIPoDetModified";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poc_id",pocID)
            , new SqlParameter("@poNbr",poNbr)
            
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param);
    }

    public DataTable selectResultList(string No, string ponbr, string beginDate, string enddate)
    {

        string sqlstr = "sp_poc_selectResultList";



        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@No",No)
            , new SqlParameter("@ponbr",ponbr)
             , new SqlParameter("@beginDate",beginDate)
              , new SqlParameter("@enddate",enddate)
            
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];



    }


    public DataTable exportPOCMstr(string poNbr, string pocCode, string pocStarDate, string pocEndDate, string status, string waiting, string uID)
    {
        string sqlstr = "sp_poc_exportPOCMstr";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@poNbr",poNbr)
            , new SqlParameter("@pocCode",pocCode)
            , new SqlParameter("@pocStarDate",pocStarDate)
            , new SqlParameter("@pocEndDate",pocEndDate)
            , new SqlParameter("@status",status)
            , new SqlParameter("@waiting" , waiting)
            , new SqlParameter("@uID",uID)
        };

        return SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }
}