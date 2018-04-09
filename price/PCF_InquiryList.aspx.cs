using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class price_PCF_InquiryList : BasePage
{

    PCF_helper helper = new PCF_helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["vender"] != null)
            {
                txtVender.Text = Request["vender"].ToString();
            }
            if (Request["TIMID"] != null)
            {
                txtIMID.Text = Request["TIMID"].ToString();
            }
            if (Request["TVender"] != null)
            {
                txtVender.Text = Request["TVender"].ToString();
            }
            
            if (Request["TVenderName"] != null)
            {
                txtVenderName.Text = Request["TVenderName"].ToString();
            }
            if (Request["TQAD"] != null)
            {
                txtQAD.Text = Request["TQAD"].ToString();
            }

            if (Request["ddlStatus"] != null)
            {
                ddlStatus.SelectedValue = Request["ddlStatus"].ToString();
            }
            bind();
        }
    }

    private void bind()
    {
        string vender = txtVender.Text.Trim();
        string venderName = txtVenderName.Text.Trim();
        string QAD = txtQAD.Text.Trim();
        string IMID = txtIMID.Text.Trim();
        

        string status = ddlStatus.SelectedValue;

        gvInquiryInfo.DataSource = helper.selectInqyuryList(vender, venderName, QAD, IMID, status);
        gvInquiryInfo.DataBind();


    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }
    protected void gvInquiryInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInquiryInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvInquiryInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

        if (e.CommandName == "lkbtnOperation")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string PCF_inquiryID = gvInquiryInfo.DataKeys[index].Values["PCF_inquiryID"].ToString();
            Response.Redirect("PCF_InquiryDet.aspx?PCF_inquiryID=" + PCF_inquiryID + "&TIMID=" + txtIMID.Text.Trim() + "&TVender=" + txtVender.Text.Trim() + "&TVenderName=" + txtVenderName.Text.Trim() + "&TQAD=" + txtQAD.Text.Trim() + "&ddlStatus=" + ddlStatus.SelectedValue.ToString());
        }
    }
    protected void btnMakeInquiry_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCF_MadeInquiryList.aspx");
    }
}