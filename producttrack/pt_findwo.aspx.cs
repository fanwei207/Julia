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
using adamFuncs;

public partial class producttrack_pt_findwo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.AddMonths(-2).Month + "/" + DateTime.Now.AddMonths(-2).Day + "/" + DateTime.Now.AddMonths(-2).Year.ToString().Substring(2,2);
            txtDate1.Text = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year.ToString().Substring(2,2);

            lblmsg.Visible = false;
            DataBind();

        }
    }

    public void DataBind()
    {

        if (txtLot.Text.Trim().Length > 0)
        {
            lblmsg.Visible = false;
            Cproducttrack pt = new Cproducttrack();
            DataTable dt = pt.GetWO(txtLot.Text.Trim(),txtDate.Text,txtDate1.Text).Tables[0];
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());

            }
            gvWO.DataSource = dt;
            gvWO.DataBind();
            dt.Dispose();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Input Lot!";
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtLot.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please Input  Lot!');";
            return;
        }
        DataBind();
    }
    protected void gvWO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWO.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void gvWO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gvWO.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('No data£¡');";
                return;
            }

            int index = int.Parse(e.CommandArgument.ToString());
            ltlAlert.Text = "window.open('pt_woisssearch.aspx?Lot=" + Server.UrlEncode(gvWO.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&Nbr=" + Server.UrlEncode(gvWO.Rows[index].Cells[2].Text.ToString()) + "&Domain=" + Server.UrlEncode(gvWO.Rows[index].Cells[0].Text.ToString()) + "&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=860,height=500,top=0,left=0');";
        }
    }
}
