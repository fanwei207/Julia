using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_completedandreported1 : BasePage
{
    adamClass adam = new adamClass();

    /// <summary>
    /// 未汇报
    /// </summary>
    public string NonReported = string.Empty;
    /// <summary>
    /// 已汇报
    /// </summary>
    public string Reported = string.Empty;
    /// <summary>
    /// 坐标轴
    /// </summary>
    public string Category = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (int i = 2013; i <= DateTime.Now.Year; i++)
            {
                dropYear.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            }

            try
            {
                dropYear.SelectedIndex = -1;
                dropMonth.SelectedIndex = -1;

                dropYear.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
                dropMonth.Items.FindByText(DateTime.Now.Month.ToString()).Selected = true;
            }
            catch
            {
                ;
            }

            GetReport();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetReport();
    }

    public void GetReport()
    {
        string strSql = "rep_selectWoCompletedAndReported1";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@year", dropYear.SelectedValue);
        param[1] = new SqlParameter("@month", dropMonth.SelectedValue);

        param[2] = new SqlParameter("@category", SqlDbType.NVarChar, 1000);
        param[2].Direction = ParameterDirection.Output;

        param[3] = new SqlParameter("@reported", SqlDbType.VarChar, 4000);
        param[3].Direction = ParameterDirection.Output;

        param[4] = new SqlParameter("@non_reported", SqlDbType.VarChar, 4000);
        param[4].Direction = ParameterDirection.Output;

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, param);

            Category = param[2].Value.ToString();
            Reported = param[3].Value.ToString();
            NonReported = param[4].Value.ToString();
        }
        catch
        {
            ;
        }
    }
}