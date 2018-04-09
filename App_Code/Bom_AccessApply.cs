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
public class Bom_AccessApply
{
    static adamClass adam = new adamClass();
    public Bom_AccessApply()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    /// <summary>
    /// 判断物料号是否存在
    /// </summary>
    /// <returns></returns>
    public static DataTable CheckBomIsExsit(string to_bom, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@to_bom", to_bom);
        param[1] = new SqlParameter("@plantCode", plantCode);
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_CheckToBomExsit", param).Tables[0];
        return dt;
    }


    /// <summary>
    /// 判断物料号是否存在更改未完成
    /// </summary>
    /// <returns></returns>
    public static DataTable CheckBomIsExsitChange(string father_bom,string son_bom,string type, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@father_bom", father_bom);
        param[1] = new SqlParameter("@son_bom", son_bom);
        param[2] = new SqlParameter("@type", type);
        param[3] = new SqlParameter("@plantCode", plantCode);
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_CheckBomIsExsitChange", param).Tables[0];
        return dt;
    }

    public static DataTable getbomown(string bom, string bom1, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@bom", bom);
        param[1] = new SqlParameter("@bom1", bom1);
        param[2] = new SqlParameter("@plantCode", plantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_getbomown", param).Tables[0];
        return dt;
    }

    public static DataTable CheckBomOwnCheckinfo(string Bom_id, int uid,int plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Bom_id", Bom_id);
        param[1] = new SqlParameter("@uid", uid);
        param[2] = new SqlParameter("@plantCode", plantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_getbomown", param).Tables[0];
        return dt;
    }

    public static DataTable GetBomCheckInfos(string Bom_id,int uid,int plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Bom_id", Bom_id);
        param[1] = new SqlParameter("@uid", uid);
        param[2] = new SqlParameter("@plantCode", plantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_GetBomCheckInfos", param).Tables[0];
        return dt;
    }



    public static DataTable GetBomCheckDetails(string Father_Bom, string Types,int uid, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@Father_Bom", Father_Bom);
        param[1] = new SqlParameter("@Types", Types);
        param[2] = new SqlParameter("@uid", uid);
        param[3] = new SqlParameter("@plantCode", plantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_GetBomCheckDetails", param).Tables[0];
        return dt;
    }

    public static DataTable GetBomCheckHaveDetails(string Bom_id, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Bom_id", Bom_id);
        //param[1] = new SqlParameter("@Types", Types);
        param[2] = new SqlParameter("@plantCode", plantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_GetBomCheckHaveDetails", param).Tables[0];
        return dt;
    }


    public static DataTable getbom(string bom, string domain)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@bom", bom);
        param[1] = new SqlParameter("@domain", domain);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_Bom_Update", param).Tables[0];
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

        //string smtp = ConfigurationManager.AppSettings["mailServer"].ToString();
       
        //SmtpClient smtpClient = new SmtpClient(smtp);
        // smtpClient.Send(message);

         if (!BasePage.SSendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), mailto, "", mailSubject + " " + DateTime.Now.ToString("MM-dd-yyyy") + "  from " + mailfrom, mailContent))
         {
             msg = "发送邮件失败";
         }

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
        MailAddress to = new MailAddress(mailto);
        MailAddress from = new MailAddress(mailfrom, displayName);
        MailMessage message = new MailMessage(from, to);
        try
        {
            message.Subject = mailSubject + " " + DateTime.Now.ToString("MM-dd-yyyy");
            message.Body = mailContent;
            message.IsBodyHtml = true;               //邮件显示的格式,以HTML格式显示
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            //string smtp = ConfigurationManager.AppSettings["mailServer"].ToString();
            //SmtpClient smtpClient = new SmtpClient(smtp);
            //smtpClient.Send(message);

            if (!BasePage.SSendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), mailto, "", mailSubject + " " + DateTime.Now.ToString("MM-dd-yyyy") + "  from " + mailfrom, mailContent))
            {
                msg = "发送邮件失败";
            }
        }
        catch
        {
            msg = "发送邮件失败";
        }

        return msg;

    }

