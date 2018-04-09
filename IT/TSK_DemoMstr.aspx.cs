using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class IT_TSK_DemoMstr : BasePage
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        GridViewBind();
    }
    public void GridViewBind()
    {
        gv.DataSource = selectdemo();
        gv.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    public DataTable selectdemo()
    {
        try
        {
            string strName = "sp_demo_selectDemo";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@title", txtTitle.Text);
            param[1] = new SqlParameter("@menu", txtMenu.Text);
            param[2] = new SqlParameter("@status", ddlstatus.SelectedValue.ToString().Trim());
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string name = "";
        string url = "";

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            url += "<table style=\"width: 100%;\">";
            name = e.Row.Cells[1].Text;

            string[] arr2 = name.Split('#');
            for (int i = 0; i < arr2.Length - 1; i = i + 2)
            {
                url += " <tr>";
                url += "<td align=\"left\">" + arr2[i] + "</td>";
                url += "<td align=\"center\" Width=\"60px\">  <a href=\"TSK_Demo.aspx?detID=" + arr2[i + 1] + "\" ><u>Demo</u></a> </td>";
                url += "<td align=\"center\" Width=\"60px\">  <a href=\"TSK_DemoStage.aspx?detID=" + arr2[i + 1] + "\" ><u>View</u></a> " + "</td>";
                url += "<td align=\"center\" Width=\"60px\">  <a href=\"TSK_DemoNew.aspx?detID=" + arr2[i + 1] + "&title=" + e.Row.Cells[0].Text+"\" ><u>Edit</u></a> " + "</td>";
                
                
                url += " </tr>";
            }

            url += "</table>";
            e.Row.Cells[1].Text = url;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void btn_New_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.showModalDialog('TSK_DemoNew.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 500px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'TSK_DemoMstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString() + "'";

       // Response.Redirect("TSK_DemoNew.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());
    }
}