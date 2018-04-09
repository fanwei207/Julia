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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using CommClass;
using WOrder;

public partial class HR_hr_ChAttendanceExcel1 : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    WorkOrder wd = new WorkOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        ToExcel();
    }

    public void ToExcel()
    {
        string strStart = Request.QueryString["dt1"].ToString().Trim();
        string strEnd = Request.QueryString["dt2"].ToString().Trim();
        string strUserNo = Request.QueryString["uno"].ToString().Trim();
        string strUserName = Request.QueryString["una"].ToString().Trim();
        int intDepart = Convert.ToInt32(Request.QueryString["dep"].ToString().Trim());
        int intUserType = Convert.ToInt32(Request.QueryString["ut"].ToString().Trim());
        int intType = 0;
        int intflag = Convert.ToInt32(Request.QueryString["fl"].ToString().Trim());

        DataTable dt = wd.CheckAttendance1(strStart, strEnd, strUserNo, strUserName, intDepart, intUserType, intflag);

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

        #region//Sheet1列宽控制
        //部门列
        AppLibrary.WriteExcel.ColumnInfo column1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column1.ColumnIndexStart = 0;
        column1.ColumnIndexStart = 0;
        column1.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column1);

        //班组列
        AppLibrary.WriteExcel.ColumnInfo column2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column2.ColumnIndexStart = 1;
        column2.ColumnIndexStart = 1;
        column2.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column2);

        //工号列
        AppLibrary.WriteExcel.ColumnInfo column3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column3.ColumnIndexStart = 2;
        column3.ColumnIndexStart = 2;
        column3.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column3);

        //姓名列
        AppLibrary.WriteExcel.ColumnInfo column4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column4.ColumnIndexStart = 3;
        column4.ColumnIndexStart = 3;
        column4.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column4);

        //日期列
        AppLibrary.WriteExcel.ColumnInfo column5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column5.ColumnIndexStart = 4;
        column5.ColumnIndexStart = 4;
        column5.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column5);

        //上班时间列
        AppLibrary.WriteExcel.ColumnInfo column6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column6.ColumnIndexStart = 5;
        column6.ColumnIndexStart = 5;
        column6.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column6);

        //下班时间列
        AppLibrary.WriteExcel.ColumnInfo column7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column7.ColumnIndexStart = 6;
        column7.ColumnIndexStart = 6;
        column7.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column7);

        //上班时间1列
        AppLibrary.WriteExcel.ColumnInfo column8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column8.ColumnIndexStart = 7;
        column8.ColumnIndexStart = 7;
        column8.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column8);

        //下班时间1列
        AppLibrary.WriteExcel.ColumnInfo column9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column9.ColumnIndexStart = 8;
        column9.ColumnIndexStart = 8;
        column9.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column9);

        //类型列
        AppLibrary.WriteExcel.ColumnInfo column10 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column10.ColumnIndexStart = 9;
        column10.ColumnIndexStart = 9;
        column10.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column10);
        #endregion

        cells1.Add(1, 1, "部门", XFstyle1);
        cells1.Add(1, 2, "班组", XFstyle1);
        cells1.Add(1, 3, "工号", XFstyle1);
        cells1.Add(1, 4, "姓名", XFstyle1);
        cells1.Add(1, 5, "日期", XFstyle1);
        cells1.Add(1, 6, "上班时间", XFstyle1);
        cells1.Add(1, 7, "下班时间", XFstyle1);
        cells1.Add(1, 8, "上班时间1", XFstyle1);
        cells1.Add(1, 9, "下班时间1", XFstyle1);
        cells1.Add(1, 10, "类型", XFstyle1);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cells1.Add(i + 2, 1, dt.Rows[i].IsNull("dname") == true ? "" : dt.Rows[i]["dname"], XFstyle1);
            cells1.Add(i + 2, 2, dt.Rows[i].IsNull("WorkGroup") == true ? "" : dt.Rows[i]["WorkGroup"], XFstyle1);
            cells1.Add(i + 2, 3, dt.Rows[i].IsNull("userNO") == true ? "" : dt.Rows[i]["userNO"], XFstyle1);
            cells1.Add(i + 2, 4, dt.Rows[i].IsNull("username") == true ? "" : dt.Rows[i]["username"], XFstyle1);
            cells1.Add(i + 2, 5, dt.Rows[i].IsNull("workdate") == true ? "" : dt.Rows[i]["workdate"], XFstyle1);
            cells1.Add(i + 2, 6, dt.Rows[i].IsNull("starttime") == true ? "" : dt.Rows[i]["starttime"], XFstyle1);
            cells1.Add(i + 2, 7, dt.Rows[i].IsNull("endtime") == true ? "" : dt.Rows[i]["endtime"], XFstyle1);
            cells1.Add(i + 2, 8, dt.Rows[i].IsNull("starttime1") == true ? "" : dt.Rows[i]["starttime1"], XFstyle1);
            cells1.Add(i + 2, 9, dt.Rows[i].IsNull("endtime1") == true ? "" : dt.Rows[i]["endtime1"], XFstyle1);
            cells1.Add(i + 2, 10, dt.Rows[i].IsNull("atypename") == true ? "" : dt.Rows[i]["atypename"], XFstyle1);
        }

        dt.Reset();
        doc.Send();
        Response.Flush();
        Response.End();
    }
}
