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

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using CommClass;
using System.Collections;
using System.Collections.Generic;

using System.Security.Principal;
using System.Collections.Generic;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


public partial class Purchase_RP_ldExcel : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ToExcel();
        this.Redirect("RP_ldList.aspx");
    }
    public SqlDataReader GetProductStruApplyMstr(string id)
    {
        try
        {
            string strName = "sp_RP_selectldmstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetProductStruApplyDetail(string id)
    {
        try
        {
            string strName = "sp_RP_selectldDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public void ToExcel()
    {
        string id = Request.QueryString["id"].ToString().Trim();
        string RP_no="";
        string mark = "";
        string createname = "";
        string RP_date = "";
        string ModifyName = "";
        string ModifyDate = "";
        string leaderdate = "";
        string leadername = "";
        IDataReader reader = GetProductStruApplyMstr(id);
        if (reader.Read())
        {
            RP_no = reader["RP_no"].ToString();
          
            mark = reader["mark"].ToString();

            createname = reader["createname"].ToString();
            RP_date = reader["RP_date"].ToString();
            ModifyName = reader["ModifyName"].ToString();
            ModifyDate = reader["ModifyDate"].ToString();
            leaderdate = reader["leaderdate"].ToString();
            leadername = reader["leadername"].ToString();
        }
        reader.Close();

        XSSFWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

       // IList<ExcelTitle> ItemList = GetExcelTitles(EXTitle);
      //  int total = ItemList.Count;

        DataTable dt = GetProductStruApplyDetail(id); ;

        //头栏样式
        ICellStyle styleHeader = SetHeaderStyle2007(workbook);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(0);
       // SetColumnTitleAndStyle2007(workbook, sheet, ItemList, dt, styleHeader, rowHeader, fullDateFormat);

        //明细样式
        ICellStyle styleDet = SetDetStyle2007(workbook);

        //写明细数据
        // SetDetailsValue2007(workbook, sheet, total, dt, 1, styleDet, fullDateFormat);

        #region
        ICell cell = rowHeader.CreateCell(0);
        cell.CellStyle = styleHeader;
        cell.SetCellValue("库存领料单");



        for (int i = 1; i < 8; i++)
        {
            cell = rowHeader.CreateCell(i);
            cell.CellStyle = styleHeader;
            cell.SetCellValue("");
        }

      

        


        sheet.AddMergedRegion(new CellRangeAddress(0,0,0,7));
        IRow row = sheet.CreateRow(1);

        cell = row.CreateCell(0);
        cell.SetCellValue("申请单号");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(1);
        cell.SetCellValue(RP_no);
        cell.CellStyle = styleDet;

        cell = row.CreateCell(2);
        cell.SetCellValue("申请人");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(3);
        cell.SetCellValue(createname);
        cell.CellStyle = styleDet;

        cell = row.CreateCell(4);
        cell.SetCellValue("申请时间");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(5);
        cell.SetCellValue(RP_date);
        cell.CellStyle = styleDet;


        for (int i = 6; i < 8; i++)
        {
            cell = row.CreateCell(i);
            cell.CellStyle = styleHeader;
            cell.SetCellValue("");
        }
        sheet.AddMergedRegion(new CellRangeAddress(1, 1, 6, 7));


         row = sheet.CreateRow(2);
      

        cell = row.CreateCell(0);
        cell.SetCellValue("部门领导");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(1);
        cell.SetCellValue(leadername);
        cell.CellStyle = styleDet;

        cell = row.CreateCell(2);
        cell.SetCellValue("确认时间");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(3);
        cell.SetCellValue(leaderdate);
        cell.CellStyle = styleDet;

        cell = row.CreateCell(4);
        cell.SetCellValue("业务部门");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(5);
        cell.SetCellValue(ModifyName);
        cell.CellStyle = styleDet;

        cell = row.CreateCell(6);
        cell.SetCellValue("确认时间");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(7);
        cell.SetCellValue(ModifyDate);
        cell.CellStyle = styleDet;

        row = sheet.CreateRow(3);//3
       
        cell = row.CreateCell(0);
        cell.SetCellValue("备注");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(1);
        cell.SetCellValue(mark);
        cell.CellStyle = styleDet;

        for (int i = 2; i < 8; i++)
        {
            cell = row.CreateCell(i);
            cell.CellStyle = styleHeader;
            cell.SetCellValue("");
        }
       

        sheet.AddMergedRegion(new CellRangeAddress(3, 3, 1, 7));

        sheet.AddMergedRegion(new CellRangeAddress(4, 4, 0, 7));

        row = sheet.CreateRow(5);
        cell = row.CreateCell(0);
        cell.SetCellValue("行号");
        cell.CellStyle = styleHeader;
            
        cell = row.CreateCell(1);
        cell.SetCellValue("QAD");
        cell.CellStyle = styleHeader;

        cell = row.CreateCell(2);
        cell.SetCellValue("描述");
        cell.CellStyle = styleHeader;

        for (int i = 3; i < 7; i++)
        {
            cell = row.CreateCell(i);
            cell.CellStyle = styleHeader;
            cell.SetCellValue("");
        }
        sheet.AddMergedRegion(new CellRangeAddress(5, 5, 2, 6));
        cell = row.CreateCell(7);
        cell.SetCellValue("数量");
        cell.CellStyle = styleHeader;


        #endregion


        if (dt.Columns.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int j = i + 6;
                 row = sheet.CreateRow(j);

               
                    cell = row.CreateCell(0);
                    cell.SetCellValue(dt.Rows[i]["line"]);
                    cell.CellStyle = styleHeader;
                    cell = row.CreateCell(1);
                    cell.SetCellValue(dt.Rows[i]["QAD"]);
                    cell.CellStyle = styleDet;
                    cell = row.CreateCell(2);
                    cell.SetCellValue(dt.Rows[i]["description"]);
                    cell.CellStyle = styleDet;

                    for (int ii = 3; ii < 7; ii++)
                    {
                        cell = row.CreateCell(ii);
                        cell.CellStyle = styleDet;
                        cell.SetCellValue("");
                    }
                    sheet.AddMergedRegion(new CellRangeAddress(j, j, 2, 6));

                    cell = row.CreateCell(7);
                    cell.SetCellValue(dt.Rows[i]["num"]);
                    cell.CellStyle = styleHeader;
            }
        }

        //sheet.AutoSizeColumn(0);  //自适应宽度
        //sheet.AutoSizeColumn(1);  //自适应宽度
        sheet.SetColumnWidth(0, 105 * 6000 / 164);
        sheet.SetColumnWidth(1, 105 * 6000 / 164);
        sheet.SetColumnWidth(2, 105 * 6000 / 164);
        sheet.SetColumnWidth(3, 105 * 6000 / 164);
        sheet.SetColumnWidth(4, 105 * 6000 / 164);
        sheet.SetColumnWidth(5, 105 * 6000 / 164);
        sheet.SetColumnWidth(6, 105 * 6000 / 164);
        sheet.SetColumnWidth(7, 105 * 6000 / 164);
       // dt.Reset();

        string _localFileName = string.Format("{0}.xlsx", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.ToArray().Length);
            localFile.Dispose();
            ms.Flush();
            //ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");



    #region
        
        //string vend ="123";// Request.QueryString["vend"].ToString().Trim();
        //string nbr = "123";//Request.QueryString["nbr"].ToString().Trim();
        //string delivery = "123";//Request.QueryString["del"].ToString().Trim();
        //string InspDateStart = "";//Request.QueryString["dd1"].ToString().Trim();
        //string InspDateEnd ="";// Request.QueryString["dd2"].ToString().Trim();
        //string qcDateStart ="";// Request.QueryString["qd1"].ToString().Trim();
        //string qcDateEnd = "";//Request.QueryString["qd2"].ToString().Trim();
        //string RcpDateStart = "";//Request.QueryString["dd3"].ToString().Trim();
        //string RcpDateEnd = "";//Request.QueryString["dd4"].ToString().Trim();

        //AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
        //doc.FileName = "excel.xls";
        //string SheetName = string.Empty;

        ////Sheet1内容
        //SheetName = "excel";
        //AppLibrary.WriteExcel.Worksheet sheet1 = doc.Workbook.Worksheets.Add(SheetName);
        //AppLibrary.WriteExcel.Cells cells1 = sheet1.Cells;

        //#region//样式1白底
        //AppLibrary.WriteExcel.XF XFstyle1 = doc.NewXF();
        //XFstyle1.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        //XFstyle1.Font.FontName = "宋体";
        //XFstyle1.UseMisc = true;
        //XFstyle1.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;
        //XFstyle1.Font.Bold = false;

        ////边框线
        //XFstyle1.BottomLineStyle = 1;
        //XFstyle1.LeftLineStyle = 1;
        //XFstyle1.TopLineStyle = 1;
        //XFstyle1.RightLineStyle = 1;

        //XFstyle1.UseBorder = true;
        //XFstyle1.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
        //XFstyle1.PatternColor = AppLibrary.WriteExcel.Colors.White;
        //XFstyle1.Pattern = 1;
        //#endregion

        //#region//Sheet1列宽控制
        ////供应商
        //AppLibrary.WriteExcel.ColumnInfo column1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        //column1.ColumnIndexStart = 0;
        //column1.ColumnIndexEnd = 0;
        //column1.Width = 120 * 6000 / 164;
        //sheet1.AddColumnInfo(column1);

        ////送货单
        //AppLibrary.WriteExcel.ColumnInfo column2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        //column2.ColumnIndexStart = 1;
        //column2.ColumnIndexStart = 1;
        //column2.Width = 120 * 6000 / 164;
        //sheet1.AddColumnInfo(column2);

        ////采购单
        //AppLibrary.WriteExcel.ColumnInfo column3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        //column3.ColumnIndexStart = 2;
        //column3.ColumnIndexStart = 2;
        //column3.Width = 120 * 6000 / 164;
        //sheet1.AddColumnInfo(column3);

        ////入厂日期
        //AppLibrary.WriteExcel.ColumnInfo column4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        //column4.ColumnIndexStart = 3;
        //column4.ColumnIndexStart = 3;
        //column4.Width = 150 * 6000 / 164;
        //sheet1.AddColumnInfo(column4);

        ////入待检库日期
        //AppLibrary.WriteExcel.ColumnInfo column5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        //column5.ColumnIndexStart = 4;
        //column5.ColumnIndexStart = 4;
        //column5.Width = 150 * 6000 / 164;
        //sheet1.AddColumnInfo(column5);

        ////质检日期
        //AppLibrary.WriteExcel.ColumnInfo column6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        //column6.ColumnIndexStart = 5;
        //column6.ColumnIndexStart = 5;
        //column6.Width = 150 * 6000 / 164;
        //sheet1.AddColumnInfo(column6);

        ////入成品库日期
        //AppLibrary.WriteExcel.ColumnInfo column7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        //column7.ColumnIndexStart = 6;
        //column7.ColumnIndexStart = 6;
        //column7.Width = 150 * 6000 / 164;
        //sheet1.AddColumnInfo(column7);

        //#endregion
      
        //cells1.Add(1, 1, "供应商", XFstyle1);
        //cells1.Add(1, 2, "送货单", XFstyle1);
        //cells1.Add(1, 3, "原始单据", XFstyle1);
        //cells1.Add(1, 4, "采购单", XFstyle1);
        //cells1.Add(1, 5, "入厂日期", XFstyle1);
        //cells1.Add(1, 6, "入待检库日期", XFstyle1);
        //cells1.Add(1, 7, "操作人", XFstyle1);
        //cells1.Add(1, 8, "质检日期", XFstyle1);
        //cells1.Add(1, 9, "入成品库日期", XFstyle1);
        //cells1.Add(1, 10, "域", XFstyle1);
        //cells1.Add(1, 11, "地点", XFstyle1);
        //cells1.Add(1, 12, "进厂域", XFstyle1);
        //cells1.Merge(1, 1, 1, 4);
        //cells1.Add(2, 1, "供应商", XFstyle1);
        //cells1.Add(2, 2, "送货单", XFstyle1);
        //cells1.Add(2, 3, "原始单据", XFstyle1);
        //cells1.Add(2, 4, "采购单", XFstyle1);
        //cells1.Add(2, 5, "入厂日期", XFstyle1);
        //cells1.Add(2, 6, "入待检库日期", XFstyle1);
        //cells1.Add(2, 7, "操作人", XFstyle1);
        //cells1.Add(2, 8, "质检日期", XFstyle1);
        //cells1.Add(2, 9, "入成品库日期", XFstyle1);
        //cells1.Add(2, 10, "域", XFstyle1);
        //cells1.Add(2, 11, "地点", XFstyle1);
        //cells1.Add(2, 12, "进厂域", XFstyle1);

        //DataTable dt = selectDelivery(vend, nbr, delivery, InspDateStart, InspDateEnd, qcDateStart, qcDateEnd, RcpDateStart, RcpDateEnd).Tables[0];

        //int i = 1;
        //for (int n = 0; n < dt.Rows.Count; n++)
        //{
        //    i++;
        //    cells1.Add(i, 1, dt.Rows[n]["vend"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 2, dt.Rows[n]["prh_receiver"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 3, dt.Rows[n]["prh_ps_nbr"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 4, dt.Rows[n]["prh_nbr"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 5, dt.Rows[n].IsNull("prh_rcp_date") == true ? "" : dt.Rows[n]["prh_rcp_date"], XFstyle1);
        //    cells1.Add(i, 6, dt.Rows[n]["prh_insp_date"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 7, dt.Rows[n]["tr_userid"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 8, dt.Rows[n].IsNull("prh_qc_date") == true ? "" : dt.Rows[n]["prh_qc_date"], XFstyle1);
        //    cells1.Add(i, 9, dt.Rows[n].IsNull("prh_inv_date") == true ? "" : dt.Rows[n]["prh_inv_date"], XFstyle1);
        //    cells1.Add(i, 10, dt.Rows[n]["prh_domain"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 11, dt.Rows[n]["prh_site"].ToString().Trim(), XFstyle1);
        //    cells1.Add(i, 12, dt.Rows[n]["prh_rcp_domain"].ToString().Trim(), XFstyle1);
        //}
        //dt.Reset();

        //doc.Send();
        //Response.Flush();
        //Response.End();
        #endregion
    }
    private ICellStyle SetDetStyle2007(IWorkbook workbook)
    {
        ICellStyle styleDet = workbook.CreateCellStyle();
        //styleDet.Alignment = HorizontalAlignment.Center;//居中对齐

        styleDet.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
        styleDet.FillPattern = FillPattern.SolidForeground;

        styleDet.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleDet.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleDet.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleDet.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontDet = workbook.CreateFont();
        fontDet.FontHeightInPoints = 9;
        fontDet.Boldweight = 600;
        fontDet.FontName = "Arial";
        fontDet.Boldweight = short.MaxValue;
        styleDet.SetFont(fontDet);

        return styleDet;
    }

    private ICellStyle SetHeaderStyle2007(IWorkbook workbook)
    {
        ICellStyle styleHeader = workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

        //styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
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
        fontHeader.FontName = "Arial";
        fontHeader.Boldweight = short.MaxValue;
        styleHeader.SetFont(fontHeader);

        return styleHeader;
    }
    public static DataSet selectDelivery(string vend, string nbr, string delivery, string InspDateStart, string InspDateEnd, string qcDateStart, string qcDateEnd, string RcpDateStart, string RcpDateEnd)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@delivery", delivery);
            param[2] = new SqlParameter("@InspDateStart", InspDateStart);
            param[3] = new SqlParameter("@InspDateEnd", InspDateEnd);
            param[4] = new SqlParameter("@qcDateStart", qcDateStart);
            param[5] = new SqlParameter("@qcDateEnd", qcDateEnd);
            param[6] = new SqlParameter("@RcpDateStart", RcpDateStart);
            param[7] = new SqlParameter("@RcpDateEnd", RcpDateEnd);
            param[8] = new SqlParameter("@vend", vend);
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.TCPC_Supplier"), CommandType.StoredProcedure, "sp_supp_selectDelivery", param);

        }
        catch
        {
            return null;
        }
    }
}
