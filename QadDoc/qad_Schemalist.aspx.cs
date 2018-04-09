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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class qad_Schemalist : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_qaddoc"];
    QadDoc qaddoc = new QadDoc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }
    private void GridViewBind() 
    {
        try
        {
            gv.DataSource = qaddoc.GetSchemaList(txt_Schemaname.Text.Trim());
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int i = qaddoc.DeleteSchema(gv.DataKeys[e.RowIndex].Values["Schemaid"].ToString(), Session["uID"].ToString(), Session["uName"].ToString());
        if (i == 1)
        {
            this.Alert("Schema已被Category引用不能删除！");
            return;
        }
        else if (i == 2 || i == 3)
        {
            this.Alert("删除失败！");
            return;
        }
        else
        {
            this.Alert("删除成功！");
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string Schemaid = gv.DataKeys[e.RowIndex].Values["Schemaid"].ToString();
        TextBox Schemaname = (TextBox)gv.Rows[e.RowIndex].FindControl("txt_Schemaname");
        if (Schemaname.Text.Length == 0)
        {
            this.Alert("类型名称不能为空！");
            return;
        }

        int i = qaddoc.UpateSchema(Schemaid, Schemaname.Text == "" ? "" : Schemaname.Text, Session["uID"].ToString(), Session["uName"].ToString());
        if (i == 1)
        {
            this.Alert("Schema已被Category引用不能删除！");
            return;
        }
        else if (i == 2)
        {
            this.Alert("已存在相同名称！");
            return;
        }
        else if (i == 3 || i == 4) 
        {
            this.Alert("保存失败,请联系管理员！");
            return;
        }
        this.Alert("修改成功！");
        gv.EditIndex = -1;
        GridViewBind();
    }
    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_Schemaname.Text))
        {
            this.Alert("Schema  name is required!");
            return;
        }

        int i = qaddoc.SaveSchema(txt_Schemaname.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());

        if (i == 1)
        {
            this.Alert("已存在相同记录!");
            return;
        }
        if (i == 2)
        {
            this.Alert("记录保存失败!");
            return;
        }
        this.Alert("记录保存成功!");
        GridViewBind();
    }

}
