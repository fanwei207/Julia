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

public partial class Purchase_RP_WebMstrList : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
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
            string strName = "sp_RP_selectwebList";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@uid", Session["uID"]);
            if (status != "")
            {
                parm[3] = new SqlParameter("@status", status);
            }
            parm[2] = new SqlParameter("@check", CheckBox1.Checked);
            parm[4] = new SqlParameter("@user", txt_user.Text.Trim());
            parm[5] = new SqlParameter("@depart", "");
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.Redirect("RP_WebDETList.aspx");
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
            string id = gvlist.DataKeys[index].Values["RP_id"].ToString().Trim();

            this.Redirect("RP_WebDETList.aspx?MID=" + id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
}