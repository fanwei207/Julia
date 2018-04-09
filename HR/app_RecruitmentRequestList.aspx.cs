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
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;

public partial class HR_hr_RecruitmentRequestList : BasePage
{
    adamClass chk = new adamClass();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("6080100", "进入面试流程，进行面试安排");
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gv.Columns[12].Visible = this.Security["6080100"].isValid;
            if (Convert.ToInt32(Session["PlantCode"].ToString()) == 1 && Convert.ToInt32(Session["uGroupID"].ToString()) == 1)
            {
                BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                BindAllCompany();
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 2 && Convert.ToInt32(Session["uGroupID"].ToString()) == 19)
            {
                BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                BindAllCompany();
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 5 && Convert.ToInt32(Session["uGroupID"].ToString()) == 5)
            {
                BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                BindAllCompany();
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 8 && Convert.ToInt32(Session["uGroupID"].ToString()) == 4)
            {
                BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                BindAllCompany();
            }
            else if (Session["uName"].ToString() == "管理员")
            {
                BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                BindAllCompany();
            }
            else if (this.Security["6080250"].isValid)
            {
                BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                BindAllCompany();
            }
            else
            {
                labCompany.Visible = false;
                labDepartment.Visible = false;
                ddlCompany.Visible = false;
                ddlDepartment.Visible = false;
                string company = GetUserCop(Convert.ToInt32(Session["PlantCode"].ToString()));
                string deptname = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));
                BindData1(company, deptname, Session["uname"].ToString(), Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                BindAllCompany();
            }
        }
    }
    // <summary>
    /// 获取当前人所属公司
    /// </summary>
    private String GetUserCop(int plantCode)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetUserCop", param).ToString();
    }
    /// <summary>
    /// 获取当前人所属部门
    /// </summary>
    private String GetUserDept(int uid, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@uid", uid);
        param[1] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetUserDept", param).ToString();
    }
    private void BindData(int status)
    {
        //DataTable dt = GetAppList(Session["uname"].ToString());
        DataTable dt = GetAppList(status,txtAppDate.Text, ddlCompany.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    private void BindData1(string company,string deptname,string username,int status)
    {
        DataTable dt = GetAppList1(company, deptname, username, status, txtAppDate.Text);
        gv.DataSource = dt;
        gv.DataBind();
    }
    //private DataTable GetAppList( string username)
    private DataTable GetAppList(int status, string appdate, string companyid, string departmentid)    
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@status", status);
        param[1] = new SqlParameter("@appdate", appdate);
        param[2] = new SqlParameter("@companyid", companyid);
        param[3] = new SqlParameter("@departmentid", departmentid);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_selectAppList",param).Tables[0];
    }
    private DataTable GetAppList1(string company, string deptname, string username, int status, string appdate)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@deptname", deptname);
        param[2] = new SqlParameter("@username", username);
        param[3] = new SqlParameter("@status", status);
        param[4] = new SqlParameter("@appdate", appdate);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_selectAppList1", param).Tables[0];

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Resume")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            Response.Redirect("app_ResumeList.aspx?App_department=" + gv.DataKeys[index].Values["App_department"].ToString() + "&App_Process=" + gv.DataKeys[index].Values["App_Process"].ToString()
                            + "&App_Company=" + gv.DataKeys[index].Values["App_Company"].ToString()+ "&App_plantCode=" + gv.DataKeys[index].Values["App_plantCode"].ToString()
                            + "&App_departmentID=" + gv.DataKeys[index].Values["App_departmentID"].ToString() + "&App_ProcID=" + gv.DataKeys[index].Values["App_ProcID"].ToString());
        }
        if (e.CommandName == "review")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            Response.Redirect("app_ReviewList.aspx?App_department=" + gv.DataKeys[index].Values["App_department"].ToString() 
                + "&App_Process=" + gv.DataKeys[index].Values["App_Process"].ToString() 
                + "&App_Company=" + gv.DataKeys[index].Values["App_Company"].ToString()
                + "&App_NewProc=" + gv.DataKeys[index].Values["APP_NewProc"].ToString()
                + "&App_plantCode=" + gv.DataKeys[index].Values["App_plantCode"].ToString()
                + "&App_departmentID=" + gv.DataKeys[index].Values["App_departmentID"].ToString() 
                + "&App_ProcID=" + gv.DataKeys[index].Values["App_ProcID"].ToString());
        }
        if (e.CommandName == "AppOver")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            //Response.Redirect("app_ReviewList.aspx?App_department=" + gv.DataKeys[index].Values["App_department"].ToString() + "&App_Process=" + gv.DataKeys[index].Values["App_Process"].ToString() + "&App_Company=" + gv.DataKeys[index].Values["App_Company"].ToString());
            if (AppOver(gv.DataKeys[index].Values["App_id"].ToString(), Session["uID"].ToString(), Session["uName"].ToString()))
            {
                if (Convert.ToInt32(Session["PlantCode"].ToString()) == 1 && Convert.ToInt32(Session["uGroupID"].ToString()) == 1)
                {
                    BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                } 
                else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 2 && Convert.ToInt32(Session["uGroupID"].ToString()) == 19)
                {
                    BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                }
                else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 5 && Convert.ToInt32(Session["uGroupID"].ToString()) == 5)
                {
                    BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                }
                else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 8 && Convert.ToInt32(Session["uGroupID"].ToString()) == 4)
                {
                    BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                }
                else if (Session["uName"].ToString() == "管理员")
                {
                    BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString())); 
                }
                else if (this.Security["6080250"].isValid)
                { 
                    BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString())); 
                }
                else
                {
                    string company = GetUserCop(Convert.ToInt32(Session["PlantCode"].ToString()));
                    string deptname = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));
                    BindData1(company, deptname, Session["uname"].ToString(), Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                }
            }
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["App_ProcID"]) == 600)
            {
                //e.Row.Cells[4].Text = "(新)" + gv.DataKeys[e.Row.RowIndex].Values["APP_NewProc"];
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }

            LinkButton linkresume = e.Row.FindControl("linkresume") as LinkButton;
            LinkButton linkReview = e.Row.FindControl("linkReview") as LinkButton;
            LinkButton linkover = e.Row.FindControl("linkover") as LinkButton;

            linkover.ForeColor = System.Drawing.Color.Blue;
            
            //状态不是招聘中
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["App_StatusID"]) != 1)
            {
                linkresume.ForeColor = System.Drawing.Color.Gray;
                //e.Row.Cells[9].BackColor = System.Drawing.Color.Gray;
                e.Row.Cells[10].Enabled = false;
            }
            else
            {
                linkresume.ForeColor = System.Drawing.Color.Blue;
            }
            if (judgeExistReviewRecord(Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_Company"])
                                       , Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_department"])
                                       , Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_Process"]))
                                       )
            {
                e.Row.Cells[11].Enabled = false;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                //不存在审核记录，但是状态也不是审核中，说明岗位申请后直接就通过了，则不允许删除操作
                if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_Status"]) != "审核中")
                {
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Gray;
                }
                else
                {
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Blue;
                }
            }
            //if (Convert.ToInt32(Session["uID"].ToString()) == 13 || Convert.ToInt32(Session["uGroupID"].ToString()) == 1)
            if (this.Security["6080250"].isValid)
            {
                linkReview.ForeColor = System.Drawing.Color.Blue;
                //如果当前用户的名字不是本行的申请人，也不可进行删除操作
                if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_userName"])
                                    != Convert.ToString(Session["uname"].ToString()))
                {
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Gray;
                }
            }
            else
            {
                if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_userName"])
                                    == Convert.ToString(Session["uname"].ToString()))
                {
                    linkReview.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Gray;
                    if (!judgeExistReviewName(Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_Company"])
                                       , Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_department"])
                                       , Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_Process"])
                                       , Session["uname"].ToString()))
                    {
                        linkReview.ForeColor = System.Drawing.Color.Gray;
                        //e.Row.Cells[8].BackColor = System.Drawing.Color.Gray;
                        e.Row.Cells[9].Enabled = false;
                    }
                    else
                    {
                        linkReview.ForeColor = System.Drawing.Color.Blue;
                    }
                }
            }
            if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_Status"]) == "招聘结束" || Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["App_Status"]) == "不通过")
            {
                e.Row.Cells[9].Enabled = false;
                linkresume.ForeColor = System.Drawing.Color.Gray;
                e.Row.Cells[10].Enabled = false;
                linkReview.ForeColor = System.Drawing.Color.Gray;
                e.Row.Cells[11].Enabled = false;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Gray;
                e.Row.Cells[12].Enabled = false;
                linkover.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
    /// <summary>
    /// 面试结束
    /// </summary>
    public bool AppOver(string appid,string uid,string uname)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@appid", appid);
        param[1] = new SqlParameter("@uid", uid);
        param[2] = new SqlParameter("@uname", uname);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_AppOver", param));
    }
    /// <summary>
    /// 判断当前用户是否在审核列表中存在
    /// </summary>
    /// <returns></returns>
    public bool judgeExistReviewName(string company, string depart, string proc,string username)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@depart", depart);
        param[2] = new SqlParameter("@proc", proc);
        param[3] = new SqlParameter("@username", username);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeExistReviewName", param));
    }

    /// <summary>
    /// 判断审核列表中是否有审核记录存在
    /// </summary>
    /// <returns></returns>
    public bool judgeExistReviewRecord(string company, string depart, string proc)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@company", company);
        param[1] = new SqlParameter("@depart", depart);
        param[2] = new SqlParameter("@proc", proc);;
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_judgeExistReviewRecord", param));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["PlantCode"].ToString()) == 1 && Convert.ToInt32(Session["uGroupID"].ToString()) == 1)
        {
            BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
        }
        else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 2 && Convert.ToInt32(Session["uGroupID"].ToString()) == 19)
        {
            BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
        }
        else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 5 && Convert.ToInt32(Session["uGroupID"].ToString()) == 5)
        {
            BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
        }
        else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 8 && Convert.ToInt32(Session["uGroupID"].ToString()) == 4)
        {
            BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
        }
        else if (Session["uName"].ToString() == "管理员")
        {
            BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
        }
        else if (this.Security["6080250"].isValid)
        {
            BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
        }
        else
        {
            string company = GetUserCop(Convert.ToInt32(Session["PlantCode"].ToString()));
            string deptname = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));
            BindData1(company, deptname, Session["uname"].ToString(), Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
        }
    }
    /// <summary>
    /// 绑定所有公司
    /// </summary>
    private void BindAllCompany()
    {
        DataTable dt = GetAllCompany();
        ddlCompany.DataSource = dt;
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("--公司--", "0"));
    }
    /// <summary>
    /// 绑定所有部门
    /// </summary>
    private void BindAllDeparment()
    {
        ddlDepartment.Items.Clear();
        DataTable dt = GetAllDeparment(Convert.ToInt32(ddlCompany.SelectedValue.ToString()));
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("----", "0"));
    }
    /// <summary>
    /// 获取所有公司
    /// </summary>
    private DataTable GetAllCompany()
    {
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAllCop").Tables[0];
    }
    /// <summary>
    /// 获取部门
    /// </summary>
    private DataTable GetAllDeparment(int copid)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@copid", copid);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAllDeptByCopID",param).Tables[0];
    }
    /// <summary>
    /// 获取岗位
    /// </summary>
    private DataTable GetAllProcess(string  copid,int deptid)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@copid", copid);
        param[1] = new SqlParameter("@deptid", deptid);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAllDeptByDeptID",param).Tables[0];
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllDeparment();
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeleteLadingList(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["App_id"].ToString())))
        {
            if (Convert.ToInt32(Session["uID"].ToString()) == 13)
            {
                BindData(Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
            }
            else
            {
                string company = GetUserCop(Convert.ToInt32(Session["PlantCode"].ToString()));
                string deptname = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));
                BindData1(company, deptname, Session["uname"].ToString(), Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
            }
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
    }
    private bool DeleteLadingList(int id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_DeleteApp", param));
    }


}