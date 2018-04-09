using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PM
{
    /// <summary>
    /// ��Ŀ��Ϣ
    /// </summary>
    public class PM_Header
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public PM_Header()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ������
        /// </summary>
        private long _PM_MstrID;
        public long PM_MstrID
        {
            get { return _PM_MstrID; }
            set { _PM_MstrID = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string _PM_ProjName;
        public string PM_ProjName
        {
            get { return _PM_ProjName; }
            set { _PM_ProjName = value; }
        }

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private string _PM_ProjCode;
        public string PM_ProjCode
        {
            get { return _PM_ProjCode; }
            set { _PM_ProjCode = value; }
        }

        /// <summary>
        /// ��Ŀ˵��
        /// </summary>
        private string _PM_ProjDesc;
        public string PM_ProjDesc
        {
            get { return _PM_ProjDesc; }
            set { _PM_ProjDesc = value; }
        }

        /// <summary>
        /// ��Ŀ���ݺ�Ŀ��
        /// </summary>
        private string _PM_Content;
        public string PM_Content
        {
            get { return _PM_Content; }
            set { _PM_Content = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _PM_ProjectDate;
        public string PM_ProjectDate
        {
            get { return _PM_ProjectDate; }
            set { _PM_ProjectDate = value; }
        }

        /// <summary>
        /// �ر�����
        /// </summary>
        private string _PM_CloseDate;
        public string PM_CloseDate
        {
            get { return _PM_CloseDate; }
            set { _PM_CloseDate = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _PM_Memo;
        public string PM_Memo
        {
            get { return _PM_Memo; }
            set { _PM_Memo = value; }
        }

        /// <summary>
        /// ������ԱID
        /// </summary>
        private string _PM_Partner;
        public string PM_Partner
        {
            get { return _PM_Partner; }
            set { _PM_Partner = value; }
        }

        /// <summary>
        /// ������Ա����
        /// </summary>
        private string _PM_PartnerName;
        public string PM_PartnerName
        {
            get { return _PM_PartnerName; }
            set { _PM_PartnerName = value; }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        private string _PM_Status;
        public string PM_Status
        {
            get { return _PM_Status; }
            set { _PM_Status = value; }
        }

        /// <summary>
        /// ������ID
        /// </summary>
        private long _PM_CreatedBy;
        public long PM_CreatedBy
        {
            get { return _PM_CreatedBy; }
            set { _PM_CreatedBy = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        private string _PM_Creater;
        public string PM_Creater
        {
            get { return _PM_Creater; }
            set { _PM_Creater = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _PM_CreatedDate;
        public string PM_CreatedDate
        {
            get { return _PM_CreatedDate; }
            set { _PM_CreatedDate = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        private string _PM_Dept;
        public string PM_Dept
        {
            get { return _PM_Dept; }
            set { _PM_Dept = value; }
        }

        /// <summary>
        /// �ƻ��������
        /// </summary>
        private string _PM_FinDate;
        public string PM_FinDate
        {
            get { return _PM_FinDate; }
            set { _PM_FinDate = value; }
        }

        /// <summary>
        /// Ԥ����Ա����
        /// </summary>
        private string _PM_HumanFee;
        public string PM_HumanFee
        {
            get { return _PM_HumanFee; }
            set { _PM_HumanFee = value; }
        }

        /// <summary>
        /// Ԥ���豸����
        /// </summary>
        private string _PM_EquipFee;
        public string PM_EquipFee
        {
            get { return _PM_EquipFee; }
            set { _PM_EquipFee = value; }
        }

        /// <summary>
        /// Ԥ���������
        /// </summary>
        private string _PM_SoftFee;
        public string PM_SoftFee
        {
            get { return _PM_SoftFee; }
            set { _PM_SoftFee = value; }
        }
    }
}