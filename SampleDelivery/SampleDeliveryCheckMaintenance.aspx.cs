using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SampleDelivery_SampleDeliveryCheckMaintenance : BasePage
{
    SampleDeliveryHelper helper = new SampleDeliveryHelper();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("320230", "送样单检测权限");
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lbl_id.Text = Request.QueryString["bsd_mstrid"];
            BindMstrData();
            BindDetailData();
        }
    }

    public void BindMstrData()
    {
        int id = int.Parse(lbl_id.Text.ToString());
        sample = helper.GetSampleDelivery(id);
        lbl_detId.Text = sample.DetId.ToString();
        txt_nbr.Text = sample.No;
        txt_createdDate.Text = sample.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
        txt_receiver.Text = sample.Receiver;
        txt_shipto.Text = sample.Shipto;
        txtRmks.Text = sample.Remarks;
    }

    public void BindDetailData()
    {
        int id = int.Parse(lbl_id.Text);

        IList<SampleDeliveryDetail> details = helper.GetSampleDeliveryDetails(id);
        gv_det.DataSource = details;
        gv_det.DataBind();
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
            Response.Redirect("SampleDeliveryCheckList.aspx");
    }

    protected void gv_det_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_det.PageIndex = e.NewPageIndex;
        BindDetailData();
    }
    protected void gv_det_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        SampleDeliveryDetail detail = e.Row.DataItem as SampleDeliveryDetail;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
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
        string chkResult = ((DropDownList)gv_det.Rows[e.RowIndex].FindControl("ddl_CheckResult")).SelectedValue;
        int uID = int.Parse(Session["uID"].ToString());
        string reason = ((TextBox)gv_det.Rows[e.RowIndex].FindControl("txtReason")).Text.Trim();
        if (chkResult == "0" && reason == "")
        {
            this.Alert("检测未通过时必须注明原因");
            return;
        }
        if (Convert.ToInt32(chkResult) < 2)
        {
            SampleDeliveryDetail detail = new SampleDeliveryDetail(id);
            detail.CheckResult = Convert.ToBoolean(Convert.ToInt32(chkResult));
            detail.CheckRemarks = reason;
            detail.CheckedBy = uID;
            if (helper.CheckSampleDeliveryDetail(detail))
            {
                gv_det.EditIndex = -1;
            }
            else
            {
                gv_det.EditIndex = -1;
                this.Alert("检测失败！");
            }
            BindDetailData();
        }
        else
        {
            this.Alert("请选择一项验收结果！");
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
            Response.Redirect("SampleDeliveryDocImport.aspx?bsdNbr=" + strNbr + "&bsddetCode=" + strDetCode + "&bsddetQad=" + strDetQad + "&bsd_mstrid=" + strMstrId + "&bsdDetId=" + strDetId + "&isChecked=false&type=c");

        }

    }
}