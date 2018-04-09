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

public partial class qc_passrate_export : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["det"] == null)
            {
                drawExcel("<b>工单</b>", "<b>ID号</b>", "<b>批序号</b>", "<b>物料号</b>", "<b>描述</b>", "<b>工单日期</b>", "<b>截止日期</b>", "<b>工单数量</b>", "<b>缺陷类别</b>", "<b>样本量</b>", "<b>次品数量</b>", "<b>次品率</b>", "<b>检验日期</b>");

                DataTable dt = GetData();

                for (int n = 0; n < dt.Rows.Count; n++)
                {
                    drawExcel(dt.Rows[n]["wo_nbr"].ToString().Trim()
                            , dt.Rows[n]["wo_lot"].ToString().Trim()
                            , dt.Rows[n]["cp_seriers"].ToString().Trim()
                            , dt.Rows[n]["wo_part"].ToString().Trim()
                            , dt.Rows[n]["pt_desc"].ToString().Trim()
                            , dt.Rows[n]["wo_ord_date"].ToString().Trim()
                            , dt.Rows[n]["wo_due_date"].ToString().Trim()
                            , dt.Rows[n]["wo_qty_ord"].ToString().Trim()
                            , dt.Rows[n]["typeName"].ToString().Trim()
                            , dt.Rows[n]["cp_sample"].ToString().Trim()
                            , dt.Rows[n]["cp_num"].ToString().Trim()
                            , dt.Rows[n]["cp_rate"].ToString().Trim()
                            , dt.Rows[n]["prdDate"].ToString().Trim());
                }

                dt.Dispose();
            }

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=rate.xls");
        }
    }

    protected DataTable GetData()
    {
        try
        {
            string strSql = "sp_QC_Rep_Cp100";

            SqlParameter[] sqlParam = new SqlParameter[8];
            sqlParam[0] = new SqlParameter("@nbr1", Request.QueryString["nbr1"].ToString());
            sqlParam[1] = new SqlParameter("@nbr2", Request.QueryString["nbr2"].ToString());
            sqlParam[2] = new SqlParameter("@orddate1", Request.QueryString["orddate1"].ToString());
            sqlParam[3] = new SqlParameter("@orddate2", Request.QueryString["orddate2"].ToString());
            sqlParam[4] = new SqlParameter("@duedate1", Request.QueryString["duedate1"].ToString());
            sqlParam[5] = new SqlParameter("@duedate2", Request.QueryString["duedate2"].ToString());
            sqlParam[6] = new SqlParameter("@typeID", Request.QueryString["type"].ToString());
            sqlParam[7] = new SqlParameter("@tcp", Request.QueryString["tcp"].ToString());

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
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


}
