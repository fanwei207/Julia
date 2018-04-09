using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using Portal.Fixas;

public partial class New_Fixas_useSummary : BasePage
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
                DataTable dt = GetFixasUseSummary(Request.QueryString["fixasNo"], Request.QueryString["fixasName"], Session["plantCode"].ToString(), Request.QueryString["start"], Request.QueryString["end"],Convert.ToInt32(Request.QueryString["type"]),Convert.ToInt32(Request.QueryString["detail"]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                    doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("固定资产维修&保养使用汇总");

                    #region 设置列宽
                    AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col1.ColumnIndexStart = 0;
                    col1.ColumnIndexEnd = 0;
                    col1.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col1);

                    AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col2.ColumnIndexStart = 1;
                    col2.ColumnIndexEnd = 1;
                    col2.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col2);

                    AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col3.ColumnIndexStart = 2;
                    col3.ColumnIndexEnd = 2;
                    col3.Width = 170 * 6000 / 164;
                    sheet.AddColumnInfo(col3);

                    AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col4.ColumnIndexStart = 3;
                    col4.ColumnIndexEnd = 3;
                    col4.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col4);

                    AppLibrary.WriteExcel.ColumnInfo col5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col5.ColumnIndexStart = 4;
                    col5.ColumnIndexEnd = 4;
                    col5.Width = 130 * 6000 / 164;
                    sheet.AddColumnInfo(col5);

                    AppLibrary.WriteExcel.ColumnInfo col6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col6.ColumnIndexStart = 5;
                    col6.ColumnIndexEnd = 5;
                    col6.Width = 90 * 6000 / 164;
                    sheet.AddColumnInfo(col6);

                    AppLibrary.WriteExcel.ColumnInfo col7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col7.ColumnIndexStart = 6;
                    col7.ColumnIndexEnd = 6;
                    col7.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col7);

                    AppLibrary.WriteExcel.ColumnInfo col8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col8.ColumnIndexStart = 7;
                    col8.ColumnIndexEnd = 7;
                    col8.Width = 90 * 6000 / 164;
                    sheet.AddColumnInfo(col8);

                    AppLibrary.WriteExcel.ColumnInfo col9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col9.ColumnIndexStart = 8;
                    col9.ColumnIndexEnd = 8;
                    col9.Width = 120 * 6000 / 164;
                    sheet.AddColumnInfo(col9);
                    #endregion

                    int rowIndex = 1;
                    PrintExcel(doc, sheet, rowIndex, "成本中心", "资产编号", "资产名称", "类型", "详细类型", "维修(次)", "维修时长（小时）", "保养(次)", "保养时长（小时）");
                    foreach (DataRow row in dt.Rows)
                    {
                        rowIndex++;
                        PrintExcel(doc, sheet, rowIndex, row["cc"], row["fixasNo"], row["fixasName"], row["fixasType"], row["fixasTypeDet"], row["repairCount"], row["repairCountTime"], row["maintainCount"], row["maintainCountTime"]);
                    }

                    doc.Save(Server.MapPath("/Excel/"), true);
                    dt.Dispose();

                    ltlAlert.Text = "window.open('/Excel/" + doc.FileName + "','_blank');";
                }
            }
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = GetFixasUseSummary(txbFixasNo.Text.Trim(), txbFixasName.Text.Trim(), Session["plantCode"].ToString(), txbMaintainDate1.Text.Trim(), txbMaintainDate2.Text.Trim(),Convert.ToInt32(dropType.SelectedValue),Convert.ToInt32(dropDetail.SelectedValue));
        gvUseSummary.DataSource = dt;
        
        lblTotal.Text = dt.Rows.Count.ToString();
        if (dt != null && dt.Rows.Count > 0)
        {
            lblReportTime.Text = dt.Rows[0]["reportDateTime"].ToString();
        }
        else
        {
            lblReportTime.Text = string.Empty;
        }
        gvUseSummary.DataBind();
        dt.Dispose();
    }

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex, Object fixasCC,
           Object fixasNo, Object fixasName, Object fixasType, Object fixasSubType, Object repairCount, Object repairCountTime, Object maintainCount, Object maintainCountTime)
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

        sheet.Cells.Add(rowIndex, 1, fixasCC.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, fixasNo.ToString(), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 3, fixasName.ToString(), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        sheet.Cells.Add(rowIndex, 4, fixasType.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, fixasSubType.ToString(), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Right;
        sheet.Cells.Add(rowIndex, 6, repairCount.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, repairCountTime.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, maintainCount.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, maintainCountTime.ToString(), xf);
    }

    protected DataTable GetFixasUseSummary(string fixasNo, string fixasName, string plantCode, string start, string end, int type, int subType)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@fixasNo", fixasNo);
            param[1] = new SqlParameter("@fixasName", fixasName);
            param[2] = new SqlParameter("@plantCode", plantCode);
            param[3] = new SqlParameter("@maintainDate1", start);
            param[4] = new SqlParameter("@maintainDate2", end);
            param[5] = new SqlParameter("@type", type);
            param[6] = new SqlParameter("@subType", subType);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectUseSummary", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/Fixas_useSummary.aspx?ty=export&fixasNo=" + txbFixasNo.Text.Trim() + "&fixasName=" + txbFixasName.Text.Trim() + "&start=" + txbMaintainDate1.Text.Trim() + "&end=" + txbMaintainDate2.Text.Trim() +"&type="+dropType.SelectedValue+"&detail="+dropDetail.SelectedValue+ "&rt=" + DateTime.Now.ToString());
    }

    protected void gvUseSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUseSummary.PageIndex = e.NewPageIndex;
        BindData();
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