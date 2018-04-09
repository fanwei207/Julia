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
    /// ����-��֤����
    /// </summary>
    public class SID_DocumentDeclarationDiff
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public SID_DocumentDeclarationDiff()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ��֤��Ʊ��
        /// </summary>
        private string _DocumentNo;
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        /// <summary>
        /// ��֤��Ʊ����
        /// </summary>
        private string _DocumentShipDate;
        public string DocumentShipDate
        {
            get { return _DocumentShipDate; }
            set { _DocumentShipDate = value; }
        }

        /// <summary>
        /// ���ط�Ʊ��
        /// </summary>
        private string _DeclarationNo;
        public string DeclarationNo
        {
            get { return _DeclarationNo; }
            set { _DeclarationNo = value; }
        }

        /// <summary>
        /// ���ط�Ʊ����
        /// </summary>
        private string _DeclarationShipDate;
        public string DeclarationShipDate
        {
            get { return _DeclarationShipDate; }
            set { _DeclarationShipDate = value; }
        }

        /// <summary>
        /// ��֤��Ʊ���
        /// </summary>
        private decimal _DocumentAmount;
        public decimal DocumentAmount
        {
            get { return _DocumentAmount; }
            set { _DocumentAmount = value; }
        }

        /// <summary>
        /// ���ط�Ʊ���
        /// </summary>
        private decimal _DeclarationAmount;
        public decimal DeclarationAmount
        {
            get { return _DeclarationAmount; }
            set { _DeclarationAmount = value; }
        }

        /// <summary>
        /// ��Ʊ������
        /// </summary>
        private decimal _DiffAmount;
        public decimal DiffAmount
        {
            get { return _DiffAmount; }
            set { _DiffAmount = value; }
        } 
    }
}
