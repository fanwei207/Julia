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
using System.Data.SqlClient;
using Microsoft.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class WF_WorkFlowTemplateEdit : BasePage
{

    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            tblSupply.Visible = false;
            gvFNIA.Visible = false;
            TabStrip1.Visible = false;
            TeamMultiTab.Visible = false;
            loadWorkFlowTemplate();
        }
    }

    protected void loadWorkFlowTemplate()
    {
        ddlWorkFlow.DataSource = wf.GetWorkFlowTemplateByDomain(Convert.ToInt32(Session["plantCode"]));
        ddlWorkFlow.DataBind();
        ddlWorkFlow.Items.Insert(0, new ListItem("--"));
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (ddlWorkFlow.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('��ѡ����������ģ�壡');";
            return;
        }
        else
        {
            if (wf.JudgeIsCanCreatedWithApplier(Convert.ToInt32(ddlWorkFlow.SelectedValue), Convert.ToInt32(Session["uID"]), Session["roleName"].ToString()))
            {
                int flowSetup = 0;
                DataTable dt = wf.GetFlowNode(Convert.ToInt32(ddlWorkFlow.SelectedValue));
                flowSetup = dt.Rows.Count;

                TabStrip1.Items.Clear();
                for (int i = 0; i < flowSetup; i++)
                {
                    TabStrip1.Items.Add(new Tab());
                    TabStrip1.Items[i].Text = dt.Rows[i]["Node_Name"].ToString();
                    if (i != 0)
                    {
                        TabStrip1.Items[i].DefaultStyle.Add("background-image", "url(../images/button17.jpg)");
                        TabStrip1.Items[i].Enabled = false;
                    }
                }

                tblSupply.Visible = true;
                gvFNIA.Visible = true;
                TabStrip1.Visible = true;
                TeamMultiTab.Visible = true;

                int flowID = Convert.ToInt32(ddlWorkFlow.SelectedValue);
                int plantCode = Convert.ToInt32(Session["PlantCode"]);
                int createdBy = Convert.ToInt32(Session["uID"]);

                string wfn_nbr = wf.AddEmptyWorkFlowInstance(flowID, plantCode, createdBy);

                if (wfn_nbr == string.Empty)
                {
                    ltlAlert.Text = "alert('���̴���ʧ��,��ˢ��һ�Σ�');";
                    return;
                }
                else
                {
                    SqlDataReader reader1 = wf.GetWorkFlowInstanceByNbr(wfn_nbr, plantCode);
                    if (reader1.Read())
                    {
                        txtReqNbr.Text = wfn_nbr;

                        SqlDataReader reader2 = wf.GetFirstUnReviewNodeSort(wfn_nbr);
                        if (reader2.Read())
                        {
                            txtSortOrder.Text = Convert.ToString(reader2["Sort_Order"]);
                        }
                        reader2.Close();
                        //�󶨽ڵ㲽������
                        SqlDataReader reader3 = wf.GetNodeSortDesc(wfn_nbr, txtSortOrder.Text.Trim());
                        if (reader3.Read())
                        {
                            txtNoteDesc.Text = reader3["NodeDesc"].ToString();
                        }
                        reader3.Close();


                        txtWorkFlowPre.Text = Convert.ToString(reader1["Flow_Req_Pre"]);
                        hlForm.Text = Convert.ToString(reader1["WFN_FormName"]);
                        hlForm.NavigateUrl = "WF_FormEdit.aspx?nbr=" + wfn_nbr + "&rm=" + DateTime.Now;
                        txtReqDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        txtDueDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(30));
                        txtCreatedBy.Text = Convert.ToString(reader1["WFN_CreatedBy"]);
                        txtCreatedByRole.Text = Convert.ToString(reader1["WFN_CreatedRole"]);
                        txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader1["WFN_CreatedDate"]);
                    }
                    reader1.Close();
                }
                attachBind();
            }
            else
            {
                ltlAlert.Text = "alert('����Ȩ�޴��������̣�������������ϵ����Ա!');";
            }
        }
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (fileAttach.Value.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('��ѡ����Ҫ�ϴ��ĸ���!');";
            attachBind();
            return;
        }
        //2014-06-16--by���������ϴ���������Ҫ�������
        //bool isActive = false;
        //SqlDataReader reader = wf.JudgeWorkFlowInstanceIsActive(txtReqNbr.Text.Trim());
        //if (reader.Read())
        //{
        //    if (Convert.ToString(reader["WFN_Status"]) != "0")
        //    {
        //        isActive = true;
        //    }
        //    else
        //    {
        //        isActive = false;
        //    }
        //}
        //reader.Close();

        //if (isActive)
        //{
        string attachName = Path.GetFileName(fileAttach.PostedFile.FileName);
        Stream attachStream = fileAttach.PostedFile.InputStream;
        int attachLength = fileAttach.PostedFile.ContentLength;
        string attachType = fileAttach.PostedFile.ContentType;
        byte[] attachByte = new byte[attachLength];
        attachStream.Read(attachByte, 0, attachLength);

        if (!wf.UploadFniAttachment(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), attachName, attachType, attachByte, Convert.ToInt32(Session["uID"])))
        {
            ltlAlert.Text = "alert('�����ϴ�ʧ��,����ϵ����Ա!');";
            attachBind();
            return;
        }
        //}
        //else
        //{
        //    ltlAlert.Text = "alert('���ȱ�����Ϣ�����ϴ�����!');";
        //    attachBind();
        //    return;
        //}
        attachBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('<��������>����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtReqDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ����ȷ��ʽ��:YYYY-MM-DD');";
                return;
            }
        }

        if (txtDueDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('<��ֹ����>����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDueDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('��ֹ���ڸ�ʽ����ȷ����ȷ��ʽ��:YYYY-MM-DD');";
                return;
            }
        }

        if (Convert.ToDateTime(txtDueDate.Text) < Convert.ToDateTime(txtReqDate.Text))
        {
            ltlAlert.Text = "alert('��ֹ���ڲ���С����������!');";
            return;
        }

        string strMailFrom = string.Empty;
        string strMailTo = string.Empty;
        StringBuilder sb = new StringBuilder();
        bool isSuccess = true;

        if (wf.JudgeExcelIsFill(txtReqNbr.Text.Trim()))
        {
            if (wf.ActiveWorkFlowInstance(txtReqNbr.Text.Trim(), txtReqDate.Text.Trim(), txtDueDate.Text.Trim()))
            {
                if (wf.UpdateFlowNodeInstance(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), txtRemark.Text.Trim().Replace("\n", "<br>"), 1, Convert.ToInt32(Session["uID"]),hidSql.Value))
                {
                    if (chkEmail.Checked)
                    {
                        try
                        {
                            SqlDataReader reader1 = wf.GetUserInfo(Convert.ToInt32(Session["uID"]));
                            if (reader1.Read())
                            {
                                strMailFrom = Convert.ToString(reader1["email"]);
                            }
                            reader1.Close();

                            if (strMailFrom.Length == 0)
                            {
                                strMailFrom = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                            }

                            MailMessage mail = new MailMessage();
                            mail.From = strMailFrom;

                            string mailTo = "";
                            DataTable dt = wf.GetNextApproverEmail(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()));
                            mailTo = dt.Rows[0]["email"].ToString();

                            if (mailTo.Length == 0)
                            {
                                ltlAlert.Text = "alert('����ɹ������ʼ�����ʧ�ܣ�����ϵ����Ա!');";
                                attachBind();
                                return;
                            }
                            else
                            {
                                mail.To = mailTo;
                                string workflowName = "";

                                SqlDataReader reader2 = wf.GetWorkFlowNameByDomainAndPre(Convert.ToInt32(Session["PlantCode"]), txtWorkFlowPre.Text.Trim());
                                if (reader2.Read())
                                {
                                    workflowName = Convert.ToString(reader2["Flow_Name"]);
                                }
                                reader2.Close();

                                mail.Subject = "100ϵͳ-->������ģ���ʼ�";
                                sb.Append("<html>");
                                sb.Append("<body>");
                                sb.Append("<form>");
                                sb.Append("  " + dt.Rows[0]["userName"].ToString() + ":<br>");
                                sb.Append("     " + txtCreatedBy.Text.Trim() + "�����ݽ���һ��" + workflowName + "�������룬��Ҫ������!" + "<br>");
                                sb.Append("     �����:" + txtReqNbr.Text.Trim() + "<br>");
                                sb.Append("     ��ע:" + txtRemark.Text.Trim() + "<br>");
                                sb.Append("<br>");
                                sb.Append("     �鿴�����̵ľ�����Ϣ�������������:<br>");
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
                        }
                        catch (Exception ex)
                        {
                            isSuccess = false;
                            ltlAlert.Text = "alert('����ɹ������ʼ�����ʧ�ܣ�����ϵ����Ա!" + ex.Message + "');";
                        }
                        finally
                        {
                            if (isSuccess)
                            {
                                ltlAlert.Text = "alert('�������ݣ������ʼ��ɹ�!');";
                            }
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('����ɹ�!');";
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('����ʧ�ܣ�����ϵ����Ա!');";
                }
            }
            else
            {
                ltlAlert.Text = "alert('����ʧ�ܣ�����ϵ����Ա!');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('��δ��д����������д��!');";
        }
        attachBind();
    }

    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WF/WF_WorkFlowInstanceList.aspx?rm=" + DateTime.Now.ToString(), true);
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
    }

    protected void gvFNIA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvFNIA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        wf.DeleteFniAttach(Convert.ToInt32(gvFNIA.DataKeys[e.RowIndex].Value));
        attachBind();
    }
}
