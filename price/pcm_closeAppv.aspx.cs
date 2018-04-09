using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_closeAppv : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/nwf_workflowInstanceReview.aspx?FlowId=6D5C4285-A3B0-4AFD-A338-E28358342D34", true);
    }
}