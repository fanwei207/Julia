using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_rp_purchaseProcedureUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("../IT/Page_Viewer.aspx?pageID=d364a905-7278-4c2c-9d59-0f67475428b9");
        }
    }
}