using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class producttrack_ProductTrackingCategory : BasePage
{
    private static string strConn = CommClass.admClass.getConnectString("SqlConn.Conn_qaddoc");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        
        String strPakagingCategory = txtPackagingType.Text.ToString().Trim();
        DataTable dt = getProductTrakingCategory(strPakagingCategory);

        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtPackagingType.Text.ToString().Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入添加的类型名称')";
            return;
        }

        string strProductCategory = txtPackagingType.Text.ToString();
        string userId = Session["uID"].ToString();
        string userName = Session["uName"].ToString();
        Int32 insertflag = insertProductTrakingCategory(strProductCategory, userId, userName);
        if (insertflag > 0)
        {
            if (insertflag == 2)
            {
                ltlAlert.Text = "alert('此产品类型下的分类已存在，不须再添加')";
                return;
            }
            else
            {
                txtPackagingType.Text = "";
                BindData();
            }
        }
        else
        {
            ltlAlert.Text = "alert('添加失败')";
            return;
        } 
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }

    } 

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    { 
        int ptt_cateid = Convert.ToInt32(gv.DataKeys[e.RowIndex][0].ToString());
        if (deleteProductTrakingCategory(ptt_cateid))
        {
            gv.EditIndex = -1;
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('类型删除失败，产品分类中使用该类型')"; 
            return; 
        } 
    }

    protected void gv_RowEditing (object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindData(); 
    }

    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int cateid = Convert.ToInt32(gv.DataKeys[e.RowIndex]["ptt_cateid"].ToString());
        TextBox txtCategory = (TextBox)gv.Rows[e.RowIndex].FindControl("txtGvCategory");

        string strCategory = txtCategory.Text.ToString();
         
        string userId = Session["uID"].ToString();
        string userName = Session["uName"].ToString();

        int updateflag = updateProductTrakingCategory(cateid, strCategory, userId, userName);
        if (updateflag > 0)
        {
            if (updateflag == 2)
            {
                ltlAlert.Text = "alert('此产品名称已存在')";
                return;
            }
            else
            {
                gv.EditIndex = -1;
                BindData();
            }
        }
        else
        {
            ltlAlert.Text = "alert('修改失败')";
            return;
        } 
        if (updateflag <= 0)
        {
            ltlAlert.Text = "alert('产品类型名称修改失败')";
            return; 
        } 
        //else
        //{ 
        //    BindData();
        //}

        BindData();
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    { 
        gv.EditIndex = -1;
        BindData();
    }   

    /// <summary>
    /// 修改类型的名称
    /// </summary>
    /// <param name="cateid"></param>
    /// <param name="strCategory"></param>
    /// <returns></returns>
    private int updateProductTrakingCategory(int cateid, string strCategory, string userId, string userName)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@ptt_cateid", cateid);
        param[1] = new SqlParameter("@ptt_typeName", strCategory);
        param[2] = new SqlParameter("@uId", userId);
        param[3] = new SqlParameter("@uName", userName); 
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_updateProductTrackingCategory", param));
    }

    /// <summary>
    /// 删除类型
    /// </summary>
    /// <param name="ptt_cateid"></param>
    /// <returns></returns>
    private bool deleteProductTrakingCategory(int ptt_cateid)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ptt_cateid", ptt_cateid); 
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_deleteProductTrackingCategory", param));
    }

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="strPakagingType"></param>
    /// <returns></returns>
    private DataTable getProductTrakingCategory(string strPakagingCategory)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ptt_type", strPakagingCategory); 
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_selectProductTrackingCategory", param).Tables[0];
    } 

    /// <summary>
    /// 增加新类型
    /// </summary>
    /// <param name="strProductType"></param>
    /// <returns></returns>
    private int insertProductTrakingCategory(string strCategory, string userId, string userName)
    {
        SqlParameter[] param = new SqlParameter[3]; 
        param[0] = new SqlParameter("@ptt_typeName", strCategory);
        param[1] = new SqlParameter("@uId", userId);
        param[2] = new SqlParameter("@uName", userName);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_insertProductTrackingCategory", param));
    } 
}