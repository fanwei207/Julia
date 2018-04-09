using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Purchase_Mold_DocList : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["molddetID"] != null)
            {
                Bind();
            }
            
        }
    }

    private void Bind()
    {
        string sqlstr = "sp_mold_selectLockAboutDoc";

        SqlParameter param = new SqlParameter("@molddetID", Request["molddetID"].ToString());

        gvDoc.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
        gvDoc.DataBind();

    }
    protected void DgDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbview")
        {
            string id = e.CommandArgument.ToString();

            string url = "../QadDoc/qad_documentsearch.aspx?docid=" + id;

            ltlAlert.Text = "$.window('文档列表', '70%', '80%','" + url + "', '', true);";


           
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mold_LockList.aspx");
    }
}