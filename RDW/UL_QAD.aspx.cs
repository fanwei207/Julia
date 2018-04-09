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
public partial class RDW_UL_QAD : System.Web.UI.Page
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id;
            id = Convert.ToString(Request.QueryString["mid"]);
            lblId.Text = id;
            //txtProject.Text = rdw.selectULproject(id);
            BindData(id);
            SqlDataReader read = rdw.selectULdet(id);
            if (read.Read())
            {
                txtProject.Text = read["UL_Project"].ToString().Trim();
              
            }
            read.Close();
        }

    }
    public void BindData(string id)
    {
        gvRDW.DataSource = rdw.selectULQAD(id);
        gvRDW.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (txtDate1.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('QAD不能为空');";
        //    return;
        //}
        //if (rdw.insertULQAD(Convert.ToString(Request.QueryString["mid"]), txtDate1.Text.Trim(), Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        //{
        //    BindData(Request.QueryString["mid"]);
        //    txtDate1.Text = "";
        //}
        //else
        //{
        //    ltlAlert.Text = "alert('QAD不存在，保存失败');";
        //    return;
        //}
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&id=" + strID + "&rm=" + DateTime.Now.ToString(), true); 
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        Response.Redirect("/RDW/UL_QADImport.aspx?t=&mid=" + strID + "&Project=" + txtProject.Text.Trim() + "&rm=" + DateTime.Now.ToString(), true); 
    }
}