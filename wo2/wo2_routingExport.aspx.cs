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
using QCProgress;
using CommClass;
using System.IO;

public partial class wsline_cs_wl_calendar_pivot_excel : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataGridBind();
        string style = @"<style> .text { mso-number-format:\@; } </script> ";
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=excel.xls");
        Response.ContentType = "application/excel";
        Response.Charset = "defect";
        StringWriter sw = new StringWriter();
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        dgRouting.RenderControl(htw);
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();
    }

    private void DataGridBind()
    {
        DataTable table = GetMopProcByType();

        for (int i = 1; i < 9; i++)
        {
            dgRouting.Columns[i].HeaderText = String.Empty;
        }

        for (int row = 0; row < table.Rows.Count; row++)
        {
            dgRouting.Columns[row + 1].HeaderText = table.Rows[row][0].ToString();
        }

        dgRouting.DataSource = GetRouting();
        dgRouting.DataBind();
    }

    private DataTable GetMopProcByType()
    {
        try
        {
            string strSql = "sp_wo2_selectMopProcByType";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@moptype", Convert.ToInt32(Request.QueryString["tp"]));

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

            return ds.Tables[0];
        }
        catch
        {
            return null;
        }
    }

    private DataSet GetRouting()
    {
        try
        {
            string strSql = "sp_wo2_selectRouting";

            SqlParameter[] parmArray = new SqlParameter[3];
            parmArray[0] = new SqlParameter("@routing", Request.QueryString["ro"].ToString().Trim());
            parmArray[1] = new SqlParameter("@moptype", Convert.ToInt32(Request.QueryString["tp"]));
            parmArray[2] = new SqlParameter("@all", 0);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
        }
        catch
        {
            return null;
        }
    }
}
