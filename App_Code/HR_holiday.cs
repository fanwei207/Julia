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
    /// 国定假日 DB:hr_holiday_mstr
    /// </summary>
    public class HR_HolidayInfo
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public HR_HolidayInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }        
        
        //HolidayID
        private int _HolidayID;    
        public int HolidayID
        {
            get { return _HolidayID; }
            set { _HolidayID = value; }
        }        
        
        // 国定假日
        private DateTime _HolidayDate;  
        public DateTime HolidayDate
        {
            get { return _HolidayDate; }
            set { _HolidayDate = value; }
        }        
        
        // 国定假日所在年
        private int _HolidayYear;  
        public int HolidayYear
        {
            get { return _HolidayYear; }
            set { _HolidayYear = value; }
        }        
        
        // 国定假日所在月
        private int _HolidayMonth;        
        public int HolidayMonth
        {
            get { return _HolidayMonth; }
            set { _HolidayMonth = value; }
        }        
        
        // 国定假日创建人
        private int _CreatedBy;   
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }        
        
        // 国定假日创建日期
        private DateTime _CreatedDate; 
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
    }
}