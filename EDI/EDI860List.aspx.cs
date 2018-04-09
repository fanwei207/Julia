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

public partial class EDI_EDI860List : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["orderdir"] = "ASC";

            txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
            txtStdDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            gvBind();
        }
    }

    private void gvBind()
    {
        DataSet dsPo = getEdiData.get860List(txtStdDate.Text, txtEndDate.Text);

        if (dsPo.Tables[0].Rows.Count == 0)
        {
            dsPo.Tables[0].Rows.Add(dsPo.Tables[0].NewRow());
        }

        gvlist.DataSource = dsPo;
        gvlist.DataBind();
    }



    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        gvBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (((Label)e.Row.FindControl("lblPoId")) != null)
            {
                Label lblPoId = (Label)e.Row.FindControl("lblPoId");

                e.Row.Attributes.Add("OnDblClick", "window.open('EDI860Detail.aspx?hrdID=" + lblPoId.Text.Trim() + "', '_blank');");
            }
        }
    }
    protected void btnExcelExport_Click(object sender, EventArgs e)
    {
        if (txtStdDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtStdDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');";

                return;
            }
        }

        if (txtEndDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');";

                return;
            }
        }

        ltlAlert.Text = "window.open('EDI860ExportExcel.aspx?stdDate=" + txtStdDate.Text.Trim() + "&endDate=" + txtEndDate.Text.Trim() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delRec")
        {
            getEdiData.del860Record(e.CommandArgument.ToString().Trim());

            gvBind();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtStdDate.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtStdDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确！');";
                return;
            }
        }

        if (txtEndDate.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtEndDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确！');";
                return;
            }
        }

        gvBind();
    }
}
