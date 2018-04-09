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
using System.Collections.Generic;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using QCProgress;
using CommClass;
using System.IO;

public partial class plan_bigOrderExcel1 : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ToExcel();
    }

    public void ToExcel()
    {
        string nbr = Request.QueryString["nbr"].ToString().Trim();
        string soNbr1 = Request.QueryString["so1"].ToString().Trim();
        string soNbr2 = Request.QueryString["so2"].ToString().Trim();
        string woNbr1 = Request.QueryString["wo1"].ToString().Trim();
        string woNbr2 = Request.QueryString["wo2"].ToString().Trim();
        string plandate1 = Request.QueryString["pd1"].ToString().Trim();
        string plandate2 = Request.QueryString["pd2"].ToString().Trim();
        string plandatec1 = Request.QueryString["pdc1"].ToString().Trim();
        string plandatec2 = Request.QueryString["pdc2"].ToString().Trim();
        string type = Request.QueryString["ty"].ToString().Trim();
        bool unaccount = Convert.ToBoolean(Request.QueryString["ua"].ToString().Trim());
        bool unplan = Convert.ToBoolean(Request.QueryString["up"].ToString().Trim());
        int unApprove = Convert.ToInt32(Request.QueryString["uap"].ToString().Trim());
        string createdName = Request.QueryString["bc"].ToString().Trim();
        string site = Request.QueryString["st"].ToString().Trim();
        bool isDiff = Convert.ToBoolean(Request.QueryString["di"].ToString().Trim());
        int errType = Convert.ToInt32(Request.QueryString["et"].ToString().Trim());

        AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
        doc.FileName = "excel.xls";
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

        //订单号列
        AppLibrary.WriteExcel.ColumnInfo column1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column1.ColumnIndexStart = 0;
        column1.ColumnIndexEnd = 0;
        column1.Width = 120 * 6000 / 164;
        sheet1.AddColumnInfo(column1);

        //接单时间列
        AppLibrary.WriteExcel.ColumnInfo column2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column2.ColumnIndexStart = 1;
        column2.ColumnIndexStart = 1;
        column2.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column2);

        //客户
        AppLibrary.WriteExcel.ColumnInfo column3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column3.ColumnIndexStart = 2;
        column3.ColumnIndexStart = 2;
        column3.Width = 150 * 6000 / 164;
        sheet1.AddColumnInfo(column3);

        //产品名称
        AppLibrary.WriteExcel.ColumnInfo column4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column4.ColumnIndexStart = 3;
        column4.ColumnIndexStart = 3;
        column4.Width = 250 * 6000 / 164;
        sheet1.AddColumnInfo(column4);

        //销售单
        AppLibrary.WriteExcel.ColumnInfo column5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column5.ColumnIndexStart = 4;
        column5.ColumnIndexStart = 4;
        column5.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column5);

        //行号
        AppLibrary.WriteExcel.ColumnInfo column6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column6.ColumnIndexStart = 5;
        column6.ColumnIndexStart = 5;
        column6.Width = 50 * 6000 / 164;
        sheet1.AddColumnInfo(column6);

        //QAD
        AppLibrary.WriteExcel.ColumnInfo column7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column7.ColumnIndexStart = 6;
        column7.ColumnIndexStart = 6;
        column7.Width = 150 * 6000 / 164;
        sheet1.AddColumnInfo(column7);

        //订单数量
        AppLibrary.WriteExcel.ColumnInfo column8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column8.ColumnIndexStart = 7;
        column8.ColumnIndexStart = 7;
        column8.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column8);

        //出运数量
        AppLibrary.WriteExcel.ColumnInfo column9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column9.ColumnIndexStart = 8;
        column9.ColumnIndexStart = 8;
        column9.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column9);

        //订单日期
        AppLibrary.WriteExcel.ColumnInfo column10 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column10.ColumnIndexStart = 9;
        column10.ColumnIndexStart = 9;
        column10.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column10);

        //截止日期
        AppLibrary.WriteExcel.ColumnInfo column11 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column11.ColumnIndexStart = 10;
        column11.ColumnIndexStart = 10;
        column11.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column11);

        //加工单
        AppLibrary.WriteExcel.ColumnInfo column12 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column12.ColumnIndexStart = 11;
        column12.ColumnIndexStart = 11;
        column12.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column12);

        //ID
        AppLibrary.WriteExcel.ColumnInfo column13 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column13.ColumnIndexStart = 12;
        column13.ColumnIndexStart = 12;
        column13.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column13);

        //类型
        AppLibrary.WriteExcel.ColumnInfo column14 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column14.ColumnIndexStart = 13;
        column14.ColumnIndexStart = 13;
        column14.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column14);

        //工单数量(套)
        AppLibrary.WriteExcel.ColumnInfo column15 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column15.ColumnIndexStart = 14;
        column15.ColumnIndexStart = 14;
        column15.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column15);

        //工单数量(只)
        AppLibrary.WriteExcel.ColumnInfo column16 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column16.ColumnIndexStart = 15;
        column16.ColumnIndexStart = 15;
        column16.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column16);

        //完工入库(套)
        AppLibrary.WriteExcel.ColumnInfo column17 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column17.ColumnIndexStart = 16;
        column17.ColumnIndexStart = 16;
        column17.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column17);

        //生产线
        AppLibrary.WriteExcel.ColumnInfo column18 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column18.ColumnIndexStart = 17;
        column18.ColumnIndexStart = 17;
        column18.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column18);

        //下达日期
        AppLibrary.WriteExcel.ColumnInfo column19 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column19.ColumnIndexStart = 18;
        column19.ColumnIndexStart = 18;
        column19.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column19);

        //计划日期
        AppLibrary.WriteExcel.ColumnInfo column20 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column20.ColumnIndexStart = 19;
        column20.ColumnIndexStart = 19;
        column20.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column20);

        //变更日期
        AppLibrary.WriteExcel.ColumnInfo column21 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column21.ColumnIndexStart = 20;
        column21.ColumnIndexStart = 20;
        column21.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column21);

        //原因
        AppLibrary.WriteExcel.ColumnInfo column22 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column22.ColumnIndexStart = 21;
        column22.ColumnIndexStart = 21;
        column22.Width = 300 * 6000 / 164;
        sheet1.AddColumnInfo(column22);

        //制地
        AppLibrary.WriteExcel.ColumnInfo column23 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column23.ColumnIndexStart = 22;
        column23.ColumnIndexStart = 22;
        column23.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column23);

        //状态
        AppLibrary.WriteExcel.ColumnInfo column24 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column24.ColumnIndexStart = 23;
        column24.ColumnIndexStart = 23;
        column24.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column24);

        //创建人
        AppLibrary.WriteExcel.ColumnInfo column25 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column25.ColumnIndexStart = 24;
        column25.ColumnIndexStart = 24;
        column25.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column25);

        //整箱与散货
        AppLibrary.WriteExcel.ColumnInfo column26 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column26.ColumnIndexStart = 25;
        column26.ColumnIndexStart = 25;
        column26.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column26);

        //备选1
        AppLibrary.WriteExcel.ColumnInfo column27 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column27.ColumnIndexStart = 26;
        column27.ColumnIndexStart = 26;
        column27.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column27);

        //备选2
        AppLibrary.WriteExcel.ColumnInfo column28 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column28.ColumnIndexStart = 27;
        column28.ColumnIndexStart = 27;
        column28.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column28);

        //客户名称
        AppLibrary.WriteExcel.ColumnInfo column29 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column29.ColumnIndexStart = 28;
        column29.ColumnIndexStart = 28;
        column29.Width = 250 * 6000 / 164;
        sheet1.AddColumnInfo(column29);

        //自动、手动
        AppLibrary.WriteExcel.ColumnInfo column30 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column30.ColumnIndexStart = 29;
        column30.ColumnIndexStart = 29;
        column30.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column30);

        //产品类型
        AppLibrary.WriteExcel.ColumnInfo column31 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column31.ColumnIndexStart = 30;
        column31.ColumnIndexStart = 30;
        column31.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column31);

        //出运信息
        AppLibrary.WriteExcel.ColumnInfo column32 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column32.ColumnIndexStart = 31;
        column32.ColumnIndexStart = 31;
        column32.Width = 400 * 6000 / 164;
        sheet1.AddColumnInfo(column32);


        #endregion 

        cells1.Add(1, 1, "订单号", XFstyle1);
        cells1.Add(1, 2, "接单时间", XFstyle1);
        cells1.Add(1, 3, "客户", XFstyle1);
        cells1.Add(1, 4, "产品名称", XFstyle1);
        cells1.Add(1, 5, "销售单", XFstyle1);
        cells1.Add(1, 6, "行号", XFstyle1);
        cells1.Add(1, 7, "QAD", XFstyle1);
        cells1.Add(1, 8, "订单数量", XFstyle1);
        cells1.Add(1, 9, "出运数量", XFstyle1);
        cells1.Add(1, 10, "订单日期", XFstyle1);
        cells1.Add(1, 11, "截止日期", XFstyle1);
        cells1.Add(1, 12, "加工单", XFstyle1);
        cells1.Add(1, 13, "ID", XFstyle1);
        cells1.Add(1, 14, "类型", XFstyle1);
        cells1.Add(1, 15, "工单数量(套)", XFstyle1);
        cells1.Add(1, 16, "工单数量(只)", XFstyle1);
        cells1.Add(1, 17, "完工入库(套)", XFstyle1);
        cells1.Add(1, 18, "生产线", XFstyle1);
        cells1.Add(1, 19, "下达日期", XFstyle1);
        cells1.Add(1, 20, "计划日期", XFstyle1);
        cells1.Add(1, 21, "变更日期", XFstyle1);
        cells1.Add(1, 22, "原因", XFstyle1);
        cells1.Add(1, 23, "制地", XFstyle1);
        cells1.Add(1, 24, "状态", XFstyle1);
        cells1.Add(1, 25, "创建人", XFstyle1);
        cells1.Add(1, 26, "整箱与散货", XFstyle1);
        cells1.Add(1, 27, "备注1", XFstyle1);
        cells1.Add(1, 28, "备注2", XFstyle1);
        cells1.Add(1, 29, "客户名称", XFstyle1);
        cells1.Add(1, 30, "自动/手动", XFstyle1);
        cells1.Add(1, 31, "产品类型", XFstyle1);
        cells1.Add(1, 32, "出运信息", XFstyle1);


        DataTable dt1 = null;

        dt1 = GetBoCheck(nbr, soNbr1, soNbr2, woNbr1, woNbr2, plandate1, plandate2, plandatec1, plandatec2, type, unaccount, unplan, unApprove, createdName, site, isDiff, errType);
       
        int i = 1;
        for (int n = 0; n < dt1.Rows.Count; n++)
        {
            i++;
            switch(dt1.Rows[n]["bo_status"].ToString().Trim())
            {
                case "0":
                    cells1.Add(i, 1, dt1.Rows[n].IsNull("ord_nbr") == true ? "" : dt1.Rows[n]["ord_nbr"].ToString().Trim(), XFstyle1);
                    break;
                case "1":
                    cells1.Add(i, 1, dt1.Rows[n].IsNull("ord_nbr") == true ? "" : dt1.Rows[n]["ord_nbr"].ToString().Trim(), XFstyle3);
                    break;
                case "2":
                    cells1.Add(i, 1, dt1.Rows[n].IsNull("ord_nbr") == true ? "" : dt1.Rows[n]["ord_nbr"].ToString().Trim(), XFstyle2);
                    break;
                case "3":
                    cells1.Add(i, 1, dt1.Rows[n].IsNull("ord_nbr") == true ? "" : dt1.Rows[n]["ord_nbr"].ToString().Trim(), XFstyle4);
                    break;
                default:
                    cells1.Add(i, 1, dt1.Rows[n].IsNull("ord_nbr") == true ? "" : dt1.Rows[n]["ord_nbr"].ToString().Trim(), XFstyle1);
                    break;
            }
            cells1.Add(i, 2, dt1.Rows[n].IsNull("PoRecDate") == true ? "" : dt1.Rows[n]["PoRecDate"], XFstyle1);
            cells1.Add(i, 3, dt1.Rows[n]["cusCode"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 4, dt1.Rows[n]["partNbr"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 5, dt1.Rows[n]["so_nbr"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 6, dt1.Rows[n].IsNull("det_line") == true ? "" : dt1.Rows[n]["det_line"], XFstyle1);
            cells1.Add(i, 7, dt1.Rows[n]["sod_part"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 8, dt1.Rows[n].IsNull("sod_qty_ord") == true ? "" : dt1.Rows[n]["sod_qty_ord"], XFstyle1);
            cells1.Add(i, 9, dt1.Rows[n].IsNull("sod_qty_ship") == true ? "" : dt1.Rows[n]["sod_qty_ship"], XFstyle1);
            cells1.Add(i, 10, dt1.Rows[n].IsNull("so_ord_date") == true ? "" : dt1.Rows[n]["so_ord_date"], XFstyle1);
            cells1.Add(i, 11, dt1.Rows[n].IsNull("sod_due_date") == true ? "" : dt1.Rows[n]["sod_due_date"], XFstyle1);
            cells1.Add(i, 12, dt1.Rows[n]["wo_nbr"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 13, dt1.Rows[n]["wo_lot"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 14, dt1.Rows[n]["bo_type"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 15, dt1.Rows[n].IsNull("wo_qty_ord") == true ? "" : dt1.Rows[n]["wo_qty_ord"], XFstyle1);
            cells1.Add(i, 16, dt1.Rows[n].IsNull("wo_qty_ord1") == true ? "" : dt1.Rows[n]["wo_qty_ord1"], XFstyle1);
            cells1.Add(i, 17, dt1.Rows[n].IsNull("wo_qty_comp") == true ? "" : dt1.Rows[n]["wo_qty_comp"], XFstyle1);
            cells1.Add(i, 18, dt1.Rows[n]["wo_line"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 19, dt1.Rows[n].IsNull("wo_rel_date") == true ? "" : dt1.Rows[n]["wo_rel_date"], XFstyle1);
            cells1.Add(i, 20, dt1.Rows[n].IsNull("wo_plandate") == true ? "" : dt1.Rows[n]["wo_plandate"], XFstyle1);
            cells1.Add(i, 21, dt1.Rows[n].IsNull("bo_plandateC") == true ? "" : dt1.Rows[n]["bo_plandateC"], XFstyle1);
            cells1.Add(i, 22, dt1.Rows[n]["bo_reason"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 23, dt1.Rows[n].IsNull("wo_site") == true ? "" : dt1.Rows[n]["wo_site"], XFstyle1);
            cells1.Add(i, 24, dt1.Rows[n]["wo_status"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 25, dt1.Rows[n]["bo_createdName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 26, dt1.Rows[n]["bo_undefine1"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 27, dt1.Rows[n]["bo_undefine2"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 28, dt1.Rows[n]["bo_undefine3"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 29, dt1.Rows[n]["cusName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 30, dt1.Rows[n].IsNull("sod_automanul") == true ? "" : dt1.Rows[n]["sod_automanul"], XFstyle1);
            cells1.Add(i, 31, dt1.Rows[n].IsNull("sod_type") == true ? "" : dt1.Rows[n]["sod_type"], XFstyle1);
            cells1.Add(i, 32, dt1.Rows[n].IsNull("sod_sid_info") == true ? "" : dt1.Rows[n]["sod_sid_info"], XFstyle1);

        }
        dt1.Reset();

        if (errType == 1)
        {
            GroupRows(sheet1, 1, 5, 6);
            GroupRows(sheet1, 2, 5, 6);
            GroupRows(sheet1, 3, 5, 6);
            GroupRows(sheet1, 4, 5, 6);
            GroupRows(sheet1, 5, 5, 6);
            GroupRows(sheet1, 6, 5, 6);
            GroupRows(sheet1, 7, 5, 6);
            GroupRows(sheet1, 8, 5, 6);
            GroupRows(sheet1, 9, 5, 6);
            GroupRows(sheet1, 10, 5, 6);
            GroupRows(sheet1, 11, 5, 6);
        }
        else if(errType == 2)
        {
            GroupRows(sheet1, 12, 12, 13);
            GroupRows(sheet1, 13, 12, 13);
            GroupRows(sheet1, 14, 12, 13);
            GroupRows(sheet1, 15, 12, 13);
            GroupRows(sheet1, 16, 12, 13);
            GroupRows(sheet1, 17, 12, 13);
            GroupRows(sheet1, 18, 12, 13);
            GroupRows(sheet1, 19, 12, 13);
            GroupRows(sheet1, 20, 12, 13);
            GroupRows(sheet1, 21, 12, 13);
            GroupRows(sheet1, 23, 12, 13);
            GroupRows(sheet1, 24, 12, 13);
        }

        doc.Send();
        Response.Flush();
        Response.End();
    }

    private DataTable GetBoCheck(string nbr, string soNbr1, string soNbr2, string woNbr1, string woNbr2, string plandate1, string plandate2, string plandateC1, string plandateC2, string type, bool unaccount, bool unplan, int unApprove, string createdName, string site, bool isDiff, int errType)
    {
        SqlParameter[] param = new SqlParameter[17];
        param[0] = new SqlParameter("@ord_nbr", nbr);
        param[1] = new SqlParameter("@so_nbr1", soNbr1);
        param[2] = new SqlParameter("@so_nbr2", soNbr2);
        param[3] = new SqlParameter("@wo_nbr1", woNbr1);
        param[4] = new SqlParameter("@wo_nbr2", woNbr2);
        param[5] = new SqlParameter("@ord_plandate1", plandate1);
        param[6] = new SqlParameter("@ord_plandate2", plandate2);
        param[7] = new SqlParameter("@ord_plandateC1", plandateC1);
        param[8] = new SqlParameter("@ord_plandateC2", plandateC2);
        param[9] = new SqlParameter("@wo_type", type);
        param[10] = new SqlParameter("@isUnAccount", unaccount);
        param[11] = new SqlParameter("@isUnPlan", unplan);
        param[12] = new SqlParameter("@isUnApprove", unApprove);
        param[13] = new SqlParameter("@bo_createdName", createdName);
        param[14] = new SqlParameter("@wo_site", site);
        param[15] = new SqlParameter("@isDiff", isDiff);
        param[16] = new SqlParameter("@errType", errType);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkBo", param).Tables[0];
    }

    private void GroupRows(AppLibrary.WriteExcel.Worksheet Sheet, int CellSpan, int CellCondition1, int CellCondition2)
    {
        int rowSpanNum = 0;
        AppLibrary.WriteExcel.Cells cells = Sheet.Cells;
        for (int i = 2; i <= Sheet.Rows.Count;  i = i + rowSpanNum)
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
