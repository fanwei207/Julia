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

public partial class qc_passrate_det : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnBack.Visible = false;

            if (Request.QueryString["vend"] != null)
            {
                txtVend.Text = Request.QueryString["vend"].ToString();
                txtPart.Text = Request.QueryString["part"].ToString();
                txtStdDate.Text = Request.QueryString["stddate"].ToString();
                txtEndDate.Text = Request.QueryString["enddate"].ToString();
                txtDueDate1.Text = Request.QueryString["dueDate1"].ToString();
                txtDueDate2.Text = Request.QueryString["dueDate2"].ToString();
                txbPoPeriod.Text = Request.QueryString["period"].ToString();

                btnBack.Visible = true;

                BindData();
            }
            else
            {
                txtDueDate1.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                txtDueDate2.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
        }
    }

    public DataTable GetIncomingNotPass(string vend, string part, string stddate, string enddate, bool inspected, bool overdue, int po_period
        , string dueDate1, string dueDate2, bool chkFacDate, string domain, string chkDate1, string chkDate2)
    {
        SqlParameter[] param = new SqlParameter[13];
        param[0] = new SqlParameter("@vend", vend);
        param[1] = new SqlParameter("@part", part);
        param[2] = new SqlParameter("@stddate", stddate);
        param[3] = new SqlParameter("@enddate", enddate);
        param[4] = new SqlParameter("@inspected", inspected);
        param[5] = new SqlParameter("@overdue", overdue);
        param[6] = new SqlParameter("@po_period", po_period);
        param[7] = new SqlParameter("@dueDate1", dueDate1);
        param[8] = new SqlParameter("@dueDate2", dueDate2);
        param[9] = new SqlParameter("@chkFacDate", chkFacDate);
        param[10] = new SqlParameter("@domain", domain);
        param[11] = new SqlParameter("@checkDate1", chkDate1);
        param[12] = new SqlParameter("@checkDate2", chkDate2);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_QC_Rep_IncomingNotPass", param).Tables[0];
    }

    protected void BindData()
    {
        if (txtStdDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtStdDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('收货日期 格式不正确！');";
                return;
            }
        }

        if (txtEndDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('收货日期 格式不正确！');";
                return;
            }
        }

        if (txtDueDate1.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDueDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截至日期 格式不正确！');";
                return;
            }
        }

        if (txtDueDate2.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDueDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截至日期 格式不正确！');";
                return;
            }
        }

        if (txbPoPeriod.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('采购周期 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txbPoPeriod.Text.Trim());

                if (_n <= 0)
                {
                    ltlAlert.Text = "alert('采购周期 不能小于0！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('采购周期 必须是整数！');";
                return;
            }
        }

        string supp = txtVend.Text.Trim();
        string part = txtPart.Text.Trim();
        string stddate = txtStdDate.Text.Trim();
        string enddate = txtEndDate.Text.Trim();
        string dueDate1 = txtDueDate1.Text.Trim();
        string dueDate2 = txtDueDate2.Text.Trim();
        string chkDate1 = txtChkDate1.Text.Trim();
        string chkDate2 = txtChkDate2.Text.Trim();

        bool inspected = chkInspect.Checked;
        bool overdue = chkOverdue.Checked;

        int poPeriod = Convert.ToInt32(txbPoPeriod.Text.Trim());

        DataTable dt = GetIncomingNotPass(supp, part, stddate, enddate, inspected, overdue, poPeriod, dueDate1, dueDate2, chkFacDate.Checked, dropDomain.SelectedValue, chkDate1, chkDate2);

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
        }
        else
        {
            this.dgLocation.DataSource = dt;
            this.dgLocation.DataBind();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('qc_passrate_export2.aspx?det=1&vend=" + txtVend.Text.Trim()
                                                     + "&p=" + txtPart.Text.Trim()
                                                     + "&d1=" + txtStdDate.Text.Trim()
                                                     + "&d2=" + txtEndDate.Text.Trim()
                                                     + "&due1=" + txtDueDate1.Text.Trim()
                                                     + "&due2=" + txtDueDate2.Text.Trim()
                                                     + "&chkInspect=" + chkInspect.Checked
                                                     + "&chkOverdue=" + chkOverdue.Checked
                                                     + "&chkFacDate=" + chkFacDate.Checked
                                                     + "&domain=" + dropDomain.SelectedValue
                                                     + "&chk1=" + txtChkDate1.Text.Trim()
                                                     + "&chk2=" + txtChkDate2.Text.Trim()
                                                     + "&pr=" + txbPoPeriod.Text.Trim() + "', '_blank');";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void dgLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgLocation.PageIndex = e.NewPageIndex;

        BindData();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("qc_passrate.aspx");
    }
}
