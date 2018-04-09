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

public partial class AccessApply_admin_accessApplyModule : BasePage
{

    bool bflagModify = false; //标志 Edit状态无审批者时
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["aamId"]))
            {
                btnSubmit.Text = "提交";
            }
            else   // 进入编辑修改原来的申请
            {
                if (Request.QueryString["aamId"].ToString() != string.Empty)
                {
                    BindSelectedModule();
                    int iApplyId = Convert.ToInt32(Request.QueryString["aamId"]);
                    string strApplyName = Convert.ToString(Session["uName"]);

                    DataSet ds = admin_AccessApply.getAccessApplyMstr(iApplyId, strApplyName, true);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbl_ApproveID.Text = ds.Tables[0].Rows[0]["approveUserID"].ToString();
                        txt_approveName.Text = ds.Tables[0].Rows[0]["approveUserName"].ToString();

                        if (lbl_ApproveID.Text != String.Empty)
                        {
                            txtApplyReason.Text = ds.Tables[0].Rows[0]["applyReason"].ToString();
                            DataTable dt = admin_AccessApply.getApplyUserInfo(Convert.ToInt32(lbl_ApproveID.Text));
                            txtApplyEmail.Text = dt.Rows[0]["email"].ToString();
                            btn_chooseApprove.Enabled = false;
                        }
                        else
                        {
                            bflagModify = true;
                        }

                        btnSubmit.Text = "确认修改";
                    }
                }
            }

            BindMenu();
            BindChildMenu();
            txtApplyEmail.Text = Convert.ToString(Session["email"]);
        }
    }

    private void BindSelectedModule()
    {
        int iApplyId = Convert.ToInt32(Request.QueryString["aamId"]);
        DataTable dt_applyModule = admin_AccessApply.getAccessApplyModuleDetail(iApplyId);

        if (dt_applyModule.Rows.Count > 0)
        {
            for (int irow = 0; irow < dt_applyModule.Rows.Count; irow++)
            {
                ListItem item = new ListItem(dt_applyModule.Rows[irow]["applyModuleName"].ToString());
                item.Value = dt_applyModule.Rows[irow]["applyModuleID"].ToString();
                // item.Selected = true;
                chkBl_SelectedModule.Items.Add(item);
            }
        }
        dt_applyModule.Reset();
    }

    private void BindMenu()
    {
        ddlMenu.DataSource = admin_AccessApply.getRootMenuInfo();
        ddlMenu.DataBind();
        ddlMenu.SelectedIndex = 0;
    }


    private void BindChildMenu()
    {
        LoadMenu(Convert.ToInt32(ddlMenu.SelectedValue.ToString()), 0);
    }

    private void LoadMenu(int irootMenuId, int imark)
    {
        imark = imark + 1;
        string strmark = "";
        for (int j = 1; j < imark; j++)
        {
            strmark = "---" + strmark;
        }
        DataTable dt = admin_AccessApply.getchildMenuInfo(irootMenuId);
        if (dt.Rows.Count > 0)
        {
            for (int irow = 0; irow < dt.Rows.Count; irow++)
            {
                ListItem item = new ListItem(strmark + dt.Rows[irow]["name"].ToString());
                item.Value = dt.Rows[irow]["id"].ToString();
                if (Convert.ToBoolean(dt.Rows[irow]["isPublic"].ToString()) || this.Security[item.Value].isValid)
                {
                    item.Selected = true;
                    item.Enabled = false;
                }
                chkBl_Module.Items.Add(item);
                LoadMenu(Convert.ToInt32(dt.Rows[irow]["id"].ToString()), imark);
                for (int i = 0; i < chkBl_SelectedModule.Items.Count; i++)  // 在申请访问的项选中的框中已有的项，不再出现在选择的框中
                {
                    if (item.Value == chkBl_SelectedModule.Items[i].Value)
                    {
                        chkBl_Module.Items.Remove(item);
                        item.Text = "(已在申请列表中）" + item.Text;
                        item.Selected = true;
                        item.Enabled = false;
                        chkBl_Module.Items.Add(item);
                    }
                }
            }
            dt.Reset();
        }

        if (this.Security["19010211"].isValid)
        {
            Response.Redirect("~/public/Message.aspx?type=0", true);
        }
    }

    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkBl_Module.Items.Clear();
        LoadMenu(Convert.ToInt32(ddlMenu.SelectedValue.ToString()), 0);
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            for (int i = 0; i < chkBl_Module.Items.Count; i++)
            {
                if (chkBl_Module.Items[i].Enabled == true)
                {
                    chkBl_Module.Items[i].Selected = true;
                }
            }
        }
        else
            if (RadioButtonList1.SelectedIndex == 1)
            {
                for (int i = 0; i < chkBl_Module.Items.Count; i++)
                    chkBl_Module.Items[i].Selected = false;
            }

    }

    protected void btn_Add_Click(object sender, EventArgs e)   //将选中的菜单增加到申请的checkbox 中
    {
        for (int i = 0; i < this.chkBl_Module.Items.Count; i++)
        {
            if (chkBl_Module.Items[i].Selected && chkBl_Module.Items[i].Enabled == true)
            {
                this.chkBl_Module.Items[i].Selected = false;
                this.chkBl_SelectedModule.Items.Add(this.chkBl_Module.Items[i]);
                this.chkBl_Module.Items.Remove(this.chkBl_Module.Items[i]);
                i--;
            }
        }
    }
    protected void btn_Remove_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.chkBl_SelectedModule.Items.Count; i++)
        {
            if (chkBl_SelectedModule.Items[i].Selected)
            {
                this.chkBl_SelectedModule.Items[i].Selected = false;
                this.chkBl_Module.Items.Add(this.chkBl_SelectedModule.Items[i]);
                this.chkBl_SelectedModule.Items.Remove(this.chkBl_SelectedModule.Items[i]);
                i--;
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["aamId"]))   //新提交申请
        {
            if (txt_approveName.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请选择审批人')";
                return;
            }
            if (txt_approveID.Text == string.Empty)
            {
                ltlAlert.Text = "alert('审批人请通过按钮“选择审批人”,输入无效！但，邮箱可输入')";
                return;
            }


            if (txt_approveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请输入审批人的邮箱，以便邮件通知审批人及时处理')";
                return;
            }
        }

        if (txtApplyEmail.Text == string.Empty)
        {
            ltlAlert.Text = "alert('邮箱地址不能为空！')";
            return;
        }
        else
        {
            if (!baseDomain.checkDomainOR(txtApplyEmail.Text)) 
            {
                ltlAlert.Text = "alert('请输入正确的公司邮箱')";
                return;
            }
           
        }

        if (txtApplyReason.Text == string.Empty)
        {
            ltlAlert.Text = "alert('申请理由不能为空！')";
            return;
        }
        if (chkBl_SelectedModule.Items.Count < 1)
        {
            ltlAlert.Text = "alert('必须勾选相关申请的菜单权限')";
            return;
        }

        int iApplyUserId = Convert.ToInt32(Session["uID"]);
        string strApplyUserName = Convert.ToString(Session["uName"]);
        string strApplyUserEmail = txtApplyEmail.Text.ToString();
        int iApplyDeptId = Convert.ToInt32(Session["deptID"]);
        DataTable dt = admin_AccessApply.getDepartmentName(Convert.ToInt32(Session["PlantCode"]), iApplyDeptId);
        lbluserDepartName.Text = dt.Rows[0]["name"].ToString();
        string strApplyDeptName = lbluserDepartName.Text.ToString();
        string strApplyReason = txtApplyReason.Text.ToString().Trim();


        if (string.IsNullOrEmpty(Request.QueryString["aamId"]))   //新提交申请
        {
            if (txt_approveName.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请选择审批人')";
                return;
            }
            if (txt_approveID.Text == string.Empty)
            {
                ltlAlert.Text = "alert('审批人请通过按钮“选择审批人”，输入无效；但，邮箱可输入')";
                return;
            }
            if (txt_approveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请输入审批人的邮箱，以便邮件通知审批人，及时处理')";
                return;
            }
            if (txt_approveID.Text == Convert.ToString(Session["uID"]))
            {
                ltlAlert.Text = "alert('申请人不可提交给本人审批，请重新选择审批人')";
                return;
            }

            int iApproveUserId = Convert.ToInt32(txt_approveID.Text.ToString());
            string strApproveName = txt_approveName.Text.ToString();
            int iApplyId = admin_AccessApply.newAccessApplyInfo(iApplyUserId, strApplyUserName, strApplyUserEmail, iApplyDeptId, strApplyDeptName, strApplyReason, iApproveUserId,strApproveName);
            if (iApplyId == -1)
            {
                ltlAlert.Text = "alert('提交失败，请重新提交一次')";
                return;
            }
            else   // 提交成功后，要发送邮件给审核人，并将其申请的模块信息保存到数据库
            {
                //admin_AccessApply.newApplyApproveProcess(iApplyId                strApproveName);
                foreach (ListItem item in chkBl_SelectedModule.Items)
                {
                    int iModuleId = Convert.ToInt32(item.Value.ToString());
                    string strModuleName = item.Text.Substring(0, item.Text.IndexOf("&&")).Trim();
                    string strModuleDesc = item.Text.Substring(item.Text.IndexOf("&&") + 1, item.Text.Length - item.Text.IndexOf("&&") - 1);
                    admin_AccessApply.newApplyRecord(iApplyId, iModuleId, strModuleName, strModuleDesc);
                }

                string mailto = txt_approveEmail.Text.ToString();
                string mailfrom = txtApplyEmail.Text.ToString();
                string displayName = Convert.ToString(Session["uName"]);
                string mailSubject = "请及时处理" + Session["uName"] + "的菜单申请（序号：" + iApplyId + "）";
                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<html>");
                sbMailContent.Append("<body>");
                sbMailContent.Append("<form>");
                sbMailContent.Append(txt_approveName.Text.ToString() + ":<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你好！<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"] + "已于" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "向你申请了菜单的访问权限，请及时审批。详情如下：<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;申请理由：" + txtApplyReason.Text.ToString() + "<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;模块信息： <br />");
                int i = 1;
                foreach (ListItem item in chkBl_SelectedModule.Items)
                {
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (i++) + "、" + item.Text.ToString() + "<br />");
                }
                sbMailContent.Append("<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;详情请登录100系统，至菜单【Admin】=>【Profile>>】=>【权限申请管理】进行审批；或点击以下链接：<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + baseDomain.getPortalWebsite() + "/AccessApply/admin_accessApplymstr.aspx?aamId=" + iApplyId + "<br /> "); sbMailContent.Append("</body>");
                sbMailContent.Append("</form>");
                sbMailContent.Append("</html>");
                string mailContent = Convert.ToString(sbMailContent);

                
                //admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);

                if (this.SendEmail(mailfrom, mailto, "", mailSubject, mailContent))
                {
                    ltlAlert.Text = "alert('提交成功，请等待审批');window.location.href = 'admin_accessApplymstr.aspx?rm=" + DateTime.Now.ToString() + "';";
                    btnSubmit.Text = "已提交";
                    btnSubmit.Enabled = false;
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('邮件发送失败，但申请成功，请主动联系审批者')";
                    btnSubmit.Text = "已提交";
                    btnSubmit.Enabled = false;
                    return;
                }
            }
        }
        else
        {
            if (Request.QueryString["aamId"].ToString() != string.Empty)  //已成功提交，当审批还未审批，可进行更改
            {
                int iApplyId = Convert.ToInt32(Request.QueryString["aamId"]);

                admin_AccessApply.updateAccessApplymstr(iApplyId, strApplyReason);
                admin_AccessApply.deleteAccessApplyDetail(iApplyId);

                if (chkBl_SelectedModule.Items.Count < 1)
                {
                    ltlAlert.Text = "alert('必须勾选相关申请的菜单权限')";
                    return;

                }
                foreach (ListItem item in chkBl_SelectedModule.Items)
                {
                    int iModuleId = Convert.ToInt32(item.Value.ToString());
                    string strModuleName = item.Text.Substring(0, item.Text.IndexOf("&&")).Trim();
                    string strModuleDesc = item.Text.Substring(item.Text.IndexOf("&&") + 1, item.Text.Length - item.Text.IndexOf("&&") - 1);
                    admin_AccessApply.newApplyRecord(iApplyId, iModuleId, strModuleName, strModuleDesc);

                }

                if (bflagModify == true)
                {
                    if (txt_approveName.Text == string.Empty)
                    {
                        ltlAlert.Text = "alert('请选择审批人')";
                        return;
                    }
                    if (txt_approveID.Text == string.Empty)
                    {
                        ltlAlert.Text = "alert('审批人请通过按钮“选择审批人”,输入无效，邮箱可输入')";
                        return;
                    }
                    if (txt_approveEmail.Text == string.Empty)
                    {
                        ltlAlert.Text = "alert('请输入审批人的邮箱，以便邮件通知审批人及时处理')";
                        return;
                    }
                    if (txt_approveID.Text == Convert.ToString(Session["uID"]))
                    {
                        ltlAlert.Text = "alert('申请人不可提交给本人审批，请重新选择审批人')";
                        return;
                    }

                    int iApproveUserId = Convert.ToInt32(txt_approveID.Text.ToString());
                    string strApproveName = txt_approveName.Text.ToString();
                    admin_AccessApply.newApplyApproveProcess(iApplyId, iApproveUserId, strApproveName);

                    string mailto = txt_approveEmail.Text.ToString();
                    string mailfrom = txtApplyEmail.Text.ToString();
                    string displayName = Convert.ToString(Session["uName"]);
                    string mailSubject = "请及时处理" + Session["uName"] + "的菜单申请（序号：" + iApplyId + "）";
                    StringBuilder sbMailContent = new StringBuilder();
                    sbMailContent.Append("<html>");
                    sbMailContent.Append("<body>");
                    sbMailContent.Append("<form>");
                    sbMailContent.Append(txt_approveName.Text.ToString() + ":<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你好！<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"] + "已于" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "向你申请了菜单的访问权限，请及时审批。详情如下：<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;申请理由：" + txtApplyReason.Text.ToString() + "<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;模块信息： <br />");
                    int i = 1;
                    foreach (ListItem item in chkBl_SelectedModule.Items)
                    {
                        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (i++) + "、" + item.Text.ToString() + "<br />");
                    }
                    sbMailContent.Append("<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;详情请登录100系统，至菜单【Admin】=>【Profile>>】=>【权限申请管理】进行审批；或点击以下链接：<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;"+baseDomain.getPortalWebsite()+"/AccessApply/admin_accessApplymstr.aspx?aamId=" + iApplyId + "<br /> ");
                    sbMailContent.Append("</body>");
                    sbMailContent.Append("</form>");
                    sbMailContent.Append("</html>");
                    string mailContent = Convert.ToString(sbMailContent);

                    
                    //admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);
                    if (this.SendEmail(mailfrom, mailto, "", mailSubject, mailContent))
                    {
                        ltlAlert.Text = "alert('提交成功，请等待审批');window.location.href = 'admin_accessApplymstr.aspx?rm=" + DateTime.Now.ToString() + "';";
                        btnSubmit.Enabled = false;
                        //Response.Redirect("accessApplyProcess.aspx", true);
                        return;
                    }
                    else
                    {
                        ltlAlert.Text = "alert('邮件发送失败，但申请成功，请主动联系审批者')";
                        btnSubmit.Enabled = false;
                        //Response.Redirect("accessApplyProcess.aspx", true);
                        return;
                    }
                }
              
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin_accessApplymstr.aspx", true);
    }

    protected void btn_chooseApprove_Click(object sender, EventArgs e)
    {
        txt_approveName.ForeColor = System.Drawing.Color.Black;
        ltlAlert.Text = "var w=window.open('admin_accessChooseApprove.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
}
