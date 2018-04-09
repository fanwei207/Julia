using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class part_Part_ItemUpdateList : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"]))
            {
                ddl_status.SelectedValue = Request.QueryString["status"];
            }
            string str = Request.QueryString["byself"];
            if (!string.IsNullOrEmpty(Request.QueryString["byself"]))
            {
                CheckBox1.Checked = Convert.ToBoolean(Request.QueryString["byself"]);
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected override void BindGridView()
    {
        gvlist.DataSource = GetProductStruApplyList(txt_nbr.Text.Trim(), ddl_status.SelectedValue);
        gvlist.DataBind();
    }
    public DataTable GetProductStruApplyList(string nbr, string status)
    {
        try
        {
            string strName = "sp_Part_selectItemupdateMstr";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@uid", Session["uID"]);
            if (status != "")
            {
                parm[3] = new SqlParameter("@status", status);
            }
            parm[2] = new SqlParameter("@check", CheckBox1.Checked);
            parm[4] = new SqlParameter("@user", txt_user.Text.Trim());
         
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirm")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvlist.DataKeys[index].Values["id"].ToString().Trim();
            string status = ddl_status.SelectedValue;
            bool byself = CheckBox1.Checked;

            this.Redirect("Part_ItemUpdateNew.aspx?id=" + id + "&status=" + status + "&byself=" + byself + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.Redirect("Part_ItemUpdateNew.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}