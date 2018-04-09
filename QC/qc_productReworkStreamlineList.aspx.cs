using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using QCProgress;

public partial class QC_qc_productReworkStreamlineList : BasePage
{
    QC qc = new QC();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        this.bind();
    }

    private void bind()
    {
        string woNbr = txtWoNbr.Text.Trim();
        string wolot = txtLot.Text.Trim();
        string type = ddlType.SelectedValue;
        string starDate = txtStarDate.Text.Trim();
        string endDate = txtEndDate.Text.Trim();

        try
        {
            if (string.Empty != starDate)
            {
                Convert.ToDateTime(starDate);
            }
            if (string.Empty != endDate)
            {
                Convert.ToDateTime(endDate);
            }
        }
        catch
        {
            ltlAlert.Text = "alert('填写必须为时间格式！');";
            return;
        }

        gvUpload.DataSource = qc.selectProductReworkStreamlineList(woNbr, wolot, type, starDate, endDate);
        gvUpload.DataBind();


    }
    protected void gvUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUpload.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnFile")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        else if (e.CommandName == "lkbtnReport")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
}