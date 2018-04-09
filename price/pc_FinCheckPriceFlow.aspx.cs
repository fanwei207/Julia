using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pc_FinCheckPriceFlow : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceReview.aspx?FlowId=3d6b58f1-95d2-41cd-afd1-b73c0f81c10f", true);
    }

}