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
    /// ������Ϣ
    /// </summary>
    public class SID_DeclarationInfo
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public SID_DeclarationInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ������
        /// </summary>
        private long _ID;
        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }   
 
        /// <summary>
        /// SID_Mstr��SID_ID
        /// </summary>
        private long _SID_ID;
        public long SID_ID
        {
            get { return _SID_ID; }
            set { _SID_ID = value; }
        }

        /// <summary>
        /// SID_Det��SID_DID
        /// </summary>
        private long _SID_DID;
        public long SID_DID
        {
            get { return _SID_DID; }
            set { _SID_DID = value; }
        }

        /// <summary>
        /// SID_Dets��SID_SDID
        /// </summary>
        private long _SID_SDID;
        public long SID_SDDID
        {
            get { return _SID_SDID; }
            set { _SID_SDID = value; }
        } 

        /// <summary>
        /// ���˱��
        /// </summary>
        private string _ShipNo;
        public string ShipNo
        {
            get { return _ShipNo; }
            set { _ShipNo = value; }
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
        /// ��������
        /// </summary>
        private DateTime _SDate;
        public DateTime SDate
        {
            get { return _SDate; }
            set { _SDate = value; }
        }

        /// <summary>
        /// �ջ��ˣ���˾
        /// </summary>
        private string _Customer;
        public string Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }

        /// <summary>
        /// ���˸ۿ�(Ŀ�ĸ�)
        /// </summary>
        private string _Harbor;
        public string Harbor
        {
            get { return _Harbor; }
            set { _Harbor = value; }
        }

        /// <summary>
        /// ���䷽ʽ
        /// </summary>
        private string _ShipVia;
        public string ShipVia
        {
            get { return _ShipVia; }
            set { _ShipVia = value; }
        }

        /// <summary>
        /// ó�׷�ʽ
        /// </summary>
        private string _Trade;
        public string Trade
        {
            get { return _Trade; }
            set { _Trade = value; }
        }

        /// <summary>
        /// ���乤������
        /// </summary>
        private string _Conveyance;
        public string Conveyance
        {
            get { return _Conveyance; }
            set { _Conveyance = value; }
        }

        /// <summary>
        /// ���˵���
        /// </summary>
        private string _BL;
        public string BL
        {
            get { return _BL; }
            set { _BL = value; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        private string _Verfication;
        public string Verfication
        {
            get { return _Verfication; }
            set { _Verfication = value; }
        }

        /// <summary>
        /// ��ͬ����
        /// </summary>
        private string _Contact;
        public string Contact
        {
            get { return _Contact; }
            set { _Contact = value; }
        }

        /// <summary>
        /// �˵ֹ�
        /// </summary>
        private string _Country;
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        /// <summary>
        /// ����ϵ�к�
        /// </summary>
        private string _SNBR;
        public string SNBR
        {
            get { return _SNBR; }
            set { _SNBR = value; }
        }

        /// <summary>
        /// ����ϵ�к�
        /// </summary>
        private string _SNO;
        public string SNO
        {
            get { return _SNO; }
            set { _SNO = value; }
        }

        /// <summary>
        /// ����ϵ������
        /// </summary>
        private string _SCode;
        public string SCode
        {
            get { return _SCode; }
            set { _SCode = value; }
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
        /// ����
        /// </summary>
        private decimal _QtySet;
        public decimal QtySet
        {
            get { return _QtySet; }
            set { _QtySet = value; }
        }

        /// <summary>
        /// ֻ��
        /// </summary>
        private decimal _QtyPcs;
        public decimal QtyPcs
        {
            get { return _QtyPcs; }
            set { _QtyPcs = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private decimal _QtyBox;
        public decimal QtyBox
        {
            get { return _QtyBox; }
            set { _QtyBox = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private decimal _QtyPkgs;
        public decimal QtyPkgs
        {
            get { return _QtyPkgs; }
            set { _QtyPkgs = value; }
        }

        /// <summary>
        /// QA�̼��
        /// </summary>
        private string _QA;
        public string QA
        {
            get { return _QA; }
            set { _QA = value; }
        }

        /// <summary>
        /// �۸�
        /// </summary>
        private decimal _Price;
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        /// <summary>
        /// �ͻ�������PO
        /// </summary>
        private string _PO;
        public string PO
        {
            get { return _PO; }
            set { _PO = value; }
        }

        /// <summary>
        /// �ͻ�������FOB
        /// </summary>
        private string _FOB;
        public string FOB
        {
            get { return _FOB; }
            set { _FOB = value; }
        }

        /// <summary>
        /// ë��
        /// </summary>
        private decimal _Weight;
        public decimal Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private decimal _Net;
        public decimal Net
        {
            get { return _Net; }
            set { _Net = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        private decimal _Volume;
        public decimal Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        private int _uID;
        public int uID
        {
            get { return _uID; }
            set { _uID = value; }
        }

        /// <summary>
        /// �޸���
        /// </summary>
        private int _ModifiedBy;
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        private DateTime _ModifiedDate;
        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        /// <summary>
        /// �ܼ�
        /// </summary>
        private decimal _Amount;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private decimal _AvgPrice;
        public decimal AvgPrice
        {
            get { return _AvgPrice; }
            set { _AvgPrice = value; }
        }

        /// <summary>
        /// ԭʼ�ܼ�
        /// </summary>
        private decimal _FixAmount;
        public decimal FixAmount
        {
            get { return _FixAmount; }
            set { _FixAmount = value; }
        }

        /// <summary>
        /// �ܼ۲���
        /// </summary>
        private decimal _Diff;
        public decimal Diff
        {
            get { return _Diff; }
            set { _Diff = value; }
        }

        /// <summary>
        /// �޸�״̬
        /// </summary>
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        /// <summary>
        /// ˰��Ʊ��
        /// </summary>
        private string _Tax;
        public string Tax
        {
            get { return _Tax; }
            set { _Tax = value; }
        }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        /// <summary>
        /// ��ƷƷ��
        /// </summary>
        private string _CodeCmmt;
        public string CodeCmmt
        {
            get { return _CodeCmmt; }
            set { _CodeCmmt = value; }
        }

        /// <summary>
        /// ��ƷƷ��
        /// </summary>
        private decimal _Old_Price;
        public decimal Old_Price
        {
            get { return _Old_Price; }
            set { _Old_Price = value; }
        }

                /// <summary>
        /// ��ƷƷ��
        /// </summary>
        private decimal _New_Price;
        public decimal New_Price
        {
            get { return _New_Price; }
            set { _New_Price = value; }
        }

        /// <summary>
        /// �ܾ���
        /// </summary>
        private decimal _TotalNet;
        public decimal TotalNet
        {
            get { return _TotalNet; }
            set { _TotalNet = value; }
        }
    }
}