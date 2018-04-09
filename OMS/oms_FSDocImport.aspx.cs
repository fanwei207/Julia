using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;

public partial class oms_FSDocImport : BasePage
{
   adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCustCode.Text = Request.QueryString["custCode"].ToString();
            lblCust.Text = Request.QueryString["custName"].ToString();
            BindCatagory();
            BindFactoryStatus();
        }
    }
    #region Factory Status

    protected void BindCatagory()
    {
        DataTable ds = OMSHelper.GetFSCategory("", "");
        ddlCatagory.DataSource = ds;
        ddlCatagory.DataValueField = "id";
        ddlCatagory.DataTextField = "tp";
        ddlCatagory.DataBind();
        ddlCatagory.Items.Insert(0, new ListItem("--All--", "0"));
    }
    protected void BindFactoryStatus()
    {
        gvFactoryStatus.DataSource = OMSHelper.GetFactoryStatus(lblCustCode.Text.ToString(), Convert.ToInt32(ddlCatagory.SelectedValue), txtFilename.Text.Trim(), 0, Convert.ToInt32(ddlImportance.SelectedValue));
        gvFactoryStatus.DataBind();
    }

    #region 上传文件
    protected bool UpLoadFile()
    {
        string strUserFileName = filename1.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string fileName = strUserFileName.Substring(flag + 1);
        if (fileName.Trim().Length <= 0)
        {
            this.Alert("a file is required！");
            return false;
        }
        string catPath = @"/TecDocs/OMS/" + lblCust.Text + "/" + ddlCatagory.SelectedItem.Text + "/";
        string strCatFolder = Server.MapPath(catPath);
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                this.Alert("fial to create file directory！");
                return false;
            }
        }

        string strFilePath = strCatFolder + fileName;
        if (File.Exists(strFilePath))
        {
            this.Alert("the file uploading does already exists！");
            return false;
        }
        if (!OMSHelper.InsertFSDocuments(strFilePath, fileName, lblCustCode.Text, Session["plantCode"].ToString(), Convert.ToInt32(ddlCatagory.SelectedValue), Convert.ToInt32(Session["uID"]), Session["eName"].ToString(), Convert.ToInt32(ddlImportance.SelectedValue)))
        {
            this.Alert("fail to operate DB！");
            return false;
        }
        if (filename1.PostedFile != null)
        {
            try
            {
                filename1.PostedFile.SaveAs(strFilePath);
            }
            catch
            {
                this.Alert("fail to update file！");
                return false;
            }
        }

        return true;

    }
    #endregion

    protected void btnUpLoad_Click(object sender, EventArgs e)
    {
        #region
        //验证代码
        if (ddlCatagory.SelectedIndex == 0)
        {
            this.Alert("Please choose Catagory！");
            return;
        }
        #endregion
        if (UpLoadFile())
        {
            this.Alert("success to upload!");
            ddlCatagory.SelectedIndex = 0;
            BindFactoryStatus();
        }
    }

    protected void gvFactoryStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteFile")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gvFactoryStatus.DataKeys[index].Values["fsd_id"].ToString());
            string filePath = OMSHelper.GetFSDocumentPath(fsId);
            if (!OMSHelper.DeleteFSDocument(fsId))
            {
                this.Alert("fail to delete!");
                return;
            }
            else
            {
                if (File.Exists(filePath))
                {
                    //如存在则删除
                    File.Delete(filePath);
                    //删除空文件夹
                    int folderIndex = filePath.LastIndexOf("\\");
                    string folderString1 = filePath.Substring(0, folderIndex);
                    if (Directory.GetDirectories(folderString1).Length == 0 && Directory.GetFiles(folderString1).Length == 0)
                    {
                        Directory.Delete(folderString1);
                        int folderIndex2 = folderString1.LastIndexOf("\\");
                        string folderString2 = folderString1.Substring(0, folderIndex2);
                        if (Directory.GetDirectories(folderString2).Length == 0)
                            Directory.Delete(folderString2);
                    }
                }
                this.Alert("success to delete!");
            }
            BindFactoryStatus();
        }
    }

    protected void gvFactoryStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFactoryStatus.PageIndex = e.NewPageIndex;
        BindFactoryStatus();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindFactoryStatus();
    }
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("oms_cust.aspx");
    }
    #endregion
}