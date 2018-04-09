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

public partial class Report_realtimeaccessanalyze : BasePage
{
    adamClass adam = new adamClass();

    public string Result = string.Empty;
    public int Year = 2013;
    public int Month = 12;

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

            Year = Convert.ToInt32(dropYear.SelectedValue);
            Month = Convert.ToInt32(dropMonth.SelectedValue) - 1;

            GetRealtimeInUse();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Year = Convert.ToInt32(dropYear.SelectedValue);
        Month = Convert.ToInt32(dropMonth.SelectedValue) - 1;

        GetRealtimeInUse();
    }

    public void GetRealtimeInUse()
    {
        string strSql = "rep_selectRealtimeInUse";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@year", dropYear.SelectedValue);
        param[1] = new SqlParameter("@month", dropMonth.SelectedValue);
        param[2] = new SqlParameter("@result", SqlDbType.VarChar, 4000);

        param[2].Direction = ParameterDirection.Output;

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, param);

            Result = param[2].Value.ToString();
        }
        catch
        {
            ;
        }
    }
}