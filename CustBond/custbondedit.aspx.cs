using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class CustBond_custbondedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year - 1, 1, 1);
            date.Value = d1.ToString("yyyy-MM-dd");
            date1.Value = now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
    }  /*数据由前台AJAX取*/

}