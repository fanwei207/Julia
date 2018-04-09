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
    /// 员工考勤信息 DB:hr_AttendanceAllInfo
    /// </summary>
    public class hr_AttendanceError
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public hr_AttendanceError()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //员工姓名
        private string _AttendanceUserName;
        public string AttendanceUserName
        {
            get { return _AttendanceUserName; }
            set { _AttendanceUserName = value; }
        }

        //员工工号
        private string _AttendanceUserCode;
        public string AttendanceUserCode
        {
            get { return _AttendanceUserCode; }
            set { _AttendanceUserCode = value; }
        }

        //员工卡号或者指纹编号
        private string _AttendanceUserNo;
        public string AttendanceUserNo
        {
            get { return _AttendanceUserNo; }
            set { _AttendanceUserNo = value; }
        }

        //考勤类型
        private string _AttendanceType;
        public string AttendanceType
        {
            get { return _AttendanceType; }
            set { _AttendanceType = value; }
        }

        //所属部门
        private string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        //考勤日期
        private string _AttendanceDate;
        public string AttendanceDate
        {
            get { return _AttendanceDate; }
            set { _AttendanceDate = value; }
        }
    }
}