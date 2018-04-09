using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EDI_edi_restart_FlowNode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/NWF/NWF_FlowNode.aspx?id=18aaf492-e20b-41e3-93c6-54af63059fef", true);
    }
}