using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class RDW_Test_ReportDetial : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //当前用户的信息
            hiduID.Value = Session["uID"].ToString();
            hiduName.Value = Session["uName"].ToString();

            hidNo.Value = Request["no"].ToString();
            hidCode.Value = Request["code"].ToString();
            hidProjName.Value = Request["name"].ToString();
            hidID.Value = Request["id"].ToString();
            hidmid.Value = Request["mid"].ToString();
            hiddid.Value = Request["did"].ToString();
            hidCreateBy.Value = Request["createby"].ToString();
            hidCreateName.Value = Request["createname"].ToString();

            labNo.Text = Request["no"].ToString();
            labCode.Text = Request["code"].ToString();
            labType.Text = Request["type"].ToString();
            labFailureTime.Text = Request["failuretime"].ToString();
            labProblemContent.Text = Request["problemcontent"].ToString();

            labTestPlan.Text = getPlanDate();
            
            if (Request["choose"] != null)
            {
                //labTestUserName.Text = Request["choose"].ToString();
                //labTestUserID.Text = Request["chooseid"].ToString();
            }
            tdMsgAndFileByAnalysisReason.InnerHtml = GetProcMagandFile(hidID.Value.ToString(), "analysisReason").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByTempSolve.InnerHtml = GetProcMagandFile(hidID.Value.ToString(), "tempSolve").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByTempAction.InnerHtml = GetProcMagandFile(hidID.Value.ToString(), "tempAction").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByRealSolve.InnerHtml = GetProcMagandFile(hidID.Value.ToString(), "realSolve").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByRealAction.InnerHtml = GetProcMagandFile(hidID.Value.ToString(), "realAction").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByEffectConfirm.InnerHtml = GetProcMagandFile(hidID.Value.ToString(), "effectConfirm").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByTestPlan.InnerHtml = GetProcMagandFile(hidID.Value.ToString(), "testPlan").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            //Bind();


            //获取每个步骤的相关人员

            labTestUserName.Text = GetProcUser("0", "username"); //测试人员
            labProjectManage.Text = GetProcUser("9", "username"); //可行性测试项目经理
            labEffectConfirm.Text = GetProcUser("10", "username"); //效果确认


            hidTestUserID.Value = GetProcUser("0", "userid"); //测试人员ID
            hidProjectManageID.Value = GetProcUser("9", "userid"); //可行性测试项目经理ID
            hidEffectConfirm.Value = GetProcUser("10", "userid"); //效果确认


            hidPowerName.Value = GetPowerName("1");
            //显示出有权限的人
            labUserPower.Text = GetProcUser("0", "username").Replace(",", "<br /><br />");
        }
    }

    private string GetPowerName(string type)
    {
        string str = "sp_prod_GetPowerName";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@type", type);
        param[1] = new SqlParameter("@mid", Request["mid"].ToString());
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    private string GetProcUser(string deptid, string type)
    {
        string str = "sp_test_GetTestUser";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@type", type);
        param[1] = new SqlParameter("@id", Request["id"].ToString());
        param[2] = new SqlParameter("@deptid", deptid);
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    private string getPlanDate()
    {
        string sql = "select convert(varchar(10),test_PlanCompleteDate,120) as test_PlanCompleteDate  from test_mstr where test_ID ='" + Request["id"].ToString() + "'";
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.Text, sql));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["come"].ToString() == "prod")
        {
            Response.Redirect("../RDW/Test_Report.aspx?" + (Request["from"] == null ? "" : "from=rdw&") +
                                                        "prodno=" + Request["no"].ToString() + "&projectname="
                                                              + Request["name"].ToString() + "&projectcode="
                                                              + Request["code"].ToString() + "&mid="
                                                              + Request["mid"].ToString() + "&did="
                                                              + Request["did"].ToString() + "&prodid="
                                                              + Request["prodid"].ToString());
        }
        else if (Request["come"].ToString() == "test")
        {
            Response.Redirect("../RDW/Test_Report.aspx");
        }
    }

    private string GetProcMagandFile(string id, string dept)
    {
        string str = "sp_test_GetTestMagandFile";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@dept", dept);
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
}