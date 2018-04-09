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
using QADSID;

public partial class SID_shipdetailForFin : BasePage
{
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           string pk=Request["pk"];
           string nbr= Request["nbr"]; 
           string createdate= Request["createdate"];
           string createdate1 = Request["createdate1"];

            DataBind();
            DataBindHeader();
        }
    }

    protected void DataBindHeader()
    {
        DataTable dt = sid.GetShipDetail1(Convert.ToString(Request.QueryString["DID"]));
        lblPK.Text = dt.Rows[0].ItemArray[0].ToString();
        lblnbr.Text = dt.Rows[0].ItemArray[1].ToString();
        lblOutDate.Text = dt.Rows[0].ItemArray[2].ToString();
        lblVia.Text = dt.Rows[0].ItemArray[3].ToString();
        lblCtype.Text = dt.Rows[0].ItemArray[4].ToString();
        lblShipDate.Text = dt.Rows[0].ItemArray[5].ToString();
        lblshipto.Text = dt.Rows[0].ItemArray[6].ToString();
        lblsite.Text = dt.Rows[0].ItemArray[7].ToString();
        lblPKref.Text = dt.Rows[0].ItemArray[8].ToString();
        lblWeight.Text = dt.Rows[0].ItemArray[10].ToString();
        lblVolume.Text = dt.Rows[0].ItemArray[11].ToString();
        lblBox.Text = dt.Rows[0].ItemArray[12].ToString();
        lblPkgs.Text = dt.Rows[0].ItemArray[13].ToString();
        lblPrice.Text = dt.Rows[0].ItemArray[14].ToString();
    }

    protected void DataBind()
    {
        DataTable dt = sid.GetShipDetailForFin(Convert.ToString(Request.QueryString["DID"]), Request.QueryString["RAD"].ToString());
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
        }
        gvShipdetail.DataSource = dt;
        gvShipdetail.DataBind();
        dt.Dispose();
    }
    protected void gvShipdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Adds")
        {
            // ltlAlert.Text = "window.open('SID_ShipDetailAdds.aspx?DID=" + Server.UrlEncode(gvShipdetail.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString().Trim()) + "&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=1000,height=500,top=0,left=0');";
            Response.Redirect("SID_ShipDetailAdds.aspx?DID=" + Server.UrlEncode(gvShipdetail.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString().Trim()) + "&DID_ori=" + Convert.ToString(Request.QueryString["DID"]) + "&rm=" + DateTime.Now.ToString());
        }
    }

    protected void gvShipdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[13].Enabled = false;
            e.Row.Cells[14].Enabled = false;
            bool bSid = false;
            if (gvShipdetail.DataKeys[e.Row.RowIndex].Values[1].ToString() != string.Empty)
                bSid = Convert.ToBoolean(gvShipdetail.DataKeys[e.Row.RowIndex].Values[1].ToString());

            if (!bSid && Convert.ToBoolean(Request.QueryString["RAD"].ToString()))
            {
                e.Row.Cells[16].Enabled = false;
                e.Row.Cells[17].Enabled = false;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_ShipForFin.aspx?pk=" + Request["pk"] + "&nbr=" + Request["nbr"] + "&createdate=" + Request["createdate"] + "&createdate1=" + Request["createdate1"] + "&outdate=" + Request["outdate"] + "&outdate1=" + Request["outdate1"] + "&rt=" + DateTime.Now.ToString());
    }
}
