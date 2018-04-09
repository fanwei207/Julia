using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_PCD_EDI210171ApprovrRestart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceRestart.aspx?FlowId=33BB25C2-9784-47CE-AC83-092CB6922332", true);
    }
}