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
    /// 需要商检号的系列信息
    /// </summary>
    public class SID_QA
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID_QA()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 系列号
        /// </summary>
        private string _SNO;
        public string SNO
        {
            get { return _SNO; }
            set { _SNO = value; }
        }

        /// <summary>
        /// 系列说明
        /// </summary>
        private string _SDESC;
        public string SDESC
        {
            get { return _SDESC; }
            set { _SDESC = value; }
        }
    }
}