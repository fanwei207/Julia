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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class qc_incomingaccess : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStdDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1"));
            txtEndDate.Text = string.Format("{0:yyyy-MM-dd}",DateTime.Now);

            BindData();
        }
    }

    public DataTable GetIncomingAssess(string vend, string part, string stddate, string enddate, bool overdue)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@vend", vend);
        param[1] = new SqlParameter("@part", part);
        param[2] = new SqlParameter("@stddate", stddate);
        param[3] = new SqlParameter("@enddate", enddate);
        param[4] = new SqlParameter("@overdue", overdue);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_QC_Rep_IncomingAssess", param).Tables[0];
    }
    protected void BindData()
    {
        DataTable dt = GetIncomingAssess(txtVend.Text.Trim(),
                                                     txtPart.Text.Trim(),
                                                     txtStdDate.Text.Trim(),
                                                     txtEndDate.Text.Trim(),
                                                     chkOverdue.Checked);

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.dgLocation.DataSource = dt;
            this.dgLocation.DataBind();
            int columnCount = dgLocation.Rows[0].Cells.Count;
            dgLocation.Rows[0].Cells.Clear();
            dgLocation.Rows[0].Cells.Add(new TableCell());
            dgLocation.Rows[0].Cells[0].ColumnSpan = columnCount;
            dgLocation.Rows[0].Cells[0].Text = "Ã»ÓÐ¼ÇÂ¼";
            dgLocation.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

            btnExport.Enabled = false;
        }
        else
        {
            btnExport.Enabled = true;

            this.dgLocation.DataSource = dt;
            this.dgLocation.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('qc_incomingaccess_export.aspx?vend=" + txtVend.Text.Trim()
                                                    + "&p=" + txtPart.Text.Trim()
                                                    + "&d1=" + txtStdDate.Text.Trim()
                                                    + "&d2=" + txtEndDate.Text.Trim()
                                                    + "&o=" + chkOverdue.Checked.ToString() + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

    }
    protected void dgLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgLocation.PageIndex = e.NewPageIndex;

        BindData();
    }
}
