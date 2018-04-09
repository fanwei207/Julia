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


public partial class HR_app_InterviewEmail : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            int plantcode = Convert.ToInt32(Session["PlantCode"].ToString());
            txtEmailTo.Text = Request.QueryString["userEmail"].ToString();
            //string company = Request.QueryString["company"];
            //string department = Request.QueryString["department"];
            //string prcoess = Request.QueryString["prcoess"];
            txtTopical.Text = "TCP强凌集团人才招聘";
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
            if (plantcode == 1)
            {
                txtEmailFrom.Text = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            }
            if (plantcode == 2)
            {
                txtEmailFrom.Text = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            }
            if (plantcode == 5)
            {
                txtEmailFrom.Text = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            }
            if (plantcode == 8)
            {
                txtEmailFrom.Text = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            }
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
        
        MailMessage mail = new MailMessage();
        StringBuilder sb = new StringBuilder();

        mail.To = txtEmailTo.Text;
        mail.From = txtEmailFrom.Text;

        mail.Subject = txtTopical.Text;
        sb.Append("<html>");
        sb.Append("<body>");
        sb.Append("<form>");
        sb.Append("<br>");
        sb.Append(Request.QueryString["username"].ToString() + CH1 + "，您好:" + "<br>");
        sb.Append("<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "恭喜您在简历筛选中通过，即将进入初试，欢迎您来我公司面试。" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公司名称：上海强凌电子有限公司" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "http://www.tcpi.com 或 http://www.tcp-china.com" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "面试地址：上海市松江区泗泾镇望东南路139号" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "面试时间：" + txtAppDate.Text + "  " + txtAppTime.Text + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "附近交通：（建议先通过百度地图查找适合您的路线）" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "（1）轨道9号线在泗泾站3号口出站，向左走，过马路（书报亭附近）转乘坐46路车到莘潮家居站（望东南路口）下车。 或者直接乘坐出租车，大概17元左右即可到达。" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "（2）乘沪松线公交车到叶星站下，沿沪松公路向回走到望东南路口，再沿望东南路走到139号。或者直接乘坐出租车，大概17元左右即可到达。" + "<br>");
        sb.Append("<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如有任何疑问您可以通过下列联系方式咨询我，祝您工作生活皆愉快！" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如果不能前来请务必提前邮件或者电话通知我们取消面试或者另行安排面试时间，感谢您的配合！" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "------------------------------------------------------------" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公司：TCP强凌集团-人力资源中心-张女士 " + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "地址：上海市松江区泗泾镇望东南路139号" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "座机：021-67613241");//sb.Append("     谢谢！" + "<br>");
        //sb.Append("     具体菜单是：工资结算--HR2 --> 员工入职审批>> --> 招聘列表" + "<br>");
        //sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
        //sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2</a>");
        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");
        mail.BodyFormat = MailFormat.Html;
        mail.Body = Convert.ToString(sb);
        BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
        //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
        //SmtpMail.Send(mail);

        if (!changeInterviewDate(txtAppDate.Text, Request.QueryString["company"].ToString(), Request.QueryString["department"].ToString(), Request.QueryString["process"].ToString(), Request.QueryString["username"].ToString()))
        {
            ltlAlert.Text = "alert('邮件已发送，已确定面试时间')";
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_InterviewList.aspx");
    }
    /// <summary>
    /// 确定面试时间
    /// </summary>
    /// <returns></returns>
    public bool changeInterviewDate(string interviewDate,string company,string department,string process,string username)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@interviewDate", interviewDate);
        param[1] = new SqlParameter("@company", company);
        param[2] = new SqlParameter("@department", department);
        param[3] = new SqlParameter("@process", process);
        param[4] = new SqlParameter("@username", username);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, "sp_app_changeInterviewDate", param));
    }
}