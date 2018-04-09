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

                if (!rdw.CheckViewRDWDetailEdit(strMID, strDID, strUID))
                {
                    ltlAlert.Text = " window.close();";
                }

                if (!rdw.CheckCancelEvaluateRDWDetail(strDID, strUID))
                {
                    ltlAlert.Text = " window.close();";
                }
                else
                {
                    RDW_Detail rd = rdw.SelectRDWDetailEdit(strDID);

                    lblProjectData.Text = rd.RDW_Project;
                    lblProdCodeData.Text = rd.RDW_ProdCode;
                    lblStepNameData.Text = rd.RDW_StepName;

                    LoadMemberUser(); 
                    LoadSubsequentFinishedStep();

                }
            }
        }
    }

    private void LoadSubsequentFinishedStep()
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        ckbSubCompSteps.Items.Clear();

        DataSet ds = rdw.getSubsequentCompSteps(strMID,strDID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            { 
              ckbSubCompSteps.Items.Add(new ListItem(ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim() + "~" + ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim(), ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim()));
               
            }
        }
        else
        {
            plSubSteps.GroupingText = "No Subsequent Completed Steps";
        }

        ds = null;
    }
     
    protected void LoadMemberUser()
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
                if (Convert.ToBoolean( dst.Tables[0].Rows[i].ItemArray[3].ToString()))
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

    /// <summary>
    /// 当确认OK,取消该步骤的完成, 当选择取消某位成员完成,其它成员在步骤操作状态是完成的;
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

        bool cancelMemberEmail = false;
        bool cancelSubSequent = false;
        bool SubSequentEmail = false;
       
        MailAddress strMailFrom; 
        if (Session["Email"].ToString().Length == 0)
        {
            strMailFrom = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString(), Session["uName"].ToString());
        }
        else
        {
            strMailFrom = new MailAddress(Session["Email"].ToString(), Session["uName"].ToString());
        }

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
            ltlAlert.Text = "alert('Please select Member!');";
            return;
        }

        #region Cancel　Members 
        for (int i = 0; i < chkUser.Items.Count; i++)
        {
            if (chkUser.Items[i].Selected)
            {     
                try
                {
                    strUserID = chkUser.Items[i].Value.ToString().Trim();

                    if (rdw.UpdateCancelEvaluateRDWDetail(strDID, strUserID, strUID, strUname, isUpdate, strReasons))
                    {
                       isUpdate = false; 
                       string strMailTo = chkUser.Items[i].Text.Trim();
                        pos = strMailTo.IndexOf("~");
                        if (pos > 0)
                        {
                            if (chkEmail.Checked)
                            { 
                                MailMessage mail = new MailMessage();
                                strPartner = strMailTo.Substring(0, pos);
                                strMailTo = strMailTo.Substring(pos + 1);
                                mail.From = strMailFrom;
                                mail.To.Add(strMailTo);
                                mail.Subject = "[Notify]Project Tracking System -- Cancel";
                                sb.Append("<html>");
                                sb.Append("<body>");
                                sb.Append("<form>");
                                sb.Append(" Dear " + strPartner + ":<br>");
                                sb.Append("     Project Name:" + lblProjectData.Text.Trim() + "<br>");
                                sb.Append("     Product Code:" + lblProdCodeData.Text.Trim() + "<br>");
                                sb.Append("     Step Name:" + lblStepNameData.Text.Trim() + "<br>");
                                sb.Append("     The Step of Member finished was Cancel.<br>"); 
                                sb.Append("    It has been Cancelled that Member:" + strPartner + " completed the step task.(" + strPartner + " 成员完成的步骤已被否绝) <br>");
                                sb.Append("     Cancel Reason :" + strReasons + "<br>");
                                sb.Append("     Please recheck your document.The completed Step has been cancelled(你完成的此步骤已被审批者取消,请检查上传的文档) <br>");
                                sb.Append("     Please click follow link to view:<br>");
                                sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "</a>");
                                sb.Append("</form>");
                                sb.Append("</html>");
                                mail.IsBodyHtml = true; 
                                mail.Body = Convert.ToString(sb);
                                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["mailServer"]);
                                smtpClient.Send(mail);
                                sb.Remove(0, sb.Length);
                                isSuccess = true;
                                cancelMemberEmail = true;
                            }
                        }
                    }
                    else
                    {
                        cancelMemberEmail = false;
                        isSuccess = false;
                        break;
                    }
                }
                catch
                {
                    isSuccess = false;
                    break;
                }
            }     
        }
        # endregion

#region Cancel Subsequent Complete Step
        if (ckbSubCompSteps.Items.Count > 0)
        {
            StringBuilder sbCancel = new StringBuilder();
            for (int i = 0; i < ckbSubCompSteps.Items.Count; i++)
            {
                if (ckbSubCompSteps.Items[i].Selected)
                {
                    cancelSubSequent = true;
                    string SubCompStepsId = ckbSubCompSteps.Items[i].Value;
                    string CurStepID = strDID;
                    if (rdw.CancelSubsequentCompStep(SubCompStepsId, CurStepID, strMID, strUID, strUname))
                    {
                        sbCancel.Append(" <br>The Approved Step：{ " + ckbSubCompSteps.Items[i].Text + " }  Was Cancelled.<br>");
                    } 
                }
            }
            if (chkEmail.Checked && cancelSubSequent)
            {
                MailMessage mail = new MailMessage();
                mail.From = strMailFrom; 
                IDataReader reader = rdw.SelectRDWProjectAllMemberEmail(strMID, strUID);
                while (reader.Read())
                {
                    mail.To.Add(reader["Email"].ToString()); 
                }
                reader.Close();
                if (mail.To.Count > 0)
                { 
                
                mail.Subject = "[Notify]Project Tracking System -- Cancel Step by Previous Step Approver";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append(" Dear parter <br>");
                sb.Append("     Project Name:" + lblProjectData.Text.Trim() + "<br>");
                sb.Append("     Product Code:" + lblProdCodeData.Text.Trim() + "<br>");
                sb.Append("     Step Name:" + lblStepNameData.Text.Trim() + "<br><br>");
                sb.Append(sbCancel.ToString());
                sb.Append("     It has been Cancelled by Approver:" + strUname + "(" + strUname + " 否绝了后续完成的步骤,为因此步骤上传的内容发生改变) <br>");
                sb.Append("     Please recheck your document.The completed Step has been cancelled(完成的此步骤已被审批者取消,请检查上传的文档) <br>");
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
                SubSequentEmail = true;
                }
                
            }
        }
#endregion
        if (isSuccess)
        {
            if (cancelMemberEmail)
            {
                ltlAlert.Text = "alert('Save data and send email successfully!'); window.close(); ";
                ltlAlert.Text += " window.opener.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                if (SubSequentEmail)
                {
                    ltlAlert.Text = "alert('Save data and send email to all Project Partner successfully!'); window.close(); ";
                    ltlAlert.Text += " window.opener.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                }
            }

        }
        else
        {
            ltlAlert.Text = "alert('Save data or send email failure!'); window.close(); ";
            ltlAlert.Text += " window.opener.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
        }
            
    }
}
