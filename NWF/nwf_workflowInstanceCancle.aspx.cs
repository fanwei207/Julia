using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class NWF_nfw_workflowInstanceCancle : BasePage
{
    private string strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
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
                else
                {
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
        InitGridView(gvDet, flowId);
        gvDet.PageIndexChanging += new GridViewPageEventHandler(gvDet_PageIndexChanging);
        gvDet.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
        DataTable dt = GetFormDataForResult(flowId, "1", txtCondition.Text.Trim());
        gvDet.DataSource = dt;
        gvDet.DataBind();
        //DataTable dtn = GetFormDataNode(flowId);
        //ddlNode.DataSource = dtn;
        //ddlNode.DataBind();
    }
    protected void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = sender as GridView;
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    public void InitGridView(GridView gv, string flowId)
    {
        gv.Columns.Clear();

        TemplateField temp = new TemplateField();
        HeaderCheckBoxTemplate header = new HeaderCheckBoxTemplate();
        ItemCheckBoxTemplate item = new ItemCheckBoxTemplate();
        temp.HeaderTemplate = header;
        temp.ItemTemplate = item;
        temp.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        temp.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        temp.ItemStyle.Width = 30;
        gv.Columns.Add(temp);

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
    private class HeaderCheckBoxTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            CheckBox ck = new CheckBox();

            ck.ID = "chkAll";
            container.Controls.Add(ck);
        }

    }
    private class ItemCheckBoxTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            CheckBox ck = new CheckBox();
            ck.ID = "chk";
            container.Controls.Add(ck);
        }

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = sender as GridView;
            string id = gv.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
            string returnUrl = HttpUtility.UrlEncode("nwf_workflowInstanceResult.aspx&FlowId=" + ddlWorkFlow.SelectedValue);
            string url = "../NWF/nwf_workflowInstanceDetail.aspx?FlowId=" + ddlWorkFlow.SelectedValue + "&QueryField=Id&QueryValue=" + id + "returnUrl=" + returnUrl;

            e.Row.Attributes.Add("ondblclick", "$.window('明细',1000,800,'" + url.ToString() + "');");
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = sender as GridView;
        gv.PageIndex = e.NewPageIndex;
        BindData();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
        DataTable dtData = GetFormDataForResult(flowId, "1", txtCondition.Text.Trim());

        this.ExportExcel(header.ToString(), dtData, true);
        BindData();
    }
    protected void btnRestart_Click(object sender, EventArgs e)
    {
        if (hidCheck.Value == ";")
        {
            ltlAlert.Text = "alert('请选择数据！');";
        }
        else if (txtRemark.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请填写理由！');";
        }
        else
        {
            //string nodeId = ddlNode.SelectedValue;
            string userId = Session["uID"].ToString();
            DataTable table = GetSelectedData();
           
            string flowId = Request.QueryString["FlowId"];



            int result = Pass(flowId, userId, table, txtRemark.Text.Trim());
            if (result == 1)
            {
                ltlAlert.Text = "alert('修改成功！');";
            }

            else
            {
                ltlAlert.Text = "alert('操作失败,请联系管理员！');";
            }
        }
        BindData();
    }
    private DataTable GetSelectedData()
    {
        string[] indexes = hidCheck.Value.Substring(1, hidCheck.Value.Length - 2).Split(new char[] { ';' });
        DataTable table = new DataTable("FormData");
        foreach (string key in gvDet.DataKeyNames)
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = key;
            table.Columns.Add(column);
        }
        foreach (string index in indexes)
        {
            DataRow row = table.NewRow();
            foreach (string key in gvDet.DataKeyNames)
            {
                object value = gvDet.DataKeys[int.Parse(index)].Values[key];

                if (value.GetType().ToString() == "System.DateTime")
                {
                    DateTime d = Convert.ToDateTime(value);
                    row[key] = d.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                else
                {
                    row[key] = gvDet.DataKeys[int.Parse(index)].Values[key];
                }
            }
            table.Rows.Add(row);
        }
        return table;
    }
    public int Pass(string flowId, string userId, DataTable detail, string remark)
    {
        int result = 0;
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@UserId", userId);
        param[1] = new SqlParameter("@detail", xmlDetail);
        param[2] = new SqlParameter("@remark", remark);
        param[3] = new SqlParameter("@flowId", flowId);
        result = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_CancleFlowNode", param));

        return result;

    }
    public DataTable GetFormDataForResult(string flowId, string status, string condition)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@FlowId", flowId);
        param[1] = new SqlParameter("@status", status);
        param[2] = new SqlParameter("@Condition", condition);
        param[3] = new SqlParameter("@Uid", Session["uID"].ToString());
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectResultReportCancle", param).Tables[0];
    }
    protected void ddlWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCondition.Text = "";
        BindData();
    }
}