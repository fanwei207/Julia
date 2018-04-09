using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class ProductTypeRelateCateNew : BasePage
{
    private static string strConn = CommClass.admClass.getConnectString("SqlConn.Conn_qaddoc");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearUselessData();
            ClearTempData();
            BindHead();
            BindDocType();
            BindDocName();
            BindData();
        }
    }

    private void BindHead()
    {
        SqlDataReader reader = GetProductTrackingTypeByID(Convert.ToInt32(Request.QueryString["id"]));

        if(reader.Read())
        {
            lblProductType.Text = reader["ptt_type"].ToString();
            lblPackagingType.Text = reader["ptt_detail"].ToString();
            lblRemark.Text = reader["ptt_rmks"].ToString();
            lblRelated.Text = reader["ptt_isRelated"].ToString();
        }
    }

    private void BindDocType()
    {
        string strSql = "";
        ListItem ls = null;
        SqlDataReader reader = null;

        strSql = " Select Distinct typeid,typename From qaddoc.dbo.DocumentType Where isDeleted Is Null Order By typeid ";

        reader = SqlHelper.ExecuteReader(strConn, CommandType.Text, strSql);
            
        while(reader.Read())
        {
            ls = new ListItem();
            ls.Value = reader[0].ToString();
            ls.Text = reader[1].ToString().Trim();
            ddlCategory.Items.Add(ls);
        }

        ddlCategory.Items.Insert(0, new ListItem("--", "0"));

        reader.Close();
    }

    private void BindDocName()
    {
        ddlName.Items.Clear();
        string strSql = "select cateid, catename from qaddoc.dbo.DocumentCategory where typeid =" + ddlCategory.SelectedValue.ToString();
        SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.Text, strSql);

        while (reader.Read())
        {
            ListItem ls = new ListItem();
            ls.Value = reader[0].ToString();
            ls.Text = reader[1].ToString().Trim();
            ddlName.Items.Add(ls);
        }
        reader.Close();

        ddlName.Items.Insert(0, new ListItem("--全部--", "0"));
    }

    private SqlDataReader GetProductTrackingTypeByID(int id)
    {
        SqlParameter param = new SqlParameter("@ppt_id", id);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_selectProductTrackingTypeByIDNew", param);
    }

    private void ClearUselessData()
    {
        string strSql = "delete from qaddoc.dbo.DocumentCategory where catename ='' or catename is null ";
        SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strSql);
    }

    private void ClearTempData()
    {
        string strSql = "delete from qaddoc.dbo.DocumentCategoryTemp where createdBy='" + Session["uID"].ToString() + "' ";
        SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strSql);
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDocName();
        BindData();
    }

    private int InsertProTypeRelateCate(int ptt_id, int type_id, int cate_id, int createBy, string createName)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@ptt_id", ptt_id);
        param[1] = new SqlParameter("@type_id", type_id);
        param[2] = new SqlParameter("@cate_id", cate_id);
        param[3] = new SqlParameter("@createBy", createBy);
        param[4] = new SqlParameter("@createName", createName);
        param[5] = new SqlParameter("@retValue", SqlDbType.Int);
        param[5].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_InsertProTypeRelateCateNew", param);
        return Convert.ToInt32(param[5].Value);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项 Catetory！');";
            return;
        }

        int nRet = InsertProTypeRelateCate(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlName.SelectedValue), Convert.ToInt32(Session["uID"]), Session["uName"].ToString());
        if (nRet == -1)
        {
            ltlAlert.Text = "alert('添加失败，请联系管理员！')";
        }
        else if (nRet == 0)
        {
            ltlAlert.Text = "alert('该关联关系已经存在！')";
        }

        BindData();
    }

    private void BindData()
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ptt_id", Convert.ToInt32(Request.QueryString["id"]));
        param[1] = new SqlParameter("@cateid", ddlCategory.SelectedValue);

        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_selectProTypeRelateCateNew", param).Tables[0];
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "MyDelete")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string ptt_id = param[0].ToString();
            string cate_id = param[1].ToString();

            string strSql = "delete from qaddoc.dbo.ProTypeRelateCateNew where ptt_id =" + ptt_id + "and cate_id =" + cate_id;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strSql);

            BindData();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductTrakingTypeNew.aspx?rm=" + DateTime.Now, true);
    }
}