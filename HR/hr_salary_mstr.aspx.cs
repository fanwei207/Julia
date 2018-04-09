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
using Microsoft.ApplicationBlocks.Data;

public partial class HR_hr_salary_mstr : BasePage
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

            if (Session["PlantCode"].ToString() == "1")
            {
                gvSalary.Columns[4].Visible = false;
                gvSalary.Columns[5].Visible = false;
                gvSalary.Columns[6].Visible = false;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvSalary.PageIndex = 0;
        gvSalary.DataBind();
    }

    /// <summary>
    /// 工资结算 / btnSalary
    /// 清除所选当月的工资数据，重新计算
    /// </summary>
    protected void btnSalary_Click(object sender, EventArgs e)
    {
        if (txtYear.Text.Length == 0 || Convert.ToInt32(txtYear.Text) < 1900)
        {
            string strScr = @"<script language='javascript'> alert('输入年份有误!例如:2004');form1.txtYear.focus(); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Year1", strScr);
            return;
        }


        try
        {
            int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0);
            if (intadjust < 0)
            {
                string str = @"<script language='javascript'> alert('工资已被财务冻结，不能重复操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
                return;
            }

            hr_salary.DeleteSalaryData(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue));

            int intflag = hr_salary.CheckSalaryValidity(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(Session["Uid"]), Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn9"]));
            if (intflag < 0 && Convert.ToInt32(Session["plantCode"]) != 1)
            {
                string strScr = @"<script language='javascript'> alert('检查出错');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CheckError", strScr);
                return;
            }
            else
            {
                if (intflag > 0)
                {
                    string str = "SELECT count(*) FROM tcpc0.dbo.hr_SalaryCheck WHERE creat ='" + Session["Uid"] + "' AND plantCode ='" + Session["Plantcode"] + "'And flag=1 ";
                    int intNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.Text, str));
                    if (intNumber > 0)
                    {
                        this.ExportExcel(adam.dsnx()
                            , "<b>加工单号</b>~^<b>加工单ID</b>~^<b>地点</b>~^<b>成本中心</b>~^<b>标志(1-汇报工时 > QAD工时 )</b>~^<b>QAD工时</b>~^<b>汇报工时</b>~^<b>差额</b>~^<b>结算日期</b>~^<b>工艺(A类时间)</b>~^<b>工艺(A+B)</b>~^<b>数量(当结算日期不为NULL,为完工入库数,反之需求数量)</b>~^<b>工艺</b>~^"
                            , "SELECT worder,wID,wsite,wocd_cc,flag,wcost,tcost,wValue,sdate,ro_tool,ro_run,wo_qty,wocd_tec FROM tcpc0.dbo.hr_SalaryCheck WHERE creat ='" + Session["Uid"] + "' AND plantCode ='" + Session["Plantcode"] + "'And flag=1  ORDER BY worder "
                            , false);

                        return;
                    }
                }
            }

            int intError;

            intError = hr_salary.CalculateSalaryPT(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), txtYear.Text + "-" + dropMonth.SelectedValue.ToString() + "-01", Convert.ToInt32(Session["Uid"]), 0);
            if (intError < 0)
            {
                string strScr = @"<script language='javascript'> alert('结算出错A，请重新结算');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error1", strScr);
                return;
            }

            intError = hr_salary.CalculateSalaryPT(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), txtYear.Text + "-" + dropMonth.SelectedValue.ToString() + "-01", Convert.ToInt32(Session["Uid"]), 1);

            if (intError < 0)
            {
                string strScr = @"<script language='javascript'> alert('结算出错B，请重新结算');</script>";
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

    protected void btnBkSalary_Click(object sender, EventArgs e)
    {

        ///<summary>
        /// 规范年份的输入
        /// </summary>
        /// 
        if (txtYear.Text.Length == 0 || Convert.ToInt32(txtYear.Text) < 1900)
        {
            string strScr = @"<script language='javascript'> alert('输入年份有误!例如:2004');form1.txtYear.focus(); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Year1", strScr);
            return;
        }

        try
        {
            int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0);
            if (intadjust < 0)
            {
                string str = @"<script language='javascript'> alert('工资已被财务冻结，不能重复操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
                return;
            }

            hr_salary.BackupSalaryDate(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 0);
            hr_salary.DeleteSalaryData(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue));

            int intflag = hr_salary.CheckSalaryValidity(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(Session["Uid"]), Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn9"]));
            if (intflag < 0 && Convert.ToInt32(Session["plantCode"]) != 1)
            {
                string strScr = @"<script language='javascript'> alert('检查出错，请重新结算');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CheckError", strScr);
                return;
            }
            else
            {
                if (intflag > 0)
                {
                    Session["EXSQL1"] = "SELECT worder,wID,wsite,wocd_cc,flag,wcost,tcost,wValue,sdate,ro_tool,ro_run,wo_qty,wocd_tec FROM tcpc0.dbo.hr_SalaryCheck WHERE creat ='" + Session["Uid"] + "' AND plantCode ='" + Session["Plantcode"] + "'And flag=1  ORDER BY worder ";
                    Session["EXTitle1"] = "<b>加工单号</b>~^<b>加工单ID</b>~^<b>地点</b>~^<b>成本中心</b>~^<b>标志(1-汇报工时 > QAD工时 )</b>~^<b>QAD工时</b>~^<b>汇报工时</b>~^<b>差额</b>~^<b>结算日期</b>~^<b>工艺(A类时间)</b>~^<b>工艺(A+B)</b>~^<b>数量(当结算日期不为NULL,为完工入库数,反之需求数量)</b>~^<b>工艺</b>~^";
                    Session["EXHeader1"] = "";

                    ltlAlert.Text = "window.open('/public/exportExcel1.aspx', '_blank');";

                    return;
                }
            }


            int intError;

            intError = hr_salary.CalculateSalaryPT(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), txtYear.Text + "-" + dropMonth.SelectedValue.ToString() + "-01", Convert.ToInt32(Session["Uid"]), 0);

            if (intError < 0)
            {
                string strScr = @"<script language='javascript'> alert('结算出错，请重新结算');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error1", strScr);
                return;
            }

            intError = hr_salary.CalculateSalaryPT(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), txtYear.Text + "-" + dropMonth.SelectedValue.ToString() + "-01", Convert.ToInt32(Session["Uid"]), 1);

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
             , hr_salary.ExportSalary(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 1)
             , hr_salary.ExportSalary(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 0)
             , true);
    }
}
