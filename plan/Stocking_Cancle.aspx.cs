using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_Stocking_Cancle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceCancle.aspx?FlowId=e5f1ed7c-d18c-4a5b-a70a-d180d48a2495", true);
    }
}