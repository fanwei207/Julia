using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class product_m5_project : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("/IT/Page_Viewer.aspx?pageID=4383386f-b66d-4795-8b0f-c636e38bc433");
        }
    }
}