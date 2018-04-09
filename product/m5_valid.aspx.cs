using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m5_valid : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("/IT/Page_Viewer.aspx?pageID=7c3282a2-3276-4f50-9760-191e9da5f707");
        }
    }
}