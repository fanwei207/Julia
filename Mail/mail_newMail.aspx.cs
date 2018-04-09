using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Mail_mail_newMail : System.Web.UI.Page
{
    public string basePath;
    protected void Page_Load(object sender, EventArgs e)
    {
         basePath = Server.MapPath("/TecDocs/ITSupport/Attachs/").Replace("\\", "/");
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string att = hidAtt.Value;
        string _from = System.Configuration.ConfigurationManager.AppSettings["IsHelpEmail"];
        string _to = txtTo.Text.Trim();
        string _copyTo = txtCopyTo.Text.Trim();
        string _subject=txtSubject.Text.Trim();
        string _body = txtEditor.Text;
        if (MailHelper.SendEmail(_from, _to, _copyTo, _subject, _body, att, basePath))
        {
            Response.Write("<script>alert('发送成功！');</script>");
            txtCopyTo.Text = "";
            txtEditor.Text = "";
            txtSubject.Text = "";
            txtTo.Text = "";
            hidAtt.Value = "";
        }
        else
        {
            Response.Write("<script>alert('请重新发送！');</script>");
        }
    }
}