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
    /// Project Detail Document信息
    /// </summary>
    public class PM_Detail_Docs
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public PM_Detail_Docs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 自增长
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
        /// 上传文件名
        /// </summary>
        private string _PM_FileName;
        public string PM_FileName
        {
            get { return _PM_FileName; }
            set { _PM_FileName = value; }
        }

        /// <summary>
        /// 上传文件内容
        /// </summary>
        private byte[] _PM_FileContent;
        public byte[] PM_FileContent
        {
            get { return _PM_FileContent; }
            set { _PM_FileContent = value; }
        }

        /// <summary>
        /// 上传文件类型
        /// </summary>
        private string _PM_FileType;
        public string PM_FileType
        {
            get { return _PM_FileType; }
            set { _PM_FileType = value; }
        }

        /// <summary>
        /// 上传人
        /// </summary>
        private string _PM_Uploader;
        public string PM_Uploader
        {
            get { return _PM_Uploader; }
            set { _PM_Uploader = value; }
        }

        /// <summary>
        /// 上传人ID
        /// </summary>
        private string _PM_UploadID;
        public string PM_UploaderID
        {
            get { return _PM_UploadID; }
            set { _PM_UploadID = value; }
        }

        /// <summary>
        /// 上传日期
        /// </summary>
        private string _PM_UploadDate;
        public string PM_UploadDate
        {
            get { return _PM_UploadDate; }
            set { _PM_UploadDate = value; }
        }
    }
}