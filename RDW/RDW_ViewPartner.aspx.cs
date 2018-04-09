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
using System.Net.Mail;
using System.Text;
using RD_WorkFlow;

public partial class RDW_ViewPartner : System.Web.UI.Page
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["did"] == null || Request.QueryString["mid"] == null)
            {
                ltlAlert.Text = " window.close();";
            }
            else
            {
                //定义参数
                string strMID = Convert.ToString(Request.QueryString["mid"]);
                string strDID = Convert.ToString(Request.QueryString["did"]);
                string strUID = Convert.ToString(Session["uID"]);
                //所有人都能看到
                //if (!rdw.CheckViewRDWDetailEdit(strMID, strDID, strUID))
                //{
                //    ltlAlert.Text = " window.close();";
                //}
                //else
                //{
                    RDW_Detail rd = rdw.SelectRDWDetailEdit(strDID);

                    lblProjectData.Text = rd.RDW_Project;
                    lblProdCodeData.Text = rd.RDW_ProdCode;
                    lblStepNameData.Text = rd.RDW_StepName;

                    LoadUser();
                //}
            }
        }
    }

    protected void LoadUser()
    {
        //定义参数
        string strDID = Convert.ToString(Request.QueryString["did"]);
        DataSet dst;

        chkUser.Items.Clear();

        dst = rdw.getPartner(strDID);
        if (dst.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            for (i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean( dst.Tables[0].Rows[i].ItemArray[3].ToString()) )
                {
                    chkUser.Items.Add(new ListItem(dst.Tables[0].Rows[i].ItemArray[1].ToString().Trim() + "~" + dst.Tables[0].Rows[i].ItemArray[2].ToString().Trim(), dst.Tables[0].Rows[i].ItemArray[0].ToString().Trim()));
                }
            }
        }
        dst = null;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkUser.Items.Count; i++)
        {
            if (chkAll.Checked)
            {
                chkUser.Items[i].Selected = true;
            }
            else
            {
                chkUser.Items[i].Selected = false;
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strReasons = txtReason.Text.Trim().Replace("\n", "<br>");
        if (strReasons.Trim().Length > 0)
        {
            strReasons = "Reason:" + strReasons.Replace("\n", "<br>");
        }
        else
        {
            ltlAlert.Text = "alert('Please enter the reason!'); ";
            return;
        }
        //定义参数
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strUID = Convert.ToString(Session["uID"]);
        string strUname = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]); 
        string strPartner = string.Empty;
        StringBuilder sb = new StringBuilder();
        bool isSuccess = false;
        bool isUpdate = true;
        int pos = 0;
        string strUserID = string.Empty;

        for (int i = 0; i < chkUser.Items.Count; i++)
        {
            if (chkUser.Items[i].Selected)
            {
                isSuccess = true;
                break;
            }
        }

        if (!isSuccess)
        {
            ltlAlert.Text = "alert('Please select partner!');";
            return;
        }

        if (chkEmail.Checked)
        {
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
            MailAddress to;
            MailMessage mail = new MailMessage();
            mail.From = from; 

            for (int i = 0; i < chkUser.Items.Count; i++)
            {
                if (chkUser.Items[i].Selected)
                {
                    try
                    {
                        string strMailTo = chkUser.Items[i].Text.Trim();
                        pos = strMailTo.IndexOf("~");
                        if (pos > 0)
                        {
                            strPartner = strMailTo.Substring(0, pos);
                            strMailTo = strMailTo.Substring(pos + 1);
                            to = new MailAddress(strMailTo, strPartner);
                            mail.To.Add(to);
                        }
                        strUserID = chkUser.Items[i].Value.ToString().Trim();
                        if (rdw.UpdateCancelEvaluateRDWDetail(strDID, strUserID, strUID, strUname, isUpdate, strReasons))
                        {
                            mail.Subject = "[Notify]Project Tracking System -- Cancel";
                            sb.Append("<html>");
                            sb.Append("<body>");
                            sb.Append("<form>");
                            sb.Append(" Dear Partner:<br>");
                            sb.Append("     Project Name:" + lblProjectData.Text.Trim() + "<br>");
                            sb.Append("     Product Code:" + lblProdCodeData.Text.Trim() + "<br>");
                            sb.Append("     Step Name:" + lblStepNameData.Text.Trim() + "<br>");
                            sb.Append("     The Step of Member finished was Cancel.<br>");
                            sb.Append("     Cancel Reason :" + strReasons + "<br>");
                            sb.Append("     Please recheck your document.The completed Step has been cancelled(你完成的此步骤已被审批者取消,请检查上传的文档) <br>");
                            sb.Append("     Please click follow link to view:<br>");
                            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "</a>");
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
                        }
                        else
                        {
                            isSuccess = false;
                            break;
                        }
                        isUpdate = false;
                    }
                    catch
                    {
                        isSuccess = false;
                        break;
                    }
                }
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Save data and send email successfully!'); window.close(); ";
                ltlAlert.Text += " window.opener.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                ltlAlert.Text = "alert('Save data or send email failure!'); window.close(); ";
                ltlAlert.Text += " window.opener.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
            }
        }
        else
        {
            for (int i = 0; i < chkUser.Items.Count; i++)
            {
                if (chkUser.Items[i].Selected)
                {
                    try
                    {
                        strUserID = chkUser.Items[i].Value.ToString().Trim();

                        if (!rdw.UpdateCancelEvaluateRDWDetail(strDID, strUserID, strUID, strUname, isUpdate, strReasons))
                        {
                            isSuccess = false;
                            break;
                        }

                        isUpdate = false;
                    }
                    catch
                    {
                        isSuccess = false;
                        break;
                    }
                }
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Save data successfully!'); window.close(); ";
                ltlAlert.Text += " window.opener.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                ltlAlert.Text = "alert('Save data failure!');";
            }
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_DetailEdit.aspx?mid=" + Request.QueryString["mid"] + "&did=" + Request.QueryString["did"] + "&cateid=" + Request.QueryString["cateid"]);
    }
}
