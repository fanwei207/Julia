using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Reflection;
using System.Net.Mail;
using System.Text;

/// <summary>
/// Summary description for SampleDeliveryHelper
/// </summary>
public class SampleDeliveryHelper
{
	public SampleDeliveryHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];

    /// <summary>
    /// 根据送样单ID获取送样单
    /// </summary>
    /// <param name="id">送样单ID</param>
    /// <returns>送样单实体</returns>
    public SampleDelivery GetSampleDelivery(int id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdMstr", param);
        if (reader.Read())
        {
            SampleDelivery sd = GetSampleDelivery(reader);
            reader.Close();
            return sd;
        }
        else
        {
            reader.Close();
            return null;
        }
    }

    public IList<SampleDelivery> GetSampleDeliveries(string no, string receiver, string fromDate, string toDate, int mstrId, bool pendingToAppr,int uId,int roleId)
    {
        if (fromDate == "")
        {
            fromDate = "1900-01-01";
        }
        if (toDate == "")
        {
            toDate = "1900-01-01";
        }
        IList<SampleDelivery> samples = new List<SampleDelivery>();
        SqlParameter[] param = new SqlParameter[8];
        param[0] = new SqlParameter("@nbr", no);
        param[1] = new SqlParameter("@receiver", receiver);
        param[2] = new SqlParameter("@fromDate", fromDate);
        param[3] = new SqlParameter("@toDate", toDate);
        param[4] = new SqlParameter("@mstrId", mstrId);
        param[5] = new SqlParameter("@pendingToAppr", pendingToAppr);
        param[6] = new SqlParameter("@uid", uId);
        param[7] = new SqlParameter("@roleid", roleId);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdMstrList", param);
        while(reader.Read())
        {
            SampleDelivery sd = GetSampleDelivery(reader);
            samples.Add(sd);
        }
        reader.Close();
        return samples;
    }

    public IList<SampleDelivery> GetSampleDeliveriesForCheck(string no, string receiver, string fromDate, string toDate,int checkValue)
    {
        if (fromDate == "")
        {
            fromDate = "1900-01-01";
        }
        if (toDate == "")
        {
            toDate = "1900-01-01";
        }
        IList<SampleDelivery> samples = new List<SampleDelivery>();
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@nbr", no);
        param[1] = new SqlParameter("@receiver", receiver);
        param[2] = new SqlParameter("@fromDate", fromDate);
        param[3] = new SqlParameter("@toDate", toDate);
        param[4] = new SqlParameter("@checkValue", checkValue);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdMstrListForCheck", param);
        while (reader.Read())
        {
            SampleDelivery sd = GetSampleDelivery(reader);
            samples.Add(sd);
        }
        reader.Close();
        return samples;
    }

    public IList<SampleDelivery> GetSampleDeliveriesForSend(string no, string receiver, string fromDate, string toDate, int sendValue)
    {
        if (fromDate == "")
        {
            fromDate = "1900-01-01";
        }
        if (toDate == "")
        {
            toDate = "1900-01-01";
        }
        IList<SampleDelivery> samples = new List<SampleDelivery>();
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@nbr", no);
        param[1] = new SqlParameter("@receiver", receiver);
        param[2] = new SqlParameter("@fromDate", fromDate);
        param[3] = new SqlParameter("@toDate", toDate);
        param[4] = new SqlParameter("@sendValue", sendValue);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdMstrListForSend", param);
        while (reader.Read())
        {
            SampleDelivery sd = GetSampleDelivery(reader);
            samples.Add(sd);
        }
        reader.Close();
        return samples;
    }

    private  SampleDelivery GetSampleDelivery(SqlDataReader reader)
    {
        SampleDelivery sd = new SampleDelivery(int.Parse(reader["bsd_mstrId"].ToString()), reader["bsd_nbr"].ToString());
        sd.CreatedBy = Convert.ToInt32(reader["bsd_createdby"]);
        sd.CreatedDate = (DateTime)reader["bsd_createddate"];
        sd.Creator = reader["createdName"].ToString();
        sd.DetId = reader["rdw_detId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["rdw_detId"]);
        sd.MstrID = reader["rdw_mstrId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["rdw_mstrId"]);
        sd.IsCanceled = reader["bsd_isCanceled"] == DBNull.Value ? false : Convert.ToBoolean(reader["bsd_isCanceled"]);
        if (reader["bsd_checkResult"] != DBNull.Value)
        {
            sd.CheckResult = Convert.ToBoolean(reader["bsd_checkResult"]);
        }
        sd.IsSended = reader["bsd_isSended"] == DBNull.Value ? false : Convert.ToBoolean(reader["bsd_isSended"]);
        sd.Receiver = reader["bsd_receiptName"].ToString();
        sd.Remarks = reader["bsd_rmks"].ToString();
        sd.SendedBy = reader["bsd_sendedby"] == DBNull.Value ? 0 : Convert.ToInt32(reader["bsd_sendedby"]);
        if (reader["bsd_sendedDate"] != DBNull.Value)
        {
            sd.SendedDate = (DateTime)reader["bsd_sendedDate"];
        }
        sd.Sender = reader["sendedName"].ToString();
        sd.Shipto = reader["bsd_shipto"].ToString();
        return sd;
    }

    /// <summary>
    /// 新增送样单
    /// </summary>
    /// <param name="sample">送样单实体</param>
    /// <returns>新增成功返回送样单的ID，失败返回0</returns>
    public int AddSampleDelivery(SampleDelivery sample,string ulid)
    {
        object lockObj = new object();
        int id = 0;
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@receiver", sample.Receiver);
        param[1] = new SqlParameter("@shipto", sample.Shipto);
        param[2] = new SqlParameter("@remarks", sample.Remarks);
        param[3] = new SqlParameter("@createdby", sample.CreatedBy);
        param[4] = new SqlParameter("@mstrId", sample.MstrID);
        param[5] = new SqlParameter("@detId", sample.DetId);
        param[6] = new SqlParameter("@ulid", ulid);
        lock (lockObj)
        {
            id = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_insertBsdMstr", param));
        }
        return id;
    }

    /// <summary>
    /// 更新送样单主体信息
    /// </summary>
    /// <param name="sample">送样单实体</param>
    /// <returns>成功返回true,失败返回false</returns>
    public bool UpdateSampleDelivery(SampleDelivery sample)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@id", sample.ID);
        param[1] = new SqlParameter("@receiver", sample.Receiver);
        param[2] = new SqlParameter("@shipto", sample.Shipto);
        param[3] = new SqlParameter("@remarks", sample.Remarks);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_updateBsdMstr", param));
    }

    /// <summary>
    /// 删除送样单（包括明细）
    /// </summary>
    /// <param name="sample">送样单实体</param>
    /// <returns>成功返回true,失败返回false</returns>
    public bool DeleteSampleDelivery(SampleDelivery sample)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", sample.ID);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_deleteBsdMstr", param));
    }

    /// <summary>
    /// 取消送样单
    /// </summary>
    /// <param name="sample">送样单实体</param>
    /// <returns>成功返回true,失败返回false</returns>
    public bool CancelSampleDelivery(SampleDelivery sample)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", sample.ID);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_updateBsdMstrCanceled", param));
    }


    public bool SendSampleDelivery(SampleDelivery sample)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", sample.ID);
        param[1] = new SqlParameter("@sendedby", sample.SendedBy);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_updateBsdMstrForSend", param));
    }

    public bool SubmitSampleDelivery(SampleDelivery sample)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", sample.ID);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_updateBsdMstrNoCheck", param));
    }

    /// <summary>
    /// 根据送样单ID获取送样单明细
    /// </summary>
    /// <param name="mstrId">送样单ID</param>
    /// <returns>送样单明细实体列表</returns>
    public IList<SampleDeliveryDetail> GetSampleDeliveryDetails(int mstrId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@mstrId", mstrId);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdDetByMstrId", param);
        IList<SampleDeliveryDetail> details = new List<SampleDeliveryDetail>();
        while(reader.Read())
        {
            SampleDeliveryDetail detail = new SampleDeliveryDetail(Convert.ToInt32(reader["bsd_detID"]), mstrId);
            detail.CheckedBy =reader["bsd_det_checkedby"]==DBNull.Value  ? 0 : Convert.ToInt32(reader["bsd_det_checkedby"]);
            if (reader["bsd_det_checkedDate"] != DBNull.Value)
            {
                detail.CheckedDate = Convert.ToDateTime(reader["bsd_det_checkedDate"]);
            }
            detail.Checker = reader["checkedName"].ToString();
            detail.CheckRemarks = reader["bsd_det_checkRmks"].ToString();
            if (reader["bsd_det_checkResult"] != DBNull.Value)
            {
                detail.CheckResult = Convert.ToBoolean(reader["bsd_det_checkResult"]);
            }
            detail.PartCode = reader["bsd_det_code"].ToString();
            detail.QadNo = reader["bsd_det_qad"].ToString();
            detail.Quantity = reader["bsd_det_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["bsd_det_qty"]);
            detail.Remarks = reader["bsd_det_rmks"].ToString();
            details.Add(detail);
        }
        reader.Close();
        return details;
    }

    /// <summary>
    /// 送样单添加明细
    /// </summary>
    /// <param name="mstrId">送样单ID</param>
    /// <param name="detail">送样单明细实体</param>
    /// <returns>成功返回送样单明细ID，失败返回0</returns>
    public int AddSampleDeliveryDetail(int mstrId, SampleDeliveryDetail detail)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@mstrId", mstrId);
        param[1] = new SqlParameter("@code", detail.PartCode);
        param[2] = new SqlParameter("@qad", detail.QadNo);
        param[3] = new SqlParameter("@qty", detail.Quantity);
        param[4] = new SqlParameter("@remarks", detail.Remarks);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_insertBsdDet", param));
    }

    /// <summary>
    /// 更新送样单明细
    /// </summary>
    /// <param name="detail">送样单明细实体</param>
    /// <returns>成功返回true,失败返回false</returns>
    public bool UpdateSampleDeliveryDetail(SampleDeliveryDetail detail)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", detail.Id);
        param[1] = new SqlParameter("@qty", detail.Quantity);
        param[2] = new SqlParameter("@remarks", detail.Remarks);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_updateBsdDet", param));
    }

    public bool CheckSampleDeliveryDetail(SampleDeliveryDetail detail)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@id", detail.Id);
        param[1] = new SqlParameter("@checkResult", detail.CheckResult);
        param[2] = new SqlParameter("@remarks", detail.CheckRemarks);
        param[3] = new SqlParameter("@checkedby", detail.CheckedBy);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_updateBsdDetForCheck", param));
    }

    /// <summary>
    /// 删除送样单明细
    /// </summary>
    /// <param name="detail">送样单明细实体</param>
    /// <returns>成功返回true,失败返回false</returns>
    public bool DeleteSampleDeliveryDetail(SampleDeliveryDetail detail)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", detail.Id);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_deleteBsdDet", param));
    }

    /// <summary>
    /// 获取送样单明细的上传文件列表
    /// </summary>
    /// <param name="detId">送样单明细ID</param>
    /// <returns>上传文件列表</returns>
    public IList<SampleDeliveryDocImport> GetSampleDeliveryDocImport(int detId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@detId", detId);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdDetDocList", param);
        IList<SampleDeliveryDocImport> docs = new List<SampleDeliveryDocImport>();
        while (reader.Read())
        {
            SampleDeliveryDocImport doc = new SampleDeliveryDocImport(Convert.ToInt32(reader["bsd_docID"]), detId);
            doc.FileDescription = reader["bsd_docFileDescs"].ToString();
            doc.FileName = reader["bsd_docFileName"].ToString();
            doc.UploadBy = (int)reader["bsd_docUploadby"];
            doc.UploadDate = (DateTime)reader["bsd_docUploadDate"];
            doc.Uploader = reader["uploader"].ToString();
            doc.VirtualFileName = reader["bsd_docVirtualFileName"].ToString();
            docs.Add(doc);
        }
        reader.Close();
        return docs;
    }

    /// <summary>
    /// 送样单明细添加上传文档
    /// </summary>
    /// <param name="detId">送样单明细ID</param>
    /// <param name="doc">上传文档实体</param>
    /// <returns>成功返回上传文档ID，失败返回0</returns>
    public int AddSampleDeliveryDocImport(int detId, SampleDeliveryDocImport doc)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@detId", detId);
        param[1] = new SqlParameter("@virFileName", doc.VirtualFileName);
        param[2] = new SqlParameter("@fileName", doc.FileName);
        param[3] = new SqlParameter("@fileDesc", doc.FileDescription);
        param[4] = new SqlParameter("@path", doc.FilePath);
        param[5] = new SqlParameter("@uploadby", doc.UploadBy);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_insertBsdDetDoc", param));
    }

    /// <summary>
    /// 删除上传文档
    /// </summary>
    /// <param name="doc">上传文档实体</param>
    /// <returns>成功返回true,失败返回false</returns>
    public bool DeleteSampleDeliveryDocImport(SampleDeliveryDocImport doc)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", doc.Id);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_deleteBsdDetDoc", param));
    }

    public SampleDeliveryApply GetSampleDeliveryApply(int mstrId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@mstrId", mstrId);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdApply", param);
        if (reader.Read())
        {
            SampleDeliveryApply apply = new SampleDeliveryApply(int.Parse(reader["bsd_applyId"].ToString()));
            apply.ApplyBy = int.Parse(reader["bsd_applyby"].ToString());
            apply.ApplyDate = (DateTime)reader["bsd_applyDate"];
            apply.Applyer = reader["applyer"].ToString();
            if (reader["bsd_approveDate"] != DBNull.Value)
            {
                apply.ApproveDate = (DateTime)reader["bsd_approveDate"];
            }
            if(reader["bsd_approveResult"]!=DBNull.Value)
            {
                apply.ApproveResult = Convert.ToBoolean(reader["bsd_approveResult"]);
            }
            apply.CurrentApproveBy = int.Parse(reader["bsd_approveby"].ToString());
            apply.CurrentApprover = reader["approver"].ToString();
            apply.MstrId = mstrId;
            apply.Reason = reader["bsd_applyReason"].ToString();
            reader.Close();
            return apply;
        }
        else
        {
            reader.Close();
            return null;
        }
    }

    public int AddSampleDeliveryApply(int mstrId, SampleDeliveryApply apply)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@mstrId", mstrId);
        param[1] = new SqlParameter("@approveBy", apply.CurrentApproveBy);
        param[2] = new SqlParameter("@applyReason", apply.Reason);
        param[3] = new SqlParameter("@applyBy", apply.ApplyBy);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_insertBsdApply", param));
    }

    public IList<SampleDeliveryApprove> GetSampleDeliveryApprove(int mstrId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@mstrId", mstrId);
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_bsd_selectBsdApproveList", param);
        IList<SampleDeliveryApprove> list = new List<SampleDeliveryApprove>();
        while (reader.Read())
        {
            SampleDeliveryApprove approve = new SampleDeliveryApprove(int.Parse(reader["bsd_approveId"].ToString()));
            approve.ApplyBy = int.Parse(reader["bsd_applyby"].ToString());
            approve.ApplyId = int.Parse(reader["bsd_applyId"].ToString());
            approve.ApproveBy = int.Parse(reader["bsd_approveby"].ToString());
            if(reader["bsd_approveDate"]!=DBNull.Value)
            {
                approve.ApproveDate = (DateTime)reader["bsd_approveDate"];
            }
            approve.ApproveNote = reader["bsd_approveNote"].ToString();
            approve.Approver = reader["approver"].ToString();
            if(reader["bsd_approveResult"]!=DBNull.Value)
            {
                approve.ApproveResult = Convert.ToBoolean(reader["bsd_approveResult"]);
            }
            list.Add(approve);

        }
        reader.Close();
        return list;
    }

    public bool UpdateSampleDeliveryApprove(SampleDeliveryApprove approve)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@applyId", approve.ApplyId);
        if (approve.ApproveBy != 0)
        {
            param[1] = new SqlParameter("@approveBy", approve.ApproveBy);
        }
        param[2] = new SqlParameter("@uId", approve.ApplyBy);
        param[3] = new SqlParameter("@approveOpinion", approve.ApproveNote);
        param[4] = new SqlParameter("@appoveresult", approve.ApproveResult);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bsd_updateBsdApprove", param));
    }

    public bool SendMail(string mstrId,string mid, string sampleCode, string receiver, string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string applyReason, string applyId, out string returnMessage)
    {
        StringBuilder sb = new StringBuilder();

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
        MailAddress to;
        MailMessage mail = new MailMessage();
        mail.From = from;
        Boolean isSuccess = false;
        try
        {
            to = new MailAddress(toEmailAddress, strApproverName);
            mail.To.Add(to);
        }
        catch
        {
            returnMessage = "the email address of " + toEmailAddress + "is incorrect.Pls correct!";
            return false;
        }

        if (applyEmailAddress != null && applyEmailAddress!="")
        {
            MailAddress cc = new MailAddress(applyEmailAddress, applyName);
            mail.CC.Add(cc);
        }

        try
        {
            mail.Subject = "[Notify]送样单审批";
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append(" Dear  Approve  <br>");
            sb.Append("     送样单号:" + sampleCode + "<br>");
            sb.Append("     接收方:" + receiver + "<br>");
            sb.Append("     申请原因:" + applyReason + "<br>");
            sb.Append("     申请者: " + applyName + "提交了送样单审批申请，请链接以下地址时查看并审批申请<br><br>");
            sb.Append("     请查看详细信息.<br>");
            sb.Append("     请点击下面的连接查看:<br>");
            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/SampleDelivery/SampleDeliveryMaintenance.aspx?bsd_mstrid=" + mstrId + "&mid=" + mid + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/SampleDelivery/SampleDeliveryMaintenance.aspx?bsd_mstrid=" + mstrId + "&mid=" + mid + "</a>");
            sb.Append("</form>");
            sb.Append("</html>");

            mail.Body = Convert.ToString(sb);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            sb.Remove(0, sb.Length);
            isSuccess = true;
            returnMessage = "Send Mail Success!";
        }
        catch (Exception ex)
        {
            isSuccess = false;
            returnMessage = "Send Mail Failed!";
        }

        return isSuccess;


    }
}