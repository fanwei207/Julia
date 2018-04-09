using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pc_closeReasonAppv : System.Web.UI.Page
{
    PC_FinCheckApply help = new PC_FinCheckApply();

    protected void Page_Load(object sender, EventArgs e)
    {
        lbDetID.Text = Request["DetID"].ToString();
        if (Session["uID"].ToString() != Request.QueryString["createdID"])
        {
            chkDiv.Visible = false;
        }
        bind();
        
    }
    private void bind()
    { 
        string QAD = string.Empty;
        string code = string.Empty;
        string price = string.Empty;
        string vender = string.Empty;
        string venderName = string.Empty;
        string checkPrice = string.Empty;
        help.selectInfoToCloseAppv(lbDetID.Text,out QAD,out code,out price,out vender ,out venderName,out checkPrice);
        lbQAD.Text = QAD;
        lbVender.Text = vender;
        lbCheckPrice.Text = checkPrice;
        lbPrice.Text = price;
        lbCode.Text = code;
        lbVenderName.Text = venderName;
    
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var loc=$('body', parent.document).find('#ifrm_121000015')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
    }
    protected void btnCloseAppv_Click(object sender, EventArgs e)
    {
        if (string.Empty.Equals(txtCloseReason.Text.ToString().Trim()) || "请输入关闭原因：".Equals(txtCloseReason.Text.ToString().Trim()))
        {
            ltlAlert.Text = "alert('请填写关闭原因！');";
        }
        else
        {

            int isdelete = chkIsClose.Checked? 0:1;
            int flag = help.appvCloce(lbDetID.Text, Convert.ToInt32(Session["uID"]), txtCloseReason.Text, isdelete);
            if (flag==1)
            {
                this.ltlAlert.Text = " alert('申请成功'); var loc=$('body', parent.document).find('#ifrm_121000015')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
            }
            else if(flag == -1)
            {
                 ltlAlert.Text = "alert('该号码已经在申请中');";
            }
            else if (flag == -2)
            {
                ltlAlert.Text = "alert('您不是该申请的创建者，请联系申请者关闭申请');";
            }
            else
            {
                ltlAlert.Text = "alert('申请失败');";
            }
        }
    }
}