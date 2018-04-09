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
using QADSID;

public partial class SID_QA : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strSNO = txtSNo.Text.Trim();

        gvSID.DataSource = sid.SelectNeedQASNO(strSNO);
        gvSID.DataBind();
    }

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSID_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        string strSNO = txtSNo.Text.Trim();

        if (sid.InsertNeedQASNO(strSNO.Trim()))
        {
            Response.Redirect("/SID/SID_QA.aspx?rm=" + DateTime.Now.ToString());
        }
        else
        {
            ltlAlert.Text = "alert('新增数据过程中出错！');";
        }
    }

    protected void gvSID_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSNO = gvSID.DataKeys[e.RowIndex].Value.ToString().Trim();

        if (sid.DeleteNeedQASNO(strSNO))
        {
            Response.Redirect("/SID/SID_QA.aspx?rm=" + DateTime.Now.ToString());
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程中出错！');";
        }
        BindData();
    }
}
