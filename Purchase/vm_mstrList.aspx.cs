using AppLibrary.MyOle2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

public partial class Purchase_vm_mstrList : BasePage
{
    Mold mold = new Mold();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("120000310", "供应商模具维护（修改）");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //供应商绑定
            ddl_vend.DataSource = mold.BindVend();
            ddl_vend.DataBind();
            ddl_vend.Items.Insert(0, new ListItem("--", "0"));
            ddl_vend.SelectedIndex = 0;
            //模具状态
            ddl_Status.Items.Insert(0, new ListItem("--", "0"));
            ddl_Status.Items.Insert(1, new ListItem("可用", "1"));
            ddl_Status.Items.Insert(2, new ListItem("不可用", "2"));
            BindGridView();

            if (!this.Security["120000310"].isValid) btn_export.Enabled = false;
        }
        
    }
    protected void BindGridView()
    {
        gv.DataSource = mold.SelectMold(ddl_vend.SelectedValue,txt_MoldID.Text.Trim(),txt_QAD.Text.Trim(),Convert.ToInt32(ddl_Status.SelectedValue));
        gv.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Edit")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string vm_id = gv.DataKeys[intRow].Values["ID"].ToString();
            string vendCode = gv.DataKeys[intRow].Values["vendCode"].ToString();
            string moldCode = gv.Rows[intRow].Cells[2].Text;
            string Qty = gv.Rows[intRow].Cells[3].Text;
            string status = gv.DataKeys[intRow].Values["intStatus"].ToString();
            string QAD= gv.Rows[intRow].Cells[5].Text; ;
            string Cavity = gv.Rows[intRow].Cells[6].Text;
            string drawingID = gv.Rows[intRow].Cells[7].Text;
            string remark = gv.Rows[intRow].Cells[8].Text;
            string isModify = "0";
            if (this.Security["120000310"].isValid) isModify = "1";
            Response.Redirect("/Purchase/vm_mstr.aspx?vm_id=" + vm_id + "&modify=" + isModify);
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex;
        int vm_id = Convert.ToInt32(gv.DataKeys[intRow].Values["ID"]);

        if (mold.DeleteMold(vm_id, Convert.ToInt32(Session["uID"]))) ltlAlert.Text = "alert('删除成功！')";
        else ltlAlert.Text = "alert('删除失败！')";
        BindGridView();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!this.Security["120000310"].isValid)
            {
                e.Row.Cells[10].Visible = false;
            }
        }
    }
    protected void btn_check_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        Response.Redirect("vm_mstrImport.aspx");
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}