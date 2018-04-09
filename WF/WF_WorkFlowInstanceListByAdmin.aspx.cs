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

public partial class PM_HeaderList : BasePage
{
    WorkFlow wf = new WorkFlow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadWorkFlowTemplate();
            BindData();
        }
    }

    protected void loadWorkFlowTemplate()
    {
        ddlWorkFlow.DataSource = wf.GetWorkFlowTemplateByDomain(Convert.ToInt32(Session["plantCode"]));
        ddlWorkFlow.DataBind();
        ddlWorkFlow.Items.Insert(0, new ListItem("--", "0"));
    }

    protected void BindData()
    {
        //定义参数
        string wfnNbr = txtNbr.Text.Trim();
        int flowID = Convert.ToInt32(ddlWorkFlow.SelectedValue);
        string reqDate1 = txtReqDate.Text.Trim();
        string uName = txtCreateBy.Text.Trim();
        int status = Convert.ToInt32(ddlStatus.SelectedValue);
        int domain = Convert.ToInt32(ddlDomain.SelectedValue);

        gvWFI.DataSource = wf.GetWorkFlowInstanceCreatedByAdmin(wfnNbr, flowID, reqDate1, uName, status, domain);
        gvWFI.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWFI_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvWFI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWFI.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtReqDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }
        BindData();
    }

    protected void gvWFI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            ltlAlert.Text = "window.open('/WF/WF_FormEdit.aspx?nbr=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now;
            ltlAlert.Text += "','','menubar=no,scrollbars=no,resizable=no,width=900,height=530,top=0,left=0');";
        }
        if (e.CommandName == "Detail")
        {
            Response.Redirect("WF_WorkFlowInstanceListDetailByAdmin.aspx?nbr=" + e.CommandArgument.ToString().Trim() + "&tp=" + "&rm=" + DateTime.Now);
        }
    }

    protected void gvWFI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
        doc.FileName = "工作流申请记录报表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string SheetName = string.Empty;

        //Sheet1内容
        SheetName = "excel";
        AppLibrary.WriteExcel.Worksheet sheet1 = doc.Workbook.Worksheets.Add(SheetName);
        AppLibrary.WriteExcel.Cells cells1 = sheet1.Cells;


        #region//样式1白底
        AppLibrary.WriteExcel.XF XFstyle1 = doc.NewXF();
        XFstyle1.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        XFstyle1.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        XFstyle1.Font.FontName = "宋体";
        XFstyle1.UseMisc = true;
        XFstyle1.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;
        XFstyle1.Font.Bold = false;

        //边框线
        XFstyle1.BottomLineStyle = 1;
        XFstyle1.LeftLineStyle = 1;
        XFstyle1.TopLineStyle = 1;
        XFstyle1.RightLineStyle = 1;

        XFstyle1.UseBorder = true;
        XFstyle1.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
        XFstyle1.PatternColor = AppLibrary.WriteExcel.Colors.White;
        XFstyle1.Pattern = 1;
        #endregion

        #region//样式2黄底
        AppLibrary.WriteExcel.XF XFstyle2 = doc.NewXF();
        XFstyle2.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        XFstyle2.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        XFstyle2.Font.FontName = "宋体";
        XFstyle2.UseMisc = true;
        XFstyle2.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;
        XFstyle2.Font.Bold = false;

        //边框线
        XFstyle2.BottomLineStyle = 1;
        XFstyle2.LeftLineStyle = 1;
        XFstyle2.TopLineStyle = 1;
        XFstyle2.RightLineStyle = 1;

        XFstyle2.UseBorder = true;
        XFstyle2.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
        XFstyle2.PatternColor = AppLibrary.WriteExcel.Colors.Yellow;
        XFstyle2.Pattern = 1;
        #endregion

        #region//样式3蓝底
        AppLibrary.WriteExcel.XF XFstyle3 = doc.NewXF();
        XFstyle3.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        XFstyle3.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        XFstyle3.Font.FontName = "宋体";
        XFstyle3.UseMisc = true;
        XFstyle3.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;
        XFstyle3.Font.Bold = false;

        //边框线
        XFstyle3.BottomLineStyle = 1;
        XFstyle3.LeftLineStyle = 1;
        XFstyle3.TopLineStyle = 1;
        XFstyle3.RightLineStyle = 1;

        XFstyle3.UseBorder = true;
        XFstyle3.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
        XFstyle3.PatternColor = AppLibrary.WriteExcel.Colors.Blue;
        XFstyle3.Pattern = 1;
        #endregion

        #region//样式4红底
        AppLibrary.WriteExcel.XF XFstyle4 = doc.NewXF();
        XFstyle4.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        XFstyle4.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        XFstyle4.Font.FontName = "宋体";
        XFstyle4.UseMisc = true;
        XFstyle4.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;
        XFstyle4.Font.Bold = false;

        //边框线
        XFstyle4.BottomLineStyle = 1;
        XFstyle4.LeftLineStyle = 1;
        XFstyle4.TopLineStyle = 1;
        XFstyle4.RightLineStyle = 1;

        XFstyle4.UseBorder = true;
        XFstyle4.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
        XFstyle4.PatternColor = AppLibrary.WriteExcel.Colors.Red;
        XFstyle4.Pattern = 1;
        #endregion

        #region//Sheet1列宽控制
        //域
        AppLibrary.WriteExcel.ColumnInfo column1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column1.ColumnIndexStart = 0;
        column1.ColumnIndexEnd = 0;
        column1.Width = 50 * 6000 / 164;
        sheet1.AddColumnInfo(column1);

        //申请号
        AppLibrary.WriteExcel.ColumnInfo column2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column2.ColumnIndexStart = 1;
        column2.ColumnIndexStart = 1;
        column2.Width = 80 * 6000 / 164;
        sheet1.AddColumnInfo(column2);

        //流程模板
        AppLibrary.WriteExcel.ColumnInfo column3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column3.ColumnIndexStart = 2;
        column3.ColumnIndexStart = 2;
        column3.Width = 150 * 6000 / 164;
        sheet1.AddColumnInfo(column3);

        //申请日期
        AppLibrary.WriteExcel.ColumnInfo column4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column4.ColumnIndexStart = 3;
        column4.ColumnIndexStart = 3;
        column4.Width = 80 * 6000 / 164;
        sheet1.AddColumnInfo(column4);

        //截止日期
        AppLibrary.WriteExcel.ColumnInfo column5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column5.ColumnIndexStart = 4;
        column5.ColumnIndexStart = 4;
        column5.Width = 80 * 6000 / 164;
        sheet1.AddColumnInfo(column5);

        //申请人
        AppLibrary.WriteExcel.ColumnInfo column6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column6.ColumnIndexStart = 5;
        column6.ColumnIndexStart = 5;
        column6.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column6);

        //操作日期
        AppLibrary.WriteExcel.ColumnInfo column7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column7.ColumnIndexStart = 6;
        column7.ColumnIndexStart = 6;
        column7.Width = 80 * 6000 / 164;
        sheet1.AddColumnInfo(column7);

        //状态
        AppLibrary.WriteExcel.ColumnInfo column8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column8.ColumnIndexStart = 7;
        column8.ColumnIndexStart = 7;
        column8.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column8);

        //步骤序号
        AppLibrary.WriteExcel.ColumnInfo column9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column9.ColumnIndexStart = 8;
        column9.ColumnIndexStart = 8;
        column9.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column9);

        //步骤名称
        AppLibrary.WriteExcel.ColumnInfo column10 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column10.ColumnIndexStart = 9;
        column10.ColumnIndexStart = 9;
        column10.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column10);

        //步骤状态
        AppLibrary.WriteExcel.ColumnInfo column11 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column11.ColumnIndexStart = 10;
        column11.ColumnIndexStart = 10;
        column11.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column11);

        //步骤操作人
        AppLibrary.WriteExcel.ColumnInfo column12 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column12.ColumnIndexStart = 11;
        column12.ColumnIndexStart = 11;
        column12.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column12);

        //步骤操作时间
        AppLibrary.WriteExcel.ColumnInfo column13 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column13.ColumnIndexStart = 12;
        column13.ColumnIndexStart = 12;
        column13.Width = 135 * 6000 / 164;
        sheet1.AddColumnInfo(column13);

        //步骤备注
        AppLibrary.WriteExcel.ColumnInfo column14 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column14.ColumnIndexStart = 13;
        column14.ColumnIndexStart = 13;
        column14.Width = 300 * 6000 / 164;
        sheet1.AddColumnInfo(column14);
        #endregion

        #region//标题
        cells1.Add(1, 1, "域", XFstyle1);
        cells1.Add(1, 2, "申请号", XFstyle1);
        cells1.Add(1, 3, "流程模板", XFstyle1);
        cells1.Add(1, 4, "申请日期", XFstyle1);
        cells1.Add(1, 5, "截止日期", XFstyle1);
        cells1.Add(1, 6, "创建人", XFstyle1);
        cells1.Add(1, 7, "创建日期", XFstyle1);
        cells1.Add(1, 8, "状态", XFstyle1);
        cells1.Add(1, 9, "步骤序号", XFstyle1);
        cells1.Add(1, 10, "步骤名称", XFstyle1);
        cells1.Add(1, 11, "状态", XFstyle1);
        cells1.Add(1, 12, "操作人", XFstyle1);
        cells1.Add(1, 13, "操作时间", XFstyle1);
        cells1.Add(1, 14, "步骤备注", XFstyle1);
        #endregion

        string wfnNbr = txtNbr.Text.Trim();
        int flowID = Convert.ToInt32(ddlWorkFlow.SelectedValue);
        string reqDate1 = txtReqDate.Text.Trim();
        string uName = txtCreateBy.Text.Trim();
        int status = Convert.ToInt32(ddlStatus.SelectedValue);
        int domain = Convert.ToInt32(ddlDomain.SelectedValue);

        DataTable dt = null;
        dt = wf.GetWorkFlowInstanceDetailCreatedByAdmin(wfnNbr, flowID, reqDate1, uName, status, domain);

        int i = 1;
        for (int n = 0; n < dt.Rows.Count; n++)
        {
            i++;
            cells1.Add(i, 1, dt.Rows[n]["WFN_Domain"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 2, dt.Rows[n]["WFN_Nbr"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 3, dt.Rows[n]["Flow_Name"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 4, dt.Rows[n]["WFN_ReqDate"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 5, dt.Rows[n]["WFN_DueDate"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 6, dt.Rows[n]["WFN_CreatedBy"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 7, dt.Rows[n]["WFN_CreatedDate"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 8, dt.Rows[n]["WFN_Status"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 9, dt.Rows[n]["Sort_Order"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 10, dt.Rows[n]["Node_Name"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 11, dt.Rows[n]["FNI_Status"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 12, dt.Rows[n]["FNI_RunUName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 13, dt.Rows[n].IsNull("FNI_RunDate") == true ? "" : dt.Rows[n]["FNI_RunDate"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 14, dt.Rows[n]["FNI_Remark"].ToString().Trim(), XFstyle1);
        }

        dt.Reset();

        GroupRows(sheet1, 1, 1, 2);
        GroupRows(sheet1, 2, 1, 2);
        GroupRows(sheet1, 3, 1, 2);
        GroupRows(sheet1, 4, 1, 2);
        GroupRows(sheet1, 5, 1, 2);
        GroupRows(sheet1, 6, 1, 2);
        GroupRows(sheet1, 7, 1, 2);
        GroupRows(sheet1, 8, 1, 2);

        doc.Save(Server.MapPath("/Excel/"), true);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + doc.FileName + "','_blank');</script>");
    }

    private void GroupRows(AppLibrary.WriteExcel.Worksheet Sheet, int CellSpan, int CellCondition1, int CellCondition2)
    {
        int rowSpanNum = 0;
        AppLibrary.WriteExcel.Cells cells = Sheet.Cells;
        for (int i = 2; i <= Sheet.Rows.Count; i = i + rowSpanNum)
        {
            rowSpanNum = 0;
            AppLibrary.WriteExcel.Row row = Sheet.Rows[(ushort)i];
            for (int j = i + 1; j <= Sheet.Rows.Count; j++)
            {
                AppLibrary.WriteExcel.Row rowNext = Sheet.Rows[(ushort)j];
                if
                (
                    row.CellAtCol((ushort)CellCondition1).Value.ToString() == rowNext.CellAtCol((ushort)CellCondition1).Value.ToString()
                    &&
                    row.CellAtCol((ushort)CellCondition2).Value.ToString() == rowNext.CellAtCol((ushort)CellCondition2).Value.ToString()
                )
                {
                    rowSpanNum++;
                }
                else
                {
                    break;
                }
            }

            if (rowSpanNum != 0)
            {
                cells.Merge(i, i + rowSpanNum, CellSpan, CellSpan);
            }
            else
            {
                rowSpanNum = 1;
            }
        }
    }
}