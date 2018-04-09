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
using BudgetProcess;
using adamFuncs;

namespace BudgetProcess
{
    public partial class budget_bg_calendar : BasePage 
    {
        adamClass chk = new adamClass();
        Budget budget = new Budget();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                dropDomain.DataSource = budget.GetDomain();
                dropDomain.DataBind();
                dropDomain.Items.Insert(0, new ListItem("--","--"));

                GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(string.Empty));
            }
            else
            {
                GridViewNullData.ResetGridView(gvCalendar);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(SearchConditions));
        }
        private string SearchConditions
        {
            get
            {
                string str = " where 1=1 ";

                if (dropDomain.SelectedIndex != 0)
                {
                    str += "and ca_domain = N'" + dropDomain.SelectedValue + "'";
                }

                if (txtDate.Text.Trim() != string.Empty)
                {
                    str += "and ca_date like N'%" + txtDate.Text.Trim() + "%'";
                }

                if (dropClose.SelectedIndex != 0)
                {
                    str += "and ca_close = N'" + dropClose.SelectedValue + "'";
                }

                return str;
            }
        }
        protected void gvCalendar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCalendar.EditIndex = -1;

            GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(SearchConditions));
        }
        protected void gvCalendar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCalendar.EditIndex = e.NewEditIndex;

            GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(""));

            string domain = ((Label)gvCalendar.Rows[e.NewEditIndex].Cells[1].FindControl("lblDomain")).Text.Trim();
            string close = ((Label)gvCalendar.Rows[e.NewEditIndex].Cells[1].FindControl("lblClose")).Text.Trim();

            DropDownList dDomain = (DropDownList)gvCalendar.Rows[e.NewEditIndex].Cells[1].FindControl("dropDomain");
            DropDownList dClose = (DropDownList)gvCalendar.Rows[e.NewEditIndex].Cells[1].FindControl("dropClose");

            dDomain.Items.FindByText(domain).Selected = true;
            dClose.Items.FindByText(close).Selected = true;
        }
        protected void gvCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            { 
                if (e.Row.RowIndex != -1)
                {
                    int id = e.Row.RowIndex + 1;
                    e.Row.Cells[0].Text = id.ToString();
                }
            }
        }
        protected void gvCalendar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gvCalendar.DataKeys[e.RowIndex].Value.ToString();

            budget.DeleteCalendar(ID);

            GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(string.Empty));
        }
        protected void gvCalendar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = gvCalendar.DataKeys[e.RowIndex].Value.ToString();
            DropDownList drpDomain = (DropDownList)gvCalendar.Rows[e.RowIndex].Cells[1].FindControl("dropDomain");
            string date = ((TextBox)gvCalendar.Rows[e.RowIndex].Cells[2].FindControl("txtDate")).Text.ToString().Trim();
            DropDownList drpClose = (DropDownList)gvCalendar.Rows[e.RowIndex].Cells[3].FindControl("dropClose");

            if (drpDomain.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('请选择一项域');";
                return;
            }

            if (drpClose.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('请选择是否关闭');";
                return;
            }

            try
            {
                string strDate = date;
                strDate = strDate.Insert(4, "-");

                Convert.ToDateTime(strDate + "-01");
            }
            catch
            {
                ltlAlert.Text = "alert('期间格式不对');";
                return;
            }

            int msg = 0;

            budget.ModifyCalendar(ID, drpDomain.SelectedValue, date, drpClose.SelectedValue, ref msg);

            if (msg == 1)
            {
                ltlAlert.Text = "alert('期间已经存在');";
                return;
            }

            gvCalendar.EditIndex = -1;

            GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(string.Empty));
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (dropDomain.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('请选择一项域');";
                return;
            }

            if (txtDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('期间不能为空');";
                return;
            }
            else
            {
                try
                {
                    string strDate = txtDate.Text.Trim();
                    strDate = strDate.Insert(4, "-");

                    Convert.ToDateTime(strDate + "-01");
                }
                catch
                {
                    ltlAlert.Text = "alert('期间格式不对');";
                    return;
                }
            }

            if (dropClose.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('请确定该期间是否关闭');";
                return;
            }

            string domain = dropDomain.SelectedItem.Text.Trim();
            string date = txtDate.Text.Trim();
            string close = dropClose.SelectedItem.Text.Trim();
            int msg = 0;

            budget.AddCalendar(domain, date, close, ref msg);

            if (msg == 1) 
            {
                ltlAlert.Text = "alert('期间已经存在');";
                return;
            }

            GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(string.Empty));
        }
        protected void gvCalendar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCalendar.PageIndex = e.NewPageIndex;
            GridViewNullData.GridViewDataBind(gvCalendar, budget.GetCalendar(string.Empty));
        }
}
}
