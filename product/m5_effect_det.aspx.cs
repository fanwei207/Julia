using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.IO;
using System.Threading;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;

public partial class m5_effect_det : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

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
    public string _parentID
    {
        get
        {
            return ViewState["parentID"].ToString();
        }
        set
        {
            ViewState["parentID"] = value;
        }
    }

    public Dictionary<string, string> ChooseAboutSafety
    {
        get
        {
            return (Dictionary<string, string>)ViewState["ChooseAboutSafety"];
        }
        set
        {
            ViewState["ChooseAboutSafety"] = value;
        }
    }

    public int _pageindex
    {
        get
        {
            return Convert.ToInt32(ViewState["pageindex"].ToString());
        }
        set
        {
            ViewState["pageindex"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            chkAllAuth.Visible = this.Security["560000417"].isValid;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            #region 绑定dropProject
            DataTable dt = this.GetM5MstrEffectByUser(Session["uID"].ToString());
            DataTable dtEffect = dt;
            dropEffect.DataSource = dtEffect;
            dropEffect.DataBind();
           
            if (dtEffect.Rows.Count != 1)
            {
                dropEffect.Items.Insert(0, new ListItem("--", "0"));

            }
            else
            { 
                 foreach(DataRow gr in dt.Rows)
                 {
                     if (gr["m5e_isAboutSafety"].ToString() == "True")
                     {
                         chkIsAboutSafety.Visible = true;
                     }
                 }
            }
            #endregion

            foreach(DataRow gr in dt.Rows)
            {
                dic.Add(gr["m5e_id"].ToString(), gr["m5e_isAboutSafety"].ToString());
                if (gr["m5_isAboutSafety"].ToString().Equals("True"))
                {
                    chkIsAboutSafety.Checked = true;
                }
            }
            ChooseAboutSafety = dic;
            //lbDescEn.Text = Request.QueryString["descEn"];
        }
    }
    public DataTable GetM5MstrEffectByUser(string uID)
    {
        try
        {
            string strSql = "sp_m5_selectM5EffectByUser";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@no", Request.QueryString["no"]);
            param[2] = new SqlParameter("@allAuth", chkAllAuth.Checked);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        _fpath = string.Empty;
        _fname = string.Empty;

        if (dropEffect.SelectedItem.Text == "--")
        {
            ltlAlert.Text = "alert('Responsibility is required')";
            return;
        }

        if (!radYes.Checked && !radNo.Checked)
        {
            ltlAlert.Text = "alert('Yes & No is required')";
            return;
        }


        if (string.IsNullOrEmpty(txtMsg.Text))
        {
            ltlAlert.Text = "alert('Comment is required')";
            return;
        }
        else if (!string.IsNullOrEmpty(file1.Value.Trim()))
        {
            UpmessageFile();
        }

        int isAboutSafety = 0;

        if(chkIsAboutSafety.Checked)
        {
            isAboutSafety = 1;
        }

        try
        {
            string strSql = "sp_m5_saveM5Effect";
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@no", Request.QueryString["no"]);
            param[1] = new SqlParameter("@effectID", dropEffect.SelectedItem.Value);
            param[2] = new SqlParameter("@msg", txtMsg.Text.Trim());
            param[3] = new SqlParameter("@fileName", _fname);
            param[4] = new SqlParameter("@filePath", _fpath);
            param[5] = new SqlParameter("@uID", Session["uID"].ToString());
            param[6] = new SqlParameter("@uName", Session["uName"].ToString());
            param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[7].Direction = System.Data.ParameterDirection.Output;
            param[8] = new SqlParameter("@isValid", radYes.Checked);
            param[9] = new SqlParameter("@retMail", SqlDbType.Bit);
            param[9].Direction = System.Data.ParameterDirection.Output;
            param[10] = new SqlParameter("@MailTo", SqlDbType.NVarChar,2000);
            param[10].Direction = System.Data.ParameterDirection.Output;
            param[11] = new SqlParameter("@MailBody", SqlDbType.NVarChar, 2000);
            param[11].Direction = System.Data.ParameterDirection.Output;
            param[12] = new SqlParameter("@MailSubject", SqlDbType.NVarChar, 2000);
            param[12].Direction = System.Data.ParameterDirection.Output;
            param[13] = new SqlParameter("@isAboutSafety", isAboutSafety);
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);
             //$('BODY', parent.parent.parent)location.href = $('BODY', parent.parent.parent.document).URL
            if (Convert.ToBoolean(param[9].Value))
            {
                #region 发送邮件
                string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string to = param[10].Value.ToString();
                string copy = "";
                string subject = param[12].Value.ToString();//"TCP ECN (产品变更通知书)";
                string body = "";
                #region 写Body
                body = param[11].Value.ToString();
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
            if (Convert.ToBoolean(param[7].Value))
            {
                this.ltlAlert.Text = " alert('Success'); var loc=$('body', parent.document).find('#ifrm_560000440')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
            }
            else
            {
                this.ltlAlert.Text = "alert('fail! Please contact the administrator');";
            }
        }
        catch
        {
            this.ltlAlert.Text = "alert('fail! Please contact the administrator');";
        }
    }

    protected bool UpmessageFile()
    {
        string strUserFileName = file1.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/ECN/";
        string strCatFolder = Server.MapPath(catPath);

        string attachName = Path.GetFileNameWithoutExtension(file1.PostedFile.FileName);
        string attachExtension = Path.GetExtension(file1.PostedFile.FileName);
        string SaveFileName = DateTime.Now.ToFileTime().ToString() + attachExtension;
        if (File.Exists(strCatFolder + SaveFileName))
        {
            try
            {
                File.Delete(strCatFolder + SaveFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('fail to delete folder！')";

                return false;
            }
        }

        try
        {
            file1.PostedFile.SaveAs(strCatFolder + SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file');";

            return false;
        }

        try
        {
            File.Move(strCatFolder + SaveFileName, Server.MapPath(catPath + SaveFileName));
        }
        catch
        {
            ltlAlert.Text = "alert('fail to move file')";

            if (File.Exists(strCatFolder + SaveFileName))
            {
                try
                {
                    File.Delete(strCatFolder + SaveFileName);
                }
                catch
                {
                    ltlAlert.Text = "alert('fail to delete folder')";

                    return false;
                }
            }
            return false;
        }


        _fpath = catPath + SaveFileName;
        _fname = fileName;
        return true;
    }

    protected void dropEffect_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ChooseAboutSafety[dropEffect.SelectedValue.ToString()].ToString() == "True")
        {
            chkIsAboutSafety.Visible = true;
        }
        else
        {
            chkIsAboutSafety.Visible = false;
        }
    }
    protected void chkAllAuth_CheckedChanged(object sender, EventArgs e)
    {

        dropEffect.Items.Clear();
        ChooseAboutSafety.Clear();
        Dictionary<string, string> dic = new Dictionary<string, string>();
           DataTable dt = this.GetM5MstrEffectByUser(Session["uID"].ToString());
            DataTable dtEffect = dt;
            dropEffect.DataSource = dtEffect;
            dropEffect.DataBind();
           
            if (dtEffect.Rows.Count != 1)
            {
                dropEffect.Items.Insert(0, new ListItem("--", "0"));

            }
            else
            { 
                 foreach(DataRow gr in dt.Rows)
                 {
                     if (gr["m5e_isAboutSafety"].ToString() == "True")
                     {
                         chkIsAboutSafety.Visible = true;
                     }
                 }
            }

            foreach(DataRow gr in dt.Rows)
            {
                dic.Add(gr["m5e_id"].ToString(), gr["m5e_isAboutSafety"].ToString());
                if (gr["m5_isAboutSafety"].ToString().Equals("True"))
                {
                    chkIsAboutSafety.Checked = true;
                }
            }
            ChooseAboutSafety = dic;
    }

}