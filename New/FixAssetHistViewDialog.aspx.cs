using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using TCPNEW;


public partial class new_FixAssetHistViewDialog : BasePage
{
    adamClass adam = new adamClass(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        { 

            lblAssetNo.Text = Server.UrlDecode(Request.QueryString["assetno"].ToString().Trim());

            gvAssetDetail.DataBind();
        }
    }
}
