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
    /// 项目信息
    /// </summary>
    public class PM_Header
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public PM_Header()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 自增长
        /// </summary>
        private long _PM_MstrID;
        public long PM_MstrID
        {
            get { return _PM_MstrID; }
            set { _PM_MstrID = value; }
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
        /// 项目内容和目标
        /// </summary>
        private string _PM_Content;
        public string PM_Content
        {
            get { return _PM_Content; }
            set { _PM_Content = value; }
        }

        /// <summary>
        /// 立项日期
        /// </summary>
        private string _PM_ProjectDate;
        public string PM_ProjectDate
        {
            get { return _PM_ProjectDate; }
            set { _PM_ProjectDate = value; }
        }

        /// <summary>
        /// 关闭日期
        /// </summary>
        private string _PM_CloseDate;
        public string PM_CloseDate
        {
            get { return _PM_CloseDate; }
            set { _PM_CloseDate = value; }
        }

        /// <summary>
        /// 奖罚方案
        /// </summary>
        private string _PM_Memo;
        public string PM_Memo
        {
            get { return _PM_Memo; }
            set { _PM_Memo = value; }
        }

        /// <summary>
        /// 参与人员ID
        /// </summary>
        private string _PM_Partner;
        public string PM_Partner
        {
            get { return _PM_Partner; }
            set { _PM_Partner = value; }
        }

        /// <summary>
        /// 参与人员名字
        /// </summary>
        private string _PM_PartnerName;
        public string PM_PartnerName
        {
            get { return _PM_PartnerName; }
            set { _PM_PartnerName = value; }
        }

        /// <summary>
        /// 评审状态
        /// </summary>
        private string _PM_Status;
        public string PM_Status
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
        /// 立项部门
        /// </summary>
        private string _PM_Dept;
        public string PM_Dept
        {
            get { return _PM_Dept; }
            set { _PM_Dept = value; }
        }

        /// <summary>
        /// 计划完成日期
        /// </summary>
        private string _PM_FinDate;
        public string PM_FinDate
        {
            get { return _PM_FinDate; }
            set { _PM_FinDate = value; }
        }

        /// <summary>
        /// 预计人员费用
        /// </summary>
        private string _PM_HumanFee;
        public string PM_HumanFee
        {
            get { return _PM_HumanFee; }
            set { _PM_HumanFee = value; }
        }

        /// <summary>
        /// 预计设备费用
        /// </summary>
        private string _PM_EquipFee;
        public string PM_EquipFee
        {
            get { return _PM_EquipFee; }
            set { _PM_EquipFee = value; }
        }

        /// <summary>
        /// 预计软件费用
        /// </summary>
        private string _PM_SoftFee;
        public string PM_SoftFee
        {
            get { return _PM_SoftFee; }
            set { _PM_SoftFee = value; }
        }
    }
}