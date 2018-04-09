using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IT;

public partial class IT_TSK_ChargerDay : System.Web.UI.Page
{
    public string Date = string.Empty;
    public string xValues = string.Empty;
    public string yValues = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable table = TaskHelper.SelectChargerDay();

            if (table == null)
            {
                Response.Write("<b>数据拉取失败！</b>");
                return;
            }
            else if (table.Rows.Count == 0)
            {
                Response.Write("<b>没有获取到xValue和yValue！</b>");
                return;
            }

            xValues = table.Rows[0]["xValues"].ToString();
            yValues = table.Rows[0]["yValues"].ToString();

        }
    }
}