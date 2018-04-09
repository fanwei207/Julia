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
using System.Data.SqlClient;
using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;

public partial class HR_hr_RecruitmentRequest : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNote.Enabled = false;
            txtAppName.Enabled = false;
            txtAppdeprt.Enabled = false;
            txtAppCop.Enabled = false;
            txtAppDate.Enabled = false;
            //txtChooseName.Enabled = false;
            txtOtherProc.Visible = false;

            labLanguage.Visible = false;
            txtNote.Visible = false;

            txtAppName.Text = Session["uname"].ToString();
            txtAppdeprt.Text = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));
            txtAppCop.Text = GetUserCop(Convert.ToInt32(Session["PlantCode"].ToString()));
            txtAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            

            BindUserPost();
            //BindEducation();
        }
    }
    /// <summary>
    /// 绑定所有岗位
    /// </summary>
    private void BindUserPost()
    {
        DataTable dt = GetUserPost();
        ddlAppProc.DataSource = dt;
        ddlAppProc.DataBind();
        ddlAppProc.Items.Insert(0, new ListItem("---申 请 岗 位---", "0"));
    }
    /// <summary>
    /// 获取所有岗位
    /// </summary>
    private DataTable GetUserPost()
    {
        SqlParameter pram = new SqlParameter("@plantcode", Session["PlantCode"].ToString());
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetUserPost", pram).Tables[0];
    }
    /// <summary>
    /// 获取当前人所属部门
    /// </summary>
    private String GetUserDept(int uid, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@uid", uid);
        param[1] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetUserDept", param).ToString();
    }
    /// <summary>
    /// 获取当前人所属公司
    /// </summary>
    private String GetUserCop(int plantCode)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetUserCop", param).ToString();
    }
    protected void btn_choose_Click(object sender, EventArgs e)
    {
        //ltlAlert.Text = "window.showModalDialog('app_choose2.aspx',window,'dialogHeight: 500px; dialogWidth: 800px;');";
        //ltlAlert.Text = "window.showModalDialog('oms_reply.aspx?parentID=" + _parentID + "&type=" + "new" + "&custName=" + lbCustName.Text + "&custCode=" + txtCustomer.Text + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text = "var w=window.open('app_accessChooseApprove.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"; 

        //ltlAlert.Text = "var w=window.open('conn_choose2.aspx?mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
    }
    protected void btn_choose_Click1(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.showModalDialog('app_choose2.aspx',window,'dialogHeight: 500px; dialogWidth: 800px; edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        //ltlAlert.Text = "var w=window.open('conn_choose2.aspx?mid=" + Request("mid") + "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
        
    }
    /// <summary>
    /// 获取所有学历
    /// </summary>
    private DataTable GetAllEdu()
    {
        string sql = "select systemCodeID,systemCodeName  from tcpc0..systemCode where systemCodeTypeID = 4";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text,sql).Tables[0];
    }
    //岗位申请
    protected void btnSub_Click(object sender, EventArgs e)
    {
        if (ddlAppProc.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择申请岗位!')";
            ddlAppProc.BackColor = System.Drawing.Color.Red;
            return;
        }
        else 
        {
            ddlAppProc.BackColor = System.Drawing.Color.White;
        }
        //预到职日期
        if (txtDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请选择预到职日期')";
            txtDate.BackColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            txtDate.BackColor = System.Drawing.Color.White;
        }
        //招聘理由备注
        string reasonnote = string.Empty;
        if (txtReason.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写招聘理由备注!')";
            return;
        }
        else
        {
            reasonnote = txtReason.Text;
        }
        //招聘人数
        if (txtAppNum.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写招聘人数!')";
            txtAppNum.BackColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            txtAppNum.BackColor = System.Drawing.Color.White;
        }
        //工作经验
        if (txtAppExp.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写工作经验!')";
            txtAppExp.BackColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            txtAppExp.BackColor = System.Drawing.Color.White;
        }
        //年龄范围
        if (txtAge.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写年龄范围!')";
            txtAge.BackColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            txtAge.BackColor = System.Drawing.Color.White;
        }
        //专业要求
        if (txtEduRequest.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写专业要求!')";
            txtEduRequest.BackColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            txtEduRequest.BackColor = System.Drawing.Color.White;
        }
        //岗位要求
        if (txtAppRequest.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写岗位要求!')";
            txtAppRequest.BackColor = System.Drawing.Color.Red;
            return;
        }
        else if (txtAppRequest.Text.Length > 300)
        {
            ltlAlert.Text = "alert('岗位要求在300字以内!')";
            txtAppRequest.BackColor = System.Drawing.Color.Red;
            return;
        }
        else 
        {
            txtAppRequest.BackColor = System.Drawing.Color.White;
        }        
        
        string name = txtAppName.Text;
        string appdate= txtAppDate.Text;
        string depart = txtAppdeprt.Text;
        string company = txtAppCop.Text;
        string proc = ddlAppProc.SelectedItem.Text.Trim();
        int procid = Convert.ToInt32(ddlAppProc.SelectedItem.Value.ToString());
        string newproc = string.Empty;
        if (procid == 600)
        {
            if (txtOtherProc.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请填写新岗位名称!')";
                return;
            }
            else newproc = txtOtherProc.Text;
        }
        string arrdate = txtDate.Text;
        string chkReason;
        string appreason = "";
        for (int i = 1; i <= 4; i++)
        {
            chkReason = "chkReason" + i;
            if (chkReason == "chkReason1" && chkReason1.Checked)
            {
                appreason = chkReason1.Text;
                break;
            }
            else if (chkReason == "chkReason2" && chkReason2.Checked)
            {
                appreason = chkReason2.Text;
                break;
            }
            else if (chkReason == "chkReason3" && chkReason3.Checked)
            {
                appreason = chkReason3.Text;
                break;
            }
            else if (chkReason == "chkReason4" && chkReason4.Checked)
            {
                appreason = chkReason4.Text;
                break;
            }
        }
        //招聘理由
        if (appreason == string.Empty)
        {
            ltlAlert.Text = "alert('请选择招聘理由!')";
            return;
        }

        int num = Convert.ToInt32(txtAppNum.Text);
        int expnum = Convert.ToInt32(txtAppExp.Text);
        string chksex ;
        string sex = "";
        for (int i = 1; i <= 3; i++)
        {
            chksex = "chkSex"+i;
            if (chksex == "chkSex1" && chkSex1.Checked)
            {
                sex = chkSex1.Text;
                break;
            }
            else if (chksex == "chkSex2" && chkSex2.Checked)
            {
                sex = chkSex2.Text;
                break;
            }
            else if (chksex == "chkSex3" && chkSex3.Checked)
            {
                sex = chkSex3.Text;
                break;
            }
        }
        //性别
        if (sex == string.Empty)
        {
            ltlAlert.Text = "alert('请选择性别!')";
            return;
        }

        string agerange = txtAge.Text;
        string chkEdu;
        string edu = "";
        for (int i = 1; i <= 5; i++)
        {
            chkEdu = "chkEdu" + i;
            if (chkEdu == "chkEdu1" && chkEdu1.Checked)
            {
                edu = chkEdu1.Text;
                break;
            }
            else if (chkEdu == "chkEdu2" && chkEdu2.Checked)
            {
                edu = chkEdu2.Text;
                break;
            }
            else if (chkEdu == "chkEdu3" && chkEdu3.Checked)
            {
                edu = chkEdu3.Text;
                break;
            }
            else if (chkEdu == "chkEdu4" && chkEdu4.Checked)
            {
                edu = chkEdu4.Text;
                break;
            }
            else if (chkEdu == "chkEdu5" && chkEdu5.Checked)
            {
                edu = chkEdu5.Text;
                break;
            }
        }
        //学历
        if (edu == string.Empty)
        {
            ltlAlert.Text = "alert('请选择学历!')";
            return;
        }
        string edurequest = txtEduRequest.Text;
        string chkLanguage;
        string language = "";
        for (int i = 1; i <= 5; i++)
        {
            chkLanguage = "chkLanguage" + i;
            if (chkLanguage == "chkLanguage1" && chkLanguage1.Checked)
            {
                language = chkLanguage1.Text;
                break;
            }
            else if (chkLanguage == "chkLanguage2" && chkLanguage2.Checked)
            {
                language = chkLanguage2.Text;
                break;
            }
            else if (chkLanguage == "chkLanguage3" && chkLanguage3.Checked)
            {
                language = chkLanguage3.Text;
                break;
            }
            else if (chkLanguage == "chkLanguage4" && chkLanguage4.Checked)
            {
                language = chkLanguage4.Text;
                break;
            }
            else if (chkLanguage == "chkLanguage5" && chkLanguage5.Checked)
            {
                language = chkLanguage5.Text;
                if (txtNote.Text == string.Empty)
                {
                    ltlAlert.Text = "alert('请填写外语备注!')";
                    return;
                }
                break;
            }            
        }
        //外语
        if (language == string.Empty)
        {
            ltlAlert.Text = "alert('请选择外语!')";
            return;
        }
        //提交人
        if (txtChooseName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请选择要提交审核的人!')";
            txtChooseName.BackColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            txtChooseName.BackColor = System.Drawing.Color.White;
        }
        string note = txtNote.Text;
        string apprequset = txtAppRequest.Text;
        string reviewname = txtChooseName.Text;
        string reviewemail= txtChooseEmail.Text;
        string reviewid = txtChooseid.Text;
        int uid = Convert.ToInt32(Session["uID"].ToString()) ;
        string useremail = Session["email"].ToString();

        string strMailTo = txtChooseEmail.Text;
        string strMailFrom = string.Empty;
        int departmentid = Convert.ToInt32(Session["deptID"].ToString());
        int roleid = Convert.ToInt32(Session["uRole"].ToString());
        int plantcode = Convert.ToInt32(Session["PlantCode"].ToString());

        if (judgeAppList(depart, company, proc))
        {
            ltlAlert.Text = "alert('已申请过此岗位，请勿重复申请!')";
            return;
        }
        else 
        {
            if (addAppList(name, appdate, depart, company, proc, procid, arrdate, appreason, num, expnum, sex, agerange, edu
                , edurequest, language, note, apprequset, reviewname, reviewemail, reviewid, uid, useremail, plantcode
                , departmentid, roleid, reasonnote, newproc))
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
                sb.Append(txtAppCop.Text.Trim() + "公司" + txtAppdeprt.Text.Trim() + "的" + txtAppName .Text.Trim() + "申请了" + ddlAppProc.SelectedItem.Text.Trim() +"岗位");
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

                Response.Redirect("app_RecruitmentRequestList.aspx");
                ltlAlert.Text = "alert('申请成功!')";
            }
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

    #region 性别checkbox
    protected void chkSex1_CheckedChanged(object sender, EventArgs e)
    {
        chkSex2.Checked = false;
        chkSex3.Checked = false;
    }
    protected void chkSex2_CheckedChanged(object sender, EventArgs e)
    {
        chkSex1.Checked = false;
        chkSex3.Checked = false;
    }
    protected void chkSex3_CheckedChanged(object sender, EventArgs e)
    {
        chkSex1.Checked = false;
        chkSex2.Checked = false;
    }
    #endregion

    #region 招聘理由checkbox
    protected void chkReason1_CheckedChanged(object sender, EventArgs e)
    {
        chkReason2.Checked = false;
        chkReason3.Checked = false;
        chkReason4.Checked = false;
    }
    protected void chkReason2_CheckedChanged(object sender, EventArgs e)
    {
        chkReason1.Checked = false;
        chkReason3.Checked = false;
        chkReason4.Checked = false;
    }
    protected void chkReason3_CheckedChanged(object sender, EventArgs e)
    {
        chkReason1.Checked = false;
        chkReason2.Checked = false;
        chkReason4.Checked = false;
    }
    protected void chkReason4_CheckedChanged(object sender, EventArgs e)
    {
        chkReason1.Checked = false;
        chkReason2.Checked = false;
        chkReason3.Checked = false;
    }
    #endregion

    #region 学历checkbox
    protected void chkEdu1_CheckedChanged(object sender, EventArgs e)
    {
        chkEdu2.Checked = false;
        chkEdu3.Checked = false;
        chkEdu4.Checked = false;
        chkEdu5.Checked = false;
    }
    protected void chkEdu2_CheckedChanged(object sender, EventArgs e)
    {
        chkEdu1.Checked = false;
        chkEdu3.Checked = false;
        chkEdu4.Checked = false;
        chkEdu5.Checked = false;
    }
    protected void chkEdu3_CheckedChanged(object sender, EventArgs e)
    {
        chkEdu1.Checked = false;
        chkEdu2.Checked = false;
        chkEdu4.Checked = false;
        chkEdu5.Checked = false;
    }
    protected void chkEdu4_CheckedChanged(object sender, EventArgs e)
    {
        chkEdu1.Checked = false;
        chkEdu2.Checked = false;
        chkEdu3.Checked = false;
        chkEdu5.Checked = false;
    }
    protected void chkEdu5_CheckedChanged(object sender, EventArgs e)
    {
        chkEdu1.Checked = false;
        chkEdu2.Checked = false;
        chkEdu3.Checked = false;
        chkEdu4.Checked = false;
    }
    #endregion

    #region 外语checkbox
    protected void chkLanguage1_CheckedChanged(object sender, EventArgs e)
    {
        txtNote.Enabled = false;
        chkLanguage2.Checked = false;
        chkLanguage3.Checked = false;
        chkLanguage4.Checked = false;
        chkLanguage5.Checked = false;
        labLanguage.Visible = false;
        txtNote.Visible = false;
    }
    protected void chkLanguage2_CheckedChanged(object sender, EventArgs e)
    {
        txtNote.Enabled = false;
        chkLanguage1.Checked = false;
        chkLanguage3.Checked = false;
        chkLanguage4.Checked = false;
        chkLanguage5.Checked = false;
        labLanguage.Visible = false;
        txtNote.Visible = false;
    }
    protected void chkLanguage3_CheckedChanged(object sender, EventArgs e)
    {
        txtNote.Enabled = false;
        chkLanguage1.Checked = false;
        chkLanguage2.Checked = false;
        chkLanguage4.Checked = false;
        chkLanguage5.Checked = false;
        labLanguage.Visible = false;
        txtNote.Visible = false;
    }
    protected void chkLanguage4_CheckedChanged(object sender, EventArgs e)
    {
        txtNote.Enabled = false;
        chkLanguage1.Checked = false;
        chkLanguage2.Checked = false;
        chkLanguage3.Checked = false;
        chkLanguage5.Checked = false;
        labLanguage.Visible = false;
        txtNote.Visible = false;
    }
    protected void chkLanguage5_CheckedChanged(object sender, EventArgs e)
    {
        txtNote.Enabled = chkLanguage5.Checked;
        chkLanguage1.Checked = false;
        chkLanguage2.Checked = false;
        chkLanguage3.Checked = false;
        chkLanguage4.Checked = false;
        labLanguage.Visible = true;
        txtNote.Visible = true;
    }
    #endregion

    public  bool addAppList(string name,string appdate,string depart,string company,string proc,int procid,string arrdate,
            string appreason, int num, int expnum, string sex, string agerange, string edu, string edurequest,
            string language, string note, string apprequset, string reviewname, string reviewemail, string revieweid, int uid,
            string useremail, int plantcode, int departmentid, int roleid, string reasonnote, string newproc)
    {
        SqlParameter[] param = new SqlParameter[27];
        param[0] = new SqlParameter("@name", name);
        param[1] = new SqlParameter("@appdate", appdate);
        param[2] = new SqlParameter("@depart", depart);
        param[3] = new SqlParameter("@compay", company);
        param[4] = new SqlParameter("@proc", proc);
        param[5] = new SqlParameter("@procid", procid);
        param[6] = new SqlParameter("@arrdate", arrdate);
        param[7] = new SqlParameter("@appreason", appreason);
        param[8] = new SqlParameter("@num", num);
        param[9] = new SqlParameter("@expnum", expnum);
        param[10] = new SqlParameter("@sex", sex);
        param[11] = new SqlParameter("@agerange", agerange);
        param[12] = new SqlParameter("@edu", edu);
        param[13] = new SqlParameter("@edurequest", edurequest);
        param[14] = new SqlParameter("@language", language);
        param[15] = new SqlParameter("@note", note);
        param[16] = new SqlParameter("@apprequset", apprequset);
        param[17] = new SqlParameter("@reviewname", reviewname);
        param[18] = new SqlParameter("@reviewemail", reviewemail);
        param[19] = new SqlParameter("@reviewid", revieweid);
        param[20] = new SqlParameter("@uid", uid);
        param[21] = new SqlParameter("@useremail", useremail);
        param[22] = new SqlParameter("@plantcode", plantcode);
        param[23] = new SqlParameter("@departmentid", departmentid);
        param[24] = new SqlParameter("@roleid", roleid);
        param[25] = new SqlParameter("@reasonnote", reasonnote);
        param[26] = new SqlParameter("@newproc", newproc);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_addAppList", param));
    }
    public bool judgeAppList(string depart, string company, string proc)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@depart", depart);
        param[1] = new SqlParameter("@compay", company);
        param[2] = new SqlParameter("@proc", proc);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeAppList", param));
    }

    protected void ddlAppProc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlAppProc.SelectedItem.Value.ToString()) == 600)
        {
            txtOtherProc.Visible = true;
        }
        else
        {
            txtOtherProc.Visible = false;
        }
    }
}