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

public partial class HR_hr_CompareAT : BasePage
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            dropDeptBind();
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
        gvCompare.PageIndex = 0;
        gvCompare.DataBind();
    }

    protected void gvCompare_DataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
            , hr_salary.AttDinerDetailString(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropMonth.SelectedValue), 0, Convert.ToInt32(Session["plantcode"]), 1)
            , hr_salary.AttDinerDetailString(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropMonth.SelectedValue), 0, Convert.ToInt32(Session["plantcode"]), 2)
            , true);
    }
    protected void gvCompare_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;

            Response.Redirect("hr_CompareATDetail.aspx?yr=" + txtYear.Text.Trim() + "&mh=" + dropMonth.SelectedValue + "&uid=" + gvCompare.DataKeys[index].Values[0].ToString());
        }
    }
    protected void btnExport0_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
            , hr_salary.AttDinerString(txtYear.Text.Trim(), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(Session["plantcode"]), 1)
            , hr_salary.AttDinerString(txtYear.Text.Trim(), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(Session["plantcode"]), 0)
            , false);
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
            , hr_salary.AttDinerDetailAllString(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropMonth.SelectedValue), 0, Convert.ToInt32(Session["plantcode"]), 1)
            , hr_salary.AttDinerDetailAllString(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropMonth.SelectedValue), 0, Convert.ToInt32(Session["plantcode"]), 2)
            , true);
    }
}
