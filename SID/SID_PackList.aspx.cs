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

public partial class SID_PackList : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtShipDateFrom.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
            txtShipDateTo.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strInv = txtInvNo.Text.Trim();
        string strDest = txtDest.Text.Trim();
        string strDateFrom = txtShipDateFrom.Text.Trim();
        string strDateTo = txtShipDateTo.Text.Trim();

        gvPackList.DataSource = sid.SelectPackList(strInv, strDest, strDateFrom, strDateTo);
        gvPackList.DataBind();
    }

    protected void gvPackList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPackList.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPackList_PreRender(object sender, EventArgs e)
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

    protected void gvPackList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

            SID_Stage856 stage = (SID_Stage856)e.Row.DataItem;

            if (stage.isFinish == true)
            {
                btnDelete.Enabled = false;
                btnDelete.Text = "";
            }
            else
            {
                btnDelete.Attributes.Add("onclick", "return confirm('确定要删除" + stage.MasterInvoice.ToString().Trim() + "吗?');");
            }
        }
    }

    protected void gvPackList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strInvNo = gvPackList.Rows[e.RowIndex].Cells[0].Text.Trim();
        string strShipDate = gvPackList.Rows[e.RowIndex].Cells[2].Text.Trim();

        if (sid.DeleteStage856(strInvNo, strShipDate))
        {
            // ltlAlert.Text = "alert('删除成功!');window.location.href='/SID/SID_PackList.aspx?rm=" + DateTime.Now.ToString() + "';";
            Response.Redirect("/SID/SID_PackList.aspx?rm=" + DateTime.Now.ToString());
        }
        else
        {
            //ltlAlert.Text = "alert('删除失败!');window.location.href='/SID/SID_PackList.aspx?rm=" + DateTime.Now.ToString() + "';";
            ltlAlert.Text = "alert('删除失败!');";
        }
    }

    protected void gvPackList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            string MasterInvoice = e.CommandArgument.ToString().Split(',')[0];
            string ShipDate = e.CommandArgument.ToString().Split(',')[1];
            Response.Redirect("SID_PackListDetail.aspx?mi=" + MasterInvoice + "&shipdate=" + ShipDate + "&rm=" + DateTime.Now.ToString());
        }
    }
}
