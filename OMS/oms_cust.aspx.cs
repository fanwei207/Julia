using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using IT;

public partial class oms_cust : BasePage
{
    string strConn = ConfigurationManager.AppSettings["SqlConn.QAD_DATA"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
            gv.Columns[3].Visible = this.Security["44000730"].isValid;
            gv.Columns[4].Visible = this.Security["44000740"].isValid;
        }
    }
    protected override void BindGridView()
    {
        try
        {
            string strSql = "sp_oms_selectCustList";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@custCode", txtCustCode.Text.Trim());
            param[1] = new SqlParameter("@cust", txtCust.Text.Trim());

            gv.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
            gv.DataBind();
        }
        catch
        {
            return;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _custCode = gv.Rows[index].Cells[0].Text;
            string _custName = gv.Rows[index].Cells[1].Text;

            this.Redirect("oms_mstr.aspx?custCode=" + _custCode + "&custName=" + _custName + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else if (e.CommandName == "FS")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _custCode = gv.Rows[index].Cells[0].Text;
            string _custName = gv.Rows[index].Cells[1].Text;

            this.Redirect("oms_FSDocImport.aspx?custCode=" + _custCode + "&custName=" + _custName + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else if(e.CommandName=="Forecast")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _custCode = gv.Rows[index].Cells[0].Text;
            string _custName = gv.Rows[index].Cells[1].Text;

            this.Redirect("oms_FSFCImport.aspx?custCode=" + _custCode + "&custName=" + _custName + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}