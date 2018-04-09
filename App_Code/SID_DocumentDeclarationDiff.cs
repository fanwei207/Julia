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
    /// 报关-单证差异
    /// </summary>
    public class SID_DocumentDeclarationDiff
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_DocumentDeclarationDiff()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 单证发票号
        /// </summary>
        private string _DocumentNo;
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        /// <summary>
        /// 单证发票日期
        /// </summary>
        private string _DocumentShipDate;
        public string DocumentShipDate
        {
            get { return _DocumentShipDate; }
            set { _DocumentShipDate = value; }
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
        /// 单证发票金额
        /// </summary>
        private decimal _DocumentAmount;
        public decimal DocumentAmount
        {
            get { return _DocumentAmount; }
            set { _DocumentAmount = value; }
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
        /// 发票金额差异
        /// </summary>
        private decimal _DiffAmount;
        public decimal DiffAmount
        {
            get { return _DiffAmount; }
            set { _DiffAmount = value; }
        } 
    }
}
