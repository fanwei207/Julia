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

public partial class QC_qc_gbtPatchEdit : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            ogv.GridViewDataBind(gvPatch, oqc.GetGbtPatch(""));
        }
    }
    protected void gvPatch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPatch.EditIndex = e.NewEditIndex;

        ogv.GridViewDataBind(gvPatch, oqc.GetGbtPatch(""));
    }
    protected void gvPatch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPatch.EditIndex = -1;

        ogv.GridViewDataBind(gvPatch, oqc.GetGbtPatch(""));
    }
    protected void gvPatch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int intOldMin = int.Parse(((Label)gvPatch.Rows[e.RowIndex].Cells[0].Controls[1]).Text.Trim());
        int intOldMax = int.Parse(((Label)gvPatch.Rows[e.RowIndex].Cells[1].Controls[1]).Text.Trim());

        TextBox txtMin = (TextBox)gvPatch.Rows[e.RowIndex].Cells[3].Controls[1];
        TextBox txtMax = (TextBox)gvPatch.Rows[e.RowIndex].Cells[4].Controls[1];
        int intNewMin = int.Parse(txtMin.Text.Trim());
        int intNewMax = int.Parse(txtMax.Text.Trim());

        string strMsg = "";

        oqc.ModifyGbtPatch(intNewMin, intNewMax, intOldMin, intOldMax, int.Parse(Session["uID"].ToString()), ref strMsg);

        if (strMsg != "")
        {
            ltlAlert.Text = "alert('" + strMsg + "');";
        }
        else
            gvPatch.EditIndex = -1;

        ogv.GridViewDataBind(gvPatch, oqc.GetGbtPatch(""));
    }
    protected void gvPatch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataBoundLiteralControl txtMin = (DataBoundLiteralControl)gvPatch.Rows[e.RowIndex].Cells[3].Controls[0];
        DataBoundLiteralControl txtMax = (DataBoundLiteralControl)gvPatch.Rows[e.RowIndex].Cells[4].Controls[0];
        int intMin = int.Parse(txtMin.Text.Trim());
        int intMax = int.Parse(txtMax.Text.Trim());

        oqc.DeleteGbtPatch(intMin, intMax);

        ogv.GridViewDataBind(gvPatch, oqc.GetGbtPatch(""));
    }
    protected void gvPatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[2].Text = id.ToString();
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        oqc.AddGbtPatch(int.Parse(Session["uID"].ToString()));

        gvPatch.EditIndex = 0;

        ogv.GridViewDataBind(gvPatch, oqc.GetGbtPatch(""));
    }
}
