using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RD_WorkFlow;
using System.IO;
using System.Text;

public partial class RDW_setCategoryDetials : System.Web.UI.Page
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            drop_template.DataSource = rdw.SelectTepmlateMstr(string.Empty, "--", "0", Convert.ToInt32(Session["uID"]));
            drop_template.DataBind();
            drop_template.Items.Insert(0, new ListItem("--", "0"));

            //drop_template.SelectedItem.Text = Request.QueryString["TempName"].ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["TempName"]))
            {
                drop_template.Items.FindByText(Request.QueryString["TempName"]).Selected = true;
            }

            lbl_cate.Text = Request.QueryString["cate"].ToString();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //保存project Category 的关联Template
        string Template = drop_template.SelectedItem.Text;
        int templateID = Convert.ToInt32(drop_template.SelectedItem.Value);
        int cateID=Convert.ToInt32(Request.QueryString["cate_id"]);

        if (rdw.UpdateCategoryTemplate(cateID, templateID))
        {
            ltlAlert.Text = "alert('Save successfuly!');";
        }
        else
        {
            ltlAlert.Text = "alert('Failed to Save!');";
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_ProjectCategory.aspx");
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //bool isSuccess = true;
        if (!fileAttachFile.HasFile)
        {
            ltlAlert.Text = "alert('Please select a file！');";
            return;
        }
        else
        {
            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("#")>0)
            {
                ltlAlert.Text = "alert('The file name contains illegal characters:#');";
                return;
            }
            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("%") > 0)
            {
                ltlAlert.Text = "alert('The file name contains illegal characters:%');";
                return;
            }

            if (fileAttachFile.ContentLength > 1024 * 1024 * 100)
            {
                ltlAlert.Text = "alert('The file is larger than 100M, can not be uploaded！');";
                return;
            }
            if (!rdw.CheckFileExtension(Path.GetExtension(fileAttachFile.FileName)))
            {
                ltlAlert.Text = "alert('Illegal file extension！');";
                return;
            }
        }
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        StringBuilder sb = new StringBuilder();
        string _cateid=Convert.ToString(Request.QueryString["cate_id"]);

        string _fileName = Path.GetFileName(fileAttachFile.FileName);//文件名，包含后缀
        string _file = Path.GetFileNameWithoutExtension(fileAttachFile.FileName);//文件名，不包含后缀
        string _fileExtension = Path.GetExtension(fileAttachFile.FileName);//文件后缀

        string docPath = "/docs/ProjectTracking/" + _cateid + "/";
        string _targetFolder = Server.MapPath(docPath);
        string _newFileName = DateTime.Now.ToFileTime().ToString() + _fileExtension;//合并两个路径为上传到服务器上的all path

        try
        {
            if (!Directory.Exists(_targetFolder))
            {
                Directory.CreateDirectory(_targetFolder);
            }

            this.fileAttachFile.MoveTo(_targetFolder + "/" + _newFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
        }
        catch
        {
            ltlAlert.Text = "alert('Failed to upload！');";
            return;
        }

        if (rdw.UploadTemplateFile(_fileName, _newFileName, _uID, _uName, _cateid, docPath))
        {
            ltlAlert.Text = "alert('Upload successfuly！')";
        }
        else
        {
            ltlAlert.Text="alert('Failed to upload！');";
        }
    }

    
    protected void BtnDoc_Click(object sender, EventArgs e)
    {

    }
}