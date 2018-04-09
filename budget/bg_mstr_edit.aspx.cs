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
using BudgetProcess;

public partial class budget_bg_mstr_edit : BasePage
{
    adamClass chk = new adamClass();
    Budget budget = new Budget();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            txtMstr.Text = Server.UrlDecode(Request.QueryString["master"].ToString());
            txtDep.Text = Server.UrlDecode(Request.QueryString["dept"].ToString());
            txtAcc.Text = Server.UrlDecode(Request.QueryString["acc"].ToString());
            txtSub.Text = Server.UrlDecode(Request.QueryString["sub"].ToString());
            txtDes.Text = Server.UrlDecode(Request.QueryString["desc"].ToString());

            if (Request.QueryString["project"] == null)
                txtPro.Text = string.Empty;
            else
                txtPro.Text = Server.UrlDecode(Request.QueryString["project"].ToString());

            txtCC.Text = Server.UrlDecode(Request.QueryString["cc"].ToString());
            txtYear.Text = Server.UrlDecode(Request.QueryString["year"].ToString());

            gvBudget.DataSource = budget.GetMstrEdit(txtMstr.Text, txtDep.Text, txtAcc.Text, txtSub.Text, txtPro.Text, txtYear.Text);
            gvBudget.DataBind();

            this.Response.Expires = -1;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string master = txtMstr.Text;
        string dept = txtDep.Text;
        string acc = txtAcc.Text;
        string sub = txtSub.Text;
        string project = txtPro.Text;
        string cc = txtCC.Text;
        string year = txtYear.Text;

        for (int i = 0; i < gvBudget.Rows.Count; i++) 
        {
            string month = gvBudget.Rows[i].Cells[0].Text.Trim().Substring(4, 2);
            string bg = ((TextBox)gvBudget.Rows[i].Cells[2].FindControl("txtBudget")).Text.Trim();
            int msg = 0;

            if (bg != string.Empty) 
            {
                try
                {
                    Convert.ToDecimal(bg);
                }
                catch 
                {
                    ltlAlert.Text = "alert('期间" + year + month.ToString() + "预测数据格式不对!');";
                    return;
                }
            }

            budget.ModifyMstr(bg, master, dept, acc, sub, project, cc, year, month, ref msg);
            if (msg == 1) 
            {
                ltlAlert.Text = "alert('期间" + year + month .ToString()+ "已关闭!');";
                return;
            }
        }

        ltlAlert.Text = "alert('保存成功!'); window.close(); window.opener.location='/budget/bg_mstr.aspx?rm=" + DateTime.Now + "';";

    }
    protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string year = e.Row.Cells[0].Text.Trim();//(Label)e.Row.Cells[0].FindControl("lblYear");
            TextBox budget = (TextBox)e.Row.Cells[2].FindControl("txtBudget");

            if (Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString()+"-01") > Convert.ToDateTime(year.Insert(4, "-") + "-01"))
            {
                budget.ReadOnly = true;
            }

            Decimal bg_ecurr = Convert.ToDecimal(gvBudget.DataKeys[e.Row.RowIndex].Values[0].ToString());
            Decimal bg_budget = Convert.ToDecimal(gvBudget.DataKeys[e.Row.RowIndex].Values[1].ToString());

            e.Row.Cells[3].Text = Convert.ToString(bg_ecurr - bg_budget);

            if (bg_budget == 0)
                budget.Text = string.Empty;
        }
    }
}
