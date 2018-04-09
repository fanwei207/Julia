using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeptDirector : BasePage
{
    private admin.DeptDirector helper = new admin.DeptDirector();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DdlDeptBindData();
            BindData();
        }
    }

    private void BindData()
    {
        string deptId = ddlDept.SelectedValue;
        string director = txtDirector.Text.Trim();
        string viceDirector = txtViceDirector.Text.Trim();
        DataGrid1.DataSource = helper.GetDeptDirectors(deptId, director, viceDirector);
        DataGrid1.DataBind();
    }

    private void DdlDeptBindData()
    {
        ddlDept.DataSource = helper.GetDepartments();
        ddlDept.DataBind();
        ddlDept.Items.Insert(0, new ListItem("--", "0"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDirector.Text.Trim() == "")
        {
            lblDirectorId.Value = "";
        }
        if (txtViceDirector.Text.Trim() == "")
        {
            lblViceDirectorId.Value = "";
        }
        if (ddlDept.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择部门！');";
            ddlDept.Focus();
            return;
        }
        //if (lblDirectorId.Value == "")
        //{
        //    ltlAlert.Text = "alert('请选择主任！');";
        //    txtDirector.Focus();
        //    return;
        //}
        //if (lblViceDirectorId.Value == "")
        //{
        //    ltlAlert.Text = "alert('请选择副主任！');";
        //    txtViceDirector.Focus();
        //    return;
        //}
        string deptId = ddlDept.SelectedValue;
        string directorId = lblDirectorId.Value;
        string viceDirectorId = lblViceDirectorId.Value;
        if (btnSave.Text == "新增")
        {
            if (helper.CheckDepartmentExists(deptId))
            {
                ltlAlert.Text = "alert('此部门信息已维护！');";
                return;
            }
            if (helper.InsertDeptDirectors(deptId, directorId, viceDirectorId, Session["uID"].ToString()))
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
            if (helper.UpdateDeptDirectors(id, directorId,viceDirectorId, Session["uID"].ToString()))
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
        txtDirector.Text = "";
        lblDirectorId.Value = "";
        txtViceDirector.Text = "";
        lblViceDirectorId.Value = "";
        BindData();
    }

    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "UpdateBtn")
        {
            lblId.Text = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            ddlDept.SelectedItem.Selected = false;
            ddlDept.Items.FindByText(e.Item.Cells[0].Text).Selected = true;
            txtDirector.Text = e.Item.Cells[1].Text.Replace("<br/>", ";").Replace("&nbsp;","");
            lblDirectorId.Value = e.Item.Cells[3].Text;

            txtViceDirector.Text = e.Item.Cells[2].Text.Replace("<br/>", ";").Replace("&nbsp;", "");
            lblViceDirectorId.Value = e.Item.Cells[4].Text;
            btnSave.Text = "保存";
        }
        else if (e.CommandName == "DeleteBtn")
        {
            string id = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
            if (helper.DeleteDeptDirectors(id))
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
    protected void btnDirector_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('DeptLineSelectUser.aspx?type=1&selectedId="+lblDirectorId.Value+"&selectedName="+txtDirector.Text.Trim()+"','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btnViceDirector_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('DeptLineSelectUser.aspx?type=2&selectedId=" + lblViceDirectorId.Value + "&selectedName=" + txtViceDirector.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
}