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
using adamFuncs;

public partial class Purchase_RP_ld_search : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["qad"] != null)
            {
                txt_QAD.Text = Request["qad"].ToString();
                txtsite.Text = Request["site"].ToString();
            }
        }
    }
    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_RP_selectlddet";
            SqlParameter[] param = new SqlParameter[3];


            param[0] = new SqlParameter("@qad", txt_QAD.Text);
            param[1] = new SqlParameter("@site", txtsite.Text);


            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strName, param);
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
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
