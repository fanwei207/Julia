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

public partial class wsline_wl_calendarModiToExcel : System.Web.UI.Page
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
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("考勤调整信息");

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
                //考勤时间列
                AppLibrary.WriteExcel.ColumnInfo checkTime = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                checkTime.ColumnIndexStart = 5;
                checkTime.ColumnIndexEnd = 5;
                checkTime.Width = 160 * 6000 / 164;
                sheet.AddColumnInfo(checkTime);
                //考勤类型列
                AppLibrary.WriteExcel.ColumnInfo checktype = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                checktype.ColumnIndexStart = 6;
                checktype.ColumnIndexEnd = 6;
                checktype.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(checktype);
                //补漏类型列
                AppLibrary.WriteExcel.ColumnInfo isCompany = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                isCompany.ColumnIndexStart = 7;
                isCompany.ColumnIndexEnd = 7;
                isCompany.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(isCompany);
                //考勤号列
                AppLibrary.WriteExcel.ColumnInfo AttendanceUserNo = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                AttendanceUserNo.ColumnIndexStart = 8;
                AttendanceUserNo.ColumnIndexEnd = 8;
                AttendanceUserNo.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(AttendanceUserNo);
                //修改者列
                AppLibrary.WriteExcel.ColumnInfo modifiedby = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                modifiedby.ColumnIndexStart = 9;
                modifiedby.ColumnIndexEnd = 9;
                modifiedby.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(modifiedby);
                //修改日期列
                AppLibrary.WriteExcel.ColumnInfo modifiedDate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                modifiedDate.ColumnIndexStart = 10;
                modifiedDate.ColumnIndexEnd = 10;
                modifiedDate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(modifiedDate);
                //是否补漏列
                AppLibrary.WriteExcel.ColumnInfo isManual = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                isManual.ColumnIndexStart = 11;
                isManual.ColumnIndexEnd = 11;
                isManual.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(isManual);
                //打卡机号列
                AppLibrary.WriteExcel.ColumnInfo Sensorid = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                isManual.ColumnIndexStart = 12;
                isManual.ColumnIndexEnd = 12;
                isManual.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(Sensorid);
                //打卡机属地列
                AppLibrary.WriteExcel.ColumnInfo plantcode = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                isManual.ColumnIndexStart = 13;
                isManual.ColumnIndexEnd = 13;
                isManual.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(plantcode);

                #endregion

                int rowIndex = 1;
                PrintExcel(doc, sheet, rowIndex, "成本中心代码", "成本中心", "工号", "姓名", "员工类型", "考勤时间", "考勤类型", "补漏类型", "考勤号", "修改者", "修改日期", "是否补漏", "打卡机号", "打卡机属地", "职务");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex, row["AttendanceUserCenter"], row["AttendanceUserCenterName"], row["AttendanceUserCode"], row["AttendanceUserName"], row["AttendanceUserType"]
                                , row["checkTime"], row["checktype"], row["isCompany"], row["AttendanceUserNo"], row["modifiedby"], row["modifiedDate"], row["isManual"], row["Sensorid"], row["plantcode"], row["roleName"]);
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
                                ,Object AttendanceUserCenter, Object AttendanceUserCenterName, Object AttendanceUserCode, Object AttendanceUserName, Object AttendanceUserType
                                , Object checkTime, Object checktype, Object isCompany, Object AttendanceUserNo, Object modifiedby, Object modifiedDate, Object isManual, Object Sensorid, Object plantcode, Object roleName)
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
        sheet.Cells.Add(rowIndex, 6, checkTime.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, checktype.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, isCompany.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, AttendanceUserNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 10, modifiedby.ToString(), xf);
        sheet.Cells.Add(rowIndex, 11, modifiedDate.ToString(), xf);
        sheet.Cells.Add(rowIndex, 12, isManual.ToString(), xf);
        sheet.Cells.Add(rowIndex, 13, Sensorid.ToString(), xf);
        sheet.Cells.Add(rowIndex, 14, plantcode.ToString(), xf);
        sheet.Cells.Add(rowIndex, 15, roleName.ToString(), xf);
    }
    protected DataTable GetAttendanceInfo()
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@plantID", Convert.ToInt32(Request.QueryString["plantID"]));
        param[1] = new SqlParameter("@userCenter", Request.QueryString["userCenter"]);
        param[2] = new SqlParameter("@userType", Convert.ToInt32(Request.QueryString["userType"]));
        param[3] = new SqlParameter("@userCode", Request.QueryString["userCode"]);
        param[4] = new SqlParameter("@checkTime", Request.QueryString["checkTime"]);
        param[5] = new SqlParameter("@checktype", Request.QueryString["checktype"]);
        param[6] = new SqlParameter("@isCompany", Convert.ToInt32(Request.QueryString["isCompany"]));
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_SelectAttendanceInfoToExcel", param).Tables[0];
    }
}