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
using MinorP;
using Microsoft.ApplicationBlocks.Data;


public partial class new_MPPrint : BasePage
{
    adamClass adam = new adamClass();
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {

        PrintdataBind(Convert.ToInt32(Request["mid"]));


        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf-8";

        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode( "Print.xls"));
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

        Response.ContentType = "application/ms-excel";
        Page.EnableViewState = false;

        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        this.Page.RenderControl(oHtmlTextWriter);

        string temp = oStringWriter.ToString();

        Response.Write(temp);
        Response.End();

    }

    private void PrintdataBind(int intMid)
    {
        DataTable dtInfor = mp.MinorPList(Convert.ToInt32(Session["Plantcode"]), 0, intMid);
        if (dtInfor.Rows.Count > 0)
        {
            lblApper.Text = dtInfor.Rows[0].ItemArray[3].ToString();
            lblDept.Text = dtInfor.Rows[0].ItemArray[4].ToString();
            lblType.Text = dtInfor.Rows[0].ItemArray[6].ToString();
            lblQuantity.Text = dtInfor.Rows[0].ItemArray[7].ToString();
            lblprice.Text = dtInfor.Rows[0].ItemArray[9].ToString();
            lblPart.Text = dtInfor.Rows[0].ItemArray[5].ToString();
            lblSP.Text = dtInfor.Rows[0].ItemArray[10].ToString();

            //lblAid.Text = dtInfor.Rows[0].ItemArray[0].ToString();

            lbltotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtInfor.Rows[0].ItemArray[7]) * Convert.ToDecimal(dtInfor.Rows[0].ItemArray[9].ToString()), 3));

        }

        string strSQL = "SELECT top 1  u.userName FROM MPApplication LEFT OUTER JOIN users u ON u.userID =Appuid Where mpid ='" + intMid.ToString() + "' Order By Appdate desc ";
        lblConform.Text = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL).ToString();
    }
 
}
