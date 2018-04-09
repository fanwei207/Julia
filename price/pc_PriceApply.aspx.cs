using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;



public partial class price_pc_PriceApply : BasePage
{

    
    /// <summary>
    /// 当前申请的状态
    /// </summary>
    private string Status
    {
        get
        {
            if (ViewState["Status"] == null)
            {
                ViewState["Status"] = "未提交";
            }
            return ViewState["Status"].ToString().Trim();
        }
        set
        {
            if ("0".Equals(value))
            {
                ViewState["Status"] = "未提交";
            }
            else if ("1".Equals(value))
            {
                ViewState["Status"] = "已提交";
            }
            else if ("-1".Equals(value))
            {
                ViewState["Status"] = "驳回";
            }
            else if ("2".Equals(value))
            {
                ViewState["Status"] = "已通过";
            }
            else
            {
                ViewState["Status"] = value;
            }

          
        }
    }
    /// <summary>
    /// 创建者ID
    /// </summary>
    private int AppliByID
    {
        get
        {
            if (ViewState["AppliByID"] == null)
            {
                ViewState["AppliByID"] = 0;
            }
            return Convert.ToInt32( ViewState["AppliByID"]);
        }
        set
        {
            if (value == 0)
            {
                ViewState["AppliByID"] = Convert.ToInt32(Session["uID"]);
            }
            else
            {
                ViewState["AppliByID"] = value;
            }
        }
    }


    private string DDLStatus
    {
        get
        {
            if (ViewState["DDLStatus"] == null)
            {
                ViewState["DDLStatus"] = "3";
            }
            return ViewState["DDLStatus"].ToString();
        }
        set
        {
            ViewState["DDLStatus"] = value;
        }
    }

    private bool ISOUT
    {
        get
        {
            if (ViewState["ISOUT"] == null)
            {
                ViewState["ISOUT"] = false;
            }
            return Convert.ToBoolean(ViewState["ISOUT"]);
        }
        set
        {
            ViewState["ISOUT"] = value;
        }
    }


