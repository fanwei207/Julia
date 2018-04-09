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
using QCProgress;

public partial class qc_rep_cp100 : BasePage
{
    QC _qc = new QC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropType.DataSource = _qc.GetProductDefectType(Session["uID"].ToString());
            dropType.DataBind();
        }
    }
    protected void btnDaily_Click(object sender, EventArgs e)
    {
        if (txtOrdDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtOrdDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('工单日期格式不正确!');";
                return;
            }
        }

        if (txtOrdDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtOrdDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('工单日期格式不正确!');";
                return;
            }
        }

        if (txtDueDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtDueDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!');";
                return;
            }
        }

        if (txtDueDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtDueDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!');";
                return;
            }
        }

        ltlAlert.Text = "window.open('qc_rep_cp100_export.aspx?nbr1=" + txtNbr1.Text.Trim() + "&nbr2=" + txtNbr2.Text + "&orddate1=" + txtOrdDate1.Text + "&orddate2=" + txtOrdDate2.Text + "&duedate1=" + txtDueDate1.Text + "&duedate2=" + txtDueDate2.Text + "&type=" + dropType.SelectedValue + "&tcp=" + chkTcp.Checked.ToString() + "', '_blank');";
    }
}
