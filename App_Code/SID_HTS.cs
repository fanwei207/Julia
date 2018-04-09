using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace QADSID
{
    /// <summary>
    /// 发票信息汇总
    /// </summary>
    public class SID_HTS
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_HTS()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// TCP Item Code
        /// </summary>
        private string _TcpItem;
        public string TcpItem
        {
            get { return _TcpItem; }
            set { _TcpItem = value; }
        }

        /// <summary>
        /// HTS Code
        /// </summary>
        private string _HtsCode;
        public string HtsCode
        {
            get { return _HtsCode; }
            set { _HtsCode = value; }
        }

        /// <summary>
        /// HTS Description
        /// </summary>
        private string _HtsDesc;
        public string HtsDesc
        {
            get { return _HtsDesc; }
            set { _HtsDesc = value; }
        }
    }
}

