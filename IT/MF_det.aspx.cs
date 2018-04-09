using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MF;
using System.Data.SqlClient;


public partial class IT_MF_det :BasePage
{

    public string _parentID
    {
        get
        {
            return ViewState["parentID"].ToString();
        }
        set
        {
            ViewState["parentID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             _parentID = Request.QueryString["id"];

            Databind();
        }
    }
    public void Databind()
    {
        string ad = "0";
        if (this.Security["2000714"].isValid)
        {
            ad = "1";
        }
        gv.DataSource = MFHelper.selectMFdet(_parentID, "0", Session["uID"].ToString(), ad);
        gv.DataBind();

        SqlDataReader read = MFHelper.selectMFmstrone(_parentID);
        while (read.Read())
        {
            lblTitle.Text = read["FM_title"].ToString();
            lblAuthorize.Text = read["FM_Authorize"].ToString();
            txtDecription.Text = read["FM_Decription"].ToString();
            txtkey1.Text = read["FM_keyWords"].ToString();
        }

    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("MF_mstr.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void btn_messageselect_Click(object sender, EventArgs e)
    {
        Databind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Databind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Save")
        {
            if (this.Security["2000714"].isValid)
            {


                int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
                int fsId = Convert.ToInt32(gv.DataKeys[index].Values["FM_id"].ToString());

                MFHelper.saveMFdet(fsId.ToString(), Session["uID"].ToString(), Session["uName"].ToString());
                Databind();

                //Response.Redirect("MF_det.aspx?from=new&id=" + fsId + "&rt=" + DateTime.Now.ToFileTime().ToString());

            }
            else
            {
                this.Alert("你没有保存权限！");
            }
        }
        if (e.CommandName == "Change")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gv.DataKeys[index].Values["FM_id"].ToString());
            ltlAlert.Text = "window.showModalDialog('MF_NewDet.aspx?id=" + _parentID + "&detid=" + fsId.ToString() + " &rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
            ltlAlert.Text += "window.location.href = 'MF_det.aspx?id=" + _parentID + " &rt=" + DateTime.Now.ToFileTime().ToString() + "'";

        }
        if (e.CommandName == "review")
        {
            string sec ="false";
            if (this.Security["2000714"].isValid)
            {
                sec = "true";
            }

            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gv.DataKeys[index].Values["FM_id"].ToString());
            ltlAlert.Text = "window.showModalDialog('MF_detReview.aspx?id=" + _parentID + "&detid=" + fsId.ToString() + "&sec= " +sec+ " &rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
            ltlAlert.Text += "window.location.href = 'MF_det.aspx?id=" + _parentID + " &rt=" + DateTime.Now.ToFileTime().ToString() + "'";
           // Response.Redirect("MF_detReview.aspx?id=" + _parentID + "&detid=" + fsId.ToString() + " &rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btn_newitem_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.showModalDialog('MF_NewDet.aspx?id=" + _parentID + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'MF_det.aspx?id=" + _parentID + " &rt=" + DateTime.Now.ToFileTime().ToString() + "'";
    }
}