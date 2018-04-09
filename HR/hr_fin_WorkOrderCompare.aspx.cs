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
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;


public partial class HR_hr_fin_WorkOrderCompare : BasePage
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

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            gvFinance.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvFinance.PageIndex = 0;
        gvFinance.DataBind();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string EXTitle = hr_salary.WorkOrderCompare(txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 1);
        string ExSql = hr_salary.WorkOrderCompare(txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0);

        this.ExportExcel(adam.dsn0(), EXTitle, ExSql, false);
    }
}
