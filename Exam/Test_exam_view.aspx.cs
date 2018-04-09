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


public partial class Test_Test_exam_view : BasePage
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
            string strName = "sp_test_selectexammstr";
            SqlParameter[] param = new SqlParameter[3];


            param[0] = new SqlParameter("@title", txtname.Text.Trim());
            param[1] = new SqlParameter("@startdate", txtstartdate.Text.Trim());
            param[2] = new SqlParameter("@enddate",txtenddate.Text.Trim());
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        this.Redirect("Test_exam_det.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
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
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string ques_id = gv.DataKeys[index].Values["exam_id"].ToString();



            this.Redirect("Test_exam_det.aspx?ques_id=" + ques_id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }

        if (e.CommandName == "person")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string ques_id = gv.DataKeys[index].Values["exam_id"].ToString();



            this.Redirect("Test_person_det.aspx?exam_id=" + ques_id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        if (e.CommandName == "add")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string ques_id = gv.DataKeys[index].Values["exam_id"].ToString();



            this.Redirect("Test_exam_person.aspx?exam_id=" + ques_id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }


        if (e.CommandName == "close")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string exam_id = gv.DataKeys[index].Values["exam_id"].ToString();



            try
            {
                string strName = "sp_test_deleteexammstr";
                SqlParameter[] param = new SqlParameter[3];


                param[1] = new SqlParameter("@examid", exam_id);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
                BindGridView();
            }
            catch
            {
                ;
            }
        }

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}