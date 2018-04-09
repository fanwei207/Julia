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


public partial class RDW_prod_Massage : System.Web.UI.Page
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
            string dept = Request["dept"].ToString();
            if (Request["dept"].ToString() != "plan")
            {
                labPlanDate.Visible = false;
                txtPlanDate.Visible = false;
            }
            labNo.Text = Request["no"].ToString();
            labCode .Text = Request["code"].ToString();
            labProj.Text = Request["proj"].ToString(); 
            if (Request["dept"].ToString() == "plan")
            {
                hidPlanDate.Value = Request["planDate"].ToString();
                txtPlanDate.Text = Request["planDate"].ToString();
            }
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
        //操作人是计划部门的人
        #region 计划部
        if (txtPlanDate.Visible)
        {
            planDate = txtPlanDate.Text;
            if (hidPlanDate.Value == txtPlanDate.Text)
            {
                if (txtMsg.Text == string.Empty)
                {
                    if (filename.Value.Trim() == string.Empty)
                    {
                        ltlAlert.Text = "alert('未做变更，无需保存！');";
                        return;
                    }
                    else
                    {
                        if (UpmessageFile(filename))
                        {
                            if (saveMassage(labNo.Text, labCode.Text, labProj.Text, txtMsg.Text, _fname, _fpath))
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
                }
                else
                {
                    if (filename.Value.Trim() != string.Empty)
                    {
                        if (UpmessageFile(filename))
                        {
                            if (saveMassage(labNo.Text, labCode.Text, labProj.Text, txtMsg.Text, _fname, _fpath))
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
                    //保存留言
                    if (!saveMassage(labNo.Text, labCode.Text, labProj.Text, txtMsg.Text, _fname, _fpath))
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
            }
            else
            {
                if (hidPlanDate.Value == string.Empty)
                {
                    massage = Session["eName"].ToString() + " 设置了PCD: " + txtPlanDate.Text + " 跟踪单号: " + labNo.Text;
                    msg = true;
                }
                else
                {
                    if (txtPlanDate.Text == string.Empty)
                    {
                        if (txtMsg.Text == string.Empty)
                        {
                            ltlAlert.Text = "alert('请填写PCD清空原因！');";
                            return;
                        }
                        else
                        {
                            dateMassage = Session["eName"].ToString() + " 将PCD清空，清空原因： " + txtMsg.Text + " 跟踪单号：" + labNo.Text;
                            dateMsg = "PCD已清空。原因:" + txtMsg.Text;
                            if (!insertMassage(Request["mid"].ToString(), Request["did"].ToString(), dateMassage))
                            {
                                ltlAlert.Text = "alert('PCD清空消息写入失败，请联系管理员！');";
                                return;
                            }
                            if (!updatePalnDate(labNo.Text, labProj.Text, labCode.Text, planDate))
                            {
                                ltlAlert.Text = "alert('PCD清空失败，请联系管理员！');";
                                return;
                            }
                            if (txtMsg.Text != string.Empty)
                            {
                                if (filename.Value.Trim() != string.Empty)
                                {
                                    if (!UpmessageFile(filename))
                                    {
                                        ltlAlert.Text = "alert('附件上传失败！')";
                                        return;
                                    }
                                }
                                if (!saveMassage(labNo.Text, labCode.Text, labProj.Text, dateMsg, _fname, _fpath))
                                {
                                    ltlAlert.Text = "alert('留言保存失败，请联系管理员！');";
                                    return;
                                }
                            }
                            ltlAlert.Text = "alert('留言保存成功！');";
                        }
                    }
                    else
                    {
                        massage = Session["eName"].ToString() + " 将PCD: " + this.hidPlanDate.Value.ToString() + " 改为: " + txtPlanDate.Text + " 跟踪单号：" + labNo.Text;
                        msg = true;
                    }
                }                
                if (msg)
                {
                    //写消息并保存留言
                    if (!insertMassage(Request["mid"].ToString(), Request["did"].ToString(), massage))
                    {
                        ltlAlert.Text = "alert('消息写入失败，请联系管理员！');";
                        return;
                    }
                    else
                    {
                        if (!updatePalnDate(labNo.Text, labProj.Text, labCode.Text, planDate))
                        {
                            ltlAlert.Text = "alert('PCD修改失败！');";
                            return;
                        }
                        else
                        {
                            msg = true;
                            hidPlanDate.Value = txtPlanDate.Text;
                        }
                        if (msg)
                        {
                            if (txtMsg.Text != string.Empty)
                            {
                                if (filename.Value.Trim() != string.Empty)
                                {
                                    if (!UpmessageFile(filename))
                                    {
                                        ltlAlert.Text = "alert('附件上传失败！')";
                                        return;
                                    }
                                }
                                if (!saveMassage(labNo.Text, labCode.Text, labProj.Text, txtMsg.Text, _fname, _fpath))
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
                            else
                            {
                                if (filename.Value.Trim() != string.Empty)
                                {
                                    if (!UpmessageFile(filename))
                                    {
                                        ltlAlert.Text = "alert('附件上传失败！')";
                                        return;
                                    }
                                    else
                                    {
                                        if (!saveMassage(labNo.Text, labCode.Text, labProj.Text, txtMsg.Text, _fname, _fpath))
                                        {
                                            ltlAlert.Text = "alert('留言保存失败，请联系管理员！');";
                                            return;
                                        }
                                        else
                                        {
                                            ltlAlert.Text = "alert('保存成功！');";
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    ltlAlert.Text = "alert('PCD保存成功！');";
                                    return;
                                }
                            }
                        }
                    }                    
                }                
            }
        }
        #endregion
        #region 其他部门
        else
        {
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
                        if (saveMassage(labNo.Text, labCode.Text, labProj.Text, txtMsg.Text, _fname, _fpath))
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
                massage = Session["eName"].ToString() + " 留言：" + txtMsg.Text + ",--项目代码 : " + labCode.Text + ",--项目名称 : " + labProj.Text;
                msg = true;
            }
            if (msg)
            { 
                //写消息并保存留言
                if (!insertMassage(Request["mid"].ToString(), Request["did"].ToString(), massage))
                {
                    ltlAlert.Text = "alert('消息写入失败，请联系管理员！');";
                    return;
                }
                else
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
                    if (!saveMassage(labNo.Text, labCode.Text, labProj.Text, txtMsg.Text, _fname, _fpath))
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
                
            }
        }
        #endregion
    }
    /// <summary>
    /// 更新计划日期
    /// </summary>
    private bool updatePalnDate(string no, string projName, string code, string planDate)
    {
        try
        {
            string str = "sp_prod_updatePalnDate";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@no", no);
            param[1] = new SqlParameter("@projName", projName);
            param[2] = new SqlParameter("@code", code);
            param[3] = new SqlParameter("@planDate", planDate);
            param[4] = new SqlParameter("@uID", Session["uID"].ToString());
            param[5] = new SqlParameter("@uName", Session["uName"].ToString());
            param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[6].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
            return Convert.ToBoolean(param[6].Value);
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 保存消息
    /// </summary>
    private bool insertMassage(string mid, string did, string massage)
    {
        try
        {
            string str = "sp_prod_insertProdMassage";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@mid", mid);
            param[1] = new SqlParameter("@did", did);
            param[2] = new SqlParameter("@massage", massage);
            param[3] = new SqlParameter("@uID", Session["uID"].ToString());
            param[4] = new SqlParameter("@uName", Session["uName"].ToString());
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
    /// <summary>
    /// 保存留言
    /// </summary>
    private bool saveMassage(string no, string code, string proj, string massage, string fname, string fpath)
    {
        string str = "sp_prod_saveMassage";
        SqlParameter []param = new SqlParameter[12];
        param[0] = new SqlParameter("@no",no);
        param[1] = new SqlParameter("@code",code);
        param[2] = new SqlParameter("@proj",proj);
        param[3] = new SqlParameter("@massage", massage);
        param[4] = new SqlParameter("@dept", Request["dept"].ToString());
        param[5] = new SqlParameter("@mid", Request["mid"].ToString());
        param[6] = new SqlParameter("@did", Request["did"].ToString());
        param[7] = new SqlParameter("@uID", Session["uID"].ToString());
        param[8] = new SqlParameter("@uName", Session["uName"].ToString());
        param[9] = new SqlParameter("@fname", fname);
        param[10] = new SqlParameter("@fpath", fpath);
        param[11] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[11].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn,CommandType.StoredProcedure,str,param);
        return Convert.ToBoolean(param[11].Value);
    }
    /// <summary>
    /// 上传文件
    /// </summary>
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
}