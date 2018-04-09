using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m5_excutor : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("../IT/Page_Viewer.aspx?pageID=7c3e60b6-3ed6-4893-a5c9-b386a19964ca&pk0=" + Request["no"]);
        }
    }
}