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

public partial class wsline_cs_wl_postAttendanceToExcel : System.Web.UI.Page
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
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("在职出勤统计信息");

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

                for (int i = 1; i <= 2; i++)
                {
                    sheet.Cells.Merge(1, 2, i, i);
                }
                for (int j = 3; j <= daysInMonth + 3; j++)
                {
                    sheet.Cells.Merge(1, 1, 2 * j - 3, 2 * j - 2);
                }
                sheet.Cells.Add(1, 1, "部门", xf);
                sheet.Cells.Add(1, 2, "工段", xf);
                int index = 3;
                for (int i = 1; i <= daysInMonth; i++)
                {
                    sheet.Cells.Add(1, index, Request.QueryString["uYear"] + " / " + Request.QueryString["uMonth"] + " / " + i.ToString(), xf);
                    index = index + 2;
                }


                for (int j = 3; j <= 2 * daysInMonth + 2; j++)
                {
                    if (j % 2 == 1)
                    {
                        sheet.Cells.Add(2, j, "在职", xf);
                    }
                    else
                    {
                        sheet.Cells.Add(2, j, "出勤", xf);

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
                
                //工段代码列
                AppLibrary.WriteExcel.ColumnInfo WorkShopName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                WorkShopName.ColumnIndexStart = 1;
                WorkShopName.ColumnIndexEnd = 1;
                WorkShopName.Width = 100 * 6000 / 164;
                

                #endregion

                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex
                                , row["deptName"], row["workshopName"]
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
                Response.Redirect("wl_postAttendance.aspx");
            }
        }

    }
    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex
                                , Object deptName, Object workshopName
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

        sheet.Cells.Add(rowIndex, 1, deptName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, workshopName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, A1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, B1.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, A2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, B2.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, A3.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, B3.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, A4.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, B4.ToString(), xf);
        sheet.Cells.Add(rowIndex, 11, A5.ToString(), xf);
        sheet.Cells.Add(rowIndex, 12, B5.ToString(), xf);
        sheet.Cells.Add(rowIndex, 13, A6.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, B6.ToString(), xf);
        sheet.Cells.Add(rowIndex, 15, A7.ToString(), xf);
        sheet.Cells.Add(rowIndex, 16, B7.ToString(), xf);
        sheet.Cells.Add(rowIndex, 17, A8.ToString(), xf);
        sheet.Cells.Add(rowIndex, 18, B8.ToString(), xf);
        sheet.Cells.Add(rowIndex, 19, A9.ToString(), xf);
        sheet.Cells.Add(rowIndex, 20, B9.ToString(), xf);
        sheet.Cells.Add(rowIndex, 21, A10.ToString(), xf);
        sheet.Cells.Add(rowIndex, 22, B10.ToString(), xf);
        sheet.Cells.Add(rowIndex, 23, A11.ToString(), xf);
        sheet.Cells.Add(rowIndex, 24, B11.ToString(), xf);
        sheet.Cells.Add(rowIndex, 25, A12.ToString(), xf);
        sheet.Cells.Add(rowIndex, 26, B12.ToString(), xf);
        sheet.Cells.Add(rowIndex, 27, A13.ToString(), xf);
        sheet.Cells.Add(rowIndex, 28, B13.ToString(), xf);
        sheet.Cells.Add(rowIndex, 29, A14.ToString(), xf);
        sheet.Cells.Add(rowIndex, 30, B14.ToString(), xf);
        sheet.Cells.Add(rowIndex, 31, A15.ToString(), xf);
        sheet.Cells.Add(rowIndex, 32, B15.ToString(), xf);
        sheet.Cells.Add(rowIndex, 33, A16.ToString(), xf);
        sheet.Cells.Add(rowIndex, 34, B16.ToString(), xf);
        sheet.Cells.Add(rowIndex, 35, A17.ToString(), xf);
        sheet.Cells.Add(rowIndex, 36, B17.ToString(), xf);
        sheet.Cells.Add(rowIndex, 37, A18.ToString(), xf);
        sheet.Cells.Add(rowIndex, 38, B18.ToString(), xf);
        sheet.Cells.Add(rowIndex, 39, A19.ToString(), xf);
        sheet.Cells.Add(rowIndex, 40, B19.ToString(), xf);
        sheet.Cells.Add(rowIndex, 41, A20.ToString(), xf);
        sheet.Cells.Add(rowIndex, 42, B20.ToString(), xf);
        sheet.Cells.Add(rowIndex, 43, A21.ToString(), xf);
        sheet.Cells.Add(rowIndex, 44, B21.ToString(), xf);
        sheet.Cells.Add(rowIndex, 45, A22.ToString(), xf);
        sheet.Cells.Add(rowIndex, 46, B22.ToString(), xf);
        sheet.Cells.Add(rowIndex, 47, A23.ToString(), xf);
        sheet.Cells.Add(rowIndex, 48, B23.ToString(), xf);
        sheet.Cells.Add(rowIndex, 49, A24.ToString(), xf);
        sheet.Cells.Add(rowIndex, 50, B24.ToString(), xf);
        sheet.Cells.Add(rowIndex, 51, A25.ToString(), xf);
        sheet.Cells.Add(rowIndex, 52, B25.ToString(), xf);
        sheet.Cells.Add(rowIndex, 53, A26.ToString(), xf);
        sheet.Cells.Add(rowIndex, 54, B26.ToString(), xf);
        sheet.Cells.Add(rowIndex, 55, A27.ToString(), xf);
        sheet.Cells.Add(rowIndex, 56, B27.ToString(), xf);
        sheet.Cells.Add(rowIndex, 57, A28.ToString(), xf);
        sheet.Cells.Add(rowIndex, 58, B28.ToString(), xf);
        sheet.Cells.Add(rowIndex, 59, A29.ToString(), xf);
        sheet.Cells.Add(rowIndex, 60, B29.ToString(), xf);
        sheet.Cells.Add(rowIndex, 61, A30.ToString(), xf);
        sheet.Cells.Add(rowIndex, 62, B30.ToString(), xf);
        sheet.Cells.Add(rowIndex, 63, A31.ToString(), xf);
        sheet.Cells.Add(rowIndex, 64, B31.ToString(), xf);
    }


    protected DataTable BindData()
    {

        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@departmentID", Convert.ToInt32(Request.QueryString["departmentID"]));
        param[1] = new SqlParameter("@workShop", Convert.ToInt32(Request.QueryString["workShop"]));
        param[2] = new SqlParameter("@uYear", Request.QueryString["uYear"]);
        param[3] = new SqlParameter("@uMonth", Request.QueryString["uMonth"]);
        param[4] = new SqlParameter("@uID", Session["uID"].ToString());
        param[5] = new SqlParameter("@uName", Session["uName"].ToString());
        param[6] = new SqlParameter("@plantCode", Session["plantCode"].ToString());
        return  SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_selectPostAttendace", param).Tables[0];

    }
}