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

public partial class TSK_EditTask : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["tskNbr"] == null)
            {
                btnDone.Enabled = false;
                this.Alert("获取唯一任务码失败!");
            }
            else
            {
                string _nbr = Request.QueryString["tskNbr"].ToString();
                if (!string.IsNullOrEmpty(_nbr))
                {
                    hidTaskNbr.Value = _nbr;

                    DataTable table = TaskHelper.SelectTaskMstrByNbr(_nbr);

                    if (table != null)
                    {
                        if (table.Rows.Count > 0)
                        {
                            txtDesc.Text = table.Rows[0]["tsk_desc"].ToString();
                            txtUserNo.Text = table.Rows[0]["tsk_applyNo"].ToString() + "--" + table.Rows[0]["tsk_applyName"].ToString() + "--" + table.Rows[0]["tsk_applyDomain"].ToString();
                            
                            if (!string.IsNullOrEmpty(table.Rows[0]["tsk_fileName"].ToString()))
                            {
                                hlinkFile.Text = table.Rows[0]["tsk_fileName"].ToString();
                                hlinkFile.NavigateUrl = table.Rows[0]["tsk_filePath"].ToString();

                                linkDelete.Visible = true;
                            }
                            else
                            {
                                hlinkFile.Visible = false;
                                linkDelete.Visible = false;
                            }
                        }
                        else
                        {
                            btnDone.Enabled = false;
                            this.Alert("没有获取到任务内容!");
                        }
                    }
                    else
                    {
                        btnDone.Enabled = false;
                        this.Alert("获取任务内容失败!");
                    }
                }
                else
                {
                    btnDone.Enabled = false;
                    this.Alert("获取唯一任务码失败!");
                }
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

        if (TaskHelper.UpdateTaskMstr(hidTaskNbr.Value, txtDesc.Text.Trim(), fileName, filePath, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                hlinkFile.Text = fileName;
                hlinkFile.NavigateUrl = filePath;

                hlinkFile.Visible = true;
                linkDelete.Visible = true;
            }

            this.Alert("任务保存成功！");
        }
        else
        {
            this.Alert("任务保存失败！");
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_DistributeList.aspx");
    }
    protected void linkDelete_Click(object sender, EventArgs e)
    {
        if (TaskHelper.DeleteTaskMstrFile(hidTaskNbr.Value))
        {
            hlinkFile.Text = string.Empty;
            hlinkFile.NavigateUrl = string.Empty;

            linkDelete.Visible = false;
        }
        else
        {
            this.Alert("关联文件删除失败！");
        }
    }
}