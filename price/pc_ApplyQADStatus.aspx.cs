using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pc_ApplyQADStatus : BasePage
{
    PC_price pc = new PC_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }

    private void bind()
    {
        string QAD = txtQAD.Text;
        string vender = txtVender.Text;
        string venderName = txtVenderName.Text;
        string status = ddlStatus.SelectedItem.Value;
        DataTable dt = pc.selectApplyQADStatus(Convert.ToInt32(Session["uID"]),QAD,vender,venderName,status);
        gvInfo.DataSource = dt;
        gvInfo.DataBind();
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string QAD = txtQAD.Text;
        string vender = txtVender.Text;
        string venderName = txtVenderName.Text;
        string status = ddlStatus.SelectedItem.Value;

        DataTable dt = pc.selectApplyQADStatus(Convert.ToInt32(Session["uID"]), QAD, vender, venderName, status);

        string title = "100^<b>QAD</b>~^100^<b>部件号</b>~^80^<b>供应商</b>~^150^<b>供应商名称</b>~^100^<b>审批状态</b>~^100^<b>询价状态</b>~^100^<b>报价处理结果</b>~^100^<b>核价处理结果</b>~^";

        if (dt != null && dt.Rows.Count > 0)
        {
            ExportExcel(title, dt, false);
        }

    }
}