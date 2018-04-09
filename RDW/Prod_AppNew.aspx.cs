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

public partial class RDW_Prod_AppNew : BasePage
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
            
            string strSQL = " SELECT typename,typeid FROM dbo.prod_type ";
            ddltype.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);
            ddltype.DataBind();
            ddltype.Items.Insert(0,"--请选择--");

            if (Request.QueryString["from"] != null)
            {
                txtPlanDate.Enabled = false;

                this.hidProjName.Value = Request.QueryString["name"];
                this.hidCode.Value = Request.QueryString["code"];
                if (Request.QueryString["no"] != null)
                {
                    txtNo.Text = Request.QueryString["no"];
                    this.hidNo.Value = Request.QueryString["no"];
                }
                if (Request.QueryString["qad"] != null)
                {
                    txtQAD.Text = Request.QueryString["qad"];
                    this.hidQAD.Value = Request.QueryString["qad"];
                }
                if (Request.QueryString["pcb"] != null)
                {
                    txtPCB.Text = Request.QueryString["pcb"];
                    this.hidPCB.Value = Request.QueryString["pcb"];
                }
                if (Request.QueryString["planDate"] != null)
                {
                    txtPlanDate.Text = Request.QueryString["planDate"];
                    this.hidPlanDate.Value = Request.QueryString["planDate"];
                }
                if (Request.QueryString["endDate"] != null)
                {
                    txtEndDate.Text = Request.QueryString["endDate"];
                    this.hidEndDate.Value = Request.QueryString["endDate"];
                }
                txtProjName.Text = Request.QueryString["name"];
                txtCode.Text = Request.QueryString["code"];


                txtNo.Enabled = false;
                txtNo.BackColor = System.Drawing.Color.LightGray;
                txtPCB.Enabled = false;
                txtPCB.BackColor = System.Drawing.Color.LightGray;
                txtEndDate.Enabled = false;
                txtEndDate.BackColor = System.Drawing.Color.LightGray;
                txtQAD.Enabled = false;
                txtQAD.BackColor = System.Drawing.Color.LightGray;
                DataTable dt = getProdMstr(Request.QueryString["projectName"], Request.QueryString["code"]);
                if (dt != null && dt.Rows.Count > 0)  //如果已经存在记录，则此操作时编辑修改
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        this.hidQAD.Value = row["prod_QAD"].ToString();
                        this.hidPCB.Value = row["prod_PCB"].ToString();
                        this.hidPlanDate.Value = row["prod_PlanDate"].ToString();
                        this.hidEndDate.Value = row["prod_EndDate"].ToString();

                        txtProjName.Text = Request.QueryString["name"];
                        txtCode.Text = Request.QueryString["code"];
                        txtQAD.Text = this.hidQAD.Value;
                        txtNo.Text = row["prod_No"].ToString();
                        txtQAD.Text = row["prod_QAD"].ToString();
                        txtPCB.Text = this.hidPCB.Value;
                        txtEndDate.Text = this.hidEndDate.Value;
                        txtPlanDate.Text = this.hidPlanDate.Value;
                    }
                    //跟踪号不允许修改
                    txtNo.Enabled = false;
                    txtNo.BackColor = System.Drawing.Color.LightGray;

                    //权限 技术部
                    if (this.Security["1701111"].isValid)
                    {
                        txtPCB.Enabled = true;
                        txtPCB.BackColor = System.Drawing.Color.White;
                        txtEndDate.Enabled = true;
                        txtEndDate.BackColor = System.Drawing.Color.White;
                        txtQAD.Enabled = true;
                        txtQAD.BackColor = System.Drawing.Color.White;
                    }
                }
                else //新增
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        this.hidQAD.Value = row["prod_QAD"].ToString();
                        this.hidPCB.Value = row["prod_PCB"].ToString();
                        this.hidPlanDate.Value = row["prod_PlanDate"].ToString();
                        this.hidEndDate.Value = row["prod_EndDate"].ToString();
                    }
                    txtProjName.Text = Request.QueryString["name"];
                    txtCode.Text = Request.QueryString["code"];

                    //权限 技术部
                    if (this.Security["1701111"].isValid)
                    {
                        txtPCB.Enabled = true;
                        txtPCB.BackColor = System.Drawing.Color.White;
                        txtEndDate.Enabled = true;
                        txtEndDate.BackColor = System.Drawing.Color.White;
                        txtQAD.Enabled = true;
                        txtQAD.BackColor = System.Drawing.Color.White;
                        txtNo.Enabled = true;
                        txtNo.BackColor = System.Drawing.Color.White;
                    }
                }
            }
            else
            {
                tdNew.Visible = false;
            }
            if (Request.QueryString["no"] != null)
            {
                this.hidQAD.Value = Request.QueryString["qad"];
                this.hidPCB.Value = Request.QueryString["pcb"];
                this.hidPlanDate.Value = Request.QueryString["planDate"];
                this.hidEndDate.Value = Request.QueryString["endDate"];

                txtNo.Enabled = false;
                txtNo.Text = Request.QueryString["no"];
                txtProjName.Text = Request.QueryString["name"];
                txtCode.Text = Request.QueryString["code"];
                txtQAD.Text = Request.QueryString["qad"];
                txtPCB.Text = Request.QueryString["pcb"];
                txtPlanDate.Text = Request.QueryString["planDate"];
                txtEndDate.Text = Request.QueryString["endDate"];
            }
            //步骤结束后，不可编辑  step 所有人都完成时不允许修改、新增
            RDW_Detail rd = rdw.SelectRDWDetailEdit(Request["did"], false);
            btnSave.Enabled = rd.RDW_Status != 2;
            //试流单取消后也不可编辑
            if (Request.QueryString["no"] != null)
            {
                if (Request["status"].ToString() == "5")
                {
                    btnSave.Visible = false;
                    txtQAD.Enabled = false;
                    txtPCB.Enabled = false;
                    txtEndDate.Enabled = false;

                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/Prod_Report.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "name=" + this.hidProjName.Value.ToString() + "&code=" + this.hidCode.Value.ToString()
            + "&mid=" + Request["mid"] + "&did=" + Request["did"]);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string massage = string.Empty;
        if (txtNo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('跟踪号不能为空！');";
            return;
        }
        if (ddltype.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择类型！');";
            return;
        }
        if (Request.QueryString["from"] != null)
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('项目代码不能为空！');";
                return;
            }
            if (txtProjName.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('项目名称不能为空！');";
                return;
            }

        }
        if (txtPCB.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('线路板不能为空！');";
            return;
        }
        if (txtEndDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('截止时间不能为空！');";
            return;
        }
        if (!checkProjectName(txtCode.Text.Trim()))
        {
            ltlAlert.Text = "alert('项目代码不存在！');";
            return;
        }
        if (txtQAD.Text.Trim().Length != 14)
        {
            ltlAlert.Text = "alert('QAD号位数必须为14位！');";
            return;
        }
        string type = ddltype.SelectedItem.Text.Trim();
        if (type == "全部")
        {
            type = "";
        }
        //如果跟踪单号存在，则认为是编辑、修改，允许多个附件上传
        if (checkNo(txtNo.Text.Trim()))
        {
            bool bMassage = false;
           
            
            if (txtEndDate.Enabled == true)
            {
                massage = Session["eName"].ToString() + "将";
                if (this.hidQAD.Value.ToString() != txtQAD.Text.Trim())
                {
                    massage = massage + "  QAD " + this.hidQAD.Value.ToString() + "  改为 " + txtQAD.Text.Trim() + "; ";
                    bMassage = true;
                } if (this.hidPCB.Value.ToString() != txtPCB.Text.Trim())
                {
                    massage = massage + "  线路板 " + this.hidPCB.Value.ToString() + "  改为 " + txtPCB.Text.Trim() + "; ";
                    bMassage = true;
                }
                if (this.hidEndDate.Value.ToString() != txtEndDate.Text.Trim())
                {
                    massage = massage + "  截止日期 " + this.hidEndDate.Value.ToString() + "  改为 " + txtEndDate.Text.Trim() + "; ";
                    bMassage = true;
                }
                massage = massage + " 跟踪单： " + txtNo.Text.Trim();
                
                if (bMassage)
                {
                    if (!insertMassage(Request.QueryString["mid"], Request.QueryString["did"], massage, Session["uID"].ToString(), Session["uName"].ToString()))
                    {
                        ltlAlert.Text = "alert('消息保存失败，请联系管理员！');";
                        return;
                    }
                    else
                    {
                        if (updateNewApp(txtNo.Text.Trim(), txtProjName.Text.Trim(), txtCode.Text.Trim(), txtQAD.Text.Trim(), txtPCB.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), txtPlanDate.Text.Trim(), txtEndDate.Text.Trim(), type))
                        {
                            ltlAlert.Text = "alert('更新成功');";
                            this.hidPCB.Value = txtPCB.Text.Trim();
                            this.hidQAD.Value = txtQAD.Text.Trim();
                            this.hidPlanDate.Value = txtPlanDate.Text.Trim();
                            this.hidEndDate.Value = txtEndDate.Text.Trim();

                        }
                    }
                }
            }
            if (filename.Value.Trim() != string.Empty)
            {
                bMassage = true;
                if (UpmessageFile(filename))
                {
                    //保存附件的名称和路径
                    if (saveFileNameAndPath(txtNo.Text.Trim(), txtCode.Text.Trim(), txtProjName.Text.Trim(), "", _fname, _fpath))
                    {
                        ltlAlert.Text = "alert('附件上传成功！')";
                        return;
                    }
                }
            }
            if (!bMassage)
            {
                ltlAlert.Text = "alert('未做变更，无需保存！');";
                return;
            }
            return;
        }
        
        //上传申请单
        _fpath = string.Empty;
        _fname = string.Empty;
        if (filename.Value.Trim() != string.Empty)
        {
            if (UpmessageFile(filename))
            {
                if (addNewApp(txtNo.Text.Trim(), txtProjName.Text.Trim(), txtCode.Text.Trim(), txtQAD.Text.Trim(), txtPCB.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), _fname, _fpath, txtEndDate.Text.Trim(), Request["mid"], Request["did"], type))
                {
                    if (this.hidPCB.Value.ToString() == string.Empty)
                    {
                        massage = Session["eName"].ToString() + " 新建跟踪单--跟踪单号：" + txtNo.Text.Trim() + ",--项目代码 : " + txtCode.Text.Trim() + ",--项目名称 : " + txtProjName.Text.Trim();
                    }
                    if(massage != string.Empty)
                    {
                        if (Request["mid"] != null)
                        {
                            if (!insertMassage(Request.QueryString["mid"], Request.QueryString["did"], massage, Session["uID"].ToString(), Session["uName"].ToString()))
                            {
                                ltlAlert.Text = "alert('消息保存失败，请联系管理员！');";
                                return;
                            }
                        }
                    }
                    if (saveFileNameAndPath(txtNo.Text.Trim(), txtCode.Text.Trim(), txtProjName.Text.Trim(), "", _fname, _fpath)) 
                    {
                        ltlAlert.Text = "alert('保存成功！');";
                        this.hidPCB.Value = txtPCB.Text.Trim();
                        this.hidQAD.Value = txtQAD.Text.Trim();
                        this.hidPlanDate.Value = txtPlanDate.Text.Trim();
                        this.hidEndDate.Value = txtEndDate.Text.Trim();
                        return;
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('数据库写入失败！')";
                    return;
                }
            }
            else
            {
                ltlAlert.Text = "alert('文件上传失败！')";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('上传文件不能为空！')";
            return;
        }        
    }

    private bool checkProjectName(string projectname)
    {
        SqlParameter pram = new SqlParameter("@projectname", projectname);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_prod_checkProjectName", pram));
    }
    private bool checkNo(string no)
    {
        SqlParameter pram = new SqlParameter("@no", no);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_prod_checkNo", pram));
    }
    private bool addNewApp(string no,string projectname,string code,string qad,string pcb,string uid,string uname,string fname,string fpath,string enddate,string mid, string did ,string type)
    { 
        SqlParameter []pram = new SqlParameter[14];
        pram[0] = new SqlParameter("@no",no);
        pram[1] = new SqlParameter("@projectname",projectname);
        pram[2] = new SqlParameter("@code", code);
        pram[3] = new SqlParameter("@qad",qad);
        pram[4] = new SqlParameter("@pcb", pcb);
        pram[5] = new SqlParameter("@uid", uid);
        pram[6] = new SqlParameter("@uname", uname);
        pram[7] = new SqlParameter("@fname", fname);
        pram[8] = new SqlParameter("@fpath", fpath);
        pram[9] = new SqlParameter("@enddate", enddate);
        pram[10] = new SqlParameter("@mid", mid);
        pram[11] = new SqlParameter("@did", did);
        pram[12] = new SqlParameter("@type", type);
        pram[13] = new SqlParameter("@typeid", ddltype.SelectedValue.ToString());
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_prod_addNewApp", pram));
    }

    /// <summary>
    /// 上传申请单
    /// </summary>
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

    private DataTable getProdMstr(string productName, string code)
    {
        string sql = "select  *  from prod_mstr where prod_ProjectName = '" + productName + "' and prod_Code = '" + code + "'";
        return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql).Tables[0];        
    }
    private bool insertMassage(string mid,string did,string massage,string uID, string uName)
    {
        try
        {
            string str = "sp_prod_insertProdMassage";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@mid", mid);
            param[1] = new SqlParameter("@did", did);
            param[2] = new SqlParameter("@massage", massage);
            param[3] = new SqlParameter("@uID", uID);
            param[4] = new SqlParameter("@uName", uName);
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
            return Convert.ToBoolean(param[5].Value);
        }
        catch 
        {
            return false;
        }
    }

    private bool updateNewApp(string no, string projectname, string code, string qad, string pcb, string uid, string uname, string plandate, string enddate,  string typename)
    {
        if (typename == "--")
        {
            typename = "";
        }
        SqlParameter[] pram = new SqlParameter[13];
        pram[0] = new SqlParameter("@no", no);
        pram[1] = new SqlParameter("@projectname", projectname);
        pram[2] = new SqlParameter("@code", code);
        pram[3] = new SqlParameter("@qad", qad);
        pram[4] = new SqlParameter("@pcb", pcb);
        pram[5] = new SqlParameter("@uid", uid);
        pram[6] = new SqlParameter("@uname", uname);
        pram[7] = new SqlParameter("@plandate", plandate);
        pram[8] = new SqlParameter("@enddate", enddate);
        pram[9] = new SqlParameter("@retValue", SqlDbType.Bit);
        pram[9].Direction = ParameterDirection.Output;
        pram[10] = new SqlParameter("@typeid", ddltype.SelectedValue.ToString());
        pram[11] = new SqlParameter("@typename", typename);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_prod_updateNewApp", pram);
        return Convert.ToBoolean(pram[9].Value);
    }
    
    private bool saveFileNameAndPath(string no, string code, string proj, string massage, string fname, string fpath)
    {
        string str = "sp_prod_saveMassage";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@code", code);
        param[2] = new SqlParameter("@proj", proj);
        param[3] = new SqlParameter("@massage", massage);
        param[4] = new SqlParameter("@dept", "prod");
        param[5] = new SqlParameter("@mid", Request["mid"].ToString());
        param[6] = new SqlParameter("@did", Request["did"].ToString());
        param[7] = new SqlParameter("@uID", Session["uID"].ToString());
        param[8] = new SqlParameter("@uName", Session["uName"].ToString());
        param[9] = new SqlParameter("@fname", fname);
        param[10] = new SqlParameter("@fpath", fpath);
        param[11] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[11].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[11].Value);
    }    
}