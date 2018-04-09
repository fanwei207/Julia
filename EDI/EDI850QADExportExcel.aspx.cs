using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Collections.Generic;

public partial class EDI_EDI850QADExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string date = string.Empty;
            if (Request.QueryString["date"] != null)
            {
                date = Request.QueryString["date"].ToString();
            }
            DataTable dt = getEdiData.get850QADExcelData(Session["uID"].ToString().Trim(), date, Session["PlantCode"].ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("EdiQAD850");

                #region 设定列宽
                //订单日期列
                AppLibrary.WriteExcel.ColumnInfo so_ordDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                so_ordDate.ColumnIndexStart = 0;
                so_ordDate.ColumnIndexEnd = 0;
                so_ordDate.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(so_ordDate);

                //港口列
                AppLibrary.WriteExcel.ColumnInfo rmk = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                rmk.ColumnIndexStart = 1;
                rmk.ColumnIndexEnd = 1;
                rmk.Width = 150 * 6000 / 164;
                sheet.AddColumnInfo(rmk);

                //订单号列
                AppLibrary.WriteExcel.ColumnInfo poNbr = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                poNbr.ColumnIndexStart = 2;
                poNbr.ColumnIndexEnd = 2;
                poNbr.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(poNbr);

                //客户订单号列
                AppLibrary.WriteExcel.ColumnInfo fob = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fob.ColumnIndexStart = 3;
                fob.ColumnIndexEnd = 3;
                fob.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fob);

                //SW2列
                AppLibrary.WriteExcel.ColumnInfo reqDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                reqDate.ColumnIndexStart = 4;
                reqDate.ColumnIndexEnd = 4;
                reqDate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(reqDate);

                //截止日期列
                AppLibrary.WriteExcel.ColumnInfo dueDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                dueDate.ColumnIndexStart = 5;
                dueDate.ColumnIndexEnd = 5;
                dueDate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(dueDate);

                //序号列
                AppLibrary.WriteExcel.ColumnInfo poLine = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                poLine.ColumnIndexStart = 6;
                poLine.ColumnIndexEnd = 6;
                poLine.Width = 40 * 6000 / 164;
                sheet.AddColumnInfo(poLine);

                //QAD订单号列
                AppLibrary.WriteExcel.ColumnInfo soNbr = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                soNbr.ColumnIndexStart = 7;
                soNbr.ColumnIndexEnd = 7;
                soNbr.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(soNbr);

                //QAD号编码列
                AppLibrary.WriteExcel.ColumnInfo qadPart = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                qadPart.ColumnIndexStart = 8;
                qadPart.ColumnIndexEnd = 8;
                qadPart.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(qadPart);

                //产品型号列
                AppLibrary.WriteExcel.ColumnInfo partNbr = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                partNbr.ColumnIndexStart = 9;
                partNbr.ColumnIndexEnd = 9;
                partNbr.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(partNbr);

                //产品型号列
                AppLibrary.WriteExcel.ColumnInfo sku = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                partNbr.ColumnIndexStart = 10;
                partNbr.ColumnIndexEnd = 10;
                partNbr.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(partNbr);

                //订购数量（套）列
                AppLibrary.WriteExcel.ColumnInfo ordQty = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                ordQty.ColumnIndexStart = 11;
                ordQty.ColumnIndexEnd = 11;
                ordQty.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(ordQty);

                //销售单地点列
                AppLibrary.WriteExcel.ColumnInfo site = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                site.ColumnIndexStart = 12;
                site.ColumnIndexEnd = 12;
                site.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(site);

                //制地列
                AppLibrary.WriteExcel.ColumnInfo wo_site = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                wo_site.ColumnIndexStart = 13;
                wo_site.ColumnIndexEnd = 13;
                wo_site.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(wo_site);

                //处理意见列
                AppLibrary.WriteExcel.ColumnInfo errMsg = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                errMsg.ColumnIndexStart = 14;
                errMsg.ColumnIndexEnd = 14;
                errMsg.Width = 150 * 6000 / 164;
                sheet.AddColumnInfo(errMsg);

                //订单操作域列
                AppLibrary.WriteExcel.ColumnInfo mpo_domain = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                mpo_domain.ColumnIndexStart = 15;
                mpo_domain.ColumnIndexEnd = 15;
                mpo_domain.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(mpo_domain);

                //收货人地址列
                AppLibrary.WriteExcel.ColumnInfo det_rmks = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                det_rmks.ColumnIndexStart = 16;
                det_rmks.ColumnIndexEnd = 16;
                det_rmks.Width = 300 * 6000 / 164;
                sheet.AddColumnInfo(det_rmks);

                //计划日期
                AppLibrary.WriteExcel.ColumnInfo planDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                det_rmks.ColumnIndexStart = 17;
                det_rmks.ColumnIndexEnd = 17;
                det_rmks.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(planDate);

                #endregion

                int rowIndex = 1;
                PrintExcel(doc, sheet, rowIndex, "订单日期", "港口", "TCP订单号", "客户订单号", "SW2", "截止日期", "序号", "QAD订单号", "QAD号编码", "产品型号", "SKU", "订购数量(套)", "销售单地点","制地", "处理意见", "订单操作域", "收货人地址", "计划日期");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex, row["so_ordDate"], row["rmk"], row["poNbr"], row["fob"], row["reqDate"]
                        , row["dueDate"], row["poLine"], row["soNbr"], row["qadPart"]
                        , row["partNbr"], row["sku"], row["ordQty"]
                        , row["site"], row["wo_site"], row["errMsg"], row["mpo_domain"], row["det_rmks"], row["planDate"]);
                }
                doc.Send();

                Response.Flush();
                Response.End();

                dt.Dispose();

            }
            else
            {
                Response.Redirect("EDI850QADExportExcel.aspx");
            }
        }
    }

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
                Object so_ordDate, Object rmk, Object poNbr, Object fob, Object reqDate, Object dueDate, Object poLine, Object soNbr, Object qadPart,
                Object partNbr, Object sku, Object ordQty, Object site, Object wo_site, Object errMsg, Object mpo_domain, Object det_rmks, Object planDate)
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
        xf.TextWrapRight = true;

        sheet.Cells.Add(rowIndex, 1, so_ordDate.ToString(),xf);
        sheet.Cells.Add(rowIndex, 2, rmk.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, poNbr.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, fob.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, reqDate.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, dueDate.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, poLine.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, soNbr.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, qadPart.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, partNbr.ToString(), xf);
        sheet.Cells.Add(rowIndex, 11, sku.ToString(), xf);
        sheet.Cells.Add(rowIndex, 12, ordQty.ToString(), xf);
        sheet.Cells.Add(rowIndex, 13, site.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, wo_site.ToString(), xf);
        sheet.Cells.Add(rowIndex, 15, errMsg.ToString(), xf);
        sheet.Cells.Add(rowIndex, 16, mpo_domain.ToString(), xf);
        sheet.Cells.Add(rowIndex, 17, det_rmks.ToString(), xf);
        sheet.Cells.Add(rowIndex, 18, planDate.ToString(), xf);
    }
}
