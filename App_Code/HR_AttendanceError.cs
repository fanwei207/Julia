using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Wage
{
    /// <summary>
    /// Ա��������Ϣ DB:hr_AttendanceAllInfo
    /// </summary>
    public class hr_AttendanceError
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public hr_AttendanceError()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //Ա������
        private string _AttendanceUserName;
        public string AttendanceUserName
        {
            get { return _AttendanceUserName; }
            set { _AttendanceUserName = value; }
        }

        //Ա������
        private string _AttendanceUserCode;
        public string AttendanceUserCode
        {
            get { return _AttendanceUserCode; }
            set { _AttendanceUserCode = value; }
        }

        //Ա�����Ż���ָ�Ʊ��
        private string _AttendanceUserNo;
        public string AttendanceUserNo
        {
            get { return _AttendanceUserNo; }
            set { _AttendanceUserNo = value; }
        }

        //��������
        private string _AttendanceType;
        public string AttendanceType
        {
            get { return _AttendanceType; }
            set { _AttendanceType = value; }
        }

        //��������
        private string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        //��������
        private string _AttendanceDate;
        public string AttendanceDate
        {
            get { return _AttendanceDate; }
            set { _AttendanceDate = value; }
        }
    }
}