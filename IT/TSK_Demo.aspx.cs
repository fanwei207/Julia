using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

public partial class IT_TSK_Demo : BasePage
{
    protected string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            demoHolder.InnerHtml = GetDemoHtml();
        }
    }

    protected string GetDemoHtml()
    {
        try
        {
            string strSql = "sp_demo_selectDemoHtmlById";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@detId", Request.QueryString["detId"]);

            return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param).ToString();
        }
        catch
        {
            return "";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _html = hidDemoHtml.Value;
        _html = _html.Replace("&gt;", ">");
        _html = _html.Replace("&lt;", "<");

        try
        {
            string strSql = "sp_demo_saveDemoHtml";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@detId", Request.QueryString["detId"]);
            param[1] = new SqlParameter("@html", _html);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

            if (!Convert.ToBoolean(param[2].Value))
            {
                this.Alert("Fail to save.");
            }
        }
        catch
        {
            this.Alert("Db level error.");
        }

        demoHolder.InnerHtml = _html;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TSK_DemoMstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}