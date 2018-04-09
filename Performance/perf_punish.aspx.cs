using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;
using CommClass;
using adamFuncs;

public partial class Performance_perf_punish : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_perf_selectpunish";
            SqlParameter[] param = new SqlParameter[6];


            param[0] = new SqlParameter("@userno", txt_NO.Text.Trim());

            param[2] = new SqlParameter("@username", txt_name.Text.Trim());
            param[3] = new SqlParameter("@stratdate", txt_stret.Text.Trim());
            param[4] = new SqlParameter("@enddate", txt_end.Text.Trim());
            param[5] = new SqlParameter("@status", ddl_status.SelectedValue);
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "done")
        {
           
            LinkButton linkPlan = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkPlan.Parent.Parent).RowIndex;
            string id = gv.DataKeys[index].Values["pref_id"].ToString();
            string StrSql = "sp_perf_donepunish";
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@id", id);
         
            param[4] = new SqlParameter("@createdby", Session["uID"].ToString());
            param[5] = new SqlParameter("@createdname", Session["UName"].ToString());
        
            SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, StrSql, param);
            BindGridView();
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv.DataKeys[e.Row.RowIndex].Values["doneby"].ToString() != string.Empty)
            {
                e.Row.Cells[4].Text = gv.DataKeys[e.Row.RowIndex].Values["donename"].ToString();
            }
        }

    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        this.Redirect("perf_punishNew.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}