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
using System.Data.SqlClient;

public partial class RDW_UL_Sample : BasePage
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id;
            id = Convert.ToString(Request.QueryString["mid"]);
            //txtProject.Text = rdw.selectULproject(id);
            BindData(id);
            SqlDataReader read = rdw.selectULdet(id);
            if (read.Read())
            {
                txtProject.Text = read["UL_Project"].ToString().Trim();
                txtbsd.Text = read["UL_bsdnbr"].ToString().Trim();
                txtDate1.Text = read["UL_bsddate"].ToString().Trim();
            }
            read.Close();

        }
    }

    public void BindData(string id)
    {
        gvRDW.DataSource = rdw.selectULSample(id);
        gvRDW.DataBind();
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&mid=" + strID + "&rm=" + DateTime.Now.ToString(), true); 
    }
}