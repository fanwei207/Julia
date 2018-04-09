using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PM
{
    /// <summary>
    /// Project Detail信息
    /// </summary>
    public class PM_Detail
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public PM_Detail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 自增长
        /// </summary>
        private long _PM_DetID;
        public long PM_DetID
        {
            get { return _PM_DetID; }
            set { _PM_DetID = value; }
        }

        /// <summary>
        /// PM_MstrID
        /// </summary>
        private long _PM_MstrID;
        public long PM_MstrID
        {
            get { return _PM_MstrID; }
            set { _PM_MstrID = value; }
        }

        /// <summary>
        /// 步骤名
        /// </summary>
        private string _PM_StepName;
        public string PM_StepName
        {
            get { return _PM_StepName; }
            set { _PM_StepName = value; }
        }

        /// <summary>
        /// 步骤说明
        /// </summary>
        private string _PM_StepDesc;
        public string PM_StepDesc
        {
            get { return _PM_StepDesc; }
            set { _PM_StepDesc = value; }
        }

        /// <summary>
        /// 步骤序号
        /// </summary>
        private int _PM_Sort;
        public int PM_Sort
        {
            get { return _PM_Sort; }
            set { _PM_Sort = value; }
        }

        /// <summary>
        /// 步骤阶段状态
        /// </summary>
        private int _PM_Status;
        public int PM_Status
        {
            get { return _PM_Status; }
            set { _PM_Status = value; }
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        private long _PM_CreatedBy;
        public long PM_CreatedBy
        {
            get { return _PM_CreatedBy; }
            set { _PM_CreatedBy = value; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _PM_Creater;
        public string PM_Creater
        {
            get { return _PM_Creater; }
            set { _PM_Creater = value; }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private string _PM_CreatedDate;
        public string PM_CreatedDate
        {
            get { return _PM_CreatedDate; }
            set { _PM_CreatedDate = value; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        private string _PM_ProjName;
        public string PM_ProjName
        {
            get { return _PM_ProjName; }
            set { _PM_ProjName = value; }
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        private string _PM_ProjCode;
        public string PM_ProjCode
        {
            get { return _PM_ProjCode; }
            set { _PM_ProjCode = value; }
        }

        /// <summary>
        /// 项目说明
        /// </summary>
        private string _PM_ProjDesc;
        public string PM_ProjDesc
        {
            get { return _PM_ProjDesc; }
            set { _PM_ProjDesc = value; }
        }

        /// <summary>
        /// 项目立项日期
        /// </summary>
        private string _PM_ProjectDate;
        public string PM_ProjectDate
        {
            get { return _PM_ProjectDate; }
            set { _PM_ProjectDate = value; }
        }

        /// <summary>
        /// 步骤起始日期
        /// </summary>
        private string _PM_StepStartDate;
        public string PM_StepStartDate
        {
            get { return _PM_StepStartDate; }
            set { _PM_StepStartDate = value; }
        }

        /// <summary>
        /// 步骤结束日期
        /// </summary>
        private string _PM_StepEndDate;
        public string PM_StepEndDate
        {
            get { return _PM_StepEndDate; }
            set { _PM_StepEndDate = value; }
        }

        public enum ProcessStatus
        {
            NoDeal = 0,
            Partial = 1,
            Pass = 2,
            Fail = 3,
        }

        /// <summary>
        /// 消息ID
        /// </summary>
        private long _PM_MessID;
        public long PM_MessID
        {
            get { return _PM_MessID; }
            set { _PM_MessID = value; }
        }

        /// <summary>
        /// 消息
        /// </summary>
        private string _PM_Message;
        public string PM_Message
        {
            get { return _PM_Message; }
            set { _PM_Message = value; }
        }

        /// <summary>
        /// 评审人ID
        /// </summary>
        private string _PM_EvaluaterID;
        public string PM_EvaluaterID
        {
            get { return _PM_EvaluaterID; }
            set { _PM_EvaluaterID = value; }
        }

        /// <summary>
        /// 评审人
        /// </summary>
        private string _PM_Evaluater;
        public string PM_Evaluater
        {
            get { return _PM_Evaluater; }
            set { _PM_Evaluater = value; }
        }

        /// <summary>
        /// 参与人ID
        /// </summary>
        private string _PM_Partner;
        public string PM_Partner
        {
            get { return _PM_Partner; }
            set { _PM_Partner = value; }
        }

        /// <summary>
        /// 参与人
        /// </summary>
        private string _PM_PartnerName;
        public string PM_PartnerName
        {
            get { return _PM_PartnerName; }
            set { _PM_PartnerName = value; }
        }
    }
}
