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
using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;

public partial class HR_app_SendEmail : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strMailFrom = string.Empty;
            string strMailTo = string.Empty;

            //获取发件人邮箱
            SqlDataReader reader2 = GetReviewUserEmail(Request.QueryString["company"], Request.QueryString["department"], Request.QueryString["process"]);
            while (reader2.Read())
            {
                strMailTo += Convert.ToString(reader2["App_ReviewEmail"]) +  ",";
            }
            reader2.Close();

            MailMessage mail = new MailMessage();
            StringBuilder sb = new StringBuilder();
            //向审核人发送邮件
            strMailFrom = Session["email"].ToString();
            mail.To = strMailTo;
            mail.From = strMailFrom;

            mail.Subject = "员工入职通知";
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append("大家好：" + "<br>");
            sb.Append("     非常感谢各位在招募人才中所付出的努力，应聘" + Request.QueryString["process"] + "岗位的" + Request.QueryString["username"] + "将於" + Convert.ToDateTime(DateTime.Now.ToShortDateString()) + "正式入职，请各位知悉！");
            sb.Append("<br>");
            sb.Append("</body>");
            sb.Append("</form>");
            sb.Append("</html>");
            mail.BodyFormat = MailFormat.Html;
            mail.Body = Convert.ToString(sb);
            BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
            //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
            //SmtpMail.Send(mail);

            //向申请人发邮件
            MailMessage mail1 = new MailMessage();
            StringBuilder sb1 = new StringBuilder();

            mail1.To = GetAppUserEmail(Request.QueryString["company"].ToString(), Request.QueryString["department"].ToString(), Request.QueryString["process"].ToString());
            mail1.From = Session["email"].ToString();

            mail1.Subject = "员工入职通知";
            sb1.Append("<html>");
            sb1.Append("<body>");
            sb1.Append("<form>");
            sb1.Append("<br>");
            sb1.Append("您好:" + "<br>");
            sb1.Append("<br>");
            sb1.Append("您好，贵部门申请的" + Request.QueryString["process"] + "岗位有新员工加入！，经过慎重选拔后，现决定录取" + Request.QueryString["username"] + "，将於" + Convert.ToDateTime(DateTime.Now.ToShortDateString()) + "入职。友情提醒：请提前准备办公桌椅、电脑等办公设备和工作所需资料。");
            //sb1.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您部门申请的" + Request.QueryString["process"]  + "岗位," + "<br>");
            //sb1.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "有新员工加入！" + "<br>");
            //sb1.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "请做好接待新员工工作，谢谢！！！" + "<br>");
            sb1.Append("</body>");
            sb1.Append("</form>");
            sb1.Append("</html>");
            mail1.BodyFormat = MailFormat.Html;
            mail1.Body = Convert.ToString(sb1);
            BasePage.SSendEmail(mail1.From, mail1.To, mail1.Cc, mail1.Subject, mail1.Body);
            //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
            //SmtpMail.Send(mail1);

            Response.Redirect("app_InterviewList.aspx");
        }
    }
    /// <summary>
    /// 获取审核人邮箱
    /// </summary>
    /// <returns></returns>
    public SqlDataReader GetReviewUserEmail(string company, string department, string process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);
        
        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetReviewUserEmail", param);
    }
    /// <summary>
    /// 获取申请人邮箱
    /// </summary>
    /// <returns></returns>
    private string GetAppUserEmail(string company, string department, string process)
    {
        SqlParameter[] pram = new SqlParameter[3];
        pram[0] = new SqlParameter("@company", company);
        pram[1] = new SqlParameter("@department", department);
        pram[2] = new SqlParameter("@process", process);

        return Convert.ToString(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAppUserEmail", pram));
    }
}