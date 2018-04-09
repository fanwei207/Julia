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
using QCProgress;

public partial class QC_qc_project_item : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            lblProject.Text = Request.QueryString["pro"].ToString();

            ogv.GridViewDataBind(gvProject, oqc.GetProjectItemByName(lblProject.Text));
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        string strMsg = "";

        oqc.AddProjectItem(lblProject.Text.Trim(), int.Parse(Session["uID"].ToString()), ref strMsg);

        if (strMsg != "")
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
        }

        gvProject.EditIndex = 0;

        ogv.GridViewDataBind(gvProject, oqc.GetProjectItemByName(lblProject.Text));
    }
    protected void gvProject_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvProject.EditIndex =e.NewEditIndex;

        gvProject.DataSource = oqc.GetProjectItemByName(lblProject.Text);
        gvProject.DataBind();
    }
    protected void gvProject_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProject.EditIndex = -1;

        ogv.GridViewDataBind(gvProject, oqc.GetProjectItemByName(lblProject.Text));
    }
    protected void gvProject_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int intItemID = int.Parse(gvProject.DataKeys[e.RowIndex].Value.ToString());
        string strName = ((TextBox)gvProject.Rows[e.RowIndex].Cells[1].FindControl("txtItem")).Text.Trim();

        string strMsg = "";

        oqc.ModifyProjectItem(intItemID, lblProject.Text.Trim(), strName, int.Parse(Session["uID"].ToString()), ref strMsg);

        if (strMsg != "")
        {
            ltlAlert.Text = "alert('" + strMsg + "');";
            return;
        }


        gvProject.EditIndex = -1;

        ogv.GridViewDataBind(gvProject, oqc.GetProjectItemByName(lblProject.Text));
    }
    protected void gvProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intItemID = int.Parse(gvProject.DataKeys[e.RowIndex].Value.ToString());

        //delete Project
        oqc.DeleteProjectItem(intItemID);

        ogv.GridViewDataBind(gvProject, oqc.GetProjectItemByName(lblProject.Text));
    }
    protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("qc_project.aspx");
    }
    protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProject.PageIndex = e.NewPageIndex;

        ogv.GridViewDataBind(gvProject, oqc.GetProjectItemByName(lblProject.Text));
    }
}
