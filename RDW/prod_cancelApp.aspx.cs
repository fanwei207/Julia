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
using RD_WorkFlow;

public partial class RDW_prod_cancelApp : System.Web.UI.Page
{
    RDW rdw = new RDW();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNo.Enabled = false;
            txtProjName.Enabled = false;
            txtCode.Enabled = false;
            txtPCB.Enabled = false;
            txtQAD.Enabled = false;
            txtEndDate.Enabled = false;
            txtPlanDate.Enabled = false;

            txtNo.Text = Request["no"].ToString();
            txtProjName.Text = Request["name"].ToString();
            txtCode.Text = Request["code"].ToString();
            txtPCB.Text = Request["pcb"].ToString();
            txtQAD.Text = Request["qad"].ToString();
            txtEndDate.Text = Request["endDate"].ToString();
            txtPlanDate.Text = Request["planDate"].ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/Prod_Report.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "no=" + Request["no"] + "&name="
                                + Request["name"] + "&code="
                                + Request["code"] + "&qad="
                                + Request["qad"] + "&pcb="
                                + Request["pcb"] + "&planDate="
                                + Request["planDate"] + "&endDate="
                                + Request["endDate"] + "&mid="
                                + Request["mid"] + "&did="
                                + Request["did"]);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if(txtCancelReas.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写试流单取消的原因！');";
            return;
        }
        if (cancelApp(txtNo.Text, txtProjName.Text, txtCode.Text))
        {
            string massage = massage = Session["eName"].ToString() + " 取消试流单，取消原因： " + txtCancelReas.Text + ",--项目代码 : " + txtCode.Text + ",--项目名称 : " + txtProjName.Text; ;
            if (!insertMassage(Request.QueryString["mid"], Request.QueryString["did"], massage, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('消息保存失败，请联系管理员！');";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('试流单取消成功！');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('试流单取消失败，请联系管理员！');";
            return;
        }
    }
    private bool cancelApp(string no, string projName, string code)
    {
        string str = "sp_prod_cancelApp";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@projectname", projName);
        param[2] = new SqlParameter("@code", code);
        param[3] = new SqlParameter("@uid", Session["uID"].ToString());
        param[4] = new SqlParameter("@uname", Session["uName"].ToString());
        param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[5].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[5].Value);
    }
    private bool insertMassage(string mid, string did, string massage, string uID, string uName)
    {
        try
        {
            string str = "sp_prod_insertProdMassage";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@mid", mid);
            param[1] = new SqlParameter("@did", did);
            param[2] = new SqlParameter("@massage", massage);
            param[3] = new SqlParameter("@uID", uID);
            param[4] = new SqlParameter("@uName", uName);
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
            return Convert.ToBoolean(param[5].Value);
        }
        catch
        {
            return false;
        }
    }
}