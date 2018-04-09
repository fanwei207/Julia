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
using QADSID;

public partial class SID_SID_UpPicture : System.Web.UI.Page
{
    SID sid = new SID();
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
            lblshipid.Text = Request.QueryString["shipid"];
            
           lblsid.Text = Request.QueryString["sid"];
           gvBindData(lblshipid.Text);
           //lblsid.Visible = true;
        }
    }
    protected void gvBindData(string sidid)
    {
        //定义参数

        DataSet ds = SelectSIDListpictureship(sidid);
       
        gvSID.DataSource = ds.Tables[0];
        gvSID.DataBind();
        gv.DataSource = ds.Tables[1];
        gv.DataBind();
       
    }
    public DataSet SelectSIDListpictureship(string sid)
    {
        adamClass adam = new adamClass();
        try
        {
            string strSql = "sp_sid_SelectpictureshipidTemp";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@sid", sid);


            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);


        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        _fpath = string.Empty;
        _fname = string.Empty;
        if (file1.Value.Trim() != string.Empty)
        {
            if (UpmessageFile())
            {
                if (sid.insertPictureurl(lblshipid.Text, _fname, _fpath, txt_tracking.Text, Session["uID"].ToString()))
                {
                    txt_tracking.Text = string.Empty;
                     ltlAlert.Text = "alert('图片上传成功！')";
                     gvBindData(lblshipid.Text);
                }
                else
                {
                    ltlAlert.Text = "alert('数据库写入失败！')";
                }

            }
            else
            {
                ltlAlert.Text = "alert('图片上传失败！')";
            }

        }
        else
        {
            ltlAlert.Text = "alert('上传文件不能为空！')";
        }


    }
    protected bool UpmessageFile()
    {
        string strUserFileName = file1.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/SID/Picture/";
        string strCatFolder = Server.MapPath(catPath);

        string attachName = Path.GetFileNameWithoutExtension(file1.PostedFile.FileName);
        string attachExtension = Path.GetExtension(file1.PostedFile.FileName);
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
            file1.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";

            return false;
        }

        string path = @"/TecDocs/SID/Picture/";

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


    protected void btn_back_Click(object sender, EventArgs e)
    {
        string[] strsid;
        strsid = lblsid.Text.Split(',');
        Response.Redirect("SID_ShowPicture.aspx?from=new&sid=" + strsid[0] + "&rt=" + DateTime.Now.ToFileTime().ToString());
    }
   
}