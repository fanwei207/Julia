using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class plan_PCD_Report : BasePage
{
    private edi.PCDApply helper = new edi.PCDApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        gvDet.DataSource = helper.GetPCDReport(txtFormDate.Text.Trim(), txtToDate.Text.Trim());
        gvDet.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = helper.GetPCDReport(txtFormDate.Text.Trim(), txtToDate.Text.Trim());
        string title = "<b>订单号</b>~^<b>客户</b>~^200^<b>客户名称</b>~^50^<b>行号</b>~^<b>销售单</b>~^200^<b>客户零件号</b>~^<b>QAD号</b>~^50^<b>数量</b>~^<b>需求日期</b>~^<b>接收日期</b>~^50^<b>审批状态</b>~^<b>审批完成日期</b>~^";
        this.ExportExcel(title, dt, true);
    }
}