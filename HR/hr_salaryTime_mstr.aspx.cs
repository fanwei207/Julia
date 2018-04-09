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
using System.Text.RegularExpressions;

public partial class HR_hr_salaryTime_mstr : BasePage
{
    
    HR hr_salary = new HR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /// <summary>
            /// Bind dropdownlist for the department 
            /// <summary>
            dropDeptBind();
            dropWorktypeBind();

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            gvSalary.DataBind();

            if (Session["PlantCode"].ToString() == "1")
            {
                gvSalary.Columns[4].Visible = false;
                gvSalary.Columns[5].Visible = false;
                gvSalary.Columns[6].Visible = false;
                gvSalary.Columns[7].Visible = false;

            }

            btnSalary.Attributes.Add("onclick", "return confirm('结算会清除上次的内容，确定要开始结算吗？');");
            btnBkSalary.Attributes.Add("onclick", "return confirm('确定要进行备份结算吗？');");

        }
    }

    private void dropDeptBind()
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

    private void dropWorktypeBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropWorktype.Items.Add(item);

        DataTable dtDropType = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropType.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropType.Rows.Count; i++)
            {
                item = new ListItem(dtDropType.Rows[i].ItemArray[1].ToString(), dtDropType.Rows[i].ItemArray[0].ToString());
                dropWorktype.Items.Add(item);
            }
        }
        dropWorktype.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvSalary.DataBind();
    }

    protected void btnBkSalary_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtYear.Text.Length == 0 || Convert.ToInt32(txtYear.Text) < 1900)
            {
                string strScr = @"<script language='javascript'> alert('输入年份有误!例如:2004');form1.txtYear.focus(); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Year1", strScr);
                return;
            }

            int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 1);
            if (intadjust < 0)
            {
                string str = @"<script language='javascript'> alert('工资已被财务冻结，不能重复操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
                return;
            }

            hr_salary.DeleteSalaryDataTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue),0);
            int intError;

            intError = hr_salary.CalculateTimeSalaryPT(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Uid"]), Convert.ToInt32(Session["Plantcode"]));

            if (intError < 0)
            {
                string strScr = @"<script language='javascript'> alert('结算出错，请重新结算');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error1", strScr);
                return;
            }


            gvSalary.DataBind();
        }
        catch
        {
            string strScr = @"<script language='javascript'> alert('结算出错，请重新结算');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strScr);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsnx()
                , hr_salary.ExportSalaryTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 1)
                , hr_salary.ExportSalaryTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 0)
                , true);
    }


    protected void btnSalary_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtYear.Text.Length == 0 || Convert.ToInt32(txtYear.Text) < 1900)
            {
                string strScr = @"<script language='javascript'> alert('输入年份有误!例如:2004');form1.txtYear.focus(); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Year1", strScr);
                return;
            }

            int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 1);
            if (intadjust < 0)
            {
                string str = @"<script language='javascript'> alert('工资已被财务冻结，不能重复操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
                return;
            }

            hr_salary.DeleteSalaryDataTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue),0);
            int intError;

            intError = hr_salary.CalculateTimeSalaryPT(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Uid"]), Convert.ToInt32(Session["Plantcode"]));

            if (intError < 0)
            {
                string strScr = @"<script language='javascript'> alert('结算出错，请重新结算');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error1", strScr);
                return;
            }

            gvSalary.DataBind();
        }
        catch
        {
            string strScr = @"<script language='javascript'> alert('结算执行异常，请重新结算');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strScr);
        }
    }
}
