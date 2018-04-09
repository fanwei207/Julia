using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Rec_AddReport : System.Web.UI.Page
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_GridView();
        }
    }
    protected void Load_GridView()
    {
        
            string sql = "select * from [WorkFlow].[dbo].[Rec_RecipientConfig]";
            gvReport.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql);
            gvReport.DataBind();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rec_RecipientConfig.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtRepName.Text.Trim()=="")
        {
            ltlAlert.Text = "alert('报表名称不能为空！');";
            return;
        }
        string sql = "sp_Rec_AddReport";

        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@reportname", txtRepName.Text.Trim());
        param[1] = new SqlParameter("@description", txtDescrip.Text.Trim());

        int re = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

        if (re == 1)
        {
            ltlAlert.Text = "alert('添加成功！');";
        }
        else
            ltlAlert.Text = "alert('已存在此报表，不能重复添加！')";
        Load_GridView();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (txtRepName.Text.Trim() == "")
        {
            ltlAlert.Text = "alert('报表名称不能为空！');";
            return;
        }
        string sql = "delete Rec_RecipientConfig where reportname=N'"+txtRepName.Text.Trim()+"'";

        if (SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, sql) > 0)
        {
            ltlAlert.Text = "alert('删除成功啦！')";
        }
        else ltlAlert.Text = "alert('删除失败了！')";
    }
    protected void gvReport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvReport.EditIndex = e.NewEditIndex;
        //gvReport.EditIndex = -1;
        Load_GridView();
    }
    protected void gvReport_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
        string sql = "sp_Rec_updateReport";

        int row = gvReport.SelectedIndex;

        SqlParameter[] Param = new SqlParameter[2];
        Param[0] = new SqlParameter("@id", gvReport.DataKeys[e.RowIndex].Value.ToString());
        Param[1] = new SqlParameter("@desc", ((TextBox)gvReport.Rows[e.RowIndex].FindControl("txtDes")).Text);

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, Param);

        gvReport.EditIndex = -1;
        Load_GridView();
    }
    protected void gvReport_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvReport.EditIndex = -1;
        Load_GridView();
    }
}