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
    /// ��֤����
    /// </summary>
    public class SID_DeclarationRpt
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public SID_DeclarationRpt()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ���ط�Ʊǰ׺
        /// </summary>
        private string _DeclarationPrefix;
        public string DeclarationPrefix
        {
            get { return _DeclarationPrefix; }
            set { _DeclarationPrefix = value; }
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
        /// ���ط�Ʊ���
        /// </summary>
        private decimal _DeclarationAmount;
        public decimal DeclarationAmount
        {
            get { return _DeclarationAmount; }
            set { _DeclarationAmount = value; }
        }

        /// <summary>
        /// ���ػ�������
        /// </summary>
        private int _DeclarationCount;
        public int DeclarationCount
        {
            get { return _DeclarationCount; }
            set { _DeclarationCount = value; }
        }
    }
}
