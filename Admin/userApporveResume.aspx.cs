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
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using QADSID;
using System.Collections.Generic;


public partial class userApporveResume : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["type"] != null)
            {
                btn_back.Visible = true;
            }
            lblUserNo.Text = Request["userNo"];
            lblUserName.Text = Request["userName"];
            lblDeptName.Text = Request["deptName"];
            lblRoleName.Text = Request["roleName"];
            lbplantID.Text = Request["plantID"];

            if (Request["userNo"] == null || Request["userNo"].Length == 0)
            {
                btnUpload.Enabled = false;
                ltlAlert.Text = "alert('传回信息不完整，无法上传简历！')";
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile)
        {
            ltlAlert.Text = "alert('请选择一个文件！')";
            return;
        }
        else if (FileUpload1.PostedFile.ContentLength > 5 * 1024 * 1024)
        {
            ltlAlert.Text = "alert('简历大小应在5M以内！')";
            return;
        }

        string _path = Server.MapPath("/TecDocs/Resume/" + Session["PlantCode"].ToString());
        if (!Directory.Exists(_path))
        {
            try
            {
                Directory.CreateDirectory(_path);
            }
            catch
            {
                ltlAlert.Text = "alert('创建文件夹失败，请联系管理员！')";
                return;
            }
        }

        _path = _path + "/" + lblUserNo.Text + Path.GetExtension(FileUpload1.PostedFile.FileName);

        if (!File.Exists(_path))
        {
            try
            {
                File.Delete(_path);
            }
            catch
            {
                ltlAlert.Text = "alert('删除已存在简历时失败，请联系管理员！')";
                return;
            }
        }

        try
        {
            FileUpload1.PostedFile.SaveAs(_path);
            if (updateResume(lblUserNo.Text, "/TecDocs/Resume/" + Session["PlantCode"].ToString() + "/" + lblUserNo.Text + Path.GetExtension(FileUpload1.PostedFile.FileName), lbplantID.Text))
            {
                Response.Redirect("userApproveNewList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
            }
            else
            {
                ltlAlert.Text = "alert('简历信息保存失败！')";
                return;
            }
        }
        catch (Exception ex)
        {
            ltlAlert.Text = "alert('简历保存时失败，请联系管理员！')";
        } 
    }

    protected bool updateResume(string userNo, string path, string plantCode)
    {
        try
        {
            string strName = "sp_userApprove_updateResume";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@userNo", userNo);
            param[1] = new SqlParameter("@path", path);
            param[2] = new SqlParameter("@plantCode", plantCode);
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[3].Value);

        }
        catch (Exception ex)
        {
            return false;
        }

    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("userApproveNewList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}
