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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;

public partial class EdiHrdDistribute : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@date", txtDate.Text.Trim());
        param[1] = new SqlParameter("@plantCode", Session["plantCode"].ToString());
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getEdiPoHrdDistribute", param);

        gvlist.DataSource = ds;
        gvlist.DataBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList dropDomain = (DropDownList)e.Row.FindControl("dropDomain");
            HtmlInputHidden hDomain = (HtmlInputHidden)e.Row.FindControl("hDomain");

            e.Row.Attributes.Add("OnDblClick", "window.open('EdiDetDistribute.aspx?id=" + gvlist.DataKeys[e.Row.RowIndex].Values["id"].ToString() + "&domain=" + gvlist.DataKeys[e.Row.RowIndex].Values["domain"].ToString() + "');");

            try
            {
                hDomain.Value = gvlist.DataKeys[e.Row.RowIndex].Values["domain"].ToString();
                dropDomain.Items.FindByValue(gvlist.DataKeys[e.Row.RowIndex].Values["domain"].ToString()).Selected = true;
            }
            catch
            {
                dropDomain.SelectedIndex = -1;
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }

    protected void drpDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((DropDownList)sender).SelectedIndex != 0)
        {
            foreach (GridViewRow row in gvlist.Rows)
            {
                HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

                if (chkImport.Checked)
                {
                    ((DropDownList)row.FindControl("dropDomain")).SelectedValue = ((DropDownList)sender).SelectedValue;
                }
            }
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            chkImport.Checked = chkAll.Checked;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvlist.Rows)
        {
            string id = gvlist.DataKeys[row.RowIndex].Values["id"].ToString();

            DropDownList dropDomain = (DropDownList)row.FindControl("dropDomain");
            HtmlInputHidden hDomain = (HtmlInputHidden)row.FindControl("hDomain");

            if (dropDomain.SelectedIndex != 0 && hDomain.Value != dropDomain.SelectedValue)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@hrdid", gvlist.DataKeys[row.RowIndex].Values["id"].ToString());
                param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateEdiHrdDomain", param);
            }
        }

        BindGridView();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('导入日期格式不正确!');";
                return;
            }
        }

        BindGridView();
    }
    protected void gvlist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
