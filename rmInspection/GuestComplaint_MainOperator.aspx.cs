using adamFuncs;
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

public partial class rmInspection_GuestComplaint_MainOperator : BasePage
{      
        private adamClass adam = new adamClass();
        string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlModule.Enabled = true;
                ModuleBindData();
                BindData();
            }
        }

        private void BindData()
        {
            string moduleId = ddlModule.SelectedValue;
            string director = txtDirector.Text.Trim();
            string viceDirector = txtViceDirector.Text.Trim();
            DataGrid1.DataSource = GetDeptMainOperators(moduleId, director, viceDirector);
            DataGrid1.DataBind();
        }

        private DataTable GetDeptMainOperators(string moduleId, string director, string viceDirector)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@moduleId", moduleId);
            param[1] = new SqlParameter("@DirectorName", director);
            param[2] = new SqlParameter("@ViceDirectorName", viceDirector);


            return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, "sp_dept_selectMainOperator", param).Tables[0];
        }

        private void ModuleBindData()
        {
            ddlModule.DataSource = GetModules();
            ddlModule.DataBind();
            ddlModule.Items.Insert(0, new ListItem("--", "0"));
        }

        private DataTable GetModules()
        {
            return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, "sp_selectModules").Tables[0];
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ddlModule.Enabled = true;
            if (txtDirector.Text.Trim() == "")
            {
                lblDirectorId.Value = "";
            }
            if (txtViceDirector.Text.Trim() == "")
            {
                lblViceDirectorId.Value = "";
            }
            if (ddlModule.SelectedValue == "0")
            {
                ltlAlert.Text = "alert('请选择部门！');";
                ddlModule.Focus();
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
            string moduleId = ddlModule.SelectedValue;
            string directorId = lblDirectorId.Value;
            string viceDirectorId = lblViceDirectorId.Value;
            if (btnSave.Text == "新增")
            {
                if (CheckDepartmentExists(moduleId))
                {
                    ltlAlert.Text = "alert('此部门信息已维护！');";
                    return;
                }
                if (InsertDeptDirectors(moduleId, directorId, viceDirectorId, Session["uID"].ToString()))
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
                if (UpdateDeptDirectors(id, directorId, viceDirectorId, Session["uID"].ToString()))
                {

                    ltlAlert.Text = "alert('修改成功！');";
                    btnSave.Text = "新增";
                }
                else
                {
                    ltlAlert.Text = "alert('修改失败！');";
                }
            }

            ddlModule.SelectedValue = "0";
            txtDirector.Text = "";
            lblDirectorId.Value = "";
            txtViceDirector.Text = "";
            lblViceDirectorId.Value = "";
            BindData();
        }

        private bool UpdateDeptDirectors(string id, string directorId, string viceDirectorId, string modifiedBy)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@DirectorId", directorId);
            param[2] = new SqlParameter("@ViceDirectorId", viceDirectorId);
            param[3] = new SqlParameter("@modifiedBy", modifiedBy);

            if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_dept_updateDeptDirector", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertDeptDirectors(string moduleId, string directorId, string viceDirectorId, string createdBy)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@moduleId", moduleId);
            param[1] = new SqlParameter("@DirectorId", directorId);
            param[2] = new SqlParameter("@ViceDirectorId", viceDirectorId);
            param[3] = new SqlParameter("@createdBy", createdBy);

            if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_dept_insertDeptDirector", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckDepartmentExists(string moduleId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@moduleId", moduleId);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, "sp_dept_checkMainOperatorExists", param));
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateBtn")
            {
                ddlModule.AutoPostBack = false;
                lblId.Text = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
                ddlModule.SelectedItem.Selected = false;
                ddlModule.Items.FindByText(e.Item.Cells[0].Text).Selected = true;
                ddlModule.Enabled = false;
                txtDirector.Text = e.Item.Cells[1].Text.Replace("<br/>", ";").Replace("&nbsp;", "");
                lblDirectorId.Value = e.Item.Cells[3].Text;

                txtViceDirector.Text = e.Item.Cells[2].Text.Replace("<br/>", ";").Replace("&nbsp;", "");
                lblViceDirectorId.Value = e.Item.Cells[4].Text;
                btnSave.Text = "保存";
            }
            else if (e.CommandName == "DeleteBtn")
            {
                string id = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
                if (DeleteDeptDirectors(id))
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
                ddlModule.SelectedItem.Value = "0";
                txtDirector.Text = "";
                txtViceDirector.Text = "";
                ddlModule.Enabled = true;
                BindData();
            }
        }

        private bool DeleteDeptDirectors(string id)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_dept_deleteDeptDirector", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
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
            ltlAlert.Text = "var w=window.open('/admin/DeptLineSelectUser.aspx?type=1&selectedId=" + lblDirectorId.Value + "&selectedName=" + txtDirector.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
        }
        protected void btnViceDirector_Click(object sender, EventArgs e)
        {
            ltlAlert.Text = "var w=window.open('/admin/DeptLineSelectUser.aspx?type=2&selectedId=" + lblViceDirectorId.Value + "&selectedName=" + txtViceDirector.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
}