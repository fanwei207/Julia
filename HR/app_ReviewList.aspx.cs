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
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;


public partial class HR_app_ReviewList : BasePage
{
    adamClass chk = new adamClass();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("6080200", "岗位维护、批准权限");
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string plantcode = Request.QueryString["App_plantCode"];
            string departmentid = Request.QueryString["App_departmentID"];
            string processid = Request.QueryString["App_ProcID"];
            btnApproval.Visible = this.Security["6080200"].isValid;
            btnProcMaintain.Visible = this.Security["6080200"].isValid;
            if (Request.QueryString["App_Process"] != "其他岗位")
            {
                btnProcMaintain.Enabled = false;
            }
            else
            {
                btnApproval.Enabled = false;
            }
            if (!judgeAppStatus(Request.QueryString["App_Company"], Request.QueryString["App_department"], Request.QueryString["App_Process"]))
            {
                btnRefuse.Enabled = false;
                btnSubmit.Enabled = false;
            }
            if (!judgeExistReviewName(Request.QueryString["App_Company"], Request.QueryString["App_department"], Request.QueryString["App_Process"], Session["uName"].ToString()))
            {
                btnRefuse.Enabled = false;
                btnSubmit.Enabled = false;
            }
            else 
            {
                if (!judgeLastReviewName(Request.QueryString["App_Company"], Request.QueryString["App_department"], Request.QueryString["App_Process"],Session["uName"].ToString()))
                {
                    btnRefuse.Enabled = false;
                    btnSubmit.Enabled = false;
                }
            }
            if(!judgeIsOverReview(Request.QueryString["App_Company"], Request.QueryString["App_department"], Request.QueryString["App_Process"]))
            {
                btnSubmit.Visible = false;
                btnRefuse.Visible = false;
                btnApproval.Visible = false;
                btnProcMaintain.Visible = false;
            }
            txtDepart.Enabled = false;
            txtProc.Enabled = false;
            txtCop.Enabled = false;
            txtCop.Text = Request.QueryString["App_Company"];
            txtDepart.Text = Request.QueryString["App_department"];
            if (Convert.ToString(Request.QueryString["App_Process"]) == "其他岗位")
            {
                txtProc.Text = "（新）" + Request.QueryString["App_NewProc"];
            }
            else
            {
                txtProc.Text = Request.QueryString["App_Process"];
            }
            BindData();
        }
    }
    /// <summary>
    /// 判断当前用户是否是审核列表中的最近的审核人
    /// </summary>
    /// <returns></returns>
    public bool judgeLastReviewName(string company, string depart, string proc ,string username)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@depart", depart);
        param[2] = new SqlParameter("@proc", proc);
        param[3] = new SqlParameter("@username", username);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeLastReviewName", param));
    }
    /// <summary>
    /// 判断审核是否完成
    /// </summary>
    /// <returns></returns>
    public bool judgeIsOverReview(string company, string depart, string proc)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@depart", depart);
        param[2] = new SqlParameter("@proc", proc);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeIsOverReview", param));
    }
    /// <summary>
    /// 判断当前招聘的状态,如果是招聘中则代表审核结束，即不能操作
    /// </summary>
    /// <returns></returns>
    public bool judgeAppStatus(string company, string depart, string proc)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@depart", depart);
        param[2] = new SqlParameter("@proc", proc);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeAppStatus", param));
    }
    /// <summary>
    /// 判断当前用户是否在审核列表中存在
    /// </summary>
    /// <returns></returns>
    public bool judgeExistReviewName(string company, string depart, string proc, string username)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@depart", depart);
        param[2] = new SqlParameter("@proc", proc);
        param[3] = new SqlParameter("@username", username);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeExistReviewName", param));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_RecruitmentRequestList.aspx");
    }
    private void BindData()
    {
        txtChooseName.Text = string.Empty;
        DataTable dt = GetReviewList(txtDepart.Text, Request.QueryString["App_Process"], txtCop.Text);
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable GetReviewList(string department, string process, string company)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@process", process);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_SelectReviewList", param).Tables[0];
    }
    protected void btnNextReview_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('app_accessChooseApprove.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"; 

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtReviewOpinion.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写审批意见!')";
            return;
        }
        if (txtChooseName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请选择要提交给的下一审批人!')";
            return;
        }
        string ReviewOpinion = txtReviewOpinion.Text;

        string strMailTo = txtChooseEmail.Text;
        string strMailFrom = string.Empty;


        if (reviewYes(ReviewOpinion, txtCop.Text, txtDepart.Text, Request.QueryString["App_Process"], Session["uName"].ToString()))
        {
            if (insertNextReview(txtCop.Text, txtDepart.Text, Request.QueryString["App_Process"], txtChooseName.Text, txtChooseEmail.Text, txtChooseid.Text))
            {
                MailMessage mail = new MailMessage();
                StringBuilder sb = new StringBuilder();
                //获取发件人邮箱
                SqlDataReader reader2 = GetUserEmail(Convert.ToInt32(Session["uID"]));
                if (reader2.Read())
                {
                    strMailFrom = Convert.ToString(reader2["email"]);
                }
                reader2.Close();
                if (strMailFrom.Length == 0)
                {
                    strMailFrom = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                }

                mail.To = strMailTo;
                mail.From = strMailFrom;

                mail.Subject = "100系统-->招聘申请邮件";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append(Convert.ToString(Session["uname"]) + "向您提交了一份关于" + txtCop.Text.Trim() + "公司" + txtDepart.Text.Trim() + txtProc.Text.Trim() + "岗位的审核");
                sb.Append("<br>");
                sb.Append("     请您审核一下！" + "<br>");
                sb.Append("     谢谢！" + "<br>");
                sb.Append("     具体菜单是：工资结算--HR2 --> 员工入职审批>> --> 招聘列表" + "<br>");
                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
                mail.BodyFormat = MailFormat.Html;
                mail.Body = Convert.ToString(sb);
                BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body);
                //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"];
                //SmtpMail.Send(mail);

                ltlAlert.Text = "alert('提交成功!')";
                if (!judgeExistReviewName(Request.QueryString["App_Company"], Request.QueryString["App_department"], Request.QueryString["App_Process"], Session["uName"].ToString()))
                {
                    btnRefuse.Enabled = false;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    if (!judgeLastReviewName(Request.QueryString["App_Company"], Request.QueryString["App_department"], Request.QueryString["App_Process"], Session["uName"].ToString()))
                    {
                        btnRefuse.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
                BindData();
            }
        }
        else
        {
            ltlAlert.Text = "alert('审评意见不通过!')";            
            BindData();
            return;
        }        
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
    /// 判断是否审核过
    /// </summary>
    /// <returns></returns>
    public bool judgeReviewYN(string company, string deparment, string process, string username)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@deparment", deparment);
        param[2] = new SqlParameter("@process", process);
        param[3] = new SqlParameter("@username", username);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeReviewYN", param));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool insertNextReview(string company,string deparment,string process,string nextname,string email,string id)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@deparment", deparment);
        param[2] = new SqlParameter("@process", process);
        param[3] = new SqlParameter("@nextreviewname", nextname);
        param[4] = new SqlParameter("@reviewemail", email);
        param[5] = new SqlParameter("@reviewid", id);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_insertReviewList", param));
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        if (overReview(Request.QueryString["App_Company"], Request.QueryString["App_department"], Request.QueryString["App_Process"],Session["uID"].ToString(),Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('审核通过，开始招聘!')";
            btnSubmit.Visible = false;
            btnRefuse.Visible = false;
            btnApproval.Visible = false;
            btnProcMaintain.Visible = false;
        }
    }
    /// <summary>
    /// 批准
    /// </summary>
    /// <returns></returns>
    public bool reviewYes(string Opinion,string company, string deparment, string process,string username)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@Opinion", Opinion);
        param[1] = new SqlParameter("@company", company);
        param[2] = new SqlParameter("@deparment", deparment);
        param[3] = new SqlParameter("@process", process);
        param[4] = new SqlParameter("@username", username);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_reviewYes", param));
    }

    protected void btnRefuse_Click(object sender, EventArgs e)
    {
        if (txtReviewOpinion.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写审批意见!')";
            return;
        }
        string ReviewOpinion = txtReviewOpinion.Text;
        if (reviewNo(ReviewOpinion, txtCop.Text, txtDepart.Text, txtProc.Text, Session["uName"].ToString()))
        {
            BindData();
        }
        else
        {
            BindData();
        }
    }
    /// <summary>
    /// 拒绝
    /// </summary>
    /// <returns></returns>
    public bool reviewNo(string Opinion, string company, string deparment, string process, string username)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@Opinion", Opinion);
        param[1] = new SqlParameter("@company", company);
        param[2] = new SqlParameter("@deparment", deparment);
        param[3] = new SqlParameter("@process", process);
        param[4] = new SqlParameter("@username", username);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_reviewNo", param));
    }
    /// <summary>
    /// 批准通过，开始招聘
    /// </summary>
    /// <returns></returns>
    public bool overReview(string company, string deparment, string process,string uid,string uname)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", deparment);
        param[2] = new SqlParameter("@process", process);
        param[3] = new SqlParameter("@uid", uid);
        param[4] = new SqlParameter("@uname", uname);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_overReview", param));
    }

    protected void btnProcMaintain_Click(object sender, EventArgs e)
    {
        if (!existProcess(Request.QueryString["App_plantCode"], Request.QueryString["App_NewProc"]))
        {
            ltlAlert.Text = "alert('新岗位还没有在系统中维护，请先维护!')";
            return;
        }
        else
        {
            if (updateNewProce(Request.QueryString["App_plantCode"], Request.QueryString["App_departmentID"], Request.QueryString["App_ProcID"], Request.QueryString["App_NewProc"]))
            {
                ltlAlert.Text = "alert('新岗位更新成功!')";
                txtProc.Text = Request.QueryString["App_NewProc"];
                if (txtProc.Text != "其他岗位" && Request.QueryString["App_NewProc"] == txtProc.Text)
                {
                    btnProcMaintain.Enabled = false;
                    btnApproval.Enabled = true;
                }
                else
                {
                    //ltlAlert.Text = "alert('新岗位更新失败，请联系管理员!')";
                    btnApproval.Enabled = false;
                }
                return;
            }
        }
    }
    private bool existProcess(string plantcode,string newProce)
    {
        SqlParameter [] pram = new SqlParameter[2];
        pram[0] = new SqlParameter("@plantcode", plantcode);
        pram[1] = new SqlParameter("@newprocess", newProce);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_existProcess", pram));

    }
    private bool updateNewProce(string plantcode, string departmentid, string processid, string newprocess)
    {
        SqlParameter [] pram = new SqlParameter[4];
        pram[0] = new SqlParameter("@plantcode",plantcode);
        pram[1] = new SqlParameter("@departmentid",departmentid);
        pram[2] = new SqlParameter("@processid", processid);
        pram[3] = new SqlParameter("@newprocess", newprocess);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_updateNewProce", pram));
    }
}