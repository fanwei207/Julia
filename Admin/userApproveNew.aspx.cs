using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using adamFuncs;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Web.Mail;
using System.Configuration;

public partial class Admin_userApproveNew : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["userNo"] != null)
            {
                txtUserNo.Text = Request.QueryString["userNo"].ToString();
                txtUserName.Text = Request.QueryString["userName"].ToString();
            }
            string StrSql = "select isnull(email,'') from tcpc0.dbo.users where userid='" + Session["uID"] + "'";
            SqlDataReader reader1 = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, StrSql);
            while (reader1.Read())
            {
                txtemail.Text = reader1[0].ToString();
            }
            reader1.Close();


        }
    }
    protected void btn_choose_Click(object sender, EventArgs e)
    {
        txt_approveName.ForeColor = System.Drawing.Color.Black;
        ltlAlert.Text = "var w=window.open('/AccessApply/admin_accessChooseApprove.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btn_next_Click(object sender, EventArgs e)
    {
        if (txt_approveName.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请选择下一审批人！')";
            return;
        }
        if (txb_sug.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请填写意见！')";
            return;
        }
        if (txt_approveEmail.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请填写审批者的邮箱！')";
            return;
        }
        //if (txtemail.Text.Trim().Length == 0)
        //{
        //    ltlAlert.Text = "alert('请填写自己的邮箱！')";
        //    return;
        //}
        if (submitUserApprove())
        {
            if (!this.sendEmail())
            {
                ltlAlert.Text = "alert('申请成功，但是邮件发送失败！')";
                return;
            }
            Response.Redirect("userApproveList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else
        {
            ltlAlert.Text = "alert('提交失败！')";
            return;
        }
    }


    protected bool sendEmail()
    {
        MailMessage mail = new MailMessage();
        StringBuilder sb = new StringBuilder();
        if (txtemail.Text.Trim().Length == 0)
        {
            mail.From = ConfigurationManager.AppSettings["AdminEmail"].ToString();
        }
        else
        {
            mail.From = txtemail.Text.Trim();
        }
        mail.To = txt_approveEmail.Text.Trim();
        try
        {
            mail.Subject = "100系统-->非A类员工入职审批";
            sb.Append("<html>");
            sb.Append("<body style="+ "font-size:12px" +">");
            sb.Append("<form>");
            sb.Append("你好，");
            sb.Append("<br>");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"].ToString() + "向您递交了一份非A类员工(工号：" + txtUserNo.Text + "  姓名：" + txtUserName.Text + ")的入职审批的申请，需要您处理!" + "<br>");
            sb.Append("<br>");
            sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
            sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/'>"+baseDomain.getPortalWebsite()+"/</a>的HR2--员工入职审批--入职申请列表");
            sb.Append("</body>");
            sb.Append("</form>");
            sb.Append("</html>");
            mail.BodyFormat = MailFormat.Html;
            mail.Body = Convert.ToString(sb);
            BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
            //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
            //SmtpMail.Send(mail);
            sb.Remove(0, sb.Length);
            return true;
        }
        catch
        {
            return false;
        }
    
    }


    protected bool submitUserApprove()
    {
        try
        {
            string strName = "sp_userApprove_submitUserApprove";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@userNo", txtUserNo.Text);
            param[1] = new SqlParameter("@approveID", txt_approveID.Text.Trim());
            param[2] = new SqlParameter("@approveName", txt_approveName.Text.Trim());
            param[3] = new SqlParameter("@content", txb_sug.Text.Trim());
            param[4] = new SqlParameter("@uID", Session["uID"]);
            param[5] = new SqlParameter("@uName", Session["uName"]);
            param[6] = new SqlParameter("@deptID", Session["deptID"]);
            param[7] = new SqlParameter("@plantCode", Session["plantCode"]);
            param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[8].Value);

        }
        catch (Exception ex)
        {
            return false;
        }

    }

    protected SqlDataReader getUserApproveMstr()
    {
        try
        {
            string strName = "sp_userApprove_selectUserApproveMstr";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", Request.QueryString["mid"]);
            return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, strName, param);

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("userApproveNewList.aspx");
    }
}