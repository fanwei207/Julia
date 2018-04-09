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


public partial class QC_qc_gbtLevelEdit : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ogv.GridViewDataBind(gvLevel, oqc.GetGbtLevel(""));
            //gvLevel.DataSource = oqc.GetGbtLevel("");
            //gvLevel.DataBind();
        }
    }
    protected void gvLevel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvLevel.EditIndex = e.NewEditIndex;

        ogv.GridViewDataBind(gvLevel, oqc.GetGbtLevel(""));
        //gvLevel.DataSource = oqc.GetGbtLevel("");
        //gvLevel.DataBind();
    }
    protected void gvLevel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvLevel.EditIndex = -1;

        ogv.GridViewDataBind(gvLevel, oqc.GetGbtLevel(""));
        //gvLevel.DataSource = oqc.GetGbtLevel("");
        //gvLevel.DataBind();
    }
    protected void gvLevel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strNewLevel = ((TextBox)gvLevel.Rows[e.RowIndex].Cells[2].Controls[1]).Text.Trim();
        string strOldLevel = ((Label)gvLevel.Rows[e.RowIndex].Cells[0].Controls[1]).Text.Trim();

        string strMsg = "";

        oqc.ModifyGbtLevel(strNewLevel, strOldLevel, int.Parse(Session["uID"].ToString()), ref strMsg);

        if (strMsg != "") 
        {
            ltlAlert.Text = "alert('" + strMsg + "');";
        }
        else
            gvLevel.EditIndex = -1;

        ogv.GridViewDataBind(gvLevel, oqc.GetGbtLevel(""));
        //gvLevel.DataSource = oqc.GetGbtLevel("");
        //gvLevel.DataBind();
    }
    protected void gvLevel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strLevel = ((Label)gvLevel.Rows[e.RowIndex].Cells[0].Controls[1]).Text.Trim();

        oqc.DeleteGbtLevel(strLevel);

        ogv.GridViewDataBind(gvLevel, oqc.GetGbtLevel(""));
        //gvLevel.DataSource = oqc.GetGbtLevel("");
        //gvLevel.DataBind();
    }
    protected void gvLevel_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        oqc.AddGbtLevel(int.Parse(Session["uID"].ToString()));

        gvLevel.EditIndex = 0;

        ogv.GridViewDataBind(gvLevel, oqc.GetGbtLevel(""));
        //gvLevel.DataSource = oqc.GetGbtLevel("");
        //gvLevel.DataBind();
    }
}
