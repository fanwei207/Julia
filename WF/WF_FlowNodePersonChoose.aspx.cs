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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;

public partial class WF_FlowNodePersonChoose : BasePage
{
  
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //工作流步骤添加操作人的权限

            loadPlant();
            loadDepartment();
            loadRoleOrUser();
        }
    }

    protected void loadPlant()
    {
        ddlPlant.DataSource = wf.GetPlant();
        ddlPlant.DataBind();
        ddlPlant.SelectedValue = Convert.ToString(Session["PlantCode"].ToString());
    }

    protected void loadDepartment()
    {
        if (ddlDept.Items.Count > 0) ddlDept.Items.Clear();
        ddlDept.DataSource = wf.GetDept(Convert.ToInt32(ddlPlant.SelectedValue));
        ddlDept.DataBind();
    }

    protected void loadRoleOrUser()
    {
        if (radRoleOrUser.Items.Count > 0) radRoleOrUser.Items.Clear();
        int plant = ddlPlant.Visible == true ? Convert.ToInt32(ddlPlant.SelectedValue) : Convert.ToInt32(Session["PlantCode"].ToString());
        int dept = Convert.ToInt32(ddlDept.SelectedValue);
        string RoleOrUserID = string.Empty;
        string RoleOrUserInfo = string.Empty;

        if (rbRole.Checked)
        {
            DataTable dt = wf.GetRole(plant);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ListItem item = new ListItem(dt.Rows[i].ItemArray[1].ToString().Trim(), dt.Rows[i].ItemArray[0].ToString().Trim());
                    radRoleOrUser.Items.Add(item);
                    if (txtID.Text.Trim().IndexOf(";" + radRoleOrUser.Items[i].Value + ";") > -1)
                    {
                        radRoleOrUser.Items[i].Selected = true;
                    }
                }
            }
        }
        else
        {
            if (ddlDept.SelectedIndex > 0)
            {
                DataTable dt = wf.GetUser(plant, dept);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ListItem item = new ListItem(dt.Rows[i].ItemArray[1].ToString().Trim(), dt.Rows[i].ItemArray[0].ToString().Trim());
                        radRoleOrUser.Items.Add(item);
                        if (txtID.Text.Trim().IndexOf(";" + radRoleOrUser.Items[i].Value + ";") > -1)
                        {
                            radRoleOrUser.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDepartment();
        loadRoleOrUser();
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadRoleOrUser();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (radRoleOrUser.Items.Count < 1) return;

        int nodeID = Convert.ToInt32(Request.QueryString["nid"].ToString());
        DataTable dt = wf.CheckNodePersonUnfinished(nodeID, Convert.ToInt32(Session["plantCode"].ToString()));
        if (dt.Rows.Count > 0)
        {
            ltlAlert.Text = "alert('存在未审批工作流，需待所有工作流审批完成才可修改!');";
            return;
        }

        int type = rbRole.Checked == true ? 0 : 1;
        string objectID = txtID.Text.Trim();
        int createdBy = Convert.ToInt32(Session["uID"].ToString());
        int plantid = Convert.ToInt32(Session["plantCode"].ToString());

        if (objectID.Replace(";", "").Length == 0)
        {
            wf.DeleteNodePerson(Convert.ToInt32(Request.QueryString["nid"].ToString()));
            ltlAlert.Text = " window.close(); window.opener.location='/WF/WF_FlowNode.aspx?id=" + Request.QueryString["fid"].ToString() + "&rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            int nRet = wf.AddNodePerson(nodeID, type, objectID, createdBy, plantid);
            if (nRet < 0)
            {
                ltlAlert.Text = "alert('保存失败，请重试!');";
                return;
            }
            else
            {
                ltlAlert.Text = " window.close(); window.opener.location='/WF/WF_FlowNode.aspx?id=" + Request.QueryString["fid"].ToString() + "&rm=" + DateTime.Now.ToString() + "';";
            }
        }
    }

    protected void rbRole_CheckedChanged(object sender, EventArgs e)
    {
        ddlDept.Enabled = false;
        ddlDept.SelectedIndex = -1;
        loadRoleOrUser();
        txtID.Text = string.Empty;
        txtInfo.Text = string.Empty;
    }

    protected void rbUser_CheckedChanged(object sender, EventArgs e)
    {
        ddlDept.Enabled = true;
        loadRoleOrUser();
        txtID.Text = string.Empty;
        txtInfo.Text = string.Empty;
    }

    protected void radRoleOrUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbUser.Checked)
        {
            if (radRoleOrUser.Items.Count == 0) return;

            string RoleOrUserID = txtID.Text.Trim().Replace(";;", ";");
            string RoleOrUserInfo = txtInfo.Text.Trim().Replace(";;", ";");

            foreach (ListItem item in radRoleOrUser.Items)
            {
                if (item.Selected)
                {
                    RoleOrUserID = RoleOrUserID.Replace(item.Value + ";", "");
                    RoleOrUserID += item.Value + ";";

                    RoleOrUserInfo = RoleOrUserInfo.Replace(item.Text.Substring(0, item.Text.Trim().IndexOf("~")) + ";", "");
                    RoleOrUserInfo += item.Text.Substring(0, item.Text.Trim().IndexOf("~")) + ";";
                }
                else
                {
                    RoleOrUserID = RoleOrUserID.Replace(item.Value + ";", "");
                    RoleOrUserInfo = RoleOrUserInfo.Replace(item.Text.Substring(0, item.Text.Trim().IndexOf("~")) + ";", "");
                }
            }

            txtID.Text = RoleOrUserID.Replace(";;", ";");
            txtInfo.Text = RoleOrUserInfo.Replace(";;", ";");
        }
        else
        {
            if (radRoleOrUser.Items.Count == 0) return;

            string RoleOrUserID = txtID.Text.Trim().Replace(";;", ";");
            string RoleOrUserInfo = txtInfo.Text.Trim().Replace(";;", ";");

            foreach (ListItem item in radRoleOrUser.Items)
            {
                if (item.Selected)
                {
                    RoleOrUserID = RoleOrUserID.Replace(item.Value + ";", "");
                    RoleOrUserID += item.Value + ";";

                    RoleOrUserInfo = RoleOrUserInfo.Replace(item.Text + ";", "");
                    RoleOrUserInfo += item.Text + ";";
                }
                else
                {
                    RoleOrUserID = RoleOrUserID.Replace(item.Value + ";", "");
                    RoleOrUserInfo = RoleOrUserInfo.Replace(item.Text + ";", "");
                }
            }

            txtID.Text = RoleOrUserID.Replace(";;", ";");
            txtInfo.Text = RoleOrUserInfo.Replace(";;", ";");
        }
    }
}
