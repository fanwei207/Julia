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
using adamFuncs;
using CommClass;
public partial class wo2_UMP_getapprover : System.Web.UI.Page
{
    private adamClass adam = new adamClass();
    //string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    RD_Steps step = new RD_Steps();
    RDW apply = new RDW();
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
            txtUserName.Text += ";";
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
        //int type = 0;
        //int id = int.Parse(Request.QueryString["mid"].ToString());

        if (chkUser.Items.Count > 0)
            chkUser.Items.Clear();

        int plantid = Convert.ToInt32(ddl_Company.SelectedValue);
        int departmentId = Convert.ToInt32(ddl_department.SelectedValue);
        string userName = txt_user.Text.Trim();
        string strUser = string.Empty;
        string strUserID = string.Empty;
        //id = int.Parse(Request.QueryString["mid"].ToString()); 
        DataSet dst = selectAllUsers(plantid, departmentId, userName);
        if (dst.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
            {
                ListItem item = new ListItem(dst.Tables[0].Rows[i].ItemArray[3].ToString().Trim() + "--" + dst.Tables[0].Rows[i].ItemArray[2].ToString().Trim(), dst.Tables[0].Rows[i].ItemArray[0].ToString().Trim());
                chkUser.Items.Add(item);
            }
        }
    }
    public DataSet selectAllUsers(int plantid, int departmentId, string userName)
    {
        //try
        //{
        string strSql = "sp_UMP_selectAllUsers";

        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@plantid", plantid);
        sqlParam[1] = new SqlParameter("@departmentId", departmentId);
        sqlParam[2] = new SqlParameter("@userName", userName);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        //}
        //catch (Exception ex)
        //{
        //    //throw ex;
        //    return null;
        //}
    }


    protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkUser.Items.Count == 0) return;


        txtApproveID.Text = string.Empty;
        txtUserName.Text = string.Empty;
        //txtApproveEmail.Text = string.Empty;

        txtApproveID.Text = chkUser.SelectedValue.ToString();
        txtUserName.Text = chkUser.SelectedItem.Text.Substring(0, chkUser.SelectedItem.Text.IndexOf("--"));    //Substring(radioBtnList_SelecetApprove.SelectedItem.Text.IndexOf('-') + 1, radioBtnList_SelecetApprove.SelectedItem.Text.Length);
        int startindex = chkUser.SelectedItem.Text.IndexOf("--") + 2;
        txtApproveEmail.Text = chkUser.SelectedItem.Text.Substring(startindex, chkUser.SelectedItem.Text.Length - startindex);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        ltlAlert.Text = "window.location.href='UMP_add.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
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


    ///// <summary>
    ///// 获取审批者
    ///// </summary>
    ///// <param name="plant"></param>
    ///// <param name="mId"></param>
    ///// <returns></returns>
    //private DataSet getProjQadApprover(int mId)
    //{
    //    try
    //    {
    //        string strSql = "sp_RDW_selectProjQad";

    //        SqlParameter[] sqlParam = new SqlParameter[2];
    //        sqlParam[0] = new SqlParameter("@mid", mId);
    //        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
    //    }
    //    catch (Exception ex)
    //    {
    //        //throw ex;
    //        return null;
    //    }
    //}

    ///// <summary>
    ///// 添加或更新Approver
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="strReviewers"></param>
    ///// <param name="strReviewerNames"></param>
    ///// <param name="uID"></param>
    ///// <returns></returns>
    //private bool UpdateProjQadApprover(int mid, int strApprover, string strApproverName, int uID, string uName)
    //{
    //    try
    //    {
    //        string strSql = "sp_RDW_UpdateProjQadApprover";

    //        SqlParameter[] sqlParam = new SqlParameter[5];
    //        sqlParam[0] = new SqlParameter("@mid", mid);
    //        sqlParam[1] = new SqlParameter("@approveid", strApprover);
    //        sqlParam[2] = new SqlParameter("@approveName", strApproverName);
    //        sqlParam[3] = new SqlParameter("@uid", uID);
    //        sqlParam[4] = new SqlParameter("@uName", uName);

    //        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
    //    }
    //    catch (Exception ex)
    //    {
    //        //throw ex;
    //        return false;
    //    }
    //}


    ///// <summary>
    ///// 选择人员
    ///// </summary>
    ///// <param name="plantid"></param>
    ///// <param name="departmentId"></param>
    ///// <param name="userName"></param>
    ///// <returns></returns>
    //private DataSet selectAllUsers(int plantid, int departmentId, string userName)
    //{
    //    //try
    //    //{
    //        string strSql = "sp_RDW_selectAllUsers";

    //        SqlParameter[] sqlParam = new SqlParameter[3];
    //        sqlParam[0] = new SqlParameter("@plantid", plantid);
    //        sqlParam[1] = new SqlParameter("@departmentId", departmentId);
    //        sqlParam[2] = new SqlParameter("@userName", userName);
    //        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    //throw ex;
    //    //    return null;
    //    //}
    //}

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {


        ltlAlert.Text = "window.close();window.opener.location='UMP_add.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + Request.QueryString["mid"].ToString() + "&rm=" + DateTime.Now.ToString() + "';";
    }

   
}