using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for RT_Header
/// </summary>
/// 
namespace RT_WorkFlow
{
    public class RT_Header
    {
        public RT_Header()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// 自增长
        /// </summary>
        private long _RDW_MstrID;
        public long RDW_MstrID
        {
            get { return _RDW_MstrID; }
            set { _RDW_MstrID = value; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        private string _RDW_Project;
        public string RDW_Project
        {
            get { return _RDW_Project; }
            set { _RDW_Project = value; }
        }

        /// <summary>
        /// 产品型号
        /// </summary>
        private string _RDW_ProdCode;
        public string RDW_ProdCode
        {
            get { return _RDW_ProdCode; }
            set { _RDW_ProdCode = value; }
        }

        /// <summary>
        /// 产品说明
        /// </summary>
        private string _RDW_ProdDesc;
        public string RDW_ProdDesc
        {
            get { return _RDW_ProdDesc; }
            set { _RDW_ProdDesc = value; }
        }

        /// <summary>
        /// 产品使用标准
        /// </summary>
        private string _RDW_Standard;
        public string RDW_Standard
        {
            get { return _RDW_Standard; }
            set { _RDW_Standard = value; }
        }

        /// <summary>
        /// 起始日期
        /// </summary>
        private string _RDW_StartDate;
        public string RDW_StartDate
        {
            get { return _RDW_StartDate; }
            set { _RDW_StartDate = value; }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        private string _RDW_EndDate;
        public string RDW_EndDate
        {
            get { return _RDW_EndDate; }
            set { _RDW_EndDate = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        private string _RDW_Memo;
        public string RDW_Memo
        {
            get { return _RDW_Memo; }
            set { _RDW_Memo = value; }
        }

        /// <summary>
        /// 参与人员ID
        /// </summary>
        private string _RDW_Partner;
        public string RDW_Partner
        {
            get { return _RDW_Partner; }
            set { _RDW_Partner = value; }
        }

        /// <summary>
        /// 参与人员名字
        /// </summary>
        private string _RDW_PartnerName;
        public string RDW_PartnerName
        {
            get { return _RDW_PartnerName; }
            set { _RDW_PartnerName = value; }
        }

        /// <summary>
        /// 评审状态
        /// </summary>
        private string _RDW_Status;
        public string RDW_Status
        {
            get { return _RDW_Status; }
            set { _RDW_Status = value; }
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        private long _RDW_CreatedBy;
        public long RDW_CreatedBy
        {
            get { return _RDW_CreatedBy; }
            set { _RDW_CreatedBy = value; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _RDW_Creater;
        public string RDW_Creater
        {
            get { return _RDW_Creater; }
            set { _RDW_Creater = value; }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private string _RDW_CreatedDate;
        public string RDW_CreatedDate
        {
            get { return _RDW_CreatedDate; }
            set { _RDW_CreatedDate = value; }
        }

        /// <summary>
        /// 完成日期
        /// </summary>
        private string _RDW_FinishDate;
        public string RDW_FinishDate
        {
            get { return _RDW_FinishDate; }
            set { _RDW_FinishDate = value; }
        }

        /// <summary>
        /// 当前步骤
        /// </summary>
        private string _RDW_CurrStep;
        public string RDW_CurrStep
        {
            get { return _RDW_CurrStep; }
            set { _RDW_CurrStep = value; }
        }
        /// <summary>
        /// 当前步骤ID
        /// </summary>
        private string _RDW_CurrMstrStep;
        public string RDW_CurrMstrStep
        {
            get { return _RDW_CurrMstrStep; }
            set { _RDW_CurrMstrStep = value; }
        }


        /// <summary>
        /// 超期天数
        /// </summary>
        private long _RDW_DalayDays;
        public long RDW_DalayDays
        {
            get { return _RDW_DalayDays; }
            set { _RDW_DalayDays = value; }
        }

        /// <summary>
        /// 审核人
        /// </summary>
        private string _RDW_Mbr;
        public string RDW_Mbr
        {
            get { return _RDW_Mbr; }
            set { _RDW_Mbr = value; }
        }

        /// <summary>
        /// 参与人
        /// </summary>
        private string _RDW_Ptr;
        public string RDW_Ptr
        {
            get { return _RDW_Ptr; }
            set { _RDW_Ptr = value; }
        }
    }
}
