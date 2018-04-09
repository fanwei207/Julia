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

public partial class RDW_prod_AppDet : BasePage
{
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
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("170160", "新建跟踪申请，新建产品跟踪申请");
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNo.Enabled = false;

            txtNo.BackColor = System.Drawing.Color.LightGray;
            SqlDataReader reader = GetAppDet(Request["no"].ToString());
            if (reader.Read())
            {
                txtNo.Text = Convert.ToString(reader["prod_No"]);
                txtProjectName.Text = Convert.ToString(reader["prod_ProjectName"]);
                txtProdName.Text = Convert.ToString(reader["prod_ProductName"]);
                txtQAD.Text = Convert.ToString(reader["prod_QAD"]);
                txtPCB.Text = Convert.ToString(reader["prod_PCB"]);
                txtEndDate.Text = Convert.ToString(reader["prod_EndDate"]);
                if (Convert.ToString(reader["prod_PlanDate"]) == string.Empty || Convert.ToString(reader["prod_PlanDate"]) == "1900-01-01")
                {
                    txtPlanDate.Text = "";
                }
                else 
                { 
                    txtPlanDate.Text = Convert.ToString(reader["prod_PlanDate"]);
                }
            }
            reader.Close();
            if (Convert.ToInt32(Request["status"].ToString()) != 0 && Convert.ToInt32(Request["status"].ToString()) != 2)
            {
                btnSave.Enabled = false;
            }
            txtQAD.Enabled = false;
            txtQAD.BackColor = System.Drawing.Color.LightGray;
            txtPlanDate.Enabled = false;
            txtPlanDate.BackColor = System.Drawing.Color.LightGray;

