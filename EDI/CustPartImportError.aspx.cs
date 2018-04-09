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
using CommClass;

public partial class CustPartImportError : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = GetTempError(Session["uID"].ToString());
            //															

            drawExcel("<b>客户/货物发往</b>", "<b>客户物料</b>", "<b>物料号</b>", "<b>生效日期</b>", "<b>截止日期</b>", "<b>说明</b>", "<b>显示客户物料</b>");

            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                drawExcel(ds.Tables[0].Rows[n]["cpt_cust"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cpt_cust_part"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cpt_part"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cpt_start_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cpt_end_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cpt_comment"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cpt_cust_partd"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["cpt_errMsg"].ToString().Trim());
            }

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=errMsg.xls");
        }
    }

    protected DataSet GetTempError(string uID)
    {
        try
        {
            string strSql = "sp_edi_selectCustPartTempError";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@uID", uID);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);
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
