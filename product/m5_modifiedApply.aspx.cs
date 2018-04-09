using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;

public partial class product_m5_modifiedApply : BasePage
{

    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    protected void Page_Load(object sender, EventArgs e)
    {

        lblNo.Text = Request["no"].ToString();
        txtModelNo.Text = Request["model"].ToString();


        btnDone.Attributes.Add("onclick", "return confirm('Are you sure to modify?')");
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        #region  上传附件验证

        string strDescName = "";//文件名
        string strCateFolder = "/TecDocs/ECN/";
        string strDescExtension = "";//文件后缀
        string strDescSaveFileName = "";//储存名

        if (fileDesc.PostedFile.FileName != string.Empty)
        {
            strDescName = Path.GetFileNameWithoutExtension(fileDesc.PostedFile.FileName);
            strDescExtension = Path.GetExtension(fileDesc.PostedFile.FileName);
            strDescSaveFileName = DateTime.Now.ToFileTime().ToString();
            if (!UploadDescFile(strCateFolder, strDescSaveFileName, strDescExtension))
            {
                this.Alert("File upload failed！");
                return;
            }
        }

        string strReasonName = "";//文件名
        string strReasonExtension = "";//文件后缀
        string strReasonSaveFileName = "";//储存名
        if (fileReason.PostedFile.FileName != string.Empty)
        {
            strReasonName = Path.GetFileNameWithoutExtension(fileReason.PostedFile.FileName);
            strReasonExtension = Path.GetExtension(fileReason.PostedFile.FileName);
            strReasonSaveFileName = DateTime.Now.AddTicks(1).ToFileTime().ToString();

            if (!UploadReasonFile(strCateFolder, strReasonSaveFileName, strReasonExtension))
            {
                this.Alert("File upload failed！");
                return;
            }
        }
        #endregion

        if (txtReason.Text.Trim().Equals(string.Empty) && txtDesc.Text.Trim().Equals(string.Empty)
             && (strDescName + strDescExtension).Equals(string.Empty) && (strReasonName + strReasonExtension).Equals(string.Empty))
        {

            this.Alert("You Have not change!Can not be save.");
            return;
        }

        try
        {
            string strName = "sp_m5_saveM5Modify";
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@no", lblNo.Text);
            param[2] = new SqlParameter("@desc", txtDesc.Text.Trim());
            param[3] = new SqlParameter("@desc_file", strDescName + strDescExtension);
            param[4] = new SqlParameter("@desc_path", strCateFolder + strDescSaveFileName + strDescExtension);
            param[5] = new SqlParameter("@reason", txtReason.Text.Trim());
            param[6] = new SqlParameter("@reason_file", strReasonName + strReasonExtension);
            param[7] = new SqlParameter("@reason_path", strCateFolder + strReasonSaveFileName + strReasonExtension);
            param[9] = new SqlParameter("@uID", Session["uID"].ToString());
            param[10] = new SqlParameter("@uName", Session["uName"].ToString());
            param[11] = new SqlParameter("@model", txtModelNo.Text.Trim());
            param[12] = new SqlParameter("@Email", SqlDbType.NVarChar, 2000);
            param[12].Direction = ParameterDirection.Output;
            param[13] = new SqlParameter("@project", SqlDbType.NVarChar, 2000);
            param[13].Direction = ParameterDirection.Output;
            param[14] = new SqlParameter("@descReturn", SqlDbType.NVarChar, 2000);
            param[14].Direction = ParameterDirection.Output;
            param[15] = new SqlParameter("@reasonReturn", SqlDbType.NVarChar, 2000);
            param[15].Direction = ParameterDirection.Output;


            

            if (SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strName, param).ToString().Equals("1"))
            {
                #region 发送邮件
                string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string to = param[12].Value.ToString();
                string copy = "";
                string subject = "TCP ECN (产品变更通知书) Need your approval (需要您审批)";
                string body = "";
                #region 写Body
                body += "<font style='font-size: 12px;'>编号No：" + lblNo.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>申请人Applicant：" + Session["uName"].ToString() + "</font><br />";
                body += "<font style='font-size: 12px;'>项目Type：" + param[13].Value.ToString() + "</font><br />";
                // body += "<font style='font-size: 12px;'>是否同意变更Agree?： " + btnNotAgree.Text + "</font><br />";
                // body += "<font style='font-size: 12px;'>生效日期Excutive Date：" + labEffDate.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>申请内容Contents：" + param[14].Value.ToString() + "</font><br />";
                body += "<font style='font-size: 12px;'>申请理由Reasons：" + param[15].Value.ToString() + "</font><br />";
                // body += "<font style='font-size: 12px;'>执行人Executed By：" + labUExcutor.Text + "</font><br />";
                body += "<br /><br />";
                body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
                body += "<font style='font-size: 12px;'>For details please visit "+baseDomain.getPortalWebsite()+" </font>";
                #endregion
                if (!this.SendEmail(from, to, copy, subject, body))
                {
                    this.ltlAlert.Text = "alert('Email sending failure');";
                }
                else
                {
                    this.ltlAlert.Text = "alert('Email sending');";
                }
                #endregion
                
                this.ltlAlert.Text = " alert('Modify success！'); var loc=$('body', parent.document).find('#ifrm_560000440')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
            }
            else
            {
                this.Alert("Modify failed！");
            }


            

        }
        catch (Exception ex)
        {
            this.Alert("Modify failed！");
            return;
        }


        
    }


    protected bool UploadDescFile(string strCateFolder, string strDescSaveFileName, string strDescExtension)
    {
        if (fileDesc.PostedFile.FileName != string.Empty)
        {
            if (fileDesc.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum file upload is 8 MB!');";
                return false;
            }

            Int32 bytes = fileDesc.PostedFile.ContentLength;

            string _logicalPath = Server.MapPath("/TecDocs/ECN/");

            if (fileDesc.PostedFile.ContentLength > 0)
            {
                try
                {
                    fileDesc.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strDescSaveFileName + strDescExtension);
                }
                catch
                {
                    ltlAlert.Text = "alert('File upload failed!');";
                    return false;
                }
            }
            else
            {
                this.Alert("File is requied!");
                return false;
            }
        }
        return true;
    }

    protected bool UploadReasonFile(string strCateFolder, string strReasonSaveFileName, string strReasonExtension)
    {
        if (fileReason.PostedFile.FileName != string.Empty)
        {
            if (fileReason.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum file upload is 8 MB!');";
                return false;
            }
            Int32 bytes = fileReason.PostedFile.ContentLength;
            string _logicalPath = Server.MapPath("/TecDocs/ECN/");
            if (fileReason.PostedFile.ContentLength > 0)
            {
                try
                {
                    fileReason.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strReasonSaveFileName + strReasonExtension);
                }
                catch
                {
                    ltlAlert.Text = "alert('File upload failed!');";
                    return false;
                }
            }
            else
            {
                this.Alert("File is requied!");
                return false;
            }
        }
        return true;
    }

}