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

public partial class HR_hr_FinExportToBank : BasePage
{
    HR hr_salary = new HR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dropBankBind();
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = Convert.ToString (DateTime.Now.Month-1);

            btnConfirm.Attributes.Add("onclick", "return confirm('点击后当月计件工资将不能更改，确定要继续吗？');");
            btnConfirmtime.Attributes.Add("onclick", "return confirm('点击后当月计时工资将不能更改，确定要继续吗？');");
        }
    }

    private void dropBankBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropBank.Items.Add(item);

        DataTable dtDropBank = HR.GetBank(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropBank.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropBank.Rows.Count; i++)
            {
                item = new ListItem(dtDropBank.Rows[i].ItemArray[1].ToString(), dtDropBank.Rows[i].ItemArray[0].ToString());
                dropBank.Items.Add(item);
            }
        }
        dropBank.SelectedIndex = 0;
                   
    }

    protected void btnBank_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0(), "<b>工号</b>~^<b>姓名</b>~^<b>实发金额</b>~^<b>账号</b>~^<b>银行</b>~^"
            , hr_salary.FinancetoBank(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropBank.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0), false);
    }


    protected void btnFintotal_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0(), hr_salary.FinanceSalary(0, 0, 0, 10), hr_salary.FinanceSalary(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0), false);  
    }


    protected void btmSummary_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0(), hr_salary.FinanceSalarySummary(0, 0, 0, 10), hr_salary.FinanceSalarySummary(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0), false);
    }


    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0);
        if (intadjust < 0)
        {
            string str = @"<script language='javascript'> alert('不能重复操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
            return;
        }

        int intflag = hr_salary.FinanceComfirm(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]),0);
        if (intflag < 0)
        {
            string str = @"<script language='javascript'> alert('操作出错,请重新操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fin", str);
            return;
        }
    }

    protected void btnConfirmtime_Click(object sender, EventArgs e)
    {
        int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 1);
        if (intadjust < 0)
        {
            string str = @"<script language='javascript'> alert('不能重复操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
            return;
        }

        int intflag = hr_salary.FinanceComfirm(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]),1);
        if (intflag < 0)
        {
            string str = @"<script language='javascript'> alert('操作出错,请重新操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "fin", str);
            return;
        }
    }

    protected void btnExportSalary_Click(object sender, EventArgs e)
    {
        string str = "sp_Hr_SalaryTmp";
        SqlParameter[] parmArray = new SqlParameter[3];
        parmArray[0] = new SqlParameter("@Plantcode", Convert.ToInt32(Session["plantcode"]));
        parmArray[1] = new SqlParameter("@Year", Convert.ToInt32(txtYear.Text.Trim()));
        parmArray[2] = new SqlParameter("@Month", Convert.ToInt32(dropMonth.SelectedValue));
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, parmArray);


        this.ExportExcel(adam.dsn0(), hr_salary.exportSalaryTmp(Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 101), hr_salary.exportSalaryTmp(Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 1), false);
    }
    protected void btnBankTest_Click(object sender, EventArgs e)
    {
        this.ExportExcel(hr_salary.FinancetoBank(0, 0, 0, 0, 10)
                , hr_salary.FinancetoBank(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropBank.SelectedValue))
                , false);
    }
}
