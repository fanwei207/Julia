using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_FinCheckDayDet : System.Web.UI.Page
{
    PCM_FinCheckApply helper = new PCM_FinCheckApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["vender"] != null)
            {
                lbVender.Text = Request["vender"].ToString();
                lbDate.Text = Request["date"].ToString();
                lbVenderName.Text = Request["venderName"].ToString();
            }
            bind();
        
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("pcm_FinCheckDayList.aspx");
    }
    private void bind()
    {
        gvInfo.DataSource = helper.selectFinCheckDayDet(lbDate.Text, lbVender.Text);
        gvInfo.DataBind();
    
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string stroutFile = "pc_FinCheckDayDet_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        helper.createExcelFinCheckDay(lbDate.Text, lbVender.Text, stroutFile,lbVenderName.Text);
        ltlAlert.Text = "window.open('/Excel/" + stroutFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
}