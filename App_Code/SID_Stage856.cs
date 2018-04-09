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
    public class SID_Stage856
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_Stage856()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 自增长
        /// </summary>
        private long _ID;
        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// Master Invoice
        /// </summary>
        private string _MasterInvoice;
        public string MasterInvoice
        {
            get { return _MasterInvoice; }
            set { _MasterInvoice = value; }
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
        /// Ship Method
        /// </summary>
        private string _ShipMeth;
        public string ShipMeth
        {
            get { return _ShipMeth; }
            set { _ShipMeth = value; }
        }

        /// <summary>
        /// Ship Destination
        /// </summary>
        private string _ShipDest;
        public string ShipDest
        {
            get { return _ShipDest; }
            set { _ShipDest = value; }
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
        /// Container Invoice
        /// </summary>
        private string _ContainerInvoice;
        public string ContainerInvoice
        {
            get { return _ContainerInvoice; }
            set { _ContainerInvoice = value; }
        }

        /// <summary>
        /// JDE Item
        /// </summary>
        private string _Item;
        public string Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        /// <summary>
        /// Item Description
        /// </summary>
        private string _ItemDesc;
        public string ItemDesc
        {
            get { return _ItemDesc; }
            set { _ItemDesc = value; }
        }

        /// <summary>
        /// PO Type
        /// </summary>
        private string _PoType;
        public string PoType
        {
            get { return _PoType; }
            set { _PoType = value; }
        }

        /// <summary>
        /// PO Number
        /// </summary>
        private string _PoNumber;
        public string PoNumber
        {
            get { return _PoNumber; }
            set { _PoNumber = value; }
        }

        /// <summary>
        /// PO Line
        /// </summary>
        private string _PoLine;
        public string PoLine
        {
            get { return _PoLine; }
            set { _PoLine = value; }
        }

        /// <summary>
        /// Ship Quantity
        /// </summary>
        private int _QtyShip;
        public int QtyShip
        {
            get { return _QtyShip; }
            set { _QtyShip = value; }
        }

        /// <summary>
        /// Item UOM
        /// </summary>
        private string _ItemUom;
        public string ItemUom
        {
            get { return _ItemUom; }
            set { _ItemUom = value; }
        }

        /// <summary>
        /// Carton Quantity
        /// </summary>
        private int _QtyCtn;
        public int QtyCtn
        {
            get { return _QtyCtn; }
            set { _QtyCtn = value; }
        }

        /// <summary>
        /// Carton UOM
        /// </summary>
        private string _CtnUom;
        public string CtnUom
        {
            get { return _CtnUom; }
            set { _CtnUom = value; }
        }

        /// <summary>
        /// PO
        /// </summary>
        private string _PO;
        public string PO
        {
            get { return _PO; }
            set { _PO = value; }
        }

        /// <summary>
        /// Detail Count
        /// </summary>
        private int _DetailCount;
        public int DetailCount
        {
            get { return _DetailCount; }
            set { _DetailCount = value; }
        }

        /// <summary>
        /// Detail Line No
        /// </summary>
        private string _LineNo;
        public string LineNo
        {
            get { return _LineNo; }
            set { _LineNo = value; }
        }

        /// <summary>
        /// Detail Item Unit Price
        /// </summary>
        private decimal _UnitPrice;
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }

        /// <summary>
        /// Detail Item Extd Price
        /// </summary>
        private decimal _ExtdPrice;
        public decimal ExtdPrice
        {
            get { return _ExtdPrice; }
            set { _ExtdPrice = value; }
        }

        /// <summary>
        /// Detail Item Total Price
        /// </summary>
        private decimal _TotalPrice;
        public decimal TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }

        /// <summary>
        /// Total Carton Quantity
        /// </summary>
        private int _TotalCtns;
        public int TotalCtns
        {
            get { return _TotalCtns; }
            set { _TotalCtns = value; }
        }

        /// <summary>
        /// Is Finished
        /// </summary>
        private bool _isFinish;
        public bool isFinish
        {
            get { return _isFinish; }
            set { _isFinish = value; }
        }
    }
}

