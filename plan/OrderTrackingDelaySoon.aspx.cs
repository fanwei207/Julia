using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using CommClass;


public partial class plan_OrderTrackingDelaySoon : BasePage
{
    private edi.OrderTracking helper = new edi.OrderTracking();
    private string connStr = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRegionData();
            //BindData();
        }
    }
    private void BindRegionData()
    {
        ddlRegion.DataSource = helper.GetRegions(Session["uID"].ToString());
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("--", ""));
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
        //string status = ddlStatus.SelectedValue;
        DataTable dt = GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, type);
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
    public DataTable GetOrderTracking(string po1, string po2, string dateFrom, string dateTo, string region, string customer, string type)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@po1", po1);
        param[1] = new SqlParameter("@po2", po2);
        param[2] = new SqlParameter("@ordDate1", dateFrom);
        param[3] = new SqlParameter("@ordDate2", dateTo);
        param[4] = new SqlParameter("@region", region);
        param[5] = new SqlParameter("@customer", customer);
        param[6] = new SqlParameter("@type", type);
        return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectOrderTrackingDelaySoon", param).Tables[0];
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        //string status = ddlStatus.SelectedValue;
        DataTable dt = GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, type);
        string title = "<b>Order#</b>~^<b>SID nbr</b>~^<b>Order Date</b>~^<b>Request Date</b>~^<b>Ship Date</b>~^100^<b>Customer Code</b>~^100^<b>Line</b>~^200^<b>Item</b>~^<b>Qad So#</b>~^<b>QAD Part</b>~^<b>Order Qty</b>~^<b>Wo Qty</b>~^<b>Online Qty</b>~^<b>Ship Qty</b>~^<b>分类</b>~^";
        this.ExportExcel(title, dt, true);
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = e.Row.RowIndex;
            if (gvlist.DataKeys[rowIndex].Values["que"].ToString() == "0")
            {
                //e.Row.Cells[5].Controls[0].Visible = false;
            }
            string planDate = gvlist.DataKeys[rowIndex].Values["planDate"].ToString();
            if (planDate == "")
            {
                // (e.Row.Cells[16].FindControl("linkPCD") as LinkButton).Text = "Apply";
            }
            if (gvlist.DataKeys[rowIndex].Values["sod_ord_date"].ToString() == string.Empty)
            {
                // e.Row.Cells[5].Controls[0].Visible = true;
            }
        }
    }
  
}