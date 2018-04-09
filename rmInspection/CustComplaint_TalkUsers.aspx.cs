using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public partial class rmInspection_CustComplaint_TalkUsers : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidNo.Value = Request["no"].ToString();
            labNo.Text = hidNo.Value.ToString();
            BindGV();
        }
    }
    private void BindGV()
    {
        DataTable dt = SelectTalkUser();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable SelectTalkUser()
    {
        string str = "sp_CustComp_SelectTalkUsers";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@no", hidNo.Value.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", strID);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_CustComp_DeleteTalkUserInfo", sqlParam);

        BindGV();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsExistsUser())
        {
            if (InsertUserInfoToTalk())
            {
                BindGV();
            }
            else
            {
                ltlAlert.Text = "alert('保存失败，请联系管理员！');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('工号不存在！');";
            return;
        }
    }
    private bool InsertUserInfoToTalk()
    {
        string str = "sp_CustComp_InsertUserInfoToTalk";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@plant", ddlPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@userno", txtUserNo.Text);
        param[2] = new SqlParameter("@no", hidNo.Value.ToString());
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool CheckIsExistsUser()
    {
        string str = "sp_CustComp_CheckIsExistsUser";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@plant",ddlPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@userno", txtUserNo.Text);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }

}