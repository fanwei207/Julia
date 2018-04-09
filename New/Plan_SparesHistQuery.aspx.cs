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

public partial class Plan_SparesHistQuery : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            BindDepartment();
            BindUser();

            BindData();
        }
    }
    protected void BindDepartment()
    {
        if (dropDept.Items.Count > 0) dropDept.Items.Clear();

        string strsql = "Select 0 As departmentID, '--' As Name Union Select departmentID, name From tcpc" + Session["PlantCode"].ToString() + ".dbo.departments where issalary=1 Order By Name ";

        dropDept.DataSource = Budget.getDept(strsql);
        dropDept.DataBind();

        dropFloor.DataSource = Budget.getDept(strsql);
        dropFloor.DataBind();
    }
    protected void BindUser()
    {
        if (dropUser.Items.Count > 0) dropUser.Items.Clear();

        int plant = Convert.ToInt32(Session["PlantCode"].ToString());
        int dept = Convert.ToInt32(dropDept.SelectedValue);
        int org = 1;

        dropUser.DataSource = Budget.getUser(plant, dept, org);
        dropUser.DataBind();

        dropUser.Items.Insert(0, new ListItem("--", "0"));
    }
    protected void BindData()
    {
        //定义参数
        string no1 = txtNo1.Text;
        string no2 = txtNo2.Text;
        string device = txtDevice.Text;
        string floor = dropFloor.SelectedItem.Text;
        string holderdate1 = txtHoderDate1.Text;
        string holderdate2 = txtHoderDate2.Text;
        string holder = dropUser.SelectedValue;
        string createddate1 = txtCreatedDate1.Text;
        string createddate2 = txtCreatedDate2.Text;
        string type = "ALL";//默认全选

        if (dropType.SelectedIndex == 1)
        {
            type = "IN";
        }
        else if (dropType.SelectedIndex == 2)
        {
            type = "OUT";
        }

        gv.DataSource = SpareHelper.SelectSpareHists(no1, no2, device, holderdate1, holderdate2, floor, holder, createddate1, createddate2, type);
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
            SpareHist hist = (SpareHist)e.Row.DataItem;

            e.Row.Cells[3].Text = string.Format("{0:yyyy-MM-dd}", hist.Holder.Date);

            if (hist.Type == SpareType.StockIn)
            {
                e.Row.Cells[9].Text = "入库";
            }
            else
            {
                e.Row.Cells[9].Text = "领用";
            }
        }
    }
    protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUser();
    }
}
