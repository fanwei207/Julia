using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmInspection_GuestComplaint_ApplyModule : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (this.Security["19641000"].isValid)
            {
                btnAgree.Visible = true;
            }
            ModuleBindData();
            BindData();
        }
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
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string moduleId = ddlModule.SelectedValue;
        gv.DataSource = SelectApplyModule(moduleId);
        gv.DataBind();
    }

    private object SelectApplyModule(string moduleId)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@moduleId", moduleId);


        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, "sp_selectApplyModuleInfo", param).Tables[0];
    }


    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (ddlModule.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择部门！');";
            ddlModule.Focus();
            return;
        }
        string moduleId = ddlModule.SelectedValue;
        if (InsertApplyModule(moduleId,Session["uID"].ToString(),Session["uName"].ToString()))
        {

            ltlAlert.Text = "alert('申请成功！请等待审批');";
        }
        BindData();
    }

    private bool InsertApplyModule(string moduleId, string createdBy, string createdName)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@moduleId", moduleId);
        param[1] = new SqlParameter("@createdBy", createdBy);
        param[2] = new SqlParameter("@createdName", createdName);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_insertApplyInfo", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = "sp_deleteApplyModuleInfo";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["ID"].ToString());
            sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[1].Value))
            {
                ltlAlert.Text = "alert('删除成功!');";
            }
            else
            {
                ltlAlert.Text = "alert('删除失败，此类别已被使用!');";
            }


            BindData();
        }
        catch
        {
            throw new Exception("DB connection error when deleting...");
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ID = gv.DataKeys[e.Row.RowIndex]["ID"].ToString();
            string str = "SELECT IsAgree from  rmInspection.dbo.ApplyModules where ID = '" + ID + "' ";
            string a = SqlHelper.ExecuteScalar(strconn, CommandType.Text, str).ToString();
            if (a == "1")
            {
                e.Row.Cells[2].BackColor = System.Drawing.Color.GreenYellow;
                e.Row.Cells[0].Enabled = false;
            }     
        }
    }
    protected void btnAgree_Click(object sender, EventArgs e)
    {
        if (gv.Rows.Count > 0)
        {
            string agreeModules = "";
            
            foreach (GridViewRow row in gv.Rows)
            {
                //string ID = gv.DataKeys[row.RowIndex]["ID"].ToString();
                if (((CheckBox)row.FindControl("chkApprove")).Checked)
                {
                    agreeModules +=gv.DataKeys[row.RowIndex]["ID"].ToString()+";";
                }
            }
            if (agreeModules.Length > 0)
            {
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@ID", agreeModules);
                param[1] = new SqlParameter("@uID", Session["uID"].ToString());
                param[2] = new SqlParameter("@uName", Session["uName"].ToString());


                SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_updateApplyModules", param);
                
            }
        }
    }
}