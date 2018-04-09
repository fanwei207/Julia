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
using Wage;

public partial class HR_hr_CompareATDetail : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

 
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Session["EXHeader1"] = "";
        Session["EXSQL1"] = hr_salary.AttDinerDetailString(Convert.ToInt32(Request["yr"]), Convert.ToInt32(Request["mh"]), Convert.ToInt32(Session["uid"]), Convert.ToInt32(Request["uid"]), Convert.ToInt32(Session["plantcode"]), 0);
        Session["EXTitle1"] = hr_salary.AttDinerDetailString(Convert.ToInt32(Request["yr"]), Convert.ToInt32(Request["mh"]), Convert.ToInt32(Session["uid"]), Convert.ToInt32(Request["uid"]), Convert.ToInt32(Session["plantcode"]), 1); ;

        ltlAlert.Text = "window.open('/public/exportExcel1.aspx', '_blank');";
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("hr_CompareAT.aspx");
    }
    protected void gvCompare_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
