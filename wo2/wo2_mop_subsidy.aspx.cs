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
using System.Collections.Generic;
using System.Data.Odbc;

public partial class wo2_wo2_mop :BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            //BindMopProc();
            BindMopSubsidy();
        }
    }

    protected void dgMopProc_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }

    protected void dgMopProc_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgMopProc.EditItemIndex = e.Item.ItemIndex;

        BindMopSubsidy();
    }

    protected void dgMopProc_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgMopProc.EditItemIndex = -1;

        BindMopSubsidy();
    }

    protected void dgMopProc_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox txtWo2_mop_subsidy_standard_gv = (TextBox)e.Item.Cells[2].FindControl("txtWo2_mop_subsidy_standard_gv");
        TextBox txtWo2_mop_subsidy_salary_gv = (TextBox)e.Item.Cells[3].FindControl("txtWo2_mop_subsidy_salary_gv");


        if (txtWo2_mop_subsidy_standard_gv.Text.Length == 0)
        {
            ltlAlert.Text = "alert('基数不能为空!');";
            return;
        }
        else
        {
            try
            {
                int intFormat = Convert.ToInt32(txtWo2_mop_subsidy_standard_gv.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('基数格式非法!');";
                return;
            }
        }

        if (txtWo2_mop_subsidy_salary_gv.Text.Length == 0)
        {
            ltlAlert.Text = "alert('补贴不能为空!');";
            return;
        }
        else
        {
            try
            {
                decimal decFormat = Convert.ToDecimal(txtWo2_mop_subsidy_salary_gv.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('补贴格式非法!');";
                return;
            }
        }

        try
        {
            if(UpdateMopSubsidy(Convert.ToInt32(dgMopProc.DataKeys[e.Item.ItemIndex]), Convert.ToInt32(txtWo2_mop_subsidy_standard_gv.Text.Trim()), Convert.ToDecimal(txtWo2_mop_subsidy_salary_gv.Text.Trim()), Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()))
            {
                dgMopProc.EditItemIndex = -1;
                BindMopSubsidy();
            }
            else
            {
                ltlAlert.Text = "alert('更新失败，请联系管理员!');";
                return;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('更新失败，请联系管理员!');";
            return;
        }
    }

    protected void dgMopProc_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (DeleteMopSubsidy(Convert.ToInt32(dgMopProc.DataKeys[e.Item.ItemIndex])))
            {
                BindMopSubsidy();
            }
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败，请联系管理员!');";
            return;
        }
    }

    protected void BindMopSubsidy()
    {
        dgMopProc.DataSource = GetMopSubsidy();
        dgMopProc.DataBind();
    }

    protected DataTable GetMopProc()
    {
        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_selectMopProc").Tables[0];
    }

    protected DataTable GetMopSubsidy()
    {
        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_selectMopSubsidy").Tables[0];
    }

    protected int InsertMopSubsidy(int wo2_mop_subsidy_proc, string wo2_mop_subsidy_procName, int wo2_mop_subsidy_standard, decimal wo2_mop_subsidy_salary, int wo2_mop_subsidy_createdBy, string wo2_mop_subsidy_createdName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@wo2_mop_subsidy_proc", wo2_mop_subsidy_proc);
            param[1] = new SqlParameter("@wo2_mop_subsidy_procName", wo2_mop_subsidy_procName);
            param[2] = new SqlParameter("@wo2_mop_subsidy_standard", wo2_mop_subsidy_standard);
            param[3] = new SqlParameter("@wo2_mop_subsidy_salary", wo2_mop_subsidy_salary);
            param[4] = new SqlParameter("@wo2_mop_subsidy_createdBy", wo2_mop_subsidy_createdBy);
            param[5] = new SqlParameter("@wo2_mop_subsidy_createdName", wo2_mop_subsidy_createdName);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_insertMopSubsidy", param));
        }
        catch
        {
            return -1;
        }
    }

    protected bool UpdateMopSubsidy(int wo2_mop_subsidy_id, int wo2_mop_subsidy_standard, decimal wo2_mop_subsidy_salary, int wo2_mop_subsidy_modifiedBy, string wo2_mop_subsidy_modifiedName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@wo2_mop_subsidy_proc", wo2_mop_subsidy_id);
            param[1] = new SqlParameter("@wo2_mop_subsidy_standard", wo2_mop_subsidy_standard);
            param[2] = new SqlParameter("@wo2_mop_subsidy_salary", wo2_mop_subsidy_salary);
            param[3] = new SqlParameter("@wo2_mop_subsidy_modifiedBy", wo2_mop_subsidy_modifiedBy);
            param[4] = new SqlParameter("@wo2_mop_subsidy_modifiedName", wo2_mop_subsidy_modifiedName);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_updateMopSubsidy", param));
        }
        catch
        {
            return false;
        }
    }

    protected bool DeleteMopSubsidy(int wo2_mop_subsidy_proc)
    {
        try
        {
            SqlParameter param = new SqlParameter("@wo2_mop_subsidy_proc", wo2_mop_subsidy_proc);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_deleteMopSubsidy", param));
        }
        catch
        {
            return false;
        }
    }
    protected void dgMopProc_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgMopProc.CurrentPageIndex = e.NewPageIndex;
        BindMopSubsidy();
    }
}
