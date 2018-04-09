using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Collections.Generic;

public partial class wo2_wo2_routingExport2 : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("工序工艺");

            int rowIndex = 1;
            List<string> routingList = new List<string>();

            #region  打印Excel标题
            DataTable routingHeader = GetHeader();
            foreach (DataRow row in routingHeader.Rows)
            {
                routingList.Add(row[0].ToString());
            }
            if (routingList.Count < 8)
            {
                for (int i = routingList.Count; i < 8; i++)
                {
                    routingList.Add("");
                }
            }
            PrintExcel(doc, sheet, rowIndex, "工艺代码", routingList, "100合计", "QAD合计", "差异");
            routingHeader.Dispose();
            routingList.Clear();
            #endregion

            #region  打印Excel内容
            DataTable routingContent = GetContent();
            if (routingContent.Rows.Count > 0)
            {
                string routing = string.Empty;
                string total = string.Empty;
                string qad = string.Empty;
                string diff = string.Empty;
                foreach (DataRow row in routingContent.Rows)
                {
                    rowIndex++;
                    for (int i = 1; i < 9; i++)
                    {
                        routingList.Add(row[i].ToString());
                    }
                    PrintExcel(doc, sheet, rowIndex, row["wo2_ro_routing"].ToString(), routingList, row["Total"].ToString(), row["QAD"].ToString(), row["Diff"].ToString());
                    routingList.Clear();
                }
                routingContent.Dispose();
            }
            #endregion

            doc.Send();

            Response.Flush();
            Response.End();
        }
    }

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex, string routing, List<string> routingList, string Total, string QAD, string Diff)
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

        sheet.Cells.Add(rowIndex, 1, routing, xf);
        for (int i = 1; i < 9; i++)
        {
            sheet.Cells.Add(rowIndex, i + 1, routingList[i - 1], xf);
        }
        sheet.Cells.Add(rowIndex, 10, Total, xf);
        sheet.Cells.Add(rowIndex, 11, QAD, xf);
        sheet.Cells.Add(rowIndex, 12, Diff, xf);
    }

    protected DataTable GetHeader()
    {
        try
        {
            string strSql = "sp_wo2_selectMopProcByType";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@moptype", Convert.ToInt32(Request.QueryString["tp"]));

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected DataTable GetContent()
    {
        try
        {
            string strSql = "sp_wo2_selectRouting";

            SqlParameter[] parmArray = new SqlParameter[3];
            parmArray[0] = new SqlParameter("@routing", Request.QueryString["ro"].ToString().Trim());
            parmArray[1] = new SqlParameter("@moptype", Convert.ToInt32(Request.QueryString["tp"]));
            parmArray[2] = new SqlParameter("@all", 0);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}