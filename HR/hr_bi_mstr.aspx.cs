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
using System.Data.SqlClient;
using adamFuncs;
using Wage;

public partial class HR_hr_bi_mstr : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            btnSave.Enabled = this.Security["6010101"].isValid;

            BindData();
        }
    }

    public void BindData()
    {
        //定义参数
        int Year = Convert.ToInt32(txtYear.Text.Trim());
        int Month = Convert.ToInt32(ddlMonth.SelectedValue);
        try
        {
            HR_BaseInfo hr_bi = hr.SelectHRBaseInfo(Year, Month);

            txtAvgDays.Text = hr_bi.AvgDays.ToString().Trim();
            txtBasePrice.Text = hr_bi.BasicPrice.ToString().Trim();
            txtBasicWage.Text = hr_bi.BasicWage.ToString().Trim();
            txtDeductRate.Text = hr_bi.DeductRate.ToString().Trim();
            txtFixedDays.Text = hr_bi.FixedDays.ToString().Trim();
            txtHolidayRate.Text = hr_bi.HolidayRate.ToString().Trim();
            txtLaborRate.Text = hr_bi.LaborRate.ToString().Trim();
            txtMidNightSubsidy.Text = hr_bi.MidNightSubsidy.ToString().Trim();
            txtNightSubsidy.Text = hr_bi.NightSubsidy.ToString().Trim();
            txtOverTimeRate.Text = hr_bi.OverTimeRate.ToString().Trim();
            txtSaturdayRate.Text = hr_bi.SaturdayRate.ToString().Trim();
            txtTex.Text = hr_bi.Tax.ToString().Trim();
            txtWholeNightSubsidy.Text = hr_bi.WholeNightSubsidy.ToString().Trim();
            txtWorkDays.Text = hr_bi.WorkDays.ToString().Trim();
            txtWorkHours.Text = hr_bi.WorkHours.ToString().Trim();

            txtSickleaveRate.Text = hr_bi.SickleaveRate.ToString().Trim();
            txtSickLeaveDay.Text = hr_bi.SickleaveDay.ToString().Trim();

            txtOverPrice.Text = hr_bi.OverPrice.ToString().Trim();

            txtSocial.Text = hr_bi.Social.ToString().Trim();
            txtUnionfee.Text = hr_bi.UnionFee.ToString().Trim();
            txtOldAge.Text = hr_bi.Oldage.ToString().Trim();
            txtUnemploy.Text = hr_bi.Unemploy.ToString().Trim();
            txtInjury.Text = hr_bi.Injury.ToString().Trim();
            txtMaternity.Text = hr_bi.Maternity.ToString().Trim();
            txtHealth.Text = hr_bi.Health.ToString().Trim();
            txtHousingFund.Text = hr_bi.HousingFund.ToString().Trim();

            txtAOldAge.Text = hr_bi.A_Oldage.ToString().Trim();
            txtAHealth.Text = hr_bi.A_Health.ToString().Trim();
            txtAInjury.Text = hr_bi.A_Injury.ToString().Trim();

            txtMaxAttbonus.Text = hr_bi.MaxAttbonus.ToString().Trim();
            txtMinAttbonus.Text = hr_bi.MinAttbonus.ToString().Trim();
            txtMaxWYbonus.Text = hr_bi.MaxWYbonus.ToString().Trim();
            txtMinWYbonus.Text = hr_bi.MinWYbonus.ToString().Trim();
            txtWorkYearbonus.Text = hr_bi.WorkYearbonus.ToString().Trim();

            txtPercentAttbonus.Text = hr_bi.PercentAttbonus.ToString().Trim();
            txtPercentWYbonus.Text = hr_bi.PercentWYbonus.ToString().Trim();
            txtMinWorkDays.Text = hr_bi.MinWorkDays.ToString().Trim();
            chkMinus.Checked = hr_bi.isMinus;

            txtBasicWageNew.Text = hr_bi.BasicWageNew.ToString();
        }
        catch
        {

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach(Control ctrl in this.Form1.Controls)
        {
            if (typeof(TextBox) == ctrl.GetType())
            {
                TextBox txt = (TextBox)ctrl;

                if (txt.ValidationGroup.Length > 0)
                {
                    if (txt.Text.Trim().Length == 0)
                    {
                        txt.Text = "0";
                    }
                    else
                    {
                        try
                        {
                            Decimal _dc = Convert.ToDecimal(txt.Text.Trim());
                        }
                        catch
                        {
                            ltlAlert.Text = "alert('" + txt.ValidationGroup + " 只能是数字！');";
                            return;
                        }
                    }
                }
            }
        }

        Decimal _dWorkDays = Convert.ToDecimal(txtWorkDays.Text.Trim());
        if (_dWorkDays <= 0)
        {
            ltlAlert.Text = "alert('每月出勤天数 只能是大于0的数字！');";
            return;
        }

        Decimal _dAvgDays = Convert.ToDecimal(txtAvgDays.Text.Trim());
        if (_dAvgDays <= 0)
        {
            ltlAlert.Text = "alert('年平均出勤率 只能是大于0的数字！');";
            return;
        }

        //定义参数
        int Year = Convert.ToInt32(txtYear.Text.Trim());
        int Month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
        int uID = Convert.ToInt32(Session["uID"]);

        HR_BaseInfo hr_bi = new HR_BaseInfo();
        hr_bi.AvgDays  = Convert.ToDecimal(txtAvgDays.Text.Trim());
        hr_bi.BasicPrice = Convert.ToDecimal(txtBasicWage.Text.Trim()) / Convert.ToDecimal(txtAvgDays.Text.Trim());
        hr_bi.BasicWage = Convert.ToDecimal(txtBasicWage.Text.Trim());
        hr_bi.DeductRate = Convert.ToDecimal(txtDeductRate.Text.Trim());
        hr_bi.FixedDays = Convert.ToDecimal(txtFixedDays.Text.Trim());
        hr_bi.HolidayRate = Convert.ToDecimal(txtHolidayRate.Text.Trim());
        hr_bi.LaborRate = Convert.ToDecimal(txtLaborRate.Text.Trim());
        hr_bi.MidNightSubsidy = Convert.ToDecimal(txtMidNightSubsidy.Text.Trim());
        hr_bi.NightSubsidy = Convert.ToDecimal(txtNightSubsidy.Text.Trim());
        hr_bi.OverTimeRate = Convert.ToDecimal(txtOverTimeRate.Text.Trim());
        hr_bi.SaturdayRate = Convert.ToDecimal(txtSaturdayRate.Text.Trim());
        hr_bi.Tax = Convert.ToInt32(txtTex.Text.Trim());
        hr_bi.WholeNightSubsidy = Convert.ToDecimal(txtWholeNightSubsidy.Text.Trim());
        hr_bi.WorkDays = Convert.ToDecimal(txtWorkDays.Text.Trim());
        hr_bi.WorkHours = Convert.ToInt32(txtWorkHours.Text.Trim());

        hr_bi.SickleaveRate = Convert.ToDecimal(txtSickleaveRate.Text.Trim());
        hr_bi.SickleaveDay = Convert.ToDecimal(txtSickLeaveDay.Text.Trim());
        hr_bi.OverPrice = Convert.ToDecimal(txtOverPrice.Text.Trim());

        hr_bi.Social = Convert.ToDecimal(txtSocial.Text.Trim());
        hr_bi.Oldage = Convert.ToDecimal(txtOldAge.Text.Trim());
        hr_bi.Unemploy = Convert.ToDecimal(txtUnemploy.Text.Trim());
        hr_bi.Injury = Convert.ToDecimal(txtInjury.Text.Trim());
        hr_bi.Maternity = Convert.ToDecimal(txtMaternity.Text.Trim());
        hr_bi.Health = Convert.ToDecimal(txtHealth.Text.Trim());
        hr_bi.HousingFund = Convert.ToDecimal(txtHousingFund.Text.Trim());
        hr_bi.UnionFee = Convert.ToDecimal(txtUnionfee.Text.Trim());

        hr_bi.A_Oldage = Convert.ToDecimal(txtAOldAge.Text.Trim());
        hr_bi.A_Health = Convert.ToDecimal(txtAHealth.Text.Trim());
        hr_bi.A_Injury = Convert.ToDecimal(txtAInjury.Text.Trim());

        hr_bi.MaxAttbonus = Convert.ToDecimal(txtMaxAttbonus.Text.Trim());
        hr_bi.MinAttbonus = Convert.ToDecimal(txtMinAttbonus.Text.Trim());
        hr_bi.MaxWYbonus = Convert.ToDecimal(txtMaxWYbonus.Text.Trim());
        hr_bi.MinWYbonus = Convert.ToDecimal(txtMinWYbonus.Text.Trim());
        hr_bi.WorkYearbonus = Convert.ToDecimal(txtWorkYearbonus.Text.Trim());

        hr_bi.PercentAttbonus = Convert.ToDecimal(txtPercentAttbonus.Text.Trim());
        hr_bi.PercentWYbonus = Convert.ToDecimal(txtWorkYearbonus.Text.Trim());
        hr_bi.MinWorkDays = Convert.ToDecimal(txtMinWorkDays.Text.Trim());

        hr_bi.isMinus = chkMinus.Checked;
        hr_bi.BasicWageNew = Convert.ToDecimal(txtBasicWageNew.Text);

        if (hr.UpdateHRBaseInfo(Year, Month, uID, hr_bi))
        {
            ltlAlert.Text = "alert('工资基础信息保存成功！');";
        }
        else
        {
            ltlAlert.Text = "alert('工资基础信息保存失败！');";
        }
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
