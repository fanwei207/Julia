using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pc_updateAppvResult : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceResult.aspx?FlowId=FA471321-7F3C-4BC1-9BB9-2EC0BA0DE159", true);
    }
}