using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using adamFuncs;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Web.Mail;
using System.Text;
using System.Configuration;

public partial class Admin_userApproveHandel : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_reject.Visible = this.Security["6080041"].isValid;
            btn_agree.Visible = this.Security["6080041"].isValid;
            SqlDataReader reader = getUserApproveMstr();
            while (reader.Read())
            {  
                lbl_applyname.Text = reader[0].ToString();
                lbl_dispcontent.Text = reader[1].ToString();
                lbl_applyEmail.Text = reader[2].ToString();
                lbl_userNo.Text = reader[3].ToString();
                lbl_userName.Text = reader[4].ToString();
                lbDomain.Text = reader[5].ToString();
                lkb.Text = reader[3].ToString();
            }
            string StrSql = "select isnull(email,'') from tcpc0.dbo.users where userid='"+ Session["uID"] + "'";
                    SqlDataReader reader1 = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, StrSql);
                    while( reader1.Read())
                    {
                        txtemail.Text = reader1[0].ToString();
                    }
                    reader1.Close();
            BindData();
            btn_agree.Attributes.Add("onclick", "return confirm('您确定要同意吗?')");
            btn_reject.Attributes.Add("onclick", "return confirm('您确定要拒绝吗?')");
        }
    }



    protected void BindData()
    {
        gv.DataSource = getUserApproveDetail();
        gv.DataBind();
    
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
        //if (txt_approveEmail.Text.Trim().Length == 0)
        //{
        //    ltlAlert.Text = "alert('请填写邮箱！')";
        //    return;
        //}
        if (insertNextApprove())
        {
            if (txt_approveEmail.Text.Trim().Length != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=" + "font-size:12px" + ">");
                sb.Append("<form>");
                sb.Append("你好，");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"].ToString() + "向您递交了一份非A类员工(工号：" + lbl_userNo.Text + "  姓名：" + lbl_userName.Text + ")入职审批的申请，需要您处理!" + "<br>");
                sb.Append("<br>");
                sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                sb.Append("         <a href='http://portal.tcp-china.com/'>http://portal.tcp-china.com/</a>的HR2--员工入职审批--入职申请列表");
                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
                if (!sendEmail(txtemail.Text.Trim(), txt_approveEmail.Text.Trim(), sb))
                {
                    ltlAlert.Text = "alert('提交成功，发送邮件失败！')";
                }
            }
            Response.Redirect("userApproveList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());

        }
        else
        {
            ltlAlert.Text = "alert('提交失败！')";
            return;
        }
    }
    protected bool insertNextApprove()
    {
        try
        {
            string strName = "sp_userApprove_insertNextApprove";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@id", Request.QueryString["mid"]);
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

    protected DataTable getUserApproveDetail()
    {
        try
        {
            string strName = "sp_userApprove_selectUserApproveDetail";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", Request.QueryString["mid"]);
            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strName, param).Tables[0];

        }
        catch (Exception ex)
        {
            return null;
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("userApproveList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }

    protected bool updateUserApprove(int type)
    {
        try
        {
            string strName = "sp_userApprove_updateUserApprove";
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@id", Request.QueryString["mid"]);
            param[1] = new SqlParameter("@content", txb_sug.Text.Trim());
            param[2] = new SqlParameter("@uID", Session["uID"]);
            param[3] = new SqlParameter("@uName", Session["uName"]);
            param[4] = new SqlParameter("@deptID", Session["deptID"]);
            param[5] = new SqlParameter("@plantID", Session["plantCode"]);
            param[6] = new SqlParameter("@type",type);
            param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[7].Value);

        }
        catch (Exception ex)
        {
            return false;
        }

    }
    protected void btn_reject_Click(object sender, EventArgs e)
    {
        if (!updateUserApprove(2))
        {
            ltlAlert.Text = "alert('拒绝失败！')";
            return;
        }
        else
        {
            if (lbl_applyEmail.Text.Trim().Length != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=" + "font-size:12px" + ">");
                sb.Append("<form>");
                sb.Append("你好，");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"].ToString() + "拒绝了您提交的非A类员工(工号：" + lbl_userNo.Text + "  姓名：" + lbl_userName.Text + ")入职审批的申请" + "<br>");
                sb.Append("<br>");
                sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                sb.Append("         <a href='http://portal.tcp-china.com/'>http://portal.tcp-china.com/</a>的HR2--员工入职审批--入职申请列表");
                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
                if (!sendEmail(txtemail.Text.Trim(), lbl_applyEmail.Text.Trim(), sb))
                {
                    ltlAlert.Text = "alert('拒绝成功，发送邮件失败！')";
                }

            }
            Response.Redirect("userApproveList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btn_agree_Click(object sender, EventArgs e)
    {
        if (!updateUserApprove(1))
        {
            ltlAlert.Text = "alert('同意失败！')";
            return;
        }
        else
        {
            if (lbl_applyEmail.Text.Trim().Length != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=" + "font-size:12px" + ">");
                sb.Append("<form>");
                sb.Append("你好，");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"].ToString() + "同意了您提交的非A类员工(工号：" + lbl_userNo.Text + "  姓名：" + lbl_userName.Text + ")入职审批的申请" + "<br>");
                sb.Append("<br>");
                sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                sb.Append("         <a href='http://portal.tcp-china.com/'>http://portal.tcp-china.com/</a>的HR2--员工入职审批--入职申请列表");
                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
                if (!sendEmail(string.Empty, lbl_applyEmail.Text.Trim(), sb))
                {
                    ltlAlert.Text = "alert('同意成功，发送邮件失败！')";
                }
                Response.Redirect("userApproveList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
            }
        }
    }

    protected bool sendEmail( string from, string to, StringBuilder sb)
    {
        MailMessage mail = new MailMessage();
        if (from.Length == 0)
        {
            mail.From = "";
        }
        else
        {
            mail.From = from;
        }
        mail.To = to;
        try
        {
            mail.Subject = "100系统-->非A类员工入职审批";
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

    protected void linkDetails_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('userApproveInfoNew.aspx?id=" + lkb.Text + "&plantID=" + lbDomain.Text + " ','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=600,top=0,left=0') ";

    }
}