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

public partial class wo2_MgMiProc : BasePage
{
    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            databind();
        }
    }

    protected void databind()
    {
        try
        {
            string strSql = "sp_wo2_t_selectInOutProc";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@type", dropType.SelectedValue);
            param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);

            DataTable table = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];

            gv.DataSource = table;
            gv.DataBind();
        }
        catch
        {
            this.Alert("获取数据失败！请联系管理员！");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (dropDomain.SelectedIndex == 0)
        {
            this.Alert("请选择一个公司！");
            return;
        }

        if (string.IsNullOrEmpty(txtProcCode.Text))
        {
            this.Alert("工序代码 不能为空！");
            return;
        }

        if (string.IsNullOrEmpty(txtProc.Text))
        {
            this.Alert("工序 不能为空！");
            return;
        }

        try
        {
            string strSql = "Insert Into wo2_InOutProc(p_type, p_domain, p_procCode, p_proc, p_orig_proc) Values(N'" + dropType.SelectedValue + "', '" + dropDomain.SelectedValue + "', N'" + txtProcCode.Text + "', N'" + txtProc.Text + "', N'" + txtOrigProc.Text + "')";

            SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.Text, strSql);
        }
        catch
        {
            this.Alert("新增失败！注意，工序不能重复！");
        }

        databind();
    }

    protected void gvSort_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gv.DataKeys[e.RowIndex].Value.ToString();

        try
        {
            string strSql = "Delete From wo2_InOutProc Where p_id = '" + id + "'";

            SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.Text, strSql);
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！");
        }

        databind();
    }

    protected void gvSort_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        databind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = gv.DataKeys[e.RowIndex].Value.ToString();
        string procCode = ((TextBox)gv.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        string proc = ((TextBox)gv.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
        string orig_proc = ((TextBox)gv.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

        if (string.IsNullOrEmpty(proc))
        {
            this.Alert("工序不能为空！");
        }
        else
        {
            try
            {
                string strSql = "Update wo2_InOutProc Set p_orig_proc = N'" + orig_proc + "', p_procCode = N'" + procCode + "', p_proc = N'" + proc + "' Where p_id = '" + id + "'";

                SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.Text, strSql);
            }
            catch
            {
                this.Alert("更新失败！注意，工序不能重复！");
            }
        }
        gv.EditIndex = -1;
        databind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (dropDomain.SelectedIndex == 0)
        {
            this.Alert("请选择一个公司！");
            return;
        }

        databind();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        databind();
    }
}
