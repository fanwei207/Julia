using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmInspection_GuestComplaint_Conent : BasePage
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
                string strSql = "sp_saveCompDisposedDet";
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@disposedNo", Request.QueryString["no"]);
                param[1] = new SqlParameter("@disposedModuleID", Request.QueryString["moduleId"]);
                param[2] = new SqlParameter("@msg", txtMsg.Text.Trim());
                param[3] = new SqlParameter("@fileName", _fname);
                param[4] = new SqlParameter("@filePath", _fpath);
                param[5] = new SqlParameter("@uID", Session["uID"].ToString());
                param[6] = new SqlParameter("@uName", Session["uName"].ToString());             
                param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[7].Direction = System.Data.ParameterDirection.Output;
                
             
                SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, param);
                //$('BODY', parent.parent.parent)location.href = $('BODY', parent.parent.parent.document).URL
                
                if (Convert.ToBoolean(param[7].Value))
                {
                    this.ltlAlert.Text = "alert('成功');var loc=$('body', parent.document).find('#ifrm_19630000')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
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
    }