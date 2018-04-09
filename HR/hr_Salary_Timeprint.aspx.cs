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

public partial class HR_hr_Salary_Timeprint : BasePage
{
    HR hr_salary = new HR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            dropDeptDatabind();

            txtUserNo.Text = "";

            dropInsuranceDatabind();
            dropWorkDatabind();
            dropWorktypeBind();
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

    private void dropInsuranceDatabind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropInsurance.Items.Add(item);

        DataTable dtInsurance = HR.GetInsurance();
        if (dtInsurance.Rows.Count > 0)
        {
            for (int i = 0; i < dtInsurance.Rows.Count; i++)
            {
                item = new ListItem(dtInsurance.Rows[i].ItemArray[1].ToString(), dtInsurance.Rows[i].ItemArray[0].ToString());
                dropInsurance.Items.Add(item);
            }
            dropInsurance.SelectedIndex = 0;
        }
    }

    private void dropWorkDatabind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropWork.Items.Add(item);

        DataTable dtWork = HR.GetEmployType();
        if (dtWork.Rows.Count > 0)
        {
            for (int i = 0; i < dtWork.Rows.Count; i++)
            {
                item = new ListItem(dtWork.Rows[i].ItemArray[1].ToString(), dtWork.Rows[i].ItemArray[0].ToString());
                dropWork.Items.Add(item);
            }
            dropWork.SelectedIndex = 0;
        }
    }


    private void dropWorktypeBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropWorkType.Items.Add(item);

        DataTable dtDropType = HR.GetSalaryType();
        if (dtDropType.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropType.Rows.Count; i++)
            {
                item = new ListItem(dtDropType.Rows[i].ItemArray[1].ToString(), dtDropType.Rows[i].ItemArray[0].ToString());
                dropWorkType.Items.Add(item);
            }
        }
        dropWorkType.SelectedIndex = 0;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string strHeader = txtYear.Text.Trim() + "年" + dropMonth.SelectedValue + "月工资发放单";
        Session["EXHeaderPrint"] = "0,18,1,1036,1,1,1,1," + strHeader;
        Session["EXTitlePrint"] = hr_salary.PrintString(30, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["plantCode"]));
        Session["EXFooterPrint"] = hr_salary.PrintString(31, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["plantCode"]));
        Session["EXSQLPrint"] = hr_salary.PrintString(32, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), 0
                                                      , 0, Convert.ToInt32(dropInsurance.SelectedValue), Convert.ToInt32(dropWork.SelectedValue), txtUserNo.Text.Trim(), Convert.ToInt32(dropWorkType.SelectedValue), Convert.ToInt32(Session["plantCode"]));
        //Response.Write(Session["EXSQLPrint"].ToString());
        //return;
        //Response.Write("<script   language=javascript>window.open('/hr/hr_Salary_Piece.aspx','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');</script>");   
        if (Convert.ToInt16(Session["PlantCode"]) == 1 || Convert.ToInt16(Session["PlantCode"]) == 2)
        {
            Response.Write("<script language=javascript>window.open('/hr/hr_Salary_Piece2.aspx','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');</script>");
        }
        else
        {
            Response.Write("<script language=javascript>window.open('/hr/hr_Salary_Piece.aspx','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');</script>");
        }
    }

    protected void btnGuPrint_Click(object sender, EventArgs e)
    {
        string strHeader = txtYear.Text.Trim() + "年" + dropMonth.SelectedValue + "月工资发放单";
        Session["EXHeaderPrint"] = "0,18,1,1036,1,1,1,1," + strHeader;
        Session["EXTitlePrint"] = hr_salary.PrintString(30, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["plantCode"]));
        Session["EXFooterPrint"] = hr_salary.PrintString(31, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["plantCode"]));
        Session["EXSQLPrint"] = hr_salary.PrintGuString(32, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), 0
                                                     , 0, Convert.ToInt32(dropInsurance.SelectedValue), Convert.ToInt32(dropWork.SelectedValue), txtUserNo.Text.Trim(), Convert.ToInt32(dropWorkType.SelectedValue), Convert.ToInt32(Session["plantCode"]));
        Response.Write("<script   language=javascript>window.open('/hr/hr_Salary_Piece.aspx','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');</script>");
    }
}
