using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class RDW_RDW_ProjectReply : BasePage
{

    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbProjectCode.Text = Request["ProjectCode"].ToString();

        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        uploadEffect();
    }

    private void uploadEffect()
    {
        string path = "/TecDocs/RDW/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        string msg = txtMsg.Text.Trim();

        if (msg.Equals(string.Empty))
        {
            ltlAlert.Text = "alert('Message can not be empty');";
            return;
        }



        if (string.Empty.Equals(file1.PostedFile.FileName))
        {
            //ltlAlert.Text = "alert('Upload path can not be null');";
            //return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path, file1))
            {
                ltlAlert.Text = "alert('Upload file failed! please Linking administrators');";
                return;
            }

        }
        if (this.uploadArgue(Request["ProjectCode"].ToString(),Request["mid"].ToString() , msg, fileName, filePate, Session["uID"].ToString(), Session["uName"].ToString(), Session["eName"].ToString()))
        {
            ltlAlert.Text = "alert(' success!');";
            return;
        }
        else
        {

            ltlAlert.Text = "alert('Save file failed! please Linking administrators');";
            return;
        }
    }

    private bool uploadArgue(string projectCode, string mstrID, string msg, string fileName, string URL, string uID, string uName, string eName)
    {
        string sqlstr = "sp_RDW_insertProjectArgueDet";

        SqlParameter[] param = new SqlParameter[]{
        
            new SqlParameter("@projectCode",projectCode)
            , new SqlParameter("@mstrID",mstrID)
            , new SqlParameter("@fileName",fileName)
            , new SqlParameter("@URL",URL)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)
            , new SqlParameter("@eName",eName)
            , new SqlParameter("@msg",msg)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));


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