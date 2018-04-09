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

public partial class HR_app_InterviewEmail_ZQL : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEmailTo.Text = Request.QueryString["userEmail"].ToString(); ;
            //邮件主题
            txtTopical.Text = "TCP强凌集团人才招聘";
            int plantcode = Convert.ToInt32(Session["PlantCode"].ToString());
            string sex = Request.QueryString["sex"].ToString();
            string CH = string.Empty;
            if (sex == "男")
            {
                CH = "先生";
            }
            if (sex == "女")
            {
                CH = "女士";
            }


            labname.Text = Request.QueryString["username"].ToString();
            labch.Text = CH;
            txtEmailFrom.Text = Session["email"].ToString();
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (txtAppDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写面试日期！')";
            return;
        }
        else
        {
            if (txtAppTime.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请填写面试时间！')";
                return;
            }
        }
        string sex = Request.QueryString["sex"].ToString();
        string CH1 = string.Empty;
        if (sex == "男")
        {
            CH1 = "先生";
        }
        if (sex == "女")
        {
            CH1 = "女士";
        }

        string body = Request.QueryString["username"].ToString() + CH1 + "，您好:" + "<br>";
        body += "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "恭喜您在简历筛选中通过，即将进入初试，欢迎您来我公司面试。" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公司名称：镇江强凌电子有限公司" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "http://www.tcpi.com 或 http://www.tcp-china.com" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "面试地址：江苏省镇江市学府路200号（江苏大学对面）" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "面试时间：" + txtAppDate.Text + "  " + txtAppTime.Text + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "附近交通：（建议先通过百度地图查找适合您的路线）" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公交汽车：D1、3路、19路、D208路、60路、28路、29路、K201路等（到汝山站下往前300米，过红绿灯路口即到）" + "<br>";
        body += "         <a href='http://j.map.baidu.com/qxrTz' rel='external' target='_blank'>http://j.map.baidu.com/qxrTz</a>";
        body += "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如有任何疑问您可以通过下列联系方式咨询我，祝您工作生活皆愉快！" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如果不能前来请务必提前邮件或者电话通知我们取消面试或者另行安排面试时间，感谢您的配合！" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "------------------------------------------------------------" + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公司：TCP强凌集团-人力资源中心-" + txtName.Text + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "地址：" + txtAddress.Text + "<br>";
        body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "座机：" + txtPhone.Text;
        SendEmail222(txtEmailFrom.Text, txtEmailTo.Text, txtTopical.Text, body);

        //向申请人发邮件
        MailMessage mail1 = new MailMessage();
        StringBuilder sb1 = new StringBuilder();

        string mto = GetAppUserEmail(Request.QueryString["company"].ToString(), Request.QueryString["department"].ToString(), Request.QueryString["process"].ToString());
        string body1 = "<html>";
        body1 += "<body>";
        body1 += "<form>";
        body1 += "<br>";
        body1 += "您好:" + "<br>";
        body1 += "<br>";
        body1 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您部门申请的" + Request.QueryString["process"].ToString() + "岗位，经人力资源部初步筛选，现邀请：" + Request.QueryString["username"].ToString() + "于" + txtAppDate.Text + "  " + txtAppTime.Text + "参加面试，请知悉！为确保面试顺利进行，请您在此前阅读应聘者简历并做好面试提纲。" + "<br>";
        body1 += "</body>";
        body1 += "</form>";
        body1 += "</html>";
        SendEmail222(txtEmailFrom.Text, mto, txtTopical.Text, body1);

        if (!changeInterviewDate(txtAppDate.Text, Request.QueryString["company"].ToString(), Request.QueryString["department"].ToString(), Request.QueryString["process"].ToString(), Request.QueryString["username"].ToString(), txtAppTime.Text))
        {
            ltlAlert.Text = "alert('邮件已发送，已确定面试时间')";
            return;
        }
    }
    /// <summary>
    /// 获取申请人邮箱
    /// </summary>
    /// <param name="company"></param>
    /// <param name="department"></param>
    /// <param name="process"></param>
    /// <returns></returns>
    private string GetAppUserEmail(string company, string department, string process)
    {
        SqlParameter[] pram = new SqlParameter[3];
        pram[0] = new SqlParameter("@company", company);
        pram[1] = new SqlParameter("@department", department);
        pram[2] = new SqlParameter("@process", process);

        return Convert.ToString(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAppUserEmail", pram));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_InterviewList.aspx");
    }
    /// <summary>
    /// 确定面试时间
    /// </summary>
    /// <returns></returns>
    public bool changeInterviewDate(string interviewDate, string company, string department, string process, string username, string interviewTime)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@interviewDate", interviewDate);
        param[1] = new SqlParameter("@company", company);
        param[2] = new SqlParameter("@department", department);
        param[3] = new SqlParameter("@process", process);
        param[4] = new SqlParameter("@username", username);
        param[5] = new SqlParameter("@interviewTime", interviewTime);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_changeInterviewDate", param));
    }
    public static bool SendEmail222(string from, string to, string subject, string body)
    {
        try
        {
            MailAddress _mailFrom = new MailAddress(from);
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = _mailFrom;

            foreach (string _to in to.Split(';'))
            {
                if (!string.IsNullOrEmpty(_to))
                {
                    _mailMessage.To.Add(_to);
                }
            }
            _mailMessage.Subject = subject;
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
}