    PC_price pc = new PC_price();//数据固化类

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("121000021", "报价核价新建页面权限");
            this.Security.Register("121000022", "追加申请供应商");
            this.Security.Register("121000030", "驳回权限");
            this.Security.Register("121000016", "修改描述权限");
            this.Security.Register("121000017", "技术部关闭申请");
        }

        base.OnInit(e);
    }
    ////空间.Visible = this.Security["121000030"].isValid;



    /// <summary>
    /// 1.未提交时申请人进入
    /// 2.未提交时其他人进入
    /// 3.提交后供应商开发部进入
    /// 4.提交后其他人进入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            

            try
            {

                if (Request["PQID"] != null)
                {
                    lbPQID.Text = Request["PQID"].ToString().Trim();
                    DDLStatus = Request["Status"].ToString().Trim();
                    ISOUT = Convert.ToBoolean(Request["isout"]);

                    if ("0".Equals(Request["Status"].ToString().Trim()) && !ISOUT)
                    {
                        txtTechnicalPrice.Visible = true;
                        lbTechnicalPrice.Visible = true;
                    }
                    
                }
            }
            catch
            {

            }
            pageBegin();


            bind();
        
        }
    }
    /// <summary>
    /// 页面新建的时候使用的方法
    /// </summary>
    private void pageBegin()
    {
        string status =string.Empty;
        string applyByName = string.Empty;
        int applyBy =0;
        string applyDate = string.Empty;
        pc.selectApplyPQInfo(lbPQID.Text, out status, out applyByName, out applyBy, out applyDate);
        Status = status;
        lbStatus.Text = Status;
        lbApplyBy.Text = applyByName;
        lbApplyDate.Text =applyDate;
        AppliByID = applyBy;
    }


   
    /// <summary>
    /// 增加供应商，需要验证供应商是否重复以及供应商是否正确
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddVender_Click(object sender, EventArgs e)
    {
        
        string vender = txtVender.Text.Trim().ToUpper();
        string qad =txtQad.Text.ToString().Trim();
        decimal technicalPrice = 0;
        long qadNo =0;
        string technicalPriceStr = txtTechnicalPrice.Text;

        if (string.Empty.Equals(vender) && vender.Length == 0)
        {
            ltlAlert.Text = "alert('供应商不能为空');";
            txtVender.Focus();
            return;
        }
        if (string.Empty.Equals(qad) && qad.Length == 0)
        {
            ltlAlert.Text = "alert('QAD号不能为空');";
            txtQad.Focus();
            return;
        }
        if(!long.TryParse(qad,out qadNo))
        {
             ltlAlert.Text = "alert('QAD号必须是数字');";
            txtQad.Focus();
            return;
        }
        if(qad.Length!=14)
        {
            ltlAlert.Text = "alert('QAD号必须是14位');";
            txtQad.Focus();
            return;
        }
        if (!string.Empty.Equals(technicalPriceStr))
        {
            if (!decimal.TryParse(txtTechnicalPrice.Text, out technicalPrice))
            {
                ltlAlert.Text = "alert('供应商参考价必须是数字');";
                txtQad.Focus();
                return;
            }
            if (txtTechnicalPrice.Text.Length >= 19)
            {
                ltlAlert.Text = "alert('供应商参考价必须小于19位');";
                txtQad.Focus();
                return;
            }
        }
        int flag = pc.addVender(txtFormat.Text.ToString().Trim(), vender, Convert.ToInt32(Session["uID"]), lbPQID.Text.ToString().Trim(), qad, ddlUM.SelectedItem.Text.ToString().Trim(), ddlType.SelectedItem.Value, ISOUT, technicalPriceStr);
        if (flag == 0)
        {
            ltlAlert.Text = "alert('供应商添加失败，请联系管理员');";
        }
        if (flag == 1)
        {
            ltlAlert.Text = "alert('供应商添加成功！');";
            txtVender.Text = "";
        }
        if (flag == 2)
        {
            ltlAlert.Text = "alert('输入的供应商不存在');";
        }
        if (flag == 3)
        {
            ltlAlert.Text = "alert('该QAD号该供应商已经被其他申请单申请');";
        }
        if (flag == 4)
        {
            ltlAlert.Text = "alert('QAD号不正确');";
        }
        if (flag == 5)
        {
            ltlAlert.Text = "alert('本单中存在相同记录');";
        }
        if (flag == 6)
        {
            ltlAlert.Text = "alert('当前申请中存在QAD的规格与当前输入不符');";
        }
        if (flag == 7)
        {
            ltlAlert.Text = "alert('本单中该QAD还没有规格或不与已存在的规格相同');";
        }
        if (flag == 8)
        {
            ltlAlert.Text = "alert('该QAD号没有申请，不可添加');";
        }
        if (flag == 9)
        {
            ltlAlert.Text = "alert('该QAD号和供应商的组合已经存在价格，申请修改请前往修改页面');";
        }    
        if (flag == 10)
        {
            ltlAlert.Text = "alert('当前申请中QAD的单位有不相同的');";
        }
        if (flag == 11)
        {
            ltlAlert.Text = "alert('当前申请中QAD号不在QAD系统中');";
        }
        if (flag == 12)
        {
            ltlAlert.Text = "alert('委外供应商号不存在，请先维护委外供应商号');";
        }
        if (flag == 13)
        {
            ltlAlert.Text = "alert('本单中存在相同QAD但技术指导价格不相同的项');";
        }
        
         bind();
     

    }




    protected void gvVender_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int infoFrom=Convert.ToInt32( ((Label)e.Row.FindControl("lbIsAppoint")).Text);
            if ("未提交".Equals(Status) /*|| "驳回".Equals(Status)*/)
            {
                if (Convert.ToInt32(Session["uID"]) != AppliByID)
                {
                    ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "";
                }
               // ((LinkButton)e.Row.Cells[2].FindControl("lkbtnDelete")).Attributes.Add("onclick", "return confirm('你确定要删除吗?');");
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "";
            }

            int index = e.Row.RowIndex;
            int status = Convert.ToInt32(gvVender.DataKeys[index].Values["status"]);

            if (("已提交".Equals(Status) || "已通过".Equals(Status)) && infoFrom != 1 && this.Security["121000022"].isValid && status<=1)
            {
                ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "删除";
            }
           
            if ("-1".Equals(((Label)e.Row.FindControl("lbDetStatue")).Text)) //已经驳回的
            {
                ((LinkButton)e.Row.FindControl("lkbtnUpdateDesc")).Visible = false;
                ((LinkButton)e.Row.FindControl("lkbtnAppvClose")).Visible = false;
                ((CheckBox)e.Row.FindControl("chk")).Checked = true;
                ((CheckBox)e.Row.FindControl("chk")).Enabled = false;
            }
            else if ("0".Equals(((Label)e.Row.FindControl("lbDetStatue")).Text) )//可以驳回的
            {
                ((CheckBox)e.Row.FindControl("chk")).Enabled = true;
                if (infoFrom == 1)
                {
                    ((LinkButton)e.Row.FindControl("lkbtnUpdateDesc")).Visible = this.Security["121000016"].isValid;
                    ((LinkButton)e.Row.FindControl("lkbtnAppvClose")).Visible = this.Security["121000017"].isValid;
                }
            
            }
            else if ("1".Equals(((Label)e.Row.FindControl("lbDetStatue")).Text))//不可以驳回的
            {

                ((CheckBox)e.Row.Cells[0].FindControl("chk")).Enabled = false;
                if (infoFrom == 1)
                {
                    ((LinkButton)e.Row.FindControl("lkbtnUpdateDesc")).Visible = this.Security["121000016"].isValid;
                    ((LinkButton)e.Row.FindControl("lkbtnAppvClose")).Visible = this.Security["121000017"].isValid;
                }
            }
            if (infoFrom == 1)
            {

                ((Label)e.Row.FindControl("lbIsAppoint")).Text = "是";
            }
            else
            {
                ((Label)e.Row.FindControl("lbIsAppoint")).Text = "";
                ((CheckBox)e.Row.FindControl("chk")).Enabled = false;
                ((LinkButton)e.Row.FindControl("lkbtnUpdateDesc")).Text = "";
                ((LinkButton)e.Row.FindControl("lkbtnAppvClose")).Text = "";
            }

            if (status <= -1)
            {
                ((LinkButton)e.Row.FindControl("lkbtnUpdateDesc")).Visible = false;
                ((LinkButton)e.Row.FindControl("lkbtnAppvClose")).Visible = false;
            }
        }
    }
    protected void gvVender_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnDelete")
        {
            int DetID=Convert.ToInt32(e.CommandArgument);
            if (pc.deleteVender(DetID))
            {
                ltlAlert.Text = "alert('删除成功');";
                bind();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败');";
            }

        }
        if (e.CommandName == "lkbtnSelectQADDOC")
        {
            Response.Redirect("pc_selectQadDoc.aspx?PQID=" + lbPQID.Text + "&QADDet=" + e.CommandArgument.ToString());
        }

        if (e.CommandName == "lkbtnUpdateDesc")
        {
             int rowIndex = int.Parse(e.CommandArgument.ToString());
             string part = gvVender.DataKeys[rowIndex].Values["QADNO"].ToString();
             string PQID = lbPQID.Text;
             string DetID = gvVender.DataKeys[rowIndex].Values["DetID"].ToString();
             int flag = pc.updateDesc(DetID, PQID, Convert.ToInt32(Session["uID"]));
            if (flag == 1)
            {
                ltlAlert.Text = "alert('申请成功');";
            }
            else if (flag == 0)
            {
                ltlAlert.Text = "alert('申请失败');";
            }
            else//-1
            {
                ltlAlert.Text = "alert('该申请已存在');";
            }
        }
        if (e.CommandName == "lkbtnAppvClose")
        {

            ltlAlert.Text = "$.window('review', '70%', '80%', '/price/pc_closeReasonAppv.aspx?DetID=" + e.CommandArgument.ToString() + "&createdID=" + AppliByID.ToString() + "', true)";

            //ltlAlert.Text = "window.showModalDialog('pc_closeReasonAppv.aspx?DetID=" + e.CommandArgument.ToString() + "', window, 'dialogHeight: 400px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
            //ltlAlert.Text += "window.location.href = 'pc_PriceApply.aspx?PQID=" + lbPQID.Text + "&DDLStatus=" + DDLStatus + "'";
        }
        
       
       // ltlHide.Text = "seeDown();";
    }

    ///// <summary>
    ///// 添加申请
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string QAD = txtQAD.Text.Trim();
    //    string reason = txtReason.Text.Trim();
    //    if (string.Empty.Equals(QAD) && QAD.Length == 0)
    //    {
    //        ltlAlert.Text = "alert('QAD号不能为空');";
    //        return;
    //    }
    //    try
    //    {
    //        ApplyMstrID = pc.addApplyMstr(QAD, reason,Convert.ToInt32( Session["uID"]));
    //    }
    //    catch
    //    {
    //        ltlAlert.Text = "alert('新建报价申请出现问题，请联系管理员');";
    //        return;
        
    //    }

    //    ltlAlert.Text = "alert('新建申请成功');";//seeDown();
    //    //btnSave.Visible = false;
     
    //    //txtQAD.Enabled = false;
    //    //btnChange.Visible = true;
    
    //    bind();
    //}
    /// <summary>
    /// 修改按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnChange_Click(object sender, EventArgs e)
    //{
    //    if (pc.changeReason(Convert.ToInt32(ApplyMstrID), Convert.ToInt32(Session["uID"]),txtReason.Text.Trim()))
    //    {
    //        ltlAlert.Text = "alert('修改成功');";
    //    }
    //    else 
    //    {
    //        ltlAlert.Text = "alert('修改失败');";
    //    }

    //}
    /// <summary>
    /// 绑定方法
    /// </summary>
    protected void bind()
    {



        if ("未提交".Equals(Status) )
        {
            gvVender.Columns[0].Visible = false;
            gvVender.Columns[9].Visible = false;
            if (Convert.ToInt32(Session["uID"]) == AppliByID && this.Security["121000021"].isValid)
            {
                btnSubmit.Enabled = true;
                btnAddVender.Enabled = true;
                ddlType.Enabled = true;
                

            }
            else
            {
                btnImport.Enabled = false;
                btnSubmit.Enabled = false;
                btnAddVender.Enabled = false;
                ddlType.Enabled = false;

            }
        }
        else//已提交的时候
        {

            if (this.Security["121000016"].isValid && this.Security["121000017"].isValid)
            {
                gvVender.Columns[9].Visible = true;
            }
            //if (Convert.ToInt32(Session["uID"]) != AppliByID)
            //{
            //    gvVender.Columns[9].Visible = false;
            //}
            ddlType.Enabled = false;
            if (this.Security["121000022"].isValid )
            {
                btnSubmit.Enabled = false;
                btnAddVender.Enabled = true;
            }
            else
            {
                btnSubmit.Enabled = false;
                btnAddVender.Enabled = false;

            }

        }
        //已提交或者驳回，有权限的
        if (("已提交".Equals(Status) || "驳回".Equals(Status) || "已通过".Equals(Status)))
        {
            divReject.Visible = true;
        }
        else
        {
            divReject.Visible = false;
        }
        if ("驳回".Equals(Status))
        {
            btnImport.Enabled = false;
            btnSubmit.Enabled = false;
            btnAddVender.Enabled = false;

            divReject.Visible = true;
            btnReject.Visible = false;
            btnPass.Visible = false;
            txtRejectReason.Enabled = false;
        }
        if ("已提交".Equals(Status) || "已通过".Equals(Status))
        {
            divReject.Visible = true;
            if (this.Security["121000030"].isValid)
            {
              
                btnReject.Visible = true;
                btnPass.Visible = true;
                txtRejectReason.Enabled = true;
                btnImport.Enabled = true;
            }
            else
            {
                btnReject.Visible = false;
                btnPass.Visible = false;
                txtRejectReason.Enabled = false;
                btnImport.Enabled = false;
            }
        }
        if ("已通过".Equals(Status))
        {
            divReject.Visible = true;
            btnPass.Enabled = false;
            btnAddVender.Enabled = false;
        }
        string rejectReason = string.Empty;
        string ddlItemValue = string.Empty;
        DataTable dt = pc.selectApplyDetList(lbPQID.Text.ToString(), out rejectReason, out ddlItemValue);
        gvVender.DataSource = dt;
        gvVender.DataBind();

        ddlType.SelectedValue = ddlItemValue;
        txtRejectReason.Text = rejectReason;
        if (!string.Empty.Equals(rejectReason))
        {
            btnExport.Enabled = true;
        }
        if (!ISOUT)
        {
            tbinsert.Visible = false;
        
        }

        

    }

    ///// <summary>
    ///// 修改原因
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void txtReason_TextChanged(object sender, EventArgs e)
    //{
    //    if (pc.changeReason(lbPQID.Text.ToString().Trim(), Convert.ToInt32(Session["uID"]), lbReason.Text.Trim()))
    //    {
    //        ltlAlert.Text = "alert('修改原因成功');";
    //    }
    //    else
    //    {
    //        ltlAlert.Text = "alert('修改原因失败');";
    //    }
    //}
    protected void btnreturn1_Click(object sender, EventArgs e)
    {
        Response.Redirect("pc_PriceApplyList.aspx?DDLStatus=" + DDLStatus);

    }
   
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string QAD = string.Empty;
        string vender = string.Empty;
        int flag = pc.PQSubmit(lbPQID.Text, Convert.ToInt32(Session["uID"]),ddlType.SelectedItem.Value, out QAD, out vender);
        if (flag==1)
        {
            ltlAlert.Text = "alert('提交成功');";
            //Response.Redirect("pc_PriceApplyList.aspx");
            Status = "1";
            lbStatus.Text = Status;
            btnSubmit.Enabled = false;
            btnAddVender.Enabled = false;
            bind();
        }
        else if(flag==2)
        {
            ltlAlert.Text = "alert('提交失败，存在已提交在报价流程中的该QAD:"+QAD+"与供应商:"+vender+"');";
        }
        else if (flag == 3)
        {
            ltlAlert.Text = "alert('请添加供应商');";
        }
        else
        {
            ltlAlert.Text = "alert('提交失败');";
        }
        
    }

    protected void gvVender_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVender.PageIndex = e.NewPageIndex;
        bind();
    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    string InfoFrom ="";
    //    if (this.Security["121000022"].isValid && ("未提交".Equals(Status) || "驳回".Equals(Status)))
    //    {
    //        InfoFrom = "1";
    //    }
    //    if (this.Security["121000021"].isValid && "已提交".Equals(Status))
    //    {
    //        InfoFrom = "0";
    //    }



    //    Response.Redirect("pc_ImportAppplyDet.aspx?PQID=" + lbPQID.Text.ToString() + "&Status=" + Status + "&uName=" + lbApplyBy.Text + "&ApplyDate=" + lbApplyDate.Text + "&AppliByID=" + AppliByID + "&InfoFrom=" + InfoFrom);
    //}
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string InfoFrom = "";
        if (this.Security["121000021"].isValid && ("未提交".Equals(Status)))
        {
            InfoFrom = "1";
        }
        if (this.Security["121000022"].isValid && ("已提交".Equals(Status) || "已通过".Equals(Status)))
        {
            InfoFrom = "0";
        }
        ImportExcelFile(InfoFrom);
        bind();
    }

    public void ImportExcelFile(string InfoFrom)
    {
        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {            
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }
        }
        
        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    string message = "";
                    bool success = pc.applyImport(strFileName, strUID, lbPQID.Text, out message, InfoFrom,ddlType.SelectedItem.Value,ISOUT);//插入，
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {
                        DataTable errDt = pc.GetApplyImportError(strUID);//输出错误信息
                        string title = "100^<b>QAD</b>~^100^<b>vendor</b>~^100^<b>um</b>~^100^<b>specifications</b>~^100^<b>technicalPrice</b>~^100^<b>错误信息</b>~^";
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式a');";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    { 
        
        int count = 0;
        StringBuilder parts = new StringBuilder();
        foreach (GridViewRow row in gvVender.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;

            string DetID = gvVender.DataKeys[row.RowIndex].Values["DetID"].ToString();
            if (chk.Checked && chk.Enabled)
            {
                parts.Append(DetID).Append(";");
                count++;
            }
        }

        if (!string.Empty.Equals(txtRejectReason.Text.Trim()))
        {
            if (pc.updateReject(lbPQID.Text, Convert.ToInt32(Session["uID"]), txtRejectReason.Text.ToString().Trim(), parts.ToString()))
            {
                ltlAlert.Text = "alert('驳回成功');";
                pc.sendMailForReject(lbPQID.Text, Convert.ToInt32(Session["uID"]));
                pageBegin();
                bind();
                

            }
            else
            {
                ltlAlert.Text = "alert('驳回失败');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('驳回原因不能为空');";
        
        }
    }
    protected void btnPass_Click(object sender, EventArgs e)
    {
        if (pc.updatePassApply(lbPQID.Text, Convert.ToInt32(Session["uID"])))
        {
            ltlAlert.Text = "alert('通过成功');";
            pageBegin();
            bind();
        }
        else
        {
            ltlAlert.Text = "alert('通过失败');";
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable errDt = pc.selectApplyDetExport(lbPQID.Text);//输出错误信息
        string title = "100^<b>QAD</b>~^100^<b>vendor</b>~^100^<b>um</b>~^100^<b>specifications</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
    }
}