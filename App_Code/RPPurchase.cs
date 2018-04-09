using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
//using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;
using CommClass;
using System.Net.Mail;



namespace Purchase
{
    public class Purchase : System.Web.UI.Page
    {
        adamClass adam = new adamClass();

        /// <summary>
        /// 隐藏的构造函数
        /// </summary>
        public Purchase()
        {}
    
    }


    /// <summary>
    /// RPPurchase 的摘要说明
    /// </summary>
    public class RPPurchase
    {
        adamClass adam = new adamClass();
        public RPPurchase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public DataTable SelectPurchaseDet(string ID)
        {
            string str = "sp_rp_SelectPurchaseDet";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", ID);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
        }
        public bool SendEmail222(string from, string to, string subject, string body)
        {
            try
            {
                //string[] uploadAtts = uploadAtt.Split(';');
                MailAddress _mailFrom = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
                MailMessage _mailMessage = new MailMessage();
                _mailMessage.From = _mailFrom;

                foreach (string _to in to.Split(';'))
                {
                    if (!string.IsNullOrEmpty(_to))
                    {
                        _mailMessage.To.Add(_to);
                    }
                }
                _mailMessage.Subject = subject + " from:" + from;
                _mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
                _mailMessage.Priority = MailPriority.Normal;
                _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, Encoding.GetEncoding("GB2312"), "text/html");
                
                _mailMessage.AlternateViews.Add(htmlBody);
                
                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(_mailMessage);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool SelectIsExistsQADFromDet(string type, string dept, string ID, string MID, string qad, string uID)
        {
            string str = "sp_rp_SelectIsExistsQADFromDet";

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@type", type);
            param[1] = new SqlParameter("@ID", ID);
            param[2] = new SqlParameter("@MID", MID);
            param[3] = new SqlParameter("@uID", uID);
            param[4] = new SqlParameter("@qad", qad);
            param[5] = new SqlParameter("@dept", dept);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
        }
    }
}