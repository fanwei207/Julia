using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;

public partial class EDI_EDI_pocEffectDet : BasePage
{
    poc_helper helper = new poc_helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["poc_id"] != null)
            {
                ddlEffectBind();
            }
        }
    }

    private void ddlEffectBind()
    {

        DataTable dtEffect = helper.selectEDIeffectMstr(Session["uID"].ToString());
        dropEffect.DataSource = dtEffect;
        dropEffect.DataBind();
        if (dtEffect.Rows.Count != 1)
        {
            dropEffect.Items.Insert(0, new ListItem("--", "0"));
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        string to = string.Empty;
        string subject = string.Empty;
        string body = string.Empty;
        string copy = string.Empty;

        if (dropEffect.SelectedValue == "")
        {
            ltlAlert.Text = "alert('Pleasc choose one effect!');";
            return;
        }

        string effectID = dropEffect.SelectedValue;
        string poc_id =    Request.QueryString["poc_id"];
        string agree = string.Empty;
        string effectDetId = string.Empty;
        if(radYes.Checked)
        {
            agree = "1";
        }
        else
        {
            agree = "0";
        }

        int flag = helper.insertIntoEffect(poc_id, effectID, agree, txtMsg.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), out effectDetId, out to, out copy, out subject, out body);
        if (flag == 1)
        {
             uploadEffect(effectDetId);

             #region 发送邮件
             string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
             //"TCP ECN (产品变更通知书)";


             if (!this.SendEmail(from, to, copy, subject, body))
             {
                 this.ltlAlert.Text = "alert('Email sending failure');";
             }
             else
             {
                 this.ltlAlert.Text = "alert('Email sending');";
             }
             #endregion
 

            this.ltlAlert.Text = " alert('Success'); var loc=$('body', parent.document).find('#ifrm_10001200')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
        }
        else if(flag == -1)
        {
            
            ltlAlert.Text = "alert('You have not the effect authority!');";
        }
        else
        {
            ltlAlert.Text = "alert('Save file failed! please Linking administrators');";
        }
        
        
    }

    private void uploadEffect(string effectDetId)
    {
        string path = "/TecDocs/EDI/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(file1.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('Upload path can't be null');";
            return ;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path, file1))
            {
                ltlAlert.Text = "alert('Upload file failed! please Linking administrators');";
                return ;
            }

        }
        if (helper.uploadEffect(Request.QueryString["poc_id"],effectDetId, fileName, filePate, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert(' success!');";
            return ;
        }
        else
        {

            ltlAlert.Text = "alert('Upload file failed! please Linking administrators');";
            return ;
        }
    }

    protected bool ImportFile(ref string _fileName, ref string _filePath, string path, System.Web.UI.HtmlControls.HtmlInputFile filename)
    {
        string attachName = Path.GetFileNameWithoutExtension(filename.PostedFile.FileName);
        string newFileName = DateTime.Now.ToFileTime().ToString();

        string attachExtension = Path.GetExtension(filename.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../import/"), newFileName + attachExtension);//合并两个路径为上传到服务器上的全路径

        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                return false;
            }
        }

        try
        {
            filename.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            return false;
        }



        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        path += newFileName + attachExtension;

        try
        {
            File.Move(SaveFileName, Server.MapPath(path));
        }
        catch
        {
            return false;
        }

        _fileName = attachName + attachExtension;
        _filePath = path;

        return true;
    }
}