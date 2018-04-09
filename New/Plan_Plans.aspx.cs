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

public partial class Plan_Plans : BasePage
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

                DropDownList dRepairItem = (DropDownList)e.Row.FindControl("dRepairItem");
                dRepairItem.DataSource = RepairItemHelper.SelectAllRepairItems();
                dRepairItem.DataBind();
                dRepairItem.Items.Insert(0, new ListItem("--", "--"));

                System.Web.UI.HtmlControls.HtmlInputHidden hRepairItem = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hRepairItem");

                try
                {
                    dRepairItem.SelectedIndex = -1;
                    dRepairItem.Items.FindByValue(hRepairItem.Value).Selected = true;
                }
                catch
                {
                    ;
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Plan_AddPlan.aspx?rm=" + DateTime.Now.ToString());
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Plan plan = new Plan();
        plan.ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString());

        if (plan.Delete)
        {
            ltlAlert.Text = "alert('计划删除成功！');";
        }
        else
        {
            ltlAlert.Text = "alert('计划删除失败，请刷新后重新操作！');";
        }

        BindData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        BindData();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txPlanDate = (TextBox)gv.Rows[e.RowIndex].Cells[7].Controls[0];
        DropDownList dRepairItem = (DropDownList)gv.Rows[e.RowIndex].FindControl("dRepairItem");

        if (txPlanDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('计划日期不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _time = Convert.ToDateTime(txPlanDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('计划日期格式不正确！正确格式为yyyy-MM-dd！');";
                return;
            }
        }

        if (dRepairItem.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一个项目！');";
            return;
        }

        Plan plan = new Plan(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString()));
        plan.Date = Convert.ToDateTime(txPlanDate.Text);
        plan.RepairItem = new RepairItem(Convert.ToInt32(dRepairItem.SelectedValue));

        plan.Modifier = new User();
        plan.Modifier.ID = Convert.ToInt32(Session["uID"].ToString());
        plan.Modifier.Date = DateTime.Now;

        if (plan.IsExist)
        {
            ltlAlert.Text = "alert('计划已存在！');";
            return;
        }

        if (plan.Update)
        {
            ltlAlert.Text = "alert('计划更新成功！');";
            gv.EditIndex = -1;
        }
        else
        {
            ltlAlert.Text = "alert('计划更新失败，请刷新后重新操作！');";
        }

        BindData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        BindData();
    }
}
