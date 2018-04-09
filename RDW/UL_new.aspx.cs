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

public partial class RDW_UL_new : BasePage
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UL_model = txtDate1.Text.Trim();
        if (txtDate1.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('ULModel is null');";
            return;
        }
        else if (!rdw.selectULmodel(UL_model))
        {
            ltlAlert.Text = "alert('ULModel is exists');";
            return;
        }
       
        string Ul_Enumber = txtUl_Enumber.Text.Trim();
        string UL_Section = txtUL_Section.Text.Trim();
        string UL_DriverJXL = txtUL_DriverJXL.Text.Trim();
        string UL_LEDJXL = txtUL_LEDJXL.Text.Trim();
        string UL_Driverlv = txtDriverlv.Text.Trim();
        string UL_LEDllv = txtLEDllv.Text.Trim();
        string UL_pic = txtUL_pic.Text.Trim();
        string UL_NOTE = txtUL_NOTE.Text.Trim();
        if (rdw.insertULnew(Convert.ToString(Request.QueryString["mid"]), UL_model, Ul_Enumber, UL_Section, UL_DriverJXL, UL_LEDJXL, UL_Driverlv, UL_LEDllv, UL_pic, UL_NOTE, Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        {
            ltlAlert.Text = "alert('保存成功');";
            string strID = Convert.ToString(Request.QueryString["mid"]);
            Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&id=" + strID + "&rm=" + DateTime.Now.ToString(), true);
        }
        else
        {
            ltlAlert.Text = "alert('保存失败,数据已存在');";
        }

         
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&rm=" + DateTime.Now.ToString(), true); 
    }
}