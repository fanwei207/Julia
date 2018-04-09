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

public partial class RDW_Template : BasePage
{
    RDW rdw = new RDW();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropOwner.DataSource = rdw.SelectOwner();
            dropOwner.DataBind();
            dropOwner.Items.Insert(0, new ListItem("--", "0"));
            BindData();
        }
    }

    protected void BindData()
    {
        gvRDW.DataSource = rdw.SelectTepmlateMstr(txtProject.Text.Trim(), "--", dropOwner.SelectedValue, Convert.ToInt32(Session["uID"]));
        gvRDW.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRDW.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "myEdit")
        {
            Response.Redirect("RDW_AddTemplate.aspx?id=" + e.CommandArgument.ToString());
        }
        else if (e.CommandName.ToString() == "editTask")
        {
            Response.Redirect("RDW_step.aspx?mid=" + e.CommandArgument.ToString());
        }
        else if (e.CommandName.ToString() == "copyTask")
        {
            Response.Redirect("RDW_AddTemplate.aspx?cp=y&id=" + e.CommandArgument.ToString());
        }
    }
    
    protected void gvRDW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = gvRDW.DataKeys[e.RowIndex].Value.ToString();

        if (rdw.DeleteTemplate(strID))
        {
            ltlAlert.Text = "window.location.href='/RDW/RDW_Template.aspx?rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('Failed to delete template.'); ";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_AddTemplate.aspx?rm=" + DateTime.Now.ToString(), true);
    }

    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                //定义参数
                string strUID = Convert.ToString(Session["uID"]);
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("linkProject");
                LinkButton btnTask = (LinkButton)e.Row.FindControl("btnTask");

                RDW_Header rh = (RDW_Header)e.Row.DataItem; 
                if (rh.RDW_CreatedBy.ToString() == strUID || Convert.ToInt32(Session["uRole"]) == 1)
                {
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;
                    btnTask.Text = "Edit";
                }
                else
                {
                    btnDelete.Enabled =false;
                    btnEdit.Enabled = false;
                    btnTask.Text = "View";
                }
            }
            catch
            { }

        }
    }
}
