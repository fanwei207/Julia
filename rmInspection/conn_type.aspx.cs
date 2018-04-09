using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmInspection_conn_type : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("../IT/Page_Viewer.aspx?pageID=daafc40e-ced4-4b56-b575-5dd3b711b7d1");
        }
    }
}