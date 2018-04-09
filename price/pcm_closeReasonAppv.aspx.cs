using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_closeReasonAppv : System.Web.UI.Page
{
    PCM_FinCheckApply help = new PCM_FinCheckApply();

    protected void Page_Load(object sender, EventArgs e)
    {
        lbDetID.Text = Request["DetID"].ToString();
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
        ltlAlert.Text = "window.close();";
    }
    protected void btnCloseAppv_Click(object sender, EventArgs e)
    {
        if (string.Empty.Equals(txtCloseReason.Text.ToString().Trim()) || "请输入关闭原因：".Equals(txtCloseReason.Text.ToString().Trim()))
        {
            ltlAlert.Text = "alert('请填写关闭原因！');";
        }
        else
        {
            int flag = help.appvCloce(lbDetID.Text, Convert.ToInt32(Session["uID"]), txtCloseReason.Text);
            if (flag==1)
            {
                ltlAlert.Text = "alert('申请成功');";
                ltlAlert.Text = "window.close();";
            }
            else if(flag == -1)
            {
                 ltlAlert.Text = "alert('该号码已经在申请中');";
            }
            else
            {
                ltlAlert.Text = "alert('申请失败');";
            }
        }
    }
}