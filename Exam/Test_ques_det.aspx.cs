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
using System.Text.RegularExpressions;
using System.Web.Security;
public partial class Test_Text_ques_det : BasePage

{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("25000021", "试题修改权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            btnSaveHrd.Visible = this.Security["25000021"].isValid;
            btnSaveLine.Visible = this.Security["25000021"].isValid;
            gvlist.Columns[2].Visible = this.Security["25000021"].isValid;
            getType();
            if (Request.QueryString["ques_id"] != null)
            {
                lblid.Text = Request.QueryString["ques_id"].ToString();
                getmstr();
                if (ddlStatu.SelectedItem.Text == "单选题" || ddlStatu.SelectedItem.Text == "多选题")
                {
                    btnSaveLine.Enabled = true;
                  
                }
                BindGridView();

            }
        }
    }
    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_test_selectquesdet";
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

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_test_selectquesmstrbyID", param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtanswer.Text = ds.Tables[0].Rows[0]["ques_answer"].ToString();
                txttitle.Text = ds.Tables[0].Rows[0]["ques_title"].ToString();
                txtenddate.Text = ds.Tables[0].Rows[0]["ques_enddate"].ToString();
                txtstratdate.Text = ds.Tables[0].Rows[0]["ques_startdate"].ToString();
                ddlStatu.SelectedValue = ds.Tables[0].Rows[0]["ques_title"].ToString();
                if (ds.Tables[0].Rows[0]["ques_key"].ToString() == "0")
                {
                    ckb_key.Checked = false;
                }
                else
                {
                    ckb_key.Checked = true;
                }
               
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails while getting po!');";
            BindGridView();
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
    protected void btnSaveHrd_Click(object sender, EventArgs e)
    {
        try
        {
            int key=0;
            if (ckb_key.Checked)
            {
                key = 1;
            }

            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@typeid", ddlStatu.SelectedValue.ToString());
            param[1] = new SqlParameter("@startdate",txtstratdate.Text.Trim() );
            param[2] = new SqlParameter("@enddate", txtenddate.Text.Trim());
            param[3] = new SqlParameter("@title", txttitle.Text.Trim());
            param[4] = new SqlParameter("@anwser", txtanswer.Text.Trim());
           
            param[5] = new SqlParameter("@createdBy", Session["uID"].ToString());
            param[6] = new SqlParameter("@createdName", Session["uName"].ToString());
            param[7] = new SqlParameter("@retValue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@retid", SqlDbType.NChar, 50);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@categoryid", ddl_category.SelectedValue.ToString());
            param[10] = new SqlParameter("@key", key.ToString());

            if (lblid.Text != "")
            {
                param[11] = new SqlParameter("@id", lblid.Text.Trim());
            }
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_insertquesmstr", param);

            if (Convert.ToInt32(param[7].Value) > 0)
            {
                if (param[8].Value.ToString().Trim() != "")
                {
                    lblid.Text = param[8].Value.ToString().Trim();
                }

                //txtDetReqDate.Text = txtReqDate.Text.Trim();
                //txtDetDueDate.Text = txtDueDate.Text.Trim();

                if (ddlStatu.SelectedItem.Text == "单选题" || ddlStatu.SelectedItem.Text == "多选题")
                {
                    btnSaveLine.Enabled = true;
                }
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
    protected void btnBack_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["mark_id"] != null)
        {
            this.Redirect("test_scores_det.aspx?mark_id=" + Request.QueryString["mark_id"] + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else
        {
            this.Redirect("Test_ques_mstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btnSaveLine_Click(object sender, EventArgs e)
    {

        try
        {
             SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", lblid.Text.Trim());
            param[1] = new SqlParameter("@mark",txtmark.Text.Trim() );
            param[2] = new SqlParameter("@detil", txtdetil.Text.Trim());
          
            param[3] = new SqlParameter("@retValue", SqlDbType.Int);
            param[3].Direction = ParameterDirection.Output;
         
            
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_insertquesdet", param);
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
            param[0] = new SqlParameter("@id", gvlist.DataKeys[e.RowIndex].Values["quesd_id"].ToString());


            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_deletequesdet", param);
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