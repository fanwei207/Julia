using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

public partial class TSK_DemoStage : BasePage
{
    protected string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDemoDet(Request.QueryString["detId"]);
        }
    }

    public void GetDemoDet(string detid)
    {
        try
        {
            string strName = "sp_demo_selectDemoDet";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@detId", detid);
            param[1] = new SqlParameter("@isMenu", true);

            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    demoList.InnerHtml += "<li><span class=\"j-demo-portlet\"><a href=\"#" + row["dmd_id"].ToString() + "\">" + row["dmd_menuName"].ToString() + "</a> </span></li>";
                }
            }
            else
            {
                Response.Redirect("TSK_DemoMstr.aspx?from=new&rt=" + DateTime.Now.ToFileTime().ToString());
            }
        }
        catch (Exception)
        {


        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TSK_DemoMstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}