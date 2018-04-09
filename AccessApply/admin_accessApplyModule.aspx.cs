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

    bool bflagModify = false; //��־ Edit״̬��������ʱ
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["aamId"]))
            {
                btnSubmit.Text = "�ύ";
            }
            else   // ����༭�޸�ԭ��������
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

                        btnSubmit.Text = "ȷ���޸�";
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
                for (int i = 0; i < chkBl_SelectedModule.Items.Count; i++)  // ��������ʵ���ѡ�еĿ������е�����ٳ�����ѡ��Ŀ���
                {
                    if (item.Value == chkBl_SelectedModule.Items[i].Value)
                    {
                        chkBl_Module.Items.Remove(item);
                        item.Text = "(���������б��У�" + item.Text;
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

    protected void btn_Add_Click(object sender, EventArgs e)   //��ѡ�еĲ˵����ӵ������checkbox ��
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
        if (string.IsNullOrEmpty(Request.QueryString["aamId"]))   //���ύ����
        {
            if (txt_approveName.Text == string.Empty)
            {
                ltlAlert.Text = "alert('��ѡ��������')";
                return;
            }
            if (txt_approveID.Text == string.Empty)
            {
                ltlAlert.Text = "alert('��������ͨ����ť��ѡ�������ˡ�,������Ч���������������')";
                return;
            }


            if (txt_approveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('�����������˵����䣬�Ա��ʼ�֪ͨ�����˼�ʱ����')";
                return;
            }
        }

        if (txtApplyEmail.Text == string.Empty)
        {
            ltlAlert.Text = "alert('�����ַ����Ϊ�գ�')";
            return;
        }
        else
        {
            if (!baseDomain.checkDomainOR(txtApplyEmail.Text)) 
            {
                ltlAlert.Text = "alert('��������ȷ�Ĺ�˾����')";
                return;
            }
           
        }

        if (txtApplyReason.Text == string.Empty)
        {
            ltlAlert.Text = "alert('�������ɲ���Ϊ�գ�')";
            return;
        }
        if (chkBl_SelectedModule.Items.Count < 1)
        {
            ltlAlert.Text = "alert('���빴ѡ�������Ĳ˵�Ȩ��')";
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


        if (string.IsNullOrEmpty(Request.QueryString["aamId"]))   //���ύ����
        {
            if (txt_approveName.Text == string.Empty)
            {
                ltlAlert.Text = "alert('��ѡ��������')";
                return;
            }
            if (txt_approveID.Text == string.Empty)
            {
                ltlAlert.Text = "alert('��������ͨ����ť��ѡ�������ˡ���������Ч���������������')";
                return;
            }
            if (txt_approveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('�����������˵����䣬�Ա��ʼ�֪ͨ�����ˣ���ʱ����')";
                return;
            }
            if (txt_approveID.Text == Convert.ToString(Session["uID"]))
            {
                ltlAlert.Text = "alert('�����˲����ύ������������������ѡ��������')";
                return;
            }

            int iApproveUserId = Convert.ToInt32(txt_approveID.Text.ToString());
            string strApproveName = txt_approveName.Text.ToString();
            int iApplyId = admin_AccessApply.newAccessApplyInfo(iApplyUserId, strApplyUserName, strApplyUserEmail, iApplyDeptId, strApplyDeptName, strApplyReason, iApproveUserId,strApproveName);
            if (iApplyId == -1)
            {
                ltlAlert.Text = "alert('�ύʧ�ܣ��������ύһ��')";
                return;
            }
            else   // �ύ�ɹ���Ҫ�����ʼ�������ˣ������������ģ����Ϣ���浽���ݿ�
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
                string mailSubject = "�뼰ʱ����" + Session["uName"] + "�Ĳ˵����루��ţ�" + iApplyId + "��";
                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<html>");
                sbMailContent.Append("<body>");
                sbMailContent.Append("<form>");
                sbMailContent.Append(txt_approveName.Text.ToString() + ":<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;��ã�<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"] + "����" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "���������˲˵��ķ���Ȩ�ޣ��뼰ʱ�������������£�<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;�������ɣ�" + txtApplyReason.Text.ToString() + "<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;ģ����Ϣ�� <br />");
                int i = 1;
                foreach (ListItem item in chkBl_SelectedModule.Items)
                {
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (i++) + "��" + item.Text.ToString() + "<br />");
                }
                sbMailContent.Append("<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;�������¼100ϵͳ�����˵���Admin��=>��Profile>>��=>��Ȩ������������������������������ӣ�<br />");
                sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + baseDomain.getPortalWebsite() + "/AccessApply/admin_accessApplymstr.aspx?aamId=" + iApplyId + "<br /> "); sbMailContent.Append("</body>");
                sbMailContent.Append("</form>");
                sbMailContent.Append("</html>");
                string mailContent = Convert.ToString(sbMailContent);

                
                //admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);

                if (this.SendEmail(mailfrom, mailto, "", mailSubject, mailContent))
                {
                    ltlAlert.Text = "alert('�ύ�ɹ�����ȴ�����');window.location.href = 'admin_accessApplymstr.aspx?rm=" + DateTime.Now.ToString() + "';";
                    btnSubmit.Text = "���ύ";
                    btnSubmit.Enabled = false;
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('�ʼ�����ʧ�ܣ�������ɹ�����������ϵ������')";
                    btnSubmit.Text = "���ύ";
                    btnSubmit.Enabled = false;
                    return;
                }
            }
        }
        else
        {
            if (Request.QueryString["aamId"].ToString() != string.Empty)  //�ѳɹ��ύ����������δ�������ɽ��и���
            {
                int iApplyId = Convert.ToInt32(Request.QueryString["aamId"]);

                admin_AccessApply.updateAccessApplymstr(iApplyId, strApplyReason);
                admin_AccessApply.deleteAccessApplyDetail(iApplyId);

                if (chkBl_SelectedModule.Items.Count < 1)
                {
                    ltlAlert.Text = "alert('���빴ѡ�������Ĳ˵�Ȩ��')";
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
                        ltlAlert.Text = "alert('��ѡ��������')";
                        return;
                    }
                    if (txt_approveID.Text == string.Empty)
                    {
                        ltlAlert.Text = "alert('��������ͨ����ť��ѡ�������ˡ�,������Ч�����������')";
                        return;
                    }
                    if (txt_approveEmail.Text == string.Empty)
                    {
                        ltlAlert.Text = "alert('�����������˵����䣬�Ա��ʼ�֪ͨ�����˼�ʱ����')";
                        return;
                    }
                    if (txt_approveID.Text == Convert.ToString(Session["uID"]))
                    {
                        ltlAlert.Text = "alert('�����˲����ύ������������������ѡ��������')";
                        return;
                    }

                    int iApproveUserId = Convert.ToInt32(txt_approveID.Text.ToString());
                    string strApproveName = txt_approveName.Text.ToString();
                    admin_AccessApply.newApplyApproveProcess(iApplyId, iApproveUserId, strApproveName);

                    string mailto = txt_approveEmail.Text.ToString();
                    string mailfrom = txtApplyEmail.Text.ToString();
                    string displayName = Convert.ToString(Session["uName"]);
                    string mailSubject = "�뼰ʱ����" + Session["uName"] + "�Ĳ˵����루��ţ�" + iApplyId + "��";
                    StringBuilder sbMailContent = new StringBuilder();
                    sbMailContent.Append("<html>");
                    sbMailContent.Append("<body>");
                    sbMailContent.Append("<form>");
                    sbMailContent.Append(txt_approveName.Text.ToString() + ":<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;��ã�<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"] + "����" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "���������˲˵��ķ���Ȩ�ޣ��뼰ʱ�������������£�<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;�������ɣ�" + txtApplyReason.Text.ToString() + "<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;ģ����Ϣ�� <br />");
                    int i = 1;
                    foreach (ListItem item in chkBl_SelectedModule.Items)
                    {
                        sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (i++) + "��" + item.Text.ToString() + "<br />");
                    }
                    sbMailContent.Append("<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;�������¼100ϵͳ�����˵���Admin��=>��Profile>>��=>��Ȩ������������������������������ӣ�<br />");
                    sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;"+baseDomain.getPortalWebsite()+"/AccessApply/admin_accessApplymstr.aspx?aamId=" + iApplyId + "<br /> ");
                    sbMailContent.Append("</body>");
                    sbMailContent.Append("</form>");
                    sbMailContent.Append("</html>");
                    string mailContent = Convert.ToString(sbMailContent);

                    
                    //admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);
                    if (this.SendEmail(mailfrom, mailto, "", mailSubject, mailContent))
                    {
                        ltlAlert.Text = "alert('�ύ�ɹ�����ȴ�����');window.location.href = 'admin_accessApplymstr.aspx?rm=" + DateTime.Now.ToString() + "';";
                        btnSubmit.Enabled = false;
                        //Response.Redirect("accessApplyProcess.aspx", true);
                        return;
                    }
                    else
                    {
                        ltlAlert.Text = "alert('�ʼ�����ʧ�ܣ�������ɹ�����������ϵ������')";
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
