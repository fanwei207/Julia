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

public partial class HR_hr_Salary_print : BasePage
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


            ListItem item;
            item = new ListItem("--", "0");
            dropWorkshop.Items.Add(item);

            dropWorkgroup.Items.Add(item);

            txtUserNo.Text = "";

            dropInsuranceDatabind();
            dropWorkDatabind();
        }
    }

    #region Department/Workshop/Workgroup
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

    private void dropWorkshopDatabind()
    {
        dropWorkshop.Items.Clear();
        ListItem item;
        item = new ListItem("--", "0");
        dropWorkshop.Items.Add(item);

        if (dropDept.SelectedIndex > 0)
        {
            DataTable dtWorkshop = HR.GetWorkShop(Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(dropDept.SelectedValue));
            if (dtWorkshop.Rows.Count > 0)
            {
                for (int i = 0; i < dtWorkshop.Rows.Count; i++)
                {
                    item = new ListItem(dtWorkshop.Rows[i].ItemArray[1].ToString(), dtWorkshop.Rows[i].ItemArray[0].ToString());
                    dropWorkshop.Items.Add(item);
                }
            }
            dropWorkshop.SelectedIndex = 0;
        }

    }


    private void dropWorkgroupDatabind()
    {
        dropWorkgroup.Items.Clear();
        ListItem item;
        item = new ListItem("--", "0");
        dropWorkgroup.Items.Add(item);

        if (dropWorkshop.SelectedIndex > 0)
        {
            DataTable dtWorkgroup = HR.GetWorkGroup(Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(dropWorkshop.SelectedValue));
            if (dtWorkgroup.Rows.Count > 0)
            {
                for (int i = 0; i < dtWorkgroup.Rows.Count; i++)
                {
                    item = new ListItem(dtWorkgroup.Rows[i].ItemArray[1].ToString(), dtWorkgroup.Rows[i].ItemArray[0].ToString());
                    dropWorkgroup.Items.Add(item);
                }
            }
        }

        dropWorkgroup.SelectedIndex = 0;
    }

    /// <summary>
    /// 部门选择变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropWorkshopDatabind();

        dropWorkgroup.Items.Clear();

        //重置班组
        ListItem item;
        item = new ListItem("--", "0");
        dropWorkgroup.Items.Add(item);

    }

    protected void dropWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropWorkgroupDatabind();
    }

    #endregion

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

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        //" 0-类别,1-每页的记录条数,2-标头,3-宽度,4-是否需要本页小计,5-是否要总人数，6-是否需要行间隔,7-是否需要输出页码,8-标头内容"
        int status=0;
        if (RadioButton2.Checked)
        {
            status = 1;
        }
        else if (RadioButton3.Checked)
        {
            status = 2;
        }

           
        string strHeader = txtYear.Text.Trim() + "年" + dropMonth.SelectedValue +"月工资发放单";
        Session["EXHeaderPrint"] = "0,18,1,1036,1,1,1,1," + strHeader;

        Session["EXTitlePrint"] = hr_salary.PrintString(0, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0,Convert.ToInt32(Session["PlantCode"]));
        Session["EXFooterPrint"] = hr_salary.PrintString(1, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["PlantCode"]));
        Session["EXSQLPrint"] = hr_salary.PrintString(2, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropWorkshop.SelectedValue)
                                                      , Convert.ToInt32(dropWorkgroup.SelectedValue), Convert.ToInt32(dropInsurance.SelectedValue), Convert.ToInt32(dropWork.SelectedValue), txtUserNo.Text.Trim(), 0, Convert.ToInt32(Session["PlantCode"]), status);
        //Response.Write(Session["EXSQLPrint"].ToString());

        if (Convert.ToInt16(Session["PlantCode"]) == 1 || Convert.ToInt16(Session["PlantCode"]) == 2)
        {
            Response.Write("<script language=javascript>window.open('/hr/hr_Salary_Piece1.aspx','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');</script>");
        }
        else
        {
            Response.Write("<script language=javascript>window.open('/hr/hr_Salary_Piece.aspx','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');</script>");
        }
    }
}
