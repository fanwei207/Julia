using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IT_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../IT/Page_Viewer.aspx?pageID=835a0d3d-a62f-435a-8abb-7fa42d609142");
    }
}