    /// <summary>
    ///  新建BOM更改明细
    /// </summary>
    /// <param name="iApplyUserId">申请人id</param>
    /// <param name="strApplyUserName">申请人姓名</param>
    /// <param name="iApplyDeptId">申请人所在部门id</param>
    /// <param name="strApplyDeptName">申请人所在部门</param>
    /// <param name="strApplyReason">申请理由</param>
    /// <returns>新建权限申请的ID号</returns>
    //public static int newAccessApplyInfo(string Father_Bom, string Son_Bom, DateTime StartTime, DateTime EndTime, string Toson_Bom, DateTime TostartTime, DateTime ToendTime, string CreateBy, string ApplyReason)
    public static int newAccessApplyInfo(string Father_Bom, string Son_Bom, string StartTime, string EndTime, string types, string Toson_Bom, string TostartTime, string ToendTime, string CreateBy, string ApplyReason, string chooseid, int PlantCode,string count,string badcount)
    {

        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@Father_Bom", Father_Bom);
        param[1] = new SqlParameter("@Son_Bom", Son_Bom);
        param[2] = new SqlParameter("@StartTime", StartTime);
        param[3] = new SqlParameter("@EndTime", EndTime);
        param[4] = new SqlParameter("@types", types);
        param[5] = new SqlParameter("@Toson_Bom", Toson_Bom);
        param[6] = new SqlParameter("@TostartTime", TostartTime);
        param[7] = new SqlParameter("@ToendTime", ToendTime);
        param[8] = new SqlParameter("@CreateBy", CreateBy);
        param[9] = new SqlParameter("@ApplyReason", ApplyReason);
        param[10] = new SqlParameter("@chooseid", chooseid);
        param[11] = new SqlParameter("@PlantCode", PlantCode);
        param[12] = new SqlParameter("@count", count);
        param[13] = new SqlParameter("@badcount", badcount);
        param[14] = new SqlParameter("@returnVar", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_bom_insertAccessChangebom", param);
        return Convert.ToInt32(param[14].Value);
    }

    /// <summary>
    ///  BOM处理
    /// </summary>
    /// <param name="iApplyUserId">申请人id</param>
    /// <param name="strApplyUserName">申请人姓名</param>
    /// <param name="iApplyDeptId">申请人所在部门id</param>
    /// <param name="strApplyDeptName">申请人所在部门</param>
    /// <param name="strApplyReason">申请理由</param>
    /// <returns>新建权限申请的ID号</returns>
    //public static int newAccessApplyInfo(string Father_Bom, string Son_Bom, DateTime StartTime, DateTime EndTime, string Toson_Bom, DateTime TostartTime, DateTime ToendTime, string CreateBy, string ApplyReason)
    public static int AccessBomCheckInfo(string bom_id, int uid, string result, int PlantCode,string suggestsuggest)
    {

        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@bom_id", bom_id);
        param[1] = new SqlParameter("@uid", uid);
        param[2] = new SqlParameter("@result", result);
        param[3] = new SqlParameter("@chooseid", "");
        param[4] = new SqlParameter("@PlantCode", PlantCode);
        param[5] = new SqlParameter("@returnVar", SqlDbType.Int);
        param[5].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_bom_check", param);
        return Convert.ToInt32(param[5].Value);
    }

    /// <summary>
    ///  BOM提交处理
    /// </summary>
    /// <param name="iApplyUserId">申请人id</param>
    /// <param name="strApplyUserName">申请人姓名</param>
    /// <param name="iApplyDeptId">申请人所在部门id</param>
    /// <param name="strApplyDeptName">申请人所在部门</param>
    /// <param name="strApplyReason">申请理由</param>
    /// <returns>新建权限申请的ID号</returns>
    //public static int newAccessApplyInfo(string Father_Bom, string Son_Bom, DateTime StartTime, DateTime EndTime, string Toson_Bom, DateTime TostartTime, DateTime ToendTime, string CreateBy, string ApplyReason)
    public static int BomCheckInfo(string bom_id, int uid, string result, string chooseid,string suggest, int PlantCode)
    {

        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@bom_id", bom_id);
        param[1] = new SqlParameter("@uid", uid);
        param[2] = new SqlParameter("@result", result);
        param[3] = new SqlParameter("@chooseid", chooseid);
        param[4] = new SqlParameter("@suggest", suggest);
        param[5] = new SqlParameter("@PlantCode", PlantCode);
        param[6] = new SqlParameter("@returnVar", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_bom_check", param);
        return Convert.ToInt32(param[6].Value);
    }

    /// <summary>
    ///  获取物料库存
    /// </summary>
    /// <param name="bom">物料号</param>
    /// <param name="PlantCode">域</param>

    public static DataTable GetBomInv(string bom,string tobom, int PlantCode)
    {

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@bom", bom);
        param[1] = new SqlParameter("@tobom", tobom);
        param[2] = new SqlParameter("@PlantCode", PlantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_bom_inv", param).Tables[0];
        return dt;
    }
    /// <summary>
    ///  获取物料在途
    /// </summary>
    /// <param name="bom">物料号</param>
    /// <param name="PlantCode">域</param>
    public static DataTable GetBomOnInv(string bom, string tobom,int PlantCode)
    {

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@bom", bom);
        param[1] = new SqlParameter("@tobom", tobom);
        param[2] = new SqlParameter("@PlantCode", PlantCode);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_bom_oninv", param).Tables[0];
        return dt;
    }   
}

