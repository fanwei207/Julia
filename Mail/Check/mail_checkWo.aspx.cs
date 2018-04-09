using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mail_Check_mail_checkWo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string order = Request.QueryString["param"].ToString();
            txtOrder.Text = order;
        }
    }
    public void BindGv()
    {
        gvStatus.DataSource = MailHelper.CheckWoStatus(txtOrder.Text.Trim());
        gvStatus.DataBind();
    }
    protected void btnTrack_Click(object sender, EventArgs e)
    {
        BindGv();
    }
}