using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using LumiSoft.Net.Log;
using LumiSoft.Net.MIME;
using LumiSoft.Net.Mime;
using LumiSoft.Net.Mail;
using LumiSoft.Net.POP3.Client;
using System.Web.UI.HtmlControls;

public partial class Mail_mail_oprating : BasePage
{
    public string bodyPath;
    public string attPath;
    public string uId;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtBegin.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            txtEnd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
            bodyPath = Server.MapPath("\\TecDocs\\ITSupport\\Mails\\").Replace("\\","/") ;
            attPath = Server.MapPath("\\TecDocs\\ITSupport\\Attachs\\").Replace("\\", "/");
            uId = Session["uId"].ToString();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("./mail_deleteMail.aspx");
    }
}
