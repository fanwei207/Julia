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
public partial class Test_Test_person_det : BasePage
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
            string strName = "sp_test_selectperson";
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@code", dropPlant.SelectedValue);
            param[1] = new SqlParameter("@user", txtstartdate.Text);
            param[2] = new SqlParameter("@examid", Request.QueryString["exam_id"].ToString());
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {

    }
    protected void btnback_Click(object sender, EventArgs e)
    {
          
                 this.Redirect("Test_exam_view.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "close")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string uid = gv.DataKeys[index].Values["Person_UserId"].ToString();



            try
            {
                string strName = "sp_test_deleteperson";
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@uid", uid);
                param[1] = new SqlParameter("@examid", Request.QueryString["exam_id"].ToString());
              
               SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
               BindGridView();
            }
            catch
            {
                ;
            }
        }

    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}