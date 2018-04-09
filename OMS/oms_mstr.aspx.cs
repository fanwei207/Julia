using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.IO;
using System.Threading;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Collections.Generic;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.OleDb;
using System.IO;

public partial class IT_oms_mstr : BasePage
{
    public string _fpath
    {
        get
        {
            return ViewState["fpath"].ToString();
        }
        set
        {
            ViewState["fpath"] = value;
        }
    }
    public string _fname
    {
        get
        {
            return ViewState["fname"].ToString();
        }
        set
        {
            ViewState["fname"] = value;
        }
    }
    public string _parentID
    {
        get
        {
            return ViewState["parentID"].ToString();
        }
        set
        {
            ViewState["parentID"] = value;
        }
    }

    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            _parentID = "0";
            _parentID = Request.QueryString["parentID"] == null ? "0" : Request.QueryString["parentID"];
            try
            {
                hidTabIndex.Value = Request.QueryString["index"];
            }
            catch (Exception)
            {

            }
            try
            {
                string reply = Request.QueryString["reply"];
                if (reply.Trim() == "1")
                {
                    gvMessage.Visible = false;
                    gvMessagereply.Visible = true;
                    btn_back.Visible = true;
                    btn_reply.Visible = true;
                    _parentID = Request.QueryString["parentID"];
                }
            }
            catch (Exception)
            {

            }
          

