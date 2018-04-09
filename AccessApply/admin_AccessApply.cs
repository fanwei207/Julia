using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using CommClass;
using System.Data.SqlClient;
using adamFuncs;
using System.Net.Mail;

/// <summary>
/// Summary description for admin_AccessApply
/// </summary>
public class admin_AccessApply
{
    static adamClass adam = new adamClass();
    public admin_AccessApply()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    ///  获取登录者 申请者的相关信息
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static DataTable getApplyUserInfo(int userId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userId", userId);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectUserInfoById", param).Tables[0];
    }

   
    
    /// <summary>
    /// 获取所有大分类下菜单名称、菜单号
    /// </summary>
    /// <returns>菜单号、名称+ 描述</returns>
    public static DataTable getRootMenuInfo()
    {
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectRootMenu").Tables[0];
        return dt;
    }
    /// <summary>
    /// 获取子菜单的名称、菜单号
    /// </summary>
    /// <param name="irootMenuId"></param>
    /// <returns></returns>
    public static DataTable getchildMenuInfo(int irootMenuId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rootMenuId", irootMenuId);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectChildMenu", param).Tables[0];
        return dt;
    }

    /// <summary>
    /// 获取公司
    /// </summary>
    /// <returns></returns>
    public static DataTable getCompanyInfo()
    {
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectCompanyInfo").Tables[0];
        return dt;
    }

    public static DataTable getDepartmentInfo(int plantCode)
    {
        string strSQL;
        strSQL = "SELECT departmentID,name From tcpc" + plantCode + ".dbo.departments where active = 1 and issalary=1 order by name";

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, strSQL).Tables[0];
        return dt;
    }


    public static DataTable getDepartmentName(int plantCode, int idepartmentID)
    {
        string strSQL;
        strSQL = "SELECT  name From tcpc" + plantCode + ".dbo.departments where active = 1 and issalary=1 and departmentID = " + idepartmentID;

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, strSQL).Tables[0];
        return dt;
    }

    public static DataTable getUserByDepart(int departmentId, string usrName, string plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@departmentId", departmentId);
        param[1] = new SqlParameter("@usrName", usrName);
        param[2] = new SqlParameter("@plantCode", plantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectUserInfoBydepart", param).Tables[0];
        return dt;
    }

    //public static void SendEmail(string mailto, string mailfrom, string displayName, string mailcc, string mailSubject, string mailContent)
    //{
    //    MailAddress to = new MailAddress(mailto);
    //    MailAddress from = new MailAddress(mailfrom, displayName);
    //    MailMessage message = new MailMessage(from, to);

    //    message.CC.Add(mailcc);
    //    message.Subject = mailSubject + " " + DateTime.Now.ToString("MM-dd-yyyy");
    //    message.Body = mailContent;
    //    message.IsBodyHtml = true;               //邮件显示的格式,以HTML格式显示
    //    message.BodyEncoding = System.Text.Encoding.UTF8;
    //    message.IsBodyHtml = true;

    //    string smtp = ConfigurationManager.AppSettings["mailServer"].ToString();
    //    SmtpClient smtpClient = new SmtpClient(smtp);
    //    try
    //    {
    //        smtpClient.Send(message);
    //    }
    //    catch
    //    { 
    //    }
    //}
    public static string SendEmail(string mailto, string mailfrom, string displayName, string mailcc, string mailSubject, string mailContent)
    {

        String msg = "";
        MailAddress to = new MailAddress(mailto);
        MailAddress from = new MailAddress(mailfrom, displayName);
        MailMessage message = new MailMessage(from, to);
        try
        {
          
        message.CC.Add(mailcc);
        message.Subject = mailSubject + " " + DateTime.Now.ToString("MM-dd-yyyy");
        message.Body = mailContent;
        message.IsBodyHtml = true;               //邮件显示的格式,以HTML格式显示
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.IsBodyHtml = true;

        string smtp = ConfigurationManager.AppSettings["mailServer"].ToString();
       
        SmtpClient smtpClient = new SmtpClient(smtp);
         smtpClient.Send(message);
        }
        catch
        {
            msg = "发送邮件失败";
        }

        return msg;
    }
    public static string SendEmail(string mailto, string mailfrom, string displayName, string mailSubject, string mailContent)
    {
        String msg = "";
        MailAddress to;
        try
        {
           to = new MailAddress(mailto);
        }
        catch (Exception)
        {

            return "邮箱地址格式不正确!";
        }
       
        MailAddress from = new MailAddress(mailfrom, displayName);
        MailMessage message = new MailMessage(from, to);
        try
        {
            message.Subject = mailSubject + " " + DateTime.Now.ToString("MM-dd-yyyy");
            message.Body = mailContent;
            message.IsBodyHtml = true;               //邮件显示的格式,以HTML格式显示
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            string smtp = ConfigurationManager.AppSettings["mailServer"].ToString();
            SmtpClient smtpClient = new SmtpClient(smtp);
            smtpClient.Send(message);
        }
        catch
        {
            msg = "发送邮件失败";
        }

        return msg;

    }

    /// <summary>
    ///  新建权限申请单
    /// </summary>
    /// <param name="iApplyUserId">申请人id</param>
    /// <param name="strApplyUserName">申请人姓名</param>
    /// <param name="iApplyDeptId">申请人所在部门id</param>
    /// <param name="strApplyDeptName">申请人所在部门</param>
    /// <param name="strApplyReason">申请理由</param>
    /// <returns>新建权限申请的ID号</returns>
    public static int newAccessApplyInfo(int iApplyUserId, string strApplyUserName,string strApplyUserEmail, int iApplyDeptId, string strApplyDeptName, string strApplyReason,int approveUserID, string approveUserName)
    {

        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@iApplyUserId", iApplyUserId);
        param[1] = new SqlParameter("@strApplyUserName", strApplyUserName);
        param[2] = new SqlParameter("@strApplyUserEmail", strApplyUserEmail);
        param[3] = new SqlParameter("@iApplyDeptId", iApplyDeptId);
        param[4] = new SqlParameter("@strApplyDeptName", strApplyDeptName);
        param[5] = new SqlParameter("@strApplyReason", strApplyReason);
        param[6] = new SqlParameter("@approveUserID", approveUserID);
        param[7] = new SqlParameter("@approveUserName", approveUserName);
        param[8] = new SqlParameter("@returnVar", SqlDbType.Int);
        param[8].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_insertAccessApplymstr", param);
        return Convert.ToInt32(param[8].Value);
    }
    /// <summary>
    /// 将新建的权限申请模块记入表中
    /// </summary>
    /// <param name="iApplyId"></param>
    /// <param name="iModuleId"></param>
    /// <param name="strModuleName"></param>
    /// <param name="strModuleDesc"></param>
    public static void newApplyRecord(int iApplyId, int iModuleId, string strModuleName, string strModuleDesc)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@iApplyId", iApplyId);
        param[1] = new SqlParameter("@iModuleId", iModuleId);
        param[2] = new SqlParameter("@strModuleName", strModuleName);
        param[3] = new SqlParameter("@strModuleDesc", strModuleDesc);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_insertAccessApplyDetail", param);
    }

    public static int newApplyApproveProcess(int iApplyId, int iApproveUserId, string strApproveName)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@iApplyId", iApplyId);
        param[1] = new SqlParameter("@iApproveUserId", iApproveUserId);
        param[2] = new SqlParameter("@strApproveName", strApproveName);

        //SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_insertAccessApplyProcess", param);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_insertAccessApplyProcess", param));
    }
    /// <summary>
    /// 获取申请记录的信息
    /// </summary>
    /// <param name="strApplyName">申请者</param>
    /// <param name="idispayOnlyToApprove"></param>
    /// <returns></returns>
    public static DataSet getAccessApplyMstr(string strApplyName, bool idispayOnlyToApprove)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@strApplyName", strApplyName);
        param[1] = new SqlParameter("@idispayOnlyToApprove", idispayOnlyToApprove);

        return  SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectAccessApplyMstr", param);
    }
    /// <summary>
    /// 获取某一确定的申请记录信息
    /// </summary>
    /// <param name="iApplyId">申请单号，唯一标识</param>
    /// <param name="strApplyName"></param>
    /// <param name="idispayOnlyToApprove"></param>
    /// <returns></returns>
    public static DataSet getAccessApplyMstr(int iApplyId, string strApplyName, bool idispayOnlyToApprove)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@iApplyId", iApplyId);
        param[1] = new SqlParameter("@strApplyName", strApplyName);
        param[2] = new SqlParameter("@idispayOnlyToApprove", idispayOnlyToApprove);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectAccessApplyMstr", param);
    }

    public static DataSet getAccessApplyMstr(int iApplyId, string strApplyName, bool idispayOnlyToApprove, int iCurrentUserId)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@iApplyId", iApplyId);
        param[1] = new SqlParameter("@strApplyName", strApplyName);
        param[2] = new SqlParameter("@idispayOnlyToApprove", idispayOnlyToApprove);
        param[3] = new SqlParameter("@iCurrentUserId", iCurrentUserId);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectAccessApplyMstr", param);
    }

    /// <summary>
    ///  选取某一申请权限审批记录
    /// </summary>
    /// <param name="iApplyId">唯一申请权限Id</param>
    /// <returns></returns>
    public static DataSet getAccessApproveProcess(int iApplyId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@iApplyId", iApplyId);
      
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectAccessApproveProcess", param);
    }

    public static DataSet getAccessApplyMstr(int iApplyId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@iApplyId", iApplyId);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectAccessApplyMstr", param);
    }

    public static void updateApplyModuleDetail(int iApplyId, int iApproveUserID, int iModuleID, int selectvalue)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@iApplyId", iApplyId);
        param[1] = new SqlParameter("@iApproveUserID", iApproveUserID);
        param[2] = new SqlParameter("@iModuleID", iModuleID);
        param[3] = new SqlParameter("@selectvalue", selectvalue);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_updateAccessApplyDetail", param);
    }
    /// <summary>
    /// 申请人在没有提交或审批人没有会签之前，可对删除申请记录
    /// </summary>
    /// <param name="iApplyId">申请记录的唯一标识</param>
    public static void deleteAccessApplymstr(int iApplyId)
    {
        
        string strSQL;
        strSQL = "Delete admin_AccessApplymstr where aamId =" + iApplyId;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    /// <summary>
    /// 申请人在没有提交或审批人没有会签之前，可对删除申请记录
    /// </summary>
    /// <param name="iApplyId"></param>
    public static void deleteAccessApplyDetail(int iApplyId)
    {
        string strSQL;
        strSQL = "Delete admin_AccessApplyDetail where aamId =" + iApplyId;

       SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }


    public static DataTable getAccessApplyModuleDetail(int iApplyId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@iApplyId", iApplyId);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectAccessApplyModuleDetail", param).Tables[0];
    }

    public static void updateAccessApproveProcess(int iApplyId, int iApproveUserID, string strApproveView, int inextApproveUserID, string strnextApproveUserName, int selectvalue)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@iApplyId", iApplyId);
        param[1] = new SqlParameter("@iApproveUserID", iApproveUserID);
        param[2] = new SqlParameter("@strApproveView", strApproveView);
        param[3] = new SqlParameter("@inextApproveUserID", inextApproveUserID);
        param[4] = new SqlParameter("@strnextApproveUserName", strnextApproveUserName);
        param[5] = new SqlParameter("@selectvalue", selectvalue);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_updateAccessApproveProcess", param);
  
    }

    public static void updateAccessApplymstr(int iApplyId, string strApplyReason)
    {
        string strSQL;
        strSQL = "Update admin_AccessApplymstr Set applyReason = N' " + strApplyReason + "' where aamId =" + iApplyId;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public static bool insertApplyInternetAccMstr(string strApplyUserId, string strApplyUserName, string strApplyLoginName, string strApplyUPlant, string strApplyReason, string strApproveUserId, string strApproveUserName)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@applyUserId", Convert.ToInt32(strApplyUserId));
        param[1] = new SqlParameter("@applyUserName", strApplyUserName);
        param[2] = new SqlParameter("@applyloginName", strApplyLoginName);
        param[3] = new SqlParameter("@plantCode", strApplyUPlant);
        param[4] = new SqlParameter("@applyAccReason", strApplyReason);
        param[5] = new SqlParameter("@approveUserId", Convert.ToInt32(strApproveUserId));
        param[6] = new SqlParameter("@approveUserName", strApproveUserName);
        
        return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_insertApplyInternetAcc", param));
    }

    public static DataTable getApplyOuterNetAccessInfo(int iPlantCode, string strUserName, bool ischecked)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@plantCode", iPlantCode);
        param[1] = new SqlParameter("@applyUserName", strUserName);
        param[2] = new SqlParameter("@ischecked", ischecked);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_selectApplyOuterNetAcc", param).Tables[0];

    }

    
    public static bool UpdateApplyInternetAcc(string id, string result)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", Convert.ToInt32(id));
        param[1] = new SqlParameter("@result", result);

        return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_admin_UpdateApplyAccNetResult", param));
    }

    public static DataTable isHaveInternetAcess(string userID)
    {
        String strSql = "Select Isnull(accInternet, 0) As accInternet from tcpc0.dbo.Users where userID =" + userID;
        return  SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];
    }

    public static DataTable isHaveInternetApply(string userID)
    {
         String strSql = @" If Exists (select id  from tcpc0.dbo.admin_ApplyOuterNetAccess100Sys  where applyUserId = "+ userID + " and approveResult is null)  select 1  else select 0 ";
         return  SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];
        
    }

    public static void DeleteApplyInternetAcc(string id)
    {
        String strSql = @" Delete tcpc0.dbo.admin_ApplyOuterNetAccess100Sys  where id= " + id;
        
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql);
    }
}

