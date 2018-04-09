using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

namespace IT
{
    public class Charger
    {
        DateTime daytime;

        public DateTime Daytime
        {
            get { return daytime; }
            set { daytime = value; }
        }
        string hours;

        public string Hours
        {
            get { return hours; }
            set { hours = value; }
        }
        string monopoly;

        public string Monopoly
        {
            get { return monopoly; }
            set { monopoly = value; }
        }
        string daytimes;

        public string Daytimes
        {
            get { return daytimes; }
            set { daytimes = value; }
        }
    }

    public class TaskHelper
    {
        static string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];

        /// <summary>
        /// 隐藏构造方法.
        /// </summary>
        private TaskHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// 获取信息部人员列表
        /// </summary>
        /// <param name="sys"></param>
        /// <returns></returns>
        public static DataTable GetUsers(string user, int deptID)
        {
            try
            {
                string strSql = "sp_tsk_selectUsers";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@user", user);
                param[1] = new SqlParameter("@deptID", deptID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 验证用户是否存在
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool CheckUser(string userNo, string domain)
        {
            try
            {
                string strName = "sp_tsk_checkUser";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@userNo", userNo);
                param[1] = new SqlParameter("@domain", domain);
                param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[2].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取系统模块
        /// </summary>
        /// <param name="sys"></param>
        /// <returns></returns>
        public static DataTable GetModule(string sys)
        {
            try
            {
                string strSql = "sp_tsk_selectSysModules";
                SqlParameter parm = new SqlParameter("@sys", sys);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取Task的唯一任务码
        /// </summary>
        /// <returns></returns>
        public static String GetTaskNbr()
        {
            try
            {
                string strName = "sp_tsk_selectTaskNbr";

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@retValue", SqlDbType.VarChar, 5);
                param[0].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return param[0].Value.ToString();

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取任务头栏
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable SelectTaskMstrByNbr(string nbr)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskMstrByNbr";
                SqlParameter parm = new SqlParameter("@mstrNbr", nbr);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 有任务头栏，获取任务明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable SelectTaskDet(string mstrNbr)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskDet";
                SqlParameter parm = new SqlParameter("@mstrNbr", mstrNbr);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 只筛选出指定月份内所有未结的任务
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <summary>
        /// 有任务ID，获取任务明细
        /// </summary>
        /// <param name="detid"></param>
        /// <returns></returns>
        public static DataTable SelectTaskDetById(string detid)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskDetById";
                SqlParameter parm = new SqlParameter("@detID", detid);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];
            }
            catch
            {
                return null;
            }
        }



        public static DataTable SelectTaskDetByIdDate(string detid)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskDetByIdDateDET";
                SqlParameter parm = new SqlParameter("@id", detid);

                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];
               return dt;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 新增任务主表
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static bool InsertTaskMstr(string nbr, string uID, string uName, string desc, string applyNo, string applyDomain, string fileName, string filePath)
        {
            try
            {
                string strName = "sp_tsk_insertTaskMstr";

                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@nbr", nbr);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@desc", desc);
                param[4] = new SqlParameter("@applyNo", applyNo);
                param[5] = new SqlParameter("@applyDomain", applyDomain);
                param[6] = new SqlParameter("@fileName", fileName);
                param[7] = new SqlParameter("@filePath", filePath);
                param[8] = new SqlParameter("@retValue", SqlDbType.Int);
                param[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[8].Value);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新主任务记录
        /// </summary>
        /// <param name="nbr"></param>
        /// <param name="desc"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool UpdateTaskMstr(string nbr, string desc, string fileName, string filePath, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_updateTaskMstr";

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@nbr", nbr);
                param[1] = new SqlParameter("@desc", desc);
                param[2] = new SqlParameter("@fileName", fileName);
                param[3] = new SqlParameter("@filePath", filePath);
                param[4] = new SqlParameter("@uID", uID);
                param[5] = new SqlParameter("@uName", uName);
                param[6] = new SqlParameter("@retValue", SqlDbType.Int);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[6].Value);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 分配任务时，设置邮件已发送(不论成功与否)
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <returns></returns>
        public static void SetTaskDistributeEmialed(string mstrNbr)
        {
            try
            {
                string strName = "sp_tsk_setTaskMstrEmialed";

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
            }
            catch (Exception ex)
            {
                ;
            }
        }
        /// <summary>
        /// 任务分配
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sys"></param>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <param name="degree"></param>
        /// <param name="extreDesc"></param>
        /// <param name="trackBy"></param>
        /// <param name="trackName"></param>
        /// <param name="trackEmail"></param>
        /// <param name="testBy"></param>
        /// <param name="testName"></param>
        /// <param name="testEmail"></param>
        /// <param name="testSecondBy"></param>
        /// <param name="testSecondName"></param>
        /// <param name="testSecondEmail"></param>
        /// <param name="updateBy"></param>
        /// <param name="updateName"></param>
        /// <param name="updateEmail"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool UpdateTaskMstr(string mstrNbr, string sys, string model, string type, string degree, string extreDesc
            , string trackBy, string trackName, string trackEmail, string testBy, string testName, string testEmail
            , string testSecondBy, string testSecondName, string testSecondEmail, string updateBy, string updateName, string updateEmail
            , string uID, string uName, bool isDistribute)
        {
            try
            {
                string strName = "sp_tsk_updateTaskMstrDistribute";
                SqlParameter[] param = new SqlParameter[22];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@sys", sys);
                param[2] = new SqlParameter("@model", model);
                param[3] = new SqlParameter("@type", type);
                param[4] = new SqlParameter("@degree", degree);
                param[5] = new SqlParameter("@extreDesc", extreDesc);

                param[6] = new SqlParameter("@trackBy", trackBy);
                param[7] = new SqlParameter("@trackName", trackName);
                param[8] = new SqlParameter("@trackEmail", trackEmail);

                param[9] = new SqlParameter("@testBy", testBy);
                param[10] = new SqlParameter("@testName", testName);
                param[11] = new SqlParameter("@testEmail", testEmail);

                param[12] = new SqlParameter("@testSecondBy", testSecondBy);
                param[13] = new SqlParameter("@testSecondName", testSecondName);
                param[14] = new SqlParameter("@testSecondEmail", testSecondEmail);
                /////修改 参数
                param[15] = new SqlParameter("@updateBy", updateBy);
                param[16] = new SqlParameter("@updateName", updateName);
                param[17] = new SqlParameter("@updateEmail", updateEmail);

                param[18] = new SqlParameter("@uID", Convert.ToInt32(uID));
                param[19] = new SqlParameter("@uName", uName);
                param[20] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[20].Direction = ParameterDirection.Output;
                param[21] = new SqlParameter("@isDistribute", isDistribute);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[20].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteTaskDet(string mstrNbr, string detID, int process, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_deleteTaskDet";
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@detID", detID);
                param[2] = new SqlParameter("@process", process);
                param[3] = new SqlParameter("@uID", uID);
                param[4] = new SqlParameter("@uName", uName);
                param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[5].Value);
            }
            catch (Exception)
            { 
                return false;
            }
        }
        /// <summary>
        /// 分配具体人员
        /// </summary>
        /// <param name="tskNbr"></param>
        /// <param name="detDesc"></param>
        /// <param name="stdDate"></param>
        /// <param name="duration"></param>
        /// <param name="unit"></param>
        /// <param name="dueDate"></param>
        /// <param name="chargeBy"></param>
        /// <param name="chargeName"></param>
        /// <param name="chargeEmail"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool InsertTaskDet(string tskNbr, string detDesc, string stdDate, string duration, string unit, string dueDate
                    , string chargeBy, string chargeName, string chargeEmail
                    , string uID, string uName, bool IsMonopoly, string taskType)
        {
            try
            {
                string strName = "sp_tsk_insertTaskDet";
                SqlParameter[] param = new SqlParameter[14];
                param[0] = new SqlParameter("@tskNbr", tskNbr);
                param[1] = new SqlParameter("@detDesc", detDesc);
                param[2] = new SqlParameter("@stdDate", stdDate);
                param[3] = new SqlParameter("@duration", duration);
                param[4] = new SqlParameter("@unit", unit);
                param[5] = new SqlParameter("@dueDate", dueDate);
                param[6] = new SqlParameter("@chargeBy", chargeBy);
                param[7] = new SqlParameter("@chargeName", chargeName);
                param[8] = new SqlParameter("@chargeEmail", chargeEmail);
                param[9] = new SqlParameter("@taskType", taskType);
                param[10] = new SqlParameter("@IsMonopoly", IsMonopoly);
                param[11] = new SqlParameter("@uID", uID);
                param[12] = new SqlParameter("@uName", uName);
                param[13] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[13].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[13].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取待分配列表（uRole=1的获取全部，否则只有创建人和跟踪人看得见）
        /// </summary>
        /// <param name="crtDate1"></param>
        /// <param name="crtDate2"></param>
        /// <param name="isDistribute"></param>
        /// <param name="tracker"></param>
        /// <param name="uID"></param>
        /// <param name="uRole"></param>
        /// <returns></returns>
        public static DataTable SelectDistributeList(string mstrNbr, string crtDate1, string crtDate2
                , string status, string tracker, string uID, string uRole)
        {
            try
            {
                string strSql = "sp_tsk_selectDistributeList";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@crtDate1", crtDate1);
                param[2] = new SqlParameter("@crtDate2", crtDate2);
                param[3] = new SqlParameter("@status", status);
                param[4] = new SqlParameter("@tracker", tracker);
                param[5] = new SqlParameter("@uID", uID);
                param[6] = new SqlParameter("@uRole", uRole);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <returns></returns>
        public static bool DeleteTaskMstr(string mstrNbr)
        {
            try
            {
                string strName = "sp_tsk_deleteTaskMstr";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[1].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除主任务的关联文件
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <returns></returns>
        public static bool DeleteTaskMstrFile(string mstrNbr)
        {
            try
            {
                string strName = "sp_tsk_deleteTaskMstrFile";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[1].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="crtDate1"></param>
        /// <param name="crtDate2"></param>
        /// <param name="isDistribute"></param>
        /// <returns></returns>
        public static DataTable SelectTaskList(string mstrNbr, string crtDate1, string crtDate2, bool isCompleted, bool isCanceled, string userId, string uID)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskList";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@crtDate1", crtDate1);
                param[2] = new SqlParameter("@crtDate2", crtDate2);
                param[3] = new SqlParameter("@isCompleted", isCompleted);
                param[4] = new SqlParameter("@isCanceled", isCanceled);
                param[5] = new SqlParameter("@userId", userId);
                param[6] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取任务的处理日志（区别于开发日志和跟踪日志，但包含此类内容）
        /// </summary>
        /// <param name="detID"></param>
        /// <returns></returns>
        public static DataTable SelectTaskLog(string mstrNbr, string type, string uID)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskLog";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@type", type);
                param[2] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 拉取开发日志
        /// </summary>
        /// <param name="detID"></param>
        /// <returns></returns>
        public static DataTable SelectTaskDevping(string detID)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskDevping";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@detID", detID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 保存需求分析和解决方案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="demand"></param>
        /// <param name="solution"></param>
        /// <returns></returns>
        public static bool InsertTaskDevping(string detID, string demand, string uID, string uName, string status)
        {
            try
            {
                string strName = "sp_tsk_insertTaskDevping";
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@detID", detID);
                param[1] = new SqlParameter("@demand", demand);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@uName", uName);
                param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@status", status);
                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[4].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static bool updateTaskDevping(string detID, string demand, string uID, string uName, string status,string date)
        {
            try
            {
                string strName = "sp_tsk_updateTaskDevping";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@detID", detID);
                param[1] = new SqlParameter("@demand", demand);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@uName", uName);
                param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@status", status);
                param[6] = new SqlParameter("@date", date);
                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[4].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 开发步骤完成后（跟踪完成前依然允许修改），通知测试
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool CompleteTaskDevping(string mstrNbr, string detID, string type, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_completeTaskDevping";
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@detID", detID);
                param[2] = new SqlParameter("@type", type);
                param[3] = new SqlParameter("@uID", uID);
                param[4] = new SqlParameter("@uName", uName);
                param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[5].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取测试方案
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <param name="mergeID"></param>
        /// <returns></returns>
        public static DataTable SelectTaskTesting(string mstrNbr, bool notTest)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskTesting";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@notTest", notTest);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 查看全部的测试方案（不论通过与否）
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <param name="detID"></param>
        /// <returns></returns>
        public static DataTable SelectTaskTesting(string mstrNbr, Int32 detID)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskTestingById";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@detID", detID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 创建测试解决方案
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="testingDesc"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool InsertTaskTesting(string detID, string testDesc, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_insertTaskTesting";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@detID", detID);
                param[1] = new SqlParameter("@testDesc", testDesc);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@uName", uName);
                param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[4].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 设置测试结果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="testType"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool UpdateTaskTesting(string id, bool isDet, string testType, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_updateTaskTesting";
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@isDet", isDet);
                param[2] = new SqlParameter("@testType", testType);
                param[3] = new SqlParameter("@uID", uID);
                param[4] = new SqlParameter("@uName", uName);
                param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[5].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除测试方案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool DeleteTaskTesting(string id, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_deleteTaskTesting";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 测试步骤完成后（跟踪完成前依然允许修改），通知更新
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool CompleteTaskTesting(string mstrNbr, string detID, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_completeTaskTesting";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@detID", detID);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@uName", uName);
                param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[4].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 退回重新开发
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool CancelTaskTesting(string detID, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_cancelTaskTesting";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@detID", detID);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取更新Log列表
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="isMstr">真，表示更新；否则，表示日志</param>
        /// <returns></returns>
        public static DataTable SelectTaskUpdating(string mstrNbr, bool notUpdate)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskUpdating";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@notUpdate", notUpdate);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Log上传、未提交的列表
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public static DataTable SelectTaskLogging(string mstrNbr, string uID)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskLogging";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 选取已测试、未上传Log的列表
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <param name="mstrNbr"></param>
        /// <returns></returns>
        public static DataTable SelectTaskLoggingPre(string mstrNbr, string uID)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskLoggingPre";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 保存每次更新Log的系统时间
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool CompleteTaskUpdating(string mstrNbr, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_completeTaskUpdating";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除更新项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool DeleteTaskUpdating(string id, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_deleteTaskUpdating";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 上传Log完成
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool CompleteTaskLogging(string mstrNbr, string detList, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_completeTaskLogging";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@detList", detList);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@uName", uName);
                param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[4].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除更新数据上传后的临时数据
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public static bool DeleteTaskChgTemp(string uID)
        {
            try
            {
                string strName = "sp_tsk_deleteTaskChgTemp";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@uID", uID);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[1].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 拉取更新日志批量导入时的错误列表，同时进行批量验证
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static DataTable SelectBatchTaskChgTemp(string mstrNbr, string uID, string uName)
        {
            try
            {
                string strSql = "sp_tsk_batchTaskChgTemp";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 选取跟踪日志
        /// </summary>
        /// <param name="detID"></param>
        /// <returns></returns>
        public static DataTable SelectTaskTracking(string mstrNbr)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskTracking";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 写入跟踪日志
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="trackDate"></param>
        /// <param name="trackDesc"></param>
        /// <returns></returns>
        public static bool InsertTaskTracking(string mstrNbr, string trackDesc, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_insertTaskTracking";
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@trackDesc", trackDesc);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@uName", uName);
                param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[4].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 跟踪最终完成
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool CompleteTaskTracking(string mstrNbr, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_completeTaskTracking";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 取消，重新从开发开始
        /// </summary>
        /// <param name="detID"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool RestartTaskDet(string mstrNbr, string detID, string step, string reason, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_restartTaskDet";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@detID", detID);
                param[2] = new SqlParameter("@step", step);
                param[3] = new SqlParameter("@reason", reason);
                param[4] = new SqlParameter("@uID", uID);
                param[5] = new SqlParameter("@uName", uName);
                param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[6].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭任务
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static bool CloseTaskMstr(string mstrNbr, string uID, string uName)
        {
            try
            {
                string strName = "sp_tsk_closeTaskMstr";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@uID", uID);
                param[2] = new SqlParameter("@uName", uName);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 验证任务是否可以关闭
        /// </summary>
        /// <param name="mstrNbr"></param>
        /// <returns></returns>
        public static bool CouldTaskMstrClosed(string mstrNbr)
        {
            try
            {
                string strName = "sp_tsk_couldTaskMstr";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@mstrNbr", mstrNbr);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return Convert.ToBoolean(param[1].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool setkfinish(string detID)
        {
            try
            {
                string strName = "sp_tsk_updateTaskfinish";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@detID", detID);
              
                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #region 报表
        public static DataSet GetChargerTaskDet(string id, DateTime time)
        {
            try
            {
                string strName = "sp_tsk_selectChargerTaskDet";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@chargeid", id);
                param[1] = new SqlParameter("@time", time);
                DataSet dt;
                dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
                return dt;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public static Charger GetChargerTaskDets(string id, DateTime time)
        {
            try
            {
                string strName = "sp_tsk_selectChargerTaskDet";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@chargeid", id);
                param[1] = new SqlParameter("@time", time);
                DataSet dt;
                dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);

                foreach (DataRow col in dt.Tables[1].Rows)
                {
                    Charger item = new Charger();
                    item.Daytime = time;
                    item.Hours = col[0].ToString();
                    item.Monopoly = col[1].ToString();
                    item.Daytimes = string.Format("{0:yyyy-MM-dd}", time);
                    if (item.Hours == "")
                    {
                        return null;
                    }
                    else
                    {
                        return item;
                    }
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static IList<Charger> addCharger(string id, DateTime time)
        {
            IList<Charger> list = new List<Charger>();
            while (true)
            {
                Charger item = GetChargerTaskDets(id, time);
                if (item != null)
                {
                    list.Add(item);
                }
                else
                {
                    break;
                }
                time = time.AddDays(1);
            }
            return list;
        }
        /// <summary>
        /// 生成甘特图，并写入数据（数据源必须有_id、_index、_day、_desc、_flag四个字段）：_id唯一标识，_index用于判定是否显示More, _flag判定记录状态
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="more"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GenerateGannt(int year, int month, int more, DataTable table)
        {
            DateTime _stdDate = Convert.ToDateTime(year.ToString() + "-" + month.ToString() + "-1");
            DateTime _endDate = _stdDate.AddMonths(1);

            string _html = "<table class=\"TaskGannt\" cellpadding=\"0\" cellspacing=\"0\">";
            _html += "<thead>";
            _html += "  <tr>";
            _html += "      <td>SUN 日</td>";
            _html += "      <td>MON 一</td>";
            _html += "      <td>TUE 二</td>";
            _html += "      <td>WED 三</td>";
            _html += "      <td>THU 四</td>";
            _html += "      <td>FRI 五</td>";
            _html += "      <td>SAT 六</td>";
            _html += "  </tr>";
            _html += "</thead>";
            _html += "<tbody>";

            _html += "<tr>";
            while (_stdDate < _endDate)
            {
                //第一天，周几之前要留白
                //最后一天，周几之后要留白
                if (_stdDate.Day == 1)
                {
                    for (int i = 0; i < (int)_stdDate.DayOfWeek; i++)
                    {
                        _html += "<td class=\"GanntEmpty\"></td>";
                    }
                }

                _html += "<td>";
                _html += "  <div class='TaskDay'>" + _stdDate.Day.ToString() + "</div>";

                foreach (DataRow row in table.Select("_day = " + _stdDate.Day.ToString()))
                {
                    //前两个正常显示，第三个显示More...
                    if (Convert.ToInt32(row["_index"]) == more)
                    {
                        _html += "<span class='TaskMore'><u>More...</u></span>";
                    }
                    else
                    {
                        //当天没有维护开发日志的，显示红色
                        if (Convert.ToBoolean(row["_isMaintLog"]))
                        {
                            _html += "<span class='TaskDesc' flag=" + row["_id"].ToString() + "><span class='TaskFlag'>&nbsp;" + row["_flag"].ToString() + "&nbsp;</span>" + row["_desc"].ToString() + "</span>";
                        }
                        else
                        {
                            _html += "<span class='TaskDesc' flag=" + row["_id"].ToString() + "><span class='TaskFlag' style='background-color: red;'>&nbsp;" + row["_flag"].ToString() + "&nbsp;</span>" + row["_desc"].ToString() + "</span>";
                        }
                    }
                }

                _html += "</td>";

                if (_stdDate.AddDays(1) == _endDate)
                {
                    for (int i = (int)_stdDate.DayOfWeek; i < (int)DayOfWeek.Saturday; i++)
                    {
                        _html += "<td class=\"GanntEmpty\"></td>";
                    }
                }

                if (_stdDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    //周六封行
                    _html += "</tr><tr>";
                }

                _stdDate = _stdDate.AddDays(1);
            }

            _html += "</tr>";
            _html += "</tbody>";
            _html += "</table>";

            return _html;
        }
        /// <summary>
        /// 显示甘特图明细（展示任务的持续时长）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="stdDay">任务的开始时间</param>
        /// <param name="endDay">任务的结束时间</param>
        /// <param name="endDay">任务的结束时间</param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GenerateGanntDetail(int year, int month, DateTime stdDate, DateTime dueDate, DateTime endDate, string _mailTo, DataTable table)
        {
            DateTime _stdDate = Convert.ToDateTime(year.ToString() + "-" + month.ToString() + "-1");
            DateTime _endDate = Convert.ToDateTime(endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-1").AddMonths(1);
            //去除开始日期的小时
            stdDate = Convert.ToDateTime(stdDate.ToShortDateString());

            int _stdMonth = _stdDate.Month;//通常，只有第一个月份要留白

            string _html = "<table class=\"TaskGannt\" cellpadding=\"0\" cellspacing=\"0\">";
            _html += "<thead>";
            _html += "  <tr>";
            _html += "      <td>SUN 日</td>";
            _html += "      <td>MON 一</td>";
            _html += "      <td>TUE 二</td>";
            _html += "      <td>WED 三</td>";
            _html += "      <td>THU 四</td>";
            _html += "      <td>FRI 五</td>";
            _html += "      <td>SAT 六</td>";
            _html += "  </tr>";
            _html += "</thead>";
            _html += "<tbody>";

            _html += "<tr>";
            while (_stdDate < _endDate)
            {
                //第一天，周几之前要留白
                //最后一天，周几之后要留白
                if (_stdDate.Day == 1 && _stdMonth == _stdDate.Month)
                {
                    for (int i = 0; i < (int)_stdDate.DayOfWeek; i++)
                    {
                        _html += "<td class=\"GanntEmpty\"></td>";
                    }
                }

                _html += "<td>";
                string _day = _stdDate.Day.ToString();

                if (_stdDate.Day == 1)
                {
                    _day = "(" + _stdDate.Month.ToString() + "月)1";
                }

                //stdDay、endDay之间的日期，绿色，否则粉色，表示过期
                if (_stdDate >= stdDate && _stdDate <= dueDate)
                {
                    _html += "  <div class='TaskDay TaskDayNormal'>" + _day + "</div>";
                }
                else if (_stdDate > dueDate && _stdDate <= endDate)
                {
                    _html += "  <div class='TaskDay TaskDayDelay'>" + _day + "</div>";
                }
                else
                {
                    _html += "  <div class='TaskDay'>" + _day + "</div>";
                }

                foreach (DataRow row in table.Select("_month = " + _stdDate.Month.ToString() + " And _day = " + _stdDate.Day.ToString()))
                {
                    _html += "<span class='TaskDetailDesc'>" + row["_desc"].ToString() + "&nbsp;&nbsp;<a href=\"mailto:" + _mailTo + "?subject=IT Task回复&body=" + row["_desc"].ToString() + "\">回复</a></span>";
                }

                _html += "</td>";

                if (_stdDate.AddDays(1) == _endDate && _stdDate.AddDays(1).Month == _endDate.Month)
                {
                    for (int i = (int)_stdDate.DayOfWeek; i < (int)DayOfWeek.Saturday; i++)
                    {
                        _html += "<td class=\"GanntEmpty\"></td>";
                    }
                }

                if (_stdDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    //周六封行
                    _html += "</tr><tr>";
                }

                _stdDate = _stdDate.AddDays(1);
            }

            _html += "</tr>";
            _html += "</tbody>";
            _html += "</table>";

            return _html;
        }
        /// <summary>
        /// 任务的汇总
        /// </summary>
        /// <param name="crtDate1"></param>
        /// <param name="crtDate2"></param>
        /// <param name="isDistribute"></param>
        /// <param name="tracker"></param>
        /// <returns></returns>
        public static DataTable SelectChargerTotal(string crtDate1, string crtDate2, bool isDistribute, string tracker)
        {
            try
            {
                string strSql = "sp_tsk_selectChargerTotal";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@uYear", crtDate1);
                param[1] = new SqlParameter("@uMonth", crtDate2);
                param[2] = new SqlParameter("@IsComplay", isDistribute);
                param[3] = new SqlParameter("@chargeid", tracker);


                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 任务的甘特图
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="type">查看的是“开发”还是“跟踪”</param>
        /// <returns></returns>
        public static DataTable SelectTaskGanntMstr(string year, string month, string type, string userId)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskGanntMstr";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@year", year);
                param[1] = new SqlParameter("@month", month);
                param[2] = new SqlParameter("@type", type);
                param[3] = new SqlParameter("@userId", userId);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取甘特图明细
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="detID"></param>
        /// <returns></returns>
        public static DataTable SelectTaskGanntDetail(string year, string month, string detID, string type)
        {
            try
            {
                string strSql = "sp_tsk_selectTaskGanntDetail";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@year", year);
                param[1] = new SqlParameter("@month", month);
                param[2] = new SqlParameter("@detID", detID);
                param[3] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 按人统计当前所有未结任务
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectChargerDay()
        {
            try
            {
                string strSql = "sp_tsk_selectChargerDay";
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 按月统计（展示）每日安排任务的时长
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataTable SelectChargerTime(string year, string month)
        {
            try
            {
                string strSql = "sp_tsk_selectChargerTime";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@year", year);
                param[1] = new SqlParameter("@month", month);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取所有JOB的历史和下次执行时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DataTable SelectJobsHistoryAndSchedule(string date, bool whichTime)
        {
            try
            {
                string strSql = "sp_tsk_selectJobHistoryAndNext";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@date", date);
                param[1] = new SqlParameter("@whichTime", whichTime);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}