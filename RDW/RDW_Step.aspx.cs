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
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace RD_WorkFlow
{
    public partial class RDW_rdw_step : BasePage
    {
        RD_Steps step = new RD_Steps();
        String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
        RDW rdw = new RDW();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbMID.Text = "";
                int uID = int.Parse(Session["uID"].ToString());
                if (Request.QueryString["mid"] != null)
                {
                    lbMID.Text = Request.QueryString["mid"].ToString();

                    RDW_Header header = rdw.SelectTepmlateMstr(lbMID.Text);
                    lblProject.Text = "Project: " + header.RDW_Project;
                }                 
              
                string strSql = "select isnull(RDW_CreatedBy,0) from RD_Workflow.dbo.RDW_TepmlateMstr where RDW_MstrID=" + lbMID.Text + "and RDW_CreatedBy=" + uID.ToString();
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.Text, strSql)) > 0 || Convert.ToInt32(Session["uRole"])==1)
                {
                    btnAdd.Enabled = true;
  
                }
                else
                {
                    btnAdd.Enabled = false;
                }

                GridBind();
            }
        }

        public void GridBind() 
        {
            int uID = int.Parse(Session["uID"].ToString());
            int mid = int.Parse(Request.QueryString["mid"].ToString());

            gvRDW.DataSource = step.SelectTasks(mid);
            gvRDW.DataBind();
        }

        protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkProName = e.Row.Cells[1].FindControl("linkProName") as LinkButton;
                if (linkProName != null)
                {
                    linkProName.Text = linkProName.Text.Replace("\r\n", "<br />");
                }
            }
        }
        protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (btnAdd.Enabled == true)
            {
                if (e.CommandName == "Task")
                {
                    Response.Redirect("RDW_AddSubTask.aspx?vr=" + Session["statusStore"] + "&mid=" + lbMID.Text + "&id=" + e.CommandArgument.ToString() + "&rm=" + DateTime.Now.ToString());
                }
                else if (e.CommandName == "Desc")
                {
                    Response.Redirect("RDW_AddTask.aspx?vr=" + Session["statusStore"] + "&mid=" + lbMID.Text + "&id=" + e.CommandArgument.ToString() + "&rm=" + DateTime.Now.ToString());
                }
                else if (e.CommandName == "AUDIT_BY")
                {
                    string StepID = e.CommandArgument.ToString();
                    ltlAlert.Text = "window.open('/RDW/rdw_step_mbr.aspx?t=m&flag=2&mid=" + lbMID.Text + "&StepID"+StepID+"&sid=" + e.CommandArgument.ToString() + "&fr=&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";
                    GridBind();
                }
                else if (e.CommandName == "PARTNER")
                {
                    ltlAlert.Text = "window.open('/RDW/rdw_step_mbr.aspx?t=p&flag=2&mid=" + lbMID.Text + "&sid=" + e.CommandArgument.ToString() + "&fr=&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";
                    GridBind();
                }
                else if (e.CommandName == "UP")
                {
                    step.CustomSortTasks(lbMID.Text, e.CommandArgument.ToString(), "UP");

                    GridBind();
                }
                else if (e.CommandName == "DOWN")
                {
                    step.CustomSortTasks(lbMID.Text, e.CommandArgument.ToString(), "DOWN");

                    GridBind();
                }
            }
        }

        protected void gvRDW_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = int.Parse(gvRDW.DataKeys[e.RowIndex].Value.ToString());
            if (step.DeleteTasks(Convert.ToInt32(lbMID.Text), ID))
            {
                ltlAlert.Text = "Form1.txtStep.focus();";
            }

            GridBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("RDW_AddTask.aspx?mid=" + Request.QueryString["mid"].ToString() + "&rm=" + DateTime.Now.ToString());
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RDW_Template.aspx?rm=" + DateTime.Now.ToString());
        }
        protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRDW.PageIndex = e.NewPageIndex;
            GridBind();
        }
}
}