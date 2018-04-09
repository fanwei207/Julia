using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class plan_so_ReviewConf : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        DataTable dt = GetSuppReviewConf(txtSupp.Text);
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    private DataTable GetSuppReviewConf(string supp)
    {
        SqlParameter pram = new SqlParameter("@suppid", supp);
        return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "sp_pur_getSuppReviewConf", pram).Tables[0];
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Enabled = false;
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "conf")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            Response.Redirect("pur_AddReviewConf.aspx?type=edit&ad_addr=" + gv.DataKeys[index].Values["ad_addr"].ToString() + "&ad_name=" + gv.DataKeys[index].Values["ad_name"].ToString()
                                                   + "&node1=" + gv.DataKeys[index].Values["node1"].ToString() + "&name1=" + gv.DataKeys[index].Values["name1"].ToString()
                                                   + "&node2=" + gv.DataKeys[index].Values["node2"].ToString() + "&name2=" + gv.DataKeys[index].Values["name2"].ToString()
                                                   + "&Node_Id3=" + gv.DataKeys[index].Values["Node_Id3"].ToString() + "&Node_Name=" + gv.DataKeys[index].Values["Node_Name"].ToString()
                                                   + "&isShow=" + gv.DataKeys[index].Values["isShow"].ToString() + "&money=" + gv.DataKeys[index].Values["money"].ToString());
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeleteSuppReviewConf(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString())))
        {
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
    }
    private bool DeleteSuppReviewConf(int id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_pur_DeleteSuppReviewConf", param));
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("pur_AddReviewConf.aspx?type=add");
    }
    protected void btnReach_Click(object sender, EventArgs e)
    {
        BindData();
    }
}