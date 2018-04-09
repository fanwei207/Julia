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
using System.ComponentModel;
using adamFuncs;

public partial class wo2_list_slow4sHisExcel : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        drawExcel("<b>地点</b>", "<b>成本中心</b>", "<b>工单号</b>", "<b>ID号</b>", "<b>工艺代码</b>", "<b>工单数量</b>", "<b>标准工时</b>", "<b>入库数量</b>", "<b>工单工时</b>", "<b>汇报工时</b>", "<b>差异</b>", "<b>生产线</b>", "<b>结算日期</b>");
        int n;
        for (n = 0; n < dt.Rows.Count; n++)
        {
            drawExcel(dt.Rows[n]["wo_site"].ToString().Trim(), dt.Rows[n]["wo_flr_cc"].ToString().Trim(), "'" + dt.Rows[n]["wo_nbr"].ToString().Trim(), dt.Rows[n]["wo_lot"].ToString().Trim(), dt.Rows[n]["wo_routing"].ToString().Trim()
                       , dt.Rows[n]["wo_qty_ord"].ToString().Trim(), dt.Rows[n]["ro_tool"].ToString().Trim(), dt.Rows[n]["wo_qty_comp"].ToString().Trim()
                       , dt.Rows[n]["wo_cost"].ToString().Trim(), dt.Rows[n]["rep_cost"].ToString().Trim(), dt.Rows[n]["rep_diff"].ToString().Trim()
                       , dt.Rows[n]["wo_line"].ToString().Trim(), dt.Rows[n]["wo_close_date"].ToString().Trim());
        }

        dt.Reset();

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.AppendHeader("content-disposition", "attachment; filename= excel.xls");
    }

    private void drawExcel(params string[] str)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        foreach (string s in str)
        {
            TableCell cell = new TableCell();
            cell.Text = s.Trim();

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"\d{10,}");

            if (reg.IsMatch(cell.Text))
            {
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            }
            row.Cells.Add(cell);
        }

        exl.Rows.Add(row);
    }

    private DataTable GetData()
    {
        string strSql = "sp_wo2_selectWorkOrderHoursListHis";

        SqlParameter[] sqlParam = new SqlParameter[7];
        sqlParam[0] = new SqlParameter("@date1", Request.QueryString["date1"].ToString());
        sqlParam[1] = new SqlParameter("@date2", Request.QueryString["date2"].ToString());
        sqlParam[2] = new SqlParameter("@part", Request.QueryString["part"].ToString());
        sqlParam[3] = new SqlParameter("@nbr", Request.QueryString["nbr"].ToString());
        sqlParam[4] = new SqlParameter("@lot", Request.QueryString["lot"].ToString());
        sqlParam[5] = new SqlParameter("@isClosed", Request.QueryString["isClosed"].ToString());
        sqlParam[6] = new SqlParameter("@isReported", Request.QueryString["isReported"].ToString());

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        return dt;
    }
}
