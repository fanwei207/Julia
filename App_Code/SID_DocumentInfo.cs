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
    /// ��Ʊ��Ϣ����
    /// </summary>
    public class SID_DocumentInfo
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public SID_DocumentInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ��������
        /// </summary>
        private int _Qty;
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        private int _Sets_Qty;
        public int Sets_Qty
        {
            get { return _Sets_Qty; }
            set { _Sets_Qty = value; }
        }
        /// <summary>
        /// �Ƽ۷�ʽ
        /// </summary>
        private string _SID_Ptype;
        public string SID_Ptype
        {
            get { return _SID_Ptype; }
            set { _SID_Ptype = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _ShipDate;
        public string ShipDate
        {
            get { return _ShipDate; }
            set { _ShipDate = value; }
        }

        /// <summary>
        /// ATL����
        /// </summary>
        private decimal _ATLPrice;
        public decimal ATLPrice
        {
            get { return _ATLPrice; }
            set { _ATLPrice = value; }
        }

        /// <summary>
        /// ATL�ܼ�
        /// </summary>
        private decimal _ATLAmount;
        public decimal ATLAmount
        {
            get { return _ATLAmount; }
            set { _ATLAmount = value; }
        }

        /// <summary>
        /// TCP����
        /// </summary>
        private decimal _TCPPrice;
        public decimal TCPPrice
        {
            get { return _TCPPrice; }
            set { _TCPPrice = value; }
        }

        /// <summary>
        /// TCP�ܼ�
        /// </summary>
        private decimal _TCPAmount;
        public decimal TCPAmount
        {
            get { return _TCPAmount; }
            set { _TCPAmount = value; }
        }

        /// <summary>
        /// ��Ʊ��
        /// </summary>
        private string _Invoice;
        public string Invoice
        {
            get { return _Invoice; }
            set { _Invoice = value; }
        }

        /// <summary>
        /// ���ϱ��
        /// </summary>
        private string _ItemNo;
        public string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _ItemDesc;
        public string ItemDesc
        {
            get { return _ItemDesc; }
            set { _ItemDesc = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        private string _OrderNo;
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        /// <summary>
        /// ���˵���
        /// </summary>
        private string _DocumentNo;
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        /// <summary>
        /// ��Ч����
        /// </summary>
        private string _Commencement;
        public string Commencement
        {
            get { return _Commencement; }
            set { _Commencement = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        private string _IsCabin;
        public string IsCabin
        {
            get { return _IsCabin; }
            set { _IsCabin = value; }
        }
    }
}
