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

public partial class bg_ApplyActual : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        { 
            if (Request.QueryString["aid"] == null)
            {
                if (Request.QueryString["fr"] == "baa")
                {
                    Response.Redirect("/budget/bg_ApplyActualList.aspx?rm=" + DateTime.Now.ToString(), true);
                }
                else
                {
                    Response.Redirect("/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString(), true);
                }

            }
            else
            {
                btnOK.Attributes.Add("onclick", "return CheckAll();");

                BindData();
            }
        }

        txtContentValue.Attributes.Add("style", "height:auto; overflow:visible; border-style:none; border-width:0px");

        #region Check All JavaScript Code
        string scr = @"<script>
            function CheckAll()
            {
                try 
                {   
                    var amount = document.getElementById('" + this.txtActual.ClientID + "').value;";
                    scr = scr + @"
                    if(amount == '')
                    {
                        alert('ʵ�ʽ���Ϊ��!');
                        return false;
                    }        
                    if(isNaN(amount))
                    {
                        alert('ʵ�ʽ��ֻ��������!');
                        return false;
                    }
                    if(amount <= 0)
                    {
                        alert('ʵ�ʽ���С����!');
                        return false;
                    }
                    else
                    {
                        return confirm('ȷ�ϸ������ʵ�ʷ�������?');
                    }
                }
                catch(e)
                {
                    //alert(e.description);  
                }
                finally{}
            }            
            </script>";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "checkall", scr);
        #endregion 
    }

    protected void BindData()
    {
        //�������
        string strAID = Convert.ToString(Request.QueryString["aid"]);
        string strSub = string.Empty;
        string strCC = string.Empty;
        string strValue = string.Empty;
        string strStatus = string.Empty;
        bool isClose = false;

        SqlDataReader reader = Budget.getApplyEvaluate(strAID);
        while (reader.Read())
        {
            lblApplicantValue.Text = reader[0].ToString();
            txtContentValue.Text = reader[1].ToString().Replace("<br>", "\n");
            lblAccountValue.Text = reader[2].ToString();
            lblAmountValue.Text = string.Format("{0:#,##0.00}", Convert.ToDecimal(reader[3]));
            isClose = Convert.ToBoolean(reader[9]);
            strSub = reader[10].ToString();
            strCC = reader[11].ToString();
            strStatus = reader[12].ToString();

            strValue = Budget.getBudgetValue(strSub, strCC);
            lblBudgetValue.Text = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));
            strValue = string.Empty;
            strValue = Budget.getCumulation(strSub, strCC);
            lblCumulationValue.Text = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));

            strValue = reader[13].ToString().Trim();
        }
        reader.Close();

        gvEvaluate.DataSource = Budget.getApplyEvaluateInfo(strAID);
        gvEvaluate.DataBind();

        if (isClose)
        {
            if (strStatus == "1")
            {
                if (chk.securityCheck(Convert.ToString(Session["uID"]), Convert.ToString(Session["uRole"]), Convert.ToString(Session["orgID"]), "8036", true, false) > 0)
                {
                    btnOK.Enabled = true;
                    btnReceipt.Enabled = true;
                    btnCopyTo.Enabled = true;

                    if (strValue.Length > 0)
                    {
                        btnOK.Enabled = false;
                        btnReceipt.Enabled = false;
                        btnCopyTo.Enabled = false;

                        txtActual.Text = string.Format("{0:N2}",Convert.ToDecimal(strValue));
                        txtActual.ReadOnly = true;
                    }
                }
                else
                {
                    btnOK.Enabled = false;
                    btnReceipt.Enabled = false;
                    btnCopyTo.Enabled = false;
                }
            }
            else
            {
                btnOK.Enabled = false;
                btnReceipt.Enabled = false;
                btnCopyTo.Enabled = false;

                ltlAlert.Text = "alert('������������˽���Ϊ��ͨ������¼��ʵ�ʷ�������!');";
            }
        }
        else
        {
            btnOK.Enabled = false;
            btnReceipt.Enabled = false;
            btnCopyTo.Enabled = false;

            ltlAlert.Text = "alert('��������δ���������ɲ���¼��ʵ�ʷ�������!');";
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
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["fr"] == "baa")
        {
            Response.Redirect("/budget/bg_ApplyActualList.aspx?rm=" + DateTime.Now.ToString(), true);
        }
        else
        {
            Response.Redirect("/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString(), true);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //�������
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
        string strActual = txtActual.Text.Trim();
        bool isSuccess = true;

        if (strTID == Convert.ToString(Session["uID"]))
        {
            ltlAlert.Text = "alert('�����Լ��ύ���Լ�!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
            return;
        }

        if (Budget.updateApplyActual(strAID, strActual))
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
                mail.Subject = "100ϵͳ-->�������� from " + strFromMail;
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + txtReceiptValue.Text.Trim() + ":<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��������:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strNotes.Trim().Replace("<br>", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�˻�:" + strAcc.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;���˻�:" + strSub.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ɱ�����:" + strCC.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;������:" + strAmount.Trim() + "<br><br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����ʾ.<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;������:" + strFromName + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;������ʵ�ʷ���: " + strActual.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�������Ѿ��ر�.<br>");
                if (strFromMail == ConfigurationManager.AppSettings["AdminEmail"].ToString())
                {
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�벻Ҫ�ظ����ʼ�!");
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
                    if (Request.QueryString["fr"] == "baa")
                    {
                        ltlAlert.Text = "alert('�����ɹ����ʼ��ѷ���!'); window.location.href='/budget/bg_ApplyActualList.aspx?rm=" + DateTime.Now.ToString() + "';";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('�����ɹ����ʼ��ѷ���!'); window.location.href='/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString() + "';";
                    }
                }
                else
                {
                    if (Request.QueryString["fr"] == "baa")
                    {
                        ltlAlert.Text = "alert('�����ɹ�!'); window.location.href='/budget/bg_ApplyActualList.aspx?rm=" + DateTime.Now.ToString() + "';";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('�����ɹ�!'); window.location.href='/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString() + "';";
                    }
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('����ʧ��!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
        }
    }
}
