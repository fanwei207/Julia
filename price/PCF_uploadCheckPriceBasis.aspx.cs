using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class price_pcf_uploadCheckPriceBasis : BasePage
{
    PCF_helper helper = new PCF_helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["PCF_inquiryID"] == null)
            {
                filename.Visible = false;
                btnUpload.Visible = false;
                btnReturn.Visible = false;
            }
            else
            {
                lbIMID.Text = Request["IMID"].ToString();
            }

            

            bindGV();

            
        
        }
    }

    private void bindGV()
    {
        string PCF_inquiryID = string.Empty;
        if (Request["PCF_inquiryID"] == null)
        { 
            PCF_inquiryID =Request["PCF_ID"].ToString();
        }
        else
        {
            PCF_inquiryID= Request["PCF_inquiryID"].ToString();
        }
         

        gvBasisInfo.DataSource = helper.selectInquiryBasisByID(PCF_inquiryID);
        gvBasisInfo.DataBind();

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCF_InquiryDet.aspx?PCF_inquiryID=" + Request["PCF_inquiryID"].ToString() + "&TVender=" + Request.QueryString["TVender"] + "&TVenderName=" + Request.QueryString["TVenderName"] + "&TQAD=" + Request.QueryString["TQAD"] + "&ddlStatus=" + Request.QueryString["ddlStatus"]);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string PCF_inquiryID = Request["PCF_inquiryID"].ToString();
        string path = "/TecDocs/Quotation/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(filename.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('上传路径不能为空');";
            return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path))
            {
                ltlAlert.Text = "alert('上传文件失败，请联系管理员');";
                return;
            }
            //else 
            //{
            //    ltlAlert.Text = "alert('上传文件成功！');";
            //}
        }

        if (helper.insertBasis(PCF_inquiryID, Session["uID"].ToString(),Session["uName"].ToString(), fileName, filePate))
        {
            ltlAlert.Text = "alert('上传文件成功！');";
            bindGV();
        }
        else
        {

            ltlAlert.Text = "alert('上传文件失败，请联系管理员！');";
            return;
        }
    }
    protected void gvBasisInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBasisInfo.PageIndex = e.NewPageIndex;
        bindGV();
    }
    protected void gvBasisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

       

        if (e.CommandName == "lkbtnView")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }

        if (e.CommandName == "lkbtndelete")
        {

            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string PCF_InquiryImportID = e.CommandArgument.ToString();

            string url = gvBasisInfo.DataKeys[index].Values["PCF_URL"].ToString();

            if (helper.deleteInquiryBasis(PCF_InquiryImportID))
            {
                ltlAlert.Text = "alert('删除成功！');";

                if(File.Exists(url))
                {
                    File.Delete(url);
                }

                bindGV();
            }
            else
            {
                 ltlAlert.Text = "alert('删除失败！请联系管理员');";
            }
        }
    }


    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="_fileName"></param>
    /// <param name="_filePath"></param>
    /// <returns></returns>
    protected bool ImportFile(ref string _fileName, ref string _filePath, string path)
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