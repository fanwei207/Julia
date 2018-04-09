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
/// �������Ĳ�������
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

    #region ������ģ��
    /// <summary>
    /// ��ӹ�����ģ��
    /// </summary>
    /// <param name="name">������ģ�������</param>
    /// <param name="pre">������ģ���ǰ׺</param>
    /// <param name="domain">������ģ�����</param>
    /// <param name="description">������ģ�������</param>
    /// <param name="formname">������ģ���������</param>
    /// <param name="formtype">������ģ����ĸ�ʽ</param>
    /// <param name="formcontent">������ģ���������</param>
    /// <param name="remark">������ģ��ı�ע</param>
    /// <param name="status">������ģ���״̬</param>
    /// <param name="createdBy">������ģ��Ĵ�����</param>
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
    /// ��ʾ�����ҹ�����ģ��
    /// </summary>
    /// <param name="name">������ģ�������</param>
    /// <param name="username">������ģ�崴���ߵ�����</param>
    /// <param name="createdDate1">������ģ��Ĵ������ڼ�������1</param>
    /// <param name="createdDate2">������ģ��Ĵ������ڼ�������2</param>
    /// <param name="domain">������ģ�����</param>
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
    /// ɾ��������ģ��
    /// </summary>
    /// <param name="id">������ģ���id</param>
    public int DeleteWorkFlowTemplate(int id)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", id);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_deleteWorkFlow", param));
    }

    /// <summary>
    /// ����id����ù�����ģ�����Ϣ
    /// </summary>
    /// <param name="id">������ģ���id</param>
    public SqlDataReader GetWorkFlowTemplateByID(int id)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowByID", param);
    }

    /// <summary>
    /// ���¹�����ģ�壨��ģ��Ҳ������
    /// </summary>
    /// <param name="id">������ģ���id</param>
    /// <param name="name">������ģ�������</param>
    /// <param name="pre">������ģ���ǰ׺</param>
    /// <param name="description">������ģ�������</param>
    /// <param name="formname">������ģ���������</param>
    /// <param name="formtype">������ģ����ĸ�ʽ</param>
    /// <param name="formcontent">������ģ���������</param>
    /// <param name="remark">������ģ��ı�ע</param>
    /// <param name="status">������ģ���״̬</param>
    /// <param name="modifiedBy">������ģ����޸���</param>
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
    /// ���¹�����ģ�壨��ģ�岻������
    /// </summary>
    /// <param name="id">������ģ���id</param>
    /// <param name="name">������ģ�������</param>
    /// <param name="pre">������ģ���ǰ׺</param>
    /// <param name="description">������ģ�������</param>
    /// <param name="formname">������ģ���������</param>
    /// <param name="remark">������ģ��ı�ע</param>
    /// <param name="status">������ģ���״̬</param>
    /// <param name="modifiedBy">������ģ����޸���</param>
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
    /// ���¹�����ģ��ı�
    /// </summary>
    /// <param name="id">������ģ���id</param>
    /// <param name="formcontent">������ģ���������</param>
    public bool UpdateWorkFlowFile(int id, byte[] formcontent)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_ID", id);
        param[1] = new SqlParameter("@Flow_FormTemplateContent", formcontent);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateWorkFlowFile", param));
    }

    /// <summary>
    /// ���ݹ�����ģ����������ù�����ģ���ID
    /// </summary>
    /// <param name="flowName">������ģ�������</param>
    public SqlDataReader GetWorkFlowTemplateIDByName(string flowName,int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_Name", flowName);
        param[1] = new SqlParameter("@plantCode", plantCode);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowIDByName", param);
    }
    #endregion

    #region ������ģ��ڵ����
    /// <summary>
    /// ��ӽڵ����
    /// </summary>
    /// <param name="sortName">�������</param>
    /// <param name="createdBy">�ڵ���ŵĴ�����</param>
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
    /// ���½ڵ����
    /// </summary>
    /// <param name="id">�ڵ���ŵ�id</param>
    /// <param name="name">�ڵ���ŵ�����</param>
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
    /// ɾ���ڵ����
    /// </summary>
    /// <param name="id">�ڵ���ŵ�id</param>
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
    /// ��ʾ�ڵ����
    /// </summary>
    public DataTable GetNodeSort()
    {
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectNodeSort").Tables[0];
    }

    /// <summary>
    /// ����ĳ�ڵ���ŵ���Ϣ
    /// </summary>
    /// <param name="id">�ڵ���ŵ�id</param>
    public SqlDataReader GetNodeSortByID(int id)
    {
        SqlParameter param = new SqlParameter("@Sort_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectNodeSortByID", param);
    }
    #endregion

    #region ������ģ�岽��
    /// <summary>
    /// ������̲���
    /// </summary>
    /// <param name="flowID">������ģ���ID</param>
    /// <param name="name">���������</param>
    /// <param name="sortorder">��ŵ�Order</param>
    /// <param name="sortname">��ŵ�����</param>
    /// <param name="description">���������</param>
    /// <param name="createdBy">����Ĵ�����</param>
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
    /// �������̲���
    /// </summary>
    /// <param name="id">�����id</param>
    /// <param name="flowID">����ģ��ID</param>
    /// <param name="name">���������</param>
    /// <param name="sortorder">��ŵ�Order</param>
    /// <param name="sortname">��ŵ�����</param>
    /// <param name="description">���������</param>
    /// <param name="modifiedBy">����Ĵ�����</param>
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
    /// ��ʾ���̲���
    /// </summary>
    /// <param name="flowID">������ģ���ID</param>
    public DataTable GetFlowNode(int flowID)
    {
        SqlParameter param = new SqlParameter("@Flow_ID", flowID);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNode", param).Tables[0];
    }

    /// <summary>
    /// ɾ�����̲���
    /// </summary>
    /// <param name="nodeID">���̲����ID</param>
    /// <param name="flowID">����ģ���ID</param>
    public int DeleteFlowNode(int nodeID, int flowID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Node_ID", nodeID);
        param[1] = new SqlParameter("@Flow_ID", flowID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_deleteFlowNode", param));
    }

    /// <summary>
    /// ����id��������̲������Ϣ
    /// </summary>
    /// <param name="id">���̲����id</param>
    public SqlDataReader GetFlowNodeByID(int id)
    {
        SqlParameter param = new SqlParameter("@Node_ID", id);
        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeByID", param);
    }
    #endregion

    #region ������ģ�岽����Ӹ�λ����Ա
    /// <summary>
    /// ��ȡ��˾
    /// </summary>
    public DataTable GetPlant()
    {
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectPlant").Tables[0];
    }

    /// <summary>
    /// ��ȡ��˾�µĲ���
    /// </summary>
    /// <param name="plantid">��˾��ID</param>
    public DataTable GetDept(int plantid)
    {
        SqlParameter param = new SqlParameter("@plantid", plantid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectDept", param).Tables[0];
    }

    /// <summary>
    /// ��ȡ��˾ĳ�����µ�������ְԱ��
    /// </summary>
    /// <param name="plantid">��˾��ID</param>
    /// <param name="deptid">���ŵ�ID</param>
    public DataTable GetUser(int plantid, int deptid)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@plantID", plantid);
        param[1] = new SqlParameter("@deptID", deptid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectUser", param).Tables[0];
    }

    /// <summary>
    /// ��ȡ��˾���н�ɫ
    /// </summary>
    /// <param name="plantid">��˾��ID</param>
    public DataTable GetRole(int plantid)
    {
        SqlParameter param = new SqlParameter("@plantID", plantid);
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectRole", param).Tables[0];
    }

    /// <summary>
    /// �޸ĸ�λ����Ա��������ģ�岽�����ж��Ƿ���δ������
    /// </summary>
    /// <param name="nodeID">������ģ�岽���ID</param>
    /// <param name="flowID">������ģ���ID</param>
    /// <param name="type">��Ӹ�λ����Ա������</param>
    /// <param name="objectID">��Ӹ�λ����Ա��ID</param>
    /// <param name="createdBy">������</param>
    public DataTable CheckNodePersonUnfinished(int nodeID, int plantid)
    {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Node_ID", nodeID);
            param[1] = new SqlParameter("@plantCode", plantid);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_CheckNodePersonUnfinished", param).Tables[0];
    }

    /// <summary>
    /// ��Ӹ�λ����Ա��������ģ�岽��
    /// </summary>
    /// <param name="nodeID">������ģ�岽���ID</param>
    /// <param name="flowID">������ģ���ID</param>
    /// <param name="type">��Ӹ�λ����Ա������</param>
    /// <param name="objectID">��Ӹ�λ����Ա��ID</param>
    /// <param name="createdBy">������</param>
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
    /// ɾ��������ģ�岽���λ����Ա
    /// </summary>
    /// <param name="nodeID">������ģ�岽���ID</param>
    public void DeleteNodePerson(int nodeID)
    {
        SqlParameter param = new SqlParameter("@Node_ID", nodeID);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wf_deleteNodePerson", param);
    }

    #endregion

    #region ������ʵ��
    /// <summary>
    /// ��ʾ�����µĹ�����ģ��
    /// </summary>
    /// <param name="plantCode">��</param>
    public DataTable GetWorkFlowTemplateByDomain(int plantCode)
    {
        SqlParameter param = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowByDomain", param).Tables[0];
    }

    /// <summary>
    /// ���һ���յĹ�����ʵ��
    /// </summary>
    /// <param name="flowID">������ģ���ID</param>
    /// <param name="plantCode">Ա��������</param>
    /// <param name="createdBy">������</param>
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
    /// ��ʾ�մ����ĿյĹ�����ʵ����Ϣ
    /// </summary>
    /// <param name="wfn_nbr">������ʵ����ID</param>
    /// <param name="plantcode">��</param>
    public SqlDataReader GetWorkFlowInstanceByNbr(string wfn_nbr, int plantcode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfn_nbr);
        param[1] = new SqlParameter("@plantcode", plantcode);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowInstanceByNbr", param);
    }

    /// <summary>
    /// ��ʾ�������ڵ�ʵ���ĵ�һ��δ�����Ĳ������
    /// </summary>
    /// <param name="wfnNbr">������ʵ���������</param>
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
    /// ����մ����Ĺ�����ʵ��
    /// </summary>
    /// <param name="wfnNbr">������ʵ���������</param>
    /// <param name="wfnReqDate">������ʵ������������</param>
    /// <param name="wfnDueDate">������ʵ���Ľ�ֹ����</param>
    public bool ActiveWorkFlowInstance(string wfnNbr, string wfnReqDate, string wfnDueDate)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@WFN_ReqDate", wfnReqDate);
        param[2] = new SqlParameter("@WFN_DueDate", wfnDueDate);
        

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_activeWorkFlowInstance", param));
    }

    /// <summary>
    /// ���¹������ڵ�ʵ��
    /// </summary>
    /// <param name="wfnNbr">������ʵ���������</param>
    /// <param name="sortOrder">������ʵ���ڵ�����</param>
    /// <param name="fniRemark">�������ڵ�ʵ���ı�ע</param>
    /// <param name="fniStatus">�������ڵ�ʵ����״̬</param>
    /// <param name="uid">������</param>
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
    /// �������ڵ�ʵ���ĸ����ϴ�
    /// </summary>
    /// <param name="wfnNbr">������ʵ���������</param>
    /// <param name="sortOrder">������ʵ���ڵ�����</param>
    /// <param name="attachName">�ϴ�����������</param>
    /// <param name="attachType">�ϴ������ĸ�ʽ</param>
    /// <param name="attachContent">�ϴ�����������</param>
    /// <param name="attachCreatedBy">�ϴ���</param>
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
    /// ��ʾ������ʵ���µĹ������ڵ�ʵ��
    /// </summary>
    /// <param name="wfnNbr">������ʵ���������</param>
    /// <param name="plantCode">��</param>
    public DataTable GetFlowNodeInstanceTouched(string wfnNbr, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceTouched", param).Tables[0];
    }

    /// <summary>
    /// ��ʾ������ʵ���ϴ��ĸ���
    /// </summary>
    /// <param name="nbr">�������ڵ�ʵ����ID</param>
    /// <param name="sortOrder">�������ڵ�ʵ�����</param>
    public DataTable GetFniAttach(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFniAttach", param).Tables[0];
    }

    /// <summary>
    /// ɾ���������ڵ�ʵ���ϴ��ĸ���
    /// </summary>
    /// <param name="attachID">�������ڵ㸽����ID</param>
    public void DeleteFniAttach(int attachID)
    {
        SqlParameter param = new SqlParameter("@Attach_ID", attachID);

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wf_DeleteFniAttach", param);
    }

    /// <summary>
    /// ��ȡ�������ڵ�ʵ���ϴ��ĸ���
    /// </summary>
    /// <param name="attachID">�������ڵ㸽����ID</param>
    public SqlDataReader GetAttachDetail(int attachID)
    {
        SqlParameter param = new SqlParameter("@Attach_ID", attachID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectAttachDetail", param);
    }

    /// <summary>
    /// ��ȡ�Լ������Ĺ�����ʵ��
    /// </summary>
    /// <param name="wfnNbr">������ʵ���ı��</param>
    /// <param name="flowID">������ģ��ı��</param>
    /// <param name="reqDate1">������ʵ������������1</param>
    /// <param name="uID">�û���ID</param>
    /// <param name="status">������ʵ����״̬</param>
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
    /// ����Ա�鿴Ա�������Ĺ�����ʵ����ϸ
    /// </summary>
    /// <param name="wfnNbr">������ʵ���ı��</param>
    /// <param name="flowID">������ģ��ı��</param>
    /// <param name="reqDate1">������ʵ������������1</param>
    /// <param name="uName">�û�������</param>
    /// <param name="status">������ʵ����״̬</param>
    /// <param name="domain">��</param>
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
    /// ����Ա�鿴Ա�������Ĺ�����ʵ����ϸ
    /// </summary>
    /// <param name="wfnNbr">������ʵ���ı��</param>
    /// <param name="flowID">������ģ��ı��</param>
    /// <param name="reqDate1">������ʵ������������1</param>
    /// <param name="uName">�û�������</param>
    /// <param name="status">������ʵ����״̬</param>
    /// <param name="domain">��</param>
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
    /// ��ȡ�Լ������Ĺ������ڵ�ʵ��
    /// </summary>
    /// <param name="wfnNbr">������ʵ���ı��</param>
    /// <param name="uID">�û���ID</param>
    public SqlDataReader GetFlowNodeInstanceCreatedBySelf(string wfnNbr, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceCreatedBySelf", param);
    }

    /// <summary>
    /// ��ȡ�������Ĺ������ڵ�ʵ��
    /// </summary>
    /// <param name="wfnNbr">������ʵ���ı��</param>
    /// <param name="uID">�û���ID</param>
    public SqlDataReader GetFlowNodeInstanceApprovedBySelf(string wfnNbr, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", wfnNbr);
        param[1] = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceApprovedBySelf", param);
    }


    /// <summary>
    /// ��ȡ���Լ��йصĹ�����ʵ��
    /// </summary>
    /// <param name="wfnNbr">������ʵ���ı��</param>
    /// <param name="flowID">������ģ��ı��</param>
    /// <param name="reqDate1">������ʵ������������1</param>
    /// <param name="uID">�û���ID</param>
    /// <param name="uRole">�û��Ľ�ɫ</param>
    /// <param name="status">������ʵ����״̬</param>
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
    /// ���¹�����ʵ���ı�
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    /// <param name="formcontent">������ʵ����������</param>
    public bool UpdateWorkFlowInstanceFile(string nbr, byte[] formcontent)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@WFN_FormContent", formcontent);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_updateWorkFlowInstanceFile", param));
    }

    /// <summary>
    /// �ж��������Ƿ����޸ĵ�Ȩ��
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    /// <param name="uID">�û�ID</param>
    public bool JudgeIsCanModifiedWithApplier(string nbr, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeIsCanModifiedWithApplier", param));
    }

    /// <summary>
    /// �жϹ�����ʵ���Ƿ�Ϊ����״̬
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    public SqlDataReader JudgeWorkFlowInstanceIsActive(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr",nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_judgeWorkFlowInstanceIsActive", param);
    }

    /// <summary>
    /// �ж��������Ƿ����޸ĵ�Ȩ��
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    /// <param name="sortOrder">���</param>
    public bool JudgeIsCanModifiedWithApprover(string nbr, int sortOrder, int uID)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);
        param[2] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeIsCanModifiedWithApprover", param));
    }

    /// <summary>
    /// ��ȡ����������ʵ������Ϣ
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    /// <param name="sortOrder">���</param>
    public SqlDataReader GetFNIInfoByNbrAndSort(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFNIInfoByNbrAndSort", param);
    }

    /// <summary>
    /// ��ȡ�û���Ϣ
    /// </summary>
    /// <param name="uID">�û�ID</param>
    public SqlDataReader GetUserInfo(int uID)
    {
        SqlParameter param = new SqlParameter("@uID", uID);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectUserInfo", param);
    }

    /// <summary>
    /// �ж��û��Ƿ������������̵�Ȩ��
    /// </summary>
    /// <param name="flowID">����ID</param>
    /// <param name="uID">�û�ID</param>
    /// <param name="roleID">�û���ɫID</param>
    public bool JudgeIsCanCreatedWithApplier(int flowID, int uID, string roleName)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@flowID", flowID);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@roleName", roleName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeIsCanCreatedWithApplier", param));
    }

    /// <summary>
    /// ��ȡ�������ڵ�ʵ���Ĳ�������
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    public DataTable GetFlowNodeInfo(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInfo", param).Tables[0];
    }

    /// <summary>
    /// ��ȡ�������ڵ�ʵ�������һ�����
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    public SqlDataReader GetFlowNodeInstanceLastSort(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceLastSort", param);
    }

    /// <summary>
    /// ��ȡ�������ڵ�ʵ���������������һ�����
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    public SqlDataReader GetFlowNodeInstanceApprovedLastSort(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectFlowNodeInstanceApprovedLastSort", param);
    }

    /// <summary>
    /// ��ȡ��һ�������˵��ʼ�
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    /// <param name="sortOrder">�������ڵ�ʵ�������</param>
    public DataTable GetNextApproverEmail(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectNextApproverEmail", param).Tables[0];
    }

    /// <summary>
    /// ��ȡ��ǰ�����˵��ʼ�
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    /// <param name="sortOrder">�������ڵ�ʵ�������</param>
    public DataTable GetPerApproverEmail(string nbr, int sortOrder)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@WFN_Nbr", nbr);
        param[1] = new SqlParameter("@Sort_Order", sortOrder);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wf_selectPerApproverEmail", param).Tables[0];
    }

    /// <summary>
    /// ��ȡ�����ߵ��ʼ�
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    public SqlDataReader GetApplierEmail(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectApplierEmail", param);
    }

    /// <summary>
    /// �ж��û��ı��Ƿ���д
    /// </summary>
    /// <param name="nbr">������ʵ���ı��</param>
    public bool JudgeExcelIsFill(string nbr)
    {
        SqlParameter param = new SqlParameter("@WFN_Nbr", nbr);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeExcelIsFill", param));
    }

    /// <summary>
    /// ��ȡ������������
    /// </summary>
    /// <param name="plantCode">��</param>
    /// <param name="pre">���̴���</param>
    public SqlDataReader GetWorkFlowNameByDomainAndPre(int plantCode, string pre)
    {
        SqlParameter[] param= new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_Domain", plantCode);
        param[1] = new SqlParameter("@Flow_Req_Pre", pre);

        return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_wf_selectWorkFlowNameByDomainAndPre", param);
    }

    /// <summary>
    /// ͨ�����ķ�ʽ��ȡ�ļ���������׺����ֹֻ�ı��ļ��ĺ�׺
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns>��׺</returns>
    public string GetFileExtension(string filepath)
    {
        System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
        string fileclass = string.Empty;
        //�����λ��Ҫ�����ж�.
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
    /// ͨ�����ķ�ʽ�ж��ϴ��ļ���������ʽ
    /// </summary>
    /// <param name="hifile"></param>
    /// <returns>bool</returns>
    public bool IsAllowedExtension(HtmlInputFile hifile)
    {
        System.IO.FileStream fs = new System.IO.FileStream(hifile.PostedFile.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
        string fileclass = "";
        //�����λ��Ҫ�����ж�.
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
        if (fileclass == "6063")//˵��255216��jpg;7173��gif;6677��BMP,13780��PNG;7790��exe,8297��rar,6063��xml
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// �жϹ�����ģ���Ƿ�Ϊ��ǰ�û�����
    /// </summary>
    /// <param name="flowID">������ģ���id</param>
    /// <param name="uID">�û�id</param>
    public bool JudgeWorkFlowIsCreatedBySelf(int flowID, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Flow_ID", flowID);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeWorkFlowIsCreatedBySelf", param));
    }

    /// <summary>
    /// �жϹ�����ģ��Ľڵ��Ƿ�Ϊ��ǰ�û�����
    /// </summary>
    /// <param name="nodeID">������ģ��Ľڵ�id</param>
    /// <param name="uID">�û�id</param>
    public bool JudgeFlowNodeIsCreatedBySelf(int nodeID, int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Node_ID", nodeID);
        param[1] = new SqlParameter("@uID", uID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_wf_judgeFlowNodeIsCreatedBySelf", param));
    }
    #endregion

    #region �����������

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
    /// �󶨲��ص��б�
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
