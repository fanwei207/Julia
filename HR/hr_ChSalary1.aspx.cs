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

public partial class HR_hr_ChSalary1 : BasePage
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

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
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
        dropType.Items.Add(new ListItem("A类", "1"));
        dropType.Items.Add(new ListItem("非A类", "0"));
        dropType.SelectedIndex = 0;
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvSalary.PageIndex = 0;
        gvSalary.DataBind();
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        Session["EXSQL"] = wd.ExportCheckSalary1(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), 0, Convert.ToInt32(dropType.SelectedValue));
        Session["EXTitle"] = wd.ExportCheckSalary1(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), 1, Convert.ToInt32(dropType.SelectedValue));
        Session["EXHeader"] = "";
        ltlAlert.Text = "window.open('/public/exportExcel.aspx', '_blank');";
    }
}
