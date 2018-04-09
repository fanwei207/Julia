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

public partial class ManualPoImportError : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = GetTempError(Session["uID"].ToString());
            //															

            drawExcel("<b>Cust</b>", "<b>Cust Po</b>", "<b>Req Date</b>", "<b>Due Date</b>", "<b>Ship To</b>", "<b>Ship Via</b>", "<b>Remarks1</b>", "<b>Line</b>", "<b>Cust Part</b>", "<b>QAD</b>", "<b>Qty</b>", "<b>UM</b>", "<b>Price</b>", "<b>Req Date</b>", "<b>Due Date</b>", "<b>Description</b>", "<b>Remarks2</b>", "<b>IsSample</b>", "<b>SaleDomain</b>", "<b>Curr</b>", "<b>ErrMsg</b>");

            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                drawExcel(ds.Tables[0].Rows[n]["hrd_cust"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["hrd_nbr"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["hrd_req_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["hrd_due_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["hrd_shipto"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["hrd_shipvia"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["hrd_rmks"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_line"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_cust_part"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_qad"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_ord_qty"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_um"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_price"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_req_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_due_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_desc"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["det_rmks"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["IsSample"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["saleDomain"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["curr"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["hrd_errMsg"].ToString().Trim());
            }

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=errMsg.xls");
        }
    }

    protected DataSet GetTempError(string uID)
    {
        try
        {
            string strSql = "sp_edi_selectManualPoTempError";

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
