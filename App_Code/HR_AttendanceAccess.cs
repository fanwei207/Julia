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
    /// ������ɼ����Ž��������豸�� DB:hr_AttendanceAccess
    /// </summary>
    public class HR_AttendanceAccess
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public HR_AttendanceAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //�����Ž��������豸��
        private string _SensorNo;
        public string SensorNo
        {
            get { return _SensorNo; }
            set { _SensorNo = value; }
        }

        //������˾plantID
        private int _orgID;
        public int orgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }
    }
}