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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.IO;
using RD_WorkFlow;

public partial class RDW_EditStatus : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_projectName.Text = Request.QueryString["nm"].ToString();
            txt_projectCode.Text = Request.QueryString["cd"].ToString();
            BindGridView();
        }
    }
    private void BindGridView()
    {
        DataTable dt = rdw.selectRDWHeaderCancelOrSuspend(Request.QueryString["mid"].ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void Btn_check_Click(object sender, EventArgs e)
    {
        if (ddl_status.SelectedIndex == 0)
        {
            this.Alert("Please choose status!");
            return;
        }
        if (string.IsNullOrEmpty(txt_remark.Text))
        {
            this.Alert("Please fill in the reason!");
            return;
        }
        string str = ddl_status.SelectedValue;
        if (rdw.UpdateRDWHeaderCancel(Request.QueryString["mid"].ToString(), ddl_status.SelectedValue, txt_remark.Text.Trim()))
        {
            BindGridView();
            ltlAlert.Text = "window.close();";
        }
        else
        {
            ltlAlert.Text = "alert('Cancel data error!'); ";
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}
