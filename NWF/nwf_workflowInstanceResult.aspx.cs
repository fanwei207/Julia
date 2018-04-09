using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class NWF_nwf_workflowInstanceResult : BasePage
{
    private NewWorkflow helper = new NewWorkflow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (BindWorkflow())
            {
                if (Request.QueryString["keyWords"] != null)
                {
                    txtCondition.Text = Request.QueryString["keyWords"];
                    BindData();
                }
                
            }
        }
    }

    private bool BindWorkflow()
    {
        string flowId = Request.QueryString["FlowId"];
        string userId = Session["uID"].ToString();
        if (flowId != null)
        {
            tdWorkFlow.Visible = false;
        }
        DataTable dt = helper.GetWorkflowById(flowId);
        ddlWorkFlow.DataSource = dt;
        ddlWorkFlow.DataBind();
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BindData()
    {
        string flowId = ddlWorkFlow.SelectedValue;
        hidCheck.Value = ";";
        InitGridView(gvDet,flowId);
        DataTable dt = helper.GetFormDataForResult(flowId, ddlStatus.SelectedValue, txtCondition.Text.Trim());
        gvDet.DataSource = dt;
        gvDet.DataBind();
    }

    protected void ddlWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCondition.Text = "";
        BindData();
    }

    public void InitGridView(GridView gv, string flowId)
    {
        gv.Columns.Clear();
        DataTable dt = helper.GetFormDesignByFlow(flowId);
        List<string> dataKeys = new List<string>();
        dataKeys.Add("Id");
        foreach (DataRow row in dt.Rows)
        {

            BoundField col = new BoundField();
            col.DataField = row["ColName"].ToString();
            col.HeaderText = row["label"].ToString();
            if (row["IsSource"].ToString() == "1")
            {
                dataKeys.Add(row["ColName"].ToString());
            }
            gv.Columns.Add(col);
        }
        BoundField col1 = new BoundField();
        col1.DataField = "result";
        col1.HeaderText = "状态";
        gv.Columns.Add(col1);
        BoundField col2 = new BoundField();
        col2.DataField = "WFI_CreatedDate";
        col2.HeaderText = "启动日期";
        gv.Columns.Add(col2);
        BoundField col3 = new BoundField();
        col3.DataField = "FinishDate";
        col3.HeaderText = "结束日期";
        gv.Columns.Add(col3);
        dt = helper.GetFlowNode(flowId);
        foreach (DataRow row in dt.Rows)
        {

            BoundField col = new BoundField();
            col.DataField = row["Node_Id"].ToString();
            col.HeaderText = row["Node_Name"].ToString();
            col.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gv.Columns.Add(col);
        }
        gv.DataKeyNames = dataKeys.ToArray();
        gv.AllowPaging = true;

        gv.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
        gv.PageIndexChanging += new GridViewPageEventHandler(gv_PageIndexChanging);
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = sender as GridView;
            string id=gv.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
            string returnUrl = HttpUtility.UrlEncode("nwf_workflowInstanceResult.aspx&FlowId=" + ddlWorkFlow.SelectedValue);
            string url = "../NWF/nwf_workflowInstanceDetail.aspx?FlowId=" + ddlWorkFlow.SelectedValue + "&QueryField=Id&QueryValue=" + id + "returnUrl=" + returnUrl;

                e.Row.Attributes.Add("ondblclick", "$.window('明细',1000,800,'" + url.ToString() + "');");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = sender as GridView;
        gv.PageIndex = e.NewPageIndex;
        BindData();

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string flowId = Request.QueryString["FlowId"];
        DataTable dtHeader = helper.GetFormDesignByFlow(flowId);
        StringBuilder header = new StringBuilder();
        foreach (DataRow row in dtHeader.Rows)
        {
            header.Append("<b>").Append(row["label"].ToString()).Append("</b>~^");
        }
        header.Append("<b>状态</b>~^<b>启动日期</b>~^<b>结束日期</b>~^");
        dtHeader.Dispose();
        dtHeader = helper.GetFlowNode(flowId);
        foreach (DataRow row in dtHeader.Rows)
        {
            header.Append("<b>").Append(row["Node_Name"].ToString()).Append("</b>~^");
            header.Append("<b>").Append("审批日期").Append("</b>~^");
        }
        DataTable dtData = helper.GetFormDataForResult(flowId, ddlStatus.SelectedValue, txtCondition.Text.Trim());

        this.ExportExcel(header.ToString(), dtData, true);
        BindData();
    }
}