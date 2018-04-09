using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using Excel;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CP
/// </summary>
public class CPay
{
    adamClass adam = new adamClass();

    /// <summary>
    /// To compensate and pay
    /// </summary>
    public CPay()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region 公共函数
    public DataSet GetDepartment()
    {
        string strSql = "sp_CP_selectDept";

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql);
    }

    #endregion

    #region 来料赔付
    public bool CheckPO(string nbr, string line, string receiver)
    {
        string strSql = "sp_CP_checkPO";

        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@line", line);
        sqlParam[2] = new SqlParameter("@receiver", receiver);

        return bool.Parse(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).ToString());
    }

    public DataSet GetCpay(string vend, string nbr, string line, string receiver, string result, string status, string confirm)
    {
        string strSql = "sp_CP_selectCpay";

        SqlParameter[] sqlParam = new SqlParameter[7];
        sqlParam[0] = new SqlParameter("@vend", vend);
        sqlParam[1] = new SqlParameter("@nbr", nbr);
        sqlParam[2] = new SqlParameter("@line", line);
        sqlParam[3] = new SqlParameter("@receiver", receiver);
        sqlParam[4] = new SqlParameter("@result", result);
        sqlParam[5] = new SqlParameter("@status", status);
        sqlParam[6] = new SqlParameter("@confirm", confirm);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void InsertCPay(string nbr, string line, string receiver, string result, string uID, string plant)
    {
        string strSql = "sp_CP_insertCpay";

        SqlParameter[] sqlParam = new SqlParameter[6];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@line", line);
        sqlParam[2] = new SqlParameter("@receiver", receiver);
        sqlParam[3] = new SqlParameter("@result", result);
        sqlParam[4] = new SqlParameter("@uID", uID);
        sqlParam[5] = new SqlParameter("@plant", plant);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void DeleteCPay(string nbr, string line, string receiver)
    {
        string strSql = "sp_CP_deleteCpay";

        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@line", line);
        sqlParam[2] = new SqlParameter("@receiver", receiver);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void SubmitCPay(string id, string status, string uID, string plant)
    {
        string strSql = "sp_CP_submitCpay";

        SqlParameter[] sqlParam = new SqlParameter[4];
        sqlParam[0] = new SqlParameter("@id", id);
        sqlParam[1] = new SqlParameter("@status", status);
        sqlParam[2] = new SqlParameter("@uID", uID);
        sqlParam[3] = new SqlParameter("@plant", plant);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void ReturnCPay(string id, string status, string uID, string plant)
    {
        string strSql = "sp_CP_returnCpay";

        SqlParameter[] sqlParam = new SqlParameter[4];
        sqlParam[0] = new SqlParameter("@id", id);
        sqlParam[1] = new SqlParameter("@status", status);
        sqlParam[2] = new SqlParameter("@uID", uID);
        sqlParam[3] = new SqlParameter("@plant", plant);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void SaveCharge(string id, string cost)
    {
        string strSql = "sp_CP_saveCharge";

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@id", id);
        sqlParam[1] = new SqlParameter("@cost", cost);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void ShowReport(string id)
    {
        string strSql = "sp_CP_showReport";

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@id", id);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void UploadReport(string id, byte[] img, string ext, string cmd)
    {
        string strSql = "sp_CP_uploadReport";

        try
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@id", id);
            sqlParam[1] = new SqlParameter("@img", img);
            sqlParam[2] = new SqlParameter("@ext", ext);
            sqlParam[3] = new SqlParameter("@cmd", cmd);

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        { }
    }

    public byte[] GetCPayImg(string id, string cmd)
    {
        string strSql = "sp_CP_selectCPayImg";

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@id", id);
        sqlParam[1] = new SqlParameter("@cmd", cmd);

        return (byte[])SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    } 
    #endregion

    #region 车间退次申请、审核
    public bool CheckWO(string nbr, string lot)
    {
        string strSql = "sp_CP_checkWO";

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@lot", lot);

        return bool.Parse(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).ToString());
    }

    public DataSet GetProcessApply(string nbr, string lot)
    {
        string strSql = "sp_CP_selectProcessApply";

        SqlParameter[] sqlParam = new SqlParameter[4];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@lot", lot);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void SubmitProcessApplyMstr(string nbr, string lot, string part, string ordqty, string date, string seriers, string uID, string plant)
    {
        string strSql = "sp_CP_submitProcessApplyMstr";

        SqlParameter[] sqlParam = new SqlParameter[8];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@lot", lot);
        sqlParam[2] = new SqlParameter("@part", part);
        sqlParam[3] = new SqlParameter("@ordqty", ordqty);
        sqlParam[4] = new SqlParameter("@date", date);
        sqlParam[5] = new SqlParameter("@seriers", seriers);
        sqlParam[6] = new SqlParameter("@uID", uID);
        sqlParam[7] = new SqlParameter("@plant", plant);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void SubmitProcessApplyDet(string nbr, string lot, string part, string desc, string num, string defect, bool scrap, string rmks)
    {
        string strSql = "sp_CP_submitProcessApplyDet";

        SqlParameter[] sqlParam = new SqlParameter[8];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@lot", lot);
        sqlParam[2] = new SqlParameter("@part", part);
        sqlParam[3] = new SqlParameter("@desc", desc);
        sqlParam[4] = new SqlParameter("@num", num);
        sqlParam[5] = new SqlParameter("@defect", defect);
        sqlParam[6] = new SqlParameter("@scrap", scrap);
        sqlParam[7] = new SqlParameter("@rmks", rmks);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void DeleteProcessApply(string nbr, string lot)
    {
        string strSql = "sp_CP_deleteProcessApply";

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@lot", lot);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public DataSet GetProcessAudit(string nbr, string lot, string part, string ap_stddate, string ap_enddate, bool audit, string ad_stddate, string ad_enddate)
    {
        string strSql = "sp_CP_selectProcessAudit";

        SqlParameter[] sqlParam = new SqlParameter[8];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@lot", lot);
        sqlParam[2] = new SqlParameter("@part", part);
        sqlParam[3] = new SqlParameter("@ap_stddate", ap_stddate);
        sqlParam[4] = new SqlParameter("@ap_enddate", ap_enddate);
        sqlParam[5] = new SqlParameter("@audit", audit);
        sqlParam[6] = new SqlParameter("@ad_stddate", ad_stddate);
        sqlParam[7] = new SqlParameter("@ad_enddate", ad_enddate);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void AuditProcessApply(string nbr, string lot, string rmks, string uID, string plant)
    {
        string strSql = "sp_CP_auditProcessApply";

        SqlParameter[] sqlParam = new SqlParameter[5];
        sqlParam[0] = new SqlParameter("@nbr", nbr);
        sqlParam[1] = new SqlParameter("@lot", lot);
        sqlParam[2] = new SqlParameter("@rmks", rmks);
        sqlParam[3] = new SqlParameter("@uID", uID);
        sqlParam[4] = new SqlParameter("@plant", plant);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void ShowProcessReport(string id)
    {
        string strSql = "sp_CP_showProcessReport";

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@id", id);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }
    #endregion

    #region 报废申请、审核
    public DataSet GetScrappedApply(string id, string date, string dept, string no)
    {
        string strSql = "sp_CP_selectScrappedApply";

        SqlParameter[] sqlParam = new SqlParameter[4];
        sqlParam[0] = new SqlParameter("@id", id);
        sqlParam[1] = new SqlParameter("@date", date);
        sqlParam[2] = new SqlParameter("@dept", dept);
        sqlParam[3] = new SqlParameter("@no", no);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void DeleteScrappedApply(string id)
    {
        string strSql = "sp_CP_deleteScrappedApply";

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@id", id);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public Int32 SubmitScrappedApplyMstr(string id, string date, string dept, string no, string user, string uID, string plant)
    {
        string strSql = "sp_CP_submitScrappedApplyMstr";

        SqlParameter[] sqlParam = new SqlParameter[7];
        sqlParam[0] = new SqlParameter("@id", id);
        sqlParam[1] = new SqlParameter("@date", date);
        sqlParam[2] = new SqlParameter("@dept", dept);
        sqlParam[3] = new SqlParameter("@no", no);
        sqlParam[4] = new SqlParameter("@user", user);
        sqlParam[5] = new SqlParameter("@uID", uID);
        sqlParam[6] = new SqlParameter("@plant", plant);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
    }

    public void SubmitScrappedApplyDet(string said, string sadid, string nbr, string lot, string part, string desc, string num, string reason, string deal)
    {
        string strSql = "sp_CP_submitScrappedApplyDet";

        SqlParameter[] sqlParam = new SqlParameter[9];
        sqlParam[0] = new SqlParameter("@sa_id", said);
        sqlParam[1] = new SqlParameter("@sad_id", sadid);
        sqlParam[2] = new SqlParameter("@nbr", nbr);
        sqlParam[3] = new SqlParameter("@lot", lot);
        sqlParam[4] = new SqlParameter("@part", part);
        sqlParam[5] = new SqlParameter("@desc", desc); 
        sqlParam[6] = new SqlParameter("@num", num);
        sqlParam[7] = new SqlParameter("@reason", reason);
        sqlParam[8] = new SqlParameter("@deal", deal);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public DataSet GetScrappedAudit(string ap_stddate, string ap_enddate, bool audit, string au_stddate, string au_enddate)
    {
        string strSql = "sp_CP_selectScrappedAudit";

        SqlParameter[] sqlParam = new SqlParameter[5];
        sqlParam[0] = new SqlParameter("@ap_stddate", ap_stddate);
        sqlParam[1] = new SqlParameter("@ap_enddate", ap_enddate);
        sqlParam[2] = new SqlParameter("@audit", audit);
        sqlParam[3] = new SqlParameter("@au_stddate", au_stddate);
        sqlParam[4] = new SqlParameter("@au_enddate", au_enddate);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public DataSet AuditScrappedApply(string id, string rmks, string user)
    {
        string strSql = "sp_CP_auditScrappedApply";

        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@id", id);
        sqlParam[1] = new SqlParameter("@rmks", rmks);
        sqlParam[2] = new SqlParameter("@user", user);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }
    #endregion

    #region 赔付通知单
    public DataSet GetPayNoticeTemp(string uID)
    {
        string strSql = "sp_CP_selectPayNoticeTemp";

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public void ConfirmPayNotice(string uID)
    {
        string strSql = "sp_CP_confirmPayNoticeTemp";

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@uID", uID);

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    public DataSet GetPayNotice(string vend, string date, string part, string nbr1, string nbr2, string unpart)
    {
        string strSql = "sp_CP_selectPayNotice";

        SqlParameter[] sqlParam = new SqlParameter[6];
        sqlParam[0] = new SqlParameter("@vend", vend);
        sqlParam[1] = new SqlParameter("@date", date);
        sqlParam[2] = new SqlParameter("@part", part);
        sqlParam[3] = new SqlParameter("@nbr1", nbr1);
        sqlParam[4] = new SqlParameter("@nbr2", nbr2);
        sqlParam[5] = new SqlParameter("@unpart", unpart);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
    }

    #endregion
}
