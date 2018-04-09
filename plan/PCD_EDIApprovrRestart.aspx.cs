using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_PCE_EDIApprovrRestart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceRestart.aspx?FlowId=2330da37-2bce-4d44-926f-d13ee4c50299", true);
    }
}