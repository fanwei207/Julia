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

public partial class QC_qc_gbt2828 : BasePage
{
    QC oqc = new QC();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            oqc.DynamicBindgvGbt2828(oqc.GetGbt2828(), this.gvGbt2828);
        }
    }

    protected void gvGbt2828_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvGbt2828.EditIndex = e.NewEditIndex;
        oqc.DynamicBindgvGbt2828(oqc.GetGbt2828(), this.gvGbt2828);
    }
    protected void gvGbt2828_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvGbt2828.EditIndex = -1;
        oqc.DynamicBindgvGbt2828(oqc.GetGbt2828(), this.gvGbt2828);
    }
    protected void gvGbt2828_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string uid = gvGbt2828.Rows[e.RowIndex].UniqueID + ":";//注意别遗漏冒号 

        int intMin = int.Parse(gvGbt2828.DataKeys[e.RowIndex].Values[0].ToString());
        int intMax = int.Parse(gvGbt2828.DataKeys[e.RowIndex].Values[1].ToString());

        string[] strLevelList = Request.Form[uid + "txt"].ToString().Split(',');
        string strLevel;
        string strCode;
        string strMsg = "";

        for (int col = 0; col < strLevelList.Length; col++)
        {
            strLevel = gvGbt2828.HeaderRow.Cells[col+2].Text.Trim();
            strCode = strLevelList[col].Trim();

            oqc.ModifyGbt2828(intMin, intMax, strLevel, strCode, int.Parse(Session["uID"].ToString()), ref strMsg);

            if (strMsg != "") 
            {
                ltlAlert.Text = "alert('" + strMsg + "');";
                break;
            }

        }

        if (strMsg == "")
        {
            gvGbt2828.EditIndex = -1;
        }

        oqc.DynamicBindgvGbt2828(oqc.GetGbt2828(), this.gvGbt2828);
    }
    protected void gvGbt2828_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intMin = int.Parse(gvGbt2828.DataKeys[e.RowIndex].Values[0].ToString());
        int intMax = int.Parse(gvGbt2828.DataKeys[e.RowIndex].Values[1].ToString());

        oqc.DeleteGbtPatch(intMin, intMax);

        oqc.DynamicBindgvGbt2828(oqc.GetGbt2828(), this.gvGbt2828);
    }
    protected void gvGbt2828_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "批量(Min)";
            e.Row.Cells[1].Text = "批量(Max)";
        }
    }
}
