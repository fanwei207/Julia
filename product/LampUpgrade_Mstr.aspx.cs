using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;


public partial class product_LampUpgrade_Mstr :BasePage
{
    private LampUpgrade bll = new LampUpgrade();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("45304011", "裸灯升级新增申请");
            this.Security.Register("45304012", "裸灯升级BOM组导入");
            this.Security.Register("45304013", "裸灯升级包装组确认");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Security["45304011"].isValid)
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            if (this.Security["45304013"].isValid)
            {
                ddl_status.SelectedIndex = -1;
                ddl_status.Items.FindByValue("10").Selected = true;
            }
            if (this.Security["45304012"].isValid)
            {
                ddl_status.SelectedIndex = -1;
                ddl_status.Items.FindByValue("20").Selected = true;
            }
            
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
 
    protected override void BindGridView()
    {
        gvlist.DataSource = bll.GetProductStruApplyList(txt_nbr.Text.Trim(), txt_prodCode.Text.Trim(), txt_new.Text.Trim(),txt_desc.Text.Trim(), ddl_status.SelectedValue);
        gvlist.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.Redirect("LampUpgradeApply_New.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    
    protected void gvlist_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirm")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvlist.DataKeys[index].Values["id"].ToString().Trim();

            this.Redirect("LampUpgradeApply_New.aspx?id=" + id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gvlist_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}