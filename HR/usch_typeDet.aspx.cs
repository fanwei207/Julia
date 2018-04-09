using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_usch_typeDet : BasePage
{ 
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            Response.Redirect("../IT/Page_Viewer.aspx?pageID=69d75e78-29b1-4a86-ae64-b304d0d1a36a");
        }
    }
}