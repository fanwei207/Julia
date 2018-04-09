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
    /// Project Detail Document��Ϣ
    /// </summary>
    public class PM_Detail_Docs
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public PM_Detail_Docs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ������
        /// </summary>
        private long _PM_DocsID;
        public long PM_DocsID
        {
            get { return _PM_DocsID; }
            set { _PM_DocsID = value; }
        }

        /// <summary>
        /// PM_DetID
        /// </summary>
        private long _PM_DetID;
        public long PM_DetID
        {
            get { return _PM_DetID; }
            set { _PM_DetID = value; }
        }

        /// <summary>
        /// �ϴ��ļ���
        /// </summary>
        private string _PM_FileName;
        public string PM_FileName
        {
            get { return _PM_FileName; }
            set { _PM_FileName = value; }
        }

        /// <summary>
        /// �ϴ��ļ�����
        /// </summary>
        private byte[] _PM_FileContent;
        public byte[] PM_FileContent
        {
            get { return _PM_FileContent; }
            set { _PM_FileContent = value; }
        }

        /// <summary>
        /// �ϴ��ļ�����
        /// </summary>
        private string _PM_FileType;
        public string PM_FileType
        {
            get { return _PM_FileType; }
            set { _PM_FileType = value; }
        }

        /// <summary>
        /// �ϴ���
        /// </summary>
        private string _PM_Uploader;
        public string PM_Uploader
        {
            get { return _PM_Uploader; }
            set { _PM_Uploader = value; }
        }

        /// <summary>
        /// �ϴ���ID
        /// </summary>
        private string _PM_UploadID;
        public string PM_UploaderID
        {
            get { return _PM_UploadID; }
            set { _PM_UploadID = value; }
        }

        /// <summary>
        /// �ϴ�����
        /// </summary>
        private string _PM_UploadDate;
        public string PM_UploadDate
        {
            get { return _PM_UploadDate; }
            set { _PM_UploadDate = value; }
        }
    }
}