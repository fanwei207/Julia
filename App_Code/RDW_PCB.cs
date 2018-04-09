using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using RD_WorkFlow;
using System.Configuration;

/// <summary>
/// Summary description for RDW_PCB
/// </summary>
public class RDW_PCB
{


    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];




	public RDW_PCB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable  selectApplyMstr(string projectCode, string productName,string status,string PCBNo,bool isAll,string uID,string udomian,bool isLayout,string domain)
    {
        string sqlstr = "sp_pcb_selectApplyMstr";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@projectCode" , projectCode)
            , new SqlParameter("@productName" , productName)
            , new SqlParameter("@status" , status)
            , new SqlParameter("@PCBNo" , PCBNo)
            , new SqlParameter("@isAll" , Convert.ToInt32(isAll))
            , new SqlParameter("@uID" , uID)
            , new SqlParameter("@udomain", udomian)
            , new SqlParameter("@isLayout" ,Convert.ToInt32(isLayout))
            , new SqlParameter("@domain" , domain)
        };

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool deleteApplyMstrByID(string PCB_ID, string uID, string uName)
    {
        string sqlstr = "sp_pcb_deleteApplyMstrByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCB_ID" , PCB_ID)
            , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));
    }

    public SqlDataReader selectApplyDetByID(string PCB_ID)
    {
        string sqlstr = "sp_pcb_selectApplyDetByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter ("@PCB_ID",PCB_ID)
        
        };
        
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sqlstr, param);

    }
    
    public void createApplyDet(string PCB_ID, string uID, string uName)
    {
        string sqlstr = "sp_pcb_createApplyDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCB_ID" , PCB_ID)
            , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
        };

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sqlstr, param);
    }

    public void createApplyDet(string PCB_ID, string uID, string uName, string ProjectCode)
    {
        string sqlstr = "sp_pcb_createApplyDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCB_ID" , PCB_ID)
            , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
            , new SqlParameter("@ProjectCode" , ProjectCode)
        };

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sqlstr, param);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="PCBOldNo"></param>
    /// <param name="projectNo"></param>
    /// <param name="lineBasis"></param>
    /// <param name="num"></param>
    /// <param name="sampleDeliveryDate"></param>
    /// <param name="size"></param>
    /// <param name="thickness"></param>
    /// <param name="ply"></param>
    /// <param name="material"></param>
    /// <param name="machining"></param>
    /// <param name="requirment"></param>
    /// <param name="soderResistPaint"></param>
    /// <param name="LAYOUTBasis"></param>
    /// <param name="screenParintingColour"></param>
    /// <param name="copperFoil"></param>
    /// <param name="package"></param>
    /// <param name="safety"></param>
    /// <param name="remark"></param>
    /// <param name="uID"></param>
    /// <param name="uName"></param>
    /// <param name="PCB_ID"></param>
    /// <param name="type">提交和保存</param>
    /// <returns></returns>
    public string savePCB(string productName, string PCBOldNo, string projectNo, string lineBasis, string num
            , string sampleDeliveryDate, string size, string thickness, string ply, string material
        , string machining, string requirment, string soderResistPaint, string LAYOUTBasis
        , string screenParintingColour, string copperFoil, string package, string safety
        , string remark, string uID, string uName, string PCB_ID, string type)
    {
        string sqlstr = "sp_pcb_savePCB";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@productName" , productName)
            , new SqlParameter("@PCBOldNo" , PCBOldNo)
            , new SqlParameter("@projectNo" , projectNo)
            , new SqlParameter("@lineBasis" , lineBasis)
            , new SqlParameter("@num" , num)
            , new SqlParameter("@sampleDeliveryDate" , sampleDeliveryDate)
            , new SqlParameter("@size" , size)
            , new SqlParameter("@thickness" , thickness)
            , new SqlParameter("@ply" , ply)
            , new SqlParameter("@material" , material)
            , new SqlParameter("@machining" , machining)
            , new SqlParameter("@requirment" , requirment)
            , new SqlParameter("@soderResistPaint" , soderResistPaint)
            , new SqlParameter("@LAYOUTBasis" , LAYOUTBasis)
            , new SqlParameter("@screenParintingColour" , screenParintingColour)
            , new SqlParameter("@copperFoil" , copperFoil)
            , new SqlParameter("@package" , package)
            , new SqlParameter("@safety" , safety) 
            , new SqlParameter("@remark" , remark)
            , new SqlParameter("@PCB_ID" , PCB_ID)
             , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
            ,new SqlParameter("@type",type)
        };

        return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public bool uploadBasis(string PCB_ID, string fileName, string filePate, string PCBI_Type, string uID, string uName)
    {
        string sqlstr = "sp_pcb_uploadBasis";

        SqlParameter[] param = new SqlParameter[]{

             new SqlParameter("@fileName" , fileName)
            , new SqlParameter("@filePate" , filePate) 
            , new SqlParameter("@PCBI_Type" , PCBI_Type)
            , new SqlParameter("@PCB_ID" , PCB_ID)
             , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
           
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));
    }

    public DataSet selectUploadAll(string PCB_ID)
    {
        string sqlstr = "sp_pcb_selectUploadAll";

        SqlParameter[] param = new SqlParameter[]{
           
             new SqlParameter("@PCB_ID" , PCB_ID)
        };

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param);
    }

    public bool deleteUploadByPCBIID(string PCBI_ID, int uID, string uName)
    {
        string sqlstr = "sp_pcb_deleteUploadByPCBLID";

        SqlParameter[] param = new SqlParameter[]{

             new SqlParameter("@PCBI_ID" , PCBI_ID)
             , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
           
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));
    }

    public string SaveSamp(string PCB_ID, string no, string var, string needDate, string uNo, string uDomain, string type, string uID, string uName)
    {
        string sqlstr = "sp_pcb_SaveSamp";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@no" , no)
            , new SqlParameter("@var" , var)
            , new SqlParameter("@needDate" , needDate)
            , new SqlParameter("@uNo" , uNo)
            , new SqlParameter("@uDomain" , uDomain)
            , new SqlParameter("@PCB_ID" , PCB_ID)
             , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
            ,new SqlParameter("@type",type)
        };

        return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public DataTable selectBosMstrByPCBID(string PCB_ID)
    {
        string sqlstr = "sp_pcb_selectBosMstrByPCBID";

        SqlParameter[] param = new SqlParameter[] { 
        
            new SqlParameter("@PCB_ID",PCB_ID)
        };

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }


    public void findEmail(string guid, out string toStr, out string copyStr)
    {
        string sqlStr = "  SELECT sendto,copyto FROM WorkFlow.dbo.Rec_RecipientConfig WHERE id = '" + guid + "'";

        SqlDataReader sdr = SqlHelper.ExecuteReader(strConn, CommandType.Text, sqlStr);
        toStr = string.Empty;
        copyStr = string.Empty;

        if (sdr.Read())
        {
            toStr = sdr["sendto"].ToString();
            copyStr = sdr["copyto"].ToString();
        }
        sdr.Close();

    }

    public bool updateReject(string PCB_ID,string rejectReason, string uID, string uName)
    {
        string sqlstr = "sp_pcb_updateReject";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@rejectReason" , rejectReason)
            , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName", uName)
            , new SqlParameter("@PCB_ID" , PCB_ID)
        
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,sqlstr,param));
    }

    public string savePCB(string productName, string PCBOldNo, string projectNo, string lineBasis, string num, string sampleDeliveryDate, string size, string thickness, string ply, string material, string machining, string requirment, string soderResistPaint, string LAYOUTBasis, string screenParintingColour, string copperFoil, string package, string safety, string remark, string uID, string uName, string PCB_ID, string type, out string domain)
    {
        string sqlstr = "sp_pcb_savePCB";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@productName" , productName)
            , new SqlParameter("@PCBOldNo" , PCBOldNo)
            , new SqlParameter("@projectNo" , projectNo)
            , new SqlParameter("@lineBasis" , lineBasis)
            , new SqlParameter("@num" , num)
            , new SqlParameter("@sampleDeliveryDate" , sampleDeliveryDate)
            , new SqlParameter("@size" , size)
            , new SqlParameter("@thickness" , thickness)
            , new SqlParameter("@ply" , ply)
            , new SqlParameter("@material" , material)
            , new SqlParameter("@machining" , machining)
            , new SqlParameter("@requirment" , requirment)
            , new SqlParameter("@soderResistPaint" , soderResistPaint)
            , new SqlParameter("@LAYOUTBasis" , LAYOUTBasis)
            , new SqlParameter("@screenParintingColour" , screenParintingColour)
            , new SqlParameter("@copperFoil" , copperFoil)
            , new SqlParameter("@package" , package)
            , new SqlParameter("@safety" , safety) 
            , new SqlParameter("@remark" , remark)
            , new SqlParameter("@PCB_ID" , PCB_ID)
             , new SqlParameter("@uID" , uID)
            , new SqlParameter("@uName" , uName)
            ,new SqlParameter("@type",type)
            ,new SqlParameter("@domain" ,SqlDbType.VarChar,20)
        };

        param[23].Direction = ParameterDirection.Output;

        string flag = SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param).ToString();

        domain = param[23].Value.ToString();

        return flag;
    }
}