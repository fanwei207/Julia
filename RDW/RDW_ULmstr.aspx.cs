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
using RD_WorkFlow;
using System.Data.SqlClient;
public partial class RDW_RDW_ULmstr : BasePage
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id;

            if (Request.QueryString["mid"] != null)
            {
                id = Convert.ToString(Request.QueryString["mid"]);
               // txtProject.Text = rdw.selectULproject("0", id);
                lblmid.Text = id;
            }
            else
            {
                btnback.Visible = false;
            }
             if (Request.QueryString["id"] != null)
            {
                id = Convert.ToString(Request.QueryString["id"]);
                SqlDataReader read = rdw.selectULdet(id);
                if (read.Read())
                {
                   // txtProject.Text = read["UL_Project"].ToString().Trim();
                    lblmid.Text = read["UL_msrtID"].ToString().Trim();
                }
                read.Close();
            }
           
            else
            {
                btnback.Enabled = false;
            }
            BindData();
        }

    }
    public void BindData()
    {
        gvRDW.DataSource = rdw.selectUL(txtProject.Text, txtDate1.Text, txtDate2.Text, txtModel.Text);
        gvRDW.DataBind();
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //string strID = Convert.ToString(Request.QueryString["mid"]);
        //if (rdw.insertUL(strID, Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        //{
            BindData();
        //}
        //else
        //{
        //    ltlAlert.Text = "alert('新建UL失败');";
        //}
    }
    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = 0;
        string strMID = string.Empty;

        if (e.CommandName.ToString() == "mySample")
        {
            intRow = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string strID = "";
            string bsdnbr = gvRDW.DataKeys[intRow].Values["UL_bsdnbr"].ToString();
            strID = gvRDW.DataKeys[intRow].Values["UL_msrtID"].ToString();
            if (bsdnbr == "维护")
            {
                bsdnbr="";
            }
            string bsddate = gvRDW.DataKeys[intRow].Values["UL_bsddate"].ToString();
            string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
            if (strID != string.Empty)
            {
                

                Response.Redirect("../SampleDelivery/SampleDeliveryList.aspx?t=&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "&bsdnbr=" + bsdnbr + "&bsddate=" + bsddate, true);
            }
            else
            {

                strID = gvRDW.DataKeys[intRow].Values["UL_id"].ToString();
                Response.Redirect("../SampleDelivery/SampleDeliveryList.aspx?rm=" + DateTime.Now.ToString() + "&ulid=" + strID + "&bsdnbr=" + bsdnbr + "&bsddate=" + bsddate, true);
            }
        }

        if (e.CommandName.ToString() == "myDoc")
        {
            intRow = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            strMID = gvRDW.DataKeys[intRow].Values["UL_id"].ToString();

            Response.Redirect("/RDW/UL_Doc.aspx?mid=" + strMID + "&rm=" + DateTime.Now.ToString(), true);
        }
        if (e.CommandName.ToString() == "myModel")
        {
            intRow = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            strMID = gvRDW.DataKeys[intRow].Values["UL_id"].ToString();

            Response.Redirect("/RDW/UL_Model.aspx?mid=" + strMID + "&rm=" + DateTime.Now.ToString(), true);
        }
        if (e.CommandName.ToString() == "myQAD")
        {
            intRow = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            strMID = gvRDW.DataKeys[intRow].Values["UL_id"].ToString();

            Response.Redirect("/RDW/UL_QAD.aspx?mid=" + strMID + "&rm=" + DateTime.Now.ToString(), true);
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strST = Request.QueryString["st"] == null ? "" : Convert.ToString(Request.QueryString["st"]);
        Response.Redirect("../RDW/RDW_DetailList.aspx?mid=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
    
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        Response.Redirect("/RDW/UL_new.aspx?rm=" + DateTime.Now.ToString(), true);
    }
    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRDW.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnEXCEL_Click(object sender, EventArgs e)
    {
        DataTable dt = rdw.selectULEXCEL(txtProject.Text, txtDate1.Text, txtDate2.Text, txtModel.Text);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "300^<b>Product</b>~^110^<b>E号</b>~^70^<b>Section</b>~^120^<b>Driver JXL</b>~^120^<b>LED JXL</b>~^120^<b>Driver Lv</b>~^120^<b>LED Lv</b>~^200^<b>文档中查到的对应图片号</b>~^100^<b>NOTE</b>~^";
        this.ExportExcel(title, dt, false);
    }
}