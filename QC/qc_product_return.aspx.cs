using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using QCProgress;

public partial class QC_qc_product_return : BasePage
{
    QC qc = new QC();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("100103293", "返工记录审批文档上传权限");
            this.Security.Register("100103295", "返工记录质检报告上传及审批权限");
            this.Security.Register("100103298", "返工记录删除权限");
        }

        base.OnInit(e);
    }
    ////空间.Visible = this.Security["121000030"].isValid;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
            btnNew.Enabled = this.Security["100103293"].isValid;
        }
    }

    private void bind()
    {
        gvUpload.DataSource = qc.selectProductReworkStreamline(Request["prdID"].ToString());
        gvUpload.DataBind();
    }
    protected void gvUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUpload.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值

        if (e.CommandName == "lkbtnNo" )
        {
            string qprsID = gvUpload.DataKeys[rowindex].Values["qprs_ID"].ToString();

            if (this.Security["100103293"].isValid)
            {
                Response.Redirect("/QC/qc_productReworkStreamlineFileUpload.aspx?prdID=" + Request["prdID"].ToString() + "&qprs_ID=" + qprsID +"&url="+e.CommandArgument.ToString());
            }
            else
            {
                ltlAlert.Text = "alert('你没有该操作权限');";
                return;
            }
        }
        else if (e.CommandName == "lkbtnFile")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        else if (e.CommandName == "lkbtnReport")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        else if (e.CommandName == "lkbtnResult")
        {
            string qprsID = gvUpload.DataKeys[rowindex].Values["qprs_ID"].ToString();

            if (this.Security["100103295"].isValid)
            {
                Response.Redirect("/QC/qc_productReworkStreamlineQC.aspx?prdID=" + Request["prdID"].ToString() + "&qprs_ID=" + qprsID + "&url=" + e.CommandArgument.ToString());
            }
            else
            {
                ltlAlert.Text = "alert('你没有该操作权限');";
                return;
            }
        }
        else if (e.CommandName == "lkbtnDelete")
        {
            string qprsID = gvUpload.DataKeys[rowindex].Values["qprs_ID"].ToString();

            if (this.Security["100103298"].isValid)
            {
                if (qc.deleteReworkStream(qprsID, Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('删除成功！');";
                    bind();
                }
                else
                {
                    ltlAlert.Text = "alert('删除失败！请联系管理员！');";
                    bind();
                }
            }
            else
            {
                ltlAlert.Text = "alert('你没有该操作权限');";
                return;
            }

        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("/QC/qc_productReworkStreamlineFileUpload.aspx?prdID=" + Request["prdID"].ToString());
    }
}