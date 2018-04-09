using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wage;

public partial class HR_hr_DinnerReportDetailExportExcel : System.Web.UI.Page
{
    HR hr = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Request.QueryString["strUserNo"].Length == 0 ? hr.SelectDinnerDetailInfo(Convert.ToDateTime(Request.QueryString["date"]), Convert.ToInt32(Request.QueryString["Plant"])).Tables[0] : hr.SelectDinnerDetailUserInfo(Convert.ToDateTime(Request.QueryString["date"]), Request.QueryString["strUserNo"], Convert.ToInt32(Request.QueryString["Plant"]), Convert.ToBoolean(Request.QueryString["isChk"])).Tables[0];

            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("就餐信息明细");

                #region 设定列宽
                //考勤编号列
                AppLibrary.WriteExcel.ColumnInfo DinnerUserNo = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                DinnerUserNo.ColumnIndexStart = 0;
                DinnerUserNo.ColumnIndexEnd = 0;
                DinnerUserNo.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(DinnerUserNo);
                //员工工号列
                AppLibrary.WriteExcel.ColumnInfo DinnerUserCode = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                DinnerUserCode.ColumnIndexStart = 1;
                DinnerUserCode.ColumnIndexEnd = 1;
                DinnerUserCode.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(DinnerUserCode);
                //员工姓名列
                AppLibrary.WriteExcel.ColumnInfo DinnerUserName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                DinnerUserName.ColumnIndexStart = 2;
                DinnerUserName.ColumnIndexEnd = 2;
                DinnerUserName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(DinnerUserName);
                //就餐时间列
                AppLibrary.WriteExcel.ColumnInfo DinnerTime = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                DinnerTime.ColumnIndexStart = 3;
                DinnerTime.ColumnIndexEnd = 3;
                DinnerTime.Width = 160 * 6000 / 164;
                sheet.AddColumnInfo(DinnerTime);
                //设备编号列
                AppLibrary.WriteExcel.ColumnInfo DinnerSensor = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                DinnerSensor.ColumnIndexStart = 4;
                DinnerSensor.ColumnIndexEnd = 4;
                DinnerSensor.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(DinnerSensor);
                //部门列
                AppLibrary.WriteExcel.ColumnInfo Deptment = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                Deptment.ColumnIndexStart = 5;
                Deptment.ColumnIndexEnd = 5;
                Deptment.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(Deptment);
                //工段列
                AppLibrary.WriteExcel.ColumnInfo GongDuan = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                GongDuan.ColumnIndexStart = 6;
                GongDuan.ColumnIndexEnd = 6;
                GongDuan.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(GongDuan);

                #endregion

                int rowIndex = 1;
                PrintExcel(doc, sheet, rowIndex, "考勤编号", "员工工号", "员工姓名", "就餐时间", "设备编号", "部门", "工段");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex, row["DinnerUserNo"], row["DinnerUserCode"], row["DinnerUserName"], row["DinnerTime"], row["DinnerSensor"], row["Deptment"], row["GongDuan"]);
                }
                doc.Send();

                Response.Flush();
                Response.End();
                dt.Dispose();

            }
            else
            {
                Response.Redirect("hr_DinnerReportDetail.aspx");
            }
        }

    }
    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex
                               , Object DinnerUserNo, Object DinnerUserCode, Object DinnerUserName, Object DinnerTime, Object DinnerSensor, Object Deptment, Object GongDuan)
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

        sheet.Cells.Add(rowIndex, 1, DinnerUserNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, DinnerUserCode.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, DinnerUserName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, DinnerTime.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, DinnerSensor.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, Deptment.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, GongDuan.ToString(), xf);
        
    }
}