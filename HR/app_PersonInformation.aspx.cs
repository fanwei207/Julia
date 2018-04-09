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
using System.IO;
using QADSID;
public partial class HR_app_PersonInformation : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        DataTable dt = GetLadingList();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable GetLadingList()
    {
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_getpersoninformation").Tables[0];
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindData();

    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //String shipnum = ((TextBox)gv.Rows[e.RowIndex].Cells[1].FindControl("txtShipNum")).Text.ToString().Trim();
        //String receipt = ((TextBox)gv.Rows[e.RowIndex].Cells[2].FindControl("txtReceipt")).Text.ToString().Trim();
        String phone = ((TextBox)gv.Rows[e.RowIndex].Cells[3].FindControl("txtPhone")).Text.ToString().Trim();

        if (UpdatePersonInfo(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString()), phone))
        {
            gv.EditIndex = -1;
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeletePersonInfo(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString())))
        {
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
    }

    private bool UpdatePersonInfo(int id, string phone)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@phone", phone);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_UpdatePersonInfo", param));
    }
    private bool DeletePersonInfo(int id)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", id);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_DeletePersonInfo", param));
    }
    protected void btnReach_Click(object sender, EventArgs e)
    {
        if (txtUserNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写工号！');";
            return;
        }
        if (txtPhone.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写联系电话！');";
            return;
        }
        if (!checkuserno(txtUserNo.Text))
        {
            ltlAlert.Text = "alert('工号不存在或不是人事部人员工号！');";
            return;
        }
        else
        {
            if (!existsuserno(txtUserNo.Text))
            {
                if (insertpersoninfo(txtUserNo.Text, txtPhone.Text))
                {
                    ltlAlert.Text = "alert('添加成功！');";
                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('添加失败！');";
                    return;
                }
            }
            else 
            {
                ltlAlert.Text = "alert('此工号已添加，请勿重复添加！');";
                return; 
            }
        }        
    }
    private bool checkuserno(string userno)
    {
        SqlParameter[] pram = new SqlParameter[1];
        pram[0] = new SqlParameter("@userno", userno);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_checkIsExistsUserno", pram));
    }
    private bool existsuserno(string userno)
    {
        SqlParameter[] pram = new SqlParameter[1];
        pram[0] = new SqlParameter("@userno", userno);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_IsExistsUserno", pram));
    }
    private bool insertpersoninfo(string userno,string phone)
    {
        SqlParameter[] pram = new SqlParameter[2];
        pram[0] = new SqlParameter("@userno", userno);
        pram[1] = new SqlParameter("@phone",phone);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_insertpersoninfo", pram));
    }
}