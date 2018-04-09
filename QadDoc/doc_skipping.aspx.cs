using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QadDoc_doc_skipping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["TYPE"].ToString().Trim() == "版本升级")
        {
            Response.Redirect("/qaddoc/qad_documentsearch.aspx?typeID=" + HttpUtility.UrlDecode(Request["typeid"].ToString()).Trim() + "&docName=" + HttpUtility.UrlEncode(Request["name"].ToString()).Trim() + "&cateid=" + HttpUtility.UrlDecode(Request["cateid"].ToString()).Trim() + "&pg= 0 &typename=" + HttpUtility.UrlEncode(Request["typename"].ToString()).Trim() + "&catename=" + HttpUtility.UrlEncode(Request["catename"].ToString()).Trim());
        }
    }
}