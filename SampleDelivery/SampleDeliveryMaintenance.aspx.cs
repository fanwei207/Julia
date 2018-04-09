using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SampleDelivery_SampleDeliveryMaintenance : BasePage
{
    SampleDeliveryHelper helper = new SampleDeliveryHelper();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("320220", "送样单新增权限");
        }

        base.OnInit(e);
    }

    private SampleDelivery sample
    {
        get
        {
            if (ViewState["sample"] == null)
            {
                ViewState["sample"] = new SampleDelivery();
            }
            return ViewState["sample"] as SampleDelivery;
        }
        set
        {
            ViewState["sample"] = value;
        }
    }

    private SampleDeliveryApply apply
    {
        get
        {
            return ViewState["apply"] as SampleDeliveryApply;
        }
        set
        {
            ViewState["apply"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(Request.QueryString["bsd_mstrid"]) || Request.QueryString["bsd_mstrid"].ToString() == "0")
            {
                btn_Add.Visible = true;
                btn_Save.Visible = false;
                tb_det.Visible = false;

                tr_check.Visible = false;
                tr_send.Visible = false;
                tr_approve.Visible = false;
                txt_createdDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
                {
                    lbl_detId.Text = Request.QueryString["mid"];
                }
                else
                {
                    lbl_detId.Text = "0";
                }
            }
            else
            {



                btn_Add.Visible = false;
                txt_receiver.Enabled = false;
                lbl_id.Text = Request.QueryString["bsd_mstrid"];
                BindMstrData();
                BindApplyData();
                BindDetailData();            
                BindApproveData();
                if (!this.Security["320220"].isValid)
                {
                    tr_det.Visible = false;
                    btn_Save.Visible = false;
                    btn_Delete.Visible = false;
                    btn_Submit.Visible = false;
                }
            }
            BindProjectData();
            lblAddTip.Visible = btn_Add.Visible;
        }
    }

    private void BindMstrData()
    {
        int id = int.Parse(lbl_id.Text.ToString());
        sample = helper.GetSampleDelivery(id);
        lbl_detId.Text = sample.MstrID.ToString();
        txt_nbr.Text = sample.No;
        txt_createdDate.Text = sample.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
        txt_receiver.Text = sample.Receiver;
        txt_shipto.Text = sample.Shipto;
        txtRmks.Text = sample.Remarks;
        if (sample.CheckResult.HasValue)
        {
            chk_isChecked.Checked = true;
        }
        else
        {
            chk_isChecked.Checked = false;
        }
        chk_isSended.Checked = sample.IsSended;
        lblState.Visible = sample.IsCanceled;
        if (chk_isChecked.Checked || chk_isSended.Checked)
        {
            btn_Save.Enabled = false;
            btn_Delete.Enabled = false;
            
            if (chk_isChecked.Checked)
            {
                if (sample.CheckResult.Value)
                {
                    lbl_CheckNote.Text = "检测通过";
                    tr_det.Visible = false;
                }
                else
                {
                    lbl_CheckNote.Text = "检测未通过";
                    btn_Submit.Visible = true;
                }
            }
            if (chk_isSended.Checked)
            {
                btn_Cancel.Visible = false;
                lbl_SendNote.Text = "发送日期:" + String.Format("{0:yyyy-MM-dd HH:MM:ss}", Convert.ToDateTime(sample.SendedDate)) + ",发送人:" + sample.Sender;
                tr_det.Visible = false;
            }
        }
        if (lblState.Visible)
        {
            btn_Save.Enabled = false;
            btn_Delete.Enabled = false;
            btn_Cancel.Visible = false;
        }
    }

    private void BindDetailData()
    {
        int id = int.Parse(lbl_id.Text);

        IList<SampleDeliveryDetail> details = helper.GetSampleDeliveryDetails(id);
        gv_det.DataSource = details;
        if (chk_isSended.Checked || apply != null)
        {
            gv_det.Columns[5].Visible = false;
            gv_det.Columns[6].Visible = false;
        }
        gv_det.DataBind();
    }

    private void BindApplyData()
    {
        int id = int.Parse(lbl_id.Text.ToString());
        apply = helper.GetSampleDeliveryApply(id);
        if (apply != null)
        {
            lblApplyId.Text = apply.Id.ToString();
            lblAproveDate.Text = apply.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss");
            txtApplyReason.Text = apply.Reason;
            tr_det.Visible = false;
            btn_ApplySubmit.Visible = false;
            txtApplyReason.Enabled = false;
            btn_Delete.Enabled = false;
            btn_Cancel.Enabled = false;
            if (apply.CurrentApproveBy == int.Parse(Session["uID"].ToString()))
            {
                btn_approve.Visible = true;
                btn_diaApp.Visible = true;
                approveopinion.Visible = true;
            }
            else
            {
                tbApply.Visible = false;
            }
            if (apply.ApproveResult.HasValue)
            {
                chk_isApproved.Checked = true;
                if (apply.ApproveResult.Value)
                {                    
                    lbl_ApproveNote.Text = "审批通过";
                }
                else
                {
                    lbl_ApproveNote.Text = "审批未通过";
                }
            }
        }
        else
        {
            btn_ApplySubmit.Visible = true;
            txtApplyReason.Enabled = true;
            btn_approve.Visible = false;
            btn_diaApp.Visible = false;
        }
    }

    private void BindApproveData()
    { 
        int id = int.Parse(lbl_id.Text.ToString());
        IList<SampleDeliveryApprove> approves = helper.GetSampleDeliveryApprove(id);
        gvApprove.DataSource = approves;
        gvApprove.DataBind();
    }

    private void BindProjectData()
    {
        if (lbl_detId.Text == "0")
        {
            tr_Project.Visible = false;
        }
        else
        {
            tr_Project.Visible = true;
            RD_WorkFlow.RDW rdw = new RD_WorkFlow.RDW();
            var project = rdw.SelectRDWHeader(lbl_detId.Text);
            txt_ProjectName.Text = project.RDW_Project;
            txt_ProjectCode.Text  = project.RDW_ProdCode;
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        sample.Receiver = txt_receiver.Text.Trim();
        sample.Shipto = txt_shipto.Text.Trim();
        sample.Remarks = txtRmks.Text.Trim();
        sample.CreatedBy = Convert.ToInt32(Session["uID"]);
        sample.MstrID = int.Parse(lbl_detId.Text);
        string ulid;
        if (Request.QueryString["ulid"] != null)
        {
            ulid = Convert.ToString(Request.QueryString["ulid"]);
        }
        else
        {
            ulid = "0";
        }
        int id = 0;
        id = helper.AddSampleDelivery(sample,ulid);
        if (id > 0)
        {
            lbl_id.Text = id.ToString();
            BindMstrData();
            tb_det.Visible = true;
            tr_det.Visible = true;
            BindDetailData();
            BindApplyData();
            btn_Add.Visible = false;
            btn_Save.Visible = true;
            lblAddTip.Visible = btn_Add.Visible;
            BindApproveData();
        }
        else
        {
            this.Alert("送样单添加失败");
            return;
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        sample.Receiver = txt_receiver.Text.Trim();
        sample.Shipto = txt_shipto.Text.Trim();
        sample.Remarks = txtRmks.Text.Trim();
        if (helper.UpdateSampleDelivery(sample))
        {
            Response.AddHeader("Refresh", "0"); 
            this.Alert("送样单修改成功");
            return;
        }
        else
        {
            this.Alert("送样单修改失败");
            return;
        }
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if (helper.DeleteSampleDelivery(sample))
        {
            this.Alert("送样单删除成功");
            if (!String.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                Response.Redirect("SampleDeliveryList.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("SampleDeliveryList.aspx");
            }
            return;
        }
        else
        {
            this.Alert("送样单删除失败");
            return;
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (helper.SubmitSampleDelivery(sample))
        {
            BindMstrData();
            this.Alert("送样单重新提交成功");
            return;
        }
        else
        {
            this.Alert("送样单重新提交失败");
            return;
        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        if (helper.CancelSampleDelivery(sample))
        {
            BindMstrData();
            this.Alert("送样单取消成功");
            return;
        }
        else
        {
            this.Alert("送样单取消失败");
            return;
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["mid"]))
        {
            Response.Redirect("SampleDeliveryList.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
        else if(!String.IsNullOrEmpty(Request.QueryString["ulid"]))
        {
            Response.Redirect("SampleDeliveryList.aspx?ulid=" + Request.QueryString["ulid"]);
        }
    }
    protected void txt_detCode_TextChanged(object sender, EventArgs e)
    {
        btnSaveDet.Enabled = true;
        string message = "";
        string strQAD = GetQadNo(txt_detCode.Text.Trim(), out message);
        if (strQAD == null)
        {
            this.Alert(message);
            return;
        }
        else
        {
            txt_detQAD.Text = strQAD;
        }
    }
    protected void btnSaveDet_Click(object sender, EventArgs e)
    {
        decimal quantity = 0;
        if (txt_detCode.Text.Trim() == "")
        {
            this.Alert("请输入部件号！");
            return;
        }
        else
        {
            string message = "";
            string strQAD = GetQadNo(txt_detCode.Text.Trim(), out message);
            if (strQAD == null)
            {
                this.Alert(message);
                return; 
            }
        }
        if (txt_detQty.Text.Trim() == "")
        {
            txt_detQty.Text = "1";
        }
        if (decimal.TryParse(txt_detQty.Text.Trim(), out quantity))
        {
            SampleDeliveryDetail detail = new SampleDeliveryDetail();
            int mstrId = int.Parse(lbl_id.Text);
            detail.PartCode = txt_detCode.Text.Trim();
            detail.QadNo = txt_detQAD.Text.Trim();
            detail.Quantity = quantity;
            detail.Remarks = txt_detRmks.Text.Trim();
            if (helper.AddSampleDeliveryDetail(mstrId, detail) > 0)
            {
                BindDetailData();
                txt_detCode.Text = "";
                txt_detQAD.Text = "";
                txt_detQty.Text = "";
                txt_detRmks.Text = "";
            }
            else
            {
                this.Alert("添加明细失败！");
                return;
            }

        }
        else
        {
            this.Alert("样品物料的数量必须是数字");
        }
    }

    protected void btn_detCancel_Click(object sender, EventArgs e)
    {
        txt_detCode.Text = "";
        txt_detQAD.Text = "";
        txt_detQty.Text = "";
        txt_detRmks.Text = "";
    }

    private string GetQadNo(string partCode, out string message)
    {
        string strCode = txt_detCode.Text.Trim().ToString();
        SampleManagement.Sample sap = new SampleManagement.Sample();
        string strQAD = sap.ConfirmExistsCode(partCode);
        if (strQAD == "0")
        {
            message = "此部件号不存在";
            return null;
        }
        else if (strQAD == "not have QAD")
        {
            message = "";
            return "";
        }
        else if (strQAD == "stop")
        {
            message = "此部件号" + strCode + "的已停用";
            return null;
        }

        message = "";
        return strQAD;
    }



    protected void gv_det_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_det.PageIndex = e.NewPageIndex;
        BindDetailData();
    }
    protected void gv_det_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        SampleDeliveryDetail detail=e.Row.DataItem as SampleDeliveryDetail;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (tr_det.Visible)
            {
                if (detail.CheckResult.HasValue)
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).Visible = true;
                }
            }
            else
            {
                ((LinkButton)e.Row.Cells[5].Controls[0]).Visible = false;
            }
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (tr_det.Visible)
                {
                    if (detail.CheckResult.HasValue)
                    {
                        ((LinkButton)e.Row.Cells[6].Controls[0]).Visible = false;
                    }
                    else
                    {
                        ((LinkButton)e.Row.Cells[6].Controls[0]).Visible = true;
                        e.Row.Cells[6].Attributes.Add("onclick", "return confirm('你确认要要删除这行吗?')");
                    }
                }
                else
                {
                    ((LinkButton)e.Row.Cells[6].Controls[0]).Visible = false;
                }
            }

            Label lblCheck = (Label)e.Row.FindControl("lblCheck");

            if (lblCheck.Text.ToLower() == "true")
            {
                lblCheck.Text = "通过";
            }
            else if (lblCheck.Text.ToLower() == "false")
            {
                lblCheck.Text = "不通过";
            }
            else
            {
                lblCheck.Text = "未检测";
            }
        }

    }
    protected void gv_det_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(gv_det.DataKeys[e.RowIndex].Values["Id"].ToString());
        SampleDeliveryDetail detail = new SampleDeliveryDetail(id);
        if (helper.DeleteSampleDeliveryDetail(detail))
        {
            BindDetailData();
            this.Alert("已删除");
            return;
        }
        else
        {
            this.Alert("删除失败");
            return;
        }
    }
    protected void gv_det_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_det.EditIndex = -1;
        BindDetailData();
    }
    protected void gv_det_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_det.EditIndex = e.NewEditIndex;
        BindDetailData();
    }
    protected void gv_det_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = int.Parse(gv_det.DataKeys[e.RowIndex].Values["Id"].ToString());
        SampleDeliveryDetail detail = new SampleDeliveryDetail(id);
        decimal quantity;
        TextBox txtdetQty = (TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetqty");
        if (txtdetQty.Text.Trim() == "")
        {
            txtdetQty.Text = "1";
        }
        if (decimal.TryParse(txtdetQty.Text.Trim(), out quantity))
        {
            detail.Quantity = quantity;
            detail.Remarks = ((TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetrmks")).Text;
            if (helper.UpdateSampleDeliveryDetail(detail))
            {
                gv_det.EditIndex = -1;
                BindDetailData();
            }
            else
            {
                this.Alert("修改失败！");
                return;
            }

        }
        else
        {
            this.Alert("样品物料的数量必须是数字");
        }
    }
    protected void gv_det_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "EditDoc")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string strNbr = txt_nbr.Text.ToString();
            string strDetCode = Server.UrlEncode(gv_det.Rows[index].Cells[0].Text.ToString()).ToString();
            string strDetQad = gv_det.Rows[index].Cells[1].Text.ToString();
            string strMstrId = lbl_id.Text;
            string strDetId = gv_det.DataKeys[index].Values["Id"].ToString();
            string strMid = lbl_detId.Text;
            //if (!String.IsNullOrEmpty(Request.QueryString["did"]))
            //{
            //    Response.Redirect("SampleNotesAccDoc.aspx?Mode=Maintain&strNbr=" + strBosNbr + "&line=" + strBosdetline + "&code=" + Server.UrlEncode(strBosdetCode).ToString() + "&qad=" + strBosdetQad + "&mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            //}
            //else
            //{
            //    Response.Redirect("SampleNotesAccDoc.aspx?Mode=Maintain&strNbr=" + strBosNbr + "&line=" + strBosdetline + "&code=" + Server.UrlEncode(strBosdetCode).ToString() + "&qad=" + strBosdetQad);
            //}
            if (strMid == "0")
            {
                Response.Redirect("SampleDeliveryDocImport.aspx?bsdNbr=" + strNbr + "&bsddetCode=" + strDetCode + "&bsddetQad=" + strDetQad + "&bsd_mstrid=" + strMstrId + "&bsdDetId=" + strDetId + "&isChecked=" + chk_isChecked.Checked.ToString());
            }
            else
            {
                Response.Redirect("SampleDeliveryDocImport.aspx?bsdNbr=" + strNbr + "&bsddetCode=" + strDetCode + "&bsddetQad=" + strDetQad + "&bsd_mstrid=" + strMstrId + "&bsdDetId=" + strDetId + "&isChecked=" + chk_isChecked.Checked.ToString() + "&mid=" + strMid);
            }
        }

    }


    protected void btn_Approver_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('../RDW/rdw_getProjQadApprover.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btn_ApplySubmit_Click(object sender, EventArgs e)
    {
        if (gv_det.Rows.Count == 0)
        {
            ltlAlert.Text = "alert('请添加零件!'); ";
            return;
        }
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Apply Reason,Please!'); ";
            return;
        }
        if (txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Select one Approver,Please!'); ";
            return;
        }

        if (chkEmail.Checked)
        {
            if (txt_ApproveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('Enter the Approver Email if you want to send email,Please!'); ";
                return;
            }
        }
        int mstrId = int.Parse(lbl_id.Text);
        int approver = int.Parse(txt_approveID.Text);
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        apply = new SampleDeliveryApply();
        apply.ApplyBy = uID;
        apply.CurrentApproveBy = approver;
        apply.Reason = applyReason;
        int applyId = helper.AddSampleDeliveryApply(mstrId, apply);
        if (applyId > 0)
        {
            bool isSuccess = true;
            string message = "";
            lblApplyId.Text = applyId.ToString();

            if (chkEmail.Checked)
            {
                isSuccess = helper.SendMail(mstrId.ToString(), lbl_detId.Text, txt_nbr.Text.Trim(), txt_receiver.Text.Trim(), txt_ApproveEmail.Text.Trim(), txtApproveName.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), applyReason, applyId.ToString(), out message);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Apply successfully!');";

            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            BindApplyData();
            BindApproveData();
            BindDetailData();
            //ltlAlert.Text += " window.location.href='/RDW/RDW_ProjQadApply.aspx?type=" + Request.QueryString["type"].ToString() + "&pqmId=" + applyId + "&mid=" + mid + "&fr= &@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";

        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }

        int applyId = int.Parse(lblApplyId.Text);
        if (txt_approveID.Text == "")
        {
            txt_approveID.Text = "0";
        }
        int approver = int.Parse(txt_approveID.Text);
        int uID = int.Parse(Session["uID"].ToString());
        string approveOpoin = txtApprOpin.Text.Trim().ToString();
        SampleDeliveryApprove approve = new SampleDeliveryApprove();
        approve.ApplyId = applyId;
        approve.ApplyBy = uID;
        approve.ApproveBy = approver;
        approve.ApproveNote = approveOpoin;
        approve.ApproveResult = true;
        if (helper.UpdateSampleDeliveryApprove(approve))
        {
            bool isSuccess = true;
            string message = "";
            if (txt_ApproveEmail.Text.Trim() != "" && chkEmail.Checked)
            {
                isSuccess = helper.SendMail(lbl_id.Text, lbl_detId.Text, txt_nbr.Text.Trim(), txt_receiver.Text.Trim(), txt_ApproveEmail.Text.Trim(), txtApproveName.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), txtApplyReason.Text.Trim(), applyId.ToString(), out message);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Successfully!');";

            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            Response.AddHeader("Refresh", "0"); 
            return;
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
    protected void btn_diaApp_Click(object sender, EventArgs e)
    {
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }

        int applyId = int.Parse(lblApplyId.Text);
        int uID = int.Parse(Session["uID"].ToString());
        string approveOpoin = txtApprOpin.Text.Trim().ToString();
        SampleDeliveryApprove approve = new SampleDeliveryApprove();
        approve.ApplyId = applyId;
        approve.ApplyBy = uID;
        approve.ApproveNote = approveOpoin;
        approve.ApproveResult = false;
        if (helper.UpdateSampleDeliveryApprove(approve))
        {
            Response.AddHeader("Refresh", "0"); 
            return;
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
}