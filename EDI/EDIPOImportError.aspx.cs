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

public partial class EDIPOImportError : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = GetTempError(Session["uID"].ToString());

            drawExcel("<b>����</b>", "<b>�ͻ�����</b>", "<b>�ۿ�</b>", "<b>TCP������</b>", "<b>�ͻ�������</b>", "<b>SW1</b>", "<b>SW2</b>"
                , "<b>��ֹ����</b>", "<b>���</b>", "<b>SZX���۶���</b>", "<b>ATL���۶���</b>", "<b>QAD�ű���</b>", "<b>��Ʒ�ͺ�</b>", "<b>��������(��)</b>"
                , "<b>����(ֻ)</b>", "<b>�Ƶ�</b>", "<b>��ע</b>", "<b>�ɹ��۸�</b>", "<b>����</b>", "<b>��Ʒ</b>","<b>��ŵ��������</b>", "<b>������Ϣ</b>");

            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                drawExcel(ds.Tables[0].Rows[n]["et_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_cust"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_port"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_tcp_po"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_cust_po"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_sw1"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_sw2"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_due_date"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_line"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_szx_so"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_atl_so"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_qad"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_item"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_qty_ord"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_qty_ord1"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_site"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_rmks"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_price"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_sample"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_IsSample"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_promisedDeliveryDate"].ToString().Trim()
                        , ds.Tables[0].Rows[n]["et_errMsg"].ToString().Trim());
            }

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=errMsg.xls");
        }
    }

    protected DataSet GetTempError(string uID)
    {
        try
        {
            string strSql = "sp_edi_selectTempError";

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
