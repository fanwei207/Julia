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

public partial class price_pcm_PriceApply : BasePage
{

    private int SubmitCount
    {
        get 
        {
            if (ViewState["SubmitCount"] == null)
            {
                ViewState["SubmitCount"] = 0;
            }
            return Convert.ToInt32(ViewState["SubmitCount"]);
        }
        set 
        {
            ViewState["SubmitCount"] = value;
        }
    
    }
    private bool ISOUT
    {
        get
        {
            if (ViewState["ISOUT"] == null)
            {
                ViewState["ISOUT"] = false ;
            }
            return Convert.ToBoolean(ViewState["ISOUT"]);
        }
        set
        {
            ViewState["ISOUT"] = value;
        }
    }
    
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


    PCM_price pc = new PCM_price();//数据固化类

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("122000021", "报价核价新建页面权限");
            this.Security.Register("122000022", "追加申请供应商");
            this.Security.Register("122000030", "驳回权限");
            this.Security.Register("122000016", "修改描述权限");
            this.Security.Register("122000017", "技术部关闭申请");
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

    protected void gvVender_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //int infoFrom=Convert.ToInt32( ((Label)e.Row.FindControl("lbIsAppoint")).Text);
            if ("未提交".Equals(Status) /*|| "驳回".Equals(Status)*/)
            {
                if (Convert.ToInt32(Session["uID"]) != AppliByID)
                {
                    ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "";
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "删除";
                }
               // ((LinkButton)e.Row.Cells[2].FindControl("lkbtnDelete")).Attributes.Add("onclick", "return confirm('你确定要删除吗?');");
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "";
                btnImport.Enabled = false;
            }

            int index = e.Row.RowIndex;
            int status = Convert.ToInt32(gvVender.DataKeys[index].Values["status"]);

            //if (("已提交".Equals(Status) || "已通过".Equals(Status)) &&  status <= 4 && status >= 0 )
            //{
            //    ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "删除";
            //}
           

