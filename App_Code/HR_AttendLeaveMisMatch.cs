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
    /// ���ʿ���ƥ��
    /// </summary>
    public class HR_AttendLeaveMisMatch
    {
        public HR_AttendLeaveMisMatch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //�к�
        private int _RowNo;
        public int RowNo
        {
            get { return _RowNo; }
            set { _RowNo = value; }
        }

        //����
        private string _UserCode;
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        //����
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //��������
        private DateTime _AttendDate;
        public DateTime AttendDate
        {
            get { return _AttendDate; }
            set { _AttendDate = value; }
        }

        //����
        private string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        //����
        private string _Construct;
        public string Construct
        {
            get { return _Construct; }
            set { _Construct = value; }
        }

        //��������
        private decimal _AttendDays;
        public decimal AttendDays
        {
            get { return _AttendDays; }
            set { _AttendDays = value; }
        }

        //�������
        private decimal _LeaveDays;
        public decimal LeaveDays
        {
            get { return _LeaveDays; }
            set { _LeaveDays = value; }
        }
    }
}