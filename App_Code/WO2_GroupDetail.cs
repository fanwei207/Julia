using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WO2Group
{
    /// <summary>
    /// 用户组信息 DB:Wo2_group_detail
    /// </summary>
    public class WO2_GroupDetail
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public WO2_GroupDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //用户组明细ID
        private int _DetailID;
        public int DetailID
        {
            get { return _DetailID; }
            set { _DetailID = value; }
        }

        //用户组ID
        private int _GroupID;
        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        //用户ID
        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        //用户工号
        private string _UserNo;
        public string UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }

        //用户名称
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //工序ID
        private int _MOPID;
        public int MOPID
        {
            get { return _MOPID; }
            set { _MOPID = value; }
        }

        //工序代码
        private string _MOPProc;
        public string MOPProc
        {
            get { return _MOPProc; }
            set { _MOPProc = value; }
        }

        //工序名称
        private string _MOPName;
        public string MOPName
        {
            get { return _MOPName; }
            set { _MOPName = value; }
        }

        //岗位ID
        private int _SOPID;
        public int SOPID
        {
            get { return _SOPID; }
            set { _SOPID = value; }
        }

        //岗位代码
        private string _SOPProc;
        public string SOPProc
        {
            get { return _SOPProc; }
            set { _SOPProc = value; }
        }

        //岗位名称
        private string _SOPName;
        public string SOPName
        {
            get { return _SOPName; }
            set { _SOPName = value; }
        }

        //岗位系数
        private decimal _SOPRate;
        public decimal SOPRate
        {
            get { return _SOPRate; }
            set { _SOPRate = value; }
        }

        // 用户明细创建人
        private int _CreatedBy;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        // 用户明细创建日期
        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
    }
}