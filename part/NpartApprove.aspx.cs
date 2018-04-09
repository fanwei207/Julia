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
        Response.Redirect("/NWF/nwf_workflowInstanceReview.aspx?FlowId=97d97fab-a4ee-4b08-9999-c40a4e5856a7", true);
    }
}