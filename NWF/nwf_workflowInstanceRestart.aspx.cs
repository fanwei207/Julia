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

public partial class NWF_nfw_workflowInstanceRestart : BasePage
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

            }
            string flowId = ddlWorkFlow.SelectedValue;
            SqlDataReader dtn = GetFormDataNode(flowId);
            ddlNode.Items.Add(new ListItem("--", ""));
            while (dtn.Read())
            {
                ddlNode.Items.Add(new ListItem(dtn["Node_name"].ToString(), dtn["Node_id"].ToString()));
            }

            dtn.Close();
            ddlNode.DataBind();
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
        DataTable dt = GetFormDataForResult(flowId, ddlStatus.SelectedValue, txtCondition.Text.Trim(), ddlNode.SelectedValue);
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
    public DataTable GetFormDataForResult(string flowId, string status, string condition, string nodeid)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@FlowId", flowId);
        param[1] = new SqlParameter("@status", status);
        param[2] = new SqlParameter("@Condition", condition);
        param[3] = new SqlParameter("@nodeid", nodeid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectRestartReport", param).Tables[0];
    }
    public SqlDataReader GetFormDataNode(string flowId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@FlowId", flowId);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_nwf_selectNode", param);
    }
    protected void ddlWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCondition.Text = "";
        BindData();
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


        DataTable dt;
        BoundField col4 = new BoundField();
        col4.DataField = "Node_Name";
        col4.HeaderText = "节点名称";
        gv.Columns.Add(col4);
        dt = helper.GetFlowNode(flowId);

        BoundField col5 = new BoundField();
        col5.DataField = "Noderesult";
        col5.HeaderText = "节点状态";
        gv.Columns.Add(col5);
        dt = helper.GetFlowNode(flowId);

        dt = helper.GetFormDesignByFlow(flowId);
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
        dataKeys.Add("Node_Name");
        dataKeys.Add("Node_Id");
        dataKeys.Add("result");
        BoundField col1 = new BoundField();
        col1.DataField = "result";
        col1.HeaderText = "审批状态";
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


        //foreach (DataRow row in dt.Rows)
        //{

        //    BoundField col = new BoundField();
        //    col.DataField = row["Node_Id"].ToString();
        //    col.HeaderText = row["Node_Name"].ToString();
        //    col.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //    gv.Columns.Add(col);
        //}
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
            string id = gv.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
            string returnUrl = HttpUtility.UrlEncode("nwf_workflowInstanceResult.aspx&FlowId=" + ddlWorkFlow.SelectedValue);
            string url = "../NWF/nwf_workflowInstanceDetail.aspx?FlowId=" + ddlWorkFlow.SelectedValue + "&QueryField=Id&QueryValue=" + id + "returnUrl=" + returnUrl;

            e.Row.Attributes.Add("ondblclick", "$.window('明细',1000,800,'" + url.ToString() + "');");
        }
    }
    //protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        GridView gv = sender as GridView;
    //        if (gv.Attributes["DetailPage"].ToString().Trim() != "")
    //        {
    //            StringBuilder url = new StringBuilder(gv.Attributes["DetailPage"]);
    //            if (gv.DataKeyNames.Length > 0)
    //            {
    //                url.Append("?");
    //                foreach (string key in gv.DataKeyNames)
    //                {
    //                    url.Append(key).Append("=");
    //                    string value = gv.DataKeys[e.Row.RowIndex].Values[key].ToString();
    //                    url.Append(value).Append("&");
    //                }
    //                url.Append("Node_Id=").Append(menuNode.SelectedValue);
    //            }
    //            e.Row.Attributes.Add("ondblclick", "$.window('明细',1000,800,'" + url.ToString() + "', '', true);");
    //        }
    //    }
    //}
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
            string nodeId = ddlNode.SelectedValue;
            string userId = Session["uID"].ToString();
            DataTable table = GetSelectedData();
            string message = "";
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
        result = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_RestartFlowNode", param));

        return result;
    }
   
    public int PassimportNew(string flowId, string userId, DataTable detail, string NodeId)
    {
        int result = 0;
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@UserId", userId);
        param[1] = new SqlParameter("@detail", xmlDetail);
        param[2] = new SqlParameter("@NodeId", NodeId);
        param[3] = new SqlParameter("@flowId", flowId);
        result = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_RestartFlowNodeKeyNew", param));

        return result;
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
    protected void linkDownload_Click(object sender, EventArgs e)
    {
        if (ddlNode.SelectedValue == "")
        {
            ltlAlert.Text = "alert('请选择节点名称！');";
        }
        else
        {
            DataTable dtHeader = GetFormDesign(ddlNode.SelectedValue);
            StringBuilder header = new StringBuilder();
            foreach (DataRow row in dtHeader.Rows)
            {
                header.Append("<b>").Append(row["label"].ToString()).Append("</b>~^");
            }
            DataTable dtData = GetFormDataForApprove(ddlNode.SelectedValue, txtCondition.Text.Trim());

            this.ExportExcel(header.ToString(), dtData, true, ExcelVersion.Excel2003);
            BindData();
        }
    }
    public DataTable GetFormDataForApprove(string nodeId, string condition)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@Condition", condition);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDataByNodekey", param).Tables[0];
    }
    public DataTable GetFormDesign(string nodeId)
    {
        SqlParameter param = new SqlParameter("@NodeId", nodeId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDesignByNodeKEY", param).Tables[0];
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (ddlNode.SelectedValue == "")
        {
            ltlAlert.Text = "alert('请选择节点名称！');";
            return;
        }
        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    string message = "";
                    DataTable dtData = this.GetExcelContents(strFileName);
                    if (Import(ddlNode.SelectedValue, dtData, out message))
                    {
                        ltlAlert.Text = "alert('重启成功！')";
                        BindData();
                    }
                    else
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                        else
                        {
                            DataTable dtHeader = GetFormDesign(ddlNode.SelectedValue);
                            StringBuilder header = new StringBuilder();
                            foreach (DataRow row in dtHeader.Rows)
                            {
                                header.Append("<b>").Append(row["label"].ToString()).Append("</b>~^");
                            }
                            header.Append("<b>错误信息</b>~^");

                            this.ExportExcel(header.ToString(), dtData, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.);";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
        BindData();
    }
    public bool Import(string nodeId, DataTable dtData, out string message)
    {
        bool success = true;
        message = "";
        DataTable table = new DataTable("FormData");
        DataTable dtFormDesign = GetFormDesign(nodeId);
        if (dtData.Columns.Count != dtFormDesign.Rows.Count)
        {
            message = "导入文件的模版不正确，请更新模板再导入!";
            return false;
        }
        else
        {
            for (int i = 0; i <= dtData.Columns.Count - 1; i++)
            {
                if (dtData.Columns[i].ColumnName != dtFormDesign.Rows[i]["label"].ToString())
                {
                    message = "导入文件的模版不正确，请更新模板再导入!";
                    table.Dispose();
                    return false;
                }
                else
                {
                    if ((dtFormDesign.Rows[i]["ReadOnly"].ToString() == "0" || dtFormDesign.Rows[i]["PK"].ToString() == "1"))
                    {
                        DataColumn column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = dtFormDesign.Rows[i]["ColName"].ToString();
                        table.Columns.Add(column);
                    }
                }
            }
            DataColumn col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = "错误信息";
            dtData.Columns.Add(col);
            foreach (DataRow dr in dtData.Rows)
            {
                StringBuilder err = new StringBuilder();
                DataRow row = table.NewRow();
                if (dr["理由"].ToString().Trim().Length == 0)
                {
                    success = false;
                    err.Append("理由不能为空;");
                }
                for (int i = 0; i <= table.Columns.Count - 1; i++)
                {
                    string colName = table.Columns[i].ColumnName;
                    DataRow rowDesign = dtFormDesign.Select("ColName='" + colName + "'")[0];
                    string dataType = rowDesign["DataType"].ToString();
                    string label = rowDesign["label"].ToString();
                    
                    if (dr[label].ToString() != "")
                    {
                        switch (dataType)
                        {
                            case "int":
                                int iCheck = 0;
                                if (!int.TryParse(dr[label].ToString(), out iCheck))
                                {
                                    success = false;
                                    err.Append(label).Append("的值不是整数;");
                                }
                                break;
                            case "float":
                                float fCheck = 0;
                                if (!float.TryParse(dr[label].ToString(), out fCheck))
                                {
                                    success = false;
                                    err.Append(label).Append("的值不是数字;");
                                }
                                break;
                            case "decimal":
                                decimal dCheck = 0;
                                if (!decimal.TryParse(dr[label].ToString(), out dCheck))
                                {
                                    success = false;
                                    err.Append(label).Append("的值不是数字;");
                                }
                                break;
                            case "datetime":
                                DateTime dateCheck;
                                if (!DateTime.TryParse(dr[label].ToString(), out dateCheck))
                                {
                                    success = false;
                                    err.Append(label).Append("的值不是日期;");
                                }
                                break;
                            case "bool":
                                if (dr[label].ToString().Trim().ToUpper() == "Y")
                                {
                                    dr[label] = 1;
                                }
                                else if (dr[label].ToString().Trim().ToUpper() == "N")
                                {
                                    dr[label] = 0;
                                }
                                else
                                {
                                    success = false;
                                    err.Append(label).Append("的值必须是Y或者N;");
                                }
                                break;
                        }
                        row[i] = dr[label];
                    }
                }
                dr["错误信息"] = err.ToString();
                table.Rows.Add(row);
            }
            if (success)
            {
                //int result = 0;
                //StringWriter writer = new StringWriter();
                //table.WriteXml(writer);
                //string xmlDetail = writer.ToString();
                string nod = nodeId;
                string flowId = Request.QueryString["FlowId"];
                string userId = Session["uID"].ToString();
                int result = PassimportNew(flowId, userId, table, nodeId);
                if (result == 1)
                {
                    //ltlAlert.Text = "alert('修改成功！');";
                    return true;
                }

                else
                {
                    ltlAlert.Text = "alert('操作失败,请联系管理员！');";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}