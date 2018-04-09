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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class note_det : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }

    private void GridViewBind() 
    {
        try
        {
            string strName = "sp_note_selectNoteReportDetail";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", Request.QueryString["id"]);
            param[1] = new SqlParameter("@type", Request.QueryString["type"] == null ? "0" : Request.QueryString["type"].ToString());
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = e.Row.FindControl("chk") as CheckBox;

            chk.Checked = Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex].Values["ntt_sel"]);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("note_export.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
}
