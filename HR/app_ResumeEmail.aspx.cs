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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

public partial class HR_app_ResumeEmail : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCompany.Text = Request.QueryString["company"].ToString();
            txtDepartment.Text = Request.QueryString["department"].ToString();
            txtProcess.Text = Request.QueryString["process"].ToString();

            txtCompany.Visible = false;
            txtDepartment.Visible = false;
            txtProcess.Visible = false;
            labCompany.Visible = false;
            labDepartment.Visible = false;
            labprocess.Visible = false;

            txtTopical.Text = "应试人员简历上传通知";
            txtText.Text = "新上传的人员信息如下：";
            txtText.Enabled = false;

            //获取发件人邮箱
            SqlDataReader reader = GetUserEmail(Convert.ToInt32(Session["uID"]));
            if (reader.Read())
            {
                txtEmailFrom.Text = Convert.ToString(reader["email"]);
            }
            reader.Close();
            //获取收件人（也即申请人）邮箱
            SqlDataReader reader1 = GetAppUserEmail(txtCompany.Text, txtDepartment.Text, txtProcess.Text);
            if (reader1.Read())
            {
                txtEmailTo.Text = Convert.ToString(reader1["App_userEmail"]);
            }
            reader1.Close();
            //获取当前公司、部门、岗位下新上传应试人员简历的信息
            BindData();
        }
    }
    private void BindData()
    {
        divUserInfo.InnerHtml = string.Empty;
        DataTable dt = GetNoSendEmailList(txtCompany.Text, txtDepartment.Text, txtProcess.Text);

        if (dt == null)
        {
            ltlAlert.Text = "alert('没有新简历上传，无须发邮件!')";
            return;
        }
        else 
        {
            foreach (DataRow row in dt.Rows)
            {
                divUserInfo.InnerHtml += "&nbsp;" + row["userName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;"
                                        + row["sex"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;"
                                        + row["education"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;"
                                        + row["graduateSchool"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;"
                                        + row["professional"].ToString() + "专业&nbsp;";
                //divUserInfo.InnerHtml += row["sex"].ToString() + "&emsp;";
                //divUserInfo.InnerHtml += row["education"].ToString() + "&emsp;";
                //divUserInfo.InnerHtml += row["graduateSchool"].ToString() + "&emsp;";
                //divUserInfo.InnerHtml += row["professional"].ToString() + "&emsp;";
                //divUserInfo.InnerHtml += "&nbsp;消息：<br />";
                //divUserInfo.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["education"].ToString() + "";
                divUserInfo.InnerHtml += "<br />";
                //divUserInfo.InnerHtml += "<hr align=\"left\" style=\" width:90%; border-top:1px dashed #000; border-bottom:0px dashed #000; height:0px\"><br />";
            }
        }



        //gv.DataSource = dt;
        //gv.DataBind();
    }
    private DataTable GetNoSendEmailList( string company,string department, string process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetNoSendEmailList", param).Tables[0];
    }

    /// <summary>
    /// 获取当前用户的邮箱
    /// </summary>
    /// <param name="e"></param>
    public SqlDataReader GetUserEmail(int uID)
    {
        SqlParameter param = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_app_selectUseremail", param);
    }
    /// <summary>
    /// 获取收件人（也即申请人）的邮箱
    /// </summary>
    /// <param name="e"></param>
    public SqlDataReader GetAppUserEmail(string company, string department, string process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);

        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAppUserEmail", param);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_ResumeList.aspx?App_Company=" + txtCompany.Text + "&App_department=" + txtDepartment.Text + "&App_Process=" + txtProcess.Text);
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        MailMessage mail = new MailMessage();
        StringBuilder sb = new StringBuilder();

        mail.To = txtEmailTo.Text + ";" + txtEmailTo1.Text;
        mail.From = txtEmailFrom.Text;

        mail.Subject = txtTopical.Text;
        mail.BodyEncoding = Encoding.GetEncoding("UTF-8");
        sb.Append("<html>");
        sb.Append("<body>");
        sb.Append("<form>");
        sb.Append("<br>");
        sb.Append("您好:" + "<br>");
        sb.Append(txtEmaliContent.Text + "<br>");
        sb.Append(txtText.Text + "<br>");
        sb.Append("<br>");
        sb.Append(divUserInfo.InnerText + "<br>");
        sb.Append("请到菜单：工资结算--HR2 --> 员工入职审批>> --> 招聘列表中（简历）处理一下" + "<br>");
        sb.Append("     谢谢！" + "<br>");
        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");
        mail.BodyFormat = MailFormat.Html;
        mail.Body = Convert.ToString(sb);
        BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
        //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
        //SmtpMail.Send(mail);

        //更改已发送人的邮件状态 
        if (changeSendEmailStatus(txtCompany.Text, txtDepartment.Text, txtProcess.Text))
        {
            if (!judgeExistNoSendEmail(txtCompany.Text, txtDepartment.Text, txtProcess.Text))
            {
                ltlAlert.Text = "alert('邮件发送成功!')";
            }
        }
    }
    /// <summary>
    /// 更改已发送人的邮件状态
    /// </summary>
    /// <returns></returns>
    public bool changeSendEmailStatus(string company, string department, string process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_changeSendEmailStatus", param));
    }
    /// <summary>
    /// 判断是否存在违法邮件的应试人员
    /// </summary>
    /// <returns></returns>
    public bool judgeExistNoSendEmail(string company, string department, string process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeExistNoSendEmail", param));
    }
}