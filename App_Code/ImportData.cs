using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Collections;
using adamFuncs;

namespace ImportDT
{
    /// <summary>
    /// Summary description for ImportData
    /// </summary>
    public class ImportData
    {
        adamClass adam = new adamClass(); 
        public ImportData()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        #region Check validate for data

        //Validate department's name
        public string CheckDeptName(string strDept)
        {
            try
            {
                string str = "SELECT departmentID FROM departments WHERE name=N'"+ strDept +"' ";
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsnx(),CommandType.Text,str));
            }
            catch
            {
                return null;
            }
        }

        //Validate User information
        public string CheckUserInfo(string strUserNo, string strUserName, string strPlant)
        {
            try
            {
                string str = "SELECT userID FROM Users WHERE userNO='" + strUserNo + "' AND  userName like N'%" + strUserName + "%' And plantcode ='" + strPlant + "' And isActive =1 And deleted=0 ";
                return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, str));
                
            }
            catch
            {
                return null;
            }
        }

        #endregion


        #region Insert Salary datas
        public int InsertSalary(string strDep,string strNum,string strUser,string strName,string strWk,decimal[] arrTy,DateTime strDate,string strkind,string strUserID,string strUU)
        {
            try
            {
                string str = "sp_hr_checkSalaryInto";
                SqlParameter[] parmArray = new SqlParameter[34];
                parmArray[0] = new SqlParameter("@departmentID", Convert.ToInt32(strDep));  // ����
                parmArray[1] = new SqlParameter("@Serialnumber", Convert.ToInt32(strNum));  // ���
                parmArray[2] = new SqlParameter("@userNo", strUser);       // ����
                parmArray[3] = new SqlParameter("@userName", strName);     // ����
                parmArray[4] = new SqlParameter("@WorkGroup", strWk);      // ����
                parmArray[5] = new SqlParameter("@BasicSalary", arrTy[5]); // ����
                parmArray[6] = new SqlParameter("@Attendence", arrTy[6]);  // ����
                parmArray[7] = new SqlParameter("@Overdays", arrTy[7]);    // �Ӱ�
                parmArray[8] = new SqlParameter("@Sundays", arrTy[8]);     // ˫��
                parmArray[9] = new SqlParameter("@holidays", arrTy[9]);    // ����
                parmArray[10] = new SqlParameter("@Night", Convert.ToInt32(arrTy[10]));     // ҹ��
                parmArray[11] = new SqlParameter("@AnnualLeave", arrTy[11]);//��� 
                parmArray[12] = new SqlParameter("@OverSalary", arrTy[12]); //�Ӱ��
                parmArray[13] = new SqlParameter("@SunSalary", arrTy[13]);  //˫�ݷ�
                parmArray[14] = new SqlParameter("@PerformanceSalary", arrTy[14]);   //��Ч
                parmArray[15] = new SqlParameter("@Summary", arrTy[15]);    //С��
                parmArray[16] = new SqlParameter("@AnnualLeaveSalary", arrTy[16]); // ��ٷ�
                parmArray[17] = new SqlParameter("@NightSalary", arrTy[17]);   //ҹ���
                parmArray[18] = new SqlParameter("@HolidaySalary", arrTy[18]); //���ٷ�   
                parmArray[19] = new SqlParameter("@Allowance", arrTy[19]);  //����
                //parmArray[20] = new SqlParameter("@Allattendence", hsy.s_Shsalary); //
                //parmArray[21] = new SqlParameter("@WYBonus", hsy.s_Department);     //
                parmArray[20] = new SqlParameter("@Duereward", arrTy[20]); //Ӧ��
                parmArray[21] = new SqlParameter("@HFound", arrTy[21]); // ����
                parmArray[22] = new SqlParameter("@RFound", arrTy[22]);        //����
                parmArray[23] = new SqlParameter("@MFound", arrTy[23]);        //ҽ��
                parmArray[24] = new SqlParameter("@MemberShip", arrTy[24]);    //����
                parmArray[25] = new SqlParameter("@Tax", arrTy[25]);   //˰
                parmArray[26] = new SqlParameter("@Workpay", arrTy[26]);    //ʵ��
                parmArray[27] = new SqlParameter("@TestScore", arrTy[27]);  //������
                parmArray[28] = new SqlParameter("@SalaryDate", strDate);   //��������
                parmArray[29] = new SqlParameter("@SalaryType", Convert.ToInt32(strkind));   // ����
                parmArray[30] = new SqlParameter("@userID", Convert.ToInt32(strUserID));     //����ID
                parmArray[31] = new SqlParameter("@holidayallowance", arrTy[28]);  //����
                parmArray[32] = new SqlParameter("@highTemp",arrTy[29]);    //����
                parmArray[33] = new SqlParameter("@creat",Convert.ToInt32(strUU));

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, parmArray);

                arrTy = new decimal[29]; //Clear arr
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int InsertSalary1(string strDep, string strNum, string strUser, string strName, string strWk, decimal[] arrTy, string strDate, string strkind, string strUserID, string strUU)
        {
            try
            {
                string str = "sp_hr_checkSalaryInto1";
                SqlParameter[] parmArray = new SqlParameter[34];
                parmArray[0] = new SqlParameter("@departmentID", Convert.ToInt32(strDep));  // ����
                parmArray[1] = new SqlParameter("@Serialnumber", Convert.ToInt32(strNum));  // ���
                parmArray[2] = new SqlParameter("@userNo", strUser);       // ����
                parmArray[3] = new SqlParameter("@userName", strName);     // ����
                parmArray[4] = new SqlParameter("@WorkGroup", strWk);      // ����
                parmArray[5] = new SqlParameter("@BasicSalary", arrTy[5]); // ����
                parmArray[6] = new SqlParameter("@Attendence", arrTy[6]);  // ����
                parmArray[7] = new SqlParameter("@Overdays", arrTy[7]);    // �Ӱ�
                parmArray[8] = new SqlParameter("@Sundays", arrTy[8]);     // ˫��
                parmArray[9] = new SqlParameter("@holidays", arrTy[9]);    // ����
                parmArray[10] = new SqlParameter("@Night", Convert.ToInt32(arrTy[10]));     // ҹ��
                parmArray[11] = new SqlParameter("@AnnualLeave", arrTy[11]);//��� 
                parmArray[12] = new SqlParameter("@OverSalary", arrTy[12]); //�Ӱ��
                parmArray[13] = new SqlParameter("@SunSalary", arrTy[13]);  //˫�ݷ�
                parmArray[14] = new SqlParameter("@PerformanceSalary", arrTy[14]);   //��Ч
                parmArray[15] = new SqlParameter("@Summary", arrTy[15]);    //С��
                parmArray[16] = new SqlParameter("@AnnualLeaveSalary", arrTy[16]); // ��ٷ�
                parmArray[17] = new SqlParameter("@NightSalary", arrTy[17]);   //ҹ���
                parmArray[18] = new SqlParameter("@HolidaySalary", arrTy[18]); //���ٷ�   
                parmArray[19] = new SqlParameter("@Allowance", arrTy[19]);  //����
                parmArray[20] = new SqlParameter("@Duereward", arrTy[20]); //Ӧ��
                parmArray[21] = new SqlParameter("@HFound", arrTy[21]); // ����
                parmArray[22] = new SqlParameter("@RFound", arrTy[22]);        //����
                parmArray[23] = new SqlParameter("@MFound", arrTy[23]);        //ҽ��
                parmArray[24] = new SqlParameter("@MemberShip", arrTy[24]);    //����
                parmArray[25] = new SqlParameter("@Tax", arrTy[25]);   //˰
                parmArray[26] = new SqlParameter("@Workpay", arrTy[26]);    //ʵ��
                parmArray[27] = new SqlParameter("@TestScore", arrTy[27]);  //������
                parmArray[28] = new SqlParameter("@SalaryDate", strDate);   //��������
                parmArray[29] = new SqlParameter("@SalaryType", Convert.ToInt32(strkind));   // ����
                parmArray[30] = new SqlParameter("@userID", Convert.ToInt32(strUserID));     //����ID
                parmArray[31] = new SqlParameter("@holidayallowance", arrTy[28]);  //����
                parmArray[32] = new SqlParameter("@highTemp", arrTy[29]);    //����
                parmArray[33] = new SqlParameter("@creat", Convert.ToInt32(strUU));

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, parmArray);

                arrTy = new decimal[29]; //Clear arr
                return 0;
            }
            catch
            {
                return -1;
            }
        }


         public int InsertRest(string strDep,string strDate,string strType,string strUU)
         {
             try
             {
                 string str = "sp_Hr_InsertSalaryRest";
                 SqlParameter[] parmArray = new SqlParameter[4];
                 parmArray[0] = new SqlParameter("@departmentID", strDep);  // ����
                 parmArray[1] = new SqlParameter("@workdate", strDate);  // ����
                 parmArray[2] = new SqlParameter("@stype", strType);       // ����
                 parmArray[3] = new SqlParameter("@creat", strUU);

                 SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, parmArray);
                 return 0;
             }
             catch
             {
                 return -1;
             }
         }

        public int InsertLeave(string strUser, string strUid, string strStart,string strEnd,string strType,string strTypeName, string strDate,string strUU)
        {
            try
            {
                string str = "sp_Hr_InsertSalaryLeave";
                SqlParameter[] parmArray = new SqlParameter[8];
                parmArray[0] = new SqlParameter("@userID", Convert.ToInt32(strUid));  // ����
                parmArray[1] = new SqlParameter("@userNO", strUser);  // ����
                parmArray[2] = new SqlParameter("@startdate", Convert.ToDateTime(strStart));       // ����
                parmArray[3] = new SqlParameter("@enddate", Convert.ToDateTime(strEnd));
                parmArray[4] = new SqlParameter("@Ntype", Convert.ToInt32(strType));
                parmArray[5] = new SqlParameter("@Nname", strTypeName);
                parmArray[6] = new SqlParameter("@NightDate", strDate);
                parmArray[7] = new SqlParameter("@creat", strUU);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, parmArray);

                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public int InsertLunchTime(string strDep, string strDate, string[] attTime, int intCreat)
        {
            try
            {
                string str = "sp_Hr_InsertSalaryLunchTime";
                SqlParameter[] parmArray = new SqlParameter[7];
                parmArray[0] = new SqlParameter("@departmentID", Convert.ToInt32(strDep));
                parmArray[1] = new SqlParameter("@effdate", strDate);
                parmArray[2] = new SqlParameter("@s1", attTime[2]);
                parmArray[3] = new SqlParameter("@e1", attTime[3]);
                parmArray[4] = new SqlParameter("@s2", attTime[4]);
                parmArray[5] = new SqlParameter("@e2", attTime[5]);
                parmArray[6] = new SqlParameter("@creat", intCreat);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, parmArray);

                attTime = new string[6]; //Clear arr

                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public int InsertAtt(string strDep, string strWk,string strDate,string strUid,string strUser,string strUserName, string[] attTime, string strType, int intCreat,string strAName)
        {
            try
            {
                string str = "sp_Hr_InsertSalaryAttendance";
                SqlParameter[] parmArray = new SqlParameter[13];
                parmArray[0] = new SqlParameter("@departmentID", Convert.ToInt32(strDep));
                parmArray[1] = new SqlParameter("@wk", strWk);
                parmArray[2] = new SqlParameter("@uid", Convert.ToInt32(strUid));
                parmArray[3] = new SqlParameter("@userNo", strUser);
                parmArray[4] = new SqlParameter("@userName", strUserName);
                parmArray[5] = new SqlParameter("@effdate", strDate);
                parmArray[6] = new SqlParameter("@s1", attTime[0]);
                parmArray[7] = new SqlParameter("@e1",  attTime[1]);
                parmArray[8] = new SqlParameter("@s2",  attTime[2]);
                parmArray[9] = new SqlParameter("@e2",  attTime[3]);
                parmArray[10] = new SqlParameter("@type", strType);
                parmArray[11] = new SqlParameter("@creat", intCreat);
                parmArray[12] = new SqlParameter("@atypeName", strAName);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, parmArray);

                attTime = new string[4]; //Clear arr

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int InsertAtt1(string strDep, string strWk, string strDate, string strUid, string strUser, string strUserName, string[] attTime, string strType, int intCreat, string strAName)
        {
            try
            {
                string str = "sp_Hr_InsertSalaryAttendance1";
                SqlParameter[] parmArray = new SqlParameter[13];
                parmArray[0] = new SqlParameter("@departmentID", Convert.ToInt32(strDep));
                parmArray[1] = new SqlParameter("@wk", strWk);
                parmArray[2] = new SqlParameter("@uid", Convert.ToInt32(strUid));
                parmArray[3] = new SqlParameter("@userNo", strUser);
                parmArray[4] = new SqlParameter("@userName", strUserName);
                parmArray[5] = new SqlParameter("@effdate", strDate);
                parmArray[6] = new SqlParameter("@s1", attTime[0]);
                parmArray[7] = new SqlParameter("@e1", attTime[1]);
                parmArray[8] = new SqlParameter("@s2", attTime[2]);
                parmArray[9] = new SqlParameter("@e2", attTime[3]);
                parmArray[10] = new SqlParameter("@type", strType);
                parmArray[11] = new SqlParameter("@creat", intCreat);
                parmArray[12] = new SqlParameter("@atypeName", strAName);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, str, parmArray);

                attTime = new string[4]; //Clear arr

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        ///  �������鳧��Ա֮ǰ�����������
        /// </summary>
        /// <param name="plantCode"></param>
        /// <returns></returns>
        public bool ClearAttUsers(string plantCode)
        {
            try
            {
                string str = "sp_Hr_ClearCHAttUsers";
                SqlParameter[] parmArray = new SqlParameter[1];
                parmArray[0] = new SqlParameter("@plantCode", plantCode);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, parmArray);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �����鳧��Ա
        /// </summary>
        /// <param name="strUserNo"></param>
        /// <param name="CreatName"></param>
        /// <param name="plantCode"></param>
        /// <returns></returns>
        public bool InsertAttUsers(string userNo, string creatName, string plantCode)
        {
            try
            {
                string str = "sp_Hr_InsertCHAttUsers";
                SqlParameter[] parmArray = new SqlParameter[3];
                parmArray[0] = new SqlParameter("@userNo", userNo);
                parmArray[1] = new SqlParameter("@creatName", creatName);
                parmArray[2] = new SqlParameter("@plantCode", plantCode);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, parmArray);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion




        #region Process for data
        public int DeleteCheckSalaryData(int intYear, int intMonth, string strUserNo, int intPlantcode,int intType)
        {
            try
            {
                string str = "sp_Hr_CheckSalayDelData";
                SqlParameter[] parmArray = new SqlParameter[5];
                parmArray[0] = new SqlParameter("@Year", intYear);
                parmArray[1] = new SqlParameter("@Month", intMonth);
                parmArray[2] = new SqlParameter("@userNo", strUserNo);
                parmArray[3] = new SqlParameter("@plantcode", intPlantcode);
                parmArray[4] = new SqlParameter("@dType", intType);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, parmArray));

               
            }
            catch
            {
                return -1;
            }
        }


        public int TransforData(int intYear, int intMonth)
        {
            try
            {

                return 0;
            }
            catch
            {
                return -1;
            }
        }



        //Night 
        public int DelADDNight(string strComm,string strDate, int intCreat, int intplantcode,int intType,string strYear,string strMonth )
        {
            try
            {
                string str;
                if (intType == 0)  // Add
                {
                    if (strComm.Length > 10)
                    {
                        int intFlag =0 ;

                        str = " SELECT id FROM hr_ChLeaveDate WHERE Ntype = -1 AND Year(NightDate)=" + strYear + "  AND Month(NightDate)= " + strMonth + " ";
                        intFlag = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.Text, str));

                        if (intFlag<=0)
                            str = " INSERT INTO hr_ChLeaveDate (Ntype,NightDate,creatBy,creatDate) VALUES (-1,'" + strDate + "'," + intCreat.ToString() + ",getdate()) ";
                    }
                    else
                        str = " UPDATE hr_ChLeaveDate SET NightDate ='" + strDate + "' WHERE Ntype = -1 AND Year(NightDate)=" + strYear + "  AND Month(NightDate)= " + strMonth + " ";
                }
                else
                    str = " DELETE FROM hr_ChLeaveDate WHERE Ntype =-1  AND Year(NightDate)=" + strYear + "  AND Month(NightDate)= " + strMonth + "  ";

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.Text, str);

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// ��Excel����ȷ�����ݵĵ��뵽Ŀ�����
        /// </summary>
        /// <param name="pageid"></param>
        /// <param name="uid"></param>
        public bool ExceltoTable(string uid)
        {
            string sql = "sp_hr_insertExToChAttendence";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@creatby", uid);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, sql, param);

            return Convert.ToBoolean(param[1].Value);
        }

        public bool ClearTempTable(string uid)
        {
            string sql = "sp_hr_ClearAttendenceTempTable";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@creatby", uid);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, sql, param);

            return Convert.ToBoolean(param[1].Value);
        }
        public void ValidTempTable(string uid)
        {
            string sql = "sp_hr_ValidTempTable";

            SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sql);

        }
        /// <summary>
        /// �����д���ļ�¼
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataTable exportChAttendenceErrMsg(string uid)
        {
            string sql = "sp_hr_exportChAttendenceErrMsg";
            SqlParameter param = new SqlParameter("@creatBy",uid);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, sql,param).Tables[0];
        }

    } //End class
}
