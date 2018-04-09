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
    /// Project Message��Ϣ
    /// </summary>
    public class PM_Message
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public PM_Message()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ������
        /// </summary>
        private long _PM_MsgID;
        public long PM_MsgID
        {
            get { return _PM_MsgID; }
            set { _PM_MsgID = value; }
        }

        /// <summary>
        /// PM_MstrID
        /// </summary>
        private long _PM_MstrID;
        public long PM_MstrID
        {
            get { return _PM_MstrID; }
            set { _PM_MstrID = value; }
        }

        /// <summary>
        /// �㱨��ID
        /// </summary>
        private long _PM_CreatedBy;
        public long PM_CreatedBy
        {
            get { return _PM_CreatedBy; }
            set { _PM_CreatedBy = value; }
        }

        /// <summary>
        /// �㱨��
        /// </summary>
        private string _PM_Reporter;
        public string PM_Reporter
        {
            get { return _PM_Reporter; }
            set { _PM_Reporter = value; }
        }

        /// <summary>
        /// �㱨����
        /// </summary>
        private string _PM_ReportDate;
        public string PM_ReportDate
        {
            get { return _PM_ReportDate; }
            set { _PM_ReportDate = value; }
        }

        /// <summary>
        /// ��Ϣ
        /// </summary>
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
    }
}
