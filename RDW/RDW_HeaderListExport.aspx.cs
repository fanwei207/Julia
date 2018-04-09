using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RD_WorkFlow;

public partial class RDW_RDW_HeaderListExport : System.Web.UI.Page
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strCateid = Request.QueryString["cateid"].ToString();
        string strProj = Request.QueryString["ProjName"].ToString();
        string strProd = Request.QueryString["Code"].ToString();
        string strSku = Request.QueryString["sku"].ToString();
        string strStart = Request.QueryString["startdate"].ToString();
        string strStatus = Request.QueryString["status"].ToString();
        string strMessage = string.Empty;
        string strUID = Request.QueryString["strUID"].ToString();
        bool canViewAll = Convert.ToBoolean(Request.QueryString["canViewAll"]);

        #region 
        AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
        doc.FileName = "report-Project List" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("");

        #region 设置列宽
        AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col1.ColumnIndexStart = 0;
        col1.ColumnIndexEnd = 0;
        col1.Width = 120 * 6000 / 164;
        sheet.AddColumnInfo(col1);

        AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col2.ColumnIndexStart = 1;
        col2.ColumnIndexEnd = 1;
        col2.Width = 250 * 6000 / 164;
        sheet.AddColumnInfo(col2);

        AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col3.ColumnIndexStart = 2;
        col3.ColumnIndexEnd = 2;
        col3.Width = 150 * 6000 / 164;
        sheet.AddColumnInfo(col3);

        AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col4.ColumnIndexStart = 3;
        col4.ColumnIndexEnd = 3;
        col4.Width = 150 * 6000 / 164;
        sheet.AddColumnInfo(col4);

        AppLibrary.WriteExcel.ColumnInfo col5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col5.ColumnIndexStart = 4;
        col5.ColumnIndexEnd = 4;
        col5.Width = 150 * 6000 / 164;
        sheet.AddColumnInfo(col5);


        AppLibrary.WriteExcel.ColumnInfo col6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col6.ColumnIndexStart = 5;
        col6.ColumnIndexEnd = 5;
        col6.Width = 200 * 6000 / 164;
        sheet.AddColumnInfo(col6);


        AppLibrary.WriteExcel.ColumnInfo col7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col7.ColumnIndexStart = 6;
        col7.ColumnIndexEnd = 6;
        col7.Width = 200 * 6000 / 164;
        sheet.AddColumnInfo(col7);


        AppLibrary.WriteExcel.ColumnInfo col8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col8.ColumnIndexStart = 7;
        col8.ColumnIndexEnd = 7;
        col8.Width = 100 * 6000 / 164;
        sheet.AddColumnInfo(col8);


        AppLibrary.WriteExcel.ColumnInfo col9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col9.ColumnIndexStart = 8;
        col9.ColumnIndexEnd = 8;
        col9.Width = 100 * 6000 / 164;
        sheet.AddColumnInfo(col9);


        AppLibrary.WriteExcel.ColumnInfo col10 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col10.ColumnIndexStart = 9;
        col10.ColumnIndexEnd = 9;
        col10.Width = 100 * 6000 / 164;
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
        col13.Width = 500 * 6000 / 164;
        sheet.AddColumnInfo(col13);

        AppLibrary.WriteExcel.ColumnInfo col14 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
        col14.ColumnIndexStart = 13;
        col14.ColumnIndexEnd = 13;
        col14.Width = 500 * 6000 / 164;
        sheet.AddColumnInfo(col14);
        #endregion

        int rowIndex = 1;
        PrintRDWHeaderList(doc, sheet, rowIndex, "Project Category", "Project Name", "Project Code", "Created Date", "Creater", "Prodject Describtion", "Start Date",
                "End Date", "Finish Date", "Status", "Views", "Leaders", "key specification","Notes");
        RDW_Header rh = new RDW_Header();

        IList<RDW_Header> RDW_HeaderList = rdw.SelectRDWListExport(strCateid, strProj, strProd, strSku, strStart, strMessage, strStatus, strUID, canViewAll);
        foreach (RDW_Header rhd in RDW_HeaderList)
        {
            rh = rhd;
            rowIndex++;
            PrintRDWHeaderList(doc, sheet, rowIndex, rh.RDW_Category, rh.RDW_Project, rh.RDW_ProdCode, rh.RDW_CreatedDate, rh.RDW_Creater, rh.RDW_ProdDesc,
                   rh.RDW_StartDate, rh.RDW_EndDate, rh.RDW_FinishDate, rh.RDW_Status, rh.RDW_PartnerName, rh.RDW_PM, rh.RDW_Standard, rh.RDW_Memo);

        }

        doc.Send();
        Response.Flush();
        Response.End();
        #endregion
    }

    private void PrintRDWHeaderList(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
        string RDW_Category, string RDW_Project, string RDW_ProdCode, string RDW_CreatedDate, string RDW_Creater, string RDW_ProdDesc, string RDW_StartDate,
        string RDW_EndDate, string RDW_FinishDate, string RDW_Status, string RDW_Partner, string RDW_Leader, string RDW_Standard,string RDW_Memo)
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

        sheet.Cells.Add(rowIndex, 1, RDW_Category, xf);
        sheet.Cells.Add(rowIndex, 2, RDW_Project, xf);
        sheet.Cells.Add(rowIndex, 3, RDW_ProdCode, xf);
        sheet.Cells.Add(rowIndex, 4, RDW_CreatedDate, xf);
        sheet.Cells.Add(rowIndex, 5, RDW_Creater, xf);
        sheet.Cells.Add(rowIndex, 6, RDW_ProdDesc, xf);
        sheet.Cells.Add(rowIndex, 7, RDW_StartDate, xf);
        sheet.Cells.Add(rowIndex, 8, RDW_EndDate, xf);
        sheet.Cells.Add(rowIndex, 9, RDW_FinishDate, xf);
        sheet.Cells.Add(rowIndex, 10, RDW_Status, xf);
        sheet.Cells.Add(rowIndex, 11, RDW_Partner, xf);
        sheet.Cells.Add(rowIndex, 12, RDW_Leader, xf);
        sheet.Cells.Add(rowIndex, 13, RDW_Standard, xf);
        sheet.Cells.Add(rowIndex, 14, RDW_Memo, xf);

    }


}