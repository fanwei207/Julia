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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;

public partial class HR_app_MattchAppList : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gv.Columns[0].Visible = false;
            BindData();
            BindAllCompany();
        }
    }
    /// <summary>
    /// 绑定所有公司
    /// </summary>
    private void BindAllCompany()
    {
        DataTable dt = GetAllCompany();
        ddlCompany.DataSource = dt;
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("--公司--", "0"));
    }
    /// <summary>
    /// 获取所有公司
    /// </summary>
    private DataTable GetAllCompany()
    {
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAllCop").Tables[0];
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllDeparment();
    }
    /// <summary>
    /// 绑定所有部门
    /// </summary>
    private void BindAllDeparment()
    {
        ddlDepartment.Items.Clear();
        DataTable dt = GetAllDeparment(Convert.ToInt32(ddlCompany.SelectedValue.ToString()));
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("----", "0"));
    }
    /// <summary>
    /// 获取部门
    /// </summary>
    private DataTable GetAllDeparment(int copid)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@copid", copid);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAllDeptByCopID", param).Tables[0];
    }
    private void BindData()
    {
        DataTable dt = GetInformationList(txtAppDate.Text, ddlCompany.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable GetInformationList(string appdate, string companyid, string departmentid)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@appdate", appdate);
        param[1] = new SqlParameter("@companyid", companyid);
        param[2] = new SqlParameter("@departmentid", departmentid);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_checkIsMatchApp", param).Tables[0];
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String appProc = ((TextBox)gv.Rows[e.RowIndex].Cells[4].FindControl("txtBirthDay")).Text.ToString().Trim();

        if (updateUserAppProcess(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString()), appProc))
        {
            gv.EditIndex = -1;
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
    }
    private bool updateUserAppProcess(int id, string appProc)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@appProc", appProc);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_updateUserAppProcess", param));
    }
    protected void btnReach_Click(object sender, EventArgs e)
    {
        BindData();
    }
}