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

public partial class IT_TSK_NewTask : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        this.Security.Register("2000201", "是否允许进行任务分配");
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _nbr = TaskHelper.GetTaskNbr();
            if (!string.IsNullOrEmpty(_nbr))
            {
                hidTaskNbr.Value = _nbr;
            }
            else
            {
                btnDone.Enabled = false;
                this.Alert("获取唯一任务码失败!");
            }
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDesc.Text))
        {
            this.Alert("任务描述 不能为空！");
            return;
        }
        else if (txtDesc.Text.Trim().Length < 10)
        {
            this.Alert("任务描述 最少10字以上！");
            return;
        }

        if (string.IsNullOrEmpty(txtUserNo.Text))
        {
            this.Alert("申请人 不能为空！");
            return;
        }

        //如果存在附件，要验证
        //有附件的，上传
        string fileName = string.Empty;
        string filePath = string.Empty;

        if (!string.IsNullOrEmpty(filename.Value.Trim()))
        {
            if (Path.GetFileName(filename.PostedFile.FileName).IndexOf("#") > 0)
            {
                this.Alert("文件名包含非法字符:#");
                return;
            }

            if (Path.GetFileName(filename.PostedFile.FileName).IndexOf("%") > 0)
            {
                this.Alert("文件名包含非法字符:%");
                return;
            }

            if (!ImportFile(ref fileName, ref filePath))
            {
                this.Alert("任务保存成功，但附件上传失败！");
                return;
            }
        }

        if (TaskHelper.CheckUser(txtUserNo.Text, txtUserDomain.Text))
        {
            if (TaskHelper.InsertTaskMstr(hidTaskNbr.Value, Session["uID"].ToString(), Session["uName"].ToString()
                            , txtDesc.Text.Trim(), txtUserNo.Text, txtUserDomain.Text, fileName, filePath))
            {    
                //有分配权限的，定向到分配页面
                if (this.Security["2000201"].isValid)
                {
                    Response.Redirect("TSK_DistributeTask.aspx?from=new&tskNbr=" + hidTaskNbr.Value + "&rt=" + DateTime.Now.ToFileTime().ToString());
                }
                else
                {
                    this.Alert("任务保存成功！");
                }
            }
            else
            {
                this.Alert("任务保存失败！");
            }
        }
        else
        {
            this.Alert("申请人 不存在！");
        }
    }

    protected bool ImportFile(ref string _fileName, ref string _filePath)
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

        string path = "/TecDocs/ItTask/";

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