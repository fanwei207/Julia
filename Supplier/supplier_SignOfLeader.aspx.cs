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

public partial class Supplier_supplier_SignOfLeader : BasePage
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
            if (Request["type"].ToString() == "FL")
            {
                hidLevel.Value = "1";
            }
            if (!string.IsNullOrEmpty(Request["level"]))
            {
                ddlFactoryInspectionLevel.SelectedValue = Request["level"].ToString();
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
                //更新等级
                if (Request["type"].ToString() == "FL")
                {
                    if (updateSupplierLevel())
                    {
                        ltlAlert.Text = "alert('等级更新失败！')";
                        return;
                    }
                    else
                    {
                        Opinion = "把供应商等级" + Request["level"].ToString() + "修改为等级" + ddlFactoryInspectionLevel.SelectedItem.ToString();
                        //if (insertResponOpinion(no, Opinion, type, typeID, _fileName, _filePath))
                        //{
                        //    ltlAlert.Text = "alert('失败！')";
                        //    return;
                        //}
                        //else
                        //{
                        //    ltlAlert.Text = "alert('成功！')";
                        //    return;
                        //}
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('成功！')";
                    return;
                }
            }
        }
    }
    private bool updateSupplierLevel()
    {
        string sql = "Update supplier_mstr Set supplier_FactoryInspectionLevelID = '" + ddlFactoryInspectionLevel.SelectedValue + "' Where supplier_No = '" + Request["no"].ToString() + "'";
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql));
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
}