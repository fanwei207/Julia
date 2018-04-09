using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TCPNEW;

public partial class New_FixAssetSearchExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = GetDataTcp.GetFixAssetReserve(Request.QueryString["inputNo"], Request.QueryString["inputDate"], Request.QueryString["vouDate"], Request.QueryString["canZhiRate"]);

            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("固定资产信息");

                #region 设定列宽
                //资产编码列
                AppLibrary.WriteExcel.ColumnInfo fixas_no = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_no.ColumnIndexStart = 0;
                fixas_no.ColumnIndexEnd = 0;
                fixas_no.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixas_no);
                //资产名称列
                AppLibrary.WriteExcel.ColumnInfo fixas_name = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_name.ColumnIndexStart = 1;
                fixas_name.ColumnIndexEnd = 1;
                fixas_name.Width = 150 * 6000 / 164;
                sheet.AddColumnInfo(fixas_name);
                //规格列
                AppLibrary.WriteExcel.ColumnInfo fixas_spec = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_spec.ColumnIndexStart = 2;
                fixas_spec.ColumnIndexEnd = 2;
                fixas_spec.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixas_spec);
                //类型列
                AppLibrary.WriteExcel.ColumnInfo fixtp_name = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_name.ColumnIndexStart = 3;
                fixtp_name.ColumnIndexEnd = 3;
                fixtp_name.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_name);
                //详细类型列
                AppLibrary.WriteExcel.ColumnInfo fixtp_det_name = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_det_name.ColumnIndexStart = 4;
                fixtp_det_name.ColumnIndexEnd = 4;
                fixtp_det_name.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_det_name);
                //折旧年限2列
                AppLibrary.WriteExcel.ColumnInfo fixtp_lefttime = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_lefttime.ColumnIndexStart = 5;
                fixtp_lefttime.ColumnIndexEnd = 5;
                fixtp_lefttime.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_lefttime);
                //折旧年限1列
                AppLibrary.WriteExcel.ColumnInfo lefttime = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                lefttime.ColumnIndexStart = 6;
                lefttime.ColumnIndexEnd = 6;
                lefttime.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(lefttime);
                //入账公司列
                AppLibrary.WriteExcel.ColumnInfo enti_name = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                enti_name.ColumnIndexStart = 7;
                enti_name.ColumnIndexEnd = 7;
                enti_name.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(enti_name);
                //入账凭证列
                AppLibrary.WriteExcel.ColumnInfo fixas_voucher = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_voucher.ColumnIndexStart = 8;
                fixas_voucher.ColumnIndexEnd = 8;
                fixas_voucher.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixas_voucher);
                //入账日期列
                AppLibrary.WriteExcel.ColumnInfo fixas_vou_date = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_vou_date.ColumnIndexStart = 9;
                fixas_vou_date.ColumnIndexEnd = 9;
                fixas_vou_date.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixas_vou_date);
                //原值列
                AppLibrary.WriteExcel.ColumnInfo fixas_cost = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_cost.ColumnIndexStart = 10;
                fixas_cost.ColumnIndexEnd = 10;
                fixas_cost.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixas_cost);
                //月折旧列1
                AppLibrary.WriteExcel.ColumnInfo fixtp_every1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_every1.ColumnIndexStart = 11;
                fixtp_every1.ColumnIndexEnd = 11;
                fixtp_every1.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_every1);
                //累计折旧列1
                AppLibrary.WriteExcel.ColumnInfo fixtp_now1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_now1.ColumnIndexStart = 12;
                fixtp_now1.ColumnIndexEnd = 12;
                fixtp_now1.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_now1);
                //净值列1
                AppLibrary.WriteExcel.ColumnInfo fixtp_total1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_total1.ColumnIndexStart = 13;
                fixtp_total1.ColumnIndexEnd = 13;
                fixtp_total1.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_total1);
                //月折旧列2
                AppLibrary.WriteExcel.ColumnInfo fixtp_every2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_every2.ColumnIndexStart = 14;
                fixtp_every2.ColumnIndexEnd = 14;
                fixtp_every2.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_every2);
                //累计折旧列2
                AppLibrary.WriteExcel.ColumnInfo fixtp_now2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_now2.ColumnIndexStart = 15;
                fixtp_now2.ColumnIndexEnd = 15;
                fixtp_now2.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_now2);
                //净值列2
                AppLibrary.WriteExcel.ColumnInfo fixtp_total2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixtp_total2.ColumnIndexStart = 16;
                fixtp_total2.ColumnIndexEnd = 16;
                fixtp_total2.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixtp_total2);
                //供应商列
                AppLibrary.WriteExcel.ColumnInfo fixas_supplier = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_supplier.ColumnIndexStart = 17;
                fixas_supplier.ColumnIndexEnd = 17;
                fixas_supplier.Width = 150 * 6000 / 164;
                sheet.AddColumnInfo(fixas_supplier);
                //估价依据列
                AppLibrary.WriteExcel.ColumnInfo fixas_reference = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_reference.ColumnIndexStart = 18;
                fixas_reference.ColumnIndexEnd = 18;
                fixas_reference.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixas_reference);
                //设备编码列
                AppLibrary.WriteExcel.ColumnInfo fixas_code = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_code.ColumnIndexStart = 19;
                fixas_code.ColumnIndexEnd = 19;
                fixas_code.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixas_code);
                //备注列
                AppLibrary.WriteExcel.ColumnInfo fixas_comment = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_comment.ColumnIndexStart = 20;
                fixas_comment.ColumnIndexEnd = 20;
                fixas_comment.Width = 200 * 6000 / 164;
                sheet.AddColumnInfo(fixas_comment);
                //开始折旧日期1
                AppLibrary.WriteExcel.ColumnInfo firstDepreciationDate1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                firstDepreciationDate1.ColumnIndexStart = 21;
                firstDepreciationDate1.ColumnIndexEnd = 21;
                firstDepreciationDate1.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(firstDepreciationDate1);
                //开始折旧日期2
                AppLibrary.WriteExcel.ColumnInfo firstDepreciationDate2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                firstDepreciationDate2.ColumnIndexStart = 22;
                firstDepreciationDate2.ColumnIndexEnd = 22;
                firstDepreciationDate2.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(firstDepreciationDate2);

                //最初所在公司列
                AppLibrary.WriteExcel.ColumnInfo enti_name2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                enti_name2.ColumnIndexStart = 23;
                enti_name2.ColumnIndexEnd = 23;
                enti_name2.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(enti_name2);
                //最初状态列
                AppLibrary.WriteExcel.ColumnInfo fixsta_name1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixsta_name1.ColumnIndexStart = 24;
                fixsta_name1.ColumnIndexEnd = 24;
                fixsta_name1.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixsta_name1);
                //最初使用日期列
                AppLibrary.WriteExcel.ColumnInfo fixas_det_startdate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_det_startdate.ColumnIndexStart = 25;
                fixas_det_startdate.ColumnIndexEnd = 25;
                fixas_det_startdate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixas_det_startdate);
                //最初成本中心列
                AppLibrary.WriteExcel.ColumnInfo fixctc_no = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixctc_no.ColumnIndexStart = 26;
                fixctc_no.ColumnIndexEnd = 26;
                fixctc_no.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixctc_no);
                //最初成本中心列
                AppLibrary.WriteExcel.ColumnInfo fixctc_name = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixctc_name.ColumnIndexStart = 27;
                fixctc_name.ColumnIndexEnd = 27;
                fixctc_name.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixctc_name);
                //开始日期列
                AppLibrary.WriteExcel.ColumnInfo fixas_det_startdate1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_det_startdate1.ColumnIndexStart = 28;
                fixas_det_startdate1.ColumnIndexEnd = 28;
                fixas_det_startdate1.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(fixas_det_startdate1);
                //所在公司列
                AppLibrary.WriteExcel.ColumnInfo enti_name1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                enti_name1.ColumnIndexStart = 29;
                enti_name1.ColumnIndexEnd = 29;
                enti_name1.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(enti_name1);
                //成本中心列
                AppLibrary.WriteExcel.ColumnInfo fixctc_no1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixctc_no1.ColumnIndexStart = 30;
                fixctc_no1.ColumnIndexEnd = 30;
                fixctc_no1.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixctc_no1);
                //成本中心列
                AppLibrary.WriteExcel.ColumnInfo fixctc_name1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixctc_name1.ColumnIndexStart = 31;
                fixctc_name1.ColumnIndexEnd = 31;
                fixctc_name1.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixctc_name1);
                //最初报废状态
                AppLibrary.WriteExcel.ColumnInfo firstRetirementState = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                firstRetirementState.ColumnIndexStart = 32;
                firstRetirementState.ColumnIndexEnd = 32;
                firstRetirementState.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(firstRetirementState);
                //最初报废日期列
                AppLibrary.WriteExcel.ColumnInfo firstRetirementDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                firstRetirementDate.ColumnIndexStart = 33;
                firstRetirementDate.ColumnIndexEnd = 33;
                firstRetirementDate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(firstRetirementDate);
                //状态列
                AppLibrary.WriteExcel.ColumnInfo fixsta_name = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixsta_name.ColumnIndexStart = 34;
                fixsta_name.ColumnIndexEnd = 34;
                fixsta_name.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixsta_name);
                //放置地点列
                AppLibrary.WriteExcel.ColumnInfo fixas_det_site = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_det_site.ColumnIndexStart = 35;
                fixas_det_site.ColumnIndexEnd = 35;
                fixas_det_site.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(fixas_det_site);
                //责任人列
                AppLibrary.WriteExcel.ColumnInfo fixas_det_responsibler = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_det_responsibler.ColumnIndexStart = 36;
                fixas_det_responsibler.ColumnIndexEnd = 36;
                fixas_det_responsibler.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixas_det_responsibler);
                //备注列
                AppLibrary.WriteExcel.ColumnInfo fixas_det_comment = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                fixas_det_comment.ColumnIndexStart = 37;
                fixas_det_comment.ColumnIndexEnd = 37;
                fixas_det_comment.Width = 80 * 6000 / 164;
                sheet.AddColumnInfo(fixas_det_comment);
                #endregion

                int rowIndex = 1;
                PrintExcel(doc, sheet, rowIndex, 
                            "资产编码", "资产名称", "规格", "类型", "详细类型",
                            "折旧年限1", "折旧年限2", "入账公司", "入账凭证", "入账日期",
                            "原值", "月折旧1", "累计折旧1", "净值1", "月折旧2",
                            "累计折旧2", "净值2", "供应商", "估价依据", "设备编码",
                            "备注", "开始折旧日期1", "开始折旧日期2", "最初所在公司", "最初状态", 
                            "最初使用日期","最初成本中心", "最初成本中心", "最初报废日期", "最初报废状态",
                            "开始日期", "所在公司", "成本中心", "成本中心", "状态",
                            "放置地点", "责任人", "备注");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex, 
                             row["fixas_no"], row["fixas_name"], row["fixas_spec"], row["fixtp_name"], row["fixtp_det_name"]
                             , row["fixtp_lefttime"], row["lefttime"], row["enti_name"], row["fixas_voucher"], row["fixas_vou_date"]
                             , row["fixas_cost"], row["fixtp_every1"], row["fixtp_now1"], row["fixtp_total1"], row["fixtp_every2"]
                             , row["fixtp_now2"], row["fixtp_total2"], row["fixas_supplier"], row["fixas_reference"], row["fixas_code"]
                             , row["fixas_comment"], row["firstDepreciationDate1"], row["firstDepreciationDate2"], row["enti_name2"], row["fixsta_name1"]
                             , row["fixas_det_startdate"], row["fixctc_no"], row["fixctc_name"], row["firstRetirementDate2"], row["firstRetirementState2"]
                             , row["fixas_det_startdate1"], row["enti_name1"], row["fixctc_no1"], row["fixctc_name1"], row["fixsta_name"]
                             , row["fixas_det_site"], row["fixas_det_responsibler"], row["fixas_det_comment"]);
                }
                doc.Send();

                Response.Flush();
                Response.End();
                dt.Dispose();

            }
            else
            {
                Response.Redirect("FixAssetSearch.aspx");
            }
        }

    }
    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex
                               , Object fixas_no, Object fixas_name, Object fixas_spec, Object fixtp_name, Object fixtp_det_name
                               , Object fixtp_lefttime, Object lefttime, Object enti_name, Object fixas_voucher, Object fixas_vou_date
                               , Object fixas_cost, Object fixtp_every1, Object fixtp_now1, Object fixtp_total1, Object fixtp_every2
                               , Object fixtp_now2, Object fixtp_total2, Object fixas_supplier, Object fixas_reference, Object fixas_code
                               , Object fixas_comment, Object firstDepreciationDate1 , Object firstDepreciationDate2, Object enti_name2, Object fixsta_name1
                               , Object fixas_det_startdate, Object fixctc_no, Object fixctc_name, Object firstRetirementDate2, Object firstRetirementState2
                               , Object fixas_det_startdate1, Object enti_name1, Object fixctc_no1, Object fixctc_name1, Object fixsta_name
                               , Object fixas_det_site, Object fixas_det_responsibler, Object fixas_det_comment)
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

        sheet.Cells.Add(rowIndex, 1, fixas_no.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, fixas_name.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, fixas_spec.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, fixtp_name.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, fixtp_det_name.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, fixtp_lefttime.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, lefttime.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, enti_name.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, fixas_voucher.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, string.Format("{0:yyyy-MM-dd}", fixas_vou_date), xf);
        sheet.Cells.Add(rowIndex, 11, fixas_cost.ToString(), xf);
        sheet.Cells.Add(rowIndex, 12, fixtp_every1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 13, fixtp_now1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, fixtp_total1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 15, fixtp_every2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 16, fixtp_now2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 17, fixtp_total2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 18, fixas_supplier.ToString(), xf);
        sheet.Cells.Add(rowIndex, 19, fixas_reference.ToString(), xf);
        sheet.Cells.Add(rowIndex, 20, fixas_code.ToString(), xf);
        sheet.Cells.Add(rowIndex, 21, fixas_comment.ToString(), xf);

        sheet.Cells.Add(rowIndex, 22, string.Format("{0:yyyy-MM-dd}", firstDepreciationDate1), xf);
        sheet.Cells.Add(rowIndex, 23, string.Format("{0:yyyy-MM-dd}", firstDepreciationDate2), xf);

        sheet.Cells.Add(rowIndex, 24, enti_name2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 25, fixsta_name1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 26, string.Format("{0:yyyy-MM-dd}", fixas_det_startdate), xf);
        sheet.Cells.Add(rowIndex, 27, fixctc_no.ToString(), xf);
        sheet.Cells.Add(rowIndex, 28, fixctc_name.ToString(), xf);

        sheet.Cells.Add(rowIndex, 29, string.Format("{0:yyyy-MM-dd}", firstRetirementDate2), xf);
        sheet.Cells.Add(rowIndex, 30, firstRetirementState2.ToString(), xf);

        sheet.Cells.Add(rowIndex, 31, string.Format("{0:yyyy-MM-dd}", fixas_det_startdate1), xf);
        sheet.Cells.Add(rowIndex, 32, enti_name1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 33, fixctc_no1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 34, fixctc_name1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 35, fixsta_name.ToString(), xf);
        sheet.Cells.Add(rowIndex, 36, fixas_det_site.ToString(), xf);
        sheet.Cells.Add(rowIndex, 37, fixas_det_responsibler.ToString(), xf);
        sheet.Cells.Add(rowIndex, 38, fixas_det_comment.ToString(), xf);


    }


}
