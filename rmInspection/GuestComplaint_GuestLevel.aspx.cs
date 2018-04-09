using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmInspection_GuestComplaint_GuestLevel : System.Web.UI.Page
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        gv.DataSource = SelectGuest_Level(txtLevel.Text.Trim());
        gv.DataBind();
    }

    private DataSet SelectGuest_Level(string levelName)
    {
        string str = "sp_selectGuestLevel";
        SqlParameter param = new SqlParameter("@levelName", levelName);
        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (txtLevel.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alter('请输入客户等级');";
            return;
        }

        string strSql = "sp_insertGuestLevel";
        SqlParameter[] sqlParam = new SqlParameter[4];
        sqlParam[0] = new SqlParameter("@levelname", txtLevel.Text.Trim());
        sqlParam[1] = new SqlParameter("@uID", Session["uID"]);
        sqlParam[2] = new SqlParameter("@uName", Session["uName"]);
        sqlParam[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        sqlParam[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);

        if (Convert.ToBoolean(sqlParam[3].Value))
        {
            ltlAlert.Text = "alert('此类别已存在')";
        }
        txtLevel.Text = " ";
        BindData();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = "sp_deleteGuestLevel";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@levelid", gv.DataKeys[e.RowIndex].Values["LevelID"].ToString());
            sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[1].Value))
            {
                ltlAlert.Text = "alert('删除成功!');";
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
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string levelid = gv.DataKeys[e.RowIndex].Values["LevelID"].ToString();
        TextBox txtLevel = (TextBox)gv.Rows[e.RowIndex].Cells[0].Controls[0];        

        try
        {
            string strSql = "sp_updateGuestLevel";
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@levelid", levelid);
            sqlParam[1] = new SqlParameter("@levelname", txtLevel.Text.Trim().ToString());            
            sqlParam[2] = new SqlParameter("@uID", Session["uID"]);
            sqlParam[3] = new SqlParameter("@uName", Session["uName"]);
            sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[4].Value))
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
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindData();
    }
}