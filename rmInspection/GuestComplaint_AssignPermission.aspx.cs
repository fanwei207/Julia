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

public partial class rmInspection_GuestComplaint_AssignPermission : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            BindModules();
            BindData();
            checkIsMaintenance();
        }
    }

    private void checkIsMaintenance()
    {
        for (int i = 1; i <= 7; i++)
        {
            string moduleID = ddlModule.SelectedValue;
            if (CheckModuleExists(moduleID, Session["uID"].ToString()))
            {
                btnHandlePerson.Enabled = false;
            }
            else
            {
                btnHandlePerson.Enabled = true;
            }
        }
        
    }
    //显示该用户申请成功的模块
    private void BindModules()
    {
        ddlModule.Items.Clear();       
        ddlModule.DataSource = GetModule();
        ddlModule.DataBind();
    }

    private DataTable GetModule()
    {
        string str = "sp_getModuleByUid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@uid", Session["uID"].ToString());
       // param[1] = new SqlParameter("@moduleId", moduleId);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnHandlePerson_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/admin/DeptLineSelectUser.aspx?type=5&selectedId=" + lblHandlePersonId.Value + "&selectedName=" + txtHandlePerson.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string moduleID = ddlModule.SelectedValue;
        string handlePersons = txtHandlePerson.Text.Trim();
        DataGrid1.DataSource = GetHandlePersons(moduleID, handlePersons);
        DataGrid1.DataBind();
    }

    private DataTable GetHandlePersons(string moduleID, string handlePersons)
    {
        string str = "sp_getHandlePersons";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@handlePersons", handlePersons);
        param[1] = new SqlParameter("@moduleID", moduleID);
        param[2] = new SqlParameter("@uid", Session["uID"].ToString());

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ModuleId = ddlModule.SelectedValue;
        string handlepersonId = lblHandlePersonId.Value;

        if (txtHandlePerson.Text.Trim() == "")
        {
            lblHandlePersonId.Value = "";
        }
        if (ddlModule.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择部门！');";
            ddlModule.Focus();
            return;
        }

        
        if (btnSave.Text == "新增")
        {
            if (txtHandlePerson.Text.Trim() == "")
            {
                ltlAlert.Text = "alert('请添加处理人！');";
                return;
            }
            
            if (InsertHandlePersons(ModuleId, handlepersonId, Session["uID"].ToString(), Session["uName"].ToString()))
            {

                ltlAlert.Text = "alert('添加成功！');";
            }
            else
            {
                ltlAlert.Text = "alert('添加失败！');";
            }
        }
        else if (btnSave.Text == "保存")
        {
            string id = lblId.Text;
            string moduledNames = CheckModuleMaintenanceByOthers(ModuleId, Session["uID"].ToString());
            if (UpdateDeptDirectors(id, handlepersonId, Session["uID"].ToString(), Session["uName"].ToString()))
            {

                ltlAlert.Text = "alert('修改成功！');";
                btnSave.Text = "新增";
            }
            else
            {
                ltlAlert.Text = "alert('修改失败！');";
            }
        }
        txtHandlePerson.Text = "";
        BindData();
    }


    private string CheckModuleMaintenanceByOthers(string ModuleId, string uid)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@moduleId", ModuleId);
        param[1] = new SqlParameter("@uid", uid);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, "sp_checkModuleMaintenanceByOthers", param)).ToString();
    }

    private bool CheckModuleExists(string ModuleId, string uid)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@moduleId", ModuleId);
        param[1] = new SqlParameter("@uid", uid);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, "sp_checkModuleExists", param));
    }

    private bool UpdateDeptDirectors(string id, string handlepersonId, string modifiedBy, string modifyName)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@handlepersonId", handlepersonId);
        param[2] = new SqlParameter("@modifiedBy", modifiedBy);
        param[3] = new SqlParameter("@modifyName", modifyName);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_updateHandlePersons", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool InsertHandlePersons(string moduleId, string handlepersonId, string createdBy, string createdName)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@moduleId", moduleId);
        param[1] = new SqlParameter("@handlepersonId", handlepersonId);
        param[2] = new SqlParameter("@createdBy", createdBy);
        param[3] = new SqlParameter("@createdName", createdName);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_insertHandlePersons", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DataGrid1.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "UpdateBtn")
        {
            lblId.Text = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            ddlModule.SelectedItem.Selected = false;
            ddlModule.Items.FindByText(e.Item.Cells[0].Text).Selected = true;
            txtHandlePerson.Text = e.Item.Cells[1].Text.Replace("<br/>", ";").Replace("&nbsp;", "");
            lblHandlePersonId.Value = e.Item.Cells[3].Text;
            btnHandlePerson.Enabled = true;
            btnSave.Text = "保存";
        }
        else if (e.CommandName == "DeleteBtn")
        {
            string id = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            if (DeleteHandlePersons(id))
            {
                BindData();
                ltlAlert.Text = "alert('删除成功！');";
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！');";
            }
        }
        else if (e.CommandName == "CancelBtn")
        {
            txtHandlePerson.Text = "";
            BindData();
        }
    }

    private bool DeleteHandlePersons(string id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_deleteHandlePersons", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkIsMaintenance();
        BindData();
    }
}