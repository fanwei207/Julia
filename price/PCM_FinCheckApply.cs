using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.Net.Mail;
using System.Text;
using adamFuncs;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

/// <summary>
/// Summary description for PC_FinCheckApply
/// </summary>
public class PCM_FinCheckApply
{
    private static adamClass adm = new adamClass();


	public PCM_FinCheckApply()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //guid生成函数
    private string getGUID()
    {
        System.Guid guid = new Guid();
        guid = Guid.NewGuid();
        string str = guid.ToString();
        return str;
    }
    public DataTable GetPartCheckPriceFinishedList(string part, string itemCode, int finishedStatus, string vender, string inquiry)
    {
        string strName = "sp_pcm_selectCheckPriceFinishedForPart";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@part", part);
        parm[1] = new SqlParameter("@ItemCode", itemCode);
        parm[2] = new SqlParameter("@finished", finishedStatus);
        parm[3] = new SqlParameter("@vender", vender);
        parm[4] = new SqlParameter("@inquiry", inquiry);

        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
    }

    public DataTable GetFinCheckApplyPart(string part,string pqid)
    {
        string strName = "sp_pcm_selectFinCheckApplyPart";
        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@part", part);
        parm[1] = new SqlParameter("@pqId", pqid);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
    }

    public DataTable GetApply(string id)
    {
        string strName = "sp_pcm_selectFinCheckApply";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@ApplyId", id);    
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
    }

    public DataTable GetApplyDet(string id)
    {
        string strName = "sp_pcm_selectFinCheckApplyDet";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@ApplyId", id);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
    }

    public DataTable GetApplyDet(DataTable partTable)
    {
        StringWriter writer = new StringWriter();
        partTable.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string strName = "sp_pcm_selectFinCheckApplyDetForNew";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@parts", xmlDetail);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
    }

    public DataTable GetApprove(string applyId)
    {
        string strName = "sp_pcm_selectFinCheckApprove";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@applyId", applyId);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
    }

    public int AddApply(string approveBy, string applyReason, string applyBy, DataTable detail)
    {
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@approveBy", approveBy);
        param[1] = new SqlParameter("@applyReason", applyReason);
        param[2] = new SqlParameter("@applyBy", applyBy);
        param[3] = new SqlParameter("@detail", xmlDetail);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_insertFinCheckApply", param));
    }

    public void UpdateApplyDet(DataTable detail)
    {
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@detail", xmlDetail);

        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_batchUpdateFinCheckApplyDet", sqlParam);
    }

