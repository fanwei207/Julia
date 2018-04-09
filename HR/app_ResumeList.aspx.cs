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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;


public partial class HR_app_ResumeList : BasePage
{
    adamClass chk = new adamClass();
    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            butUpResume.Visible = this.Security["6080090"].isValid;
            butSendEmail.Visible = this.Security["6080090"].isValid;
            btnImport.Visible = this.Security["6080090"].isValid;
            txtDepart.Enabled = false;
            txtProc.Enabled = false;
            txtCop.Enabled = false;
            txtCop.Text = Request.QueryString["App_Company"];
            txtDepart.Text = Request.QueryString["App_department"];
            txtProc.Text = Request.QueryString["App_Process"];
            txtPlantcode.Text = Request.QueryString["App_plantCode"];
            txtDepartID.Text = Request.QueryString["App_departmentID"];
            txtProcID.Text = Request.QueryString["App_ProcID"];
            txtPlantcode.Visible = false;
            txtDepartID.Visible = false;
            txtProcID.Visible = false;

            if (!judgeExistNoSendEmail(txtCop.Text, txtDepart.Text, txtProc.Text))
            {
                butSendEmail.Visible = false;
            }
            BindData();
        }
        else
        {
            if (!judgeExistNoSendEmail(txtCop.Text, txtDepart.Text, txtProc.Text))
            {
                butSendEmail.Visible = false;
            }
            BindData();
        }
    }
    protected void butUpResume_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_UploadResume.aspx?company=" + txtCop.Text + "&department=" + txtDepart.Text + "&process=" + txtProc.Text
            + "&plantcode=" + txtPlantcode.Text + "&departmentid=" + txtDepartID.Text + "&processid=" + txtProcID.Text);
    }
    private void BindData()
    {
        DataTable dt = GetInformationList(txtDepart.Text,txtProc.Text,txtCop.Text);
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable GetInformationList(string department,string process,string company)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@department", department);
        param[1] = new SqlParameter("@process", process);
        param[2] = new SqlParameter("@company", company);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_selectInformationList", param).Tables[0];
    }
    protected void butBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_RecruitmentRequestList.aspx");
    }
    public static string GetFilePath(int fsid)
    {
        string str = "sp_app_getFilePath";
        SqlParameter parms = new SqlParameter("@fsid", fsid);
        DataTable dt = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, str, parms).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
            return "";
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownLoad")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string filePath = gv.DataKeys[index].Values["fpath"].ToString();
            try
            {
                filePath = Server.MapPath(filePath);
                filePath = filePath.Replace("\\", "/");
            }
            catch (Exception)
            {

                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }

            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName == "ResumeYse")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (updateResumestatus(Request.QueryString["App_department"], Request.QueryString["App_Process"], Request.QueryString["App_Company"], gv.DataKeys[index].Values["userName"].ToString(), 0, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                BindData();
            }
        }
        if (e.CommandName == "ResumeNo")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (updateResumestatus(Request.QueryString["App_department"], Request.QueryString["App_Process"], Request.QueryString["App_Company"], gv.DataKeys[index].Values["userName"].ToString(), 1, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                BindData();
            }
        }
        if (e.CommandName == "Employ")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (updateResumestatus(Request.QueryString["App_department"], Request.QueryString["App_Process"], Request.QueryString["App_Company"], gv.DataKeys[index].Values["userName"].ToString(), 2, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                BindData();
            }
        }
        if (e.CommandName == "UnEmploy")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (updateResumestatus(Request.QueryString["App_department"], Request.QueryString["App_Process"], Request.QueryString["App_Company"], gv.DataKeys[index].Values["userName"].ToString(), 3, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                BindData();
            }
        }
        if (e.CommandName == "Resume")
        { }
        if (e.CommandName == "GetInto")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (updateResumestatus(Request.QueryString["App_department"], Request.QueryString["App_Process"], Request.QueryString["App_Company"], gv.DataKeys[index].Values["userName"].ToString(), 4, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                BindData();
            }
        }
        if (e.CommandName == "GetOutTo")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (updateResumestatus(Request.QueryString["App_department"], Request.QueryString["App_Process"], Request.QueryString["App_Company"], gv.DataKeys[index].Values["userName"].ToString(), 5, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                BindData();
            }
        }
        if (e.CommandName == "Reserve")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (updateResumestatus(Request.QueryString["App_department"], Request.QueryString["App_Process"], Request.QueryString["App_Company"], gv.DataKeys[index].Values["userName"].ToString(), 6, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                BindData();
            }
        }
    }
    /// <summary>
    /// 进入面试流程
    /// </summary>
    /// <returns></returns>
    public bool updateInterviewstatus(string deparment, string process, string company, string username)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@deparment", deparment);
        param[2] = new SqlParameter("@process", process);
        param[3] = new SqlParameter("@username", username);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_updateinterviewstatus", param));
    }
    /// <summary>
    /// 同意面试
    /// </summary>
    /// <returns></returns>
    public bool updateResumestatus(string deparment, string process, string company, string username, int status,int uid, string name)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@deparment", deparment);
        param[2] = new SqlParameter("@process", process);
        param[3] = new SqlParameter("@username", username);
        param[4] = new SqlParameter("@status", status);
        param[5] = new SqlParameter("@uid", uid);
        param[6] = new SqlParameter("@uname", name);


        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_updateResumestatus", param));
    }
    /// <summary>
    /// 判断当前用户角色是否具有对面试、聘用的权限
    /// </summary>
    /// <returns></returns>
    public bool checkPower(string roleid)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@roleid", roleid);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_checkUserPower", param));
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton linkEmploy = e.Row.FindControl("linkEmploy") as LinkButton;
            LinkButton linkUnEmploy = e.Row.FindControl("linkUnEmploy") as LinkButton;
            LinkButton linkReserve = e.Row.FindControl("linkReserve") as LinkButton;

            LinkButton ResumeYse = e.Row.FindControl("linkResumeYse") as LinkButton;
            LinkButton ResumeNo = e.Row.FindControl("linkResumeNo") as LinkButton;

            LinkButton GetInto = e.Row.FindControl("linkInto") as LinkButton;
            LinkButton GetOutTo = e.Row.FindControl("linkOutTo") as LinkButton;
            
            if (gv.DataKeys[e.Row.RowIndex].Values["status"] == string.Empty)
            {
                if (e.Row.Cells[9].Enabled = this.Security["6080180"].isValid)
                {
                    if (Session["deptID"].ToString() != txtDepartID.Text)
                    {
                        e.Row.Cells[9].Enabled = false;
                    }
                }
                linkEmploy.ForeColor = System.Drawing.Color.Gray;
                linkUnEmploy.ForeColor = System.Drawing.Color.Gray;
                linkReserve.ForeColor = System.Drawing.Color.Gray;

                ResumeYse.ForeColor = System.Drawing.Color.Blue;
                ResumeNo.ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[10].Enabled = false;

                GetInto.ForeColor = System.Drawing.Color.Gray;
                GetOutTo.ForeColor = System.Drawing.Color.Gray;
                e.Row.Cells[11].Enabled = false;

                e.Row.Cells[12].Enabled = false;
            }
            else 
            {
                if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "同意面试")
                {
                    if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["examinationTime"]) != string.Empty)
                    {
                        if(e.Row.Cells[10].Enabled = this.Security["6080180"].isValid)
                        {
                            if (Session["deptID"].ToString() != txtDepartID.Text)
                            {
                                e.Row.Cells[10].Enabled = false;
                            }
                        }

                        ResumeYse.ForeColor = System.Drawing.Color.Red; 
                        ResumeNo.ForeColor = System.Drawing.Color.Gray;
                        linkEmploy.ForeColor = System.Drawing.Color.Blue;
                        linkUnEmploy.ForeColor = System.Drawing.Color.Blue;
                        GetInto.ForeColor = System.Drawing.Color.Gray;
                        GetOutTo.ForeColor = System.Drawing.Color.Gray;
                        linkReserve.ForeColor = System.Drawing.Color.Gray;
                        e.Row.Cells[9].Enabled = false;
                        e.Row.Cells[11].Enabled = false;
                        e.Row.Cells[12].Enabled = false;
                    }
                    else
                    {
                        ResumeYse.ForeColor = System.Drawing.Color.Red;
                        ResumeNo.ForeColor = System.Drawing.Color.Gray;
                        linkEmploy.ForeColor = System.Drawing.Color.Gray;
                        linkUnEmploy.ForeColor = System.Drawing.Color.Gray;
                        GetInto.ForeColor = System.Drawing.Color.Gray;
                        GetOutTo.ForeColor = System.Drawing.Color.Gray;
                        linkReserve.ForeColor = System.Drawing.Color.Gray;
                        e.Row.Cells[9].Enabled = false;
                        e.Row.Cells[10].Enabled = false;
                        e.Row.Cells[11].Enabled = false;
                        e.Row.Cells[12].Enabled = false;
                    }
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "拒绝面试")
                {
                    linkEmploy.ForeColor = System.Drawing.Color.Gray;
                    linkUnEmploy.ForeColor = System.Drawing.Color.Gray;
                    linkReserve.ForeColor = System.Drawing.Color.Gray;

                    ResumeYse.ForeColor = System.Drawing.Color.Gray;
                    ResumeNo.ForeColor = System.Drawing.Color.Red;

                    GetInto.ForeColor = System.Drawing.Color.Gray;
                    GetOutTo.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[12].Enabled = false;
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "同意聘用")
                {

                    e.Row.Cells[11].Enabled = this.Security["6080190"].isValid;

                    linkEmploy.ForeColor = System.Drawing.Color.Red;
                    linkUnEmploy.ForeColor = System.Drawing.Color.Gray;

                    ResumeYse.ForeColor = System.Drawing.Color.Red;
                    ResumeNo.ForeColor = System.Drawing.Color.Gray;

                    GetInto.ForeColor = System.Drawing.Color.Blue;
                    GetOutTo.ForeColor = System.Drawing.Color.Blue;

                    linkReserve.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[12].Enabled = false;
                    
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "拒绝聘用")
                {
                    linkEmploy.ForeColor = System.Drawing.Color.Gray;
                    linkUnEmploy.ForeColor = System.Drawing.Color.Red;
                    linkReserve.ForeColor = System.Drawing.Color.Blue;

                    ResumeYse.ForeColor = System.Drawing.Color.Gray;
                    ResumeNo.ForeColor = System.Drawing.Color.Red;

                    GetInto.ForeColor = System.Drawing.Color.Gray;
                    GetOutTo.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[11].Enabled = false;
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "同意录用")
                {
                    linkEmploy.ForeColor = System.Drawing.Color.Red;
                    linkUnEmploy.ForeColor = System.Drawing.Color.Gray;

                    ResumeYse.ForeColor = System.Drawing.Color.Red;
                    ResumeNo.ForeColor = System.Drawing.Color.Gray;

                    GetInto.ForeColor = System.Drawing.Color.Red;
                    GetOutTo.ForeColor = System.Drawing.Color.Gray;

                    linkReserve.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[12].Enabled = false;
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "拒绝录用")
                {
                    linkEmploy.ForeColor = System.Drawing.Color.Red;
                    linkUnEmploy.ForeColor = System.Drawing.Color.Gray;

                    ResumeYse.ForeColor = System.Drawing.Color.Red;
                    ResumeNo.ForeColor = System.Drawing.Color.Gray;

                    GetInto.ForeColor = System.Drawing.Color.Gray;
                    GetOutTo.ForeColor = System.Drawing.Color.Red;

                    linkReserve.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[11].Enabled = false;
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "人才储备")
                {
                    linkEmploy.ForeColor = System.Drawing.Color.Gray;
                    linkUnEmploy.ForeColor = System.Drawing.Color.Gray;

                    ResumeYse.ForeColor = System.Drawing.Color.Red;
                    ResumeNo.ForeColor = System.Drawing.Color.Gray;

                    GetInto.ForeColor = System.Drawing.Color.Gray;
                    GetOutTo.ForeColor = System.Drawing.Color.Gray;

                    linkReserve.ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[12].Enabled = false;
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "已建档")
                {
                    linkEmploy.ForeColor = System.Drawing.Color.Red;
                    linkUnEmploy.ForeColor = System.Drawing.Color.Gray;

                    ResumeYse.ForeColor = System.Drawing.Color.Red;
                    ResumeNo.ForeColor = System.Drawing.Color.Gray;

                    GetInto.ForeColor = System.Drawing.Color.Red;
                    GetOutTo.ForeColor = System.Drawing.Color.Gray;

                    linkReserve.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[12].Enabled = false;
                }              
            }
        }
    }
    protected void btnchoose_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('app_accessChooseApprove.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"; 
    }
    protected void butSendEmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_ResumeEmail.aspx?company=" + txtCop.Text + "&department=" + txtDepart.Text + "&process=" + txtProc.Text);
        

        //string strMailTo = txtChooseEmail.Text;
        //string strMailFrom = string.Empty;

        //MailMessage mail = new MailMessage();
        //StringBuilder sb = new StringBuilder();
        ////获取发件人邮箱
        //SqlDataReader reader2 = GetUserEmail(Convert.ToInt32(Session["uID"]));
        //if (reader2.Read())
        //{
        //    strMailFrom = Convert.ToString(reader2["email"]);
        //}
        //reader2.Close();

        //mail.To = strMailTo;
        //mail.From = strMailFrom;

        //mail.Subject = "100系统-->招聘申请邮件";
        //sb.Append("<html>");
        //sb.Append("<body>");
        //sb.Append("<form>");
        ////sb.Append("  " + dt1.Rows[i]["userName"].ToString() + ":<br>");
        ////sb.Append("     " + txtCreatedBy.Text.Trim() + "通过了您已经审批过的" + workflowName + "流程!" + "<br>");
        ////sb.Append("     申请号:" + txtReqNbr.Text.Trim() + "<br>");
        ////sb.Append("     备注:" + txtRemark.Text.Trim() + "<br>");
        //sb.Append(txtCop.Text.Trim() + "公司" + txtDepart.Text.Trim() + "的" + txtProc.Text.Trim() + "一职，有了新的简历");
        ////sb.Append("<br>");
        //sb.Append("     请您处理一下！" + "<br>");
        //sb.Append("     谢谢！" + "<br>");
        //sb.Append("     具体菜单是：工资结算--HR2 --> 员工入职审批>> --> 招聘列表 （简历）" + "<br>");
        ////sb.Append("     查看该流程的具体信息请点击下面的链接:<br>");
        ////sb.Append("         <a href='"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/WF/WF_WorkFlowInstanceReview.aspx?nbr=" + txtReqNbr.Text.Trim() + "&tp=2</a>");
        //sb.Append("</body>");
        //sb.Append("</form>");
        //sb.Append("</html>");
        //mail.BodyFormat = MailFormat.Html;
        //mail.Body = Convert.ToString(sb);
        //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
        //SmtpMail.Send(mail);

        //更改已发送人的邮件状态 
        //if (changeSendEmailStatus(txtCop.Text, txtDepart.Text, txtProc.Text))
        //{
        //    ltlAlert.Text = "alert('邮件发送成功!')";
        //    if (!judgeExistNoSendEmail(txtCop.Text, txtDepart.Text, txtProc.Text))
        //    {
        //        Label1.Visible = false;
        //        txtChooseName.Visible = false;
        //        btnchoose.Visible = false;
        //        butSendEmail.Visible = false;
        //    }
        //}

    }
    /// <summary>
    /// 获取当前用户的邮箱
    /// </summary>
    /// <param name="e"></param>
    public SqlDataReader GetUserEmail(int uID)
    {
        SqlParameter param = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_app_selectUseremail", param);
    }
    /// <summary>
    /// 判断是否存在未发送邮件的应试人员
    /// </summary>
    /// <returns></returns>
    public bool judgeExistNoSendEmail(string company,string department,string process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeExistNoSendEmail", param));
    }
    /// <summary>
    /// 更改已发送人的邮件状态
    /// </summary>
    /// <returns></returns>
    public bool changeSendEmailStatus(string company, string department, string process)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_changeSendEmailStatus", param));
    }
    /// <summary>
    /// 人才库导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_ImportTalentList.aspx?company=" + txtCop.Text + "&department=" + txtDepart.Text + "&process=" + txtProc.Text + "&plantCode=" + txtPlantcode.Text + "&departmentID=" + txtDepartID.Text + "&processID=" + txtProcID.Text);
    }
}