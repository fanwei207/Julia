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
    /// �����Ϣ DB:hr_leave_mstr
    /// </summary>
    public class HR_LeaveInfo
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
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
        
        //�����ID
        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID=value; }
        }  

        //����˹���
        private string _UserCode;
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        //���������
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        
        //��ٿ�ʼ����
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate=value; }
        }

        //��ٽ�������
        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate=value; }
        }

        //��ٱ�ע
        private string _Comment;
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        // ��ٴ�����ID
        private int _CreatedBy;   
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        //��ٴ�����
        private string _Creater;
        public string Creater
        {
            get { return _Creater; }
            set { _Creater = value; }
        }

        // ��ٴ�������
        private DateTime _CreatedDate; 
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        //��������
        private decimal _SickDays;
        public decimal SickDays
        {
            get { return _SickDays; }
            set { _SickDays = value; }
        }

        //�¼�����
        private decimal _BussinessDays;
        public decimal BussinessDays
        {
            get { return _BussinessDays; }
            set { _BussinessDays = value; }
        }

        //�������
        private decimal _MerrageDays;
        public decimal MerrageDays
        {
            get { return _MerrageDays; }
            set { _MerrageDays = value; }
        }

        //ɥ������
        private decimal _FuneralDays;
        public decimal FuneralDays
        {
            get { return _FuneralDays; }
            set { _FuneralDays = value; }
        }

        //������
        private decimal _MinerDays;
        public decimal MinerDays
        {
            get { return _MinerDays; }
            set { _MinerDays = value; }
        }

        //�������
        private decimal _Days;
        public decimal Days
        {
            get { return _Days; }
            set { _Days = value; }
        }
    }
}