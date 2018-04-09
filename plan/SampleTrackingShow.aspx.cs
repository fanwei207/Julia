using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
public partial class EDI_SampleTrackingShow : BasePage
{
    private edi.OrderTracking helper = new edi.OrderTracking();
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
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string customer = txtCustomer.Text.Trim();
        //string status = ddlStatus.SelectedValue;
        DataTable dt = GetOrderTracking(po1, po2, orderDate1, orderDate2, customer, type);
        

        gvlist.DataSource = dt;
        gvlist.DataBind();
        txtTotal.Text = dt.Rows.Count.ToString();
    }

  
    private string connStr = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];

    public DataTable GetOrderTracking(string po1, string po2, string dateFrom, string dateTo,  string customer, string type)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@po1", po1);
        param[1] = new SqlParameter("@po2", po2);
        param[2] = new SqlParameter("@ordDate1", dateFrom);
        param[3] = new SqlParameter("@ordDate2", dateTo);
        param[4] = new SqlParameter("@region", string.Empty);
        param[5] = new SqlParameter("@customer", customer);
       
        param[6] = new SqlParameter("@type", type);
        return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectOrderTrackingSample", param).Tables[0];
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string type = dropType.SelectedValue;
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        
        string customer = txtCustomer.Text.Trim();
        //string status = ddlStatus.SelectedValue;
        DataTable dt = GetOrderTracking(po1, po2, orderDate1, orderDate2, customer, type);

        string EXTitle = "<b>Order#</b>~^<b>Order Date</b>~^<b>Customer Code</b>~^<b>Line</b>~^<b>Item</b>~^<b>QAD Part</b>~^<b>Order Qty</b>~^<b>Request Date</b>~^<b>Wo nbr</b>~^<b>Wo lot</b>~^<b>Wo Qty</b>~^<b>Complete Qty</b>~^<b>Product Line</b>~^<b>Site</b>~^<b>Release Date</b>~^<b>Online Date</b>~^<b>Offline Date</b>~^<b>PCD</b>~^<b>PCD创建人</b>~^<b>PCD创建日期</b>~^<b>分类</b>~^<b>PCD备注</b>~^";
       // DataSet ds = getEdiData.getExcelData(txtDate.Text.Trim(), Session["PlantCode"].ToString());
        this.ExportExcel(EXTitle, dt, false);
    }
    protected void btnExportWo_Click(object sender, EventArgs e)
    {

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
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
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = e.Row.RowIndex;
            if (gvlist.DataKeys[rowIndex].Values["wo_nbr"].ToString() != string.Empty)
            {
                e.Row.Cells[5].Controls[0].Visible = false;
            }
            string planDate = gvlist.DataKeys[rowIndex].Values["planDate"].ToString();
            if (planDate == "")
            {
                (e.Row.Cells[17].FindControl("linkPCD") as LinkButton).Text = "Apply";
            }
           
        }
    }
}