using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class product_m5_rep_M5Process : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("/IT/Page_Viewer.aspx?pageID=ea0d14d9-ad25-4c08-9e51-e9c5f581d00b");
        }
    }
}