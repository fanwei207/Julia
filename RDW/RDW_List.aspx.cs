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
using System.IO;
using RD_WorkFlow;

public partial class RDW_List : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropCatetory.DataSource = rdw.SelectProjectCategory(string.Empty);
            dropCatetory.DataBind();
            dropCatetory.Items.Insert(0, new ListItem("--", "0"));
           
            SKUHelper skuHelper = new SKUHelper();

            dropSKU.DataSource = skuHelper.Items(string.Empty);
            dropSKU.DataBind();

            dropSKU.Items.Insert(0, new ListItem("--", "--"));

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strProj = txtProject.Text.Trim();
        string strProd = txtProjectCode.Text.Trim();
        string strSku =  dropSKU.SelectedValue;
        string strStart = txtStartDate.Text.Trim();
        string strStatus = "PROCESS";
        string strUID = Convert.ToString(Session["uID"]);
        string qad=txtQad.Text.Trim();
        string cateid = dropCatetory.SelectedValue;

        //By Shanzm 2014-02-27 决定该人员是否能够查看全部项目。
        bool canViewAll = this.Security["170008"].isValid;

        switch (ddlStatus.SelectedValue.ToString())
        {
            case "0":  //show expired step
                gvRDW.DataSource = rdw.SelectRDWQueryList(cateid, strProj, strProd, strStart, strStatus, strUID, canViewAll, 0, qad,strSku);
                break;

            case "1": //Show current step
                gvRDW.DataSource = rdw.SelectRDWRptList(strProj, strProd, strStart, strStatus, strUID, canViewAll, qad);
                break;

            case "2": //Show all steps
                gvRDW.DataSource = rdw.SelectRDWQueryList(cateid, strProj, strProd, strStart, strStatus, strUID, canViewAll, 2, qad, strSku);
                break;

            case "3":  //Show your current steps
                gvRDW.DataSource = rdw.SelectRDWRptList(strProj, strProd, strStart, strStatus, strUID, false, qad);
                break;
            case "4": //show No Expired & no completed
                gvRDW.DataSource = rdw.SelectRDWQueryList(cateid, strProj, strProd, strStart, strStatus, strUID, canViewAll, 4, qad, strSku);
                break;
        }
        gvRDW.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRDW.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strMID = string.Empty;

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gvRDW.DataKeys[intRow][0].ToString();
            if (ddlStatus.SelectedValue == "3")
            {
                //Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + gvRDW.DataKeys[intRow][1].ToString() + "&fr=&st=3&rm=" + DateTime.Now.ToString(), true);
                Response.Redirect("/RDW/RDW_DetailList.aspx?mid=" + strMID + "&fr=quy&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("/RDW/RDW_DetailList.aspx?mid=" + strMID + "&fr=quy&rm=" + DateTime.Now.ToString(), true);
            }
        }

        if (e.CommandName.ToString() == "gobom")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gvRDW.DataKeys[intRow][0].ToString();
            //Response.Redirect("/RDW/RDW_app3.aspx?id=" + strMID + "&rm=" + DateTime.Now.ToString(), true);
            ltlAlert.Text = "var w=window.open('/RDW/RDW_doclist.aspx?mid=" + strMID + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			";
    
        }

    }

    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ccff99'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

                RDW_Header rh = (RDW_Header) e.Row.DataItem; 
                if (rh.RDW_DalayDays > 0 && string.IsNullOrEmpty(rh.RDW_FinishDate) && rh.RDW_isActive)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                }
            }
        }
        catch
        { }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        gvRDW.AllowPaging = false;
        gvRDW.Columns[7].Visible = false;
        gvRDW.Columns[8].Visible = false;
        gvRDW.Columns[11].Visible = true;
        gvRDW.Columns[12].Visible = true;
        BindData();
        gvRDW.Columns[8].Visible = false;
        gvRDW.Width = 1200;

        string style = @"<style> .text { mso-number-format:\@; word-break:keep-all; word-wrap:normal; }  </script> ";
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";  
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=Project.xls");
        //Response.ContentType = "application/excel";
        Response.ContentType = "application/vnd.xls";
        this.EnableViewState = false; 

        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        gvRDW.RenderControl(htw);

        // Style is added dynamically
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();

        gvRDW.AllowPaging = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRDW.PageIndex = 0;
        BindData();
    }

    /// <summary>
    /// 默认导出项目当前步骤，所有信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strProj = txtProject.Text.Trim();
        string strProd = txtProjectCode.Text.Trim();
        string strSku = dropSKU.SelectedValue;
        string strStart = txtStartDate.Text.Trim();
        string strStatus = "PROCESS";
        string strUID = Convert.ToString(Session["uID"]);
        string qad = txtQad.Text.Trim();
        string cateid = dropCatetory.SelectedValue;

        //By Shanzm 2014-02-27 决定该人员是否能够查看全部项目。
        bool canViewAll = this.Security["170008"].isValid;
        bool showAll = ddlStatus.SelectedValue == "2";

        //ltlAlert.Text = "window.open('RDW_ProjectSummaryExport.aspx?proj=" + strProj + "&projcode=" + strProd + "&date1=" + strStart + "&sku=" + strSku + "', '_blank');";

        string title = "<b>Project Category</b>~^150^<b>Project</b>~^120^<b>Project Code</b>~^200^<b>Project Desc</b>~^120^<b>Project Createddate</b>~^<b>Project CreatedName</b>~^"
            + "80^<b>Current TaskId</b>~^160^<b>Current StepName</b>~^<b>Step EndDate</b>~^<b>Step FinishDate</b>~^150^<b>Member</b>~^"
            + "200^<b>Step Description</b>~^<b>SKU#</b>~^<b>Product Category</b>~^<b>Lumens</b>~^<b>Voltage</b>~^<b>Wattage</b>~^<b>BeamAngle</b>~^<b>CCT</b>~^<b>CRT</b>~^<b>LPW</b>~^<b>Driver Type</b>~^"
            + "<b>STKorMTO</b>~^<b>UPC</b>~^<b>UL</b>~^<b>Sku Createtor</b>~^200^<b>Notes</b>~^80^<b>Expired</b>~^";
        DataTable dt =null;
        switch (ddlStatus.SelectedValue.ToString())
        {
            case "0":  //show expired step
                dt = rdw.SelectRDWQueryTable(cateid, strProj, strProd, strStart, strStatus, strUID, canViewAll, 0, qad, strSku);
                break;
            case "2": //Show all steps
                dt = rdw.SelectRDWQueryTable(cateid, strProj, strProd, strStart, strStatus, strUID, canViewAll, 2, qad, strSku);
                break;
            case "4": //show No Expired & no completed
                dt = rdw.SelectRDWQueryTable(cateid, strProj, strProd, strStart, strStatus, strUID, canViewAll, 4, qad, strSku);
                break;
        }
        ExportExcel(title, dt, false);
   
    }
   
}
