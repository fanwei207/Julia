using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RDW_RDW_TrackingPrjForAmerica : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_TrackingPrjByTamplate.aspx?mstrId=AF54E532-1C2A-4457-B7A8-5A11E5862A8D", true);
    }
}