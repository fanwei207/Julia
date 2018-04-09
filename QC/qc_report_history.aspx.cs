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

public partial class QC_qc_report_history : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected string FormType
    {
        get
        {
            if (Request.QueryString["type"] != null)
            {
                return Request.QueryString["type"];
            }
            else
            {
                return "";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblPage.Text = Request.QueryString["page"].ToString().Trim();
            lblGroup.Text = Request.QueryString["group"].ToString().Trim();

            DataTable table = oqc.GetReportSnapByGroup(lblPage.Text.Trim(), lblGroup.Text.Trim());

            if (table.Rows.Count == 0)
            {
                Response.Redirect("qc_report.aspx");
                return;
            }

            lblPrhid.Text = table.Rows[0][0].ToString().Trim();
            lblReceiver.Text = table.Rows[0][1].ToString().Trim();
            lblOrder.Text = table.Rows[0][2].ToString().Trim();
            lblLine.Text = table.Rows[0][3].ToString().Trim();
            lblPart.Text = table.Rows[0][4].ToString().Trim();
            lblCust.Text = table.Rows[0][5].ToString().Trim();

            ogv.GridViewDataBind(gvReport, oqc.GetReportHistory(lblPrhid.Text.Trim(),Session["uID"].ToString()));
        }
        else
        {
            ogv.ResetGridView(gvReport);
        }
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //order
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

            if (e.Row.Cells[4].Text.Trim() != "" && e.Row.Cells[4].Text.Trim() != "&nbsp;")
            {
                if (int.Parse(e.Row.Cells[4].Text.Trim()) == 0)
                {
                    e.Row.Cells[4].Text = "NO";
                }
                else
                {
                    e.Row.Cells[4].Text = "OK";
                }
            }

            if (Request.QueryString["page"].ToString() == "100103110") 
            {
                ((LinkButton)e.Row.Cells[5].FindControl("LinkButton1")).Text = "查看项目";
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["page"].ToString() == "100103110")
        {
            if (FormType == "read")
            {
                this.Response.Redirect(string.Format("qc_report_complete.aspx?type=read&ponbr={0}&receiver={1}&line={2}&part={3}", Request.QueryString["ponbr"], Request.QueryString["receiver"], Request.QueryString["line"], Request.QueryString["part"]));
            }
            else
            {
                this.Response.Redirect("qc_report_complete.aspx");
            }
        }
        else
            this.Response.Redirect("qc_report.aspx");
    }
    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "link") 
        {
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
            string _rcvd = gvReport.DataKeys[index].Values[0].ToString().Trim();
            string _ID = gvReport.DataKeys[index].Values[1].ToString().Trim();

            if (Request.QueryString["page"].ToString() == "100103110")
            {
                Response.Redirect("qc_report_project.aspx?parent=100103111&page=100103110&id=" + _ID + "&group=" + lblGroup.Text.Trim() + "&rcvd=" + _rcvd);
            }
            else 
            {
                Response.Redirect("qc_report_project.aspx?parent=100103111&page=100103097&id=" + _ID + "&group=" + lblGroup.Text.Trim() + "&rcvd=" + _rcvd);
            }
        }
    }
}
