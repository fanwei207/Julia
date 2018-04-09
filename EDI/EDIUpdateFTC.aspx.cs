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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using QCProgress;
using CommClass;

public partial class EDI_EDIUpdateFTC : BasePage
{
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            ogv.GridViewDataBind(gvFTC, GetFTCItem("","","",0,"G"));
        }
    }
    protected void gvFTC_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFTC.EditIndex = -1;

        ogv.GridViewDataBind(gvFTC, GetFTCItem("", "", "", 0, "G"));
    }

    protected void gvFTC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      string strID = ((Label)gvFTC.Rows[e.RowIndex].Cells[0].Controls[1]).Text.Trim();
      DeleteFTCItem("", "", strID, Convert.ToInt32(Session["uID"]), "D");
      ogv.GridViewDataBind(gvFTC, GetFTCItem("", "", "", 0, "G"));
    }
    protected void gvFTC_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvFTC.EditIndex = e.NewEditIndex;

        ogv.GridViewDataBind(gvFTC, GetFTCItem("", "", "", 0, "G"));
    }
    protected void gvFTC_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strID = ((Label)gvFTC.Rows[e.RowIndex].Cells[0].Controls[1]).Text.Trim();
        string strJItem = ((TextBox)gvFTC.Rows[e.RowIndex].Cells[1].Controls[1]).Text.Trim();
        string strQItem = ((TextBox)gvFTC.Rows[e.RowIndex].Cells[2].Controls[1]).Text.Trim();

        if (strJItem.Length <= 0)
        {
            ltlAlert.Text = "alert('Please Input JDEItem!')";
            return;      
        }
        UpdateFTCItem(strJItem, strQItem, strID, Convert.ToInt32(Session["uID"]), "U");

       gvFTC.EditIndex = -1;

       ogv.GridViewDataBind(gvFTC, GetFTCItem("", "", "", 0, "G"));
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txbJDE.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please Input JDEItem!')";
            return;
        }
        AddFTCItem(txbJDE.Text.Trim(), txbQAD.Text.Trim(), "", Convert.ToInt32(Session["uID"]), "A");

        gvFTC.EditIndex = 0;

        ogv.GridViewDataBind(gvFTC, GetFTCItem("", "", "", 0, "G"));
    }

    protected DataTable GetFTCItem(string Jitem, string Qitem, string ID, int UID, string Type)
    {
        DataSet _dataset = new DataSet();

        try
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@Jitem", Jitem);
            parm[1] = new SqlParameter("@Qitem", Qitem);
            parm[2] = new SqlParameter("@ID", ID);
            parm[3] = new SqlParameter("@UID", UID);
            parm[4] = new SqlParameter("@type", Type);
            
            _dataset = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_EDI_getFTC", parm);

            return _dataset.Tables[0];
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    protected void UpdateFTCItem(string Jitem,string Qitem, string ID,int UID,string Type)
    {
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@Jitem", Jitem);
        parm[1] = new SqlParameter("@Qitem", Qitem);
        parm[2] = new SqlParameter("@ID", ID);
        parm[3] = new SqlParameter("@UID", UID);
        parm[4] = new SqlParameter("@type",Type);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_EDI_getFTC", parm);
        
    }
    protected void DeleteFTCItem(string Jitem, string Qitem, string ID, int UID, string Type)
    {
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@Jitem", Jitem);
        parm[1] = new SqlParameter("@Qitem", Qitem);
        parm[2] = new SqlParameter("@ID", ID);
        parm[3] = new SqlParameter("@UID", UID);
        parm[4] = new SqlParameter("@type", Type);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_EDI_getFTC", parm);

    }

    protected void AddFTCItem(string Jitem, string Qitem, string ID, int UID, string Type)
    {
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@Jitem", Jitem);
        parm[1] = new SqlParameter("@Qitem", Qitem);
        parm[2] = new SqlParameter("@ID", ID);
        parm[3] = new SqlParameter("@UID", UID);
        parm[4] = new SqlParameter("@type", Type);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_EDI_getFTC", parm);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       ogv.GridViewDataBind(gvFTC, GetFTCItem(txbJDE.Text.Trim(),txbQAD.Text.Trim(),"",0,"G"));

    }
    protected void gvFTC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFTC.PageIndex = e.NewPageIndex;
        ogv.GridViewDataBind(gvFTC, GetFTCItem("", "", "", 0, "G"));

    }
}
