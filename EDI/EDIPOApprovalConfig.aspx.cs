using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EDI_EDIPOApprovalConfig : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/NWF_FlowNode.aspx?id=8C930BDD-4C65-45E6-A9D7-3EDD873005A3", true);
    }
}