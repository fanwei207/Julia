using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Net.Mail;
using System.Text;
using System.IO;

namespace edi
{
    /// <summary>
    /// Summary description for PCDApply
    /// </summary>
    public class PCDApply
    {
        private string connStr = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];

        public DataTable GetPoLineInfo(string poNbr, string poLine)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@poNbr", poNbr);
            param[1] = new SqlParameter("@poLine", poLine);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectPoDet", param).Tables[0];
        }

        public DataTable GetApply(string poNbr, string poLine)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@poNbr", poNbr);
            param[1] = new SqlParameter("@poLine", poLine);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectPCDApply", param).Tables[0];
        }

        public DataTable GetApply(string applyId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ApplyId", applyId);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectPCDApplyByID", param).Tables[0];
        }

        public DataTable GetApplyDet(string poNbr, string poLine)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@poNbr", poNbr);
            param[1] = new SqlParameter("@poLine", poLine);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectPCDApplyDet", param).Tables[0];
        }

        public DataTable GetApplyDet(string applyId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ApplyId", applyId);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectPCDApplyDetByID", param).Tables[0];
        }

        public DataTable GetApprove(string applyId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@applyId", applyId);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectPCDApprove", param).Tables[0];
        }

        public int AddApply(string poNbr, string approveBy, string applyReason, string applyBy,DataTable detail)
        {
            StringWriter writer = new StringWriter();
            detail.WriteXml(writer);
            string xmlDetail = writer.ToString();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@poNbr", poNbr);
            param[1] = new SqlParameter("@approveBy", approveBy);
            param[2] = new SqlParameter("@applyReason", applyReason);
            param[3] = new SqlParameter("@applyBy", applyBy);
            param[4] = new SqlParameter("@detail", xmlDetail);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connStr, CommandType.StoredProcedure, "sp_edi_insertPCDApply", param));
        }

        public void AddApplyDet(DataTable table)
        {
            if (table != null && table.Rows.Count > 0)
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connStr))
                {
                    bulkCopy.DestinationTableName = "dbo.PCD_ApplyDet";
                    bulkCopy.ColumnMappings.Add("ApplyId", "ApplyId");
                    bulkCopy.ColumnMappings.Add("poNbr", "poNbr");
                    bulkCopy.ColumnMappings.Add("poLine", "poLine");
                    bulkCopy.ColumnMappings.Add("planDate", "planDate");
                    bulkCopy.ColumnMappings.Add("applyPlanDate", "applyPlanDate");
                    bulkCopy.WriteToServer(table);
                    table.Dispose();
                }
            }
        }

        public void UpdateApplyDet(DataTable detail)
        {
            StringWriter writer = new StringWriter();
            detail.WriteXml(writer);
            string xmlDetail = writer.ToString();

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@detail", xmlDetail);

            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_batchUpdateApplyDet", sqlParam);
        }

        public bool Approve(string applyId, string approveBy, string uId, string approveOpinion, bool appoveresult)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@applyId", applyId);

            if (approveBy == "")
            {
                param[1] = new SqlParameter("@approveBy", DBNull.Value);
            }
            else
            {
                param[1] = new SqlParameter("@approveBy", approveBy);
            }
            param[2] = new SqlParameter("@uId", uId);
            param[3] = new SqlParameter("@approveOpinion", approveOpinion);
            param[4] = new SqlParameter("@appoveresult", appoveresult);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(connStr, CommandType.StoredProcedure, "sp_edi_updateApprove", param));
        
        }

        public DataTable GetApplyList(string poNbr, string custPart, string part, string applyDate, bool pendingToAppr,string userId,string roleId)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@poNbr", poNbr);
            param[1] = new SqlParameter("@partNbr", custPart);
            param[2] = new SqlParameter("@qadPart", part);
            param[3] = new SqlParameter("@applyDate", applyDate);
            param[4] = new SqlParameter("@pendingToAppr", pendingToAppr);
            param[5] = new SqlParameter("@uId", userId);
            param[6] = new SqlParameter("@roleId", roleId);

            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectPCDApplyList", param).Tables[0];
        }


        public bool CheckPCDExist(string poNbr, string poLine)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@poNbr", poNbr);
            param[1] = new SqlParameter("@poLine", poLine);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(connStr, CommandType.StoredProcedure, "sp_edi_checkPCDExist", param));
        }

        public int DeletePCDApply(string applyId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ApplyId", applyId);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connStr, CommandType.StoredProcedure, "sp_edi_deletePCDApply", param));
        }

        public bool SendMailForApprove(string poNbr, string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string applyReason, string applyId, out string returnMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append(" Dear  Approve  <br>");
            sb.Append("     订单号:" + poNbr + "<br>");
            sb.Append("     申请原因:" + applyReason + "<br>");
            sb.Append("     申请者: " + applyName + "提交了PCD审批申请，请链接以下地址时查看并审批申请<br><br>");
            sb.Append("     请查看详细信息.<br>");
            sb.Append("     请点击下面的连接查看:<br>");
            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/plan/PCD_Apply.aspx?poNbr=" + poNbr + "&Id=" + applyId + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/plan/PCD_Apply.aspx?poNbr=" + poNbr + "&Id=" + applyId + "</a>");
            sb.Append("</form>");
            sb.Append("</html>");
            return SendMail(toEmailAddress, strApproverName, applyEmailAddress, applyName, sb.ToString(), out returnMessage);
        }

        public bool SendMailForPass(string poNbr, string applyEmailAddress, string applyName, string approveEmailAddress, string approveName, string applyReason, string applyId, out string returnMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append(" Dear  申请者  <br>");
            sb.Append("     订单号:" + poNbr + "<br>");
            sb.Append("     申请原因:" + applyReason + "<br>");
            sb.Append("     审批人: " + approveName + "通过了您的PCD审批申请，请链接以下地址时查看<br><br>");
            sb.Append("     请查看详细信息.<br>");
            sb.Append("     请点击下面的连接查看:<br>");
            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/plan/PCD_Apply.aspx?poNbr=" + poNbr + "&Id=" + applyId + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/plan/PCD_Apply.aspx?poNbr=" + poNbr + "&Id=" + applyId + "</a>");
            sb.Append("</form>");
            sb.Append("</html>");
            return SendMail(applyEmailAddress, applyName, approveEmailAddress, approveName, sb.ToString(), out returnMessage);
        }

        public bool SendMailForFailed(string poNbr, string applyEmailAddress, string applyName, string approveEmailAddress, string approveName, string reason, string applyId, out string returnMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append(" Dear  申请者  <br>");
            sb.Append("     订单号:" + poNbr + "<br>");
            sb.Append("     拒绝原因:" + reason + "<br>");
            sb.Append("     审批人: " + approveName + "拒绝了您的PCD审批申请，请链接以下地址时查看<br><br>");
            sb.Append("     请查看详细信息.<br>");
            sb.Append("     请点击下面的连接查看:<br>");
            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/plan/PCD_Apply.aspx?poNbr=" + poNbr + "&Id=" + applyId + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/plan/PCD_Apply.aspx?poNbr=" + poNbr + "&Id=" + applyId + "</a>");
            sb.Append("</form>");
            sb.Append("</html>");
            return SendMail(applyEmailAddress, applyName, approveEmailAddress, approveName, sb.ToString(), out returnMessage);
        }

        private bool SendMail(string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string mailBody, out string returnMessage)
        {

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

            if (applyEmailAddress != null && applyEmailAddress != "")
            {
                MailAddress cc = new MailAddress(applyEmailAddress, applyName);
                mail.CC.Add(cc);
            }

            try
            {
                mail.Subject = "[Notify]PCD审批";
                mail.Body = mailBody;
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

        public int GetPCDApproveStatus(string poNbr, string poLine)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@poNbr", poNbr);
            param[1] = new SqlParameter("@poLine", poLine);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connStr, CommandType.StoredProcedure, "sp_pcd_selectPCDApproveStatus", param));
        }

        public void StartApprove(string poNbr, string poLine, string reasonValue, string reasonText, string remarks, string applyDate, string userId)
        {
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@po_nbr", poNbr);
            sqlParam[1] = new SqlParameter("@po_line", poLine);
            sqlParam[2] = new SqlParameter("@reason_value", reasonValue);
            sqlParam[3] = new SqlParameter("@reason_text", reasonText);
            sqlParam[4] = new SqlParameter("@remark", remarks);
            sqlParam[5] = new SqlParameter("@applyDate", applyDate);
            sqlParam[6] = new SqlParameter("@modify_By", userId);

            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "tcpc0.dbo.sp_PCD_updatethrowPoToAppv", sqlParam);
        }

        public DataTable GetPCDReport(string fromDate, string toDate)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@fromDate", fromDate);
            sqlParam[1] = new SqlParameter("@toDate", toDate);
            return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "tcpc0.dbo.sp_pcd_selectPoLineReport", sqlParam).Tables[0];
        }
    }
}