            string custCode = Request.QueryString["custCode"];
            int flag = custCode.LastIndexOf(",");
            custCode = custCode.Substring(flag + 1);
            lbCustCode.Text = Request.QueryString["custCode"];
            lbCustName.Text = Request.QueryString["custName"];
            txtCustomer.Text = Request.QueryString["custCode"];
            txtCustomer.Enabled = false;
            BindProduct();
            BindMessage();
            BindMessagereply();
            OTBindData();
            BindFSCategory();
            BindFSDomain();
            BindFactoryStatus();
            BindGvForecast();
            BindRegionData();
            //_parentID = "0";

        }
    }
    #region Factory Status
    protected void BindFactoryStatus()
    {
        gvFactoryStatus.DataSource = OMSHelper.GetFactoryStatus(lbCustCode.Text, Convert.ToInt32(ddlCategory.SelectedValue), txtFilename.Text.Trim(), Convert.ToInt32(ddlDomain.SelectedValue), Convert.ToInt32(ddlImpt.SelectedValue));
        gvFactoryStatus.DataBind();
    }
    protected void BindFSCategory()
    {
        DataTable ds = OMSHelper.GetFSCategory("", "");
        ddlCategory.DataSource = ds;
        ddlCategory.DataValueField = "id";
        ddlCategory.DataTextField = "tp";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("--All--", "0"));
    }
    protected void BindFSDomain()
    {
        ddlDomain.Items.Insert(0, new ListItem("--All--", "0"));
        ddlDomain.Items.Insert(1, new ListItem("SQL", "1"));
        ddlDomain.Items.Insert(2, new ListItem("ZQL", "2"));
        ddlDomain.Items.Insert(3, new ListItem("YQL", "5"));
        ddlDomain.Items.Insert(4, new ListItem("HQL", "8"));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("oms_cust.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void btnSearchFS_Click(object sender, EventArgs e)
    {
        BindFactoryStatus();
    }
    protected void gvFactoryStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hidTabIndex.Value = "0";

        if (e.CommandName == "Download")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gvFactoryStatus.DataKeys[index].Values["fsd_id"].ToString());
            string filePath = OMSHelper.GetFSDocumentPath(fsId);

            if (!File.Exists(filePath))
            {
                ltlAlert.Text = "alert('the source file does not exist！')";

                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
            BindFactoryStatus();
        }
        if (e.CommandName == "DeleteFile")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gvFactoryStatus.DataKeys[index].Values["fsd_id"].ToString());
            string filePath = OMSHelper.GetFSDocumentPath(fsId);
            if (!OMSHelper.DeleteFSDocument(fsId))
            {
                ltlAlert.Text = "alert('Failed to delete！')";

                return;
            }
            else
            {
                if (File.Exists(filePath))
                {
                    //如存在则删除
                    File.Delete(filePath);
                    //删除空文件夹
                    int folderIndex = filePath.LastIndexOf("\\");
                    string folderString1 = filePath.Substring(0, folderIndex);
                    if (Directory.GetDirectories(folderString1).Length == 0 && Directory.GetFiles(folderString1).Length == 0)
                    {
                        Directory.Delete(folderString1);
                        int folderIndex2 = folderString1.LastIndexOf("\\");
                        string folderString2 = folderString1.Substring(0, folderIndex2);
                        if (Directory.GetDirectories(folderString2).Length == 0)
                            Directory.Delete(folderString2);
                    }
                }
                ltlAlert.Text = "alert('Successfully deleted！')";

            }
            BindFactoryStatus();
        }
    }
    protected void gvFactoryStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        hidTabIndex.Value = "0";

        gvFactoryStatus.PageIndex = e.NewPageIndex;
        BindFactoryStatus();
    }
    #endregion

    #region Forecast
    protected void BindGvForecast()
    {
        DateTime dt;
        string date = txtFcYear.Text.Trim() + "-" + txtMonth.Text.Trim() + "-1";
        if (txtFcYear.Text.Trim() == string.Empty && txtMonth.Text.Trim() == string.Empty)
            dt = Convert.ToDateTime("2000/1/1 00:00:00");
        else
            dt = Convert.ToDateTime(date);
        gvForecast.DataSource = OMSHelper.GetForecast(lbCustCode.Text, txtFCPart.Text.Trim(), dt);
        gvForecast.DataBind();
    }
    protected void gvForecast_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvForecast.PageIndex = e.NewPageIndex;
        hidTabIndex.Value = "2";
        BindGvForecast();
    }
    protected void btnFcQuery_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        DateTime dt;
        string date = txtFcYear.Text.Trim() + "-" + txtMonth.Text.Trim() + "-1";
        if (txtFcYear.Text.Trim() == string.Empty && txtMonth.Text.Trim() == string.Empty)
        {
            dt = Convert.ToDateTime("2000/1/1");
        }
        else
        {
            try
            {
                dt = Convert.ToDateTime(date);
            }
            catch
            {
                this.Alert("Invaild Datetime,try again");
                return;
            }
        }
        BindGvForecast();
    }
    #endregion

    #region Project Tracking
    protected void BindMessage()
    {
        string keywords = txtkeywords.Text.Trim();
        keywords = keywords.Replace("*", "%");
        gvMessage.DataSource = OMSHelper.SelectTaskGVMessage(Request.QueryString["custCode"], keywords, ralStatus.SelectedValue, "");
        gvMessage.DataBind();
        gvMessage.Attributes.Add("style", "word-break:break-all;word-wrap:break_word");
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("oms_cust.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    //}
    //protected void btnUpLoad_Click(object sender, EventArgs e)
    //{

    //}
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvMessage.PageIndex = e.NewPageIndex;
        BindMessage();
    }

    protected void gvMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hidTabIndex.Value = "4";

        if (e.CommandName == "DownloadFile")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gvMessage.DataKeys[index].Values["fst_id"].ToString());
            string filePath = gvMessage.DataKeys[index].Values["fst_filepath"].ToString();

            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";



            BindMessage();
        }
        if (e.CommandName == "reply")
        {
            //_mstr = ((LinkButton)e.CommandSource).Text;
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            _parentID = gvMessage.DataKeys[index].Values["fst_id"].ToString();
            string Uid = gvMessage.DataKeys[index].Values["fst_createBy"].ToString();
            string closed = gvMessage.DataKeys[index].Values["fst_IsClosed"].ToString().Trim();
            gvMessagereply.Visible = true;
            gvMessage.Visible = false;
            BindMessagereply();
            btn_back.Visible = true;
            btn_reply.Visible = true;
            ralStatus.Visible = false;
            if (Session["uID"].ToString() == Uid)
            {
                // btn_close.Visible = true;
            }
            if (closed == "True")
            {

                btn_reply.Visible = false;
            }
        }
        if (e.CommandName == "close")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            _parentID = gvMessage.DataKeys[index].Values["fst_id"].ToString();
            string Uid = gvMessage.DataKeys[index].Values["fst_createBy"].ToString();
            if (Session["uID"].ToString() == Uid)
            {
                closemessage();
            }

        }
    }

    protected void BindMessagereply()
    {
        string keywords = txtkeywords.Text.Trim();
        keywords = keywords.Replace("*", "%");
        gvMessagereply.DataSource = OMSHelper.SelectTaskGVMessage(_parentID, Request.QueryString["custCode"], keywords);
        //gvMessagereply.Columns[1].HeaderText = _mstr;

        gvMessagereply.DataBind();
       

        //((Label)gvMessagereply.Rows[0].FindControl("Label1")).Text=((Label)gvMessagereply.Rows[0].FindControl("Label1")).Text.Replace("|", "<br>");
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        gvMessage.Visible = true;
        gvMessagereply.Visible = false;
        btn_reply.Visible = false;
        btn_back.Visible = false;

        ralStatus.Visible = true;
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        //Response.Redirect("oms_reply.aspx?parentID=" + _parentID + "&custCode=" + lbCustCode.Text + "&custName=" + lbCustName.Text + "&custCode=" + txtCustomer.Text + "&rt=" + DateTime.Now.ToFileTime().ToString());
        ltlAlert.Text = "window.showModalDialog('oms_reply.aspx?parentID=" + _parentID + "&type=" + "new" + "&custName=" + lbCustName.Text + "&custCode=" + txtCustomer.Text + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'oms_mstr.aspx?custCode=" + lbCustCode.Text + "&custName=" + lbCustName.Text + "&index=" + 4 + " &rt=" + DateTime.Now.ToFileTime().ToString() + "'";

    }
    protected void gvMessagereply_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadFile")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gvMessagereply.DataKeys[index].Values["fst_id"].ToString());
            string filePath = gvMessagereply.DataKeys[index].Values["fst_filepath"].ToString();

            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";



            BindMessagereply();
        }
    }
    protected void btn_reply_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.showModalDialog('oms_reply.aspx?parentID=" + _parentID + "&type= " + "reply" + " &custName=" + lbCustName.Text + "&custCode=" + txtCustomer.Text + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'oms_mstr.aspx?parentID=" + _parentID + "&custCode=" + lbCustCode.Text + "&custName=" + lbCustName.Text + "&index=" + 4 + "&reply=" + 1 + " &rt=" + DateTime.Now.ToFileTime().ToString() + "'";

    }
    protected void gvMessagereply_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvMessagereply.PageIndex = e.NewPageIndex;
        BindMessagereply();
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        if (OMSHelper.closeTaskMessage(_parentID) == true)
        {
            ltlAlert.Text = "alert('主题关闭成功！')";
            gvMessage.Visible = true;
            BindMessage();
            gvMessagereply.Visible = false;

            btn_reply.Visible = false;
            btnBack.Visible = false;
        }
        else
        {
            ltlAlert.Text = "alert('主题关闭失败！')";
        }
        hidTabIndex.Value = "4";
        //string Uid = gvMessagereply.DataKeys[0].Values["fst_createBy"].ToString();
        //if (Session["uID"].ToString() == Uid)
        //{
        //    btn_close.Visible = true;
        //}
    }
    protected void closemessage()
    {
        if (OMSHelper.closeTaskMessage(_parentID) == true)
        {
            ltlAlert.Text = "alert('主题关闭成功！')";
            gvMessage.Visible = true;
            BindMessage();
            gvMessagereply.Visible = false;

            btn_reply.Visible = false;
            btnBack.Visible = false;
        }
        else
        {
            ltlAlert.Text = "alert('主题关闭失败！')";
        }
        hidTabIndex.Value = "4";
    }
    protected void btn_messageselect_Click(object sender, EventArgs e)
    {
        BindMessage();
        BindMessagereply();
        txtkeywords.Text = string.Empty;
        hidTabIndex.Value = "4";
    }
    protected void gvMessage_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            string closed = gvMessage.DataKeys[index].Values["fst_IsClosed"].ToString().Trim();

            string Uid = gvMessage.DataKeys[index].Values["fst_createBy"].ToString().Trim();
            //e.Row.Cells[3].Enabled = false;
            //LinkButton close = ((LinkButton)e.Row.FindControl("close"));
            if (closed == "True")
            {
                //e.Row.Cells[3].Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Font.Underline = false;
                // e.Row.Cells[3].BackColor = System.Drawing.Color.RoyalBlue;
                return;

            }
            else if (Session["uID"].ToString() != Uid)
            {
                e.Row.Cells[3].Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Enabled = false;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Text = string.Empty;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).Font.Underline = false;
                return;
            }
            else
            {
                e.Row.Cells[3].Enabled = true;
                ((LinkButton)e.Row.Cells[3].FindControl("close")).ForeColor = System.Drawing.Color.Blue;
                return;
            }

        }
    }
    #endregion

    #region order Tracking
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "3";
        OTBindData();
    }

    private void OTBindData()
    {
        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        string status = ddlStatus.SelectedValue;
        DataTable dt = OMSHelper.GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, status);
        gvlist.DataSource = dt;
        gvlist.DataBind();
        txtTotal.Text = dt.Rows.Count.ToString();
    }

    private void BindRegionData()
    {
        ddlRegion.DataSource = OMSHelper.GetRegions();
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("--", ""));
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        OTBindData();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "4";

        string po1 = txtPo1.Text.Trim();
        string po2 = txtPo2.Text.Trim();
        string orderDate1 = txtOrdDate1.Text.Trim();
        string orderDate2 = txtOrdDate2.Text.Trim();
        string region = ddlRegion.SelectedValue;
        string customer = txtCustomer.Text.Trim();
        string status = ddlStatus.SelectedValue;
        DataTable dt = OMSHelper.GetOrderTracking(po1, po2, orderDate1, orderDate2, region, customer, status);
        string title = "<b>Order#</b>~^<b>Order Date</b>~^<b>Region</b>~^<b>Customer Code</b>~^200^<b>Customer</b>~^200^<b>Item</b>~^200^<b>Order Question</b>~^<b>Load QAD Date</b>~^<b>Qad So#</b>~^50^<b>Line</b>~^110^<b>QAD Part</b>~^<b>Order Qty</b>~^<b>Request Date</b>~^<b>Wo Qty</b>~^<b>Online Qty</b>~^<b>Ship Qty</b>~^<b>Inspection Date</b>~^<b>Ship Date</b>~^<b>PCD</b>~^";
        this.ExportExcel(title, dt, false);

    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = e.Row.RowIndex;
            if (gvlist.DataKeys[rowIndex].Values["que"].ToString() == "0")
            {
                e.Row.Cells[4].Controls[0].Visible = false;
            }
            //else
            //{
            //    for (int i = 5; i <= 15; i++)
            //    {
            //        e.Row.Cells[i].Text = "";
            //    }
            //}
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "3";

        OMSHelper.RefreshOrderTracking();
        OTBindData();
    }

    #region 导出Excel通用方法
    /// <summary>
    /// 导出Excel通用方法（数据源是DataTable）ExData的列数一定要大于或等于ExTitle里的标题列数。如果等于则从ExData从第一列开始写入Excel,如果大于则取后半部分和标题列数相等的部分写入Excel。
    /// </summary>
    /// <param name="dsnx">chk.dsn0()或chk.dsnx()</param>
    /// <param name="EXTitle">格式如：<b>工号</b>~^<b>姓名</b>~^</param>
    /// <param name="EXSQL"></param>
    /// <param name="fullDateFormat">日期格式：yyyy-MM-dd HH:mm:ss还是yyyy-MM-dd</param>
    public void ExportExcel(string EXTitle, DataTable EXData, bool fullDateFormat)
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

        IList<ExcelTitle> ItemList = GetExcelTitles(EXTitle);
        int total = ItemList.Count;

        DataTable dt = EXData;

        //头栏样式
        ICellStyle styleHeader = SetHeaderStyle(workbook);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(0);
        SetColumnTitleAndStyle(workbook, sheet, ItemList, dt, styleHeader, rowHeader);

        //写明细数据
        SetDetailsValue(sheet, total, dt, 1, fullDateFormat);

        dt.Reset();

        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");
    }
    private void SetDetailsValue(ISheet sheet, int total, DataTable dt, int startRowIndex, bool fullDateFormat = false)
    {
        if (dt.Columns.Count >= total)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + startRowIndex);

                for (int j = 1; j <= total; j++)
                {
                    ICell cell = row.CreateCell(j - 1);
                    int _col1 = j - 1 + (dt.Columns.Count - total);
                    cell.SetCellValue(dt.Rows[i][_col1], fullDateFormat);
                }
            }
        }
    }

    private void SetColumnTitleAndStyle(IWorkbook workbook, ISheet sheet, IList<ExcelTitle> ItemList, DataTable dt, ICellStyle styleHeader, IRow rowHeader)
    {
        int total = ItemList.Count;
        foreach (ExcelTitle item in ItemList)
        {
            int titleIndex = ItemList.IndexOf(item);
            sheet.SetColumnWidth(titleIndex, item.Width);

            ICell cell = rowHeader.CreateCell(titleIndex);
            cell.CellStyle = styleHeader;
            cell.SetCellValue(item.Name);

            int dtCol = 0;

            if (dt.Columns.Count == total)
            {
                dtCol = titleIndex;
            }
            else
            {
                dtCol = titleIndex + (dt.Columns.Count - total);
            }

            ICellStyle columnStyle = SetColumnStyleByDataType(workbook, dt.Columns[dtCol].DataType.ToString());
            sheet.SetDefaultColumnStyle(titleIndex, columnStyle);
        }
    }
    private ICellStyle SetColumnStyleByDataType(IWorkbook workbook, string dataType)
    {
        ICellStyle style = workbook.CreateCellStyle();
        IFont font = workbook.CreateFont();

        switch (dataType)
        {
            case "System.DateTime":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.Int16":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int32":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int64":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Decimal":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Double":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Boolean":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.String":
                style.Alignment = HorizontalAlignment.Left;
                style.WrapText = true;
                break;
        }
        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        font.FontHeightInPoints = 9;
        style.SetFont(font);
        return style;
    }

    private ICellStyle SetHeaderStyle(IWorkbook workbook)
    {
        ICellStyle styleHeader = workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        styleHeader.SetFont(fontHeader);

        return styleHeader;
    }
    /// <summary>
    /// Excel标题定义：250^<b>产品型号</b>~^200^<b>产品简称</b>~^
    /// </summary>
    private class ExcelTitle
    {
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private int _width;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public ExcelTitle(string name, int width)
        {
            this.Name = name;
            this.Width = width;
        }

        public ExcelTitle()
        {

        }
    }


    private IList<ExcelTitle> GetExcelTitles(string EXTitle)
    {
        var ItemList = new List<ExcelTitle>();

        string str = EXTitle;
        int total = 0;
        int ind = 0;
        while (str.Length > 0)
        {
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                total = total + 1;
                str = "";
                break;
            }
            total = total + 1;
            str = str.Substring(ind + 2);
        }

        str = EXTitle;

        for (int i = 0; i <= total - 1; i++)
        {
            ExcelTitle item = new ExcelTitle();
            int width = 100 * 6000 / 164;
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                ind = str.IndexOf("L~");
                if (ind > -1)
                {
                    str = str.Substring(2);
                }

                ind = str.IndexOf("^");
                if (ind == -1)
                {
                    item.Name = str.Substring(2);
                    item.Width = width;
                }
                else
                {
                    item.Name = str.Substring(ind + 1);
                    item.Width = Convert.ToInt32(str.Substring(0, ind)) * 6000 / 164;
                }
                str = "";
                break;
            }
            else
            {
                item.Name = str.Substring(0, ind);
                item.Width = width;
                str = str.Substring(ind + 2);

                ind = item.Name.IndexOf("L~");
                if (ind > -1)
                {
                    item.Name = item.Name.Substring(2);
                }

                ind = item.Name.IndexOf("^");
                if (ind > -1)
                {
                    item.Width = Convert.ToInt32(item.Name.Substring(0, ind)) * 6000 / 164;
                    item.Name = item.Name.Substring(ind + 1);
                }
            }

            item.Name = item.Name.Replace("<b>", "").Replace("</b>", "");
            ItemList.Add(item);
        }
        return ItemList;
    }
    #endregion


    #endregion

    #region Products
    protected void BindProduct()
    {
        string custCode = lbCustCode.Text;
        string custPart = txtCustPart.Text.Trim();
        string part = txtPart.Text.Trim();

        gvProduct.DataSource = OMSHelper.GetProducts(custCode, custPart, part);
        gvProduct.DataBind();
    }

    protected void gvProduct_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        hidTabIndex.Value = "1";
        gvProduct.PageIndex = e.NewPageIndex;
        BindProduct();
    }

    protected void gvProduct_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        hidTabIndex.Value = "1";
        //定义参数
        string strMID = string.Empty;

        if (e.CommandName.ToString() == "gobom")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            Response.Redirect("/QadDoc/qad_bomviewdoc.aspx?cmd=omsproduct&custCode=" + lbCustCode.Text + "&custName=" + lbCustName.Text + "&part=" + gvProduct.DataKeys[index].Values["cp_part"].ToString());
        }
        else if (e.CommandName.ToString() == "desc")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            Response.Redirect("/OMS/oms_productDescDetail.aspx?cmd=omsproduct&custCode=" + lbCustCode.Text + "&custName=" + lbCustName.Text + "&no=" + gvProduct.DataKeys[index].Values["cp_cust_part"].ToString());
        }
    }

    protected void btnSearcheProduct_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "1";
        BindProduct();
    }
    #endregion

    protected void btnBack_Click1(object sender, EventArgs e)
    {
        Response.Redirect("oms_cust.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}