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
using adamFuncs;

public partial class wsline_wl_calendarModiIncludToExcel : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = GetAttendanceInfo();

            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("考勤调整信息(包含工段)");

                #region 设定列宽
                //成本中心代码列
                AppLibrary.WriteExcel.ColumnInfo AttendanceUserCenter = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                AttendanceUserCenter.ColumnIndexStart = 0;
                AttendanceUserCenter.ColumnIndexEnd = 0;
                AttendanceUserCenter.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(AttendanceUserCenter);
                //成本中心列
                AppLibrary.WriteExcel.ColumnInfo AttendanceUserCenterName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                AttendanceUserCenterName.ColumnIndexStart = 1;
                AttendanceUserCenterName.ColumnIndexEnd = 1;
                AttendanceUserCenterName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(AttendanceUserCenterName);
                //工号列
                AppLibrary.WriteExcel.ColumnInfo AttendanceUserCode = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                AttendanceUserCode.ColumnIndexStart = 2;
                AttendanceUserCode.ColumnIndexEnd = 2;
                AttendanceUserCode.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(AttendanceUserCode);
                //姓名列
                AppLibrary.WriteExcel.ColumnInfo AttendanceUserName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                AttendanceUserName.ColumnIndexStart = 3;
                AttendanceUserName.ColumnIndexEnd = 3;
                AttendanceUserName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(AttendanceUserName);
                //员工类型列
                AppLibrary.WriteExcel.ColumnInfo AttendanceUserType = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                AttendanceUserType.ColumnIndexStart = 4;
                AttendanceUserType.ColumnIndexEnd = 4;
                AttendanceUserType.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(AttendanceUserType);
                //工段列
                AppLibrary.WriteExcel.ColumnInfo workshopName = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                workshopName.ColumnIndexStart = 5;
                workshopName.ColumnIndexEnd = 5;
                workshopName.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(workshopName);
                //考勤时间列
                AppLibrary.WriteExcel.ColumnInfo checkTime = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                checkTime.ColumnIndexStart = 6;
                checkTime.ColumnIndexEnd = 6;
                checkTime.Width = 160 * 6000 / 164;
                sheet.AddColumnInfo(checkTime);
                //考勤类型列
                AppLibrary.WriteExcel.ColumnInfo checktype = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                checktype.ColumnIndexStart = 7;
                checktype.ColumnIndexEnd = 7;
                checktype.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(checktype);
                //补漏类型列
                AppLibrary.WriteExcel.ColumnInfo isCompany = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                isCompany.ColumnIndexStart = 8;
                isCompany.ColumnIndexEnd = 8;
                isCompany.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(isCompany);
                //考勤号列
                AppLibrary.WriteExcel.ColumnInfo AttendanceUserNo = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                AttendanceUserNo.ColumnIndexStart = 9;
                AttendanceUserNo.ColumnIndexEnd = 9;
                AttendanceUserNo.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(AttendanceUserNo);
                //修改者列
                AppLibrary.WriteExcel.ColumnInfo modifiedby = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                modifiedby.ColumnIndexStart = 10;
                modifiedby.ColumnIndexEnd = 10;
                modifiedby.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(modifiedby);
                //修改日期列
                AppLibrary.WriteExcel.ColumnInfo modifiedDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                modifiedDate.ColumnIndexStart = 11;
                modifiedDate.ColumnIndexEnd = 11;
                modifiedDate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(modifiedDate);
               

                #endregion

                int rowIndex = 1;
                PrintExcel(doc, sheet, rowIndex, "成本中心代码", "成本中心", "工号", "姓名", "员工类型", "工段", "考勤时间", "考勤类型", "补漏类型", "考勤号", "修改者", "修改日期");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex, row["AttendanceUserCenter"], row["AttendanceUserCenterName"], row["AttendanceUserCode"], row["AttendanceUserName"], row["AttendanceUserType"]
                                , row["workshopName"], row["checkTime"], row["checktype"], row["isCompany"], row["AttendanceUserNo"], row["modifiedby"], row["modifiedDate"]);
                }
                doc.Send();

                Response.Flush();
                Response.End();
                dt.Dispose();

            }
            else
            {
                Response.Redirect("wl_calendarModi.aspx");
            }
        }

    }
    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex
                                , Object AttendanceUserCenter, Object AttendanceUserCenterName, Object AttendanceUserCode, Object AttendanceUserName, Object AttendanceUserType
                                , Object workshopName, Object checkTime, Object checktype, Object isCompany, Object AttendanceUserNo, Object modifiedby, Object modifiedDate)
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

        sheet.Cells.Add(rowIndex, 1, AttendanceUserCenter.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, AttendanceUserCenterName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, AttendanceUserCode.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, AttendanceUserName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, AttendanceUserType.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, workshopName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, checkTime.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, checktype.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, isCompany.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, AttendanceUserNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 11, modifiedby.ToString(), xf);
        sheet.Cells.Add(rowIndex, 12, modifiedDate.ToString(), xf);
        



    }
    protected DataTable GetAttendanceInfo()
    {
        SqlParameter[] param = new SqlParameter[7];
        string aa = chk.dsnx();
        param[0] = new SqlParameter("@plantID", Convert.ToInt32(Request.QueryString["plantID"]));
        param[1] = new SqlParameter("@userCenter", Request.QueryString["userCenter"]);
        param[2] = new SqlParameter("@userType", Convert.ToInt32(Request.QueryString["userType"]));
        param[3] = new SqlParameter("@userCode", Request.QueryString["userCode"]);
        param[4] = new SqlParameter("@checkTime", Request.QueryString["checkTime"]);
        param[5] = new SqlParameter("@checktype", Request.QueryString["checktype"]);
        param[6] = new SqlParameter("@isCompany", Convert.ToInt32(Request.QueryString["isCompany"]));
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_SelectAttendanceInfoIncludToExcel", param).Tables[0];
    }
}
