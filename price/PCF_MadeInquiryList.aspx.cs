using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class price_PCF_MadeInquiryList : BasePage
{
    PCF_helper helper = new PCF_helper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
        
    }

    private void bind()
    {
        gvVenderList.DataSource = helper.selectMadeInquiryList(txtQAD.Text.Trim(), txtVender.Text.Trim(), txtVenderName.Text.Trim());
        gvVenderList.DataBind();
    }
    protected void gvVenderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVenderList.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvVenderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         

        if (e.CommandName == "lkbtnQuotation")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string vender = gvVenderList.DataKeys[index].Values["PCF_vender"].ToString();
            string venderName = gvVenderList.DataKeys[index].Values["PCF_venderName"].ToString();

            Response.Redirect("PCF_madeInquiryDet.aspx?vender=" + vender + "&venderName=" + venderName);
        }
        if (e.CommandName == "lkbtnInquiry")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string vender = gvVenderList.DataKeys[index].Values["PCF_vender"].ToString();

            Response.Redirect("PCF_InquiryList.aspx?vender=" + vender);
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }
}