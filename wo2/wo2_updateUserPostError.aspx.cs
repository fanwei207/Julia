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

public partial class wo2_wo2_updateUserPostError : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = GetUsersPostTempError(Session["uID"].ToString());
            if (dt != null)
            {
                DrawExcel("有误的员工岗位信息");
                DrawExcel("工号", "工序名称", "岗位名称", "错误描述", true);
                foreach (DataRow row in dt.Rows)
                {
                    DrawExcel(row["userNo"].ToString(), row["processName"].ToString(), row["postName"].ToString(), row["errorDesc"].ToString(), false);
                }
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                Response.Write("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=gb2312\"/>");
                Response.AppendHeader("content-disposition", "attachment; filename= WrongUsersPost.xls");
            }
        }
    }

    protected void DrawExcel(string excelTitle)
    {
        TableRow row = new TableRow();

        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = excelTitle;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(800);
        cell.ColumnSpan = 4;
        cell.BackColor = System.Drawing.Color.Yellow;
        row.Cells.Add(cell);

        excelTable.Rows.Add(row);
    }

    protected void DrawExcel(string userNo, string processName, string postName, string errorDesc, bool isTitle)
    {
        TableRow row = new TableRow();

        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = userNo;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = processName;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = postName;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = errorDesc;
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(500);
        if (!isTitle)
        {
            cell.ForeColor = System.Drawing.Color.Red;
        }
        row.Cells.Add(cell);

        excelTable.Rows.Add(row);
    }

    protected DataTable GetUsersPostTempError(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", uID);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_selectUsersPostError", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}
