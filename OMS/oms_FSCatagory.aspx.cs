using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class Factory_Status_Factory_Status_Type_Maintainance : BasePage
{
    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvBind();
        }
    }
    protected void gvBind()
    {
        gv.DataSource = OMSHelper.GetFSCategory(txtName.Text.Trim(),txtDesc.Text.Trim());
        gv.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim() == string.Empty)
        {
            this.Alert("Catagory name can't be empty!");
            return;
        }
        if (btnAdd.Text == "Add")
        {
            if (!OMSHelper.CheckFSCatagory(txtName.Text.Trim(),0))
            {
                this.Alert("This Catagory Exists！");
                return;
            }
            if (OMSHelper.InsertFSCatagory(txtName.Text.Trim(), txtDesc.Text.Trim(), Convert.ToInt32(Session["uId"]), Session["uName"].ToString()))
            {
                this.Alert(" Add Success!"); 
            }
            else
            {
                this.Alert("Add Failed！!");
            }
        }
        if (btnAdd.Text.Trim() == "Modify")
        {
            int id = Convert.ToInt32(lblCaId.Text.ToString());
            if (!OMSHelper.CheckFSCatagory(txtName.Text.Trim(), id))
            {
                this.Alert("This Catagory Exists！");
                return;
            }
            if (!OMSHelper.ModifyFSCatagory(txtName.Text.Trim(), txtDesc.Text.Trim(), id))
            {
                this.Alert("Modify Failed");
                return;
            }
            else
            {
                this.Alert("Modify Success!");
            }
        }
        txtName.Text = "";
        txtDesc.Text = "";
        btnAdd.Text = "Add";
        gvBind();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtDesc.Text = "";
        btnAdd.Text = "Add";
        gvBind();

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Edit1")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            //  index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gv.DataKeys[index].Values["id"].ToString();
            lblCaId.Text = id;
            txtName.Text = gv.Rows[index].Cells[0].Text.Trim();
            txtDesc.Text = gv.Rows[index].Cells[1].Text.Trim() == "&nbsp;" ? "" : gv.Rows[index].Cells[1].Text.Trim();
            btnAdd.Text = "Modify";
        }
        if (e.CommandName == "Delete1")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            //  index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gv.DataKeys[index].Values["id"].ToString();
            lblCaId.Text = id;
            if (OMSHelper.CheckFDeleteFSCatagory(Convert.ToInt32(lblCaId.Text)))
            {
                this.Alert("This catagory has been used!");
                return;
            }
            if (!OMSHelper.DeleteFSCatagory(Convert.ToInt32(lblCaId.Text)))
            {
                this.Alert("Delete Failed!");
            }
            else
            {
                this.Alert("Delete Success!!!");
            }
            txtName.Text = "";
            txtDesc.Text = "";
            btnAdd.Text = "Add";
        }
        gvBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        gvBind();
    }
}