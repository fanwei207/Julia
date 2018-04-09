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

public partial class Purchase_vc_maintenance : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    adamClass chk = new adamClass();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("120000220", "确认赔付，发送邮件权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            #region 设置默认值
            //默认ID=0
            hidID.Value = "0";
            //初始化公司：默认选中当前域
            try
            {
                dropPlant.SelectedIndex = -1;
                dropPlant.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
                dropPlant_SelectedIndexChanged(this, new EventArgs());
            }
            catch (Exception)
            {
                ;
            }
            //初始化科目
            dropCate.DataSource = VendCompHelper.GetCategory();
            dropCate.DataBind();
            dropCate.Items.Insert(0, new ListItem("--", "0"));
            //初始化供应商
            dropVender.DataSource = VendCompHelper.GetVender("");
            dropVender.DataBind();
            dropVender.Items.Insert(0, new ListItem("--", "0"));
            //初始化提交者
            txtSubmiter.Text = Session["uName"].ToString();
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //设置提示脚本
            btnBack.Attributes.Add("onclick", "return confirm('如果当前记录未保存，则继续录入后，会丢失！你要继续吗？');");
            btn_cancle.Visible = false;
            #endregion

            #region 如果是修改的话
            //修改：有传回cmd参数的，就表示修改
            chkIsModify.Checked = Request.QueryString["isModify"] == null ? false : true;
            //修改时，要初始化数据
            if (chkIsModify.Checked)
            {
                if ( this.Security["120000220"].isValid )
                {
                    //tr_reason.Visible = true;
                    tr_reason.Visible = false;
                    tr_upload.Visible = false;
                    txtAmount.Enabled = false;
                    txtRate.Enabled = false;
                    txtRemark.Enabled = false;
                }
                //tr_reason.Visible = true;
                tr_reason.Visible = false;
                btn_cancle.Visible = true ;
                btnUpload.Enabled = true;
                btnBack.Visible = true;
                btnBack.Text = "返回";
                btnBack.Attributes.Add("onclick", "");
                hidID.Value = Request.QueryString["vc_id"];
                DataTable table = VendCompHelper.GetMstrByID(hidID.Value);

                

                if (table == null)
                {
                    this.Alert("获取头栏信息时出错，请联系管理员！");
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;

                    foreach (DataRow row in table.Rows)
                    {
                        //设置公司
                        try
                        {
                            dropPlant.SelectedIndex = -1;
                            dropSite.SelectedIndex = -1;
                            dropVender.SelectedIndex = -1;
                            dropCate.SelectedIndex = -1;

                            dropPlant.Items.FindByValue(row["vc_plant"].ToString()).Selected = true;
                            dropPlant_SelectedIndexChanged(this, new EventArgs());
                            if (!string.IsNullOrEmpty(row["vc_site"].ToString()))
                            {
                                dropSite.Items.FindByValue(row["vc_site"].ToString()).Selected = true;
                            }
                            dropVender.Items.FindByValue(row["vc_vend"].ToString()).Selected = true;
                            dropCate.Items.FindByText(row["vc_catename"].ToString()).Selected = true;                            
                        }
                        catch (Exception)
                        {
                            this.Alert("设置工厂、供应商、科目时报错！");
                            btnSave.Enabled = false;
                        }
                        //状态是0的时候显示拒绝和同意按钮
                        if (row["intStatus"].ToString() != "0")
                        {
                            btn_approve.Visible = false;
                            btn_reject.Visible = false;
                        }
                        //状态是-1 或者 0  保存按钮出现
                        if (row["intStatus"].ToString() != "-1" && row["intStatus"].ToString() != "0")
                        {
                            btnSave.Visible = false;
                            btnUpload.Visible = false;
                        }
                        if (row["intStatus"].ToString() == "-2")
                        {
                            btn_delete.Visible = true;
                        }
                        else
                        {
                            btn_delete.Visible = false;
                        }
                        //其他
                        txtFactory.Text = row["vc_FactoryName"].ToString();
                        txtAtten.Text = row["vc_userNameC"].ToString();
                        txtEmail.Text = row["vc_email"].ToString();
                        txtSubmiter.Text = row["userName"].ToString();
                        txtDate.Text = row["vc_date"].ToString();
                        txtAmount.Text = row["vc_amount"].ToString();
                        txtRate.Text = row["vc_rate"].ToString();
                        txtRemark.Text = row["vc_remark"].ToString();
                        if (row["vc_status"].ToString() != "--")
                            txt_reason.Text = row["vc_reason"].ToString();
                        //工厂、供应商、科目不允许修改
                        dropPlant.Enabled = false;
                        dropSite.Enabled = false;
                        dropVender.Enabled = false;
                        dropCate.Enabled = false;
                        //只有发送邮件后才能显示取消按钮
                        if (row["vc_emailDate"].ToString() == "") btn_cancle.Visible = false;
                    }
                }
                if(!this.Security["120000111"].isValid)
                {
                    btn_cancle.Enabled = false;
                }
                BindGridView();
            }
            #endregion
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (dropPlant.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('工厂不能为空！')";
            return;
        }
        if (dropVender.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('供应商不能为空！')";
            return;
        }
        if (txtAtten.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('联系人不能为空！')";
            return;
        }
        if (txtEmail.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('邮箱不能为空！')";
            return;
        }
        if (dropCate.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('科目不能为空！')";
            return;
        }   
        if (txtAmount.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('金额不能为空！')";
            return;
        }
        decimal amount;
        try 
        {
            amount= Convert.ToDecimal(txtAmount.Text.Trim());
        }
        catch
        {
            ltlAlert.Text = "alert('金额可以是小数或整数，只能是数字哦!');";
            return;
        }

        decimal rate = 0;
        if (txtRate.Text.Trim().Length == 0) rate = 0;
        else
        {
            try
            {
                rate = Convert.ToDecimal(txtRate.Text.Trim()); 
            }
            catch
            {
                this.Alert("只能填数字！");
            }
                
        }

        if (Convert.ToInt32(hidID.Value) > 0)
        {
            if (VendCompHelper.UpdateMstr(hidID.Value, amount.ToString(), rate.ToString(), txtRemark.Text.Trim()
                , txtAtten.Text.Trim(), txtEmail.Text.Trim(), txtFactory.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString()))
            {
                this.Alert("修改成功！");
                string mailto = txtEmail.Text;
                string mailSubject = "强凌 - " + dropCate.SelectedItem.ToString() + "通知";
                SendEmail2(mailto, mailSubject
                    , dropVender.SelectedItem.ToString().Substring(10)
                    , dropVender.SelectedItem.ToString()
                    , dropCate.SelectedItem.ToString()
                    , txtAmount.Text
                    , hidID.Value);
                this.Alert("同意成功！");
            }
            else
            {
                this.Alert("修改失败！请联系管理员！");
            }
        }
        else
        {
            int _mstrID = VendCompHelper.InsertMstr(dropPlant.SelectedValue, dropSite.SelectedValue, dropVender.SelectedValue, dropCate.SelectedItem.Text
                    , amount.ToString(), rate.ToString(), Session["uID"].ToString(), txtRemark.Text.Trim(), txtDate.Text.Trim(), dropVender.SelectedValue
                    , txtAtten.Text.Trim(), txtEmail.Text.Trim(), txtFactory.Text.Trim());
            if (_mstrID > 0)
            {
                //为严谨对待，故保存成功后，工厂、供应商、科目不允许修改
                dropPlant.Enabled = false;
                dropVender.Enabled = false;
                dropCate.Enabled = false;

                hidID.Value = _mstrID.ToString();
                btnUpload.Enabled = true;
                this.Alert("新增成功！");
                string mailto = txtEmail.Text;
                string mailSubject = "强凌 - " + dropCate.SelectedItem.ToString() + "通知";
                SendEmail2(mailto, mailSubject
                    , dropVender.SelectedItem.ToString().Substring(10)
                    , dropVender.SelectedItem.ToString()
                    , dropCate.SelectedItem.ToString()
                    , txtAmount.Text
                    , hidID.Value);
                this.Alert("同意成功！");
            }
            else
            {
                this.Alert("新增失败！请联系管理员！");
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["vc_cancle"] != null)
        {
            this.Redirect("vc_mstrCancelList.aspx");
        }
        if (chkIsModify.Checked)
        {
            this.Redirect("vc_mstrList.aspx");
        }
        else
        {
            this.Redirect("vc_mstr.aspx");
        }
    }
    protected void dropVender_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAtten.Text = string.Empty;
        txtEmail.Text = string.Empty;
        if (dropVender.SelectedIndex > 0)
        {
            DataTable table = VendCompHelper.GetVender(dropVender.SelectedValue);

            if (table == null)
            {
                this.Alert("获取供应商信息失败！请联系管理员！");
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    txtAtten.Text = row["usr_userNameC"].ToString();
                    txtEmail.Text = row["usr_email"].ToString();
                }
            }
        }
    }

    #region 上传附件
    protected void btnUpload_Click(object sender, EventArgs e)
    {
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

                int vc_mstrid = Convert.ToInt32(hidID.Value);

                string strSql = "sp_vc_insertDoc1";
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@vc_mstrid", vc_mstrid);
                param[1] = new SqlParameter("@vc_docname", _fileName);//FileUpload2.PostedFile.FileName.Substring(FileUpload2.PostedFile.FileName.LastIndexOf("\\") + 1)
                param[2] = new SqlParameter("@vc_date", DateTime.Now.ToString());
                param[3] = new SqlParameter("@vc_AllPath", _filePath);
                param[4] = new SqlParameter("@uID", Session["uID"].ToString());
                param[5] = new SqlParameter("@uName", Session["uName"].ToString());
                param[6] = new SqlParameter("@reValue", SqlDbType.Bit);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

                if (param[6].Value != DBNull.Value)
                {

                    ltlAlert.Text = "alert('上传成功!')";
                }
                else
                {
                    ltlAlert.Text = "alert('上传失败，不可重复上传文件!')";
                }

            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!')";
            }

            BindGridView();
        }
        else
        {
            this.ltlAlert.Text = "alert('请选择一个文件！');";
        }
    }
    private bool UploadFile(ref string filePath, ref string fileName)
    {
        string strUserFileName = FileUpload2.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/VendComp/";
        string strCatFolder = Server.MapPath(catPath);

        string attachName = Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
        string attachExtension = Path.GetExtension(FileUpload2.PostedFile.FileName);
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
            FileUpload2.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";

            return false;
        }
        string path = @"/TecDocs/VendComp/";

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
    protected override void BindGridView()
    {
        gv.DataSource = VendCompHelper.GetDocuments(hidID.Value);
        gv.DataBind();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string sql = "select vc_AllPath from vc_doc where vc_id=" + gv.DataKeys[e.RowIndex].Values[0].ToString();
        string AllPath = Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.Text, sql));
        AllPath = AllPath.Replace('/', '\\');
        AllPath = Server.MapPath(AllPath);
        File.Delete(AllPath);
        sql = "delete vc_doc where vc_id=" + gv.DataKeys[e.RowIndex].Values["vc_id"].ToString();
        SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, sql);
        BindGridView();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string url = gv.DataKeys[intRow].Values["vc_AllPath"].ToString();
            ltlAlert.Text = "var w=window.open(' " + url + "'); w.focus();";
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    #endregion
    protected void dropPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropSite.Items.Clear();

        if (dropPlant.SelectedValue == "1")
        {
            dropSite.Items.Add(new ListItem("1000", "1000"));
            dropSite.Items.Add(new ListItem("2100", "2100"));
            dropSite.Items.Add(new ListItem("4100", "4100"));
        }
        else if (dropPlant.SelectedValue == "2")
        {
            dropSite.Items.Add(new ListItem("2000", "2000"));
            dropSite.Items.Add(new ListItem("3000", "3000"));
            dropSite.Items.Add(new ListItem("1200", "1200"));
        }
        else if (dropPlant.SelectedValue == "5")
        {
            dropSite.Items.Add(new ListItem("4000", "4000"));
            dropSite.Items.Add(new ListItem("1400", "1400"));
        }
        else if (dropPlant.SelectedValue == "8")
        {
            dropSite.Items.Add(new ListItem("5000", "5000"));
            dropSite.Items.Add(new ListItem("1500", "1500"));
        }

    }
    protected void btn_cancle_Click(object sender, EventArgs e)
    {
        int vc_id = Convert.ToInt32(Request.QueryString["vc_id"]);
        int re = VendCompHelper.CancleItem(vc_id);
        if (re == 0) this.Alert("取消失败！");
        else　if(re==1) this.Alert("取消成功！");
        else this.Alert("已经取消过了！"); 
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        int vc_id = Convert.ToInt32(Request.QueryString["vc_id"]);
        int re = VendCompHelper.ApproveORreject(vc_id, 1,"",Convert.ToInt32(Session["uID"]) , Session["uName"].ToString());
        if (re == 0) this.Alert("同意失败！");
        else if (re == 1)
        {
            string sql = "sp_vc_confirmInfo";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@vc_id", vc_id);
            param[1] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

            //发送邮件

            string mailto = txtEmail.Text;
            string mailSubject = "强凌 - " + dropCate.SelectedItem.ToString() + "通知";
            SendEmail2(mailto, mailSubject
                , dropVender.SelectedItem.ToString().Substring(10)
                , dropVender.SelectedItem.ToString()
                , dropCate.SelectedItem.ToString()
                , txtAmount.Text
                , hidID.Value);
            this.Alert("同意成功！");
            this.Redirect("/Purchase/vc_mstr.aspx?vc_id=" + vc_id + "&isModify=0" );
            
        }
        else this.Alert("已经是同意的状态了！"); 
    }
    private void SendEmail2(string mailto, string mailSubject, string companyName, string vend, string cateName, string amount, string vc_id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<form>");
        sb.Append("<br>");
        sb.Append(companyName + "(" + vend + ")" + "<br>");
        sb.Append("    您好:" + "<br>");
        sb.Append("<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "在" + System.DateTime.Now + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "我司对贵公司 " + cateName + "罚款金额为" + amount + " ，" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如有疑问请登陆我司--供应链管理系统<赔付明细>页面查看具体内容，并在48小时内联系我司相关人员，逾期将视为贵司默认，谢谢配合！ " + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "供应链管理系统："+baseDomain.getSupplierWebsite()+" " + "<br>");

        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");

        if (!this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), mailto, "", mailSubject, sb.ToString()))
        {
            this.Alert("邮件发送失败！");
        }
        else
        {
            VendCompHelper.MarkEmailSended(vc_id, Session["uID"].ToString(), Session["uName"].ToString());
            this.Alert("邮件发送成功！");
        }
    }
    protected void btn_reject_Click(object sender, EventArgs e)
    {
        if(txt_reason.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('拒绝时，必须填写理由！')";
            return;
        }
        int vc_id = Convert.ToInt32(Request.QueryString["vc_id"]);
        int re = VendCompHelper.ApproveORreject(vc_id, -1, txt_reason.Text.Trim(), Convert.ToInt32(Session["uID"]), Session["uName"].ToString());
        if (re == 0) this.Alert("拒绝失败！");
        else if (re == 1) 
        {
            //发送邮件
            string mailto = VendCompHelper.GetEmail(vc_id);
            string mailSubject = "赔付拒绝通知";
            SendEmail(mailto, mailSubject
               , dropVender.SelectedItem.ToString()
               , dropCate.SelectedItem.ToString()
               , hidID.Value
               , txt_reason.Text.Trim());
            
            this.Alert("拒绝成功！");
            this.Redirect("/Purchase/vc_mstr.aspx?vc_id=" + vc_id + "&isModify=0");
        }
        else this.Alert("已经是拒绝的状态了！"); 
    }

    private void SendEmail(string mailto, string mailSubject, string vend, string cateName, string vc_id,string reason)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<form>");
        sb.Append("<br>");
        sb.Append("    您好:" + "<br>");
        sb.Append("<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "在" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您提交的赔付类型为" + cateName +",供应商为" + vend +"的赔付申请，已经被拒绝。 <br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "理由是：<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + reason + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "请修改后再次保存或者取消该赔付申请 " + "<br>");    
        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");

        if (!this.SendEmail(Session["email"].ToString(), mailto, "", mailSubject, sb.ToString()))
        {
            this.Alert("邮件发送失败！");
        }
        else
        {
            this.Alert("邮件发送成功！");
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        int vc_id = Convert.ToInt32(Request.QueryString["vc_id"]);
        int re = VendCompHelper.CancleItem(vc_id);
        if (re == 0) this.Alert("取消失败！");
        else if (re == 1) this.Alert("取消成功！");
        else this.Alert("已经取消过了！"); 
    }
}