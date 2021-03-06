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
using BudgetProcess;
using adamFuncs;

namespace BudgetProcess
{
    public partial class budget_bg_acd : BasePage
    {
        adamClass chk = new adamClass();
        Budget budget = new Budget();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
                string master = Server.UrlDecode(Request.QueryString["master"].ToString());
                string dept = Server.UrlDecode(Request.QueryString["dept"].ToString());
                string acc = Server.UrlDecode(Request.QueryString["acc"].ToString());
                string sub = Server.UrlDecode(Request.QueryString["sub"].ToString());
                string project = Server.UrlDecode(Request.QueryString["project"].ToString());
                string year = Server.UrlDecode(Request.QueryString["year"].ToString());
                string per = Server.UrlDecode(Request.QueryString["per"].ToString());

                GridViewNullData.GridViewDataBind(gvAcd, budget.GetAcd(master, dept, acc, sub, project, year, per));
            }

        }
    }
}