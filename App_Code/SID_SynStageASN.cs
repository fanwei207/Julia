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
    public class SID_SynStageASN
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_SynStageASN()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Invoice Number
        /// </summary>
        private string _InvoiceNo;
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        /// <summary>
        /// Shipment Number
        /// </summary>
        private string _ShipNo;
        public string ShipNo
        {
            get { return _ShipNo; }
            set { _ShipNo = value; }
        }

        /// <summary>
        /// Ship Date
        /// </summary>
        private string _ShipDate;
        public string ShipDate
        {
            get { return _ShipDate; }
            set { _ShipDate = value; }
        }

        /// <summary>
        /// Container
        /// </summary>
        private string _Container;
        public string Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        /// <summary>
        /// ShipCount
        /// </summary>
        private int _ShipCount;
        public int ShipCount
        {
            get { return _ShipCount; }
            set { _ShipCount = value; }
        }

        /// <summary>
        /// ZhangQiZhang
        /// </summary>
        private string _ZhangQiZhang;
        public string ZhangQiZhang
        {
            get { return _ZhangQiZhang; }
            set { _ZhangQiZhang = value; }
        }
    }
}

