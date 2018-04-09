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

public partial class m5_new_Edit : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 单号获取
            if (Request.QueryString["no"] != null || Request.QueryString["no"] == string.Empty)
            {
                lblNo.Text = Request.QueryString["no"];
            }
            else
            {
                btnDone.Enabled = false;
                this.Alert("The No. is error!Please contact the administrator!");
            }
            #endregion



            #region 获取ddlMarketing ddlLevel ModelNo

            ddlLevel.DataSource = this.GetDDLLevel();
            ddlLevel.DataBind();

            this.GetLevelAndModel();
            #endregion

            //this.GetDdlMarketing();
           

            #region 绑定dropProject
            radProject.Items.Clear();
            radProject.DataSource = this.GetProjects();
            radProject.DataBind();
            #endregion

            #region 页面信息绑定
            BindPage();
            #endregion
        }
    }
    private DataTable GetDDLLevel()
    {
        try
        {
            string sqlstr = "SELECT soque_degreeName,soque_did FROM dbo.soque_degree";

            return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sqlstr).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private void GetLevelAndModel()
    {
        ddlMarketing.DataSource = this.getMarket();
        ddlMarketing.DataBind();
        #region 获取Level和modelNo.
        SqlDataReader sdr = this.GetLevelAndModel(lblNo.Text);
        while (sdr.Read())
        {
            ddlLevel.SelectedValue = sdr["m5_level"].ToString();
            txtModelNo.Text = sdr["m5_modelNumber"].ToString();
            ddlMarketing.SelectedValue = sdr["m5_market"].ToString();
        
        }
        sdr.Close();
        #endregion

    }
    private SqlDataReader GetLevelAndModel(string no)
    {
        try
        {
            string sqlstr = "sp_m5_selectLevelAndModelByNo";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@no",no)
            
            };

            return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sqlstr, param);


        }
        catch
        {
            return null;
        }
    }
    private DataTable getMarket()
    {
        try
        {
            string sqlstr = "sp_m5_GetDdlMarketing";

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private void GetDdlMarketing()
    {
        try
        {
            string sqlstr = "sp_m5_GetDdlMarketing";

            SqlDataReader sdr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sqlstr);
            ddlMarketing.Items.Add(new ListItem("Please choose one marketing", "0"));
            while (sdr.Read())
            {
                ddlMarketing.Items.Add(new ListItem(sdr["m5mk_name"].ToString(), sdr["m5mk_ID"].ToString()));

            }
            sdr.Dispose();
            sdr.Close();


        }
        catch
        {

        }
    }
    public DataTable GetProjects()
    {
        try
        {
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_m5_selectProject").Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public DataTable GetM5MstrByNo(string no)
    {
        try
        {
            string strSql = "sp_m5_selectM5MstrByNo";
            SqlParameter param = new SqlParameter("@no", no);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public void BindPage()
    {
        DataTable _mstrM5 = GetM5MstrByNo(lblNo.Text.Trim());

        if (_mstrM5.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('Failed to get the details!');";
            return;
        }
        radProject.Items.FindByValue(_mstrM5.Rows[0]["m5_project"].ToString()).Selected = true;
        ddlMarketing.SelectedValue = _mstrM5.Rows[0]["m5mk_ID"].ToString();
        txtDesc.Text = _mstrM5.Rows[0]["m5_desc"].ToString();
        txtReason.Text = _mstrM5.Rows[0]["m5_reason"].ToString();
        if (!string.IsNullOrEmpty(_mstrM5.Rows[0]["m5_desc_file"].ToString()))
        {
            hlinkDesc.Text = "附件:" + _mstrM5.Rows[0]["m5_desc_file"].ToString();
            hlinkDesc.NavigateUrl = _mstrM5.Rows[0]["m5_desc_path"].ToString();
            cb_decsfile.Visible = true;
        }
        else
        {
            cb_decsfile.Visible = false;
            hlinkDesc.Text = "";
        }
        if (!string.IsNullOrEmpty(_mstrM5.Rows[0]["m5_reason_file"].ToString()))
        {
            hlinkReason.Text = "附件:" + _mstrM5.Rows[0]["m5_reason_file"].ToString();
            hlinkReason.NavigateUrl = _mstrM5.Rows[0]["m5_reason_path"].ToString();
            cb_reasonfile.Visible = true;
        }
        else
        {
            cb_reasonfile.Visible = false;
            hlinkReason.Text = "";
        }
    }
    protected bool UploadDescFile(string strCateFolder, string strDescSaveFileName, string strDescExtension)
    {
        if (fileDesc.PostedFile.FileName != string.Empty)
        {
            if (fileDesc.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }

            Int32 bytes = fileDesc.PostedFile.ContentLength;

            string _logicalPath = Server.MapPath("/TecDocs/ECN/");

            if (fileDesc.PostedFile.ContentLength > 0)
            {
                try
                {
                    fileDesc.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strDescSaveFileName+strDescExtension);
                }
                catch
                {
                    ltlAlert.Text = "alert('文档上传失败!');";
                    return false;
                }
            }
            else
            {
                this.Alert("不能上传空文档!");
                return false;
            }
        }
        return true;
    }

    protected bool UploadReasonFile(string strCateFolder, string strReasonSaveFileName,string strReasonExtension)
    {
        if (fileReason.PostedFile.FileName != string.Empty)
        {
            if (fileReason.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }
            Int32 bytes = fileReason.PostedFile.ContentLength;
            string _logicalPath = Server.MapPath("/TecDocs/ECN/");
            if (fileReason.PostedFile.ContentLength > 0)
            {
                try
                {
                    fileReason.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strReasonSaveFileName + strReasonExtension);
                }
                catch
                {
                    ltlAlert.Text = "alert('文档上传失败!');";
                    return false;
                }
            }
            else
            {
                this.Alert("不能上传空文档!");
                return false;
            }
        }
        return true;
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (radProject.SelectedIndex < 0)
        {
            this.Alert("请先选择一个 变更的项目！");
            return;
        }
        if (string.IsNullOrEmpty(txtDesc.Text))
        {
            this.Alert("申请变更内容 不能为空！");
            return;
        }
         if (string.IsNullOrEmpty(txtReason.Text))
        {
            this.Alert("申请变更理由 不能为空！");
            return;
        }
         if (!"06553FC5-982B-4C6A-8A35-83DEC9887C32".Equals(ddlMarketing.SelectedValue.ToString()) && txtModelNo.Text.ToString().Trim().Equals(string.Empty))
         {
             this.Alert("Please enter ModelNo!");
             return;
         }

         #region  上传附件验证

        string strDescName = "";//文件名
        string strCateFolder = "/TecDocs/ECN/";
        string strDescExtension = "";//文件后缀
        string strDescSaveFileName = "";//储存名

        if (fileDesc.PostedFile.FileName != string.Empty)
        {
            if (!string.IsNullOrEmpty(hlinkDesc.NavigateUrl))
            {
                string pDescPath = Server.MapPath("../" + hlinkDesc.NavigateUrl);
                File.Delete(pDescPath);
            }
            strDescName = Path.GetFileNameWithoutExtension(fileDesc.PostedFile.FileName);
            strDescExtension = Path.GetExtension(fileDesc.PostedFile.FileName);
            strDescSaveFileName = DateTime.Now.ToFileTime().ToString();
            if (!UploadDescFile(strCateFolder, strDescSaveFileName, strDescExtension))
            {
                this.Alert("文档上传失败！");
                return;
            }
            cb_decsfile.Checked = true;
        }
        if (fileDesc.PostedFile.FileName == string.Empty && cb_decsfile.Checked == true)
        {
            btnDone.Attributes.Add("onclick", "return confirm('您选择了变更内容更新文件而未上传文件，是否继续?')");
        }
        string strReasonName = "";//文件名
        string strReasonExtension = "";//文件后缀
        string strReasonSaveFileName = "";//储存名
        if (fileReason.PostedFile.FileName != string.Empty)
        {
            if (!string.IsNullOrEmpty(hlinkReason.NavigateUrl))
            {
                string pReasonPath = Server.MapPath("../" + hlinkReason.NavigateUrl);
                File.Delete(pReasonPath);
            } 
            strReasonName = Path.GetFileNameWithoutExtension(fileReason.PostedFile.FileName);
             strReasonExtension = Path.GetExtension(fileReason.PostedFile.FileName);
             strReasonSaveFileName = DateTime.Now.ToFileTime().ToString();

             if (!UploadReasonFile(strCateFolder, strReasonSaveFileName,strReasonExtension))
             {
                 this.Alert("文档上传失败！");
                 return;
             }
             cb_reasonfile.Checked = true;
         }
        if (fileReason.PostedFile.FileName == string.Empty && cb_reasonfile.Checked == true)
        {
            btnDone.Attributes.Add("onclick", "return confirm('您选择了申请理由更新文件而未上传文件，是否继续?')");
        }
        #endregion

         try
        {
            string strName = "sp_m5_saveM5MstrEdit";
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@no", lblNo.Text);
            param[1] = new SqlParameter("@project", radProject.SelectedValue);
            param[2] = new SqlParameter("@desc", txtDesc.Text.Trim());
            param[3] = new SqlParameter("@desc_file", strDescName + strDescExtension);
            param[4] = new SqlParameter("@desc_path", strCateFolder + strDescSaveFileName + strDescExtension);
            param[5] = new SqlParameter("@isUpatedescFile", cb_decsfile.Checked);
            param[6] = new SqlParameter("@reason", txtReason.Text.Trim());
            param[7] = new SqlParameter("@reason_file", strReasonName + strReasonExtension);
            param[8] = new SqlParameter("@reason_path", strCateFolder + strReasonSaveFileName + strReasonExtension);
            param[9] = new SqlParameter("@isUpateReasonFile", cb_reasonfile.Checked);
            param[10] = new SqlParameter("@market", ddlMarketing.SelectedItem.Value);
            param[11] = new SqlParameter("@uID", Session["uID"].ToString());
            param[12] = new SqlParameter("@uName", Session["uName"].ToString());
            param[13] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[13].Direction = ParameterDirection.Output;
            param[14] = new SqlParameter("@level",ddlLevel.SelectedValue);
            param[15] = new SqlParameter("@model", txtModelNo.Text.Trim());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[13].Value))
            {   
                this.Alert("提交失败！请联系管理员！");
            }
            else
            {
                this.ltlAlert.Text = "alert('保存成功！'); window.location='/product/m5_mstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "'";
            }
        }
        catch (Exception ex)
        {
            this.Alert("数据库操作失败！请联系管理员！");
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.ltlAlert.Text = "window.location='/product/m5_mstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "'";
    }
}