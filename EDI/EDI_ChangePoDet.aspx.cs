using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;
using System.Drawing;
using System.Text;

public partial class EDI_EDI_ChangePoDet : BasePage
{
    poc_helper helper = new poc_helper();
    public string EffectTR = string.Empty;//第一个标签：部门确认显示的tr

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {

            this.Security.Register("10001251", "经理审批权限");
            this.Security.Register("10001258", "通知人审批权限");
            this.Security.Register("10001255", "讨论组填写页面权限");
            this.Security.Register("10001257", "执行权限");
        }

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 记录当前选中的Tab
            try
            {
                hidTabIndex.Value = Request.QueryString["index"];
            }
            catch (Exception)
            {

            }
            #endregion
            hidPocId.Value = Request.QueryString["poc_id"];
            bind();
            #region 注册btn客户端提示
            btnDone.Attributes.Add("onclick", "return confirm('Once click, you can't change!')");
            btnNotDone.Attributes.Add("onclick", "return confirm('Once click, you can't change it!')");

            btnValid.Attributes.Add("onclick", "return confirm('Once click, you can't change it!')");
            btnNotValid.Attributes.Add("onclick", "return confirm('Once click, you can't change it!')");

            btnExcute.Attributes.Add("onclick", "return confirm('Once click, you can't change it!')");
            #endregion
        }
    }

    private void bind()
    {
        GetManualPoHrd();
        BindGridView();
        BindUpload();
        gv1bind();
        BindInfo();
     

    }
    /// <summary>
    /// 附件绑定
    /// </summary>

    /// <summary>
    /// 留言以及审批信息
    /// </summary>
    private void BindInfo()
    {
        SqlDataReader sdr = helper.selectEdIPoMstrByID(hidPocId.Value);
        
        if(sdr.Read())
        {
            hidCreatedBy.Value = sdr["createdBy"].ToString();
            txtApprMsg.Text = sdr["poc_managerMsg"].ToString();

            //lbManagerAgreeBy.InnerHtml=sdr["poc_managerName"].ToString();

            txtNotice.Text = sdr["poc_noticeMsg"].ToString();

            //tdAgree.InnerHtml = sdr["poc_noticeName"].ToString();

            txtExcuteMsg.Text = sdr["poc_resuleMsg"].ToString();

            td1.InnerHtml = sdr["poc_resuleName"].ToString();

            bindBtnAndTxt(sdr);


        }
        sdr.Dispose();
        sdr.Close();



    }

    /// <summary>
    /// 控制按钮与填写意见的地方可否填写
    /// </summary>
    private void bindBtnAndTxt(SqlDataReader sdr)
    {
        string isCommit = sdr["poc_commit"].ToString();
        string isManager = sdr["poc_managerIsAgree"].ToString();
        string isNotice = sdr["poc_noticeIsAgree"].ToString();
        string isresule = sdr["poc_resuleIsDo"].ToString();
        string iseffectFlag = sdr["effectFlag"].ToString();//存在最后意见不同意的讨论组为 false //讨论组全部通过是 true

        SqlDataReader sdrAtta = helper.selectEDIAttaByID(hidPocId.Value);

        while (sdrAtta.Read())
        {
            if(sdrAtta["poc_docType"].ToString().Equals("manager"))
            {
                hlinkManager.Text = "Attachment:" + sdrAtta["poc_docFileName"].ToString();
                hlinkManager.NavigateUrl = sdrAtta["poc_docURL"].ToString();
            }
            else if(sdrAtta["poc_docType"].ToString().Equals("notice"))
            {
                hlnotice.Text = "Attachment:" + sdrAtta["poc_docFileName"].ToString();
                hlnotice.NavigateUrl = sdrAtta["poc_docURL"].ToString();
            }
        }
        sdrAtta.Dispose();
        sdrAtta.Close();

        //  this.Security.Register("10001251", "经理审批权限");
        //    this.Security.Register("10001258", "通知人审批权限");
        //    this.Security.Register("10001255", "讨论组填写页面权限");
        //  this.Security.Register("10001257", "执行权限");
        //  this.Security["560000466"].isValid
        if (Request["status"] != null)
        {
            if (Request["status"] != "-20")
            { 
                if (isCommit == "True"   )
                {
                    if (isManager == "" && this.Security["10001251"].isValid)
                    {
                        txtApprMsg.Enabled = true;
                        fileManager.Visible = true;
                        btnDone.Enabled = true;
                        btnNotDone.Enabled = true;
                    }
                    else if (isManager == "False")
                    {
                        btnDone.Enabled = false;
                        btnNotDone.Enabled = false;
                        btnNotDone.Text = "DisAgree by " + sdr["poc_managerName"].ToString();
                    }
                    else if (isManager == "True")
                    {
                        btnDone.Enabled = false;
                        btnNotDone.Enabled = false;
                        btnDone.Text = "Agree by " + sdr["poc_managerName"].ToString();


                        if (isresule == "" && this.Security["10001255"].isValid)
                        {
                            chkCanEffect.Checked = true;
                        }
                        else
                        {
                            chkCanEffect.Checked = false;
                        }


                        if (isresule == "" && this.Security["10001257"].isValid && iseffectFlag == "True")
                        {
                            txtExcuteMsg.Enabled = true;
                            btnExcute.Enabled = true;
                        }
                        else if (isresule == "" && this.Security["10001258"].isValid && iseffectFlag == "False")
                        {
                            btnNotValid.Enabled = true;
                            txtNotice.Enabled = true;
                            fileNotice.Visible = true;
                        }
                        else if (isresule == "True")
                        {
                            btnExcute.Text = "Result by " + sdr["poc_resuleName"].ToString();
                            btnExcute.Enabled = false;
                        }

                        //if (isNotice == "" && this.Security["10001255"].isValid )
                        //{
                        //    chkCanEffect.Checked = true;
                        //}
                        //else
                        //{
                        //    chkCanEffect.Checked = false;
                        //}


                        //if (isNotice == "" && this.Security["10001258"].isValid && iseffectFlag == "True")
                        //{
                        //    txtNotice.Enabled = true;
                        //    fileNotice.Visible = true;
                        //    btnValid.Enabled = true;
                        //    btnNotValid.Enabled = true;
                        //}
                        //else if (isNotice == "False")
                        //{
                        //    btnNotValid.Text = "DisAgree by " + sdr["poc_noticeName"].ToString();
                        //    btnValid.Enabled = false;
                        //    btnNotValid.Enabled = false;
                        //}
                        //else if (isNotice == "True")
                        //{
                        //    btnValid.Enabled = false;
                        //    btnNotValid.Enabled = false;
                        //    btnValid.Text = "Agree by " + sdr["poc_noticeName"].ToString();

                        //    if (isresule == "" && this.Security["10001257"].isValid)
                        //    {
                        //        txtExcuteMsg.Enabled = true;
                        //        btnExcute.Enabled = true;
                        //    }
                        //    else if (isresule == "True")
                        //    {
                        //        btnExcute.Text = "Result by " + sdr["poc_resuleName"].ToString();
                        //        btnExcute.Enabled = false;
                        //    }
                        //}
                    }
                }

       
                
            }
        }

    }
    /// <summary>
    /// 绑定effect
    /// </summary>
    private void gv1bind()
    {
        gv1.DataSource = helper.GetPocEffect(hidPocId.Value);
        gv1.DataBind();
    }

   
    /// <summary>
    /// 绑定顶栏位
    /// </summary>
    protected void GetManualPoHrd()
    {
        try
        {

            DataSet ds = helper.selectEDIPoHrd(hidPocId.Value, "", Session["uID"].ToString(), Session["uName"].ToString());



            if (ds.Tables[0].Rows.Count > 0)
            {
                lblNo.Text = ds.Tables[0].Rows[0]["poc_Code"].ToString();
                lblCommitName.Text = ds.Tables[0].Rows[0]["poc_commitName"].ToString();
                lbCustPo.Text = ds.Tables[0].Rows[0]["poNbr"].ToString();
                lblCusrCode.Text =  "(" + ds.Tables[0].Rows[0]["cusCode"].ToString() + ")" +ds.Tables[0].Rows[0]["cusName"].ToString() ;
                lbShipTo.Text = "(" + ds.Tables[0].Rows[0]["shipto"].ToString() + ")" + ds.Tables[0].Rows[0]["shiptoName"].ToString();
                lbShipVia.Text = ds.Tables[0].Rows[0]["shipVia"].ToString();
                lbDueDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["dueDate"]));
                lbChannel.Text = ds.Tables[0].Rows[0]["mpo_channel"].ToString();
                lbCommitDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["poc_commitDate"]));
                txtReason.Text = ds.Tables[0].Rows[0]["poc_reason"].ToString();
               

            }




        }
        catch
        {
            this.Alert("Operation fails while getting po!");
        }
    }
    /// <summary>
    /// 绑定修改的行的信息
    /// </summary>
    protected void BindGridView()
    {

        DataSet ds = helper.selectEDIPoDetModified(hidPocId.Value, lbCustPo.Text.Trim());



        lbparentLine.InnerHtml = ds.Tables[0].Rows[0]["parentLine"].ToString().Equals(string.Empty) ? "" : "The Line had been split";
        gvlist.DataSource = ds.Tables[0];
        gvlist.DataBind();
    }
    /// <summary>
    /// 绑定申请中上传的文件
    /// </summary>
    private void BindUpload()
    {
        DataTable dt = helper.selectPoDocModifiedByID(hidPocId.Value);

        gvUpload.DataSource = dt;
        gvUpload.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            GridViewRow row = e.Row;
            bool flag = false;

            if (gvlist.DataKeys[e.Row.RowIndex].Values["addflag"].ToString() == "add")
            {
                flag = true;
                ((Label)e.Row.FindControl("lbChangeType")).Text = "A";
            }
            else if (gvlist.DataKeys[e.Row.RowIndex].Values["isdelete"].ToString() == "True")
            {
                ((Label)e.Row.FindControl("lbChangeType")).Text = "D";
            }
            else
            {
                ((Label)e.Row.FindControl("lbChangeType")).Text = "U";
            }
            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }

            CellShow(row, "partNbr", ref flag);
            CellShow(row, "sku", ref flag);
            CellShow(row, "qadPart", ref flag);
            CellShow(row, "ordQty", ref flag);
            CellShow(row, "um", ref flag);
            CellShow(row, "price", ref flag);
            CellShow(row, "reqDate", ref flag);
            CellShow(row, "dueDate", ref flag);
            CellShow(row, "remark", ref flag);
            CellShow(row, "desc", ref flag);



            //if (flag)
            //{

            //    ((LinkButton)e.Row.FindControl("lkbCancel")).Visible = true;
            //    ((LinkButton)e.Row.FindControl("lkbEdit")).Visible = true;
            //}
        }
    }

    /// <summary>
    /// 处理行的方法
    /// </summary>
    /// <param name="row">当前行的对象</param>
    /// <param name="field">列的字段</param>
    private void CellShow(GridViewRow row, string field, ref bool flag)
    {
        string value = ((DataRowView)row.DataItem)[field].ToString();
        Label link = row.FindControl("link" + field) as Label;
        Label label = row.FindControl("lbl" + field) as Label;
        if (value.Contains("|"))
        {
            string[] values = value.Split(new char[] { '|' });
            string oldValue = values[0];
            string newValue = values[1];
            if (link != null)
            {
                link.Text = newValue;

            }
            if (label != null)
            {
                label.Text = oldValue;
                label.Font.Strikeout = true;
            }
            flag = true;
        }
        else
        {
            if (label != null)
            {
                label.Text = value;
                label.Font.Strikeout = false;
            }
            if (link != null)
            {
                link.Visible = false;
            }
        }
    }

    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnView")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            string pocId = hidPocId.Value;
            string _effectID = rowView["poce_effectID"].ToString();

            EffectTR += "<tr><td class=\"FixedGridHeight\" style=\"height:50px;\"></td>";
            EffectTR += " <td class=\"FixedGridLeft\" style=\"word-break:break-all; word-wrap:break-word;\" colspan=\"4\">" + rowView["poce_enDesc"].ToString() + "</td>";
            #region 绑定留言，同时设置EffectTR
            EffectTR += " <td colspan=\"13\" style=\"text-align: left;\">";
            foreach (DataRow row in helper.GetEffectDetail(pocId, _effectID).Rows)
            {
                e.Row.Cells[1].Text += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["createdDate"])) +
                    "&nbsp;&nbsp;&nbsp;&nbsp;" + row["createdName"].ToString()
                    + "&nbsp;&nbsp;&nbsp;&nbsp;"
                    + (string.Empty.Equals(row["poced_isAgree"].ToString()) ? "" : (Convert.ToBoolean(row["poced_isAgree"]) ? "<span style='color:green'> Agree</span>" : "<span style='color:red'> Disagree</span>")) + "<br />";
                EffectTR += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["createdDate"])) + "&nbsp;&nbsp;&nbsp;&nbsp;" + row["createdName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;"
                    + (string.Empty.Equals(row["poced_isAgree"].ToString()) ? "" : (Convert.ToBoolean(row["poced_isAgree"]) ? "<span style='color:green'> Agree</span>" : "<span style='color:red'> Disagree</span>")) + "<br />";

                e.Row.Cells[1].Text += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["poced_msg"].ToString() + "<br />";
                EffectTR += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["poced_msg"].ToString() + "<br />";

                if (!string.IsNullOrEmpty(row["poced_fileName"].ToString()))
                {
                    e.Row.Cells[1].Text += "Attachment:<a href='" + row["poced_url"].ToString() + "' target='_blank'>" + row["poced_fileName"].ToString() + "</a>" + "<br />";
                    EffectTR += "Attachment:<a href='" + row["poced_url"].ToString() + "' target='_blank'>" + row["poced_fileName"].ToString() + "</a>" + "<br />";
                }

                e.Row.Cells[1].Text += "<br />";
                EffectTR += "<br />";
            }
            EffectTR += " </td>";
            #endregion
            #region 绑定责任人，同时设置EffectTR
            EffectTR += " <td class=\"FixedGridRight\">";
            foreach (DataRow row in helper.GetEffectUser(_effectID).Rows)
            {
                //Email
                if (!string.IsNullOrEmpty(row["email"].ToString()))
                {
                    hidEmailAddress.Value = hidEmailAddress.Value.Replace(row["email"].ToString() + ";", "");
                    hidEmailAddress.Value += row["email"].ToString() + ";";
                }

                e.Row.Cells[2].Text += row["poceu_userName"].ToString() + "-" + row["englishName"].ToString() + "<br />";
                EffectTR += row["poceu_userName"].ToString() + " - " + row["englishName"].ToString() + "<br />";
            }
            EffectTR += " </td>";
            #endregion
            EffectTR += "</tr>";
        }
    }

    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["no"] != null)
        {
            Response.Redirect("EDI_ChangePoMstr.aspx?pocCode=" + Request["pocCode"].ToString() + "&no=" + Request["no"].ToString()
                + "&statuValue=" + Request["statuValue"].ToString() + "&typeValue=" + Request["typeValue"].ToString()
                + "&dateBegin=" + Request["dateBegin"].ToString() + "&dateEnd=" + Request["dateEnd"].ToString());
        }
        else
        {
            Response.Redirect("EDI_ChangePoMstr.aspx");
        }
       
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (hidCreatedBy.Value.Equals(Session["uID"].ToString()))
        {
            this.Alert("fail! The Apply is applied with you.You can not approval");
            return;
        }

        btnDo("manager", "1", fileManager, txtApprMsg.Text.Trim());

        
    }

    
    protected void btnNotDone_Click(object sender, EventArgs e)
    {
        if (hidCreatedBy.Value.Equals(Session["uID"].ToString()))
        {
            this.Alert("fail! The Apply is applied with you.You can not approval");
            return;
        }

        btnDo("manager", "0", fileManager, txtApprMsg.Text.Trim());
    }
    protected void btnValid_Click(object sender, EventArgs e)
    {
        btnDo("notice", "1", fileNotice, txtNotice.Text.Trim());
    }
    protected void btnNotValid_Click(object sender, EventArgs e)
    {
        btnDo("notice", "0", fileNotice, txtNotice.Text.Trim());
    }
    protected void btnExcute_Click(object sender, EventArgs e)
    {
        btnDo("excute", "1", null, txtExcuteMsg.Text.Trim());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">类型 经理 notice 执行</param>
    /// <param name="flag">结果</param>
    /// <param name="file对象">结果</param>
    private void btnDo(string type, string flag, System.Web.UI.HtmlControls.HtmlInputFile file,string leaveMsg)
    {

        string to = string.Empty;
        string subject = string.Empty;
        string body = string.Empty;
        string copy = string.Empty;
        if (file != null)
        {
            if (!string.Empty.Equals(file.PostedFile.FileName))
            {
                this.uploadForAll(type, file);
            }
        }
        int lastflag = helper.enterButton(type, flag, hidPocId.Value, Session["uID"].ToString(), Session["uName"].ToString(), leaveMsg, out to, out copy, out subject, out body);

        if (lastflag == 1)
        {
            ltlAlert.Text = "alert('Success!');";
            #region 发送邮件
            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            //"TCP ECN (产品变更通知书)";


            if (!this.SendEmail(from, to, copy, subject, body))
            {
                this.ltlAlert.Text = "alert('Email sending failure');";
            }
            else
            {
                this.ltlAlert.Text = "alert('Email sending');";
            }
            #endregion
        }
        else if (lastflag == 2 && type.Equals("excute"))
        {
            ltlAlert.Text = "alert('The Line had been split ,Please Link Administrators to handle the Data');";
            

        }
        else
        {
            ltlAlert.Text = "alert('Database Error,Please Link Administrators');";
        }

       
 
        bind();
        
    }
    /// <summary>
    /// 上传附件的方法
    /// </summary>
    /// <param name="type"></param>
    /// <param name="file"></param>
    private void uploadForAll(string type, System.Web.UI.HtmlControls.HtmlInputFile file)
    {
        string path = "/TecDocs/EDI/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(file.PostedFile.FileName))
        {
            //ltlAlert.Text = "alert('Upload path can't be null');";
            return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path, file))
            {
                ltlAlert.Text = "alert('Upload file failed! please Linking administrators');";
                return;
            }

        }
        if (helper.uploadAtta(hidPocId.Value, fileName, filePate,type, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('Upload file success!');";
            return;
            
        }
        else
        {

            ltlAlert.Text = "alert('Upload file failed! please Linking administrators');";
            return;
        }
    }

    protected bool ImportFile(ref string _fileName, ref string _filePath, string path, System.Web.UI.HtmlControls.HtmlInputFile filename)
    {
        string attachName = Path.GetFileNameWithoutExtension(filename.PostedFile.FileName);
        string newFileName = DateTime.Now.ToFileTime().ToString();

        string attachExtension = Path.GetExtension(filename.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../import/"), newFileName + attachExtension);//合并两个路径为上传到服务器上的全路径

        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                return false;
            }
        }

        try
        {
            filename.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            return false;
        }



        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        path += newFileName + attachExtension;

        try
        {
            File.Move(SaveFileName, Server.MapPath(path));
        }
        catch
        {
            return false;
        }

        _fileName = attachName + attachExtension;
        _filePath = path;

        return true;
    }
}