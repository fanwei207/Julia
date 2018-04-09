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
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;
using RD_WorkFlow;


public partial class product_m5_appNew : System.Web.UI.Page
{
    RDW rdw = new RDW();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
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
            txtNum.Text = Request["no"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('跟踪号不能为空！');";
            return;
        }
        if (txtQAD.Text.Length != 14)
        {
            ltlAlert.Text = "alert('QAD号位数必须为14位！');";
            return;
        }
        if (txtPCB.Text == string.Empty)
        {
            ltlAlert.Text = "alert('线路板不能为空！');";
            return;
        }
        if (txtEndDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('截止时间不能为空！');";
            return;
        }
        if (filename.Value.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('附件不能为空！');";
            return;
        }

        _fpath = string.Empty;
        _fname = string.Empty;
        if (checkNo(txtNo.Text))
        {
            ltlAlert.Text = "alert('跟踪号已存在！');";
            return;
        }
        else
        {
            if (filename.Value.Trim() != string.Empty)
            {
                if (UpmessageFile(filename))
                {
                    if (addNewApp(txtNo.Text, txtNum.Text, txtQAD.Text, txtPCB.Text, Session["uID"].ToString(), Session["uName"].ToString(), _fname, _fpath, txtEndDate.Text))
                    {
                        ltlAlert.Text = "alert('新建试流单成功！');";
                        return;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 判断跟踪号是否存在
    /// </summary>
    /// <param name="no"></param>
    /// <returns></returns>
    private bool checkNo(string no)
    {
        SqlParameter pram = new SqlParameter("@no", no);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_prod_checkNo", pram));
    }
    /// <summary>
    /// 上传附件
    /// </summary>
    /// <param name="fileID"></param>
    /// <returns></returns>
    protected bool UpmessageFile(HtmlInputFile fileID)
    {
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        string _stepID = Convert.ToString(Request.QueryString["did"]);

        string strUserFileName = fileID.PostedFile.FileName;//是获取文件的路径，即FileUpload控件文本框中的所有内容，
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string attachExtension = Path.GetExtension(fileID.PostedFile.FileName);
        string _newFileName = DateTime.Now.ToFileTime().ToString() + attachExtension;

        string catPath = @"/TecDocs/ProjectTracking/" + Request["did"] + "/";
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
            if (rdw.UploadFile(_fileName, _newFileName, _uID, _uName, _stepID))
            {

            }
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
    /// <summary>
    /// 新建试流单
    /// </summary>
    /// <returns></returns>
    private bool addNewApp(string no, string num, string qad, string pcb, string uid, string uname, string fname, string fpath, string enddate)
    {
        SqlParameter[] pram = new SqlParameter[9];
        pram[0] = new SqlParameter("@no", no);
        pram[1] = new SqlParameter("@num", num);
        pram[2] = new SqlParameter("@qad", qad);
        pram[3] = new SqlParameter("@pcb", pcb);
        pram[4] = new SqlParameter("@uid", uid);
        pram[5] = new SqlParameter("@uname", uname);
        pram[6] = new SqlParameter("@fname", fname);
        pram[7] = new SqlParameter("@fpath", fpath);
        pram[8] = new SqlParameter("@enddate", enddate);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_m5_addNewApp", pram));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/Prod_Report.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "name=" + txtNum.Text);
    }
}