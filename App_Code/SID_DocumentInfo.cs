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
    public class SID_DocumentInfo
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_DocumentInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 出运数量
        /// </summary>
        private int _Qty;
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        /// <summary>
        /// 出运套数
        /// </summary>
        private int _Sets_Qty;
        public int Sets_Qty
        {
            get { return _Sets_Qty; }
            set { _Sets_Qty = value; }
        }
        /// <summary>
        /// 计价方式
        /// </summary>
        private string _SID_Ptype;
        public string SID_Ptype
        {
            get { return _SID_Ptype; }
            set { _SID_Ptype = value; }
        }

        /// <summary>
        /// 出运日期
        /// </summary>
        private string _ShipDate;
        public string ShipDate
        {
            get { return _ShipDate; }
            set { _ShipDate = value; }
        }

        /// <summary>
        /// ATL单价
        /// </summary>
        private decimal _ATLPrice;
        public decimal ATLPrice
        {
            get { return _ATLPrice; }
            set { _ATLPrice = value; }
        }

        /// <summary>
        /// ATL总价
        /// </summary>
        private decimal _ATLAmount;
        public decimal ATLAmount
        {
            get { return _ATLAmount; }
            set { _ATLAmount = value; }
        }

        /// <summary>
        /// TCP单价
        /// </summary>
        private decimal _TCPPrice;
        public decimal TCPPrice
        {
            get { return _TCPPrice; }
            set { _TCPPrice = value; }
        }

        /// <summary>
        /// TCP总价
        /// </summary>
        private decimal _TCPAmount;
        public decimal TCPAmount
        {
            get { return _TCPAmount; }
            set { _TCPAmount = value; }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        private string _Invoice;
        public string Invoice
        {
            get { return _Invoice; }
            set { _Invoice = value; }
        }

        /// <summary>
        /// 物料编号
        /// </summary>
        private string _ItemNo;
        public string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }

        /// <summary>
        /// 物料描述
        /// </summary>
        private string _ItemDesc;
        public string ItemDesc
        {
            get { return _ItemDesc; }
            set { _ItemDesc = value; }
        }

        /// <summary>
        /// 订单号
        /// </summary>
        private string _OrderNo;
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        /// <summary>
        /// 出运单号
        /// </summary>
        private string _DocumentNo;
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        /// <summary>
        /// 生效日期
        /// </summary>
        private string _Commencement;
        public string Commencement
        {
            get { return _Commencement; }
            set { _Commencement = value; }
        }

        /// <summary>
        /// 配舱
        /// </summary>
        private string _IsCabin;
        public string IsCabin
        {
            get { return _IsCabin; }
            set { _IsCabin = value; }
        }
    }
}
