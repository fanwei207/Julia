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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;
using RD_WorkFlow;
using CommClass;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
//using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using CommClass;
using System.Net.Mail;

public partial class Purchase_rp_purchaseMessage : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
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
            string dept = Request["dept"].ToString();
            hidID.Value = Request["ID"].ToString();
            labNo.Text = Request["No"].ToString();
        }
    }
    protected void btnSaveMsg_Click(object sender, EventArgs e)
    {
        Boolean msg = false;
        string massage = string.Empty;
        string dateMsg = string.Empty; // 写入留言
        string dateMassage = string.Empty; // 写入消息
        _fpath = string.Empty;
        _fname = string.Empty;
        string planDate = string.Empty;
        bool isSendEmail = false;
        #region 保存留言
        if (txtMsg.Text == string.Empty)
        {
            if (filename.Value.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('未做更改，无须保存！');";
                return;
            }
            else
            {
                if (UpmessageFile(filename))
                {
                    if (saveMassage(labNo.Text, hidID.Value.ToString(), Request["dept"].ToString(), txtMsg.Text, _fname, _fpath))
                    {
                        ltlAlert.Text = "alert('保存成功！')";
                        return;
                    }
                    else
                    {
                        ltlAlert.Text = "alert('保存失败！')";
                        return;
                    }
                }
            }
            ltlAlert.Text = "alert('留言不能为空！');";
            return;
        }
        else
        {
            massage = Session["eName"].ToString() + " 留言：" + txtMsg.Text;
            msg = true;
        }
        if (msg)
        {
            if (filename.Value.Trim() != string.Empty)
            {
                if (!UpmessageFile(filename))
                {
                    ltlAlert.Text = "alert('附件上传失败！')";
                    return;
                }
            }
            //保存留言
            if (!saveMassage(labNo.Text, hidID.Value.ToString(), Request["dept"].ToString(), txtMsg.Text, _fname, _fpath))
            {
                ltlAlert.Text = "alert('留言保存失败，请联系管理员！');";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('留言保存成功！');";

                return;
            }

        }
        #endregion
    }
    /// <summary>
    /// 保存留言
    /// </summary>
    private bool saveMassage(string no, string id, string dept, string massage, string fname, string fpath)
    {
        string str = "sp_rp_saveMassage";
        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@id", id);
        param[2] = new SqlParameter("@dept", dept);
        param[3] = new SqlParameter("@massage", massage);
        param[4] = new SqlParameter("@uID", Session["uID"].ToString());
        param[5] = new SqlParameter("@uName", Session["uName"].ToString());
        param[6] = new SqlParameter("@fname", fname);
        param[7] = new SqlParameter("@fpath", fpath);
        param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[8].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[8].Value);
    }
    
    /// <summary>
    /// 上传文件
    /// </summary>
    protected bool UpmessageFile(HtmlInputFile fileID)
    {
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        
        string strUserFileName = fileID.PostedFile.FileName;//是获取文件的路径，即FileUpload控件文本框中的所有内容，
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string attachExtension = Path.GetExtension(fileID.PostedFile.FileName);
        string _newFileName = DateTime.Now.ToFileTime().ToString() + attachExtension;

        string catPath = @"/TecDocs/RP/";
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
}