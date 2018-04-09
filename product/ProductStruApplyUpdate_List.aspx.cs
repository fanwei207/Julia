using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductStruApplyUpdate_List : BasePage
{
    private ProductStru bll = new ProductStru();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.Security.Register("45301011", "BOM新增申请");
            //this.Security.Register("45301012", "BOM组新增确认");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Security["45301050"].isValid)
            {
                btnUpdate.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
            }
            if (this.Security["45301040"].isValid)
            {
                ddl_status.SelectedIndex = -1;
                ddl_status.Items.FindByValue("10").Selected = true;
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected override void BindGridView()
    {
        gvlist.DataSource = bll.GetProductStruApplyUpdateList(txt_nbr.Text.Trim(), txt_prodCode.Text.Trim(), txt_desc.Text.Trim(), ddl_status.SelectedValue, Convert.ToInt32(Session["uID"]));
        gvlist.DataBind();
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirm")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvlist.DataKeys[index].Values["id"].ToString().Trim();

            this.Redirect("ProductStruApply_Update.aspx?id=" + id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        this.Redirect("ProductStruApply_Update.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}