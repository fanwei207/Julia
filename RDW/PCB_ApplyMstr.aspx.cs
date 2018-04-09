using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RDW_PCB_ApplyMstr : BasePage
{
    RDW_PCB helper = new RDW_PCB();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("350220", "PCB新增申请单权限");
            this.Security.Register("350230", "PCB打样单权限");
            this.Security.Register("170008", "注册查看所有项目的权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["ProjectCode"] != null)
            {
                txtProjectNo.Enabled = false;
                txtProjectNo.Text = Request["ProjectCode"].ToString();
                btnReturn.Visible = true;
            }
            btnAdd.Visible = this.Security["350220"].isValid;
            if (this.Security["350220"].isValid)
            {
                ddlStatus.SelectedValue = "已提交";
            }
            this.Bind();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        this.Bind();
    }

    private void Bind()
    {
        gvApplyList.DataSource = helper.selectApplyMstr(txtProjectNo.Text, txtProductName.Text, ddlStatus.SelectedValue.ToString(), txtPCBNo.Text.ToString(), this.Security["170008"].isValid, Session["uID"].ToString(), Session["PlantCode"].ToString(), this.Security["350220"].isValid,ddlDomain.SelectedValue);
        gvApplyList.DataBind();
    }
    protected void gvApplyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApplyList.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void gvApplyList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string PCB_ID = e.CommandArgument.ToString();

        if (e.CommandName == "lkbtnDelete")
        {
            if (helper.deleteApplyMstrByID(PCB_ID, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('删除成功！');";
                Bind();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！');";
            }
        }
        if (e.CommandName == "lkbtnList")
        {
            if (Request["ProjectCode"] == null)
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?PCB_ID=" + PCB_ID);
            }
            else
            {
                Response.Redirect("/RDW/PCB_ApplyDet.aspx?ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
                + Request["mid"].ToString() 
                //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
                //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
                //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
                + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
                + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
                + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
                + "&rm=" + Request["rm"].ToString() + "&PCB_ID=" + PCB_ID, true);
            }
        }
    }
    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             string status = gvApplyList.DataKeys[e.Row.RowIndex].Values["PCB_LAYOUTStatus"].ToString();

             if ("已提交".Equals(status) || "已生成打样单".Equals(status) || "已删除".Equals(status))
             {
                 ((LinkButton)e.Row.FindControl("lkbtnDelete")).Text = "";
             
             }
             if ("已删除".Equals(status))
             {
                 e.Row.Cells[3].Text = "已关闭";
             
             }
         
         }
    }
    protected void benAdd_Click(object sender, EventArgs e)
    {
        if (Request["mid"] == null)
        {
            Response.Redirect("/RDW/PCB_ApplyDet.aspx");

        }
        else
        {
            Response.Redirect("/RDW/PCB_ApplyDet.aspx?ProjectCode=" + Request["ProjectCode"].ToString()+"&mid="
            + Request["mid"].ToString() 
            //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
            //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
            //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
            + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
            + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
            + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
            + "&rm=" + Request["rm"].ToString(),true);
        }

        
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_DetailList.aspx?mid="
            + Request["mid"].ToString() 
            //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
            //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
            //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
            + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
            + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
            + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
            + "&rm=" + Request["rm"].ToString(),true);
    }

  
}