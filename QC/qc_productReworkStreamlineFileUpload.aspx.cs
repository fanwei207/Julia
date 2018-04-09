using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using QCProgress;

public partial class QC_qc_productReworkStreamlineFileUpload : BasePage
{
    QC qc = new QC();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/QC/qc_product_return.aspx?prdID=" + Request["prdID"].ToString());
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string path = "/TecDocs/QC/";
        string prdID = Request["prdID"].ToString();
        string qprsID = string.Empty; 
        if(Request["qprs_ID"] != null)
        {
            qprsID = Request["qprs_ID"].ToString();
        }
        else
        {
            qprsID = System.Guid.NewGuid().ToString();
        }
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(fileManager.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('上传文件不能为空！');";
            return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path, fileManager))
            {
                ltlAlert.Text = "alert('上传失败！请联系管理员！');";
                return;
            }
            if (Request["url"] != null)
            {
                if (File.Exists(Request["url"].ToString()))
                {
                    File.Delete(Request["url"].ToString());
                }
            
            }

        }
        if (qc.uploadProductReworkStreamlineFile(prdID, qprsID, fileName, filePate, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('上传成功！');";
            Response.Redirect("/QC/qc_product_return.aspx?prdID=" + Request["prdID"].ToString());
        }
        else
        {

            ltlAlert.Text = "alert('上传失败！请联系管理员！');";
            return;
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