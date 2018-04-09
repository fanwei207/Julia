using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class part_Npart_typeManage : BasePage
{
    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.Empty.Equals(Request.QueryString["typeName"]))
            {
                txtTypeName.Text = Request.QueryString["typeName"];
            }
            Bind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind();
    }

    private void Bind()
    {
        gvInfo.DataSource = help.selectAllTypeMstr(txtTypeName.Text.Trim());
        gvInfo.DataBind();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string isCanUpdate = gvInfo.DataKeys[e.Row.RowIndex].Values["isCanUpdate"].ToString();
            if (isCanUpdate != "True")
            {
                ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "";
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "删除";
            }
            
        }
    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Npart_partTypeNew.aspx");
    }
    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnEdit")
        {
            string mstrID = e.CommandArgument.ToString();
            Response.Redirect("Npart_partTypeDetEdit.aspx?mstrID=" + mstrID);
        }
        else if (e.CommandName == "lkbtnDelete")
        { 
            string mstrID = e.CommandArgument.ToString();

            string flag = help.deleteTypeMstrByID(mstrID);

            if (flag.Equals("1"))
            {
                Alert("删除成功");
                Bind();
            }
            else if (flag.Equals("2"))
            {
                Alert("您的模板已经使用过，原则上不允许删除，若需要修改请联系管理员");
            }
            else
            {
                Alert("删除失败");
            }
        
        }
    }
}