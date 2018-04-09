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
using BudgetProcess;

public partial class Plan_SparesItem : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            BindDepartment();

            BindData();
        }
    }
    protected void BindDepartment()
    {
        if (dropDept.Items.Count > 0) dropDept.Items.Clear();

        string strsql = "Select 0 As departmentID, '--' As Name Union Select departmentID, name From tcpc" + Session["PlantCode"].ToString() + ".dbo.departments where issalary=1 Order By Name ";

        dropDept.DataSource = Budget.getDept(strsql);
        dropDept.DataBind();
    }
    protected void BindData()
    {
        //定义参数
        gv.DataSource = SpareHelper.SelectSpareItems(txtNo.Text, txtDesc.Text, txtDevice.Text, dropDept.SelectedItem.Text);

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

    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                TextBox txDesc = (TextBox)e.Row.Cells[1].Controls[0];
                txDesc.Attributes.Add("class", "SmallTextBox4");
                txDesc.Style.Add("width", "100%");

                TextBox txDevice = (TextBox)e.Row.Cells[2].Controls[0];
                txDevice.Attributes.Add("class", "SmallTextBox4");
                txDevice.Style.Add("width", "100%");

                DropDownList dDept = (DropDownList)e.Row.FindControl("dDept");

                if (dDept.Items.Count > 0) dDept.Items.Clear();

                string strsql = "Select 0 As departmentID, '--' As Name Union Select departmentID, name From tcpc" + Session["PlantCode"].ToString() + ".dbo.departments where issalary=1 Order By Name ";

                dDept.DataSource = Budget.getDept(strsql);
                dDept.DataBind();

                try
                {
                    dDept.SelectedIndex = -1;
                    dDept.Items.FindByText(gv.DataKeys[e.Row.RowIndex].Values["Floor"].ToString()).Selected = true;
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
        SpareItem item = new SpareItem();
        item.No = txtNo.Text;
        item.Description = txtDesc.Text;
        item.Device = txtDevice.Text;
        item.Floor = dropDept.SelectedItem.Text;
        item.Creator = new User();
        item.Creator.ID = Convert.ToInt32(Session["uID"].ToString());
        item.Creator.Date = DateTime.Now;

        if (txtNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('备件编号不能为空！');";
            return;
        }
        else
        {
            if (item.IsExist)
            {
                ltlAlert.Text = "alert('备件编号已经存在，请重新维护一个编号！');";
                return;
            }
        }

        if (item.Insert)
        {
            ltlAlert.Text = "alert('增加成功！');";

            txtNo.Text = string.Empty;
            txtDesc.Text = string.Empty;
        }
        else
        {
            ltlAlert.Text = "alert('增加失败，请刷新后重新操作！');";
        }

        BindData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SpareItem item = new SpareItem();
        item.ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString());

        if (item.Delete)
        {
            ltlAlert.Text = "alert('删除成功！');";
        }
        else
        {
            ltlAlert.Text = "alert('删除失败或该编号已经被使用！');";
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
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].Cells[1].Controls[0];
        TextBox txDevice = (TextBox)gv.Rows[e.RowIndex].Cells[2].Controls[0];
        DropDownList dDept = (DropDownList)gv.Rows[e.RowIndex].FindControl("dDept");

        SpareItem item = new SpareItem();
        item.ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString());
        item.Description = txDesc.Text;
        item.Device = txDevice.Text;
        item.Floor = dDept.SelectedItem.Text;

        item.Modifier = new User();
        item.Modifier.ID = Convert.ToInt32(Session["uID"].ToString());
        item.Modifier.Date = DateTime.Now;

        if (item.Update)
        {
            ltlAlert.Text = "alert('更新成功！');";
            gv.EditIndex = -1;
        }
        else
        {
            ltlAlert.Text = "alert('更新失败，请刷新后重新操作！');";
        }

        BindData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        BindData();
    }
}
