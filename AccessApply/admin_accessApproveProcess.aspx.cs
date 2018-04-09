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
using System.Threading;
using System.Net.Mail;
using System.Text;
 

public partial class AccessApply_admin_accessApproveProcess : BasePage
{

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("19010212", "最终写入数据库后，用户才能访问该菜单");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_approve.Visible = this.Security["19010212"].isValid;   // 最终权限分配权限管理，若拥有此权限者，可赋予权限给申请人，
            btn_approve.Enabled = this.Security["19010212"].isValid;


            if (Request.QueryString["islook"].ToString() == "yes" || Request.QueryString["istartApprove"].ToString() == "no")
            { 
                btn_submit.Visible = false;
                btn_deny.Visible = false;
                btn_approve.Visible = false;
                lblApproveView.Visible = false;
                txtApproveView.Visible = false;
                lbl_submitnext.Visible = false;
                txt_approveName.Visible = false;
                lblannote.Visible = false;
                lbl_emailnote.Visible = false;
                txt_approveEmail.Visible = false;
                btn_selectApprove.Visible = false;
                lbl_CCapplyEmail.Visible = false;
                txt_applyUserEmail.Visible = false;
                lbl_currentApproveEmail.Visible = false;
                txt_currentApproveEmail.Visible = false;


                btn_back.Visible = false;
                btn_backlook.Visible = true;

            }

            if (Request.QueryString["aamId"].ToString() == string.Empty)
            {
                Response.Redirect("admin_accessApplymstr.aspx");
            }
            else
            {
                lblApplyId.Text = Request.QueryString["aamId"].ToString();
                BindData();
                if (lblCurrentApproveID.Text.ToString() != Convert.ToString(Session["uID"]) && Request.QueryString["istartApprove"].ToString() == "yes")
                {
                    Response.Redirect("admin_accessApplymstr.aspx");
                }
            }

