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
    public class SID_PackingListInfo
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_PackingListInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// PO行号
        /// </summary>
        private string _No;
        public string No
        {
            get { return _No; }
            set { _No = value; }
        }

        /// <summary>
        /// 客户订单号PO
        /// </summary>
        private string _PO;
        public string PO
        {
            get { return _PO; }
            set { _PO = value; }
        }

        /// <summary>
        /// 出运单号
        /// </summary>
        private string _nbr;
        public string nbr
        {
            get { return _nbr; }
            set { _nbr = value; }
        }

        /// <summary>
        /// QAD
        /// </summary>
        private string _QAD;
        public string QAD
        {
            get { return _QAD; }
            set { _QAD = value; }
        }

        /// <summary>
        /// 部件号
        /// </summary>
        private string _Part;
        public string Part
        {
            get { return _Part; }
            set { _Part = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string _Descriptions;
        public string Descriptions
        {
            get { return _Descriptions; }
            set { _Descriptions = value; }
        }

        /// <summary>
        /// 出运数量
        /// </summary>
        private string _Qty;
        public string Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        private string _Unit;
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }

        /// <summary>
        /// 价格1
        /// </summary>
        private decimal _Price1;
        public decimal Price1
        {
            get { return _Price1; }
            set { _Price1 = value; }
        }

        /// <summary>
        /// 价格2
        /// </summary>
        private decimal _Price2;
        public decimal Price2
        {
            get { return _Price2; }
            set { _Price2 = value; }
        }

        /// <summary>
        /// 价格3
        /// </summary>
        private decimal _Price3;
        public decimal Price3
        {
            get { return _Price3; }
            set { _Price3 = value; }
        }

        /// <summary>
        /// 计价单位
        /// </summary>
        private decimal _UM;
        public decimal UM
        {
            get { return _UM; }
            set { _UM = value; }
        }

        /// <summary>
        /// 价格
        /// </summary>
        private decimal _Price;
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        /// <summary>
        /// 币别
        /// </summary>
        private string _Currency;
        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }

        /// <summary>
        /// 总价
        /// </summary>
        private decimal _Amount;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        /// <summary>
        /// 客户附件文件保实际名
        /// </summary>
        private string _CustPoDocPath;
        public string CustPoDocPath
        {
            get { return _CustPoDocPath; }
            set { _CustPoDocPath = value; }
        }

        /// <summary>
        /// 总价
        /// </summary>
        private string _PriceIsNull;
        public string PriceIsNull
        {
            get { return _PriceIsNull; }
            set { _PriceIsNull = value; }
        }

        /// <summary>
        /// 客户附件文件保存名
        /// </summary>
        private string _CustPoDocName;
        public string CustPoDocName
        {
            get { return _CustPoDocName; }
            set { _CustPoDocName = value; }
        }
    }
}
