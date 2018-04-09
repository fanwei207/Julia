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
using System.IO;

public partial class EDI_edi850ExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //divCim.InnerHtml = Server.UrlDecode(Request.QueryString["cim"].ToString());

        Response.Clear();
        Response.Buffer = false;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=adstmt.cim");
        Response.ContentType = "text/plain";
        this.EnableViewState = false;
        Response.Write(Request.QueryString["cim"].ToString());
        Response.End();


    }
}