            BindApplyMenu();
            BindApproveRecords();
        }
    }

    private void BindData()
    {
        int iApplyId = Convert.ToInt32(Request.QueryString["aamId"].ToString());

        DataSet ds = admin_AccessApply.getAccessApplyMstr(iApplyId);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtApplyUser.Text = ds.Tables[0].Rows[0]["applyUserName"].ToString();
            lblApplyUserID.Text = ds.Tables[0].Rows[0]["applyUserID"].ToString();
            txtApplyReason.Text = ds.Tables[0].Rows[0]["applyReason"].ToString();
            txtApplyDept.Text = ds.Tables[0].Rows[0]["applyDepartmentName"].ToString();
            txt_applyUserEmail.Text = ds.Tables[0].Rows[0]["applyUserEmail"].ToString().Trim();
            lblCurrentApproveID.Text = ds.Tables[0].Rows[0]["approveUserID"].ToString().Trim();
        }

        if (Request.QueryString["islook"].ToString() == "yes")
        {
            btn_submit.Visible = false;
            btn_deny.Visible = false;
            btn_approve.Visible = false;
            lblApproveView.Visible = false;
            txtApproveView.Visible = false;
        }
        if (Request.QueryString["istartApprove"].ToString() == "yes")
        {
            lbl_currentApproveEmail.Visible = true;
            txt_currentApproveEmail.Visible = true;
            txt_currentApproveEmail.Text = Convert.ToString(Session["email"]);
        }
    }
    private void BindApproveRecords()
    {
        DataSet ds = admin_AccessApply.getAccessApproveProcess(Convert.ToInt32(Request.QueryString["aamId"].ToString()));

        gv_ApproveRecords.DataSource = ds;
        gv_ApproveRecords.DataBind();

    }
    private void BindApplyMenu()
    {
        int iApplyId = Convert.ToInt32(Request.QueryString["aamId"]);
        DataTable dt_applyModule = admin_AccessApply.getAccessApplyModuleDetail(iApplyId);

        if (dt_applyModule.Rows.Count > 0)
        {
            for (int irow = 0; irow < dt_applyModule.Rows.Count; irow++)
            {
                ListItem item = new ListItem(dt_applyModule.Rows[irow]["applyModuleName"].ToString());
                item.Value = dt_applyModule.Rows[irow]["applyModuleID"].ToString();
                item.Selected = true;
                if (dt_applyModule.Rows[irow]["isDeny"].ToString() != string.Empty)
                {
                    item.Selected = false;
                    item.Enabled = false;
                }
                chkBL_applyedModule.Items.Add(item);
            }
        }
        dt_applyModule.Reset();
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_ApproveRecords.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            if (e.Row.Cells[7].Text.ToString() != "&nbsp;")
            {
                if (Convert.ToInt32(e.Row.Cells[7].Text.ToString()) == 0)
                {
                    e.Row.Cells[7].Text = "提交";
                }
                else if (Convert.ToInt32(e.Row.Cells[7].Text.ToString()) == -1)
                {
                    e.Row.Cells[7].Text = "申请";
                }
                else
                {
                    if (Convert.ToInt32(e.Row.Cells[7].Text.ToString()) == 1)
                    {
                        e.Row.Cells[7].Text = "拒绝";
                    }
                    else
                    {
                        e.Row.Cells[7].Text = "批准";
                    }
                }
            }
        }

    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ApproveRecords.PageIndex = e.NewPageIndex;
        this.BindData();
    }


    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_ApproveRecords.EditIndex = -1;
        BindData();
    }


    protected void btn_submit_Click(object sender, EventArgs e)    //提交下一审批人
    {
        if (txtApproveView.Text == string.Empty)
        {
            ltlAlert.Text = "alert('你还没填写审批意见，请确认')";
            return;
        }
        if (txt_currentApproveEmail.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请输入你的邮箱地址！')";
            return;
        }
        if (!baseDomain.checkDomainOR( txt_currentApproveEmail.Text.Trim()))
        {
            ltlAlert.Text = "alert('请填写正确邮箱地址！')";
            return;
        }

        if (txt_approveID.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请选择下一位审批人')";
            return;
        }
        if (txt_approveID.Text == Convert.ToString(Session["uID"]))
        {
            ltlAlert.Text = "alert('下一位审批人不能是本人')";
            return;
        }
        string mailfrom = txt_currentApproveEmail.Text.ToString().Trim();
        string displayName = Convert.ToString(Session["uName"]);
        string mailto = txt_approveEmail.Text.ToString().Trim();
        string mailcc = txt_applyUserEmail.Text.ToString().Trim();
        string mailSubject = "请及时处理" + Session["uName"].ToString() + "转交的权限申请（序号：" + lblApplyId.Text.ToString() + "）";
        StringBuilder sbMailContent = new StringBuilder();
        sbMailContent.Append("<html>");
        sbMailContent.Append("<body>");
        sbMailContent.Append("<form>");
        sbMailContent.Append(txt_approveName.Text.ToString() + ":<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你好！<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + Convert.ToString(Session["uName"]) + "将" + txtApplyUser.Text + "的权限申请现转交给你审批。详情如下：<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;申请理由：" + txtApplyReason.Text.ToString() + "<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;模块信息：<br />");
        int i = 1;
        foreach (ListItem item in chkBL_applyedModule.Items)
        {
            if (item.Selected == true)
            {
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (i++) + "、" + item.Text.ToString() + "<br />");
            }
        }
        sbMailContent.Append("<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;转交理由:" + txtApproveView.Text + "<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;详情请登录100系统，至菜单【Admin】=>【Profile>>】=>【权限申请管理】进行审批；或点击以下链接：<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;"+baseDomain.getPortalWebsite()+"/AccessApply/admin_accessApplymstr.aspx?aamId=" + lblApplyId.Text.ToString() + "<br /> ");
        sbMailContent.Append("</body>");
        sbMailContent.Append("</form>");
        sbMailContent.Append("</html>");
        string mailContent = Convert.ToString(sbMailContent);

        
        //string mes = admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailcc, mailSubject, mailContent);
        if (!this.SendEmail(mailfrom, mailto,  mailcc, mailSubject, mailContent))
       {
           ltlAlert.Text = "alert('发送邮件失败')";
            return;
       }
      
        saveApproveProcessInfo(0);
        saveApproveResult(0);

        ltlAlert.Text = "alert('已提交给下一审批人')";
        btn_submit.Text = "已提交";
        btn_submit.Enabled = false;
        Thread.Sleep(3000);
        Response.Redirect("admin_accessApplymstr.aspx");
    }

    private void saveApproveResult(int selectvalue)
    {
        int iApplyId = Convert.ToInt32(lblApplyId.Text.ToString());
        int iApproveUserID = Convert.ToInt32(Session["uID"].ToString());
        if (selectvalue == 0) //提交给下一审批人
        {
            if (chkBL_applyedModule.Items.Count > 0)
            {
                for (int i = 0; i < chkBL_applyedModule.Items.Count; i++)
                {
                    if (chkBL_applyedModule.Items[i].Selected == false && chkBL_applyedModule.Items[i].Enabled == true)
                    {
                        int iModuleID = Convert.ToInt32(chkBL_applyedModule.Items[i].Value.ToString());

                        admin_AccessApply.updateApplyModuleDetail(iApplyId, iApproveUserID, iModuleID, selectvalue);
                    }
                }
            }
        }
        if (selectvalue == 1)   //拒绝
        {
            admin_AccessApply.updateApplyModuleDetail(iApplyId, iApproveUserID, 0, selectvalue);
        }
        if (selectvalue == 2)  //审批通过，赋予权限，并将通过的iApplyId 申请的Module模块 写入到表AccessCanRule 表中
        {
            if (chkBL_applyedModule.Items.Count > 0)
            {
                for (int i = 0; i < chkBL_applyedModule.Items.Count; i++)
                {
                    if (chkBL_applyedModule.Items[i].Selected == false && chkBL_applyedModule.Items[i].Enabled == true)
                    {
                        int iModuleID = Convert.ToInt32(chkBL_applyedModule.Items[i].Value.ToString());
                        admin_AccessApply.updateApplyModuleDetail(iApplyId, iApproveUserID, iModuleID, 0); // 审批通过时，审批者可否定部份权限
                    }

                    //if (chkBL_applyedModule.Items[i].Selected == true)
                    //{
                    //    admin_AccessApply.updateApplyModuleDetail(iApplyId, iApproveUserID, 0, selectvalue); //更新申请表中的数据记录
                    //}
                }
            }
            admin_AccessApply.updateApplyModuleDetail(iApplyId, iApproveUserID, 0, selectvalue); //更新申请表中的数据记录

            //admin_AccessApply.addAccessRuleInfo(iApplyId);  //将申请通过的权限，写入到表AccessCanRule 表中
        }
    }

    private void saveApproveProcessInfo(int selectvalue)
    {
        int iApplyId = Convert.ToInt32(lblApplyId.Text.ToString().Trim());
        int iApproveUserID = Convert.ToInt32(Session["uID"].ToString());
        string strApproveView = txtApproveView.Text.ToString().Trim();
        string strnextApproveUserName = txt_approveName.Text.ToString().Trim();
        int inextApproveUserID;
        if (selectvalue == 0 && txt_approveID.Text.ToString() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择下一位审批人')";
            return;
        }

        if (txt_approveID.Text.ToString() == string.Empty)
        {
            inextApproveUserID = 0;
        }
        else
        {
            inextApproveUserID = Convert.ToInt32(txt_approveID.Text.ToString());
        }

        admin_AccessApply.updateAccessApproveProcess(iApplyId, iApproveUserID, strApproveView, inextApproveUserID, strnextApproveUserName, selectvalue);
    }

    protected void btn_selectApprove_Click(object sender, EventArgs e)
    {
        txt_approveName.ForeColor = System.Drawing.Color.Black;
        ltlAlert.Text = "var w=window.open('admin_accessChooseApprove.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }


    protected void txt_currentApproveEmail_TextChanged(object sender, EventArgs e)
    {
        if (txt_currentApproveEmail.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请输入你的邮箱地址！')";
            return;
        }
        if (! baseDomain.checkDomainOR( txt_currentApproveEmail.Text.Trim()) )
        {
            ltlAlert.Text = "alert('请填写正确邮箱地址！')";
            return;
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin_accessApplymstr.aspx");
    }

    protected void btn_deny_Click(object sender, EventArgs e)  // 拒绝
    {
        if (txtApproveView.Text == string.Empty)
        {
            ltlAlert.Text = "alert('你还没填写审批意见，请确认')";
            return;
        }
        if (txt_currentApproveEmail.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请输入你的邮箱地址！')";
            return;
        }
        if ( !baseDomain.checkDomainOR(txt_currentApproveEmail.Text.Trim())) 
        {
            ltlAlert.Text = "alert('请填写正确邮箱地址！')";
            return;
        }
        string mailfrom = txt_currentApproveEmail.Text.ToString().Trim();
        string displayName = Convert.ToString(Session["uName"]);

        string mailto = txt_applyUserEmail.Text.ToString().Trim();
        string mailSubject = "你的申请（序号：" + lblApplyId.Text.ToString() + "）已被" + Convert.ToString(Session["uName"]) + "否绝<br />";

        StringBuilder sbMailContent = new StringBuilder();
        sbMailContent.Append("<html>");
        sbMailContent.Append("<body>");
        sbMailContent.Append("<form>");
        sbMailContent.Append(txtApplyUser.Text.ToString() + ":<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你好！ <br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你申请的100系统菜单访问权限已被" + Convert.ToString(Session["uName"]) + "否绝。详情如下：<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;申请理由：" + txtApplyReason.Text.ToString() + "<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;模块信息：<br />");
        int i = 1;
        foreach (ListItem item in chkBL_applyedModule.Items)
        {
            if (item.Selected == true)
            {
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (i++) + "、" + item.Text.ToString() + "<br />");
            }
        }
        sbMailContent.Append("<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;否绝理由:" + txtApproveView.Text + "<br />");
        sbMailContent.Append("<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;详情请登录100系统，至菜单【Admin】=>【Profile>>】=>【权限申请管理】进行查看；或点击以下链接：<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;"+baseDomain.getPortalWebsite()+"/AccessApply/admin_accessApplymstr.aspx?aamId=" + lblApplyId.Text.ToString() + "<br /> ");
        sbMailContent.Append("</body>");
        sbMailContent.Append("</form>");
        sbMailContent.Append("</html>");
        string mailContent = Convert.ToString(sbMailContent);

       
        //string mes = admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);
        if (!this.SendEmail(mailfrom, mailto, "", mailSubject, mailContent))
        {
            ltlAlert.Text = "alert('发送邮件失败')";
            return;
        }
        saveApproveProcessInfo(1);
        saveApproveResult(1);

        ltlAlert.Text = "alert('已拒绝申请人的申请')";
        btn_deny.Text = "已拒绝";
        btn_deny.Enabled = false;
        Thread.Sleep(3000);
        Response.Redirect("admin_accessApplymstr.aspx");
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (txtApproveView.Text == string.Empty)
        {
            ltlAlert.Text = "alert('你还没填写审批意见，请确认')";
            return;
        }
        if (txt_currentApproveEmail.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请输入你的邮箱地址！')";
            return;
        }
        if (!baseDomain.checkDomainOR(txt_currentApproveEmail.Text.Trim()))
        {
            ltlAlert.Text = "alert('请填写正确邮箱地址！')";
            return;
        }
        string mailfrom = txt_currentApproveEmail.Text.ToString().Trim();
        string displayName = Convert.ToString(Session["uName"]);

        string mailto = txt_applyUserEmail.Text.ToString().Trim();
        string mailSubject = "你的申请（申请序号：" + lblApplyId.Text.ToString() + "）已通过。";

        StringBuilder sbMailContent = new StringBuilder();
        sbMailContent.Append("<html>");
        sbMailContent.Append("<body>");
        sbMailContent.Append("<form>");
        sbMailContent.Append(txtApplyUser.Text.ToString() + ":<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你好！<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你申请的100系统菜单访问权限已获得通过。现在，你就可对该菜单进行访问了。详情如下：<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;申请理由：" + txtApplyReason.Text.ToString() + "<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;模块信息：<br />");
        int i = 1;
        foreach (ListItem item in chkBL_applyedModule.Items)
        {
            if (item.Selected == true)
            {
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (i++) + "、" + item.Text.ToString() + "<br />");
            }
        }
        sbMailContent.Append("<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;通过理由:" + txtApproveView.Text + "<br />");
        sbMailContent.Append("<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;详情请登录100系统，至菜单【Admin】=>【Profile>>】=>【权限申请管理】进行查看；或点击以下链接：<br />");
        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;"+baseDomain.getPortalWebsite()+"/AccessApply/admin_accessApplymstr.aspx?aamId=" + lblApplyId.Text.ToString() + "<br /> ");
        sbMailContent.Append("</body>");
        sbMailContent.Append("</form>");
        sbMailContent.Append("</html>");
        string mailContent = Convert.ToString(sbMailContent);

        
        //string mes = admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);
        if (!this.SendEmail(mailfrom, mailto, "", mailSubject, mailContent))
        {
            ltlAlert.Text = "alert('发送邮件失败')";
            return;
        }
        saveApproveProcessInfo(2);
        saveApproveResult(2);

        ltlAlert.Text = "alert('已批准申请人的申请')";
        btn_approve.Text = "已批准";
        btn_approve.Enabled = false;
        Thread.Sleep(3000);
        Response.Redirect("admin_accessApplymstr.aspx");
    }
}
