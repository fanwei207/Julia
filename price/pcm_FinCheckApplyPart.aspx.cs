using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_FinCheckApplyPart : BasePage
{
    private PCM_FinCheckApply helper = new PCM_FinCheckApply();
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