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

public partial class EDI_EDI860Detail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["orderdir"] = "ASC";
            gvBind(Convert.ToString(Request["hrdID"]).Trim());
        }
    }

    private void gvBind(string hrdId)
    {
        DataSet dsPo = getEdiData.get860Detail(hrdId);

        if (dsPo.Tables[0].Rows.Count == 0)
        {
            dsPo.Tables[0].Rows.Add(dsPo.Tables[0].NewRow());
        }
        gvDetail.DataSource = dsPo;
        gvDetail.DataBind();
    }


    protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetail.PageIndex = e.NewPageIndex;
        gvBind(Convert.ToString(Request["hrdID"]).Trim());
    }


    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lbltype")) != null)
            {
                Label lblPoId = (Label)e.Row.FindControl("lbltype");

                if (lblPoId.Text.Trim() == "C")
                {
                    lblPoId.Text = "Modify";
                }
                else if (lblPoId.Text.Trim() == "D")
                {
                    lblPoId.Text = "Delete";
                }
            }
            if (((Label)e.Row.FindControl("lbltype")) != null)
            {
                Button btnAdd = (Button)e.Row.FindControl("btnAdd");
                Label lbltype = (Label)e.Row.FindControl("lbltype");
                if (lbltype.Text.ToLower() == "a")
                {
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                }
            }
            if (((Button)e.Row.FindControl("btnFinish")) != null && ((Label)e.Row.FindControl("lblIsUpdate")) != null)
            {
                Button btnFinish = (Button)e.Row.FindControl("btnFinish");
                Label lblIsUpdate = (Label)e.Row.FindControl("lblIsUpdate");

                if (lblIsUpdate.Text == "True")
                {
                    btnFinish.Visible = false;
                }
                else if (lblIsUpdate.Text == "False")
                {
                    btnFinish.Visible = true;
                }
            }
        }
    }

    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "finish")
        {
            string strStatus =getEdiData.update860DetailStatus(e.CommandArgument.ToString().Trim());
            if (strStatus == "error")
            {
                ltlAlert.Text = "alert('Diffrent from QAD Information');";
            }
            gvBind(Convert.ToString(Request["hrdID"]).Trim());
        }
        if (e.CommandName == "add")
        {
            string strStatus = getEdiData.update860DetailAdd(e.CommandArgument.ToString().Trim());
           
            ltlAlert.Text = "alert('" + strStatus + "');";
            

            gvBind(Convert.ToString(Request["hrdID"]).Trim());
        }
    }
}
