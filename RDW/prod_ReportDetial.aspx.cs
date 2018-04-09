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
public partial class RDW_prod_ReportDetial : BasePage
{
    RDW rdw = new RDW();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidTypeStatus.Value = Request["typeStatus"].ToString();
            labNo.Text = Request["no"].ToString();
            labCode.Text = Request["code"].ToString();
            labProjectName.Text = Request["name"].ToString();
            labQAD.Text = Request["qad"].ToString();
            labPCB.Text = Request["pcb"].ToString();
            labEndDate.Text = Request["endDate"].ToString();
            labPlanDate.Text = getPlanDate();
            //表头 试流单进本信息
            hidNo.Value = Request["no"].ToString();
            hidCode.Value = Request["code"].ToString();
            hidProjName.Value = Request["name"].ToString();
            hidQAD.Value = Request["qad"].ToString();
            hidPCB.Value = Request["pcb"].ToString();
            hidEndDate.Value = Request["endDate"].ToString();
            hidPlanDate.Value = labPlanDate.Text;
            hidmid.Value = Request["mid"].ToString();
            hiddid.Value = Request["did"].ToString();
            hidprodid.Value = Request["prodid"].ToString();
            lbltype.Text = GetProcUser(1, "username","1"); 
            //获取每个步骤的相关人员
            labProcUser.Text = GetProcUser(1, "username"); //工艺确认
            labPlanUser.Text = GetProcUser(2, "username"); //计划情况
            labPurUser.Text = GetProcUser(3, "username"); //采购情况
            labProdUser.Text = GetProcUser(4, "username"); //试流结果


            hidProcUser.Value = GetProcUser(1, "userid"); //工艺确认
            hidPlanUser.Value = GetProcUser(2, "userid"); //计划情况
            hidPurUser.Value = GetProcUser(3, "userid"); //采购情况
            hidProdUser.Value = GetProcUser(4, "userid"); //试流结果




            labProcAnalyUser.Text = GetProcUser(5, "username"); //试流总结
            labProcSolveUser.Text = GetProcUser(6, "username"); //解决方案

            //
            hidPowerNameByCeshi.Value = GetPowerName("0");
            hidPowerName.Value = GetPowerName("1");
            //相关项目步骤的人员也可以进行试流总结及分析

