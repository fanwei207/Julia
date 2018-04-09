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
using WOrder;
using adamFuncs;
using Wage;

public partial class wo2_Wo2_UsersStatistic : BasePage
{
    adamClass adam = new adamClass();
    WorkOrder wd = new WorkOrder();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStart.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            txtEnd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            dropDeptBind();
            gvUserBind();
        }
    }

    private void dropDeptBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
    }

    private void gvUserBind()
    {
        try
        {
            DataTable dtUser = wd.WoAtCompareSelect(txtStart.Text.Trim(), Convert.ToDateTime(txtEnd.Text.Trim()).AddDays(1).ToShortDateString(), chkClose.Checked, Convert.ToInt32(dropDept.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue),
                                                    Convert.ToInt32(Session["uRole"]), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]));
            gvUsers.DataSource = dtUser;
            gvUsers.DataBind();
            dtUser.Clear();
        }
        catch
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtStart.Text.Trim()) > Convert.ToDateTime(txtEnd.Text.Trim()))
        {
            ltlAlert.Text = "alert('开始日期不能晚于结束日期!');";
        }
        if (this.Security["600030005"].isValid == false)
        {
            if (DateTime.Compare(Convert.ToDateTime(txtStart.Text),Convert.ToDateTime(txtEnd.Text)) != 0)
            {
                ltlAlert.Text = "alert('只能查询一天的数据!');";
                return;
            }
        }
        gvUsers.PageIndex = 0;
        gvUserBind();
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        gvUserBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
                , wd.WoAtCompare(txtStart.Text.Trim(), Convert.ToDateTime(txtEnd.Text.Trim()).AddDays(1).ToShortDateString(), chkClose.Checked, Convert.ToInt32(dropDept.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue),
                                                    Convert.ToInt32(Session["uRole"]), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), 1)
                , wd.WoAtCompare(txtStart.Text.Trim(), Convert.ToDateTime(txtEnd.Text.Trim()).AddDays(1).ToShortDateString(), chkClose.Checked, Convert.ToInt32(dropDept.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue),
                                                    Convert.ToInt32(Session["uRole"]), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), 0)
                , false);
    }
}
