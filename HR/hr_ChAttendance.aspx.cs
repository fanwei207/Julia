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
using WOrder;
using Microsoft.ApplicationBlocks.Data;

public partial class HR_hr_ChAttendance : BasePage
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();
    WorkOrder wd = new WorkOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /// <summary>
            /// Bind dropdownlist for the department 
            /// <summary>
            dropDeptBind();
            dropTypeBind();

            txtSDate.Text = DateTime.Today.AddMonths(-1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txtEDate.Text = DateTime.Today.AddDays(-DateTime.Now.Day).ToString("yyyy-MM-dd");
            
            //btnExport_Click();
            if (Session["plantCode"].ToString().Trim() == "1")
            {
                btnUpdate.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
            }
        }
    }

    private void dropDeptBind()
    {
        ListItem item;
        item = new ListItem("--", "-1");
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

    private void dropTypeBind()
    {
        dropType.Items.Add(new ListItem("AÀà", "1"));
        dropType.Items.Add(new ListItem("·ÇAÀà", "0"));
        dropType.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvAttendance.PageIndex = 0;
        gvAttendance.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('hr_ChAttendanceExcel.aspx?dt1=" + txtSDate.Text.Trim() + "&dt2=" + txtEDate.Text.Trim() + "&uno=" + txtUserNo.Text.Trim()
        + "&una=" + txtUserName.Text.Trim() + "&dep=" + dropDept.SelectedValue.ToString() + "&ut=" + dropType.SelectedValue.ToString() + "&fl=" + ((chkUser.Checked) ? "1" : "0") + "', '_blank');";
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strStart = txtSDate.Text.Trim();

        wd.UpdateCheckAttendance(strStart);

        gvAttendance.PageIndex = 0;
        gvAttendance.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsnx()
                , wd.ExportCheckAttendanceSum(txtSDate.Text, txtEDate.Text, txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropType.SelectedValue), 1, (chkUser.Checked) ? 1 : 0)
                , wd.ExportCheckAttendanceSum(txtSDate.Text, txtEDate.Text, txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropType.SelectedValue), 0, (chkUser.Checked) ? 1 : 0)
                , false);
    }
}
