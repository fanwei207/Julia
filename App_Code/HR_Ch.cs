using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

namespace Wage
{
    /// <summary>
    /// Summary description for HR_Ch
    /// </summary>
    public class HR_Ch : HR
    {
        public HR_Ch()
        {
            adam = new adamClass();
        }

        protected override SqlDataReader GetTimeCalculateInfoPT(int intYear, int intMonth, int intPlantcode)
        {
            string strSql = "sp_hr_GetTimeCalculateInfoPT_ch";
            SqlParameter[] parmArray = new SqlParameter[4];
            parmArray[0] = new SqlParameter("@Year", intYear);
            parmArray[1] = new SqlParameter("@Month", intMonth);
            string CalDate = intMonth.ToString();
            if (CalDate.Trim().Length == 1)
                CalDate = "0" + CalDate;
            parmArray[2] = new SqlParameter("@CalDate", intYear.ToString() + CalDate);
            parmArray[3] = new SqlParameter("@Plantcode", intPlantcode);

            SqlDataReader sdrReader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            return sdrReader;
        
        }

        protected override void AdjustTimeSalaryPT(hr_TimeSalary hts, ref decimal decWork)
        {
            decimal actWork = 0;
            string strSql = "sp_hr_GetFinSalary";
            SqlParameter[] parmArray = new SqlParameter[2];
            parmArray[0] = new SqlParameter("@userId", hts.T_userID);
            parmArray[1] = new SqlParameter("@salaryDate", hts.T_salaryDate);
            SqlDataReader sdrReader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            if (sdrReader.Read())
            {
                actWork = Convert.ToDecimal(sdrReader["duereward"]);
            }
            sdrReader.Close();
            if (actWork >= decWork || decWork - actWork <= hts.T_benefit)
            {
                hts.T_benefit += actWork - decWork;
            }
            else
            {
                hts.T_benefit = 0;
                hts.T_basic -= decWork - actWork - hts.T_benefit;
            }
            decWork = actWork;
        }

        public override HR_BaseInfo SelectHRBaseInfo(int Year, int Month)
        {
            HR_BaseInfo hr_bi= base.SelectHRBaseInfo(Year, Month);
            hr_bi.OverTimeRate = 2;
            hr_bi.SaturdayRate = 2;
            return hr_bi;
        }

