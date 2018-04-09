using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
using System.Data.SqlClient;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

/// <summary>
/// Summary description for getEdiData
/// </summary>
public class getEdiData
{
	public getEdiData()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataSet getEdiPoHrd(string order, string filter, string date, string plantCode, string userId)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@order", order);
        param[1] = new SqlParameter("@filter", filter);
        param[2] = new SqlParameter("@date", date);
        param[3] = new SqlParameter("@plantCode", plantCode);
        param[4] = new SqlParameter("@userId", userId);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getEdiPoHrd", param);
        return ds;
    }

    public static DataSet getEdiPoDet(string po_id)
    {
        SqlParameter param = new SqlParameter("@po_id", po_id);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getEdiPoDet",param);
       return ds;
    }
    /// <summary>
    /// 验证PO是否在EDIPoHrd中
    /// </summary>
    /// <param name="po_nbr"></param>
    /// <returns></returns>
    public static bool CheckPoExist(string poNbr)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@poNbr", poNbr);
        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkPoExist", param);
        return Convert.ToBoolean(param[1].Value);
    }
    
    public static bool CheckPodetCompare(string id,string fob)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@hrdid", id);
        param[1] = new SqlParameter("@fob", fob);
        param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkPodetcompare", param);
        return Convert.ToBoolean(param[2].Value);
    }
    public static DataSet createISA(string cus_code, string doc_type, Boolean doc_in)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@cus_code", cus_code);
        param[1] = new SqlParameter("@doctype", doc_type);
        param[2] = new SqlParameter("@doc_in", doc_in);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_createISA", param);
        return ds;
    }

    public static DataSet createEDI850DET(string ord_id, string cus_code)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ord_id", ord_id);
        param[1] = new SqlParameter("@cus_code", cus_code);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_create850DET", param);
        return ds;
    }

    public static DataSet createEDI850HRD(string ord_id, string cus_code)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ord_id", ord_id);
        param[1] = new SqlParameter("@cus_code", cus_code);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_create850HRD", param);
        return ds;
    }

    public static void updateOrdStatus(string ord_id, string filter)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ord_id", ord_id);
        param[1] = new SqlParameter("@filter", filter);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateOrdStatus", param);
    }

    public static void updateOrdStatus1(string ord_id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ord_id", ord_id);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateOrdStatus1", param);
    }


    public static void updateOrdStatus2(string detIds)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Ids", detIds);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateOrdStatus2", param);
    }

    public static void RejectOrder(string detid)
    {
        SqlParameter param = new SqlParameter("@detid", detid);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_rejectOrder", param);
    }

    public static Boolean updateDetErrorMsg(string det_id)
    {
        string flag = "";
        SqlParameter param = new SqlParameter("@det_id", det_id);
        flag=Convert.ToString(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateDetErrorMsg", param));
        if (flag == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void updateDetToPlan(string id, string uID, string uName)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_submitEdiPoDetToPlan", param);
    }

    public static DataSet getExcelData(string date, string plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@date", date);
        param[1] = new SqlParameter("@plantCode", plantCode);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getExcelData", param);
        return ds;
    }

    public static DataSet get850QADExcelData(string userid, string date, string plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@userid", userid);
        param[1] = new SqlParameter("@date", date);
        param[2] = new SqlParameter("@plantCode", plantCode);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_850QADExcel", param);
        return ds;
    }

    public static DataSet get855ExportList(string filter)
    {
        SqlParameter param = new SqlParameter("@exportType", filter);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_get855ExportList", param);
        return ds;
    }

    public static DataSet create855BAK(string ordid,string filter)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ordid", ordid);
        param[1] = new SqlParameter("@exportType", filter);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_create855BAK", param);
        return ds;
    }

    public static DataSet create855TD5(string ordid, string filter)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ordid", ordid);
        param[1] = new SqlParameter("@exportType", filter);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_create855TD5", param);
        return ds;
    }

    public static DataSet create855PO1(string ordid, string filter)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ordid", ordid);
        param[1] = new SqlParameter("@exportType", filter);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_create855PO1", param);
        return ds;
    }

    public static void update855Status(string ordid,string filter)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ordid", ordid);
        param[1] = new SqlParameter("@exportType", filter);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_update855Status", param);
    }

    public static void insert855His(string ordid, string filter, string uid, string uname)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@ordid", ordid);
        param[1] = new SqlParameter("@exportType", filter);
        param[2] = new SqlParameter("@uid", uid);
        param[3] = new SqlParameter("@uName", uname);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insert855His", param);
    }

    public static DataSet get860List(string stdDate, string endDate)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@stdDate", stdDate);
        param[1] = new SqlParameter("@endDate", endDate);

        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_get860List", param);
    }

    public static DataSet get860Detail(string hrdId)
    {
        SqlParameter param = new SqlParameter("@hrd_id", hrdId);
        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_get860Detail", param);
    }

    public static string update860DetailStatus(string detailId)
    {
        SqlParameter param = new SqlParameter("@detailId", detailId);
        return Convert.ToString(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_update860DetailStatus", param)).Trim();
    }
    public static string update860DetailAdd(string detailId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@detailId", detailId);
            param[1] = new SqlParameter("@err", SqlDbType.NVarChar, 100);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_update860DetailAdd", param);
            return param[1].Value.ToString();
        }
        catch (Exception)
        {
            
            return "导入失败！";
        }
       
    }
    public static DataSet export860Excel(string stdDate, string endDate)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@stdDate", stdDate);
        param[1] = new SqlParameter("@endDate", endDate);

        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_860Excel", param);
    }

    public static void del860Record(string Hrdid)
    {
        SqlParameter param = new SqlParameter("@hrdid", Hrdid);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_del860Record", param);
    }

    public static DataSet getEdiPoHrdInternal(string filter,string plantCode,string contact,string uname)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@filter", filter);
        param[1] = new SqlParameter("@plantCode", plantCode);
        param[2] = new SqlParameter("@contact", contact);
        param[3] = new SqlParameter("@uname", uname);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getEdiPoHrdInternal", param);
        return ds;
    }

    public static DataSet createEDI850HRDInternal(string ord_id)
    {
        SqlParameter param = new SqlParameter("@ord_id", ord_id);
        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_create850HRDInternal", param);
    }

    public static DataSet createEDI850DETInternal(string ord_id)
    {
        SqlParameter param = new SqlParameter("@ord_id", ord_id);
        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_create850DETInternal", param);
    }

    public static void updateOrdStatusInternal(string ord_id, bool chk)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ord_id", ord_id);
        param[1] = new SqlParameter("@chk", chk);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateOrdStatusInternal", param);
    }

    public static DataSet getEdiPoDetInternal(string po_id)
    {
        SqlParameter param = new SqlParameter("@po_id", po_id);
        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getEdiPoDetInternal", param);
    }

    public static void getQadEdiInternal(string uid,string plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@user", uid);
        param[1] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_get850FromQadInternal", param);
    }

    public static DataSet get850QADExcelDataInternal(string userid,string plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@userid", userid);
        param[1] = new SqlParameter("@plantCode", plantCode);
        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getQadInternal850Excel", param);
    }

    public static void sendEdiPo(string userid, string plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@userid", userid);
        param[1] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_Insert850Internal", param);
    }

    public static DataSet get850QADExcelDataInternal(string filter, string plantCode, string contact, string uname, string date)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@filter", filter);
            param[1] = new SqlParameter("@plantCode", plantCode);
            param[2] = new SqlParameter("@contact", contact);
            param[3] = new SqlParameter("@uname", uname);
            param[4] = new SqlParameter("@date", date);
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getQadInternal850Excel1", param);
        }
        catch
        {
            return null;
        }
    }

    public static DataSet createISAInternal(string po_id)
    {
        SqlParameter param = new SqlParameter("@po_id", po_id);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_createISAInternal", param);
        return ds;
    }

    public static void createSamplePo(string userid)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userid", userid);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_createSamplePo", param);
    }
    /// <summary>
    /// 获取没有正确地址的行，并按格式取出，最终生成cimload文件
    /// </summary>
    /// <returns></returns>
    public static DataTable GetNoAddrRows()
    {
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectNoAddrRows");
        return ds.Tables[0];
    }
    /// <summary>
    /// 更新EdiPoHrd的notNeeded字段
    /// </summary>
    /// <param name="userid"></param>
    public static bool UpdatePoHrdNeedProp(string hrdid, string fob, string plantCode, string type)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@hrdid", hrdid);
        param[1] = new SqlParameter("@fob", fob);
        param[2] = new SqlParameter("@plantCode", plantCode);
        param[3] = new SqlParameter("@type", type);
        param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[4].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updatePoHrdNeedProp", param);

        return Convert.ToBoolean(param[4].Value);
    }
    /// <summary>
    /// 更新EdiPoDet的notNeeded字段
    /// </summary>
    /// <param name="userid"></param>
    public static void UpdatePoDetNeedProp(string id, string plantCode, string type)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@plantCode", plantCode);
        param[2] = new SqlParameter("@type", type);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updatePoDetNeedProp", param);
    }

    /// <summary>
    /// 取消EDI订单
    /// </summary>
    /// <param name="userid"></param>
    public static void UpdatePoHrdCancelProp(string hrdid, string plantCode, string type)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", hrdid);
        param[1] = new SqlParameter("@plantCode", plantCode);
        param[2] = new SqlParameter("@type", type);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updatePoHrdCancelProp", param);
    }

    /// <summary>
    /// 从数据库就可以按格式取出要导入的EDI订单格式了
    /// </summary>
    /// <returns></returns>
    public static DataSet GetEdiPoHrdExportList(string date)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@date", date);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectEdiPoHrdExportList", param);
        return ds;
    }
    /// <summary>
    /// 按ID列表
    /// </summary>
    /// <param name="strID"></param>
    /// <returns></returns>
    public static DataSet GetEdiPoHrdExportList1(string strID)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Ids", strID);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectEdiPoHrdExportList1", param);
        return ds;
    }

    /// <summary>
    /// 按ID列表
    /// </summary>
    /// <param name="strID"></param>
    /// <returns></returns>
    public static DataSet GetEdiPoDetExportList(string strID)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Ids", strID);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectEdiPoDetExportList", param);
        return ds;
    }

    public static void UpdateDetLoadRmks(string detId, string loadRmks, string remark)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", detId);
        param[1] = new SqlParameter("@loadRmks", loadRmks);
        param[2] = new SqlParameter("@remark", remark);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateDetLoadRmks", param);
    }

    public static DataTable GetEdiPoDetWithIds(string ids)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Ids", ids);

        DataTable dt = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectEdiPoDetByIds", param).Tables[0];
        return dt;
    }

    public static void UpdateEdiPoDetFinishedByIds(string ids)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Ids", ids);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateEdiPoDetFinishedByIds", param);
    }

    public static void InsertEdiPoLoadTemp(string ids, string userId)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Ids", ids);
        param[1] = new SqlParameter("@userId", userId);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertEdiPoLoadTemp", param);
    }

    public static DataTable GetEdiPoLoadTempError(string userId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userId", userId);
        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectEdiPoLoadTempError", param).Tables[0];
    }

    public static void ExportCimLoadExcel(string tempFile, string outputFile, DataTable dt)
    {
        FileStream templetFile = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(templetFile);

        ISheet workSheet = workbook.GetSheetAt(1);
        int nRows = 4;
        foreach (DataRow row in dt.Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue(row["so_nbr"]);
            iRow.CreateCell(1).SetCellValue("yes");
            iRow.CreateCell(2).SetCellValue(row["cusCode"]);
            iRow.CreateCell(3).SetCellValue(row["poLine"]);
            iRow.CreateCell(4).SetCellValue(row["qadPart"]);
            iRow.CreateCell(5).SetCellValue(row["site"]);
            iRow.CreateCell(6).SetCellValue(row["ordQty"]);
            iRow.CreateCell(7).SetCellValue("yes");
            iRow.CreateCell(8).SetCellValue(".");
            nRows++;
        }

        // 输出Excel文件并退出 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    public static bool CheckPoLineExists(string detId, string poLine)
    {
        string flag = "";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@detId", detId);
        param[1] = new SqlParameter("@poLine", poLine);
        flag = Convert.ToString(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkPoLineExists", param));
        if (flag == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool PoDetSplitLine(string detId, DataTable detail)
    {
        string flag = "";
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@detId", detId);
        param[1] = new SqlParameter("@detail", xmlDetail);
        flag = Convert.ToString(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_poDetSplitLine", param));
        if (flag == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
