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
using Microsoft.Web.UI.WebControls;
using System.IO;

public partial class WF_WorkFlowTemplateEdit : BasePage
{

    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dataBind();
            attachBind();

            string type = Request.QueryString["tp"].ToString();
            if (type == "0")
            {
                txtRemark.Enabled = true;
                fileAttach.Disabled = false;
                radFinish.Enabled = true;
                radStop.Enabled = true;
                btn_upload.Enabled = true;
                btn_save.Enabled = true;
                chkEmail.Enabled = true;
                tdReject.Visible = true;

                ddlReject.ClearSelection();
                DataTable dt = wf.RejectDDLBind(Request.QueryString["nbr"].ToString());
                if (dt.Rows.Count > 0)
                {
                    //tdReject.Visible = true;
                    ddlReject.DataSource = dt;
                    ddlReject.DataBind();
                    btn_reject.Visible = true;
                    ddlReject.Visible = true;
                    lbReject.Visible = true;
                }
                else
                {
                    //tdReject.Visible = false;
                    btn_reject.Visible = false;
                    ddlReject.Visible = false;
                    lbReject.Visible = false;
                }
            }
            else
            {
                if (!wf.JudgeIsCanModifiedWithApprover(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), Convert.ToInt32(Session["uID"])))
                {
                    txtRemark.Enabled = false;
                    fileAttach.Disabled = true;
                    radFinish.Enabled = false;
                    radStop.Enabled = false;
                    btn_upload.Enabled = false;
                    btn_save.Enabled = false;
                    chkEmail.Enabled = false;
                    btn_reject.Enabled = false;
                    ddlReject.Visible = false;
                    lbReject.Visible = false;
                    
                }
            }
        }
    }

    protected void dataBind()
    {
        string wfn_nbr = Request.QueryString["nbr"].ToString();
        string type = Request.QueryString["tp"].ToString();
        int plantCode = Convert.ToInt32(Session["PlantCode"]);
        int uID = Convert.ToInt32(Session["uID"]);

        int flowSetup = 0;
        DataTable dt = wf.GetFlowNodeInfo(wfn_nbr);
        flowSetup = dt.Rows.Count;

        TabStrip1.Items.Clear();
        for (int i = 0; i < flowSetup; i++)
        {
            TabStrip1.Items.Add(new Tab());
            TabStrip1.Items[i].Text = dt.Rows[i]["Node_Name"].ToString();
        }

        //工作流实例信息绑定
        SqlDataReader reader1 = wf.GetWorkFlowInstanceByNbr(wfn_nbr, plantCode);
        if (reader1.Read())
        {
            txtReqNbr.Text = wfn_nbr;
            txtWorkFlowPre.Text = Convert.ToString(reader1["Flow_Req_Pre"]);
            hlForm.Text = Convert.ToString(reader1["WFN_FormName"]);
            hlForm.NavigateUrl = "WF_FormEdit.aspx?nbr=" + wfn_nbr + "&rm=" + DateTime.Now;
            txtReqDate.Text = string.Format("{0:yyyy-MM-dd}", reader1["WFN_ReqDate"]);
            txtDueDate.Text = string.Format("{0:yyyy-MM-dd}", reader1["WFN_DueDate"]);
        }
        reader1.Close();

        //确定跟审批者相关的序号
        SqlDataReader reader2 = null;
        if (type == "0")
        {
            reader2 = wf.GetFirstUnReviewNodeSort(wfn_nbr);
            if (reader2.Read())
            {
                txtSortOrder.Text = Convert.ToString(reader2["Sort_Order"]);
            }
            //TabStrip1中哪一项被选中
            TabStrip1.SelectedIndex = Convert.ToInt32(txtSortOrder.Text.Trim()) / 10 - 1;

            //TabStrip1中未操作的需要冻结
            int LastSort = 0;
            SqlDataReader reader3 = wf.GetFlowNodeInstanceLastSort(wfn_nbr);
            if (reader3.Read())
            {
                LastSort = Convert.ToInt32(reader3["Sort_Order"]);
            }
            reader3.Close();

            if (Convert.ToInt32(txtSortOrder.Text.Trim()) != LastSort)
            {
                for (int i = Convert.ToInt32(txtSortOrder.Text.Trim()) / 10; i < LastSort / 10; i++)
                {
                    TabStrip1.Items[i].DefaultStyle.Add("background-image", "url(../images/button17.jpg)");
                    TabStrip1.Items[i].Enabled = false;
                }
            }
        }
        else
        {
            reader2 = wf.GetFlowNodeInstanceApprovedBySelf(wfn_nbr, uID);
            if (reader2.Read())
            {
                txtSortOrder.Text = Convert.ToString(reader2["Sort_Order"]);
            }
            //TabStrip1中哪一项被选中
            TabStrip1.SelectedIndex = Convert.ToInt32(txtSortOrder.Text.Trim()) / 10 - 1;

            //TabStrip1中未操作的需要冻结
            int LastSort = 0;
            SqlDataReader reader3 = wf.GetFlowNodeInstanceLastSort(wfn_nbr);
            if (reader3.Read())
            {
                LastSort = Convert.ToInt32(reader3["Sort_Order"]);
            }
            reader3.Close();

            int ALastSort = 0;
            SqlDataReader reader4 = wf.GetFlowNodeInstanceApprovedLastSort(wfn_nbr);
            if (reader4.Read())
            {
                ALastSort = Convert.ToInt32(reader4["Sort_Order"]);
            }
            reader4.Close();

            for (int i = ALastSort / 10; i < LastSort / 10; i++)
            {
                TabStrip1.Items[i].DefaultStyle.Add("background-image", "url(../images/button17.jpg)");
                TabStrip1.Items[i].Enabled = false;
            }
        }
        reader2.Close();

        //工作流节点实例信息绑定
        if (type == "0")
        {
            SqlDataReader reader5 = wf.GetUserInfo(Convert.ToInt32(Session["uID"]));
            if (reader5.Read())
            {
                txtCreatedBy.Text = Convert.ToString(reader5["userName"]);
                txtCreatedByRole.Text = Convert.ToString(reader5["roleName"]);
                txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            }
            reader5.Close();
        }
        else
        {
            SqlDataReader reader6 = wf.GetFNIInfoByNbrAndSort(wfn_nbr, Convert.ToInt32(txtSortOrder.Text.Trim()));
            if (reader6.Read())
            {
                txtRemark.Text = Convert.ToString(reader6["FNI_Remark"]);
                txtCreatedBy.Text = Convert.ToString(reader6["userName"]);
                txtCreatedByRole.Text = Convert.ToString(reader6["roleName"]);
                txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader6["FNI_RunDate"]);

                if (type == "2")
                {
                    radFinish.Checked = true;
                    radStop.Checked = false;
                }
                else
                {
                    radFinish.Checked = false;
                    radStop.Checked = true;
                }
            }
            reader6.Close();
        }
        //如果是发起的步骤，则按钮显示提交----王大龙
        if (txtSortOrder.Text == "10")
        {
            btn_save.Text = "提交";
        }
        else
        {
            btn_save.Text = "签署";
        }

        //绑定节点步骤描述
        SqlDataReader reader7 = wf.GetNodeSortDesc(wfn_nbr, txtSortOrder.Text.Trim());
        if (reader7.Read())
        {
            txtNoteDesc.Text = reader7["NodeDesc"].ToString();
        }
        reader7.Close();

       
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (fileAttach.Value.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择需要上传的附件!');";
            attachBind();
            return;
        }

        string attachName = Path.GetFileName(fileAttach.PostedFile.FileName);
        Stream attachStream = fileAttach.PostedFile.InputStream;
        int attachLength = fileAttach.PostedFile.ContentLength;
        string attachType = fileAttach.PostedFile.ContentType;
        byte[] attachByte = new byte[attachLength];
        attachStream.Read(attachByte, 0, attachLength);

        if (!wf.UploadFniAttachment(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), attachName, attachType, attachByte, Convert.ToInt32(Session["uID"])))
        {
            ltlAlert.Text = "alert('附件上传失败,请联系管理员!');";
            attachBind();
            return;
        }
        attachBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        int status = 2;
        if (radFinish.Checked) status = 2;
        if (radStop.Checked) status = 3;

        if (txtRemark.Text.Length >= 1000)
        {
            ltlAlert.Text = "alert('备注栏位不能超过500字符，请重新输入!');";
            return;
        }
        if (wf.UpdateFlowNodeInstance(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), txtRemark.Text.Trim(), status, Convert.ToInt32(Session["uID"]),hidSql.Value))
        {
            if (chkEmail.Checked)
            {
                string strMailFrom = string.Empty;
                string strMailTo = string.Empty;
                StringBuilder sb = new StringBuilder();
                bool isSuccess = true;
                MailMessage mail = new MailMessage();

                //获取最后一个节点
                int LastSort = 0;
                SqlDataReader reader1 = wf.GetFlowNodeInstanceLastSort(txtReqNbr.Text.Trim());
                if (reader1.Read())
                {
                    LastSort = Convert.ToInt32(reader1["Sort_Order"]);
                }
                reader1.Close();

                //获取发件人邮箱
                SqlDataReader reader2 = wf.GetUserInfo(Convert.ToInt32(Session["uID"]));
                if (reader2.Read())
                {
                    strMailFrom = Convert.ToString(reader2["email"]);
                }
                reader2.Close();
                if (strMailFrom.Length == 0)
                {
                    strMailFrom = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                }


                //获取以前审批人的邮箱，这个是必须要处理的
                DataTable dt1 = wf.GetPerApproverEmail(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()));

                string workflowName = "";
                SqlDataReader reader10 = wf.GetWorkFlowNameByDomainAndPre(Convert.ToInt32(Session["PlantCode"]), txtWorkFlowPre.Text.Trim());
                if (reader10.Read())
                {
                    workflowName = Convert.ToString(reader10["Flow_Name"]);
                }
                reader10.Close();

                //审批通过
                if (radFinish.Checked)
                {
                    try//给以前审批者发送邮件
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            strMailTo = dt1.Rows[i]["email"].ToString();
                            if (strMailTo.Length == 0)
                            {
                                ltlAlert.Text = "alert('以前审批者无邮箱!');";
                            }
                            else
                            {
                                mail.To = strMailTo;
                                mail.From = strMailFrom;

                                mail.Subject = "100系统-->工作流模块邮件";
                                sb.Append("<html>");
                                sb.Append("<body>");
                                sb.Append("<form>");
                                sb.Append("  " + dt1.Rows[i]["userName"].ToString() + ":<br>");
                                sb.Append("     " + txtCreatedBy.Text.Trim() + "通过了您已经审批过的" + workflowName + "流程!" + "<br>");
                                sb.Append("     申请号:" + txtReqNbr.Text.Trim() + "<br>");
                                sb.Append("     备注:" + txtRemark.Text.Trim() + "<br>");
                                sb.Append("<br>");
                                sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                                sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2</a>");
                                sb.Append("</body>");
                                sb.Append("</form>");
                                sb.Append("</html>");
                                mail.BodyFormat = MailFormat.Html;
                                mail.Body = Convert.ToString(sb);
                                BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                                //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                                //SmtpMail.Send(mail);
                                sb.Remove(0, sb.Length);
                            }
                        }
                    }
                    catch
                    {
                        isSuccess = false;
                    }

                    try//向申请者发送邮件
                    {
                        string uName = "";
                        SqlDataReader reader11 = wf.GetApplierEmail(txtReqNbr.Text.Trim());
                        if (reader11.Read())
                        {
                            strMailTo = Convert.ToString(reader11["email"]);
                            uName = Convert.ToString(reader11["userName"]);
                        }
                        reader11.Close();

                        if (strMailTo == "")
                        {
                            ltlAlert.Text = "alert('申请者无邮箱!');";
                        }
                        else
                        {
                            mail.To = strMailTo;
                            mail.From = strMailFrom;

                            mail.Subject = "100系统-->工作流模块邮件";
                            sb.Append("<html>");
                            sb.Append("<body>");
                            sb.Append("<form>");
                            sb.Append("  " + uName + ":<br>");
                            sb.Append("     " + txtCreatedBy.Text.Trim() + "通过了您申请的" + workflowName + "流程!" + "<br>");
                            sb.Append("     申请号:" + txtReqNbr.Text.Trim() + "<br>");
                            sb.Append("     备注:" + txtRemark.Text.Trim() + "<br>");
                            sb.Append("<br>");
                            sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                            sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceList.aspx' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceList.aspx</a>");
                            sb.Append("</body>");
                            sb.Append("</form>");
                            sb.Append("</html>");
                            mail.BodyFormat = MailFormat.Html;
                            mail.Body = Convert.ToString(sb);
                            BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                            //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                            //SmtpMail.Send(mail);
                            sb.Remove(0, sb.Length);
                        }
                    }
                    catch
                    {
                        isSuccess = false;
                    }


                    if (Convert.ToInt32(txtSortOrder.Text.Trim()) != LastSort)//如果不是最后一个审批者,也会向后发邮件
                    {
                        DataTable dt2 = wf.GetNextApproverEmail(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()));
                        if (dt2.Rows[0]["email"].ToString() == "")
                        {
                            ltlAlert.Text = "alert('下一审批者无邮箱!');";
                        }
                        else
                        {
                            try
                            {
                                mail.To = dt2.Rows[0]["email"].ToString();
                                mail.From = strMailFrom;

                                mail.Subject = "100系统-->工作流模块邮件";
                                sb.Append("<html>");
                                sb.Append("<body>");
                                sb.Append("<form>");
                                sb.Append("  " + dt2.Rows[0]["userName"].ToString() + ":<br>");
                                sb.Append("     " + txtCreatedBy.Text.Trim() + "向您递交了一份" + workflowName + "流程申请，需要您处理!" + "<br>");
                                sb.Append("     申请号:" + txtReqNbr.Text.Trim() + "<br>");
                                sb.Append("     备注:" + txtRemark.Text.Trim() + "<br>");
                                sb.Append("<br>");
                                sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                                sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=0&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=0</a>");
                                sb.Append("</body>");
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
                        }
                    }

                    if (isSuccess)
                    {
                        ltlAlert.Text = "alert('保存数据，发送邮件成功!'); window.location.href = '/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=" + status + "&rm=" + DateTime.Now + "';";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('保存成功，但邮件发送失败，请联系管理员!'); window.location.href = '/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=" + status + "&rm=" + DateTime.Now + "';";
                    }

                }
                else//审批不通过，不会向后发邮件，只会向前发邮件
                {
                    try//向以前审批过的人发送邮件
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            strMailTo = dt1.Rows[i]["email"].ToString();
                            if (strMailTo.Length == 0)
                            {
                                ltlAlert.Text = "alert('以前审批者无邮箱!');";
                            }
                            else
                            {
                                mail.To = strMailTo;
                                mail.From = strMailFrom;

                                mail.Subject = "100系统-->工作流模块邮件";
                                sb.Append("<html>");
                                sb.Append("<body>");
                                sb.Append("<form>");
                                sb.Append("  " + dt1.Rows[i]["userName"].ToString() + ":<br>");
                                sb.Append("     " + txtCreatedBy.Text.Trim() + "否定了您已经审批过的" + workflowName + "流程!" + "<br>");
                                sb.Append("     申请号:" + txtReqNbr.Text.Trim() + "<br>");
                                sb.Append("     备注:" + txtRemark.Text.Trim() + "<br>");
                                sb.Append("<br>");
                                sb.Append("     查看该流程的具体信息请登录:<br>");
                                sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2</a>");
                                sb.Append("</body>");
                                sb.Append("</form>");
                                sb.Append("</html>");
                                mail.BodyFormat = MailFormat.Html;
                                mail.Body = Convert.ToString(sb);
                                BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                                //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                                //SmtpMail.Send(mail);
                                sb.Remove(0, sb.Length);
                            }
                        }
                    }
                    catch
                    {
                        isSuccess = false;
                    }

                    try//向申请者发送邮件
                    {
                        string uName = "";
                        SqlDataReader reader15 = wf.GetApplierEmail(txtReqNbr.Text.Trim());
                        if (reader15.Read())
                        {
                            strMailTo = Convert.ToString(reader15["email"]);
                            uName = Convert.ToString(reader15["userName"]);
                        }
                        reader15.Close();
                        if (strMailTo == "")
                        {
                            ltlAlert.Text = "alert('申请者无邮箱!');";
                        }
                        {
                            mail.To = strMailTo;
                            mail.From = strMailFrom;

                            mail.Subject = "100系统-->工作流模块邮件";
                            sb.Append("<html>");
                            sb.Append("<body>");
                            sb.Append("<form>");
                            sb.Append("  " + uName + ":<br>");
                            sb.Append("     " + txtCreatedBy.Text.Trim() + "否定了您申请的" + workflowName + "流程!" + "<br>");
                            sb.Append("     申请号:" + txtReqNbr.Text.Trim() + "<br>");
                            sb.Append("     备注:" + txtRemark.Text.Trim() + "<br>");
                            sb.Append("<br>");
                            sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                            sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceList.aspx' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceList.aspx</a>");
                            sb.Append("</body>");
                            sb.Append("</form>");
                            sb.Append("</html>");
                            mail.BodyFormat = MailFormat.Html;
                            mail.Body = Convert.ToString(sb);
                            BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                            //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                            //SmtpMail.Send(mail);
                            sb.Remove(0, sb.Length);
                        }
                    }
                    catch
                    {
                        isSuccess = false;
                    }

                    if (isSuccess)
                    {
                        //ltlAlert.Text = "alert('保存数据，发送邮件成功!'); window.location.href = '/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=" + status + "&rm=" + DateTime.Now + "';";
                        ltlAlert.Text = "alert('保存数据，发送邮件成功!'); window.location.href =  '/WF/WF_WorkFlowInstanceTaskList.aspx';";
                    }
                    else
                    {
                        //ltlAlert.Text = "alert('保存成功，但邮件发送失败，请联系管理员!'); window.location.href = '/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=" + status + "&rm=" + DateTime.Now + "';";
                        ltlAlert.Text = "alert('保存成功，但邮件发送失败，请联系管理员!'); window.location.href =  '/WF/WF_WorkFlowInstanceTaskList.aspx';";
                    }
                }
            }
            else
            {
               // ltlAlert.Text = "alert('保存成功!'); window.location.href = '/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=" + status + "&rm=" + DateTime.Now + "';";
                ltlAlert.Text = "alert('保存成功!'); window.location.href = '/WF/WF_WorkFlowInstanceTaskList.aspx';";
            }

        }
        else
        {
            ltlAlert.Text = "alert('保存失败，请联系管理员!');";
        }
        attachBind();
    }

    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("WF_WorkFlowInstanceTaskList.aspx?rm=" + DateTime.Now, true);
    }

    protected void attachBind()
    {
        DataTable dt = wf.GetFniAttach(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()));
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvFNIA.DataSource = dt;
            this.gvFNIA.DataBind();
            int ColunmCount = gvFNIA.Rows[0].Cells.Count;
            gvFNIA.Rows[0].Cells.Clear();
            gvFNIA.Rows[0].Cells.Add(new TableCell());
            gvFNIA.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvFNIA.Rows[0].Cells[0].Text = "No Data!";
            gvFNIA.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            this.gvFNIA.DataSource = dt;
            this.gvFNIA.DataBind();
        }
        FormBind();
    }

    private void FormBind()
    {
        divForm.InnerHtml = wf.GenerateForm(txtReqNbr.Text.Trim(), txtSortOrder.Text, btn_save.Enabled);
    }

    protected void gvFNIA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            ltlAlert.Text = "var w=window.open('/WF/WF_AttachViewDown.aspx?id=" + e.CommandArgument.ToString().Trim() + "','AttachViewDown',";
            ltlAlert.Text += "'menubar=No,scrollbars = No,resizable=No,width=1,height=1,top=0,left=0'); w.focus();";
        }
        attachBind();
    }

    protected void gvFNIA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string wfn_nbr = Request.QueryString["nbr"].ToString();
            string type = Request.QueryString["tp"].ToString();
            int uID = Convert.ToInt32(Session["uID"]);

            if (type == "0")
            {
                string SortOrder = "";
                SqlDataReader reader1 = wf.GetFirstUnReviewNodeSort(wfn_nbr);
                if (reader1.Read())
                {
                    SortOrder = Convert.ToString(reader1["Sort_Order"]);
                }
                reader1.Close();
                if (SortOrder != txtSortOrder.Text.Trim())
                {
                    e.Row.Cells[4].Text = string.Empty;
                }
            }
            else
            {
                if (!wf.JudgeIsCanModifiedWithApprover(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), uID))
                {
                    e.Row.Cells[4].Text = string.Empty;
                }
            }
        }
    }

    protected void gvFNIA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        wf.DeleteFniAttach(Convert.ToInt32(gvFNIA.DataKeys[e.RowIndex].Value));
        attachBind();
    }

    protected void TabStrip1_SelectedIndexChange(object sender, EventArgs e)
    {
        string wfn_nbr = Request.QueryString["nbr"].ToString();
        string type = Request.QueryString["tp"].ToString();
        int uID = Convert.ToInt32(Session["uID"]);

        txtSortOrder.Text = Convert.ToString((TabStrip1.SelectedIndex + 1) * 10);
        if (txtSortOrder.Text.Trim() == "10")
        {
            tblReview.Visible = false;
        }
        else
        {
            tblReview.Visible = true;
        }


        //工作流节点实例信息绑定
        if (type == "0")
        {
            string SortOrder = "";
            SqlDataReader reader1 = wf.GetFirstUnReviewNodeSort(wfn_nbr);
            if (reader1.Read())
            {
                SortOrder = Convert.ToString(reader1["Sort_Order"]);
            }
            reader1.Close();
            if (SortOrder == txtSortOrder.Text.Trim())
            {
                txtRemark.Enabled = true;
                fileAttach.Disabled = false;
                radFinish.Enabled = true;
                radStop.Enabled = true;
                btn_upload.Enabled = true;
                btn_save.Enabled = true;
                chkEmail.Enabled = true;
                tdReject.Visible = true;

                SqlDataReader reader2 = wf.GetUserInfo(uID);
                if (reader2.Read())
                {
                    txtRemark.Text = string.Empty;
                    txtCreatedBy.Text = Convert.ToString(reader2["userName"]);
                    txtCreatedByRole.Text = Convert.ToString(reader2["roleName"]);
                    txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                }
                reader2.Close();
            }
            else
            {
                txtRemark.Enabled = false;
                fileAttach.Disabled = true;
                radFinish.Enabled = false;
                radStop.Enabled = false;

                btn_upload.Enabled = false;

                btn_save.Enabled = false;
                chkEmail.Enabled = false;

                tdReject.Visible = false;

                SqlDataReader reader3 = wf.GetFNIInfoByNbrAndSort(wfn_nbr, Convert.ToInt32(txtSortOrder.Text.Trim()));
                if (reader3.Read())
                {
                    txtRemark.Text = Convert.ToString(reader3["FNI_Remark"]);
                    txtCreatedBy.Text = Convert.ToString(reader3["userName"]);
                    txtCreatedByRole.Text = Convert.ToString(reader3["roleName"]);
                    txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader3["FNI_RunDate"]);
                }
                reader3.Close();
            }
        }
        else
        {
            if (!wf.JudgeIsCanModifiedWithApprover(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), uID))
            {
                txtRemark.Enabled = false;
                fileAttach.Disabled = true;
                radFinish.Enabled = false;
                radStop.Enabled = false;
                btn_upload.Enabled = false;
                btn_save.Enabled = false;
                chkEmail.Enabled = false;
                tdReject.Visible = false;
            }
            else
            {
                txtRemark.Enabled = true;
                fileAttach.Disabled = false;
                radFinish.Enabled = true;
                radStop.Enabled = true;
                btn_upload.Enabled = true;
                btn_save.Enabled = true;
                chkEmail.Enabled = true;
                tdReject.Visible = true;
            }

            SqlDataReader reader4 = wf.GetFNIInfoByNbrAndSort(wfn_nbr, Convert.ToInt32(txtSortOrder.Text.Trim()));
            if (reader4.Read())
            {
                txtRemark.Text = Convert.ToString(reader4["FNI_Remark"]);
                txtCreatedBy.Text = Convert.ToString(reader4["userName"]);
                txtCreatedByRole.Text = Convert.ToString(reader4["roleName"]);
                txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader4["FNI_RunDate"]);

                if (Convert.ToString(reader4["FNI_Status"]) == "2")
                {
                    radFinish.Checked = true;
                    radStop.Checked = false;
                }
                else
                {
                    radFinish.Checked = false;
                    radStop.Checked = true;
                }
            }
            reader4.Close();
        }
        attachBind();
        //如果是发起的步骤，则按钮显示提交----王大龙
        if (txtSortOrder.Text == "10")
        {
            btn_save.Text = "提交";
        }
        else
        {
            btn_save.Text = "签署";
        }

        //绑定节点步骤描述
        SqlDataReader reader7 = wf.GetNodeSortDesc(wfn_nbr, txtSortOrder.Text.Trim());
        if (reader7.Read())
        {
            txtNoteDesc.Text = reader7["NodeDesc"].ToString();
        }
        reader7.Close();
    }

    protected void btn_reject_Click(object sender, EventArgs e)
    {
        string strMailTo = string.Empty;
        string strRejectTo = string.Empty;
        if (wf.upadateReject(txtReqNbr.Text.Trim(), Session["uID"].ToString(), ddlReject.SelectedValue, out strMailTo, out strRejectTo))
        {
            //发邮件         
            StringBuilder sb = new StringBuilder();
            bool isSuccess = true;
            MailMessage mail = new MailMessage();

            try
            {
                mail.To = strMailTo;
                mail.From = ConfigurationManager.AppSettings["AdminEmail"].ToString();

                mail.Subject = "100系统-->工作流模块邮件";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append("  ");
                sb.Append("     您审批通过的流程" + "<br>");
                sb.Append("     申请号:" + txtReqNbr.Text.Trim() + "<br>");
                sb.Append("     在您审批的流程被:  "+Session["uName"].ToString() +"  驳回<br>");
                sb.Append("     驳回到步奏  " + strRejectTo + "  <br>");
                sb.Append("     请您及时处理<br>");
                sb.Append("<br>");
                sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
                sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceList.aspx' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceList.aspx</a>");
                sb.Append("</body>");
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
            if (isSuccess)
            {
                ltlAlert.Text = "alert('驳回成功!'); window.location.href = '/WF/WF_WorkFlowInstanceTaskList.aspx?rm=" + DateTime.Now + "';";
            }
            else 
            {
                ltlAlert.Text = "alert('驳回成功!,但发送邮件失败！'); window.location.href = '/WF/WF_WorkFlowInstanceTaskList.aspx?rm=" + DateTime.Now + "';";

            }
        }
        else
        {
            ltlAlert.Text = "alert('驳回失败！'); window.location.href = '/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=" + "2" + "&rm=" + DateTime.Now + "';";
        }
    }
}
