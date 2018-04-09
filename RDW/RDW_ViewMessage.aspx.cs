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
using RD_WorkFlow;

public partial class RDW_ViewMessage : System.Web.UI.Page
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["did"] == null || Request.QueryString["mid"] == null)
            {
                ltlAlert.Text = " window.close();";
            }
            else
            {
                //定义参数
                string strMID = Convert.ToString(Request.QueryString["mid"]);
                string strDID = Convert.ToString(Request.QueryString["did"]);
                string strUID = Convert.ToString(Session["uID"]);

                if (!rdw.CheckViewRDWDetailEdit(strMID, strDID, strUID))
                {
                    ltlAlert.Text = " window.close();";
                }
                else
                {
                    gvMessage.DataSource = rdw.SelectRDWDetailMessage(strDID);
                    gvMessage.DataBind();
                }
            }
        }
    }
}
