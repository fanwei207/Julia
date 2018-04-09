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

public partial class IT_UL_Model : System.Web.UI.Page
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            string id;
            id = Convert.ToString(Request.QueryString["mid"]);
            //txtProject.Text = rdw.selectULproject(id);
          
            SqlDataReader read = rdw.selectULdet(id);
            if (read.Read())
            {
                txtProject.Text = read["UL_Project"].ToString().Trim();

                txtDate1.Text = read["UL_model"].ToString().Trim();
                txtUl_Enumber.Text = read["Ul_Enumber"].ToString().Trim();
                txtUL_Section.Text = read["UL_Section"].ToString().Trim();
                txtUL_DriverJXL.Text = read["UL_DriverJXL"].ToString().Trim();
                txtUL_LEDJXL.Text = read["UL_LEDJXL"].ToString().Trim();
                txtDriverlv.Text = read["UL_DriverLv"].ToString().Trim();
                txtLEDllv.Text = read["UL_LEDLv"].ToString().Trim();
                txtUL_pic.Text = read["UL_pic"].ToString().Trim();
                txtUL_NOTE.Text = read["UL_NOTE"].ToString().Trim();
               

            }
            read.Close();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDate1.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('ULModel is null');";
            return;
        }
        string UL_model = txtDate1.Text.Trim();
        string Ul_Enumber = txtUl_Enumber.Text.Trim();
        string UL_Section = txtUL_Section.Text.Trim();
        string UL_DriverJXL = txtUL_DriverJXL.Text.Trim();
        string UL_LEDJXL = txtUL_LEDJXL.Text.Trim();
        string UL_Driverlv = txtDriverlv.Text.Trim();
        string UL_LEDllv = txtLEDllv.Text.Trim();
        string UL_pic = txtUL_pic.Text.Trim();
        string UL_NOTE = txtUL_NOTE.Text.Trim();
        if (rdw.updateULModel(Convert.ToString(Request.QueryString["mid"]), UL_model, Ul_Enumber, UL_Section, UL_DriverJXL, UL_LEDJXL, UL_Driverlv, UL_LEDllv, UL_pic, UL_NOTE))
        {
            ltlAlert.Text = "alert('保存成功');";
            string strID = Convert.ToString(Request.QueryString["mid"]);
            Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&id=" + strID + "&rm=" + DateTime.Now.ToString(), true); 
        }
        else
        {
            ltlAlert.Text = "alert('保存失败');";
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&id=" + strID + "&rm=" + DateTime.Now.ToString(), true); 
    }
}