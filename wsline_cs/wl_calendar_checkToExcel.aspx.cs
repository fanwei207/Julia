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
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class wsline_cs_wl_calendar_checkToExcel : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = BindData();
            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("考勤统计信息");

                #region 动态创建表头
                int rowIndex = 2;
                int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(Request.QueryString["uYear"]), Convert.ToInt32(Request.QueryString["uMonth"]));
                AppLibrary.WriteExcel.XF xf = doc.NewXF();
                xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
                xf.Font.FontName = "宋体";
                xf.UseMisc = true;
                xf.Font.Bold = true;
                xf.Font.Height = 9 * 256 / 13; //对应size = 13
                xf.LeftLineStyle = 1;
                xf.TopLineStyle = 1;
                xf.RightLineStyle = 1;
                xf.BottomLineStyle = 1;
                xf.TextWrapRight = true;

                for (int i = 1; i <= 9; i++)
                {
                    sheet.Cells.Merge(1, 2, i, i);
                }
                for (int j = 10; j <= daysInMonth + 10; j++)
                {
                    sheet.Cells.Merge(1, 1, 2 * j - 10, 2 * j - 9);
                }
                sheet.Cells.Add(1, 1, "部门", xf);
                sheet.Cells.Add(1, 2, "工号", xf);
                sheet.Cells.Add(1, 3, "姓名", xf);
                sheet.Cells.Add(1, 4, "入司日期", xf);
                sheet.Cells.Add(1, 5, "人员类型", xf);
                sheet.Cells.Add(1, 6, "计酬方式", xf);
                sheet.Cells.Add(1, 7, "工段", xf);
                sheet.Cells.Add(1, 8, "考勤天数", xf);
                sheet.Cells.Add(1, 9, "离职日期", xf);
                int index = 10;
                for (int i = 1; i <= daysInMonth; i++)
                {
                    sheet.Cells.Add(1, index, Request.QueryString["uYear"] + " / " + Request.QueryString["uMonth"] + " / " + i.ToString(), xf);
                    index = index + 2;
                }


                for (int j = 10; j <= 2 * daysInMonth + 9; j++)
                {
                    if (j % 2 == 1)
                    {
                        sheet.Cells.Add(2, j, "下班", xf);
                    }
                    else
                    {
                        sheet.Cells.Add(2, j, "上班", xf);

                    }


                }

                #endregion

                #region 设定列宽
                //部门代码列
                AppLibrary.WriteExcel.ColumnInfo DepartmentName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                DepartmentName.ColumnIndexStart = 0;
                DepartmentName.ColumnIndexEnd = 0;
                DepartmentName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(DepartmentName);
                //工号代码列
                AppLibrary.WriteExcel.ColumnInfo userNo = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                userNo.ColumnIndexStart = 1;
                userNo.ColumnIndexEnd = 1;
                userNo.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(userNo);
                //姓名代码列
                AppLibrary.WriteExcel.ColumnInfo userName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                userName.ColumnIndexStart = 2;
                userName.ColumnIndexEnd = 2;
                userName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(userName);
                //入司日期代码列
                AppLibrary.WriteExcel.ColumnInfo enterDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                enterDate.ColumnIndexStart = 3;
                enterDate.ColumnIndexEnd = 3;
                enterDate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(enterDate);
                //人员类型代码列
                AppLibrary.WriteExcel.ColumnInfo UserTypeName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                UserTypeName.ColumnIndexStart = 4;
                UserTypeName.ColumnIndexEnd = 4;
                UserTypeName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(UserTypeName);
                //计酬方式代码列
                AppLibrary.WriteExcel.ColumnInfo workTypeName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                workTypeName.ColumnIndexStart = 5;
                workTypeName.ColumnIndexEnd = 5;
                workTypeName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(workTypeName);
                //工段代码列
                AppLibrary.WriteExcel.ColumnInfo WorkShopName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                WorkShopName.ColumnIndexStart = 6;
                WorkShopName.ColumnIndexEnd = 6;
                WorkShopName.Width = 100 * 6000 / 164;
                //考勤天数代码列
                AppLibrary.WriteExcel.ColumnInfo checkDays = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                checkDays.ColumnIndexStart = 7;
                checkDays.ColumnIndexEnd = 7;
                checkDays.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(checkDays);
                //离职日期代码列
                AppLibrary.WriteExcel.ColumnInfo leaveDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                leaveDate.ColumnIndexStart = 8;
                leaveDate.ColumnIndexEnd = 8;
                leaveDate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(leaveDate);

                #endregion

                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex
                                , row["DepartmentName"], row["userNo"], row["userName"], row["enterDate"], row["UserTypeName"]
                                , row["workTypeName"], row["WorkShopName"], row["checkDays"], row["leaveDate"]
                                , row["A1"], row["B1"], row["A2"], row["B2"], row["A3"], row["B3"], row["A4"], row["B4"], row["A5"], row["B5"]
                                , row["A6"], row["B6"], row["A7"], row["B7"], row["A8"], row["B8"], row["A9"], row["B9"], row["A10"], row["B10"]
                                , row["A11"], row["B11"], row["A12"], row["B12"], row["A13"], row["B13"], row["A14"], row["B14"], row["A15"], row["B15"]
                                , row["A16"], row["B16"], row["A17"], row["B17"], row["A18"], row["B18"], row["A19"], row["B19"], row["A20"], row["B20"]
                                , row["A21"], row["B21"], row["A22"], row["B22"], row["A23"], row["B23"], row["A24"], row["B24"], row["A25"], row["B25"]
                                , row["A26"], row["B26"], row["A27"], row["B27"], row["A28"], row["B28"], row["A29"], row["B29"], row["A30"], row["B30"]
                                , row["A31"], row["B31"]);
                }
                doc.Send();

                Response.Flush();
                Response.End();
                dt.Dispose();
            }
            else
            {
                Response.Redirect("wl_calendar_check.aspx");
            }
        }

    }
    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex
                                , Object DepartmentName, Object userNo, Object userName, Object enterDate, Object UserTypeName
                                , Object workTypeName, Object WorkShopName, Object checkDays, Object leaveDate
                                , Object A1, Object B1, Object A2, Object B2, Object A3, Object B3, Object A4, Object B4, Object A5, Object B5
                                , Object A6, Object B6, Object A7, Object B7, Object A8, Object B8, Object A9, Object B9, Object A10, Object B10
                                , Object A11, Object B11, Object A12, Object B12, Object A13, Object B13, Object A14, Object B14, Object A15, Object B15
                                , Object A16, Object B16, Object A17, Object B17, Object A18, Object B18, Object A19, Object B19, Object A20, Object B20
                                , Object A21, Object B21, Object A22, Object B22, Object A23, Object B23, Object A24, Object B24, Object A25, Object B25
                                , Object A26, Object B26, Object A27, Object B27, Object A28, Object B28, Object A29, Object B29, Object A30, Object B30
                                , Object A31, Object B31)
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

        sheet.Cells.Add(rowIndex, 1, DepartmentName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, userNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, userName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd}", enterDate), xf);
        sheet.Cells.Add(rowIndex, 5, UserTypeName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, workTypeName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, WorkShopName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, checkDays.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, string.Format("{0:yyyy-MM-dd}", leaveDate), xf);
        sheet.Cells.Add(rowIndex, 10, A1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 11, B1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 12, A2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 13, B2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, A3.ToString(), xf);
        sheet.Cells.Add(rowIndex, 15, B3.ToString(), xf);
        sheet.Cells.Add(rowIndex, 16, A4.ToString(), xf);
        sheet.Cells.Add(rowIndex, 17, B4.ToString(), xf);
        sheet.Cells.Add(rowIndex, 18, A5.ToString(), xf);
        sheet.Cells.Add(rowIndex, 19, B5.ToString(), xf);
        sheet.Cells.Add(rowIndex, 20, A6.ToString(), xf);
        sheet.Cells.Add(rowIndex, 21, B6.ToString(), xf);
        sheet.Cells.Add(rowIndex, 22, A7.ToString(), xf);
        sheet.Cells.Add(rowIndex, 23, B7.ToString(), xf);
        sheet.Cells.Add(rowIndex, 24, A8.ToString(), xf);
        sheet.Cells.Add(rowIndex, 25, B8.ToString(), xf);
        sheet.Cells.Add(rowIndex, 26, A9.ToString(), xf);
        sheet.Cells.Add(rowIndex, 27, B9.ToString(), xf);
        sheet.Cells.Add(rowIndex, 28, A10.ToString(), xf);
        sheet.Cells.Add(rowIndex, 29, B10.ToString(), xf);
        sheet.Cells.Add(rowIndex, 30, A11.ToString(), xf);
        sheet.Cells.Add(rowIndex, 31, B11.ToString(), xf);
        sheet.Cells.Add(rowIndex, 32, A12.ToString(), xf);
        sheet.Cells.Add(rowIndex, 33, B12.ToString(), xf);
        sheet.Cells.Add(rowIndex, 34, A13.ToString(), xf);
        sheet.Cells.Add(rowIndex, 35, B13.ToString(), xf);
        sheet.Cells.Add(rowIndex, 36, A14.ToString(), xf);
        sheet.Cells.Add(rowIndex, 37, B14.ToString(), xf);
        sheet.Cells.Add(rowIndex, 38, A15.ToString(), xf);
        sheet.Cells.Add(rowIndex, 39, B15.ToString(), xf);
        sheet.Cells.Add(rowIndex, 40, A16.ToString(), xf);
        sheet.Cells.Add(rowIndex, 41, B16.ToString(), xf);
        sheet.Cells.Add(rowIndex, 42, A17.ToString(), xf);
        sheet.Cells.Add(rowIndex, 43, B17.ToString(), xf);
        sheet.Cells.Add(rowIndex, 44, A18.ToString(), xf);
        sheet.Cells.Add(rowIndex, 45, B18.ToString(), xf);
        sheet.Cells.Add(rowIndex, 46, A19.ToString(), xf);
        sheet.Cells.Add(rowIndex, 47, B19.ToString(), xf);
        sheet.Cells.Add(rowIndex, 48, A20.ToString(), xf);
        sheet.Cells.Add(rowIndex, 49, B20.ToString(), xf);
        sheet.Cells.Add(rowIndex, 50, A21.ToString(), xf);
        sheet.Cells.Add(rowIndex, 51, B21.ToString(), xf);
        sheet.Cells.Add(rowIndex, 52, A22.ToString(), xf);
        sheet.Cells.Add(rowIndex, 53, B22.ToString(), xf);
        sheet.Cells.Add(rowIndex, 54, A23.ToString(), xf);
        sheet.Cells.Add(rowIndex, 55, B23.ToString(), xf);
        sheet.Cells.Add(rowIndex, 56, A24.ToString(), xf);
        sheet.Cells.Add(rowIndex, 57, B24.ToString(), xf);
        sheet.Cells.Add(rowIndex, 58, A25.ToString(), xf);
        sheet.Cells.Add(rowIndex, 59, B25.ToString(), xf);
        sheet.Cells.Add(rowIndex, 60, A26.ToString(), xf);
        sheet.Cells.Add(rowIndex, 61, B26.ToString(), xf);
        sheet.Cells.Add(rowIndex, 62, A27.ToString(), xf);
        sheet.Cells.Add(rowIndex, 63, B27.ToString(), xf);
        sheet.Cells.Add(rowIndex, 64, A28.ToString(), xf);
        sheet.Cells.Add(rowIndex, 65, B28.ToString(), xf);
        sheet.Cells.Add(rowIndex, 66, A29.ToString(), xf);
        sheet.Cells.Add(rowIndex, 67, B29.ToString(), xf);
        sheet.Cells.Add(rowIndex, 68, A30.ToString(), xf);
        sheet.Cells.Add(rowIndex, 69, B30.ToString(), xf);
        sheet.Cells.Add(rowIndex, 70, A31.ToString(), xf);
        sheet.Cells.Add(rowIndex, 71, B31.ToString(), xf);
    }


    protected DataTable BindData()
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@departmentID", Convert.ToInt32(Request.QueryString["departmentID"]));
        param[1] = new SqlParameter("@userType", Convert.ToInt32(Request.QueryString["userType"]));
        param[2] = new SqlParameter("@workShop", Convert.ToInt32(Request.QueryString["workShop"]));
        param[3] = new SqlParameter("@userNo", Request.QueryString["userNo"]);
        param[4] = new SqlParameter("@uYear", Request.QueryString["uYear"]);
        param[5] = new SqlParameter("@uMonth", Convert.ToInt32(Request.QueryString["uMonth"]));
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_selectAttendaceCheck", param).Tables[0];

    }
}