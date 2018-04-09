using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_Pur_ApproveConfig : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/NWF_FlowNode.aspx?id=EDAF9855-7C48-40DD-9683-F13872885185", true);
    }
}