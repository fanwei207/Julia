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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class pur_CustPartTypeAdd : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    PurResult result = new PurResult();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }
    private void GridViewBind() 
    {
        string CustCode = txt_CustCode.Text;
        try
        {
            gv.DataSource = result.GetCustPartTypeList(CustCode);
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string CustCode = gv.DataKeys[e.RowIndex].Values["pur_CustCode"].ToString();
        if (result.DeleteCustPartType(CustCode, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("删除成功！");
        }
        else
        {
            this.Alert("删除失败！");
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string CustCode = gv.DataKeys[e.RowIndex].Values["pur_CustCode"].ToString();
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txt_PartType");
        //String strTcpPo = ((TextBox)gv.Rows[e.RowIndex].Cells[9].FindControl("txtpo")).Text.ToString().Trim();
        int i = result.UpdateCustPartType(CustCode, txDesc.Text, Session["uID"].ToString(), Session["uName"].ToString());

        if (i == 1 || i == 2)
        {
            this.Alert("历史记录处理失败，请联系管理员!");
            return;
        }
        gv.EditIndex = -1;
        GridViewBind();
    }
    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {
            //if (gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            //{
            //    ltlAlert.Text = "alert('此行是空行！');";
            //    return;
            //}
            //string versionid = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionId"].ToString();
            //string versionname = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionName"].ToString();
            //string flag = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionFlag"].ToString();
            //Response.Redirect("pur_ResultCheckStandard.aspx?id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&rm=" + DateTime.Now);
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_CustCode.Text))
        {
            this.Alert("供应商代码不能为空!");
            return;
        }
        if (string.IsNullOrEmpty(txt_PartType.Text))
        {
            this.Alert("供货类别不能为空!");
            return;
        }
        string CustCode = txt_CustCode.Text;
        string PartType = txt_PartType.Text;
        int i = result.SaveCustPartType(CustCode, PartType, Session["uID"].ToString(), Session["uName"].ToString());

        if (i == 1 || i == 2)
        {
            this.Alert("历史记录处理失败，请联系管理员!");
            return;
        }
        if (i == 3)
        {
            this.Alert("记录保存失败!");
            return;
        }
        this.Alert("记录保存成功!");
        GridViewBind();

    }
}
