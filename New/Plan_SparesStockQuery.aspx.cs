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
using BudgetProcess;

public partial class Plan_SparesStockQuery : BasePage
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
        string no = txtNo.Text;
        string desc = txtDesc.Text;
        string device = txtDevice.Text;
        string floor = dropDept.SelectedItem.Text;

        gv.DataSource = SpareHelper.SelectSpareStocks(no, desc, device, floor);
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
             
        }
    }
}
