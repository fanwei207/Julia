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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

public partial class soque_list : BasePage 
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!this.Security["44000012"].isValid)
            {
                btnClose.Visible = false;
                gvlist.Columns[13].Visible = false;
            }

            BindGridView();
        }
    }

    protected void BindGridView()
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@nbr", txtNbr.Text.Trim());
        param[1] = new SqlParameter("@part", txtPart.Text.Trim());
        param[2] = new SqlParameter("@cust", txtCust.Text.Trim());
        param[3] = new SqlParameter("@createdBy", txtCreatedBy.Text.Trim());
        param[4] = new SqlParameter("@status", ddlStatus.SelectedValue);
        param[5] = new SqlParameter("@CloseDate",txtCloseDate.Text.Trim());

        DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_selectSoQueList", param);

        gvlist.DataSource = ds.Tables[0];
        gvlist.DataBind();
    }

  

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gvlist.DataKeys[e.Row.RowIndex].Values["soque_status"].ToString() == "Finish")
            {
                e.Row.Cells[13].Text = string.Empty;
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        
        if (txtCloseDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime date = Convert.ToDateTime(txtCloseDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('The Close Date formate is error!Please enter a true!');Form1.txtCloseDate.focus();";

            }
            //额外添加的功能，如果closedate有值且不出错则将选择改为已完成
            ddlStatus.SelectedValue = "1";
           
        }
       
         BindGridView(); 
        
        
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('" + "soque_list_export.aspx?nbr=" + txtNbr.Text.Trim() + "&cust=" + txtCust.Text.Trim() + "&part=" + txtPart.Text.Trim() + "&crt=" + txtCreatedBy.Text.Trim() + "&close=" + ddlStatus.SelectedValue + "', '_blank');";
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            Response.Redirect("soque_edit.aspx?id=" + e.CommandArgument.ToString() +"&rt=" + DateTime.Now.ToString());
        }
        if (e.CommandName == "myHist")
        {
            ltlAlert.Text = "window.open('" + "soque_message.aspx?id=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvlist.Rows)
        {
            if (((CheckBox)row.FindControl("chk")).Checked)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@id", gvlist.DataKeys[row.RowIndex].Values["soque_id"].ToString());
                    param[1] = new SqlParameter("@uID", Session["uID"].ToString());
                    param[2] = new SqlParameter("@uName", Session["uName"].ToString());

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_closeSoQueList", param);
                }
                catch
                {
                    ltlAlert.Text = "alert('Operation Failure！');";

                    break;
                }
            }
        }

        BindGridView();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
}
