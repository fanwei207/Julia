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
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Net.Mail;

public partial class m5_valid_det : System.Web.UI.Page
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
            #region 绑定dropProject
            dropValid.DataSource = this.GetM5MstrValidByUser(Session["uID"].ToString());
            dropValid.DataBind();
            dropValid.Items.Insert(0, new ListItem("--", "0"));
            #endregion
         
        }
    }
    public DataTable GetM5MstrValidByUser(string uID)
    {
        try
        {
            string strSql = "sp_m5_selectM5ValidByUser";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);
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

        if (dropValid.SelectedIndex == 0)
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

        try
        {
            string strSql = "sp_m5_saveM5Valid";
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@no", Request.QueryString["no"]);
            param[1] = new SqlParameter("@validID", dropValid.SelectedValue);
            param[2] = new SqlParameter("@msg", txtMsg.Text.Trim());
            param[3] = new SqlParameter("@fileName", _fname);
            param[4] = new SqlParameter("@filePath", _fpath);
            param[5] = new SqlParameter("@uID", Session["uID"].ToString());
            param[6] = new SqlParameter("@uName", Session["uName"].ToString());
            param[8] = new SqlParameter("@isValid", radYes.Checked);
            param[9] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[9].Direction = System.Data.ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

            if (Convert.ToBoolean(param[9].Value))
            {
                this.ltlAlert.Text = "alert('Success'); window.close();";
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
}