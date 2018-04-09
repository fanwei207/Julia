using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_maintainTimeCost : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txbMaintainDate1.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txbMaintainDate2.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            if (Request.QueryString["ty"] == "export")
            {
                DataSet ds = FixasMaintainHelper.SelectMaintainTimeCost(txbFixasNo.Text.Trim(), txbFixasName.Text.Trim(), Request.QueryString["d1"], Request.QueryString["d2"], Convert.ToInt32(Session["plantCode"]));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                    doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("固定资产保养时长汇总");

                    #region 设置列宽
                    AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col1.ColumnIndexStart = 0;
                    col1.ColumnIndexEnd = 0;
                    col1.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col1);

                    AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col2.ColumnIndexStart = 1;
                    col2.ColumnIndexEnd = 1;
                    col2.Width = 60 * 6000 / 164;
                    sheet.AddColumnInfo(col2);

                    AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col3.ColumnIndexStart = 2;
                    col3.ColumnIndexEnd = 2;
                    col3.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col3);

                    AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col4.ColumnIndexStart = 3;
                    col4.ColumnIndexEnd = 3;
                    col4.Width = 90 * 6000 / 164;
                    sheet.AddColumnInfo(col4);

                    AppLibrary.WriteExcel.ColumnInfo col5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col5.ColumnIndexStart = 4;
                    col5.ColumnIndexEnd = 4;
                    col5.Width = 180 * 6000 / 164;
                    sheet.AddColumnInfo(col5);

                    AppLibrary.WriteExcel.ColumnInfo col6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col6.ColumnIndexStart = 5;
                    col6.ColumnIndexEnd = 5;
                    col6.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col6);

                    AppLibrary.WriteExcel.ColumnInfo col7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col7.ColumnIndexStart = 6;
                    col7.ColumnIndexEnd = 6;
                    col7.Width = 180 * 6000 / 164;
                    sheet.AddColumnInfo(col7);

                    AppLibrary.WriteExcel.ColumnInfo col8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col8.ColumnIndexStart = 7;
                    col8.ColumnIndexEnd = 7;
                    col8.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col8);

                    AppLibrary.WriteExcel.ColumnInfo col9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col9.ColumnIndexStart = 8;
                    col9.ColumnIndexEnd = 8;
                    col9.Width = 70 * 6000 / 164;
                    sheet.AddColumnInfo(col9);

                    AppLibrary.WriteExcel.ColumnInfo col10 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col10.ColumnIndexStart = 9;
                    col10.ColumnIndexEnd = 9;
                    col10.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col10);

                    AppLibrary.WriteExcel.ColumnInfo col11 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col11.ColumnIndexStart = 10;
                    col11.ColumnIndexEnd = 10;
                    col11.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col11);

                    AppLibrary.WriteExcel.ColumnInfo col12 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col12.ColumnIndexStart = 11;
                    col12.ColumnIndexEnd = 1;
                    col12.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col12);
                    #endregion

                    int rowIndex = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PrintExcel(doc, sheet, rowIndex, row["ccDesc"], row["fixasType"], row["fixasSubType"], row["fixasNo"], row["fixasName"], row["maintainOrder"], row["maintainedName"], row["planDate"], row["maintainStatus"], row["beginDate"], row["endDate"], row["diff"], row["reportTime"]);
                        if (rowIndex == 1)
                        {
                            rowIndex += 4;
                        }
                        else
                        {
                            rowIndex++;
                        }
                    }

                    doc.Save(Server.MapPath("/Excel/"), true);
                    ds.Dispose();

                    ltlAlert.Text = "window.open('/Excel/" + doc.FileName + "','_blank');";
                }
            }

            BindData();
        }
    }

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex, Object fixasCC, Object fixasType, Object fixasSubType,
       Object fixasNo, Object fixasName, Object maintainOrder, Object maintainedName, Object planDate, Object maintainStatus, Object beginDate, Object endDate, Object diff, Object reportTime)
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 13;

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        if (rowIndex == 1)
        {
            sheet.Cells.Merge(rowIndex, rowIndex, 1, 12);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
            sheet.Cells.Add(rowIndex, 1, "保养时长汇总：保养时间 " + txbMaintainDate1.Text.Trim() + " - " + txbMaintainDate2.Text.Trim(), xf);
            for (int i = 2; i <= 12; i++)
            {
                sheet.Cells.Add(rowIndex, i, string.Empty, xf);
            }
            rowIndex++;

            sheet.Cells.Merge(rowIndex, rowIndex, 1, 12);
            sheet.Cells.Add(rowIndex, 1, "生成报表时间 " + string.Format("{0:yyyy-MM-dd HH:mm:ss}", reportTime), xf);
            for (int i = 2; i <= 12; i++)
            {
                sheet.Cells.Add(rowIndex, i, string.Empty, xf);
            }

            rowIndex++;
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            sheet.Cells.Add(rowIndex, 1, "成本中心", xf);
            sheet.Cells.Add(rowIndex, 2, "类型", xf);
            sheet.Cells.Add(rowIndex, 3, "详细类型", xf);
            sheet.Cells.Add(rowIndex, 4, "资产编号", xf);
            sheet.Cells.Add(rowIndex, 5, "资产名称", xf);
            sheet.Cells.Add(rowIndex, 6, "保养单", xf);
            sheet.Cells.Add(rowIndex, 7, "保养人", xf);
            sheet.Cells.Add(rowIndex, 8, "计划保养时间", xf);
            sheet.Cells.Add(rowIndex, 9, "保养状态", xf);
            sheet.Cells.Add(rowIndex, 10, "报表开始时间", xf);
            sheet.Cells.Add(rowIndex, 11, "报表截止时间", xf);
            sheet.Cells.Add(rowIndex, 12, "历时/h", xf);
            rowIndex++;
        }

        sheet.Cells.Add(rowIndex, 1, fixasCC.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, fixasType.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, fixasSubType.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, fixasNo.ToString(), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 5, fixasName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, maintainOrder.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, maintainedName.ToString(), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        sheet.Cells.Add(rowIndex, 8, string.Format("{0:yyyy-MM-dd HH:mm}", planDate), xf);
        if (maintainStatus.ToString() == "planned")
        {
            maintainStatus = "已计划";
        }
        else
        {
            maintainStatus = "已完成";
        }
        sheet.Cells.Add(rowIndex, 9, maintainStatus.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, string.Format("{0:yyyy-MM-dd HH:mm}", beginDate), xf);
        sheet.Cells.Add(rowIndex, 11, string.Format("{0:yyyy-MM-dd HH:mm}", endDate), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Right;
        sheet.Cells.Add(rowIndex, 12, diff.ToString(), xf);
    }

    protected void BindData()
    {
        DataSet ds = FixasMaintainHelper.SelectMaintainTimeCost(txbFixasNo.Text.Trim(), txbFixasName.Text.Trim(), txbMaintainDate1.Text, txbMaintainDate2.Text.Trim(), Convert.ToInt32(Session["plantCode"]));
        gvMaintainReport.DataSource = ds;
        lblTotal.Text = ds.Tables[0].Rows.Count.ToString();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblReportTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", ds.Tables[0].Rows[0]["reportTime"]);
        }
        else
        {
            lblReportTime.Text = string.Empty;
        }
        gvMaintainReport.DataBind();
        ds.Dispose();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/Fixas_maintainTimeCost.aspx?ty=export&d1=" + txbMaintainDate1.Text.Trim() + "&d2=" + txbMaintainDate2.Text.Trim() + "&rt=" + DateTime.Now.ToString());
    }

    protected void gvMaintainReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMaintainReport.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvMaintainReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[8].Text == "planned")
            {
                e.Row.Cells[8].Text = "已计划";
                e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
            }
            else if (e.Row.Cells[8].Text == "completed")
            {
                e.Row.Cells[8].Text = "已完成";
            }
            else
            {
                e.Row.Cells[8].Text = "--";
                e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
            }
        }
    }
}