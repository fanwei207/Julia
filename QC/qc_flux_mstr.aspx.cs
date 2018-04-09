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
using QCProgress;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;

public partial class QC_qc_flux_mstr : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

     protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("100105140", "编辑信息权限");
        }

        base.OnInit(e);
    }

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropStatu.SelectedIndex = 1;

            GridViewBind();

            checkAuth();
        }
        else
        {
            ogv.ResetGridView(gvFluxMstr);
        }
    }

    private void checkAuth()
    {
        if (!this.Security["100105140"].isValid)
        {
            btnAdd.Visible = false;
            trupload1.Visible = false;
            trupload2.Visible = false;
            trupload3.Visible = false;
            gvFluxMstr.Columns[21].Visible = false;
            gvFluxMstr.Columns[22].Visible = false;
        }
    }
    /// <summary>
    /// 根据txtID重新绑定头拦
    /// </summary>
    private void bindHard()
    {
        SqlDataReader sdr = oqc.SelectFlux(txtID.Text.Trim());
        if (sdr.Read())
        {
            txtProduct.Text = sdr["fl_product"].ToString();
            txtQad.Text = sdr["fl_qad"].ToString();
            txtOldPro.Text = sdr["fl_oldpro"].ToString();
            txtOldQad.Text = sdr["fl_oldqad"].ToString();
            txtPo.Text = sdr["fl_po"].ToString();
            txtCustomer.Text = sdr["fl_cust"].ToString();
            txtWo.Text = sdr["fl_wo"].ToString();
            txtProvider.Text = sdr["fl_provider"].ToString();
            txtLamp.Text = sdr["fl_lamp"].ToString();
            txtSeries.Text = sdr["fl_series"].ToString();
            txtPeriod.Text = sdr["fl_period"].ToString();
            txtVersion.Text = sdr["fl_version"].ToString();
            txtDept.Text = sdr["fl_dept"].ToString();
            txtTemp.Text = sdr["fl_temp"].ToString();
            txtAging.Text = sdr["fl_aging"].ToString();
            txtDate.Text = sdr["fl_date"].ToString();
            txtTester.Text = sdr["fl_tester"].ToString();
            dropStatu.SelectedValue = sdr["fl_statu"].ToString();
            ddlType.SelectedValue = sdr["fl_type"].ToString();
            txtRemarks.Text = sdr["fl_rmks"].ToString();

           
        }

        sdr.Dispose();
        sdr.Close();

        btnAdd.Text = "保存";
        btnClear.Text = "取消";
    }

    private void GridViewBind() 
    {
        DataTable table = oqc.SelectFlux(txtProduct.Text.Trim(), txtQad.Text.Trim(), txtOldPro.Text.Trim(), txtOldQad.Text.Trim(), txtPo.Text.Trim(), txtCustomer.Text.Trim(), txtWo.Text.Trim(), txtProvider.Text.Trim(),
                                         txtLamp.Text.Trim(), txtSeries.Text.Trim(), txtPeriod.Text.Trim(), txtVersion.Text.Trim(), txtDept.Text.Trim(), txtTemp.Text.Trim(),
                                         txtAging.Text.Trim(), txtDate.Text.Trim(),txtDate1.Text.Trim(), txtTester.Text.Trim(), txtRemarks.Text.Trim(), dropStatu.SelectedIndex == 0 ? string.Empty : dropStatu.SelectedValue
                                         ,  ddlType.SelectedValue);

        ogv.GridViewDataBind(gvFluxMstr, table);

        table.Dispose();
    }
    
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        txtID.Text = string.Empty;
        btnAdd.Text = "添加";
        btnClear.Text = "清空";

        GridViewBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtProduct.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('产品型号不能为空');Form1.txtProduct.focus();";
            return;
        }

        if (txtQad.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('QAD号不能为空');Form1.txtQad.focus();";
            return;
        }

        if (txtOldPro.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('裸灯号不能为空');Form1.txtOldPro.focus();";
            return;
        }

        if (txtOldQad.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('裸灯QAD号不能为空');Form1.txtOldQad.focus();";
            return;
        }

        if (txtPo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('订单号不能为空');Form1.txtPo.focus();";
            return;
        }

        if (txtCustomer.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('客户不能为空');Form1.txtCustomer.focus();";
            return;
        }

        if (txtWo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('加工单号不能为空');Form1.txtWo.focus();";
            return;
        }

        if (txtProvider.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('供应商不能为空');Form1.txtProvider.focus();";
            return;
        }

        //if (txtLamp.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('灯管号不能为空');Form1.txtLamp.focus();";
        //    return;
        //}

        //if (txtSeries.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('批次号不能为空');Form1.txtSeries.focus();";
        //    return;
        //}

        if (txtPeriod.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('生产周期不能为空');Form1.txtPeriod.focus();";
            return;
        }

        if (txtVersion.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('电子版号不能为空');Form1.txtVersion.focus();";
            return;
        }

        if (txtDept.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('送测部门不能为空');Form1.txtDept.focus();";
            return;
        }

        //if (txtTemp.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('临时老化台号不能为空');Form1.txtTemp.focus();";
        //    return;
        //}

        //if (txtAging.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('长点老化台号不能为空');Form1.txtAging.focus();";
        //    return;
        //}

        if (txtDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('测试日期不能为空');Form1.txtDate.focus();";
            return;
        }
        else 
        {
            try
            {
                DateTime dd = DateTime.Parse(txtDate.Text.Trim());
            }
            catch 
            {
                ltlAlert.Text = "alert('测试日期格式不正确');Form1.txtDate.focus();";
                return;
            }
        }

        int nPlantid = int.Parse(Session["PlantCode"].ToString());

        if (txtTester.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('测试员不能为空');Form1.txtTester.focus();";
            return;
        }
        else if (!oqc.CheckUser(nPlantid, txtTester.Text.Trim())) 
        {
            ltlAlert.Text = "alert('测试员不存在');Form1.txtTester.focus();";
            return;
        }

        if (dropStatu.SelectedIndex == 0) 
        {
            ltlAlert.Text = "alert('请选择一项状态');Form1.dropStatu.focus();";
            return;
        }

        if (ddlType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项类型');Form1.ddlType.focus();";
            return;
        }

        FuncErrType error = FuncErrType.操作成功;
        int uID = int.Parse(Session["uID"].ToString());
        int nID = 0;

        if (txtID.Text.Trim() == string.Empty)
        {
            error = oqc.InsertFlux(txtProduct.Text.Trim(), txtQad.Text.Trim(), txtOldPro.Text.Trim(), txtOldQad.Text.Trim(), txtPo.Text.Trim(), txtCustomer.Text.Trim(), txtWo.Text.Trim(), 
                                   txtProvider.Text.Trim(),txtLamp.Text.Trim(), txtSeries.Text.Trim(), txtPeriod.Text.Trim(), txtVersion.Text.Trim(), txtDept.Text.Trim(), txtTemp.Text.Trim(),
                                   txtAging.Text.Trim(), txtDate.Text.Trim(), txtTester.Text.Trim(), dropStatu.SelectedValue, txtRemarks.Text.Trim(), uID, ref nID, ddlType.SelectedValue);

            if (error == FuncErrType.操作成功) 
            {
                txtID.Text = nID == 0 ? string.Empty : nID.ToString();

                if (txtFile.Name != string.Empty)
                    this.btnUploadTxt_ServerClick();
                if (excelFile.Name != string.Empty)
                    this.btnUploadExcel_ServerClick();
                if (filename.Name != string.Empty)
                    this.btnUplodeFile_Click(this, new EventArgs());
                    
            }
        }
        else 
        {
            int ID = int.Parse(txtID.Text.Trim());

            error = oqc.UpdateFlux(ID, txtProduct.Text.Trim(), txtQad.Text.Trim(), txtOldPro.Text.Trim(), txtOldQad.Text.Trim(), txtPo.Text.Trim(), txtCustomer.Text.Trim(), txtWo.Text.Trim(), 
                                   txtProvider.Text.Trim(), txtLamp.Text.Trim(), txtSeries.Text.Trim(), txtPeriod.Text.Trim(), txtVersion.Text.Trim(), txtDept.Text.Trim(), txtTemp.Text.Trim(),
                                   txtAging.Text.Trim(), txtDate.Text.Trim(), txtTester.Text.Trim(), dropStatu.SelectedValue, txtRemarks.Text.Trim(), uID,ddlType.SelectedValue);
        }

        if (error != FuncErrType.操作成功)
            ltlAlert.Text = "alert('操作失败');";
        else
        {
            ltlAlert.Text = "alert('操作成功');";
            ClearInfo();
        }

        GridViewBind();
    }
    protected void ClearInfo() 
    {
        txtID.Text = string.Empty;
        txtProduct.Text = string.Empty;
        txtQad.Text = string.Empty;
        txtOldPro.Text = string.Empty;
        txtOldQad.Text = string.Empty;
        txtPo.Text = string.Empty;
        txtCustomer.Text = string.Empty;
        txtWo.Text = string.Empty;
        txtProvider.Text = string.Empty;
        txtLamp.Text = string.Empty;
        txtSeries.Text = string.Empty;
        txtPeriod.Text = string.Empty;
        txtVersion.Text = string.Empty;
        txtDept.Text = string.Empty;
        txtTemp.Text = string.Empty;
        txtAging.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtTester.Text = string.Empty;
        dropStatu.SelectedIndex = 0;
        txtRemarks.Text = string.Empty;
        ddlType.SelectedIndex = 0;
        txtDate1.Text = string.Empty;

        txtFile.Name = string.Empty;
        excelFile.Name = string.Empty;

        btnAdd.Text = "添加";
        btnClear.Text = "清空";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearInfo();

        GridViewBind();
    }

    /// <summary>
    /// 用于被调用的上传方法
    /// </summary>
    protected void btnUploadTxt_ServerClick()
    {
        if (txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择一项纪录,然后再上传');";
            return;
        }

        if (txtHour.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('测试时间不能为空');";
            return;
        }
        else
        {
            if (!Regex.IsMatch(txtHour.Text.Trim(), @"^\d+$"))
            {
                ltlAlert.Text = "alert('测试时间格式不对');";
                return;
            }
        }
        if (dropWay.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项点燃方式');";
            return;
        }

        FileOperate fop = new FileOperate(txtFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.TXT;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());

            fop.FluxImportTxt(id, txtHour.Text.Trim(), dropWay.SelectedValue, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }
            oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            ltlAlert.Text = "alert('导入数据成功!')";
        }
    }

    /// <summary>
    /// 上传详情的方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadTxt_ServerClick(object sender, EventArgs e)
    {
        if (txtID.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('请选择一项纪录,然后再上传');";
            return;
        }

        if (txtHour.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('测试时间不能为空');";
            return;
        }
        else 
        {
            if (!Regex.IsMatch(txtHour.Text.Trim(), @"^\d+$"))
            {
                ltlAlert.Text = "alert('测试时间格式不对');";
                return;
            }
        }
        if (dropWay.SelectedIndex == 0) 
        {
            ltlAlert.Text = "alert('请选择一项点燃方式');";
            return;
        }

        FileOperate fop = new FileOperate(txtFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.TXT;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());

            fop.FluxImportTxt(id, txtHour.Text.Trim(), dropWay.SelectedValue, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }
            oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            bindHard();
            GridViewBind();
     
            ltlAlert.Text = "alert('导入数据成功!')";
        }
    }

    /// <summary>
    /// 被调用的上传excel的方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadExcel_ServerClick()
    {
        if (txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择一项纪录,然后再上传');";
            return;
        }

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

        strUserFileName = excelFile.PostedFile.FileName;
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


        if (excelFile.PostedFile != null)
        {
            if (excelFile.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                excelFile.PostedFile.SaveAs(strFileName);//上传 文件
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


                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
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



        FileOperate fop = new FileOperate(excelFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.XLS;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());
            fop.FluxImportExcel(id, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }

            oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
           
            ltlAlert.Text = "alert('导入数据成功!')";
        }
    }

    /// <summary>
    /// 实际上传excel文件的方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadExcel_ServerClick(object sender, EventArgs e)
    {
        if (txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择一项纪录,然后再上传');";
            return;
        }

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

        strUserFileName = excelFile.PostedFile.FileName;
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


        if (excelFile.PostedFile != null)
        {
            if (excelFile.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                excelFile.PostedFile.SaveAs(strFileName);//上传 文件
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
                    
                    
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
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



        FileOperate fop = new FileOperate(excelFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.XLS;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());
            fop.FluxImportExcel(id, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }

            oqc.updateQCFluxStatusByID(txtID.Text.Trim(),Session["uID"].ToString(),Session["uName"].ToString());
            bindHard();
            GridViewBind();
            ltlAlert.Text = "alert('导入数据成功!')";
        }
    }


    protected void gvFluxMstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Cust_Edit") 
        {
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;

            txtID.Text = e.CommandArgument.ToString();
            txtProduct.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[0].Text.Trim());
            txtQad.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[1].Text.Trim());
            txtOldPro.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[2].Text.Trim());
            txtOldQad.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[3].Text.Trim());
            txtPo.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[4].Text.Trim());
            txtCustomer.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[5].Text.Trim());
            txtWo.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[6].Text.Trim());
            txtProvider.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[7].Text.Trim());
            txtLamp.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[8].Text.Trim());
            txtSeries.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[9].Text.Trim());
            txtPeriod.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[10].Text.Trim());
            txtVersion.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[11].Text.Trim());
            txtDept.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[12].Text.Trim());
            txtTemp.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[13].Text.Trim());
            txtAging.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[14].Text.Trim());
            txtDate.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[15].Text.Trim());
            txtTester.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[16].Text.Trim());
            dropStatu.SelectedValue = gvFluxMstr.Rows[index].Cells[17].Text.Trim();
            ddlType.SelectedValue = gvFluxMstr.DataKeys[index].Values["fl_type"].ToString();
            txtRemarks.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[19].Text.Trim());

            btnAdd.Text = "保存";
            btnClear.Text = "取消";
        }
    }

    private string changeEmpty(string enter)
    {
        if(enter.Equals("&nbsp;"))
        {
            return "";
        }
        else
        {
            return enter;
        }
    }
    protected void gvFluxMstr_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = int.Parse(gvFluxMstr.DataKeys[e.RowIndex].Values["fl_id"].ToString());

        if (oqc.DeleteFlux(ID))
        {
            ltlAlert.Text = "alert('删除成功');";

            ClearInfo();
            GridViewBind();
        }
        else
            ltlAlert.Text = "alert('删除失败');";
    }
    protected void gvFluxMstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton link = (LinkButton)e.Row.Cells[19].FindControl("linkDetail");
            link.Attributes["onclick"] = "window.open('qc_flux_det.aspx?id=" + gvFluxMstr.DataKeys[e.Row.RowIndex].Values["fl_id"].ToString().Trim() + "','','menubar=no,scrollbars=no,resizable=no,width=1000,height=500,top=0,left=0');";

            string type = gvFluxMstr.DataKeys[e.Row.RowIndex].Values["fl_type"].ToString();
        }
    }
    protected void gvFluxMstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFluxMstr.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        string title = "30^<b>序号</b>~^200^<b>产品型号</b>~^120^<b>测试日期</b>~^100^<b>点燃方式</b>~^80^<b>x</b>~^80^<b>y</b>~^80^<b>u</b>~^80^<b>v</b>~^80^<b>色温(K)</b>~^80^<b>色容差(SDCM)</b>~^80^<b>光通量(lm)</b>~^80^<b>光效(lm/W)</b>~^80^<b>辐射功率(W)</b>~^80^<b>U(V)</b>~^80^<b>I(A)</b>~^80^<b>P(W)</b>~^80^<b>PF</b>~^80^<b>红色比(%)</b>~^80^<b>色纯度(%)</b>~^80^<b>主波长(nm)</b>~^80^<b>峰值波长(nm)</b>~^80^<b>Ra</b>~^80^<b>R9</b>~^80^<b>DUV</b>~^";

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

    /// <summary>
    /// 用于被调用的上传附件的方法
    /// </summary>
    protected void btnUplodeFile_Click()
    {

        if (txtID.Text.Equals(string.Empty))
        {
            ltlAlert.Text = "alert('请先选择一个产品！');";
            return;
        }
        else
        {

            string path = "/TecDocs/QC/";
            string fileName = string.Empty;//原文件名
            string filePate = string.Empty;//文件路径+文件名（存储的）
            if (string.Empty.Equals(filename.PostedFile.FileName))
            {
                ltlAlert.Text = "alert('上传路径不能为空');";
                return;

            }
            else
            {
                if (!ImportFile(ref fileName, ref filePate, path))
                {
                    ltlAlert.Text = "alert('上传文件失败，请联系管理员');";
                    return;
                }
                //else 
                //{
                //    ltlAlert.Text = "alert('上传文件成功！');";
                //}
            }

            if (oqc.importFluxBasisFile(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), fileName, filePate))
            {
                ltlAlert.Text = "alert('上传文件成功！');";
                oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
                return;
            }
            else
            {

                ltlAlert.Text = "alert('上传文件失败，请联系管理员！');";
                return;
            }
        }
    }


    /// <summary>
    /// 上传附件的方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUplodeFile_Click(object sender, EventArgs e)
    {

        if (txtID.Text.Equals(string.Empty))
        {
            ltlAlert.Text = "alert('请先选择一个产品！');";
            return;
        }
        else
        { 
            
            string path = "/TecDocs/QC/";
            string fileName = string.Empty;//原文件名
            string filePate = string.Empty;//文件路径+文件名（存储的）
            if (string.Empty.Equals(filename.PostedFile.FileName))
            {
                ltlAlert.Text = "alert('上传路径不能为空');";
                return;

            }
            else
            {
                if (!ImportFile(ref fileName, ref filePate, path))
                {
                    ltlAlert.Text = "alert('上传文件失败，请联系管理员');";
                    return;
                }
                //else 
                //{
                //    ltlAlert.Text = "alert('上传文件成功！');";
                //}
            }

            if (oqc.importFluxBasisFile(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), fileName, filePate))
            {
                ltlAlert.Text = "alert('上传文件成功！');";
                oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
                bindHard();
                GridViewBind();
                return;
            }
            else
            {

                ltlAlert.Text = "alert('上传文件失败，请联系管理员！');";
                return;
            }
        }
    }

    protected bool ImportFile(ref string _fileName, ref string _filePath, string path)
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
