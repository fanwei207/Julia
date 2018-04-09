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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;

public partial class QAD_qad_bom_exportreports : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetBomDocuments(Request.QueryString["qad"], Request.QueryString["beginDate"], Request.QueryString["endDate"], Request.QueryString["hasDocs"].ToString());
        if (dt != null)
        {
            drawExcel("<b>有无文档</b>", "<b>QAD号</b>", "<b>老部件号</b>", "<b>QAD描述</b>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drawExcel(dt.Rows[i]["hasDocs"].ToString(), dt.Rows[i]["qad"].ToString(), dt.Rows[i]["oldcode"].ToString(), dt.Rows[i]["qadDescs"].ToString());
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Write("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=gb2312\"/>");
            Response.AppendHeader("content-disposition", "attachment; filename= AssociatedDocs.xls");
        }
    }

    private void drawExcel(string docs, string qad, string oldcode, string descs)
    {
        TableRow row = new TableRow();

        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = docs.Trim();
        cell.Width = new Unit(100);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = qad.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(150);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = oldcode.Trim();
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        cell.Width = new Unit(150);
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Text = descs.Trim();
        cell.Width = new Unit(300);
        row.Cells.Add(cell);

        excelTable.Rows.Add(row);
    }

    protected DataTable GetBomDocuments(string strQad, string strBeginDate, string strEndDate, string strHasDocs)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@beginDate", strBeginDate);
            param[1] = new SqlParameter("@endDate", strEndDate);
            param[2] = new SqlParameter("@qad", strQad);
            param[3] = new SqlParameter("@hasDocs", strHasDocs);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_qad_selectBomDocuments", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}
