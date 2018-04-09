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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

public partial class EDI_FIFOshowALL : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected void BindGridView()
    {
        try
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@CustomerPO", txtSOPO.Text.Trim());
            parm[1] = new SqlParameter("@QADSalesOrder", txtSO.Text.Trim());
            parm[2] = new SqlParameter("@InvoiceNumber", txtINV.Text.Trim());
            parm[3] = new SqlParameter("@Type", ddltype.SelectedValue.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_selectFIFOAll", parm);
            gvlist.DataSource = ds;
            gvlist.DataBind();
        }
        catch (Exception ee)
        { ;}
    }
    protected void gvlist_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    
    protected void btnquery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btnimport_Click(object sender, EventArgs e)
    {
        DataTable dt;
         try
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@CustomerPO", txtSOPO.Text.Trim());
            parm[1] = new SqlParameter("@QADSalesOrder", txtSO.Text.Trim());
            parm[2] = new SqlParameter("@InvoiceNumber", txtINV.Text.Trim());
            parm[3] = new SqlParameter("@Type", ddltype.SelectedValue.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_selectFIFOAll", parm);
            dt = ds.Tables[0];
            string title = "<b>SOPO</b>~^<b>CUST</b>~^<b>SO</b>~^<b>LINE</b>~^<b>WO</b>~^200^<b>QAD</b>~^200^<b>CUSTITEM</b>~^200^<b>QTY</b>~^200^<b>SHIPDATE</b>~^<b>AluminumPCBCostRMB</b>~^<b>ConnectorCostRMB</b>~^<b>DriverCostRMB</b>~^<b>HeatSinkCostRMB</b>~^<b>LampBaseCostRMB</b>~^<b>LampShadeCostRMB</b>~^<b>LEDChipCostRMB</b>~^<b>OtherCostRMB</b>~^<b>PackageCostRMB</b>~^<b>PlasticCostRMB</b>~^<b>MaterialCostRMB</b>~^<b>LaborCostRMB</b>~^<b>OVERHEAD(RMB)</b>~^<b>Inv Price</b>~^<b>INV NO</b>~^";
            this.ExportExcel(title, dt, true);
        }
        catch (Exception ee)
        { ;}

        
    }
}