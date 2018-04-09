using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RDW_RDW_PPAMstrAppvResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceResult.aspx?FlowId=96dda7ae-3bf4-4176-8712-121c8394efdc", true);
    }
}