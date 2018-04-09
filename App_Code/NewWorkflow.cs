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

/// <summary>
/// Summary description for NewWorkflow
/// </summary>
public class NewWorkflow
{

    private string strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
	public NewWorkflow()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 根据id来获得工作流模板的信息
    /// </summary>
    /// <param name="id">工作流模板的id</param>
    public SqlDataReader GetWorkFlowTemplateByID(string id)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_nwf_selectWorkFlowByID", param);
    }

    /// <summary>
    /// 判断工作流模板是否为当前用户创建
    /// </summary>
    /// <param name="flowID">工作流模板的id</param>
    /// <param name="uID">用户id</param>
    public bool JudgeWorkFlowIsCreatedBySelf(string flowID, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_ID", flowID);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_judgeWorkFlowIsCreatedBySelf", param));
    }

    /// <summary>
    /// 添加流程步骤
    /// </summary>
    /// <param name="flowID">工作流模板的ID</param>
    /// <param name="name">步骤的名称</param>
    /// <param name="sortorder">序号的Order</param>
    /// <param name="type">步骤类型</param>
    /// <param name="description">步骤的描述</param>
    /// <param name="createdBy">步骤的创建者</param>
    public void AddFlowNode(string flowID, string name, int sortorder, string type, string description, int createdBy, bool Node_email)
    {

        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@Flow_ID", flowID);
        param[1] = new SqlParameter("@Node_Name", name);
        param[2] = new SqlParameter("@Sort_Order", sortorder);
        param[3] = new SqlParameter("@Node_Type", type);
        param[4] = new SqlParameter("@Node_Description", description);
        param[5] = new SqlParameter("@Node_CreatedBy", createdBy);
        param[6] = new SqlParameter("@Node_email", Node_email);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_nwf_insertFlowNode", param);
    }

    /// <summary>
    /// 更新流程步骤
    /// </summary>
    /// <param name="id">步骤的id</param>
    /// <param name="flowID">流程模板ID</param>
    /// <param name="name">步骤的名称</param>
    /// <param name="sortorder">序号的Order</param>
    /// <param name="type">步骤类型</param>
    /// <param name="description">步骤的描述</param>
    /// <param name="modifiedBy">步骤的创建者</param>
    public void UpdateFlowNode(string id, string flowID, string name, int sortorder, string type, string description, int modifiedBy,bool mail = false)
    {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Node_ID", id);
            param[1] = new SqlParameter("@Flow_ID", flowID);
            param[2] = new SqlParameter("@Node_Name", name);
            param[3] = new SqlParameter("@Sort_Order", sortorder);
            param[4] = new SqlParameter("@Node_Type", type);
            param[5] = new SqlParameter("@Node_Description", description);
            param[6] = new SqlParameter("@Node_ModifiedBy", modifiedBy);
            param[7] = new SqlParameter("@Node_mail", mail);
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_nwf_updateFlowNode", param);
    }

    /// <summary>
    /// 删除流程步骤
    /// </summary>
    /// <param name="nodeID">流程步骤的ID</param>
    public int DeleteFlowNode(string nodeID,int userId)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Node_ID", nodeID);
        param[1] = new SqlParameter("@UserId", userId);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_deleteFlowNode", param));
    }

    /// <summary>
    /// 判断工作流模板的节点是否为当前用户创建
    /// </summary>
    /// <param name="nodeID">工作流模板的节点id</param>
    /// <param name="uID">用户id</param>
    public bool JudgeFlowNodeIsCreatedBySelf(string nodeID, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Node_ID", nodeID);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_judgeFlowNodeIsCreatedBySelf", param));
    }

    /// <summary>
    /// 根据id来获得流程步骤的信息
    /// </summary>
    /// <param name="id">流程步骤的id</param>
    public SqlDataReader GetFlowNodeByID(string id)
    {
        SqlParameter param = new SqlParameter("@Node_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_nwf_selectFlowNodeByID", param);
    }

  
    public DataTable GetSourceTableCols(string nodeId)
    {
        SqlParameter param = new SqlParameter("@NodeId", nodeId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectSourceTableColumnInfoByNode", param).Tables[0];
    }

    public DataTable GetSourceTableColsByFlow(string flowId)
    {
        SqlParameter param = new SqlParameter("@FlowId", flowId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectSourceTableColumnInfoByFlow", param).Tables[0];
    }

    public DataTable GetFormCols(string nodeId)
    {
        SqlParameter param = new SqlParameter("@NodeId", nodeId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDataColumnInfoByNode", param).Tables[0];
    }

    public DataTable GetFormColsByFlow(string flowId)
    {
        SqlParameter param = new SqlParameter("@FlowId", flowId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDataColumnInfoByFlow", param).Tables[0];
    }

    public void DeleteFormDesign(string nodeId, string userId)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@UserId", userId);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_nwf_deleteFormDesign", param);
    }

    public void DeleteFormDesignByFlow(string flowId, string userId)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@FlowId", flowId);
        param[1] = new SqlParameter("@UserId", userId);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_nwf_deleteFormDesignByFlow", param);
    }

    public void SaveFormDesign(string nodeId,string flowId, string userId, DataTable sourceTable, DataTable fromTable)
    {
       
        if ((sourceTable != null && sourceTable.Rows.Count > 0) || (fromTable != null && fromTable.Rows.Count > 0))
        {
            if (nodeId != null)
            {
                DeleteFormDesign(nodeId, userId);
            }
            else
            {
                DeleteFormDesignByFlow(flowId, userId);
            }
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
            {
                bulkCopy.DestinationTableName = "dbo.NWF_SourceFormDesign";
                bulkCopy.ColumnMappings.Add("Node_ID", "Node_Id");
                bulkCopy.ColumnMappings.Add("Flow_ID", "Flow_Id");
                bulkCopy.ColumnMappings.Add("ColName", "ColName");
                bulkCopy.ColumnMappings.Add("Label", "label");
                bulkCopy.ColumnMappings.Add("PK", "PK");
                bulkCopy.ColumnMappings.Add("Query", "Query");
                bulkCopy.ColumnMappings.Add("Sort", "Sort");
                bulkCopy.ColumnMappings.Add("hid", "hid");
                bulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                bulkCopy.WriteToServer(sourceTable);
                sourceTable.Dispose();
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
            {
                bulkCopy.DestinationTableName = "dbo.NWF_FormDesign";
                bulkCopy.ColumnMappings.Add("Node_ID", "Node_Id");
                bulkCopy.ColumnMappings.Add("Flow_ID", "Flow_Id");
                bulkCopy.ColumnMappings.Add("ColName", "ColName");
                bulkCopy.ColumnMappings.Add("Label", "Label");
                bulkCopy.ColumnMappings.Add("ReadOnly", "ReadOnly");
                bulkCopy.ColumnMappings.Add("Required", "Required");
                bulkCopy.ColumnMappings.Add("Sort", "Sort");
                bulkCopy.ColumnMappings.Add("hid", "hid");
                bulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                bulkCopy.WriteToServer(fromTable);
                fromTable.Dispose();
            }
        }
    }

    public DataTable GetWorkflow(string userId, string flowId = null)
    {
        SqlParameter[] param = new SqlParameter[2];  
        param[0] = new SqlParameter("@UserId", userId);
        if (flowId != null)
        {
            param[1] = new SqlParameter("@FlowId", flowId);
        }
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectWorkflowByUser", param).Tables[0];
    }

    public DataTable GetWorkflowById(string flowId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Flow_Id", flowId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectWorkflowByID", param).Tables[0];
    }

    public DataTable GetFlowNode(string flowId)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@FlowId", flowId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFlowNode", param).Tables[0];
    }

    public DataTable GetFlowNode(string flowId, string userId)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@FlowId", flowId);
        param[1] = new SqlParameter("@UserId", userId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFlowNodeByUser", param).Tables[0];
    }

    public DataTable GetFormDesign(string nodeId)
    {
        SqlParameter param = new SqlParameter("@NodeId", nodeId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDesignByNode", param).Tables[0];
    }
    public DataTable GetFormDesignReview(string nodeId,string uid)
    {
       // SqlParameter param = new SqlParameter("@NodeId", nodeId);
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@uid", uid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDesignByNodeReview", param).Tables[0];
    }
    public DataTable GetFormDesignByFlow(string flowId)
    {
        SqlParameter param = new SqlParameter("@FlowId", flowId);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDesignByFlow", param).Tables[0];
    }

    public void InitGridView(GridView gv, string nodeId, bool canselect)
    {
        SqlDataReader reader = GetFlowNodeByID(nodeId);
        if (reader.Read())
        {
            gv.Attributes.Add("DetailPage", reader["Node_DetailPageUrl"].ToString());
        }
        reader.Close();
        gv.Columns.Clear();
        if (canselect)
        {
            TemplateField temp = new TemplateField();
            HeaderCheckBoxTemplate header = new HeaderCheckBoxTemplate();
            ItemCheckBoxTemplate item = new ItemCheckBoxTemplate();
            temp.HeaderTemplate = header;
            temp.ItemTemplate = item;
            temp.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.Width = 30;
            gv.Columns.Add(temp);
        }
        
        DataTable dt = GetFormDesign(nodeId);
        List<string> dataKeys = new List<string>();
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
        gv.DataKeyNames = dataKeys.ToArray();
        gv.AllowPaging = true;
        gv.PageSize = 100;
        

    }

    public void InitGridViewReview(GridView gv, string nodeId, bool canselect, string uid)
    {
        SqlDataReader reader = GetFlowNodeByID(nodeId);
        if (reader.Read())
        {
            gv.Attributes.Add("DetailPage", reader["Node_DetailPageUrl"].ToString());
        }
        reader.Close();
        gv.Columns.Clear();
        if (canselect)
        {
            TemplateField temp = new TemplateField();
            HeaderCheckBoxTemplate header = new HeaderCheckBoxTemplate();
            ItemCheckBoxTemplate item = new ItemCheckBoxTemplate();
            temp.HeaderTemplate = header;
            temp.ItemTemplate = item;
            temp.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.Width = 30;
            gv.Columns.Add(temp);
        }

        DataTable dt = GetFormDesignReview(nodeId, uid);
        List<string> dataKeys = new List<string>();
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
        gv.DataKeyNames = dataKeys.ToArray();
        gv.AllowPaging = true;
        gv.PageSize = 100;


    }

    public DataTable GetFormDataForApprove(string nodeId, string condition)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@Condition", condition);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDataByNode", param).Tables[0];
    }

    public DataTable GetFormDataForResult(string flowId, string status, string condition)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@FlowId", flowId);
        param[1] = new SqlParameter("@status", status);
        param[2] = new SqlParameter("@Condition", condition);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectResultReport", param).Tables[0];
    }

    public DataTable GetFormData(string nodeId, string condition, string queryField, string queryValue,string uid)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@Condition", condition);
        if (queryField != null)
        {
            param[2] = new SqlParameter("@QueryField", queryField);
        }
        if (queryValue != null)
        {
            param[3] = new SqlParameter("@QueryValue", queryValue);
        }
        param[4] = new SqlParameter("@uid", uid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_nwf_selectFormDataByNodeForDetail", param).Tables[0];
    }

    public int  Pass(string nodeId, string userId, DataTable detail,out string message)
    {
        int result = 0;
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@UserId", userId);
        param[2] = new SqlParameter("@detail", xmlDetail);
        param[3] = new SqlParameter("@message", SqlDbType.NVarChar, 100);
        param[3].Direction = ParameterDirection.Output;

        result = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_passFlowNode", param));
        message = param[3].Value.ToString();
        return result;
    }

    public int PassForURL(string nodeId, string userId, DataTable detail, out string message)
    {
        int result = 0;
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@UserId", userId);
        param[2] = new SqlParameter("@detail", xmlDetail);
        param[3] = new SqlParameter("@message", SqlDbType.NVarChar, 100);
        param[3].Direction = ParameterDirection.Output;

        result = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_passFlowNodeForURL", param));
        message = param[3].Value.ToString();
        return result;
    }
    public int Fail(string nodeId, string userId, DataTable detail, out string message)
    {
        int result = 0;
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@UserId", userId);
        param[2] = new SqlParameter("@detail", xmlDetail);
        param[3] = new SqlParameter("@message", SqlDbType.NVarChar, 100);
        param[3].Direction = ParameterDirection.Output;
        result = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_failFlowNode", param));
        message = param[3].Value.ToString();
        return result;
    }

    public int FailForUrl(string nodeId, string userId, DataTable detail, out string message)
    {
        int result = 0;
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@UserId", userId);
        param[2] = new SqlParameter("@detail", xmlDetail);
        param[3] = new SqlParameter("@message", SqlDbType.NVarChar, 100);
        param[3].Direction = ParameterDirection.Output;
        result = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_failFlowNodeForURL", param));
        message = param[3].Value.ToString();
        return result;
    }
    public bool UpdateFormData(string nodeId, DataTable detail)
    {
        StringWriter writer = new StringWriter();
        detail.WriteXml(writer);
        string xmlDetail = writer.ToString();

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@detail", xmlDetail);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_nwf_updateFormDataByNode", param));
    }

    public bool Import(string nodeId, DataTable dtData,out string message)
    {
        bool success = true;
        message = "";
        DataTable table=new DataTable("FormData");
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
                                int iCheck=0;
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
                if (UpdateFormData(nodeId, table))
                {
                    return true;
                }
                else
                {
                    message = "操作失败，请联系管理员！";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public void RestartWorkflow(string id, string uID,string nodeId,string reason)
    {
        string strSQL = "sp_nwf_restartWorkflow";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@SourceId", id);
        parm[1] = new SqlParameter("@nodeId", nodeId);
        parm[2] = new SqlParameter("@UserId", uID);
        parm[3] = new SqlParameter("@Reason", reason);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSQL, parm);
    }

    public void RestartWorkflowForAll(string id, string uID, string nodeId, string reason)
    {
        string strSQL = "sp_nwf_restartWorkflowForAll";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@SourceId", id);
        parm[1] = new SqlParameter("@nodeId", nodeId);
        parm[2] = new SqlParameter("@UserId", uID);
        parm[3] = new SqlParameter("@Reason", reason);

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSQL, parm);
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
}

