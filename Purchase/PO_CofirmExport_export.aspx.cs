using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Collections.Generic;

public partial class Purchase_PO_CofirmExport_export : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = GetConfirmedPO();
            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("采购单确认报表");

                #region 设定列宽
                //域列
                AppLibrary.WriteExcel.ColumnInfo domain = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                domain.ColumnIndexStart = 0;
                domain.ColumnIndexEnd = 0;
                domain.Width = 50 * 6000 / 164;
                sheet.AddColumnInfo(domain);

                //采购单号列
                AppLibrary.WriteExcel.ColumnInfo po_nbr = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_nbr.ColumnIndexStart = 1;
                po_nbr.ColumnIndexEnd = 1;
                po_nbr.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_nbr);

                //供应商列
                AppLibrary.WriteExcel.ColumnInfo po_vend = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_vend.ColumnIndexStart = 2;
                po_vend.ColumnIndexEnd = 2;
                po_vend.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_vend);

                //供应商名称列
                AppLibrary.WriteExcel.ColumnInfo companyName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                companyName.ColumnIndexStart = 3;
                companyName.ColumnIndexEnd = 3;
                companyName.Width = 200 * 6000 / 164;
                sheet.AddColumnInfo(companyName);

                //地点列
                AppLibrary.WriteExcel.ColumnInfo site = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                site.ColumnIndexStart = 4;
                site.ColumnIndexEnd = 4;
                site.Width = 50 * 6000 / 164;
                sheet.AddColumnInfo(site);

                //订单日期列
                AppLibrary.WriteExcel.ColumnInfo ord_date = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                ord_date.ColumnIndexStart = 5;
                ord_date.ColumnIndexEnd = 5;
                ord_date.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(ord_date);

                //截止日期列
                AppLibrary.WriteExcel.ColumnInfo due_date = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                due_date.ColumnIndexStart = 6;
                due_date.ColumnIndexEnd = 6;
                due_date.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(due_date);

                //确认日期列
                AppLibrary.WriteExcel.ColumnInfo con_date = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                con_date.ColumnIndexStart = 7;
                con_date.ColumnIndexEnd = 7;
                con_date.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(con_date);

                //最早送货日期列
                AppLibrary.WriteExcel.ColumnInfo earliestDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                earliestDate.ColumnIndexStart = 8;
                earliestDate.ColumnIndexEnd = 8;
                earliestDate.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(earliestDate);

                //订单总价列
                AppLibrary.WriteExcel.ColumnInfo totalCost = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                totalCost.ColumnIndexStart = 9;
                totalCost.ColumnIndexEnd = 9;
                totalCost.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(totalCost);

                //备注列
                AppLibrary.WriteExcel.ColumnInfo rmks = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                rmks.ColumnIndexStart = 10;
                rmks.ColumnIndexEnd = 10;
                rmks.Width = 150 * 6000 / 164;
                sheet.AddColumnInfo(rmks);

                #endregion

                int rowIndex = 1;
                PrintExcel(doc, sheet, rowIndex, "域", "采购单号", "供应商", "供应商名称", "地点", "订单日期", "订单日期1", "截止日期", "确认日期", "未确认间隔(h)", "过期确认间隔(h)", "最早送货日期", "订单总价", "备注", true);
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex, row["po_domain"], row["po_nbr"], row["po_vend"], row["companyName"], row["po_ship"]
                        , row["po_ord_date"], row["po_ord_date1"], row["po_due_date"], row["po_con_date"]
                        , row["Hour_overNotConfirm"], row["Hour_overConfirm"]
                        , row["prh_rcp_date"], row["po_total_cost"], row["po_rmks"], false);
                }
                doc.Send();

                Response.Flush();
                Response.End();

                dt.Dispose();
            }
            else
            {
                Response.Redirect("PO_CofirmExport.aspx");
            }
        }
    }

    protected DataTable GetConfirmedPO()
    {
        bool strOverNotConfirm = Request.QueryString["overNotConfirm"] == "True" ? true : false;
        bool strOverConfirm = Request.QueryString["overConfirm"] == "True" ? true : false;

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@domain", Request.QueryString["domain"]);
        param[1] = new SqlParameter("@vend", Request.QueryString["vend"]);
        param[2] = new SqlParameter("@nbr1", Request.QueryString["nbr1"]);
        param[3] = new SqlParameter("@nbr2", Request.QueryString["nbr2"]);
        param[4] = new SqlParameter("@ord_date1", Request.QueryString["ordDate1"]);
        param[5] = new SqlParameter("@ord_date2", Request.QueryString["ordDate2"]);
        param[6] = new SqlParameter("@con_date1", Request.QueryString["conDate1"]);
        param[7] = new SqlParameter("@con_date2", Request.QueryString["conDate2"]);
        param[8] = new SqlParameter("@overNotConfirm", strOverNotConfirm);
        param[9] = new SqlParameter("@overConfirm", strOverConfirm);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_pur_selectSuppConfirmedPo", param).Tables[0];
    }

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
                Object domain, Object po_nbr, Object po_vend, Object companyName, Object site, Object ordDate, Object ordDate1, Object dueDate, Object conDate,
                Object overNotConfirmHour, Object overConfirmHour, Object earliestDate, Object totalCost, Object remark, bool isHeader)
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 13; //对应size = 13

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        sheet.Cells.Add(rowIndex, 1, domain, xf);
        sheet.Cells.Add(rowIndex, 2, po_nbr.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, po_vend.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, companyName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, site.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, isHeader ? ordDate.ToString() : string.Format("{0:yyyy-MM-dd}", ordDate), xf);
        sheet.Cells.Add(rowIndex, 7, isHeader ? ordDate1.ToString() : string.Format("{0:yyyy-MM-dd HH:mm}", ordDate1), xf);
        sheet.Cells.Add(rowIndex, 8, isHeader ? dueDate.ToString() : string.Format("{0:yyyy-MM-dd}", dueDate), xf);
        sheet.Cells.Add(rowIndex, 9, isHeader ? conDate.ToString() : string.Format("{0:yyyy-MM-dd HH:mm}", conDate), xf);
        sheet.Cells.Add(rowIndex, 10, overNotConfirmHour, xf);
        sheet.Cells.Add(rowIndex, 11, overConfirmHour, xf);
        sheet.Cells.Add(rowIndex, 12, isHeader ? earliestDate.ToString() : string.Format("{0:yyyy-MM-dd HH:mm}", earliestDate), xf);
        sheet.Cells.Add(rowIndex, 13, totalCost.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, remark.ToString(), xf);
    }
}