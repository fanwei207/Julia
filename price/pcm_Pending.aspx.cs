using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_Pending : BasePage
{
    PCM_FinCheckApply help = new PCM_FinCheckApply();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbDetID.Text=Request["DetID"].ToString();
            lbIsPrice.Text = Request["isPrice"].ToString();
            lbStatus.Text = Request["status"].ToString();
            bind();
        }

    }
    private void bind()
    {
        string QAD = string.Empty;
        string code = string.Empty;
        string price = string.Empty;
        string vender = string.Empty;
        string checkPrice = string.Empty;
        help.selectApplyDetByDetID(lbDetID.Text, out QAD, out code, out price, out vender, out checkPrice);
        lbQAD.Text = QAD;
        lbVender.Text = vender;
        lbCheckPrice.Text = checkPrice;
        lbPrice.Text = price;
        lbCode.Text = code;
        
    }
    protected void btnPending_Click(object sender, EventArgs e)
    {
        if (string.Empty.Equals(txtPending.Text.ToString().Trim()) || "请输入挂起原因：".Equals(txtPending.Text.ToString().Trim()))
        {
            ltlAlert.Text = "alert('请填写挂起原因！');";
        }
        else
        {
            
            if (help.updateApplyDetToPinding(lbDetID.Text, Convert.ToInt32(Session["uID"]), txtPending.Text))
            {
                ltlAlert.Text = "alert('挂起成功');";
                Response.Redirect("pcm_FinPrice.aspx?isPrice=" + lbIsPrice.Text + "&status=" + lbStatus.Text);
            }
            else
            {
                ltlAlert.Text = "alert('挂起失败');";
            }
        }
        
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("pcm_FinPrice.aspx?isPrice=" + lbIsPrice.Text + "&status=" + lbStatus.Text);
    }
}