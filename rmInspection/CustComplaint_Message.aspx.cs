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


public partial class EDI_CustComplaint_Message : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

    adamClass chk = new adamClass();
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
            if (Request["dept"].ToString() != "testPlan")
            {
                labPlanDate.Visible = false;
                txtPlanDate.Visible = false;
            }
            labNo.Text = Request["no"].ToString();
            labCust.Text = Request["cust"].ToString();
            labOrder.Text = Request["order"].ToString();
            if (Request["dept"].ToString() == "testPlan")
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
                    if (saveMassage(labNo.Text, labCust.Text, labOrder.Text, txtMsg.Text, _fname, _fpath))
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
            if (!saveMassage(labNo.Text, labCust.Text, labOrder.Text, txtMsg.Text, _fname, _fpath))
            {
                ltlAlert.Text = "alert('留言保存失败，请联系管理员！');";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('留言保存成功！');";

                if (Request["dept"].ToString() == "opinion" || Request["dept"].ToString() == "talk" || Request["dept"].ToString() == "finalOpinion")
                {
                    if (txtMsg.Text != string.Empty)
                    {
                        #region 发送邮件给客户投诉单所有相关联的人
                        string Topical = "客户投诉单：" + labNo.Text;
                        string mto = GetUserEmailTo("0", "all", labNo.Text);
                        string body = "<html>";
                        body += "<body>";
                        body += "<form>";
                        body += "<br>";
                        body += "您好:" + "<br>";
                        body += "   客户投诉单：" + labNo.Text + "," + Session["uName"].ToString() + "有新的留言，内容如下：" + "<br>";
                        body += txtMsg.Text + "<br>";
                        body += "<br>";
                        body += "</body>";
                        body += "</form>";
                        body += "</html>";
                        //SendEmail222(Session["email"].ToString(), mto, Topical, body);
                        #endregion
                    }
                }
                /*
                if (Request["dept"].ToString() == "analysisReason")
                {
                    string body = Request.QueryString["createName"].ToString() + "，您好:" + "<br>";
                    body += "<br>";
                    body += "&nbsp;&nbsp;" + "跟踪单号：" + Request["no"].ToString() + "<br>";
                    body += "&nbsp;&nbsp;" + "项目代码：" + Request["code"].ToString() + "<br>";
                    //body += "&nbsp;&nbsp;&nbsp;&nbsp;" + "分类：" + Request["no"].ToString() + "<br>";
                    //body += "&nbsp;&nbsp;&nbsp;&nbsp;" + "失效代码：" + Request["no"].ToString() + "<br>";
                    //body += "&nbsp;&nbsp;&nbsp;&nbsp;" + "问题内容：" + Request["no"].ToString() + "<br>";
                    body += "&nbsp;&nbsp;" + "原因分析：" + txtMsg.Text.ToString() + "<br>";

                    body += "&nbsp;&nbsp;<a href='http://localhost:100/RDW/Test_Report.aspx' rel='external' target='_blank'>测试报告链接</a>";
                        
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "（1）轨道9号线在泗泾站3号口出站，向左走，过马路（书报亭附近）转乘坐46路车到莘潮家居站（望东南路口）下车。 或者直接乘坐出租车，大概17元左右即可到达。" + "<br>";
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "（2）乘沪松线公交车到叶星站下，沿沪松公路向回走到望东南路口，再沿望东南路走到139号。或者直接乘坐出租车，大概17元左右即可到达。" + "<br>";
                    body += "<a href='http://j.map.baidu.com/YzPiz' rel='external' target='_blank'>http://j.map.baidu.com/YzPiz</a>";
                    body += "<br>";
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如有任何疑问您可以通过下列联系方式咨询我，祝您工作生活皆愉快！" + "<br>";
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如果不能前来请务必提前邮件或者电话通知我们取消面试或者另行安排面试时间，感谢您的配合！" + "<br>";
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "------------------------------------------------------------" + "<br>";
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公司：TCP强凌集团-人力资源中心-" + txtName.Text + "<br>";
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "地址：" + txtAddress.Text + "<br>";
                    body += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "座机：" + txtPhone.Text;
                        
                    //测试发送邮件
                    SendEmail222(Session["email"].ToString(), GetCreateEmail(), "可行性项目测试原因分析", body);
                        
                }*/
                return;
            }

        }
        #endregion
    }
    private string GetUserEmailTo(string procid, string type, string no)
    {
        string str = "sp_CustComp_SelectEmailTo";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@procid", procid);
        param[1] = new SqlParameter("@type", type);
        param[2] = new SqlParameter("@no", no);

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    //private string GetCreateEmail()
    //{
    //    string sql = "select email from tcpc0.dbo.users where userid = " + Request["createby"].ToString();
    //    return Convert.ToString(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql));
    //}
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static bool SendEmail222(string from, string to, string subject, string body)
    {
        try
        {
            //string[] uploadAtts = uploadAtt.Split(';');
            MailAddress _mailFrom = new MailAddress(from);
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = _mailFrom;

            foreach (string _to in to.Split(';'))
            {
                if (!string.IsNullOrEmpty(_to))
                {
                    _mailMessage.To.Add(_to);
                }
            }
            _mailMessage.Subject = subject;
            _mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, Encoding.GetEncoding("GB2312"), "text/html");

            _mailMessage.AlternateViews.Add(htmlBody);

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(_mailMessage);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }
    /// <summary>
    /// 更新计划日期
    /// </summary>
    private bool updatePalnDate(string id, string projName, string code, string planDate)
    {
        try
        {
            string str = "sp_test_updatePalnCompleteDate";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@id", id);
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
    /// 保存留言
    /// </summary>
    private bool saveMassage(string no, string cust, string order, string massage, string fname, string fpath)
    {
        string str = "sp_CusComp_saveMassage";
        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@cust", cust);
        param[2] = new SqlParameter("@order", order);
        param[3] = new SqlParameter("@massage", massage);
        param[4] = new SqlParameter("@dept", Request["dept"].ToString());
        param[5] = new SqlParameter("@uID", Session["uID"].ToString());
        param[6] = new SqlParameter("@uName", Session["uName"].ToString());
        param[7] = new SqlParameter("@fname", fname);
        param[8] = new SqlParameter("@fpath", fpath);
        param[9] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[9].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[9].Value);
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