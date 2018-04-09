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
using System.Data.SqlClient;
using System.Configuration;

public partial class Supplier_FI_department : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAllCompany();
            Bind();
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllDeparment();
    }
    private void BindAllDeparment()
    {
        ddlDepartment.Items.Clear();
        DataTable dt = GetAllDeparment(Convert.ToInt32(ddlCompany.SelectedValue.ToString()));
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("----", "0"));
    }
    private DataTable GetAllDeparment(int copid)
    {
        string sql = "SELECT departmentID,name From tcpc" + copid + ".dbo.departments where active = 1 and issalary=1 order by name";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private void BindAllCompany()
    {
        DataTable dt = GetAllCompany();
        ddlCompany.DataSource = dt;
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("--公司--", "0"));
    }
    private DataTable GetAllCompany()
    {
        string sql = "SELECT plantID,description From tcpc0.dbo.plants where isAdmin=0  AND inServiceSys = 1 order by plantID";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }

    protected void gv_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        if (DeleteEffectUser(gv.DataKeys[e.RowIndex].Values["id"].ToString()))
        {
            ltlAlert.Text = "alert('删除成功！');";
            Bind();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
    }
    private bool DeleteEffectUser(string id)
    {
        string str = "sp_fi_deletedepartmentUser";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private void Bind()
    {
        gv.DataSource = GetEffectUser();
        gv.DataBind();
    }
    private DataTable GetEffectUser()
    {
        string sql = "SELECT * FROM dbo.FI_department_config";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择公司！')";
            return;
        }
        else if (ddlDepartment.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择部门！')";
            return;
        }
        else if (txtUser.Text == string.Empty)
        {
            ltlAlert.Text = "alert('工号不能为空！')";
            return;
        }
        if (checkUserNo(ddlCompany.SelectedValue, ddlDepartment.SelectedValue, txtUser.Text))
        {
            saveConfig(ddlCompany.SelectedValue, ddlDepartment.SelectedValue, txtUser.Text, Session["uID"].ToString(), Session["uName"].ToString(), ddlCompany.SelectedItem.Text.Trim(), ddlDepartment.SelectedItem.Text.Trim(), txttype.Text.Trim());
            Bind();
        }
        else
        {
            ltlAlert.Text = "alert('工号不存在或该员工已离职！')";
            return;
        }
    }
    private bool checkUserNo(string company, string department, string userNo)
    {
        string str = "sp_FI_checkUserNo";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@userNo", userNo);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[3].Value);
    }
    private void saveConfig(string company, string department, string userNo, string uID, string uName, string plantname, string departmentname, string type)
    {
        string str = "sp_FI_savedeparmentConfig";
        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@userNo", userNo);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;
        param[4] = new SqlParameter("@createBy", uID);
        param[5] = new SqlParameter("@createName", uName);
        param[6] = new SqlParameter("@plantname", plantname);
        param[7] = new SqlParameter("@departmentname", departmentname);
        param[8] = new SqlParameter("@TYPE", type);
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, str, param);
        if (Convert.ToBoolean(param[3].Value))
        {
            ltlAlert.Text = "alert('保存成功！')";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('保存失败！')";
            return;
        }
    }
}