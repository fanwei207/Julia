using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmInspection_GuestComplaint_ResConent : BasePage
{
        string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];

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

            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            _fpath = string.Empty;
            _fname = string.Empty;


            if (string.IsNullOrEmpty(txtMsg.Text))
            {
                ltlAlert.Text = "alert('请填写您的意见')";
                return;
            }
            else if (!string.IsNullOrEmpty(file1.Value.Trim()))
            {
                UpmessageFile();
            }


            try
            {
                string strSql = "sp_comp_saveCompResDet";
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@resNo", Request.QueryString["no"]);
                param[1] = new SqlParameter("@resModuleID", Request.QueryString["moduleId"]);
                param[2] = new SqlParameter("@resDutyName", Request.QueryString["dutyName"]);
                param[3] = new SqlParameter("@msg", txtMsg.Text.Trim());
                param[4] = new SqlParameter("@fileName", _fname);
                param[5] = new SqlParameter("@filePath", _fpath);
                param[6] = new SqlParameter("@uID", Session["uID"].ToString());
                param[7] = new SqlParameter("@uName", Session["uName"].ToString());
                param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[8].Direction = System.Data.ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, param);                

                if (Convert.ToBoolean(param[8].Value))
                {
                    this.ltlAlert.Text = " alert('成功'); var loc=$('body', parent.document).find('#ifrm_19630000')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
                    if (checkISAllPartyFinished(Request.QueryString["no"]))
                    {
                        Semail(Request.QueryString["no"], "resParty", "financeAproach", "");                        
                        return;
                    }
                }
                else
                {
                    this.ltlAlert.Text = "alert('失败，请联系管理员');";
                }
            }
            catch
            {
                this.ltlAlert.Text = "alert('失败，请联系管理员');";
            }
        }

        protected bool UpmessageFile()
        {
            string strUserFileName = file1.PostedFile.FileName;
            int flag = strUserFileName.LastIndexOf("\\");
            string fileName = strUserFileName.Substring(flag + 1);

            string catPath = @"/TecDocs/GuestComp/";
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
                    ltlAlert.Text = "alert('删除文件失败！')";

                    return false;
                }
            }

            try
            {
                file1.PostedFile.SaveAs(strCatFolder + SaveFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('保存文件失败');";

                return false;
            }

            try
            {
                File.Move(strCatFolder + SaveFileName, Server.MapPath(catPath + SaveFileName));
            }
            catch
            {
                ltlAlert.Text = "alert('移动文件失败')";

                if (File.Exists(strCatFolder + SaveFileName))
                {
                    try
                    {
                        File.Delete(strCatFolder + SaveFileName);
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('删除文件失败')";

                        return false;
                    }
                }
                return false;
            }


            _fpath = catPath + SaveFileName;
            _fname = fileName;
            return true;
        }
        private bool checkISAllPartyFinished(string GuestCompNo)
        {
            string str = "sp_comp_checkISAllPartyFinished";
            SqlParameter param = new SqlParameter("@GuestCompNo", GuestCompNo);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param));
        }
        private void Semail(string _no, string _moduleFrom, string _moduleTo, string remark)
        {
            DataTable dt = GetEmail(_no, _moduleFrom);
            string mailto = dt.Rows[0]["email"].ToString();
            StringBuilder sb = new StringBuilder();
            string mailSubject = "强凌 - " + dt.Rows[0]["GuestComplaintNo"].ToString() + "-客诉审批通知";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<html>");
                    sb.Append("<form>");
                    sb.Append("<br>");
                    sb.Append("ALL," + "<br>");
                    sb.Append("    下列客诉单" + dt.Rows[0]["moduleName"].ToString() + "已审批! 客诉详细信息如下。" + "<br>");
                    sb.Append("<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "客诉单号：" + _no + " ，" + "<br>");
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "内容描述：" + dt.Rows[0]["ProblemContent"].ToString() + " ，" + "<br>");
                    sb.Append("请尽快进行审批，谢谢" + "<br>");
                    sb.Append("详情请登录100系统客诉模块查看，谢谢" + "<br>");
                    sb.Append("</body>");
                    sb.Append("</form>");
                    sb.Append("</html>");
                }

            }

            DataTable d = getHaveAuthority(_moduleTo, remark);
            if (d.Rows.Count > 0)
            {
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    if (!this.SendEmail(mailto, d.Rows[i]["email"].ToString(), "", mailSubject, sb.ToString()))
                    {
                        this.Alert("邮件发送失败！");
                        return;
                    }
                }
            }
        }
        private DataTable getHaveAuthority(string moduleName, string remark)
        {
            string str = "sp_comp_selectModuleEmail";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@moduleName", moduleName);
            param[1] = new SqlParameter("@remark", remark);

            return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
        }

        private DataTable GetEmail(string No, string moduleName)
        {
            string str = "sp_comp_selectComplaintInfo";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@compNo", No);
            param[1] = new SqlParameter("@uid", Session["uID"]);
            param[2] = new SqlParameter("@moduleName", moduleName);

            return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
        }
}