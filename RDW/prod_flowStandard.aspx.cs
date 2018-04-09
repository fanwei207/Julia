using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class prod_flowStandard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("../IT/Page_Viewer.aspx?pageID=7093c06b-0af1-4b6d-a984-e9f982df0713");
        }
    }
}