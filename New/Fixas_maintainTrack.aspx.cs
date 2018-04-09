using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Portal.Fixas;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class New_Fixas_maintainTrack : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txbMaintainDate1.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txbMaintainDate2.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            BindTypes();
            dropType.SelectedIndex = dropType.Items.IndexOf(dropType.Items.FindByText("机器设备"));
            BindSubTypes();
            if (Request.QueryString["ty"] == "export")
            {
                DataSet ds = FixasMaintainHelper.SelectMaintainTrack(Request.QueryString["fixasNo"], Request.QueryString["fixasName"], Request.QueryString["d1"], Request.QueryString["d2"], Convert.ToInt32(Request.QueryString["type"]), Convert.ToInt32(Request.QueryString["detail"]), Convert.ToInt32(Session["plantCode"]));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                    doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("固定资产保养跟踪");

                    #region 设置列宽
                    AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col1.ColumnIndexStart = 0;
                    col1.ColumnIndexEnd = 0;
                    col1.Width = 30 * 6000 / 164;
                    sheet.AddColumnInfo(col1);

                    AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col2.ColumnIndexStart = 1;
                    col2.ColumnIndexEnd = 1;
                    col2.Width = 60 * 6000 / 164;
                    sheet.AddColumnInfo(col2);

                    AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col3.ColumnIndexStart = 2;
                    col3.ColumnIndexEnd = 2;
                    col3.Width = 80 * 6000 / 164;
                    sheet.AddColumnInfo(col3);

                    AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col4.ColumnIndexStart = 3;
                    col4.ColumnIndexEnd = 3;
                    col4.Width = 80 * 6000 / 164;
                    sheet.AddColumnInfo(col4);

                    AppLibrary.WriteExcel.ColumnInfo col5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col5.ColumnIndexStart = 4;
                    col5.ColumnIndexEnd = 4;
                    col5.Width = 150 * 6000 / 164;
                    sheet.AddColumnInfo(col5);

                    AppLibrary.WriteExcel.ColumnInfo col6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col6.ColumnIndexStart = 5;
                    col6.ColumnIndexEnd = 5;
                    col6.Width = 50 * 6000 / 164;
                    sheet.AddColumnInfo(col6);

                    AppLibrary.WriteExcel.ColumnInfo col7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col7.ColumnIndexStart = 6;
                    col7.ColumnIndexEnd = 6;
                    col7.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col7);

                    AppLibrary.WriteExcel.ColumnInfo col8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col8.ColumnIndexStart = 7;
                    col8.ColumnIndexEnd = 7;
                    col8.Width = 60 * 6000 / 164;
                    sheet.AddColumnInfo(col8);

                    AppLibrary.WriteExcel.ColumnInfo col9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col9.ColumnIndexStart = 8;
                    col9.ColumnIndexEnd = 8;
                    col9.Width = 60 * 6000 / 164;
                    sheet.AddColumnInfo(col9);

                    AppLibrary.WriteExcel.ColumnInfo col10 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col10.ColumnIndexStart = 9;
                    col10.ColumnIndexEnd = 9;
                    col10.Width = 60 * 6000 / 164;
                    sheet.AddColumnInfo(col10);

                    AppLibrary.WriteExcel.ColumnInfo col11 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col11.ColumnIndexStart = 10;
                    col11.ColumnIndexEnd = 10;
                    col11.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col11);

                    AppLibrary.WriteExcel.ColumnInfo col12 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col12.ColumnIndexStart = 11;
                    col12.ColumnIndexEnd = 11;
                    col12.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col12);

                    AppLibrary.WriteExcel.ColumnInfo col13 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col13.ColumnIndexStart = 12;
                    col13.ColumnIndexEnd = 12;
                    col13.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col13);

                    AppLibrary.WriteExcel.ColumnInfo col14 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col14.ColumnIndexStart = 13;
                    col14.ColumnIndexEnd = 13;
                    col14.Width = 40 * 6000 / 164;
                    sheet.AddColumnInfo(col14);

                    AppLibrary.WriteExcel.ColumnInfo col15 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col15.ColumnIndexStart = 14;
                    col15.ColumnIndexEnd = 14;
                    col15.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col15);

                    AppLibrary.WriteExcel.ColumnInfo col16 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col16.ColumnIndexStart = 15;
                    col16.ColumnIndexEnd = 15;
                    col16.Width = 40 * 6000 / 164;
                    sheet.AddColumnInfo(col16);

                    AppLibrary.WriteExcel.ColumnInfo col17 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col17.ColumnIndexStart = 16;
                    col17.ColumnIndexEnd = 16;
                    col17.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col17);

                    AppLibrary.WriteExcel.ColumnInfo col18 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col18.ColumnIndexStart = 17;
                    col18.ColumnIndexEnd = 17;
                    col18.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col18);

                    AppLibrary.WriteExcel.ColumnInfo col19 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col19.ColumnIndexStart = 18;
                    col19.ColumnIndexEnd = 18;
                    col19.Width = 150 * 6000 / 164;
                    sheet.AddColumnInfo(col19);

                    #endregion

                    int rowIndex = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PrintExcel(doc, sheet, rowIndex, row["domain"], row["cc"], row["planNo"], row["fixasNo"], row["fixasName"], row["maintainPeriod"], row["lastMaintainDate"], row["IsOutPeriod"], row["maintainStatus"], row["fixasType"], row["fixasTypeDet"], row["planDate"],
                                   row["maintainDesc"], row["planCreateName"], row["planCreateDate"], row["maintainName"], row["beginDate"], row["endDate"], row["maintainRecord"], row["reportDateTime"]);
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

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex, Object domain, Object cc, Object planNo, Object fixasNo, Object fixasName, Object maintainPeriod, Object lastMaintainDate, Object IsOutPeriod, Object maintainStatus, Object fixasType, Object fixasTypeDet, Object planDate,
                                   Object maintainDesc, Object planCreateName, Object planCreateDate,  Object maintainName, Object beginDate, Object endDate, Object maintainRecord, Object reportDateTime)
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
            sheet.Cells.Merge(rowIndex, rowIndex, 1, 11);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
            sheet.Cells.Add(rowIndex, 1, "固定资产保养跟踪：跟踪时间 " + Request.QueryString["d1"] + " - " + Request.QueryString["d2"], xf);
            for (int i = 2; i <= 11; i++)
            {
                sheet.Cells.Add(rowIndex, i, string.Empty, xf);
            }
            rowIndex++;

            sheet.Cells.Merge(rowIndex, rowIndex, 1, 11);
            sheet.Cells.Add(rowIndex, 1, "生成报表时间 " + string.Format("{0:yyyy-MM-dd HH:mm:ss}", reportDateTime), xf);
            for (int i = 2; i <= 11; i++)
            {
                sheet.Cells.Add(rowIndex, i, string.Empty, xf);
            }

            rowIndex++;
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            sheet.Cells.Add(rowIndex, 1, "域", xf);
            sheet.Cells.Add(rowIndex, 2, "成本中心", xf);
            sheet.Cells.Add(rowIndex, 3, "保养单", xf);
            sheet.Cells.Add(rowIndex, 4, "资产编号", xf);
            sheet.Cells.Add(rowIndex, 5, "资产名称", xf);
            sheet.Cells.Add(rowIndex, 6, "保养周期", xf);
            sheet.Cells.Add(rowIndex, 7, "上次保养时间", xf);
            sheet.Cells.Add(rowIndex, 8, "是否超期", xf);
            sheet.Cells.Add(rowIndex, 9, "保养状态", xf);
            sheet.Cells.Add(rowIndex, 10, "类型", xf);
            sheet.Cells.Add(rowIndex, 11, "详细类型", xf);
            sheet.Cells.Add(rowIndex, 12, "计划保养时间", xf);
            sheet.Cells.Add(rowIndex, 13, "保养描述", xf);
            sheet.Cells.Add(rowIndex, 14, "计划人", xf);
            sheet.Cells.Add(rowIndex, 15, "计划时间", xf);
            sheet.Cells.Add(rowIndex, 16, "保养人", xf);
            sheet.Cells.Add(rowIndex, 17, "保养开始时间", xf);
            sheet.Cells.Add(rowIndex, 18, "保养结束时间", xf);
            sheet.Cells.Add(rowIndex, 19, "保养记录", xf);

            rowIndex++;
        }
        sheet.Cells.Add(rowIndex, 1, domain.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, cc.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, planNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, fixasNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, fixasName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, maintainPeriod.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, string.Format("{0:yyyy-MM-dd HH:mm}", lastMaintainDate), xf);
        sheet.Cells.Add(rowIndex, 8, IsOutPeriod.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, maintainStatus.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, fixasType.ToString(), xf);
        sheet.Cells.Add(rowIndex, 11, fixasTypeDet.ToString(), xf);
        sheet.Cells.Add(rowIndex, 12, string.Format("{0:yyyy-MM-dd HH:mm}", planDate), xf);
        sheet.Cells.Add(rowIndex, 13, maintainDesc.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, planCreateName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 15, string.Format("{0:yyyy-MM-dd HH:mm}", planCreateDate), xf);
        sheet.Cells.Add(rowIndex, 16, maintainName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 17, string.Format("{0:yyyy-MM-dd HH:mm}", beginDate), xf);
        sheet.Cells.Add(rowIndex, 18, string.Format("{0:yyyy-MM-dd HH:mm}", endDate), xf);
        sheet.Cells.Add(rowIndex, 19, maintainRecord.ToString(), xf);
        
    }


    protected void BindData()
    {
        DataSet ds = FixasMaintainHelper.SelectMaintainTrack(txbFixasNo.Text.Trim(), txbFixasName.Text.Trim(), txbMaintainDate1.Text, txbMaintainDate2.Text.Trim(), Convert.ToInt32(dropType.SelectedValue), Convert.ToInt32(dropDetail.SelectedValue), Convert.ToInt32(Session["plantCode"]));
        gvMaintainReport.DataSource = ds;
        lblTotal.Text = ds.Tables[0].Rows.Count.ToString();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblReportTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", ds.Tables[0].Rows[0]["reportDateTime"]);
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
        Response.Redirect("/new/Fixas_maintainTrack.aspx?ty=export&fixasNo=" + txbFixasNo.Text.Trim()+ "&fixasName="+ txbFixasName.Text.Trim()+"&type="+dropType.SelectedValue+"&detail="+dropDetail.SelectedValue+"&d1=" + txbMaintainDate1.Text.Trim() + "&d2=" + txbMaintainDate2.Text.Trim() + "&rt=" + DateTime.Now.ToString());
    }

    protected void gvMaintainReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMaintainReport.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvMaintainReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    protected void BindTypes()
    {
        dropType.Items.Clear();
        dropType.Items.Add(new ListItem(" -- ", "0"));
        DataTable dt = FixasTypeHelper.SelectFixasTypeList();
        foreach (DataRow row in dt.Rows)
        {
            dropType.Items.Add(new ListItem(row["fixasTypeName"].ToString(), row["fixasTypeID"].ToString()));
        }
    }

    protected void BindSubTypes()
    {
        dropDetail.Items.Clear();
        dropDetail.Items.Add(new ListItem(" -- ", "0"));
        DataTable dt = FixasTypeHelper.SelectFixasSubTypeList(dropType.SelectedValue);
        foreach (DataRow row in dt.Rows)
        {
            dropDetail.Items.Add(new ListItem(row["fixasTypeName"].ToString(), row["fixasTypeID"].ToString()));
        }
    }
    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubTypes();
    }
}