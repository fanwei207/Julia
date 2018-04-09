using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;
public partial class Supplier_Supp_FactoryInspection_mstr : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getType();
            if (!string.IsNullOrEmpty(Request.QueryString["no"]))
            {
                txt_no.Text = Request.QueryString["no"].ToString();
            }
            BindGridView();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    public void getType()
    {
        try
        {
            string strName = "sp_FI_selectFIType";

            droptype.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
            droptype.DataBind();
            droptype.Items.Insert(0, new ListItem("--All--", "-1"));
            droptype.SelectedValue = "-1";

        }
        catch (Exception ex)
        {

        }
    }
    protected override void BindGridView()
    {
        try
        {
            int i = Convert.ToInt32(ddlStatu.SelectedValue);
            string strName = "sp_FI_selectFIMstr";
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@type", droptype.SelectedItem.Text);
            param[1] = new SqlParameter("@no", txt_no.Text);
            param[2] = new SqlParameter("@vent", txtvent.Text);
            param[3] = new SqlParameter("@name", txtname.Text);
            param[4] = new SqlParameter("@stutes", Convert.ToInt32(ddlStatu.SelectedValue));
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string fi_id = gv.DataKeys[index].Values["FI_id"].ToString();
            string createby = gv.DataKeys[index].Values["FI_createby"].ToString();
            string comm = "0";
            if (Session["uID"].ToString() == createby)
            {
                comm = "1";
            }

            this.Redirect("Supp_FatocryInspection_New.aspx?FI_id=" + fi_id + "&comm=" + comm + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }

        else if (e.CommandName == "Detailcheck")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string fi_id = gv.DataKeys[index].Values["FI_id"].ToString();
            string FI_NO = gv.DataKeys[index].Values["FI_NO"].ToString();

            this.Redirect("FI_check.aspx?FI_id=" + fi_id + "&fi_no=" + FI_NO + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string fi_id = gv.DataKeys[index].Values["FI_id"].ToString();
            string FI_NO = gv.DataKeys[index].Values["FI_NO"].ToString();

            this.Redirect("FI_view.aspx?FI_id=" + fi_id + "&fi_no=" + FI_NO + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else if (e.CommandName == "close")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string FI_NO = gv.DataKeys[index].Values["FI_NO"].ToString();
            ddlStatu.SelectedValue = "-1";
            txt_no.Text = FI_NO;
            BindGridView();
        }

    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}