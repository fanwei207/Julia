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


public partial class Test_Test_exam_det : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getType();
            if (Request.QueryString["ques_id"] != null)
            {
                lblid.Text = Request.QueryString["ques_id"].ToString();
                getmstr();
                btnSaveLine.Enabled = true;
                BindGridView();

            }
        }
    }
    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_test_selectexamdet";
            SqlParameter[] param = new SqlParameter[2];


            param[0] = new SqlParameter("@id", lblid.Text);

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gvlist.DataSource = ds;
            gvlist.DataBind();
        }
        catch
        {
            ;
        }
    }
    public void getmstr()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", lblid.Text.Trim());

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_test_selectexammstrbyID", param);

            if (ds.Tables[0].Rows.Count > 0)
            {

                txtenddate.Text = ds.Tables[0].Rows[0]["exam_enddate"].ToString();
                txtstratdate.Text = ds.Tables[0].Rows[0]["exam_startdate"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["exam_name"].ToString();
                txttime.Text = ds.Tables[0].Rows[0]["exam_alltime"].ToString();


            }
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails while getting po!');";
            BindGridView();
        }
    }
    protected void btnSaveHrd_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@name", txtname.Text.Trim());
            param[1] = new SqlParameter("@startdate", txtstratdate.Text.Trim());
            param[2] = new SqlParameter("@enddate", txtenddate.Text.Trim());
            param[3] = new SqlParameter("@time", txttime.Text.Trim());

            param[5] = new SqlParameter("@createdBy", Session["uID"].ToString());
            param[6] = new SqlParameter("@createdName", Session["uName"].ToString());
            param[7] = new SqlParameter("@retValue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@retid", SqlDbType.NChar, 50);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@categoryid", ddl_category.SelectedValue.ToString());
            if (lblid.Text != "")
            {
                param[10] = new SqlParameter("@id", lblid.Text.Trim());
            }
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_insertexammstr", param);

            if (Convert.ToInt32(param[7].Value) > 0)
            {
                if (param[8].Value.ToString().Trim() != "")
                {
                    lblid.Text = param[8].Value.ToString().Trim();
                }

                //txtDetReqDate.Text = txtReqDate.Text.Trim();
                //txtDetDueDate.Text = txtDueDate.Text.Trim();

               
                btnSaveLine.Enabled = true;
                
                BindGridView();
            }
            else
            {
                ltlAlert.Text = "alert('Operation fails!Please try again!');";
                return;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails!Please try again!');";
            return;
        }
    }
    public void getType()
    {
        try
        {
            string strName = "sp_test_selecttype";

            ddlStatu.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
            ddlStatu.DataBind();
            //ddlStatu.Items.Insert(0, new ListItem("--All--", "0"));
            //ddlStatu.SelectedValue = "-1";
            strName = "sp_test_selectcategory";

            ddl_category.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
            ddl_category.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("Test_exam_view.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void btnSaveLine_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@id", lblid.Text.Trim());
            param[1] = new SqlParameter("@status", ddlStatu.SelectedValue.ToString());
            param[2] = new SqlParameter("@cate", ddl_category.SelectedValue.ToString());
            param[3] = new SqlParameter("@num", txtnum.Text.Trim());
            param[4] = new SqlParameter("@scores", txtscores.Text.Trim());
            param[5] = new SqlParameter("@retValue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@order", txtorder.Text.Trim());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_insertexamdet", param);
            BindGridView();
        }
        catch (Exception)
        {


        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gvlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id", gvlist.DataKeys[e.RowIndex].Values["examd_id"].ToString());


            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_deleteexamdet", param);
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails while deleting!Please try again!');";
            BindGridView();
        }

        BindGridView();
    }
    protected void gvlist_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}