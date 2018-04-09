using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO; 
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient; 

public partial class rm_type : BasePage
{
    string strConn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            BindData();
        }
    }

    protected void BindData()
    {
        gv.DataSource = SelectConn_mstrtype(txtType.Text.Trim());
        gv.DataBind();
    }

    private DataSet SelectConn_mstrtype(string typename)
    {

        string strSql = "usp_rm_selectMstrtype";
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@typename", typename); 
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam); 
    } 

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
    
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (txtType.Text.Trim().Length <= 0)
        { 
            ltlAlert.Text = "alert('请输入类别名称!');";
            return;
        }
         
        //try
        //{
            string strSql = "usp_rm_insertMstrtype";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@typename", txtType.Text.Trim());
            sqlParam[1] = new SqlParameter("@uID", Session["uID"]);
            sqlParam[2] = new SqlParameter("@uName", Session["eName"]); 
            sqlParam[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery( strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (!Convert.ToBoolean(sqlParam[3].Value))
            {
              ltlAlert.Text = "alert('此类别已存在!');";
            } 
            BindData();
        //}
        //catch
        //{
        //    throw new Exception("DB connection error when inserting...");
        //}
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = "usp_rm_deleteMstrtype";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@typeid", gv.DataKeys[e.RowIndex].Values["conn_typeid"].ToString());
            sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[1].Value))
            {
                ltlAlert.Text = "alert('success!');";
            }
            else
            {
                ltlAlert.Text = "alert('删除失败，此类别已被使用!');";
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
        string typeId = gv.DataKeys[e.RowIndex].Values["conn_typeid"].ToString();
        TextBox txtType = (TextBox)gv.Rows[e.RowIndex].Cells[0].Controls[0]; 

        try
        {
            string strSql = "usp_rm_updateMstrtype";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@typeid", typeId);
            sqlParam[1] = new SqlParameter("@typename", txtType.Text.Trim().ToString()); 
            sqlParam[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

            if (!Convert.ToBoolean(sqlParam[2].Value))
            {
                ltlAlert.Text = "alert('此类别已存在!');";
                return;
            } 
            BindData();
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
         
}
