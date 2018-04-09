using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SampleDelivery_SampleDeliverySendList : BasePage
{
    SampleDeliveryHelper helper = new SampleDeliveryHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_CreatedDate1.Text = System.DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd");

            //Response.Redirect("/Supplier/SampleNotesMaintain.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            BindData();
        }
    }

    private void BindData()
    {
        string no = txt_nbr.Text.Trim();
        string receiver = txt_receiver.Text.Trim();
        string fromdate;
        string todate;
        DateTime date;
        if (DateTime.TryParse(txt_CreatedDate1.Text.Trim(), out date))
        {
            fromdate = date.ToString("yyyy-MM-dd");
        }
        else
        {
            fromdate = "";
        }
        if (DateTime.TryParse(txt_CreatedDate2.Text.Trim(), out date))
        {
            todate = date.ToString("yyyy-MM-dd");
        }
        else
        {
            todate = "";
        }
        int sendValue = int.Parse(ddl_sendValue.SelectedValue);
        IList<SampleDelivery> samples = null;

        samples = helper.GetSampleDeliveriesForSend(no, receiver, fromdate, todate, sendValue);
        gv_mstr.DataSource = samples;
        gv_mstr.DataBind();
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_nbr.Text = "";
        txt_receiver.Text = "";
        txt_rmks.Text = "";
        txt_CreatedDate1.Text = System.DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd");
        txt_CreatedDate2.Text = "";
        BindData();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["did"]))
        {
            Response.Redirect("SampleDeliveryMaintenance.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
        else
        {
            Response.Redirect("SampleDeliveryMaintenance.aspx");
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {

    }

    protected void gv_mstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv_mstr.DataKeys[e.Row.RowIndex].Values["IsSended"].ToString().ToLower() == "true" )
            {
                e.Row.Cells[5].Text = "已发送";
            }
        }

    }


    protected void gv_mstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_mstr.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gv_mstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {
            int id = int.Parse(e.CommandArgument.ToString());
            int uid=int.Parse(Session["uID"].ToString());
            SampleDelivery sample = new SampleDelivery(id);
            sample.SendedBy = uid;

            if (helper.SendSampleDelivery(sample))
            {
                this.Alert("操作成功");
               
            }
            else
            {
                this.Alert("操作失败");
            }
            BindData();
        }
    }
}