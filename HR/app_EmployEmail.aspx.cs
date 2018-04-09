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

public partial class HR_app_EmployEmail : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int plantcode = Convert.ToInt32(Session["PlantCode"].ToString());
            txtEmailTo.Text = Request.QueryString["userEmail"].ToString(); ;
            txtTopical.Text = "TCP强凌集团录用通知函";
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
            //信息初始化
            txtAppDate.Text = "三个月";

            txtArrYear.Text = DateTime.Now.Year.ToString();
            txtArrMonth.Text = DateTime.Now.Month.ToString();

            txtAppProc.Text = Request.QueryString["process"];
            txtEndYear.Text = DateTime.Now.Year.ToString();
            txtEndMonth.Text = DateTime.Now.Month.ToString();
            txtEndDay.Text = DateTime.Now.Day.ToString();
            txtEmailFrom.Text = Session["email"].ToString();
            
            //状态初始化
            txtAppYear.Enabled = false;
            txtEndYear.Enabled = false;
            txtEndMonth.Enabled = false;
            txtEndDay.Enabled = false;
            
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_InterviewList.aspx");
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (txtLeadership.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写汇报上级！')";
            return;
        }
        else if (txtAppDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写试用期期限！')";
            return;
        }
        else if (txtProMoney.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写试用期工资！')";
            return;
        }
        else if (txtArrYear.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写报道年！')";
            return;
        }
        else if (txtArrMonth.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写报道月！')";
            return;
        }
        else if (txtArrDay.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写报道日！')";
            return;
        }
        string Leadership = txtLeadership.Text;
        string AppDate = txtAppDate.Text;
        string ProMoney = txtProMoney.Text;
        string ArrDate = txtArrYear.Text + "年" + txtArrMonth.Text + "月" + txtArrDay.Text + "日";
        //发送录用通知函
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

        string phone = string.Empty;
        string username = Session["uName"].ToString().Substring(0, 1);
        int sexid = 13;
        string CH2 = string.Empty;
        SqlDataReader reader1 = GetPersonSexID(Convert.ToInt32(Session["uID"].ToString()));
        while (reader1.Read())
        {
            sexid = Convert.ToInt32(reader1["sexID"]);
        }
        if (sexid == 13)
        {
            CH2 = "先生";
        }
        else
        {
            CH2 = "女士";
        }
        SqlDataReader reader2 = GetReviewUserEmail(Convert.ToInt32(Session["uID"].ToString()));
        while (reader2.Read())
        {
            phone = Convert.ToString(reader2["phone"]);
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
        sb.Append(Request.QueryString["username"].ToString() + CH1 + "：" + "<br>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您好：</p>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "首先感谢您对强凌集团的信任和大力支持，我们非常荣幸的通知您：</p>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您的职位为 <b>" + txtAppProc.Text + "</b>，直接汇报上级为 <b>" + txtLeadership.Text + "</b></p>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "试用期为 <b>" + txtAppDate.Text + "</b> 试用期薪资为 <b>" + txtProMoney.Text + "</b></p>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "请您带好（新员工报到须知）所列证明文件，于<b>" + ArrDate + "</b>上午前来我公司人力资源部门办理报到手续。如果资料齐全，我们将与您签订为期<b>三</b>年的劳动合同/保密协议。" + "</p>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "相信您的加盟会给强凌集团带来新的活力，我们将一起努力，创造公司的未来，也愿您在强凌集团迎来辉煌的明天。</p>");
        sb.Append("<p style=" + " margin-left:500px;" + ">强凌集团人力资源中心<br>");
        sb.Append(txtEndYear.Text + "年" + txtEndMonth.Text + "月" + txtEndDay.Text + "日" + "</p>");
        //sb.Append("<br>");
        sb.Append("<p style=" + "color:Red;" + ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如果您对本录用通知函及附件中所列各事项确认无误并表示接受，请于3个工作日内回复此邮件，如果逾期公司未能收到您的回复或在面试和录用过程中提供的资料经调查与实际不符，本录用通知函将自动失效。</p>");
        sb.Append("<p align=" + "center" + ">------------------------------------------------------------------------------------------------------------------------" + "</p><br>");
        sb.Append("<p align=" + "center" + ">新员工报到须知</p><br>");
        sb.Append("亲爱的员工：<br>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您好！欢迎加盟我公司，在您正式报到之前，请准备以下文件，并请您在正式报到时交到公司人力资源部门：</p>");
        sb.Append("<ul>" +
                "<li>个人免冠一寸近照四张</li>" +
                "<li>身份证原件</li>" +
                "<li>学历证书及资格证书原件</li>" +
                "<li>体检健康证（如您在半年内有过体检，无须再体检，提供相应证明即可）</li>" +
                "<li>离职证明（劳动手册/退工单/档案保管卡/退休证/报到证）</li>" +
                "<li>建设银行账号</li>" +
                "<li>户口本第一页和本人页复印件</li>" +
                "<ol>□	居住证原件/副联（可选）</ol>" +
                "<ol>□	职称证书原件（可选）</ol>" +
                "<ol>□	驾驶证原件（可选）</ol>" +
                "<ol>□	上岗证原件（可选）</ol>" +
                "</ul>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如果您提供以上物品有任何困难， 对上述事项如有任何疑问，欢迎与人力资源部相关负责人联系。</p>");
        sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联系人: " + username + CH2 + "     联系电话: " + phone + "</p>");
        //sb.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备注：    </p>");
        sb.Append("<p>(备注用于填写由于特殊原因而导致报到日无法提供所有材料，个人承诺在何时将材料补齐等类似特殊情况的声明。)</p><br><br>");
        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");
        mail.BodyFormat = MailFormat.Html;
        mail.Body = Convert.ToString(sb);
        BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
        //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
        //SmtpMail.Send(mail);
        if(changeEmployEmailStatus(Request.QueryString["id"]))
        {
            ltlAlert.Text = "alert('邮件发送成功！')";
            return;
        }
    }
    private bool changeEmployEmailStatus(string id)
    {
        SqlParameter pram = new SqlParameter("@id", id);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_changeEmployEmailStatus", pram));
    }
    /// <summary>
    /// 获取人事信息
    /// </summary>
    /// <returns></returns>
    public SqlDataReader GetReviewUserEmail(int id)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", id);

        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_app_getPersonInfoPhone", param);
    }
    /// <summary>
    /// 获取人事信息
    /// </summary>
    /// <returns></returns>
    public SqlDataReader GetPersonSexID(int id)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@uid", id);

        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_app_getPersonSexID", param);
    }
}