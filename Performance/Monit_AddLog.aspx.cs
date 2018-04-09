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
using System.Text;
using System.Net.Mail;
using System.IO;

public partial class Performance_Monit_AddLog : BasePage
{
    Monitor monitor = new Monitor();
    static private int picCount=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            #region 设置默认值
            //默认ID=0
            hidLogID.Value = "0";
            //修改：有传回cmd参数的，就表示修改
            chkIsModify.Checked = Request.QueryString["isModify"] == null ? false : true;
            txt_actualDate.Text = DateTime.Now.ToString();
            #endregion

            #region 修改Log
            if(Request.QueryString["id"]!=null)
            {
                btn_Back.Visible = true;
                btnContinue.Visible = false;
                hidLogID.Value = Request.QueryString["id"];
                DataTable dt = monitor.SelectLogByID(hidLogID.Value);
                txt_log.Text = dt.Rows[0]["Monit_Content"].ToString();
                ddl_plant.SelectedIndex = -1;
                ddl_plant.Items.FindByValue(dt.Rows[0]["Plant"].ToString()).Selected=true;
                BindMID();
                ddl_mID.SelectedValue = dt.Rows[0]["Monit_mID"].ToString();
                txt_beltline.Text = dt.Rows[0]["Monit_Beltline"].ToString();
                txt_actualDate.Text = dt.Rows[0]["Monit_ActualDate"].ToString();
                BindArea();
                ddl_area.Items.FindByText(dt.Rows[0]["Monit_Area"].ToString()).Selected = true;
                txt_beltline.Visible = true;
                ddl_area.Visible = true;
                ddl_falg.Items.FindByValue(dt.Rows[0]["Monit_Flag"].ToString()).Selected = true;
                if (Request.QueryString["isModify"] == "0") btnUpload.Enabled = false;
                GridViewBind();
            }
            #endregion
            else
            {
                //选中当前域
                ddl_plant.SelectedIndex = -1;
                ddl_plant.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
                //绑定摄像头编号
                BindMID();
                ddl_mID.Items.FindByValue("--").Selected = true;
                if(ddl_mID.SelectedValue!="--")
                {
                    GetBeltlineArea();
                }
                
            }
            
           
        }
    }
    
    #region 上传附件
    //附件明细初始化
    protected int GridViewBind()
    {
        int count=0;
        if(hidLogID.Value!="0")
        {
            gv.DataSource = monitor.SelectPicByID(hidLogID.Value,out count);
            gv.DataBind();
            
        }
        return count;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //首先要先保存记录,
        SaveWithoutLog();
        
        //再上传照片
        if (FileUpload2.Value.Trim() != string.Empty)
        {
            try
            {
                string _filePath = "";
                string _fileName = "";
                if (!UploadFile(ref _filePath, ref _fileName))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }

                int LogID = Convert.ToInt32(hidLogID.Value);

                if (!monitor.InsertMonitPic(LogID, _fileName, _filePath, Session["uID"].ToString()))
                {
                    ltlAlert.Text = "alert('上传失败，不可重复上传文件!')";
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!')";
            }
            
            
        }
        else
        {
            //当修改LOG时，一定会有照片，所以不用上传文件；新增LOG时必须有照片
            if (!chkIsModify.Checked && ddl_falg.SelectedValue == "2")
            this.ltlAlert.Text = "alert('请选择一个文件！必须上传截图！');";
        }
        //返回截图数量
        picCount = GridViewBind();
        if (picCount <= 0 && ddl_falg.SelectedValue=="2")
        {
            ltlAlert.Text = "alert('必须上传截图!')";
            return;
        }
    }
    private bool UploadFile(ref string filePath, ref string fileName)
    {
        string strUserFileName = FileUpload2.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/Monitor/";
        string strCatFolder = Server.MapPath(catPath);

        string attachName = Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
        string attachExtension = Path.GetExtension(FileUpload2.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../images/"), DateTime.Now.ToFileTime().ToString() + attachExtension);//合并两个路径为上传到服务器上的全路径
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
        string path = @"/TecDocs/Monitor/";

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


        filePath = catPath + docid;
        fileName = _fileName;
        return true;
    }
    //删除附件
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gv.DataKeys[e.RowIndex].Values[0].ToString();
        string AllPath = monitor.SelectPicAllPath(id);
        AllPath = AllPath.Replace('/', '\\');
        AllPath = Server.MapPath(AllPath);
        File.Delete(AllPath);
        monitor.DeletePic(id,Session["uID"].ToString());
        picCount = GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //无权限修改
            if (Request.QueryString["isModify"] == "0") e.Row.Cells[2].Enabled = false;
        }
    }
    //下载附件
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string url = gv.DataKeys[intRow].Values["Monit_AllPath"].ToString();
            ltlAlert.Text = "var w=window.open(' " + url + "'); w.focus();";
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    #endregion
    protected void SaveWithoutLog()
    {
        #region 必填
        if (ddl_plant.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('公司必选!')";
            return;
        }
        if (ddl_mID.SelectedValue == "--")
        {
            ltlAlert.Text = "alert('摄像头编号必选!')";
            return;
        }
        if (ddl_falg.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('类型必选!')";
            return;
        }
        if (txt_log.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('日志内容必填!')";
            return;
        }
        #endregion
        #region 修改
        //新增时hidLogID.Value="0",x修改时为LogID
        if (Convert.ToInt32(hidLogID.Value) > 0)//
        {
            monitor.ModifyLog(hidLogID.Value, ddl_mID.SelectedValue, ddl_plant.SelectedItem.ToString(), txt_log.Text.Trim(), txt_actualDate.Text.Trim(), ddl_falg.SelectedValue, ddl_area.SelectedItem.ToString(), txt_beltline.Text, Session["uID"].ToString());
        }
        #endregion
        #region 新增
        else
        {
            int LogID = monitor.AddLog(ddl_plant.SelectedItem.ToString(), ddl_mID.SelectedValue, txt_log.Text.Trim(),txt_actualDate.Text.Trim(),ddl_falg.SelectedValue,ddl_area.SelectedItem.ToString(),txt_beltline.Text, Session["uID"].ToString());
            if (LogID > 0) hidLogID.Value = LogID.ToString();
        }
        #endregion
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (chkIsModify.Checked)
        {
            this.Redirect("Monit_LogList.aspx");
        }
        else
        {
            this.Redirect("Monit_AddLog.aspx");
        }
    }
    protected void ddl_plant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMID();
        ddl_mID.Items.FindByValue("--").Selected = true;
    }
    
    protected void BindMID()
    {
        ddl_mID.DataSource = monitor.GetMIDByPlantID(ddl_plant.SelectedValue);
        ddl_mID.DataBind();
        ddl_mID.Items.Add(new ListItem("--", "--"));
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Monit_LogList.aspx");
    }
    protected void ddl_mID_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindArea();
        GetBeltlineArea();
    }
    protected void BindArea()
    {
        this.ddl_area.Items.Clear();
        ddl_area.DataSource = monitor.BindArea(ddl_plant.SelectedItem.ToString());
        ddl_area.DataBind();
    }
    protected void GetBeltlineArea()
    {
        DataTable dt = monitor.SelectMonitorByID(ddl_mID.SelectedValue);
        if(dt.Rows.Count>0)
        {
            txt_beltline.Text = dt.Rows[0]["Monit_Beltline"].ToString();
            ddl_area.SelectedValue = dt.Rows[0]["Monit_AreaID"].ToString();
            txt_beltline.Visible = true;
            ddl_area.Visible = true;
        }
        
    }

}