            hiduserid.Value = GetProjUser(Convert.ToInt32(hidmid.Value));
            //获取每个步骤相关留言及其附件
            if (GetProcMagandFile(hidNo.Value.ToString(), "prod") == string.Empty)
            {
                tdFileByProc.InnerHtml = "无附件";
            }
            else
            {
                tdFileByProc.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "prod");
            }
            tdceshi.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "ceshi").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");


            tdMsgAndFileByPur.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "purchase").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByProc.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "proc").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByPlan.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "plan").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByProduct.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "product").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByProcAnaly.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "procAnaly").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            tdMsgAndFileByProcSolve.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "procSolve").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
            Bind();

            hiduName.Value = Session["uName"].ToString();
            hiduID.Value = Session["uID"].ToString();
            if (labProcUser.Text.IndexOf(hiduName.Value.ToString()) == -1)
            {
                btnProcYes.Enabled = false;
                btnProcNo.Enabled = false;
            }
            if (labProdUser.Text.IndexOf(hiduName.Value.ToString()) == -1)
            {
                btnPassYes.Enabled = false;
                btnPassNo.Enabled = false;
            }
        }

        hiduserid.Value = GetProjUser(Convert.ToInt32(hidmid.Value));
    }
    private void Bind()
    {
        DataTable dtMstr = getProdMstr(labNo.Text);
        foreach (DataRow rw in dtMstr.Rows)
        {
            hidProcStatus.Value = rw["prod_procStatus"].ToString();
            hidPurStatus.Value = rw["prod_purStatus"].ToString();
            hidPlanStatus.Value = rw["prod_planStatus"].ToString();
            hidProdStatus.Value = rw["prod_prodStatus"].ToString();
            hidProcAnalyStatus.Value = rw["prod_procAnalyStatus"].ToString();
            hidProcSolveStatus.Value = rw["prod_procSolveStatus"].ToString();

            if (hidProcStatus.Value.ToString() == "1")
            {
                labShowProc.Text = "工艺可试流" + "\t" + rw["prod_procName"].ToString() + "\t" + rw["prod_procDate"].ToString();
            }
            else if (hidProcStatus.Value.ToString() == "2")
            {
                labShowProc.Text = "工艺不可试流" + "\t" + rw["prod_procName"].ToString() + "\t" + rw["prod_procDate"].ToString();
            }
            if (hidProdStatus.Value.ToString() == "1")
            {
                btnProcYes.Enabled = false;
                btnProcNo.Enabled = false;
                //btnPassYes.Enabled = false;
                //btnPassNo.Enabled = false;
                labShowProd.Text = "试流通过" + "\t" + rw["prod_prodName"].ToString() + "\t" + rw["prod_prodDate"].ToString();
            }
            else if (hidProdStatus.Value.ToString() == "2")
            {
                btnProcYes.Enabled = false;
                btnProcNo.Enabled = false;
                //btnPassYes.Enabled = false;
                //btnPassNo.Enabled = false;
                labShowProd.Text = "试流不通过" + "\t" + rw["prod_prodName"].ToString() + "\t" + rw["prod_prodDate"].ToString();
            }
            else if (hidProdStatus.Value.ToString() == "3")
            {
                btnProcYes.Enabled = false;
                btnProcNo.Enabled = false;
            }
            if (!ExistsFlowRecord())
            {
                hidExistsFlowRecord.Value = "1";
                btnProcYes.Enabled = false;
                btnProcNo.Enabled = false;
            }
            else
            {
                hidExistsFlowRecord.Value = "0";
            }
        }
    }
    private bool ExistsFlowRecord()
    {
        string str = "sp_prod_existsFlowRecord";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@no", Request["no"].ToString());
        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[1].Value);
    }
    private DataTable getProdMstr(string no)
    {
        string str = "sp_prod_selectProdMstr";

        SqlParameter param = new SqlParameter("@no", no);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    private string GetPowerName(string type)
    {
        string str = "sp_prod_GetPowerName";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@type", type);
        param[1] = new SqlParameter("@mid", Request["mid"].ToString());
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    private string GetProcUser(int deptid, string type,string gettype = "0")
    {
        string str = "sp_prod_GetProcUser";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@deptID", deptid);
        param[1] = new SqlParameter("@type", type);
        param[2] = new SqlParameter("@mid", Request["prodid"].ToString());
        param[3] = new SqlParameter("@gettype", gettype);
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    private string GetProjUser(int mid)
    {
        string str = "sp_prod_GetProjUser";
        SqlParameter param = new SqlParameter("@mid", mid);
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    private string GetProcMagandFile(string no, string dept)
    {
        string str = "sp_prod_GetProcMagandFile";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@dept", dept);
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    protected void btnProcYes_Click(object sender, EventArgs e)
    {
        string didStatus = string.Empty;
        if (this.hiddid.Value.ToString() == string.Empty)
        {
            didStatus = "Y";
        }
        else
        {
            didStatus = "N";
        }
        if (changeProdStatus(labNo.Text, labProjectName.Text, labCode.Text, "1", didStatus, hidprodid.Value.ToString()))
        {
            Bind();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('可试流失败，请联系管理员！');";
            return;
        }
    }
    protected void btnProcNo_Click(object sender, EventArgs e)
    {
        string didStatus = string.Empty;
        if (this.hiddid.Value.ToString() == string.Empty)
        {
            didStatus = "Y";
        }
        else
        {
            didStatus = "N";
        }
        if (changeProdStatus(labNo.Text, labProjectName.Text, labCode.Text, "2", didStatus, hidprodid.Value.ToString()))
        {
            Bind();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('不可试流失败，请联系管理员！');";
            return;
        }
    }
    private string getPlanDate()
    {
        string sql = "select convert(varchar(10),prod_PlanDate,120) as prod_PlanDate  from prod_mstr where prod_No ='" + Request["no"].ToString() + "'";
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.Text, sql));
    }
    private bool changeProdStatus(string no, string proj, string code, string status, string didStatus, string prodid)
    {
        string str = "sp_prod_changeProdStatus";

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@proj", proj);
        param[2] = new SqlParameter("@code", code);
        param[3] = new SqlParameter("@status", status);
        param[4] = new SqlParameter("@didStatus", didStatus);
        param[5] = new SqlParameter("@uID", Session["uID"].ToString());
        param[6] = new SqlParameter("@uName", Session["uName"].ToString());
        param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[7].Direction = ParameterDirection.Output;
        param[8] = new SqlParameter("@prodid", prodid);

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, str, param);
        return Convert.ToBoolean(param[7].Value);

    }
    protected void btnPassYes_Click(object sender, EventArgs e)
    {
        string didStatus = string.Empty;
        if (this.hiddid.Value.ToString() == string.Empty)
        {
            didStatus = "Y";
        }
        else
        {
            didStatus = "N";
        }
        if (changeProdStatus(labNo.Text, labProjectName.Text, labCode.Text, "3", didStatus, hidprodid.Value.ToString()))
        {
            Bind();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('试流结果通过失败，请联系管理员！');";
            return;
        }
    }
    protected void btnPassNo_Click(object sender, EventArgs e)
    {
        string didStatus = string.Empty;
        if (this.hiddid.Value.ToString() == string.Empty)
        {
            didStatus = "Y";
        }
        else
        {
            didStatus = "N";
        }
        if (changeProdStatus(labNo.Text, labProjectName.Text, labCode.Text, "4", didStatus, hidprodid.Value.ToString()))
        {
            Bind();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('试流结果不通过失败，请联系管理员！');";
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("/RDW/Prod_Report.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "no=" + Request["no"].ToString() + "&name="
                                      + Request["name"].ToString() + "&code="
                                      + Request["code"].ToString() + "&qad="
                                      + Request["qad"].ToString() + "&pcb="
                                      + Request["pcb"].ToString() + "&planDate="
                                      + Request["planDate"].ToString() + "&endDate="
                                      + Request["endDate"].ToString() + "&mid="
                                      + Request["mid"].ToString() + "&did="
                                      + Request["did"].ToString() + "&typeStatus="
                                      + Request["typeStatus"]);
    }
}