using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SampleDelivery_SampleDeliveryList : BasePage
{
    SampleDeliveryHelper helper = new SampleDeliveryHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["bsdnbr"]))
            {
                if (Request.QueryString["bsdnbr"] != "")
                {
                    txt_nbr.Text = Request.QueryString["bsdnbr"];
                    chkb_displayToApprove.Checked = false;
                }
                else
                {
                    txt_CreatedDate1.Text = System.DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                txt_CreatedDate1.Text = System.DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd");
            }
            //Response.Redirect("/Supplier/SampleNotesMaintain.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            if (!String.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                btn_Back.Visible = true;
            }
            if (!String.IsNullOrEmpty(Request.QueryString["ulid"]))
            {
                btn_ulback.Visible = true;
            }
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
        IList<SampleDelivery> samples = null;

        int mid = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
        {
            mid = Convert.ToInt32(Request.QueryString["mid"]);
        }
        bool pendingToAppr = chkb_displayToApprove.Checked;
        int userId = int.Parse(Session["uID"].ToString());
        int roleId = int.Parse(Session["uRole"].ToString());
        samples = helper.GetSampleDeliveries(no, receiver, fromdate, todate, mid, pendingToAppr, userId, roleId);

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
        if (!String.IsNullOrEmpty(Request.QueryString["mid"]))
        {
            Response.Redirect("SampleDeliveryMaintenance.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
        else
        {
            if (Request.QueryString["ulid"] != null)
            {
                Response.Redirect("SampleDeliveryMaintenance.aspx?ulid=" + Convert.ToString(Request.QueryString["ulid"]));
            }
            else
            {
                Response.Redirect("SampleDeliveryMaintenance.aspx");
            }
        } 
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strST = Request.QueryString["st"] == null ? "" : Convert.ToString(Request.QueryString["st"]);
        if (!String.IsNullOrEmpty(Request.QueryString["bsdnbr"]))
        {
            Response.Redirect("../RDW/RDW_ULmstr.aspx?mid=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);

        }
        else
        {
            Response.Redirect("../RDW/RDW_DetailList.aspx?mid=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
    }

    protected void gv_mstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv_mstr.DataKeys[e.Row.RowIndex].Values["CheckResult"] == null)
            {
                e.Row.Cells[5].Text = " ";
            }
            else if (gv_mstr.DataKeys[e.Row.RowIndex].Values["CheckResult"].ToString().ToLower() == "true")
            {
                e.Row.Cells[5].Text = "检测通过";
            }
            else
            {
                e.Row.Cells[5].Text = "检测未通过";
            }
            if (gv_mstr.DataKeys[e.Row.RowIndex].Values["IsSended"].ToString().ToLower() == "true")
            {
                e.Row.Cells[6].Text = "已发送";
            }
            else
            {
                e.Row.Cells[6].Text = " ";
            }

            if (gv_mstr.DataKeys[e.Row.RowIndex].Values["IsCanceled"].ToString().ToLower() == "true")
            {
                e.Row.Cells[7].Text = "取消";
            }
            else
            {
                e.Row.Cells[7].Text = " ";
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
            string id = e.CommandArgument.ToString();
            if (!String.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                Response.Redirect("SampleDeliveryMaintenance.aspx?bsd_mstrid=" + id + "&mid=" + Convert.ToString(Request.QueryString["mid"])  + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("SampleDeliveryMaintenance.aspx?bsd_mstrid=" + id);
            }

        }
    }
    protected void chkb_displayToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btn_ulback_Click(object sender, EventArgs e)
    {
        string strMID = Convert.ToString(Request.QueryString["ulid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strST = Request.QueryString["st"] == null ? "" : Convert.ToString(Request.QueryString["st"]);
        
            Response.Redirect("../RDW/RDW_ULmstr.aspx?id=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);

        
       
    }
}