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
    /// δ����ϵͳ���˵���Ϣ
    /// </summary>
    public class SID_PackingInfo
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public SID_PackingInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ������
        /// </summary>
        private int _SID;
        public int SID
        {
            get { return _SID; }
            set { _SID = value; }
        }    
    
        /// <summary>
        /// ϵͳ���˵���
        /// </summary>
        private string _PK;
        public string PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        /// <summary>
        /// ���˵���
        /// </summary>
        private string _Nbr;
        public string Nbr
        {
            get { return _Nbr; }
            set { _Nbr = value; }
        }

        /// <summary>
        /// ��֤��Ʊ
        /// </summary>
        private string _Shipno;
        public string Shipno
        {
            get { return _Shipno; }
            set { _Shipno = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string _OutDate;
        public string OutDate
        {
            get { return _OutDate; }
            set { _OutDate = value; }
        }

        /// <summary>
        /// ���䷽ʽ
        /// </summary>
        private string _Via;
        public string Via
        {
            get { return _Via; }
            set { _Via = value; }
        }

        /// <summary>
        /// ��װ������
        /// </summary>
        private string _Container;
        public string Container
        {
            get { return _Container; }
            set { _Container = value; }
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
        /// ��Ʊ����
        /// </summary>
        private string _PackingDate;
        public string PackingDate
        {
            get { return _PackingDate; }
            set { _PackingDate = value; }
        }


        /// <summary>
        /// ����
        /// </summary>
        private string _ShipTo;
        public string ShipTo
        {
            get { return _ShipTo; }
            set { _ShipTo = value; }
        }

        /// <summary>
        /// װ��ص�
        /// </summary>
        private string _Site;
        public string Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        /// <summary>
        /// ϵͳ���˵��ο���
        /// </summary>
        private string _PKRef;
        public string PKRef
        {
            get { return _PKRef; }
            set { _PKRef = value; }
        }

        /// <summary>
        /// ��
        /// </summary>
        private string _Domain;
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        /// <summary>
        /// ����ȷ��״̬
        /// </summary>
        private bool _SID_org1_con;
        public bool SID_org1_con
        {
            get { return _SID_org1_con; }
            set { _SID_org1_con = value; }
        }


        /// <summary>
        /// ����ȷ�Ϲ���
        /// </summary>
        private string _SID_org1_uid;
        public string SID_org1_uid
        {
            get { return _SID_org1_uid; }
            set { _SID_org1_uid = value; }
        }


        /// <summary>
        /// ����ȷ��״̬
        /// </summary>
        private bool _SID_org2_con;
        public bool SID_org2_con
        {
            get { return _SID_org2_con; }
            set { _SID_org2_con = value; }
        }

        /// <summary>
        /// ����ȷ�Ϲ���
        /// </summary>
        private string _SID_org2_uid;
        public string SID_org2_uid
        {
            get { return _SID_org2_uid; }
            set { _SID_org2_uid = value; }
        }

        /// <summary>
        /// ���ؾܾ�ȷ��״̬
        /// </summary>
        private bool _SID_org3_con;
        public bool SID_org3_con
        {
            get { return _SID_org3_con; }
            set { _SID_org3_con = value; }
        }

        /// <summary>
        /// ���ؾܾ�ȷ�Ϲ���
        /// </summary>
        private string _SID_org3_uid;
        public string SID_org3_uid
        {
            get { return _SID_org3_uid; }
            set { _SID_org3_uid = value; }
        }

        /// <summary>
        /// ȷ����ID
        /// </summary>
        private int _ApprovedBy;
        public int ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }   

        /// <summary>
        /// ������ID
        /// </summary>
        private int _CreatedBy;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }  

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        /// <summary>
        /// �ϲ����˵�����װ�䵥
        /// </summary>
        private string _NbrCombine;
        public string NbrCombine
        {
            get { return _NbrCombine; }
            set { _NbrCombine = value; }
        }

    }
}