using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace QADSID
{
    /// <summary>
    /// 未处理系统货运单信息
    /// </summary>
    public class SID_Info
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_Info()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 自增长
        /// </summary>
        private int _SID;
        public int SID
        {
            get { return _SID; }
            set { _SID = value; }
        }    
    
        /// <summary>
        /// 系统货运单号
        /// </summary>
        private string _PK;
        public string PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        /// <summary>
        /// 出运单号
        /// </summary>
        private string _Nbr;
        public string Nbr
        {
            get { return _Nbr; }
            set { _Nbr = value; }
        }

        /// <summary>
        /// 出厂日期
        /// </summary>
        private string _OutDate;
        public string OutDate
        {
            get { return _OutDate; }
            set { _OutDate = value; }
        }

        /// <summary>
        /// 运输方式
        /// </summary>
        private string _Via;
        public string Via
        {
            get { return _Via; }
            set { _Via = value; }
        }

        /// <summary>
        /// 集装箱类型
        /// </summary>
        private string _Container;
        public string Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        /// <summary>
        /// 出运日期
        /// </summary>
        private string _ShipDate;
        public string ShipDate
        {
            get { return _ShipDate; }
            set { _ShipDate = value; }
        }

        /// <summary>
        /// 运往
        /// </summary>
        private string _ShipTo;
        public string ShipTo
        {
            get { return _ShipTo; }
            set { _ShipTo = value; }
        }

        /// <summary>
        /// 装箱地点
        /// </summary>
        private string _Site;
        public string Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        /// <summary>
        /// 系统货运单参考号
        /// </summary>
        private string _PKRef;
        public string PKRef
        {
            get { return _PKRef; }
            set { _PKRef = value; }
        }

        /// <summary>
        /// 域
        /// </summary>
        private string _Domain;
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        /// <summary>
        /// 确认人ID
        /// </summary>
        private int _ApprovedBy;
        public int ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }   

        /// <summary>
        /// 创建人ID
        /// </summary>
        private int _CreatedBy;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }  

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }  
    }
}