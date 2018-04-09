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
    /// �û�����Ϣ DB:wo2_group
    /// </summary>
    public class WO2_Group
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public WO2_Group()
        {
            //
            // TODO: Add constructor logic here
            //
        }    

        //�û���ID
        private int _GroupID;
        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        //�û������
        private string _GroupCode;
        public string GroupCode
        {
            get { return _GroupCode; }
            set { _GroupCode = value; }
        }

        //�û�������
        private string _GroupName;
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        //�û���Ա����
        private int _GroupCount;
        public int GroupCount
        {
            get { return _GroupCount; }
            set { _GroupCount = value; }
        }

        // �û��鴴����
        private int _CreatedBy;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        // �û��鴴������
        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
    }
}