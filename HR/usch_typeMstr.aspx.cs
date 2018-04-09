using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_usch_typeMstr : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("../IT/Page_Viewer.aspx?pageID=8dcec1ff-a60a-48b1-8c39-220ba596db08");
        }
    }
}