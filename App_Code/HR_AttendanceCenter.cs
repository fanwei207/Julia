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
    /// Ա�����ڳɱ����� DB:hr_Attendance_CC
    /// </summary>
    public class HR_AttendanceCenter
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public HR_AttendanceCenter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //������ID
        private long _CenterID;
        public long CenterID
        {
            get { return _CenterID; }
            set { _CenterID = value; }
        }

        //�����豸��
        private string _Sensor;
        public string Sensor
        {
            get { return _Sensor; }
            set { _Sensor = value; }
        }

        //������˾orgID
        private int _orgID;
        public int orgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        //�ɱ����Ĵ���
        private string _CenterCode;
        public string CenterCode
        {
            get { return _CenterCode; }
            set { _CenterCode = value; }
        }

        //�ɱ���������
        private string _CenterName;
        public string CenterName
        {
            get { return _CenterName; }
            set { _CenterName = value; }
        }

        //�ɱ�����(�ɱ����Ĵ��� + ����)
        private string _Center;
        public string Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

        //Ա������
        private string _UserNo;
        public string UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }

        //Ա��ID
        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
    }
}