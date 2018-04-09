using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Collections;
using adamFuncs;
using System.IO;

namespace Wage
{
    /// <summary>
    /// Summary description for HR
    /// </summary>
    public partial class HR
    {

        protected adamClass adam = new adamClass();

        public HR()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Public Information
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>����ID����������</returns>
        public static DataTable GetDepartment(int intSiteID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_Department";
                SqlParameter parm = new SqlParameter("@siteID", intSiteID);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="siteID">��/PlantCode</param>
        /// <param name="intDeparmentID">����ID</param>
        /// <returns>����ID����������</returns>
        public static DataTable GetWorkShop(int intSiteID, int intDeparmentID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_WorkShop";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@siteID", intSiteID);
                parmArray[1] = new SqlParameter("@departmentID", intDeparmentID);

                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="intSiteID">��/PlantCode</param>
        /// <param name="intWorkshopID">����ID</param>
        /// <returns>����ID,��������</returns>
        public static DataTable GetWorkGroup(int intSiteID, int intWorkshopID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_WorkGroup";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@siteID", intSiteID);
                parmArray[1] = new SqlParameter("@workshopID", intWorkshopID);

                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="intSiteID">��/PlantCode</param>
        /// <param name="intWorkgroupID">����ID</param>
        /// <returns>����ID,��������</returns>
        public static DataTable GetWorkType(int intSiteID, int intWorkgroupID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_WorkType";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@siteID", intSiteID);
                parmArray[1] = new SqlParameter("@workgroupID", intWorkgroupID);

                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ�ۿ�����
        /// </summary>
        /// <returns>����ID������</returns>
        public static DataTable GetDeductType()
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_DeductType";
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ�ù�����
        /// </summary>
        /// <returns>ID������</returns>
        public static DataTable GetEmployType()
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_EmployType";
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns>ID������</returns>
        public static DataTable GetInsurance()
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_Insurance";
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetSalaryType()
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_SalaryType";
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetBank(int intSiteID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_Hr_Bank";
                SqlParameter parm = new SqlParameter("@siteID", intSiteID);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        public string exportSalaryTmp(int intplant, int intYear, int intMonth, int intType)
        {
            try
            {
                string str = "sp_Hr_SalaryTmpExport";
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

        # endregion

        #region ���ʻ�����Ϣ DB:hr_bi_mstr
        /// <summary>
        /// ��ù��ʻ�����Ϣ
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>���ع��ʻ�����Ϣ</returns>
        public virtual HR_BaseInfo SelectHRBaseInfo(int Year, int Month)
        {
            try
            {
                string strSql = "sp_hr_SelectBaseInfo";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@year", Year);
                sqlParam[1] = new SqlParameter("@month", Month);

                HR_BaseInfo hr_bi = new HR_BaseInfo();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    hr_bi.AMStartTime = reader["AMStartTime"].ToString();
                    hr_bi.AvgDays = Convert.ToDecimal(reader["AvgDays"]);
                    hr_bi.BasicPrice = Convert.ToDecimal(reader["BasicPrice"]);
                    hr_bi.BasicWage = Convert.ToDecimal(reader["BasicWage"]);
                    hr_bi.DeductRate = Convert.ToDecimal(reader["DeductRate"]);
                    hr_bi.DinnerTime = Convert.ToDecimal(reader["DinnerTime"]);
                    hr_bi.FixedDays = Convert.ToDecimal(reader["FixedDays"]);
                    hr_bi.HolidayRate = Convert.ToDecimal(reader["HolidayRate"]);
                    hr_bi.LaborRate = Convert.ToDecimal(reader["LaborRate"]);
                    hr_bi.LunchTime = Convert.ToDecimal(reader["LunchTime"]);
                    hr_bi.MidNightStartTime = reader["MidNightStartTime"].ToString();
                    hr_bi.MidNightSubsidy = Convert.ToDecimal(reader["MidNightSubsidy"].ToString());
                    hr_bi.NightStartTime = reader["NightStartTime"].ToString();
                    hr_bi.NightSubsidy = Convert.ToDecimal(reader["NightSubsidy"]);
                    hr_bi.OverTimeRate = Convert.ToDecimal(reader["OverTimeRate"]);
                    hr_bi.PMEndTime = reader["PMEndTime"].ToString();
                    hr_bi.SaturdayRate = Convert.ToDecimal(reader["SaturdayRate"]);
                    hr_bi.Tax = Convert.ToInt32(reader["Tax"].ToString());
                    hr_bi.WholeNightStartTime = reader["WholeNightStartTime"].ToString();
                    hr_bi.WholeNightSubsidy = Convert.ToDecimal(reader["WholeNightSubsidy"]);
                    hr_bi.WorkDays = Convert.ToDecimal(reader["WorkDays"]);
                    hr_bi.WorkHours = Convert.ToInt32(reader["WorkHours"]);
                    hr_bi.StartTime = reader["bi_start"].ToString();
                    hr_bi.EndTime = reader["bi_end"].ToString();
                    hr_bi.RestTime = Convert.ToDecimal(reader["bi_rest"]);
                    hr_bi.OtherTime = Convert.ToDecimal(reader["bi_other"]);

                    hr_bi.bi_Work1 = Convert.ToDecimal(reader["bi_work1"]);
                    hr_bi.bi_Work2 = Convert.ToDecimal(reader["bi_Work2"]);

                    hr_bi.bi_Night1 = Convert.ToInt32(reader["bi_night1"]);
                    hr_bi.bi_Night2 = Convert.ToInt32(reader["bi_night2"]);

                    hr_bi.SickleaveRate = Convert.ToDecimal(reader["SickleaveRate"]);
                    hr_bi.SickleaveDay = Convert.ToDecimal(reader["SickleaveDay"]);

                    hr_bi.Social = Convert.ToDecimal(reader["SocialInsuranceBase"]);
                    hr_bi.Oldage = Convert.ToDecimal(reader["OldageInsurance"]);
                    hr_bi.Unemploy = Convert.ToDecimal(reader["UnemployInsurance"]);
                    hr_bi.Injury = Convert.ToDecimal(reader["InjuryInsurance"]);
                    hr_bi.Maternity = Convert.ToDecimal(reader["MaternityInsurance"]);
                    hr_bi.Health = Convert.ToDecimal(reader["HealthInsurance"]);
                    hr_bi.HousingFund = Convert.ToDecimal(reader["HousingFund"]);
                    hr_bi.UnionFee = Convert.ToDecimal(reader["UnionFee"]);

                    hr_bi.A_Oldage = Convert.ToDecimal(reader["AgricultureOldageInsurance"]);
                    hr_bi.A_Health = Convert.ToDecimal(reader["AgricultureHealthInsurance"]);
                    hr_bi.A_Injury = Convert.ToDecimal(reader["AgricultureInjuryInsurance"]);

                    hr_bi.MaxAttbonus = Convert.ToDecimal(reader["MaxAttbonus"]);
                    hr_bi.MinAttbonus = Convert.ToDecimal(reader["MinAttbonus"]);
                    hr_bi.MaxWYbonus = Convert.ToDecimal(reader["MaxWYbonus"]);
                    hr_bi.MinWYbonus = Convert.ToDecimal(reader["MinWYbonus"]);
                    hr_bi.WorkYearbonus = Convert.ToDecimal(reader["WorkYearbonus"]);

                    hr_bi.OverPrice = Convert.ToDecimal(reader["OverPrice"]);
                    hr_bi.PercentAttbonus = Convert.ToDecimal(reader["PercentAttbonus"]);
                    hr_bi.PercentWYbonus = Convert.ToDecimal(reader["PercentWYbonus"]);
                    hr_bi.MinWorkDays = Convert.ToDecimal(reader["MinWorkDays"]);

                    hr_bi.isMinus = Convert.ToBoolean(reader["isMinus"]);
                    hr_bi.BasicWageNew = Convert.ToDecimal(reader["BasicWageNew"]);
                }
                reader.Close();
                return hr_bi;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ���¹��ʻ�����Ϣ
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="uID"></param>
        /// <param name="sqlparam"></param>
        /// <returns>�����Ƿ���³ɹ�</returns>
        public bool UpdateHRBaseInfo(int Year, int Month, int uID, HR_BaseInfo hb)
        {
            try
            {
                string strSql = "sp_hr_UpdateBaseInfo";

                SqlParameter[] sqlParam = new SqlParameter[42];
                sqlParam[0] = new SqlParameter("@year", Year);
                sqlParam[1] = new SqlParameter("@month", Month);
                sqlParam[2] = new SqlParameter("@uID", uID);
                sqlParam[3] = new SqlParameter("@AvgDays", hb.AvgDays);
                sqlParam[4] = new SqlParameter("@BasicPrice", hb.BasicPrice);
                sqlParam[5] = new SqlParameter("@BasicWage", hb.BasicWage);
                sqlParam[6] = new SqlParameter("@DeductRate", hb.DeductRate);
                sqlParam[7] = new SqlParameter("@FixedDays", hb.FixedDays);
                sqlParam[8] = new SqlParameter("@HolidayRate", hb.HolidayRate);
                sqlParam[9] = new SqlParameter("@LaborRate", hb.LaborRate);
                sqlParam[10] = new SqlParameter("@MidNightSubsidy", hb.MidNightSubsidy);
                sqlParam[11] = new SqlParameter("@NightSubsidy", hb.NightSubsidy);
                sqlParam[12] = new SqlParameter("@OverTimeRate", hb.OverTimeRate);
                sqlParam[13] = new SqlParameter("@SaturdayRate", hb.SaturdayRate);
                sqlParam[14] = new SqlParameter("@Tax", hb.Tax);
                sqlParam[15] = new SqlParameter("@WholeNightSubsidy", hb.WholeNightSubsidy);
                sqlParam[16] = new SqlParameter("@WorkDays", hb.WorkDays);
                sqlParam[17] = new SqlParameter("@WorkHours", hb.WorkHours);
                sqlParam[18] = new SqlParameter("@SickLeaveRate", hb.SickleaveRate);
                sqlParam[19] = new SqlParameter("@SickleaveDay", hb.SickleaveDay);
                sqlParam[20] = new SqlParameter("@Social", hb.Social);
                sqlParam[21] = new SqlParameter("@Oldage", hb.Oldage);
                sqlParam[22] = new SqlParameter("@Unemploy", hb.Unemploy);
                sqlParam[23] = new SqlParameter("@Injury", hb.Injury);
                sqlParam[24] = new SqlParameter("@Maternity", hb.Maternity);
                sqlParam[25] = new SqlParameter("@Health", hb.Health);
                sqlParam[26] = new SqlParameter("@HousingFund", hb.HousingFund);
                sqlParam[27] = new SqlParameter("@UnionFee", hb.UnionFee);
                sqlParam[28] = new SqlParameter("@AOldage", hb.A_Oldage);
                sqlParam[29] = new SqlParameter("@AHealth", hb.A_Health);
                sqlParam[30] = new SqlParameter("@AInjury", hb.A_Injury);
                sqlParam[31] = new SqlParameter("@MaxAttbonus", hb.MaxAttbonus);
                sqlParam[32] = new SqlParameter("@MinAttbonus", hb.MinAttbonus);
                sqlParam[33] = new SqlParameter("@MaxWYbonus", hb.MaxWYbonus);
                sqlParam[34] = new SqlParameter("@MinWYbonus", hb.MinWYbonus);
                sqlParam[35] = new SqlParameter("@WorkYearbonus", hb.WorkYearbonus);
                sqlParam[36] = new SqlParameter("@OverPrice", hb.OverPrice);
                sqlParam[37] = new SqlParameter("@PercentAttbonus", hb.PercentAttbonus);
                sqlParam[38] = new SqlParameter("@PercentWYbonus", hb.PercentWYbonus);
                sqlParam[39] = new SqlParameter("@MinWorkDays", hb.MinWorkDays);
                sqlParam[40] = new SqlParameter("@isMinus", hb.isMinus);
                sqlParam[41] = new SqlParameter("@BasicWageNew", hb.BasicWageNew);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// �жϿ����Ƿ��мӰ��
        /// </summary>
        /// <param name="start">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="total">�ܼ�</param>
        /// <returns>2-ȫҹ  1-ҹ��  0-�а�</returns>
        public int CalculateNight(decimal start, decimal end, decimal total)
        {
            int intflag = -1;
            if (total >= 24 || (total >= 12 && start <= 22 && end >= 30))
            {
                intflag = 2;
            }
            else
            {
                if (total >= 8 && (end > 24 || (start >= 0 && start <= 3)))
                    intflag = 1;
                else
                {
                    if (total >= 8 && (end >= 22 && end <= 24))
                        intflag = 0;
                }
            }
            return intflag;
        }

        #endregion ���ʻ�����Ϣ

        # region �������� DB:hr_holiday_mstr

        /// <summary>
        /// ��ù���������Ϣ
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>���ع���������Ϣ</returns>
        public IList<HR_HolidayInfo> SelectHolidayList(int year, int month)
        {
            try
            {
                string strSql = "sp_hr_SelectHoliday";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@year", year);
                sqlParam[1] = new SqlParameter("@month", month);

                IList<HR_HolidayInfo> HolidayInfo = new List<HR_HolidayInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    HR_HolidayInfo hi = new HR_HolidayInfo();
                    hi.HolidayID = Convert.ToInt32(reader["HolidayID"]);
                    hi.HolidayDate = Convert.ToDateTime(reader["HolidayDate"]);
                    hi.HolidayYear = Convert.ToInt32(reader["HolidayYear"]);
                    hi.HolidayMonth = Convert.ToInt32(reader["HolidayMonth"]);
                    hi.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    hi.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    HolidayInfo.Add(hi);
                }
                reader.Close();
                return HolidayInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public IList<HR_HolidayInfo> SelectHolidayListNotfee(int year, int month)
        {
            try
            {
                string strSql = "sp_hr_SelectHoliday_notfee";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@year", year);
                sqlParam[1] = new SqlParameter("@month", month);

                IList<HR_HolidayInfo> HolidayInfo = new List<HR_HolidayInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    HR_HolidayInfo hi = new HR_HolidayInfo();
                    hi.HolidayID = Convert.ToInt32(reader["HolidayID"]);
                    hi.HolidayDate = Convert.ToDateTime(reader["HolidayDate"]);
                    hi.HolidayYear = Convert.ToInt32(reader["HolidayYear"]);
                    hi.HolidayMonth = Convert.ToInt32(reader["HolidayMonth"]);
                    hi.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    hi.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    HolidayInfo.Add(hi);
                }
                reader.Close();
                return HolidayInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ��������������Ϣ
        /// </summary>
        /// <param name="strHoliday"></param>
        /// <param name="uID"></param>
        /// <returns>�����Ƿ������ɹ�</returns>
        public bool InsertHoliday(string strHoliday, int uID)
        {
            try
            {
                string strSql = "sp_hr_InsertHoliday";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@holiday", strHoliday);
                sqlParam[1] = new SqlParameter("@uID", uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public bool InsertHolidayNotfee(string strHoliday, int uID)
        {
            try
            {
                string strSql = "sp_hr_InsertHoliday_notfee";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@holiday", strHoliday);
                sqlParam[1] = new SqlParameter("@uID", uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ɾ������������Ϣ
        /// </summary>
        /// <param name="HolidayID"></param>
        /// <returns>�����Ƿ�ɾ���ɹ�</returns>
        public bool DeleteHoliday(int HolidayID)
        {
            try
            {
                string strSql = "sp_hr_DeleteHoliday";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@holidayID", HolidayID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        public bool DeleteHolidayNotfee(int HolidayID)
        {
            try
            {
                string strSql = "sp_hr_DeleteHoliday_notfee";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@holidayID", HolidayID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        #endregion

        #region ��� DB:hr_leave_mstr

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <returns>���������Ϣ</returns>
        public IList<HR_LeaveInfo> SelectLeaveList(int Type, string strStart, string strEnd, string strUserNo, string strUserName, int uID, int uRole)
        {
            try
            {
                string strSql = string.Empty;
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@start", strStart);
                sqlParam[1] = new SqlParameter("@end", strEnd);
                sqlParam[2] = new SqlParameter("@userno", strUserNo);
                sqlParam[3] = new SqlParameter("@username", strUserName);
                sqlParam[4] = new SqlParameter("@uID", uID);
                sqlParam[5] = new SqlParameter("@uRole", uRole);

                switch (Type)
                {
                    //�¼�
                    case 1:
                        strSql = "sp_hr_SelectBussinessLeave";
                        break;

                    //����
                    case 2:
                        strSql = "sp_hr_SelectSickLeave";
                        break;

                    //���
                    case 3:
                        strSql = "sp_hr_SelectMerrageLeave";
                        break;

                    //ɥ��
                    case 4:
                        strSql = "sp_hr_SelectFuneralLeave";
                        break;

                    //����
                    case 5:
                        strSql = "sp_hr_SelectMinerLeave";
                        break;

                    //����
                    case 6:
                        strSql = "sp_hr_SelectMaternityLeave";
                        break;

                    //����
                    case 7:
                        strSql = "sp_hr_SelectInjuryLeave";
                        break;
                }

                IList<HR_LeaveInfo> HR_LeaveInfo = new List<HR_LeaveInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    HR_LeaveInfo li = new HR_LeaveInfo();
                    li.LeaveID = Convert.ToInt32(reader["LeaveID"]);
                    li.UserCode = reader["UserCode"].ToString();
                    li.UserName = reader["UserName"].ToString();
                    li.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    li.EndDate = Convert.ToDateTime(reader["EndDate"]);
                    li.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    li.UserName = reader["UserName"].ToString();
                    li.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    li.Creater = reader["Creater"].ToString();
                    li.Days = Convert.ToDecimal(reader["Days"]);
                    li.Comment = reader["Comment"].ToString();
                    HR_LeaveInfo.Add(li);
                }
                reader.Close();
                return HR_LeaveInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ���Ա����Ϣ
        /// </summary>
        /// <param name="strUserNo">Ա������</param>
        /// <returns>����Ա������</returns>
        public string SelectUserName(string strUserNo, int PlantID)
        {
            try
            {
                string strSql = "sp_hr_SelectUserName";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@uno", strUserNo);
                sqlParam[1] = new SqlParameter("@plant", PlantID);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return "";
            }
        }

        /// <summary>
        /// �ж����������Ƿ����ڽ��������뿪ʼ���ڵĲ�ֵ��
        /// </summary>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strDay"></param>
        /// <returns>����True/False</returns>
        public bool CheckLeaveDays(string strStart, string strEnd, string strDay)
        {
            try
            {
                TimeSpan startdate = new TimeSpan(Convert.ToDateTime(strStart).Ticks);
                TimeSpan enddate = new TimeSpan(Convert.ToDateTime(strEnd).Ticks);
                TimeSpan datediff = enddate.Subtract(startdate).Duration();

                if (strDay != "0")
                {
                    if (Convert.ToDecimal(strDay) > datediff.Days + 1 || Convert.ToDecimal(strDay) < datediff.Days) return false;
                    else return true;
                }
                else return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��û�ټ�¼
        /// </summary>
        /// <param name="strLaborID"></param>
        /// <returns>���ػ������</returns>
        public int SelectMerrageDaysInfo(string strLaborID)
        {
            try
            {
                string strSql = "sp_hr_SelectMerrageLeaveInfo";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@laborid", strLaborID);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -1;
            }
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="lID"></param>
        /// <param name="lNo"></param>
        /// <param name="lName"></param>
        /// <param name="uID"></param>
        /// <returns>�����Ƿ������ɹ�</returns>
        public bool InsertLeaveInfo(int Type, string strStart, string strEnd, string strDay, string strLaborID, string strLaborNo, string strLaborName, string strMemo, int uID)
        {
            try
            {
                string strSql = string.Empty;

                //if (strDay == "") strDay = "0";

                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@start", strStart);
                sqlParam[1] = new SqlParameter("@end", strEnd);
                sqlParam[2] = new SqlParameter("@day", strDay == "" ? "0" : strDay);
                sqlParam[3] = new SqlParameter("@laborid", strLaborID);
                sqlParam[4] = new SqlParameter("@laborno", strLaborNo);
                sqlParam[5] = new SqlParameter("@laborname", strLaborName);
                sqlParam[6] = new SqlParameter("@memo", strMemo);
                sqlParam[7] = new SqlParameter("@uid", uID);

                switch (Type)
                {
                    //�¼�
                    case 1:
                        strSql = "sp_hr_InsertBussinessLeave";
                        break;

                    //����
                    case 2:
                        strSql = "sp_hr_InsertSickLeave";
                        break;

                    //���
                    case 3:
                        strSql = "sp_hr_InsertMerrageLeave";
                        break;

                    //ɥ��
                    case 4:
                        strSql = "sp_hr_InsertFuneralLeave";
                        break;

                    //����
                    case 5:
                        strSql = "sp_hr_InsertMinerLeave";
                        break;

                    //����
                    case 6:
                        strSql = "sp_hr_InsertMaternityLeave";
                        break;

                    //����
                    case 7:
                        strSql = "sp_hr_InsertInjuryLeave";
                        break;
                }

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ɾ�������Ϣ
        /// </summary>
        /// <param name="LevelID"></param>
        /// <returns></returns>
        public bool DeleteLeaveInfo(int LeaveID)
        {
            try
            {
                string strSql = "sp_hr_DeleteLeaveInfo";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@leaveid", LeaveID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��������ϢSql
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="uID"></param>
        /// <param name="uRole"></param>
        /// <returns>���������ϢSql</returns>
        public string SelectLeaveExcel(int Type, string strStart, string strEnd, string strUserNo, string strUserName, int uID, int uRole)
        {
            try
            {
                string strSql = string.Empty;
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@start", strStart);
                sqlParam[1] = new SqlParameter("@end", strEnd);
                sqlParam[2] = new SqlParameter("@userno", strUserNo);
                sqlParam[3] = new SqlParameter("@username", strUserName);
                sqlParam[4] = new SqlParameter("@uID", uID);
                sqlParam[5] = new SqlParameter("@uRole", uRole);
                sqlParam[6] = new SqlParameter("@type", Type.ToString());

                strSql = "sp_hr_SelectLeaveExcel";

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return "";
            }
        }

        #endregion

        #region ������ٲ�ƥ��

        /// <summary>
        /// ������ٲ�ƥ��
        /// </summary>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="DepartmentID"></param>
        /// <returns>���ؿ�����ٲ�ƥ������б�</returns>
        public IList<HR_AttendLeaveMisMatch> SelectAttendLeaveMisMatch(string strStart, string strEnd, string strUserNo, string strUserName, int DepartmentID)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendLeaveMisMatch";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@start", strStart);
                sqlParam[1] = new SqlParameter("@end", strEnd);
                sqlParam[2] = new SqlParameter("@userno", strUserNo);
                sqlParam[3] = new SqlParameter("@username", strUserName);
                sqlParam[4] = new SqlParameter("@departmentid", DepartmentID);

                IList<HR_AttendLeaveMisMatch> HR_AttendLeaveMisMatch = new List<HR_AttendLeaveMisMatch>();
                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    HR_AttendLeaveMisMatch almm = new HR_AttendLeaveMisMatch();
                    almm.RowNo = Convert.ToInt32(reader["RowNo"]);
                    almm.UserCode = reader["UserCode"].ToString();
                    almm.UserName = reader["UserName"].ToString();
                    almm.AttendDate = Convert.ToDateTime(reader["AttendDate"]);
                    almm.AttendDays = Convert.ToDecimal(reader["AttendDays"]);
                    almm.Department = reader["Department"].ToString();
                    almm.Construct = reader["Construct"].ToString();
                    almm.LeaveDays = Convert.ToDecimal(reader["LeaveDays"]);
                    HR_AttendLeaveMisMatch.Add(almm);
                }
                reader.Close();
                return HR_AttendLeaveMisMatch;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ������ٲ�ƥ�����EXCEL
        /// </summary>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strUserNo"></param>
        /// <param name="strUserName"></param>
        /// <param name="DepartmentID"></param>
        /// <returns>���ؿ�����ٲ�ƥ������б�</returns>
        public string SelectAttendLeaveMisMatchExcel(string strStart, string strEnd, string strUserNo, string strUserName, int DepartmentID)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendLeaveMisMatch";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@start", strStart);
                sqlParam[1] = new SqlParameter("@end", strEnd);
                sqlParam[2] = new SqlParameter("@userno", strUserNo);
                sqlParam[3] = new SqlParameter("@username", strUserName);
                sqlParam[4] = new SqlParameter("@departmentid", DepartmentID);

                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        #endregion

        #region �籣 hr_accfound_mstr
        /// <summary>
        /// �籣��Ϣ�
        /// </summary>
        /// <param name="intYear">���</param>
        /// <param name="intMonth">�·�</param>
        /// <param name="strUserno">����</param>
        /// <param name="strUsername">����</param>
        /// <param name="intDept">����ID</param>
        /// <param name="intSatus">Ա��״̬</param>
        /// <param name="intIns">�籣����</param>
        /// <param name="flag">1�������ݼ� / 0�����ַ���</param>
        /// <returns></returns>
        public static DataTable gvInsDataBind(int intYear, int intMonth, string strUserno, string strUsername, int intDept, int intSatus, int intIns)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_GetInsurance";
                SqlParameter[] parmArray = new SqlParameter[8];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Userno", strUserno);
                parmArray[3] = new SqlParameter("@Username", strUsername);
                parmArray[4] = new SqlParameter("@DeptID", intDept);
                parmArray[5] = new SqlParameter("@SatusID", intSatus);
                parmArray[6] = new SqlParameter("@InsID", intIns);
                parmArray[7] = new SqlParameter("@flag", 1);
                //return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
                DataTable dtS = SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
                return dtS;
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// �籣��Ϣ����excel
        /// </summary>
        /// <param name="intYear">���</param>
        /// <param name="intMonth">�·�</param>
        /// <param name="strUserno">����</param>
        /// <param name="strUsername">����</param>
        /// <param name="intDept">����ID</param>
        /// <param name="intSatus">Ա��״̬</param>
        /// <param name="intIns">�籣����</param>
        /// <param name="flag">1�������ݼ� / 0�����ַ���</param>
        /// <returns></returns>
        public string ExportIns(int intYear, int intMonth, string strUserno, string strUsername, int intDept, int intSatus, int intIns)
        {
            try
            {
                string strIns = "";
                string strSql = "sp_hr_GetInsurance";
                SqlParameter[] parmArray = new SqlParameter[8];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Userno", strUserno);
                parmArray[3] = new SqlParameter("@Username", strUsername);
                parmArray[4] = new SqlParameter("@DeptID", intDept);
                parmArray[5] = new SqlParameter("@SatusID", intSatus);
                parmArray[6] = new SqlParameter("@InsID", intIns);
                parmArray[7] = new SqlParameter("@flag", -1);
                strIns = SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).ToString();

                return strIns;
            }
            catch
            {
                return "";
            }
        }

        public static void UpdateIns(decimal hr_accfound_hFound, decimal hr_accfound_mFound, decimal hr_accfound_eFound, decimal hr_accfound_rFound, decimal hr_accfound_Injury, int hr_accfound_id, int intUser)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_UpdateInsurance";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@hr_accfound_hFound", hr_accfound_hFound);
                parmArray[1] = new SqlParameter("@hr_accfound_mFound", hr_accfound_mFound);
                parmArray[2] = new SqlParameter("@hr_accfound_eFound", hr_accfound_eFound);
                parmArray[3] = new SqlParameter("@hr_accfound_rFound", hr_accfound_rFound);
                parmArray[4] = new SqlParameter("@hr_accfound_Injury", hr_accfound_Injury);
                parmArray[5] = new SqlParameter("@hr_accfound_id", hr_accfound_id);
                parmArray[6] = new SqlParameter("@userID", intUser);
                SqlHelper.ExecuteNonQuery(adc.dsnx(), CommandType.StoredProcedure, strSql, parmArray);


            }
            catch
            {

            }
        }

        public int SaveAndUpdateIns(int intUserID, string strUserNo, string strUserName, decimal decHfound, decimal decMfound, decimal decEfound, decimal decRfound, decimal decInjury, int intCreat,
                                     string strDate, string roleID, string deptID, string workshop, string workgroup, string roleTypeID ,string PlantCode)
        {
            try
            {
                string strSql = "sp_hr_SaveInsurance";
                SqlParameter[] parmArray = new SqlParameter[17];
                parmArray[0] = new SqlParameter("@UserID", intUserID);
                parmArray[1] = new SqlParameter("@UserNo", strUserNo);
                parmArray[2] = new SqlParameter("@UserName", strUserName);
                parmArray[3] = new SqlParameter("@hr_accfound_hFound", decHfound);
                parmArray[4] = new SqlParameter("@hr_accfound_mFound", decMfound);
                parmArray[5] = new SqlParameter("@hr_accfound_eFound", decEfound);
                parmArray[6] = new SqlParameter("@hr_accfound_rFound", decRfound);
                parmArray[7] = new SqlParameter("@hr_accfound_Injury", decInjury);
                parmArray[8] = new SqlParameter("@createBy", intCreat);
                parmArray[9] = new SqlParameter("@hr_accfound_date", strDate);
                parmArray[10] = new SqlParameter("@roleID", Convert.ToInt32(roleID));
                parmArray[11] = new SqlParameter("@deptID", Convert.ToInt32(deptID));
                parmArray[12] = new SqlParameter("@workshop", Convert.ToInt32(workshop));
                parmArray[13] = new SqlParameter("@workgroup", Convert.ToInt32(workgroup));
                parmArray[14] = new SqlParameter("@roleTypeID", Convert.ToInt32(roleTypeID));
                parmArray[15] = new SqlParameter("@retValue", DbType.Int32);
                parmArray[15].Direction = ParameterDirection.Output;
                parmArray[16] = new SqlParameter("@PlantCode", Convert.ToInt32(PlantCode));
                
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

                return Convert.ToInt32(parmArray[15].Value);

            }
            catch
            {
                return 0;
            }
        }
        public int DeleteIns(int hr_accfound_id)
        {
            try
            {
                string strSql = "sp_hr_DeleteInsurance";
                SqlParameter parm = new SqlParameter("@hr_accfound_id", hr_accfound_id);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parm);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region ����ά��
        public DataSet GetDept(string plantid)
        {
            try
            {
                string strSql = "SELECT departmentID,name FROM tcpc" + plantid + ".dbo.departments WHERE isSalary='1' AND active='1'";
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetRole(string plantid)
        {
            try
            {
                string strSql = "SELECT RoleID,RoleName FROM tcpc" + plantid + ".dbo.Roles ORDER BY RoleID";
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetAllowance(string plantid, string typeid, string temp, string year, string month, string userno, string username, string dept, string role)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetAllowance";

                SqlParameter[] parmArray = new SqlParameter[9];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@typeid", typeid);
                parmArray[2] = new SqlParameter("@temp", temp);
                parmArray[3] = new SqlParameter("@year", year);
                parmArray[4] = new SqlParameter("@month", month);
                parmArray[5] = new SqlParameter("@userno", userno);
                parmArray[6] = new SqlParameter("@username", username);
                parmArray[7] = new SqlParameter("@dept", dept);
                parmArray[8] = new SqlParameter("@role", role);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                return null;
            }
        }

        public int SaveAllowance(string allowid, string plantid, string typeid, string workdate, string userno, string amount, string comment, string uID, string aid)
        {
            int nRet = -1;

            try
            {
                //string strSql = "tcpc" + plantid + ".dbo.sp_hr_SaveAllowance";
                string strSql = "sp_hr_SaveAllowance";
                SqlParameter[] parmArray = new SqlParameter[10];
                parmArray[0] = new SqlParameter("@allowid", allowid);
                parmArray[1] = new SqlParameter("@plantid", plantid);
                parmArray[2] = new SqlParameter("@typeid", typeid);
                parmArray[3] = new SqlParameter("@workdate", workdate);
                parmArray[4] = new SqlParameter("@userno", userno);
                parmArray[5] = new SqlParameter("@amount", amount);
                parmArray[6] = new SqlParameter("@comment", comment);
                parmArray[7] = new SqlParameter("@uID", uID);
                parmArray[8] = new SqlParameter("@aid", aid);
                parmArray[9] = new SqlParameter("@returnVal", SqlDbType.Int);
                parmArray[9].Direction = ParameterDirection.Output;

                nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                nRet = int.Parse(parmArray[9].Value.ToString());
            }
            catch
            {
                nRet = -1;
            }

            return nRet;
        }

        public bool DeleteAllowance(string allowid, string plantid, int intType)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_DeleteAllowance";

                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@allowid", allowid);
                parmArray[1] = new SqlParameter("@typeID", intType);
                parmArray[2] = new SqlParameter("@returnVal", SqlDbType.Int);

                parmArray[2].Direction = ParameterDirection.Output;

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                if (n > -1)
                    bRet = true;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public string GetUserNameByNo(string plantid, string temp, string userno, string currentdate)
        {
            try
            {
                string strSql = "sp_hr_GetUserNameByNo";

                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@temp", temp);
                parmArray[2] = new SqlParameter("@userno", userno);
                parmArray[3] = new SqlParameter("@currentdate ", currentdate);


                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString().Trim();
            }
            catch
            {
                return "DB-Opt-Err";
            }
        }

        public bool ModifyAllowance(string plantid, string allowid, string amount, string rmks, string strtype)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_ModifyAllowance";

                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@allowid", allowid);
                parmArray[1] = new SqlParameter("@atype", strtype);
                parmArray[2] = new SqlParameter("@amount", amount);
                parmArray[3] = new SqlParameter("@rmks", rmks);


                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                if (n > -1)
                    bRet = true;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        #endregion

        #region Ա�����ݼ�ά��
        public DataSet GetRestLeave(string plantid, string workdate, string temp, string year, string month, string userno, string username, string dept)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetRestLeave";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@workdate", workdate);
                parmArray[1] = new SqlParameter("@temp", temp);
                parmArray[2] = new SqlParameter("@year", year);
                parmArray[3] = new SqlParameter("@month", month);
                parmArray[4] = new SqlParameter("@userno", userno);
                parmArray[5] = new SqlParameter("@username", username);
                parmArray[6] = new SqlParameter("@dept", dept);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetRestLeaves(string plantid, string workdate, string temp, string year, string month, string userno, string username, string dept)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetRestLeaveByMonth";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@workdate", workdate);
                parmArray[1] = new SqlParameter("@temp", temp);
                parmArray[2] = new SqlParameter("@year", year);
                parmArray[3] = new SqlParameter("@month", month);
                parmArray[4] = new SqlParameter("@userno", userno);
                parmArray[5] = new SqlParameter("@username", username);
                parmArray[6] = new SqlParameter("@dept", dept);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// ����Ա����ָ�������������������
        /// </summary>
        /// <param name="plantid"></param>
        /// <param name="uid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public decimal GetRestLeaveDays(string plantid, string uid, DateTime startdate, DateTime enddate)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetRestLeaveDays";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@uid", uid);
                parmArray[1] = new SqlParameter("@startdate", startdate);
                parmArray[2] = new SqlParameter("@enddate", enddate);

                return decimal.Parse(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString());
            }
            catch
            {
                return 0;
            }
        }

        public Int32 SaveRestLeave(string plantid, string uid, DateTime workdate, Decimal days, decimal workyears, decimal restdays, int createby)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_SaveRestLeave";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@uid", uid);
                parmArray[1] = new SqlParameter("@workdate", workdate);
                parmArray[2] = new SqlParameter("@days", days);
                parmArray[3] = new SqlParameter("@workyears", workyears);
                parmArray[4] = new SqlParameter("@restdays", restdays);
                parmArray[5] = new SqlParameter("@createby", createby);
                parmArray[6] = new SqlParameter("@retValue", SqlDbType.Int);
                parmArray[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString();

                return Convert.ToInt32(parmArray[6].Value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public bool DeleteRestLeave(string id, string plantid)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_DeleteRestLeave";

                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@id", id);
                parmArray[1] = new SqlParameter("@returnVal", SqlDbType.Int);
                parmArray[1].Direction = ParameterDirection.Output;

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                if (int.Parse(parmArray[1].Value.ToString()) == 1)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public string GetUserIDByNo(string plantid, string temp, string userno)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetUserIDByNo";

                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@temp", temp);
                parmArray[2] = new SqlParameter("@userno", userno);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetUserEnterDateByNo(string plantid, string temp, string userno)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetUserEnterDateByNo";

                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@temp", temp);
                parmArray[2] = new SqlParameter("@userno", userno);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetUserDeptByNo(string plantid, string temp, string userno)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetUserDeptByNo";

                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@temp", temp);
                parmArray[2] = new SqlParameter("@userno", userno);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// ����ָ���ڼ�ĳԱ���벡��������������ټ����У�����ĳԱ����һ��Ĳ�������
        /// </summary>
        /// <param name="strSdate">���������ݵ���һ�����գ���2013-1-1ǰ��2012-1-1</param>
        /// <param name="strEdate">���������ݵ���һ����ĩ����2013-1-1ǰ��2012-12-31</param>
        /// <param name="intPlant"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public decimal Leavedays(string strSdate, string strEdate, int intPlant, int userID)
        {
            try
            {
                string str = "sp_hr_RLeaveday";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Sdate", strSdate);
                parmArray[1] = new SqlParameter("@Edate", strEdate);
                parmArray[2] = new SqlParameter("@userID", userID);
                parmArray[3] = new SqlParameter("@plantcode", intPlant);
                return Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));

            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// δ���ǲ��٣�ֻ�������Ա����������������Ӧ���е��������
        /// </summary>
        /// <param name="dtEnter"></param>
        /// <param name="dtWork"></param>
        /// <returns></returns>
        public decimal Restday(DateTime dtEnter, DateTime dtWork)
        {
            decimal decDay;
            decDay = 0;
            DateTime dtadjust;

            int intValue, intYear;
            intValue = (dtWork.Year - dtEnter.Year) * 12 + dtWork.Month - dtEnter.Month;
            intYear = dtWork.Year - dtEnter.Year;
            /*
             * Marked By Shan Zhiming 2013-12-13
             * ����ְ����ٵ��죬δ����12���µ�
             * ע�⣺�����ٵ��ա��족С����ְ�յġ��족�Ļ���Ҳ��������12����
             */
            if (dtWork.Day < dtEnter.Day && intValue >= 1 && intValue <= 12)
                intValue = intValue - 1;

            TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(dtWork.Year.ToString() + "-01-01").Ticks);
            //TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(dtWork.Year.ToString() + "-12-31").Ticks);
            TimeSpan ts2 = new TimeSpan(Convert.ToDateTime((dtWork.Year + 1).ToString() + "-01-01").Ticks);
            TimeSpan ts3;
            //����ְ��ʱ���������£�����2013-3-1��ְ�������2014-6-1����˱���Ϊ2014-3-31
            dtadjust = Convert.ToDateTime(dtWork.Year.ToString() + "-" + dtEnter.Month.ToString() + "-01").AddMonths(1).AddDays(-1);
            //Ҳ����˵�����������ĩ��ְ�ģ�
            if (dtadjust.Day >= dtEnter.Day)
                ts3 = new TimeSpan(Convert.ToDateTime(dtWork.Year.ToString() + "-" + dtEnter.Month.ToString() + "-" + dtEnter.Day.ToString()).Ticks);
            else
                ts3 = new TimeSpan(Convert.ToDateTime(dtWork.Year.ToString() + "-" + dtEnter.Month.ToString() + "-" + dtadjust.Day.ToString()).Ticks);

            TimeSpan tsB = ts1.Subtract(ts3).Duration();
            TimeSpan tsR = ts2.Subtract(ts3).Duration();

            //���dtWork��dtEnterͬ�꣬��decRemainΪ��������Ȼ��ͨ��HR�����Ƴ�һ��ά����ٵ�
            //decbefor��Ϊʣ������������Ϊ����ĳ���
            decimal decRemain, decbefor;
            decRemain = Convert.ToDecimal(tsR.Days);
            decbefor = Convert.ToDecimal(tsB.Days);

            if (intValue >= 12)
            {
                if (intYear < 10) //before 10 years 
                {
                    if (intValue < 25 - dtEnter.Month)
                        decDay = Math.Truncate(decRemain / 365.0M * 5.0M);
                    else
                        decDay = 5;
                }
                else
                {
                    if (intYear < 20)  //befor 20 years
                    {
                        if (intValue < 133 - dtEnter.Month)
                            decDay = Math.Truncate(decbefor / 365.0M * 5.0M + decRemain / 365.0M * 10.0M);
                        else
                            decDay = 10;
                    }
                    else
                    {
                        if (intValue < 253 - dtEnter.Month)
                            decDay = Math.Truncate(decbefor / 365.0M * 10.0M + decRemain / 365.0M * 15.0M);
                        else
                            decDay = 15;
                    }
                }
            }
            //2009-03-19   2010-12-12

            return decDay;

        }

        public bool DeleteRestLeaveTemp(string Uid, string plantid)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc0" + ".dbo.sp_hr_DeleteRestLeaveTemp";

                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@Uid", Uid);
                parmArray[1] = new SqlParameter("@returnVal", SqlDbType.Int);
                parmArray[1].Direction = ParameterDirection.Output;

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                if (int.Parse(parmArray[1].Value.ToString()) == 1)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public bool CheckRestLeaveTempErr(string Uid)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc0" + ".dbo.sp_hr_CheckRestLeaveTempErr";

                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@Uid", Uid);
                parmArray[1] = new SqlParameter("@returnVal", SqlDbType.Int);
                parmArray[1].Direction = ParameterDirection.Output;

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                if (int.Parse(parmArray[1].Value.ToString()) == 1)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public bool CheckRestLeaveIsExists(int strUID, string workdate, int Uid, string plantid)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_CheckRestLeaveIsExists";

                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@strUID", strUID);
                parmArray[1] = new SqlParameter("@workdate", workdate);
                parmArray[2] = new SqlParameter("@createby", Uid);
                parmArray[3] = new SqlParameter("@retValue", SqlDbType.Int);
                parmArray[3].Direction = ParameterDirection.Output;

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                if (int.Parse(parmArray[3].Value.ToString()) == 1)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }
        public Int32 SaveRestLeaveTemp(int createby, string plantid)
        {
            try
            {
                string strSql = "tcpc" + plantid  + ".dbo.sp_hr_SaveRestLeaveTemp";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@createby", createby);
                parmArray[1] = new SqlParameter("@retValue", SqlDbType.Int);
                parmArray[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString();

                return Convert.ToInt32(parmArray[1].Value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public Int32 RestLeaveTempTotal(int creatby, string plantid)
        {
            try
            {
                string strSql = "tcpc0" + ".dbo.sp_hr_RestLeaveTempTotal";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@creatby", creatby);
                parmArray[1] = new SqlParameter("@retValue", SqlDbType.Int);
                parmArray[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString();

                return Convert.ToInt32(parmArray[1].Value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public DataTable RestLeaveTempCheckTotal(int creatby, string plantid)
        {
            try
            {
                string strSql = "tcpc0" + ".dbo.sp_hr_RestLeaveTempCheckTotal";
                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@creatby", creatby);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region �ۿ�ά��

        public DataSet GetType(string plantid)
        {
            try
            {
                string strSql = "Select systemCodeID,systemCodeName From tcpc0.dbo.systemCode INNER JOIN";
                strSql += " tcpc0.dbo.systemCodeType on tcpc0.dbo.systemCodeType.systemCodeTypeID = tcpc0.dbo.systemCode.systemCodeTypeID ";
                strSql += " Where tcpc0.dbo.systemCodeType.systemCodeTypeName = 'Deduct Money Type' ";
                strSql += " Order By systemCodeID ";

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetMoneyType(string plantid, string typeid)
        {
            try
            {
                string strSql = "SELECT * FROM tcpc" + plantid + ".dbo.DeductMoneyType WHERE deductTypeID = " + typeid;

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetDeduct(string plantid, int temp, int status, int role, int uID, int conceal, string startdate, string enddate, int dept, string userno, string username, int type)
        {
            //DataSet ds;
            //int i;
            DateTime dtGetDate = Convert.ToDateTime(startdate);

            string rr = dtGetDate.Year.ToString() + "-" + dtGetDate.Month.ToString() + "-1";
            HR hrdeduct = new HR();
            string strSql = hrdeduct.ExportDeduct(plantid, temp, status, role, uID, conceal, startdate, enddate, dept, userno, username, type, rr);
            //string strSql = " SELECT d.ID,d.UserCode,u.UserNo,u.UserName,ISNULL(dp.name,''),ISNULL(w.Name,''),d.WorkDate,d.Comments,d.DeductNum,d.Amount,s.SystemCodeName,u1.UserName AS createName,CreatedDate, s1.SystemCodeName,ISNULL(u.fax,'') ";
            //strSql += " FROM tcpc" + plantid + ".dbo.hr_Deduct d INNER JOIN tcpc0.dbo.users u ON u.UserID = d.UserCode ";

            //if (status != 2)
            //    strSql += " INNER JOIN tcpc" + plantid + ".dbo.DeductMoneyType dt ON dt.id= d.deductMoneyTypeID AND dt.status=" + status.ToString();

            //strSql += " LEFT OUTER JOIN  users u1 ON u1.userID=d.createdBy ";
            //strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Departments dp ON dp.departmentID=u.departmentID ";
            //strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Workshop w ON w.id=u.workshopID ";
            //strSql += " LEFT OUTER JOIN (select w.userID,w.worktypeID From tcpc" + plantid + ".dbo.WorktypeChange w inner join (select userid,min(changedate) as changedate From tcpc" + plantid + ".dbo.WorktypeChange WHERE changedate>='" + rr + "' GROUP BY userID) sd  ON sd.userID=w.userID AND sd.changedate=w.changedate) wt ON wt.userID=u.userID ";
            //strSql += " INNER JOIN systemCode s ON s.systemCodeID=CASE WHEN wt.workTypeID is null THEN u.workTypeID ELSE wt.workTypeID END  ";
            //strSql += " LEFT OUTER JOIN systemCode s1 ON s1.systemCodeID=d.typeID ";

            //if (role == 1)
            //{
            //    if (enddate == string.Empty)
            //        strSql += " WHERE DATEPART(MONTH,d.WorkDate)= '" + Convert.ToDateTime(startdate).Month + "'AND YEAR(d.WorkDate)='" + Convert.ToDateTime(startdate).Year + "'";
            //    else
            //        strSql += " WHERE d.WorkDate>='" + startdate + "' AND d.WorkDate<='" + enddate + "'";

            //    if (conceal == 0)
            //        strSql += " AND u.isTemp='" + temp.ToString() + "' ";


            //    if (dept > 0)
            //        strSql += " AND u.DepartmentID = " + dept.ToString();

            //    if (userno != string.Empty)
            //        strSql += " AND CAST(u.UserNo AS VARCHAR) ='" + userno + "'";

            //    if (username != string.Empty)
            //        strSql += " AND LOWER(u.UserName) like N'%" + username.ToLower() + "%'";

            //    if (type > 0)
            //        strSql += " AND d.TypeID=" + type.ToString();

            //    strSql += " AND u.PlantCode='" + plantid + "' ";

            //}
            //else
            //{
            //    strSql += " INNER JOIN tcpc" + plantid + ".dbo.Manager_Worker m ON m.Worker=d.CreatedBy AND m.Manager= " + uID.ToString();

            //    if (enddate == string.Empty)
            //        strSql += " WHERE DATEPART(MONTH,d.WorkDate) = '" + Convert.ToDateTime(startdate).Month + "'AND YEAR(d.WorkDate)='" + Convert.ToDateTime(startdate).Year + "'";
            //    else
            //        strSql += " WHERE d.WorkDate>='" + startdate + "' and d.WorkDate<='" + enddate + "'";

            //    if (conceal == 0)
            //        strSql += " AND u.IsTemp='" + temp + "' ";


            //    if (dept > 0)
            //        strSql += " AND u.DepartmentID = " + dept.ToString();

            //    if (userno != string.Empty)
            //        strSql += " AND CAST(u.UserNo AS VARCHAR) ='" + userno + "'";

            //    if (username != string.Empty)
            //        strSql += " AND LOWER(u.UserName) LIKE N'%" + username.ToLower() + "%'";

            //    if (type > 0)
            //        strSql += " AND d.TypeID=" + type.ToString();

            //    strSql += " AND u.PlantCode='" + plantid + "' ";

            //    strSql += " UNION ";
            //    strSql += " SELECT d.ID,d.UserCode,u.UserNo,u.UserName,ISNULL(dp.Name,''),ISNULL(w.Name,''),d.WorkDate,d.Comments,d.DeductNum,d.Amount,s.SystemCodeName,u1.UserName AS CreateName,CreatedDate,s1.SystemCodeName,ISNULL(u.fax,'')  ";

            //    strSql += " FROM tcpc" + plantid + ".dbo.hr_Deduct d INNER JOIN tcpc0.dbo.Users u ON u.UserID = d.UserCode ";

            //    if (status != 2)
            //        strSql += " INNER JOIN tcpc" + plantid + ".dbo.DeductMoneyType dt ON dt.ID= d.DeductMoneyTypeID AND dt.Status=" + status.ToString();

            //    strSql += " LEFT OUTER JOIN  tcpc0.dbo.users u1 on u1.userID=d.createdBy ";
            //    strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Departments dp on dp.departmentID=u.departmentID ";
            //    strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Workshop w on w.id=u.workshopID ";
            //    strSql += " LEFT OUTER JOIN (SELECT w.UserID,w.UorktypeID FROM tcpc" + plantid + ".dbo.WorktypeChange w INNER JOIN (SELECT Sserid,MIN(ChangeDate) AS changedate FROM tcpc" + plantid + ".dbo.WorktypeChange WHERE ChangeDate>='" + rr + "' GROUP BY UserID) sd  ON sd.UserID=w.UserID AND sd.ChangeDate=w.ChangeDate) wt ON wt.UserID=u.UserID ";
            //    strSql += " INNER JOIN tcpc0.dbo.SystemCode s ON s.SystemCodeID=CASE WHEN wt.WorkTypeID IS NULL THEN u.WorkTypeID ELSE wt.workTypeID END  ";
            //    strSql += " LEFT OUTER JOIN tcpc0.dbo.systemCode s1 ON s1.SystemCodeID=d.TypeID ";

            //    strSql += " WHERE  d.CreatedBy = " + uID.ToString();
            //    if (enddate == string.Empty)
            //        strSql += " AND DATEPART(MONTH,d.WorkDate)= '" + Convert.ToDateTime(startdate).Month + "'AND YEAR(d.WorkDate)='" + Convert.ToDateTime(startdate).Year + "'";
            //    else
            //        strSql += " AND d.WorkDate>='" + startdate + "' AND d.WorkDate<='" + enddate + "'";

            //    if (conceal == 0)
            //        strSql += " AND u.IsTemp = '" + temp + "' ";

            //    if (dept > 0)
            //        strSql += " AND u.DepartmentID = " + dept.ToString();

            //    if (userno != string.Empty)
            //        strSql += " AND CAST(u.UserNo AS VARCHAR) ='" + userno + "'";

            //    if (username != string.Empty)
            //        strSql += " AND LOWER(u.UserName) LIKE N'%" + username.ToLower() + "%'";

            //    if (type > 0)
            //        strSql += " AND d.TypeID=" + type.ToString();

            //    strSql += " AND u.plantCode='" + plantid + "' ";
            //}

            //strSql += " ORDER BY d.ID DESC ";

            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql);
            }
            catch
            {
                return null;
            }
        }

        public string ExportDeduct(string plantid, int temp, int status, int role, int uID, int conceal, string startdate, string enddate, int dept, string userno, string username, int type, string rr)
        {
            string strSql = " SELECT d.ID,d.UserCode,u.UserNo,u.UserName,ISNULL(dp.name,''),ISNULL(w.Name,''),d.WorkDate,d.Comments,d.DeductNum,d.Amount,s.SystemCodeName,u1.UserName AS createName,CreatedDate, s1.SystemCodeName as deductname,ISNULL(dt.name,'') as deductType,ISNULL(u.fax,'') ";
            strSql += " FROM tcpc" + plantid + ".dbo.hr_Deduct d INNER JOIN tcpc0.dbo.users u ON u.UserID = d.UserCode ";

            // if (status != 2)
            strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.DeductMoneyType dt ON dt.id= d.deductMoneyTypeID ";
            //AND dt.status=" + status.ToString();

            strSql += " LEFT OUTER JOIN  tcpc0.dbo.users u1 ON u1.userID=d.createdBy ";
            strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Departments dp ON dp.departmentID=u.departmentID ";
            strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Workshop w ON w.id=u.workshopID ";
            strSql += " LEFT OUTER JOIN (select w.userID,w.worktypeID From tcpc" + plantid + ".dbo.WorktypeChange w inner join (select userid,min(changedate) as changedate From tcpc" + plantid + ".dbo.WorktypeChange WHERE changedate>='" + rr + "' GROUP BY userID) sd  ON sd.userID=w.userID AND sd.changedate=w.changedate) wt ON wt.userID=u.userID ";
            strSql += " INNER JOIN  tcpc0.dbo.systemCode s ON s.systemCodeID=CASE WHEN wt.workTypeID is null THEN u.workTypeID ELSE wt.workTypeID END  ";
            strSql += " LEFT OUTER JOIN  tcpc0.dbo.systemCode s1 ON s1.systemCodeID=d.typeID ";

            if (role == 1)
            {
                if (enddate == string.Empty)
                    strSql += " WHERE DATEPART(MONTH,d.WorkDate)= '" + Convert.ToDateTime(startdate).Month + "'AND YEAR(d.WorkDate)='" + Convert.ToDateTime(startdate).Year + "'";
                else
                    strSql += " WHERE d.WorkDate>='" + startdate + "' AND d.WorkDate<='" + enddate + "'";

                //if (conceal == 0)
                //    strSql += " AND u.isTemp='" + temp.ToString() + "' ";


                if (dept > 0)
                    strSql += " AND u.DepartmentID = " + dept.ToString();

                if (userno != string.Empty)
                    strSql += " AND CAST(u.UserNo AS VARCHAR) ='" + userno + "'";

                if (username != string.Empty)
                    strSql += " AND LOWER(u.UserName) like N'%" + username.ToLower() + "%'";

                if (type > 0)
                    strSql += " AND d.TypeID=" + type.ToString();

                strSql += " AND u.PlantCode='" + plantid + "' ";

            }
            else
            {
                strSql += " INNER JOIN tcpc" + plantid + ".dbo.Manager_Worker m ON m.Worker=d.CreatedBy AND m.Manager= " + uID.ToString();

                if (enddate == string.Empty)
                    strSql += " WHERE DATEPART(MONTH,d.WorkDate) = '" + Convert.ToDateTime(startdate).Month + "'AND YEAR(d.WorkDate)='" + Convert.ToDateTime(startdate).Year + "'";
                else
                    strSql += " WHERE d.WorkDate>='" + startdate + "' and d.WorkDate<='" + enddate + "'";

                //if (conceal == 0)
                //    strSql += " AND u.IsTemp='" + temp + "' ";


                if (dept > 0)
                    strSql += " AND u.DepartmentID = " + dept.ToString();

                if (userno != string.Empty)
                    strSql += " AND CAST(u.UserNo AS VARCHAR) ='" + userno + "'";

                if (username != string.Empty)
                    strSql += " AND LOWER(u.UserName) LIKE N'%" + username.ToLower() + "%'";

                if (type > 0)
                    strSql += " AND d.TypeID=" + type.ToString();

                strSql += " AND u.PlantCode='" + plantid + "' ";

                strSql += " UNION ";
                strSql += " SELECT d.ID,d.UserCode,u.UserNo,u.UserName,ISNULL(dp.Name,''),ISNULL(w.Name,''),d.WorkDate,d.Comments,d.DeductNum,d.Amount,s.SystemCodeName,u1.UserName AS CreateName,CreatedDate,s1.SystemCodeName as deductname,ISNULL(dt.name,'') as deductType,ISNULL(u.fax,'')  ";

                strSql += " FROM tcpc" + plantid + ".dbo.hr_Deduct d INNER JOIN tcpc0.dbo.Users u ON u.UserID = d.UserCode ";

                // if (status != 2)
                strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.DeductMoneyType dt ON dt.ID= d.DeductMoneyTypeID ";  //AND dt.Status=" + status.ToString();

                strSql += " LEFT OUTER JOIN  tcpc0.dbo.users u1 on u1.userID=d.createdBy ";
                strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Departments dp on dp.departmentID=u.departmentID ";
                strSql += " LEFT OUTER JOIN tcpc" + plantid + ".dbo.Workshop w on w.id=u.workshopID ";
                strSql += " LEFT OUTER JOIN (select w.userID,w.worktypeID From tcpc" + plantid + ".dbo.WorktypeChange w inner join (select userid,min(changedate) as changedate From tcpc" + plantid + ".dbo.WorktypeChange WHERE changedate>='" + rr + "' GROUP BY userID) sd  ON sd.userID=w.userID AND sd.changedate=w.changedate) wt ON wt.userID=u.userID ";
                strSql += " INNER JOIN  tcpc0.dbo.systemCode s ON s.systemCodeID=CASE WHEN wt.workTypeID is null THEN u.workTypeID ELSE wt.workTypeID END  ";
                strSql += " LEFT OUTER JOIN  tcpc0.dbo.systemCode s1 ON s1.systemCodeID=d.typeID ";

                if (enddate == string.Empty)
                    strSql += " WHERE DATEPART(MONTH,d.WorkDate)= '" + Convert.ToDateTime(startdate).Month + "'AND YEAR(d.WorkDate)='" + Convert.ToDateTime(startdate).Year + "'";
                else
                    strSql += " WHERE d.WorkDate>='" + startdate + "' AND d.WorkDate<='" + enddate + "'";

                //if (conceal == 0)
                //    strSql += " AND u.IsTemp = '" + temp + "' ";

                if (dept > 0)
                    strSql += " AND u.DepartmentID = " + dept.ToString();

                if (userno != string.Empty)
                    strSql += " AND CAST(u.UserNo AS VARCHAR) ='" + userno + "'";

                if (username != string.Empty)
                    strSql += " AND LOWER(u.UserName) LIKE N'%" + username.ToLower() + "%'";

                if (type > 0)
                    strSql += " AND d.TypeID=" + type.ToString();

                strSql += " AND u.plantCode='" + plantid + "' ";
            }

            strSql += " ORDER BY d.ID DESC ";

            return strSql;
        }

        public bool SaveDeduct(string plantid, string workdate, string userno, string typeid, string moneyid, string amount, string comment, string uID)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_SaveDeduct";

                SqlParameter[] parmArray = new SqlParameter[8];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@workdate", workdate);
                parmArray[2] = new SqlParameter("@userno", userno);
                parmArray[3] = new SqlParameter("@typeid", typeid);
                parmArray[4] = new SqlParameter("@moneyid", moneyid);
                parmArray[5] = new SqlParameter("@amount", amount);
                parmArray[6] = new SqlParameter("@comment", comment);
                parmArray[7] = new SqlParameter("@uID", uID);


                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                if (n > -1)
                    bRet = true;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public bool DeleteDeduct(string plantid, string id)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_DeleteDeduct";

                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@id", id);

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                if (n > -1)
                    bRet = true;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        #endregion

        #region ������Ա��Ӧ����ά��

        public DataSet GetProbationer(string plantid, string temp, string year, string month, string userno, string username, string dept, string enterdate)
        {
            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_GetProbationer";

                SqlParameter[] parmArray = new SqlParameter[8];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@temp", temp);
                parmArray[2] = new SqlParameter("@year", year);
                parmArray[3] = new SqlParameter("@month", month);
                parmArray[4] = new SqlParameter("@userno", userno);
                parmArray[5] = new SqlParameter("@username", username);
                parmArray[6] = new SqlParameter("@enterdate", enterdate);
                parmArray[7] = new SqlParameter("@dept", dept);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                return null;
            }
        }

        public bool SaveProbationer(string plantid, string uID, string enterdate, string userno, string year, string month, string attendence)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_SaveProbationer";

                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@plantid", plantid);
                parmArray[1] = new SqlParameter("@uID", uID);
                parmArray[2] = new SqlParameter("@enterdate", enterdate);
                parmArray[3] = new SqlParameter("@userno", userno);
                parmArray[4] = new SqlParameter("@year", year);
                parmArray[5] = new SqlParameter("@month", month);
                parmArray[6] = new SqlParameter("@attendence", attendence);

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

                if (n > -1)
                    bRet = true;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public bool DeleteProbationer(string plantid, string id)
        {
            bool bRet = false;

            try
            {
                string strSql = "tcpc" + plantid + ".dbo.sp_hr_DeleteProbationer";

                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@id", id);
                parmArray[1] = new SqlParameter("@returnVal", SqlDbType.Int);
                parmArray[1].Direction = ParameterDirection.Output;

                int n = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                bRet = Convert.ToBoolean(n);
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        #endregion

        #region Attendance For Time
        public static DataTable SelectTimeAttendance(int intYear, int intMonth, string strUser, string strName, int intDept, int PlantCode, int intUserID, bool bolAll, int intDays)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                string strSql = hr_salary.Attendance(intYear, intMonth, strUser, strName, intDept, PlantCode, intUserID, bolAll, intDays, 0);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public string Attendance(int intYear, int intMonth, string strUser, string strName, int intDept, int PlantCode, int intUserID, bool bolAll, int intDays, int intType)
        {
            try
            {
                string strSql = "sp_hr_AttendanceSelect";
                SqlParameter[] parmArray = new SqlParameter[10];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@Depart", intDept);
                parmArray[5] = new SqlParameter("@PlantCode", PlantCode);
                parmArray[6] = new SqlParameter("@UserID", intUserID);
                parmArray[7] = new SqlParameter("@bolAll", bolAll ? 1 : 0);
                parmArray[8] = new SqlParameter("@Days", intDays);
                parmArray[9] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

            }
            catch
            {
                return null;
            }
        }

        public int SaveAttendance(string strDate, int intDept, string strUser, string strName, int intInputUser, int PlantCode, int intUserID, string strStart, string strEnd, decimal decRest, decimal decTotal, decimal decDays, int intMid, int intNight, int intWhole
                                  , string strS1, string strE1, decimal decRest1, string strS2, string strE2, decimal decRest2, decimal decAmount)
        {
            try
            {

                string strSql = "sp_hr_AttendanceSaveData";
                SqlParameter[] parmArray = new SqlParameter[22];
                parmArray[0] = new SqlParameter("@AttendanceDate", strDate);
                parmArray[1] = new SqlParameter("@Depart", intDept);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@intInputUser", intInputUser);
                parmArray[5] = new SqlParameter("@PlantCode", PlantCode);
                parmArray[6] = new SqlParameter("@intUserID", intUserID);
                parmArray[7] = new SqlParameter("@StartTime", strStart);
                parmArray[8] = new SqlParameter("@EndTime", strEnd);
                parmArray[9] = new SqlParameter("@Rest", decRest);
                parmArray[10] = new SqlParameter("@Total", decTotal);
                parmArray[11] = new SqlParameter("@Days", decDays);
                parmArray[12] = new SqlParameter("@Mid", intMid);
                parmArray[13] = new SqlParameter("@Night", intNight);
                parmArray[14] = new SqlParameter("@Whole", intWhole);
                parmArray[15] = new SqlParameter("@S1", strS1);
                parmArray[16] = new SqlParameter("@E1", strE1);
                parmArray[17] = new SqlParameter("@R1", decRest1);
                parmArray[18] = new SqlParameter("@S2", strS2);
                parmArray[19] = new SqlParameter("@E2", strE2);
                parmArray[20] = new SqlParameter("@R2", decRest2);
                parmArray[21] = new SqlParameter("@amount", decAmount);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return -1;
            }
        }

        public int DelAttendanc(int hr_attendance_id, int Plantcode)
        {
            try
            {
                string strSql = "sp_hr_AttendanceDelData";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@hr_attendance_id", hr_attendance_id);
                parmArray[1] = new SqlParameter("@Plantcode", Plantcode);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return -1;
            }
        }

        public string CheckTimeUser(string strUser, int Plantcode, string strDate)
        {
            try
            {
                string strSql = "sp_hr_CheckTimeUser";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@userNo", strUser);
                parmArray[1] = new SqlParameter("@sDate", strDate);
                parmArray[2] = new SqlParameter("@Plantcode", Plantcode);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

            }
            catch
            {
                return "";
            }
        }


        public string AttendanceStatistic(int intYear, int intMonth, string strUser, string strName, int intDept, int PlantCode, int intUserID, bool bolAll, int intDayStart, int intDayEnd, int intType)
        {
            try
            {
                string strSql = "sp_hr_AttendanceStatisticSelect";
                SqlParameter[] parmArray = new SqlParameter[11];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@Depart", intDept);
                parmArray[5] = new SqlParameter("@PlantCode", PlantCode);
                parmArray[6] = new SqlParameter("@UserID", intUserID);
                parmArray[7] = new SqlParameter("@bolAll", bolAll ? 1 : 0);
                parmArray[8] = new SqlParameter("@Daystart", intDayStart);
                parmArray[9] = new SqlParameter("@Dayend", intDayEnd);
                parmArray[10] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }

        public static DataTable AttendanceStatisticSelect(int intYear, int intMonth, string strUser, string strName, int intDept, int PlantCode, int intUserID, bool bolAll, int intDayStart, int intDayEnd, int intType)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                string strSql = hr_salary.AttendanceStatistic(intYear, intMonth, strUser, strName, intDept, PlantCode, intUserID, bolAll, intDayStart, intDayEnd, 0);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Salary
        public static DataTable SelectSalary(int intYear, int intMonth, string strUser, string strName, int intDept, int PlantCode)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "tcpc" + PlantCode + ".dbo.sp_hr_SelectSalary";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@Depart", intDept);

                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;

            }
        }

        public void DeleteSalaryData(int intYear, int intMonth)
        {
            try
            {
                string strSql = "sp_hr_DeleteSalary";
                SqlParameter[] parmArray = new SqlParameter[2];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {

            }

        }

        public void BackupSalaryDate(int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_BackupSalary";
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

        public void SaveSalaryData(HR_Salary hsy, string strDate, Int32 intUserID)
        {
            try
            {

                string strSql = "sp_hr_SaveSalary";
                SqlParameter[] parmArray = new SqlParameter[85];
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

                parmArray[71] = new SqlParameter("@hr_Salary_tiaoxiu", hsy.bt_TiaoXiu);
                parmArray[72] = new SqlParameter("@hr_Salary_chaoe", hsy.bt_ChoaE);
                parmArray[73] = new SqlParameter("@hr_Salary_MutualFunds", hsy.s_MutualFunds);

                parmArray[74] = new SqlParameter("@hr_Salary_AllMobile", hsy.s_AllMobile);
                parmArray[75] = new SqlParameter("@hr_Salary_AllKilometer", hsy.s_AllKilometer);
                parmArray[76] = new SqlParameter("@hr_Salary_AllBox", hsy.s_AllBox);
                parmArray[77] = new SqlParameter("@hr_Salary_AllBusiness", hsy.s_AllBusiness);
                parmArray[78] = new SqlParameter("@hr_Salary_AllOnDuty", hsy.s_AllOnDuty);
                parmArray[79] = new SqlParameter("@hr_Salary_AllPost", hsy.s_AllPost);

                parmArray[80] = new SqlParameter("@hr_Salary_FeeElectricity", hsy.f_Electricity);
                parmArray[81] = new SqlParameter("@hr_Salary_FeeWater", hsy.f_Water);
                parmArray[82] = new SqlParameter("@hr_Salary_Dormitory", hsy.f_Dormitory);
                parmArray[83] = new SqlParameter("@hr_Salary_newCost", hsy.bt_NewCost);
                parmArray[84] = new SqlParameter("@hr_Salary_OtherDay", hsy.s_Otherday);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

            }
            catch
            {

            }

        }

        public string ExportSalary(int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_ExportSalary";
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

        public int WorkOrderCheck(int intYear, int intMonth, int intPlantCode)
        {
            try
            {
                string str = "sp_hr_CheckWorkOrder";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlantCode);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));
            }
            catch
            {
                return -1;
            }
        }
        public int CalCheckCount(int intYear, int intMonth, int intPlantCode, Int32 intUserID)
        {
            try
            {
                string str = "sp_hr_CheckErrorCount";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@userID", intUserID);
                parmArray[3] = new SqlParameter("@PlantCode", intPlantCode);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));

            }
            catch
            {
                return -1;
            }
        }
        private int CheckQadData(string strOrder, string strID, string strSite, int intPlantCode, string strConnect, string strRouting, decimal decCost, int intUser)
        {
            try
            {
                string strSql;
                string strdomain;
                strSql = "SELECT realdomain FROM Domain_Mes WHERE qad_site ='" + strSite + "' ";
                strdomain = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSql));
                OdbcConnection conn = new OdbcConnection(strConnect);
                conn.Open();
                strSql = "SELECT wo_qty_comp,wo_close_date,wo_qty_ord FROM PUB.wo_mstr WHERE wo_domain='" + strdomain + "'";
                strSql = strSql + " AND wo_site='" + strSite + "' AND wo_nbr='" + strOrder + "' AND wo_lot='" + strID + "' ";
                OdbcCommand comm = new OdbcCommand(strSql, conn);

                OdbcDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string strQuery;
                    string strRo_tool;
                    strQuery = "SELECT ISNULL(ro_tool,0) FROM QAD_Data.dbo.ro_det WHERE ro_routing = '" + strRouting + "' AND ro_domain = '" + strdomain + "' ";
                    strRo_tool = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.Text, strQuery));
                    if (strRo_tool.Trim().Length == 0)
                        strRo_tool = "0";
                    //wo_close_date IS NULL , the work order don't settled ,get the order quantity to compare the Qad cost
                    decimal decQty, decAll, decValue;

                    if (Convert.IsDBNull(dr.GetValue(1)))
                        decQty = Convert.ToDecimal(dr.GetValue(2));
                    else
                    {
                        decQty = Convert.ToDecimal(dr.GetValue(0));
                        strQuery = "SELECT SUM(ISNULL(w.wocd_cost,0) + ISNULL(b.wocd_cost,0)/5.37)  FROM wo_cost_detail  w LEFT OUTER JOIN wo_cost_detail_bk b ON b.wocd_nbr =w.wocd_nbr AND b.wocd_id =w.wocd_id AND b.wocd_type IS NULL ";
                        strQuery = strQuery + " WHERE w.wocd_nbr ='" + strOrder + "' AND w.wocd_id ='" + strID + "'AND w.wocd_type IS NULL GROUP BY w.wocd_nbr ";
                        decAll = Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.Text, strQuery));
                        decCost = decAll;
                    }
                    decValue = decQty * Convert.ToDecimal(strRo_tool) * 1.14M;
                    if (decValue < decCost)
                    {
                        SaveCheckData(strOrder, strID, strSite, intPlantCode, false, decValue - decCost, Convert.IsDBNull(dr.GetValue(1)) ? "" : Convert.ToDateTime(dr.GetValue(1)).ToShortDateString(), intUser, decValue, decCost);
                    }
                    else
                    {
                        SaveCheckData(strOrder, strID, strSite, intPlantCode, true, decValue - decCost, Convert.IsDBNull(dr.GetValue(1)) ? "" : Convert.ToDateTime(dr.GetValue(1)).ToShortDateString(), intUser, decValue, decCost);
                    }

                }
                dr.Close();
                conn.Close();

                comm.Dispose();
                conn.Dispose();
                return 1;

            }
            catch
            {
                return 0;
            }
        }

        public void SaveCheckData(string strOrder, string strID, string strSite, int intPlantCode, bool blflag, decimal decValue, string strdate, int intUser, decimal decWcost, decimal decTcost)
        {
            try
            {
                String str = "sp_hr_SaveCheck";
                SqlParameter[] parmArray = new SqlParameter[10];
                parmArray[0] = new SqlParameter("@Order", strOrder);
                parmArray[1] = new SqlParameter("@ID", strID);
                parmArray[2] = new SqlParameter("@Site", strSite);
                parmArray[3] = new SqlParameter("@PlantCode", intPlantCode);
                parmArray[4] = new SqlParameter("@flag", blflag ? 1 : 0);
                parmArray[5] = new SqlParameter("@Value", decValue);
                parmArray[6] = new SqlParameter("@sdate", strdate);
                parmArray[7] = new SqlParameter("@userID", intUser);
                parmArray[8] = new SqlParameter("@wcost", decWcost);
                parmArray[9] = new SqlParameter("@tcost", decTcost);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, parmArray);
            }
            catch
            {

            }

        }

        public int CheckSalaryValidity(int intYear, int intMonth, int intPlantCode, Int32 intUserID, string strConnect)
        {
            try
            {
                //SqlDataReader reader;
                String str = "sp_hr_CheckSalaryValidity1";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlantCode);
                parmArray[3] = new SqlParameter("@UserID", intUserID);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, parmArray);


                return CalCheckCount(intYear, intMonth, intPlantCode, intUserID);

            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// �Ƽ����ʽ���������
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intPlantCode"></param>
        /// <param name="strDate"></param>
        /// <param name="intUserID"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public int CalculateSalaryPT(int intYear, int intMonth, int intPlantCode, string strDate, Int32 intUserID, int intType)
        {
            try
            {
                string CalDate;
                //HR_Salary hrs = new HR_Salary();
                //IList<HR_Salary> Salary = new List<HR_Salary>();
                //Get basic information for salary
                HR_BaseInfo hrbi = SelectHRBaseInfo(intYear, intMonth);


                string strSql;
                if (intType == 0)
                    strSql = "sp_hr_GetCalculateInfoBK";
                else
                    strSql = "sp_hr_GetCalculateInfoBKtoB";

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

                DataSet dsLine = null; //��������ߵ��߳����ʣ���ʱMark�� 2013-12-04 By Shan Zhiming

                if (intType == 1)
                {
                    htbDepart = GetHashData(intYear, intMonth, intPlantCode, 0, hrbi.WorkDays, 1);
                    htbLine = GetHashData(intYear, intMonth, intPlantCode, 1, hrbi.WorkDays, 1);

                    dsLine = GetLineRelation(intPlantCode);
                }



                SqlDataReader sdtr = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
                while (sdtr.Read())
                {
                    HR_Salary hrs = new HR_Salary();

                    decimal decSalary, decWorkpay, decOver, decBasic, decDeduct, decSday, descOldSday, decBenefit, decCoef, decRateSalary;
                    decSalary = 0;
                    decWorkpay = 0;
                    decOver = 0;
                    decDeduct = 0;
                    decSday = 0;
                    descOldSday = 0;
                    decBenefit = 0;
                    decCoef = 0;
                    decRateSalary = 0;

                    decimal decRateDeduct = 0;  //�����۲�
                    decimal decRate;

                    decimal decSDays; // ��ɥ������
                    decSDays = 0;

                    bool Special = true;  // �Ƿ���������������

                    //--����-----------//

                    //--End ----------//

                    //--��Ա����Ϣ��---------//
                    hrs.s_UserID = Convert.ToInt32(sdtr["userID"]);
                    string userno = sdtr["userNo"].ToString();
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


                    //����
                    hrs.s_HumanAllowance = Convert.ToDecimal(sdtr["humanAllowance"]);
                    hrs.s_HightTemp = Convert.ToDecimal(sdtr["hightTemp"]);
                    hrs.s_NOrmal = Convert.ToDecimal(sdtr["amount"]);
                    hrs.s_Other = Convert.ToDecimal(sdtr["sig"]);
                    hrs.s_Newuser = Convert.ToDecimal(sdtr["newuser"]);
                    hrs.s_Student = Convert.ToDecimal(sdtr["student"]);
                    hrs.s_Bonus = Convert.ToDecimal(sdtr["bonus"]);
                    hrs.s_AllMobile = Convert.ToDecimal(sdtr["allMobile"]);
                    hrs.s_AllKilometer = Convert.ToDecimal(sdtr["allKilometer"]);
                    hrs.s_AllBox = Convert.ToDecimal(sdtr["allBox"]);
                    hrs.s_AllBusiness = Convert.ToDecimal(sdtr["allBusiness"]);
                    hrs.s_AllOnDuty = Convert.ToDecimal(sdtr["allOnDuty"]);
                    hrs.s_AllPost = Convert.ToDecimal(sdtr["allPost"]);

                    //�ۿ�
                    hrs.s_MaterialDeduct = Convert.ToDecimal(sdtr["materialDeduct"]);
                    hrs.s_AssessDeduct = Convert.ToDecimal(sdtr["assessDeduct"]);
                    hrs.s_VacationDeduct = Convert.ToDecimal(sdtr["vacationDeduct"]);
                    hrs.s_OtherDeduct = Convert.ToDecimal(sdtr["Otherdeduct"]);
                    hrs.s_Ship = Convert.ToDecimal(sdtr["ship"]);
                    hrs.s_LocFin = Convert.ToDecimal(sdtr["locFin"]);
                    hrs.s_Quanlity = Convert.ToDecimal(sdtr["quanlity"]);
                    hrs.s_Break = Convert.ToDecimal(sdtr["abreak"]);

                    hrs.bt_TiaoXiu = Convert.ToDecimal(sdtr["bt_tiaoxiu"]);
                    hrs.bt_ChoaE = Convert.ToDecimal(sdtr["bt_chaoe"]);
                    hrs.bt_NewCost = Convert.ToDecimal(sdtr["bt_newCost"]);
                    hrs.s_MutualFunds = Convert.ToDecimal(sdtr["MutualFunds"]);

                    hrs.f_Electricity = Convert.ToDecimal(sdtr["FeeElectricity"]);
                    hrs.f_Water = Convert.ToDecimal(sdtr["FeeWater"]);
                    hrs.f_Dormitory = Convert.ToDecimal(sdtr["FeeDormitory"]);

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
                    bool blNewUser = false;  // �ж��Ƿ�Ϊ�½�Ա��
                    hrs.s_ProbWorkDays = Convert.ToDecimal(sdtr["pwork"]);
                    decimal hrbiBasicWage = hrbi.BasicWage;//Modify By Shanzm 2015-01-04:����������Ա���������ʺ������Ļ�������
                    if (Convert.ToDateTime(sdtr["enterdate"]).Year == intYear && Convert.ToDateTime(sdtr["enterdate"]).Month == intMonth)
                    {
                        blNewUser = true;
                        //2015-02-02����ά���ģ���ֱ�Ӱ��������ļ���
                        if (hrs.s_ProbWorkDays > 0)
                        {
                            hrbiBasicWage = hrs.s_ProbWorkDays * hrbi.BasicWage / hrbi.AvgDays;
                        }
                    }

                    hrs.s_Workyear = Convert.ToInt32(sdtr["workyear"]);
                    //--- End -------------//

                    //--�籣��/ ��ҹ���  / ����--------------------------//
                    hrs.s_Hfound = Convert.ToDecimal(sdtr["hFound"]);
                    hrs.s_AccrFound = Convert.ToDecimal(sdtr["rFound"]);
                    hrs.s_AcceFound = Convert.ToDecimal(sdtr["eFound"]);
                    hrs.s_AccmFound = Convert.ToDecimal(sdtr["mFound"]);
                    hrs.s_Rfound = hrs.s_AccrFound + hrs.s_AcceFound + hrs.s_AccmFound;

                    hrs.s_NightMoney = Math.Round(hrbi.MidNightSubsidy * Convert.ToInt32(sdtr["mid"]) + hrbi.NightSubsidy * Convert.ToInt32(sdtr["night"]) + hrbi.WholeNightSubsidy * Convert.ToInt32(sdtr["whole"]), 1);
                    hrs.s_Mid = Convert.ToInt32(sdtr["mid"]);
                    hrs.s_Night = Convert.ToInt32(sdtr["night"]);
                    hrs.s_Whole = Convert.ToInt32(sdtr["whole"]);

                    hrs.s_Oallowance = hrs.s_HumanAllowance + hrs.s_HightTemp + hrs.s_NOrmal + hrs.s_Other + hrs.s_Newuser + hrs.s_Student + hrs.s_Bonus + hrs.s_AllMobile + hrs.s_AllKilometer + hrs.s_AllBox + hrs.s_AllBusiness + hrs.s_AllOnDuty + hrs.s_AllPost;
                    hrs.s_Childwance = Convert.ToDecimal(sdtr["sig"]);
                    //---End --------------------------//

                    //--�ۿ��������ʣ��ۿ�------//

                    hrs.s_Lastduct = Convert.ToDecimal(sdtr["remaindeduct"]);
                    decDeduct = hrs.s_Lastduct + Convert.ToDecimal(sdtr["deduct"]);
                    //��������
                    decRate = Convert.ToDecimal(sdtr["rate"]);
                    if (decRate == 0)
                        decRateDeduct = 0 - Convert.ToDecimal(sdtr["mark"]) * Convert.ToDecimal(sdtr["drate"]);
                    else
                    {
                        if (decRate > 0 && decRate < 100)
                            decRateDeduct = 0 - Convert.ToDecimal(sdtr["mark"]) * decRate;
                        else
                            decRateDeduct = (decRate - 100 - Convert.ToDecimal(sdtr["mark"])) * 0.003M;
                    }

                    if (decRate > 100)
                        decRateDeduct = hrbiBasicWage * decRateDeduct;


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

                    //--End -----------------------//

                    //--���¼٣����-- ��٣�ɥ��/���ˣ�����-------------//
                    if (Convert.ToDecimal(sdtr["sickDays"]) == 0 && Convert.ToDecimal(sdtr["bussinessDays"]) == 0 && Convert.ToDecimal(sdtr["minerDays"]) == 0)
                        hrs.s_Leave = true;
                    else
                        hrs.s_Leave = false;
                    hrs.s_Leaveday = Convert.ToDecimal(sdtr["bussinessDays"]);
                    hrs.s_MinerDays = Convert.ToDecimal(sdtr["minerDays"]);
                    hrs.s_Restleave = Convert.ToDecimal(sdtr["number"]);
                    hrs.s_SickLeave = Convert.ToDecimal(sdtr["sickDays"]);

                    hrs.s_SickLeavePay = Math.Round(hrbiBasicWage * hrbi.SickleaveRate * hrs.s_SickLeave / 100 / hrbi.SickleaveDay, 1);

                    decSDays = Math.Round((Convert.ToDecimal(sdtr["merrageDays"]) + Convert.ToDecimal(sdtr["funeralDays"]) + Convert.ToDecimal(sdtr["injuryDays"])) / hrbi.SickleaveDay * hrbi.AvgDays, 2);
                    descOldSday = Convert.ToDecimal(sdtr["merrageDays"]) + Convert.ToDecimal(sdtr["funeralDays"]) + Convert.ToDecimal(sdtr["injuryDays"]);
                    hrs.s_Otherday = descOldSday;

                    hrs.s_MaternityDays = Convert.ToDecimal(sdtr["maternityDays"]);
                    hrs.s_MaternityPay = Math.Round(hrs.s_MaternityDays * hrbiBasicWage / hrbi.SickleaveDay, 1);
                    //--End ----------------------//

                    //--����--------------------//
                    if (sdtr["holidaycost"].ToString().Length > 0 && Convert.ToDecimal(sdtr["holidaycost"]) > 0)
                    {
                        hrs.s_Shday = Math.Round(Convert.ToDecimal(sdtr["holidaycost"]) / 8, 2);
                        hrs.s_Shsalary = Math.Round(hrs.s_Shday * hrbi.BasicPrice * hrbi.HolidayRate, 1);
                        hrs.s_Holiday = hrs.s_Shsalary;
                    }
                    else
                    {
                        hrs.s_Shday = 0;
                        hrs.s_Shsalary = 0;
                        hrs.s_Holiday = 0;
                    }

                    //--End --------------------//

                    //--����-----------------------//
                    //A���ϵ�������ó�0��
                    if (Convert.ToDecimal(sdtr["bc_coef"]) > 0)
                    {//B��
                        decCoef = Convert.ToDecimal(sdtr["bc_coef"]);
                        hrs.s_Attdays = Convert.ToDecimal(sdtr["wdd"]);
                        hrs.s_AttHours = Convert.ToDecimal(sdtr["tdd"]);
                    }
                    else
                    {//A��
                        hrs.s_Attdays = Convert.ToDecimal(sdtr["gday"]);
                        hrs.s_AttHours = Convert.ToDecimal(sdtr["attendence"]);
                    }
                    //-----------------------//

                    //--����   1-----//
                    hrs.s_WorkMoney = Math.Round(Convert.ToDecimal(sdtr["cost"]) / 8, 2) - hrs.s_Shday; //sdtr["cost"] �Ѿ������˳����
                    if (hrs.s_WorkMoney < 0)
                        hrs.s_WorkMoney = 0;

                    hrs.s_Original = Convert.ToDecimal(sdtr["original"]);//sdtr["original"] �Ѿ������˳����
                    // ʵ�ʼӹ�������------//
                    hrs.s_RealMoney = Math.Round((Convert.ToDecimal(sdtr["jcost"]) + Convert.ToDecimal(sdtr["kcost"])) / 8, 2);

                    //���ڼƼ���ʱת��
                    if (Convert.ToInt32(sdtr["attUserID"]) > 0)
                    {
                        hrs.s_ExChange = true;
                        decSDays = 0;
                        hrs.s_Restleave = 0;
                    }
                    else
                        hrs.s_ExChange = false;

                    //�Ƿ�Ϊ�����½�Ա��
                    if (hrs.s_ProbWorkDays > 0)
                    {
                        hrs.s_IsProbationer = true;
                        decSday = hrs.s_ProbWorkDays;
                    }
                    else
                    {
                        hrs.s_IsProbationer = false;
                        decSday = hrbi.WorkDays;
                    }
                    //decSday Ϊ����Ӧ����
                    //decSDays �����Ļ�|ɥ|���˼�

                    if (hrs.s_WorkMoney + hrs.s_Restleave + decSDays >= decSday)
                    {
                        decBasic = hrbiBasicWage;
                        //Modify By Shanzm 2014-04-30:�Ӱ�Ѹ��á��������ʡ�
                        decOver = Math.Round((hrs.s_WorkMoney + hrs.s_Restleave + decSDays - decSday) * hrbi.OverPrice * hrbi.OverTimeRate, 1);
                        //decOver = Math.Round((hrs.s_WorkMoney + hrs.s_Restleave + decSDays - decSday) * hrbi.BasicPrice * hrbi.OverTimeRate, 1);
                    }
                    else
                    {
                        //  �������Ӧ���ڣ�Ҫ�ѹ��������
                        if (intType == 0)
                        {
                            Decimal dechody = 0;

                            decBasic = Math.Round((hrs.s_WorkMoney + hrs.s_Restleave + decSDays + dechody) * hrbi.BasicPrice, 1);
                            if (decBasic >= hrbiBasicWage)
                                decBasic = hrbiBasicWage;
                        }
                        else
                        { // A�� ��ȡƽ������
                            decimal decfin;
                            if (Convert.ToBoolean(sdtr["dflag"])) // �Ƿ�ȡ����ƽ������ 
                            {
                                decRateSalary = Convert.ToDecimal(htbDepart[hrs.s_Department]);
                                decfin = Math.Round(Convert.ToDecimal(htbDepart[hrs.s_Department]) * (hrs.s_Attdays + hrs.s_Restleave) * decCoef, 2);
                            }
                            else
                            {
                                if (hrs.s_WorkshopID > 0)
                                {
                                    decRateSalary = 0;
                                    DataRow[] rows = dsLine.Tables[0].Select("bcf_userid = " + hrs.s_UserID.ToString());
                                    if (rows.Length > 0)
                                    {
                                        foreach (DataRow row in rows)
                                        {
                                            int _workShop = Convert.ToInt32(row["bcf_workshop"]);
                                            decRateSalary += Convert.ToDecimal(htbLine[_workShop]);
                                        }
                                        decRateSalary = decRateSalary / rows.Length;
                                    }
                                    decfin = Math.Round(decRateSalary * (hrs.s_Attdays + hrs.s_Restleave) * decCoef, 2);
                                }
                                else
                                {
                                    decRateSalary = Convert.ToDecimal(htbDepart[hrs.s_Department]);
                                    decfin = Math.Round(Convert.ToDecimal(htbDepart[hrs.s_Department]) * (hrs.s_Attdays + hrs.s_Restleave) * decCoef, 2);
                                }
                            }

                            //decSDays:�����Ļ�ɥ�����ڣ�decSday������Ӧ����
                            if (hrs.s_AttHours + hrs.s_Restleave + decSDays >= decSday)
                            {
                                decBasic = hrbiBasicWage;
                                //Modify By Shanzm 2014-04-30:�Ӱ�Ѹ��á��������ʡ�OverPrice
                                decOver = Math.Round((hrs.s_AttHours + hrs.s_Restleave + decSDays - decSday) * hrbi.OverPrice * hrbi.OverTimeRate, 1);
                                //decOver = Math.Round((hrs.s_AttHours + hrs.s_Restleave + decSDays - decSday) * hrbi.BasicPrice * hrbi.OverTimeRate, 1);
                            }
                            else
                            {
                                decBasic = Math.Round((hrs.s_AttHours + hrs.s_Restleave + decSDays) * hrbi.BasicPrice, 1);
                                if (decBasic >= hrbiBasicWage)
                                    decBasic = hrbiBasicWage;
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
                    }

                    //  �ӹ������ѡ�������٣��飬ɥ�ٵĹ��� ������decSalary


                    //ȫ�»�||ɥ||����
                    if ((Convert.ToDecimal(sdtr["merrageDays"]) + Convert.ToDecimal(sdtr["funeralDays"]) + Convert.ToDecimal(sdtr["injuryDays"])) == hrbi.SickleaveDay)
                    {
                        decBasic = hrbiBasicWage;
                        decOver = 0;
                    }

                    //--  ȫ�� & ���䲹��---//
                    hrs.s_Allattendence = Convert.ToDecimal(sdtr["allattendence"]);
                    hrs.s_WYbonus = Convert.ToDecimal(sdtr["wybonus"]);
                    //--  End--------------//

                    //����������͵��ݲ�������Ӱ��
                    decOver += hrs.bt_TiaoXiu;

                    //By Shanzm 2015-01-21:����ֽ��������ʣ�1270����ʵ�ʻ������ʣ�1480����һ��
                    if (hrbi.BasicWageNew > 0 && hrbi.BasicWageNew < decBasic)
                    {
                        decOver += decBasic - hrbi.BasicWageNew;
                        decBasic = hrbi.BasicWageNew;
                    }

                    //Ӧ����� ���������ʡ������Ӱ�ѡ���������������Ů������ҹ����������������  + ȫ�� & ���䲹�� + �������� ����
                    decSalary = decBasic + decOver + hrs.s_Oallowance + hrs.s_NightMoney + hrs.s_Subsidies + hrs.s_Holiday + hrs.s_Allattendence + hrs.s_WYbonus + hrs.s_SickLeavePay + hrs.s_MaternityPay + hrs.s_Benefit;

                    // �Ϻ���Ҫ����������ʣ��۳��籣�Ļ����Ϻ͸��·ѡ�������Ů�ѣ�
                    if ((intPlantCode == 1) && (decSalary - hrs.s_Hfound - hrs.s_Rfound - hrs.s_HightTemp - hrs.s_Other < hrbiBasicWage) && (hrs.s_Fire == false) && (hrs.s_ExChange == false) && (hrs.s_Leave == true) && (blNewUser == false))
                    {
                        decimal subsidies = hrbiBasicWage - decSalary + hrs.s_Hfound + hrs.s_Rfound + hrs.s_HightTemp + hrs.s_Other;
                        hrs.s_Subsidies += subsidies;
                        decSalary = decSalary + subsidies;
                    }

                    if (intPlantCode == 1)
                    {
                        decimal found = decimal.Round((hrs.s_Hfound + hrs.s_Rfound) * 1.05m, 0);
                        if (decOver >= found)
                        {
                            decOver -= found;
                            decBenefit += found;
                        }
                        else
                        {                         
                            decBenefit += decOver;
                            decOver = 0;
                        }
                    }


                    if (Special == false) //����������ʵ�Ա���ۿ�ŵ��¸��¿۳�
                    {
                        hrs.s_Duct = 0;
                        hrs.s_Currduct = decDeduct;
                    }
                    else
                    {����//�ۿ�ܳ���Ӧ������20%
                        if (decSalary >= 0 && decSalary * hrbi.DeductRate < decDeduct)
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


                    //--�����---��˰--------//
                    if (Convert.ToBoolean(sdtr["isLabourUnion"]))
                        hrs.s_Memship = Math.Round(hrbiBasicWage * hrbi.LaborRate, 1);
                    else
                        hrs.s_Memship = 0;

                    hrs.s_Tax = Math.Round(SalaryTax(decSalary - hrs.s_Childwance, hrs.s_Duct + hrbi.Tax + hrs.s_Hfound + hrs.s_Rfound, intPlantCode), 2);
                    decimal decContants;
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

                    SaveSalaryData(hrs, strDate, intUserID);

                }
                sdtr.Close();

                htbDepart.Clear();
                htbLine.Clear();

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intUsertype"></param>
        /// <param name="decLeave"></param>
        /// <param name="strRole"></param>
        /// <param name="intBelate"></param>
        /// <returns></returns>
        public decimal Attbonus(int intUsertype, decimal decLeave, string strRole, int intBelate, decimal decMax, decimal decMin)
        {
            decimal Abonus = 0;
            if (intUsertype <= 396 || strRole == "�ֿⱣ��Ա" || strRole == "��Ʒ����Ա")
            {
                //No be late and leave early
                if (intBelate == 0)
                {
                    if (decLeave == 0)
                        Abonus = decMax;
                    else
                    {
                        if (decLeave <= 0.125M)
                            Abonus = decMin;
                    }
                }
            }

            return Abonus;
        }

        public decimal WorkBonus(decimal decfix, decimal decMax, decimal decMin, decimal decNormal, decimal decAllowance, int intWorkYear, int intUserID)
        {
            decimal Wbonus = 0;
            decimal decBonusMax = 200;
            decimal decBonusMin = 100;

            return Wbonus;
        }


        /// <summary>
        /// ˰
        /// </summary>
        /// <param name="decTotal"></param>
        /// <param name="decDuct"></param>
        /// <returns></returns>
        public decimal SalaryTax(decimal decTotal, decimal decDuct, int plantCode)
        {
            decimal sTax = 0;
            //if (decTotal - decDuct <= 500)
            //    sTax = (decTotal - decDuct) * 0.05M;
            //else
            //{
            //    if (decTotal - decDuct <= 2000)
            //        sTax = (decTotal - decDuct) * 0.1M - 25;
            //    else
            //    {
            //        if (decTotal - decDuct <= 5000)
            //            sTax = (decTotal - decDuct) * 0.15M - 125;
            //        else
            //            sTax = (decTotal - decDuct) * 0.2M - 375;
            //    }
            //}

            if (decTotal - decDuct <= 1500)
                sTax = (decTotal - decDuct) * 0.03M;
            else
            {
                if (decTotal - decDuct <= 4500)
                    sTax = (decTotal - decDuct) * 0.1M - 105;
                else
                {
                    if (decTotal - decDuct <= 9000)
                        sTax = (decTotal - decDuct) * 0.2M - 555;
                    else
                    {
                        if (decTotal - decDuct <= 35000)
                            sTax = (decTotal - decDuct) * 0.25M - 1005;
                        else
                            sTax = (decTotal - decDuct) * 0.3M - 2755;
                    }
                }
            }

            if (sTax < 0)
                sTax = 0;
            //if ((plantCode == 2 || plantCode == 5 || plantCode == 8) && sTax < 1)
            //{
            //    sTax = 0;
            //}
            return sTax;
        }

        public static DataTable GetSalaryVersion(int intYear, int intMonth, int intPlantCode)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_SalaryVersion";
                SqlParameter[] parmArray = new SqlParameter[6];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@plantCode", intPlantCode);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable SelectSalaryCompare(int intYear, int intMonth, string userNo, string userName, int intDepartment, int intVersion)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_GetSalaryCompare";
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

        public string ExportSalaryCompare(int intYear, int intMonth, string strUserNo, string strUserName, int intDepartment, int intVersion, int intType)
        {
            try
            {
                string strSql = "sp_hr_GetSalaryCompare";
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


        public string SalaryUserCheck(string strUser, int intPlantcode, int intYear, int intMonth, int intOperateID)
        {
            try
            {
                string strSql = "sp_hr_SalaryUserCheck";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Plantcode", intPlantcode);
                parmArray[3] = new SqlParameter("@UserNo", strUser);
                parmArray[4] = new SqlParameter("@OperateID", intOperateID);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }


        public int AdjustSalarySave(int intYear, int intMonth, int intPlantcode, int intDepartment, int intWorkshop, int intWorkgroup, int intWorktype, string strUserID, decimal decAdjPercent, decimal decAdjMoney, int intOperateID, int intSalaryID, string strReason)
        {
            try
            {
                string strSql = "sp_hr_AdjustSalarySave";
                SqlParameter[] parmArray = new SqlParameter[13];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Plantcode", intPlantcode);
                parmArray[3] = new SqlParameter("@department", intDepartment);
                parmArray[4] = new SqlParameter("@workshop", intWorkshop);
                parmArray[5] = new SqlParameter("@workgroup", intWorkgroup);
                parmArray[6] = new SqlParameter("@worktype", intWorktype);
                parmArray[7] = new SqlParameter("@UserID", strUserID);
                parmArray[8] = new SqlParameter("@percent", decAdjPercent);
                parmArray[9] = new SqlParameter("@money", decAdjMoney);
                parmArray[10] = new SqlParameter("@operateID", intOperateID);
                parmArray[11] = new SqlParameter("@SalaryID", intSalaryID);
                parmArray[12] = new SqlParameter("@reason", strReason);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
                //string str = Convert.ToString (SqlHelper.ExecuteScalar (adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray));
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public string AdjustSalaryExport(int intYear, int intMonth, int intPlantcode, int intDepartment, int intWorkshop, int intWorkgroup, int intWorktype, string strUserNo, string strUserName, int intOperateID)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "sp_hr_AdjustSalarySelect";
                SqlParameter[] parmArray = new SqlParameter[10];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
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

        public static DataTable AdjustSalarySelect(int intYear, int intMonth, int intPlantcode, int intDepartment, int intWorkshop, int intWorkgroup, int intWorktype, string strUserNo, string strUserName, int intOperateID)
        {
            try
            {
                adamClass adc = new adamClass();
                //string strSql = "sp_hr_AdjustSalarySelect";
                //SqlParameter[] parmArray = new SqlParameter[10];
                //parmArray[0] = new SqlParameter("@Year", intYear);
                //parmArray[1] = new SqlParameter("@Month", intMonth);
                //parmArray[2] = new SqlParameter("@Plantcode", intPlantcode);
                //parmArray[3] = new SqlParameter("@department", intDepartment);
                //parmArray[4] = new SqlParameter("@workshop", intWorkshop);
                //parmArray[5] = new SqlParameter("@workgroup", intWorkgroup);
                //parmArray[6] = new SqlParameter("@worktype", intWorktype);
                //parmArray[7] = new SqlParameter("@Userno", strUserNo);
                //parmArray[8] = new SqlParameter("@Username", strUserName);
                //parmArray[9] = new SqlParameter("@OperateID", intOperateID);
                HR hr_salary = new HR();
                string str = hr_salary.AdjustSalaryExport(intYear, intMonth, intPlantcode, intDepartment, intWorkshop, intWorkgroup, intWorktype, strUserNo, strUserName, intOperateID);
                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.Text, str).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        public int AdjustToFinacial(int intYear, int intMonth, int intOperateID)
        {
            try
            {
                string strSql = "sp_hr_SalaryToFinance";
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


        public string PrintString(int intType, int intYear, int intMonth, int intDept, int intWorkshop, int intWorkgroup, int intInsurance, int intEmployType, string strUserNo, int intWorkType, int intPlant, int status = 0)
        {

            try
            {
                string strSql = "sp_hr_PrintSalary";
                SqlParameter[] parmArray = new SqlParameter[12];
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
                parmArray[11] = new SqlParameter("@status", status);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return null;
            }
        }

        public string PrintGuString(int intType, int intYear, int intMonth, int intDept, int intWorkshop, int intWorkgroup, int intInsurance, int intEmployType, string strUserNo, int intWorkType, int intPlant)
        {

            try
            {
                string strSql = "sp_hr_PrintSalaryGU";
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


        public string ExportFinSalary(int intYear, int intMonth, int intType, int intPlant)
        {
            try
            {
                string strSql = "sp_hr_ExportFin_Salary";
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

        public int finAdjust(int intYear, int intMonth, int intPlantcode, int intType)
        {
            try
            {
                string strSql = "sp_hr_finAdjust";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlantcode);
                parmArray[3] = new SqlParameter("@Type", intType);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region  Time Salary
        public virtual void DeleteSalaryDataTime(int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_DeleteSalaryTime";
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

        public virtual void SaveTimeSalary(hr_TimeSalary hrt, int intUserID)
        {
            try
            {
                string strSql = "sp_hr_SaveTimeSalary";
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
            catch
            {
            }
        }

        /// <summary>
        /// ��ʱ���ʽ���������
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intUserID"></param>
        /// <param name="intPlantcode"></param>
        /// <returns></returns>
        public int CalculateTimeSalaryPT(int intYear, int intMonth, int intUserID, int intPlantcode)
        {
            //try
            //{
                HR_BaseInfo hrBaseInfo = SelectHRBaseInfo(intYear, intMonth);

                SqlDataReader sdrReader = GetTimeCalculateInfoPT(intYear, intMonth, intPlantcode);

                Hashtable htbDepart = new Hashtable();
                Hashtable htbLine = new Hashtable();

                htbDepart = GetHashData(intYear, intMonth, intPlantcode, 0, hrBaseInfo.WorkDays, 0);
                htbLine = GetHashData(intYear, intMonth, intPlantcode, 1, hrBaseInfo.WorkDays, 0);

                while (sdrReader.Read())
                {
                    hr_TimeSalary hts = new hr_TimeSalary();

                    bool bolflag;
                    bolflag = false; //Whether the user type is B/C and pay avg salary 

                    //User's basic information
                    hts.T_userID = Convert.ToInt32(sdrReader["userID"]);
                    string userno = Convert.ToString(sdrReader["userNo"]);
                    hts.T_userNo = Convert.ToString(sdrReader["userNo"]);
                    hts.T_userName = Convert.ToString(sdrReader["userName"]);
                    hts.T_employTypeID = Convert.ToInt32(sdrReader["employTypeID"]);
                    hts.T_department = Convert.ToInt32(sdrReader["departmentID"]);
                    hts.T_insuranceTypeID = Convert.ToInt32(sdrReader["insuranceTypeID"]);
                    hts.T_worktype = Convert.ToInt32(sdrReader["worktypeID"]);
                    hts.T_salaryDate = intYear.ToString() + "-" + intMonth.ToString() + "-01";
                    hts.T_workyear = Convert.ToInt32(sdrReader["workyear"]);
                    hts.T_workShopID = Convert.ToInt32(sdrReader["workshopID"]);
                    hts.T_userType = Convert.ToInt32(sdrReader["userType"]);
                    if (sdrReader["leavedate"].ToString() == "")
                        hts.T_fire = false;
                    else
                    {
                        if (Convert.ToDateTime(sdrReader["leavedate"]) < Convert.ToDateTime(hts.T_salaryDate).AddMonths(1))
                            hts.T_fire = true;
                        else
                            hts.T_fire = false;
                    }

                    //---------
                    hts.t_HumanAllowance = Convert.ToDecimal(sdrReader["humanAllowance"]);
                    hts.t_HightTemp = Convert.ToDecimal(sdrReader["hightTemp"]);
                    hts.t_Normal = Convert.ToDecimal(sdrReader["amount"]);
                    hts.t_Allother = Convert.ToDecimal(sdrReader["sig"]);
                    hts.T_newuser = Convert.ToDecimal(sdrReader["newuser"]);
                    hts.T_student = Convert.ToDecimal(sdrReader["student"]);
                    hts.T_bonus = Convert.ToDecimal(sdrReader["bonus"]);
                    hts.t_AllMobile = Convert.ToDecimal(sdrReader["allMobile"]);
                    hts.t_AllKilometer = Convert.ToDecimal(sdrReader["allKilometer"]);
                    hts.t_AllBox = Convert.ToDecimal(sdrReader["allBox"]);
                    hts.t_AllBusiness = Convert.ToDecimal(sdrReader["allBusiness"]);
                    hts.t_AllOnDuty = Convert.ToDecimal(sdrReader["allOnDuty"]);
                    hts.t_AllPost = Convert.ToDecimal(sdrReader["allPost"]);

                    hts.T_materialDeduct = Convert.ToDecimal(sdrReader["materialDeduct"]);
                    hts.T_vacationDeduct = Convert.ToDecimal(sdrReader["vacationDeduct"]);
                    hts.T_otherDeduct = Convert.ToDecimal(sdrReader["otherDeduct"]);
                    hts.t_Ship = Convert.ToDecimal(sdrReader["ship"]);
                    hts.t_LocFin = Convert.ToDecimal(sdrReader["locFin"]);
                    hts.t_Quanlity = Convert.ToDecimal(sdrReader["quanlity"]);
                    hts.t_Break = Convert.ToDecimal(sdrReader["abreak"]);
                    //---------


                    //----User benefit---------//
                    hts.T_hfound = Convert.ToDecimal(sdrReader["hFound"]);
                    hts.T_accrFound = Convert.ToDecimal(sdrReader["rFound"]);
                    hts.T_acceFound = Convert.ToDecimal(sdrReader["eFound"]);
                    hts.T_accmFound = Convert.ToDecimal(sdrReader["mFound"]);
                    hts.T_rfound = hts.T_accrFound + hts.T_acceFound + hts.T_accmFound;
                    //hts.T_rfound = Convert.ToDecimal(sdrReader["rHound"]);

                    hts.T_mid = Convert.ToInt32(sdrReader["Mid"]);
                    hts.T_night = Convert.ToInt32(sdrReader["Night"]);
                    hts.T_whole = Convert.ToInt32(sdrReader["Whole"]);
                    hts.T_nightWork = Math.Round(hts.T_mid * hrBaseInfo.MidNightSubsidy + hts.T_night * hrBaseInfo.NightSubsidy + hts.T_whole * hrBaseInfo.WholeNightSubsidy, 1);

                    hts.T_allowance = hts.t_HumanAllowance + hts.t_HightTemp + hts.t_Normal + hts.t_Allother + hts.T_newuser + hts.T_student + hts.T_bonus
                            + hts.t_AllMobile + hts.t_AllKilometer + hts.t_AllBox + hts.t_AllBusiness + hts.t_AllOnDuty + hts.t_AllPost;


                    hts.T_hday = Math.Round(Convert.ToDecimal(sdrReader["holiday"]) / 8, 2);
                    hts.T_holiday = Math.Round(hts.T_hday * hrBaseInfo.HolidayRate * hrBaseInfo.BasicPrice, 1);

                    hts.T_annual = Convert.ToDecimal(sdrReader["number"]);

                    hts.T_sleave = Convert.ToDecimal(sdrReader["sickDays"]);

                    hts.T_MutualFunds = Convert.ToDecimal(sdrReader["MutualFunds"]);
                    hts.F_Electricity = Convert.ToDecimal(sdrReader["FeeElectricity"]);
                    hts.F_Water = Convert.ToDecimal(sdrReader["FeeWater"]);
                    hts.F_Dormitory = Convert.ToDecimal(sdrReader["FeeDormitory"]);

                    //-------------------- -----------------------

                    // �̶�����
                    hts.T_fixedsalary = Convert.ToDecimal(sdrReader["fixedsalary"]);

                    hts.T_attendance = Math.Round((Convert.ToDecimal(sdrReader["Total"]) - Convert.ToDecimal(sdrReader["holiday"])) / 8, 2);  //����Сʱ

                    hts.T_attday = Convert.ToDecimal(sdrReader["Days"]) - Convert.ToDecimal(sdrReader["holdays"]); //������

                    decimal decWork, decOver, decLeaveDay, decBasic, descLeavePay, decOld, decFixedsalary;

                    decFixedsalary = hts.T_fixedsalary;
                    decOver = 0;
                    decLeaveDay = Math.Round((Convert.ToDecimal(sdrReader["merrageDays"]) + Convert.ToDecimal(sdrReader["funeralDays"]) + Convert.ToDecimal(sdrReader["injuryDays"])) / hrBaseInfo.SickleaveDay * hrBaseInfo.AvgDays, 2); // �飬ɥ������
                    
                    hts.T_other = decLeaveDay;
                    hts.T_leave = Convert.ToDecimal(sdrReader["bussinessDays"]);
                    hts.T_maternityDays = Convert.ToDecimal(sdrReader["maternityDays"]);    //��������
                    hts.T_maternityPay = Math.Round(hts.T_maternityDays * hrBaseInfo.BasicWage / hrBaseInfo.SickleaveDay, 1);   //���ٹ���

                    hts.T_sleavepay = Math.Round(hts.T_sleave * hrBaseInfo.SickleaveRate * hrBaseInfo.BasicWage / 100 / hrBaseInfo.SickleaveDay, 1);
                    if (hts.T_sleave == hrBaseInfo.SickleaveDay)
                        hts.T_sleavepay = Math.Round(hrBaseInfo.BasicWage * hrBaseInfo.SickleaveRate / 100, 1);

                    decOld = Convert.ToDecimal(sdrReader["merrageDays"]) + Convert.ToDecimal(sdrReader["funeralDays"]) + Convert.ToDecimal(sdrReader["injuryDays"]) + hts.T_maternityDays + hts.T_sleave;

                    bool blNewUser = false;  // �ж��Ƿ�Ϊ�½�Ա��
                    if (Convert.ToDateTime(sdrReader["enterdate"]).Year == intYear && Convert.ToDateTime(sdrReader["enterdate"]).Month == intMonth)
                        blNewUser = true;

                    //Time salary
                    // �������� / �Ӱ��  /  Ч�湤��
                    if (hts.T_attendance + hts.T_annual + hts.T_other >= hrBaseInfo.WorkDays)
                    {
                        hts.T_basic = hrBaseInfo.BasicWage;
                        decOver = Math.Round((hts.T_attendance + hts.T_annual + hts.T_other - hrBaseInfo.WorkDays) * hrBaseInfo.BasicPrice * hrBaseInfo.OverTimeRate, 1);
                    }
                    else
                    {
                        if (hts.T_maternityDays + hts.T_sleave >= hrBaseInfo.SickleaveDay)
                        {
                            decBasic = 0;
                            hts.T_basic = 0;
                        }
                        else
                        {
                            int intday = CheckHoliday(sdrReader["enterdate"].ToString(), sdrReader["leavedate"].ToString(), intYear, intMonth, intUserID, intPlantcode);

                            decBasic = Math.Round((hts.T_attendance + hts.T_annual + decLeaveDay + intday) * hrBaseInfo.BasicPrice, 1);
                            if (decBasic >= hrBaseInfo.BasicWage)
                            {
                                //decOver = decBasic - hrBaseInfo.BasicWage;//�ϰ�����û�г����³�����������ʹ����Ҳ����Ӱ๤��
                                decBasic = hrBaseInfo.BasicWage;
                            }

                            hts.T_basic = decBasic;
                        }
                    }

                    //ȫ�»�||ɥ||����
                    if ((Convert.ToDecimal(sdrReader["merrageDays"]) + Convert.ToDecimal(sdrReader["funeralDays"]) + Convert.ToDecimal(sdrReader["injuryDays"])) == hrBaseInfo.SickleaveDay)
                    {
                        hts.T_basic = hrBaseInfo.BasicWage;
                        decOver = 0;
                    }

                    hts.T_benefit = 0;
                    descLeavePay = 0;  // ��ٿۿ���0

                    //-----B/C�� --------//
                    if (hts.T_userType == 395 || hts.T_userType == 396)
                    {
                        if (hts.T_fixedsalary == 0) // û�й̶�����
                        {
                            bolflag = true;

                            if (hts.T_workShopID > 0)
                            {
                                if (Convert.ToDecimal(sdrReader["coef"]) > 0)
                                    hts.T_fixedsalary = Math.Round(Convert.ToDecimal(htbLine[hts.T_workShopID]) * (hts.T_attday + hts.T_annual) * Convert.ToDecimal(sdrReader["coef"]), 2);
                                else
                                    hts.T_fixedsalary = Math.Round(Convert.ToDecimal(htbLine[hts.T_workShopID]) * (hts.T_attday + hts.T_annual), 2);

                                //if (hts.T_fixedsalary == 0 && decOld != hrBaseInfo.SickleaveDay )
                                //    hts.T_fixedsalary = 1;  
                            }
                            else
                            {
                                if (hts.T_department > 0)
                                {
                                    if (Convert.ToDecimal(sdrReader["coef"]) > 0)
                                        hts.T_fixedsalary = Math.Round(Convert.ToDecimal(htbDepart[hts.T_department]) * (hts.T_attday + hts.T_annual) * Convert.ToDecimal(sdrReader["coef"]), 2);
                                    else
                                        hts.T_fixedsalary = Math.Round(Convert.ToDecimal(htbDepart[hts.T_department]) * (hts.T_attday + hts.T_annual), 2);

                                    //if (hts.T_fixedsalary == 0 && decOld != hrBaseInfo.SickleaveDay)
                                    //    hts.T_fixedsalary = 1;  
                                }
                                //else
                                //    hts.T_fixedsalary = 1;
                            }

                            if (hts.T_fixedsalary != 1)
                            {
                                if ((Convert.ToDecimal(sdrReader["merrageDays"]) + Convert.ToDecimal(sdrReader["funeralDays"]) + Convert.ToDecimal(sdrReader["injuryDays"])) != hrBaseInfo.SickleaveDay)
                                    hts.T_fixedsalary = hts.T_fixedsalary + Math.Round(decLeaveDay * hrBaseInfo.BasicPrice, 1);

                            }
                        }

                        if ((Convert.ToDecimal(sdrReader["merrageDays"]) + Convert.ToDecimal(sdrReader["funeralDays"]) + Convert.ToDecimal(sdrReader["injuryDays"])) == hrBaseInfo.SickleaveDay)
                            hts.T_fixedsalary = hrBaseInfo.BasicWage;
                    }
                    //-----End --------//

                    if (hts.T_fixedsalary > 0)  //Ӧ�����ܴ���
                    {
                        //descLeavePay = Math.Round((hts.T_leave + hts.T_sleave) * Math.Round(hts.T_fixedsalary / hrBaseInfo.AvgDays, 2), 1);
                        //�̶����ʵ����½�Ա��      price =�̶����� / ��ƽ����

                        if (hts.T_attendance + hts.T_annual + hts.T_other >= hrBaseInfo.FixedDays)
                        {
                            if (hts.T_fixedsalary - descLeavePay > hts.T_basic + decOver)
                            {
                                decBasic = 0;
                                //�����������
                                if (hts.T_basic < hrBaseInfo.BasicWage)
                                {
                                    decBasic = hrBaseInfo.BasicWage - hts.T_basic;
                                    hts.T_basic = hrBaseInfo.BasicWage;
                                }

                                if (hts.T_fixedsalary - descLeavePay > hts.T_basic + decOver)
                                {
                                    hts.T_benefit = hts.T_fixedsalary - descLeavePay - hts.T_basic - decOver;
                                }
                                else
                                {
                                    decOver = hts.T_fixedsalary - descLeavePay - hts.T_basic;
                                    if (decOver < 0)
                                        decOver = 0;
                                }

                            }
                            else
                            {
                                if (hts.T_fixedsalary < hrBaseInfo.BasicWage) //�̶����ʲ����������
                                {
                                    hts.T_basic = hts.T_fixedsalary;
                                    decOver = 0;
                                }
                                else
                                {
                                    decOver = hts.T_fixedsalary - descLeavePay - hts.T_basic;
                                    if (decOver < 0)
                                        decOver = 0;
                                }
                            }
                        }
                        else
                        {
                            //bolflag��B\C��ƽ�����ʵ���
                            if (bolflag)
                            {

                                if (hts.T_fixedsalary < hrBaseInfo.BasicWage)
                                {
                                    hts.T_basic = hts.T_fixedsalary;
                                    decOver = 0;
                                }
                                else
                                {
                                    hts.T_basic = hrBaseInfo.BasicWage;
                                    decOver = hts.T_fixedsalary - hrBaseInfo.BasicWage;
                                }
                            }
                            else
                            {

                                decBasic = Math.Round((hts.T_attendance + hts.T_annual + decLeaveDay) * Math.Round(hts.T_fixedsalary / hrBaseInfo.FixedDays, 2), 1);
                                if (hts.T_userType == 395 || hts.T_userType == 396)
                                {
                                    if ((Convert.ToDecimal(sdrReader["merrageDays"]) + Convert.ToDecimal(sdrReader["funeralDays"]) + Convert.ToDecimal(sdrReader["injuryDays"])) != hrBaseInfo.SickleaveDay)
                                    {
                                        decBasic += Math.Round(decLeaveDay * hrBaseInfo.BasicPrice, 1);
                                    }
                                }
                                if (decBasic > decFixedsalary)
                                {
                                    decBasic = decFixedsalary;
                                }

                                decOver = 0;
                                if (decBasic >= hrBaseInfo.BasicWage)
                                {
                                    hts.T_benefit = decBasic - hrBaseInfo.BasicWage;
                                    decBasic = hrBaseInfo.BasicWage;
                                }

                                //������Ա
                                if (hts.T_fixedsalary <= hrBaseInfo.BasicWage && hts.T_attendance == 0)
                                {
                                    decBasic = hts.T_fixedsalary;
                                }

                                hts.T_basic = decBasic;
                            }
                        }

                    }


                    //��B��C�࣬����ϵ��
                    if (hts.T_userType != 395 && hts.T_userType != 396 && Convert.ToDecimal(sdrReader["coef"]) > 0)
                        hts.T_benefit = hts.T_benefit + Math.Round(hts.T_basic * Convert.ToDecimal(sdrReader["coef"]), 2) - hts.T_basic;
                    //hts.T_benefit = hts.T_benefit + Math.Round((hts.T_basic + decOver) * Convert.ToDecimal(sdrReader["coef"]), 2) - hts.T_basic - decOver;

                    hts.T_benefit = hts.T_benefit + Convert.ToDecimal(sdrReader["SalaryAdjust"]); //��ʱ��Ч��

                    hts.T_assess = decOver; // �Ӱ��


                    //----------ȫ�ڽ���������--------------------//
                    //if (hts.T_fire == false || Convert.ToDateTime(sdrReader["enterdate"]) < Convert.ToDateTime(hts.T_salaryDate))
                    //    hts.T_fullattendence = Attbonus(hts.T_userType, hts.T_other  + hts.T_leave + hts.T_maternityDays + hts.T_sleave, "", Convert.ToInt32(sdrReader["belate"]), hrBaseInfo.MaxAttbonus, hrBaseInfo.MinAttbonus);
                    //else
                    //    hts.T_fullattendence = 0;
                    hts.T_fullattendence = Convert.ToDecimal(sdrReader["allatt"]);
                    hts.T_lengService = Convert.ToDecimal(sdrReader["wy"]);
                    //-------------------------------------//

                    //���¼Ƽ�����ת��ʱ-----------------
                    //��hr_fin_mstr�У�������˰�����
                    if (Convert.ToDecimal(sdrReader["spiece"]) > 0)
                    {
                        if (hts.T_basic + Convert.ToDecimal(sdrReader["spiece"]) < hrBaseInfo.BasicWage)
                        {
                            hts.T_basic = hts.T_basic + Convert.ToDecimal(sdrReader["spiece"]);
                        }
                        else
                        {
                            hts.T_benefit = hts.T_benefit + Convert.ToDecimal(sdrReader["spiece"]) + hts.T_basic - hrBaseInfo.BasicWage;
                            hts.T_basic = hrBaseInfo.BasicWage;
                        }

                    }
                    //-----------------------------//

                    //Ӧ�����
                    //���ϵ��ݵ�
                    hts.T_restTimeMoney = Convert.ToDecimal(sdrReader["rt_money"]);
                    hts.T_assess += hts.T_restTimeMoney;

                    //By Shanzm 2015-01-21:����ֽ��������ʣ�1270����ʵ�ʻ������ʣ�1480����һ��
                    if (hrBaseInfo.BasicWageNew > 0 && hrBaseInfo.BasicWageNew < hts.T_basic)
                    {
                        hts.T_assess += hts.T_basic - hrBaseInfo.BasicWageNew;
                        hts.T_basic = hrBaseInfo.BasicWageNew;
                    }

                    decWork = hts.T_basic + hts.T_assess + hts.T_benefit + hts.T_allowance + hts.T_subsidies + hts.T_holiday + hts.T_nightWork + hts.T_fullattendence + hts.T_lengService + hts.T_maternityPay + hts.T_sleavepay;

                    // �Ϻ���Ҫ����������ʣ��۳��籣�͸��·ѡ�������Ů�ѵĻ����ϣ�
                    if ((intPlantcode == 1) && (decWork - hts.T_hfound - hts.T_rfound - hts.t_HightTemp - hts.t_Allother < hrBaseInfo.BasicWage) && (hts.T_fire == false) && (decOld == 0) && (blNewUser == false) && hts.T_leave == 0)
                    {
                        decimal subsidies = hrBaseInfo.BasicWage - decWork + hts.T_hfound + hts.T_rfound + hts.t_HightTemp + hts.t_Allother;
                        hts.T_subsidies += subsidies;
                        decWork = decWork + subsidies;
                    }

                    //�ۿ�
                    decimal decRate, decRateDeduct, decDeduct;
                    decDeduct = Convert.ToDecimal(sdrReader["deduct"]) + Convert.ToDecimal(sdrReader["remaindeduct"]);
                    hts.T_lastdeduct = Convert.ToDecimal(sdrReader["remaindeduct"]); //������ۿ�

                    //-----------��������-----------//
                    decRate = Convert.ToDecimal(sdrReader["rate"]);
                    if (decRate == 0)
                        decRateDeduct = 0 - Convert.ToDecimal(sdrReader["mark"]) * Convert.ToDecimal(sdrReader["drate"]);
                    else
                    {
                        if (decRate > 0 && decRate < 100)
                            decRateDeduct = 0 - Convert.ToDecimal(sdrReader["mark"]) * decRate;
                        else
                            decRateDeduct = (decRate - 100 - Convert.ToDecimal(sdrReader["mark"])) * 0.003M;
                    }

                    if (decRate > 100)
                        decRateDeduct = hrBaseInfo.BasicWage * decRateDeduct;


                    if (decRateDeduct < 0)
                        decDeduct = decDeduct - decRateDeduct;
                    else
                        hts.T_allowance = hts.T_allowance + decRateDeduct;

                    hts.T_Rate = decRateDeduct;
                    //------------End -----------------//

                    //���ʿɿ۽��
                    if (decWork * hrBaseInfo.DeductRate < decDeduct)
                    {
                        hts.T_deduct = Math.Round(decWork * hrBaseInfo.DeductRate, 1);
                        hts.T_currdeduct = decDeduct - hts.T_deduct;
                    }
                    else
                    {
                        hts.T_deduct = decDeduct;
                        hts.T_currdeduct = 0;
                    }

                    //����� ///// ˰
                    if (Convert.ToBoolean(sdrReader["isLabourUnion"]))
                        hts.T_member = Math.Round(hrBaseInfo.BasicWage * hrBaseInfo.LaborRate, 1);
                    else
                        hts.T_member = 0;

                    AdjustTimeSalaryPT(hts, ref decWork);

                    hts.T_tax = Math.Round(SalaryTax(decWork - Convert.ToDecimal(sdrReader["sig"]), hts.T_deduct + hrBaseInfo.Tax + hts.T_hfound + hts.T_rfound, intPlantcode), 2);
                    decimal decContants;
                    decContants = decWork - Convert.ToDecimal(sdrReader["sig"]) - hts.T_deduct - hrBaseInfo.Tax - hts.T_hfound - hts.T_rfound;
                    if (decContants <= 1500)
                    {
                        hts.T_taxRate = 0.03M;
                        hts.T_taxDeduct = 0;
                    }
                    else
                    {
                        if (decContants <= 4500)
                        {
                            hts.T_taxRate = 0.1M;
                            hts.T_taxDeduct = 105;
                        }
                        else
                        {
                            if (decContants <= 9000)
                            {
                                hts.T_taxRate = 0.2M;
                                hts.T_taxDeduct = 555;
                            }
                            else
                            {
                                hts.T_taxRate = 0.25M;
                                hts.T_taxDeduct = 1005;
                            }
                        }
                    }
                    //-----end------------------//

                    hts.T_duereward = decWork;
                    hts.T_workpay = hts.T_duereward - hts.T_deduct - hts.T_member - hts.T_tax - hts.T_hfound - hts.T_rfound;




                    if (hts.T_workpay < 0)
                        hts.T_workpay = 0;

                    SaveTimeSalary(hts, intUserID);

                }
                sdrReader.Close();

                htbDepart.Clear();
                htbLine.Clear();
                return 0;
            //}
            //catch
            //{
            //    return -1;
            //}
        }

        protected virtual void AdjustTimeSalaryPT(hr_TimeSalary hts, ref decimal decWork)
        {

        }

        protected virtual SqlDataReader GetTimeCalculateInfoPT(int intYear, int intMonth, int intPlantcode)
        {
            string strSql = "sp_hr_GetTimeCalculateInfoPT";
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

        public int CheckHoliday(string strEnterdate, string strLeavedate, int intYear, int intMonth, int intUserID, int intPlantcode)
        {
            try
            {
                int intFlag;
                intFlag = 0;

                string strSql = "sp_hr_CheckHoliday";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlantcode);
                parmArray[3] = new SqlParameter("@Endate", strEnterdate);
                parmArray[4] = new SqlParameter("@Leavedate", strLeavedate);


                SqlDataReader sqlReader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                while (sqlReader.Read())
                {
                    if (Convert.ToDateTime(strEnterdate) < Convert.ToDateTime(sqlReader[0]))
                    {
                        if (strLeavedate.Length == 0)
                            intFlag = intFlag + 1;
                        else
                        {
                            if (Convert.ToDateTime(strLeavedate) > Convert.ToDateTime(sqlReader[0]))
                                intFlag = intFlag + 1;
                        }
                    }

                }
                sqlReader.Close();

                return intFlag;

            }
            catch
            {
                return 0;
            }
        }

        public Hashtable GetHashData(int intYear, int intMonth, int intPlant, int intType, decimal decDay, int intKind)
        {
            try
            {
                Hashtable htbtemp = new Hashtable();
                htbtemp.Clear();

                string strSql;
                if (intKind == 0)
                    strSql = "sp_hr_PieceAvgSalary";
                else
                    strSql = "sp_hr_PieceAvgSalary_BK";

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
        /// ��ȡ�߳��������ߵĹ�ϵ
        /// </summary>
        /// <param name="plantCode"></param>
        /// <returns></returns>
        public DataSet GetLineRelation(int plantCode)
        {
            try
            {
                string strSql = "sp_hr_selectBCoefMore";

                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@plantCode", plantCode);

                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable SelectTimeSalary(int intYear, int intMonth, string strUser, string strName, int intDept, int intWorkType, int PlantCode)
        {
            try
            {
                adamClass adc = new adamClass();
                string strSql = "tcpc" + PlantCode + ".dbo.sp_hr_SelectSalaryTime";
                SqlParameter[] parmArray = new SqlParameter[6];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strName);
                parmArray[4] = new SqlParameter("@Depart", intDept);
                parmArray[5] = new SqlParameter("@Worktype", intWorkType);
                //string str = Convert.ToString(SqlHelper.ExecuteScalar(adc.), CommandType.StoredProcedure, strSql, parmArray));
                //return null;
                return SqlHelper.ExecuteDataset(adc.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;

            }

        }

        public string ExportSalaryTime(int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_ExportSalaryTime";
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


        public int AdjustSalaryTimeSave(int intYear, int intMonth, int intPlantcode, int intDepartment, int intWorktype, string strUserID, decimal decAdjPercent, decimal decAdjMoney, int intOperateID, int intSalaryID, string strReason)
        {
            try
            {
                string strSql = "sp_hr_AdjustSalarySaveTime";
                SqlParameter[] parmArray = new SqlParameter[11];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Plantcode", intPlantcode);
                parmArray[3] = new SqlParameter("@department", intDepartment);
                parmArray[4] = new SqlParameter("@worktype", intWorktype);
                parmArray[5] = new SqlParameter("@UserID", strUserID);
                parmArray[6] = new SqlParameter("@percent", decAdjPercent);
                parmArray[7] = new SqlParameter("@money", decAdjMoney);
                parmArray[8] = new SqlParameter("@operateID", intOperateID);
                parmArray[9] = new SqlParameter("@SalaryID", intSalaryID);
                parmArray[10] = new SqlParameter("@reason", strReason);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
                //string str = Convert.ToString (SqlHelper.ExecuteScalar (adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray));
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public string SalaryTimeUserCheck(string strUser, int intPlantcode, int intYear, int intMonth, int intOperateID)
        {
            try
            {
                string strSql = "sp_hr_SalaryTimeUserCheck";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Plantcode", intPlantcode);
                parmArray[3] = new SqlParameter("@UserNo", strUser);
                parmArray[4] = new SqlParameter("@OperateID", intOperateID);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }

        public string AdjustSalaryTime(int intYear, int intMonth, int intPlantcode, int intDepartment, int intWorktype, string strUserNo, string strUserName, int intOperateID, int intType)
        {
            try
            {
                string strSql = "sp_hr_AdjustSalaryTimeSelect";
                SqlParameter[] parmArray = new SqlParameter[9];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Plantcode", intPlantcode);
                parmArray[3] = new SqlParameter("@department", intDepartment);
                parmArray[4] = new SqlParameter("@worktype", intWorktype);
                parmArray[5] = new SqlParameter("@Userno", strUserNo);
                parmArray[6] = new SqlParameter("@Username", strUserName);
                parmArray[7] = new SqlParameter("@OperateID", intOperateID);
                parmArray[8] = new SqlParameter("@Type", intType);
                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray).ToString();
            }
            catch
            {
                return "";
            }
        }

        public static DataTable AdjustSalaryTimeSelect(int intYear, int intMonth, int intPlantcode, int intDepartment, int intWorktype, string strUserNo, string strUserName, int intOperateID)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, hr_salary.AdjustSalaryTime(intYear, intMonth, intPlantcode, intDepartment, intWorktype, strUserNo, strUserName, intOperateID, 0)).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        public int AdjustToFinacialTime(int intYear, int intMonth, int intOperateID)
        {
            try
            {
                string strSql = "sp_hr_SalaryToFinanceTime";
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

        public string SalaryCC(int intYear, int intMonth, int intPlantCode, int intType)
        {
            try
            {

                string strSql = "sp_hr_BtypeSalary";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Plantcode", intPlantCode);
                parmArray[3] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));

            }
            catch
            {
                return null;
            }

        }


        public string ExportFin_SalaryTime(int intYear, int intMonth, int intType, int intPlant)
        {
            try
            {
                string strSql = "sp_hr_ExportFin_SalaryTime";
                SqlParameter[] parmArray = new SqlParameter[4];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Type", intType);
                parmArray[3] = new SqlParameter("@plantCode", intPlant);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return null;
            }
        }
        #endregion  //Time Salary

        #region ����˰
        public int AdjustTimeSalry(int intYear, int intMonth, int intPlant, int intCreat)
        {
            try
            {
                HR_BaseInfo hrbi = SelectHRBaseInfo(intYear, intMonth);
                decimal basicWage = hrbi.BasicWageNew > 0 ? hrbi.BasicWageNew : hrbi.BasicWage;
                string strSql = "sp_hr_TimeAdjustTax";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[1] = new SqlParameter("@Year", intYear);
                parmArray[2] = new SqlParameter("@Month", intMonth);
                parmArray[3] = new SqlParameter("@PlantCode", intPlant);
                parmArray[4] = new SqlParameter("@Creat", intCreat);

                SqlDataReader sdtr = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                decimal decSalary, decWorkpay, decOver, decBasic, decDeduct, decCurrent, decTax, decadjust, decAccess, decReplace, decSub;

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

                    decadjust = Math.Round((Convert.ToDecimal(sdtr[1]) + Convert.ToDecimal(sdtr[2]) + Convert.ToDecimal(sdtr[14])) * Convert.ToDecimal(sdtr[10]) / 100 + Convert.ToDecimal(sdtr[11]), 2);

                    decReplace = decadjust;

                    if (intPlant == 1 && Convert.ToDecimal(sdtr[15]) > 0)
                    {
                        if (decReplace > 0)
                        {
                            if (decReplace > Convert.ToDecimal(sdtr[15]))
                            {
                                decReplace = decReplace - Convert.ToDecimal(sdtr[15]);
                                //decSub = Convert.ToDecimal(sdtr[16]) - Convert.ToDecimal(sdtr[15]);
                            }
                            else
                            {
                                decReplace = 0;
                                decSub = Convert.ToDecimal(sdtr[16]);
                            }
                        }
                        else
                        {
                            decReplace = 0;
                            decSub = Convert.ToDecimal(sdtr[16]);
                        }
                    }

                    if (decReplace >= 0)
                    {
                        if (decReplace + Convert.ToDecimal(sdtr[1]) < basicWage)
                        {
                            decBasic = decReplace + Convert.ToDecimal(sdtr[1]);
                            decOver = Convert.ToDecimal(sdtr[2]);
                            decAccess = Convert.ToDecimal(sdtr[14]);
                        }
                        else
                        {
                            decBasic = basicWage;
                            decAccess = Convert.ToDecimal(sdtr[14]) + decReplace + Convert.ToDecimal(sdtr[1]) - decBasic;
                            decOver = Convert.ToDecimal(sdtr[2]);
                        }
                    }
                    else
                    {
                        if (decReplace + Convert.ToDecimal(sdtr[14]) >= 0)
                        {
                            decBasic = Convert.ToDecimal(sdtr[1]);
                            decOver = Convert.ToDecimal(sdtr[2]);
                            decAccess = decReplace + Convert.ToDecimal(sdtr[14]);
                        }
                        else
                        {
                            decAccess = 0;
                            if (decReplace + Convert.ToDecimal(sdtr[14]) + Convert.ToDecimal(sdtr[2]) > 0)
                            {
                                decBasic = Convert.ToDecimal(sdtr[1]);
                                decOver = decReplace + Convert.ToDecimal(sdtr[2]) + Convert.ToDecimal(sdtr[14]);
                            }
                            else
                            {
                                decOver = 0;
                                decBasic = decReplace + Convert.ToDecimal(sdtr[14]) + Convert.ToDecimal(sdtr[2]) + Convert.ToDecimal(sdtr[1]);
                                if (decBasic < 0)
                                    decBasic = 0;
                            }
                        }

                    }


                    decSalary = Convert.ToDecimal(sdtr[3]) + decReplace;

                    if (decSalary - Convert.ToDecimal(sdtr[13]) > 0)
                    {
                        decTax = Math.Round(SalaryTax(decSalary - Convert.ToDecimal(sdtr[13]), Convert.ToDecimal(sdtr[8]) + hrbi.Tax + Convert.ToDecimal(sdtr[5]) + Convert.ToDecimal(sdtr[6]), intPlant), 2);
                        decWorkpay = decSalary - decTax - Convert.ToDecimal(sdtr[5]) - Convert.ToDecimal(sdtr[6]) - Convert.ToDecimal(sdtr[7]) - Convert.ToDecimal(sdtr[8]);

                        if (decWorkpay < 0)
                            decWorkpay = 0;
                    }


                    SaveTimeAdjustTax(Convert.ToInt32(sdtr[0]), decSalary, decWorkpay, decBasic, decOver, decDeduct, decCurrent, decTax, decadjust, intCreat, intPlant, decAccess, decSub);

                }
                sdtr.Close();
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public void SaveTimeAdjustTax(int intID, decimal decSalary, decimal decWorkpay, decimal decBasic, decimal decOver, decimal decDeduct, decimal decCurrent, decimal decTax, decimal decadjust, int intCreat, int intPlant, decimal decAccess, decimal decSub)
        {
            try
            {
                string strSql = "sp_hr_SaveTimeAdjustTax";
                SqlParameter[] parmArray = new SqlParameter[14];
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
                parmArray[12] = new SqlParameter("@Access", decAccess);
                parmArray[13] = new SqlParameter("@Subsible", decSub);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            }
            catch
            {

            }
        }


        public int AdjustPieceSalary(int intYear, int intMonth, int intPlant, int intCreat)
        {
            try
            {
                HR_BaseInfo hrbi = SelectHRBaseInfo(intYear, intMonth);

                string strSql = "sp_hr_pieceAdjustTax";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[1] = new SqlParameter("@Year", intYear);
                parmArray[2] = new SqlParameter("@Month", intMonth);
                parmArray[3] = new SqlParameter("@PlantCode", intPlant);
                parmArray[4] = new SqlParameter("@Creat", intCreat);

                SqlDataReader sdtr = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                decimal decSalary, decWorkpay, decOver, decBasic, decDeduct, decCurrent, decTax, decadjust, decReplace, decSub, hrbiBasicWage;

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
                    hrbiBasicWage = hrbi.BasicWageNew > 0 ? hrbi.BasicWageNew : hrbi.BasicWage;


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
                        if (decReplace + Convert.ToDecimal(sdtr[1]) < hrbiBasicWage)
                        {
                            decBasic = decReplace + Convert.ToDecimal(sdtr[1]);
                            decOver = Convert.ToDecimal(sdtr[2]);
                        }
                        else
                        {
                            decBasic = hrbiBasicWage;
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

                    SaveAdjustTax(Convert.ToInt32(sdtr[0]), decSalary, decWorkpay, decBasic, decOver, decDeduct, decCurrent, decTax, decadjust, intCreat, intPlant, decSub);

                }
                sdtr.Close();
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public void SaveAdjustTax(int intID, decimal decSalary, decimal decWorkpay, decimal decBasic, decimal decOver, decimal decDeduct, decimal decCurrent, decimal decTax, decimal decadjust, int intCreat, int intPlant, decimal decSub)
        {
            try
            {
                string strSql = "sp_hr_SaveAdjustTax";
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



        #endregion

        #region Hr_Finance

        public string WorkOrderCompare(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_WorkCostCompare";
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


        public static DataTable SelectWorkCostCompare(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                string strSql = hr_salary.WorkOrderCompare(strUser, strName, intDept, PlantCode, intYear, intMonth, intType);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// ��ȡtcpc0�����ƴ�����
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intBank"></param>
        /// <param name="intPlantCode"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public string FinancetoBank(int intYear, int intMonth, int intBank, int intPlantCode, int intType)
        {
            try
            {
                string strSql = "sp_hr_FinancetoBank";
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
        /// ��ȡtcpcx����ķ������еĹ����б�DataTable��
        /// </summary>
        /// <param name="intYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intBank"></param>
        /// <returns></returns>
        public DataTable FinancetoBank(int intYear, int intMonth, int intBank)
        {
            try
            {
                string strSql = "sp_hr_FinancetoBank";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@Bank", intBank);
                return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public string FinanceSalary(int intYear, int intMonth, int intPlantCode, int intType)
        {
            try
            {
                string strSql = "sp_hr_FinanceSalary";
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

        public string FinanceSalarySummary(int intYear, int intMonth, int intPlantCode, int intType)
        {
            try
            {
                string strSql = "sp_hr_FinanceSalarySummary";
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

        public int FinanceComfirm(int intYear, int intMonth, int intPlantCode, int intUserID, int intType)
        {
            try
            {
                string strSql = "sp_hr_FinanceSalaryConfirm";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlantCode);
                parmArray[3] = new SqlParameter("@UserID", intUserID);
                parmArray[4] = new SqlParameter("@Type", intType);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region NoSalary
        public string NoSalary(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                string strSql = "sp_hr_NoSalarySelect";
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


        public static DataTable NoSalarySelect(string strUser, string strName, int intDept, int PlantCode, int intYear, int intMonth, int intType)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                string strSql = hr_salary.NoSalary(strUser, strName, intDept, PlantCode, intYear, intMonth, intType);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch
            {
                return null;
            }

        }

        public bool SalaryCheckDPAtt(int intYear, int intMonth, int intPlant)
        {
            try
            {
                string strSql = "sp_hr_CheckDPAtt";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@PlantCode", intPlant);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ���ٿ���
        public int SaveHolidayData(int intUserID, string strUserNo, string strUserName, string strHolidayDate, decimal decHours, decimal cost, int intPlant, int intCreat, int intType)
        {
            try
            {
                string strSql = "sp_hr_holidayAttSave";
                SqlParameter[] parmArray = new SqlParameter[9];
                parmArray[0] = new SqlParameter("@UserID", intUserID);
                parmArray[1] = new SqlParameter("@UserNo", strUserNo);
                parmArray[2] = new SqlParameter("@UserName", strUserName);
                parmArray[3] = new SqlParameter("@Holidaydate", strHolidayDate);
                parmArray[4] = new SqlParameter("@Hours", decHours);
                parmArray[5] = new SqlParameter("@cost", cost);
                parmArray[6] = new SqlParameter("@PlantCode", intPlant);
                parmArray[7] = new SqlParameter("@Creat", intCreat);
                parmArray[8] = new SqlParameter("@Type", intType);

                if (intType == 0)
                {
                    SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);
                    return 1;
                }
                else
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return -1;
            }
        }

        public static DataTable HolidayAttSelect(int intYear, int intMonth, int intDept, string strUser, string strUserName, int intPlant, int intCreat)
        {
            try
            {
                adamClass adc = new adamClass();
                HR hr_salary = new HR();
                string strSql = hr_salary.HolidayAtt(intYear, intMonth, intDept, strUser, strUserName, intPlant, 0, intCreat);
                return SqlHelper.ExecuteDataset(adc.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public string HolidayAtt(int intYear, int intMonth, int intDept, string strUser, string strUserName, int intPlant, int intType, int intCreat)
        {
            try
            {
                string strSql = "sp_hr_holidayAtt";
                SqlParameter[] parmArray = new SqlParameter[8];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@UserNo", strUser);
                parmArray[3] = new SqlParameter("@UserName", strUserName);
                parmArray[4] = new SqlParameter("@Depart", intDept);
                parmArray[5] = new SqlParameter("@PlantCode", intPlant);
                parmArray[6] = new SqlParameter("@UserID", intCreat);
                parmArray[7] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray));
            }
            catch
            {
                return "";
            }
        }

        public int DelHolidayAtt(int intplantcode, int hr_holiday_id)
        {
            try
            {
                string str = "DELETE FROM tcpc" + Convert.ToString(intplantcode) + ".dbo.hr_holiday_att WHERE hr_holiday_id =" + Convert.ToString(hr_holiday_id);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region ���ڻ����
        /// <summary>
        /// ���ڻ������Ƿ����
        /// </summary>
        /// <param name="strCard">����</param>
        /// <returns>True/False</returns>
        public bool chkCardNumIsExist(string strCard)
        {
            try
            {
                string strSql = "sp_hr_CheckCardNoIsExist";
                SqlParameter sqlParam = new SqlParameter("@card", strCard);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ���¿���
        /// </summary>
        /// <param name="strCardSource">ԭʼ����</param>
        /// <param name="strCardTarget">����󿨺�</param>
        /// <returns>True/False</returns>
        public bool UpdateCardNum(string strCardSource, string strCardTarget)
        {
            try
            {
                string strSql = "sp_hr_UpdateCardNum";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@source", strCardSource);
                sqlParam[1] = new SqlParameter("@target", strCardTarget);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ɾ�����п��ڻ�Ա����Ϣ
        /// </summary>
        /// <param name="intPlantID">Session["orgID"]</param>
        /// <returns>True/False</returns>
        public bool DeleteBIOUserInfo(int intPlantID)
        {
            try
            {
                string strSql = "sp_hr_DeleteBIOUserInfo";
                SqlParameter sqlParam = new SqlParameter("@plantid", intPlantID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�û���Ӧ�Ŀ���
        /// </summary>
        /// <param name="intUserID">UserID</param>
        /// <param name="intPlantID">Session["uID"]</param>
        /// <returns>Cardnum</returns>
        public string GetCardNum(int intUserID, int intPlantID)
        {
            try
            {
                string strSql = "sp_hr_GetUserCardNum";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@uid", intUserID);
                sqlParam[1] = new SqlParameter("@plantid", intPlantID);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ����ʱ��������
        /// </summary>
        /// <param name="intUserID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool InsertAttendanceInfo(int intUserID)
        {
            try
            {
                string strSql = "sp_hr_InsertAttendanceInfo";
                SqlParameter sqlParam = new SqlParameter("@uid", intUserID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// �ж��Ƿ������ͬ�ļ�¼
        /// </summary>
        /// <param name="strSensor">�豸��</param>
        /// <param name="strCardnum">����</param>
        /// <param name="strType">����</param>
        /// <param name="strDate">��������</param>
        /// <returns>True/False</returns>
        public bool chkRecordIsExist(string strSensor, string strCardnum, string strType, DateTime Date)
        {
            try
            {
                string strSql = "sp_hr_chkRecordIsExist";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@sensor", strSensor);
                sqlParam[1] = new SqlParameter("@cardnum", strCardnum);
                sqlParam[2] = new SqlParameter("@type", strType);
                sqlParam[3] = new SqlParameter("@date", Date);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return true;
            }
        }

        /// <summary>
        /// ��ȡ�ֹ����뿼����Ϣ
        /// </summary>
        /// <param name="strUserNo">Ա������</param>
        /// <param name="strType">��������</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>�����ֹ����뿼����Ϣ</returns>
        public IList<HR_AttendanceInfo> SelectAttendanceInfo(string strUserNo, string strType, string strPlant, string strDate, string strUserType, string strCenter)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceInfo";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@userno", strUserNo);
                sqlParam[1] = new SqlParameter("@type", strType);
                sqlParam[2] = new SqlParameter("@plant", strPlant);
                sqlParam[3] = new SqlParameter("@date", strDate);
                sqlParam[4] = new SqlParameter("@usertype", strUserType);
                sqlParam[5] = new SqlParameter("@userCenter", strCenter);

                IList<HR_AttendanceInfo> HR_AttendanceInfo = new List<HR_AttendanceInfo>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    HR_AttendanceInfo hr_ai = new HR_AttendanceInfo();
                    hr_ai.AttendanceID = Convert.ToInt32(reader["AttendanceID"]);
                    hr_ai.AttendanceUserNo = reader["AttendanceUserNo"].ToString();
                    hr_ai.AttendanceTime = Convert.ToDateTime(reader["AttendanceTime"]);
                    hr_ai.AttendanceType = reader["AttendanceType"].ToString();
                    hr_ai.Center = reader["Center"].ToString();
                    hr_ai.CreatedBy = Convert.ToInt32(reader["ImportedBy"]);
                    hr_ai.CreatedDate = Convert.ToDateTime(reader["ImportedDate"]);
                    hr_ai.Sensor = reader["Sensor"].ToString();
                    hr_ai.IsManual = Convert.ToBoolean(reader["isManual"]);
                    hr_ai.UserType = reader["UserType"].ToString();
                    HR_AttendanceInfo.Add(hr_ai);
                }
                reader.Close();
                return HR_AttendanceInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ��ȡ���ڻ���Ӧ�ĳɱ�����
        /// </summary>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>���ؿ��ڻ���Ӧ�ĳɱ�����</returns>
        public DataTable SelectAttendanceCenter(string strPlant)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceCenter";
                SqlParameter sqlParam = new SqlParameter("@plant", strPlant);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// �ж�Ա�������п��ڿ��Ż�ָ�Ʊ�ŵǼ�
        /// </summary>
        /// <param name="strUserNo">Ա������</param>
        /// <returns>True/False</returns>
        public bool CheckUserIsReg(string strUserNo)
        {
            try
            {
                string strSql = "sp_hr_CheckUserIsReg";
                SqlParameter sqlParam = new SqlParameter("@userno", strUserNo);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// �����ֹ�����Ŀ�����Ϣ
        /// </summary>
        /// <param name="hr_ai">HR_AttendanceInfo</param>
        /// <returns>True/False</returns>
        public bool InsertAttendanceInfoManual(HR_AttendanceInfo hr_ai)
        {
            try
            {
                string strSql = "sp_hr_InsertAttendanceInfoManual";
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@userno", hr_ai.AttendanceUserNo);
                sqlParam[1] = new SqlParameter("@type", hr_ai.AttendanceType);
                sqlParam[2] = new SqlParameter("@plant", hr_ai.orgID);
                sqlParam[3] = new SqlParameter("@date", hr_ai.AttendanceTime);
                sqlParam[4] = new SqlParameter("@sensor", hr_ai.Sensor);
                sqlParam[5] = new SqlParameter("@isManual", hr_ai.IsManual);
                sqlParam[6] = new SqlParameter("@uid", hr_ai.CreatedBy);
                sqlParam[7] = new SqlParameter("@center", hr_ai.Center);
                sqlParam[8] = new SqlParameter("@name", hr_ai.CenterName);
                sqlParam[9] = new SqlParameter("@usertype", hr_ai.UserTypeID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ɾ���ֹ�����Ŀ���������Ϣ
        /// </summary>
        /// <param name="strAID">AttendanceID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool DeleteAttendanceInfoManual(string strAID, string strUID)
        {
            try
            {
                string strSql = "sp_hr_DeleteAttendanceInfoManual";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@aid", Convert.ToInt32(strAID));
                sqlParam[1] = new SqlParameter("@uid", Convert.ToInt32(strUID));

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ���ڻ���Ӧ�ĳɱ����Ķ����б�
        /// </summary>
        /// <param name="strCenter">�ɱ�����</param>
        /// <param name="strSensor">�����豸��</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns></returns>
        public IList<HR_AttendanceCenter> SelectAttendanceCenter(string strCenter, string strSensor, string strPlant, string strUserNo)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceCenterInfo";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@center", strCenter);
                sqlParam[1] = new SqlParameter("@sensor", strSensor);
                sqlParam[2] = new SqlParameter("@plant", strPlant);
                sqlParam[3] = new SqlParameter("@userno", strUserNo);

                IList<HR_AttendanceCenter> AttendanceCenter = new List<HR_AttendanceCenter>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    HR_AttendanceCenter hr_ac = new HR_AttendanceCenter();
                    hr_ac.CenterID = Convert.ToInt32(reader["CenterID"]);
                    hr_ac.CenterCode = reader["CenterCode"].ToString();
                    hr_ac.CenterName = reader["CenterName"].ToString();
                    hr_ac.Center = reader["Center"].ToString();
                    hr_ac.Sensor = reader["Sensor"].ToString();
                    hr_ac.orgID = Convert.ToInt32(reader["orgID"]);
                    hr_ac.UserNo = reader["UserNo"].ToString();
                    AttendanceCenter.Add(hr_ac);
                }
                reader.Close();
                return AttendanceCenter;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ���ӳɱ����Ķ�Ӧ���ڻ���Ϣ
        /// </summary>
        /// <param name="hr_ai">HR_AttendanceCenter</param>
        /// <returns>True/False</returns>
        public bool InsertAttendanceCenter(HR_AttendanceCenter hr_ac)
        {
            try
            {
                string strSql = hr_ac.UserNo.Length == 0 ? "sp_hr_InsertAttendanceCenter" : "sp_hr_InsertAttendanceCenterUser";
                SqlParameter[] sqlParam = hr_ac.UserNo.Length == 0 ? new SqlParameter[4] : new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@code", hr_ac.CenterCode);
                sqlParam[1] = new SqlParameter("@name", hr_ac.CenterName);
                sqlParam[2] = new SqlParameter("@plant", hr_ac.orgID);
                sqlParam[3] = new SqlParameter("@sensor", hr_ac.Sensor);
                if (hr_ac.UserNo.Length > 0)
                {
                    sqlParam[4] = new SqlParameter("@userno", hr_ac.UserNo);
                    sqlParam[5] = new SqlParameter("@userid", hr_ac.UserID);
                }

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// �жϳɱ�������QAD�Ƿ����
        /// </summary>
        /// <param name="strCenter">�ɱ����Ĵ���</param>
        /// <returns>�ɱ�����˵��</returns>
        public string CheckCenterQADIsExist(string strCenter, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_CheckCenterQADIsExist";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@code", strCenter);
                sqlParam[1] = new SqlParameter("@plant", Convert.ToInt32(strPlant));

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// �ж���ؼ�¼�Ƿ����
        /// </summary>
        /// <param name="strCenter">�ɱ����Ĵ���</param>
        /// <param name="strSensor">���ڻ��豸��</param>
        /// <returns>�ɱ�����˵��</returns>
        public bool CheckRecordIsExist(string strCenter, string strSensor, string strPlant, string strUserNo)
        {
            try
            {
                string strSql = strUserNo.Length == 0 ? "sp_hr_CheckRecordIsExist" : "sp_hr_CheckRecordIsExistUser";
                SqlParameter[] sqlParam = strUserNo.Length == 0 ? new SqlParameter[3] : new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@code", strCenter);
                sqlParam[1] = new SqlParameter("@sensor", strSensor);
                sqlParam[2] = new SqlParameter("@plant", Convert.ToInt32(strPlant));
                if (strUserNo.Length > 0) sqlParam[3] = new SqlParameter("@userno", strUserNo);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ɾ���ɱ����Ķ�Ӧ���ڻ���Ϣ
        /// </summary>
        /// <param name="strCID">CenterID</param>
        /// <returns>True/False</returns>
        public bool DeleteAttendanceCenter(string strCID)
        {
            try
            {
                string strSql = "sp_hr_DeleteAttendanceCenter";
                SqlParameter sqlParam = new SqlParameter("@cid", Convert.ToInt32(strCID));

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ���п�����Ϣ
        /// </summary>
        /// <param name="strUserNo">Ա������</param>
        /// <param name="strType">��������</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>�������п�����Ϣ</returns>
        public IList<HR_AttendanceInfo> SelectAttendanceAllInfo(string strUserNo, string strType, string strPlant, string strDate, string strUserType, string strCenter)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceAllInfo";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@userno", strUserNo);
                sqlParam[1] = new SqlParameter("@type", strType);
                sqlParam[2] = new SqlParameter("@plant", strPlant);
                sqlParam[3] = new SqlParameter("@date", strDate);
                sqlParam[4] = new SqlParameter("@usertype", strUserType);
                sqlParam[5] = new SqlParameter("@userCenter", strCenter);

                IList<HR_AttendanceInfo> HR_AttendanceInfo = new List<HR_AttendanceInfo>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    HR_AttendanceInfo hr_ai = new HR_AttendanceInfo();
                    hr_ai.AttendanceID = Convert.ToInt32(reader["AttendanceID"]);
                    hr_ai.AttendanceUserNo = reader["AttendanceUserNo"].ToString();
                    hr_ai.AttendanceTime = Convert.ToDateTime(reader["AttendanceTime"]);
                    hr_ai.AttendanceType = reader["AttendanceType"].ToString();
                    hr_ai.Center = reader["Center"].ToString();
                    hr_ai.CreatedBy = Convert.ToInt32(reader["ImportedBy"]);
                    hr_ai.CreatedDate = Convert.ToDateTime(reader["ImportedDate"]);
                    hr_ai.Sensor = reader["Sensor"].ToString();
                    hr_ai.IsManual = Convert.ToBoolean(reader["isManual"]);
                    hr_ai.UserType = reader["UserType"].ToString();
                    HR_AttendanceInfo.Add(hr_ai);
                }
                reader.Close();
                return HR_AttendanceInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// �ж�Ա���Ƿ���Ч
        /// </summary>
        /// <param name="strUserNo">Ա������</param>
        /// <param name="strPlant">������˾</param>
        /// <returns>UserID</returns>
        public string CheckUserIsValid(string strUserNo, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_CheckUserIsValid";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@userno", strUserNo);
                sqlParam[1] = new SqlParameter("@plant", Convert.ToInt32(strPlant));

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// �ж�Ա���Ƿ���Ч(������ְ�����ж�)
        /// </summary>
        /// <param name="strUserNo">Ա������</param>
        /// <param name="strPlant">������˾</param>
        /// <returns>UserID</returns>
        public string CheckUserIsEffective(string strUserNo, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_CheckUserIsEffective";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@userno", strUserNo);
                sqlParam[1] = new SqlParameter("@plant", Convert.ToInt32(strPlant));

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// �ж���ְԱ���Ŀ�������
        /// </summary>
        /// <param name="strUserNo"></param>
        /// <param name="strPlant"></param>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public int CheckUserAttendIsEffective(string strUserNo, string strPlant, string strDate)
        {
            try
            {
                string strSql = "sp_hr_CheckUserAttendIsEffective";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@userno", strUserNo);
                sqlParam[1] = new SqlParameter("@plant", Convert.ToInt32(strPlant));
                sqlParam[2] = new SqlParameter("@date", Convert.ToDateTime(strDate));

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -99999;
            }
        }

        // A type User
        public bool ChecklimitedUser(string strUserNo, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_CheckAtype";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@userno", strUserNo);
                sqlParam[1] = new SqlParameter("@plant", Convert.ToInt32(strPlant));
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// <summary>
        /// ��ȡԱ����������
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable SelectUserType()
        {
            try
            {
                string strSql = " Select systemCodeID, systemCodeName ";
                strSql += " From SystemCode sc ";
                strSql += " Inner Join SystemCodeType sct On sc.systemCodeTypeID = sct.systemCodeTypeID And systemCodeTypeName ='Access Type' ";
                strSql += " Order By systemCodeID ";

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="strErr">������Ϣ</param>
        /// <param name="strUID">Session["uID"]</param>
        public void InsertErrorInfo(string strErr, string strUID)
        {
            try
            {
                string strSql = " Insert into ImportError(ErrorInfo,userid) values(N'" + strErr + "','" + strUID + "')";
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        /// <summary>
        /// ���뿼���ֹ����뵼����Ϣ
        /// </summary>
        /// <param name="strCenter">�ɱ�����</param>
        /// <param name="strCode">Ա������</param>
        /// <param name="strStart">�ϰ�ʱ��</param>
        /// <param name="strEnd">�°�ʱ��</param>
        /// <param name="strUID">Session["uID"]</param>
        public void InsertAttendanceManualImportTemp(string strCenter, string strCode, string strStart, string strEnd, string strUID)
        {
            try
            {
                string strSql = " Insert Into hr_AttendanceManualImport_Temp(Center, Code, Starttime, Endtime, createdBy) ";
                strSql += "Values(@center, @code, @start, @end, @uid)";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@center", strCenter);
                sqlParam[1] = new SqlParameter("@code", strCode);
                sqlParam[2] = new SqlParameter("@start", strStart);
                sqlParam[3] = new SqlParameter("@end", strEnd);
                sqlParam[4] = new SqlParameter("@uid", strUID);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        /// <summary>
        /// �жϿ��ڵ�����ʱ�����»�������
        /// </summary>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>���»�������</returns>
        public int CheckImportYearMonth(string strUID)
        {
            try
            {
                string strSql = " Select Count(*) From ( Select Distinct Year(Starttime), Month(Starttime) From hr_AttendanceManualImport_Temp Where createdBy = @uid) ";
                SqlParameter sqlParam = new SqlParameter("@uid", strUID);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -1;
            }
        }

        /// <summary>
        /// ����ʱ���������
        /// </summary>
        /// <param name="strUID">Session["uID"]</param>
        /// <param name="strPlant">������˾</param>
        /// <returns>Int</returns>
        public int ImportAttendanceManual(string strUID, string struName, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_InsertAttendanceInfoFromImport";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@plant", Convert.ToInt32(strPlant));
                sqlParam[1] = new SqlParameter("@uid", Convert.ToInt32(strUID));
                sqlParam[2] = new SqlParameter("@uname", struName);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -1;
            }
        }

        /// <summary>
        /// ������ʱ������
        /// </summary>
        /// <param name="createdBy">Session["uID"]</param>
        /// <param name="plant">������˾</param>
        public void CheckAttendanceManual(string plant, string createdBy)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plant", plant);
                param[1] = new SqlParameter("@createdBy", createdBy);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_hr_checkAttendanceManual", param);
            }
            catch
            {

            }
        }

        /// <summary>
        /// �ж���ʱ������ȫ��ȷ
        /// </summary>
        /// <param name="createdBy">Session["uID"]</param>
        public bool JudgeAttendanceManual(string createdBy)
        {
            try
            {
                SqlParameter param = new SqlParameter("@createdBy", createdBy);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_hr_judgeAttendanceManual", param));
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region �������ݵ���
        /// <summary>
        /// �ж�Ա���Ƿ���Ч
        /// </summary>
        /// <param name="strUserNo">Ա������</param>
        /// <param name="strPlant">������˾</param>
        /// <returns>UserID</returns>
        public string CheckMiscellaneousUserIsValid(string strUserNo, string strDate, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_CheckMiscellaneousUserIsValid";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@userno", strUserNo);
                sqlParam[1] = new SqlParameter("@date", strDate);
                sqlParam[2] = new SqlParameter("@plant", Convert.ToInt32(strPlant));

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        public void ImportMiscellaneousInfo(string strUID, string strDate, string strAmount, string strMemo, string strPlant, string strImporter, int Type)
        {
            try
            {
                string strSql = string.Empty;

                //if (strDay == "") strDay = "0";

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@uid", Convert.ToInt32(strUID));
                sqlParam[1] = new SqlParameter("@date", strDate);
                sqlParam[2] = new SqlParameter("@amount", Convert.ToDecimal(strAmount));
                sqlParam[3] = new SqlParameter("@memo", strMemo);
                sqlParam[4] = new SqlParameter("@plant", Convert.ToInt32(strPlant));
                sqlParam[5] = new SqlParameter("@importer", Convert.ToInt32(strImporter));

                switch (Type)
                {
                    //ȫ�ڽ�
                    case 1:
                        strSql = "sp_hr_ImportPerfectAward";
                        break;

                    //���䲹��
                    case 2:
                        strSql = "sp_hr_ImportSubsidies";
                        break;

                    //���·�
                    case 3:
                        strSql = "sp_hr_ImportHighCost";
                        break;

                    //B��ϵ��
                    case 4:
                        strSql = "sp_hr_ImportClassBCoefficient";
                        break;

                    //���Ͽۿ�
                    case 5:
                        strSql = "sp_hr_ImportMaterialCharge";
                        break;

                    //���˿ۿ�
                    case 6:
                        strSql = "sp_hr_ImportAssessmentCharge";
                        break;

                    //���ٿ���
                    case 7:
                        strSql = "sp_hr_ImportHolidayAttendance";
                        break;

                    //ѧ������
                    case 8:
                        strSql = "sp_hr_ImportStudentSubsidies";
                        break;

                    //����
                    case 9:
                        strSql = "sp_hr_ImportAward";
                        break;

                    //�ͲͿۿ�
                    case 10:
                        strSql = "sp_hr_ImportDinerdeduct";
                        break;

                    //��������
                    case 11:
                        strSql = "sp_hr_ImportMutualFunds";
                        break;

                    //��������
                    case 12:
                        strSql = "sp_hr_ImportQualityDeduct";
                        break;
                }

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return;
            }
        }
        #endregion

        #region �Ͳ�
        /// <summary>
        /// ��þͲ�ͳ����Ϣ
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Plant"></param>
        /// <returns>DataSet</returns>
        public DataSet SelectDinnerInfo(int Year, int Month, int Plant)
        {
            try
            {
                string strSql = "sp_hr_SelectDinnerInfo";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@year", Year);
                sqlParam[1] = new SqlParameter("@month", Month);
                sqlParam[2] = new SqlParameter("@plant", Plant);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                return null;
            }
        }
        public DataSet SelectDinnerInfoByYear(int Year, int Plant)
        {
            try
            {
                string strSql = "sp_hr_SelectDinnerInfoByYear";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@year", Year);
                sqlParam[1] = new SqlParameter("@plant", Plant);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��þͲ���Ϣ��ϸ
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="Plant"></param>
        /// <returns></returns>
        public DataSet SelectDinnerDetailInfo(DateTime Date, int Plant)
        {
            try
            {
                string strSql = "sp_hr_SelectDinnerDetailInfo";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@date", Date);
                sqlParam[1] = new SqlParameter("@plant", Plant);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ָ��Ա���Ͳ���Ϣ��ϸ
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserNo"></param>
        /// <param name="Plant"></param>
        /// <returns></returns>
        public DataSet SelectDinnerDetailUserInfo(DateTime Date, string UserNo, int Plant, bool isChkAll)
        {
            try
            {
                string strSql = "sp_hr_SelectDinnerDetailUserInfo";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@date", Date);
                sqlParam[1] = new SqlParameter("@uno", UserNo);
                sqlParam[2] = new SqlParameter("@plant", Plant);
                sqlParam[3] = new SqlParameter("@chk", isChkAll);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ŷ�Ӧ�豸���
        /// </summary>
        /// <param name="strDevice">�������</param>
        /// <param name="strSensor">�豸���</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>DataSet</returns>
        public DataSet SelectDinnerDevice(string strDevice, string strSensor, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_SelectDinnerDeviceInfo";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@device", strDevice);
                sqlParam[1] = new SqlParameter("@sensor", strSensor);
                sqlParam[2] = new SqlParameter("@plant", strPlant);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ɾ��������Ŷ�Ӧ�豸���
        /// </summary>
        /// <param name="strDeviceID">DeviceID</param>
        /// <returns>True/False</returns>
        public bool DeleteDinnerDevice(string strDeviceID)
        {
            try
            {
                string strSql = "sp_hr_DeleteDinnerDevice";
                SqlParameter sqlParam = new SqlParameter("@deviceid", Convert.ToInt32(strDeviceID));

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// �ж���ؼ�¼�Ƿ����
        /// </summary>
        /// <param name="strDevice">�������</param>
        /// <param name="strSensor">�豸���</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>True/False</returns>
        public bool CheckRecordIsExist(string strDevice, string strSensor, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_CheckDeviceRecordIsExist";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@device", strDevice);
                sqlParam[1] = new SqlParameter("@sensor", strSensor);
                sqlParam[2] = new SqlParameter("@plant", Convert.ToInt32(strPlant));

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ���ӻ�����Ŷ�Ӧ�豸���
        /// </summary>
        /// <param name="strDevice">�������</param>
        /// <param name="strSensor">�豸���</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>True/False</returns>
        public bool InsertDinnerDevice(string strDevice, string strSensor, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_InsertDinnerDevice";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@device", strDevice);
                sqlParam[1] = new SqlParameter("@sensor", strSensor);
                sqlParam[2] = new SqlParameter("@plant", strPlant);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }


        /// <summary>
        /// ���ھͲ����Ƚ� �ַ���
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="intMonth"></param>
        /// <param name="intDepartment"></param>
        /// <param name="strUser"></param>
        /// <param name="strUserName"></param>
        /// <param name="intPlant"></param>
        /// <param name="intType">0-</param>
        /// <returns></returns>

        public string AttDinerString(string strYear, int intMonth, int intDepartment, string strUser, string strUserName, int intPlant, int intType)
        {
            try
            {
                string strSql = "sp_Hr_AttDinerCompare";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@year", Convert.ToInt32(strYear));
                sqlParam[1] = new SqlParameter("@month", intMonth);
                sqlParam[2] = new SqlParameter("@department", intDepartment);
                sqlParam[3] = new SqlParameter("@userno", strUser);
                sqlParam[4] = new SqlParameter("@username", strUserName);
                sqlParam[5] = new SqlParameter("@plantcode", intPlant);
                sqlParam[6] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public DataSet AttAndDinerCompare(string strYear, int intMonth, int intDepartment, string strUser, string strUserName, int intPlant)
        {
            try
            {

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, AttDinerString(strYear, intMonth, intDepartment, strUser, strUserName, intPlant, 0));

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public string AttDinerDetailString(int intYear, int intMonth, int intDepartment, int intUserID, int intPlant, int intType)
        {
            try
            {
                string strSql = "sp_Hr_AttDinerCompareDetail";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@year", intYear);
                sqlParam[1] = new SqlParameter("@month", intMonth);
                sqlParam[2] = new SqlParameter("@department", intDepartment);
                sqlParam[3] = new SqlParameter("@userID", intUserID);
                sqlParam[4] = new SqlParameter("@plantcode", intPlant);
                sqlParam[5] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string AttDinerDetailAllString(int intYear, int intMonth, int intDepartment, int intUserID, int intPlant, int intType)
        {
            try
            {
                string strSql = "sp_Hr_AttDinerCompareDetailAll";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@year", intYear);
                sqlParam[1] = new SqlParameter("@month", intMonth);
                sqlParam[2] = new SqlParameter("@department", intDepartment);
                sqlParam[3] = new SqlParameter("@userID", intUserID);
                sqlParam[4] = new SqlParameter("@plantcode", intPlant);
                sqlParam[5] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public DataSet AttDinerDetail(int intYear, int intMonth, int intDepartment, int intUserID, int intPlant)
        {
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, AttDinerDetailString(intYear, intMonth, intDepartment, intUserID, intPlant, 0));
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public string ExportAttDinerDetailString(int intYear, int intMonth, int intDepartment, int intUserID, int intPlant, int intType)
        {
            try
            {
                string strSql = "sp_Hr_AttDinerCompareDetail";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@year", intYear);
                sqlParam[1] = new SqlParameter("@month", intMonth);
                sqlParam[2] = new SqlParameter("@department", intDepartment);
                sqlParam[3] = new SqlParameter("@userID", intUserID);
                sqlParam[4] = new SqlParameter("@plantcode", intPlant);
                sqlParam[5] = new SqlParameter("@Type", intType);
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion

        #region ��Ƹ����
        public DataSet SelectDepartmentPlan(int intdept, int intYear, int intMonth, int intPlant)
        {
            try
            {
                string str = ExportExcelForHiring(intdept, intYear, intMonth, intPlant, 0);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public string ExportExcelForHiring(int intdept, int intYear, int intMonth, int intPlant, int intType)
        {
            try
            {
                string str = "sp_hr_SelectHiringPlanning";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@departmentID", intdept);
                sqlParam[1] = new SqlParameter("@year", intYear);
                sqlParam[2] = new SqlParameter("@month", intMonth);
                sqlParam[3] = new SqlParameter("@plant", intPlant);
                sqlParam[4] = new SqlParameter("@type", intType);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam).ToString();
            }
            catch
            {
                return "";
            }
        }

        public int InsertHiringPlaning(int intDept, int intYear, int intMonth, int intPlant, decimal decUp, decimal decDown, int intCreat)
        {
            try
            {
                string str = "sp_hr_InsertHiringPlaning";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@departmentID", intDept);
                sqlParam[1] = new SqlParameter("@year", intYear);
                sqlParam[2] = new SqlParameter("@month", intMonth);
                sqlParam[3] = new SqlParameter("@Pup", decUp);
                sqlParam[4] = new SqlParameter("@Pdown", decDown);
                sqlParam[5] = new SqlParameter("@creatby", intCreat);
                sqlParam[6] = new SqlParameter("@plant", intPlant);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
                return 1;
            }
            catch
            {
                return -1;
            }
        }


        public int DeleteHiringData(int intID)
        {
            try
            {
                string str = "DELETE FROM Hr_HirePlaning WHERE id=" + intID.ToString();
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);

                return 1;
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region �Ž������
        /// <summary>
        /// ��ȡ���ڶ�Ӧ���Ž������������б�
        /// </summary>
        /// <param name="strSensor">�����Ž��������豸��</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>���ڶ�Ӧ���Ž������������б�</returns>
        public IList<HR_AttendanceAccess> SelectAttendanceAccess(string strSensor, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceAccessInfo";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@sensor", strSensor);
                sqlParam[1] = new SqlParameter("@plant", strPlant);

                IList<HR_AttendanceAccess> AttendanceAccess = new List<HR_AttendanceAccess>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    HR_AttendanceAccess hr_aa = new HR_AttendanceAccess();
                    hr_aa.SensorNo = reader["SensorNo"].ToString();
                    hr_aa.orgID = Convert.ToInt32(reader["orgID"]);
                    AttendanceAccess.Add(hr_aa);
                }
                reader.Close();
                return AttendanceAccess;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ���ӿ��ڶ�Ӧ�Ž���������Ϣ
        /// </summary>
        /// <param name="strSensor">�����Ž��������豸��</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>True/False</returns>
        public bool InsertAttendanceAccess(string strSensor, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_InsertAttendanceAccess";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@sensor", strSensor);
                sqlParam[1] = new SqlParameter("@plant", strPlant);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ɾ���Ž���������Ӧ������Ϣ
        /// </summary>
        /// <param name="strSensor">�Ž��������豸��</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>True/False</returns>
        public bool DeleteAttendanceAccess(string strSensor, string strPlant)
        {
            try
            {
                string strSql = "sp_hr_DeleteAttendanceAccess";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@sensor", strSensor);
                sqlParam[1] = new SqlParameter("@plant", strPlant);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ����բ���ͳ��俼���쳣�Ĳ���
        /// </summary>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <returns>���ش���բ���ͳ��俼���쳣�Ĳ���</returns>
        public DataTable SelectAttendanceErrorDept(string strPlant)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceErrorDept";
                SqlParameter sqlParam = new SqlParameter("@plant", strPlant);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ��ȡ����բ���ͳ��俼���쳣��Ϣ
        /// </summary>
        /// <param name="strYear">��</param>
        /// <param name="strMonth">��</param>
        /// <param name="strUserNo">Ա������</param>
        /// <param name="strDept">��������</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <param name="intSelect">�쳣ѡ��</param>
        /// <returns>���ش���բ���ͳ��俼���쳣��Ϣ</returns>
        public IList<hr_AttendanceError> SelectAttendanceError(string strYear, string strMonth, string strUserNo, string strDept, string strPlant, int intSelect)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceError";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@year", strYear);
                sqlParam[1] = new SqlParameter("@month", strMonth);
                sqlParam[2] = new SqlParameter("@userno", strUserNo);
                sqlParam[3] = new SqlParameter("@dept", strDept);
                sqlParam[4] = new SqlParameter("@plant", strPlant);
                sqlParam[5] = new SqlParameter("@select", intSelect);

                IList<hr_AttendanceError> hr_AttendanceError = new List<hr_AttendanceError>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    hr_AttendanceError hr_ae = new hr_AttendanceError();
                    hr_ae.AttendanceUserNo = reader["AttendanceUserNo"].ToString();
                    hr_ae.AttendanceUserName = reader["AttendanceUserName"].ToString();
                    hr_ae.AttendanceUserCode = reader["AttendanceUserCode"].ToString();
                    hr_ae.AttendanceType = reader["AttendanceType"].ToString();
                    hr_ae.Department = reader["DepartmentName"].ToString();
                    hr_ae.AttendanceDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["AttendanceDate"].ToString()));
                    hr_AttendanceError.Add(hr_ae);
                }
                reader.Close();
                return hr_AttendanceError;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ��ȡ����բ���ͳ��俼���쳣��ϢExcel
        /// </summary>
        /// <param name="strYear">��</param>
        /// <param name="strMonth">��</param>
        /// <param name="strUserNo">Ա������</param>
        /// <param name="strDept">��������</param>
        /// <param name="strPlant">Session["orgID"]</param>
        /// <param name="intSelect">�쳣ѡ��</param>
        /// <returns>���ش���բ���ͳ��俼���쳣��ϢExcel</returns>
        public string SelectAttendanceErrorExcel(string strYear, string strMonth, string strUserNo, string strDept, string strPlant, int intSelect)
        {
            try
            {
                string strSql = "sp_hr_SelectAttendanceErrorExcel";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@year", strYear);
                sqlParam[1] = new SqlParameter("@month", strMonth);
                sqlParam[2] = new SqlParameter("@userno", strUserNo);
                sqlParam[3] = new SqlParameter("@dept", strDept);
                sqlParam[4] = new SqlParameter("@plant", strPlant);
                sqlParam[5] = new SqlParameter("@select", intSelect);

                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return "";
            }
        }
        #endregion

        #region ���ٵ������
        public bool holidayImport(string filePath, string uId, int plantCode, out string message)
        {
            message = "";
            DataTable dt = null;
            bool success = true;
            try
            {
                dt = adam.getExcelContents(filePath).Tables[0];
            }
            catch (Exception ex)
            {
                message = "�����ļ�������Excel��ʽ'" + ex.Message.ToString() + "'.";
                success = false;
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (success)
            {
                try
                {
                    if (
                        dt.Columns[0].ColumnName != "����" ||
                        dt.Columns[1].ColumnName != "����" ||
                        dt.Columns[2].ColumnName != "����" ||
                        dt.Columns[3].ColumnName != "Сʱ"
                        )
                    {
                        dt.Reset();
                        message = "�����ļ���ģ�治��ȷ�������ģ���ٵ���!";
                        success = false;
                    }

                    DataTable TempTable = new DataTable("TempTable");
                    DataColumn TempColumn;
                    DataRow TempRow;

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "holidayDate";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "userNo";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "userName";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.Decimal");
                    TempColumn.ColumnName = "hours";
                    TempTable.Columns.Add(TempColumn);

                    //TempColumn = new DataColumn();
                    //TempColumn.DataType = System.Type.GetType("System.Decimal");
                    //TempColumn.ColumnName = "cost";
                    //TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "errorMsg";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.Int32");
                    TempColumn.ColumnName = "createdby";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "createdDate";

                    TempTable.Columns.Add(TempColumn);

                    if (dt.Rows.Count > 0)
                    {


                        //decimal cost = -1;
                        string createdBy = uId;
                        string createdDate = DateTime.Now.ToString();

                        DateTime dateFormat = DateTime.Now;

                        //�������ʱ���и��ϴ�Ա���ļ�¼
                        if (ClearTempTable(Convert.ToInt32(uId)))
                        {

                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                string holidayDate = string.Empty;
                                string userNo = string.Empty;
                                string userName = string.Empty;
                                decimal hours = 0;
                                string errorMsg = "";
                                TempRow = TempTable.NewRow();//�����µ���
                                if (dt.Rows[i].IsNull(0))
                                {
                                    errorMsg += "���ڲ���Ϊ��;";
                                }
                                else
                                {
                                    holidayDate = dt.Rows[i].ItemArray[0].ToString().Trim();
                                    try
                                    {
                                        DateTime dtFormat = Convert.ToDateTime(holidayDate);
                                        TempRow["holidayDate"] = dtFormat;
                                    }
                                    catch
                                    {
                                        errorMsg += "�´����ڸ�ʽ����ȷ;";
                                    }

                                }

                                if (dt.Rows[i].IsNull(1))
                                {

                                    errorMsg += "���Ų���Ϊ��;";
                                }
                                else
                                {
                                    userNo = dt.Rows[i].ItemArray[1].ToString().Trim().ToUpper();
                                }


                                if (dt.Rows[i].IsNull(2))
                                {
                                    errorMsg += "Ա����������Ϊ��;";
                                }
                                else
                                {
                                    userName = dt.Rows[i].ItemArray[2].ToString().Trim();
                                }

                                if (dt.Rows[i].IsNull(3))
                                {
                                    errorMsg += "ʱ������Ϊ��;";
                                    TempRow["hours"] = DBNull.Value;
                                }
                                else
                                {

                                    if (!decimal.TryParse(dt.Rows[i].ItemArray[3].ToString().Trim(), out hours))
                                    {
                                        errorMsg += "ʱ��ֻ��������;";
                                        TempRow["hours"] = DBNull.Value;
                                    }
                                    else if (hours <= 0)
                                    {
                                        errorMsg += "ʱ��ֻ���Ǵ���0������;";
                                        TempRow["hours"] = DBNull.Value;
                                    }
                                    else
                                    {
                                        TempRow["hours"] = hours;
                                    }

                                }

                                if (dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1) && dt.Rows[i].IsNull(2) && dt.Rows[i].IsNull(3))
                                {
                                    continue;
                                }

                                //TempRow["cost"] 
                                TempRow["userNo"] = userNo;
                                TempRow["userName"] = userName;
                                TempRow["errorMsg"] = errorMsg.Trim();
                                TempRow["createdBy"] = Convert.ToInt32(createdBy);
                                TempRow["createdDate"] = createdDate;

                                TempTable.Rows.Add(TempRow);
                            }
                            //TempTable�����ݵ�������������Ƶ����ݿ���
                            if (TempTable != null && TempTable.Rows.Count > 0)
                            {
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsnx(), SqlBulkCopyOptions.UseInternalTransaction))
                                {
                                    bulkCopy.DestinationTableName = "hr_holidayImportTemp";

                                    bulkCopy.ColumnMappings.Clear();

                                    bulkCopy.ColumnMappings.Add("holidayDate", "holidayDate");
                                    bulkCopy.ColumnMappings.Add("userNo", "userNo");
                                    bulkCopy.ColumnMappings.Add("userName", "userName");
                                    bulkCopy.ColumnMappings.Add("hours", "hours");
                                    bulkCopy.ColumnMappings.Add("errorMsg", "errorMsg");
                                    bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                                    bulkCopy.ColumnMappings.Add("createdDate", "createdDate");
                                    try
                                    {
                                        bulkCopy.WriteToServer(TempTable);
                                    }
                                    catch (Exception ex)
                                    {
                                        message = "����ʱ��������ϵϵͳ����ԱA��";
                                        success = false;
                                    }
                                    finally
                                    {
                                        TempTable.Dispose();
                                        bulkCopy.Close();
                                    }
                                }
                            }
                            dt.Reset();
                            if (success)
                            {
                                //���ݿ����֤
                                if (CheckTempTable(Convert.ToInt32(uId), plantCode))
                                {
                                    //�ж��ϴ������ܷ�ͨ����֤
                                    if (JudgeTempTable(Convert.ToInt32(uId)))
                                    {
                                        if (TransTempTable(Convert.ToInt32(uId), plantCode))
                                        {
                                            message = "�����ļ��ɹ�";
                                            success = true;
                                        }
                                        else
                                        {
                                            message = "����ʱ��������ϵ����ԱC!";
                                            success = false;
                                        }
                                    }
                                    else
                                    {
                                        message = "�����ļ��������д���!";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    message = "����ʱ��������ϵ����ԱB!";
                                    success = false;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    message = "�����ļ�ʧ��!";
                    success = false;
                }
                finally
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            return success;

        }


        private bool ClearTempTable(int uID)
        {
            string sqlstr = "sp_hr_clearHolidayTempTable";
            SqlParameter[] param = new SqlParameter[1]{
            new SqlParameter("@uID",uID)
            };
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));


        }

        private bool CheckTempTable(int uID, int plantCode)
        {
            string sqlstr = "sp_hr_checkHolidayTempTable";
            SqlParameter[] param = new SqlParameter[2]{
            new SqlParameter("@uID",uID)
            ,new SqlParameter("@plant",plantCode)
            };
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
        }

        private bool JudgeTempTable(int uID)
        {
            string sqlstr = "sp_hr_checkHolidayTempError";
            SqlParameter[] param = new SqlParameter[1]{
            new SqlParameter("@uID",uID)
            };
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));

        }

        private bool TransTempTable(int uID, int plantCode)
        {
            string sqlstr = "sp_hr_insertToHolidayAtt";
            SqlParameter[] param = new SqlParameter[2]{
            new SqlParameter("@uID",uID),
            new SqlParameter("@plantCode",plantCode )
            };
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
        }
        public DataTable GetImportError(string strUID)
        {
            string sqlstr = "sp_hr_selectHolidayTempError";
            SqlParameter[] param = new SqlParameter[1]{
            new SqlParameter("@uID",strUID)
            };
            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        }

        #endregion


    }
}