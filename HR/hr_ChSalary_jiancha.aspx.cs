using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wage;

public partial class HR_hr_ChSalary_jiancha : BasePage
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

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    private void dropDeptBind()
    {
        ListItem item;
        item = new ListItem("--", "-0");
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
        DataTable dt = hr_salary.CheckSalary(txtYear.Text.Trim(), dropMonth.Text, txtUserNo.Text.Trim(), txtUserName.Text.Trim(), int.Parse(dropDept.SelectedValue));
        gvSalary.DataSource = dt;
        gvSalary.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string title = "50^工号~^70^姓名~^50^员工类型~^50^年月~^70^基本工资~^70^加班工资~^60^绩效奖~^70^工龄工资~^60^全勤奖~^50^津贴~^70^中夜津贴~^50^国假~^70^应发金额~^60^扣款~^45^公积~^50^养老~^45^工会~^50^所得税~^70^实发金额~^";
        DataTable dt = hr_salary.CheckSalary(txtYear.Text.Trim(), dropMonth.Text, txtUserNo.Text.Trim(), txtUserName.Text.Trim(), int.Parse(dropDept.SelectedValue));
        this.ExportExcel(title, dt, false);
    }
    protected void gvSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSalary.PageIndex = e.NewPageIndex;
        BindData();
    }
}