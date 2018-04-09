using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Web.Security;

public partial class QC_QC_CertificationTestNew : BasePage
{
    adamClass adam = new adamClass();
    public string _fpath
    {
        get
        {
            return ViewState["fpath"].ToString();
        }
        set
        {
            ViewState["fpath"] = value;
        }
    }
    public string _fname
    {
        get
        {
            return ViewState["fname"].ToString();
        }
        set
        {
            ViewState["fname"] = value;
        }
    }
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("591200400", "权限-查看明细");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"].ToString() == "new")
            {
                //labNo.Text = SelectPurchaseNo();
            }
            else if (Request["type"].ToString() == "det")
            {
                //labNo.Text = Request["no"].ToString();
                //ddlBusDept.SelectedValue = Request["deptid"];
                //BindGV();
                DataTable dt = selectQCTestMstrInfo();
                if (dt == null || dt.Rows.Count == 0)
                {
                    ltlAlert.Text = "alert('读取信息有误，请联系管理员'); ";
                    return;
                }
                else 
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        txtNbr.Text = row["QC_Nbr"].ToString();
                        txtLot.Text = row["QC_Lot"].ToString();
                        txtSite.Text = row["QC_Site"].ToString();
                        hidSite.Value = row["QC_Site"].ToString();
                        txtDomain.Text = row["QC_Domain"].ToString();
                        hidDomain.Value = row["QC_Domain"].ToString();
                        txtPart.Text = row["QC_Part"].ToString();
                        hidPart.Value = row["QC_Part"].ToString();
                        txtDesc.Text = row["QC_Desc"].ToString();
                        hidDesc.Value = row["QC_Desc"].ToString();
                        txtContent.Text = row["QC_TestDesc"].ToString();
                        rbtTestType.SelectedValue = row["QC_TestType"].ToString();
                        hidCreateBy.Value = row["createBy"].ToString();
                    }
                }
                if (hidCreateBy.Value.ToString() != Session["uID"].ToString())
                {
                    //没有权限的只能查看
                    if (!this.Security["591200400"].isValid) // View All Project 权限
                    {
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        btnUpload.Visible = false;
                    }
                }
            }
            BindFileList(); 
        }
    }
    private DataTable selectQCTestMstrInfo()
    {
        string str = "sp_qc_selectQCTestMstrInfo";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", Request["ID"].ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    private void BindFileList()
    {
        DataTable dt = getPurchaseFileList();
        gvFile.DataSource = dt;
        gvFile.DataBind();
    }
    private DataTable getPurchaseFileList()
    {
        string sql = string.Empty;
        //sql = "Select * From QC_TestFileTemp Where Convert(varchar(10),CreateDate,120) = Convert(varchar(10),getdate(),120) and CreateBy = '" + Session["uID"].ToString() + "'";

        if (Request["type"].ToString() == "new")
        {
            sql = "Select * From QC_TestFileTemp Where Convert(varchar(10),CreateDate,120) = Convert(varchar(10),getdate(),120) and CreateBy = '" + Session["uID"].ToString() + "'";
        }
        else if (Request["type"].ToString() == "det")
        {
            sql = "Select * From QC_TestFile Where MID = '" + Request["ID"] + "'";
        }

        //string sql = "Select * From rp_purchaseFileList Where rp_No = '" + labNo.Text + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        QCTestMstr("submit");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        QCTestMstr("save");
    }
    private void QCTestMstr(string type)
    {
        string no = string.Empty;
        if (Request["type"].ToString() == "new")
        {
            no = SelectPurchaseNo();
            if (!insertQCTestMstr(no,type))
            {
                ltlAlert.Text = "alert('认证检验单提交失败，请联系管理员'); ";
                return;
            }
            else
            {
                Response.Redirect("../QC/QC_CertificationTestList.aspx");
            }
        }
        else if (Request["type"].ToString() == "det")
        {
            no = Request["no"].ToString();
            if (!updateQCTestMstr(type))
            {
                ltlAlert.Text = "alert('认证检验单提交失败，请联系管理员'); ";
                return;
            }
            else
            {
                Response.Redirect("../QC/QC_CertificationTestList.aspx");
            }
        }
    }
    private bool insertQCTestMstr(string no, string type)
    {
        string str = "sp_qc_insertQCTestMstr";
        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@nbr", txtNbr.Text.Trim());
        param[1] = new SqlParameter("@lot", txtLot.Text.Trim());
        param[2] = new SqlParameter("@part", hidPart.Value.ToString());
        param[3] = new SqlParameter("@domain", hidDomain.Value.ToString());
        param[4] = new SqlParameter("@site", hidSite.Value.ToString());
        param[5] = new SqlParameter("@desc", hidDesc.Value.ToString());
        param[6] = new SqlParameter("@testType", rbtTestType.SelectedValue.ToString());
        param[7] = new SqlParameter("@content", txtContent.Text.ToString());
        param[8] = new SqlParameter("@uID", Session["uID"].ToString());
        param[9] = new SqlParameter("@uName", Session["uName"].ToString());
        param[10] = new SqlParameter("@no", no);
        param[11] = new SqlParameter("@type", type);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool updateQCTestMstr(string type)
    {
        string str = "sp_qc_UpdateQCTestMstr";
        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@nbr", txtNbr.Text.Trim());
        param[1] = new SqlParameter("@lot", txtLot.Text.Trim());
        param[2] = new SqlParameter("@part", hidPart.Value.ToString());
        param[3] = new SqlParameter("@domain", hidDomain.Value.ToString());
        param[4] = new SqlParameter("@site", hidSite.Value.ToString());
        param[5] = new SqlParameter("@desc", hidDesc.Value.ToString());
        param[6] = new SqlParameter("@testType", rbtTestType.SelectedValue.ToString());
        param[7] = new SqlParameter("@content", txtContent.Text.ToString());
        param[8] = new SqlParameter("@uID", Session["uID"].ToString());
        param[9] = new SqlParameter("@uName", Session["uName"].ToString());
        param[10] = new SqlParameter("@id", Request["ID"].ToString());
        param[11] = new SqlParameter("@type", type);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private string SelectPurchaseNo()
    {
        string str = "sp_qc_SelectQCTestNo";

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str));
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (filename.Value.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('上传文件不能为空！')";
            return;
        }
        if (UpLoadFile(filename))
        {
            if (Request["type"].ToString() == "new")
            {
                if (!InsertPurchaseFileTemp(_fname, _fpath, Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('保存文件信息失败，请联系管理员！')";
                    return;
                }
                else
                {
                    BindFileList();
                }
            }
            else if (Request["type"].ToString() == "det")
            {
                if (!InsertPurchaseFile(_fname, _fpath, Session["uID"].ToString(), Session["uName"].ToString(), Request["ID"].ToString()))
                {
                    ltlAlert.Text = "alert('保存文件信息失败，请联系管理员！')";
                    return;
                }
                else
                {
                    BindFileList();
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('上传文件失败，请联系管理员！')";
            return;
        }
    }
    /// <summary>
    /// 上传文件
    /// </summary>
    protected bool UpLoadFile(HtmlInputFile fileID)
    {
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);

        string strUserFileName = fileID.PostedFile.FileName;//是获取文件的路径，即FileUpload控件文本框中的所有内容，
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string attachExtension = Path.GetExtension(fileID.PostedFile.FileName);
        string _newFileName = DateTime.Now.ToFileTime().ToString() + attachExtension;

        string catPath = @"/TecDocs/QCtest/";
        string strCatFolder = Server.MapPath(catPath);
        if (!Directory.Exists(strCatFolder))
        {
            Directory.CreateDirectory(strCatFolder);
        }

        string SaveFileName = System.IO.Path.Combine(strCatFolder, _newFileName);//合并两个路径为上传到服务器上的全路径
        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('fail to delete folder！')";
                return false;
            }
        }
        try
        {
            fileID.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";
            return false;
        }

        _fpath = catPath + _newFileName;
        _fname = _fileName;
        return true;
    }

    private bool InsertPurchaseFileTemp(string _fname, string _fpath, string _uID, string _uName)
    {
        string str = "sp_qc_InsertQCTestFileTemp";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@fname", _fname);
        param[1] = new SqlParameter("@fpath", _fpath);
        param[2] = new SqlParameter("@uID", _uID);
        param[3] = new SqlParameter("@uName", _uName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertPurchaseFile(string _fname, string _fpath, string _uID, string _uName, string ID)
    {
        string str = "sp_qc_InsertQCTestFile";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@fname", _fname);
        param[1] = new SqlParameter("@fpath", _fpath);
        param[2] = new SqlParameter("@uID", _uID);
        param[3] = new SqlParameter("@uName", _uName);
        param[4] = new SqlParameter("@ID", ID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../QC/QC_CertificationTestList.aspx");
    }
    protected void gvFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string ID = gvFile.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", ID);
        string str = string.Empty;
        if (Request["type"].ToString() == "new")
        {
            str = "sp_qc_DeleteQCTestFileTemp";
        }
        else if (Request["type"].ToString() == "det")
        {
            str = "sp_qc_DeleteQCTestFile";
        }
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

        BindFileList();
    }
    protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFile.DataKeys[intRow].Values["fpath"].ToString().Trim();
            string fileName = gvFile.DataKeys[intRow].Values["fname"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
}