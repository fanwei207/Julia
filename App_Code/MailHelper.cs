using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
using adamFuncs;
using System.Configuration;
using LumiSoft.Net.POP3.Client;
using LumiSoft.Net.MIME;
using LumiSoft.Net.Mail;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Mail_Helper
/// </summary>
public class MailHelper
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
	private MailHelper(){}
    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    //public static string DecodeStr(string str)
    //{
    //    string result = "";
    //    if (str != "" || str != null)
    //    {
    //        if (str.ToUpper().Contains("UTF-8"))
    //        {
    //            String[] array = str.Split('?');
    //            if (array.Length > 2)
    //            {
    //                string title = array[3];
    //                byte[] bytes = Convert.FromBase64CharArray(title.ToCharArray(), 0, title.ToCharArray().Length);
    //                Encoding en = Encoding.GetEncoding("utf-8");
    //                result = en.GetString(bytes);
    //            }

    //        }
    //        else
    //        {
    //            String[] array = str.Split('?');
    //            if (array.Length > 2)
    //            {
    //                string title = array[3];
    //                byte[] bytes = Convert.FromBase64CharArray(title.ToCharArray(), 0, title.ToCharArray().Length);
    //                Encoding en = Encoding.GetEncoding("gb2312");
    //                result = en.GetString(bytes);
    //            }
    //        }
    //    }
    //    return result;
    //}
    /// <summary>
    /// 批量导入
    /// </summary>
    /// <param name="map">一个Dictionary,键表示源数据，值表示目标数据</param>
    /// <param name="connStr">连接字符串</param>
    /// <param name="table">源数据表</param>
    /// <param name="destinationTable">数据库中的目标表</param>
    /// <returns></returns>
    public static bool ImporToServer(Dictionary<string, string> map, string connStr, DataTable table, string destinationTable)
    {
        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connStr))
        {
            bulkCopy.DestinationTableName = destinationTable;
            foreach (var value in map)
            {
                bulkCopy.ColumnMappings.Add(value.Key, value.Value);
            }
            try
            {
                bulkCopy.WriteToServer(table);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                table.Dispose();
            }
        }
        return true;
    }
    /// <summary>
    /// 获取数据库邮件列表
    /// </summary>
    /// <param name="isDisposed"></param>
    /// <param name="bDate"></param>
    /// <param name="eDate"></param>
    /// <param name="uID"></param>
    /// <returns></returns>
    public static DataTable GetMalSubject(int isDisposed, string bDate, string eDate, string uID)
    {
        string sp = "sp_mail_selectMailSubject";
        SqlParameter []parms = new SqlParameter[4];
        parms[0]=new SqlParameter("@isDispose", isDisposed);
        parms[1] = new SqlParameter("@bDate", bDate);
        parms[2] = new SqlParameter("@eDate", eDate);
        parms[3] = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    /// <summary>
    /// 获取数据库邮件正文内容
    /// </summary>
    /// <param name="m_UID"></param>
    /// <param name="uID"></param>
    /// <returns></returns>
    public static SqlDataReader GetMailInfo(string m_UID, string uID)
    {
        string sp = "sp_mail_selectMailBody";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@m_UID", m_UID);
        parms[1] = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sp, parms);
    }
    /// <summary>
    /// 修改未读状态,和处理状态
    /// </summary>
    /// <param name="UID"></param>
    public static void ModifyReadStatus(string m_UID, int uId, int disposed)
    {
        string sp = "sp_mail_ModifyMailReadStatus";
        SqlParameter[] parms = new SqlParameter[3];
        parms[0]=new SqlParameter("@m_UID", m_UID);
        parms[1] = new SqlParameter("@m_disposedBy", uId);
        parms[2] = new SqlParameter("@m_disposed", disposed);
        SqlHelper.ExecuteNonQuery(strConn,CommandType.StoredProcedure,sp,parms);
    }
    /// <summary>
    /// 获取文档附件
    /// </summary>
    /// <param name="m_UID">邮件ID</param>
    /// <returns></returns>
    public static DataTable GetAttachments(string m_UID, string type)
    {
        string sp = "sp_mail_selectAttachments";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@m_UID", m_UID);
        parms[1] = new SqlParameter("@type", type);
        return  SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    /// <summary>
    /// 获取数据库中最大的邮件ID
    /// </summary>
    /// <returns></returns>
    public static Int64 GetMaxMailID()
    {
        try
        {
            string sp = "sp_mail_selectMaxMailID";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@retValue", SqlDbType.VarChar, 50);
            parms[0].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sp, parms);

            return Convert.ToInt64(parms[0].Value);
        }
        catch
        {
            return 0;
        }
    }
    /// <summary>
    /// 将新的邮件最大ID更新到Mail_maxMailID，并将邮件从临时表转到正是表
    /// </summary>
    /// <returns></returns>
    public static bool SetMaxMailID(string uID)
    {
        try
        {
            string sp = "sp_mail_setMaxMailID";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@uID", uID);
            parms[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            parms[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sp, parms);

            return Convert.ToBoolean(parms[1].Value);
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 每次收邮件之前，都要清掉临时表
    /// </summary>
    /// <param name="uID"></param>
    /// <returns></returns>
    public static bool ClearMailMstrTemp(string uID)
    {
        try
        {
            string sp = "sp_mail_clearMailMstrTemp";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@uID", uID);
            parms[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            parms[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sp, parms);

            return Convert.ToBoolean(parms[1].Value);
        }
        catch
        {
            return false;
        }
    }
    public static void AddReadStatus(string m_UID, int uId)
    {
        string sp = "sp_mail_insertReadMan";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@m_UID", m_UID);
        parms[1] = new SqlParameter("@uId", uId);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sp, parms);
    }

    public static bool GetReadStatus(string m_UID, int uId)
    {
        string sp = "sp_mail_selectReaderStatus";
        SqlParameter[] parms = new SqlParameter[3];
        parms[0] = new SqlParameter("@m_UID", m_UID);
        parms[1] = new SqlParameter("@uId", uId);
        parms[2] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[2].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sp, parms);
        return Convert.ToBoolean(parms[2].Value);
    }
    /// <summary>
    /// 将图片转换为base64位编码
    /// </summary>
    /// <param name="Imagefilename"></param>
    /// <returns></returns>
    public static string ImgToBase64String(string Imagefilename)
    {
        try
        {
            Bitmap bmp = new Bitmap(Imagefilename);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            String strbaser64 = Convert.ToBase64String(arr);
            return strbaser64;
        }
        catch (Exception ex)
        {
           return null;
        }
    }
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="copy"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="type">邮件发送类型，回复或者转发</param>
    /// <returns></returns>
    public static bool SendEmail(string from, string to, string copy, string subject, string body, string type ,string uId ,string uploadAtt, string imgPath)
    {
        try
        {
            string mailAccount = ConfigurationManager.AppSettings["IsHelpEmail"];
            string[] uploadAtts = uploadAtt.Split(';');
            MailAddress _mailFrom = new MailAddress(ConfigurationManager.AppSettings["IsHelpEmail"]);
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = _mailFrom;

            foreach (string _to in to.Split(';'))
            {
                if (!string.IsNullOrEmpty(_to)&&_to.ToLower()!=mailAccount.ToLower())
                {
                    _mailMessage.To.Add(_to);
                }
            }

            if (!string.IsNullOrEmpty(copy))
            {
                foreach (string _cc in copy.Split(';'))
                {
                    if (!string.IsNullOrEmpty(_cc)&&_cc.Trim().ToLower()!=mailAccount.ToLower())
                    {
                        MailAddress _mailCopy = new MailAddress(_cc);
                        _mailMessage.CC.Add(_mailCopy);
                    }
                }
            }
            _mailMessage.Subject = subject + " from:" + from;
            _mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
             System.Net.Mail.Attachment att;
            string extention = null;
            //string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string basePath = imgPath;
            string cId;
            if (type == "copyTo")
            {
                DataTable atts = MailHelper.GetAttachments(uId, "NotInline");
                if (atts.Rows.Count > 0 && atts != null)
                {
                    for (int i = 0; i < atts.Rows.Count; i++)
                    {
                        att = new Attachment(basePath.Replace("\\", "/") + atts.Rows[i].ItemArray[1].ToString().Replace("\\", "/"));
                        _mailMessage.Attachments.Add(att);
                    }
                }
            }
            DataTable dt = MailHelper.GetAttachments(uId, "isInline");
            if (dt.Rows.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    cId = dt.Rows[i].ItemArray[0].ToString().Substring(1, dt.Rows[i].ItemArray[0].ToString().Length - 2);
                    body = body.Replace(dt.Rows[i].ItemArray[1].ToString(), "cid:" + cId);
                }
            }
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, Encoding.GetEncoding("GB2312"), "text/html");
            
            if (dt.Rows.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int j = dt.Rows[i].ItemArray[1].ToString().LastIndexOf(".");
                    extention = dt.Rows[i].ItemArray[1].ToString().Substring(j + 1);      
                    cId = dt.Rows[i].ItemArray[0].ToString().Substring(1, dt.Rows[i].ItemArray[0].ToString().Length - 2);
                    LinkedResource lrImage;
                    try
                    {
                         lrImage = new LinkedResource(basePath.Replace("\\", "/") + dt.Rows[i].ItemArray[1].ToString().Replace("\\", "/"), "image/" + extention);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            lrImage = new LinkedResource(dt.Rows[i].ItemArray[1].ToString());
                        }
                        catch
                        {
                            lrImage = new LinkedResource(basePath.Replace("\\", "/") + "JULIA/" + dt.Rows[i].ItemArray[1].ToString());
                        }
                    }
                    lrImage.ContentId = cId;
                    htmlBody.LinkedResources.Add(lrImage);
                }
            }
            _mailMessage.AlternateViews.Add(htmlBody);
            foreach (string attss in uploadAtts)
            {
                if (attss.Trim() != string.Empty)
                {
                    Attachment newAtt = new Attachment(attss.Trim());
                    _mailMessage.Attachments.Add(newAtt);
                }
            }
            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(_mailMessage);

            //BasePage.SSendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), to, copy, subject + " " + DateTime.Now.ToString("MM-dd-yyyy") + "  from " + from, body);
            

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    public static bool SendEmail(string from, string to, string copy, string subject, string body,  string uploadAtt, string imgPath)
    {
        try
        {
           string[] uploadAtts = uploadAtt.Split(';');
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

            if (!string.IsNullOrEmpty(copy))
            {
                foreach (string _cc in copy.Split(';'))
                {
                    if (!string.IsNullOrEmpty(_cc))
                    {
                        MailAddress _mailCopy = new MailAddress(_cc);
                        _mailMessage.CC.Add(_mailCopy);
                    }
                }
            }
            _mailMessage.Subject = subject + " from:" + from;
            _mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            string extention = null;
            Dictionary<string, string> imgUrl = MailHelper.GetImgSrcAndCid(body);
            if (imgUrl.Count > 0 && imgUrl != null)
            {
                foreach (KeyValuePair<string,string> kp in imgUrl)
                {
                    body = body.Replace(kp.Key, "cid:" + kp.Value);
                }
            }
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, Encoding.GetEncoding("GB2312"), "text/html");
            if (imgUrl.Count > 0 && imgUrl != null)
            {
                foreach (KeyValuePair<string, string> kp in imgUrl)
                {
                    int j = kp.Key.LastIndexOf(".");
                    extention = kp.Key.Substring(j + 1);  
                    LinkedResource lrImage;
                    try
                    {
                        lrImage = new LinkedResource(imgPath.Replace("\\", "/") + kp.Key, "image/" + extention);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            lrImage = new LinkedResource(kp.Key);
                        }
                        catch
                        {
                            string _imgPath = @"D:\tcpcnew\";
                            lrImage = new LinkedResource(_imgPath + "JULIA/" + kp.Key);
                        }
                    }
                    lrImage.ContentId = kp.Value;
                    htmlBody.LinkedResources.Add(lrImage);
                }
            }
            _mailMessage.AlternateViews.Add(htmlBody);
            foreach (string attss in uploadAtts)
            {
                if (attss.Trim() != string.Empty)
                {
                    int index = attss.Trim().LastIndexOf("/");
                    Attachment newAtt = new Attachment(attss.Trim());
                    newAtt.Name = attss.Trim().Substring(index+1);
                    _mailMessage.Attachments.Add(newAtt);
                }
            }
            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(_mailMessage);

            //BasePage.SSendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), to, copy, subject + " " + DateTime.Now.ToString("MM-dd-yyyy") + "  from " + from, body);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
    /// <summary>
    /// 转发的时，获取当前用户邮件信息
    /// </summary>
    /// <param name="uId"></param>
    /// <returns></returns>
    public static string GetUserEmail(int uId)
    {
        string sp = "sp_mail_selectUserMail";
        SqlParameter[] parms = new SqlParameter[1];
        parms[0] = new SqlParameter("@uId",uId);
        SqlDataReader dr=SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sp, parms);
        if (dr.Read())
        {
            return dr["email"].ToString();
        }
        else
            return null;
    }
    /// <summary> 
    /// 取得HTML中所有图片的 URL。 
    /// </summary> 
    /// <param name="sHtmlText">HTML代码</param> 
    /// <returns>图片的URL列表</returns> 
    public static void GetImgSrcAndInsert(string sHtmlText, string mailId)
    {
        // 定义正则表达式用来匹配 img 标签 
        Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
        // 搜索匹配的字符串 
        MatchCollection matches = regImg.Matches(sHtmlText);
        ArrayList sUrlList = new ArrayList();
        // 取得匹配项列表 
        foreach (Match match in matches)
        {
            if (!sUrlList.Contains(match.Groups["imgUrl"].Value) && !(match.Groups["imgUrl"].Value.Contains("src=\"http://")))
            {
                InsertAttachmentSrc(match.Groups["imgUrl"].Value, "<_foxmail@" + DateTime.Now.ToString("yyyyMMddhhmmss")+DateTime.Now.Millisecond.ToString()+">", mailId);
            }
        }
    }
    /// <summary>
    /// 发送邮件的时候，回去邮件Body中的图片的路径，并且为其设置一个相应的附件cid
    /// </summary>
    /// <param name="sHtmlText"></param>
    /// <returns></returns>
    public static Dictionary<string,string> GetImgSrcAndCid(string sHtmlText)
    {
        Dictionary<string, string> urlList=new Dictionary<string,string>();
        // 定义正则表达式用来匹配 img 标签 
        Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
        // 搜索匹配的字符串 
        MatchCollection matches = regImg.Matches(sHtmlText);
        ArrayList sUrlList = new ArrayList();
        // 取得匹配项列表 
        foreach (Match match in matches)
        {
      
            if (!urlList.ContainsKey(match.Groups["imgUrl"].Value) && !(match.Groups["imgUrl"].Value.Contains("src=\"http://")))
            {
                urlList.Add(match.Groups["imgUrl"].Value, "_foxmail@" + DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString());
            }
        }
        return urlList;
    }
    /// <summary>
    /// 将html中的<img>的src插入到数据库中
    /// </summary>
    /// <param name="src"></param>
    /// <param name="contentId"></param>
    /// <param name="mailId"></param>
    public static void InsertAttachmentSrc(string src,string contentId, string mailId)
    {
        string sp = "sp_mail_insertAttachment";
        SqlParameter[] parms = new SqlParameter[3];
        parms[0] = new SqlParameter("@src",src);
        parms[1] = new SqlParameter("@cId",contentId);
        parms[2] = new SqlParameter("@mId", mailId);
        SqlHelper.ExecuteNonQuery(strConn,CommandType.StoredProcedure,sp,parms);
    }

    #region LumiSoft 接收邮件
    public static void ReceiveMail(string uId, out int mailNum ,string bodyPath ,string attPath)
    {
         mailNum = 0;
        //每次收取之前，先清空临时表
        if (!MailHelper.ClearMailMstrTemp(uId)) 
        {
            return;
        }

        string _mailServer = ConfigurationManager.AppSettings["mailServer"];
        string _isHelpAccount = ConfigurationManager.AppSettings["IsHelpEmail"];
        string _isHelpPassword = Encoding.Default.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["IsHelpEmailPwd"].ToString()));

        if (string.IsNullOrEmpty(_mailServer) || string.IsNullOrEmpty(_isHelpAccount) || string.IsNullOrEmpty(_isHelpPassword))
        {
            return;
        }
        string _mailId = string.Empty;
        string _from = string.Empty;  //发件人
        string _fromName = string.Empty;//发件人姓名
        string _to = string.Empty;    //收件人
        string _copyTo = "";//抄送人
        string _subject = string.Empty;  //主题
        string _body = string.Empty;    //正文
        DateTime _receiveDate = new DateTime();//接收时间
        string _attName;//附件保存名称
        Dictionary<string, string> mailList = new Dictionary<string, string>();  //邮件列表

        Dictionary<string, string> attList = new Dictionary<string, string>();   //附件列表
        string _bodyPath = "\\TecDocs\\ITSupport\\Mails\\";//邮件内容存储路径
        string _attachPath = "\\TecDocs\\ITSupport\\Attachs\\";//附件存储路径
        DataTable table = new DataTable();
        DataTable atts = new DataTable();
        DataRow row = null;
        DataRow attRow = null;
        DataColumn column;

        #region 定义邮件内容表
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "uid";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "from";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fromName";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "subject";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "body";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "reDate";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "receiveBy";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "receiveName";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "createDate";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "cc";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "to";
        table.Columns.Add(column);
        #endregion

        #region 定义附件表
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "uid";
        atts.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "attNmame";
        atts.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "attPath";
        atts.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "attIsInline";
        atts.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "attContentID";
        atts.Columns.Add(column);

        #endregion

        #region  对应导入项
        mailList.Add("uid", "m_UID");
        mailList.Add("from", "m_from");
        mailList.Add("fromName", "m_fromName");
        mailList.Add("subject", "m_subject");
        mailList.Add("body", "m_body");
        mailList.Add("reDate", "m_receiveDate");
        mailList.Add("receiveBy", "m_receiveBy");
        mailList.Add("receiveName", "m_receiveName");
        mailList.Add("createDate", "m_createDate");
        mailList.Add("cc", "m_copyTo");
        mailList.Add("to", "m_to");

        attList.Add("uid", "att_mailId");
        attList.Add("attNmame", "att_name");
        attList.Add("attPath", "att_path");
        attList.Add("attIsInline", "att_isInline");
        attList.Add("attContentID", "att_contentID");
        #endregion

        Int64 maxUID = MailHelper.GetMaxMailID(); //获取上一次接收邮件最大的ID

        if (maxUID <= 0)
        {
            return;
        }
        using (POP3_Client pop = new POP3_Client())
        {
            try
            {
                pop.Connect(_mailServer, 110, false);
                pop.Login(_isHelpAccount, _isHelpPassword);
                POP3_ClientMessageCollection messages = pop.Messages;
                //1415170251.6684.Qmail,S=866235：第一部分是递增的
                //最后一份邮件都不是最新的，要提示
                _mailId = messages[messages.Count - 1].UID;
                if (Convert.ToInt64(_mailId.Split('.')[0]) <= maxUID)
                {
                    return;
                }
                for (int i = messages.Count - 1; 0 <= i; i--)
                {
                    _mailId = messages[i].UID;
                    if (Convert.ToInt64(_mailId.Split('.')[0]) <= maxUID)
                        break;
                    #region 读取邮件

                    POP3_ClientMessage ms = messages[i];
                    if (ms != null)
                    {
                        mailNum++;    //有新邮件就+1
                        byte[] messageBytes = ms.MessageToByte();
                        Mail_Message mime_message = Mail_Message.ParseFromByte(messageBytes);
                        ArrayList arr = new ArrayList();
                        row = table.NewRow();
                        
                        try
                        {
                            _subject = mime_message.Subject.Trim() == string.Empty ? "无主题" : mime_message.Subject;
                        }
                        catch (Exception ex)
                        {
                            _subject = "无主题";
                        }
                        _from = mime_message.From == null ? "sender is null" : mime_message.From[0].Address;
                        try
                        {
                            _fromName = mime_message.From[0].DisplayName.Trim() == "" ? mime_message.From[0].Address : mime_message.From[0].DisplayName;
                        }
                        catch (Exception fe)
                        {
                            _fromName = "无名氏";
                        }
                        _to = "";
                        try 
                        { 
                            Mail_t_AddressList to=mime_message.To;
                             if (to.Count > 0 && to != null)
                            {
                                for (int ii = 0; ii < to.Mailboxes.Length; ii++)
                                {
                                    _to += to.Mailboxes[ii].Address + ";";
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            _to = _isHelpAccount;
                        }
                        _copyTo = "";
                        try
                        {
                            Mail_t_AddressList copyTo = mime_message.Cc; //获取的抄送人信息
                            if (copyTo.Count > 0 && copyTo != null)
                            {
                                for (int ii = 0; ii < copyTo.Mailboxes.Length; ii++)
                                {
                                    _copyTo += copyTo.Mailboxes[ii].Address + ";";
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            _copyTo = "";
                        }
                        _receiveDate = mime_message.Date;
                        //_body = mime_message.BodyHtmlText;
                        _body = mime_message.BodyText;
                        try
                        {
                            if (!string.IsNullOrEmpty(mime_message.BodyHtmlText))
                            {
                                _body = mime_message.BodyHtmlText;
                            }
                        }
                        catch
                        {
                            ;//Response.Write("<script>alert('HTMLBODY');</script>");//屏蔽编码出现错误的问题，错误在BodyText存在而BodyHtmlText不存在的时候，访问BodyHtmlText会出现
                        }
                        row["uid"] = _mailId;
                        row["from"] = _from;
                        row["fromName"] = _fromName;
                        row["subject"] = _subject;
                        row["reDate"] = _receiveDate;
                        row["createDate"] = DateTime.Now;
                        row["receiveBy"] = Convert.ToInt32(uId);
                        row["cc"] = _copyTo;
                        row["to"] = _to;
                        MIME_Entity[] attachments = mime_message.GetAttachments(true, true);
                        foreach (MIME_Entity entity in attachments)
                        {
                            attRow = atts.NewRow();
                            string cid;
                            int eIndex;
                            if (entity.ContentDisposition != null)
                            {
                                cid = entity.ContentID;
                                string _aName = entity.ContentDisposition.Param_FileName;//区别物理名和逻辑
                                _attName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + _aName;
                               string type=entity.ContentType.ValueToString();
                                if (File.Exists(attPath + _attName))
                                    File.Delete(attPath + _attName);
                                
                                if (!string.IsNullOrEmpty(_attName))
                                {
                                    string path = Path.Combine(attPath, _attName);
                                    try 
                                    {
                                        MIME_b_SinglepartBase byteObj = (MIME_b_SinglepartBase)entity.Body;
                                         Stream decodedDataStream = byteObj.GetDataStream();
                                         using (FileStream fs = new FileStream(path, FileMode.Create))
                                         {
                                             try
                                             {
                                                 LumiSoft.Net.Net_Utils.StreamCopy(decodedDataStream, fs, 4000);
                                             }
                                             catch (Exception e)
                                             {
                                                 ;
                                             }
                                         }
                                    }
                                    catch(Exception e)
                                    {
                                        if (type == "message/rfc822\r\n")
                                        {
                                            _attName += ".eml";
                                            entity.ToFile(attPath + _attName, null, null);
                                        }
                                    }

                                    
                                }
                                attRow["attPath"] = _attachPath + _attName;  //路径
                                if (_aName != null)
                                    attRow["attNmame"] = _aName;
                                else
                                    attRow["attNmame"] = _attName;
                                attRow["attIsInline"] = entity.ContentDisposition.DispositionType == MIME_DispositionTypes.Inline ? 1 : 0;
                            }
                            else
                            {
                                cid = entity.ContentID;
                                string _aName = entity.ContentType.Parameters["name"];
                                try
                                {
                                    eIndex = _aName.LastIndexOf(".");//有些图片没有后缀名，直接添加一个后缀
                                    if (eIndex == -1)
                                        _aName += ".png";
                                }
                                catch(Exception e)
                                {
                                    _aName = DateTime.Now.ToString("yyyyMMddhhmmssfff")+".png";
                                }
                                _attName = DateTime.Now.ToString("yyyyMMddhhmmssfff_") + _aName;
                                attRow["attPath"] = _attachPath + _attName;  //路径

                                if (File.Exists(attPath + _attName))
                                    File.Delete(attPath + _attName);
                                attRow["attNmame"] = _aName;
                                string enString = entity.ToString();
                                int a = enString.LastIndexOf(">");
                                string base64string = enString.Substring(a + 5);
                                byte[] bt = Convert.FromBase64String(base64string);
                                File.WriteAllBytes(attPath + _attName, bt);
                                attRow["attIsInline"] = 1;
                            }
                            attRow["uid"] = _mailId;
                            attRow["attContentID"] = entity.ContentID;
                            atts.Rows.Add(attRow);
                            try
                            {
                                if (cid!=null||cid.Trim() != string.Empty)
                                    _body = _body.Replace("cid:" + cid.Substring(1, cid.Length - 2), _attachPath + _attName);

                            }
                            catch (Exception e)
                            {
                                ;
                            }
                        }
                        //Body内容存储为一个文件
                        string _bodyFileName = DateTime.Now.ToFileTime().ToString() + ".html";
                        try
                        {
                            File.WriteAllText(bodyPath + _bodyFileName, _body);
                            // File.w
                        }
                        catch
                        {
                            ;
                        }

                        row["body"] = _bodyPath + _bodyFileName;
                        table.Rows.Add(row);
                    #endregion
                    }
                }
                if (table.Rows.Count > 0 && table != null)
                {
                    if (!MailHelper.ImporToServer(mailList, ConfigurationManager.AppSettings["SqlConn.Conn_WF"], table, "dbo.Mail_mstr_Temp"))
                    {
                        return;// this.Alert("邮件导入失败");
                    }
                    else
                    {
                        if (!MailHelper.SetMaxMailID(uId))
                        {
                            //this.Alert("邮件导入后复制失败！");
                            return;
                        }
                    }

                    if (atts.Rows.Count > 0 && atts != null)
                    {
                        if (!MailHelper.ImporToServer(attList, ConfigurationManager.AppSettings["SqlConn.Conn_WF"], atts, "Mail_atts_mstr"))
                        {
                            return;
                       //     this.Alert("邮件附件导入失败");
                        }
                    }
                }
             //   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "addNum", "$(\"#newMailNum\").val(\"" + mailNum + "\");", true);
            }
            catch (Exception e)
            {
                ;
            }
            pop.Disconnect();
        }
    }
    #endregion

    public static void GetMailPath(DateTime bDate, DateTime eDate, out DataTable bodyPath, out DataTable attPath)
    {
        string sp = "sp_mail_selectMailPath";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@bDate",bDate);
        parms[1] = new SqlParameter("@eDate", eDate);
        attPath = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sp, parms).Tables[0];
        bodyPath = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sp, parms).Tables[1];
    }
    /// <summary>
    /// 删除下载好的邮件
    /// </summary>
    /// <param name="bDate"></param>
    /// <param name="eDate"></param>
    public static void DeleteMail(DateTime bDate, DateTime eDate)
    {
        string sp = "sp_mail_deleteMails";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@bDate", bDate);
        parms[1] = new SqlParameter("@eDate", eDate);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sp, parms);
    }
    /// <summary>
    /// 删除服务器上的邮件
    /// </summary>
    /// <param name="mails"></param>
    public static void DeleteServerMail(ArrayList mails)
    {
        if(mails.Count>0)
        {
            string _mailServer = ConfigurationManager.AppSettings["mailServer"];
            string _isHelpAccount = ConfigurationManager.AppSettings["IsHelpEmail"];
            string _isHelpPassword = Encoding.Default.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["IsHelpEmailPwd"].ToString()));
            using (POP3_Client pop = new POP3_Client())
            {
                try
                {
                    pop.Connect(_mailServer, 110, false);
                    pop.Login(_isHelpAccount, _isHelpPassword);
                    POP3_ClientMessageCollection messages = pop.Messages;
                   if(mails.Count>0)
                    {
                        for(int j=0;j<messages.Count-1;j++)
                        {
                            POP3_ClientMessage mail=messages[j];
                            if(mails.Contains(mail.UID))
                            {
                                mail.MarkForDeletion();
                            }
                        }
                    }
                 }
                catch(Exception e)
                {
                    ;
                }
            }
        }
    }
    public static DataTable getUsersMail(string key)
    {
        string sp = "sp_mail_selectUserMail";
        SqlParameter parms = new SqlParameter("@key",key);
        return SqlHelper.ExecuteDataset(strConn,CommandType.StoredProcedure,sp,parms).Tables[0];
    }
    #region 检测处理各种单据
    /*
     * *********************
     *关于加工单、销售单、采购单处理的函数 
     * **********************
    */
    public static DataTable CheckPodStatus(string podNbr)
    {
        string sp = "sp_mail_CheckPodStatus";
        SqlParameter[] parms = new SqlParameter[1];
        parms[0] = new SqlParameter("@poNbr", podNbr);
        return SqlHelper.ExecuteDataset(strConn,CommandType.StoredProcedure,sp,parms).Tables[0];
    }
    public static DataTable CheckSoStatus(string podNbr)
    {
        string sp = "sp_mail_CheckSoStatus";
        SqlParameter[] parms = new SqlParameter[1];
        parms[0] = new SqlParameter("@poNbr", podNbr);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    public static DataTable CheckWoStatus(string podNbr)
    {
        string sp = "sp_mail_CheckWoStatus";
        SqlParameter[] parms = new SqlParameter[1];
        parms[0] = new SqlParameter("@poNbr", podNbr);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    #endregion
}