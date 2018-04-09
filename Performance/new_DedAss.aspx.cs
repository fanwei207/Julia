using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class new_DedAss : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label1.Enabled = false;
            BindData();
        }
    }
    protected void BindData()
    {
        string str = "Select perfd_id, perfd_type, perfd_desc From tcpc0..perf_deduct_new  Order by perfd_id desc";
        showType.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str).Tables[0];
        showType.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ModifyDesc")
        {
            Label1.Text = e.CommandArgument.ToString().Trim();
            Label1.Visible = false;
            SqlParameter param = new SqlParameter("@perfd_id", e.CommandArgument.ToString().Trim());
            SqlDataReader reader1 = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_perf_selectDedAssessmentById", param);
            if (reader1.Read())
            {
                TextBox2.Text = Convert.ToString(reader1["perfd_type"]);
                txtNote.Text = Convert.ToString(reader1["perfd_desc"]);
            }
            reader1.Close();
            btnAdd.Visible = false;
            btnupdate.Visible = true;
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
        }
    }
    /// <summary>
    /// 根据ID编辑扣分名称和备注
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string perfd_id =Label1.Text;
        string perfd_type = TextBox2.Text;
        string perfd_desc = txtNote.Text;
        
        Int32 insertflag = updateProductTrackingType(perfd_id,perfd_type, perfd_desc);
        if (insertflag > 0)
        {
            if (insertflag == 2)
            {
                ltlAlert.Text = "alert('此扣分类型已存在，无法修改')";
                return;
            }
            else
            {
                Label1.Visible = false;
                btnAdd.Visible = true;
                btnupdate.Visible = false;
                TextBox2.Text = string.Empty;
                txtNote.Text = string.Empty;
                BindData();
            }
        }
        else
        {
            ltlAlert.Text = "alert('修改失败')";
            return;
        }
    }
    private int updateProductTrackingType(string perfd_id,string perfd_type, string txt_Note)
    {

        string str = "sp_perf_updateDedAssessmentById";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@perfd_id", perfd_id);
        param[1] = new SqlParameter("@perfd_type", perfd_type);
        param[2] = new SqlParameter("@perfd_desc", txt_Note);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 添加新的扣分名称
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text == "")
        {
            Label1.Text = string.Empty;
            ltlAlert.Text = "alert('请填写扣分类型')";
            return;
        }
        else 
        {
            Label1.Text = string.Empty;
            string perfd_type = TextBox2.Text;
            string txt_Note=txtNote.Text;
            Int32 insertflag = InsertProductTrackingType(perfd_type, txt_Note);
            if (insertflag > 0)
            {
                if (insertflag == 2)
                {
                    ltlAlert.Text = "alert('此扣分类型已存在，无法添加！')";
                    return;
                }
                else
                {
                    TextBox2.Text = string.Empty;
                    txtNote.Text = string.Empty;
                    BindData();
                }
            }
            else
            {
                ltlAlert.Text = "alert('添加失败')";
                return;
            } 
        }
    }
    private int InsertProductTrackingType(string perfd_type, string txt_Note)
    {
        string str = "sp_perf_AddDedAssessmentType";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@perfd_type", perfd_type);
        param[1] = new SqlParameter("@txt_Note", txt_Note);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Label1.Enabled = false;
        TextBox2.Text = string.Empty;
        Label1.Text = string.Empty;
        txtNote.Text = string.Empty;
        btnAdd.Visible = true;
        btnupdate.Visible = false;
        BindData();
    }
    /// <summary>
    /// 根据名称查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text == "")
        {
            BindData();
        }
        else
        {
            string perfd_id = Label1.Text;
            string perfd_type = TextBox2.Text;
            string str = "sp_perf_SelectDedAssessmentByType";
            SqlParameter [] param = new SqlParameter[2];
            param[0] = new SqlParameter("@perfd_id", perfd_id);
            param[1] = new SqlParameter("@perfd_type", perfd_type);
            showType.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
            showType.DataBind();
        }
    }
    /// <summary>
    /// 根据ID进行删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void showType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        TextBox2.Text = string.Empty;
        Label1.Text = string.Empty;
        txtNote.Text = string.Empty;
        int perfd_id = Convert.ToInt32(showType.DataKeys[e.RowIndex][0].ToString());
        string str = "sp_perf_DeleteDedAssessmentByType";
        SqlParameter param = new SqlParameter("@perfd_id", perfd_id);
        SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param);
        BindData();
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void showType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        showType.PageIndex = e.NewPageIndex;
        BindData();
    }
}