

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

public partial class HR_hr_salary_Compare : BasePage
{
    HR hr_salary = new HR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropDeptDatabind();
           
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            dropVersionDatabind();
        }
    }

    private void dropDeptDatabind()
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

    private void dropVersionDatabind()
    {
        dropVersion.Items.Clear();

        ListItem item;
        item = new ListItem("--", "0");
        dropVersion.Items.Add(item);

        DataTable dtDropVersion = HR.GetSalaryVersion(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Plantcode"]));
        if (dtDropVersion.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropVersion.Rows.Count; i++)
            {
                item = new ListItem(dtDropVersion.Rows[i].ItemArray[0].ToString() + "-" + Convert.ToDateTime(dtDropVersion.Rows[i].ItemArray[1]).ToShortDateString(),dtDropVersion.Rows[i].ItemArray[0].ToString());
                dropVersion.Items.Add(item);
            }
        }
        dropVersion.SelectedIndex = 0;
    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ExportExcel();
        gvSalaryCompare.PageIndex = 0;
        gvSalaryCompare.DataBind();
    }

    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ExportExcel();
        dropVersionDatabind();
        gvSalaryCompare.DataBind();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        ExportExcel();
        dropVersionDatabind();
        gvSalaryCompare.DataBind();
    }

    private void ExportExcel()
    {
        Session["EXSQL"] = hr_salary.ExportSalaryCompare(Convert.ToInt32 (txtYear.Text),Convert.ToInt32(dropMonth.SelectedValue ),txtUserNo.Text ,txtUserName.Text ,Convert.ToInt32(dropDept.SelectedValue ),Convert.ToInt32(dropVersion.SelectedValue ),1) ;
        Session["EXTitle"] = hr_salary.ExportSalaryCompare(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), txtUserNo.Text, txtUserName.Text, Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropVersion.SelectedValue), 0); 
        Session["EXHeader"] = "";
    }
}
