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
    public partial class budget_bg_sub : BasePage
    {
        adamClass chk = new adamClass();
        Budget budget = new Budget();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewNullData.GridViewDataBind(gvBudget, budget.GetBudget(string.Empty));
            }
            else
            {
                GridViewNullData.ResetGridView(gvBudget);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {


            GridViewNullData.GridViewDataBind(gvBudget, budget.GetBudget(SearchConditions));
        }
        private string SearchConditions
        {
            get
            {
                string str = " where 1=1 ";

                if (txtCode.Text.Trim() != string.Empty)
                {
                    str += "and sub_code like N'%" + txtCode.Text.Trim() + "%'";
                }

                if (txtDesc.Text.Trim() != string.Empty)
                {
                    str += "and sub_desc like N'%" + txtDesc.Text.Trim() + "%'";
                }

                if (txtPrty.Text.Trim() != string.Empty)
                {
                    str += "and sub_property like N'%" + txtPrty.Text.Trim() + "%'";
                }

                return str;
            }
        }
        protected void gvBudget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBudget.EditIndex = -1;

            GridViewNullData.GridViewDataBind(gvBudget, budget.GetBudget(SearchConditions));
        }
        protected void gvBudget_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBudget.EditIndex = e.NewEditIndex;

            GridViewNullData.GridViewDataBind(gvBudget, budget.GetBudget(SearchConditions));
        }
        protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //order
                if (e.Row.RowIndex != -1)
                {
                    int id = e.Row.RowIndex + 1;
                    e.Row.Cells[0].Text = id.ToString();
                }
            }
        }
        protected void gvBudget_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gvBudget.DataKeys[e.RowIndex].Value.ToString();

            budget.DeleteBudget(ID);

            GridViewNullData.GridViewDataBind(gvBudget, budget.GetBudget(string.Empty));
        }
        protected void gvBudget_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = gvBudget.DataKeys[e.RowIndex].Value.ToString();
            string code = ((TextBox)gvBudget.Rows[e.RowIndex].Cells[1].FindControl("txtCode")).Text.ToString().Trim();
            string desc = ((TextBox)gvBudget.Rows[e.RowIndex].Cells[2].FindControl("txtDesc")).Text.ToString().Trim();
            string prpt = ((TextBox)gvBudget.Rows[e.RowIndex].Cells[3].FindControl("txtPrpt")).Text.ToString().Trim();

            budget.ModifyBudget(ID, code, desc, prpt);

            gvBudget.EditIndex = -1;

            GridViewNullData.GridViewDataBind(gvBudget, budget.GetBudget(string.Empty));
        }
        protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBudget.PageIndex = e.NewPageIndex;
            GridViewNullData.GridViewDataBind(gvBudget, budget.GetBudget(string.Empty));
        }
    }
}