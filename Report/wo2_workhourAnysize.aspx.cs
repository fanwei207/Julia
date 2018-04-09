using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

public partial class wo2_wo2_workhourAnysize : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    public string _Result = string.Empty;
    public int _Year = 2013;
    public int _Month = 12;
    public string _Title = "工单汇报工时分布图, 2014.1（上海）";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (int i = 2011; i <= DateTime.Now.Year; i++)
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

            _Year = Convert.ToInt32(dropYear.SelectedValue);
            _Month = Convert.ToInt32(dropMonth.SelectedValue);

            switch (Convert.ToInt32(Session["PlantCode"].ToString()))
            {
                case 1: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（上海）"; break;
                case 2: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（镇江）"; break;
                case 5: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（扬州）"; break;
                case 8: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（淮安）"; break;
            }

            GetData();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        _Year = Convert.ToInt32(dropYear.SelectedValue);
        _Month = Convert.ToInt32(dropMonth.SelectedValue);

        switch (Convert.ToInt32(Session["PlantCode"].ToString()))
        {
            case 1: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（上海）"; break;
            case 2: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（镇江）"; break;
            case 5: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（扬州）"; break;
            case 8: _Title = "工单" + (chk.Checked ? "结算" : "汇报") + "工时分布图, " + _Year.ToString() + "." + _Month.ToString() + "（淮安）"; break;
        }

        GetData();
    }

    public void GetData()
    {
        string strSql = "rep_selectWorkHourAnysize";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@year", dropYear.SelectedValue);
        param[1] = new SqlParameter("@month", dropMonth.SelectedValue);
        param[2] = new SqlParameter("@chk", chk.Checked);
        param[3] = new SqlParameter("@result", SqlDbType.VarChar, 4000);

        param[3].Direction = ParameterDirection.Output;

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, param);

            _Result = param[3].Value.ToString();
        }
        catch
        {
            ;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (!chk.Checked)
        {
            ltlAlert.Text = "alert('请勾选结算')";
            return;
        }
         

        ltlAlert.Text = "window.open('wo2_EffectWorkHourInClearingExcel.aspx?year=" + dropYear.SelectedValue + "&month=" + dropMonth.SelectedValue + "&chk=" + chk.Checked + "')"; 
          
    } 
}