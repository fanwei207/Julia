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

public partial class RDW_Test_Report : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            //如果from不为空，则表示从步骤页面链接，此时 code不可编辑，New按钮可见
            if (Request["mid"] != null)
            {
                if (Request["from"] == null)
                {
                    hidProdNo.Value = Request["prodno"].ToString();
                    hidProdID.Value = Request["prodid"].ToString();
                    txtProdNo.Text = Request["prodno"].ToString();
                }
                else
                {
                    btnBack.Visible = false;
                }
                hidMID.Value = Request["mid"].ToString();
                hidDID.Value = Request["did"].ToString();
                hidProjectName.Value = Request["projectname"].ToString();
                hidProjectCode.Value = Request["projectcode"].ToString();

                txtProjectCode.Text = Request["projectcode"].ToString();
                //txtProdNo.Enabled = false;
                txtProjectCode.Enabled = false;
            }
            else
            {
                btnBack.Visible = false;
            }
            BindGv();
        }
    }
    private void BindGv()
    {
        DataTable dt = getTestList(txtProdNo.Text.Trim(), txtProjectCode.Text.Trim(), ddlType.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString(), ddlStep.SelectedValue.ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable getTestList(string prodno, string prodcode, string type, string status, string step)
    {
        string str = "sp_test_selectTestMstr";
        SqlParameter[] pram = new SqlParameter[20];
        pram[0] = new SqlParameter("@prodno", prodno);
        pram[1] = new SqlParameter("@prodcode", prodcode);
        pram[2] = new SqlParameter("@type", type);
        pram[3] = new SqlParameter("@status", status);
        pram[4] = new SqlParameter("@step", step);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, str, pram).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGv();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (Request["mid"] != null)
        {
            if (Request["from"] != null)
            {
                Response.Redirect("../RDW/Test_NewTest.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + 
                                                   "mid=" + hidMID.Value + 
                                                   "&did=" + hidDID.Value +
                                                   //"&prodno=" + hidProdNo.Value +
                                                   "&projectname=" + hidProjectName.Value +
                                                   "&projectcode=" + hidProjectCode.Value);
            }
            else
            {
                Response.Redirect("../RDW/Test_NewTest.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "mid=" + hidMID.Value + "&did=" + hidDID.Value +
                                                   "&prodno=" + hidProdNo.Value +
                                                   "&projectname=" + hidProjectName.Value +
                                                   "&projectcode=" + hidProjectCode.Value +
                                                   "&prodid=" + hidProdID.Value);
            }
        }        
        else
        {
            Response.Redirect("../RDW/Test_NewTest.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["from"] == null)
        {
            Response.Redirect("../RDW/prod_Report.aspx");
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGv();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "det")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (Request["mid"] != null)
            {
                Response.Redirect("/RDW/Test_ReportDetial.aspx?" + (Request["from"] == null ? "" : "from=rdw&") +
                                                       "id=" + gv.DataKeys[index].Values["test_ID"].ToString() +
                                                       "&no=" + gv.DataKeys[index].Values["prod_No"].ToString() +
                                                       "&name=" + gv.DataKeys[index].Values["prod_ProjectName"].ToString() +
                                                       "&code=" + gv.DataKeys[index].Values["prod_Code"].ToString() +
                                                       "&type=" + gv.DataKeys[index].Values["test_Type"].ToString() +
                                                       "&mid=" + gv.DataKeys[index].Values["prod_mid"].ToString() +
                                                       "&did=" + gv.DataKeys[index].Values["prod_did"].ToString() +
                                                       "&failuretime=" + gv.DataKeys[index].Values["test_FailureTime"].ToString() +
                                                       "&problemcontent=" + gv.DataKeys[index].Values["test_ProblemContent"].ToString() +
                                                       "&prodid=" + hidProdID.Value.ToString() +
                                                       "&createby=" + gv.DataKeys[index].Values["CreateBy"].ToString() +
                                                       "&createname=" + gv.DataKeys[index].Values["CreateName"].ToString() +
                                                       "&come=" + "prod");
            }
            else
            {
                Response.Redirect("/RDW/Test_ReportDetial.aspx?" + (Request["from"] == null ? "" : "from=rdw&") +
                                                       "id=" + gv.DataKeys[index].Values["test_ID"].ToString() +
                                                       "&no=" + gv.DataKeys[index].Values["prod_No"].ToString() +
                                                       "&name=" + gv.DataKeys[index].Values["prod_ProjectName"].ToString() +
                                                       "&code=" + gv.DataKeys[index].Values["prod_Code"].ToString() +
                                                       "&type=" + gv.DataKeys[index].Values["test_Type"].ToString() +
                                                       "&mid=" + gv.DataKeys[index].Values["prod_mid"].ToString() +
                                                       "&did=" + gv.DataKeys[index].Values["prod_did"].ToString() +
                                                       "&failuretime=" + gv.DataKeys[index].Values["test_FailureTime"].ToString() +
                                                       "&problemcontent=" + gv.DataKeys[index].Values["test_ProblemContent"].ToString() +
                                                       "&prodid=" + hidProdID.Value.ToString() +
                                                       "&createby=" + gv.DataKeys[index].Values["CreateBy"].ToString() +
                                                       "&createname=" + gv.DataKeys[index].Values["CreateName"].ToString() +
                                                       "&come=" + "test");
            }
        } 
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGv();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGv();
    }
    protected void ddlStep_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGv();
    }
    protected void btnEXCEL_Click(object sender, EventArgs e)
    {
        DataTable dt = getTestListecxel(txtProdNo.Text.Trim(), txtProjectCode.Text.Trim(), ddlType.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString(), ddlStep.SelectedValue.ToString());
        string title = "<b>跟踪号</b>~^200^<b>项目名称/ECN编号</b>~^<b>项目代码</b>~^<b>分类</b>~^<b>失效时间</b>~^100^<b>严重程度</b>~^100^<b>问题内容</b>~^100^<b>创建人</b>~^100^<b>创建时间</b>~^100^<b>跟踪计划</b>~^<b>原因分析</b>~^<b>临时解决方案</b>~^<b>临时行动</b>~^<b>根本解决方案</b>~^<b>根本行动</b>~^<b>效果确认</b>~^";
        this.ExportExcel(title, dt, true);
    }
    private DataTable getTestListecxel(string prodno, string prodcode, string type, string status, string step)
    {
        string str = "sp_test_selectTestMstrExcel";
        SqlParameter[] pram = new SqlParameter[20];
        pram[0] = new SqlParameter("@prodno", prodno);
        pram[1] = new SqlParameter("@prodcode", prodcode);
        pram[2] = new SqlParameter("@type", type);
        pram[3] = new SqlParameter("@status", status);
        pram[4] = new SqlParameter("@step", step);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, str, pram).Tables[0];
    }
}