    public bool Approve(string applyId, string approveBy, string uId, string approveOpinion, bool appoveresult)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@applyId", applyId);
        param[1] = new SqlParameter("@approveBy", approveBy);
        param[2] = new SqlParameter("@uId", uId);
        param[3] = new SqlParameter("@approveOpinion", approveOpinion);
        param[4] = new SqlParameter("@appoveresult", appoveresult);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_updateFinCheckApprove", param));

    }

    public bool Pass(string applyId, string approveBy, string uId, string approveOpinion)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@applyId", applyId);
        param[1] = new SqlParameter("@approveBy", approveBy);
        param[2] = new SqlParameter("@uId", uId);
        param[3] = new SqlParameter("@approveOpinion", approveOpinion);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_PassFinCheckApprove", param));
    }

    public DataTable GetApplyList(string itemCode, string part, string applyDate, bool pendingToAppr, string userId, string roleId)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@itemCode", itemCode);
        param[1] = new SqlParameter("@qadPart", part);
        param[2] = new SqlParameter("@applyDate", applyDate);
        param[3] = new SqlParameter("@pendingToAppr", pendingToAppr);
        param[4] = new SqlParameter("@uId", userId);
        param[5] = new SqlParameter("@roleId", roleId);


        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_selectFinCheckApplyList", param).Tables[0];
    }

    public DataTable GetFinPrice(string itemCode, string part, string vender, int type, string status)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@itemCode", itemCode);
        param[1] = new SqlParameter("@part", part);
        param[2] = new SqlParameter("@vender", vender);
        param[3] = new SqlParameter("@type", type);
        param[4] = new SqlParameter("@status", status);


        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_selectApplyDetForFin", param).Tables[0];
    }

    public void FinCancel(DataTable detail, int type)
    {
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@detail", xmlDetail);
        sqlParam[1] = new SqlParameter("@type", type);
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_updateApplyDetForFinCancel", sqlParam);
    }

    public void FinHandle(DataTable detail, int type)
    {
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@detail", xmlDetail);
        sqlParam[1] = new SqlParameter("@type", type);
        DataTable dtUsers = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_updateApplyDetForHandle", sqlParam).Tables[0];

        if (dtUsers.Rows[0][0].ToString() != "0" && type==2)
        {
            
            foreach (DataRow row in dtUsers.Rows)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append(" Dear  申请者  <br>");
                sb.Append("     您申请的零件：<br>");
                sb.Append(row["Parts"].ToString().Replace(";", "<br>")+"<br>");
                sb.Append("     审核流程已通过。<br>");
                sb.Append("     财务正导入到QAD系统，大约15分钟处理完成。");
                sb.Append("</form>");
                sb.Append("</html>");
                SendMail(row["email"].ToString(), row["userName"].ToString(), sb.ToString());
            }
        }
        if (dtUsers.Rows[0][0].ToString() == "-8")
        {
            throw new Exception("您输入的起始日期不能使价格日起连续，请查清该价格结束日期再填写！");
        }
        if (dtUsers.Rows[0][0].ToString() != "0" && type == 1)
        {

            foreach (DataRow row in dtUsers.Rows)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append(" Dear  申请者  <br>");
                sb.Append("     您申请的零件：<br>");
                sb.Append(row["Parts"].ToString().Replace(";", "<br>") + "<br>");
                sb.Append("     成本获取流程已经结束<br>");
                sb.Append("     财务正将成本导入到QAD系统，大约15分钟处理完成。");
                sb.Append("</form>");
                sb.Append("</html>");
                SendMail(row["email"].ToString(), row["userName"].ToString(), sb.ToString());
            }
        }
  ;
    }

    public void ExportExcel(string tempFile,string outputFile, DataTable detail,int type)
    {
        FileStream templetFile = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(templetFile);
        DataSet ds = GetCimloadData(detail, type);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count >= 1)
            {
                if (ds.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    throw new Exception("处理出错请联系管理员");

                }
            }
            for (int i = 1; i <= 5; i++)
            {
                DataTable dt = ds.Tables[i - 1];
                ISheet workSheet = workbook.GetSheetAt(i);
                int nRows = 4;
                foreach (DataRow row in dt.Rows)
                {
                    IRow iRow = workSheet.CreateRow(nRows);
                    iRow.CreateCell(0).SetCellValue(row["pc_list"]);
                    iRow.CreateCell(1).SetCellValue(row["pc_curr"]);
                    iRow.CreateCell(2).SetCellValue(row["pc_empty1"]);
                    iRow.CreateCell(3).SetCellValue(row["pc_part"]);
                    iRow.CreateCell(4).SetCellValue(row["pc_um"]);
                    iRow.CreateCell(5).SetCellValue(row["pc_start"], false, "MM/dd/yy");
                    iRow.CreateCell(6).SetCellValue(row["pc_expire"], false, "MM/dd/yy");
                    iRow.CreateCell(7).SetCellValue(row["pc_empty2"]);
                    iRow.CreateCell(8).SetCellValue(row["pc_empty3"]);
                    iRow.CreateCell(9).SetCellValue(row["pc_price"]);
                    //iRow.CreateCell(10).SetCellValue(row["pc_price1"]);
                    nRows++;
                }
            }
        }
        // 输出Excel文件并退出 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    private DataSet GetCimloadData(DataTable detail, int type)
    {
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@detail", xmlDetail);
        sqlParam[1] = new SqlParameter("@type", type);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_selectApplyDetForCimload", sqlParam);
    }

    public void ExportCostExcel(string tempFile, string outputFile, DataTable detail)
    {
        FileStream templetFile = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(templetFile);
        DataTable dt = GetCostCimloadData(detail);
        for (int i = 1; i <= 5; i++)
        {
            ISheet workSheet = workbook.GetSheetAt(i);
            int nRows = 4;
            foreach (DataRow row in dt.Select("domain='" + workSheet.SheetName + "'"))
            {
                IRow iRow = workSheet.CreateRow(nRows);
                iRow.CreateCell(0).SetCellValue(row[0]);
                iRow.CreateCell(1).SetCellValue(row[1]);
                iRow.CreateCell(2).SetCellValue(row[2]);
                iRow.CreateCell(3).SetCellValue(row[3]);
                iRow.CreateCell(4).SetCellValue(row[4]);
                iRow.CreateCell(5).SetCellValue(row[5]);
                nRows++;
            }
        }
        // 输出Excel文件并退出 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    private DataTable GetCostCimloadData(DataTable detail)
    {
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@detail", xmlDetail);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_selectApplyDetForCost", sqlParam).Tables[0];
    }


    public bool SendMailForApprove(string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string reason, string applyId, out string returnMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<body>");
        sb.Append("<form>");
        sb.Append(" Dear  Approve  <br>");
        sb.Append("     申请原因:" + reason + "<br>");
        sb.Append("     申请者: " + applyName + "提交了核价审批申请，请链接以下地址时查看并审批申请<br><br>");
        sb.Append("     请查看详细信息.<br>");
        sb.Append("     请点击下面的连接查看:<br>");
        sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/price/PC_FinCheckApply.aspx?Id=" + applyId + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/price/PC_FinCheckApply.aspx?Id=" + applyId + "</a>");
        sb.Append("</form>");
        sb.Append("</html>");
        return SendMail(toEmailAddress, strApproverName, applyEmailAddress, applyName, sb.ToString(), out returnMessage);
    }

    public bool SendMailForFailed(string applyEmailAddress, string applyName, string approveEmailAddress, string approveName, string reason, string applyId, out string returnMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<body>");
        sb.Append("<form>");
        sb.Append(" Dear  申请者  <br>");
        sb.Append("     拒绝原因:" + reason + "<br>");
        sb.Append("     审批人: " + approveName + "拒绝了您的核价申请，请链接以下地址时查看<br><br>");
        sb.Append("     请查看详细信息.<br>");
        sb.Append("     请点击下面的连接查看:<br>");
        sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/price/PC_FinCheckApply.aspx?Id=" + applyId + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/price/PC_FinCheckApply.aspx?Id=" + applyId + "</a>");
        sb.Append("</form>");
        sb.Append("</html>");
        return SendMail(applyEmailAddress, applyName, approveEmailAddress, approveName, sb.ToString(), out returnMessage);
    }


    private bool SendMail(string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName,string mailBody, out string returnMessage)
    {

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
        MailAddress to;
        MailMessage mail = new MailMessage();
        mail.From = from;
        Boolean isSuccess = false;
        try
        {
            to = new MailAddress(toEmailAddress, strApproverName);
            mail.To.Add(to);
        }
        catch
        {
            returnMessage = "the email address of " + toEmailAddress + "is incorrect.Pls correct!";
            return false;
        }

        if (applyEmailAddress != null && applyEmailAddress != "")
        {
            MailAddress cc = new MailAddress(applyEmailAddress, applyName);
            mail.CC.Add(cc);
        }

        try
        {
            mail.Subject = "[Notify]核价审批";
            mail.Body = mailBody;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            isSuccess = true;
            returnMessage = "Send Mail Success!";
        }
        catch (Exception ex)
        {
            isSuccess = false;
            returnMessage = "Send Mail Failed!";
        }

        return isSuccess;
    }

    private bool SendMail(string toEmailAddress, string toEmailName, string mailBody)
    {

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
        MailAddress to;
        MailMessage mail = new MailMessage();
        mail.From = from;
        Boolean isSuccess = false;
        try
        {
            to = new MailAddress(toEmailAddress, toEmailName);
            mail.To.Add(to);
        }
        catch
        {
            return false;
        }

        try
        {
            mail.Subject = "[Notify]零件报价完成";
            mail.Body = mailBody;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            isSuccess = false;
        }

        return isSuccess;


    }


    public void selectApplyDetByDetID(string DetID, out string QAD, out string code, out string price, out string vender, out string checkPrice)
    {
        string sqlstr = "sp_pcm_selectApplyDetByDetID";


        QAD = string.Empty;
        code = string.Empty;
        price = string.Empty;
        vender = string.Empty;
        checkPrice = string.Empty;
        SqlParameter[] param = new SqlParameter[6]{
        new SqlParameter("@DetID",DetID)
        ,new SqlParameter("@QAD",SqlDbType.VarChar,20)
        ,new SqlParameter("@code",SqlDbType.NVarChar,200)
        ,new SqlParameter("@price",SqlDbType.VarChar,20)
        ,new SqlParameter("@vender",SqlDbType.NVarChar,200)
         ,new SqlParameter("@checkPrice",SqlDbType.VarChar,20)
        };
        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        param[4].Direction = ParameterDirection.Output;
        param[5].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param);

        QAD = param[1].Value.ToString().Trim();
        code = param[2].Value.ToString().Trim();
        price = param[3].Value.ToString().Trim();
        vender = param[4].Value.ToString().Trim();
        checkPrice = param[5].Value.ToString().Trim();




    }



    public bool updateApplyDetToPinding(string DetID, int uID, string pendingReason)
    {
        string sqlstr = "sp_pcm_updateApplyDetToPindings";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@DetID",DetID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@pendingReason",pendingReason)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param));

    }

    public DataTable selectFinCheckDayList(string date, string vender, string venderName)
    {
        string sqlstr = "sp_pcm_selectFinCheckDayList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@date",date)
        ,new SqlParameter("@vender",vender)
        ,new SqlParameter("@venderName",venderName)
        };

        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectFinCheckDayDet(string date, string vender)
    {
        string sqlstr = "sp_pcm_selectFinCheckDayDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@date",date)
        ,new SqlParameter("@vender",vender)
        };

        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }



    public void createExcelFinCheckDay(string date, string vender, string stroutFile, string venderName)
    {
        DataTable dt = selectFinCheckDayDet(date, vender);
        string excelname = System.DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
        string strFile = stroutFile;
        string filePath = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
        MemoryStream ms = RenderDataTableToExcel(dt, filePath, venderName) as MemoryStream;
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        byte[] data = ms.ToArray();
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
        data = null;
        ms = null;
        fs = null;
    }

    private MemoryStream RenderDataTableToExcel(DataTable SourceTable, string filePath, string venderName)
    {
        MemoryStream ms = new MemoryStream();
        FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/pc_exceFinCheckDay.xls"), FileMode.Open, FileAccess.Read);
        NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");//workbook.CreateSheet();
        NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(25);//sheet.GetRow(0);
        file.Close();

        //输出头部信息
        #region
        sheet.GetRow(3).GetCell(1).SetCellValue("供应商：");
        sheet.GetRow(3).GetCell(2).SetCellValue(venderName);
        sheet.GetRow(3).GetCell(8).SetCellValue("确认日期：");
        sheet.GetRow(3).GetCell(9).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
        sheet.GetRow(5).GetCell(1).SetCellValue("联系人：");
        sheet.GetRow(5).GetCell(8).SetCellValue("联系电话：");
        sheet.GetRow(7).GetCell(1).SetCellValue("备注：");


        //设置7列宽为100
      // sheet.SetColumnWidth(100, 250);

        sheet.GetRow(9).GetCell(1).SetCellValue("QAD");
        sheet.GetRow(9).GetCell(2).SetCellValue("部件号");
        sheet.GetRow(9).GetCell(3).SetCellValue("单位");
        sheet.GetRow(9).GetCell(4).SetCellValue("币种");
        sheet.GetRow(9).GetCell(5).SetCellValue("价格");
        sheet.GetRow(9).GetCell(6).SetCellValue("需求规格");
        sheet.GetRow(9).GetCell(7).SetCellValue("详细描述");
        sheet.GetRow(9).GetCell(8).SetCellValue("描述1");
        sheet.GetRow(9).GetCell(9).SetCellValue("描述2");
        sheet.GetRow(9).GetCell(10).SetCellValue("生效时间");


        //明细起始行
        int rowIndex = 10;



        foreach (DataRow row in SourceTable.Rows)
        {
            NPOI.SS.UserModel.IRow dataRow = sheet.GetRow(rowIndex);
            dataRow.Height = 300;
            //列高50
            sheet.GetRow(rowIndex).Height = 300;

            sheet.GetRow(rowIndex).GetCell(1).SetCellValue(row["Part"].ToString());
            sheet.GetRow(rowIndex).GetCell(2).SetCellValue(row["ItemCode"].ToString());
            sheet.GetRow(rowIndex).GetCell(3).SetCellValue(row["UM"].ToString());
            sheet.GetRow(rowIndex).GetCell(4).SetCellValue(row["Curr"].ToString());
            sheet.GetRow(rowIndex).GetCell(5).SetCellValue(row["FinCheckPrice"].ToString());
            sheet.GetRow(rowIndex).GetCell(6).SetCellValue(row["Formate"].ToString());
            sheet.GetRow(rowIndex).GetCell(7).SetCellValue(row["ItemDescription"].ToString());
            sheet.GetRow(rowIndex).GetCell(8).SetCellValue(row["ItemDesc1"].ToString());
            sheet.GetRow(rowIndex).GetCell(9).SetCellValue(row["ItemDesc2"].ToString());
            sheet.GetRow(rowIndex).GetCell(10).SetCellValue(row["CheckPriceStartDate"].ToString());
            rowIndex++;
        }

        for (int i = sheet.LastRowNum; i > SourceTable.Rows.Count + 10; i--)
        {
            sheet.ShiftRows(i, i + 1, -1);
        }

        #endregion

        hssfworkbook.Write(ms);
        //workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;
        sheet = null;
        headerRow = null;
        //workbook = null;
        hssfworkbook = null;
        return ms;

    }

    public void selectInfoToCloseAppv(string DetID, out string QAD, out string code, out string price, out string vender, out string venderName, out string checkPrice)
    {
        string sqlstr = "sp_pcm_selectInfoToCloseAppv";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@DetID",DetID)
        ,new SqlParameter("@QAD",SqlDbType.VarChar,20)
        ,new SqlParameter("@code",SqlDbType.NVarChar,200)
        ,new SqlParameter("@price",SqlDbType.VarChar,20)
        ,new SqlParameter("@vender",SqlDbType.NVarChar,200)
         ,new SqlParameter("@checkPrice",SqlDbType.VarChar,20)
         ,new SqlParameter("@venderName",SqlDbType.NVarChar,200)
        };
        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        param[4].Direction = ParameterDirection.Output;
        param[5].Direction = ParameterDirection.Output;
        param[6].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param);

        QAD = param[1].Value.ToString().Trim();
        code = param[2].Value.ToString().Trim();
        price = param[3].Value.ToString().Trim();
        vender = param[4].Value.ToString().Trim();
        checkPrice = param[5].Value.ToString().Trim();
        venderName = param[6].Value.ToString().Trim();
    }

    public int appvCloce(string DetID, int uID,string closeReason)
    {
        string sqlstr = "sp_pcm_appvCloce";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@DetID",DetID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@closeReason",closeReason)
        ,new SqlParameter("@guid",getGUID())
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public int CheckFinDateIsLegal(DataTable table)
    {
        StringWriter writer = new StringWriter();
        table.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@detail", xmlDetail);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_pcm_checkFinDateisLegal", sqlParam));
    }

    public DataTable selectFinCheckToExport(string applyID)
    {
        try
        {
            string sqlstr = "sp_pcm_selectFinCheckToExport";

            SqlParameter[] param =  new SqlParameter[]{
            new SqlParameter("@applyID",applyID)
            };

            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
        }
        catch
        {
            return null;
        
        }
    }

    public bool FinUploadCheckPrice(string strFileName, string strUID, string ApplyId, out string message ,DataTable dt)
    {

        try
        {
            message = "";
            bool success = true;
            if (success)
            {
                try
                {
                    #region excel创建datatable
                    if (
                        dt.Columns[0].ColumnName != "申请号" ||
                        dt.Columns[1].ColumnName != "申请id" ||
                        dt.Columns[2].ColumnName != "QAD" ||
                        dt.Columns[3].ColumnName != "供应商" ||
                        dt.Columns[4].ColumnName != "供应商名称" ||
                        dt.Columns[5].ColumnName != "单位" ||
                        dt.Columns[6].ColumnName != "币种" ||
                        dt.Columns[7].ColumnName != "期望价格" ||
                        dt.Columns[8].ColumnName != "核价" ||
                        dt.Columns[9].ColumnName != "批准核价" ||
                        dt.Columns[10].ColumnName != "生效日期" ||
                        dt.Columns[11].ColumnName != "失效日期" 
                        )
                    {
                        dt.Reset();
                        message = "导入文件的模版不正确，请更新模板再导入!";
                        success = false;
                    }

                    DataTable TempTable = new DataTable("TempTable");
                    DataColumn TempColumn;
                    DataRow TempRow;

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "applyID";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "DetID";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "PQDetID";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "QAD";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "Vender";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "VenderName";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "um";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "curr";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "applyPrice";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "checkPrice";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "FinCheckPrice";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "starDate";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "endDate";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "uID";
                    TempTable.Columns.Add(TempColumn);
                    #endregion

                    if (dt.Rows.Count > 0)
                    {

                        //先清空临时表中该上传员工的记录
                        if (ClearFinCheckPriceTempTable(Convert.ToInt32(strUID)))
                        {
                            #region 前台导入数据
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {

                                TempRow = TempTable.NewRow();//创建新的行
                                if (dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1) && dt.Rows[i].IsNull(2) 
                                    && dt.Rows[i].IsNull(3) && dt.Rows[i].IsNull(4) && dt.Rows[i].IsNull(5)
                                    && dt.Rows[i].IsNull(6) && dt.Rows[i].IsNull(7) && dt.Rows[i].IsNull(8)
                                    && dt.Rows[i].IsNull(9) && dt.Rows[i].IsNull(10) && dt.Rows[i].IsNull(11)
                                    )
                                {
                                    continue;
                                }


                                TempRow["applyID"] = ApplyId;
                                TempRow["DetID"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                                TempRow["PQDetID"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                                TempRow["QAD"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                                TempRow["Vender"] = dt.Rows[i].ItemArray[3].ToString().Trim();
                                TempRow["VenderName"] = dt.Rows[i].ItemArray[4].ToString().Trim();
                                TempRow["um"] = dt.Rows[i].ItemArray[5].ToString().Trim();
                                TempRow["curr"] = dt.Rows[i].ItemArray[6].ToString().Trim();
                                TempRow["applyPrice"] = dt.Rows[i].ItemArray[7].ToString().Trim();
                                TempRow["checkPrice"] = dt.Rows[i].ItemArray[8].ToString().Trim();
                                TempRow["FinCheckPrice"] = dt.Rows[i].ItemArray[9].ToString().Trim();
                                TempRow["starDate"] = dt.Rows[i].ItemArray[10].ToString().Trim();
                                TempRow["endDate"] = dt.Rows[i].ItemArray[11].ToString().Trim();
                                TempRow["uID"] = strUID;

                                TempTable.Rows.Add(TempRow);
                            }
                            #endregion
                            //TempTable有数据的情况下批量复制到数据库里
                            if (TempTable != null && TempTable.Rows.Count > 0)
                            {
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adm.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                {
                                    bulkCopy.DestinationTableName = "pcm_uploadFinCheckPriceTemp";

                                    bulkCopy.ColumnMappings.Clear();
                                    bulkCopy.ColumnMappings.Add("applyID", "applyID");
                                    bulkCopy.ColumnMappings.Add("DetID", "applyDetID");
                                    bulkCopy.ColumnMappings.Add("PQDetID", "PQDetID");
                                    bulkCopy.ColumnMappings.Add("QAD", "part");
                                    bulkCopy.ColumnMappings.Add("Vender", "vender");
                                    bulkCopy.ColumnMappings.Add("VenderName", "venderName");
                                    bulkCopy.ColumnMappings.Add("um", "um");
                                    bulkCopy.ColumnMappings.Add("curr", "curr");
                                    bulkCopy.ColumnMappings.Add("applyPrice", "applyPrice");
                                    bulkCopy.ColumnMappings.Add("checkPrice", "checkPrice");
                                    bulkCopy.ColumnMappings.Add("FinCheckPrice", "FinCheckPrice");
                                    bulkCopy.ColumnMappings.Add("starDate", "FinStartDate");
                                    bulkCopy.ColumnMappings.Add("endDate", "FinEndDate");
                                    bulkCopy.ColumnMappings.Add("uID", "uID");
                                    try
                                    {
                                        bulkCopy.WriteToServer(TempTable);
                                    }
                                    catch (Exception ex)
                                    {
                                        message = "导入时出错，请联系系统管理员A！";
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
                                //数据库端验证
                                if (CheckFinCheckPriceTempTable(Convert.ToInt32(strUID), ApplyId))
                                {
                                    //判断上传内容能否通过验证
                                    if (JudgeFinCheckPriceTempTable(Convert.ToInt32(strUID),ApplyId))
                                    {
                                        
                                        if (TransFinCheckPriceTempTable(Convert.ToInt32(strUID), ApplyId))
                                        {
                                            message = "导入文件并保存成功";
                                            success = true;
                                        }
                                        else
                                        {
                                            message = "导入时出错，请联系管理员C!";
                                            success = false;
                                        }
                                    }
                                    else
                                    {
                                        message = "导入文件结束，有错误!";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    message = "导入时出错，请联系管理员B!";
                                    success = false;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    message = "导入文件失败!";
                    success = false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
            return success;

        }
        catch(Exception exx)
        {
            message = "数据出错，请联系管理员";
            return false;
        }
    }

    private bool TransFinCheckPriceTempTable(int uID, string ApplyId)
    {
        try
        {
            string sqlstr = "sp_pcm_insertTransFinCheckPriceTempTable";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@uID",uID)
                ,new  SqlParameter ("@ApplyId",ApplyId)
            };
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param));
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private bool JudgeFinCheckPriceTempTable(int uID ,string ApplyId)
    {
        try
        {
            string sqlstr = "sp_pcm_checkFincheckImportHaveError";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@uID",uID)
                ,new  SqlParameter ("@ApplyId",ApplyId)
            };
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param));
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private bool CheckFinCheckPriceTempTable(int uID, string ApplyId)
    {
        try
        {
            string sqlstr = "sp_pcm_CheckFinCheckPriceTempTable";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@uID",uID)
                ,new  SqlParameter ("@ApplyId",ApplyId)
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(),CommandType.StoredProcedure,sqlstr,param));
        }
        catch (Exception e)
        {
            return false; 
        }
    }

    private bool ClearFinCheckPriceTempTable(int uID )
    {
        try
        {
            string sqlstr = "sp_pcm_ClearFinCheckPriceTempTable";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@uID",uID)
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param));

        }
        catch (Exception e)
        {
            return false;
        }
    }



    public DataTable GetInquiryPriceImportError(string strUID, string applyID)
    {
        try
        {
            string sqlstr = "sp_pcm_GetInquiryPriceImportError";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@uID",strUID)
                ,new  SqlParameter ("@ApplyId",applyID)
            };

            return  SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}