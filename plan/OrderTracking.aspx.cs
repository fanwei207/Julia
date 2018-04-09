using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class plan_OrderTracking :BasePage
{
    private edi.OrderTracking helper = new edi.OrderTracking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRegionData();
            //BindData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();      
    }

    private void BindData()
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        string status = ddlStatus.SelectedValue;
        DataTable dt = helper.GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, status, type);
        foreach (DataRow row in dt.Rows)
        {
            if (!this.Security["44000611"].isValid)
            {
                row["det_price"] = DBNull.Value;
            }
        }

        gvlist.DataSource = dt;
        gvlist.DataBind();
        txtTotal.Text = dt.Rows.Count.ToString();
    }

    private void BindRegionData()
    {
        ddlRegion.DataSource = helper.GetRegions(Session["uID"].ToString());
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("--", ""));
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        string status = ddlStatus.SelectedValue;
        DataTable dt = helper.GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, status, type);
        foreach (DataRow row in dt.Rows)
        {
            if (!this.Security["44000611"].isValid)
            {
                row["det_price"] = DBNull.Value;
            }
        }

        string title = "<b>Order#</b>~^<b>Order Date</b>~^<b>Region</b>~^<b>Customer Code</b>~^200^<b>Customer</b>~^50^<b>Line</b>~^200^<b>Item</b>~^<b>Order Qty</b>~^200^<b>Order Question</b>~^<b>Load QAD Date</b>~^<b>Qad So#</b>~^110^<b>QAD Part</b>~^<b>Part Type</b>~^<b>Work Hours</b>~^<b>Order Qty</b>~^<b>Request Date</b>~^<b>Wo Qty</b>~^<b>Online Qty</b>~^<b>Warehousing Date</b>~^<b>Ship Qty</b>~^<b>Inspection Date</b>~^<b>Ship Date</b>~^<b>PCD</b>~^<b>PCD创建人</b>~^<b>PCD创建日期</b>~^<b>价格</b>~^<b>分类</b>~^200^<b>PCD备注</b>~^<b>是否开票</b>~^<b>制地</b>~^<b>周期章</b>~^";
        this.ExportExcel(title, dt, true);
        
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = e.Row.RowIndex;
            if (gvlist.DataKeys[rowIndex].Values["que"].ToString() == "0")
            {
                e.Row.Cells[5].Controls[0].Visible = false;
            }
            string planDate=gvlist.DataKeys[rowIndex].Values["planDate"].ToString();
            if (planDate == "")
            {
                (e.Row.Cells[16].FindControl("linkPCD") as LinkButton).Text = "Apply";
            }
            if (gvlist.DataKeys[rowIndex].Values["sod_ord_date"].ToString() == string.Empty)
            {
                e.Row.Cells[5].Controls[0].Visible = true;
            }
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        helper.RefreshOrderTracking();
        BindData();
    }
    protected void btnExportWo_Click(object sender, EventArgs e)
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        string status = ddlStatus.SelectedValue;
        DataTable dt = helper.GetOrderTrackingWithWo(po1, po2, orderDate1, orderDate2, region, customer, status, type);
        foreach (DataRow row in dt.Rows)
        {
            if (!this.Security["44000611"].isValid)
            {
                row["det_price"] = DBNull.Value;
            }
        }

        string title = "<b>Order#</b>~^<b>Order Date</b>~^<b>Region</b>~^<b>Customer Code</b>~^200^<b>Customer</b>~^50^<b>Line</b>~^200^<b>Item</b>~^<b>Order Qty</b>~^200^<b>Order Question</b>~^<b>Load QAD Date</b>~^<b>Qad So#</b>~^110^<b>QAD Part</b>~^<b>Part Type</b>~^<b>Work Hours</b>~^<b>Order Qty</b>~^<b>Request Date</b>~^<b>Wo Qty</b>~^<b>Online Qty</b>~^<b>Warehousing Date</b>~^<b>Ship Qty</b>~^<b>Inspection Date</b>~^<b>Ship Date</b>~^<b>PCD</b>~^<b>PCD创建人</b>~^<b>PCD创建日期</b>~^<b>价格</b>~^<b>分类</b>~^200^<b>PCD备注</b>~^<b>是否开票</b>~^<b>制地</b>~^<b>周期章</b>~^<b>Wo#</b>~^<b>Wo ID</b>~^110^<b>QAD Part</b>~^<b>Wo Qty</b>~^<b>Complete Qty</b>~^<b>Product Line</b>~^<b>Site</b>~^<b>Release Date</b>~^<b>Online Date</b>~^<b>Offline Date</b>~^<b>Finish Date</b>~^";
        this.ExportExcel(title, dt, true, 29, "poNbr", "poLine");
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PCD")
        {
            int rowIndex = ((e.CommandSource as Control).Parent.Parent as GridViewRow).RowIndex;
            string poNbr = gvlist.DataKeys[rowIndex].Values["poNbr"].ToString();
            string poLine = gvlist.DataKeys[rowIndex].Values["poLine"].ToString();
            //string url = string.Format("/plan/PCD_ApplyReason.aspx?poNbr={0}&poLine={1}", poNbr, poLine);
            string url = string.Format("/plan/PCD_View.aspx?poNbr={0}&poLine={1}", poNbr, poLine);
            ltlAlert.Text = "$.window('明细',1200,800,'" + url.ToString() + "');";
        }
    }
}