using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IT;

public partial class TSK_GanntMstr : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int _n = DateTime.Now.Year;
            while (_n >= 2014)
            {
                dropYear.Items.Insert(0, new ListItem(_n.ToString(), _n.ToString()));

                _n--;
            }

            dropYear.SelectedValue = DateTime.Now.Year.ToString();

            _n = 12;
            while (_n > 0)
            {
                dropMonth.Items.Insert(0, new ListItem(_n.ToString(), _n.ToString()));

                _n--;
            }

            dropMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;

            BindData();
            BindUser();
            try
            {
                dropUsers.SelectedIndex = -1;
                dropUsers.Items.FindByValue(Session["uID"].ToString()).Selected = true;
            }
            catch { }
        }
    }

    protected void BindData()
    {
        DataTable table = TaskHelper.SelectTaskGanntMstr(dropYear.SelectedValue, dropMonth.SelectedValue, dropType.SelectedValue, dropUsers.SelectedValue);

        if (table == null)
        {
            this.Alert("无法获取未结任务！");
        }
        else
        {
            divGannt.InnerHtml = TaskHelper.GenerateGannt(Convert.ToInt32(dropYear.SelectedValue), Convert.ToInt32(dropMonth.SelectedValue), 4, table);
        }
    }
    protected void BindUser()
    {
        DataTable table = TaskHelper.GetUsers(string.Empty, 404);
        dropUsers.Items.Clear();

        dropUsers.DataSource = table;
        dropUsers.DataBind();
        dropUsers.Items.Insert(0, new ListItem("--全部--", "0"));
    }
    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void LinkShowAllTasks_Click(object sender, EventArgs e)
    {
        Response.Redirect("TSK_ChargerTotal.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void dropUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}