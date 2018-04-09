using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QadDoc_qad_documentItemAppvResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceResult.aspx?FlowId=67EF9CD9-4D4A-469B-AC6B-5AD9678CCBFD", true);
    }
}