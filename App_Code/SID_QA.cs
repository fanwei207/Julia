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
    /// ��Ҫ�̼�ŵ�ϵ����Ϣ
    /// </summary>
    public class SID_QA
    {
        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public SID_QA()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ϵ�к�
        /// </summary>
        private string _SNO;
        public string SNO
        {
            get { return _SNO; }
            set { _SNO = value; }
        }

        /// <summary>
        /// ϵ��˵��
        /// </summary>
        private string _SDESC;
        public string SDESC
        {
            get { return _SDESC; }
            set { _SDESC = value; }
        }
    }
}