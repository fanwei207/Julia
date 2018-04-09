using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pcm_FinCheckDayList : System.Web.UI.Page
{
    PCM_FinCheckApply helper = new PCM_FinCheckApply();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           txtDate.Text= DateTime.Now.ToString("yyyy-MM-dd");
           bind();
        }
    }

    private void bind()
    {
        DataTable dt = helper.selectFinCheckDayList(txtDate.Text, txtVender.Text, txtVenderName.Text);

        gvInfo.DataSource = dt;
        gvInfo.DataBind();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnList")
        {
           
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string vender = gvInfo.DataKeys[index].Values["vender"].ToString();
            string venderName = e.CommandArgument.ToString();
            string date = gvInfo.DataKeys[index].Values["CheckPriceLoadDate"].ToString();

            Response.Redirect("pcm_FinCheckDayDet.aspx?vender=" + vender + "&venderName=" + venderName + "&date=" + date);
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }
}