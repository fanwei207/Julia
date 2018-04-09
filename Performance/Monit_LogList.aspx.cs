using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Performance_Monit_LogList : BasePage
{
    Monitor monitor = new Monitor();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("897003", "监控日志维护（修改）");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            txtDate1.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1";
            //选中当前域
            ddl_plant.SelectedIndex = -1;
            ddl_plant.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
            GVBind();
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        GVBind();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex;
        string id = gv.DataKeys[intRow].Values["Monit_id"].ToString();

        if (monitor.DeleteLog(id)) ltlAlert.Text = "alert('删除成功!')";
        else ltlAlert.Text = "alert('删除失败!')";

        GVBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "myEdit")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string id = gv.DataKeys[intRow].Values["Monit_id"].ToString();
            string isModify="0";
            if (this.Security["897003"].isValid) isModify = "1";
            Response.Redirect("Monit_AddLog.aspx?id=" + id + "&isModify=" + isModify);
        }
        else if (e.CommandName.ToString() == "PIC")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string id = gv.DataKeys[intRow].Values["Monit_id"].ToString();
            Response.Redirect("Monit_PIC.aspx?id="+id);
        }
    }
    protected void btn_check_Click(object sender, EventArgs e)
    {
        GVBind();
    }
    protected void GVBind()
    {
        //起始日期不能为空
        if(txtDate1.Text.Trim().Length==0)
        {
            ltlAlert.Text = "alert('起始日期必填!')";
            return;
        }
        string Date2;
        if (txtDate2.Text.Trim().Length == 0) Date2 = DateTime.Now.AddDays(1).ToString();
        else Date2 = txtDate2.Text.Trim();
        gv.DataSource = monitor.SelectLog(ddl_plant.SelectedItem.ToString(), txtDate1.Text.Trim(), Date2, txt_mID.Text.Trim(),ddl_falg.SelectedValue,txt_PIC.Text.Trim());
        gv.DataBind();
    }
    protected void ddl_plant_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            if (!this.Security["8970041"].isValid)
            {
                (e.Row.Cells[8].FindControl("lkb_Pic") as LinkButton).Enabled = false;
                (e.Row.Cells[8].FindControl("lkb_Pic") as LinkButton).Style[HtmlTextWriterStyle.TextDecoration] = "none";
            }
        }
    }
}