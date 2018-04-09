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

public partial class WF_NodeSort : BasePage
{
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            databind();
        }
    }

    protected void databind()
    {
        DataTable dt = wf.GetNodeSort();
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvSort.DataSource = dt;
            this.gvSort.DataBind();
            int ColunmCount = gvSort.Rows[0].Cells.Count;
            gvSort.Rows[0].Cells.Clear();
            gvSort.Rows[0].Cells.Add(new TableCell());
            gvSort.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvSort.Rows[0].Cells[0].Text = "没有数据";
            gvSort.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            this.gvSort.DataSource = dt;
            this.gvSort.DataBind();
        }
    }

    protected void gvSort_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
        }
    }

    protected void gvSort_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSort.PageIndex = e.NewPageIndex;
        databind();
    }

    protected void rbChoose_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvSort.Rows)
        {
            RadioButton rbChoose = (RadioButton)row.FindControl("rbChoose");
            rbChoose.Checked = false;
        }

        (sender as RadioButton).Checked = true;
        foreach (GridViewRow row in gvSort.Rows)
        {
            RadioButton rbChoose = (RadioButton)row.FindControl("rbChoose");
            if (rbChoose.Checked)
            {
                txtChooseValue.Text = row.Cells[1].Text.Trim() + ";" + row.Cells[2].Text.Trim();
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }
}
