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
    /// 工资考勤匹配
    /// </summary>
    public class HR_AttendLeaveMisMatch
    {
        public HR_AttendLeaveMisMatch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //行号
        private int _RowNo;
        public int RowNo
        {
            get { return _RowNo; }
            set { _RowNo = value; }
        }

        //工号
        private string _UserCode;
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        //姓名
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //考勤日期
        private DateTime _AttendDate;
        public DateTime AttendDate
        {
            get { return _AttendDate; }
            set { _AttendDate = value; }
        }

        //部门
        private string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        //工段
        private string _Construct;
        public string Construct
        {
            get { return _Construct; }
            set { _Construct = value; }
        }

        //考勤天数
        private decimal _AttendDays;
        public decimal AttendDays
        {
            get { return _AttendDays; }
            set { _AttendDays = value; }
        }

        //请假天数
        private decimal _LeaveDays;
        public decimal LeaveDays
        {
            get { return _LeaveDays; }
            set { _LeaveDays = value; }
        }
    }
}