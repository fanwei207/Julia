using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class part_NPartApproveCancel : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceCancle.aspx?FlowId=97d97fab-a4ee-4b08-9999-c40a4e5856a7", true);
    }
}