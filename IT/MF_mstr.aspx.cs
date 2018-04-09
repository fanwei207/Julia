using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MF;

public partial class IT_MF_step : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Databind();
        }
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("MF_New.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());
    }
    public void Databind()
    {
        gv.DataSource = MFHelper.selectMFmstr(ddlstatus.SelectedValue, txtkeywords.Text.Trim());
        gv.DataBind();
    }
    protected void btn_messageselect_Click(object sender, EventArgs e)
    {
        Databind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gv.DataKeys[index].Values["FM_id"].ToString());
            MFHelper.setvisit(fsId.ToString(), Session["uID"].ToString(), Session["uName"].ToString());


            Response.Redirect("MF_det.aspx?from=new&id=" + fsId + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }

    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Databind();
    }
}