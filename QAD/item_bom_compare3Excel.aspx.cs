using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Item;

public partial class QAD_item_bom_compare3Excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ToExcel();
    }

    public void ToExcel()
    {
        string uID = Request.QueryString["uID"].ToString().Trim();
        string strLevel = Request.QueryString["level"].ToString().Trim();
      

        AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
        doc.FileName = "excel"+ System.DateTime.Now+".xls";
        string SheetName = string.Empty;

        //Sheet1内容
        SheetName = "Bom结构比较";
        AppLibrary.WriteExcel.Worksheet sheet1 = doc.Workbook.Worksheets.Add(SheetName);
        AppLibrary.WriteExcel.Cells cells1 = sheet1.Cells;

        #region//样式1白底
        AppLibrary.WriteExcel.XF XFstyle1 = doc.NewXF();
        XFstyle1.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
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

       
        #region//Sheet1列宽控制

        //父件
        AppLibrary.WriteExcel.ColumnInfo column1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column1.ColumnIndexStart = 0;
        column1.ColumnIndexEnd = 0;
        column1.Width = 120 * 6000 / 164;
        sheet1.AddColumnInfo(column1);

        //qad库子件
        AppLibrary.WriteExcel.ColumnInfo column2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column2.ColumnIndexStart = 1;
        column2.ColumnIndexStart = 1;
        column2.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column2);

        //Qad单位用量
        AppLibrary.WriteExcel.ColumnInfo column3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column3.ColumnIndexStart = 2;
        column3.ColumnIndexStart = 2;
        column3.Width = 150 * 6000 / 164;
        sheet1.AddColumnInfo(column3);

        //Qad层级
        AppLibrary.WriteExcel.ColumnInfo column4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column4.ColumnIndexStart = 3;
        column4.ColumnIndexStart = 3;
        column4.Width = 250 * 6000 / 164;
        sheet1.AddColumnInfo(column4);

        //100库子件
        AppLibrary.WriteExcel.ColumnInfo column5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column5.ColumnIndexStart = 4;
        column5.ColumnIndexStart = 4;
        column5.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column5);

        //100库子件
        AppLibrary.WriteExcel.ColumnInfo column6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column6.ColumnIndexStart = 5;
        column6.ColumnIndexStart = 5;
        column6.Width = 50 * 6000 / 164;
        sheet1.AddColumnInfo(column6);

        //100库子件
        AppLibrary.WriteExcel.ColumnInfo column7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column7.ColumnIndexStart = 6;
        column7.ColumnIndexStart = 6;
        column7.Width = 150 * 6000 / 164;
        sheet1.AddColumnInfo(column7); 
        #endregion

        cells1.Add(1, 1, "父件", XFstyle1);
        cells1.Add(1, 2, "QAD库子件", XFstyle1);
        cells1.Add(1, 3, "QAD库单位用量", XFstyle1);
        cells1.Add(1, 4, "QAD库层级", XFstyle1);
        cells1.Add(1, 5, "100库子件", XFstyle1);
        cells1.Add(1, 6, "100库单位用量", XFstyle1);
        cells1.Add(1, 7, "100库层级", XFstyle1);
       


        DataTable dt1 = null;

        Item_Bom_Compare ibc = new Item_Bom_Compare();
        dt1 = ibc.CompareItemStru1(Convert.ToInt32(uID), strLevel); 
        int i = 1;
        for (int n = 0; n < dt1.Rows.Count; n++)
        {
            i++;
            cells1.Add(i, 1, dt1.Rows[n].IsNull("bom_par") == true ? "" : dt1.Rows[n]["bom_par"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 2, dt1.Rows[n].IsNull("item_bom_comp") == true ? "" : dt1.Rows[n]["item_bom_comp"], XFstyle1);
            cells1.Add(i, 3, dt1.Rows[n].IsNull("item_bom_qty") == true ? "" : dt1.Rows[n]["item_bom_qty"], XFstyle1);
            cells1.Add(i, 4, dt1.Rows[n].IsNull("item_bom_lel") == true ? "" : dt1.Rows[n]["item_bom_lel"], XFstyle1);
            cells1.Add(i, 5, dt1.Rows[n].IsNull("product_bom_comp") == true ? "" : dt1.Rows[n]["product_bom_comp"], XFstyle1);
            cells1.Add(i, 6, dt1.Rows[n].IsNull("product_bom_qty") == true ? "" : dt1.Rows[n]["product_bom_qty"], XFstyle1);
            cells1.Add(i, 7, dt1.Rows[n].IsNull("product_bom_lel") == true ? "" : dt1.Rows[n]["product_bom_lel"], XFstyle1);
            
        }
        dt1.Reset();

        doc.Send();
        Response.Flush();
        Response.End();
    } 
}