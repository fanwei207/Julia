using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wo2_wo2_routingAppvConfig : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/NWF_FlowNode.aspx?id=4DD36480-265A-4B39-8A61-CAC92664EDFF", true);
    }
}