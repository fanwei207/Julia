using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class wo2_wo2_UserWorkHoursDisplayExcel : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ToExcel();
    }

    public void ToExcel()
    {
        string strSdate = Request.QueryString["start"].ToString().Trim();
        string strEdate = Request.QueryString["end"].ToString().Trim();
        int intPlant = Convert.ToInt32(Session["Plantcode"]);
        string strNbr = Request.QueryString["nbr"].ToString().Trim();
        string strOrder = Request.QueryString["lot"].ToString().Trim();
        bool blflag = Convert.ToBoolean(Request.QueryString["clo"]);
        string strUserNo = Request.QueryString["uno"].ToString().Trim();

        DataTable dt = GetUserWorkHours(strSdate, strEdate, intPlant, strNbr, strOrder, blflag, strUserNo);

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

        //工号
        AppLibrary.WriteExcel.ColumnInfo column1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column1.ColumnIndexStart = 0;
        column1.ColumnIndexEnd = 0;
        column1.Width = 50 * 6000 / 164;
        sheet1.AddColumnInfo(column1);

        //姓名
        AppLibrary.WriteExcel.ColumnInfo column2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column2.ColumnIndexStart = 1;
        column2.ColumnIndexStart = 1;
        column2.Width = 60 * 6000 / 164;
        sheet1.AddColumnInfo(column2);

        //加工单
        AppLibrary.WriteExcel.ColumnInfo column3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column3.ColumnIndexStart = 2;
        column3.ColumnIndexStart = 2;
        column3.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column3);

        //加工单ID
        AppLibrary.WriteExcel.ColumnInfo column4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column4.ColumnIndexStart = 3;
        column4.ColumnIndexStart = 3;
        column4.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column4);

        //流水线
        AppLibrary.WriteExcel.ColumnInfo column5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column5.ColumnIndexStart = 4;
        column5.ColumnIndexStart = 4;
        column5.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column5);

        //工序
        AppLibrary.WriteExcel.ColumnInfo column6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column6.ColumnIndexStart = 5;
        column6.ColumnIndexStart = 5;
        column6.Width = 145 * 6000 / 164;
        sheet1.AddColumnInfo(column6);

        //工位
        AppLibrary.WriteExcel.ColumnInfo column7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column7.ColumnIndexStart = 6;
        column7.ColumnIndexStart = 6;
        column7.Width = 145 * 6000 / 164;
        sheet1.AddColumnInfo(column7);

        //工位系数
        AppLibrary.WriteExcel.ColumnInfo column8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column8.ColumnIndexStart = 7;
        column8.ColumnIndexStart = 7;
        column8.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column8);

        //汇报数量
        AppLibrary.WriteExcel.ColumnInfo column9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column9.ColumnIndexStart = 8;
        column9.ColumnIndexStart = 8;
        column9.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column9);
        #endregion

        #region//标题
        cells1.Add(1, 1, "工号", XFstyle1);
        cells1.Add(1, 2, "姓名", XFstyle1);
        cells1.Add(1, 3, "加工单号", XFstyle1);
        cells1.Add(1, 4, "加工单ID", XFstyle1);
        cells1.Add(1, 5, "流水线", XFstyle1);
        cells1.Add(1, 6, "工序", XFstyle1);
        cells1.Add(1, 7, "工位", XFstyle1);
        cells1.Add(1, 8, "工位系数", XFstyle1);
        cells1.Add(1, 9, "汇报数量", XFstyle1);
        #endregion

        #region//内容
        int i = 1;
        for (int n = 0; n < dt.Rows.Count; n++)
        {
            i++;
            cells1.Add(i, 1, dt.Rows[n].IsNull("wo2_userNo") == true ? "" : dt.Rows[n]["wo2_userNo"], XFstyle1);
            cells1.Add(i, 2, dt.Rows[n].IsNull("wo2_userName") == true ? "" : dt.Rows[n]["wo2_userName"], XFstyle1);
            cells1.Add(i, 3, dt.Rows[n].IsNull("wo2_nbr") == true ? "" : dt.Rows[n]["wo2_nbr"], XFstyle1);
            cells1.Add(i, 4, dt.Rows[n].IsNull("wo2_wID") == true ? "" : dt.Rows[n]["wo2_wID"], XFstyle1);
            cells1.Add(i, 5, dt.Rows[n].IsNull("wo2_line") == true ? "" : dt.Rows[n]["wo2_line"], XFstyle1);
            cells1.Add(i, 6, dt.Rows[n].IsNull("wo2_procName") == true ? "" : dt.Rows[n]["wo2_procName"], XFstyle1);
            cells1.Add(i, 7, dt.Rows[n].IsNull("wo2_postName") == true ? "" : dt.Rows[n]["wo2_postName"], XFstyle1);
            cells1.Add(i, 8, dt.Rows[n].IsNull("wo2_postProportion") == true ? "" : dt.Rows[n]["wo2_postProportion"], XFstyle1);
            cells1.Add(i, 9, dt.Rows[n].IsNull("wo2_line_comp") == true ? "" : dt.Rows[n]["wo2_line_comp"], XFstyle1);
        }
        dt.Reset();
        #endregion

        doc.Send();
        Response.Flush();
        Response.End();
    }

    private DataTable GetUserWorkHours(string strSdate, string strEdate, int intPlant, string strNbr, string strOrder, bool blflag, string strUserNo)
    {
        try
        {
            string str = "sp_hr_selectUserWorkHours";
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
            sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
            sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
            sqlParam[3] = new SqlParameter("@WorkOrder", strNbr);
            sqlParam[4] = new SqlParameter("@OrderID", strOrder);
            sqlParam[5] = new SqlParameter("@flag", blflag);
            sqlParam[6] = new SqlParameter("@UserNo", strUserNo);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}