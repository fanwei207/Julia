using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_PCD_ApplyReason : BasePage
{
    private edi.PCDApply helper = new edi.PCDApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string poNbr = Request.QueryString["poNbr"];
            string poLine = Request.QueryString["poLine"];
            int status = helper.GetPCDApproveStatus(poNbr, poLine);
            switch (status)
            {
                case 0:
                    ltlAlert.Text = "alert('此订单行没有审批数据');$('BODY', parent.parent.parent.document).find('#JULIA-MODAL-DIALOG').remove();";
                    break;
                case 1:
                    Response.Redirect("/NWF/nwf_workflowInstanceResult.aspx?FlowId=11579712-7103-470E-A1AB-B9906549B1E4&keyWords=" + poNbr, true);
                    break;
                case 2:
                    Response.Redirect("/NWF/nwf_workflowInstanceResult.aspx?FlowId=87AB52EF-1D03-4EA6-AA64-37320B16F639&keyWords=" + poNbr, true);
                    break;
            }
            
        }
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        if (ddlReason.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择修改原因！')";
            return;
        }
        if(txtApplyDate.Text.Trim()=="")
        {
            ltlAlert.Text = "alert('请输入申请日期！')";
            return;
        }
        string poNbr = Request.QueryString["poNbr"];
        string poLine = Request.QueryString["poLine"];
        helper.StartApprove(poNbr, poLine, ddlReason.SelectedValue, ddlReason.SelectedItem.Text, txtRemark.Text.Trim(), txtApplyDate.Text.Trim(), Session["uID"].ToString());
        ltlAlert.Text = "alert('修改申请成功！');$.loading('none');$('BODY', parent.parent.parent.document).find('#JULIA-MODAL-DIALOG').remove();";

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "$('BODY', parent.parent.parent.document).find('#JULIA-MODAL-DIALOG').remove();";
    }
}