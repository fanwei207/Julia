using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_PCD_updateApprove : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceReview.aspx?FlowId=87AB52EF-1D03-4EA6-AA64-37320B16F639", true);
    }
}