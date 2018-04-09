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
    public class RDW_Detail_Docs
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public RDW_Detail_Docs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private long _RDW_DocsID;
        /// <summary>
        /// ������
        /// </summary>
        public long RDW_DocsID
        {
            get { return _RDW_DocsID; }
            set { _RDW_DocsID = value; }
        }

        private long _RDW_DetID;
        /// <summary>
        /// RDW_DetID
        /// </summary>
        public long RDW_DetID
        {
            get { return _RDW_DetID; }
            set { _RDW_DetID = value; }
        }

        private string _RDW_FileName;
        /// <summary>
        /// �洢�����ݿ��е�����
        /// </summary>
        public string RDW_FileName
        {
            get { return _RDW_FileName; }
            set { _RDW_FileName = value; }
        }

        private byte[] _RDW_FileContent;
        /// <summary>
        /// �ϴ��ļ����ݣ����ã�
        /// </summary>
        public byte[] RDW_FileContent
        {
            get { return _RDW_FileContent; }
            set { _RDW_FileContent = value; }
        }

        private string _RDW_FileType;
        /// <summary>
        /// �ϴ��ļ�����
        /// </summary>
        public string RDW_FileType
        {
            get { return _RDW_FileType; }
            set { _RDW_FileType = value; }
        }
        private bool _RDW_TransferStatus;
        /// <summary>
        /// �ļ�ת��״̬
        /// </summary>
        public bool RDW_TransferStatus
        {
            get { return _RDW_TransferStatus; }
            set { _RDW_TransferStatus = value; }
        }
        private string _RDW_Uploader;
        /// <summary>
        /// �ϴ���
        /// </summary>
        public string RDW_Uploader
        {
            get { return _RDW_Uploader; }
            set { _RDW_Uploader = value; }
        }

        private string _RDW_UploadID;
        /// <summary>
        /// �ϴ���ID
        /// </summary>
        public string RDW_UploaderID
        {
            get { return _RDW_UploadID; }
            set { _RDW_UploadID = value; }
        }

        private string _RDW_UploadDate;
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public string RDW_UploadDate
        {
            get { return _RDW_UploadDate; }
            set { _RDW_UploadDate = value; }
        }
        
        private string _RDW_StepName;
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string RDW_StepName
        {
            get { return _RDW_StepName; }
            set { _RDW_StepName = value; }
        }

        private string _RDW_StepNo;
        /// <summary>
        /// ��Ŀ�������
        /// </summary>
        public string RDW_StepNo
        {
            get { return _RDW_StepNo; }
            set { _RDW_StepNo = value; }
        }

        private string _RDW_DocVerCount;
        /// <summary>
        /// ��Ŀ�����ĵ��İ汾��
        /// </summary>
        public string RDW_DocVerCount
        {
            get { return _RDW_DocVerCount; }
            set { _RDW_DocVerCount = value; }
        }

        private string _RDW_PhysicalName;
        /// <summary>
        /// �洢�ڴ����ϵ�����
        /// </summary>
        public string RDW_PhysicalName
        {
            get { return _RDW_PhysicalName; }
            set { _RDW_PhysicalName = value; }
        }
        private string _RDW_Path;
        /// <summary>
        /// �洢�ڴ����ϵ�����
        /// </summary>
        public string RDW_Path
        {
            get { return _RDW_Path; }
            set { _RDW_Path = value; }
        }

        private string _RDW_fromDocID;
        /// <summary>
        /// �洢�ڴ����ϵ�����
        /// </summary>
        public string RDW_fromDocID
        {
            get { return _RDW_fromDocID; }
            set { _RDW_fromDocID = value; }
        }

        public bool RDW_isDelete
        {
            get;
            set;
        }
    }
}