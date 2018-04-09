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
using WO2Group;

public partial class wo2_GroupView : BasePage
{
    adamClass chk = new adamClass();
    WO2 wo2 = new WO2();
    int nRet = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            if (Request.QueryString["code"] == null || Request.QueryString["name"] == null || Request.QueryString["code"] == "" || Request.QueryString["name"] == "")
            {
                Response.Redirect("/wo2/wo2_group.aspx?rm=" + DateTime.Now.ToString(), true);
            }

            lblInfo.Text = "用户组代码:" + Request.QueryString["code"] + ", 用户组名称:" + Server.UrlDecode(Request.QueryString["name"]);
            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        int intGroupID = Convert.ToInt32(Request.QueryString["id"]);

        gvDetail.DataSource = wo2.SelectGroupDetailList(intGroupID);
        gvDetail.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetail_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetail.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("/wo2/wo2_group.aspx?rm=" + DateTime.Now.ToString());
    }

    protected void gvDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (wo2.DeleteGroupDetail(Convert.ToInt32(gvDetail.DataKeys[e.RowIndex].Value.ToString())))
        {
            ltlAlert.Text = "alert('删除用户组明细数据成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "'";
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程中出错！');";
        }
    }

    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

            if (!this.Security["600000005"].isValid)
            {
                btnDelete.Attributes.Add("onclick", "return confirm('你确认要删除吗?')");
            }
            else
            {
                btnDelete.Enabled = false;
                btnDelete.Text = "";
            }
        }
    }
}
