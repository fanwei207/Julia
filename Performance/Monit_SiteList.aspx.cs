using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Performance_CCTV_LogList : BasePage
{
    Monitor monitor = new Monitor();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("897001", "监控点维护（修改）");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //公司初始化
            ddl_plant.SelectedValue = "0";
            //选中当前域
            ddl_plant.SelectedIndex = -1;
            ddl_plant.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
            GridViewBind();
        }
    }
    protected void GridViewBind()
    {
        string mID = txt_mID.Text.Trim();
        string PlantID = ddl_plant.SelectedValue;

        gv.DataSource = monitor.SelectMonitor(mID,PlantID);
        gv.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex;
        string mID = gv.DataKeys[intRow]["Monit_mID"].ToString();
        if (!monitor.DeleteMonitor(mID, Session["uID"].ToString()))
        ltlAlert.Text = "alert('删除失败！')";
        GridViewBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "myEdit")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string Monit_mID = gv.DataKeys[intRow].Values["Monit_mID"].ToString();
            string ID = gv.DataKeys[intRow]["Monit_ID"].ToString();
            string isModify = "0";
            if (this.Security["897001"].isValid) isModify = "1";
            Response.Redirect("/Performance/Monit_Site.aspx?Monit_mID=" + Monit_mID + "&isModify=" + isModify+"&ID="+ID);
        }
    }
    protected void btn_Query_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
}