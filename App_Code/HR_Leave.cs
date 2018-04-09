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
    /// 请假信息 DB:hr_leave_mstr
    /// </summary>
    public class HR_LeaveInfo
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public HR_LeaveInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //LeaveID
        private int _LeaveID;
        public int LeaveID
        {
            get { return _LeaveID; }
            set { _LeaveID=value; }
        }    
        
        //请假人ID
        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID=value; }
        }  

        //请假人工号
        private string _UserCode;
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        //请假人姓名
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        
        //请假开始日期
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate=value; }
        }

        //请假结束日期
        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate=value; }
        }

        //请假备注
        private string _Comment;
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        // 请假创建人ID
        private int _CreatedBy;   
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        //请假创建人
        private string _Creater;
        public string Creater
        {
            get { return _Creater; }
            set { _Creater = value; }
        }

        // 请假创建日期
        private DateTime _CreatedDate; 
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        //病假天数
        private decimal _SickDays;
        public decimal SickDays
        {
            get { return _SickDays; }
            set { _SickDays = value; }
        }

        //事假天数
        private decimal _BussinessDays;
        public decimal BussinessDays
        {
            get { return _BussinessDays; }
            set { _BussinessDays = value; }
        }

        //婚假天数
        private decimal _MerrageDays;
        public decimal MerrageDays
        {
            get { return _MerrageDays; }
            set { _MerrageDays = value; }
        }

        //丧假天数
        private decimal _FuneralDays;
        public decimal FuneralDays
        {
            get { return _FuneralDays; }
            set { _FuneralDays = value; }
        }

        //矿工天数
        private decimal _MinerDays;
        public decimal MinerDays
        {
            get { return _MinerDays; }
            set { _MinerDays = value; }
        }

        //请假天数
        private decimal _Days;
        public decimal Days
        {
            get { return _Days; }
            set { _Days = value; }
        }
    }
}