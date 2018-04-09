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

public partial class Supplier_supplier_newApplyList : BasePage
{
    
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BingSupplierList();
            BindGridView();
            BindddlApplyDept();
        }
    }

    protected override void BindGridView()
    {
        BingSupplierList();
    }



    private void BingSupplierList()
    {
        DataTable dt = SelectSupplierList();

        FQgvList.DataSource = dt;
        FQgvList.DataBind();
    }
    private DataTable SelectSupplierList()
    {
        string str = "sp_supplier_SelectNewSupplierList";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@SupplierNo", txtSupplierNo.Text);
        param[1] = new SqlParameter("@DeptID", ddlApplyDept.SelectedValue);
        param[2] = new SqlParameter("@Status", ddlStatus.SelectedValue);
        param[3] = new SqlParameter("@ProceStatus", ddlProceStatus.SelectedValue);
        param[4] = new SqlParameter("@Supplier", txtSupplier.Text);
        param[5] = new SqlParameter("@SupplierName", txtSupplierName.Text);
        
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BingSupplierList();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.Redirect("supplier_newApply1.aspx?type=add");
    }
    protected void FQgvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            this.Redirect("supplier_newApply.aspx?type=edit&no=" + FQgvList.DataKeys[intRow].Values["supplier_No"].ToString().Trim());
            //string strPath = FQgv.DataKeys[intRow].Values["supplier_FilePath"].ToString().Trim();
            //string fileName = FQgv.DataKeys[intRow].Values["supplier_FileName"].ToString();
            //ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _no = FQgvList.DataKeys[index].Values["supplier_No"].ToString();
            string _leaderIsAgree = FQgvList.DataKeys[index].Values["supplier_LeaderIsAgree"].ToString();
            //有签名则不能编辑
            if (_leaderIsAgree != "0")
            {
                //ltlAlert.Text = "alert('主管已签字不能修改！');";
                //return;
                this.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }
            else
            {
                this.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }
        }
    }
    private void BindddlApplyDept()
    {
        DataTable dt = selectApplyDept();
        ddlApplyDept.Items.Clear();
        ddlApplyDept.DataSource = dt;
        ddlApplyDept.DataBind();
        ddlApplyDept.Items.Insert(0, new ListItem("--申请部门--", "0"));
    }
    private DataTable selectApplyDept()
    {
        string str = "select distinct supplier_AppDeptID,supplier_AppDeptName  from Supplier_mstr";
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, str).Tables[0];
    }
    protected void ddlProceStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BingSupplierList();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BingSupplierList();
    }
    protected void ddlApplyDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BingSupplierList();
    }
    protected void FQgvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FQgvList.PageIndex = e.NewPageIndex;
        BingSupplierList();
    }
}