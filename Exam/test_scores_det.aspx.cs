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

public partial class Test_test_scores_det : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
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
            string strName = "sp_test_selectscoresdet";
            SqlParameter[] param = new SqlParameter[4];


            param[0] = new SqlParameter("@markid", Request.QueryString["mark_id"]);
          
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        this.Redirect("test_scores_mstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string ques_id = gv.DataKeys[index].Values["ques_id"].ToString();



            this.Redirect("Test_ques_det.aspx?ques_id=" + ques_id + "&mark_id=" + Request.QueryString["mark_id"] + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}