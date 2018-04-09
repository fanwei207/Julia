using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Configuration;

public partial class Supplier_supplier_SignOfLeader1 : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"].ToString() == "SupplierNum")
            {
                Label1.Text = "供应商代码：";
                hidType.Value = "0";
            }
            if (Request["type"].ToString() == "SuppDept")
            {
                btnYes.Text = "提交";
                btnNo.Text = "拒绝";
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string _fileName = "";
        string _filePath = "";
        string Opinion = txtOpinion.Text;
        string no = Request["no"].ToString();
        string type = Request["type"].ToString();
        string typeID = Request["typeID"].ToString();
        #region 上传文件
        if (FileUpload1.FileName == string.Empty)
        {
            _filePath = "";
            _fileName = "";
        }
        else
        {
            try
            {
                if (!UploadFile(ref _filePath, ref _fileName))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
        #endregion
        if (type == "SupplierNum")
        {
            if (insertSupplierNum())
            {
                ltlAlert.Text = "alert('供应商代码添加失败，请联系管理员！')";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('供应商代码添加成功！')";
                return;
            }
        }
        else
        {
            if (insertResponOpinion(no, Opinion, type, typeID, _fileName, _filePath))
            {
                ltlAlert.Text = "alert('失败！')";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('成功！')";
                return;
            }
        }
    }
    private bool insertSupplierNum()
    {
        string str = "sp_supplier_InsertSupplierNum";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@no", Request["no"].ToString());
        param[1] = new SqlParameter("@supplierNum", txtOpinion.Text);
        param[2] = new SqlParameter("@uID", Session["uID"].ToString());
        param[3] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool UploadFile(ref string filePath, ref string fileName)
    {
        string strUserFileName = string.Empty;

        strUserFileName = FileUpload1.PostedFile.FileName;

        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/NewSupplier/" + Request["no"].ToString() + "/";
        string strCatFolder = Server.MapPath(catPath);
        string attachName = "";
        string attachExtension = "";

        //获取文件的名称和后缀
        //attachName = Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
        attachExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);


        //string SaveFileName = System.IO.Path.Combine(strCatFolder, _newFileName);//合并两个路径为上传到服务器上的全路径
        string SaveFileName = System.IO.Path.Combine(strCatFolder, DateTime.Now.ToFileTime().ToString() + attachExtension);//合并两个路径为上传到服务器上的全路径
        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('文件删除失败！')";
                return false;
            }
        }
        if (!Directory.Exists(Server.MapPath(catPath)))
        {
            Directory.CreateDirectory(Server.MapPath(catPath));
        }
        try
        {
            FileUpload1.PostedFile.SaveAs(SaveFileName);

            //GetExcelContents(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('文件上传失败')";
            return false;
        }
        string path = @"/TecDocs/NewSupplier/" + Request["no"].ToString() + "/";

        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        string docid = DateTime.Now.ToFileTime().ToString() + attachExtension;
        try
        {
            File.Move(SaveFileName, Server.MapPath(path + docid));
        }
        catch
        {
            ltlAlert.Text = "alert('文件移除失败')";

            if (File.Exists(SaveFileName))
            {
                try
                {
                    File.Delete(SaveFileName);
                }
                catch
                {
                    ltlAlert.Text = "alert('文件夹删除失败')";

                    return false;
                }
            }
            return false;
        }


        filePath = catPath + docid;
        fileName = _fileName;
        return true;
    }
    private bool insertResponOpinion(string no, string opinion, string type, string typeID, string fileName, string filePath)
    {
        string str = "sp_supplier_InsertResponOpinion";

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@ResponOpinion", opinion);
        param[2] = new SqlParameter("@ResponType", type);
        param[3] = new SqlParameter("@ResponTypeID", typeID);
        param[4] = new SqlParameter("@fileName", fileName);
        param[5] = new SqlParameter("@filePath", filePath);
        param[6] = new SqlParameter("@ResponID", Session["uID"].ToString());
        param[7] = new SqlParameter("@ResponName", Session["uName"].ToString());
        param[8] = new SqlParameter("@uID", Session["uID"].ToString());
        param[9] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        string _fileName = "";
        string _filePath = "";
        string Opinion = txtOpinion.Text;
        string no = Request["no"].ToString();
        string type = Request["type"].ToString();
        string typeID = Request["typeID"].ToString();
        #region 上传文件
        if (FileUpload1.FileName == string.Empty)
        {
            _filePath = "";
            _fileName = "";
        }
        else
        {
            try
            {
                if (!UploadFile(ref _filePath, ref _fileName))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
        #endregion

        #region 保存留言
        if (insertResponOpinion(no, Opinion, type, typeID, _fileName, _filePath))
        {
            ltlAlert.Text = "alert('留言失败！')";
            return;
        }
        //else
        //{
        //    ltlAlert.Text = "alert('留言成功！')";
        //    return;
        //}
        #endregion

        if (btnIsAgree(type, "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            this.Alert("操作成功！");

            if (!Request.QueryString["successEmailTo"].Equals(string.Empty))
            {
                emailMassage(Request.QueryString["successEmailTo"], string.Empty, "新增供应商有需要您审批的项目", "新增供应商有需要您审批的项目");
            }

            return;
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        string _fileName = "";
        string _filePath = "";
        string Opinion = txtOpinion.Text;
        string no = Request["no"].ToString();
        string type = Request["type"].ToString();
        string typeID = Request["typeID"].ToString();
        #region 上传文件
        if (FileUpload1.FileName == string.Empty)
        {
            _filePath = "";
            _fileName = "";
        }
        else
        {
            try
            {
                if (!UploadFile(ref _filePath, ref _fileName))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
        #endregion

        #region 保存留言
        if (insertResponOpinion(no, Opinion, type, typeID, _fileName, _filePath))
        {
            ltlAlert.Text = "alert('留言失败！')";
            return;
        }
        //else
        //{
        //    ltlAlert.Text = "alert('留言成功！')";
        //    return;
        //}
        #endregion

        #region 按钮操作
        if (btnIsAgree(type, "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            this.Alert("操作成功！");

            if (!Request.QueryString["FailEmailTo"].Equals(string.Empty))
            {
                if (Request.QueryString["type"].Equals("SuppDept"))
                {
                    emailMassage(Request.QueryString["FailEmailTo"], string.Empty, "新增供应商您申请的项目被拒绝", "新增供应商您申请的项目被拒绝");
                }
                else
                {
                    emailMassage(Request.QueryString["FailEmailTo"], string.Empty, "新增供应商有需要您审批的项目", "新增供应商有需要您审批的项目");
                }
                
            }
            return;
        }
        #endregion
    }

    private bool btnIsAgree(string btnType, string btnTypeValue)
    {
        string str = "sp_supplier_btnIsAgree";

        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@btnType", btnType);
        param[1] = new SqlParameter("@btnTypeValue", btnTypeValue);
        param[2] = new SqlParameter("@uID", Session["uID"].ToString());
        param[3] = new SqlParameter("@uName", Session["uName"].ToString());
        param[4] = new SqlParameter("@no", Request["no"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }

    private void emailMassage(string to, string copy, string subject, string body)
    {

        #region 发送邮件
        string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();

        subject = "新增供应商有需要您审批的项目";



        string bodyTemp = body;

        #region 写Body
        body = "<font style='font-size: 12px;'>" + bodyTemp + "</font><br />";
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
    }
}