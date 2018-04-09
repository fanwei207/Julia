using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RD_WorkFlow
{
    /// <summary>
    /// R&D Work Flow��Ϣ
    /// </summary>
    public class RDW_Header
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public RDW_Header()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ������
        /// </summary>
        private long _RDW_MstrID;
        public long RDW_MstrID
        {
            get { return _RDW_MstrID; }
            set { _RDW_MstrID = value; }
        }

        private string _RDW_EStarDLC;
        public string RDW_EStarDLC
        {
            get { return _RDW_EStarDLC; }
            set { _RDW_EStarDLC = value; }
        }

        private string _RDW_Tier;
        public string RDW_Tier
        {
            get { return _RDW_Tier; }
            set { _RDW_Tier = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string _RDW_Priority;
        public string RDW_Priority
        {
            get { return _RDW_Priority; }
            set { _RDW_Priority = value; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string _RDW_LampType;
        public string RDW_LampType
        {
            get { return _RDW_LampType; }
            set { _RDW_LampType = value; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string _RDW_EngineerTeam;
        public string RDW_EngineerTeam
        {
            get { return _RDW_EngineerTeam; }
            set { _RDW_EngineerTeam = value; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string _RDW_Customer;
        public string RDW_Customer
        {
            get { return _RDW_Customer; }
            set { _RDW_Customer = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string _RDW_Project;
        public string RDW_Project
        {
            get { return _RDW_Project; }
            set { _RDW_Project = value; }
        }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        private string _RDW_ProdCode;
        public string RDW_ProdCode
        {
            get { return _RDW_ProdCode; }
            set { _RDW_ProdCode = value; }
        }

        /// <summary>
        /// ��ƷSKU#
        /// </summary>
        private string _RDW_ProdSKU;
        public string RDW_ProdSKU
        {
            get { return _RDW_ProdSKU; }
            set { _RDW_ProdSKU = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string _RDW_Category;
        public string RDW_Category
        {
            get { return _RDW_Category; }
            set { _RDW_Category = value; }
        }

        /// <summary>
        /// ��Ŀ˵��
        /// </summary>
        private string _RDW_ProdDesc;
        public string RDW_ProdDesc
        {
            get { return _RDW_ProdDesc; }
            set { _RDW_ProdDesc = value; }
        }

        /// <summary>
        /// ��Ʒʹ�ñ�׼
        /// </summary>
        private string _RDW_Standard;
        public string RDW_Standard
        {
            get { return _RDW_Standard; }
            set { _RDW_Standard = value; }
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private string _RDW_StartDate;
        public string RDW_StartDate
        {
            get { return _RDW_StartDate; }
            set { _RDW_StartDate = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _RDW_EndDate;
        public string RDW_EndDate
        {
            get { return _RDW_EndDate; }
            set { _RDW_EndDate = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        private string _RDW_Memo;
        public string RDW_Memo
        {
            get { return _RDW_Memo; }
            set { _RDW_Memo = value; }
        }

        /// <summary>
        /// ������ԱID
        /// </summary>
        private string _RDW_Partner;
        public string RDW_Partner
        {
            get { return _RDW_Partner; }
            set { _RDW_Partner = value; }
        }

        /// <summary>
        /// ������Ա����
        /// </summary>
        private string _RDW_PartnerName;
        public string RDW_PartnerName
        {
            get { return _RDW_PartnerName; }
            set { _RDW_PartnerName = value; }
        }

        /// <summary>
        /// ģ��ID
        /// </summary>
        private int _RDW_Template;
        public int RDW_Template
        {
            get { return _RDW_Template; }
            set { _RDW_Template = value; }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        private string _RDW_Status;
        public string RDW_Status
        {
            get { return _RDW_Status; }
            set { _RDW_Status = value; }
        }

        /// <summary>
        /// ������ID
        /// </summary>
        private long _RDW_CreatedBy;
        public long RDW_CreatedBy
        {
            get { return _RDW_CreatedBy; }
            set { _RDW_CreatedBy = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        private string _RDW_Creater;
        public string RDW_Creater
        {
            get { return _RDW_Creater; }
            set { _RDW_Creater = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _RDW_CreatedDate;
        public string RDW_CreatedDate
        {
            get { return _RDW_CreatedDate; }
            set { _RDW_CreatedDate = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        private string _RDW_FinishDate;
        public string RDW_FinishDate
        {
            get { return _RDW_FinishDate; }
            set { _RDW_FinishDate = value; }
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        private string _RDW_CurrStep;
        public string RDW_CurrStep
        {
            get { return _RDW_CurrStep; }
            set { _RDW_CurrStep = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private long _RDW_DalayDays;
        public long RDW_DalayDays
        {
            get { return _RDW_DalayDays; }
            set { _RDW_DalayDays = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        private string _RDW_Mbr;
        public string RDW_Mbr
        {
            get { return _RDW_Mbr; }
            set { _RDW_Mbr = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        private string _RDW_Ptr;
        public string RDW_Ptr
        {
            get { return _RDW_Ptr; }
            set { _RDW_Ptr = value; }
        }


        /// <summary>
        /// PM ID
        /// </summary>
        private string _RDW_PMID;
        public string RDW_PMID
        {
            get { return _RDW_PMID; }
            set { _RDW_PMID = value; }
        }

        /// <summary>
        /// PM ID
        /// </summary>
        private string _RDW_PM;
        public string RDW_PM
        {
            get { return _RDW_PM; }
            set { _RDW_PM = value; }
        }

        
        /// <summary>
        /// Project Owner ID
        /// </summary>
        private long _RDW_LeaderID;
        public long RDW_LeaderID
        {
            get { return _RDW_LeaderID; }
            set { _RDW_LeaderID = value; }
        }

        /// <summary>
        /// Project Owner Name
        /// </summary>
        private string _RDW_Leader;
        public string RDW_Leader
        {
            get { return _RDW_Leader; }
            set { _RDW_Leader = value; }
        }

        /// <summary>
        /// Task ID
        /// </summary>
        private string _RDW_TaskID;
        public string RDW_TaskID
        {
            get { return _RDW_TaskID; }
            set { _RDW_TaskID = value; }
        }

        /// <summary>
        /// Step ID
        /// </summary>
        private long _RDW_DetID;
        public long RDW_DetID
        {
            get { return _RDW_DetID; }
            set { _RDW_DetID = value; }
        }

        /// <summary>
        /// �����Ƿ�ɱ༭
        /// </summary>
        private bool _RDW_isActive;
        public bool RDW_isActive
        {
            get { return _RDW_isActive; }
            set { _RDW_isActive = value; }
        }

        /// <summary>
        /// ״̬����
        /// </summary>
        private string _RDW_Remark;
        public string RDW_Remark
        {
            get { return _RDW_Remark; }
            set { _RDW_Remark = value; }
        }

        /// <summary>
        /// ����ĿID
        /// </summary>
        private string  _RDW_Type;
        public string RDW_Type
        {
            get { return _RDW_Type; }
            set { _RDW_Type = value; }
        }

        private string _RDW_TypeName;
        public string RDW_TypeName
        {
            get { return _RDW_TypeName; }
            set { _RDW_TypeName = value; }
        }
        /// <summary>
        /// ECN Code
        /// </summary>
        private string _RDW_EcnCode;
        public string RDW_EcnCode
        {
            get { return _RDW_EcnCode; }
            set { _RDW_EcnCode = value; }
        }
        /// <summary>
        /// PPA
        /// </summary>
        private string _RDW_PPA;
        public string RDW_PPA
        {
            get { return _RDW_PPA; }
            set { _RDW_PPA = value; }
        }
        /// <summary>
        /// ��ĿType
        /// </summary>
        private int _RDW_OldID;
        public int RDW_OldID
        {
            get { return _RDW_OldID; }
            set { _RDW_OldID = value; }
        }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        private string _RDW_MGR;
        public string RDW_MGR
        {
            get { return _RDW_MGR; }
            set { _RDW_MGR = value; }
        }
        private string _RDW_PPAMsttrid;
        public string RDW_PPAMstrid
        {
            get { return _RDW_PPAMsttrid; }
            set { _RDW_PPAMsttrid = value; }
        }

        private string _RDW_Stage;
        public string RDW_Stage
        {
            get { return _RDW_Stage; }
            set { _RDW_Stage = value; }
        }
    }
}
