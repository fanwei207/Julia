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
            DataTable dt;

            drawExcel("<b>��Ӧ��</b>", "<b>��Ӧ������</b>", "<b>�ɹ���</b>", "<b>�к�</b>", "<b>���Ϻ�</b>", "<b>����</b>", "<b>��������</b>", "<b>��ֹ����</b>", "<b>�ջ���</b>", "<b>�ջ�����</b>", "<b>�ջ�����</b>", "<b>������</b>", "<b>��������</b>", "<b>�Ƿ���</b>", "<b>������</b>", "<b>�ؼ��ɹ�</b>");

            dt = GetData();

            for (int n = 0; n < dt.Rows.Count; n++)
            {
                drawExcel(dt.Rows[n]["prh_vend"].ToString().Trim()
                        , dt.Rows[n]["ad_name"].ToString().Trim()
                        , dt.Rows[n]["prh_nbr"].ToString().Trim()
                        , dt.Rows[n]["prh_line"].ToString().Trim()
                        , dt.Rows[n]["prh_part"].ToString().Trim()
                        , dt.Rows[n]["pt_desc"].ToString().Trim()
                        , dt.Rows[n]["po_ord_date"].ToString().Trim()
                        , dt.Rows[n]["pod_due_date"].ToString().Trim()
                        , dt.Rows[n]["prh_receiver"].ToString().Trim()
                        , dt.Rows[n]["prh_rcp_date"].ToString().Trim()
                        , dt.Rows[n]["prh_rcvd"].ToString().Trim()
                        , dt.Rows[n]["pt_insp_lead"].ToString().Trim()
                        , dt.Rows[n]["prh_date"].ToString().Trim()
                        , dt.Rows[n]["OverDue"].ToString().Trim()
                        , dt.Rows[n]["userName"].ToString().Trim()
                        , dt.Rows[n]["isKeyPart"].ToString().Trim());
            }

            dt.Dispose();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=rate.xls");
        }
    }

    protected DataTable GetData()
    {
        try
        {
            string strSql = "sp_QC_Rep_IncomingAssess";

            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@vend", Request.QueryString["vend"].ToString());
            sqlParam[1] = new SqlParameter("@part", Request.QueryString["p"].ToString());
            sqlParam[2] = new SqlParameter("@stddate", Request.QueryString["d1"].ToString());
            sqlParam[3] = new SqlParameter("@enddate", Request.QueryString["d2"].ToString());
            sqlParam[4] = new SqlParameter("@overdue", Request.QueryString["o"].ToString());

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
