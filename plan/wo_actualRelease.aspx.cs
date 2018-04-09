using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class plan_wo_actualRelease : BasePage
{
    private wo.Wo_ActualRelease helper = new wo.Wo_ActualRelease();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = txtDateFrom.Text.Trim();
        string relDateTo = txtDateTo.Text.Trim();
        string actDateFrom = txtActDateFrom.Text.Trim();
        string actDateTo = txtActDateTo.Text.Trim();
        string domain = ddlDomain.SelectedItem.Text;
        gvlist.DataSource = helper.GetWoActRelList(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain);
        gvlist.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = txtDateFrom.Text.Trim();
        string relDateTo = txtDateTo.Text.Trim();
        string actDateFrom = txtActDateFrom.Text.Trim();
        string actDateTo = txtActDateTo.Text.Trim();
        string domain = ddlDomain.SelectedItem.Text;
        DataTable dt = helper.GetWoActRelList(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain);
        string title = "100^<b>加工单</b>~^100^<b>ID</b>~^120^<b>QAD</b>~^100^<b>QAD下达日期</b>~^100^<b>计划日期</b>~^100^<b>评审日期</b>~^100^<b>上线日期</b>~^100^<b>工单数量</b>~^100^<b>地点</b>~^100^<b>生产线</b>~^100^<b>成本中心</b>~^100^<b>工厂</b>~^100^<b>周期章</b>~^";
        ExportExcel(title, dt, false);
    }
}