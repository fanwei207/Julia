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
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Collections.Generic;
using QADSID;


public partial class HR_app_UploadResume : System.Web.UI.Page
{
    adamClass chk = new adamClass();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCompany.Text = Request.QueryString["company"];
            txtDepartment.Text = Request.QueryString["department"];
            txtProcess.Text = Request.QueryString["process"];
            txtPlantcode.Text = Request.QueryString["plantcode"];
            txtDepartmentID.Text = Request.QueryString["departmentid"];
            txtProcessID.Text = Request.QueryString["processid"];
            txtCompany.Visible = false;
            txtDepartment.Visible = false;
            txtProcess.Visible = false;
            txtPlantcode.Visible = false;
            txtDepartmentID.Visible = false;
            txtProcessID.Visible = false;
            BindEducation();
            BindSex();
            BindPlace();
        }
    }
    /// <summary>
    /// 绑定学历列表
    /// </summary>
    private void BindEducation()
    {
        DataTable dt = GetAllEdu();
        ddlEdu.DataSource = dt;
        ddlEdu.DataBind();
        ddlEdu.Items.Insert(0, new ListItem("---- 学 历 ----", "0"));
    }
    /// <summary>
    /// 获取所有学历
    /// </summary>
    private DataTable GetAllEdu()
    {
        string sql = "select systemCodeID,systemCodeName  from tcpc0..systemCode where systemCodeTypeID = 4";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    /// <summary>
    /// 绑定性别列表
    /// </summary>
    private void BindSex()
    {
        DataTable dt = GetSex();
        ddlSex.DataSource = dt;
        ddlSex.DataBind();
        ddlSex.Items.Insert(0, new ListItem("---- 性 别 ----", "0"));
    }
    /// <summary>
    /// 获取性别
    /// </summary>
    private DataTable GetSex()
    {
        string sql = "select systemCodeID,systemCodeName  from tcpc0..systemCode where systemCodeTypeID = 1";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    /// <summary>
    /// 绑定籍贯列表
    /// </summary>
    private void BindPlace()
    {
        DataTable dt = GetPlace();
        ddlPlace.DataSource = dt;
        ddlPlace.DataBind();
        ddlPlace.Items.Insert(0, new ListItem("---- 籍 贯 ----", "0"));
    }
    /// <summary>
    /// 获取籍贯列表
    /// </summary>
    private DataTable GetPlace()
    {
        string sql = "select systemCodeID,systemCodeName  from tcpc0..systemCode where systemCodeTypeID = 6";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写姓名!')";
            return;
        }
        if (ddlSex.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择性别!')";
            return;
        }
        if (txtbirth.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写出生年月!')";
            return;
        }
        if (ddlEdu.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择学历!')";
            return;
        }
        if (txtProfessional.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写专业!')";
            return;
        }
        if (ddlPlace.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请填写籍贯!')";
            return;
        }
        if (txtSchool.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写毕业院校!')";
            return;
        }
        if (txtEmail.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写邮箱!')";
            return;
        }
        //上传简历
        _fpath = string.Empty;
        _fname = string.Empty;
        if (FileUpload2.Value.Trim() != string.Empty)
        {
            if (UpmessageFile())
            {
                //if (insertFile(lblshipid.Text, _fname, _fpath, txt_tracking.Text, Session["uID"].ToString()))
                if (addInformation(_fname, _fpath
                    ,txtCompany.Text,txtDepartment.Text,txtProcess.Text
                        , txtUserName.Text, txtProfessional.Text
                        , Convert.ToInt32(ddlSex.SelectedItem.Value), ddlSex.SelectedItem.Text.Trim()
                        , txtbirth.Text
                        , Convert.ToInt32(ddlEdu.SelectedItem.Value), ddlEdu.SelectedItem.Text.Trim()
                        , txtSchool.Text
                        , Convert.ToInt32(ddlPlace.SelectedItem.Value),ddlPlace.SelectedItem.Text.Trim()
                        ,Convert.ToInt32(Session["uID"].ToString()),Session["uName"].ToString()
                        ,txtEmail.Text,txtPlantcode.Text, txtDepartmentID.Text, txtProcessID.Text)
                    )
                {
                    ltlAlert.Text = "alert('提交成功！')";
                    txtUserName.Text = string.Empty;
                    ddlSex.SelectedIndex = 0;
                    txtbirth.Text = string.Empty;
                    ddlEdu.SelectedIndex = 0;
                    txtProfessional.Text = string.Empty;
                    ddlPlace.SelectedIndex = 0;
                    txtSchool.Text = string.Empty;
                }
                else
                {
                    ltlAlert.Text = "alert('数据库写入失败！')";
                }
            }
            else
            {
                ltlAlert.Text = "alert('文件上传失败！')";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('上传文件不能为空！')";
        }


        //if (addInformation(txtCompany.Text,txtDepartment.Text,txtProcess.Text
        //                ,txtUserName.Text
        //                , Convert.ToInt32(ddlSex.SelectedItem.Value), ddlSex.SelectedItem.Text.Trim()
        //                , Convert.ToInt32(txtAge.Text)
        //                , Convert.ToInt32(ddlEdu.SelectedItem.Value), ddlEdu.SelectedItem.Text.Trim()
        //                , txtSchool.Text
        //                , Convert.ToInt32(ddlPlace.SelectedItem.Value),ddlPlace.SelectedItem.Text.Trim()))
        //{
        //    ltlAlert.Text = "alert('添加成功!')";
        //}
    }
    protected bool UpmessageFile()
    {
        string strUserFileName = FileUpload2.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/APP/";
        string strCatFolder = Server.MapPath(catPath);

        string attachName = Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
        string attachExtension = Path.GetExtension(FileUpload2.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../Excel/"), DateTime.Now.ToFileTime().ToString() + attachExtension);//合并两个路径为上传到服务器上的全路径
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
            FileUpload2.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";

            return false;
        }
        string path = @"/TecDocs/APP/";

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
            ltlAlert.Text = "alert('fail to move file')";

            if (File.Exists(SaveFileName))
            {
                try
                {
                    File.Delete(SaveFileName);
                }
                catch
                {
                    ltlAlert.Text = "alert('fail to delete folder')";

                    return false;
                }
            }
            return false;
        }


        _fpath = catPath + docid;
        _fname = fileName;
        return true;
    }
    /// <summary>
    /// 上传应聘人员信息
    /// </summary>
    /// <returns></returns>
    public bool addInformation(string fname, string fpath, string company, string department, string process
                            , string username,string professional, int sexID, string sex, string birthday, int educationID
                            , string education, string school, int placeID, string place,int uid ,string uname,string email
                            , string plantcode, string departid, string procid)
    {
        SqlParameter[] param = new SqlParameter[21];
        param[0] = new SqlParameter("@fname", fname);
        param[1] = new SqlParameter("@fpath", fpath);

        param[2] = new SqlParameter("@company", company);
        param[3] = new SqlParameter("@department", department);
        param[4] = new SqlParameter("@process", process);
        param[5] = new SqlParameter("@username", username);
        param[6] = new SqlParameter("@professional", professional);

        param[7] = new SqlParameter("@sexID", sexID);
        param[8] = new SqlParameter("@sex", sex);
        param[9] = new SqlParameter("@birthday", birthday);
        param[10] = new SqlParameter("@educationID", educationID);
        param[11] = new SqlParameter("@education", education);
        param[12] = new SqlParameter("@school", school);
        param[13] = new SqlParameter("@placeID", placeID);
        param[14] = new SqlParameter("@place", place);
        param[15] = new SqlParameter("@upID", uid);
        param[16] = new SqlParameter("@upName", uname);
        param[17] = new SqlParameter("@email", email);
        param[18] = new SqlParameter("@plantcode", plantcode);
        param[19] = new SqlParameter("@departid", departid);
        param[20] = new SqlParameter("@procid", procid);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_addInformation", param));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_ResumeList.aspx?App_Company=" + txtCompany.Text + "&App_department=" + txtDepartment.Text + "&App_Process=" + txtProcess.Text);
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <returns></returns>
    public bool insertFile(string shipid, string fname, string fpath, string remark, string createby)
    {
        try
        {
            string strSql = "sp_sid_insertPictureurl";

            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@shipid", shipid);
            sqlParam[1] = new SqlParameter("@fname", fname);
            sqlParam[2] = new SqlParameter("@fpath", fpath);
            sqlParam[3] = new SqlParameter("@remark", remark);
            sqlParam[4] = new SqlParameter("@createby", createby);
            SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            return true;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
    }
}