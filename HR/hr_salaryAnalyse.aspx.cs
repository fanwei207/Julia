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

public partial class HR_hr_salaryAnalyse : BasePage
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            dropInsuranceDatabind();
            dropWorkDatabind();
        }
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

    protected void btnPieceSalary_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('/salary/salaryanalyseexcel.aspx?sate=" + dropMonth.SelectedValue + "&year=" + txtYear.Text.Trim() + "', '_blank');";
    }

    protected void btnDecompose_Click(object sender, EventArgs e)
    {
        //" 0-类别,1-每页的记录条数,2-标头,3-宽度,4-是否需要本页小计,5-是否要总人数，6-是否需要行间隔,7-是否需要输出页码,8-标头内容"
        string strHeader;
        strHeader = txtYear.Text.Trim() + "年" + dropMonth.SelectedValue + "月工资凭证";
        if (dropWork.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择用工性质');Form1.dropWork.focus();";
            return;
        }
        else
        {
            if (dropInsurance.SelectedIndex > 0)
                strHeader = strHeader + "(" + dropWork.SelectedItem.Text + "/" + dropInsurance.SelectedItem.Text + ")";
            else
                strHeader = strHeader + "(" + dropWork.SelectedItem.Text + ")";
        }



        Session["EXHeaderPrint"] = "1,38,1,1036,1,1,0,1," + strHeader;

        Session["EXTitlePrint"] = hr_salary.PrintString(0, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["plantCode"])).Replace("工号", "序号");

        Session["EXFooterPrint"] = hr_salary.PrintString(1, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["plantCode"]));
        Session["EXSQLPrint"] = hr_salary.PrintString(3, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, Convert.ToInt32(dropInsurance.SelectedValue), Convert.ToInt32(dropWork.SelectedValue), "", 0, Convert.ToInt32(Session["plantCode"]));
        ltlAlert.Text = "window.open('/hr/hr_Salary_Piece.aspx?ty=3','_blank','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');";
    }


    protected void btnPersonal_Click(object sender, EventArgs e)
    {
        if (txtStartYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('起始年月 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtStartYear.Text.Trim());
                if (_n < 1900)
                {
                    ltlAlert.Text = "alert('起始年月 不能小于1900！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('起始年月 只能是整数！');";
                return;
            }
        }

        if (txtStartMonth.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('起始年月 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtStartMonth.Text.Trim());
                if (_n < 1 || _n > 12)
                {
                    ltlAlert.Text = "alert('起始年月 只能是1至12之间的整数！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('起始年月 只能是整数！');";
                return;
            }
        }

        if (txtEndYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('结束年月 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtEndYear.Text.Trim());
                if (_n < 1900)
                {
                    ltlAlert.Text = "alert('结束年月 不能小于1900！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('结束年月 只能是整数！');";
                return;
            }
        }

        if (txtEndMonth.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('结束年月 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtEndMonth.Text.Trim());
                if (_n < 1 || _n > 12)
                {
                    ltlAlert.Text = "alert('结束年月 只能是1至12之间的整数！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('结束年月 只能是整数！');";
                return;
            }
        }

        ltlAlert.Text = "window.open('/salary/salaryPesonalReport.aspx?sy=" + txtStartYear.Text.Trim() + "&sm=" + txtStartMonth.Text.Trim() + "&ey=" + txtEndYear.Text.Trim() + "&em=" + txtEndMonth.Text.Trim() + "&cod=" + txtUserNo.Text.Trim() + "', '_blank');";
    }
}
