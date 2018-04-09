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

    public string Categories = string.Empty;
    public string SZX = string.Empty;
    public string ZQL = string.Empty;
    public string YQL = string.Empty;
    public string HQL = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStdDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-31));
            txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            DateTime _dtStd = Convert.ToDateTime(txtStdDate.Text);
            DateTime _dtEnd = Convert.ToDateTime(txtEndDate.Text);

            while (_dtStd <= _dtEnd)
            {
                Categories += "'" + string.Format("{0:MM-dd}", _dtStd) + "', ";

                _dtStd = _dtStd.AddDays(1);
            }

            Categories = Categories.Substring(0, Categories.Length - 2);
            GetAnalyzeRealtimeAccess();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime _dtStd, _dtEnd;

        if (txtStdDate.Text.Length > 0 && !this.IsDate(txtStdDate.Text))
        {
            this.Alert("监控日期 格式不正确！");
            return;
        }

        _dtStd = Convert.ToDateTime(txtStdDate.Text);

        if (txtEndDate.Text.Length > 0 && !this.IsDate(txtEndDate.Text))
        {
            this.Alert("监控日期 格式不正确！");
            return;
        }

        _dtEnd = Convert.ToDateTime(txtEndDate.Text);

        if ((_dtEnd - _dtStd).Days < 10 )
        {
            this.Alert("监控日期 间隔太近！请保持间隔在10天以上");
            return;
        }

        while (_dtStd <= _dtEnd)
        {
            Categories += "'" + string.Format("{0:MM-dd}", _dtStd) + "', ";

            _dtStd = _dtStd.AddDays(1);
        }

        Categories = Categories.Substring(0, Categories.Length - 2);

        GetAnalyzeRealtimeAccess();
    }

    public void GetAnalyzeRealtimeAccess()
    {
        string strSql = "rep_analyzeRealtimeAccess";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@stdDate", txtStdDate.Text);
        param[1] = new SqlParameter("@endDate", txtEndDate.Text);
        param[2] = new SqlParameter("@szx", SqlDbType.VarChar, 4000);
        param[3] = new SqlParameter("@zql", SqlDbType.VarChar, 4000);
        param[4] = new SqlParameter("@yql", SqlDbType.VarChar, 4000);
        param[5] = new SqlParameter("@hql", SqlDbType.VarChar, 4000);

        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        param[4].Direction = ParameterDirection.Output;
        param[5].Direction = ParameterDirection.Output;

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, param);

            SZX = param[2].Value.ToString();
            ZQL = param[3].Value.ToString();
            YQL = param[4].Value.ToString();
            HQL = param[5].Value.ToString();
        }
        catch
        {
            ;
        }
    }
}