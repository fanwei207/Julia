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
            ltlAlert.Text = "alert('��˵�������������')";
            return; 
        }

        if (txt_approveID.Text == String.Empty || txt_approveName.Text == String.Empty)
        {

            ltlAlert.Text = "alert('��ѡ��������')";
            return;
        }
        else
        {
            if (txt_approveEmail.Text == String.Empty)
            {
                ltlAlert.Text = "alert('��ϵͳδά�������˵����䣬���ֶ�ά�������˵�����')";
                return;
            }
        }

        DataTable dt = null;
        dt = admin_AccessApply.isHaveInternetAcess(Convert.ToString(Session["uId"]));
        if (Convert.ToInt32(dt.Rows[0]["accInternet"]) == 1)
        {
            ltlAlert.Text = "alert('�����д���������100��Ȩ�ޣ�����Ҫ������')";
            return;
        }
        else 
        {
            dt = admin_AccessApply.isHaveInternetApply(Convert.ToString(Session["uId"]));
            if (Convert.ToInt32(dt.Rows[0][0]) == 1)
            {
                ltlAlert.Text = "alert('���Ѿ��ύ��������100��Ȩ�����룬����������������')";
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
            string mailSubject = "�뼰ʱ����" + Session["uName"] + "��������100������ ";
            StringBuilder sbMailContent = new StringBuilder();
            sbMailContent.Append("<html>");
            sbMailContent.Append("<body>");
            sbMailContent.Append("<form>");
            sbMailContent.Append(txt_approveName.Text.ToString() + ":<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;��ã�<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + Session["uName"] + "����" + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "������������������100ϵͳȨ�ޣ��뼰ʱ�������������£�<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;�������ɣ�" + txtApplyReason.Text.ToString() + "<br />");
            sbMailContent.Append("<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;�������¼100ϵͳ�����˵���Admin��=>��Profile>>��=>��Ȩ������������������������������ӣ�<br />");
            sbMailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;http://portal.tcp-china.com/AccessApply/admin_ApproveInternet.aspx <br /> ");
            sbMailContent.Append("</body>");
            sbMailContent.Append("</form>");
            sbMailContent.Append("</html>");
            string mailContent = Convert.ToString(sbMailContent);
            string msg = admin_AccessApply.SendEmail(mailto, mailfrom, displayName, mailSubject, mailContent);
            if (msg != string.Empty)
            {
                ltlAlert.Text = "alert('�ύ�ɹ�����" + msg + "�����������ⷢ�ʼ����������')";
                btnSubmit.Text = "���ύ";
                btnSubmit.Enabled = false;
                return;
            }
            else
            {
                ltlAlert.Text = "alert('�ύ�ɹ���ϵͳ�ѷ��ʼ����������')";
                btnSubmit.Text = "���ύ";
                btnSubmit.Enabled = false;
                return;
            }

        }
        else
        {
            ltlAlert.Text = "alert('δ�ύ�ɹ����������ύ����')"; 
            return;
        }
         
    }
}
