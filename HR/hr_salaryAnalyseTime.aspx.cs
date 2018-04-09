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

public partial class HR_hr_salaryAnalyseTime : BasePage
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

    protected void btnTimeSalary_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('/salary/salaryanalysetimeexcel1.aspx?sate=" + dropMonth.SelectedValue + "&year=" + txtYear.Text.Trim() + "', '_blank');";
     }

    protected void btnDecompose_Click(object sender, EventArgs e)
    {
        //" 0-���,1-ÿҳ�ļ�¼����,2-��ͷ,3-���,4-�Ƿ���Ҫ��ҳС��,5-�Ƿ�Ҫ��������6-�Ƿ���Ҫ�м��,7-�Ƿ���Ҫ���ҳ��,8-��ͷ����"

        //string strHeader = txtYear.Text.Trim() + "��" + dropMonth.SelectedValue + "�¹��ʷ��ŵ�";
        string strHeader;
        strHeader = txtYear.Text.Trim() + "��" + dropMonth.SelectedValue + "�¹���ƾ֤";
        if (dropWork.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('����ѡ���ù�����');Form1.dropWork.focus();";
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

        Session["EXTitlePrint"] = hr_salary.PrintString(30, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["PlantCode"])).Replace("����", "���");

        Session["EXFooterPrint"] = hr_salary.PrintString(31, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, 0, 0, "", 0, Convert.ToInt32(Session["PlantCode"]));
        //Session["EXSQLPrint"] = hr_salary.PrintString(2, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropWorkshop.SelectedValue)
        // , Convert.ToInt32(dropWorkgroup.SelectedValue), Convert.ToInt32(dropInsurance.SelectedValue), Convert.ToInt32(dropWork.SelectedValue), txtUserNo.Text.Trim(), 0);
        string strSQL = hr_salary.PrintString(33, Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, 0, 0, Convert.ToInt32(dropInsurance.SelectedValue), Convert.ToInt32(dropWork.SelectedValue), "", 0, Convert.ToInt32(Session["PlantCode"]));
        //strSQL = strSQL.Replace("AND hr_Time_SalaryUserID NOT IN (SELECT usercode FROM EmployeeGu WHERE YEAR(currentdate) =" + txtYear.Text.Trim() + "  AND Month(currentdate)=" + dropMonth.SelectedValue + ")", "");
        //string str =" AND hr_Time_SalaryUserID NOT IN (SELECT usercode FROM tcpc" + Convert.ToString(Session["Plantcode"])+".dbo.EmployeeGu WHERE YEAR(currentdate) ="+ txtYear.Text.Trim() +"  AND Month(currentdate)="+ dropMonth.SelectedValue+")";
        strSQL = strSQL.Replace(" AND hr_Time_SalaryUserID NOT IN (SELECT usercode FROM tcpc" + Convert.ToString(Session["Plantcode"])+".dbo.EmployeeGu WHERE YEAR(currentdate) ="+ txtYear.Text.Trim() +" AND Month(currentdate)="+ dropMonth.SelectedValue+")", "");
    
        Session["EXSQLPrint"] = strSQL;
        ltlAlert.Text = "window.open('/hr/hr_Salary_Piece.aspx?ty=3','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');";
        //Response.Write("<script   language=javascript>window.open('/hr/hr_Salary_Piece.aspx?ty=3','','width=600,height=600,left=0,top=0,scrollbars=1,status=1,resizable=1,menubar=yes');</script>");

    }
 
    protected void btnPersonal_Click(object sender, EventArgs e)
    {
        if (txtStartYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('��ʼ���� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtStartYear.Text.Trim());
                if (_n < 1900)
                {
                    ltlAlert.Text = "alert('��ʼ���� ����С��1900��');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���� ֻ����������');";
                return;
            }
        }

        if (txtStartMonth.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('��ʼ���� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtStartMonth.Text.Trim());
                if (_n < 1 || _n > 12)
                {
                    ltlAlert.Text = "alert('��ʼ���� ֻ����1��12֮���������');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���� ֻ����������');";
                return;
            }
        }

        if (txtEndYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('�������� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtEndYear.Text.Trim());
                if (_n < 1900)
                {
                    ltlAlert.Text = "alert('�������� ����С��1900��');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('�������� ֻ����������');";
                return;
            }
        }

        if (txtEndMonth.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('�������� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtEndMonth.Text.Trim());
                if (_n < 1 || _n > 12)
                {
                    ltlAlert.Text = "alert('�������� ֻ����1��12֮���������');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('�������� ֻ����������');";
                return;
            }
        }

        ltlAlert.Text = "window.open('/salary/salaryPesonalReportTime.aspx?sy=" + txtStartYear.Text.Trim() + "&sm=" + txtStartMonth.Text.Trim() + "&ey=" + txtEndYear.Text.Trim() + "&em=" + txtEndMonth.Text.Trim() + "&cod=" + txtUserNo.Text.Trim() + "', '_blank');";
     }
}



