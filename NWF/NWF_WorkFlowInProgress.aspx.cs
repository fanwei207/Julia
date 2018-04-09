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

public partial class NWF_NWF_WorkFlowInProgress : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindData();
            //BindFlow();
        }
    }

    //private void BindFlow()
    //{
    //    ddlFlow.Items.Clear();
    //    ddlFlow.DataSource = GetFlows();
    //    ddlFlow.DataBind();
    //    ddlFlow.Items.Insert(0, new ListItem(" -- ", "0"));
    //}

    //private object GetFlows()
    //{
    //    string str = "sp_ass_getFlows";
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@type", 1);

    //    return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str,param).Tables[0];
    //}

    //private void BindInProgressStep()
    //{
    //    ddlStep.Items.Clear();
    //    ddlStep.DataSource = GetInProgressSteps();
    //    ddlStep.DataBind();
    //    ddlStep.Items.Insert(0, new ListItem(" -- ", "0"));
    //}

    //private DataTable GetInProgressSteps()
    //{
    //    string str = "sp_ass_getInProgressSteps";
    //    SqlParameter[] param = new SqlParameter[2];
    //    param[0] = new SqlParameter("@uid", Session["uID"]);
    //    param[1] = new SqlParameter("@flowid", ddlFlow.SelectedValue);

    //    return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    //}

    private void BindData()
    {
        //string flowID = ddlFlow.SelectedValue;
        //string stepID = ddlStep.SelectedValue;
        DataGrid1.DataSource = GetInProgressStepInfo("0","0");
        DataGrid1.DataBind();
    }

    private DataTable GetInProgressStepInfo(string flowID, string stepID)
    {
        string str = "sp_ass_GetInProgressStepInfo";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@flowId", flowID);
        param[1] = new SqlParameter("@setpId", stepID);
        param[2] = new SqlParameter("@uid", Session["uID"]);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    BindData();
    //}
    //protected void ddlStep_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindData();
    //}
    //protected void ddlFlow_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindInProgressStep();
    //}
    protected void DataGrid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;

            string flowID = DataGrid1.DataKeys[index].Values["FlowID"].ToString();
            if(flowID.Contains("/"))
            {
                Response.Redirect(flowID,true);
            }
            else
            {
                Response.Redirect("/NWF/nwf_workflowInstanceReview.aspx?FlowId=" + flowID, true);
            }
        }
    }
}