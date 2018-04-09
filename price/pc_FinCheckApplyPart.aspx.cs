using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pc_FinCheckApplyPart : BasePage
{
    private PC_FinCheckApply helper = new PC_FinCheckApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string part = Request.QueryString["part"];
        string PQID = Request.QueryString["PQID"];
        gvDet.DataSource = helper.GetFinCheckApplyPart(part,PQID);
        gvDet.DataBind();
    }
}