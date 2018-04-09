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
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class Desktop : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    /// <summary>
    /// 生成脚本
    /// </summary>
    /// <returns></returns>
    public string BuildHomePageScripts()
    {
        StringBuilder _builder = new StringBuilder("");

        DataTable table = this.GetUserHomePages(Session["uID"].ToString());

        if (table != null)
        {
            for (int i = 0; i < table.Rows.Count; i++ )
            {
                int top = 95 * (i % 5) + 10;
                int left = 100 * (i / 5);

                _builder.Append("<a id=\"" + table.Rows[i]["id"].ToString() + "\" class=\"icon\" style=\"width: 80px; height: 80px; top: " + top.ToString() + "px; left:" + left.ToString() + "px;\" href=\"#" + table.Rows[i]["url"].ToString() + "?HeightPixel=" + Session["HeightPixel"].ToString() + "\" doc=\"" + table.Rows[i]["doc"].ToString() + "\">");
                _builder.Append("<img src=\"/images/icon.gif\" alt=\"\" />" + table.Rows[i]["name"].ToString());
                _builder.Append("</a>");
            }

            //显示空白的
            if (table.Rows.Count < 15)
            {
                int top = 95 * (table.Rows.Count % 5) + 10;
                int left = 100 * (table.Rows.Count / 5);

                _builder.Append("<a id=\"19010220\" class=\"icon\" style=\"width: 80px; height: 80px; top: " + top.ToString() + "px; left:" + left.ToString() + "px;\" href=\"#/admin/Sethomepage.aspx?HeightPixel=" + Session["HeightPixel"].ToString() + "\" doc=\"\">");

                if (Convert.ToInt32(Session["PlantCode"]) == 98 || Convert.ToInt32(Session["PlantCode"]) == 99)
                {
                    _builder.Append("<img src=\"/images/icon_add.gif\" alt=\"\" /> Shortcut");
                }
                else
                {
                    _builder.Append("<img src=\"/images/icon_add.gif\" alt=\"\" /> 快捷方式");
                }

                _builder.Append("</a>");
            }
        }

        return _builder.ToString();
    }

    public string ReportScript
    { 
        get
        {
            if (Session["ReportProc"] == null || string.IsNullOrEmpty(Session["ReportProc"].ToString()))
            {
                return "$('#divReporter').html('由于Session丢失或为空，导致无法获取系统报表！');";
            }

            string strSql = Session["ReportProc"].ToString();
            SqlParameter[] param = new SqlParameter[1];

            try
            {
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql).ToString();
            }
            catch
            {
                return "$('#divReporter').html('连接数据库失败！');";
            }
        }  
    }

    public string ReportScript2
    {
        get
        {
            string strSql = "rep_noneInProcess";
            SqlParameter[] param = new SqlParameter[1];

            try
            {
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql).ToString();
            }
            catch
            {
                return "$('#divReporter2').html('连接数据库失败！');";
            }
        }
    }
}
