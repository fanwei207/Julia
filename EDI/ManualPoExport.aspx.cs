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
using CommClass;

public partial class ManualPoExport : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        string cust = Request.QueryString["cust"];
        string nbr = Request.QueryString["nbr"];
        string reqDate = Request.QueryString["reqDate"];
        string dueDate = Request.QueryString["dueDate"];
        string shipTo = Request.QueryString["shipTo"];
        string shipVia = Request.QueryString["shipVia"];
        string createdBy = Request.QueryString["createdBy"];
        string createdDate = Request.QueryString["createdDate"];


        DataSet ds = GetManualPoExport(cust, nbr, reqDate, dueDate, shipTo, shipVia, createdBy, createdDate);
        drawExcel("<b>Cust</b>", "<b>Cust Po</b>", "<b>Req Date</b>", "<b>Due Date</b>", "<b>Ship To</b>", "<b>Ship Via</b>", "<b>Remarks1</b>", "<b>So Nbr</b>", "<b>Domain</b>", "<b>Submit Date</b>"
                    , "<b>Line</b>", "<b>Cust Part</b>", "<b>QAD</b>", "<b>Qty</b>", "<b>UM</b>", "<b>Price</b>", "<b>Req Date</b>", "<b>Due Date</b>", "<b>Remarks2</b>", "<b>Site</b>", "<b>IsSample</b>");
        int n;
        for (n = 0; n < ds.Tables[0].Rows.Count; n++)
        {
            drawExcel(ds.Tables[0].Rows[n]["mpo_cust"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_nbr"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_req_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_due_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_shipto"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_shipvia"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_rmks"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_so_nbr"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_so_domain"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpo_submittedDate"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_line"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_cust_part"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_qad"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_ord_qty"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_um"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_price"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_req_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_due_date"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_rmks"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_sod_site"].ToString().Trim()
                         , ds.Tables[0].Rows[n]["mpod_IsSample"].ToString().Trim());
        }

        ds.Reset();

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.AppendHeader("content-disposition", "attachment; filename= ppu.xls");
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

    private DataSet GetManualPoExport(string cust, string nbr, string reqDate, string dueDate, string shipTo, string shipVia, string createdBy, string createdDate)
    {
        SqlParameter[] param = new SqlParameter[8];
        param[0] = new SqlParameter("@cust", cust);
        param[1] = new SqlParameter("@nbr", nbr);
        param[2] = new SqlParameter("@reqDate", reqDate);
        param[3] = new SqlParameter("@dueDate", dueDate);
        param[4] = new SqlParameter("@shipTo", shipTo);
        param[5] = new SqlParameter("@shipVia", shipVia);
        param[6] = new SqlParameter("@createdBy", createdBy);
        param[7] = new SqlParameter("@createdDate", createdDate);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectManualPoExport", param);
        return ds;
    }
}
