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
using adamFuncs;

public partial class QC_qc_aqlCodeEdit : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            ogv.GridViewDataBind(gvCode, oqc.GetAqlCode(""));
        }
    }
    protected void gvCode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCode.EditIndex = -1;

        ogv.GridViewDataBind(gvCode, oqc.GetAqlCode(""));
    }
    protected void gvCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[1].Text = id.ToString();
            }
        }
    }
    protected void gvCode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataBoundLiteralControl dbc = (DataBoundLiteralControl)gvCode.Rows[e.RowIndex].Cells[2].Controls[0];
        string strCode = dbc.Text.Trim();
        oqc.DeleteAqlCode(strCode);

        ogv.GridViewDataBind(gvCode, oqc.GetAqlCode(""));
    }
    protected void gvCode_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCode.EditIndex = e.NewEditIndex;

        ogv.GridViewDataBind(gvCode, oqc.GetAqlCode(""));
    }
    protected void gvCode_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strOldCode = ((Label)gvCode.Rows[e.RowIndex].Cells[0].Controls[1]).Text.Trim();
        string strNewCode = ((TextBox)gvCode.Rows[e.RowIndex].Cells[2].Controls[1]).Text.Trim();
        string strOrig = ((TextBox)gvCode.Rows[e.RowIndex].Cells[3].Controls[1]).Text.Trim();

        string strMsg = "";

        oqc.ModifyAqlCode(strNewCode, strOldCode, strOrig, int.Parse(Session["uID"].ToString()), ref strMsg);

        if (strMsg != "") 
        {
            ltlAlert.Text = "alert('" + strMsg + "');";
        }
        else
            gvCode.EditIndex = -1;

        ogv.GridViewDataBind(gvCode, oqc.GetAqlCode(""));
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        oqc.AddAqlCode(int.Parse(Session["uID"].ToString()));

        gvCode.EditIndex = 0;

        ogv.GridViewDataBind(gvCode, oqc.GetAqlCode(""));
    }
}
