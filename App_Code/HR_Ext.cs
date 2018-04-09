using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

namespace Wage
{
    /// <summary>
    /// Summary description for HR_Ext
    /// </summary>
    public partial class HR
    {
        /// <summary>
        /// 按单价结算。UP=Unit Price，指按单价计算的方式
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="strUser"></param>
        /// <param name="strName"></param>
        /// <param name="intDept"></param>
        /// <param name="PlantCode"></param>
        /// <returns></returns>
        public DataTable SelectSalaryUP(int intYear, int intMonth, string strUser, string strName, int intDept, int PlantCode)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_SelectSalaryUP";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@Depart", intDept);

                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;

            }
        }
        /// <summary>
        /// 在结算之前，清空表hr_Salary_up
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        public void DeleteSalaryDataUP(int intYear, int intMonth)
        {
            try
            {
                string strSql = "sp_hr_DeleteSalaryUP";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 计算A类人员工资时用
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlant"></param>
        /// <param name="intType"></param>
        /// <param name="decDay"></param>
        /// <param name="intKind"></param>
        /// <returns></returns>
        public Hashtable GetHashDataUP(int intYear, int intMonth, int intPlant, int intType, decimal decDay, int intKind)
        {
            try
            {
                Hashtable htbtemp = new Hashtable();
                htbtemp.Clear();

                string strSql;
                if (intKind == 0)
                    strSql = "sp_hr_PieceAvgSalaryUP";
                else
                    strSql = "sp_hr_PieceAvgSalary_BKUP";

                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlant);
                parmArray[3] = new SqlParameter("@Type", intType);
                parmArray[4] = new SqlParameter("@WorkDay", decDay);
                SqlDataReader sqlReader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                while (sqlReader.Read())
                {
                    htbtemp.Add(Convert.ToInt32(sqlReader[2]), Convert.ToDecimal(sqlReader[1]) == 0 ? 0 : Math.Round(Convert.ToDecimal(sqlReader[0]) / Convert.ToDecimal(sqlReader[1]), 2));
                }
                sqlReader.Close();

                return htbtemp;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 按照单价（Unit Price）方式结算
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlantCode"></param>
        /// <param name="strDate"></param>
        /// <param name="intUserID"></param>
        /// <param name="intType">0=A类结算 1=B类结算</param>
        /// <returns></returns>
        public int CalculateSalaryPTUP(int intYear, int intMonth, int intPlantCode, string strDate, Int32 intUserID, int intType)
        {
            try
            {
                string CalDate;
                HR_BaseInfo hrbi = SelectHRBaseInfo(intYear, intMonth);

                string strSql;
                if (intType == 0)
                    strSql = "sp_hr_GetCalculateInfoBKUP";
                else
                    strSql = "sp_hr_GetCalculateInfoBKtoBUP";

                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);

                CalDate = intMonth.ToString();
                if (CalDate.Trim().Length == 1)
                    CalDate = "0" + CalDate;

                parmArray[2] = new SqlParameter("@CalDate", intYear.ToString() + CalDate);
                parmArray[3] = new SqlParameter("@PlanCode", intPlantCode);
                parmArray[4] = new SqlParameter("@UserID", intUserID);

                Hashtable htbDepart = new Hashtable();
                Hashtable htbLine = new Hashtable();
                if (intType == 1)
                {
                    htbDepart = GetHashDataUP(intYear, intMonth, intPlantCode, 0, hrbi.WorkDays, 1);
                    htbLine = GetHashDataUP(intYear, intMonth, intPlantCode, 1, hrbi.WorkDays, 1);
                }

                SqlDataReader sdtr = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
                while (sdtr.Read())
                {
                    HR_Salary hrs = new HR_Salary();

                    decimal decSalary, decOver, decBasic, decDeduct, decSday, descOldSday, decBenefit, decCoef, decRateSalary;
                    decBasic = 0;
                    decSalary = 0;
                    decOver = 0; //加班费 = 汇报工资 - 基本工资
                    decDeduct = 0;
                    decSday = 0;
                    descOldSday = 0;
                    decBenefit = 0;
                    decCoef = 0;
                    decRateSalary = 0;

                    decimal decRateDeduct = 0;  //考评扣补
                    decimal decRate;
                    
                    decimal decSDays = 0; // 婚丧假天数

                    bool Special = true;  // 是否已贴到基本工资

                    //--考勤-----------//

                    //--End ----------//

                    //--　员工信息　---------//
                    hrs.s_UserID = Convert.ToInt32(sdtr["userID"]);
                    hrs.s_UserNo = sdtr["userNo"].ToString();
                    hrs.s_UserName = sdtr["userName"].ToString();
                    hrs.s_SalaryDate = intYear.ToString() + "-" + intMonth.ToString() + "-01";
                    hrs.s_Department = Convert.ToInt32(sdtr["departmentID"]);
                    hrs.s_EmployTypeID = Convert.ToInt32(sdtr["employTypeID"]);
                    hrs.s_InsuranceTypeID = Convert.ToInt32(sdtr["insuranceTypeID"]);
                    hrs.s_Temp = Convert.ToBoolean(sdtr["isTemp"]);
                    hrs.s_WorkshopID = Convert.ToInt32(sdtr["workshopID"]);
                    hrs.s_WorkgroupID = Convert.ToInt32(sdtr["workprocedureID"]);
                    hrs.s_WorkkindID = Convert.ToInt32(sdtr["kindswork"]);
                    hrs.s_UserType = Convert.ToInt32(sdtr["userType"]);


                    //----------
                    hrs.s_HumanAllowance = Convert.ToDecimal(sdtr["humanAllowance"]);
                    hrs.s_HightTemp = Convert.ToDecimal(sdtr["hightTemp"]);
                    hrs.s_NOrmal = Convert.ToDecimal(sdtr["amount"]);
                    hrs.s_Other = Convert.ToDecimal(sdtr["sig"]);
                    hrs.s_Newuser = Convert.ToDecimal(sdtr["newuser"]);
                    hrs.s_Student = Convert.ToDecimal(sdtr["student"]);
                    hrs.s_Bonus = Convert.ToDecimal(sdtr["bonus"]);

                    hrs.s_MaterialDeduct = Convert.ToDecimal(sdtr["materialDeduct"]);
                    hrs.s_AssessDeduct = Convert.ToDecimal(sdtr["assessDeduct"]);
                    hrs.s_VacationDeduct = Convert.ToDecimal(sdtr["vacationDeduct"]);
                    hrs.s_OtherDeduct = Convert.ToDecimal(sdtr["Otherdeduct"]);
                    hrs.s_Ship = Convert.ToDecimal(sdtr["ship"]);
                    hrs.s_LocFin = Convert.ToDecimal(sdtr["locFin"]);
                    hrs.s_Quanlity = Convert.ToDecimal(sdtr["quanlity"]);
                    hrs.s_Break = Convert.ToDecimal(sdtr["abreak"]);
                    //--------


                    if (sdtr["leavedate"].ToString() == "")
                        hrs.s_Fire = false;
                    else
                    {
                        if (Convert.ToDateTime(sdtr["leavedate"]) < Convert.ToDateTime(hrs.s_SalaryDate).AddMonths(1))
                            hrs.s_Fire = true;
                        else
                            hrs.s_Fire = false;
                    }

                    bool blNewUser = false;  // 判断是否为新进员工
                    if (Convert.ToDateTime(sdtr["enterdate"]).Year == intYear && Convert.ToDateTime(sdtr["enterdate"]).Month == intMonth)
                        blNewUser = true;

                    hrs.s_Workyear = Convert.ToInt32(sdtr["workyear"]);
                    //--- End -------------//

                    //--社保　/ 中夜班费  / 津贴--------------------------//
                    hrs.s_Hfound = Convert.ToDecimal(sdtr["hFound"]);
                    hrs.s_AccrFound = Convert.ToDecimal(sdtr["rFound"]);
                    hrs.s_AcceFound = Convert.ToDecimal(sdtr["eFound"]);
                    hrs.s_AccmFound = Convert.ToDecimal(sdtr["mFound"]);
                    hrs.s_Rfound = hrs.s_AccrFound + hrs.s_AcceFound + hrs.s_AccmFound;

                    hrs.s_NightMoney = Math.Round(hrbi.MidNightSubsidy * Convert.ToInt32(sdtr["mid"]) + hrbi.NightSubsidy * Convert.ToInt32(sdtr["night"]) + hrbi.WholeNightSubsidy * Convert.ToInt32(sdtr["whole"]), 1);
                    hrs.s_Mid = Convert.ToInt32(sdtr["mid"]);
                    hrs.s_Night = Convert.ToInt32(sdtr["night"]);
                    hrs.s_Whole = Convert.ToInt32(sdtr["whole"]);

                    hrs.s_Oallowance = Convert.ToDecimal(sdtr["amount"]) + Convert.ToDecimal(sdtr["sig"]) + Convert.ToDecimal(sdtr["hightTemp"]) + Convert.ToDecimal(sdtr["humanAllowance"]) + Convert.ToDecimal(sdtr["newuser"]) + Convert.ToDecimal(sdtr["student"]) + Convert.ToDecimal(sdtr["bonus"]);
                    hrs.s_Childwance = Convert.ToDecimal(sdtr["sig"]);
                    //---End --------------------------//

                    //--扣款　／　上月剩余扣款------//

                    hrs.s_Lastduct = Convert.ToDecimal(sdtr["remaindeduct"]);
                    decDeduct = hrs.s_Lastduct + Convert.ToDecimal(sdtr["deduct"]);
                    //考核评分。rate取自tcpcx..perf_employee.perf_rate
                    decRate = Convert.ToDecimal(sdtr["rate"]);
                    if (decRate == 0)
                        //mark始终等于0。drate取自tcpc0.dbo.perf_rate.perf_rate
                        decRateDeduct = 0 - Convert.ToDecimal(sdtr["mark"]) * Convert.ToDecimal(sdtr["drate"]);
                    else
                    {
                        if (decRate > 0 && decRate < 100)
                            decRateDeduct = 0 - Convert.ToDecimal(sdtr["mark"]) * decRate;
                        else
                            decRateDeduct = (decRate - 100 - Convert.ToDecimal(sdtr["mark"])) * 0.003M;
                    }

                    if (decRate > 100)
                        decRateDeduct = hrbi.BasicWage * decRateDeduct;


                    if (decRateDeduct < 0)
                    {
                        decDeduct = decDeduct - decRateDeduct;
                        hrs.s_DecRateDeduct = 0 - decRateDeduct;
                    }
                    else
                    {
                        hrs.s_Oallowance = hrs.s_Oallowance + decRateDeduct;
                        hrs.s_DecRateDeduct = 0;
                    }

                    // hrs.s_DecRateDeduct = decRateDeduct;
                    //--End -----------------------//

                    //--病事假，年假-- 婚假／丧假/工伤，产假-------------//
                    //该员工没有请病假、事假，也没旷工
                    if (Convert.ToDecimal(sdtr["sickDays"]) == 0 && Convert.ToDecimal(sdtr["bussinessDays"]) == 0 && Convert.ToDecimal(sdtr["minerDays"]) == 0)
                        hrs.s_Leave = true;
                    else
                        hrs.s_Leave = false;

                    hrs.s_Leaveday = Convert.ToDecimal(sdtr["bussinessDays"]);//事假
                    hrs.s_MinerDays = Convert.ToDecimal(sdtr["minerDays"]);//旷工
                    hrs.s_Restleave = Convert.ToDecimal(sdtr["number"]);//年休
                    hrs.s_SickLeave = Convert.ToDecimal(sdtr["sickDays"]);//病假

                    hrs.s_SickLeavePay = Math.Round(hrbi.BasicWage * hrbi.SickleaveRate * hrs.s_SickLeave / 100 / hrbi.SickleaveDay, 1);

                    //折算一下婚丧假
                    decSDays = Math.Round((Convert.ToDecimal(sdtr["merrageDays"]) + Convert.ToDecimal(sdtr["funeralDays"]) + Convert.ToDecimal(sdtr["injuryDays"])) / hrbi.SickleaveDay * hrbi.AvgDays, 2);
                    descOldSday = Convert.ToDecimal(sdtr["merrageDays"]) + Convert.ToDecimal(sdtr["funeralDays"]) + Convert.ToDecimal(sdtr["injuryDays"]);
                    hrs.s_Otherday = descOldSday;

                    hrs.s_MaternityDays = Convert.ToDecimal(sdtr["maternityDays"]);
                    hrs.s_MaternityPay = Math.Round(hrs.s_MaternityDays * hrbi.BasicWage / hrbi.SickleaveDay, 1);

                    //--国假--------------------//
                    //如果国假有汇报的话
                    if (sdtr["holidaycost"].ToString().Length > 0 && Convert.ToDecimal(sdtr["holidaycost"]) > 0)
                    {
                        hrs.s_Shday = Math.Round(Convert.ToDecimal(sdtr["holidaycost"]) / 8, 2);
                        hrs.s_Shsalary = Math.Round(hrs.s_Shday * hrbi.BasicPrice * hrbi.HolidayRate, 1);
                        hrs.s_Holiday = Math.Round(hrs.s_Shday * hrbi.BasicPrice * hrbi.HolidayRate, 1);
                    }
                    else
                    {
                        hrs.s_Shday = 0;
                        hrs.s_Shsalary = 0;
                        hrs.s_Holiday = 0;
                    }

                    //--End --------------------//

                    //--考勤-----------------------//
                    //读取时值就是0，所以此段不会执行
                    if (Convert.ToDecimal(sdtr["bc_coef"]) > 0)
                    {
                        decCoef = Convert.ToDecimal(sdtr["bc_coef"]);
                        hrs.s_Attdays = Convert.ToDecimal(sdtr["wdd"]);
                        hrs.s_AttHours = Convert.ToDecimal(sdtr["tdd"]);
                    }
                    else
                    {
                        hrs.s_Attdays = Convert.ToDecimal(sdtr["gday"]);
                        hrs.s_AttHours = Convert.ToDecimal(sdtr["attendence"]);
                    }
                    //-----------------------//

                    //存在计件计时转换
                    if (Convert.ToInt32(sdtr["attUserID"]) > 0)
                    {
                        hrs.s_ExChange = true;
                        decSDays = 0;
                        hrs.s_Restleave = 0;
                    }
                    else
                        hrs.s_ExChange = false;

                    //是否为当月新进员工。存在表hr_Probationer中
                    if (Convert.ToDecimal(sdtr["pwork"]) > 0)
                        decSday = Convert.ToDecimal(sdtr["pwork"]);
                    else
                        decSday = hrbi.WorkDays;

                    //计算A类人员的工资
                    if (intType == 1)
                    { // A类 但取平均工资
                        decimal decfin;
                        if (Convert.ToBoolean(sdtr["dflag"])) // 是否取部门平均工资 
                        {
                            decRateSalary = Convert.ToDecimal(htbDepart[hrs.s_Department]);
                            decfin = Math.Round(Convert.ToDecimal(htbDepart[hrs.s_Department]) * (hrs.s_Attdays + hrs.s_Restleave) * decCoef, 2);
                        }
                        else
                        {
                            if (hrs.s_WorkshopID > 0)
                            {
                                decRateSalary = Convert.ToDecimal(htbLine[hrs.s_WorkshopID]);
                                decfin = Math.Round(Convert.ToDecimal(htbLine[hrs.s_WorkshopID]) * (hrs.s_Attdays + hrs.s_Restleave) * decCoef, 2);
                            }
                            else
                            {
                                decRateSalary = Convert.ToDecimal(htbDepart[hrs.s_Department]);
                                decfin = Math.Round(Convert.ToDecimal(htbDepart[hrs.s_Department]) * (hrs.s_Attdays + hrs.s_Restleave) * decCoef, 2);
                            }
                        }

                        if (hrs.s_AttHours + hrs.s_Restleave + decSDays >= decSday)
                        {
                            decBasic = hrbi.BasicWage;
                            decOver = Math.Round((hrs.s_AttHours + hrs.s_Restleave + decSDays - decSday) * hrbi.BasicPrice * hrbi.OverTimeRate, 1);
                        }
                        else
                        {
                            decBasic = Math.Round((hrs.s_AttHours + hrs.s_Restleave + decSDays) * hrbi.BasicPrice, 1);
                            if (decBasic >= hrbi.BasicWage)
                                decBasic = hrbi.BasicWage;
                        }

                        if (decfin > decBasic + decOver)
                            decBenefit = decfin - decBasic - decOver;
                        else
                        {
                            if (decBasic >= decfin)
                            {
                                decBasic = decfin;
                                decOver = 0;
                            }
                            else
                                decOver = decfin - decBasic;
                        }
                    }


                    //decSday 为当月应出勤

                    //全月婚||丧||工价
                    //全月婚丧假（不折算？）等于病假周期的时候，取基本工资
                    if ((Convert.ToDecimal(sdtr["merrageDays"]) + Convert.ToDecimal(sdtr["funeralDays"]) + Convert.ToDecimal(sdtr["injuryDays"])) == hrbi.SickleaveDay)
                    {
                        decBasic = hrbi.BasicWage;
                        decOver = 0;
                    }

                    //--  全勤 & 工龄补贴---//
                    hrs.s_Allattendence = Convert.ToDecimal(sdtr["allattendence"]);
                    hrs.s_WYbonus = Convert.ToDecimal(sdtr["wybonus"]);
                    //--  End--------------//

                    /* 全月计件工资 = 汇报数量 * 工序单价
                     * 应得计件工资 = 全月计件工资 + 年休工资 + 婚假工资+ 丧假工资
                     * 应得计件工资 < 最低工资的话，这里就是最低工资，反之，是多少就是多少
                     */

                    //全月计件工资
                    Decimal wage_up = decBasic + decOver;
                    //如果是计件的话
                    if (intType == 0)
                    {
                        wage_up = Convert.ToDecimal(sdtr["UnitPrice"]);
                    }

                    //应得计件工资
                    //decSDays 是 折算后的婚丧假天数。年休、婚丧假付全额工资
                    Decimal wage_std = wage_up + Math.Round((hrs.s_Restleave + decSDays) * hrbi.BasicPrice, 1);

                    //如果每月汇报工资+（年休假+婚丧假）工资小于基本工资的，计算多少就是多少，否则，多出部分按加班费计算
                    if (wage_std <= hrbi.BasicWage)
                    {
                        decBasic = wage_std;
                        decOver = 0;
                    }
                    else
                    {
                        decBasic = hrbi.BasicWage;
                        decOver = wage_std - hrbi.BasicWage;
                    }

                    //应发金额 ＝基本工资　＋　加班费　＋ 津贴 ＋ 独生子女 ＋ 中夜 ＋ 补贴 ＋ 国假  + 全勤 & 工龄补贴 + 病、工伤 工资
                    decSalary = decBasic + decOver + hrs.s_Oallowance + hrs.s_NightMoney + hrs.s_Subsidies + hrs.s_Holiday + hrs.s_Allattendence + hrs.s_WYbonus + hrs.s_SickLeavePay + hrs.s_MaternityPay + hrs.s_Benefit;

                    // 上海需要补足基本工资（扣除社保的基础上）
                    //对为结算日时未作离职的，且不是工种变更的、无婚丧假工伤、也不是新员工的情况下，应发工资扣除社保等金额少于最低工资的，要补齐
                    if ((intPlantCode == 1) && (decSalary - hrs.s_Hfound - hrs.s_Rfound < hrbi.BasicWage) && (hrs.s_Fire == false) && (hrs.s_ExChange == false) && (hrs.s_Leave == true) && (blNewUser == false))
                    {
                        hrs.s_Subsidies = hrs.s_Subsidies + hrbi.BasicWage - decSalary + hrs.s_Hfound + hrs.s_Rfound;
                        decSalary = decSalary + hrs.s_Subsidies;
                    }

                    if (Special == false) //补足基本工资的员工的扣款放到下个月扣除
                    {
                        hrs.s_Duct = 0;
                        hrs.s_Currduct = decDeduct;
                    }
                    else
                    {　　//扣款不能超过应发金额的20%
                        if (decSalary * hrbi.DeductRate < decDeduct)
                        {
                            hrs.s_Duct = Math.Round(decSalary * hrbi.DeductRate, 1);
                            hrs.s_Currduct = decDeduct - hrs.s_Duct;
                        }
                        else
                        {
                            hrs.s_Duct = decDeduct;
                            hrs.s_Currduct = 0;
                        }
                    }

                    //--工会费---／税--------//
                    if (Convert.ToBoolean(sdtr["isLabourUnion"]))
                        hrs.s_Memship = Math.Round(hrbi.BasicWage * hrbi.LaborRate, 1);
                    else
                        hrs.s_Memship = 0;

                    hrs.s_Tax = Math.Round(SalaryTax(decSalary - hrs.s_Childwance, hrs.s_Duct + hrbi.Tax + hrs.s_Hfound + hrs.s_Rfound, intPlantCode), 2);

                    decimal decContants;//交税的税基呀
                    
                    decContants = decSalary - hrs.s_Childwance - hrs.s_Duct - hrbi.Tax - hrs.s_Hfound - hrs.s_Rfound;

                    if (decContants <= 1500)
                    {
                        hrs.s_TaxRate = 0.03M;
                        hrs.s_TaxDeduct = 0;
                    }
                    else
                    {
                        if (decContants <= 4500)
                        {
                            hrs.s_TaxRate = 0.1M;
                            hrs.s_TaxDeduct = 105;
                        }
                        else
                        {
                            if (decContants <= 9000)
                            {
                                hrs.s_TaxRate = 0.2M;
                                hrs.s_TaxDeduct = 555;
                            }
                            else
                            {
                                hrs.s_TaxRate = 0.25M;
                                hrs.s_TaxDeduct = 1005;
                            }
                        }
                    }

                    //-----end------------------//

                    hrs.s_Duereward = decSalary;
                    hrs.s_Workpay = hrs.s_Duereward - hrs.s_Duct - hrs.s_Tax - hrs.s_Hfound - hrs.s_Rfound - hrs.s_Memship;

                    hrs.s_Basic = decBasic;
                    hrs.s_Over = decOver;

                    hrs.s_Benefit = decBenefit;
                    hrs.s_Coef = decCoef;
                    hrs.s_Rate = decRateSalary;

                    if (hrs.s_Workpay < 0)
                        hrs.s_Workpay = 0;

                    SaveSalaryDataUP(hrs, strDate, intUserID);

                }

                sdtr.Close();

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 保存到hr_Salary_up
        /// </summary>
        /// <param name="hsy"></param>
        /// <param name="strDate"></param>
        /// <param name="intUserID"></param>
        public void SaveSalaryDataUP(HR_Salary hsy, string strDate, Int32 intUserID)
        {
            try
            {
                string strSql = "sp_hr_SaveSalaryUP";
                SqlParameter[] parmArray = new SqlParameter[71];
                parmArray[0] = new SqlParameter("@hr_Salary_userID", hsy.s_UserID);
                parmArray[1] = new SqlParameter("@hr_Salary_salaryDate", strDate);
                parmArray[2] = new SqlParameter("@hr_Salary_basic", hsy.s_Basic);
                parmArray[3] = new SqlParameter("@hr_Salary_over", hsy.s_Over);
                parmArray[4] = new SqlParameter("@hr_Salary_nightMoney", hsy.s_NightMoney);
                parmArray[5] = new SqlParameter("@hr_Salary_holiday", hsy.s_Holiday);
                parmArray[6] = new SqlParameter("@hr_Salary_oallowance", hsy.s_Oallowance);
                parmArray[7] = new SqlParameter("@hr_Salary_subsidies", hsy.s_Subsidies);
                parmArray[8] = new SqlParameter("@hr_Salary_duereward", hsy.s_Duereward);
                parmArray[9] = new SqlParameter("@hr_Salary_hfound", hsy.s_Hfound);
                parmArray[10] = new SqlParameter("@hr_Salary_rfound", hsy.s_Rfound);
                parmArray[11] = new SqlParameter("@hr_Salary_memship", hsy.s_Memship);
                parmArray[12] = new SqlParameter("@hr_Salary_duct", hsy.s_Duct);
                parmArray[13] = new SqlParameter("@hr_Salary_tax", hsy.s_Tax);
                parmArray[14] = new SqlParameter("@hr_Salary_workpay", hsy.s_Workpay);
                parmArray[15] = new SqlParameter("@hr_Salary_fire", (hsy.s_Fire) ? 1 : 0);
                parmArray[16] = new SqlParameter("@hr_Salary_currduct", hsy.s_Currduct);
                parmArray[17] = new SqlParameter("@hr_Salary_lastduct", hsy.s_Lastduct);
                parmArray[18] = new SqlParameter("@hr_Salary_restleave", hsy.s_Restleave);
                parmArray[19] = new SqlParameter("@hr_Salary_shday", hsy.s_Shday);
                parmArray[20] = new SqlParameter("@hr_Salary_shsalary", hsy.s_Shsalary);
                parmArray[21] = new SqlParameter("@hr_Salary_departmentID", hsy.s_Department);
                parmArray[22] = new SqlParameter("@hr_Salary_employTypeID", hsy.s_EmployTypeID);
                parmArray[23] = new SqlParameter("@hr_Salary_insuranceTypeID", hsy.s_InsuranceTypeID);
                parmArray[24] = new SqlParameter("@hr_Salary_workyear", hsy.s_Workyear);
                parmArray[25] = new SqlParameter("@hr_Salary_createdBy", intUserID);
                parmArray[26] = new SqlParameter("@hr_Salary_workShopID", hsy.s_WorkshopID);
                parmArray[27] = new SqlParameter("@hr_Salary_workGroupID", hsy.s_WorkgroupID);
                parmArray[28] = new SqlParameter("@hr_Salary_workKindID", hsy.s_WorkkindID);
                parmArray[29] = new SqlParameter("@hr_Salary_sickleave", hsy.s_SickLeave);
                parmArray[30] = new SqlParameter("@hr_Salary_sickleavePay", hsy.s_SickLeavePay);
                parmArray[31] = new SqlParameter("@hr_Salary_decRateDeduct", hsy.s_DecRateDeduct);
                parmArray[32] = new SqlParameter("@hr_Salary_workMoney", hsy.s_WorkMoney);
                parmArray[33] = new SqlParameter("@hr_Salary_maternityDays", hsy.s_MaternityDays);
                parmArray[34] = new SqlParameter("@hr_Salary_maternityPay", hsy.s_MaternityPay);
                parmArray[35] = new SqlParameter("@hr_Salary_realMoney", hsy.s_RealMoney);
                parmArray[36] = new SqlParameter("@hr_Salary_userType", hsy.s_UserType);
                parmArray[37] = new SqlParameter("@hr_Salary_leaveDay", hsy.s_Leaveday);
                parmArray[38] = new SqlParameter("@hr_Salary_minerDays", hsy.s_MinerDays);
                parmArray[39] = new SqlParameter("@hr_Salary_Fullattendence", hsy.s_Allattendence);
                parmArray[40] = new SqlParameter("@hr_Salary_LengService", hsy.s_WYbonus);

                parmArray[41] = new SqlParameter("@hr_Salary_AttendenceDay", hsy.s_Attdays);
                parmArray[42] = new SqlParameter("@hr_Salary_AttendenceHours", hsy.s_AttHours);
                parmArray[43] = new SqlParameter("@hr_Salary_AccountRfound", hsy.s_AccrFound);
                parmArray[44] = new SqlParameter("@hr_Salary_AccountEfound", hsy.s_AcceFound);
                parmArray[45] = new SqlParameter("@hr_Salary_AccountMfound", hsy.s_AccmFound);
                parmArray[46] = new SqlParameter("@hr_Salary_Mid", hsy.s_Mid);
                parmArray[47] = new SqlParameter("@hr_Salary_Night", hsy.s_Night);
                parmArray[48] = new SqlParameter("@hr_Salary_Whole", hsy.s_Whole);
                parmArray[49] = new SqlParameter("@hr_Salary_TaxRate", hsy.s_TaxRate);
                parmArray[50] = new SqlParameter("@hr_Salary_TaxDeduct", hsy.s_TaxDeduct);
                parmArray[51] = new SqlParameter("@hr_Salary_Exchange", (hsy.s_ExChange) ? 1 : 0);


                parmArray[52] = new SqlParameter("@hr_Salary_humanAllowance", hsy.s_HumanAllowance);
                parmArray[53] = new SqlParameter("@hr_Salary_hightTemp", hsy.s_HightTemp);
                parmArray[54] = new SqlParameter("@hr_Salary_normal", hsy.s_NOrmal);
                parmArray[55] = new SqlParameter("@hr_Salary_other", hsy.s_Other);


                parmArray[56] = new SqlParameter("@hr_Salary_materialDeduct", hsy.s_MaterialDeduct);
                parmArray[57] = new SqlParameter("@hr_Salary_assessDeduct", hsy.s_AssessDeduct);
                parmArray[58] = new SqlParameter("@hr_Salary_vacationDeduct", hsy.s_VacationDeduct);
                parmArray[59] = new SqlParameter("@hr_Salary_otherDeduct", hsy.s_OtherDeduct);
                parmArray[60] = new SqlParameter("@hr_Salary_ship", hsy.s_Ship);
                parmArray[61] = new SqlParameter("@hr_Salary_locFin", hsy.s_LocFin);
                parmArray[62] = new SqlParameter("@hr_Salary_quanlity", hsy.s_Quanlity);
                parmArray[63] = new SqlParameter("@hr_Salary_break", hsy.s_Break);

                parmArray[64] = new SqlParameter("@hr_Salary_newuser", hsy.s_Newuser);
                parmArray[65] = new SqlParameter("@hr_Salary_student", hsy.s_Student);
                parmArray[66] = new SqlParameter("@hr_Salary_bonus", hsy.s_Bonus);


                parmArray[67] = new SqlParameter("@hr_Salary_benefit", hsy.s_Benefit);
                parmArray[68] = new SqlParameter("@hr_Salary_coef", hsy.s_Coef);
                parmArray[69] = new SqlParameter("@hr_Salary_SalaryRate", hsy.s_Rate);

                parmArray[70] = new SqlParameter("@hr_Salary_Original", hsy.s_Original);


                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                ;
            }

        }

        /// <summary>
        /// Excel导出的SQL语句
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string ExportSalaryUP(int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_ExportSalaryUP";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Type", intType);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return null;
            }
        }

        public bool CheckSalaryValidityUP(int intYear, int intMonth, Int32 intUserID)
        {
            try
            {
                String strSql = "sp_hr_CheckSalaryValidityUP";

                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserID", intUserID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检验是否已经有过财务确认了
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <returns></returns>
        public bool finAdjustUP(int intYear, int intMonth)
        {
            try
            {
                string strSql = "sp_hr_finAdjustUP";

                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                parmArray[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return Convert.ToBoolean(parmArray[2].Value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 财务确认时的备份
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlantCode"></param>
        /// <param name="intUserID"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public int FinanceComfirmUP(int intYear, int intMonth, int intUserID)
        {
            try
            {
                string strSql = "sp_hr_FinanceSalaryConfirmUP";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserID", intUserID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 结算时的备份操作
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        public void BackupSalaryDateUP(int intYear, int intMonth)
        {
            try
            {
                string strSql = "sp_hr_BackupSalaryUP";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 重置税
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlant"></param>
        /// <param name="intCreat"></param>
        /// <returns></returns>
        public int AdjustPieceSalaryUP(int intYear, int intMonth, int intPlant, int intCreat)
        {
            try
            {
                HR_BaseInfo hrbi = SelectHRBaseInfo(intYear, intMonth);

                string strSql = "sp_hr_pieceAdjustTaxUP";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[1] = new SqlParameter("@Year", intYear);
                parmArray[2] = new SqlParameter("@Month", intMonth);
                parmArray[3] = new SqlParameter("@PlantCode", intPlant);
                parmArray[4] = new SqlParameter("@Creat", intCreat);

                SqlDataReader sdtr = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                decimal decSalary, decWorkpay, decOver, decBasic, decDeduct, decCurrent, decTax, decadjust, decReplace, decSub;

                while (sdtr.Read())
                {
                    decSalary = 0;
                    decWorkpay = 0;
                    decOver = 0;
                    decBasic = 0;
                    decDeduct = 0;
                    decCurrent = 0;
                    decadjust = 0;
                    decTax = 0;
                    decReplace = 0;
                    decSub = 0;

                    decadjust = Math.Round((Convert.ToDecimal(sdtr[1]) + Convert.ToDecimal(sdtr[2])) * Convert.ToDecimal(sdtr[10]) / 100 + Convert.ToDecimal(sdtr[11]), 2);

                    decReplace = decadjust;
                    // plantcode =1 do follow
                    if (intPlant == 1 && Convert.ToDecimal(sdtr[14]) > 0)
                    {
                        if (decReplace > 0)
                        {
                            if (decReplace >= Convert.ToDecimal(sdtr[14]))
                                decReplace = decReplace - Convert.ToDecimal(sdtr[14]);
                            else
                            {
                                decReplace = 0;
                                decSub = Convert.ToDecimal(sdtr[14]);
                            }
                        }
                        else
                        {
                            decReplace = 0;
                            decSub = Convert.ToDecimal(sdtr[14]);
                        }

                    }
                    //End

                    if (decReplace >= 0)
                    {
                        if (decReplace + Convert.ToDecimal(sdtr[1]) < hrbi.BasicWage)
                        {
                            decBasic = decReplace + Convert.ToDecimal(sdtr[1]);
                            decOver = Convert.ToDecimal(sdtr[2]);
                        }
                        else
                        {
                            decBasic = hrbi.BasicWage;
                            decOver = Convert.ToDecimal(sdtr[2]) + decReplace + Convert.ToDecimal(sdtr[1]) - decBasic;
                        }
                    }
                    else
                    {
                        if (decReplace + Convert.ToDecimal(sdtr[2]) >= 0)
                        {
                            decBasic = Convert.ToDecimal(sdtr[1]);
                            decOver = decReplace + Convert.ToDecimal(sdtr[2]);
                        }
                        else
                        {
                            decOver = 0;
                            if (decReplace + Convert.ToDecimal(sdtr[2]) + Convert.ToDecimal(sdtr[1]) > 0)
                                decBasic = decReplace + Convert.ToDecimal(sdtr[2]) + Convert.ToDecimal(sdtr[1]);
                            else
                                decBasic = 0;
                        }

                    }

                    if (intPlant == 1 && Convert.ToDecimal(sdtr[14]) > 0)
                    {
                        decSalary = (decSub == 0) ? Convert.ToDecimal(sdtr[3]) - Convert.ToDecimal(sdtr[14]) + decReplace : Convert.ToDecimal(sdtr[3]) + decReplace;
                    }
                    else
                        decSalary = Convert.ToDecimal(sdtr[3]) + decReplace;

                    if (decSalary - Convert.ToDecimal(sdtr[13]) > 0)
                    {
                        decTax = Math.Round(SalaryTax(decSalary - Convert.ToDecimal(sdtr[13]), Convert.ToDecimal(sdtr[8]) + hrbi.Tax + Convert.ToDecimal(sdtr[5]) + Convert.ToDecimal(sdtr[6]), intPlant), 2);
                        decWorkpay = decSalary - decTax - Convert.ToDecimal(sdtr[5]) - Convert.ToDecimal(sdtr[6]) - Convert.ToDecimal(sdtr[7]) - Convert.ToDecimal(sdtr[8]);

                        if (decWorkpay < 0)
                            decWorkpay = 0;
                    }

                    SaveAdjustTaxUP(Convert.ToInt32(sdtr[0]), decSalary, decWorkpay, decBasic, decOver, decDeduct, decCurrent, decTax, decadjust, intCreat, intPlant, decSub);
                }
                sdtr.Close();
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 保存重置税的结果
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="decSalary"></param>
        /// <param name="decWorkpay"></param>
        /// <param name="decBasic"></param>
        /// <param name="decOver"></param>
        /// <param name="decDeduct"></param>
        /// <param name="decCurrent"></param>
        /// <param name="decTax"></param>
        /// <param name="decadjust"></param>
        /// <param name="intCreat"></param>
        /// <param name="intPlant"></param>
        /// <param name="decSub"></param>
        public void SaveAdjustTaxUP(int intID, decimal decSalary, decimal decWorkpay, decimal decBasic, decimal decOver, decimal decDeduct, decimal decCurrent, decimal decTax, decimal decadjust, int intCreat, int intPlant, decimal decSub)
        {
            try
            {
                string strSql = "sp_hr_SaveAdjustTaxUP";
                SqlParameter[] parmArray = new SqlParameter[13];
                parmArray[1] = new SqlParameter("@aID", intID);
                parmArray[2] = new SqlParameter("@Salary", decSalary);
                parmArray[3] = new SqlParameter("@Workpay", decWorkpay);
                parmArray[4] = new SqlParameter("@Basic", decBasic);
                parmArray[5] = new SqlParameter("@Over", decOver);
                parmArray[6] = new SqlParameter("@Deduct", decDeduct);
                parmArray[7] = new SqlParameter("@Current", decCurrent);
                parmArray[8] = new SqlParameter("@Tax", decTax);
                parmArray[9] = new SqlParameter("@Adjust", decadjust);
                parmArray[10] = new SqlParameter("@Creat", intCreat);
                parmArray[11] = new SqlParameter("@PlantCode", intPlant);
                parmArray[12] = new SqlParameter("@Subsible", decSub);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 工资目录导出
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intType"></param>
        /// <param name="intPlant"></param>
        /// <returns></returns>
        public string ExportFinSalaryUP(int intYear, int intMonth, int intType, int intPlant)
        {
            try
            {
                string strSql = "sp_hr_ExportFin_SalaryUP";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Type", intType);
                parmArray[3] = new SqlParameter("@Plantcode", intPlant);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取工资比较版本
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlantCode"></param>
        /// <returns></returns>
        public static DataTable GetSalaryVersionUP(int intYear, int intMonth)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_SalaryVersionUP";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取单价法的比较数据
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="userNo"></param>
        /// <param name="userName"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intVersion"></param>
        /// <returns></returns>
        public static DataTable SelectSalaryCompareUP(int intYear, int intMonth, string userNo, string userName, int intDepartment, int intVersion)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_GetSalaryCompareUP";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", userNo);
                parmArray[3] = new SqlParameter("@UserName", userName);
                parmArray[4] = new SqlParameter("@DepartmentID", intDepartment);
                parmArray[5] = new SqlParameter("@Version", intVersion);
                parmArray[6] = new SqlParameter("@Type", -1);

                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取导出字符串
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intVersion"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string ExportSalaryCompareUP(int intYear, int intMonth, string strUserNo, string strUserName, int intDepartment, int intVersion, int intType)
        {
            try
            {
                string strSql = "sp_hr_GetSalaryCompareUP";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUserNo);
                parmArray[3] = new SqlParameter("@UserName", strUserName);
                parmArray[4] = new SqlParameter("@DepartmentID", intDepartment);
                parmArray[5] = new SqlParameter("@Version", intVersion);
                parmArray[6] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工资给银行EXCEL
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intBank"></param>
        /// <param name="intPlantCode"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string FinancetoBankUP(int intYear, int intMonth, int intBank, int intPlantCode, int intType)
        {
            try
            {
                string strSql = "sp_hr_FinancetoBankUP";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Bank", intBank);
                parmArray[3] = new SqlParameter("@PlantCode", intPlantCode);
                parmArray[4] = new SqlParameter("@Type", intType);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工资明细报表
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlantCode"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string FinanceSalaryUP(int intYear, int intMonth, int intPlantCode, int intType)
        {
            try
            {
                string strSql = "sp_hr_FinanceSalaryUP";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlantCode);
                parmArray[3] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工资汇总表
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlantCode"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string FinanceSalarySummaryUP(int intYear, int intMonth, int intPlantCode, int intType)
        {
            try
            {
                string strSql = "sp_hr_FinanceSalarySummaryUP";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlantCode);
                parmArray[3] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工资分析导出
        /// </summary>
        /// <param name="intplant"></param>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string exportSalaryTmpUP(int intplant, int intYear, int intMonth, int intType)
        {
            try
            {
                string str = "sp_Hr_SalaryTmpExportUP";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Plantcode", intplant);
                parmArray[1] = new SqlParameter("@Year", intYear);
                parmArray[2] = new SqlParameter("@Month", intMonth);
                parmArray[3] = new SqlParameter("@typeID", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray).ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 提交给财务时操作，此操作设置有权限
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intOperateID"></param>
        /// <returns></returns>
        public int AdjustToFinacialUP(int intYear, int intMonth, int intOperateID)
        {
            try
            {
                string strSql = "sp_hr_SalaryToFinanceUP";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@operateID", intOperateID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取调整记录
        /// </summary>
        /// <param name="module"></param>
        /// <param name="nbr"></param>
        /// <param name="lot"></param>
        /// <param name="procName"></param>
        /// <param name="userNo"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable SelectSalaryAdjusting(string module, string nbr, string lot, string procName, string userNo, string userName)
        {
            try
            {
                string strSql = "sp_hr_selectSalaryAdjusting";
                SqlParameter[] parmArray = new SqlParameter[6];
                parmArray[0] = new SqlParameter("@module", module);
                parmArray[1] = new SqlParameter("@nbr", nbr);
                parmArray[2] = new SqlParameter("@lot", lot);
                parmArray[3] = new SqlParameter("@procName", procName);
                parmArray[4] = new SqlParameter("@userNo", userNo);
                parmArray[5] = new SqlParameter("@userName", userName);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 工资调整
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlantcode"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intWorkshop"></param>
        /// <param name="intWorkgroup"></param>
        /// <param name="intWorktype"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="intOperateID"></param>
        /// <returns></returns>
        public string AdjustSalaryExportUP(string stdDate, string endDate, int intPlantcode, int intDepartment, int intWorkshop, int intWorkgroup, int intWorktype, string strUserNo, string strUserName, int intOperateID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_AdjustSalarySelectUP";
                SqlParameter[] parmArray = new SqlParameter[10];
                parmArray[0] = new SqlParameter("@stdDate", stdDate);
                parmArray[1] = new SqlParameter("@endDate", endDate);
                parmArray[2] = new SqlParameter("@Plantcode", intPlantcode);
                parmArray[3] = new SqlParameter("@department", intDepartment);
                parmArray[4] = new SqlParameter("@workshop", intWorkshop);
                parmArray[5] = new SqlParameter("@workgroup", intWorkgroup);
                parmArray[6] = new SqlParameter("@worktype", intWorktype);
                parmArray[7] = new SqlParameter("@Userno", strUserNo);
                parmArray[8] = new SqlParameter("@Username", strUserName);
                parmArray[9] = new SqlParameter("@OperateID", intOperateID);

                return SqlHelper.ExecuteScalar(adc.dsnx(), CommandType.StoredProcedure, strSql, parmArray).ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 保存调整的结果
        /// </summary>
        /// <param name="module"></param>
        /// <param name="id"></param>
        /// <param name="qtyAdjust"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool SaveSalaryAdjusting(string module, string id, string qtyAdjust, string uID, string uName)
        {
            try
            {
                string strSql = "sp_hr_saveSalaryAdjusting";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@module", module);
                parmArray[1] = new SqlParameter("@id", id);
                parmArray[2] = new SqlParameter("@qtyAdjust", qtyAdjust);
                parmArray[3] = new SqlParameter("@uID", uID);
                parmArray[4] = new SqlParameter("@uName", uName);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 已财务结算的，不予调整；不存在汇报记录的，不予调整
        /// </summary>
        /// <param name="eff_date"></param>
        /// <param name="userNo"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public bool SalaryUserCheckUP(string effDate, string userNo, string uID)
        {
            try
            {
                string strSql = "sp_hr_SalaryUserCheckUP";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@effDate", effDate);
                parmArray[1] = new SqlParameter("@userNo", userNo);
                parmArray[2] = new SqlParameter("@uID", uID);
                parmArray[3] = new SqlParameter("@retValue", SqlDbType.Bit);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return Convert.ToBoolean(parmArray[3].Value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检验指定的生效日期是否应财务结算了
        /// </summary>
        /// <param name="effDate"></param>
        /// <returns></returns>
        public bool CheckFinIsOver(string effDate)
        {
            try
            {
                string strSql = "sp_hr_checkFinIsOver";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@effDate", effDate);
                parmArray[1] = new SqlParameter("@retValue", SqlDbType.Bit);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return Convert.ToBoolean(parmArray[1].Value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 把调整数据从临时表转存到正式表
        /// </summary>
        /// <param name="uID"></param>
        public void AdjustSalarySaveUP(string uID)
        {
            try
            {
                string strSql = "sp_hr_AdjustSalarySaveUP";

                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                ;
            }
        }
        /// <summary>
        /// 保存工资调整的情况到临时表中
        /// </summary>
        /// <param name="effDate"></param>
        /// <param name="intDepartment"></param>
        /// <param name="intWorkshop"></param>
        /// <param name="intWorkgroup"></param>
        /// <param name="intWorktype"></param>
        /// <param name="userNo"></param>
        /// <param name="decAdjPercent"></param>
        /// <param name="decAdjMoney"></param>
        /// <param name="strReason"></param>
        /// <param name="uID"></param>
        /// <returns>如果找到员工，也返回false</returns>
        public bool SaveSalaryAdjustToTemp(string effDate, int dept, int workshop, int workgroup, int worktype, string userNo, decimal percent, decimal money, string reason, string uID)
        {
            try
            {
                string strSql = "sp_hr_saveSalaryAdjustToTemp";
                SqlParameter[] parmArray = new SqlParameter[11];
                parmArray[0] = new SqlParameter("@effDate", effDate);
                parmArray[1] = new SqlParameter("@dept", dept);
                parmArray[2] = new SqlParameter("@workshop", workshop);
                parmArray[3] = new SqlParameter("@workgroup", workgroup);
                parmArray[4] = new SqlParameter("@worktype", worktype);
                parmArray[5] = new SqlParameter("@userNo", userNo);
                parmArray[6] = new SqlParameter("@percent", percent);
                parmArray[7] = new SqlParameter("@money", money);
                parmArray[8] = new SqlParameter("@reason", reason);
                parmArray[9] = new SqlParameter("@uID", uID);
                parmArray[10] = new SqlParameter("@retValue", SqlDbType.Bit);
                parmArray[10].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return Convert.ToBoolean(parmArray[10].Value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 选取临时调整表
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public DataTable SelectSalaryAdjustTemp(string uID)
        {
            try
            {
                string strSql = "sp_hr_selectSalaryAdjustTemp";

                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 选择正式的调整记录（非导出格式）
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public DataTable SelectSalaryAdjust(string year, string month, string dept, string workshop, string workgroup, string worktype, string userNo)
        {
            try
            {
                string strSql = "sp_hr_selectSalaryAdjust";

                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@year", year);
                parmArray[1] = new SqlParameter("@month", month);
                parmArray[2] = new SqlParameter("@dept", dept);
                parmArray[3] = new SqlParameter("@workshop", workshop);
                parmArray[4] = new SqlParameter("@workgroup", workgroup);
                parmArray[5] = new SqlParameter("@worktype", worktype);
                parmArray[6] = new SqlParameter("@userNo", userNo);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 调整工资导出SQL语句
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="dept"></param>
        /// <param name="workshop"></param>
        /// <param name="workgroup"></param>
        /// <param name="worktype"></param>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public String SelectSalaryAdjustExport(string year, string month, string dept, string workshop, string workgroup, string worktype, string userNo)
        {
            try
            {
                string strSql = "sp_hr_selectSalaryAdjustExport";

                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@year", year);
                parmArray[1] = new SqlParameter("@month", month);
                parmArray[2] = new SqlParameter("@dept", dept);
                parmArray[3] = new SqlParameter("@workshop", workshop);
                parmArray[4] = new SqlParameter("@workgroup", workgroup);
                parmArray[5] = new SqlParameter("@worktype", worktype);
                parmArray[6] = new SqlParameter("@userNo", userNo);

                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 选取导入时出现问题的记录
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public DataTable SelectSalaryAdjustTempError(string uID)
        {
            try
            {
                string strSql = "sp_hr_selectSalaryAdjustTempError";

                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 删除临时调整
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public void DeleteSalaryAdjustTemp(string id, string uID)
        {
            try
            {
                string strSql = "sp_hr_deleteSalaryAdjustTemp";

                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@id", id);
                parmArray[1] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                ;
            }
        }
        /// <summary>
        /// 更新临时调整
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uID"></param>
        public void UpdateSalaryAdjustTemp(string id, string money, string reason, string uID)
        {
            try
            {
                string strSql = "sp_hr_updateSalaryAdjustTemp";

                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@id", id);
                parmArray[1] = new SqlParameter("@money", money);
                parmArray[2] = new SqlParameter("@reason", reason);
                parmArray[3] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                ;
            }
        }
        /// <summary>
        /// 导入调整的同时，导出错误项
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public Boolean CheckSalaryAdjustError(string uID, string effDate)
        {
            try
            {
                string strSql = "sp_hr_checkSalaryAdjustError";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@uID", uID);
                sqlParam[1] = new SqlParameter("@effDate", effDate);
                sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
                sqlParam[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);

                return Convert.ToBoolean(sqlParam[2].Value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 清空调整临时表
        /// </summary>
        /// <param name="uID"></param>
        public bool ClearSalaryAdjustToTemp(string uID)
        {
            try
            {
                string strSql = "sp_hr_clearSalaryAdjustTemp";
                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 工资打印
        /// </summary>
        /// <param name="intType"></param>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intDept"></param>
        /// <param name="intWorkshop"></param>
        /// <param name="intWorkgroup"></param>
        /// <param name="intInsurance"></param>
        /// <param name="intEmployType"></param>
        /// <param name="strUserNo"></param>
        /// <param name="intWorkType"></param>
        /// <param name="intPlant"></param>
        /// <returns></returns>
        public string PrintStringUP(int intType, int intYear, int intMonth, int intDept, int intWorkshop, int intWorkgroup, int intInsurance, int intEmployType, string strUserNo, int intWorkType, int intPlant)
        {

            try
            {
                string strSql = "sp_hr_PrintSalaryUP";
                SqlParameter[] parmArray = new SqlParameter[11];
                parmArray[0] = new SqlParameter("@Type", intType);
                parmArray[1] = new SqlParameter("@Year", intYear);
                parmArray[2] = new SqlParameter("@Month", intMonth);
                parmArray[3] = new SqlParameter("@department", intDept);
                parmArray[4] = new SqlParameter("@workshop", intWorkshop);
                parmArray[5] = new SqlParameter("@workgroup", intWorkgroup);
                parmArray[6] = new SqlParameter("@Insurance", intInsurance);
                parmArray[7] = new SqlParameter("@EmployType", intEmployType);
                parmArray[8] = new SqlParameter("@UserNo", strUserNo);
                parmArray[9] = new SqlParameter("@WorkType", intWorkType);
                parmArray[10] = new SqlParameter("@PlantCode", intPlant);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 没有工资查询
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strName"></param>
        /// <param name="intDept"></param>
        /// <param name="PlantCode"></param>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public DataTable NoSalarySelectUP(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                string strSql = hr_salary.NoSalaryUP(strUser, strName, intDept, PlantCode, intYear, intMonth, intType);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 没有工资查询
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strName"></param>
        /// <param name="intDept"></param>
        /// <param name="PlantCode"></param>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string NoSalaryUP(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_NoSalarySelectUP";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@Depart", intDept);
                parmArray[5] = new SqlParameter("@PlantCode", PlantCode);
                parmArray[6] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工费对比报表
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strName"></param>
        /// <param name="intDept"></param>
        /// <param name="PlantCode"></param>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string WorkOrderCompareUP(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_WorkCostCompareUP";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@Depart", intDept);
                parmArray[5] = new SqlParameter("@PlantCode", PlantCode);
                parmArray[6] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 工费对比报表
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strName"></param>
        /// <param name="intDept"></param>
        /// <param name="PlantCode"></param>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public static DataTable SelectWorkCostCompareUP(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                string strSql = hr_salary.WorkOrderCompareUP(strUser, strName, intDept, PlantCode, intYear, intMonth, intType);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch
            {
                return null;
            }

        }
    }
}
