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
    /// 考勤需采集的门禁控制器设备号 DB:hr_AttendanceAccess
    /// </summary>
    public class HR_AttendanceAccess
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public HR_AttendanceAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //考勤门禁控制器设备号
        private string _SensorNo;
        public string SensorNo
        {
            get { return _SensorNo; }
            set { _SensorNo = value; }
        }

        //所属公司plantID
        private int _orgID;
        public int orgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }
    }
}