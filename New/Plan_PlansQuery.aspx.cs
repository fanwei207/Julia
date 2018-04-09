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
using System.IO;
using adamFuncs;
using Portal.Fixas;
using TCPNEW;

public partial class Plan_PlansQuery : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            dropEntity.DataSource = GetDataTcp.GetEntityFixAsset();
            dropEntity.DataBind();
            dropEntity.Items.Insert(0, new ListItem("--", "--"));

            dropType.DataSource = GetDataTcp.GetTypeFixAsset();
            dropType.DataBind();
            dropType.Items.Insert(0, new ListItem("--", "--"));

            dropRepairItem.DataSource = RepairItemHelper.SelectAllRepairItems();
            dropRepairItem.DataBind();
            dropRepairItem.Items.Insert(0, new ListItem("--", "--"));

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string fixasNo = txtFixasNo.Text;
        string fixasName = txtFixasName.Text;
        string fixasDesc = txtFixasDesc.Text;
        string fixasType = dropType.SelectedItem.Text;
        string fixasEntity = dropEntity.SelectedItem.Text;
        string fixasVouDate = txtFixasVouDate.Text;
        string fixasSupplier = txtFixasSupplier.Text;
        string planDate = txtPlanDate.Text; 

        gv.DataSource = PlanHelper.SelectPlans(fixasNo, fixasName, fixasDesc, fixasType, fixasEntity, fixasVouDate, fixasSupplier, planDate);
        gv.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                TextBox txPlanDate = (TextBox)e.Row.Cells[7].Controls[0];
                txPlanDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txPlanDate.Text));
                txPlanDate.Attributes.Add("class", "SmallTextBox4");
                txPlanDate.Style.Add("width", "100%");
            }
        }
    }
}
