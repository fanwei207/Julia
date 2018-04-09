using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Drawing;

public partial class Mail_mail_detail : BasePage
{
    /// <summary>
    /// 邮件正文
    /// </summary>
    public string MAILBODY="";
    /// <summary>
    /// 邮件列表的索引
    /// </summary>
    public string TRINDEX = "0";
    //public string _imgPath = @"D:\tcp_cn\"; //附件存储的绝对地址
    public string _imgPath = @"D:\tcpcnew\";//正式库路径
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUID.Text = Request.QueryString["m_UID"].ToString();
            SqlDataReader dr = MailHelper.GetMailInfo(lblUID.Text, Session["uID"].ToString());
            MailHelper.AddReadStatus(lblUID.Text,Convert.ToInt32(Session["uId"].ToString()));
            if (dr.Read())
            {
                hidMailPath.Value = dr["m_body"].ToString();
                hidFrom.Value = dr["m_from"].ToString();
                hidTo.Value = System.Configuration.ConfigurationManager.AppSettings["IsHelpEmail"];
                hidCc.Value = dr["m_copyTo"].ToString();
                hidDate.Value = dr["m_receiveDate"].ToString();
                hidSubject.Value = dr["m_subject"].ToString();
                MAILBODY = File.ReadAllText(Server.MapPath(hidMailPath.Value));
                txtEditor.Text = MAILBODY;
               lblFrom.Text = hidFrom.Value;
              //  txtFromName.Text = dr["m_fromName"].ToString()+"<"+dr["m_from"].ToString()+">";
                lblFromName.Text = dr["m_fromName"].ToString();
                lblSendDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(hidDate.Value));
                lblSubject.Text = hidSubject.Value;
                lblCc.Text = dr["m_copyTo"].ToString();
                txtCc.Text = dr["m_copyTo"].ToString();
                lblTo1.Text = dr["m_to"].ToString();
                txtRecepient.Text = dr["m_to"].ToString();
            }

            dr.Dispose();
        }
    }

    protected void showAttachments()
    {
        System.Data.DataTable dr = MailHelper.GetAttachments(lblUID.Text,"");
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        ht.RenderBeginTag("table");
        ht.RenderBeginTag("tr");
        if (dr.Rows.Count > 0 && dr != null)
        {
            for(int i=0;i<dr.Rows.Count;i++)
            {
                ht.RenderBeginTag("td");
                ht.AddAttribute("href", dr.Rows[i].ItemArray[1].ToString());
                ht.RenderBeginTag("a");
                ht.WriteLine(dr.Rows[i].ItemArray[0].ToString());
                ht.RenderEndTag();
                ht.RenderEndTag();
            }
        }
        ht.RenderEndTag();
        ht.RenderEndTag();
        Response.Write(sw.ToString());
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (clickType.Value == "reply")  //回复
        {
            File.WriteAllText(Server.MapPath(hidMailPath.Value), txtEditor.Text);
            string _from = hidTo.Value;
            string _to;
            string _cc;
            _to = txtRecepient.Text.Trim();
            _cc = txtCc.Text.Trim(); 
            string _subject = "回复：" + hidSubject.Value;
            string mailBody = txtEditor.Text;
            MailHelper.GetImgSrcAndInsert(mailBody, lblUID.Text);//获取图片src
            if (upLoadFile.PostedFile.FileName.Trim() != string.Empty)
            {
                string path = upLoadFile.PostedFile.FileName.ToString();
                int i = path.LastIndexOf("\\");
                string name = path.Substring(i + 1);
                if (path.Trim().Length > 0)
                {
                    string serverPath = Server.MapPath(@"\Excel\");
                    path = serverPath + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "_" + name;
                    lblAttsPath.Text = lblAttsPath.Text + path + ";";
                    upLoadFile.PostedFile.SaveAs(path);
                    lblAtt.Text = lblAtt.Text + "<a href='" + path + "'>" + name + "</a>   ;";
                }
            }
            if (MailHelper.SendEmail(_from, _to, _cc, _subject, mailBody, "reply", lblUID.Text, lblAttsPath.Text.Trim(), _imgPath))
            {
                MailHelper.ModifyReadStatus(lblUID.Text, Convert.ToInt32(Session["uId"].ToString()), 1);
                this.Alert("发送成功！");
                lblAttsPath.Text = "";
            }
            else
            {
                this.Alert("邮件发送失败！");
                return;
            }
        }
        if (clickType.Value == "copyTo")  //转发
        {
            File.WriteAllText(Server.MapPath(hidMailPath.Value), txtEditor.Text);
            Dictionary<string, string> atts = new Dictionary<string, string>();
            //  string _from = MailHelper.GetUserEmail(Convert.ToInt32(Session["uId"].ToString())).ToString();
            string _from = System.Configuration.ConfigurationManager.AppSettings["IsHelpEmail"];
            string _to = txtTo.Text;
            string _cc = txtCopyTo.Text;
            string _subject = "转发：" + hidSubject.Value;
            string mailBody = txtEditor.Text;
            if (upLoadFile.PostedFile.FileName.Trim() != string.Empty)
            {
                string path = upLoadFile.PostedFile.FileName.ToString();
                int i = path.LastIndexOf("\\");
                string name = path.Substring(i + 1);
                if (path.Trim().Length > 0)
                {
                    string serverPath = Server.MapPath(@"\Excel\");
                    path = serverPath + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "_" + name;
                    lblAttsPath.Text = lblAttsPath.Text + path + ";";
                    upLoadFile.PostedFile.SaveAs(path);
                    lblAtt.Text = lblAtt.Text + "<a href='" + path + "'>" + name + "</a>   ;";
                }
            }
           // string _imgPath = @"D:\tcpcnew\";
          //  string _imgPath = @"D:\tcp_cn\";
            if (MailHelper.SendEmail(_from, _to, _cc, _subject, mailBody, "copyTo", lblUID.Text, lblAttsPath.Text.Trim(), _imgPath))
            {
              //  MailHelper.ModifyReadStatus(lblUID.Text, Convert.ToInt32(Session["uId"].ToString()), 1);
                this.Alert("发送成功！");
                lblAttsPath.Text = "";
            }
            else
            {
                this.Alert("邮件发送失败！");
                return;
            }
        }
    }
}