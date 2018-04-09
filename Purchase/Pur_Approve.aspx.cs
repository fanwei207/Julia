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

public partial class Purchase_Pur_Approve : BasePage
{
    private string strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@FlowId", "a9df4dfa-c4f5-4184-bcc5-7da66edbb3b4");
        param[1] = new SqlParameter("@Uid", Convert.ToInt32(Session["uID"].ToString()));
        DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_edi_unfinishedByPerson", param).Tables[0];
        if (table.Rows.Count > 0)
        {
            string myscript = @"alert('您有未完结采购单审批待审批，请先对未完结采购单审批做审批！');window.location.href='/NWF/nwf_workflowInstanceReview.aspx?FlowId=a9df4dfa-c4f5-4184-bcc5-7da66edbb3b4';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        else
        {
            table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "qadplan.dbo.sp_pod_selectUnfishedPodDet").Tables[0];
            if (table.Rows.Count > 0)
            {
                string EXTitle = "<b>域</b>~^<b>供应商</b>~^<b>名称</b>~^<b>采购单</b>~^<b>行</b>~^<b>QAD</b>~^250^<b>描述</b>~^<b>数量</b>~^<b>截止日期</b>~^";
                this.ExportExcel(EXTitle, table, false);
            }
            else
            {
                Response.Redirect("/NWF/nwf_workflowInstanceReview.aspx?FlowId=EDAF9855-7C48-40DD-9683-F13872885185", true);
            }
        }
    }
}