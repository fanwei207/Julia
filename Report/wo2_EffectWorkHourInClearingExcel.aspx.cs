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

public partial class wo2_EffectWorkHourInClearingExcel : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ToExcel();
    }

    public void ToExcel()
    { 
        string _year = Request.QueryString["year"].ToString().Trim();
        string _month = Request.QueryString["month"].ToString().Trim(); 
        bool _chk = Convert.ToBoolean(Request.QueryString["chk"].ToString().Trim());

        DateTime currentMonth = Convert.ToDateTime(_year + "-" + _month + "-01");

        String curMHead = currentMonth.ToString().Substring(0, 6);
        String OneMHead = currentMonth.AddMonths(-1).ToString().Substring(0, 6);
        String twoMHead = currentMonth.AddMonths(-2).ToString().Substring(0, 6);

        AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
        doc.FileName = "workhourexcel" + curMHead + ".xls";
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

        #region//样式2黄底
        AppLibrary.WriteExcel.XF XFstyle2 = doc.NewXF();
        XFstyle2.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
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

        //工号
        AppLibrary.WriteExcel.ColumnInfo column1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column1.ColumnIndexStart = 0;
        column1.ColumnIndexEnd = 0;
        column1.Width = 80 * 6000 / 164;
        sheet1.AddColumnInfo(column1);

        //姓名
        AppLibrary.WriteExcel.ColumnInfo column2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column2.ColumnIndexStart = 1;
        column2.ColumnIndexStart = 1;
        column2.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column2);

        //部门
        AppLibrary.WriteExcel.ColumnInfo column3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column3.ColumnIndexStart = 2;
        column3.ColumnIndexStart = 2;
        column3.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column3);

        //工段
        AppLibrary.WriteExcel.ColumnInfo column4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column4.ColumnIndexStart = 3;
        column4.ColumnIndexStart = 3;
        column4.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column4);

        //工序
        AppLibrary.WriteExcel.ColumnInfo column5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column5.ColumnIndexStart = 4;
        column5.ColumnIndexStart = 4;
        column5.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column5);

        //员工类型
        AppLibrary.WriteExcel.ColumnInfo column6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column6.ColumnIndexStart = 5;
        column6.ColumnIndexStart = 5;
        column6.Width = 50 * 6000 / 164;
        sheet1.AddColumnInfo(column6);

        //结算日期，汇报日期为结算日期前2月的汇报 工时
        AppLibrary.WriteExcel.ColumnInfo column7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column7.ColumnIndexStart = 6;
        column7.ColumnIndexStart = 6;
        column7.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column7);

        //结算日期，汇报日期为结算日期前1月的汇报 工时
        AppLibrary.WriteExcel.ColumnInfo column8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column8.ColumnIndexStart = 7;
        column8.ColumnIndexStart = 7;
        column8.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column8);

        //结算日期，汇报日期为结算日期前0月的汇报 工时
        AppLibrary.WriteExcel.ColumnInfo column9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column9.ColumnIndexStart = 8;
        column9.ColumnIndexStart = 8;
        column9.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column9);

        //结算日期
        AppLibrary.WriteExcel.ColumnInfo column10 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet1);
        column10.ColumnIndexStart = 9;
        column10.ColumnIndexStart = 9;
        column10.Width = 100 * 6000 / 164;
        sheet1.AddColumnInfo(column10); 
        #endregion 

     
 
        DataSet ds = null;
        DataTable dt1 = null;  //明细
        DataTable dt2 = null;  //总
        ds = GetExportData(_year, _month, _chk);
        dt1 = ds.Tables[1];
        dt2 = ds.Tables[0];   
         
        int i = 1;
        float sumwk = 0;


        cells1.Add(1, 1, "工时结算周期", XFstyle1);
        cells1.Add(1, 2, curMHead, XFstyle1);
        cells1.Add(1, 3, " ", XFstyle1);
        cells1.Add(1, 4, "结算的总工时", XFstyle1);
        cells1.Add(1, 5,"", XFstyle1);
        cells1.Add(1, 6, " ", XFstyle1);
        cells1.Add(1, 7, "", XFstyle1);
        cells1.Add(1, 8, "", XFstyle1);
        cells1.Add(1, 9, "", XFstyle1);
        cells1.Add(1, 10, "", XFstyle1);
        int y = 0;
        for (int n = 0; n < dt2.Rows.Count; n++)
        {
            y=y+2;
            cells1.Add(2, n + y , dt2.Rows[n]["wo2_effDate"].ToString().Trim()+"月汇报总工时", XFstyle1);
            cells1.Add(2, n + y + 1, dt2.Rows[n].IsNull(1) == true ? "" : dt2.Rows[n][1], XFstyle1);
            sumwk = sumwk + Convert.ToSingle(dt2.Rows[n][1]);
        }
        cells1.Add(1, 5, sumwk, XFstyle1);

        //cells1.Add(3, 1, "工号", XFstyle1);
        //cells1.Add(3, 2, "姓名", XFstyle1);
        //cells1.Add(3, 3, "部门", XFstyle1);
        //cells1.Add(3, 4, "工段", XFstyle1);
        //cells1.Add(3, 5, "工序", XFstyle1);
        //cells1.Add(3, 6, "人员类型", XFstyle1);
        //cells1.Add(3, 7, twoMHead + "汇报工时", XFstyle1);
        //cells1.Add(3, 8, OneMHead + "汇报工时", XFstyle1);
        //cells1.Add(3, 9, curMHead + "汇报工时", XFstyle1);
        //cells1.Add(3, 10, "结算年月", XFstyle1);

        i = 4;
        cells1.Add(i, 1, "工号", XFstyle1);
        cells1.Add(i, 2, "姓名", XFstyle1);
        cells1.Add(i, 3, "部门", XFstyle1);
        cells1.Add(i, 4, "工段", XFstyle1);
        cells1.Add(i, 5, "工序", XFstyle1);
        cells1.Add(i, 6, "人员类型", XFstyle1);
        cells1.Add(i, 7, "结算年月", XFstyle1); 
         
        for( int x= 8; x< dt1.Columns.Count; x++)
        {
              cells1.Add(i, x, dt1.Columns[x].ColumnName, XFstyle1);
        } 

        for (int n = 0; n < dt1.Rows.Count; n++)
        {
            i++;
            cells1.Add(i, 1, dt1.Rows[n]["wo2_userNo"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 2, dt1.Rows[n]["wo2_userName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 3, dt1.Rows[n]["departmentName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 4, dt1.Rows[n]["WorkShopName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 5, dt1.Rows[n]["wo2_procName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 6, dt1.Rows[n]["UserTypeName"].ToString().Trim(), XFstyle1);
            cells1.Add(i, 7, dt1.Rows[n]["wo2_ym"].ToString().Trim(), XFstyle1);
            for (int x = 8; x < dt1.Columns.Count; x++)
            {
                cells1.Add(i, x, dt1.Rows[n].IsNull(x) == true ? "" : dt1.Rows[n][x], XFstyle1);
                
            }   
        }
        dt1.Reset(); 
        doc.Send();
        Response.Flush();
        Response.End();
    }

    private DataSet GetExportData(string _year, string _month, bool _chk)
    {
         
        string strSql = "rep_selectEffectWorkHourInClearing";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@year", _year);
        param[1] = new SqlParameter("@month", _month);
        param[2] = new SqlParameter("@chk", _chk);
        DataSet ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, param);
        return ds;
        //}
        //catch
        //{
        //    ;
        //}
    }

   }
