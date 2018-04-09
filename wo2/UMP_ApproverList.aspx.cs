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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using RD_WorkFlow;
using System.Net.Mail;
using System.Text;
using System.IO;
using adamFuncs;
using CommClass;


public partial class wo2_UMP_ApproverList : BasePage
{
    private adamClass adam = new adamClass();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("630090003", "通过权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_approve.Visible = this.Security["630090003"].isValid;
            BindProjData();
        }
    }
    private void BindProjData()
    {
        //string strID = Convert.ToString(Request.QueryString["mid"]);

        DataTable dt = getProjQadApplyMstr(txtProjectcode.Text, ddlstatus.SelectedValue, chkb_displayToApprove.Checked, Session["uID"].ToString(), ddldomain.SelectedItem.Text, txtdepCode.Text);

        gv.DataSource = dt;
        gv.DataBind();

    }
    public DataTable getProjQadApplyMstr(string projCode, string status, bool readme, string uid, string domain, string depcode)
    {
        string strSql = "sp_UMP_selectUMPApplyList";
        SqlParameter[] sqlParam = new SqlParameter[8];
        sqlParam[0] = new SqlParameter("@projCode", projCode);
        sqlParam[1] = new SqlParameter("@status", status);
        sqlParam[2] = new SqlParameter("@readme", readme);
        sqlParam[3] = new SqlParameter("@uid", uid);
        sqlParam[4] = new SqlParameter("@domain", domain);
        sqlParam[5] = new SqlParameter("@depcode", depcode);
        sqlParam[6] = new SqlParameter("@date", txtApplyDate.Text);
        sqlParam[7] = new SqlParameter("@date2", txtApplyDate1.Text);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        this.BindProjData();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "look")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string UMP_appby = gv.DataKeys[index].Values["UMP_appby"].ToString();
            string UMP_createby = gv.DataKeys[index].Values["UMP_createby"].ToString();
            string UMP_status = gv.DataKeys[index].Values["UMP_status"].ToString();
            string appv = "0";
            if (UMP_appby == "")
            {
                if (UMP_status == "新增")
                {
                    if (UMP_createby == Session["uID"].ToString())
                    {
                        appv = "1";
                    }
                }
            }
            else
            {
                if (UMP_appby == Session["uID"].ToString())
                {
                    appv = "1";
                }
            }
            //if (chkb_displayToApprove.Checked)
            //{
            //    appv = "1";
            //}
            string strmid = gv.DataKeys[index].Values["id"].ToString();
            Response.Redirect("UMP_add.aspx?mid=" + strmid + "&islook=yes&iApprove=" + appv, true);
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((gv.DataKeys[e.Row.RowIndex].Values["UMP_status"]).ToString() != "新增")
            {
                e.Row.Cells[14].Text = string.Empty;
            }

        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindProjData();
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("UMP_add.aspx", true);
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = getProjQadApplyMstr(txtProjectcode.Text, ddlstatus.SelectedValue, chkb_displayToApprove.Checked, Session["uID"].ToString(), ddldomain.SelectedItem.Text, txtdepCode.Text);
        string title = "<b>申请单号</b>~^<b>状态</b>~^<b>创建人</b>~^<b>创建时间</b>~^<b>域 Code</b>~^100^<b>地点</b>~^100^<b>成本中心</b>~^100^<b>类型</b>~^<b>总账</b>~^100^<b>明细账</b>~^";
        this.ExportExcel(title, dt, true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void chkb_displayToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindProjData();
        if (chkb_displayToApprove.Checked && ddlstatus.SelectedValue == "1")
        {
            btnappv.Visible = true;
        }
        else
        {
            btnappv.Visible = false;
        }
    }
    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gv.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gv.DataKeys[i].Values["id"].ToString() + ",";
            }
        }
        return strSelect;
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }
        if (txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择提交人!'); ";
            return;
        }

        if (chkEmail.Checked)
        {
            if (txt_ApproveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('你选择的提交人没有邮箱!'); ";
                return;
            }
        }
        string strRet = chkSelect();
        if (strRet.Length == 0)
        {
            ltlAlert.Text = "alert('请至少勾选一条数据!'); ";
            return;
        }
        string[] strsid;
        strRet = strRet.Substring(0, strRet.Length - 1);
        strsid = strRet.Split(',');


        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {
            bool isSuccess = true;
            foreach (var item in strsid)
            {
                string mid = item;
                AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, "1");

                //ltlAlert.Text += " window.location.href='UMP_ApproverList.aspx?code=" + txtUMPcode + "rm=" + DateTime.Now.ToString() + "';";

            }
            if (chkEmail.Checked)
            {
                isSuccess = SendMail2(txt_ApproveEmail.Text.Trim(), strApproverName, Session["Email"].ToString(), Session["uName"].ToString(), applyReason);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Apply successfully!');";
                BindProjData();
            }

        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            BindProjData();
            return;
        }
    }
    public int AddProjQadLink(string mid, string strApprover, string strApproverName, string applyReason, int uID, string uName, string status)
    {
        string strSql = "sp_UMP_InsertProjQadApply";
        SqlParameter[] sqlParam = new SqlParameter[7];
        sqlParam[0] = new SqlParameter("@mid", mid);
        sqlParam[1] = new SqlParameter("@approverBy", strApprover);
        sqlParam[2] = new SqlParameter("@approverName", strApproverName);
        sqlParam[3] = new SqlParameter("@applyReason", applyReason);
        sqlParam[4] = new SqlParameter("@uId", uID);
        sqlParam[5] = new SqlParameter("@uName", uName);
        sqlParam[6] = new SqlParameter("@status", status);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
    }
    public bool SendMail2(string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string applyReason)
    {
        StringBuilder sb = new StringBuilder();

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
        MailAddress to;
        MailMessage mail = new MailMessage();
        mail.From = from;
        Boolean isSuccess = false;
        try
        {
            to = new MailAddress(toEmailAddress, strApproverName);
            mail.To.Add(to);
        }
        catch
        {
            // returnMessage = "the email address of " + toEmailAddress + "is incorrect.Pls correct!";
            return false;
        }

        if (applyEmailAddress != null && applyEmailAddress != "")
        {
            MailAddress cc = new MailAddress(applyEmailAddress, applyName);
            mail.CC.Add(cc);
        }

        try
        {
            mail.Subject = "[Notify]计划外出入库有需要你审批的数据";
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append(" 审批员你好  <br>");

            // sb.Append("     申请单号:" + projectCode + "<br>");
            sb.Append("     申请说明:" + applyReason + "<br>");
            sb.Append("     申请者: " + applyName + "在此项目添加了计划外出入库申请，请链接以下地址时查看并审批申请<br><br>");

            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/wo2/UMP_ApproverList.aspx' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/wo2/UMP_ApproverList.aspx</a>");
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
            //returnMessage = "Send Mail Success!";
        }
        catch (Exception ex)
        {
            isSuccess = false;
            // returnMessage = "Send Mail Failed!";
        }

        return isSuccess;


    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }
        string strRet = chkSelect();
        if (strRet.Length == 0)
        {
            ltlAlert.Text = "alert('请至少勾选一条数据!'); ";
            return;
        }
        string[] strsid;
        strRet = strRet.Substring(0, strRet.Length - 1);
        strsid = strRet.Split(',');

        //string mid = lbl_id.Text;
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {
           
            foreach (var item in strsid)
            {
                string mid = item;
                AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, "2");
                BindProjData();
             

            }
          

          
        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            BindProjData();
            return;
        }
    }
    protected void btn_diaApp_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Approver_Click(object sender, EventArgs e)
    {
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        ltlAlert.Text = "var w=window.open('UMP_getapprover.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gv.Rows[i].FindControl("chk_Select");
            if (lblcheck.Text=="0")
            {
                cb.Checked = true;
                lblcheck.Text = "1";
            }
            else
            {
                cb.Checked = false;
                lblcheck.Text = "0";
            }
        }
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProjData();
        if (chkb_displayToApprove.Checked && ddlstatus.SelectedValue == "1")
        {
            btnappv.Visible = true;
        }
        else
        {
            btnappv.Visible = false;
        }
    }
    protected void btnappv_Click(object sender, EventArgs e)
    {
         string strRet = chkSelect();
        if (strRet.Length == 0)
        {
            ltlAlert.Text = "alert('请至少勾选一条数据!'); ";
            return;
        }
        ltlAlert.Text = "var w=window.open('UMP_ApproverlistAppv.aspx?mid=" + strRet + "','','menubar=No,scrollbars = No,resizable=No,width=800,height=300,top=200,left=300'); w.focus();";
       // ltlAlert.Text = "window.open('UMP_ApproverlistAppv.aspx?mid=" + strRet + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 350px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'UMP_ApproverList.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "'";
        //BindProjData();
    }
}