            if ("-1".Equals(((Label)e.Row.FindControl("lbDetStatue")).Text)) //已经驳回的
            {
                ((CheckBox)e.Row.FindControl("chk")).Checked = true;
                ((CheckBox)e.Row.FindControl("chk")).Enabled = false;

                btnExport.Enabled = true;
                
        
            }
            else if ("0".Equals(((Label)e.Row.FindControl("lbDetStatue")).Text) )//可以驳回的
            {
                ((CheckBox)e.Row.FindControl("chk")).Enabled = true;
            
            }
            else if ("1".Equals(((Label)e.Row.FindControl("lbDetStatue")).Text))//不可以驳回的
            {

                ((CheckBox)e.Row.Cells[0].FindControl("chk")).Enabled = false;
               
                   
                
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
                SubmitCount = 0;
                bind();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败');";
            }

        }
        if (e.CommandName == "lkbtnSelectQADDOC")
        {
            ltlAlert.Text = "$.window('文档查看', 600, 800,'./price/pcm_selectQadDoc.aspx?QADDet=" + e.CommandArgument.ToString()+"')";
            //Response.Redirect("pcm_selectQadDoc.aspx?PQID=" + lbPQID.Text + "&QADDet=" + e.CommandArgument.ToString());
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
            ltlAlert.Text = "window.showModalDialog('pc_closeReasonAppv.aspx?DetID=" + e.CommandArgument.ToString() + "', window, 'dialogHeight: 400px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
            ltlAlert.Text += "window.location.href = 'pc_PriceApply.aspx?PQID=" + lbPQID.Text + "&DDLStatus=" + DDLStatus + "'";
        }

        if (e.CommandName == "lkbtnHist")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string QAD = gvVender.DataKeys[rowIndex].Values["QADNO"].ToString();
            string vender = gvVender.DataKeys[rowIndex].Values["vender"].ToString();
            ltlAlert.Text = "$.window('历史价格查看', 600, 800,'./price/pcm_HistoryPriceByQADAndVender.aspx?QAD=" + QAD + "&vender=" + vender + "')";
        }
        
       
       // ltlHide.Text = "seeDown();";
    }


    /// <summary>
    /// 绑定方法
    /// </summary>
    protected void bind()
    {



        if ("未提交".Equals(Status) )
        {
            
            gvVender.Columns[0].Visible = false;
            gvVender.Columns[10].Visible = false;
            if (Convert.ToInt32(Session["uID"]) == AppliByID && this.Security["122000021"].isValid)
            {

                btnSubmit.Enabled = true;
            
                ddlType.Enabled = true;
                

            }
            else
            {
                btnImport.Enabled = false;
                btnSubmit.Enabled = false;
        
                ddlType.Enabled = false;

            }
            
        }
        else//已提交的时候
        {
            btnImport.Enabled = false;
            btnSubmit.Enabled = false;

        }
        //已提交或者驳回，有权限的
        if (("已提交".Equals(Status) || "驳回".Equals(Status) || "已通过".Equals(Status)))
        {
            //divReject.Visible = true;
        }
        else
        {
            divReject.Visible = false;
        }
        if ("驳回".Equals(Status))
        {
            btnImport.Enabled = false;
            btnSubmit.Enabled = false;
        

            //divReject.Visible = true;
            btnReject.Visible = false;
           
            txtRejectReason.Enabled = false;
        }
        if ("已提交".Equals(Status) || "已通过".Equals(Status))
        {
            if (this.Security["122000030"].isValid)
            {
                //divReject.Visible = true;
                btnReject.Visible = true;
                txtRejectReason.Enabled = true;
                btnImport.Enabled = true;
            }
            else
            {
                divReject.Visible = false;
                btnReject.Visible = false;
             
                txtRejectReason.Enabled = false;
                btnImport.Enabled = false;
            }
        }
        string rejectReason = string.Empty;
        string ddlItemValue = string.Empty;

        DataTable dt = pc.selectApplyDetList(lbPQID.Text.ToString(), out rejectReason, out ddlItemValue);
        gvVender.DataSource = dt;
        gvVender.DataBind();

        ddlType.SelectedValue = ddlItemValue;
        txtRejectReason.Text = rejectReason;



    }


    protected void btnreturn1_Click(object sender, EventArgs e)
    {
        Response.Redirect("pcm_PriceApplyList.aspx?DDLStatus=" + DDLStatus);

    }
   
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        string QAD = string.Empty;
        string vender = string.Empty;
        int flag = pc.PQSubmit(lbPQID.Text, Convert.ToInt32(Session["uID"]),ddlType.SelectedItem.Value, out QAD, out vender,SubmitCount);
        if (flag==1)
        {

            pageBegin();
            bind();
            btnSubmit.Enabled = false;
          
        }
        else if(flag==2)
        {
            ltlAlert.Text = "alert('确认失败，存在已确认在报价流程中的该QAD:" + QAD + "与供应商:" + vender + "');";
        }
        else if (flag == 3)
        {
            ltlAlert.Text = "alert('确认失败，确认必须要有可以被确认的条目和相关的依据才能确认！');";
        }
        else if (flag == 4)
        {
            ltlAlert.Text = "alert('确认等待，申请中存在价格开始时间超过今天开始的前后30天，若检查没有问题则再次点击“确认”即可');";
            SubmitCount  = 1;
        }
        else
        {
            ltlAlert.Text = "alert('确认失败');";
        }
        
      

        
    }

    protected void gvVender_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVender.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string InfoFrom = "";
        SubmitCount = 0;
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

                DataTable dt = null;
               
                try
                {
                    //dt = adam.getExcelContents(filePath).Tables[0];
                    //NPOIHelper helper = new NPOIHelper();
                    dt = this.GetExcelContents(strFileName);
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式');";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                try
                {
                    string message = "";
                    //
                    bool isExcel = pc.applyImport(strFileName, strUID, lbPQID.Text, out message, "1",ddlType.SelectedItem.Value,dt,ISOUT);//插入，
                    if (isExcel)
                    {
                        ltlAlert.Text = "window.open('/Excel/" + message + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('" + message + "')";
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
            

            if (chk.Checked)
            {
                int index = row.RowIndex;
                parts.Append(gvVender.DataKeys[index].Values["DetId"].ToString()).Append(";");
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable errDt = pc.selectApplyDetExport(lbPQID.Text);//输出错误信息
        string title = "100^<b>QAD</b>~^100^<b>vendor</b>~^100^<b>um</b>~^100^<b>rejectReason</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
    }

    

    //private void chk_OnCheckedChanged(object sender, EventArgs e)
    //{ 
    //    CheckBox chk = (CheckBox)sender;
    //    int index = ((GridViewRow)((chk.NamingContainer))).RowIndex;
    //    string  QAD =gvVender.DataKeys[index].Values["QADNO"].ToString();
    //    string vender = gvVender.DataKeys[index].Values["vender"].ToString();

    //    if (pc.addVenderForModifiy(QAD, vender, lbPQID.Text, Convert.ToInt32(Session["uID"])))
    //    {
    //        ltlAlert.Text = "alert('保存成功');";
    //        bind();
    //    }
    //    else
    //    {
    //        ltlAlert.Text = "alert('保存失败请联系管理员');";
    //    }

    //}
    protected void lkbtnDownList_Click(object sender, EventArgs e)
    {


        string title = "160^<b>QAD</b>~^100^<b>供应商</b>~^200^<b>需求规格</b>~^80^<b>采购单位</b>~^"
        + "80^<b>基本单位</b>~^80^<b>转换因子</b>~^80^<b>币种</b>~^100^<b>当前最低价格</b>~^100^<b>当前价格</b>~^100^<b>当前最高价格</b>~^100^<b>当前是否可抵扣</b>~^" +
        "100^<b>当前税率</b>~^100^<b>当前价格起始时间</b>~^100^<b>当前价格终止时间</b>~^80^<b>期望价格</b>~^"+
        "100^<b>期望是否可抵扣</b>~^100^<b>期望税率</b>~^100^<b>期望价格起始时间</b>~^100^<b>期望价格终止时间</b>~^"+
        "100^<b>供应商名</b>~^200^<b>验证ID(请勿修改)</b>~^";

        string[] titleSub = title.Split(new char[] { '~' });   

        DataTable dtExcel = new DataTable("temp");
        DataColumn col;

        foreach (string colName in titleSub)
        {
            col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = colName;
            dtExcel.Columns.Add(col);
        }

        ExportExcel(title, dtExcel, false);
    }
    protected void btnBasis_Click(object sender, EventArgs e)
    {
        Response.Redirect("pcm_ViewBasis.aspx?IMID=" + lbPQID.Text + "&Status=" +Request["Status"].ToString().Trim() + "&isout=" +Request["isout"].ToString());
    }
}