            txtProjectName.Enabled = false;
            txtProjectName.BackColor = System.Drawing.Color.LightGray;
            txtProdName.Enabled = false;
            txtProdName.BackColor = System.Drawing.Color.LightGray;
            txtPCB.Enabled = false;
            txtPCB.BackColor = System.Drawing.Color.LightGray;
            txtEndDate.Enabled = false;
            txtEndDate.BackColor = System.Drawing.Color.LightGray;
            //权限 计划部
            if (this.Security["1701112"].isValid)
            {
                txtPlanDate.Enabled = true;
                txtPlanDate.BackColor = System.Drawing.Color.White;
            }
            //权限 技术部
            if (this.Security["1701111"].isValid)
            {
                txtProjectName.Enabled = true;
                txtProjectName.BackColor = System.Drawing.Color.White;
                txtProdName.Enabled = true;
                txtProdName.BackColor = System.Drawing.Color.White;
                txtPCB.Enabled = true;
                txtPCB.BackColor = System.Drawing.Color.White;
                txtEndDate.Enabled = true;
                txtEndDate.BackColor = System.Drawing.Color.White;
                txtQAD.Enabled = true;
                txtQAD.BackColor = System.Drawing.Color.White;
            }            
            Bind();
        }        
    }
    private void Bind()
    {
        DataTable dt = getAppDetList(Request["no"].ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    public SqlDataReader GetAppDet(string no)
    {
        SqlParameter param = new SqlParameter("@no", no);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_prod_GetAppDet", param);
    }
    private DataTable getAppDetList(string no)
    {
        SqlParameter []pram= new SqlParameter[1];
        pram[0] = new SqlParameter("@no", no);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_prod_getAppDetList", pram).Tables[0];
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("prod_AppList.aspx");
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["uName"].ToString() != Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["prod_CreateByName"]))
            {
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Gray;
                e.Row.Cells[4].Enabled = false;
            }
            if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == Request["status"].ToString())
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 0)
            {
                e.Row.Cells[0].Text = "申请单";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 1)
            {
                e.Row.Cells[0].Text = "通过申请单";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 2)
            {
                e.Row.Cells[0].Text = "未通过申请单";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 3)
            {
                e.Row.Cells[0].Text = "方案制定";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 4)
            {
                e.Row.Cells[0].Text = "方案审核";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 5)
            {
                e.Row.Cells[0].Text = "生产记录";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 6)
            {
                e.Row.Cells[0].Text = "次品记录";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 7)
            {
                e.Row.Cells[0].Text = "质检报告";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_FileType"]) == 8)
            {
                e.Row.Cells[0].Text = "分析报告";
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeleteFileList(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["prod_No"].ToString())))
        {
            Bind();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
    }
    private bool DeleteFileList(int no)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@no", no);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_prod_DeleteFileList", param));
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        { 
            
        }
        if (e.CommandName == "download")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string filePath = gv.DataKeys[index].Values["prod_FPath"].ToString();
            try
            {
                filePath = Server.MapPath(filePath);
                filePath = filePath.Replace("\\", "/");
            }
            catch (Exception)
            {

                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }

            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('申请号不能为空！');";
            return;
        }
        if (txtPCB.Text == string.Empty)
        {
            ltlAlert.Text = "alert('线路板不能为空！');";
            return;
        }
        if (txtEndDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('截止日期不能为空！');";
            return;
        }
        if (!checkProjectName(txtProjectName.Text))
        {
            ltlAlert.Text = "alert('项目名称不存在！');";
            return;
        }
        if (txtQAD.Text.Length != 14)
        {
            ltlAlert.Text = "alert('QAD号位数必须为14位！');";
            return;
        }
        if (Convert.ToInt32(Session["deptID"].ToString()) == 4 || Convert.ToInt32(Session["deptID"].ToString()) == 404)
        {
            if (updateAppDet(txtNo.Text, txtProjectName.Text, txtProdName.Text, txtQAD.Text, txtPCB.Text, Session["uID"].ToString(), Session["uName"].ToString(), "", "", txtPlanDate.Text, txtEndDate.Text, "1"))
            {
                ltlAlert.Text = "alert('保存成功！');";
                Bind();
            }
            else
            {
                ltlAlert.Text = "alert('数据库写入失败！')";
                return;
            }
        }
        else if (Convert.ToInt32(Session["deptID"].ToString()) == 197)
        { 
            //上传申请单
            _fpath = string.Empty;
            _fname = string.Empty;
            if (filename.Value.Trim() != string.Empty)
            {
                if (UpmessageFile())
                {
                    if (updateAppDet(txtNo.Text, txtProjectName.Text, txtProdName.Text, txtQAD.Text, txtPCB.Text, Session["uID"].ToString(), Session["uName"].ToString(), _fname, _fpath, txtPlanDate.Text, txtEndDate.Text,"2"))
                    {
                        ltlAlert.Text = "alert('保存成功！');";
                        Bind();
                    }
                    else
                    {
                        ltlAlert.Text = "alert('数据库写入失败！')";
                        return;
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('附件上传失败！')";
                    return;
                }
            }
            else 
            {
                ltlAlert.Text = "alert('上传附件不能为空！')";
                return;
            }
        }
    }
    private bool checkProjectName(string projectname)
    {
        SqlParameter pram = new SqlParameter("@projectname", projectname);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_prod_checkProjectName", pram));
    }
    private bool updateAppDet(string no, string projectname, string prodname, string qad, string pcb, string uid, string uname, string fname, string fpath, string plandate, string enddate,string status)
    {
        SqlParameter[] pram = new SqlParameter[12];
        pram[0] = new SqlParameter("@no", no);
        pram[1] = new SqlParameter("@projectname", projectname);
        pram[2] = new SqlParameter("@prodname", prodname);
        pram[3] = new SqlParameter("@qad", qad);
        pram[4] = new SqlParameter("@pcb", pcb);
        pram[5] = new SqlParameter("@uid", uid);
        pram[6] = new SqlParameter("@uname", uname);
        pram[7] = new SqlParameter("@fname", fname);
        pram[8] = new SqlParameter("@fpath", fpath);
        pram[9] = new SqlParameter("@plandate", plandate);
        pram[10] = new SqlParameter("@enddate", enddate);
        pram[11] = new SqlParameter("@status", status);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_prod_updateAppDet", pram));
    }
    protected bool UpmessageFile()
    {
        string strUserFileName = filename.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/PROD/";
        string strCatFolder = Server.MapPath(catPath);

        string attachName = Path.GetFileNameWithoutExtension(filename.PostedFile.FileName);
        string attachExtension = Path.GetExtension(filename.PostedFile.FileName);
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
            filename.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";

            return false;
        }
        string path = @"/TecDocs/PROD/";

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
}