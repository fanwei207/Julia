using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;


using System.Collections.Generic;

using Microsoft.ApplicationBlocks.Data;


public partial class admin_doc : BasePage
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            trCmd.Visible = false;
            chk.Checked = false;

            dg.Columns[3].Visible = false;
            if (this.Security["320301"].isValid)
            {
                trCmd.Visible = true;
                chk.Checked = true;
            }
            if (this.Security["320302"].isValid)
            {
                dg.Columns[3].Visible = true;
            }

            BindData();
            lblformat.Text = ShareDocument.getDocumentType();
        }
    }

    protected void BindData()
    {
        this.dg.DataSource = ShareDocument.GetDocument().Tables[0];
        this.dg.DataBind();
    }
    protected void dg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != string.Empty)
            {
            }
        }
    }
    protected void dg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg.PageIndex = e.NewPageIndex;

        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        
        if (File1.PostedFile.FileName != string.Empty)
        {
            if (File1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            string _filename = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf('\\') + 1); ;

            Int32 bytes = File1.PostedFile.ContentLength;

            string _tempPath = Server.MapPath("/import/") + _filename;
            string _logicalPath = Server.MapPath("/TecDocs/ShareDocs/");
            string _physicalFile = "";

            if (File1.PostedFile.ContentLength > 0)
            {
                try
                {
                    File1.PostedFile.SaveAs(_tempPath);
                }
                catch
                {
                    ltlAlert.Text = "alert('文档上传失败!');";
                    return;
                }

                _physicalFile = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(_tempPath);
                string ext = ShareDocument.GetFileExtension(_tempPath);

                if (ShareDocument.CheckDocument(ext))
                {
                   
                    ext = Path.GetExtension(_tempPath);
                    
                    if (!ShareDocument.InsertDocument(_filename, ext, _physicalFile, Convert.ToInt32(Session["uID"]), Session["uName"].ToString()))
                    {
                        this.Alert("文件保存失败！请刷新后重新操作一遍！");
                    }
                    else
                    {
                        try
                        {
                            File.Move(_tempPath, _logicalPath + _physicalFile);

                            this.Alert("文件保存成功！");
                        }
                        catch
                        {
                            this.Alert("文件转移失败！请删除记录后重新上传！");   
                        }
                    }
                }
                else
                {
                    this.Alert("文档格式不正确，请联系管理员！格式代码：" + ext);
                   
                }
            }
            else
            {
                this.Alert("不能上传空文档!");
            }
        }
        else
        {
            this.Alert("请先指定一个要上传的文档!");
        }

        BindData();
    }

    protected void dg_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!ShareDocument.DeleteDocument(dg.DataKeys[e.RowIndex].Values["doc_id"].ToString(), Convert.ToInt32(Session["uID"]), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('操作失败,您无权删除此文档,只能操作自己所创建文件!');";
            BindData();
            return;
        }
        string FilePath = Server.MapPath("/TecDocs/ShareDocs/" + dg.DataKeys[e.RowIndex].Values["doc_path"].ToString());
        if (File.Exists(FilePath))
        {
            //如存在则删除
            File.Delete(FilePath);
        }
        BindData();
    }

    protected void dg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "DownLoad")
        {
            LinkButton link = (LinkButton)e.CommandSource;

            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;

            string _filePath = dg.DataKeys[index].Values["doc_path"].ToString();
            ltlAlert.Text = "var w=window.open('/TecDocs/ShareDocs/" + _filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

            BindData();
        }
    }
}
