using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pc_MadeInquiryList : System.Web.UI.Page
{
    PC_price pc = new PC_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind()
    {
        DataTable dt = pc.selectNotInquiryFromApplyDet(txtQAD.Text.ToString().Trim(), txtVender.Text.ToString().Trim(), txtVenderName.Text.ToString().Trim());
        gvVenderList.DataSource = dt;
        gvVenderList.DataBind();
    }
    protected void gvVenderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVenderList.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvVenderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string notInInquiry = ((LinkButton)e.Row.Cells[2].FindControl("lkbtnQuotation")).Text;
            ((LinkButton)e.Row.Cells[2].FindControl("lkbtnQuotation")).Text = "未成单(" + notInInquiry + ")";


            string countInquiry = ((LinkButton)e.Row.Cells[3].FindControl("lkbtnInquiry")).Text;
            ((LinkButton)e.Row.Cells[3].FindControl("lkbtnInquiry")).Text = "未完成(" + countInquiry + ")";
        }
    }
    protected void gvVenderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnQuotation")
        {
            string vender =e.CommandArgument.ToString();
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string venderName = gvVenderList.Rows[index].Cells[1].Text.ToString();
            Response.Redirect("pc_MadeInquiry.aspx?vender=" + vender + "&From=0&venderName=" + venderName);
           
        }

        if (e.CommandName == "lkbtnInquiry")
        {
            string vender = e.CommandArgument.ToString();
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            Response.Redirect("pc_InquiryList.aspx?vender=" + vender);
        }

    }
}