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
    /// 员工考勤成本中心 DB:hr_Attendance_CC
    /// </summary>
    public class HR_AttendanceCenter
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public HR_AttendanceCenter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //自增长ID
        private long _CenterID;
        public long CenterID
        {
            get { return _CenterID; }
            set { _CenterID = value; }
        }

        //考勤设备号
        private string _Sensor;
        public string Sensor
        {
            get { return _Sensor; }
            set { _Sensor = value; }
        }

        //所属公司orgID
        private int _orgID;
        public int orgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        //成本中心代码
        private string _CenterCode;
        public string CenterCode
        {
            get { return _CenterCode; }
            set { _CenterCode = value; }
        }

        //成本中心描述
        private string _CenterName;
        public string CenterName
        {
            get { return _CenterName; }
            set { _CenterName = value; }
        }

        //成本中心(成本中心代码 + 描述)
        private string _Center;
        public string Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

        //员工工号
        private string _UserNo;
        public string UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }

        //员工ID
        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
    }
}