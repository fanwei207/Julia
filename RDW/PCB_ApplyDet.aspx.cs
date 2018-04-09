using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using SampleManagement;
using System.Net.Mail;
using System.Text;
using System.Configuration;

public partial class RDW_PCB_ApplyDet : BasePage
{

    RDW_PCB helper = new RDW_PCB();
    Sample sap = new Sample();
    //guid生成函数
    private string getGUID()
    {
        System.Guid guid = new Guid();
        guid = Guid.NewGuid();
        string str = guid.ToString();
        return str;
    }

    private string PCB_ID
    {
        get
        {
            return ViewState["PCB_ID"].ToString();
        }
        set
        {
            ViewState["PCB_ID"] = value;
        }
    }
    /// <summary>
    /// 已创建
    /// 已提交
    /// 已关闭
    /// 已驳回
    /// 已生成打样单
    /// </summary>
    private string PCB_LAYOUTStatus
    {
        get
        {
            return ViewState["PCB_LAYOUTStatus"].ToString();
        }
        set
        {
            ViewState["PCB_LAYOUTStatus"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("350220", "PCB新增申请单权限");
            this.Security.Register("350230", "PCB打样单权限");
        }

        base.OnInit(e);
    }
    ////空间.Visible = this.Security["121000030"].isValid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PCB_LAYOUTStatus = string.Empty;
            if (Request["PCB_ID"] != null)
            {
                PCB_ID = Request["PCB_ID"].ToString();
                this.bind();
            }
            else
            {
                PCB_ID = getGUID();
                if (Request["mid"] != null)
                {
                    txtProjectNo.Text = Request["ProjectCode"].ToString();
                    txtProjectNo.Enabled = false;
                    helper.createApplyDet(PCB_ID, Session["uID"].ToString(), Session["uName"].ToString(), Request["ProjectCode"].ToString());
                }
                else
                {
                    helper.createApplyDet(PCB_ID, Session["uID"].ToString(), Session["uName"].ToString());
                }
                hidCreatedName.Value = Session["uName"].ToString();
                PCB_LAYOUTStatus = "已创建";
                
                
                
            }

            gvBind();

            if (PCB_LAYOUTStatus.Equals("已创建"))
            {
                

                btnReject.Visible = this.Security["350230"].isValid;
                btnSave.Visible = this.Security["350220"].isValid;
                btnSubmit.Visible = this.Security["350220"].isValid;
                tableSample.Visible = false;
                divReject.Visible = false;
                

            }
            else if (PCB_LAYOUTStatus.Equals("已驳回"))
            {

                btnReject.Visible = false;
                btnSave.Visible = this.Security["350220"].isValid;
                btnSubmit.Visible = this.Security["350220"].isValid;
                tableSample.Visible = false;
                
            }
            else if (PCB_LAYOUTStatus.Equals("已提交"))
            {
                applyBtn.Visible = false;//下方按钮区
                btnUpload.Visible = false;
                btnUploadLAYOUT.Visible = false;
                btnUploadPackage.Visible = false;
                btnUploadLayoutSelf.Visible = false;
                if (gvSize.Visible)
                {
                    gvSize.Columns[2].Visible = false;

                }
                if (gvLAYOUTBasis.Visible)
                {
                    gvLAYOUTBasis.Columns[2].Visible = false;

                }
                if (gvPackage.Visible)
                {
                    gvPackage.Columns[2].Visible = false;

                }
                if(gvLayout.Visible)
                {
                    gvLayout.Columns[2].Visible = false;
                }
 
                gvBosBind();



                btnReject.Visible = this.Security["350230"].isValid;
                btnSeveS.Visible = this.Security["350230"].isValid;
                btnSemp.Visible = this.Security["350230"].isValid;
            }
            else if (PCB_LAYOUTStatus.Equals("已生成打样单"))
            {
                applyBtn.Visible = false;//下方按钮区
                btnUpload.Visible = false;
                btnUploadLAYOUT.Visible = false;
                btnUploadPackage.Visible = false;
                btnUploadLayoutSelf.Visible = false;
                if (gvSize.Visible)
                {
                    gvSize.Columns[2].Visible = false;

                }
                if (gvLAYOUTBasis.Visible)
                {
                    gvLAYOUTBasis.Columns[2].Visible = false;

                }
                if (gvPackage.Visible)
                {
                    gvPackage.Columns[2].Visible = false;

                }
                if (gvLayout.Visible)
                {
                    gvLayout.Columns[2].Visible = false;
                }
                gvBosBind();



                divReject.Visible = false;
                btnSeveS.Visible = this.Security["350230"].isValid;
                btnSemp.Visible = this.Security["350230"].isValid;
            }
            else if (PCB_LAYOUTStatus.Equals("已删除"))
            {
                applyBtn.Visible = false;//下方按钮区
                btnUpload.Visible = false;
                btnUploadLAYOUT.Visible = false;
                btnUploadPackage.Visible = false;
                btnUploadLayoutSelf.Visible = false;
                if (gvSize.Visible)
                {
                    gvSize.Columns[2].Visible = false;

                }
                if (gvLAYOUTBasis.Visible)
                {
                    gvLAYOUTBasis.Columns[2].Visible = false;

                }
                if (gvPackage.Visible)
                {
                    gvPackage.Columns[2].Visible = false;

                }
                if (gvLayout.Visible)
                {
                    gvLayout.Columns[2].Visible = false;
                }
                gvBosBind();



                btnReject.Visible = false;
                btnSeveS.Visible = false;
                btnSemp.Visible = false;
            
            }

        }
    }



    private void bind()
    {
        SqlDataReader sdr = helper.selectApplyDetByID(PCB_ID);

        if (sdr.Read())
        {
            txtProductName.Text = sdr["PCB_ProductName"].ToString();
            txtPCBOldNo.Text = sdr["PCB_OldNo"].ToString();
            txtProjectNo.Text = sdr["PCB_ProjectCode"].ToString();
            //txtLineBasis.Text = sdr["PCB_LineBasis"].ToString();
            txtNum.Text = sdr["PCB_Num"].ToString();
            lbCreatedDate.Text = sdr["createdDate"].ToString();
            txtSampleDeliveryDate.Text = sdr["PCB_SampleDeliveryDate"].ToString();
            txtSize.Text = sdr["PCB_Size"].ToString();
            txtThickness.Text = sdr["PCB_Thickness"].ToString();

            PCB_LAYOUTStatus = sdr["PCB_LAYOUTStatus"].ToString();

            chklply.SelectedValue = sdr["PCB_Ply"].ToString();

            bindCHKL(chklMaterial, txtMaterial, sdr["PCB_Material"].ToString());
            bindCHKL(chklMachining, txtMachining, sdr["PCB_Machining"].ToString());
            chklRequirment.SelectedValue = sdr["PCB_Requirment"].ToString();

            bindCHKL(chklSoderResistPaint, txtSoderResistPaint, sdr["PCB_SolderResistPaint"].ToString());
            bindCHKL(chklLAYOUTBasis, txtLAYOUTBasis, sdr["PCB_LAYontBasis"].ToString());

            chklScreenParintingColour.SelectedValue = sdr["PCB_ScreenPrintingColour"].ToString();

            bindCHKL(chklCopperFoil, txtCopperFoil, sdr["PCB_CopperFoil"].ToString());
            bindCHKL(chklPackage, txtPackage, sdr["PCB_Package"].ToString());
            bindCHKL(chklSafety, txtSafety, sdr["PCB_Safety"].ToString());

            txtRemark.Text = sdr["PCB_Remark"].ToString();

            txtNo.Text = sdr["PCB_No"].ToString();
            txtVar.Text = sdr["PCB_Var"].ToString();
            txtNeedDate.Text = sdr["PCB_NeedDate"].ToString();
            lbDrawingName.Text = sdr["PCB_DrawingName"].ToString();

            hidCreatedBy.Value = sdr["createdBy"].ToString();
            hidCreatedName.Value = sdr["createdName"].ToString();
            hidCreatedDate.Value = sdr["createdDate"].ToString();

            txtReject.Text = sdr["PCB_RejectReason"].ToString();
        }

        sdr.Close();

    }

    private void bindCHKL(CheckBoxList chkl, TextBox txt, string strInfo)
    {
        int count = chkl.Items.Count;
       
        chkl.SelectedValue = strInfo;
        if (chkl.SelectedValue.Equals(string.Empty) && !strInfo.Equals(string.Empty))
        {

            chkl.SelectedIndex = count - 1;
            txt.Text = strInfo;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string flag = this.SavePCB("保存");
        if (flag.Equals("1"))
        {
            ltlAlert.Text = "alert('保存成功！');";
        }
        else
        {
            ltlAlert.Text =  flag  ;
        }

        string PCBI_Type = "外形尺寸";

        this.uploadForAll(PCBI_Type, fileSize);

        gvBind();


    }


    private void uploadForAll(string PCBI_Type, System.Web.UI.HtmlControls.HtmlInputFile filename)
    {
        string path = "/TecDocs/PCB/";
        string fileName = string.Empty;//原文件名
        string filePate = string.Empty;//文件路径+文件名（存储的）
        if (string.Empty.Equals(filename.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('上传路径不能为空');";
            return;

        }
        else
        {
            if (!ImportFile(ref fileName, ref filePate, path, filename))
            {
                ltlAlert.Text = "alert('上传文件失败，请联系管理员');";
                return;
            }
            //else 
            //{
            //    ltlAlert.Text = "alert('上传文件成功！');";
            //}
        }
        if (helper.uploadBasis(PCB_ID, fileName, filePate, PCBI_Type, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('上传文件成功！');";
            bind();
        }
        else
        {

            ltlAlert.Text = "alert('上传文件失败，请联系管理员！');";
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


    protected void btnSave_Click(object sender, EventArgs e)
    {



        string flag = this.SavePCB("保存");
        if (flag.Equals("1"))
        {
            ltlAlert.Text = "alert('保存成功！');";
        }
        else
        {
            ltlAlert.Text = flag;
        }

    }

    private string SavePCB(string type)
    {
        string productName = txtProductName.Text.Trim();
        string PCBOldNo = txtPCBOldNo.Text.Trim();
        string projectNo = txtProjectNo.Text.Trim();
       // string lineBasis = txtLineBasis.Text.Trim();
        string num = txtNum.Text.Trim();
        string sampleDeliveryDate = txtSampleDeliveryDate.Text.Trim();
        string size = txtSize.Text.Trim();
        string thickness = txtThickness.Text.Trim();

        string ply = chklply.SelectedValue.ToString().Trim();
        string material = getOther(chklMaterial, txtMaterial);
        string machining = getOther(chklMachining, txtMachining);
        string requirment = chklRequirment.SelectedValue.ToString().Trim();
        string soderResistPaint = getOther(chklSoderResistPaint, txtSoderResistPaint);

        string LAYOUTBasis = getOther(chklLAYOUTBasis, txtLAYOUTBasis);
        string screenParintingColour = chklScreenParintingColour.SelectedValue.ToString().Trim();
        string copperFoil = getOther(chklCopperFoil, txtCopperFoil);
        string package = getOther(chklPackage, txtPackage);
        string safety = getOther(chklSafety, txtSafety);
        string remark = txtRemark.Text.Trim();

        try
        {
            if (!num.Equals(string.Empty))
            {
                Convert.ToInt32(num);
            }

        }
        catch
        {
            return "alert('数量必须是大于0的正整数');";

        }

        try
        {
            if (!sampleDeliveryDate.Equals(string.Empty))
            {
                Convert.ToDateTime(sampleDeliveryDate);
            }

        }
        catch
        {
            return "alert('交付日期必须是日期格式yyyy-MM-dd');";

        }

        if (type == "提交")
        {
            if (productName.Equals(string.Empty))
            {
                return "alert('PCB品名不能为空');";

            }
            if (projectNo.Equals(string.Empty))
            {
                return "alert('项目号不能为空');";

            }
            //if (lineBasis.Equals(string.Empty))
            //{
            //    return "alert('线路依据不能为空');";

            //}
            if (num.Equals(string.Empty))
            {
                return "alert('样品数量不能为空');";

            }
            if (sampleDeliveryDate.Equals(string.Empty))
            {
                return "alert('交样日期不能为空');";

            }
            if (size.Equals(string.Empty) && gvSize.Rows.Count ==0)
            {
                return "alert('外形尺寸不能为空');";

            }
            if (thickness.Equals(string.Empty))
            {
                return "alert('PCB厚度不能为空');";

            }
            if (ply.Equals(string.Empty))
            {
                return "alert('PCB层数为空');";

            }
            if (material.Equals(string.Empty))
            {
                return "alert('PCB材质不能为空');";

            }
            if (machining.Equals(string.Empty))
            {
                return "alert('PCB处理不能为空');";

            }
            if (requirment.Equals(string.Empty))
            {
                return "alert('制程要求不能为空');";

            }
            if (soderResistPaint.Equals(string.Empty))
            {
                return "alert('防焊漆不能为空');";

            }
            if (LAYOUTBasis.Equals(string.Empty))
            {
                return "alert('LAYOUT依据不能为空');";

            }
            if (screenParintingColour.Equals(string.Empty))
            {
                return "alert('丝印颜色不能为空');";

            }
            if (copperFoil.Equals(string.Empty))
            {
                return "alert('铜箔厚度不能为空');";

            }
            if (package.Equals(string.Empty))
            {
                return "alert('新器件封装不能为空');";

            }
            if (safety.Equals(string.Empty))
            {
                return "alert('安规不能为空');";

            }


        }

        return helper.savePCB(productName, PCBOldNo, projectNo, ""/*lineBasis*/, num, sampleDeliveryDate, size, thickness, ply
                             , material, machining, requirment, soderResistPaint, LAYOUTBasis, screenParintingColour, copperFoil
                             , package, safety, remark, Session["uID"].ToString(), Session["uName"].ToString(), PCB_ID, type);


    }




    private string getOther(CheckBoxList chkl, TextBox txt)
    {
        int count = chkl.Items.Count;

        if (chkl.SelectedIndex == count - 1)
        {
            return txt.Text.Trim();
        }
        else
        {
            return chkl.SelectedValue.ToString().Trim();
        }


    }
    protected void gvSize_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvSize.PageIndex = e.NewPageIndex;
        gvBind();

    }

    private void gvBind()
    {
        DataSet ds = helper.selectUploadAll(PCB_ID);

        gvSize.DataSource = ds.Tables[0];
        gvSize.DataBind();

        gvLAYOUTBasis.DataSource = ds.Tables[1];
        gvLAYOUTBasis.DataBind();

        gvPackage.DataSource = ds.Tables[2];
        gvPackage.DataBind();

        gvLayout.DataSource = ds.Tables[3];
        gvLayout.DataBind();


        gvSize.Visible = gvSize.Rows.Count != 0;
        gvLAYOUTBasis.Visible = gvLAYOUTBasis.Rows.Count != 0;
        gvPackage.Visible = gvPackage.Rows.Count != 0;
        gvLayout.Visible = gvLayout.Rows.Count != 0;


    }
    protected void gvSize_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "lkbtndelete")
        {
            string PCBI_ID = e.CommandArgument.ToString();

            if (helper.deleteUploadByPCBIID(PCBI_ID, Convert.ToInt32(Session["uID"]), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('删除成功！');";
                gvBind();

            }
            else
            {

                ltlAlert.Text = "alert('删除失败！');";
            }

        }
        if (e.CommandName == "lkbtnView")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }
    protected void gvLAYOUTBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLAYOUTBasis.PageIndex = e.NewPageIndex;
        gvBind();
    }
    protected void gvLAYOUTBasis_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        gvSize_RowCommand(sender, e);
    }
    protected void gvPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPackage.PageIndex = e.NewPageIndex;
        gvBind();
    }
    protected void gvPackage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        gvSize_RowCommand(sender, e);
    }
    protected void btnUploadLAYOUT_Click(object sender, EventArgs e)
    {
        string flag = this.SavePCB("保存");
        if (flag.Equals("1"))
        {
            ltlAlert.Text = "alert('保存成功！');";
        }
        else
        {
            ltlAlert.Text = flag;
        }

        string PCBI_Type = "LAYOUT依据";

        this.uploadForAll(PCBI_Type, fileLAYOUTBasis);
        gvBind();
    }
    protected void btnUploadPackage_Click(object sender, EventArgs e)
    {

        string flag = this.SavePCB("保存");
        if (flag.Equals("1"))
        {
            ltlAlert.Text = "alert('保存成功！');";
        }
        else
        {
            ltlAlert.Text = flag;
        }

        string PCBI_Type = "新器件封装";

        this.uploadForAll(PCBI_Type, filePackage);
        gvBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string domain = string.Empty;

        string flag = this. SavePCB("提交",out domain);
        if (flag.Equals("1"))
        {
            ltlAlert.Text = "alert('提交成功！');";
            #region 发送邮件
            string toStr = string.Empty;
            string copyStr = string.Empty;

            if (!domain.Equals("SZX") && !domain.Equals("ZQL"))
            {
                domain = Session["PlantCode"].ToString();
            }

            if (domain.Equals("ZQL"))
            {
                helper.findEmail("F7FBCCBC-368C-40A9-8D3E-5FB8069E7B5B", out toStr, out copyStr);
            }
            else
            {
                helper.findEmail("2C3E6161-640B-43CE-8B9B-D1EF56639CF1", out toStr, out copyStr);
            }




            string from = Session["email"].ToString();
            string to = toStr;
            string copy = copyStr;
            string subject = "PCB申请需要您审批";
            string body = "";
            #region 写Body
            body += "<font style='font-size: 12px;'>PCB品名：" + txtProductName.Text.ToString() + "</font><br />";
            body += "<font style='font-size: 12px;'>申请人：" + hidCreatedName.Value.ToString() + "</font><br />";
            body += "<font style='font-size: 12px;'>项目编号：" + txtProjectNo.Text.ToString() + "</font><br />";
            // body += "<font style='font-size: 12px;'>是否同意变更Agree?： " + btnNotAgree.Text + "</font><br />";
            // body += "<font style='font-size: 12px;'>生效日期Excutive Date：" + labEffDate.Text + "</font><br />";
            body += "<br /><br />";
            body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
            body += "<font style='font-size: 12px;'>For details please visit "+baseDomain.getPortalWebsite()+" </font>";
            #endregion
            if (!this.SendEmail(from, to, copy, subject, body))
            {
                this.ltlAlert.Text = "alert('Email sending failure');";
            }
            else
            {
                this.ltlAlert.Text = "alert('Email sending');";
            }
            #endregion
            if (Request["mid"] == null)
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?PCB_ID=" + PCB_ID);
            }
            else
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
                + Request["mid"].ToString() 
                //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
                //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
                //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
                + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
                + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
                + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
                + "&PCB_ID=" + PCB_ID
                + "&rm=" + Request["rm"].ToString(),true);
            }
           
        }
        else if (flag.Equals("2"))
        {
            ltlAlert.Text = "alert('您填写的项目编号不存在，请查清再填写');";
        }
        else
        {
            ltlAlert.Text = flag;
        }

    }

    private string SavePCB(string type, out string domain)
    {
        domain = string.Empty;

        string productName = txtProductName.Text.Trim();
        string PCBOldNo = txtPCBOldNo.Text.Trim();
        string projectNo = txtProjectNo.Text.Trim();
        // string lineBasis = txtLineBasis.Text.Trim();
        string num = txtNum.Text.Trim();
        string sampleDeliveryDate = txtSampleDeliveryDate.Text.Trim();
        string size = txtSize.Text.Trim();
        string thickness = txtThickness.Text.Trim();

        string ply = chklply.SelectedValue.ToString().Trim();
        string material = getOther(chklMaterial, txtMaterial);
        string machining = getOther(chklMachining, txtMachining);
        string requirment = chklRequirment.SelectedValue.ToString().Trim();
        string soderResistPaint = getOther(chklSoderResistPaint, txtSoderResistPaint);

        string LAYOUTBasis = getOther(chklLAYOUTBasis, txtLAYOUTBasis);
        string screenParintingColour = chklScreenParintingColour.SelectedValue.ToString().Trim();
        string copperFoil = getOther(chklCopperFoil, txtCopperFoil);
        string package = getOther(chklPackage, txtPackage);
        string safety = getOther(chklSafety, txtSafety);
        string remark = txtRemark.Text.Trim();

        try
        {
            if (!num.Equals(string.Empty))
            {
                Convert.ToInt32(num);
            }

        }
        catch
        {
            return "alert('数量必须是大于0的正整数');";

        }

        try
        {
            if (!sampleDeliveryDate.Equals(string.Empty))
            {
                Convert.ToDateTime(sampleDeliveryDate);
            }

        }
        catch
        {
            return "alert('交付日期必须是日期格式yyyy-MM-dd');";

        }

        if (type == "提交")
        {
            if (productName.Equals(string.Empty))
            {
                return "alert('PCB品名不能为空');";

            }
            if (projectNo.Equals(string.Empty))
            {
                return "alert('项目号不能为空');";

            }
            //if (lineBasis.Equals(string.Empty))
            //{
            //    return "alert('线路依据不能为空');";

            //}
            if (num.Equals(string.Empty))
            {
                return "alert('样品数量不能为空');";

            }
            if (sampleDeliveryDate.Equals(string.Empty))
            {
                return "alert('交样日期不能为空');";

            }
            if (size.Equals(string.Empty) && gvSize.Rows.Count == 0)
            {
                return "alert('外形尺寸不能为空');";

            }
            if (thickness.Equals(string.Empty))
            {
                return "alert('PCB厚度不能为空');";

            }
            if (ply.Equals(string.Empty))
            {
                return "alert('PCB层数为空');";

            }
            if (material.Equals(string.Empty))
            {
                return "alert('PCB材质不能为空');";

            }
            if (machining.Equals(string.Empty))
            {
                return "alert('PCB处理不能为空');";

            }
            if (requirment.Equals(string.Empty))
            {
                return "alert('制程要求不能为空');";

            }
            if (soderResistPaint.Equals(string.Empty))
            {
                return "alert('防焊漆不能为空');";

            }
            if (LAYOUTBasis.Equals(string.Empty))
            {
                return "alert('LAYOUT依据不能为空');";

            }
            if (screenParintingColour.Equals(string.Empty))
            {
                return "alert('丝印颜色不能为空');";

            }
            if (copperFoil.Equals(string.Empty))
            {
                return "alert('铜箔厚度不能为空');";

            }
            if (package.Equals(string.Empty))
            {
                return "alert('新器件封装不能为空');";

            }
            if (safety.Equals(string.Empty))
            {
                return "alert('安规不能为空');";

            }


        }

        return helper.savePCB(productName, PCBOldNo, projectNo, ""/*lineBasis*/, num, sampleDeliveryDate, size, thickness, ply
                             , material, machining, requirment, soderResistPaint, LAYOUTBasis, screenParintingColour, copperFoil
                             , package, safety, remark, Session["uID"].ToString(), Session["uName"].ToString(), PCB_ID, type,out domain);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (Request["mid"] == null)
        {
            Response.Redirect("/RDW/PCB_ApplyMstr.aspx");
        }
        else
        {
            Response.Redirect("/RDW/PCB_ApplyMstr.aspx?ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
                + Request["mid"].ToString() 
                //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
                //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
                //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
                + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
                + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
                + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
                + "&rm=" + Request["rm"].ToString(),true);
        }
    }
    protected void btnSeveS_Click(object sender, EventArgs e)
    {
        string flag = sampSave("保存");

        if ("1".Equals(flag))
        {
            ltlAlert.Text = "alert('保存成功！');";
            if (Request["mid"] == null)
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?PCB_ID=" + PCB_ID);
            }
            else
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
                + Request["mid"].ToString() 
                //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
                //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
                //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
                + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
                + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
                + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
                + "&PCB_ID=" + PCB_ID
                + "&rm=" + Request["rm"].ToString() , true);
            }
        }
        else if (flag == "0")
        {
            ltlAlert.Text = "alert('保存失败！');";
        }
        else
        {
            ltlAlert.Text =  flag;
        }


    }

    private string sampSave(string type)
    {
        string no = txtNo.Text.Trim();
        string var = txtVar.Text.Trim();
        string needDate = txtNeedDate.Text.Trim();
        string uNo = "";
        string uDomain = "";


        if ((!txtUserNoInput.Text.Equals(string.Empty) && !txtUserNoInput.Text.Equals("No. OR Name"))|| !txtUserNo.Text.Equals(string.Empty))
        {
            uNo = txtUserNo.Text;
            uDomain = txtUserDomain.Text;
        }

        try
        {
            if (!needDate.Equals(string.Empty))
            {
                Convert.ToDateTime(needDate);
            }

        }
        catch
        {
            return "alert('需求日期必须是日期格式yyyy-MM-dd');";

        }

        if (type.Equals("生成打样单"))
        {
            if (no.Equals(string.Empty))
            {
                return "alert('PCB编号不能为空');";

            }
            if (var.Equals(string.Empty))
            {
                return "alert('PCB版本不能为空');";

            }
            if (needDate.Equals(string.Empty))
            {
                return "alert('需求日期不能为空');";

            }
            if ((lbDrawingName.Text.Equals(string.Empty)) && (txtUserNoInput.Text.Equals(string.Empty)))
            {
                return "alert('绘图者不能为空');";

            }

        }


        return helper.SaveSamp(PCB_ID, no, var, needDate, uNo, uDomain, type, Session["uID"].ToString(), Session["uName"].ToString());

    }


    private void gvBosBind()
    {
        gv_bos_mstr.DataSource = helper.selectBosMstrByPCBID(PCB_ID);
        gv_bos_mstr.DataBind();
    }

    protected void gv_bos_mstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_bos_mstr.PageIndex = e.NewPageIndex;
        gvBosBind();
    }
    protected void gv_bos_mstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_vendIsConfirm"].ToString().ToLower() == "true")
            {
                e.Row.Cells[6].Text = "已确认";
            }
            else
            {
                e.Row.Cells[6].Text = " ";
            }
            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_receiptIsConfirm"].ToString().ToLower() == "true")
            {
                e.Row.Cells[7].Text = "已收";
            }
            else
            {
                e.Row.Cells[7].Text = " ";
            }

            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_isCanceled"].ToString().ToLower() == "true")
            {
                e.Row.Cells[8].Text = "取消";
            }
            else
            {
                e.Row.Cells[8].Text = " ";
            }

            if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_isSendEmail"].ToString().ToLower() == "true")
            {
                LinkButton lnkEmail = (LinkButton)e.Row.FindControl("linkEmail");
                lnkEmail.Text = "再次发送";

                //如果这里已经确认了，就不用再发
                if (gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_vendIsConfirm"].ToString().ToLower() == "true")
                {
                    lnkEmail.Text = "";
                }
            }
            if(gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_vend"].ToString().Equals("pcb"))
            {
                e.Row.Cells[1].Text = " ";
                LinkButton lnkEmail = (LinkButton)e.Row.FindControl("linkEmail");
                lnkEmail.Text = "";
            }
            #region 对于完工日期晚于需求日期的日期现红

            string finishTimeAllStr = gv_bos_mstr.DataKeys[e.Row.RowIndex].Values["bos_completeDate"].ToString();

            if (!finishTimeAllStr.Equals(string.Empty))
            {
                string finishTimeStr = finishTimeAllStr.Substring(0, finishTimeAllStr.IndexOf(" ")).Replace("/", "-");

                try
                {
                    Convert.ToDateTime(finishTimeStr);
                }
                catch
                {
                    return;
                }

                try
                {
                    Convert.ToDateTime(txtNeedDate.Text.Trim());
                }
                catch
                {
                    return;
                }

                if (Convert.ToDateTime(finishTimeStr) > Convert.ToDateTime(txtNeedDate.Text.Trim()))
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                }
            }
            #endregion


        }




    }
    protected void gv_bos_mstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {
            string bosNbr = e.CommandArgument.ToString();
            if (Request["mid"] != null)
            {
                Response.Redirect("PCB_SampleDet.aspx?bos_nbr=" + bosNbr + "&PCB_ID=" + PCB_ID + "&Num=" + txtNum.Text.Trim() + "&needDate=" + txtNeedDate.Text.Trim() + "&ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
            + Request["mid"].ToString() 
            //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
            //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
            //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
            + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
            + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
            + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
            + "&rm=" + Request["rm"].ToString(), true);
            }
            else
            {
                Response.Redirect("PCB_SampleDet.aspx?bos_nbr=" + bosNbr + "&PCB_ID=" + PCB_ID + "&Num=" + txtNum.Text.Trim() + "&needDate=" + txtNeedDate.Text.Trim());
            }

        }
        else if (e.CommandName.ToString() == "SendDetail")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string bosnbr = gv_bos_mstr.Rows[index].Cells[0].Text.ToString();
            string bosVend = gv_bos_mstr.Rows[index].Cells[1].Text.ToString();
            string bosVendName = gv_bos_mstr.Rows[index].Cells[2].Text.ToString();
            string bosrmks = gv_bos_mstr.Rows[index].Cells[3].Text.ToString();
            SendVendEmail(bosnbr, bosVend, bosVendName, bosrmks);
            gvBosBind();
        }
    }

    private void SendVendEmail(string bosnbr, string bosVend, string bosVendName, string bosrmks)
    {
        bool SendEmail = false;
        string mailfrom = ConfigurationManager.AppSettings["AdminEmail"].ToString();
        string vendcode = bosVend;

        string mailto = sap.getBosVendEmail(vendcode);
        if (mailto == string.Empty)
        {
            this.Alert("供应商" + vendcode + "的邮箱地址未维护,请联系管理员进行维护");
            return;
        }

        string cc = "";
        string bosCreatedName = "";
        DataTable dt = sap.getBosnbrCreatedbyEmail(bosnbr);
        if (dt.Rows.Count > 0)
        {
            cc = dt.Rows[0]["email"].ToString();
            bosCreatedName = dt.Rows[0]["userName"].ToString();
        }

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(mailfrom, "TCP-China System");
        try
        {
            MailAddress addressto = new MailAddress(mailto, bosVendName);
            mail.To.Add(addressto);
            if (cc != string.Empty)
            {
                mail.CC.Add(new MailAddress(cc, bosCreatedName));
            }
            mail.Subject = "No Reply [打样单通知 - 新建], 单号为:" + bosnbr;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append("   " + bosVendName + "(" + vendcode + ")" + ",<br>");
            sb.Append("    &nbsp; &nbsp; 您好 !<br>");
            sb.Append("     <br>");
            sb.Append("    我司需向贵司下达打样单。详细信息如下：<br>");
            sb.Append("    打样单号:" + bosnbr + "<br>");
            sb.Append("    打样单备注:" + bosrmks + "<br>");
            sb.Append("     详细内容请登录我司供应链系统进行查看并确认<br>");
            sb.Append("           <a href='"+baseDomain.getSupplierWebsite()+"'target='_blank'>"+baseDomain.getSupplierWebsite()+"/</a><br>");
            sb.Append("         <br><p style='color:Gray; font-size:12px;'> 系统通知邮件,请勿回复</p><br>");
            sb.Append("         <br>");
            sb.Append("</form>");
            sb.Append("</html>");
            mail.Body = Convert.ToString(sb);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["EmailHost"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            SendEmail = true;
            sb.Remove(0, sb.Length);
        }
        catch
        {
            SendEmail = false;
        }

        if (SendEmail)
        {
            sap.updateBosMstrSendEmail(bosnbr);
            this.Alert("邮件发送成功");
            return;
        }
        else
        {
            this.Alert("邮件发送失败");
            return;
        }
    }
    protected void btnSemp_Click(object sender, EventArgs e)
    {
        string flag = sampSave("生成打样单");

        if (flag.Substring(0, 3).Equals("BOS"))
        {
            ltlAlert.Text = "alert('生成打样单成功！');";
            if (Request["mid"] == null)
            {
                Response.Redirect("/RDW/PCB_SampleDet.aspx?bos_nbr=" + flag + "&PCB_ID=" + PCB_ID + "&Num=" + txtNum.Text.Trim() + "&needDate=" + txtNeedDate.Text.Trim());

            }
            else
            {
                Response.Redirect("/RDW/PCB_SampleDet.aspx?bos_nbr=" + flag + "&PCB_ID=" + PCB_ID + "&Num=" + txtNum.Text.Trim() + "&needDate=" + txtNeedDate.Text.Trim() + "&ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
            + Request["mid"].ToString() 
            //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
            //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
            //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
            + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
            + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
            + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
            + "&rm=" + Request["rm"].ToString(), true);
            }
        }
        else if (flag == "0")
        {
            ltlAlert.Text = "alert('生成打样单失败！');";
        }
        else
        {
            ltlAlert.Text =  flag;
        }
    }
    protected void btnReturnS_Click(object sender, EventArgs e)
    {
        if (Request["mid"] == null)
        {
            Response.Redirect("/RDW/PCB_ApplyMstr.aspx");
        }
        else
        {
            Response.Redirect("/RDW/PCB_ApplyMstr.aspx?ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
                + Request["mid"].ToString() 
                //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
                //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
                //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
                + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
                + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
                + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
                + "&rm=" + Request["rm"].ToString() , true);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (helper.updateReject(PCB_ID,txtReject.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('驳回成功');";

            if (Request["mid"] == null)
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?PCB_ID=" + PCB_ID);
            }
            else
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
                + Request["mid"].ToString() 
                //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
                //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
                //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
                + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
                + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
                + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
                + "&PCB_ID=" + PCB_ID
                + "&rm=" + Request["rm"].ToString(), true);
            }
        }
        else
        {
            ltlAlert.Text = "alert('驳回失败');";
        }
    }
    protected void btnUploadLayoutSelf_Click(object sender, EventArgs e)
    {
        string flag = this.SavePCB("保存");
        if (flag.Equals("1"))
        {
            ltlAlert.Text = "alert('保存成功！');";
        }
        else
        {
            ltlAlert.Text = flag;
        }

        string PCBI_Type = "LAYOUT";

        this.uploadForAll(PCBI_Type, fileLayoutSelf);
        gvBind();
    }
}
