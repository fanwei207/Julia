using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WO2Group
{
    /// <summary>
    /// �û�����Ϣ DB:Wo2_group_detail
    /// </summary>
    public class WO2_GroupDetail
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public WO2_GroupDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //�û�����ϸID
        private int _DetailID;
        public int DetailID
        {
            get { return _DetailID; }
            set { _DetailID = value; }
        }

        //�û���ID
        private int _GroupID;
        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        //�û�ID
        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        //�û�����
        private string _UserNo;
        public string UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }

        //�û�����
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //����ID
        private int _MOPID;
        public int MOPID
        {
            get { return _MOPID; }
            set { _MOPID = value; }
        }

        //�������
        private string _MOPProc;
        public string MOPProc
        {
            get { return _MOPProc; }
            set { _MOPProc = value; }
        }

        //��������
        private string _MOPName;
        public string MOPName
        {
            get { return _MOPName; }
            set { _MOPName = value; }
        }

        //��λID
        private int _SOPID;
        public int SOPID
        {
            get { return _SOPID; }
            set { _SOPID = value; }
        }

        //��λ����
        private string _SOPProc;
        public string SOPProc
        {
            get { return _SOPProc; }
            set { _SOPProc = value; }
        }

        //��λ����
        private string _SOPName;
        public string SOPName
        {
            get { return _SOPName; }
            set { _SOPName = value; }
        }

        //��λϵ��
        private decimal _SOPRate;
        public decimal SOPRate
        {
            get { return _SOPRate; }
            set { _SOPRate = value; }
        }

        // �û���ϸ������
        private int _CreatedBy;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        // �û���ϸ��������
        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
    }
}