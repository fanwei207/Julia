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
using System.Text;

public partial class AccessApply_ApplyInternetAcc : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        } 
    }
    protected void btn_chooseApprove_Click(object sender, EventArgs e)
    {
        txt_approveName.ForeColor = System.Drawing.Color.Black;
        ltlAlert.Text = "var w=window.open('admin_accessChooseApprove.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Length <= 0)
        {
            ltlAlert.Text = "alert('请说明你的申请理由')";
            return; 
        }

        if (txt_approveID.Text == String.Empty || txt_approveName.Text == String.Empty)
        {

            ltlAlert.Text = "alert('请选择审批人')";
            return;
        }
        else
        {
            if (txt_approveEmail.Text == String.Empty)
            {
                ltlAlert.Text = "alert('因系统未维护审批人的邮箱，请手动维护审批人的邮箱')";
                return;
            }
        }

        DataTable dt = null;
        dt = admin_AccessApply.isHaveInternetAcess(Convert.ToString(Session["uId"]));
        if (Convert.ToInt32(dt.Rows[0]["accInternet"]) == 1)
        {
            ltlAlert.Text = "alert('你已有从外网访问100的权限，不需要再申请')";
            return;
        }
        else 
        {
            dt = admin_AccessApply.isHaveInternetApply(Convert.ToString(Session["uId"]));
            if (Convert.ToInt32(dt.Rows[0][0]) == 1)
            {
                ltlAlert.Text = "alert('你已经提交外网访问100的权限申请，请提醒审批人审批')";
                return; 
            }
        }

        String strApplyUserId = Convert.ToString(Session["uId"]);
        String strApplyUserName = Convert.ToString(Session["uName"]);
        String strApplyLoginName = Convert.ToString(Session["loginName"]);
        String strApplyUserEmail = Convert.ToString(Session["email"]);
        String strApplyUPlant = Convert.ToString(Session["PlantCode"]);
        String strApplyReason = txtApplyReason.Text.ToString().Trim();
        String strApproveUserId = txt_approveID.Text.ToString();
        String strApproveUserName = txt_approveName.Text.ToString();
        String strApproveUserEmail = txt_approveEmail.Text.ToString();

        if (admin_AccessApply.insertApplyInternetAccMstr(strApplyUserId, strApplyUserName, strApplyLoginName, strApplyUPlant, strApplyReason, strApproveUserId, strApproveUserName))
        {
            string mailto = strApproveUserEmail;
            string mailfrom = strApplyUserEmail;
            string displayName = Convert.ToString(Session["uName"]);
            string mailSubject = "请及时处理" + Session["uName"] + "外网访问100的申请 ";
            StringBuilder sbMailContent = new StringBuilder();
            sbMailContent.Append("<html>");
            sbMailContent.Append("<body>");
            sbMailContent.Append("<form>");
            sbMailContent.Append(txt_approveName.Text.ToString() + ":<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;你好！<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"] + "已于" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "向你申请了外网访问100系统权限，请及时审批。详情如下：<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;申请理由：" + txtApplyReason.Text.ToString() + "<br />");
            sbMailContent.Append("<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;详情请登录100系统，至菜单【Admin】=>【Profile>>】=>【权限申请管理】进行审批；或点击以下链接：<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;http://portal.tcp-china.com/AccessApply/admin_ApproveInternet.aspx <br /> ");
            sbMailContent.Append("</body>");
            sbMailContent.Append("</form>");
            sbMailContent.Append("</html>");
            string mailContent = Convert.ToString(sbMailContent);
            string msg = admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);
            if (msg != string.Empty)
            {
                ltlAlert.Text = "alert('提交成功，但" + msg + "，建议你另外发邮件提醒审核人')";
                btnSubmit.Text = "已提交";
                btnSubmit.Enabled = false;
                return;
            }
            else
            {
                ltlAlert.Text = "alert('提交成功，系统已发邮件提醒审核人')";
                btnSubmit.Text = "已提交";
                btnSubmit.Enabled = false;
                return;
            }

        }
        else
        {
            ltlAlert.Text = "alert('未提交成功，请重新提交申请')"; 
            return;
        }
         
    }
}
