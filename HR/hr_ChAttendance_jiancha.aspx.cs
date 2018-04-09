using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wage;

public partial class HR_hr_ChAttendance_jiancha : BasePage
{

    HR_Ch hr_salary = new HR_Ch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /// <summary>
            /// Bind dropdownlist for the department 
            /// <summary>
            dropDeptBind();

            txtSDate.Text = DateTime.Today.AddMonths(-1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txtEDate.Text = DateTime.Today.AddDays(-DateTime.Now.Day).ToString("yyyy-MM-dd");
            BindData();
        }
    }

    private void dropDeptBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR_Ch.GetDepartment(Convert.ToInt32(Session["Plantcode"]));

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

    private void BindData()
    {
        DataTable dt = hr_salary.CheckAttendance(txtSDate.Text.Trim(), txtEDate.Text.Trim(), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), int.Parse(dropDept.SelectedValue));
        gvAttendance.DataSource = dt;
        gvAttendance.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gvAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAttendance.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string title = "<b>部门</b>~^<b>班组</b>~^<b>工号</b>~^<b>姓名</b>~^<b>日期</b>~^<b>上班时间</b>~^<b>下班时间</b>~^<b>下班时间1</b>~^<b>上班时间1</b>~^";
        DataTable dt = hr_salary.CheckAttendance(txtSDate.Text.Trim(), txtEDate.Text.Trim(), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), int.Parse(dropDept.SelectedValue));
        this.ExportExcel(title, dt, true);
    }
}