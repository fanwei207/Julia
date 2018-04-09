using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;

public partial class rmInspection_rm_subtype : System.Web.UI.Page
{

    private string strConn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblType.Text = Request.QueryString["typename"].ToString();
            lblID.Text = Request.QueryString["typeid"].ToString();

            BindData();
        }
    }

    protected void BindData()
    {
        DataTable table = SelectConn_mstrsubtype(lblID.Text);
        gv.DataSource = table;
        gv.DataBind();

    }

    private DataTable SelectConn_mstrsubtype(string typeid)
    {

        string strSql = "usp_rm_selectMstrSubtype";
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@typeid", typeid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请输入类型名称!');";
            return;
        }
        string strSql = "usp_rm_insertMstrSubtype";
        SqlParameter[] sqlParam = new SqlParameter[5];
        sqlParam[0] = new SqlParameter("@subtypename", txtName.Text.Trim());
        sqlParam[1] = new SqlParameter("@typeid", lblID.Text.Trim());
        sqlParam[2] = new SqlParameter("@uID", Session["uID"]);
        sqlParam[3] = new SqlParameter("@uName", Session["eName"]);
        sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
        sqlParam[4].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

        if (!Convert.ToBoolean(sqlParam[4].Value))
        {
            ltlAlert.Text = "alert('此类型已存在!');";
        }
        BindData();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //order
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                id = gv.PageIndex * 20 + id;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = "usp_rm_deleteMstrSubtype";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@typeid", gv.DataKeys[e.RowIndex].Values["conn_subtypeid"].ToString());
            sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[1].Value))
            {
                ltlAlert.Text = "alert('success!');";
            }
            else
            {
                ltlAlert.Text = "alert('删除失败，此类型已被使用!');";
            }


            BindData();
        }
        catch
        {
            throw new Exception("DB connection error when inserting...");
        }
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string typeId = gv.DataKeys[e.RowIndex].Values["conn_subtypeid"].ToString();
        TextBox txtType = (TextBox)gv.Rows[e.RowIndex].Cells[1].FindControl("txtSubtype");

        try
        {
            string strSql = "usp_rm_updateMstrSubtype";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@typeid", typeId);
            sqlParam[1] = new SqlParameter("@typename", txtType.Text.Trim().ToString());
            sqlParam[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (!Convert.ToBoolean(sqlParam[2].Value))
            {
                ltlAlert.Text = "alert('此类型已存在!');";
                return;
            }
            //BindData();
        }
        catch
        {
            throw new Exception("DB connection error when updating...");
        }

        gv.EditIndex = -1;
        BindData();
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindData();

    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("rm_type.aspx");
    }
}