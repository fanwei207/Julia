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

public partial class HR_hr_Salary_AdjustTime : BasePage
{
    HR hr_salary = new HR();
    adamClass chk = new adamClass();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("6056150", "计时工资调整（财务）");
            this.Security.Register("6056160", "计时工资调整（工资导出）");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dropDeptDatabind();
            dropWorktypeDatabind();

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();


            lblUsername.Text = "";
            lblUserID.Text = "0";
            lblSalaryID.Text = "0";
            gvSalaryAdjust.DataBind();
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

    private void dropWorktypeDatabind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropWorktype.Items.Add(item);

        DataTable dtDropType = HR.GetSalaryType();
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

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        gvSalaryAdjust.PageIndex = 0;
        gvSalaryAdjust.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtPercent.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtPercent.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('调整比 只能为数字');";
                return;
            }
        }

        if (txtMoney.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtMoney.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('调整金额 只能为数字');";
                return;
            }
        }

        if (lblUserID.Text == "0" && dropDept.SelectedIndex == 0  && dropWorktype.SelectedIndex == 0 )
        {
            string str = @"<script language='javascript'> alert('必须输入一个工号或选择部门,计酬方式等'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Save", str);
            return;
        }

        if (txtPercent.Text.Trim().Length == 0 && txtMoney.Text.Trim().Length == 0)
        {
            string str = @"<script language='javascript'> alert('调整比和调整金额不能为空'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Save1", str);
            return;
        }

     
        int intFlag = hr_salary.AdjustSalaryTimeSave(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(dropDept.SelectedValue),
                                    Convert.ToInt32(dropWorktype.SelectedValue), lblUserID.Text,
                                   Convert.ToDecimal((txtPercent.Text.Trim().Length == 0) ? "0" : txtPercent.Text), Convert.ToDecimal((txtMoney.Text.Trim().Length == 0) ? "0" : txtMoney.Text),
                                   Convert.ToInt32(Session["Uid"]), Convert.ToInt32(lblSalaryID.Text),txtReason.Text.Trim());

        if (intFlag < 0)
        {
            string str = @"<script language='javascript'> alert('保存出错,请重新调整!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SaveMes", str);
        }
        else
        {
            string str = @"<script language='javascript'> alert('保存成功!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SaveMes1", str);

            lblSalaryID.Text = "0";
            lblUserID.Text = "0";
            lblUsername.Text = "";
            txtInputUser.Text = "";
            txtMoney.Text = "";
            txtPercent.Text = "";
        }
        gvSalaryAdjust.DataBind();
        gvSalaryAdjust.PageIndex = 0;
    }

    protected void btnFin_Click(object sender, EventArgs e)
    {
        if (!this.Security["6056150"].isValid)
        {
            Response.Redirect("~/public/denied.htm");
        }
        // the button can use only o
        int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]),1);
        if (intadjust < 0)
        {
            string str = @"<script language='javascript'> alert('工资已被财务冻结，不能重复操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
            return;
        }
        else
        {
            int intMes = hr_salary.AdjustToFinacialTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Uid"]));
            if (intMes < 0)
            {
                string str = @"<script language='javascript'> alert('操作出错,请重新操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fin", str);
                return;
            }
        }
    }

    protected void txtInputUser_TextChanged(object sender, EventArgs e)
    {
        if (txtInputUser.Text.Trim().Length > 0)
        {
            string strCheckUser = hr_salary.SalaryTimeUserCheck(txtInputUser.Text, Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Uid"]));
            if (strCheckUser.Trim().Length > 0)
            {
                string[] strUser = strCheckUser.Split(',');
                lblUserID.Text = strUser[0];
                lblUsername.Text = strUser[1];
                lblSalaryID.Text = strUser[2];

                this.txtPercent.Focus();
            }
            else
            {
                lblUserID.Text = "0";
                lblUsername.Text = "";
                lblSalaryID.Text = "";
                string str = @"<script language='javascript'> alert('此工号不存在工资结算中或没有权利操作'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "User", str);
                this.txtInputUser.Focus();

            }
        }
    }

       /// <summary>
    /// Recalculate tax in salary.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRecalculate_Click(object sender, EventArgs e)
    {
        if (!this.Security["6056150"].isValid)
        {
            Response.Redirect("~/public/denied.htm");
        }
        int intMes = hr_salary.AdjustTimeSalry(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue),Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]));
        if (intMes < 0)
        {
            string str = @"<script language='javascript'> alert('重置税操作出错,请重新操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fin", str);
            return;
        }
    }



    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (!this.Security["6056160"].isValid)
        {
            Response.Redirect("~/public/denied.htm");
        }
        string EXTitle = hr_salary.ExportFin_SalaryTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 1, Convert.ToInt32(Session["PlantCode"]));
        string ExSql = hr_salary.ExportFin_SalaryTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 0, Convert.ToInt32(Session["PlantCode"]));

        this.ExportExcel(chk.dsnx(), EXTitle, ExSql, false);

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string EXTitle = hr_salary.AdjustSalaryTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropWorktype.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(Session["Uid"]), 1);
        string ExSql = hr_salary.AdjustSalaryTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropWorktype.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(Session["Uid"]), 0);

        this.ExportExcel(chk.dsn0(), EXTitle, ExSql, false);
    }
}
