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
using RD_WorkFlow;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using adamFuncs;
using System.Runtime.Serialization;


public partial class product_m5_chosePerson : System.Web.UI.Page
{

    //string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    RD_Steps step = new RD_Steps();
    RDW apply = new RDW();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            //int mId = int.Parse(Request.QueryString["mid"].ToString());
            BindCompany();

            ddl_Company.SelectedIndex = -1;
            try
            {
                ddl_Company.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
            }
            catch
            {
                ddl_Company.SelectedIndex = -1;
            }

            int plant = int.Parse(ddl_Company.SelectedValue);

            //DataSet dst = getProjQadApprover(mId);

            //if (dst.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
            //    {

            //        txtUsers.Text += ";" + dst.Tables[0].Rows[i]["rdw_approverId"].ToString();
            //        txtUserNames.Text += ";" + dst.Tables[0].Rows[i]["rdw_approverName"].ToString();
            //    }
            //}

            txtUsers.Text += ";";
            //txtUserName.Text += ";";
            BindDepartment();
        }
    }

    private void BindCompany()
    {
        ddl_Company.DataSource = admin_AccessApply.getCompanyInfo();
        ddl_Company.DataBind();
    }
    private void BindDepartment()
    {
        ddl_department.DataSource = admin_AccessApply.getDepartmentInfo(Convert.ToInt32(ddl_Company.SelectedValue.ToString()));
        ddl_department.DataBind();
    }

    protected void loadUser()
    {

        if (chkUser.Items.Count > 0)
            chkUser.Items.Clear();

        int plantid = Convert.ToInt32(ddl_Company.SelectedValue);
        int departmentId = Convert.ToInt32(ddl_department.SelectedValue);
        string userName = txt_user.Text.Trim();
        string strUser = string.Empty;
        string strUserID = string.Empty;
        //id = int.Parse(Request.QueryString["mid"].ToString()); 
        SqlDataReader dst = selectAllUsers(plantid, departmentId, userName);
        while (dst.Read())
        {
            ListItem item = new ListItem(dst["userName"].ToString() + "--" + dst["email"].ToString(), dst["userID"].ToString());
            chkUser.Items.Add(item);

            //USERLIST.Clear();
            //USERLIST.Add(Convert.ToInt32(dst["userID"]), new personInfo(Convert.ToInt32(dst["userID"]),
            //    dst["userNo"].ToString(), dst["userName"].ToString(), dst["departmentname"].ToString()
            //    , dst["roleName"].ToString(), dst["plantCode"].ToString(), dst["email"].ToString()));
        
        }
        dst.Dispose();
        dst.Close();
     
    }

 
    public SqlDataReader selectAllUsers(int plantid, int departmentId, string userName)
    {
       
       
        string strSql = "sp_m5_selectAllUsers";

        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@plantid", plantid);
        sqlParam[1] = new SqlParameter("@departmentId", departmentId);
        sqlParam[2] = new SqlParameter("@userName", userName);
        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
          
       
    }
    


    protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkUser.Items.Count == 0) return;

        //txtChoseUserID.Text = string.Empty;
        //txtChoseuserNo.Text = string.Empty;
        //txtChoseUserName.Text = string.Empty;
        //txtChoseuserDept.Text = string.Empty;
        //txtChoseuserDomain.Text = string.Empty;
        //txtChoseUserEmail.Text = string.Empty;
        //txtChoseuserRole.Text = string.Empty;


        //txtChoseUserID.Text = USERLIST[Convert.ToInt32(chkUser.SelectedValue)].UserID.ToString();
        //txtChoseuserNo.Text = USERLIST[Convert.ToInt32(chkUser.SelectedValue)].UserNo.ToString();
        //txtChoseUserName.Text = USERLIST[Convert.ToInt32(chkUser.SelectedValue)].UserName.ToString();
        //txtChoseuserDept.Text = USERLIST[Convert.ToInt32(chkUser.SelectedValue)].UserDept.ToString();
        //txtChoseuserDomain.Text = USERLIST[Convert.ToInt32(chkUser.SelectedValue)].UserDomain.ToString();
        //txtChoseUserEmail.Text = USERLIST[Convert.ToInt32(chkUser.SelectedValue)].UserEmail.ToString();
        //txtChoseuserRole.Text = USERLIST[Convert.ToInt32(chkUser.SelectedValue)].UserRole.ToString();
    }


    protected void ddl_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartment();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_user.Text = string.Empty;
        loadUser();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        loadUser();
    }




    protected void btnClose_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int plantid = Convert.ToInt32(ddl_Company.SelectedValue);
        SqlDataReader sdr = this.selectUserInfoByID(chkUser.SelectedValue.ToString(), plantid);
        txtChoseUserID.Text = string.Empty;
        txtChoseuserNo.Text = string.Empty;
        txtChoseUserName.Text = string.Empty;
        txtChoseuserDept.Text = string.Empty;
        txtChoseuserDomain.Text = string.Empty;
        txtChoseUserEmail.Text = string.Empty;
        txtChoseuserRole.Text = string.Empty;


        while (sdr.Read())
        {

            txtChoseUserID.Text = sdr["userID"].ToString();
            txtChoseuserNo.Text = sdr["userNo"].ToString();
            txtChoseUserName.Text = sdr["userName"].ToString();
            txtChoseuserDept.Text = sdr["departmentname"].ToString();
            txtChoseuserDomain.Text = sdr["plantCode"].ToString();
            txtChoseUserEmail.Text = sdr["email"].ToString();
            txtChoseuserRole.Text = sdr["roleName"].ToString();
        }
        sdr.Dispose();
        sdr.Close();
        //USERLIST.Add(Convert.ToInt32(dst["userID"]), new personInfo(Convert.ToInt32(dst["userID"]),
        //    dst["userNo"].ToString(), dst["userName"].ToString(), dst["departmentname"].ToString()
        //    , dst["roleName"].ToString(), dst["plantCode"].ToString(), dst["email"].ToString()));

        ltlAlert.Text = "post();";
    }

    private System.Data.SqlClient.SqlDataReader selectUserInfoByID(string userID,int plantid)
    {
        string strSql = "sp_m5_selectUserInfoByID";

        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@uID", userID);
        sqlParam[1] = new SqlParameter("@plantid", plantid);

        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
    }
}