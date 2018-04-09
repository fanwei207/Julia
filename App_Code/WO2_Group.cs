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
    /// 用户组信息 DB:wo2_group
    /// </summary>
    public class WO2_Group
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public WO2_Group()
        {
            //
            // TODO: Add constructor logic here
            //
        }    

        //用户组ID
        private int _GroupID;
        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        //用户组代码
        private string _GroupCode;
        public string GroupCode
        {
            get { return _GroupCode; }
            set { _GroupCode = value; }
        }

        //用户组名称
        private string _GroupName;
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        //用户组员数量
        private int _GroupCount;
        public int GroupCount
        {
            get { return _GroupCount; }
            set { _GroupCount = value; }
        }

        // 用户组创建人
        private int _CreatedBy;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        // 用户组创建日期
        private DateTime _CreatedDate;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
    }
}