        public override void SaveTimeSalary(hr_TimeSalary hrt, int intUserID)
        {
            string strSql = "sp_hr_SaveTimeSalary_ch";
            SqlParameter[] parmArray = new SqlParameter[77];
            parmArray[0] = new SqlParameter("@hr_Time_SalaryUserID", hrt.T_userID);
            parmArray[1] = new SqlParameter("@hr_Time_SalaryUserNo", hrt.T_userNo);
            parmArray[2] = new SqlParameter("@hr_Time_SalaryUserName", hrt.T_userName);
            parmArray[3] = new SqlParameter("@hr_Time_SalaryDate", hrt.T_salaryDate);
            parmArray[4] = new SqlParameter("@hr_Time_SalaryBasic", hrt.T_basic);
            parmArray[5] = new SqlParameter("@hr_Time_SalaryBenefit", hrt.T_benefit);
            parmArray[6] = new SqlParameter("@hr_Time_SalaryNightWork", hrt.T_nightWork);
            parmArray[7] = new SqlParameter("@hr_Time_SalaryAllowance", hrt.T_allowance);
            parmArray[8] = new SqlParameter("@hr_Time_SalaryAssess", hrt.T_assess);
            parmArray[9] = new SqlParameter("@hr_Time_SalaryHoliday", hrt.T_holiday);
            parmArray[10] = new SqlParameter("@hr_Time_SalaryDuereward", hrt.T_duereward);
            parmArray[11] = new SqlParameter("@hr_Time_SalaryHfound", hrt.T_hfound);
            parmArray[12] = new SqlParameter("@hr_Time_SalaryRfound", hrt.T_rfound);
            parmArray[13] = new SqlParameter("@hr_Time_SalaryMember", hrt.T_member);
            parmArray[14] = new SqlParameter("@hr_Time_SalaryDeduct", hrt.T_deduct);
            parmArray[15] = new SqlParameter("@hr_Time_SalaryTax", hrt.T_tax);
            parmArray[16] = new SqlParameter("@hr_Time_SalaryWorkpay", hrt.T_workpay);
            parmArray[17] = new SqlParameter("@hr_Time_Fire", hrt.T_fire ? 1 : 0);
            parmArray[18] = new SqlParameter("@hr_Time_SalaryCDeduct", hrt.T_currdeduct);
            parmArray[19] = new SqlParameter("@hr_Time_SalaryLDeduct", hrt.T_lastdeduct);
            parmArray[20] = new SqlParameter("@hr_Time_SalaryAnnual", hrt.T_annual);
            parmArray[21] = new SqlParameter("@hr_Time_SalaryHdays", hrt.T_hday);
            parmArray[22] = new SqlParameter("@hr_Time_SalarySleave", hrt.T_sleave);
            parmArray[23] = new SqlParameter("@hr_Time_SalarySLeavePay", hrt.T_sleavepay);
            parmArray[24] = new SqlParameter("@hr_Time_SalaryDepartment", hrt.T_department);
            parmArray[25] = new SqlParameter("@hr_Time_SalaryEmployTypeID", hrt.T_employTypeID);
            parmArray[26] = new SqlParameter("@hr_Time_SalaryInsuranceTypeID", hrt.T_insuranceTypeID);
            parmArray[27] = new SqlParameter("@hr_Time_SalaryWorktype", hrt.T_worktype);
            parmArray[28] = new SqlParameter("@hr_Time_SalaryCreateBy", intUserID);
            parmArray[29] = new SqlParameter("@hr_Time_SalaryAttendance", hrt.T_attendance);
            parmArray[30] = new SqlParameter("@hr_Time_SalaryAttDay", hrt.T_attday);
            parmArray[31] = new SqlParameter("@hr_Time_SalaryMid", hrt.T_mid);
            parmArray[32] = new SqlParameter("@hr_Time_SalaryNight", hrt.T_night);
            parmArray[33] = new SqlParameter("@hr_Time_SalaryWhole", hrt.T_whole);
            parmArray[34] = new SqlParameter("@hr_Time_SalaryLeave", hrt.T_leave);
            parmArray[35] = new SqlParameter("@hr_Time_SalaryOtherDay", hrt.T_other);
            parmArray[36] = new SqlParameter("@hr_Time_Salaryfixedsalary", hrt.T_fixedsalary);
            parmArray[37] = new SqlParameter("@hr_Time_SalaryRate", hrt.T_Rate);
            parmArray[38] = new SqlParameter("@hr_Time_SalaryWorkYear", hrt.T_workyear);
            parmArray[39] = new SqlParameter("@hr_Time_SalaryMaternityDays", hrt.T_maternityDays);
            parmArray[40] = new SqlParameter("@hr_Time_SalaryMaternityPay", hrt.T_maternityPay);
            parmArray[41] = new SqlParameter("@hr_Time_WorkShopID", hrt.T_workShopID);
            parmArray[42] = new SqlParameter("@hr_Time_userType", hrt.T_userType);
            parmArray[43] = new SqlParameter("@hr_Time_Fullattendence", hrt.T_fullattendence);
            parmArray[44] = new SqlParameter("@hr_Time_LengService", hrt.T_lengService);

            parmArray[45] = new SqlParameter("@hr_Time_accrFound", hrt.T_accrFound);
            parmArray[46] = new SqlParameter("@hr_Time_acceFound", hrt.T_acceFound);
            parmArray[47] = new SqlParameter("@hr_Time_accmFound", hrt.T_accmFound);
            parmArray[48] = new SqlParameter("@hr_Time_taxRate", hrt.T_taxRate);
            parmArray[49] = new SqlParameter("@hr_Time_taxDeduct", hrt.T_taxDeduct);

            parmArray[50] = new SqlParameter("@hr_Time_humanAllowance", hrt.t_HumanAllowance);
            parmArray[51] = new SqlParameter("@hr_Time_hightTemp", hrt.t_HightTemp);
            parmArray[52] = new SqlParameter("@hr_Time_normal", hrt.t_Normal);
            parmArray[53] = new SqlParameter("@hr_Time_allother", hrt.t_Allother);

            parmArray[54] = new SqlParameter("@hr_Time_materialDeduct", hrt.T_materialDeduct);
            parmArray[55] = new SqlParameter("@hr_Time_assessDeduct", hrt.T_assessDeduct);
            parmArray[56] = new SqlParameter("@hr_Time_vacationDeduct", hrt.T_vacationDeduct);
            parmArray[57] = new SqlParameter("@hr_Time_otherDeduct", hrt.T_otherDeduct);
            parmArray[58] = new SqlParameter("@hr_Time_ship", hrt.t_Ship);
            parmArray[59] = new SqlParameter("@hr_Time_locFin", hrt.t_LocFin);
            parmArray[60] = new SqlParameter("@hr_Time_quanlity", hrt.t_Quanlity);
            parmArray[61] = new SqlParameter("@hr_Time_break", hrt.t_Break);
            parmArray[62] = new SqlParameter("@hr_Time_newuser", hrt.T_newuser);
            parmArray[63] = new SqlParameter("@hr_Time_student", hrt.T_student);
            parmArray[64] = new SqlParameter("@hr_Time_bonus", hrt.T_bonus);
            parmArray[65] = new SqlParameter("@hr_Time_rtMoeny", hrt.T_restTimeMoney);

            parmArray[66] = new SqlParameter("@hr_Time_MutualFunds", hrt.T_MutualFunds);

            parmArray[67] = new SqlParameter("@hr_Time_AllMobile", hrt.t_AllMobile);
            parmArray[68] = new SqlParameter("@hr_Time_AllKilometer", hrt.t_AllKilometer);
            parmArray[69] = new SqlParameter("@hr_Time_AllBox", hrt.t_AllBox);
            parmArray[70] = new SqlParameter("@hr_Time_AllBusiness", hrt.t_AllBusiness);
            parmArray[71] = new SqlParameter("@hr_Time_AllOnDuty", hrt.t_AllOnDuty);
            parmArray[72] = new SqlParameter("@hr_Time_AllPost", hrt.t_AllPost);

            parmArray[73] = new SqlParameter("@hr_Time_FeeElectricity", hrt.F_Electricity);
            parmArray[74] = new SqlParameter("@hr_Time_FeeWater", hrt.F_Water);
            parmArray[75] = new SqlParameter("@hr_Time_FeeDormitory", hrt.F_Dormitory);

            parmArray[76] = new SqlParameter("@hr_Time_Subsidies", hrt.T_subsidies);

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
        }

        public override void DeleteSalaryDataTime(int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_DeleteSalaryTime_ch";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Type", intType);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {

            }

        }

        public DataTable CheckAttendance(string strStart, string strEnd, string strUserNo, string strUserName, int intDepart)
        {
            try
            {
                string strSql = "sp_hr_CheckAttendance_jiancha";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@starttime", strStart);
                parmArray[1] = new SqlParameter("@endtime", strEnd);
                parmArray[2] = new SqlParameter("@UserNo", strUserNo);
                parmArray[3] = new SqlParameter("@UserName", strUserName);
                parmArray[4] = new SqlParameter("@DepartmentID", intDepart);
                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable CheckSalary(string strYear, string strMonth, string strUserNo, string strUserName, int intDepart)
        {
            try
            {
                string strSql = "sp_hr_CheckSalary_jiancha";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", strYear);
                parmArray[1] = new SqlParameter("@Month", strMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUserNo);
                parmArray[3] = new SqlParameter("@UserName", strUserName);
                parmArray[4] = new SqlParameter("@DepartmentID", intDepart);
                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
    }
}