using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pc_InquiryList : System.Web.UI.Page
{
    PC_price pc = new PC_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                if (Request["vender"] != null)
                {
                    txtVender.Text = Request["vender"].ToString();
                }
                if (Request["txtIMID"] != null)
                {
                    txtIMID.Text = Request["txtIMID"].ToString();
                }
                if (Request["ItemCode"] != null)
                {
                    txtItemCode.Text = Request["ItemCode"].ToString();
                    
                }
                if (Request["QAD"] != null)
                {
                    txtQAD.Text = Request["QAD"].ToString();
                    
                }
                if (Request["VenderName"] != null)
                {
                    txtVenderName.Text = Request["VenderName"].ToString();
                    
                }
                if (Request["chkSelf"] != null)
                {
                    chkSelf.Checked =(Request["chkSelf"].ToString()=="1"?true:false);
                    
                }
                if (Request["ddlStatus"] != null)
                {
                    ddlStatus.SelectedValue = Request["ddlStatus"].ToString();
                    
                }
 
            bind();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind()
    {
    
        DataTable dt = pc.selectInquiryList(txtIMID.Text.ToString().Trim(), txtVender.Text.ToString().Trim(), txtVenderName.Text.ToString().Trim(), txtQAD.Text.ToString().Trim(),ddlStatus.SelectedItem.Value,(chkSelf.Checked?"1":"0"),txtItemCode.Text.Trim());
        gvInquiryInfo.DataSource = dt;
        gvInquiryInfo.DataBind();
    }
    protected void gvInquiryInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInquiryInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvInquiryInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           int status=Convert.ToInt32( ((Label)e.Row.Cells[3].FindControl("lbStatus")).Text);
           if (status == 0)
           { 
                ((Label)e.Row.Cells[3].FindControl("lbStatus")).Text="未报价";
                ((LinkButton)e.Row.Cells[4].FindControl("lkbtnOperation")).Text = "报价";
           }
           else if (status == 1)
           { 
                ((Label)e.Row.Cells[3].FindControl("lbStatus")).Text="已报价";
                ((LinkButton)e.Row.Cells[4].FindControl("lkbtnOperation")).Text = "核价";
           }
           else if (status == 2)
           {
               ((Label)e.Row.Cells[3].FindControl("lbStatus")).Text = "已核价";
               ((LinkButton)e.Row.Cells[4].FindControl("lkbtnOperation")).Text = "核价";
           }
           else if (status == 3)
           {
               ((Label)e.Row.Cells[3].FindControl("lbStatus")).Text = "已完成";
               ((LinkButton)e.Row.Cells[4].FindControl("lkbtnOperation")).Text = "查看";
           }

        
        }
    }
    protected void gvInquiryInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnOperation")
        {
            Response.Redirect("pc_InquiryDet.aspx?IMID=" + e.CommandArgument.ToString() + "&ComeFrom=0" + "&vender=" + txtVender.Text + "&ItemCode="
                + txtItemCode.Text + "&QAD=" + txtQAD.Text + "&VenderName=" + txtVenderName.Text + "&chkSelf=" + (chkSelf.Checked ? "1" : "0")+"&ddlStatus=" + ddlStatus.SelectedItem.Value + "&txtIMID=" +txtIMID.Text);
            //ltlAlert.Text = "window.showModalDialog('pc_InquiryDet.aspx?IMID=" + e.CommandArgument.ToString() +"&ComeFrom=0" +"', window, 'dialogHeight: 900px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
            //ltlAlert.Text += "window.location.href = 'pc_InquiryList.aspx?vender=" + txtVender.Text + "&IMID=" + txtIMID.Text + "&ItemCode="
            //    + txtItemCode.Text + "&QAD=" + txtQAD.Text + "&VenderName=" + txtVenderName.Text + "&chkSelf=" + (chkSelf.Checked?"1":"0")
            //    + "&ddlStatus=" + ddlStatus.SelectedItem.Value + "'";

        }
    }
    protected void btnMakeInquiry_Click(object sender, EventArgs e)
    {
        Response.Redirect("pc_MadeInquiryList.aspx");
    }
}