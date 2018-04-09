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

public partial class Test_Test_ques_mstr : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getType();
            BindGridView();
        }
    }

    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_test_selectquesmstr";
            SqlParameter[] param = new SqlParameter[3];
            if (ddlStatu.SelectedValue.ToString() != "0")
            {
               
                param[0] = new SqlParameter("@type", ddlStatu.SelectedValue.ToString());
                param[1] = new SqlParameter("@title", txt_title.Text.Replace("*","%"));
            }
            else

            {

                param[0] = new SqlParameter("@title", txt_title.Text.Replace("*", "%"));
            }
            param[2] = new SqlParameter("@cate", ddl_category.SelectedValue);
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }

    public void getType()
    {
        try
        {
            string strName = "sp_test_selecttype";

            ddlStatu.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
            ddlStatu.DataBind();
            ddlStatu.Items.Insert(0, new ListItem("--All--", "0"));
            ddlStatu.SelectedValue = "-1";

            strName = "sp_test_selectcategory";

            ddl_category.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
            ddl_category.DataBind();

        }
        catch (Exception ex)
        {

        }
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



            this.Redirect("Test_ques_det.aspx?ques_id=" + ques_id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }

        if (e.CommandName == "close")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string ques_id = gv.DataKeys[index].Values["ques_id"].ToString();



            try
            {
                string strName = "sp_test_deletequesmstr";
                SqlParameter[] param = new SqlParameter[3];


                param[1] = new SqlParameter("@id", ques_id);

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
    protected void btnQuery_Click(object sender, EventArgs e)
    {
       
        BindGridView();
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        this.Redirect("Test_ques_det.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}