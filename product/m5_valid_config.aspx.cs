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
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class product_m5_valid_config : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Request["pk0"].ToString();
        if (!IsPostBack)
        {
            BindAllCompany();
            Bind();
        }
    }
    private void Bind()
    {
        gv.DataSource = GetEffectUser(Request["pk0"].ToString());
        gv.DataBind();
    }
    private DataTable GetEffectUser(string desc)
    {
        string str = "sp_m5_getValidUser";

        SqlParameter param = new SqlParameter("@desc", desc);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
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
        string sql = "SELECT plantID,description From tcpc0.dbo.plants where isAdmin=0 order by plantID";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text,sql).Tables[0];
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
            if (checkExistsUserNo(txtUser.Text, Request["pk0"].ToString()))
            {
                ltlAlert.Text = "alert('工号已存在！')";
                return;
            }
            else
            {
                saveConfig(ddlCompany.SelectedValue, ddlDepartment.SelectedValue, txtUser.Text, Session["uID"].ToString(), Session["uName"].ToString(), Label1.Text);
                Bind();
            }
        }
        else
        {
            ltlAlert.Text = "alert('工号不存在或该员工已离职！')";
            return; 
        }
    }
    private void saveConfig(string company, string department, string userNo, string uID, string uName,string desc)
    {
        string str = "sp_m5_saveValidConfig";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@company",company);
        param[1] = new SqlParameter("@department",department);
        param[2] = new SqlParameter("@userNo", userNo);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;
        param[4] = new SqlParameter("@createBy", uID);
        param[5] = new SqlParameter("@createName", uName);
        param[6] = new SqlParameter("@desc", desc);

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
    private bool checkUserNo(string company, string department, string userNo)
    {
        string str = "sp_m5_checkUserNo";
        SqlParameter []param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@department", department);
        param[2] = new SqlParameter("@userNo", userNo);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;
        
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[3].Value);
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeleteValidUser(gv.DataKeys[e.RowIndex].Values["m5vu_validid"].ToString(), gv.DataKeys[e.RowIndex].Values["m5vu_userid"].ToString()))
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
    private bool DeleteValidUser(string m5eID, string uid)
    {
        string str = "sp_m5_deleteValidUser";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@m5eID", m5eID);
        param[1] = new SqlParameter("@uid", uid);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(),CommandType.StoredProcedure, str, param));
    }
    
    private bool checkExistsUserNo(string userNo, string desc)
    {
        string str = "sp_m5_checkExistsValidUserNo";

        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@userNo",userNo);
        param[1] = new SqlParameter("@desc", desc);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(),CommandType.StoredProcedure,str,param));
    }
}