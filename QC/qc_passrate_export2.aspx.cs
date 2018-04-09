using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Collections.Generic;

public partial class QC_qc_passrate_export2 : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt;
        if (Request.QueryString["det"] == null)
        {
            dt = GetIncomingPassRate();
            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("进料检验总览报表");

                #region 设置列宽
                //供应商列
                AppLibrary.WriteExcel.ColumnInfo po_vend = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_vend.ColumnIndexStart = 0;
                po_vend.ColumnIndexEnd = 0;
                po_vend.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_vend);

                //供应商名称列
                AppLibrary.WriteExcel.ColumnInfo companyName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                companyName.ColumnIndexStart = 1;
                companyName.ColumnIndexEnd = 1;
                companyName.Width = 200 * 6000 / 164;
                sheet.AddColumnInfo(companyName);

                //物料列
                AppLibrary.WriteExcel.ColumnInfo part = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                part.ColumnIndexStart = 2;
                part.ColumnIndexEnd = 2;
                part.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(part);

                //总批次列
                AppLibrary.WriteExcel.ColumnInfo totalBatch = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                totalBatch.ColumnIndexStart = 3;
                totalBatch.ColumnIndexEnd = 3;
                totalBatch.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(totalBatch);

                //超期批次列
                AppLibrary.WriteExcel.ColumnInfo overdueBatch = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                overdueBatch.ColumnIndexStart = 4;
                overdueBatch.ColumnIndexEnd = 4;
                overdueBatch.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(overdueBatch);

                //超期率列
                AppLibrary.WriteExcel.ColumnInfo overdueRate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                overdueRate.ColumnIndexStart = 5;
                overdueRate.ColumnIndexEnd = 5;
                overdueRate.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(overdueRate);

                //超期价格列
                AppLibrary.WriteExcel.ColumnInfo overduePrice = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                overduePrice.ColumnIndexStart = 6;
                overduePrice.ColumnIndexEnd = 6;
                overduePrice.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(overduePrice);

                //已检批次列
                AppLibrary.WriteExcel.ColumnInfo inspectBatch = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                inspectBatch.ColumnIndexStart = 7;
                inspectBatch.ColumnIndexEnd = 7;
                inspectBatch.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(inspectBatch);

                //合格批次列
                AppLibrary.WriteExcel.ColumnInfo passBatch = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                passBatch.ColumnIndexStart = 8;
                passBatch.ColumnIndexEnd = 8;
                passBatch.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(passBatch);

                //合格率列
                AppLibrary.WriteExcel.ColumnInfo passRate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                passRate.ColumnIndexStart = 9;
                passRate.ColumnIndexEnd = 9;
                passRate.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(passRate);

                //不合格价格列
                AppLibrary.WriteExcel.ColumnInfo notPassPrice = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                notPassPrice.ColumnIndexStart = 10;
                notPassPrice.ColumnIndexEnd = 10;
                notPassPrice.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(notPassPrice);

                //周期次列
                AppLibrary.WriteExcel.ColumnInfo period = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                period.ColumnIndexStart = 11;
                period.ColumnIndexEnd = 11;
                period.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(period);

                //周期率列
                AppLibrary.WriteExcel.ColumnInfo periodRate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                periodRate.ColumnIndexStart = 12;
                periodRate.ColumnIndexEnd = 12;
                periodRate.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(periodRate);

                //周期价格列
                AppLibrary.WriteExcel.ColumnInfo periodPrice = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                periodPrice.ColumnIndexStart = 13;
                periodPrice.ColumnIndexEnd = 13;
                periodPrice.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(periodPrice);

                //描述列
                AppLibrary.WriteExcel.ColumnInfo desc = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                desc.ColumnIndexStart = 14;
                desc.ColumnIndexEnd = 14;
                desc.Width = 300 * 6000 / 164;
                sheet.AddColumnInfo(desc);


                #endregion

                int rowIndex = 1;
                PrintExcelSummary(doc, sheet, rowIndex, "域", "地点", "供应商", "供应商名称", "物料号", "总批次", "超期批次", "超期率", "超期价格", "已检批次", "合格批次", "合格率", "不合格价格", "周期批次", "周期率", "周期价格", "描述", true, "关键物料");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcelSummary(doc, sheet, rowIndex, row["prh_domain"], row["prh_site"], row["prh_vend"], row["ad_name"], row["prh_part"], row["total"], row["overdue"], row["overduerate"], row["OverDuePrice"], row["Inspected"], row["pass"], row["InspectedRate"], row["NotPassPrice"], row["Period"], row["PeriodRate"], row["PeriodPrice"], row["pt_desc"], false, row["isKeyPart"]);
                }
                doc.Send();

                Response.Flush();
                Response.End();
            }
            dt.Dispose();
        }
        else
        {
            dt = GetIncomingNotPass();
            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("进料检验明细报表");

                #region 设置列宽
                //供应商列
                AppLibrary.WriteExcel.ColumnInfo po_vend = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_vend.ColumnIndexStart = 0;
                po_vend.ColumnIndexEnd = 0;
                po_vend.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_vend);

                //供应商名称列
                AppLibrary.WriteExcel.ColumnInfo companyName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                companyName.ColumnIndexStart = 1;
                companyName.ColumnIndexEnd = 1;
                companyName.Width = 200 * 6000 / 164;
                sheet.AddColumnInfo(companyName);

                //采购单列
                AppLibrary.WriteExcel.ColumnInfo po_nbr = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_nbr.ColumnIndexStart = 2;
                po_nbr.ColumnIndexEnd = 2;
                po_nbr.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_nbr);

                //行号列
                AppLibrary.WriteExcel.ColumnInfo line = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                line.ColumnIndexStart = 3;
                line.ColumnIndexEnd = 3;
                line.Width = 40 * 6000 / 164;
                sheet.AddColumnInfo(line);

                //物料号列
                AppLibrary.WriteExcel.ColumnInfo part = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                part.ColumnIndexStart = 4;
                part.ColumnIndexEnd = 4;
                part.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(part);

                //描述列
                AppLibrary.WriteExcel.ColumnInfo desc = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                desc.ColumnIndexStart = 5;
                desc.ColumnIndexEnd = 5;
                desc.Width = 200 * 6000 / 164;
                sheet.AddColumnInfo(desc);

                //订单日期列
                AppLibrary.WriteExcel.ColumnInfo ord_date = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                ord_date.ColumnIndexStart = 6;
                ord_date.ColumnIndexEnd = 6;
                ord_date.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(ord_date);

                //截止日期列
                AppLibrary.WriteExcel.ColumnInfo due_date = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                due_date.ColumnIndexStart = 7;
                due_date.ColumnIndexEnd = 7;
                due_date.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(due_date);

                //收货单列
                AppLibrary.WriteExcel.ColumnInfo po_recv = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_recv.ColumnIndexStart = 8;
                po_recv.ColumnIndexEnd = 8;
                po_recv.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_recv);

                //收货数量列
                AppLibrary.WriteExcel.ColumnInfo po_recv_qty = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_recv_qty.ColumnIndexStart = 9;
                po_recv_qty.ColumnIndexEnd = 9;
                po_recv_qty.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_recv_qty);

                //收货日期列
                AppLibrary.WriteExcel.ColumnInfo po_recv_date = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_recv_date.ColumnIndexStart = 10;
                po_recv_date.ColumnIndexEnd = 10;
                po_recv_date.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(po_recv_date);

                //地点列
                AppLibrary.WriteExcel.ColumnInfo po_site = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_site.ColumnIndexStart = 11;
                po_site.ColumnIndexEnd = 11;
                po_site.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(po_site);

                //域列
                AppLibrary.WriteExcel.ColumnInfo po_domain = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                po_domain.ColumnIndexStart = 12;
                po_domain.ColumnIndexEnd = 12;
                po_domain.Width = 60 * 6000 / 164;
                sheet.AddColumnInfo(po_domain);

                //是否超期列
                AppLibrary.WriteExcel.ColumnInfo over_due = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                over_due.ColumnIndexStart = 13;
                over_due.ColumnIndexEnd = 13;
                over_due.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(over_due);

                //检验结果列
                AppLibrary.WriteExcel.ColumnInfo result = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                result.ColumnIndexStart = 14;
                result.ColumnIndexEnd = 14;
                result.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(result);

                //原因列
                AppLibrary.WriteExcel.ColumnInfo reason = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                reason.ColumnIndexStart = 15;
                reason.ColumnIndexEnd = 15;
                reason.Width = 250 * 6000 / 164;
                sheet.AddColumnInfo(reason);
                #endregion

                int rowIndex = 1;
                PrintExcelDeatil(doc, sheet, rowIndex, "供应商", "供应商名称", "采购单", "行号", "物料号", "描述", "订单日期", "截止日期", "收货单", "收货数量", "收货日期", "地点", "域", "是否超期", "是否周期", "检验结果", "原因", true, "关键物料");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcelDeatil(doc, sheet, rowIndex, row["prh_vend"], row["ad_name"], row["prh_nbr"], row["prh_line"], row["prh_part"], row["pt_desc"], row["po_ord_date"], row["pod_due_date"], row["prh_receiver"],
                        row["prh_rcvd"], row["prh_rcp_date"], row["pod_site"], row["prh_domain"], row["OverDue"], row["Period"], row["Result"], row["Reason"], false, row["isKeyPart"]);
                }

                doc.Send();

                Response.Flush();
                Response.End();
            }
            dt.Dispose();
        }
    }

    /// <summary>
    /// 来料检验明细
    /// </summary>
    /// <returns></returns>
    protected DataTable GetIncomingNotPass()
    {
        try
        {
            string strSql = "sp_QC_Rep_IncomingNotPass";

            SqlParameter[] sqlParam = new SqlParameter[13];
            sqlParam[0] = new SqlParameter("@vend", Request.QueryString["vend"].ToString());
            sqlParam[1] = new SqlParameter("@part", Request.QueryString["p"].ToString());
            sqlParam[2] = new SqlParameter("@stddate", Request.QueryString["d1"].ToString());
            sqlParam[3] = new SqlParameter("@enddate", Request.QueryString["d2"].ToString());
            sqlParam[4] = new SqlParameter("@inspected", Convert.ToBoolean(Request.QueryString["chkInspect"]));
            sqlParam[5] = new SqlParameter("@overdue", Convert.ToBoolean(Request.QueryString["chkOverdue"]));
            sqlParam[6] = new SqlParameter("@po_period", Request.QueryString["pr"].ToString());
            sqlParam[7] = new SqlParameter("@dueDate1", Request.QueryString["due1"].ToString());
            sqlParam[8] = new SqlParameter("@dueDate2", Request.QueryString["due2"].ToString());
            sqlParam[9] = new SqlParameter("@chkFacDate", Convert.ToBoolean(Request.QueryString["chkFacDate"]));
            sqlParam[10] = new SqlParameter("@domain", Request.QueryString["domain"]);
            sqlParam[11] = new SqlParameter("@checkDate1", Request.QueryString["chk1"].ToString());
            sqlParam[12] = new SqlParameter("@checkDate2", Request.QueryString["chk2"].ToString());

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 来料检验总览
    /// </summary>
    /// <returns></returns>
    protected DataTable GetIncomingPassRate()
    {
        try
        {
            string strSql = "sp_QC_Rep_IncomingPassRate";

            SqlParameter[] sqlParam = new SqlParameter[13];
            sqlParam[0] = new SqlParameter("@vend", Request.QueryString["vend"].ToString());
            sqlParam[1] = new SqlParameter("@part", Request.QueryString["p"].ToString());
            sqlParam[2] = new SqlParameter("@stddate", Request.QueryString["d1"].ToString());
            sqlParam[3] = new SqlParameter("@enddate", Request.QueryString["d2"].ToString());
            sqlParam[4] = new SqlParameter("@inspected", Convert.ToBoolean(Request.QueryString["chkInspect"]));
            sqlParam[5] = new SqlParameter("@overdue", Convert.ToBoolean(Request.QueryString["chkOverdue"]));
            sqlParam[6] = new SqlParameter("@ordDate1", Request.QueryString["ordDate1"].ToString());
            sqlParam[7] = new SqlParameter("@ordDate2", Request.QueryString["ordDate2"].ToString());
            sqlParam[8] = new SqlParameter("@dueDate1", Request.QueryString["dueDate1"].ToString());
            sqlParam[9] = new SqlParameter("@dueDate2", Request.QueryString["dueDate2"].ToString());
            sqlParam[10] = new SqlParameter("@po_period", Request.QueryString["poPeriod"]);
            sqlParam[11] = new SqlParameter("@chkFacDate", Convert.ToBoolean(Request.QueryString["chkFacDate"]));
            sqlParam[12] = new SqlParameter("@domain", Request.QueryString["domain"]);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void PrintExcelDeatil(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
        Object po_vend, Object companyName, Object po_nbr, Object line, Object part, Object desc, Object ord_date, Object due_date,
        Object po_recv, Object po_recv_qty, Object po_recv_date, Object po_site, Object po_domain, Object over_due, Object period, Object qc_result,
        Object qc_reason, bool isHeader, Object isKeyPart)
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

        sheet.Cells.Add(rowIndex, 1, po_vend.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, companyName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, po_nbr.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, line.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, part.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, desc.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, isHeader ? ord_date.ToString() : string.Format("{0:yyyy-MM-dd}", ord_date), xf);
        sheet.Cells.Add(rowIndex, 8, isHeader ? due_date.ToString() : string.Format("{0:yyyy-MM-dd}", due_date), xf);
        sheet.Cells.Add(rowIndex, 9, po_recv.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, po_recv_qty.ToString(), xf);
        sheet.Cells.Add(rowIndex, 11, isHeader ? po_recv_date.ToString() : string.Format("{0:yyyy-MM-dd}", po_recv_date), xf);
        sheet.Cells.Add(rowIndex, 12, po_site.ToString(), xf);
        sheet.Cells.Add(rowIndex, 13, po_domain.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, over_due.ToString(), xf);
        sheet.Cells.Add(rowIndex, 15, period.ToString(), xf);
        sheet.Cells.Add(rowIndex, 16, qc_result.ToString(), xf);
        sheet.Cells.Add(rowIndex, 17, qc_reason.ToString(), xf);
        sheet.Cells.Add(rowIndex, 18, isKeyPart.ToString(), xf);
    }

    protected void PrintExcelSummary(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
        Object po_domain, Object po_site, Object po_vend, Object companyName, Object part, Object totalBatch, Object overDueBatch, Object overDueRate, Object overDuePrice, Object inspectBatch,
        Object passBatch, Object passRate, Object notPassPrice, Object period, Object periodRate, Object periodPrice, Object desc, bool isHeader, Object isKeyPart)
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

        sheet.Cells.Add(rowIndex, 1, po_domain.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, po_site.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, po_vend.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, companyName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, part.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, totalBatch, xf);
        sheet.Cells.Add(rowIndex, 7, overDueBatch, xf);
        sheet.Cells.Add(rowIndex, 8, isHeader ? overDueRate : string.Format("{0:P}", overDueRate), xf);
        sheet.Cells.Add(rowIndex, 9, overDuePrice, xf);
        sheet.Cells.Add(rowIndex, 10, inspectBatch, xf);
        sheet.Cells.Add(rowIndex, 11, passBatch, xf);
        sheet.Cells.Add(rowIndex, 12, isHeader ? passRate : string.Format("{0:P}", passRate), xf);
        sheet.Cells.Add(rowIndex, 13, notPassPrice, xf);
        sheet.Cells.Add(rowIndex, 14, period, xf);
        sheet.Cells.Add(rowIndex, 15, isHeader ? periodRate : string.Format("{0:P}", periodRate), xf);
        sheet.Cells.Add(rowIndex, 16, periodPrice, xf);
        sheet.Cells.Add(rowIndex, 17, desc.ToString(), xf);
        sheet.Cells.Add(rowIndex, 18, isKeyPart.ToString(), xf);
    }
}