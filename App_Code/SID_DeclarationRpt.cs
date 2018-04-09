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
    /// 单证汇总
    /// </summary>
    public class SID_DeclarationRpt
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_DeclarationRpt()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 报关发票前缀
        /// </summary>
        private string _DeclarationPrefix;
        public string DeclarationPrefix
        {
            get { return _DeclarationPrefix; }
            set { _DeclarationPrefix = value; }
        }

        /// <summary>
        /// 报关发票号
        /// </summary>
        private string _DeclarationNo;
        public string DeclarationNo
        {
            get { return _DeclarationNo; }
            set { _DeclarationNo = value; }
        }

        /// <summary>
        /// 报关发票日期
        /// </summary>
        private string _DeclarationShipDate;
        public string DeclarationShipDate
        {
            get { return _DeclarationShipDate; }
            set { _DeclarationShipDate = value; }
        }

        /// <summary>
        /// 报关发票金额
        /// </summary>
        private decimal _DeclarationAmount;
        public decimal DeclarationAmount
        {
            get { return _DeclarationAmount; }
            set { _DeclarationAmount = value; }
        }

        /// <summary>
        /// 报关汇总数量
        /// </summary>
        private int _DeclarationCount;
        public int DeclarationCount
        {
            get { return _DeclarationCount; }
            set { _DeclarationCount = value; }
        }
    }
}
