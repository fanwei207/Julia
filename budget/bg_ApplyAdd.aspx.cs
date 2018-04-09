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
using System.Web.Mail;
using System.Text;
using adamFuncs;
using BudgetProcess;

public partial class bg_ApplyAdd :BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        { 
            btnSave.Attributes.Add("onclick", "return CheckAll();");

            LoadSubAccount();
            LoadCostCenter();
        }

        #region Check All JavaScript Code
        string scr = @"<script>
            function CheckAll()
            {
                try 
                {   
                    var receipt = document.getElementById('" + this.txtReceipt.ClientID + "').value;";
                    scr = scr + @"
                    var sc = document.getElementById('" + this.ddlSub.ClientID + "');";
                    scr = scr + @"
                    var cc = document.getElementById('" + this.ddlCC.ClientID + "');";
                    scr = scr + @"
                    var notes = document.getElementById('" + this.txtNotes.ClientID + "').value;";
                    scr = scr + @"
                    var amount = document.getElementById('" + this.txtAmount.ClientID + "').value;";
                    scr = scr + @"
                    if(receipt == '')
                    {
                        alert('�����˲���Ϊ��!');
                        return false;
                    }
                    if(sc.selectedIndex == 0)
                    {
                        alert('������;����Ϊ��!');
                        return false;
                    }
                    if(cc.selectedIndex == 0)
                    {
                        alert('�ɱ����Ĳ���Ϊ��!');
                        return false;
                    } 
                    if(notes == '')
                    {
                        alert('�������ݲ���Ϊ��!');
                        return false;
                    }   
                    if(amount == '')
                    {
                        alert('�������Ϊ��!');
                        return false;
                    }        
                    if(isNaN(amount))
                    {
                        alert('������ֻ��������!');
                        return false;
                    }
                    if(amount <= 0)
                    {
                        alert('�������С����!');
                        return false;
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

    protected void LoadSubAccount()
    {
        if (ddlSub.Items.Count > 0) ddlSub.Items.Clear();

        ddlSub.DataSource = Budget.getSubAccount();
        ddlSub.DataBind();
        ddlSub.SelectedIndex = 0;
    }

    protected void LoadCostCenter()
    {
        if (ddlCC.Items.Count > 0) ddlCC.Items.Clear();

        ddlCC.DataSource = Budget.getCostCenter();
        ddlCC.DataBind();
        ddlCC.SelectedIndex = 0;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/budget/bg_ApplyList.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //�������
        string strTo = txtReceiptID.Text.Trim();
        string strToName = txtReceiptValue.Text.Trim();
        string strFrom = Convert.ToString(Session["uID"]);
        string strFromName = Convert.ToString(Session["uName"]);
        string strToEmail = txtReceiptEmail.Text.Trim();
        string strCopyToEmail = txtCopyToEmail.Text.Trim();
        string strCopyTo = txtCopyToValue.Text.Trim();
        string strAcc = lblAccountValue.Text.Trim();
        string strSub = ddlSub.SelectedItem.Value.ToString();
        string strCC = ddlCC.SelectedItem.Value.ToString();
        string strDesc = ddlSub.SelectedItem.Text.Trim();
        string strNotes = txtNotes.Text.Trim().Replace("\n", "<br>");
        string strAmount = txtAmount.Text.Trim();
        string strFromMail = string.Empty;
        bool isSuccess = true;

        if (strFrom == strTo)
        {
            ltlAlert.Text = "alert('�����Լ��ύ���Լ�!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
            return;
        }

        strDesc = strDesc.Substring(0, strDesc.IndexOf("("));

        long AID = Budget.InsertApply(strFrom, strFromName, strTo, strToName, strCopyTo, strAcc, strSub, strCC, strNotes, strAmount, strDesc);
        if ( AID <= 0)
        {
            ltlAlert.Text = "alert('��������ʧ��!'); ";
            ltlAlert.Text += " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value; ";
            ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";
        }
        else
        {
            try
            {
                strFromMail = Convert.ToString(Session["email"]).Length == 0 ? ConfigurationManager.AppSettings["AdminEmail"].ToString() : Convert.ToString(Session["email"]);
                strSub = ddlSub.SelectedItem.Text.ToString();
                strCC = ddlCC.SelectedItem.Text.ToString();

                StringBuilder sb = new StringBuilder();

                MailMessage mail = new MailMessage();
                mail.To = strToEmail;
                if (strCopyToEmail.Length > 0)
                    mail.Cc = strCopyToEmail;
                mail.From = strFromMail;
                mail.Subject = "100ϵͳ-->��������";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + txtReceiptValue.Text.Trim() + ":<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��������:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strNotes.Trim().Replace("<br>", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�˻�:" + strAcc.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;������;:" + strSub.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ɱ�����:" + strCC.Trim() + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;������:" + strAmount.Trim() + "<br><br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����ʾ.<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;������:" + Convert.ToString(Session["uName"]) + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�������:<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyEvaluate.aspx?id=" + AID.ToString() + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/budget/bg_ApplyDetail.aspx?id=" + AID.ToString() + "</a>");
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
                    ltlAlert.Text = "alert('����ɹ����ʼ��ѷ���!'); window.location.href='/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString() + "';";
                }
                else
                {
                    ltlAlert.Text = "alert('����ɹ�!'); window.location.href='/budget/bg_ApplyList.aspx?rm=" + DateTime.Now.ToString() + "';";
                }
            }
        }
    }

    protected void ddlSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value;";
        ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";

        if (ddlSub.SelectedIndex != 0 && ddlCC.SelectedIndex != 0)
        {
            getAccount();
        }
        else
        {
            lblDeptValue.Text = "";
            lblAccountValue.Text = "";
            lblCumulationValue.Text = "";
            lblBudgetValue.Text = "";
            btnSave.Enabled = true;
        }
    }
    
    protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltlAlert.Text = " document.getElementById('txtReceipt').value=document.getElementById('txtReceiptValue').value;";
        ltlAlert.Text += " document.getElementById('txtCopyTo').value=document.getElementById('txtCopyToValue').value; ";

        if (ddlCC.SelectedIndex != 0)
        {
            string[] strValue = ddlCC.SelectedItem.Text.Trim().Split(',');
            lblDeptValue.Text = strValue[1].ToString();

            if (ddlSub.SelectedIndex != 0)
            {
                getAccount();
            }
        }
        else
        {
            lblDeptValue.Text = "";
            lblAccountValue.Text = "";
            lblCumulationValue.Text = "";
            lblBudgetValue.Text = "";
            btnSave.Enabled = true;
        }
    }

    protected void getAccount()
    {
        string strSub = ddlSub.SelectedItem.Value.ToString();
        string strCC = ddlCC.SelectedItem.Value.ToString();
        string strValue = Budget.getAccount(strSub, strCC);

        lblAccountValue.Text = strValue;
        if (strValue.Length == 0)
        {
            ltlAlert.Text += "alert('û���ҵ��������˻����!');";
            btnSave.Enabled = false;
            lblCumulationValue.Text = "";
            lblBudgetValue.Text = "";
        }
        else
        {
            //����Ԥ��ֵ
            strValue = string.Empty;
            strValue = Budget.getBudgetValue(strSub, strCC);
            lblBudgetValue.Text = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));
            //�ۼ�����ֵ
            strValue = string.Empty;
            strValue = Budget.getCumulation(strSub, strCC);
            lblCumulationValue.Text = strValue.Length == 0 ? "0" : string.Format("{0:N2}", Convert.ToDecimal(strValue));

            btnSave.Enabled = true;
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
}
