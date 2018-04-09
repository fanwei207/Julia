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
using adamFuncs;
using Wage;

public partial class HR_hr_attendance_Statistics : BasePage
{
    HR hr_salary = new HR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropDepartmentBind();

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            gvAttendance.DataBind();
        }
    }

    private void dropDepartmentBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDepartment.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDepartment.Items.Add(item);
            }
        }
        dropDepartment.SelectedIndex = 0;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvAttendance.PageIndex = 0;
        gvAttendance.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
                , hr_salary.AttendanceStatistic(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(),
                                                Convert.ToInt32(dropDepartment.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), chkAll.Checked, Convert.ToInt32(txtDayStart.Text.Trim().Length == 0 ? "0" : txtDayStart.Text.Trim()), Convert.ToInt32(txtDayEnd.Text.Trim().Length == 0 ? "0" : txtDayEnd.Text.Trim()), 1)
                , hr_salary.AttendanceStatistic(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(),
                    Convert.ToInt32(dropDepartment.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), chkAll.Checked, Convert.ToInt32(txtDayStart.Text.Trim().Length == 0 ? "0" : txtDayStart.Text.Trim()), Convert.ToInt32(txtDayEnd.Text.Trim().Length == 0 ? "0" : txtDayEnd.Text.Trim()), 0)
            , false);
    }
}
