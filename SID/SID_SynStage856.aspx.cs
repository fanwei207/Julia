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

public partial class SID_SynStage856 : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtShipDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strInvNo = txtInvNo.Text.Trim();
        string strShipNo = txtShipNo.Text.Trim();
        string strShipDate = txtShipDate.Text.Trim();
        bool isChkAll = chkAll.Checked;

        gvSynStageList.DataSource = sid.SelectSynStageList(strInvNo, strShipNo, strShipDate, isChkAll);
        gvSynStageList.DataBind();
    }

    protected void gvSynStageList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSynStageList.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSynStageList_PreRender(object sender, EventArgs e)
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

    protected void gvSynStageList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SID_SynStageASN stage = (SID_SynStageASN)e.Row.DataItem;

            if (stage.Container.Length == 0)
            {
                e.Row.Cells[4].Text = "";
                e.Row.Cells[5].Text = "";

            }
            else
            {
                if (sid.JudgeSynStage856(stage.InvoiceNo.Trim()))
                {
                    ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick", "return confirm('确定要同步ASN吗?')");
                }
                else
                {
                    e.Row.Cells[4].Text = "";
                }
            }
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvSynStageList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strInvNo = string.Empty;
        string strShipDate = string.Empty;
        string strZhouQiZhang = string.Empty;

        if (e.CommandName.ToString() == "SynStage")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strInvNo = gvSynStageList.Rows[intRow].Cells[0].Text.ToString().Trim();
            strShipDate = gvSynStageList.Rows[intRow].Cells[2].Text.ToString().Trim();
            strZhouQiZhang = gvSynStageList.Rows[intRow].Cells[3].Text.ToString().Trim();
            //ltlAlert.Text = "alert('" + strInvNo + "');";
            if (sid.SynStage(strInvNo, strShipDate))
            {
                ltlAlert.Text = "alert('同步成功!'); window.location.href='/SID/SID_SynStage856.aspx?rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                ltlAlert.Text = "alert('同步失败!'); window.location.href='/SID/SID_SynStage856.aspx?rm=" + DateTime.Now.ToString() + "';";
            }
        }

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strInvNo = gvSynStageList.Rows[intRow].Cells[0].Text.ToString().Trim();
           // BindData();
          Response.Redirect("SID_SynStage856Detail.aspx?InvNo=" + strInvNo + "&rm=" + DateTime.Now.ToString());
        }
    }
}
