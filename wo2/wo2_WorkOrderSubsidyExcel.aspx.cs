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
using QCProgress;
using CommClass;
using System.IO;

public partial class wo2_wo2_WorkOrderSubsidyExcel : System.Web.UI.Page 
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        int departmentID = Convert.ToInt32(Request.QueryString["dp"].ToString().Trim());
        string loginName = Request.QueryString["un"].ToString().Trim();
        int uYear = Convert.ToInt32(Request.QueryString["ye"].ToString().Trim());
        int uMonth = Convert.ToInt32(Request.QueryString["mo"].ToString().Trim());
        int workshop = Convert.ToInt32(Request.QueryString["ws"].ToString().Trim());
        string workcenter = Request.QueryString["wc"].ToString().Trim();
        bool isValue = Convert.ToBoolean(Request.QueryString["iv"].ToString().Trim());

        DataTable dt = GetWorkOrderSubsidy(departmentID, loginName, uYear, uMonth, workshop, workcenter, isValue);

        drawExcel("<b>工号</b>", "<b>姓名</b>", "<b>部门</b>", "<b>工段</b>", "<b>工时</b>");

        for (int n = 0; n < dt.Rows.Count; n++ )
        {
            drawExcel(dt.Rows[n]["wo2_userNo"].ToString().Trim(), dt.Rows[n]["wo2_userName"].ToString().Trim(), dt.Rows[n]["deptName"].ToString().Trim(), dt.Rows[n]["workshop"].ToString().Trim(), dt.Rows[n]["wo_subsidysum"].ToString().Trim());
        }

        dt.Reset();
        Response.ContentType = "application/excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.AddHeader("content-disposition", "attachment; filename=excel.xls");
    }

    private DataTable GetWorkOrderSubsidy(int departmentID, string loginName, int uYear, int uMonth, int workshop, string workcenter, bool isValue)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@dept", departmentID);
        param[1] = new SqlParameter("@userNo", loginName);
        param[2] = new SqlParameter("@year", uYear);
        param[3] = new SqlParameter("@month", uMonth);
        param[4] = new SqlParameter("@workshop", workshop);
        param[5] = new SqlParameter("@workcenter", workcenter);
        param[6] = new SqlParameter("@isValue", isValue);

        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectWorkOrderSubsidy", param).Tables[0];
    }

    private void drawExcel(string str1, string str2, string str3, string str4, string str5)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.Text = str1.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str2.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str3.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str4.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = str5.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        wo_excel.Rows.Add(row);
    }
}
