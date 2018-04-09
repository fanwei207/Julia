using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;

public partial class TSK_ViewLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbtskNbr.Text = Request.QueryString["tskNbr"];

            BindData();
        }
    }

    protected void BindData()
    {
       

        DataTable table = TaskHelper.SelectTaskLog(lbtskNbr.Text, dropType.SelectedValue, Session["uID"].ToString());
        gvMessagereply.DataSource = table;
        gvMessagereply.DataBind();
       
    }
    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}