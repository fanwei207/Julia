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

public partial class Performance_Forum_TypeManage : System.Web.UI.Page
{
    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    private void bind()
    {
        gvInfo.DataSource = this.selectForumType(Convert.ToInt32(chkIsAll.Checked ? 1 : 0));
        gvInfo.DataBind();
    }

    private DataTable selectForumType(int isAll)
    {
        string strSql = "sp_Forum_selectForumType";
        SqlParameter[] parms = new SqlParameter[1];
        parms[0] = new SqlParameter("@isAll", isAll);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, parms).Tables[0];
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Forum_mstr.aspx");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.showModalDialog('Forum_AddType.aspx', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'Forum_TypeManage.aspx'";
    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnDelete")
        {
            int typeID = Convert.ToInt32(e.CommandArgument.ToString());
            if (this.updateTypeToCancel(typeID, Convert.ToInt32(Session["uID"])))
            {
                ltlAlert.Text = "alter('删除成功');";
                bind();
            }
            else
            {
                ltlAlert.Text = "alter('删除失败');";
            }
        }
    }

    private bool updateTypeToCancel(int typeID, int uID)
    {
        string strSql = "sp_Forum_updateTypeToCancel";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@typeID", typeID);
        parms[1] = new SqlParameter("@uID", uID);
        return Convert.ToBoolean( SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, strSql, parms));
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if(((Label)e.Row.Cells[2].FindControl("lbIsCancel")).Text.ToString().Equals("True"))
            {
                ((LinkButton)e.Row.Cells[1].FindControl("lkbtnDelete")).Text = "已删除";
                ((LinkButton)e.Row.Cells[1].FindControl("lkbtnDelete")).Enabled = false;
                ((LinkButton)e.Row.Cells[1].FindControl("lkbtnDelete")).ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void chkIsAll_CheckedChanged(object sender, EventArgs e)
    {
        bind();
    }
}