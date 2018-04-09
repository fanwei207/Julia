using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EDI_EdiRestart_WoTracking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["soNbr"] != null && Request.QueryString["qadPart"] != null && Request.QueryString["poLine"] != null)
        {
            Response.Redirect("/plan/WoTracking.aspx?soNbr=" + Request.QueryString["soNbr"] + "&part=" + Request.QueryString["qadPart"] + "&line=" + Request.QueryString["poLine"]);
        }
    }
}