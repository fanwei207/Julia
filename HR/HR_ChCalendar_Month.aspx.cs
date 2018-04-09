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
public partial class HR_HR_ChCalendar_Month : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txb_year.Text = DateTime.Now.Year.ToString();
            dropDepartmentBind();
            BindData();
        }
    }

    private void dropDepartmentBind()
    {
        DataTable dt = GetDepartment();
        this.ddl_dp.DataSource = dt;
        this.ddl_dp.DataBind();
        ddl_dp.Items.Insert(0, new ListItem("--", "0"));
    }

    private DataTable GetDepartment()
    {
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectDepartment").Tables[0];
    }

    private void BindData()
    {
        DataTable dt = GetChCalendar( Convert.ToInt32(ddl_dp.SelectedValue), txb_userno.Text.Trim(), Convert.ToInt32(txb_year.Text.Trim()));
        gv_hac.DataSource = dt;
        gv_hac.DataBind();
    }

    private DataTable GetChCalendar(int departmentID, string userno, int uYear)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@departmentID", departmentID);
        param[1] = new SqlParameter("@userNo", userno);
        param[2] = new SqlParameter("@year", uYear);
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_selectAttendanceCalendar_ch", param).Tables[0];
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txb_year.Text == string.Empty)
        {
            ltlAlert.Text = "alert('年不能为空!')";
        }
        else
        {
            BindData();
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string title = "<b>部门</b>~^<b>工号</b>~^<b>姓名</b>~^<b>年</b>~^<b>1月</b>~^<b>2月</b>~^<b>3月</b>~^<b>4月</b>~^<b>5月</b>~^<b>6月</b>~^<b>7月</b>~^<b>8月</b>~^<b>9月</b>~^<b>10月</b>~^<b>11月</b>~^<b>12月</b>~^<b>小计</b>~^";
        DataTable dt = GetChCalendar(Convert.ToInt32(ddl_dp.SelectedValue), txb_userno.Text.Trim(), Convert.ToInt32(txb_year.Text.Trim()));
        this.ExportExcel(title, dt, false);
    }
    protected void gv_hac_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_hac.PageIndex = e.NewPageIndex;
        BindData();
    }
}