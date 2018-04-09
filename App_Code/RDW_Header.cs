using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RD_WorkFlow
{
    /// <summary>
    /// R&D Work Flow信息
    /// </summary>
    public class RDW_Header
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public RDW_Header()
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

        private string _RDW_EStarDLC;
        public string RDW_EStarDLC
        {
            get { return _RDW_EStarDLC; }
            set { _RDW_EStarDLC = value; }
        }

        private string _RDW_Tier;
        public string RDW_Tier
        {
            get { return _RDW_Tier; }
            set { _RDW_Tier = value; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        private string _RDW_Priority;
        public string RDW_Priority
        {
            get { return _RDW_Priority; }
            set { _RDW_Priority = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        private string _RDW_LampType;
        public string RDW_LampType
        {
            get { return _RDW_LampType; }
            set { _RDW_LampType = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        private string _RDW_EngineerTeam;
        public string RDW_EngineerTeam
        {
            get { return _RDW_EngineerTeam; }
            set { _RDW_EngineerTeam = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        private string _RDW_Customer;
        public string RDW_Customer
        {
            get { return _RDW_Customer; }
            set { _RDW_Customer = value; }
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
        /// 产品代码
        /// </summary>
        private string _RDW_ProdCode;
        public string RDW_ProdCode
        {
            get { return _RDW_ProdCode; }
            set { _RDW_ProdCode = value; }
        }

        /// <summary>
        /// 产品SKU#
        /// </summary>
        private string _RDW_ProdSKU;
        public string RDW_ProdSKU
        {
            get { return _RDW_ProdSKU; }
            set { _RDW_ProdSKU = value; }
        }

        /// <summary>
        /// 项目分类
        /// </summary>
        private string _RDW_Category;
        public string RDW_Category
        {
            get { return _RDW_Category; }
            set { _RDW_Category = value; }
        }

        /// <summary>
        /// 项目说明
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
        /// 模板ID
        /// </summary>
        private int _RDW_Template;
        public int RDW_Template
        {
            get { return _RDW_Template; }
            set { _RDW_Template = value; }
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


        /// <summary>
        /// PM ID
        /// </summary>
        private string _RDW_PMID;
        public string RDW_PMID
        {
            get { return _RDW_PMID; }
            set { _RDW_PMID = value; }
        }

        /// <summary>
        /// PM ID
        /// </summary>
        private string _RDW_PM;
        public string RDW_PM
        {
            get { return _RDW_PM; }
            set { _RDW_PM = value; }
        }

        
        /// <summary>
        /// Project Owner ID
        /// </summary>
        private long _RDW_LeaderID;
        public long RDW_LeaderID
        {
            get { return _RDW_LeaderID; }
            set { _RDW_LeaderID = value; }
        }

        /// <summary>
        /// Project Owner Name
        /// </summary>
        private string _RDW_Leader;
        public string RDW_Leader
        {
            get { return _RDW_Leader; }
            set { _RDW_Leader = value; }
        }

        /// <summary>
        /// Task ID
        /// </summary>
        private string _RDW_TaskID;
        public string RDW_TaskID
        {
            get { return _RDW_TaskID; }
            set { _RDW_TaskID = value; }
        }

        /// <summary>
        /// Step ID
        /// </summary>
        private long _RDW_DetID;
        public long RDW_DetID
        {
            get { return _RDW_DetID; }
            set { _RDW_DetID = value; }
        }

        /// <summary>
        /// 步骤是否可编辑
        /// </summary>
        private bool _RDW_isActive;
        public bool RDW_isActive
        {
            get { return _RDW_isActive; }
            set { _RDW_isActive = value; }
        }

        /// <summary>
        /// 状态事由
        /// </summary>
        private string _RDW_Remark;
        public string RDW_Remark
        {
            get { return _RDW_Remark; }
            set { _RDW_Remark = value; }
        }

        /// <summary>
        /// 老项目ID
        /// </summary>
        private string  _RDW_Type;
        public string RDW_Type
        {
            get { return _RDW_Type; }
            set { _RDW_Type = value; }
        }

        private string _RDW_TypeName;
        public string RDW_TypeName
        {
            get { return _RDW_TypeName; }
            set { _RDW_TypeName = value; }
        }
        /// <summary>
        /// ECN Code
        /// </summary>
        private string _RDW_EcnCode;
        public string RDW_EcnCode
        {
            get { return _RDW_EcnCode; }
            set { _RDW_EcnCode = value; }
        }
        /// <summary>
        /// PPA
        /// </summary>
        private string _RDW_PPA;
        public string RDW_PPA
        {
            get { return _RDW_PPA; }
            set { _RDW_PPA = value; }
        }
        /// <summary>
        /// 项目Type
        /// </summary>
        private int _RDW_OldID;
        public int RDW_OldID
        {
            get { return _RDW_OldID; }
            set { _RDW_OldID = value; }
        }

        /// <summary>
        /// 产品经理
        /// </summary>
        private string _RDW_MGR;
        public string RDW_MGR
        {
            get { return _RDW_MGR; }
            set { _RDW_MGR = value; }
        }
        private string _RDW_PPAMsttrid;
        public string RDW_PPAMstrid
        {
            get { return _RDW_PPAMsttrid; }
            set { _RDW_PPAMsttrid = value; }
        }

        private string _RDW_Stage;
        public string RDW_Stage
        {
            get { return _RDW_Stage; }
            set { _RDW_Stage = value; }
        }
    }
}
