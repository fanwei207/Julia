using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for RT_Detail_Docs
/// </summary>
/// 
namespace RT_WorkFlow
{
    public class RT_Detail_Docs
    {
        public RT_Detail_Docs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 自增长
        /// </summary>
        private long _RDW_DocsID;
        public long RDW_DocsID
        {
            get { return _RDW_DocsID; }
            set { _RDW_DocsID = value; }
        }

        /// <summary>
        /// RDW_DetID
        /// </summary>
        private long _RDW_DetID;
        public long RDW_DetID
        {
            get { return _RDW_DetID; }
            set { _RDW_DetID = value; }
        }

        /// <summary>
        /// 上传文件名
        /// </summary>
        private string _RDW_FileName;
        public string RDW_FileName
        {
            get { return _RDW_FileName; }
            set { _RDW_FileName = value; }
        }

        /// <summary>
        /// 上传文件内容
        /// </summary>
        private byte[] _RDW_FileContent;
        public byte[] RDW_FileContent
        {
            get { return _RDW_FileContent; }
            set { _RDW_FileContent = value; }
        }

        /// <summary>
        /// 上传文件类型
        /// </summary>
        private string _RDW_FileType;
        public string RDW_FileType
        {
            get { return _RDW_FileType; }
            set { _RDW_FileType = value; }
        }

        /// <summary>
        /// 上传人
        /// </summary>
        private string _RDW_Uploader;
        public string RDW_Uploader
        {
            get { return _RDW_Uploader; }
            set { _RDW_Uploader = value; }
        }

        /// <summary>
        /// 上传人ID
        /// </summary>
        private string _RDW_UploadID;
        public string RDW_UploaderID
        {
            get { return _RDW_UploadID; }
            set { _RDW_UploadID = value; }
        }

        /// <summary>
        /// 上传日期
        /// </summary>
        private string _RDW_UploadDate;
        public string RDW_UploadDate
        {
            get { return _RDW_UploadDate; }
            set { _RDW_UploadDate = value; }
        }
    }
}
