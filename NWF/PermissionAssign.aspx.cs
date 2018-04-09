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

public partial class NWF_PermissionAssign : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindFlow();
        }
    }
    private void BindFlow()
    {
        ddlFlow.Items.Clear();  
        ddlFlow.DataSource = GetFlows();
        ddlFlow.DataBind();
        ddlFlow.Items.Insert(0, new ListItem(" -- ", "0"));
    }
    private object GetFlows()
    {
        string str = "sp_ass_getFlows";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@type", 1);
        param[1] = new SqlParameter("@uid", Session["uID"]);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    private void BindSteps()
    {
        ddlStep.Items.Clear();
        ddlStep.DataSource = GetHaveAuthoritySteps();
        ddlStep.DataBind();
        ddlStep.Items.Insert(0, new ListItem(" -- ", "0"));
    }

    private DataTable GetHaveAuthoritySteps()
    {
        string str = "sp_ass_GetHaveAuthSteps";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@uid", Session["uID"].ToString());       
        param[1] = new SqlParameter("@flowid", ddlFlow.SelectedValue);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string flowID = ddlFlow.SelectedValue;
        string stepID = ddlStep.SelectedValue;
        DataGrid1.DataSource = GetPermissionAssign(flowID,stepID);
        DataGrid1.DataBind();
    }

    private DataTable GetPermissionAssign(string flowID,string stepID)
    {
        string str = "sp_ass_GetPermissionAssign";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@flowId", flowID);
        param[1] = new SqlParameter("@setpId", stepID);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnPerson_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/admin/DeptLineSelectUser.aspx?type=7&selectedId=" + lblPersonId.Value + "&selectedName=" + txtPerson.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {       
        if (e.CommandName == "DeleteBtn")
        {
            string id = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            if (DeletePermissionConfig(id))
            {
                ltlAlert.Text = "alert('删除成功！');";
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！');";
            }
        }
        else if (e.CommandName == "CancelBtn")
        {
            ddlStep.SelectedValue = "0";
            BindData();
        }
    }

    private bool DeletePermissionConfig(string id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_ass_deletePermissionAssign", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

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

    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DataGrid1.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string setpId = ddlStep.SelectedValue;
        string personsId = lblPersonId.Value;
        string id = lblId.Text;

        if (txtPerson.Text.Trim() == "")
        {
            lblPersonId.Value = "";
        }

        if (ddlStep.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择步骤名！');";
            return;
        }
        if (btnSave.Text == "新增")
        {
            if (InsertPermissionAssign(setpId, personsId, Session["uID"].ToString()))
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

            if (UpdatePermissionAssign(id, setpId, personsId))
            {

                ltlAlert.Text = "alert('修改成功！');";
                btnSave.Text = "新增";
            }
            else
            {
                ltlAlert.Text = "alert('修改失败！');";
            }
        }

        txtPerson.Text = "";
        BindData();
    }

    private bool UpdatePermissionAssign(string id, string setpId, string personsId)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@setpId", setpId);  
        param[2] = new SqlParameter("@personsId", personsId);
        //param[3] = new SqlParameter("@modifiedBy", modifiedBy);
        //param[4] = new SqlParameter("@modifyName", modifyName);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_ass_updatePermissionAssign", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool InsertPermissionAssign(string setpId, string personsId, string createdBy)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@setpId", setpId);
        param[1] = new SqlParameter("@personsId", personsId);
        param[2] = new SqlParameter("@createdBy", createdBy);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_ass_insertPermissionAssign", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void ddlStep_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSteps();
    }
}