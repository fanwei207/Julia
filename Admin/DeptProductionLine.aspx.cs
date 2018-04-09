using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeptProductionLine : BasePage
{
    private admin.DeptProductionLine helper = new admin.DeptProductionLine();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DdlDeptBindData();
            //DdlDirectorBindData();
            //DdlViceDirectorBindData();
            ddlline.Items.Insert(0, new ListItem("--", "0"));
            BindData();
        }
    }

    private void BindData()
    {
        string deptId = ddlDept.SelectedValue;
        string line = ddlline.SelectedValue;
        string lineLeader = txtLineLeader.Text.Trim();
        string processMonitor = txtProcessMonitor.Text.Trim();
        DataGrid1.DataSource = helper.GetDeptProductionLines(deptId, line, lineLeader, processMonitor);
        DataGrid1.DataBind();
    }

    private void DdlDeptBindData()
    {
        ddlDept.DataSource = helper.GetDepartments();
        ddlDept.DataBind();
        ddlDept.Items.Insert(0, new ListItem("--", "0"));
    }

    //private void DdlDirectorBindData()
    //{
    //    string deptId = ddlDept.SelectedValue;
    //    ddlDirector.DataSource = helper.GetDirectors(deptId);
    //    ddlDirector.DataBind();
    //    ddlDirector.Items.Insert(0, new ListItem("--", "0"));
    //}

    //private void DdlViceDirectorBindData()
    //{
    //    string deptId = ddlDept.SelectedValue;
    //    ddlViceDirector.DataSource = helper.GetViceDirectors(deptId);
    //    ddlViceDirector.DataBind();
    //    ddlViceDirector.Items.Insert(0, new ListItem("--", "0"));
    //}

 
    //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DdlDirectorBindData();
    //    DdlViceDirectorBindData();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtLineLeader.Text.Trim() == "")
        {
            lblLineLeaderId.Value = "";
        }
        if (txtProcessMonitor.Text.Trim() == "")
        {
            lblProcessMonitorId.Value = "";
        }
        if (ddlDept.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择部门！');";
            ddlDept.Focus();
            return;
        }
        if (ddlline.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请输入生产线！');";
            ddlline.Focus();
            return;
        }
        //if (lblLineLeaderId.Value == "")
        //{
        //    ltlAlert.Text = "alert('请选择线长！');";
        //    txtLineLeader.Focus();
        //    return;
        //}
        //if (lblProcessMonitorId.Value == "")
        //{
        //    ltlAlert.Text = "alert('请选择过控员！');";
        //    txtProcessMonitor.Focus();
        //    return;
        //}
        string deptId = ddlDept.SelectedValue;

        string line = ddlline.SelectedItem.Text;
         string lineid = ddlline.SelectedValue;
        string lineLeaderId = lblLineLeaderId.Value;
        string processMonitorId = lblProcessMonitorId.Value;
        if (btnSave.Text == "新增")
        {
            if (helper.InsertDeptProductionLine(deptId, line, lineLeaderId, processMonitorId, Session["uID"].ToString(), lineid))
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
            if (helper.UpdateDeptProductionLine(id,deptId, line, lineLeaderId, processMonitorId, Session["uID"].ToString()))
            {
               
                ltlAlert.Text = "alert('修改成功！');";
                btnSave.Text = "新增";
            }
            else
            {
                ltlAlert.Text = "alert('修改失败！');";
            }
        }

        ddlDept.SelectedValue = "0";
        ddlline.SelectedValue = "0";
        txtLineLeader.Text = "";
        txtProcessMonitor.Text = "";
        lblLineLeaderId.Value = "";
        lblProcessMonitorId.Value = "";
        BindData();
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "UpdateBtn")
        {
            
            lblId.Text = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            ddlDept.SelectedItem.Selected = false;
            ddlDept.Items.FindByText(e.Item.Cells[0].Text).Selected = true;

            ddlline.DataSource = helper.GetDeptProductionLines(ddlDept.SelectedValue);
            ddlline.DataBind();
            ddlline.Items.Insert(0, new ListItem("--", "0"));

            ddlline.SelectedValue = e.Item.Cells[10].Text;

            txtLineLeader.Text = e.Item.Cells[4].Text.Replace("<br/>", ";").Replace("&nbsp;", ""); ;
            lblLineLeaderId.Value = e.Item.Cells[6].Text;
            txtProcessMonitor.Text = e.Item.Cells[5].Text.Replace("<br/>", ";").Replace("&nbsp;", ""); ;
            lblProcessMonitorId.Value = e.Item.Cells[7].Text;
            btnSave.Text = "保存";
        }
        else if(e.CommandName=="DeleteBtn")
        {
            string id = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            if (helper.DeleteDeptProductionLine(id))
            {
                BindData();
                ltlAlert.Text = "alert('删除成功！');";
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！');";
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DataGrid1.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnLineLeader_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('DeptLineSelectUser.aspx?type=3&selectedId=" + lblLineLeaderId.Value + "&selectedName=" + txtLineLeader.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btnProcessMonitor_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('DeptLineSelectUser.aspx?type=4&selectedId=" + lblProcessMonitorId.Value + "&selectedName=" + txtProcessMonitor.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlline.DataSource = helper.GetDeptProductionLines(ddlDept.SelectedValue);
        ddlline.DataBind();
        ddlline.Items.Insert(0, new ListItem("--", "0"));
    }
}