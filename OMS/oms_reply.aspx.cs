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

public partial class OMS_oms_reply : System.Web.UI.Page
{
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
            string cust= Request.QueryString["custCode"];
            _pageindex = 0;
            _parentID = Request.QueryString["parentID"];
            string type = Request.QueryString["type"].Trim();
            if (type == "reply")
            {
                txtmstr.Enabled = false;
                txtmstr.Visible = false;
                lblmatr.Visible = false;
            }
         
        }
    }
   
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        _fpath = string.Empty;
        _fname = string.Empty;
        if (txtmstr.Enabled == true && txtmstr.Text.Trim() == string.Empty)
        {
            
            ltlAlert.Text = "alert('Subject is required')";
            return;
        }
        else if (txtmstr.Enabled == true && txtmstr.Text.Length > 150)
        {
            ltlAlert.Text = "alert('Subject must less than 150 words')";
            return;
        }
        if (txt_tracking.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Message is required！')";
            return;
        }
        else if (file1.Value.Trim() != string.Empty)
        {
            UpmessageFile();
        }

        if (txtmstr.Enabled == true)
        {
            if (!OMSHelper.insertTaskMessage(Session["uID"].ToString(), Session["uName"].ToString(), txtmstr.Text.Trim(), _fpath, _fname, _parentID, txt_tracking.Text,Request.QueryString["custCode"]))
            {
                ltlAlert.Text = "alert('Your message failed！')";
            }
            else
            {
                txt_tracking.Text = string.Empty;
                ltlAlert.Text = "window.close();";

            }
        }
        else
        {
            if (!OMSHelper.insertTaskMessage(Session["uID"].ToString(), Session["uName"].ToString(), txt_tracking.Text.Trim(), _fpath, _fname, _parentID, txt_tracking.Text.Trim(),Request.QueryString["custCode"]))
            {
                ltlAlert.Text = "alert('fail to attach file')";
            }
            else
            {
                txt_tracking.Text = string.Empty;
                ltlAlert.Text = "window.close();";

            }
        }
    }

    protected bool UpmessageFile()
    {
        string strUserFileName = file1.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/OMS/message/";
        string strCatFolder = Server.MapPath(catPath);

        string attachName = Path.GetFileNameWithoutExtension(file1.PostedFile.FileName);
        string attachExtension = Path.GetExtension(file1.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../Temp/"), DateTime.Now.ToFileTime().ToString() + "-" + attachName);//合并两个路径为上传到服务器上的全路径
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

        string path = @"/TecDocs/OMS/message/";

        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        string docid = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "-" + attachName + attachExtension;
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


        _fpath = strCatFolder + docid;
        _fname = fileName;
        return true;
    }

   
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("oms_mstr.aspx?custCode=" + lbCustCode.Text + "&custName=" + lbCustName.Text + "&custCode=" + txtCustomer.Text + "&index="+4+" &rt=" + DateTime.Now.ToFileTime().ToString());
    }
}