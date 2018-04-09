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
using System.IO;
using System.Collections.Generic;
//using System.Web.Mail;
using System.Net.Mail;
using System.Text;
using RD_WorkFlow;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


public partial class RDW_DetailEdit : BasePage
{
    RDW rdw = new RDW();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("170008", "注册查看所有项目的权限");
        }

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        gvMessage.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");  
        if (!IsPostBack)
        {
            int mid = Convert.ToInt32(Request.QueryString["mid"]);
            this.hidMID.Value = mid.ToString();
            this.hidDID.Value = Request.QueryString["did"];
            if (!rdw.IsApproveStep(Convert.ToInt32(hidDID.Value)) && rdw.CanDisapprove(Convert.ToInt32(hidDID.Value),Convert.ToInt32(hidMID.Value),Convert.ToInt32(Session["uID"])))
            {
                btn_disApprove.Visible = true;
            }
            //记录每个进入RDW_DetailEdit的日志表（哪个人在什么时候进过这个步骤）
            rdw.InsertEnterLog(this.hidDID.Value, this.hidMID.Value, Session["uID"].ToString(), Session["uName"].ToString());         

            if (Request.QueryString["mid"] == null || Request.QueryString["did"] == null)
            {
                Response.Redirect("/RDW/RDW_HeaderList.aspx?rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                //定义参数
                string strMID = Convert.ToString(Request.QueryString["mid"]);
                string strDID = Convert.ToString(Request.QueryString["did"]);
                string strUID = Convert.ToString(Session["uID"]);
                string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);

                //if (!this.Security["170008"].isValid) // View All Project 权限
                //{
                //    if (!rdw.CheckViewRDWDetailEdit(strMID, strDID, strUID))
                //    {
                //        Response.Redirect("/RDW/RDW_DetailList.aspx?id=" + strMID + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString(), true);
                //    }
                //}

                if (!rdw.CheckFinishRDWDetail(strDID, strUID))
                {
                    btnFinish.Visible = false;
                    btnUpload.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    btnFinish.Visible = true;
                    btnCancelFinish.Visible = false;
                   
                }

                if (rdw.CheckCancleMemberFinish(strDID, strUID))
                {
                    btnCancelFinish.Visible = true;           
                }
                else
                {
                    btnCancelFinish.Visible = false;
                }

                if (rdw.CheckLeaderSave(strDID, strUID) || rdw.CanDisapprove(Convert.ToInt32(hidDID.Value), Convert.ToInt32(hidMID.Value), Convert.ToInt32(Session["uID"])))
                {
                    btnUpload.Enabled = true;
                    btnSave.Enabled = true;
                }

                if (rdw.CheckLinkSample(strDID))
                {
                    btnSample.Visible = true;
                    BindgvVendImportDoc();
                }
                else
                {
                    btnSample.Visible = false;
                }

                string checkdelay =rdw.Checkdelay(strDID).Trim();
                if (checkdelay=="1")
                {
                    btndelay.Visible = true;
                    lbldelaydate.Visible = false;
                    lbldelayreason.Visible = false;
                   
                }
                else if (checkdelay == "0")
                {
                  
                    lbldelaydate.Visible = false;
                    lbldelayreason.Visible = false;
                    btndelay.Visible = false;
                }
                else
                {
                    lbldelaydate.Visible = true;
                    lbldelayreason.Visible = true;
                    lbldelaydateshow.Visible = true;
                    lbldelayreasonshow.Visible = true;
                    btndelay.Visible = true;
                    SqlDataReader reader = rdw.SelectRDWDelay(strDID, 1);
                    while (reader.Read())
                    {
                        lbldelaydate.Text =string.Format("{0:yyyy-MM-dd}", reader["RDW_delaytime"]);
                        lbldelayreason.Text = reader["RDW_delayrmk"].ToString();
                    }
                   
                }

            }

            txtStepData.Attributes.Add("style", "height:auto; overflow:visible; border-style:none; border-width:0px");

            BindData();

        }
    }

    protected void BindData()
    {
        //定义参数
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strUID = Convert.ToString(Session["uID"]);
        bool isClosed = false;
        if (Request.QueryString["isClosed"] == "1")
        {
            isClosed = true;
        }

        RDW_Detail rd = rdw.SelectRDWDetailEdit(strDID, isClosed);

        lblProjectData.Text = rd.RDW_Project;
        lblProdCodeData.Text = rd.RDW_ProdCode;
        lblProdDescData.Text = rd.RDW_ProdDesc;
        lblStartDateData.Text = rd.RDW_StartDate;
        lblEndDateData.Text = rd.RDW_EndDate;
        lblStepNameData.Text = rd.RDW_StepName;
        txtStepData.Text = rd.RDW_StepDesc;
        lblStepStartData.Text = rd.RDW_StepStartDate;
        lblStepEndData.Text = rd.RDW_StepEndDate;
        hidStepTitle.Value = rd.RDW_StepTitle;

        switch (Convert.ToString(rd.RDW_Status))
        {
            //初始
            case "0":
                upload.Visible = true;
                notes.Visible = true;
                //chkEmail.Visible = true;
                lblprojStatus.Text = "1";
                break;

            //成员完成
            //case "1":
            //    btnUpload.Enabled = false;
            //    upload.Visible = false;
            //    notes.Visible = true;
            //    chkEmail.Visible = true;
            //    lblprojStatus.Text = "1";
            //    break;

            // 审批完成
            case "2":
                btnSave.Enabled = false;
                btnUpload.Enabled = false;
                upload.Visible = false;
                notes.Visible = false;
                chkEmail.Visible = false;
                lblprojStatus.Text = "2";
                break;
        }
        //add by Shanzm 2015-05-17：决定该步骤是否应显示btnTrack按钮
        //btnTrack.Visible = rd.RDW_needTracking;

        if (rdw.CheckParentID(strDID, strUID))
        {
            btnSave.Enabled = true;
            btnUpload.Enabled = true;
            upload.Visible = true;
            notes.Visible = true;
        }

        RDW_Header rh = rdw.SelectRDWHeader(strMID);
        if (rd.RDW_Extra == false)
        {
            if (rh.RDW_Status != "PROCESS" || isClosed)
            {
                btnSave.Enabled = false;
                btnUpload.Enabled = false;
                upload.Visible = false;
                notes.Visible = false;
                chkEmail.Visible = false;
            }
        }
            trCancelReason.Visible = btnCancelFinish.Visible;
            BindMessage();
            BindUpload();
       
    }

    protected void BindMessage()
    {
        //定义参数
        string strDID = Convert.ToString(Request.QueryString["did"]);

        gvMessage.DataSource = rdw.SelectRDWDetailMessage(strDID);
        gvMessage.DataBind();
    }

    protected void BindUpload()
    {
        //定义参数
        string strDID = Convert.ToString(Request.QueryString["did"]);

        gvUpload.DataSource = rdw.SelectRDWDetailDocs(strDID);
        gvUpload.DataBind();
    }

    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {

            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strDetID = gvUpload.DataKeys[intRow].Values["RDW_DetID"].ToString();
            string strPath = gvUpload.DataKeys[intRow].Values["RDW_Path"].ToString().Trim();
            string strPhysicalName = gvUpload.DataKeys[intRow].Values["RDW_PhysicalName"].ToString();
            string status = gvUpload.DataKeys[intRow].Values["RDW_TransferStatus"].ToString();
            string fileName = gvUpload.DataKeys[intRow].Values["RDW_FileName"].ToString();
            if (status == "False")
            {
                //if (String.IsNullOrEmpty(gvUpload.DataKeys[intRow].Values["RDW_fromDocID"].ToString()))
                //{
                //    ltlAlert.Text = "var w=window.open('/TecDocs/ProjectTracking/" + strDetID + "/" + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

                //}
                //else
                //{
                //    ltlAlert.Text = "var w=window.open('" + strPath + fileName + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

                //}
                if (String.IsNullOrEmpty(strPath))
                {
                    ltlAlert.Text = "var w=window.open('/TecDocs/ProjectTracking/" + strDetID + "/" + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

                }
                else
                {
                    ltlAlert.Text = "var w=window.open('" + strPath + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

                }
            }
            else
            {
                ltlAlert.Text = "var w=window.open('" + strPath + fileName + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

            }
           
        }
        if (e.CommandName.ToString() == "Transfer")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string id = param[0].ToString();
            string status = param[1].ToString();
            string stepID = param[2].ToString();
            Response.Redirect("/Supplier/SampleNotesDocTransferDetail.aspx?sourceDocID=" + id + "&status=" + status + "&source=" + "pj" + "&stepID=" + stepID + "&mid=" + Request.QueryString["mid"]);
        }
    }

    protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //定义参数
        string strDocID = string.Empty;
        string strUID = Convert.ToString(Session["uID"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strMID = Convert.ToString(Request.QueryString["mid"]);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton linkTransfer = (LinkButton)e.Row.FindControl("linkTransfer");
            //获取文档的转移状态,如果已转移则显示
            if (gvUpload.DataKeys[e.Row.RowIndex].Values["RDW_TransferStatus"].ToString() == "False")
            {
                if (String.IsNullOrEmpty(gvUpload.DataKeys[e.Row.RowIndex].Values["RDW_fromDocID"].ToString())) 
                {
                    linkTransfer.Text = "Copy";
                }
                else
                {
                  linkTransfer.Text = "";
                }
                
            }
            else
            {
                linkTransfer.Text = "CopyDetail";
            }


            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

            RDW_Detail rd = rdw.SelectRDWDetailEdit(strDID);
            RDW_Detail_Docs rdd = (RDW_Detail_Docs)e.Row.DataItem;
            strDocID = rdd.RDW_DocsID.ToString();
            if (rdd.RDW_isDelete)
            {
                btnDelete.Enabled = false;
                e.Row.Cells[4].Text = "Deleted";
                linkTransfer.Text = "";
            }
            else
            {
                if (Convert.ToString(rd.RDW_Status) != "0")
                {
                    btnDelete.Enabled = false;
                    btnDelete.Text = "";
                }
                else
                {
                    if (rdd.RDW_UploaderID == Convert.ToString(strUID) && btnFinish.Visible == true)
                    {
                        ;
                    }
                    else
                    {

                        if (rdd.RDW_UploaderID == Convert.ToString(strUID) && btnUpload.Enabled == true)
                        {
                            ;
                        }
                        else
                        {
                            btnDelete.Enabled = false;
                            btnDelete.Text = "";
                        }
                    }
                }
            }

            RDW_Header rh = rdw.SelectRDWHeader(strMID);

            if (rh.RDW_Status != "PROCESS")
            {
                btnDelete.Enabled = false;
                btnDelete.Text = "";
            }
        }
    }

    protected void gvUpload_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDocID = gvUpload.DataKeys[e.RowIndex].Values["RDW_DocsID"].ToString();
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strPhysicalName = gvUpload.DataKeys[e.RowIndex].Values["RDW_PhysicalName"].ToString();
        string strDeleteName = "D_" + strPhysicalName;

        if (rdw.DeleteRDWDetailDocs(strDocID))
        {
            try
            {
                //File.Delete(System.IO.Path.Combine(Server.MapPath("/TecDocs/ProjectTracking/"), strDID, strPhysicalName));
                File.Move(System.IO.Path.Combine(Server.MapPath("/TecDocs/ProjectTracking/"), strDID, strPhysicalName), System.IO.Path.Combine(Server.MapPath("/TecDocs/ProjectTracking/"), strDID, strDeleteName));
            }
            catch
            {
                ;
            }

            ltlAlert.Text = "alert('Delete document successfully!');";
            ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('Delete document failure！'); ";
            BindData();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //定义参数
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strUID = Convert.ToString(Session["uID"]);
        string strUName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strNotes = txtNotes.Text.Trim().Replace("\n", "<br>");
        string strMailFrom = string.Empty;
        string strMailTo = string.Empty;
        StringBuilder sb = new StringBuilder();
        bool isSuccess = true;

        if (strNotes.Trim().Length > 0)
        {
            if (strNotes.Trim().Length > 500)
            {
                ltlAlert.Text = "alert('the length of notes shoule be less than 500!'); ";
                return;
            }

            strNotes = strUName + " : Notes -- " + strNotes.Trim().Replace("\n", "<br>");
            if (rdw.SaveDetailEdit(strDID, strNotes, strUID, strUName))
            {
                if (chkEmail.Checked)
                {
                    MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
                    MailAddress to;
                    MailMessage mail = new MailMessage();
                    mail.From = from;

                    IDataReader reader = rdw.SelectRDWDetailMemberEmail(strDID, strUID);
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                to = new MailAddress(reader["Email"].ToString(), reader["UserName"].ToString());
                                mail.To.Add(to);
                            }
                            catch
                            {
                                ltlAlert.Text = "alert('the email address of " + reader["UserName"].ToString() + "is incorrect.Pls contact with system administrator!');";
                                return;
                            }
                        }
                        reader.Close();
                        if (Session["Email"] != null && Session["Email"].ToString()!="")
                        {
                            MailAddress cc = new MailAddress(Session["Email"].ToString(), Session["uName"].ToString());
                            mail.CC.Add(cc);
                        }
                        try
                        {
                            mail.Subject = "[Notify]Project Tracking System -- Save Notes  " + lblProjectData.Text + "-" +hidStepTitle.Value;
                            sb.Append("<html>");
                            sb.Append("<body>");
                            sb.Append("<form>");
                            sb.Append(" Dear Partners : <br>");
                            sb.Append("     Project Name:" + lblProjectData.Text.Trim() + "<br>");
                            sb.Append("     Product Code:" + lblProdCodeData.Text.Trim() + "<br>");
                            sb.Append("     Step Name:" + lblStepNameData.Text.Trim() + "<br>");
                            sb.Append("     Steps Notes:" + strNotes + "<br>");
                            sb.Append("     中文译-步骤成员:" + Convert.ToString(Session["uName"]) + " 填写了备注，请查看具体详细信息.<br><br>");
                            sb.Append("     Please see detail information.<br>");
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
                        catch
                        {
                            reader.Close();
                            isSuccess = false;
                        }
                        finally
                        {
                            if (isSuccess)
                            {
                                ltlAlert.Text = "alert('Save data and send email successfully!');";
                                txtNotes.Text = string.Empty;
                                BindData();
                                // ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
                            }
                            else
                            {
                                ltlAlert.Text = "alert('Save data successfully!');";
                                txtNotes.Text = string.Empty;
                                BindData();
                            }
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('Save data successfully!');";
                        txtNotes.Text = string.Empty;
                        BindData();
                    }
                }
                else
                {
                    if (isSuccess)
                    {
                        ltlAlert.Text = "alert('Save data successfully!');";
                        txtNotes.Text = string.Empty;
                        BindData();
                        //ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
                    }
                }

            }
            else
            {
                ltlAlert.Text = "alert('Save data failure!'); ";
            }
        }
        else
        {
            ltlAlert.Text = "alert('Please enter notes!'); ";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //定义参数
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strST = Request.QueryString["st"] == null ? "" : Convert.ToString(Request.QueryString["st"]);

        if (Convert.ToString(Request.QueryString["fr"]) == "znqap")
        {
            Response.Redirect("/RDW/RDW_Approval.aspx?id=" + strMID + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString(), true);
        }
        else if (Convert.ToString(Request.QueryString["fr"]) == "projlog")
        {
            this.Redirect("/RDW/RDW_ProjLog.aspx");
        }
        else if (Convert.ToString(Request.QueryString["fr"]) == "doclist")
        {
            Response.Redirect("/RDW/RDW_DocList.aspx?mid=" + strMID);
        }
        else if (Convert.ToString(Request.QueryString["fr"]) == "prod")
        {
            Response.Redirect("/RDW/prod_Report.aspx");
        }    
        else
        {
            if (Convert.ToString(Request.QueryString["fr"]) == "znqmb")
            {
                Response.Redirect("/RDW/RDW_Member.aspx?id=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("/RDW/RDW_DetailList.aspx?mid=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        bool isSuccess = true;
        if (!fileAttachFile.HasFile)
        {
            this.Alert("Please select a file！");
            return;
        }
        else
        {
            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("#") > 0)
            {
                this.Alert("The file name contains illegal characters:#");
                return;
            }

            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("%") > 0)
            {
                this.Alert("The file name contains illegal characters:%");
                return;
            }

            if (fileAttachFile.ContentLength > 1024 * 1024 * 100)
            {
                this.Alert("The file is larger than 100M, can not be uploaded！");
                return;
            }

            if (!rdw.CheckFileExtension(Path.GetExtension(fileAttachFile.FileName)))
            {
                this.Alert("Illegal file extension！");
                return;
            }
        }

        //定义参数
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        string _stepID = Convert.ToString(Request.QueryString["did"]);
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strMailFrom = string.Empty;
        string strMailTo = string.Empty;
        StringBuilder sb = new StringBuilder();

        string _fileName = Path.GetFileName(fileAttachFile.FileName);//文件名，包含后缀
        string _fileExtension = Path.GetExtension(fileAttachFile.FileName);//文件后缀
        string _targetFolder = Server.MapPath("/TecDocs/ProjectTracking/" + _stepID + "/");

        string _newFileName = DateTime.Now.ToFileTime().ToString() + _fileExtension;//合并两个路径为上传到服务器上的全路径

        try
        {
            if (!Directory.Exists(_targetFolder))
            {
                Directory.CreateDirectory(_targetFolder);
            }

            this.fileAttachFile.MoveTo(_targetFolder + "/" + _newFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
        }
        catch
        {
            this.Alert("Failed to upload！");
            return;
        }

        if (rdw.UploadFile(_fileName, _newFileName, _uID, _uName, _stepID))
        {
            BindData();
            if (chkEmail.Checked)
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
                MailAddress to;
                MailMessage mail = new MailMessage();
                mail.From = from;

                IDataReader reader = rdw.SelectRDWDetailMemberEmail(_stepID, _uID);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            to = new MailAddress(reader["Email"].ToString(), reader["UserName"].ToString());
                            mail.To.Add(to);
                        }
                        catch
                        {
                            ltlAlert.Text = "alert('the email address of " + reader["UserName"].ToString() + "is incorrect.Pls contact with system administrator!');";
                            return;
                        }
                    }

                    reader.Close();
                    try
                    {
                        mail.Subject = "[Notify]Product Development System Email -- Upload file  " + lblProjectData.Text + " - " +hidStepTitle.Value;
                        sb.Append("<html>");
                        sb.Append("<body>");
                        sb.Append("<form>");
                        sb.Append(" Dear Partner: <br>");
                        sb.Append("     Project Name:" + lblProjectData.Text.Trim() + "<br>");
                        sb.Append("     Product Code:" + lblProdCodeData.Text.Trim() + "<br>");
                        sb.Append("     Step Name:" + lblStepNameData.Text.Trim() + "<br>");
                        sb.Append("     Uploaded a file : " + _fileName + ", Please see detail information.<br>");
                        sb.Append("     中文译-步骤:" + Convert.ToString(Session["uName"]) + "上传了文件: " + _fileName + ", 请查看具体信息并审批.<br><br>");
                        sb.Append("     Please click follow link to view:<br>");
                        sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + _stepID + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + _stepID + "</a>");
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
                    catch
                    {
                        reader.Close();
                        isSuccess = false;
                    }
                    finally
                    {
                        if (isSuccess)
                        {
                            ltlAlert.Text = "alert('Upload file and send email successfully!');";
                            ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + _stepID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                        }
                        else
                        {
                            ltlAlert.Text = "alert('Upload file successfully!');";
                            ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + _stepID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                        }
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('Upload file successfully!');";
                    ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + _stepID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                }
            }
            else
            {
                ltlAlert.Text = "alert('Upload file successfully!'); ";
                ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + _stepID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            }
        }
        else
        {
            ltlAlert.Text = "alert('Upload file failure!');";
        }
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        //if (!rdw.CheckIsAllSubTasksCompleted(Request.QueryString["did"].ToString()))
        //{
        //    ltlAlert.Text = "alert('Can not Member Finished!because not all subtasks are completed!');";
        //    return;
        //}
        //定义参数
        string strNotes = txtNotes.Text.Trim();
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strUID = Convert.ToString(Session["uID"]);
        string strUName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strMailFrom = string.Empty;
        string strMailTo = string.Empty;
        StringBuilder sb = new StringBuilder();
        bool isSuccess = false;

        if (strNotes.Trim().Length == 0)
        {
            if (rdw.CheckMessageExists(strDID, strUID))
            {
                strNotes = strUName + " :Member Finish Step";
            }
            else
            {
                ltlAlert.Text = "alert('You must upload file or fill in the notes'); ";
                return;
            }
        }
        else
        {
            if (strNotes.Trim().Length > 500)
            {
                ltlAlert.Text = "alert('The length of notes should be less than 500!'); ";
                return;
            }
            strNotes = strUName + " :Member Finish Step , " + " Notes -- " + strNotes.Replace("\n", "<br>");
        }
        int status = rdw.UpdateFinishRDWDetail(strDID, strUID, strNotes, strUName);
        if (status >= 1)
        {
            if (chkEmail.Checked && status>=2)
            {
                //IDataReader reader = rdw.SelectRDWDetailMemberEmail(strDID, strUID);

                // 2013-11-19 caixia modify ,当某步骤完成时，通知项目的所有成员
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
                MailAddress to;
                MailMessage mail = new MailMessage();
                mail.From = from;

                //Add By Shanzm 2014-03-03:当所有步骤都已审核时，发送“项目完成”的通知
                string _RDW_Status = "";

                IDataReader reader = rdw.SelectRDWProjectAllMemberEmail(strMID, strUID);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        //Add By  2014-03-03:当所有步骤都已审核时，发送“项目完成”的通知
                        if (reader["UserName"].ToString().Length == 0)
                        {
                            _RDW_Status = reader["RDW_Status"].ToString();
                        }
                        else
                        {
                            try
                            {
                                to = new MailAddress(reader["Email"].ToString(), reader["UserName"].ToString());
                                mail.To.Add(to);
                            }
                            catch
                            {
                                ltlAlert.Text = "alert('the email address of " + reader["UserName"].ToString() + "is incorrect.Pls contact with system administrator!');";
                                return;
                            }
                        }
                    }

                    reader.Close();
                    if (Session["Email"] != null && Session["Email"].ToString() != "")
                    {
                        MailAddress cc = new MailAddress(Session["Email"].ToString(), Session["uName"].ToString());
                        mail.CC.Add(cc);
                    }
                    try
                    {
                        mail.Subject = "[Notify]Project Tracking System -- Member Finish Step  " + lblProjectData.Text + "-" + hidStepTitle.Value;
                        sb.Append("<html>");
                        sb.Append("<body>");
                        sb.Append("<form>");
                        sb.Append(" Dear  Partner  <br>");
                        sb.Append("     Project Name:" + lblProjectData.Text.Trim() + "<br>");
                        sb.Append("     Product Code:" + lblProdCodeData.Text.Trim() + "<br>");
                        sb.Append("     Step Name:" + lblStepNameData.Text.Trim() + "<br>");
                        //Add By Shanzm 2014-03-03:当所有步骤都已审核时，发送“项目完成”的通知
                        if (_RDW_Status == "CLOSE")
                        {
                            sb.Append("     <font style='font-weight:bold;'>This project has been completed when this last step is approved.</font><br>");
                        }

                        sb.Append("      Notes:" + strNotes + "<br>");
                        sb.Append("     中文译-- 步骤成员: " + Session["uName"].ToString() + "完成了这步骤，请链接以下地址时查看详细信息<br><br>");
                        sb.Append("     Please see detail information.<br>");
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
                    catch
                    {
                        isSuccess = false;
                    }
                    finally
                    {
                        if (isSuccess)
                        {
                            btnFinish.Enabled = false;
                            ltlAlert.Text = "alert('Member Finish and send email successfully!');";
                            ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                        }
                        else
                        {
                            btnFinish.Enabled = false;
                            ltlAlert.Text = "alert('Member Finish successfully!');";
                            ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                        }
                    }
                }
                else
                {
                    btnFinish.Enabled = false;
                    ltlAlert.Text = "alert('Member Finish successfully!');";
                    ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                }
            }
            else
            {
                btnFinish.Enabled = false;
                ltlAlert.Text = "alert('Member Finish successfully!');";
                ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            }
        }
        else
        {
            ltlAlert.Text = "alert('Member Finish failure!');";
        }
    }

    protected void lnkMessage_Click(object sender, EventArgs e)
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        ltlAlert.Text = "var w=window.open('/RDW/RDW_ViewMessage.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString();
        ltlAlert.Text += "','ViewMessage','menubar=No,scrollbars=No,resizable=No,width=700,height=500,top=200,left=200'); w.focus();";
    }

    protected void btnCancelFinish_Click(object sender, EventArgs e)
    {
        String strReason = txtReason.Text.Trim().ToString();
        if (strReason.Length <= 0)
        {
            ltlAlert.Text = "alert('Please enter cancel finish reason !'); ";
            return;
        }
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strUID = Convert.ToString(Session["uID"]);
        string strUName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        if (rdw.CancelMemberFinish(strMID, strDID, strUID, strUName, strReason))
        {
            StringBuilder sb = new StringBuilder();
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
            MailAddress to;
            MailMessage mail = new MailMessage();
            mail.From = from;

            IDataReader reader = rdw.SelectRDWDetailMemberEmail(strDID, strUID);
            if (reader != null)
            {
                while (reader.Read())
                {
                    try
                    {
                        to = new MailAddress(reader["Email"].ToString(), reader["UserName"].ToString());
                        mail.To.Add(to);
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('the email address of " + reader["UserName"].ToString() + "is incorrect.Pls contact with system administrator!');";
                        return;
                    }
                }
                reader.Close();

                if (Session["Email"] != null  && Session["Email"].ToString() != "")
                {
                    MailAddress cc = new MailAddress(Session["Email"].ToString(), Session["uName"].ToString());
                    mail.CC.Add(cc);
                }
                try
                {
                    mail.Subject = "[Notify]Project Tracking System Email -- Cancel Finish  " + lblProjectData.Text + "-" + hidStepTitle.Value;
                    sb.Append("<html>");
                    sb.Append("<body>");
                    sb.Append("<form>");
                    sb.Append(" Dear Partners:<br>");
                    sb.Append("     Project Name:" + lblProjectData.Text.Trim() + "<br>");
                    sb.Append("     Product Code:" + lblProdCodeData.Text.Trim() + "<br>");
                    sb.Append("     Step Name:" + lblStepNameData.Text.Trim() + "<br>");
                    sb.Append("     Step Notes:" + txtNotes.Text.Trim().ToString() + "<br>");
                    sb.Append("     Cancel Finish Reason:" + strReason + "<br>");
                    sb.Append("     Please note the Member: " + Session["uName"].ToString() + "cancel this step finish.<br>");
                    sb.Append("     中文译 - 步骤成员:" + Session["uName"].ToString() + "取消了这步骤的完成，请知悉 <br>");
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

                    ltlAlert.Text = "alert('Cancel Member Finish and Send Email successfully !');";
                    ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
                }
                catch
                {
                    ltlAlert.Text = "alert('Cancel Member Finish successfully!');";
                    ltlAlert.Text += " window.location.href='/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=" + strQuy + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";

                }
                finally
                {
                    sb.Remove(0, sb.Length);
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert(' Fail to Cancel Member Finish !');";
        }
    }
    protected void btnSample_Click(object sender, EventArgs e)
    {
         Response.Redirect("/Supplier/SampleNotesLists.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
 
    }

    private void BindgvVendImportDoc()
    {

        DataTable dt = rdw.getBosSuppImportDocs(Convert.ToString(Request.QueryString["did"]));
        if (dt.Rows.Count == 0)
        {
            gvlist.Visible = false;
        }
        else
        {
            gvlist.DataSource = dt;
            gvlist.DataBind();
        }
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = gvrow.RowIndex;
            string[] param = e.CommandArgument.ToString().Split(',');
            string vend = param[0].ToString();
            string docid = param[1].ToString();
            string path = param[2].ToString();
            string status = param[3].ToString();
            if (status == "False")
            {
                if (string.IsNullOrEmpty(path))
                {
                    ltlAlert.Text = "var w=window.open('/TecDocs/Supplier/" + vend + "/" + docid + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300');w.focus();";
                }
                else
                {
                    ltlAlert.Text = "var w=window.open('" + path + docid + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300');w.focus();";
                }
            }
            else
            {
                ltlAlert.Text = "var w=window.open('" + path + gvlist.Rows[index].Cells[0].Text + "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300');w.focus();";
            }
        }
    }
    protected void BtnDoc_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_FromDocs.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
       // ltlAlert.Text = "var w=window.open('/RDW/RDW_FromDocs.aspx?did=" + Convert.ToString(Request.QueryString["did"]) + "&rm=" + DateTime.Now.ToString() + "','','menubar=no,scrollbars = no,resizable=no,titlebar=no,toolbar=no,status=no,width=800,height=500,top=0,left=0'); w.focus();";
    }

    protected void btndelay_Click(object sender, EventArgs e)
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = Convert.ToString(Request.QueryString["did"]);
        string strQuy = Request.QueryString["fr"] == null ? string.Empty : Convert.ToString(Request.QueryString["fr"]);
        RDW_Detail rd = rdw.SelectRDWDetailEdit(strDID);

        string StepName = rd.RDW_StepName;
        string NewStepName=StepName.Replace("\n", "");
       // string url = "/RDW/RDW_AddDelay.aspx?mid=" + strMID + "&fr=" + strQuy + "&name=" + StepName + "&did=" + strDID + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString();
        //Response.Redirect("/RDW/RDW_AddDelay.aspx?mid=" + strMID + "&fr=" + strQuy + "&name=" + StepName + "&did=" + strDID + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        Response.Redirect("RDW_AddDelay.aspx?mid=" + strMID + "&fr=" + strQuy + "&name=" + NewStepName + "&did=" + strDID + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
    }

    protected void btn_Doc_Click(object sender, EventArgs e)
    {

        string strID = Convert.ToString(Request.QueryString["mid"]);
        //if (rdw.insertUL(strID, Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        //{
            //string strID = Convert.ToString(Request.QueryString["mid"]);
            string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
            Response.Redirect("/RDW/UL_Doc.aspx?t=&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        //}
        //else
        //{
        //    ltlAlert.Text = "alert('新建UL失败');";
        //}
       
    }
    protected void btnTrack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Prod_AppNew.aspx?projectName=" + lblProjectData.Text + "&code=" + lblProdCodeData.Text +
                           "&mid=" + Request.QueryString["mid"] + "&cateid=" + Request.QueryString["mid"] + "&did=" +
                           Request.QueryString["mid"] + "&isClosed=" + Request.QueryString["mid"] + "&fr=" + Request.QueryString["mid"] + 
                           "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + 
                           Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + 
                           Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
    }
    protected void btn_disApprove_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_ViewPartner.aspx?mid=" + hidMID.Value + "&did=" + hidDID.Value + "&cateid=" + Request.QueryString["cateid"]);
    }
    protected void btnTest_Click(object sender, EventArgs e)
    {
        Response.Redirect("../RDW/Test_Report.aspx?projectName=" + lblProjectData.Text + "&projectcode=" + lblProdCodeData.Text +
                           "&mid=" + Request.QueryString["mid"] + "&cateid=" + Request.QueryString["mid"] + "&did=" +
                           Request.QueryString["mid"] + "&isClosed=" + Request.QueryString["mid"] + "&fr=" + Request.QueryString["mid"] +
                           "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" +
                           Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" +
                           Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
    }
}
