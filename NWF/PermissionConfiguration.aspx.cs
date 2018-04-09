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

public partial class NWF_PermissionConfiguration : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindDepartments();
            BindFlow();
            BindSteps();
        }
    }

    private void BindSteps()
    {
        ddlStep.Items.Clear();
        ddlStep.DataSource = GetSteps();
        ddlStep.DataBind();
        ddlStep.Items.Insert(0, new ListItem(" -- ", "0"));
    }

    private void BindFlow()
    {
        ddlFlow.Items.Clear();
        ddlFlow.DataSource = GetFlows();
        ddlFlow.DataBind();
        ddlFlow.Items.Insert(0, new ListItem(" -- ", "0"));
    }

    private DataTable GetFlows()
    {
        string str = "sp_ass_getFlows";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@type", "0");
        //param[1] = new SqlParameter("@uid", 0);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str,param).Tables[0];
    }
    private DataTable GetSteps()
    {
        string str = "sp_ass_getSteps";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Flow_id",ddlFlow.SelectedValue);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str,param).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindDepartments()
    {
        ddlDepartment.Items.Clear();
        ddlDepartment.DataSource = GetDepartment();
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem(" -- ", "0"));
    }

    private object GetDepartment()
    {
        string str = "sp_ass_getDepartments";
        SqlParameter[] param = new SqlParameter[1];

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    private void BindData()
    {
        string flowName = ddlFlow.SelectedValue;
        string setpName = ddlStep.SelectedValue;
        string departmentID = ddlDepartment.SelectedValue;
        string Persons = txtPerson.Text.Trim();
        DataGrid1.DataSource = GetPermissionConfig(flowName, setpName, departmentID, Persons);
        DataGrid1.DataBind();
    } 

    private DataTable GetPermissionConfig(string flowName, string setpName, string departmentID, string Persons)
    {
        string str = "sp_ass_GetPermissionConfig";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@flowId", flowName);
        param[1] = new SqlParameter("@setpId", setpName);
        param[2] = new SqlParameter("@departmentID", departmentID);
        param[3] = new SqlParameter("@persons", Persons);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string departmentId = ddlDepartment.SelectedValue;
        string flowId = ddlFlow.SelectedValue;
        string setpId = ddlStep.SelectedValue;
        string personsId = lblPersonId.Value;
        string id = lblId.Text;

        if (txtPerson.Text.Trim() == "")
        {
            lblPersonId.Value = "";
        }
        if (ddlDepartment.SelectedValue == "0" && txtPerson.Text.Trim() == "")
        {
            ltlAlert.Text = "alert('请选择部门！');";
            ddlDepartment.Focus();
            return;
        }
        if (ddlFlow.SelectedValue =="0")
        {
            ltlAlert.Text = "alert('请选择流号名！');";
            return;
        }

        if (ddlStep.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请填写步骤名！');";
            return;
        }
        if (btnSave.Text == "新增")
        {
            if (InsertPermissionConfig(flowId, setpId, departmentId, personsId, Session["uID"].ToString(), Session["uName"].ToString()))
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

            if (UpdatePermissionConfig(id, flowId, setpId, departmentId, personsId, Session["uID"].ToString(), Session["uName"].ToString()))
            {

                ltlAlert.Text = "alert('修改成功！');";
                btnSave.Text = "新增";
            }
            else
            {
                ltlAlert.Text = "alert('修改失败！');";
            }
        }
        ddlFlow.SelectedValue = "0";
        ddlStep.SelectedValue = "0";
        ddlDepartment.SelectedValue = "0";
        txtPerson.Text = "";
        BindData();
    }

    private bool UpdatePermissionConfig(string id, string flowId, string setpId, string departmentId, string personsId, string modifiedBy, string modifyName)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@flowId", flowId);
        param[2] = new SqlParameter("@setpId", setpId);
        param[3] = new SqlParameter("@departmentId", departmentId);
        param[4] = new SqlParameter("@personsId", personsId);
        param[5] = new SqlParameter("@modifiedBy", modifiedBy);
        param[6] = new SqlParameter("@modifyName", modifyName);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_ass_updatePermissionConfig", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool InsertPermissionConfig(string flowId, string setpId, string departmentId, string personsId, string createdBy, string createdName)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@flowId", flowId);
        param[1] = new SqlParameter("@setpId", setpId);
        param[2] = new SqlParameter("@departmentId", departmentId);
        param[3] = new SqlParameter("@personsId", personsId);
        param[4] = new SqlParameter("@createdBy", createdBy);
        param[5] = new SqlParameter("@createdName", createdName);
        param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[6].Direction = ParameterDirection.Output;


            if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_ass_insertPermissionConfig", param) > 0)
            {
                if (Convert.ToBoolean(param[6].Value))
                {
                    ltlAlert.Text = "alert('此类别已存在')";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }          
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "UpdateBtn")
        {
            lblId.Text = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            ddlDepartment.SelectedItem.Selected = false;
            ddlDepartment.Items.FindByText(e.Item.Cells[2].Text).Selected = true;
            ddlFlow.SelectedItem.Selected = false;
            ddlFlow.Items.FindByText(e.Item.Cells[0].Text).Selected = true;
            ddlStep.SelectedItem.Selected = false;
            ddlStep.Items.FindByText(e.Item.Cells[1].Text).Selected = true;
            txtPerson.Text = e.Item.Cells[3].Text.Replace("<br/>", ";").Replace("&nbsp;", "");
            lblPersonId.Value = getResultLoginNames(e.Item.Cells[3].Text);
            btnPerson.Enabled = true;
            btnSave.Text = "保存";
        }
        else if (e.CommandName == "DeleteBtn")
        {
            string id = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            if (DeletePermissionConfig(id))
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
            ddlFlow.SelectedValue = "0";
            ddlStep.SelectedValue = "0";
            ddlDepartment.SelectedValue = "0";
            txtPerson.Text = "";
            BindData();
        }
    }

    private bool DeletePermissionConfig(string id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_ass_deletePermissionConfig", param) > 0)
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
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnPerson_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/admin/DeptLineSelectUser.aspx?type=7&selectedId=" + lblPersonId.Value + "&selectedName=" + txtPerson.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }

    //返回loginName
    private string getResultLoginNames(string preResultNames)
    {
        //删除字符串中中文后得到的字符串
        string AfterDelChinese = getDelChineseStr(preResultNames);
        //
        string loginNames = "";
        string names = AfterDelChinese;
        names = names.Replace('-', ' ');
        string[] sArray2 = names.Split(';');
        foreach (string i in sArray2)
        {
            loginNames += i.ToString().Trim() + ';';
        }
        if (loginNames.Length > 0)
            loginNames = loginNames.Substring(0, loginNames.Length - 1);

        return loginNames;
    }
    //删除LoginName中的中文
    private string getDelChineseStr(string preResultNames)
    {
        string retValue = preResultNames;
        if (System.Text.RegularExpressions.Regex.IsMatch(preResultNames, @"[\u4e00-\u9fa5]"))
        {
            retValue = string.Empty;
            var strsStrings = preResultNames.ToCharArray();
            for (int index = 0; index < strsStrings.Length; index++)
            {
                if (strsStrings[index] >= 0x4e00 && strsStrings[index] <= 0x9fa5)
                {
                    continue;
                }
                retValue += strsStrings[index];
            }
        }
        return retValue;
    }
    protected void ddlFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSteps();
    }
    protected void ddlStep_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}