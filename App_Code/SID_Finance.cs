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
    public class SID_Finance
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_Finance()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Flag
        /// </summary>
        private string _Flag;
        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        /// <summary>
        /// Domain
        /// </summary>
        private string _Domain;
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        /// <summary>
        /// Invoice
        /// </summary>
        private string _Invoice;
        public string Invoice
        {
            get { return _Invoice; }
            set { _Invoice = value; }
        }

        /// <summary>
        /// Effective Date
        /// </summary>
        private string _EffDate;
        public string EffDate
        {
            get { return _EffDate; }
            set { _EffDate = value; }
        }

        /// <summary>
        /// Bill To
        /// </summary>
        private string _Bill;
        public string Bill
        {
            get { return _Bill; }
            set { _Bill = value; }
        }

        /// <summary>
        /// Sell To
        /// </summary>
        private string _Sell;
        public string Sell
        {
            get { return _Sell; }
            set { _Sell = value; }
        }

        /// <summary>
        /// Ship To
        /// </summary>
        private string _Ship;
        public string Ship
        {
            get { return _Ship; }
            set { _Ship = value; }
        }

        /// <summary>
        /// Currency
        /// </summary>
        private string _Curr;
        public string Curr
        {
            get { return _Curr; }
            set { _Curr = value; }
        }

        /// <summary>
        /// ATL Amount
        /// </summary>
        private decimal _ATLAmount;
        public decimal ATLAmount
        {
            get { return _ATLAmount; }
            set { _ATLAmount = value; }
        }

        /// <summary>
        /// TCP Amount
        /// </summary>
        private decimal _TCPAmount;
        public decimal TCPAmount
        {
            get { return _TCPAmount; }
            set { _TCPAmount = value; }
        }

        /// <summary>
        /// Tax No
        /// </summary>
        private string _TaxNo;
        public string TaxNo
        {
            get { return _TaxNo; }
            set { _TaxNo = value; }
        }


        /// <summary>
        /// Invoice2
        /// </summary>
        private string _Invoice2;
        public string Invoice2
        {
            get { return _Invoice2; }
            set { _Invoice2 = value; }
        }
    }
}

