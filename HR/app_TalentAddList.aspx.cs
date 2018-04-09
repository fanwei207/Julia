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
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Net.Mail;
using QADSID;

public partial class HR_app_TalentAddList : System.Web.UI.Page
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_TalentList.aspx");
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
                if (addTalentList(_fname, _fpath
                        , txtUserName.Text, txtProfessional.Text
                        , Convert.ToInt32(ddlSex.SelectedItem.Value), ddlSex.SelectedItem.Text.Trim()
                        , txtbirth.Text
                        , Convert.ToInt32(ddlEdu.SelectedItem.Value), ddlEdu.SelectedItem.Text.Trim()
                        , txtSchool.Text
                        , Convert.ToInt32(ddlPlace.SelectedItem.Value), ddlPlace.SelectedItem.Text.Trim()
                        , Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()
                        , txtEmail.Text)
                    )
                {
                    ltlAlert.Text = "alert('提交成功！')";
                    txtUserName.Text = string.Empty;
                    //ddlSex.SelectedIndex = 0;
                    //txtbirth.Text = string.Empty;
                    //ddlEdu.SelectedIndex = 0;
                    //txtProfessional.Text = string.Empty;
                    //ddlPlace.SelectedIndex = 0;
                    //txtSchool.Text = string.Empty;
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
    public bool addTalentList(string fname, string fpath
                            , string username, string professional, int sexID, string sex, string birthday, int educationID
                            , string education, string school, int placeID, string place, int uid, string uname, string email)
    {
        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@fname", fname);
        param[1] = new SqlParameter("@fpath", fpath);
        param[2] = new SqlParameter("@username", username);
        param[3] = new SqlParameter("@professional", professional);

        param[4] = new SqlParameter("@sexID", sexID);
        param[5] = new SqlParameter("@sex", sex);
        param[6] = new SqlParameter("@birthday", birthday);
        param[7] = new SqlParameter("@educationID", educationID);
        param[8] = new SqlParameter("@education", education);
        param[9] = new SqlParameter("@school", school);
        param[10] = new SqlParameter("@placeID", placeID);
        param[11] = new SqlParameter("@place", place);
        param[12] = new SqlParameter("@upID", uid);
        param[13] = new SqlParameter("@upName", uname);
        param[14] = new SqlParameter("@email", email);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_addTalentList", param));
    }
}