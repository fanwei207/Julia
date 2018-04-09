using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;

public partial class Purchase_rp_purchaseList : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGV();
        }
    }
    private void BindGV()
    {
        DataTable dt = SelectPurchaseList();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable SelectPurchaseList()
    {
        string str = "sp_rp_SelectPurchaseList";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@type", ddlType.SelectedValue.ToString());
        param[1] = new SqlParameter("@plant",  ddlPlant.SelectedValue.ToString());
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGV();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindGV();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindGV();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string qad = gv.DataKeys[e.RowIndex].Values["rp_QAD"].ToString().Trim();
        string vend = gv.DataKeys[e.RowIndex].Values["rp_supplier"].ToString().Trim();
        string vendName = gv.DataKeys[e.RowIndex].Values["rp_supplierName"].ToString().Trim();
        string nbr = ((TextBox)gv.Rows[e.RowIndex].Cells[8].FindControl("txtNbr")).Text.ToString().Trim();
        string line = ((TextBox)gv.Rows[e.RowIndex].Cells[9].FindControl("txtLine")).Text.ToString().Trim();
        

        string str = "sp_rp_UpdatePurchaseList";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@qad", qad);
        param[1] = new SqlParameter("@vend", vend);
        param[2] = new SqlParameter("@vendName", vendName);
        param[3] = new SqlParameter("@nbr", nbr);
        param[4] = new SqlParameter("@line", line);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, param);

        gv.EditIndex = -1;
        BindGV();
    }
    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGV();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGV();
    }
}