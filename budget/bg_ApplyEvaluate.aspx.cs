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
using System.Web.Mail;
using System.Text;
using adamFuncs;
using BudgetProcess;

public partial class bg_ApplyEvaluate : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        { 
            if (Request.QueryString["aid"] == null)
            {
                Response.Redirect("/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                btnPass.Attributes.Add("onclick", "return CheckPass();");
                btnPassClose.Attributes.Add("onclick", "return confirm('确认要通过并关闭该申请吗？');");
                btnNoPass.Attributes.Add("onclick", "return confirm('确认要不通过该申请吗？');");

                BindData();
            }
        }

        txtContentValue.Attributes.Add("style", "height:auto; overflow:visible; border-style:none; border-width:0px");

        #region Check All JavaScript Code
        string scr = @"<script>
            function CheckPass()
            {
                try 
                {   
                    var receipt = document.getElementById('" + this.txtReceipt.ClientID + "').value;";
                    scr = scr + @"
                    if(receipt == '')
                    {
                        alert('接收人不能为空!');
                        return false;
                    }
                    else
                    {
                        return confirm('确认要通过该申请吗？');
                    }
                }
                catch(e)
                {
                    //alert(e.description);  
                }
                finally{}
            }            
            </script>";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "checkpass", scr);

        #endregion 
    }

    protected void BindData()
    {
        //定义参数
        string strAID = Convert.ToString(Request.QueryString["aid"]);
        string strSub = string.Empty;
        string strCC = string.Empty;
        string strValue = string.Empty;
        bool isClose = false;

        SqlDataReader reader = Budget.getApplyEvaluate(strAID);
        while (reader.Read())
        {
            lblApplicantValue.Text = reader[0].ToString();
            txtContentValue.Text = reader[1].ToString().Replace("<br>", "\n");
            lblAccountValue.Text = reader[2].ToString();
            lblAmountValue.Text = string.Format("{0:#,##0.00}", Convert.ToDecimal(reader[3]));
            EvaluateID.Value = reader[4].ToString();
            isClose = Convert.ToBoolean(reader[9]);
            strSub = reader[10].ToString();
            strCC = reader[11].ToString();

            strValue = Budget.getBudgetValue(strSub, strCC);
            lblBudgetValue.Text = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));
            strValue = string.Empty;
            strValue = Budget.getCumulation(strSub, strCC);
            lblCumulationValue.Text = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));
        }
        reader.Close();

        gvEvaluate.DataSource = Budget.getApplyEvaluateInfo(strAID);
        gvEvaluate.DataBind();

        if (isClose)
        {
            btnPass.Enabled = false;
            btnNoPass.Enabled = false;
            btnPassClose.Enabled = false;
            btnReceipt.Enabled = false;
            btnCopyTo.Enabled = false;

            ltlAlert.Text = "alert('此申请已经关闭!');";
        }
        else
        {
            if (EvaluateID.Value.ToString() == Convert.ToString(Session["uID"]))
            {

                btnPass.Enabled = true;
                btnNoPass.Enabled = true;

                if (chk.securityCheck(Convert.ToString(Session["uID"]), Convert.ToString(Session["uRole"]), Convert.ToString(Session["orgID"]), "8034", true, false) > 0)
                {
                    btnPassClose.Enabled = true;
                }
                else
                {
                    btnPassClose.Enabled = false;
                }

            }
            else
            {
                btnPass.Enabled = false;
                btnPassClose.Enabled = false;
                btnNoPass.Enabled = false;
                btnReceipt.Enabled = false;
                btnCopyTo.Enabled = false;

            }
        }
    }

    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/budget/bg_User.aspx?type=0&rm=" + DateTime.Now;
        ltlAlert.Text += "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0'); w.focus();";
    }

    protected void btnCopyTo_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/budget/bg_User.aspx?type=1&rm=" + DateTime.Now;
        ltlAlert.Text += "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0'); w.focus();";
    }
    
    protected void btnPass_Click(object sender, EventArgs e)
    {
        //定义参数
        string strAID = Convert.ToString(Request.QueryString["aid"]);
        string strTID = txtReceiptID.Text.Trim();
        string strToName = txtReceiptValue.Text.Trim();
        string strToEmail = txtReceiptEmail.Text.Trim();
        string strCopyTo = txtCopyToValue.Text.Trim();
        string strCopyToEmail = txtCopyToEmail.Text.Trim();
        string strNotes = txtNotes.Text.Trim();
        long intUID = Convert.ToInt64(Session["uID"]);
        string strFromMail = string.Empty;
        string strFromName = string.Empty;
        string strAcc = string.Empty;
        string strSub = string.Empty;
        string strCC = string.Empty;
        string strAmount = lblAccountValue.Text.Trim();
        int isPass = 1;
        int isClose = 0;
        bool isSuccess = true;

        if (strTID == Convert.ToString(Session["uID"]))
        {
            ltlAlert.Text = "alert('不能自己提交给自己!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
            return;
        }

        if (Budget.UpdateApplyDetail(strAID, strTID, strToName, strCopyTo, strNotes, isPass, isClose, intUID))
        {
            try
            {
                SqlDataReader reader = Budget.getApplyEvaluate(strAID);
                while (reader.Read())
                {
                    strAcc = reader[5].ToString().Trim();
                    strSub = reader[6].ToString().Trim();
                    strCC = reader[7].ToString().Trim();
                    strFromName = reader[0].ToString().Trim();
                    strCopyToEmail = reader[8].ToString().Length == 0 ? strCopyToEmail : reader[8].ToString() + "," + strCopyToEmail;
                }
                reader.Close();

                strFromMail = Convert.ToString(Session["email"]).Length == 0 ? ConfigurationManager.AppSettings["AdminEmail"].ToString() : Convert.ToString(Session["email"]);

                StringBuilder sb = new StringBuilder();

                MailMessage mail = new MailMessage();
                mail.To = strToEmail;
                if (strCopyToEmail.Length > 0)
                    mail.Cc = strCopyToEmail;
                mail.From = strFromMail;
                mail.Subject = "100系统-->费用申请";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + txtReceiptValue.Text.Trim() + ":<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请内容:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strNotes.Trim().Replace("<br>", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;账户:" + strAcc.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;费用用途:" + strSub.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;成本中心:" + strCC.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请金额:" + strAmount.Trim() + "<br><br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请批示.<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请人:" + strFromName + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核结论: 通过<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;相关链接:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyEvaluate.aspx?id=" + strAID + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyDetail.aspx?id=" + strAID + "</a>");
                if (strFromMail == ConfigurationManager.AppSettings["AdminEmail"].ToString())
                {
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请不要回复此邮件!");
                }
                sb.Append("</form>");
                sb.Append("</html>");
                mail.BodyFormat = MailFormat.Html;
                mail.Body = Convert.ToString(sb);
                BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                //SmtpMail.Send(mail);
                sb.Remove(0, sb.Length);
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                if (isSuccess)
                {
                    ltlAlert.Text = "alert('审核操作成功，邮件已发送!'); window.location.href='/budget/bg_ApplyEvaluate.aspx?aid=" + strAID + "&rm=" + DateTime.Now.ToString() + "';";
                }
                else
                {
                    ltlAlert.Text = "alert('审核操作成功!'); window.location.href='/budget/bg_ApplyEvaluate.aspx?aid=" + strAID + "&rm=" + DateTime.Now.ToString() + "';";
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('审核操作失败!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString(), true);
    }

    protected void btnNoPass_Click(object sender, EventArgs e)
    {
        //定义参数
        string strAID = Convert.ToString(Request.QueryString["aid"]);
        string strTID = txtReceiptID.Text.Trim();
        string strToName = txtReceiptValue.Text.Trim();
        string strToEmail = txtReceiptEmail.Text.Trim();
        string strCopyTo = txtCopyToValue.Text.Trim();
        string strCopyToEmail = txtCopyToEmail.Text.Trim();
        string strNotes = txtNotes.Text.Trim();
        long intUID = Convert.ToInt64(Session["uID"]);
        string strFromMail = string.Empty;
        string strFromName = string.Empty;
        string strAcc = string.Empty;
        string strSub = string.Empty;
        string strCC = string.Empty;
        string strAmount = lblAccountValue.Text.Trim();
        int isPass = -1;
        int isClose = 1;
        bool isSuccess = true;

        if (strTID == Convert.ToString(Session["uID"]))
        {
            ltlAlert.Text = "alert('不能自己提交给自己!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
            return;
        }

        if (Budget.UpdateApplyDetail(strAID, strTID, strToName, strCopyTo, strNotes, isPass, isClose, intUID))
        {
            try
            {
                SqlDataReader reader = Budget.getApplyEvaluate(strAID);
                while (reader.Read())
                {
                    strAcc = reader[5].ToString().Trim();
                    strSub = reader[6].ToString().Trim();
                    strCC = reader[7].ToString().Trim();
                    strFromName = reader[0].ToString().Trim();
                    strCopyToEmail = reader[8].ToString().Length == 0 ? strCopyToEmail : reader[8].ToString() + "," + strCopyToEmail;
                }
                reader.Close();

                strFromMail = Convert.ToString(Session["email"]).Length == 0 ? ConfigurationManager.AppSettings["AdminEmail"].ToString() : Convert.ToString(Session["email"]);

                StringBuilder sb = new StringBuilder();

                MailMessage mail = new MailMessage();
                mail.To = strToEmail;
                if (strCopyToEmail.Length > 0)
                    mail.Cc = strCopyToEmail;
                mail.From = strFromMail;
                mail.Subject = "100系统-->费用申请";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + txtReceiptValue.Text.Trim() + ":<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请内容:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strNotes.Trim().Replace("<br>", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;账户:" + strAcc.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;费用用途:" + strSub.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;成本中心:" + strCC.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请金额:" + strAmount.Trim() + "<br><br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请批示.<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请人:" + strFromName + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核结论: 不通过<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;此申请已关闭<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;相关链接:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyDetail.aspx?id=" + strAID + "</a>");
                if (strFromMail == ConfigurationManager.AppSettings["AdminEmail"].ToString())
                {
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请不要回复此邮件!");
                }
                sb.Append("</form>");
                sb.Append("</html>");
                mail.BodyFormat = MailFormat.Html;
                mail.Body = Convert.ToString(sb);
                BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                //SmtpMail.Send(mail);
                sb.Remove(0, sb.Length);
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                if (isSuccess)
                {
                    ltlAlert.Text = "alert('审核操作成功，邮件已发送!'); window.location.href='/budget/bg_ApplyEvaluate.aspx?aid=" + strAID + "&rm=" + DateTime.Now.ToString() + "';";
                }
                else
                {
                    ltlAlert.Text = "alert('审核操作成功!'); window.location.href='/budget/bg_ApplyEvaluate.aspx?aid=" + strAID + "&rm=" + DateTime.Now.ToString() + "';";
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('审核操作失败!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
        }
    }

    protected void btnPassClose_Click(object sender, EventArgs e)
    {
        //定义参数
        string strAID = Convert.ToString(Request.QueryString["aid"]);
        string strTID = txtReceiptID.Text.Trim().Length == 0 ? "0" : txtReceiptID.Text.Trim();
        string strToName = txtReceiptValue.Text.Trim();
        string strToEmail = txtReceiptEmail.Text.Trim();
        string strCopyTo = txtCopyToValue.Text.Trim();
        string strCopyToEmail = txtCopyToEmail.Text.Trim();
        string strNotes = txtNotes.Text.Trim();
        long intUID = Convert.ToInt64(Session["uID"]);
        string strFromMail = string.Empty;
        string strFromName = string.Empty;
        string strAcc = string.Empty;
        string strSub = string.Empty;
        string strCC = string.Empty;
        string strAmount = lblAccountValue.Text.Trim();
        int isPass = 1;
        int isClose = 1;
        bool isSuccess = true;

        if (strTID == Convert.ToString(Session["uID"]))
        {
            ltlAlert.Text = "alert('不能自己提交给自己!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
            return;
        }

        if (Budget.UpdateApplyDetail(strAID, strTID, strToName, strCopyTo, strNotes, isPass, isClose, intUID))
        {
            try
            {
                SqlDataReader reader = Budget.getApplyEvaluate(strAID);
                while (reader.Read())
                {
                    strAcc = reader[5].ToString().Trim();
                    strSub = reader[6].ToString().Trim();
                    strCC = reader[7].ToString().Trim();
                    strFromName = reader[0].ToString().Trim();
                    strCopyToEmail = reader[8].ToString().Length == 0 ? strCopyToEmail : reader[8].ToString() + "," + strCopyToEmail;
                }
                reader.Close();

                strFromMail = Convert.ToString(Session["email"]).Length == 0 ? ConfigurationManager.AppSettings["AdminEmail"].ToString() : Convert.ToString(Session["email"]);

                StringBuilder sb = new StringBuilder();

                MailMessage mail = new MailMessage();
                if (strToEmail.Length > 0)
                {
                    mail.To = strToEmail;
                    if (strCopyToEmail.Length > 0)
                        mail.Cc = strCopyToEmail;
                }
                else
                {
                    if (strCopyToEmail.Length > 0)
                        mail.To = strCopyToEmail;
                }                
                mail.From = strFromMail;
                mail.Subject = "100系统-->费用申请";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + txtReceiptValue.Text.Trim() + ":<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请内容:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strNotes.Trim().Replace("<br>", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;账户:" + strAcc.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;费用用途:" + strSub.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;成本中心:" + strCC.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请金额:" + strAmount.Trim() + "<br><br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请批示.<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请人:" + strFromName  + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核结论: 通过<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;此申请已关闭<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;相关链接:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyEvaluate.aspx?id=" + strAID + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyDetail.aspx?id=" + strAID + "</a>");
                if (strFromMail == ConfigurationManager.AppSettings["AdminEmail"].ToString())
                {
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请不要回复此邮件!");
                }
                sb.Append("</form>");
                sb.Append("</html>");
                mail.BodyFormat = MailFormat.Html;
                mail.Body = Convert.ToString(sb);
                BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                //SmtpMail.Send(mail);
                sb.Remove(0, sb.Length);
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                if (isSuccess)
                {
                    ltlAlert.Text = "alert('审核操作成功，邮件已发送!'); window.location.href='/budget/bg_ApplyEvaluate.aspx?aid=" + strAID + "&rm=" + DateTime.Now.ToString() + "';";
                }
                else
                {
                    ltlAlert.Text = "alert('审核操作成功!'); window.location.href='/budget/bg_ApplyEvaluate.aspx?aid=" + strAID + "&rm=" + DateTime.Now.ToString() + "';";
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('审核操作失败!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
        }
    }
}
