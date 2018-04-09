using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TCPC.ClassFactory
{
    /// <summary>
    /// 存储Session的副本
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
       private int _userID;

        /// <summary>
        /// 用户Name
        /// </summary>
        private string _userName;

        /// <summary>
        /// 用户Role
        /// </summary>
        private string _userRole;

        public LoginInfo(Page page)
        {
            string _Queue = ConfigurationManager.AppSettings["SessionQueue"].ToString();
            string _Separator = ConfigurationManager.AppSettings["SessionSeparator"].ToString();

            if (page != null)
            {
                
            }
            else
            {
                this._userID = 0;
                this._userName = "";
                this._userRole = "";
            }
        }


    }
}