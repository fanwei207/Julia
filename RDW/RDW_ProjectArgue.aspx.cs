using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class RDW_RDW_ProjectArgue : BasePage
{

    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbProjectCode.Text = Request["ProjectCode"].ToString();


            gvbind();
        }
    }

    private void gvbind()
    {
        string keywords = txtKeywords.Text.Trim();

        string sqlstr = "sp_RDW_ProjectArgueListByIDAndKey";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@keywords",keywords)
            , new SqlParameter("@MstrID",Request["mid"].ToString())
        };

        gvMessagereply.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
        gvMessagereply.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvbind();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_DetailList.aspx?ProjectCode=" + Request["ProjectCode"].ToString() 
            + "&mid="+ Request["mid"].ToString() 
          //  + "&projType=" + Request["projType"].ToString() 
          //  + "&region=" + Request["region"].ToString()
          //+ "&ecnCode=" + Request["ecnCode"].ToString() 
          //+ "&oldmID= " + Request["oldmID"].ToString()
          //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
          + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
          + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
          + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
          + "&rm=" + Request["rm"].ToString());
    }
    protected void btnReply_Click(object sender, EventArgs e)
    {
        string url = "../RDW/RDW_ProjectReply.aspx?ProjectCode=" + Request["ProjectCode"].ToString() + "&mid="
          + Request["mid"].ToString() 
          //+ "&projType=" + Request["projType"].ToString() + "&region=" + Request["region"].ToString()
          //+ "&ecnCode=" + Request["ecnCode"].ToString() + "&oldmID= " + Request["oldmID"].ToString()
          //+ "&@__kw=" + Request["@__kw"].ToString() + "&@__ca=" + Request["@__ca"].ToString()
          + "&@__pn=" + Request["@__pn"].ToString() + "&@__pc=" + Request["@__pc"].ToString()
          + "&@__sd=" + Request["@__sd"].ToString() + "&@__st=" + Request["@__st"].ToString()
          + "&@__sk=" + Request["@__sk"].ToString() + "&@__pg=" + Request["@__pg"].ToString()
          + "&rm=" + Request["rm"].ToString();

        ltlAlert.Text = "$.window('Reply', '70%', '80%','" + url + "', '', true);";
        //string test = "$.window('文档查看', '70%', '80%', '../part/NPart_PartVendDocView.aspx?NPartQAD=" + part + "&NPartVendor=', '', false);";
    }
    protected void gvMessagereply_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMessagereply.PageIndex = e.NewPageIndex;
        gvbind();
    }
    protected void gvMessagereply_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadFile")
        {
            ltlAlert.Text = "var w=window.open('" + e.CommandArgument.ToString() + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
}