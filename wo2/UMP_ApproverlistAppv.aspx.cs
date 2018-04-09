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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using RD_WorkFlow;
using System.Net.Mail;
using System.Text;
using System.IO;
using adamFuncs;
using CommClass;

public partial class wo2_UMP_ApproverlistAppv : BasePage
{
    private adamClass adam = new adamClass();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("630090003", "通过权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblApplyDate.Text = Request.QueryString["mid"].ToString();
            btn_approve.Visible = this.Security["630090003"].isValid;
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }
        if (txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择提交人!'); ";
            return;
        }

        if (chkEmail.Checked)
        {
            if (txt_ApproveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('你选择的提交人没有邮箱!'); ";
                return;
            }
        }
        string strRet = lblApplyDate.Text;
        if (strRet.Length == 0)
        {
            ltlAlert.Text = "alert('请至少勾选一条数据!'); ";
            return;
        }
        string[] strsid;
        strRet = strRet.Substring(0, strRet.Length - 1);
        strsid = strRet.Split(',');


        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {
            bool isSuccess = true;
            foreach (var item in strsid)
            {
                string mid = item;
                AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, "1");

                //ltlAlert.Text += " window.location.href='UMP_ApproverList.aspx?code=" + txtUMPcode + "rm=" + DateTime.Now.ToString() + "';";

            }
            if (chkEmail.Checked)
            {
                isSuccess = SendMail2(txt_ApproveEmail.Text.Trim(), strApproverName, Session["Email"].ToString(), Session["uName"].ToString(), applyReason);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Apply successfully!');";
                ltlAlert.Text += "window.opener.location.reload();";
                ltlAlert.Text += "window.close();";
               // ltlAlert.Text += " window.location.href='UMP_ApproverList.aspx?rm=" + DateTime.Now.ToString() + "';";
                //BindProjData();
            }

        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            //BindProjData();
            return;
        }
    }
    public int AddProjQadLink(string mid, string strApprover, string strApproverName, string applyReason, int uID, string uName, string status)
    {
        string strSql = "sp_UMP_InsertProjQadApply";
        SqlParameter[] sqlParam = new SqlParameter[7];
        sqlParam[0] = new SqlParameter("@mid", mid);
        sqlParam[1] = new SqlParameter("@approverBy", strApprover);
        sqlParam[2] = new SqlParameter("@approverName", strApproverName);
        sqlParam[3] = new SqlParameter("@applyReason", applyReason);
        sqlParam[4] = new SqlParameter("@uId", uID);
        sqlParam[5] = new SqlParameter("@uName", uName);
        sqlParam[6] = new SqlParameter("@status", status);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
    }
    public bool SendMail2(string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string applyReason)
    {
        StringBuilder sb = new StringBuilder();

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
        MailAddress to;
        MailMessage mail = new MailMessage();
        mail.From = from;
        Boolean isSuccess = false;
        try
        {
            to = new MailAddress(toEmailAddress, strApproverName);
            mail.To.Add(to);
        }
        catch
        {
            // returnMessage = "the email address of " + toEmailAddress + "is incorrect.Pls correct!";
            return false;
        }

        if (applyEmailAddress != null && applyEmailAddress != "")
        {
            MailAddress cc = new MailAddress(applyEmailAddress, applyName);
            mail.CC.Add(cc);
        }

        try
        {
            mail.Subject = "[Notify]计划外出入库有需要你审批的数据";
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append(" 审批员你好  <br>");

            // sb.Append("     申请单号:" + projectCode + "<br>");
            sb.Append("     申请说明:" + applyReason + "<br>");
            sb.Append("     申请者: " + applyName + "在此项目添加了计划外出入库申请，请链接以下地址时查看并审批申请<br><br>");

            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/wo2/UMP_ApproverList.aspx' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/wo2/UMP_ApproverList.aspx</a>");
            sb.Append("</form>");
            sb.Append("</html>");

            mail.Body = Convert.ToString(sb);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            sb.Remove(0, sb.Length);
            isSuccess = true;
            //returnMessage = "Send Mail Success!";
        }
        catch (Exception ex)
        {
            isSuccess = false;
            // returnMessage = "Send Mail Failed!";
        }

        return isSuccess;


    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }
        string strRet = lblApplyDate.Text;
        if (strRet.Length == 0)
        {
            ltlAlert.Text = "alert('请至少勾选一条数据!'); ";
            return;
        }
        string[] strsid;
        strRet = strRet.Substring(0, strRet.Length - 1);
        strsid = strRet.Split(',');

        //string mid = lbl_id.Text;
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {

            foreach (var item in strsid)
            {
                string mid = item;
                AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, "2");
               // BindProjData();


            }



        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            //BindProjData();
            return;
        }
        ltlAlert.Text += "window.opener.location.reload();";
        ltlAlert.Text += "window.close();";
    }
    protected void btn_diaApp_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }
        string strRet = lblApplyDate.Text;
        if (strRet.Length == 0)
        {
            ltlAlert.Text = "alert('请至少勾选一条数据!'); ";
            return;
        }
        string[] strsid;
        strRet = strRet.Substring(0, strRet.Length - 1);
        strsid = strRet.Split(',');

        //string mid = lbl_id.Text;
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {

            foreach (var item in strsid)
            {
                string mid = item;
                AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, "-1");
                // BindProjData();


            }



        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            //BindProjData();
            return;
        }
        ltlAlert.Text += "window.opener.location.reload();";
        ltlAlert.Text += "window.close();";
    }
    protected void btn_Approver_Click(object sender, EventArgs e)
    {
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        ltlAlert.Text = "var w=window.open('UMP_getapprover.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
}