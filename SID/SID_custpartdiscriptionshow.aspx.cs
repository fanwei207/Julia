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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using PIInfo;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using adamFuncs;
using System.Data.OleDb;
using QADSID;

public partial class SID_SID_custpartdiscriptionshow : BasePage
{
    adamClass adam = new adamClass();
    SID sid = new SID();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["id"] != null)
            {
                txtcust.Text = Request["cust"].ToString();
                txtpart.Text = Request["part"].ToString();
            }
            Databind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Databind();
    }
    public void Databind()
    {
        gv.DataSource = sid.showCustDiscription(txtpart.Text.Trim(),txtcust.Text.Trim(),txtHST.Text.Trim());
        gv.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Databind();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["SID_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_SID_deleteCustPart", param);
            string retValue = param[3].Value.ToString();
            if (retValue == "true")
            {
                ltlAlert.Text = "alert('删除成功！');";
            }
            else
            {
                ltlAlert.Text = "alert('此客户物料已出运，不可删除！');";
            }
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败！请刷新后重新操作一次！');";
        }

        Databind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string cpId = gv.DataKeys[index].Values["SID_id"].ToString();
            Response.Redirect("SID_custpartdiscriptionEdit.aspx?id=" + cpId + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = sid.showCustDiscription(txtpart.Text.Trim(), txtcust.Text.Trim(), txtHST.Text.Trim());
        string title = "120^<b>客户物料号</b>~^80^<b>客户号</b>~^110^<b>HST</b>~^360^<b>描述</b>~^";
        this.ExportExcel(title, dt, false);
    }
}