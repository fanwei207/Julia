using System;
using System.Data;
using System.Web.UI.WebControls;
using adamFuncs;
using Wage;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;


public partial class wo_cost_wo_workRateReports : BasePage
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            dropS.SelectedValue = DateTime.Now.Month.ToString();
            dropE.SelectedValue = DateTime.Now.Month.ToString();

            BindDept();
        }
    }

    private void BindDept()
    {

        ListItem item;
        item = new ListItem("--", "0");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));

        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
    }


    //单月工效分析报表
    protected void btnSMReport_Click(object sender, EventArgs e)
    {
        if (txtYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('年份不能为空!');";
            return;
        }
        else
        {

        }

        if (txtDayStart.Text.Trim().Length == 0 || txtDayEnd.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('日期不能为空!');";
            return;
        }
        else
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-" + txtDayStart.Text.Trim());
                DateTime dd1 = Convert.ToDateTime(txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-" + txtDayEnd.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('日期数值不正确!');";
                return;
            }
        }

        Session["EXHeader"] = Session["orgName"].ToString() + "       " + txtYear.Text.Trim() + "年" + dropMonth.SelectedValue + "月" + "      " + txtDayStart.Text.Trim() + "  -  " + txtDayEnd.Text.Trim() + "日  工效比报表";
        Session["EXTitle"] = SMReport(txtYear.Text.Trim(), dropMonth.SelectedValue, txtDayStart.Text.Trim(), txtDayEnd.Text.Trim(), Convert.ToInt32(Session["plantcode"]), 1);
        Session["EXSQL"] = SMReport(txtYear.Text.Trim(), dropMonth.SelectedValue, txtDayStart.Text.Trim(), txtDayEnd.Text.Trim(), Convert.ToInt32(Session["plantcode"]), 0);
        ltlAlert.Text = "window.open('/public/exportExcel.aspx?rt="+DateTime.Now.ToString()+"')";
    }


    //全年工效分析报表
    protected void btnSYReport_Click(object sender, EventArgs e)
    {

        if (txtYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('年份不能为空!');";
            return;
        }


        if (Convert.ToInt32(dropS.SelectedValue) > Convert.ToInt32(dropE.SelectedValue))
        {
            ltlAlert.Text = "alert('结束月份不能小于开始月份!');";
            return;
        }

        Session["EXHeader"] = Session["orgName"].ToString() + "       " + txtYear.Text.Trim() + "年" + dropS.SelectedValue + "月   -   " + dropE.SelectedValue + "月  工效比报表";
        Session["EXTitle"] = SYReport(txtYear.Text.Trim(), dropS.SelectedValue, dropE.SelectedValue, dropDept.SelectedValue, Convert.ToInt32(Session["plantcode"]), 1);
        Session["EXSQL"] = SYReport(txtYear.Text.Trim(), dropS.SelectedValue, dropE.SelectedValue, dropDept.SelectedValue, Convert.ToInt32(Session["plantcode"]), 0);
        ltlAlert.Text = "window.open('/public/exportExcel.aspx?rt=" + DateTime.Now.ToString() + "')";
    }


    private string SMReport(string strYear, string strMonth, string strSday, string strEday, int intPlantcode, int intType)
    {
        try
        {
            string str = "sp_wo_SingleMonthReport1";
            SqlParameter[] parmArray = new SqlParameter[6];
            parmArray[0] = new SqlParameter("@syear", strYear);
            parmArray[1] = new SqlParameter("@smonth", strMonth);
            parmArray[2] = new SqlParameter("@Sday", strSday);
            parmArray[3] = new SqlParameter("@Eday", strEday);
            parmArray[4] = new SqlParameter("@plantcode", intPlantcode);
            parmArray[5] = new SqlParameter("@Type", intType);

            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray).ToString();
        }
        catch
        {
            return "1111";
        }



    }

    private string SYReport(string strYear, string strMS, string strME, string departmentID, int intPlantcode, int intType)
    {
        try
        {
            string str = "sp_wo_SingleYearReport";
            SqlParameter[] parmArray = new SqlParameter[6];
            parmArray[0] = new SqlParameter("@syear", strYear);
            parmArray[1] = new SqlParameter("@smonth", strMS);
            parmArray[2] = new SqlParameter("@emonth", strME);
            parmArray[3] = new SqlParameter("@departmentID", departmentID);
            parmArray[4] = new SqlParameter("@plantcode", intPlantcode);
            parmArray[5] = new SqlParameter("@Type", intType);

            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray).ToString();
        }
        catch
        {
            return "222";
        }
    }
}
