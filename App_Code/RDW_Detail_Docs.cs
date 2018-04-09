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
    /// R&D Work Flow信息
    /// </summary>
    public class RDW_Detail_Docs
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public RDW_Detail_Docs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private long _RDW_DocsID;
        /// <summary>
        /// 自增长
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
        /// 存储在数据库中的名称
        /// </summary>
        public string RDW_FileName
        {
            get { return _RDW_FileName; }
            set { _RDW_FileName = value; }
        }

        private byte[] _RDW_FileContent;
        /// <summary>
        /// 上传文件内容（弃用）
        /// </summary>
        public byte[] RDW_FileContent
        {
            get { return _RDW_FileContent; }
            set { _RDW_FileContent = value; }
        }

        private string _RDW_FileType;
        /// <summary>
        /// 上传文件类型
        /// </summary>
        public string RDW_FileType
        {
            get { return _RDW_FileType; }
            set { _RDW_FileType = value; }
        }
        private bool _RDW_TransferStatus;
        /// <summary>
        /// 文件转移状态
        /// </summary>
        public bool RDW_TransferStatus
        {
            get { return _RDW_TransferStatus; }
            set { _RDW_TransferStatus = value; }
        }
        private string _RDW_Uploader;
        /// <summary>
        /// 上传人
        /// </summary>
        public string RDW_Uploader
        {
            get { return _RDW_Uploader; }
            set { _RDW_Uploader = value; }
        }

        private string _RDW_UploadID;
        /// <summary>
        /// 上传人ID
        /// </summary>
        public string RDW_UploaderID
        {
            get { return _RDW_UploadID; }
            set { _RDW_UploadID = value; }
        }

        private string _RDW_UploadDate;
        /// <summary>
        /// 上传日期
        /// </summary>
        public string RDW_UploadDate
        {
            get { return _RDW_UploadDate; }
            set { _RDW_UploadDate = value; }
        }
        
        private string _RDW_StepName;
        /// <summary>
        /// 项目步骤
        /// </summary>
        public string RDW_StepName
        {
            get { return _RDW_StepName; }
            set { _RDW_StepName = value; }
        }

        private string _RDW_StepNo;
        /// <summary>
        /// 项目步骤序号
        /// </summary>
        public string RDW_StepNo
        {
            get { return _RDW_StepNo; }
            set { _RDW_StepNo = value; }
        }

        private string _RDW_DocVerCount;
        /// <summary>
        /// 项目步骤文档的版本数
        /// </summary>
        public string RDW_DocVerCount
        {
            get { return _RDW_DocVerCount; }
            set { _RDW_DocVerCount = value; }
        }

        private string _RDW_PhysicalName;
        /// <summary>
        /// 存储在磁盘上的名称
        /// </summary>
        public string RDW_PhysicalName
        {
            get { return _RDW_PhysicalName; }
            set { _RDW_PhysicalName = value; }
        }
        private string _RDW_Path;
        /// <summary>
        /// 存储在磁盘上的名称
        /// </summary>
        public string RDW_Path
        {
            get { return _RDW_Path; }
            set { _RDW_Path = value; }
        }

        private string _RDW_fromDocID;
        /// <summary>
        /// 存储在磁盘上的名称
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