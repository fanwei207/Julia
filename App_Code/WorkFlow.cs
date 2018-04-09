using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using adamFuncs;

/// <summary>
/// 工作流的操作集合
/// </summary>
public class WorkFlow
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
	public WorkFlow()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region 工作流模板
    /// <summary>
    /// 添加工作流模板
    /// </summary>
    /// <param name="name">工作流模板的名称</param>
    /// <param name="pre">工作流模板的前缀</param>
    /// <param name="domain">工作流模板的域</param>
    /// <param name="description">工作流模板的描述</param>
    /// <param name="formname">工作流模板表单的名称</param>
    /// <param name="formtype">工作流模板表单的格式</param>
    /// <param name="formcontent">工作流模板表单的内容</param>
    /// <param name="remark">工作流模板的备注</param>
    /// <param name="status">工作流模板的状态</param>
    /// <param name="createdBy">工作流模板的创建者</param>
    public int AddWorkFlowTemplate(string name, string pre, int domain, string description, string formname, string formtype, byte[] formcontent, string remark, bool status, int createdBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Flow_Name", name);
            param[1] = new SqlParameter("@Flow_Req_Pre", pre);
            param[2] = new SqlParameter("@Flow_Domain", domain);
            param[3] = new SqlParameter("@Flow_Description", description);
            param[4] = new SqlParameter("@Flow_FormTemplateName", formname);
            param[5] = new SqlParameter("@Flow_FormTemplateFormat", formtype);
            param[6] = new SqlParameter("@Flow_FormTemplateContent", formcontent);
            param[7] = new SqlParameter("@Flow_Remark", remark);
            param[8] = new SqlParameter("@Flow_Status", status);
            param[9] = new SqlParameter("@Flow_CreatedBy", createdBy);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_insertWorkFlow", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 显示及查找工作流模板
    /// </summary>
    /// <param name="name">工作流模板的名称</param>
    /// <param name="username">工作流模板创建者的名称</param>
    /// <param name="createdDate1">工作流模板的创建日期检索条件1</param>
    /// <param name="createdDate2">工作流模板的创建日期检索条件2</param>
    /// <param name="domain">工作流模板的域</param>
    public DataTable GetWorkFlowTemplate(string name, string username, string createdDate1, string createdDate2, bool status, int domain)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@Flow_Name", name);
        param[1] = new SqlParameter("@Flow_CreatedBy", username);
        param[2] = new SqlParameter("@Flow_CreatedDate1", createdDate1);
        param[3] = new SqlParameter("@Flow_CreatedDate2", createdDate2);
        param[4] = new SqlParameter("@Flow_Status", status);
        param[5] = new SqlParameter("@Flow_Domain", domain);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlow", param).Tables[0];
    }

    /// <summary>
    /// 删除工作流模板
    /// </summary>
    /// <param name="id">工作流模板的id</param>
    public int DeleteWorkFlowTemplate(int id)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", id);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_deleteWorkFlow", param));
    }

    /// <summary>
    /// 根据id来获得工作流模板的信息
    /// </summary>
    /// <param name="id">工作流模板的id</param>
    public SqlDataReader GetWorkFlowTemplateByID(int id)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowByID", param);
    }

    /// <summary>
    /// 更新工作流模板（表单模板也更换）
    /// </summary>
    /// <param name="id">工作流模板的id</param>
    /// <param name="name">工作流模板的名称</param>
    /// <param name="pre">工作流模板的前缀</param>
    /// <param name="description">工作流模板的描述</param>
    /// <param name="formname">工作流模板表单的名称</param>
    /// <param name="formtype">工作流模板表单的格式</param>
    /// <param name="formcontent">工作流模板表单的内容</param>
    /// <param name="remark">工作流模板的备注</param>
    /// <param name="status">工作流模板的状态</param>
    /// <param name="modifiedBy">工作流模板的修改者</param>
    public int UpdateWorkFlowTemplateWithFile(int id, string name, string pre, string description, string formname, string formtype, byte[] formcontent, string remark, bool status, int modifiedBy, bool sotpTemp = false)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Flow_ID", id);
            param[1] = new SqlParameter("@Flow_Name", name);
            param[2] = new SqlParameter("@Flow_Req_Pre", pre);
            param[3] = new SqlParameter("@Flow_Description", description);
            param[4] = new SqlParameter("@Flow_FormTemplateName", formname);
            param[5] = new SqlParameter("@Flow_FormTemplateFormat", formtype);
            param[6] = new SqlParameter("@Flow_FormTemplateContent", formcontent);
            param[7] = new SqlParameter("@Flow_Remark", remark);
            param[8] = new SqlParameter("@Flow_Status", status);
            param[9] = new SqlParameter("@Flow_ModifiedBy", modifiedBy);
            param[10] = new SqlParameter("@Flow_StopFormTemplate", sotpTemp);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateWorkFlowWithFile", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 更新工作流模板（表单模板不更换）
    /// </summary>
    /// <param name="id">工作流模板的id</param>
    /// <param name="name">工作流模板的名称</param>
    /// <param name="pre">工作流模板的前缀</param>
    /// <param name="description">工作流模板的描述</param>
    /// <param name="formname">工作流模板表单的名称</param>
    /// <param name="remark">工作流模板的备注</param>
    /// <param name="status">工作流模板的状态</param>
    /// <param name="modifiedBy">工作流模板的修改者</param>
    public int UpdateWorkFlowTemplateWithNoFile(int id, string name, string pre, string description, string formname, string remark, bool status, int modifiedBy,bool sotpTemp=false)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Flow_ID", id);
            param[1] = new SqlParameter("@Flow_Name", name);
            param[2] = new SqlParameter("@Flow_Req_Pre", pre);
            param[3] = new SqlParameter("@Flow_Description", description);
            param[4] = new SqlParameter("@Flow_FormTemplateName", formname);
            param[5] = new SqlParameter("@Flow_Remark", remark);
            param[6] = new SqlParameter("@Flow_Status", status);
            param[7] = new SqlParameter("@Flow_ModifiedBy", modifiedBy);
            param[8] = new SqlParameter("@Flow_StopFormTemplate", sotpTemp);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateWorkFlowWithNoFile", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 更新工作流模板的表单
    /// </summary>
    /// <param name="id">工作流模板的id</param>
    /// <param name="formcontent">工作流模板表单的内容</param>
    public bool UpdateWorkFlowFile(int id, byte[] formcontent)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_ID", id);
        param[1] = new SqlParameter("@Flow_FormTemplateContent", formcontent);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateWorkFlowFile", param));
    }

    /// <summary>
    /// 根据工作流模板名称来获得工作流模板的ID
    /// </summary>
    /// <param name="flowName">工作流模板的名称</param>
    public SqlDataReader GetWorkFlowTemplateIDByName(string flowName,int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_Name", flowName);
        param[1] = new SqlParameter("@plantCode", plantCode);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowIDByName", param);
    }
    #endregion

    #region 工作流模板节点序号
    /// <summary>
    /// 添加节点序号
    /// </summary>
    /// <param name="sortName">序号名称</param>
    /// <param name="createdBy">节点序号的创建者</param>
    public int AddNodeSort(string sortName,int createdBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Sort_Name", sortName);
            param[1] = new SqlParameter("@Sort_CreatedBy", createdBy);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_insertNodeSort", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 更新节点序号
    /// </summary>
    /// <param name="id">节点序号的id</param>
    /// <param name="name">节点序号的名称</param>
    public int UpdateNodeSort(int id, string name)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Sort_ID", id);
            param[1] = new SqlParameter("@Sort_Name", name);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateNodeSort", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 删除节点序号
    /// </summary>
    /// <param name="id">节点序号的id</param>
    public int DeleteNodeSort(int id)
    {
        try
        {
            SqlParameter param = new SqlParameter("@Sort_ID", id);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_deleteNodeSort", param));
        }
        catch
        {
            return -1;
        }  
    }

    /// <summary>
    /// 显示节点序号
    /// </summary>
    public DataTable GetNodeSort()
    {
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectNodeSort").Tables[0];
    }

    /// <summary>
    /// 查找某节点序号的信息
    /// </summary>
    /// <param name="id">节点序号的id</param>
    public SqlDataReader GetNodeSortByID(int id)
    {
        SqlParameter param = new SqlParameter("@Sort_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectNodeSortByID", param);
    }
    #endregion

    #region 工作流模板步骤
    /// <summary>
    /// 添加流程步骤
    /// </summary>
    /// <param name="flowID">工作流模板的ID</param>
    /// <param name="name">步骤的名称</param>
    /// <param name="sortorder">序号的Order</param>
    /// <param name="sortname">序号的名称</param>
    /// <param name="description">步骤的描述</param>
    /// <param name="createdBy">步骤的创建者</param>
    public int AddFlowNode(int flowID, string name, int sortorder, string sortname, string description, int createdBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Flow_ID", flowID);
            param[1] = new SqlParameter("@Node_Name", name);
            param[2] = new SqlParameter("@Sort_Order", sortorder);
            param[3] = new SqlParameter("@Sort_Name", sortname);
            param[4] = new SqlParameter("@Node_Description", description);
            param[5] = new SqlParameter("@Node_CreatedBy", createdBy);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_insertFlowNode", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 更新流程步骤
    /// </summary>
    /// <param name="id">步骤的id</param>
    /// <param name="flowID">流程模板ID</param>
    /// <param name="name">步骤的名称</param>
    /// <param name="sortorder">序号的Order</param>
    /// <param name="sortname">序号的名称</param>
    /// <param name="description">步骤的描述</param>
    /// <param name="modifiedBy">步骤的创建者</param>
    public int UpdateFlowNode(int id, int flowID, string name, int sortorder, string sortname, string description, int modifiedBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Node_ID", id);
            param[1] = new SqlParameter("@Flow_ID", flowID);
            param[2] = new SqlParameter("@Node_Name", name);
            param[3] = new SqlParameter("@Sort_Order", sortorder);
            param[4] = new SqlParameter("@Sort_Name", sortname);
            param[5] = new SqlParameter("@Node_Description", description);
            param[6] = new SqlParameter("@Node_ModifiedBy", modifiedBy);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateFlowNode", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 显示流程步骤
    /// </summary>
    /// <param name="flowID">工作流模板的ID</param>
    public DataTable GetFlowNode(int flowID)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", flowID);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNode", param).Tables[0];
    }

    /// <summary>
    /// 删除流程步骤
    /// </summary>
    /// <param name="nodeID">流程步骤的ID</param>
    /// <param name="flowID">流程模板的ID</param>
    public int DeleteFlowNode(int nodeID, int flowID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Node_ID", nodeID);
        param[1] = new SqlParameter("@Flow_ID", flowID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_deleteFlowNode", param));
    }

    /// <summary>
    /// 根据id来获得流程步骤的信息
    /// </summary>
    /// <param name="id">流程步骤的id</param>
    public SqlDataReader GetFlowNodeByID(int id)
    {
        SqlParameter param = new SqlParameter("@Node_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeByID", param);
    }
    #endregion

    #region 工作流模板步骤添加岗位或人员
    /// <summary>
    /// 获取公司
    /// </summary>
    public DataTable GetPlant()
    {
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectPlant").Tables[0];
    }

    /// <summary>
    /// 获取公司下的部门
    /// </summary>
    /// <param name="plantid">公司的ID</param>
    public DataTable GetDept(int plantid)
    {
        SqlParameter param = new SqlParameter("@plantid", plantid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectDept", param).Tables[0];
    }

    /// <summary>
    /// 获取公司某部门下的所有在职员工
    /// </summary>
    /// <param name="plantid">公司的ID</param>
    /// <param name="deptid">部门的ID</param>
    public DataTable GetUser(int plantid, int deptid)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@plantID", plantid);
        param[1] = new SqlParameter("@deptID", deptid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectUser", param).Tables[0];
    }

    /// <summary>
    /// 获取公司所有角色
    /// </summary>
    /// <param name="plantid">公司的ID</param>
    public DataTable GetRole(int plantid)
    {
        SqlParameter param = new SqlParameter("@plantID", plantid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectRole", param).Tables[0];
    }

    /// <summary>
    /// 修改岗位或人员到工作流模板步骤先判断是否有未审批表单
    /// </summary>
    /// <param name="nodeID">工作流模板步骤的ID</param>
    /// <param name="flowID">工作流模板的ID</param>
    /// <param name="type">添加岗位或人员的类型</param>
    /// <param name="objectID">添加岗位或人员的ID</param>
    /// <param name="createdBy">创建者</param>
    public DataTable CheckNodePersonUnfinished(int nodeID, int plantid)
    {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Node_ID", nodeID);
            param[1] = new SqlParameter("@plantCode", plantid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_CheckNodePersonUnfinished", param).Tables[0];
    }

    /// <summary>
    /// 添加岗位或人员到工作流模板步骤
    /// </summary>
    /// <param name="nodeID">工作流模板步骤的ID</param>
    /// <param name="flowID">工作流模板的ID</param>
    /// <param name="type">添加岗位或人员的类型</param>
    /// <param name="objectID">添加岗位或人员的ID</param>
    /// <param name="createdBy">创建者</param>
    public int AddNodePerson(int nodeID,int type, string objectID, int createdBy, int plantid)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Node_ID", nodeID);
            param[1] = new SqlParameter("@NodePerson_Type", type);
            param[2] = new SqlParameter("@NodePerson_ObjectID", objectID);
            param[3] = new SqlParameter("@NodePerson_CreatedBy", createdBy);
            param[4] = new SqlParameter("@plantid", plantid);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_insertNodePerson", param).ToString());
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 删除工作流模板步骤岗位或人员
    /// </summary>
    /// <param name="nodeID">工作流模板步骤的ID</param>
    public void DeleteNodePerson(int nodeID)
    {
        SqlParameter param = new SqlParameter("@Node_ID", nodeID);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wf_deleteNodePerson", param);
    }

    #endregion

    #region 工作流实例
    /// <summary>
    /// 显示该域下的工作流模板
    /// </summary>
    /// <param name="plantCode">域</param>
    public DataTable GetWorkFlowTemplateByDomain(int plantCode)
    {
        SqlParameter param = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowByDomain", param).Tables[0];
    }

    /// <summary>
    /// 添加一个空的工作流实例
    /// </summary>
    /// <param name="flowID">工作流模板的ID</param>
    /// <param name="plantCode">员工所在域</param>
    /// <param name="createdBy">创建者</param>
    public string AddEmptyWorkFlowInstance(int flowID, int plantCode, int createdBy)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@Flow_ID", flowID);
        param[1] = new SqlParameter("@WFN_PlantCode", plantCode);
        param[2] = new SqlParameter("@WFN_CreatedBy", createdBy);
        param[3] = new SqlParameter("@WFN_Nbr", SqlDbType.VarChar, 20);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wf_insertEmptyWorkFlowInstance", param);

        return param[3].Value.ToString();
    }

    /// <summary>
    /// 显示刚创建的空的工作流实例信息
    /// </summary>
    /// <param name="wfn_nbr">工作流实例的ID</param>
    /// <param name="plantcode">域</param>
    public SqlDataReader GetWorkFlowInstanceByNbr(string wfn_nbr, int plantcode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfn_nbr);
        param[1] = new SqlParameter("@plantcode", plantcode);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowInstanceByNbr", param);
    }

    /// <summary>
    /// 显示工作流节点实例的第一个未审批的步骤序号
    /// </summary>
    /// <param name="wfnNbr">工作流实例的申请号</param>
    public SqlDataReader GetFirstUnReviewNodeSort(string wfnNbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", wfnNbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFirstUnReviewNodeSort", param);
    }

    public SqlDataReader GetNodeSortDesc(string wfnNbr, string nodeSort)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@nodeSort", nodeSort);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectNodeSortDesc", param);
    }

    /// <summary>
    /// 激活刚创建的工作流实例
    /// </summary>
    /// <param name="wfnNbr">工作流实例的申请号</param>
    /// <param name="wfnReqDate">工作流实例的需求日期</param>
    /// <param name="wfnDueDate">工作流实例的截止日期</param>
    public bool ActiveWorkFlowInstance(string wfnNbr, string wfnReqDate, string wfnDueDate)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@WFN_ReqDate", wfnReqDate);
        param[2] = new SqlParameter("@WFN_DueDate", wfnDueDate);
        

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_activeWorkFlowInstance", param));
    }

    /// <summary>
    /// 更新工作流节点实例
    /// </summary>
    /// <param name="wfnNbr">工作流实例的申请号</param>
    /// <param name="sortOrder">工作流实例节点的序号</param>
    /// <param name="fniRemark">工作流节点实例的备注</param>
    /// <param name="fniStatus">工作流节点实例的状态</param>
    /// <param name="uid">操作人</param>
    public bool UpdateFlowNodeInstance(string wfnNbr, int sortOrder, string fniRemark, int fniStatus, int uid,string sql)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);
        param[2] = new SqlParameter("@FNI_Remark", fniRemark);
        param[3] = new SqlParameter("@FNI_Status", fniStatus);
        param[4] = new SqlParameter("@FNI_RunID", uid);
        param[5] = new SqlParameter("@sql", sql);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateFlowNodeInstance", param));
    }

    /// <summary>
    /// 工作流节点实例的附件上传
    /// </summary>
    /// <param name="wfnNbr">工作流实例的申请号</param>
    /// <param name="sortOrder">工作流实例节点的序号</param>
    /// <param name="attachName">上传附件的名称</param>
    /// <param name="attachType">上传附件的格式</param>
    /// <param name="attachContent">上传附件的内容</param>
    /// <param name="attachCreatedBy">上传者</param>
    public bool UploadFniAttachment(string wfnNbr, int sortOrder, string attachName, string attachType, byte[] attachContent, int attachCreatedBy)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);
        param[2] = new SqlParameter("@Attach_Name", attachName);
        param[3] = new SqlParameter("@Attach_Type", attachType);
        param[4] = new SqlParameter("@Attach_Content", attachContent);
        param[5] = new SqlParameter("@Attach_CreatedBy", attachCreatedBy);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_uploadFniAttach", param));
    }

    /// <summary>
    /// 显示该流程实例下的工作流节点实例
    /// </summary>
    /// <param name="wfnNbr">工作流实例的申请号</param>
    /// <param name="plantCode">域</param>
    public DataTable GetFlowNodeInstanceTouched(string wfnNbr, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceTouched", param).Tables[0];
    }

    /// <summary>
    /// 显示工作流实例上传的附件
    /// </summary>
    /// <param name="nbr">工作流节点实例的ID</param>
    /// <param name="sortOrder">工作流节点实例序号</param>
    public DataTable GetFniAttach(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFniAttach", param).Tables[0];
    }

    /// <summary>
    /// 删除工作流节点实例上传的附件
    /// </summary>
    /// <param name="attachID">工作流节点附件的ID</param>
    public void DeleteFniAttach(int attachID)
    {
        SqlParameter param = new SqlParameter("@Attach_ID", attachID);

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wf_DeleteFniAttach", param);
    }

    /// <summary>
    /// 读取工作流节点实例上传的附件
    /// </summary>
    /// <param name="attachID">工作流节点附件的ID</param>
    public SqlDataReader GetAttachDetail(int attachID)
    {
        SqlParameter param = new SqlParameter("@Attach_ID", attachID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectAttachDetail", param);
    }

    /// <summary>
    /// 读取自己创建的工作流实例
    /// </summary>
    /// <param name="wfnNbr">工作流实例的编号</param>
    /// <param name="flowID">工作流模板的编号</param>
    /// <param name="reqDate1">工作流实例的需求日期1</param>
    /// <param name="uID">用户的ID</param>
    /// <param name="status">工作流实例的状态</param>
    public DataTable GetWorkFlowInstanceCreatedBySelf(string wfnNbr, int flowID, string reqDate1, int uID, int status)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@Flow_ID", flowID);
        param[2] = new SqlParameter("@req_date1", reqDate1);
        param[3] = new SqlParameter("@uID", uID);
        param[4] = new SqlParameter("@status", status);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowInstanceCreatedBySelf", param).Tables[0];
    }

    /// <summary>
    /// 管理员查看员工创建的工作流实例详细
    /// </summary>
    /// <param name="wfnNbr">工作流实例的编号</param>
    /// <param name="flowID">工作流模板的编号</param>
    /// <param name="reqDate1">工作流实例的需求日期1</param>
    /// <param name="uName">用户的姓名</param>
    /// <param name="status">工作流实例的状态</param>
    /// <param name="domain">域</param>
    public DataTable GetWorkFlowInstanceDetailCreatedByAdmin(string wfnNbr, int flowID, string reqDate1, string uName, int status, int domain)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@Flow_ID", flowID);
        param[2] = new SqlParameter("@req_date1", reqDate1);
        param[3] = new SqlParameter("@uName", uName);
        param[4] = new SqlParameter("@status", status);
        param[5] = new SqlParameter("@domain", domain);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowInstanceDetailByAdmin", param).Tables[0];
    }

    /// <summary>
    /// 管理员查看员工创建的工作流实例详细
    /// </summary>
    /// <param name="wfnNbr">工作流实例的编号</param>
    /// <param name="flowID">工作流模板的编号</param>
    /// <param name="reqDate1">工作流实例的需求日期1</param>
    /// <param name="uName">用户的姓名</param>
    /// <param name="status">工作流实例的状态</param>
    /// <param name="domain">域</param>
    public DataTable GetWorkFlowInstanceCreatedByAdmin(string wfnNbr, int flowID, string reqDate1, string uName, int status, int domain)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@Flow_ID", flowID);
        param[2] = new SqlParameter("@req_date1", reqDate1);
        param[3] = new SqlParameter("@uName", uName);
        param[4] = new SqlParameter("@status", status);
        param[5] = new SqlParameter("@domain", domain);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowInstanceByAdmin", param).Tables[0];
    }

    /// <summary>
    /// 读取自己创建的工作流节点实例
    /// </summary>
    /// <param name="wfnNbr">工作流实例的编号</param>
    /// <param name="uID">用户的ID</param>
    public SqlDataReader GetFlowNodeInstanceCreatedBySelf(string wfnNbr, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceCreatedBySelf", param);
    }

    /// <summary>
    /// 读取审批过的工作流节点实例
    /// </summary>
    /// <param name="wfnNbr">工作流实例的编号</param>
    /// <param name="uID">用户的ID</param>
    public SqlDataReader GetFlowNodeInstanceApprovedBySelf(string wfnNbr, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceApprovedBySelf", param);
    }


    /// <summary>
    /// 读取跟自己有关的工作流实例
    /// </summary>
    /// <param name="wfnNbr">工作流实例的编号</param>
    /// <param name="flowID">工作流模板的编号</param>
    /// <param name="reqDate1">工作流实例的需求日期1</param>
    /// <param name="uID">用户的ID</param>
    /// <param name="uRole">用户的角色</param>
    /// <param name="status">工作流实例的状态</param>
    public DataTable GetWorkFlowInstanceWithSelf(string wfnNbr, int flowID, string reqDate1, int uID, string roleName, int status, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@WFN_Nbr",wfnNbr);
        param[1] = new SqlParameter("@Flow_ID",flowID);
        param[2] = new SqlParameter("@req_date1",reqDate1);
        param[3] = new SqlParameter("@uID", uID);
        param[4] = new SqlParameter("@roleName", roleName);
        param[5] = new SqlParameter("@status", status);
        param[6] = new SqlParameter("@plantCode", plantCode);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowInstanceWithSelf", param).Tables[0];
    }

    /// <summary>
    /// 更新工作流实例的表单
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    /// <param name="formcontent">工作流实例表单的内容</param>
    public bool UpdateWorkFlowInstanceFile(string nbr, byte[] formcontent)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@WFN_FormContent", formcontent);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateWorkFlowInstanceFile", param));
    }

    /// <summary>
    /// 判断申请者是否有修改的权限
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    /// <param name="uID">用户ID</param>
    public bool JudgeIsCanModifiedWithApplier(string nbr, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeIsCanModifiedWithApplier", param));
    }

    /// <summary>
    /// 判断工作流实例是否为激活状态
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    public SqlDataReader JudgeWorkFlowInstanceIsActive(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr",nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_judgeWorkFlowInstanceIsActive", param);
    }

    /// <summary>
    /// 判断审批者是否有修改的权限
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    /// <param name="sortOrder">序号</param>
    public bool JudgeIsCanModifiedWithApprover(string nbr, int sortOrder, int uID)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);
        param[2] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeIsCanModifiedWithApprover", param));
    }

    /// <summary>
    /// 获取工作流步骤实例的信息
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    /// <param name="sortOrder">序号</param>
    public SqlDataReader GetFNIInfoByNbrAndSort(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFNIInfoByNbrAndSort", param);
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="uID">用户ID</param>
    public SqlDataReader GetUserInfo(int uID)
    {
        SqlParameter param = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectUserInfo", param);
    }

    /// <summary>
    /// 判断用户是否具有申请该流程的权限
    /// </summary>
    /// <param name="flowID">流程ID</param>
    /// <param name="uID">用户ID</param>
    /// <param name="roleID">用户角色ID</param>
    public bool JudgeIsCanCreatedWithApplier(int flowID, int uID, string roleName)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@flowID", flowID);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@roleName", roleName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeIsCanCreatedWithApplier", param));
    }

    /// <summary>
    /// 获取工作流节点实例的步骤名称
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    public DataTable GetFlowNodeInfo(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInfo", param).Tables[0];
    }

    /// <summary>
    /// 获取工作流节点实例的最后一个序号
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    public SqlDataReader GetFlowNodeInstanceLastSort(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceLastSort", param);
    }

    /// <summary>
    /// 获取工作流节点实例已审批过的最后一个序号
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    public SqlDataReader GetFlowNodeInstanceApprovedLastSort(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceApprovedLastSort", param);
    }

    /// <summary>
    /// 获取下一个审批人的邮件
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    /// <param name="sortOrder">工作流节点实例的序号</param>
    public DataTable GetNextApproverEmail(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectNextApproverEmail", param).Tables[0];
    }

    /// <summary>
    /// 获取以前审批人的邮件
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    /// <param name="sortOrder">工作流节点实例的序号</param>
    public DataTable GetPerApproverEmail(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectPerApproverEmail", param).Tables[0];
    }

    /// <summary>
    /// 获取申请者的邮件
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    public SqlDataReader GetApplierEmail(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectApplierEmail", param);
    }

    /// <summary>
    /// 判断用户的表单是否填写
    /// </summary>
    /// <param name="nbr">工作流实例的编号</param>
    public bool JudgeExcelIsFill(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeExcelIsFill", param));
    }

    /// <summary>
    /// 获取工作流的名称
    /// </summary>
    /// <param name="plantCode">域</param>
    /// <param name="pre">流程代码</param>
    public SqlDataReader GetWorkFlowNameByDomainAndPre(int plantCode, string pre)
    {
        SqlParameter[] param= new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_Domain", plantCode);
        param[1] = new SqlParameter("@Flow_Req_Pre", pre);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowNameByDomainAndPre", param);
    }

    /// <summary>
    /// 通过流的方式获取文件的真正后缀。防止只改变文件的后缀
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns>后缀</returns>
    public string GetFileExtension(string filepath)
    {
        System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
        string fileclass = string.Empty;
        //这里的位长要具体判断.
        byte buffer;
        try
        {
            buffer = r.ReadByte();
            fileclass = buffer.ToString();
            buffer = r.ReadByte();
            fileclass += buffer.ToString();

        }
        catch
        {

        }

        r.Close();
        fs.Close();

        return fileclass;
    }

    /// <summary>
    /// 通过流的方式判断上传文件的真正格式
    /// </summary>
    /// <param name="hifile"></param>
    /// <returns>bool</returns>
    public bool IsAllowedExtension(HtmlInputFile hifile)
    {
        System.IO.FileStream fs = new System.IO.FileStream(hifile.PostedFile.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
        string fileclass = "";
        //这里的位长要具体判断.
        byte buffer;
        try
        {
            buffer = r.ReadByte();
            fileclass = buffer.ToString();
            buffer = r.ReadByte();
            fileclass += buffer.ToString();
        }
        catch
        {
        }
        r.Close();
        fs.Close();
        if (fileclass == "6063")//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar,6063是xml
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 判断工作流模板是否为当前用户创建
    /// </summary>
    /// <param name="flowID">工作流模板的id</param>
    /// <param name="uID">用户id</param>
    public bool JudgeWorkFlowIsCreatedBySelf(int flowID, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_ID", flowID);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeWorkFlowIsCreatedBySelf", param));
    }

    /// <summary>
    /// 判断工作流模板的节点是否为当前用户创建
    /// </summary>
    /// <param name="nodeID">工作流模板的节点id</param>
    /// <param name="uID">用户id</param>
    public bool JudgeFlowNodeIsCreatedBySelf(int nodeID, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Node_ID", nodeID);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeFlowNodeIsCreatedBySelf", param));
    }
    #endregion

    #region 工作流表单设计

    public DataTable GetFormCols(string flowId)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", flowId);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFormDataColInfo", param).Tables[0];
    }

    public void DeleteFormDesign(string flowId, string userId)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_ID", flowId);
        param[1] = new SqlParameter("@UserId", userId);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wf_deleteFormDesign", param);
    }

    public void SaveFormDesign(string flowId, string userId, DataTable table)
    {
        if (table != null && table.Rows.Count > 0)
        {
            DeleteFormDesign(flowId, userId);
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
            {
                bulkCopy.DestinationTableName = "dbo.WF_FormDesign";
                bulkCopy.ColumnMappings.Add("Flow_ID", "Flow_ID");
                bulkCopy.ColumnMappings.Add("Sort_Order", "Sort_Order");
                bulkCopy.ColumnMappings.Add("ColName", "ColName");
                bulkCopy.ColumnMappings.Add("Label", "Label");
                bulkCopy.ColumnMappings.Add("Required", "Required");
                bulkCopy.ColumnMappings.Add("Sort", "Sort");
                bulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                bulkCopy.WriteToServer(table);
                table.Dispose();
            }
        }
    }

    public DataTable GetFormDesign(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFormDesign", param).Tables[0];
    }

    public DataTable GetFormData(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFormData", param).Tables[0];
    }

    public string GenerateForm(string nbr,string sortOrder,bool enble)
    {
        DataTable formData = GetFormData(nbr);
        StringBuilder formHtml = new StringBuilder();
        int tdIndex = 0;
        DataTable formDesgin = GetFormDesign(nbr);
        SqlDataReader reader4 = GetFlowNodeInstanceApprovedLastSort(nbr);
        int currentSortOrder = 0;
        if (reader4.Read())
        {
            currentSortOrder = Convert.ToInt32(reader4["Sort_Order"]);
        }
        reader4.Close();
        if ((currentSortOrder <= 10 && int.Parse(sortOrder) <= 10) || formData.Rows.Count > 0)
        {
            if (formDesgin.Rows.Count > 0)
            {
                formHtml.Append("<table cellpadding='0' cellspacing='0'>");
                foreach (DataRow row in formDesgin.Rows)
                {
                    tdIndex %= 3;
                    string type = row["type"].ToString();
                    string colName = row["ColName"].ToString();
                    string value = "";
                    if (formData.Rows.Count > 0)
                    {
                        value = formData.Rows[0][colName].ToString();
                    }
                    string colSortOrder = row["Sort_Order"].ToString();
                    if (type == "datetime" && value != "")
                    {
                        value = DateTime.Parse(value).ToString("yyyy-MM-dd");
                    }
                    if (type == "longString" && tdIndex > 0)
                    {
                        while (tdIndex <= 2)
                        {
                            formHtml.Append("<td>");
                            formHtml.Append("</td>");
                            formHtml.Append("<td>");
                            formHtml.Append("</td>");
                            tdIndex++;
                        }
                        formHtml.Append("</tr>");
                        tdIndex = 0;
                    }
                    if (tdIndex == 0)
                    {
                        formHtml.Append("<tr>");
                    }
                    formHtml.Append("<td align='right'style='height: 20px' valign='middle'>");
                    if (row["Required"].ToString().ToLower() == "true")
                    {
                        formHtml.Append("<font color='red'>*</font>");
                    }
                    formHtml.Append(row["Label"].ToString()).Append(":");
                    formHtml.Append("</td>");
                    if (type == "longString")
                    {
                        formHtml.Append("<td colspan='5' style='height: 20px' valign='middle'>");
                        formHtml.Append("<textarea rows='2' cols='20' id='").Append(colName).Append("' style='height:50px;width:655px;' class='FormData SmallTextBox");
                        if (row["Required"].ToString().ToLower() == "true")
                        {
                            formHtml.Append(" Required");
                        }
                        if (!enble || (colSortOrder != "0" && colSortOrder != sortOrder))
                        {
                            formHtml.Append("' readonly");
                        }
                        else
                        {
                            formHtml.Append(" String'");
                        }
                        formHtml.Append(">");
                        formHtml.Append(value);
                        formHtml.Append("</textarea>");
                        formHtml.Append("</td>");
                        formHtml.Append("</tr>");
                    }
                    else
                    {
                        formHtml.Append("<td style='height: 20px' valign='middle'>");
                        if (type == "bool")
                        {

                            formHtml.Append("<input type='checkbox' class='FormData' id='").Append(colName).Append("'");
                            if (value.ToLower() == "true")
                            {
                                formHtml.Append(" checked='checked'");
                            }
                            if (!enble || (colSortOrder != "0" && colSortOrder != sortOrder))
                            {
                                formHtml.Append(" disabled");
                            }
                        }
                        else
                        {
                            formHtml.Append("<input type='text' id='").Append(colName).Append("' value='").Append(value).Append("' class='FormData SmallTextBox");
                            if (row["Required"].ToString().ToLower() == "true")
                            {
                                formHtml.Append(" Required");
                            }
                            if (!enble || (colSortOrder != "0" && colSortOrder != sortOrder))
                            {
                                formHtml.Append("' readonly");
                            }
                            else
                            {
                                switch (type)
                                {
                                    case "datetime":
                                        formHtml.Append(" Date'");
                                        break;
                                    case "int":
                                        formHtml.Append(" Numeric'");
                                        break;
                                    case "float":
                                    case "decimal":
                                        formHtml.Append(" Numeric'");
                                        break;
                                    case "shortString":
                                        formHtml.Append(" String'");
                                        break;
                                }
                            }

                        }
                        formHtml.Append("/>");
                        formHtml.Append("</td>");
                        tdIndex++;
                        if (tdIndex == 3)
                        {
                            formHtml.Append("</tr>");
                        }
                    }

                }
                formHtml.Append("</table>");
            }
        }
        return formHtml.ToString();
    }
    #endregion

    /// <summary>
    /// 绑定驳回的列表
    /// </summary>
    /// <returns></returns>
    public DataTable RejectDDLBind(string nbr)
    {
        string sqlstr = "sp_wf_selectRejectDDL";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@nbr" , nbr)
        
        };

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    public bool upadateReject(string nbr, string uID, string rejectTo, out string strMailTo, out string strRejectTo)
    {
        try
        {
            string sqlstr = "sp_wf_upadateReject";

            SqlParameter[] param = new SqlParameter[]{
                new  SqlParameter("@nbr" , nbr )
                , new SqlParameter("@uID" , uID)
                , new SqlParameter("@rejectTo",rejectTo)
                , new SqlParameter("@strMailTo",SqlDbType.NVarChar,200)
                , new SqlParameter("@strRejectTo",SqlDbType.NVarChar,200)
            };
             param[3].Direction = ParameterDirection.Output;
             param[4].Direction = ParameterDirection.Output;

            bool flag = Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,sqlstr,param));

            strMailTo = param[3].Value.ToString();
            strRejectTo = param[4].Value.ToString();

            return flag;


        }
        catch
        {
            strMailTo = string.Empty;
            strRejectTo = string.Empty;
            return false;
        }
    }
}
