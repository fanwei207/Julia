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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using RD_WorkFlow;
using System.Net.Mail;
using System.Text;
using System.IO;
using adamFuncs;
using CommClass;

public partial class wo2_UMP_finishList : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProjData();
        }
    }
    private void BindProjData()
    {
        //string strID = Convert.ToString(Request.QueryString["mid"]);

        DataTable dt = getProjQadApplyMstr(txtProjectcode.Text, ddlstatus.SelectedValue, chkb_displayToApprove.Checked, Session["uID"].ToString(), ddldomain.SelectedItem.Text, txtdepCode.Text);

        gv.DataSource = dt;
        gv.DataBind();

    }
    public DataTable getProjQadApplyMstr(string projCode, string status, bool readme, string uid, string domain, string depcode)
    {
        string strSql = "sp_UMP_selectUMPfinishList";
        SqlParameter[] sqlParam = new SqlParameter[9];
        sqlParam[0] = new SqlParameter("@projCode", projCode);
        sqlParam[1] = new SqlParameter("@status", status);
        sqlParam[2] = new SqlParameter("@readme", readme);
        sqlParam[3] = new SqlParameter("@uid", uid);
        sqlParam[4] = new SqlParameter("@domain", domain);
        sqlParam[5] = new SqlParameter("@depcode", depcode);
        sqlParam[6] = new SqlParameter("@date", txtApplyDate.Text);
        sqlParam[7] = new SqlParameter("@date2", txtApplyDate1.Text);
        sqlParam[8] = new SqlParameter("@trnbr", txttrnbr.Text);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        this.BindProjData();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "look")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string UMP_appby = gv.DataKeys[index].Values["UMP_appby"].ToString();
            string UMP_createby = gv.DataKeys[index].Values["UMP_createby"].ToString();
            string appv = "0";
            
            string strmid = gv.DataKeys[index].Values["id"].ToString();
            Response.Redirect("UMP_add.aspx?mid=" + strmid + "&islook=no&iApprove=" + appv, true);
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //if ((gv.DataKeys[e.Row.RowIndex].Values["UMP_status"]).ToString() != "新增")
            //{
            //    e.Row.Cells[13].Text = string.Empty;
            //}

        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindProjData();
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("UMP_add.aspx", true);
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {

        DataTable dt = getProjQadApplyMstr(txtProjectcode.Text, ddlstatus.SelectedValue, chkb_displayToApprove.Checked, Session["uID"].ToString(), ddldomain.SelectedItem.Text, txtdepCode.Text);

        string title = "<b>申请单号</b>~^<b>创建人</b>~^<b>创建时间</b>~^<b>域 Code</b>~^100^<b>地点</b>~^100^<b>成本中心</b>~^100^<b>类型</b>~^<b>总账</b>~^100^<b>明细账</b>~^100^<b>物料号</b>~^100^<b>物料描述</b>~^100^<b>申请量</b>~^100^<b>实发量</b>~^100^<b>单位</b>~^100^<b>备注</b>~^";
        this.ExportExcel(title, dt, true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
}