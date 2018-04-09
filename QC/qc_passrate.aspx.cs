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

public partial class QC_qc_passrate : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            txtDueDate1.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
            txtDueDate2.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            //BindData();
        }
    }

    public DataTable GetIncomingPassRate(string vend, string part, string stddate, string enddate, bool inspected, bool overdue, string ordDate1
            , string ordDate2, string dueDate1, string dueDate2, bool chkFacDate, int poPeriod, string domain)
    {
        SqlParameter[] param = new SqlParameter[13];
        param[0] = new SqlParameter("@vend", vend);
        param[1] = new SqlParameter("@part", part);
        param[2] = new SqlParameter("@stddate", stddate);
        param[3] = new SqlParameter("@enddate", enddate);
        param[4] = new SqlParameter("@inspected", inspected);
        param[5] = new SqlParameter("@overdue", overdue);
        param[6] = new SqlParameter("@ordDate1", ordDate1);
        param[7] = new SqlParameter("@ordDate2", ordDate2);
        param[8] = new SqlParameter("@dueDate1", dueDate1);
        param[9] = new SqlParameter("@dueDate2", dueDate2);
        param[10] = new SqlParameter("@po_period", poPeriod);
        param[11] = new SqlParameter("@chkFacDate", chkFacDate);
        param[12] = new SqlParameter("@domain", domain);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_QC_Rep_IncomingPassRate", param).Tables[0];
    }

    protected bool IsInteger(string val)
    {
        try
        {
            int n = Convert.ToInt32(val);
            if (n >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    protected void BindData()
    {
        DataTable dt = GetIncomingPassRate(txtVend.Text.Trim()
                                         , txtPart.Text.Trim()
                                         , txtStdDate.Text.Trim()
                                         , txtEndDate.Text.Trim()
                                         , chkInspect.Checked
                                         , chkOverdue.Checked
                                         , txtOrdDate1.Text.Trim()
                                         , txtOrdDate2.Text.Trim()
                                         , txtDueDate1.Text.Trim()
                                         , txtDueDate2.Text.Trim()
                                         , chkFacDate.Checked
                                         , IsInteger(txbPoPeriod.Text.Trim()) ? Convert.ToInt32(txbPoPeriod.Text.Trim()) : 0
                                         , dropDomain.SelectedValue);

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.dgLocation.DataSource = dt;
            this.dgLocation.DataBind();
            int columnCount = dgLocation.Rows[0].Cells.Count;
            dgLocation.Rows[0].Cells.Clear();
            dgLocation.Rows[0].Cells.Add(new TableCell());
            dgLocation.Rows[0].Cells[0].ColumnSpan = columnCount;
            dgLocation.Rows[0].Cells[0].Text = "没有记录";
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
        DateTime _d;
        if (txtOrdDate1.Text.Length > 0)
        {
            try
            {
                _d = Convert.ToDateTime(txtOrdDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('订单日期 格式不正确！');";
                return;
            }
        }

        if (txtOrdDate2.Text.Length > 0)
        {
            try
            {
                _d = Convert.ToDateTime(txtOrdDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('订单日期 格式不正确！');";
                return;
            }
        }

        if (txtDueDate1.Text.Length > 0)
        {
            try
            {
                _d = Convert.ToDateTime(txtDueDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('截至日期 格式不正确！');";
                return;
            }
        }

        if (txtDueDate2.Text.Length > 0)
        {
            try
            {
                _d = Convert.ToDateTime(txtDueDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('截至日期 格式不正确！');";
                return;
            }
        }

        if (txtStdDate.Text.Length > 0)
        {
            try
            {
                _d = Convert.ToDateTime(txtStdDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('收货日期 格式不正确！');";
                return;
            }
        }

        if (txtEndDate.Text.Length > 0)
        {
            try
            {
                _d = Convert.ToDateTime(txtEndDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('收货日期 格式不正确！');";
                return;
            }
        }

        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('qc_passrate_export2.aspx?vend=" + txtVend.Text.Trim()
                                                    + "&p=" + txtPart.Text.Trim()
                                                    + "&d1=" + txtStdDate.Text.Trim()
                                                    + "&d2=" + txtEndDate.Text.Trim()
                                                    + "&ordDate1=" + txtOrdDate1.Text.Trim()
                                                    + "&ordDate2=" + txtOrdDate2.Text.Trim()
                                                    + "&dueDate1=" + txtDueDate1.Text.Trim()
                                                    + "&dueDate2=" + txtDueDate2.Text.Trim()
                                                    + "&chkFacDate=" + chkFacDate.Checked
                                                    + "&chkOverdue=" + chkOverdue.Checked
                                                    + "&chkInspect=" + chkInspect.Checked
                                                    + "&domain=" + dropDomain.SelectedValue
                                                    + "&poPeriod=" + (IsInteger(txbPoPeriod.Text.Trim()) ? txbPoPeriod.Text.Trim() : "0") + " ');";

    }
    protected void dgLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgLocation.PageIndex = e.NewPageIndex;

        BindData();
    }
    protected void dgLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.ToolTip = "双击查看明细";

            if (e.Row.Cells[2].Text != "&nbsp;")
            {
                if (dgLocation.DataKeys[e.Row.RowIndex].Value.ToString() != string.Empty && dgLocation.DataKeys[e.Row.RowIndex].Value.ToString() != "&nbsp;")
                {
                    if (Convert.ToDouble(dgLocation.DataKeys[e.Row.RowIndex].Value.ToString()) < 1)
                    {
                        if (int.Parse(e.Row.Cells[10].Text.Trim()) != 0)
                        {
                            e.Row.Attributes.Add("ondblclick", "location.href='qc_passrate_det.aspx?vend=" + e.Row.Cells[2].Text.Trim() + "&part=" + e.Row.Cells[3].Text.Trim() + "&stddate=" + txtStdDate.Text.Trim() + "&enddate=" + txtEndDate.Text.Trim() + "&dueDate1=" + txtDueDate1.Text.Trim() + "&dueDate2=" + txtDueDate2.Text.Trim() + "&period=" + txbPoPeriod.Text.Trim() + "&rm=" + DateTime.Now.ToLocalTime() + "'");
                        }
                    }
                }
            }
        }
    }
}
