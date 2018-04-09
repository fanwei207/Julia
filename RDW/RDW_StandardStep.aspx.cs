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

public partial class RDW_RDW_StandardStep : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindMstr();
            if (Request.QueryString["mid"] != null)
            {
                ddlMstr.SelectedValue = Request.QueryString["mid"];
            }
            GridBind();
        }
    }
    public void GridBind() 
        {
            int uID = int.Parse(Session["uID"].ToString());
          

            gvRDW.DataSource = SelectTasks();
            gvRDW.DataBind();
        }

    private void BindMstr()
    {
        string strSql = "select RDW_Id,RDW_Name from RDW_StandardStep_Mstr";
        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql).Tables[0];
        ddlMstr.DataSource = dt;
        ddlMstr.DataBind();

    }

    public DataTable SelectTasks()
    {
        string strName = "sp_RDW_SelectStandardStep";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@mstrId", ddlMstr.SelectedValue);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName,param).Tables[0];
    }
    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRDW.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "Desc")
                {
                    int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
                   // string RDW_StepName = gvRDW.DataKeys[index].Values["RDW_StepName"].ToString();
                    Response.Redirect("RDW_AddStandardStep.aspx?id=" + e.CommandArgument.ToString() + "&mid=" + ddlMstr.SelectedValue + "&rm=" + DateTime.Now.ToString());
                }
         else if (e.CommandName == "UP")
         {
             CustomSortTasks(e.CommandArgument.ToString(), "UP");

             GridBind();
         }
         else if (e.CommandName == "DOWN")
         {
             CustomSortTasks(e.CommandArgument.ToString(), "DOWN");

             GridBind();
         }
    }
    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvRDW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = gvRDW.DataKeys[e.RowIndex].Values["RDW_Code"].ToString();
        if (DeleteTasks( ID))
        {
            ltlAlert.Text = "Form1.txtStep.focus();";
        }

        GridBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_AddStandardStep.aspx?&mid=" + ddlMstr.SelectedValue + "&rm=" + DateTime.Now.ToString());
    }
    public bool DeleteTasks( string id)
    {
        try
        {
            string strName = "sp_RDW_DeleteStandardStep";
            SqlParameter[] param = new SqlParameter[1];
            
            param[0] = new SqlParameter("@id", id);

            int nRet = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (nRet > -1)
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
    }
    public void CustomSortTasks(string id, string cmd)
    {
        try
        {
            string strName = "sp_RDW_CustomSortStandardStep";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@cmd", cmd);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        }
        catch
        {

        }
    }
    protected void ddlMstr_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridBind();
    }
}