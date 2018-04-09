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
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class HR_app_InterviewList : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            BindAllCompany();
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
    /// 获取所有公司
    /// </summary>
    private DataTable GetAllCompany()
    {
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAllCop").Tables[0];
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllDeparment();
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
    /// 获取部门
    /// </summary>
    private DataTable GetAllDeparment(int copid)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@copid", copid);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetAllDeptByCopID", param).Tables[0];
    }
    private void BindData()
    {
        DataTable dt = GetInformationList(Convert.ToInt32(ddlStatus.SelectedValue.ToString()), ddlCompany.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), txtInterviewDate.Text);
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable GetInformationList(int status, string companyid, string departmentid, string appdate)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@status", status);
        param[1] = new SqlParameter("@companyid", companyid);
        param[2] = new SqlParameter("@departmentid", departmentid);
        param[3] = new SqlParameter("@appdate", appdate);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetInterviewList", param).Tables[0];
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton linkInterview = e.Row.FindControl("linkInterview") as LinkButton;
            LinkButton linkEmploy = e.Row.FindControl("linkEmploy") as LinkButton;
            LinkButton linkEmail = e.Row.FindControl("linkEmail") as LinkButton; 
            //是否面试
            if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["interviewTime"]) == string.Empty)
            {
                linkInterview.ForeColor = System.Drawing.Color.Blue;
                linkEmail.ForeColor = System.Drawing.Color.Gray;
                linkEmploy.ForeColor = System.Drawing.Color.Gray;
                if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["plantcode"]) != Convert.ToInt32(Session["PlantCode"].ToString()))
                {
                    e.Row.Cells[10].Enabled = false;
                }
                e.Row.Cells[11].Enabled = false;
                e.Row.Cells[14].Enabled = false;
            }
            else
            {
                if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "同意录用")
                {
                    if (Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex].Values["isSendEmployEmail"]) == true)
                    {
                        linkInterview.ForeColor = System.Drawing.Color.Gray;
                        linkEmail.ForeColor = System.Drawing.Color.Red;
                        linkEmploy.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[10].Enabled = false;
                        e.Row.Cells[11].Enabled = false;
                    }
                    else
                    {
                        linkInterview.ForeColor = System.Drawing.Color.Gray;
                        linkEmail.ForeColor = System.Drawing.Color.Blue;
                        linkEmploy.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[10].Enabled = false;
                    }
                }
                else if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) == "已建档")
                {
                    linkInterview.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[10].Enabled = false;
                    linkEmail.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[14].Enabled = false;
                    e.Row.Cells[14].Text = "已建档";
                    e.Row.Cells[14].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    linkInterview.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[10].Enabled = false;
                    linkEmail.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[11].Enabled = false;
                    linkEmploy.ForeColor = System.Drawing.Color.Gray;
                    e.Row.Cells[14].Enabled = false;
                }
            }
            //是否应试
            //if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["examinationTime"]) == string.Empty)
            //{
            //    linkEmploy.ForeColor = System.Drawing.Color.Blue;
            //    e.Row.Cells[13].Enabled = false;
            //}
            //else
            //{
            //    linkInterview.ForeColor = System.Drawing.Color.Gray;
            //    e.Row.Cells[12].Enabled = false;
            //}
            //人员录用，信息进入正式系统
            //if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["status"]) != "同意聘用")
            //{
            //    linkEmploy.ForeColor = System.Drawing.Color.Gray;
            //    e.Row.Cells[13].Enabled = false;
            //}
            //else
            //{
            //    linkEmploy.ForeColor = System.Drawing.Color.Blue;
            //}
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownLoad")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string filePath = gv.DataKeys[index].Values["fpath"].ToString();
            try
            {
                filePath = Server.MapPath(filePath);
                filePath = filePath.Replace("\\", "/");
            }
            catch (Exception)
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName == "interview")
        {
            if (Convert.ToInt32(Session["PlantCode"].ToString()) == 1)
            {
                int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
                //Response.Redirect("app_ReviewList.aspx?App_department=" + gv.DataKeys[index].Values["App_department"].ToString() + "&App_Process=" + gv.DataKeys[index].Values["App_Process"].ToString() + "&App_Company=" + gv.DataKeys[index].Values["App_Company"].ToString());
                Response.Redirect("app_InterviewEmail_SZX.aspx?userEmail=" + gv.DataKeys[index].Values["email"].ToString() + "&username=" + gv.DataKeys[index].Values["username"].ToString()
                                    + "&sex=" + gv.DataKeys[index].Values["sex"].ToString() + "&company=" + gv.DataKeys[index].Values["company"].ToString() + "&department="
                                    + gv.DataKeys[index].Values["department"].ToString() + "&process=" + gv.DataKeys[index].Values["process"].ToString());
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 2)
            {
                int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
                //Response.Redirect("app_ReviewList.aspx?App_department=" + gv.DataKeys[index].Values["App_department"].ToString() + "&App_Process=" + gv.DataKeys[index].Values["App_Process"].ToString() + "&App_Company=" + gv.DataKeys[index].Values["App_Company"].ToString());
                Response.Redirect("app_InterviewEmail_ZQL.aspx?userEmail=" + gv.DataKeys[index].Values["email"].ToString() + "&username=" + gv.DataKeys[index].Values["username"].ToString()
                                    + "&sex=" + gv.DataKeys[index].Values["sex"].ToString() + "&company=" + gv.DataKeys[index].Values["company"].ToString() + "&department="
                                    + gv.DataKeys[index].Values["department"].ToString() + "&process=" + gv.DataKeys[index].Values["process"].ToString());
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 5)
            {
                int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
                //Response.Redirect("app_ReviewList.aspx?App_department=" + gv.DataKeys[index].Values["App_department"].ToString() + "&App_Process=" + gv.DataKeys[index].Values["App_Process"].ToString() + "&App_Company=" + gv.DataKeys[index].Values["App_Company"].ToString());
                Response.Redirect("app_InterviewEmail_YQL.aspx?userEmail=" + gv.DataKeys[index].Values["email"].ToString() + "&username=" + gv.DataKeys[index].Values["username"].ToString()
                                    + "&sex=" + gv.DataKeys[index].Values["sex"].ToString() + "&company=" + gv.DataKeys[index].Values["company"].ToString() + "&department="
                                    + gv.DataKeys[index].Values["department"].ToString() + "&process=" + gv.DataKeys[index].Values["process"].ToString());
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 8)
            {
                int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
                //Response.Redirect("app_ReviewList.aspx?App_department=" + gv.DataKeys[index].Values["App_department"].ToString() + "&App_Process=" + gv.DataKeys[index].Values["App_Process"].ToString() + "&App_Company=" + gv.DataKeys[index].Values["App_Company"].ToString());
                Response.Redirect("app_InterviewEmail_HQL.aspx?userEmail=" + gv.DataKeys[index].Values["email"].ToString() + "&username=" + gv.DataKeys[index].Values["username"].ToString()
                                    + "&sex=" + gv.DataKeys[index].Values["sex"].ToString() + "&company=" + gv.DataKeys[index].Values["company"].ToString() + "&department="
                                    + gv.DataKeys[index].Values["department"].ToString() + "&process=" + gv.DataKeys[index].Values["process"].ToString());
            }
        }
        if (e.CommandName == "exam")
        {
           Response.Redirect("app_NewResume.aspx");
        }
        if (e.CommandName == "employ")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int id = Convert.ToInt32(gv.DataKeys[index].Values["id"].ToString());
            int appid = Convert.ToInt32(gv.DataKeys[index].Values["appid"].ToString());
            if (copyUserInfoToFormal(id,appid,Convert.ToInt32(Session["uID"].ToString()),Convert.ToInt32(Session["PlantCode"].ToString())))
            {
                ltlAlert.Text = "alert('恭喜，人员录用成功！')";
                return;
            }
        }
        if (e.CommandName == "employ1")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int id = Convert.ToInt32(gv.DataKeys[index].Values["id"].ToString());
            int appid = Convert.ToInt32(gv.DataKeys[index].Values["appid"].ToString());
            string company = gv.DataKeys[index].Values["company"].ToString();
            string department = gv.DataKeys[index].Values["department"].ToString();
            string process = gv.DataKeys[index].Values["process"].ToString();
            Response.Redirect("app_EditPersonnel.aspx?tableid=" + id + "&appid=" + appid + "&company=" + company + "&department=" + department + "&process=" + process);
        }
        if (e.CommandName == "Email")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            Response.Redirect("app_EmployEmail.aspx?userEmail=" + gv.DataKeys[index].Values["email"].ToString() + "&sex=" + gv.DataKeys[index].Values["sex"].ToString()
                 + "&username=" + gv.DataKeys[index].Values["username"].ToString() + "&process=" + gv.DataKeys[index].Values["process"].ToString()
                 + "&id=" + gv.DataKeys[index].Values["id"].ToString());
        }
    }
    public bool copyUserInfoToFormal(int id,int appid,int uid,int plantcode)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@appid", appid);
        param[2] = new SqlParameter("@uid", uid);
        param[3] = new SqlParameter("@plantcode", plantcode);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_copyUserInfoToFormal", param));
    }
    protected void btnQuery_Click(object sender, System.EventArgs e)
    {
        BindData();
    }
}