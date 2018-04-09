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

public partial class TSK_DemoView : BasePage
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
}