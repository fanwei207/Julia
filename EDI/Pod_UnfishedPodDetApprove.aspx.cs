using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class plan_PCD_updateApprove : BasePage
{
     private string strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@FlowId", "EDAF9855-7C48-40DD-9683-F13872885185");
        param[1] = new SqlParameter("@Uid", Convert.ToInt32(Session["uID"].ToString()));
        DataTable ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_edi_unfinishedbyperson", param).Tables[0];
        if (ds.Rows.Count > 0)
        {
            string myscript = @"alert('您有采购单未做审批，请先做审批！');window.location.href='/NWF/nwf_workflowInstanceReview.aspx?FlowId=EDAF9855-7C48-40DD-9683-F13872885185';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);  
        }
        else
        {
            Response.Redirect("/NWF/nwf_workflowInstanceReview.aspx?FlowId=a9df4dfa-c4f5-4184-bcc5-7da66edbb3b4", true);
        }
    }
}