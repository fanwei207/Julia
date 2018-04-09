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
using adamFuncs;

namespace Wage
{
    /// <summary>
    /// Summary description for HR
    /// </summary>
    public partial class HRTrain
    {

        adamClass adam = new adamClass();

        public HRTrain()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        # region 技能信息维护 DB:train_Skills

        /// <summary>
        /// 增加培训技能信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回状态</returns>
        public int InsertSkillType(string skilltype, int uID, string uName, int plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@SkillTypes", skilltype);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@plantcode", plantcode);
                param[4] = new SqlParameter("@retValue", SqlDbType.Int);
                param[4].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_InserSkillTypeInfo", param);
                return Convert.ToInt32(param[4].Value.ToString());
            }
            catch
            {
                return 2;
            }
        }

        /// <summary>
        /// 增加培训技能信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回状态</returns>
        public int InsertSkillInfo(int skilltypid, string skilltype, string SkillName, int uID, string uName, int plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@SkillTypeId", skilltypid);
                param[1] = new SqlParameter("@SkillTypes", skilltype);
                param[2] = new SqlParameter("@SkillName", SkillName);
                param[3] = new SqlParameter("@uID", uID);
                param[4] = new SqlParameter("@uName", uName);
                param[5] = new SqlParameter("@plantcode", plantcode);
                param[6] = new SqlParameter("@retValue", SqlDbType.Int);
                param[6].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_InserSkillInfo", param);
                return Convert.ToInt32(param[6].Value.ToString());
            }
            catch
            {
                return 2;
            }
        }

        /// <summary>
        /// 绑定界面获取培训类别信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回培训详情信息</returns>
        public DataTable selectSkillType()
        {
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectSkillTypeList").Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 绑定界面获取培训明细信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回培训详情信息</returns>
        public DataTable selectSkillList(int SkillTypeID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@SkillTypeID", SkillTypeID);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectSkillList", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ID删除培训类型信息判断是否被引用
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool DeleteSkillTypeCheckUse(int skilltypeid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@SkillId", skilltypeid);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_DeleteCheckUseSkillType", param);
                return Convert.ToBoolean(param[1].Value.ToString());
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 根据ID删除培训类型信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool DeleteSkillType(int skilltypeid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@SkillTypeId", skilltypeid);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_DeleteSkillType", param);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据ID删除培训信息判断是否有被引用
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool DeleteCheckUseSkill(int skillid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@SkillId", skillid);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_DeleteCheckUseSkill", param);
                return Convert.ToBoolean(param[1].Value.ToString());
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 根据ID删除培训类型信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool DeleteSkill(int skillid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@SkillId", skillid);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_DeleteSkill", param);
            }
            catch
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 根据单号删除所选培训类型信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool DeleteSelectSkill(string AppNo, int skillid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@AppNo", AppNo);
                param[1] = new SqlParameter("@SkillId", skillid);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_DeleteSelectSkill", param);
            }
            catch
            {
                return false;
            }
            return true;
        }


        #endregion


        # region 培训申请 DB:train_attendInfo

        /// <summary>
        /// 绑定界面获取培训信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回培训详情信息</returns>
        public DataTable selectTrainDet(string formid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@AppNo", formid);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_SelectTrainDetInfo", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 获取部门List信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回部门List信息</returns>
        public DataTable selectDeptList(int plantcode, int deptID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantID", plantcode);
                param[1] = new SqlParameter("@DeptNo", deptID);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectDept", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取技能LIST信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回培训详情信息</returns>
        public DataTable selectSilleDet(bool skilltypeORfromid, string AppNo, int skilltypeid, int plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@skilltypeORfromid", skilltypeORfromid);
                param[1] = new SqlParameter("@AppNo", AppNo);
                param[2] = new SqlParameter("@skilltypeid", skilltypeid);
                param[3] = new SqlParameter("@Train_PlantCode", plantcode);
                
                //param[1] = new SqlParameter("@CheckStatus", false);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_SelectSkillListInfo", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 获取Domain信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回Domain信息</returns>
        public DataTable SelectDomain(string formid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_SelectTrainDomainInfo", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 生成表单单号
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回单号信息</returns>
        public string CreateAppNo(int uID, string uName, int plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@Train_AppType", "HR");
                param[1] = new SqlParameter("@Train_AppForm", "TRN");
                param[2] = new SqlParameter("@Train_EmpNo", uID);
                param[3] = new SqlParameter("@Train_EmpName", uName);
                param[4] = new SqlParameter("@Train_plantcode", plantcode);
                param[5] = new SqlParameter("@retValue", SqlDbType.VarChar, 30);
                param[5].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_CreateAppNo", param);
                return param[5].Value.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 提交培训申请
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool insertTainMstr(int deptID, string DeptName, string Code, DateTime AgentDate, string AppEno, string AppName
                                    , string CompanyName, DateTime dTime, int DepNo, string DepName, string Teacher, string Subject
                                    , int Phone, int EntriesNumber, int TrainTime, string Content, string Place, string Object, int uID
                                    , string uName, string formid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[22];
                param[0] = new SqlParameter("@Train_DeptNo", deptID);//DateTime.Now.ToString("yyyy-MM-dd"));
                param[1] = new SqlParameter("@Train_DeptName", DeptName);
                param[2] = new SqlParameter("@Train_code", Code);
                param[3] = new SqlParameter("@Train_AgentDate", AgentDate);//Session["uID"].ToString());
                param[4] = new SqlParameter("@Train_AppEno", AppEno);//Session["uName"].ToString());
                param[5] = new SqlParameter("@Train_AppName", AppName);
                param[6] = new SqlParameter("@Train_Company", CompanyName);
                param[7] = new SqlParameter("@Train_dTime", dTime);
                param[8] = new SqlParameter("@Train_Dep", DepNo);
                param[9] = new SqlParameter("@Train_DepName", DepName);
                param[10] = new SqlParameter("@Train_Teacher", Teacher);
                param[11] = new SqlParameter("@Train_Subject", Subject);
                param[12] = new SqlParameter("@Train_Phone", Phone);
                param[13] = new SqlParameter("@Train_EntriesNumber", 0);
                param[14] = new SqlParameter("@Train_TrainTime", TrainTime);
                param[15] = new SqlParameter("@Train_Content", Content);
                param[16] = new SqlParameter("@Train_Place", Place);
                param[17] = new SqlParameter("@Train_Object", Object);
                param[18] = new SqlParameter("@uID", uID);
                param[19] = new SqlParameter("@uName", uName);
                param[20] = new SqlParameter("@retValue", SqlDbType.VarChar, 30);
                param[20].Direction = ParameterDirection.Output;
                param[21] = new SqlParameter("@Train_AppNo", formid);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_insertTrainMstr", param);
            }
            catch
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 提交培训申请同时保持培训域
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool insertIntoDomainInfo(int domainid, string formid, int uid, string uname)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@Train_Domain", domainid);
                param[2] = new SqlParameter("@uID", uid);
                param[3] = new SqlParameter("@uName", uname);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_InsertDomainInfo", param);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送邮件失败删除提交记录
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool AppErrDeleteMstr(string formid, string domainid)
        {
            try
            {
                SqlParameter[] param2 = new SqlParameter[2];
                param2[0] = new SqlParameter("@Train_AppNo", formid);
                param2[1] = new SqlParameter("@Train_Domain", domainid);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_DeleteTrainMstr", param2);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存技能节点
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回NULL</returns>
        public bool SaveSkills(string formid, bool otherskill, int SkillTypeID, string SkillTypeName, int Skillid, string SkillName, int plantcode, int uid, string uName)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@train_otherskill", otherskill);
                param[2] = new SqlParameter("@train_SkillTypeID", SkillTypeID);
                param[3] = new SqlParameter("@train_SkillTypeName", SkillTypeName);
                param[4] = new SqlParameter("@train_SkillID", Skillid);
                param[5] = new SqlParameter("@train_SkillName", SkillName);
                param[6] = new SqlParameter("@Train_Domain", plantcode);
                param[7] = new SqlParameter("@uID", uid);
                param[8] = new SqlParameter("@uName", uName);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_InsertSkillsInfo", param);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得HR Email信息
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>返回HR Email信息</returns>
        public DataTable SelectHrEmail(string formid, int plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@Train_Domain", plantcode);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_SelectHREmail", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        #endregion

        # region 员工HR培训信息查询 DB:train_attendInfo

        /// <summary>
        /// 员工或HR查询培训信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回员工培训信息</returns>
        public DataTable SelectTrainListByPer(string Domain, string StartDate, string EndDate, string User, string deptno)
        {
            try
            {
                string strName = "sp_train_SelectTrainByPer";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@plantCode", Domain);
                param[1] = new SqlParameter("@Startdate", StartDate);
                param[2] = new SqlParameter("@Enddate", EndDate);
                param[3] = new SqlParameter("@user", User);
                param[4] = new SqlParameter("@dept", deptno);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {

                return null;
            }
        }

        #endregion

        # region 培训信息查询 DB:train_attendInfo

        /// <summary>
        /// 培训实到人员确认
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回实到人员信息</returns>
        public DataTable SelectTrainList(string Domain, string StartDate, string EndDate, string User, string deptno)
        {
            try
            {
                string strName = "sp_train_SelectTrainInfo";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@plantCode", Domain);
                param[1] = new SqlParameter("@Startdate", StartDate);
                param[2] = new SqlParameter("@Enddate", EndDate);
                param[3] = new SqlParameter("@user", User);
                param[4] = new SqlParameter("@dept", deptno);

               return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param).Tables[0];
            }
            catch
            {

                return null;
            }
        }

        #endregion

        # region 培训实到人员确认 DB:train_attendInfo

        /// <summary>
        /// 培训实到人员确认
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回实到人员信息</returns>
        public bool insertCheckEmpForRecord(string empno, string EmployeeName, string DeptMentNo, string DeptMentName, int plantcode, string formid,Int32 uid, string uname)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@Train_EmpNo", empno);
                param[2] = new SqlParameter("@Train_EmpName", EmployeeName);
                param[3] = new SqlParameter("@Train_DeptmentNo", Convert.ToInt32(DeptMentNo));
                param[4] = new SqlParameter("@Train_DepartmentName", DeptMentName);
                param[5] = new SqlParameter("@train_PlantCode", plantcode);
                param[6] = new SqlParameter("@uID", uid);
                param[7] = new SqlParameter("@uName", uname);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_InsertCheckAttendInfo", param);
            }
            catch
            {
               
                return false;
            }
            return true;
        }

        #endregion

        #region 培训应到基础信息 DB:train_attendInfo
        /// <summary>
        /// 获得培训人员信息
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>返回参加培训人员信息</returns>
        public DataTable AttedPersonInfo(string formid, bool checkstatus, string plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@CheckStatus", checkstatus);
                param[2] = new SqlParameter("@PlantCode", plantcode);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_SelectAttendInfo", param).Tables[0];

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除参加培训人员
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>返回NULL</returns>
        public bool DeleteEmployeeForRecord(string formid, string EmpNo, int plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@EmpNo", EmpNo);
                param[2] = new SqlParameter("@train_PlantCode", plantcode);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_DeleteAttendInfo", param);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得培训人员信息
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>返回参加培训人员信息</returns>
        public DataTable DeptPersonsInfo(int plantcode, int deptno, string EmpNo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@plantID", plantcode);
                param[1] = new SqlParameter("@DeptNo", deptno);
                param[2] = new SqlParameter("@EmpNo", EmpNo);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectPersonsInfo", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 增加培训人员信息
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>返回参加培训人员信息</returns>
        public bool insertIntoEmpForRecord(string empno, string EmployeeName, string DeptMentNo, string DeptMentName, int plantcode, string formid, string empname)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@Train_EmpNo", empno);
                param[2] = new SqlParameter("@Train_EmpName", EmployeeName);
                param[3] = new SqlParameter("@Train_DeptmentNo", Convert.ToInt32(DeptMentNo));
                param[4] = new SqlParameter("@Train_DepartmentName", DeptMentName);
                param[5] = new SqlParameter("@train_PlantCode", plantcode);
                param[6] = new SqlParameter("@uID", plantcode);
                param[7] = new SqlParameter("@uName", empname);
                //param[4] = new SqlParameter("@retValue", SqlDbType.Int);
                //param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_train_InsertAttendInfo", param);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得培训人员Email信息
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>返回参加培训人员Email信息</returns>
        public DataTable GetAttendEmailInfo(string formid, string plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Train_AppNo", formid);
                param[1] = new SqlParameter("@Train_Domain", plantcode);
                //param[4] = new SqlParameter("@retValue", SqlDbType.Int);
                //param[4].Direction = ParameterDirection.Output;
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_SelectAttendPersonsEmail", param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得培训维护信息
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>返回培训维护信息</returns>
        public DataTable GetTrainInfo(string formid, string plantcode)
        {
            try
            {
                SqlParameter[] param2 = new SqlParameter[2];
                param2[0] = new SqlParameter("@Train_AppNo", formid);
                param2[1] = new SqlParameter("@Train_Domain", plantcode);
                //param[4] = new SqlParameter("@retValue", SqlDbType.Int);
                //param[4].Direction = ParameterDirection.Output;

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_train_SelectTrainInfoForReport", param2).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得部门信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>返回部门信息</returns>
        public SqlDataReader SelectDeptMentList(string plantcode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@plantID", plantcode);
                param[1] = new SqlParameter("@DeptNo", 999999);
                SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectDept", param);
                return reader;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
    }
        #endregion
}