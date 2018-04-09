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
/// Summary description for RT_Detail
/// </summary>
/// 
namespace RT_WorkFlow
{
    public class RT_Detail
    {
        public RT_Detail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// 自增长
        /// </summary>
        private long _RDW_DetID;
        public long RDW_DetID
        {
            get { return _RDW_DetID; }
            set { _RDW_DetID = value; }
        }

        /// <summary>
        /// RDW_MstrID
        /// </summary>
        private long _RDW_MstrID;
        public long RDW_MstrID
        {
            get { return _RDW_MstrID; }
            set { _RDW_MstrID = value; }
        }

        /// <summary>
        /// 步骤名
        /// </summary>
        private string _RDW_StepName;
        public string RDW_StepName
        {
            get { return _RDW_StepName; }
            set { _RDW_StepName = value; }
        }

        /// <summary>
        /// 步骤说明
        /// </summary>
        private string _RDW_StepDesc;
        public string RDW_StepDesc
        {
            get { return _RDW_StepDesc; }
            set { _RDW_StepDesc = value; }
        }

        /// <summary>
        /// 步骤序号
        /// </summary>
        private int _RDW_Sort;
        public int RDW_Sort
        {
            get { return _RDW_Sort; }
            set { _RDW_Sort = value; }
        }

        /// <summary>
        /// 步骤阶段状态
        /// </summary>
        private int _RDW_Status;
        public int RDW_Status
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
        /// 项目起始日期
        /// </summary>
        private string _RDW_StartDate;
        public string RDW_StartDate
        {
            get { return _RDW_StartDate; }
            set { _RDW_StartDate = value; }
        }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        private string _RDW_EndDate;
        public string RDW_EndDate
        {
            get { return _RDW_EndDate; }
            set { _RDW_EndDate = value; }
        }

        /// <summary>
        /// 步骤起始日期
        /// </summary>
        private string _RDW_StepStartDate;
        public string RDW_StepStartDate
        {
            get { return _RDW_StepStartDate; }
            set { _RDW_StepStartDate = value; }
        }

        /// <summary>
        /// 步骤结束日期
        /// </summary>
        private string _RDW_StepEndDate;
        public string RDW_StepEndDate
        {
            get { return _RDW_StepEndDate; }
            set { _RDW_StepEndDate = value; }
        }

        public enum ProcessStatus
        {
            NoDeal = 0,
            Partial = 1,
            Pass = 2,
            Fail = 3,
        }

        /// <summary>
        /// RDW_MessID
        /// </summary>
        private long _RDW_MessID;
        public long RDW_MessID
        {
            get { return _RDW_MessID; }
            set { _RDW_MessID = value; }
        }

        /// <summary>
        /// 消息
        /// </summary>
        private string _RDW_Message;
        public string RDW_Message
        {
            get { return _RDW_Message; }
            set { _RDW_Message = value; }
        }

        /// <summary>
        /// 评审人ID
        /// </summary>
        private string _RDW_EvaluaterID;
        public string RDW_EvaluaterID
        {
            get { return _RDW_EvaluaterID; }
            set { _RDW_EvaluaterID = value; }
        }

        /// <summary>
        /// 评审人
        /// </summary>
        private string _RDW_Evaluater;
        public string RDW_Evaluater
        {
            get { return _RDW_Evaluater; }
            set { _RDW_Evaluater = value; }
        }

        /// <summary>
        /// 参与人ID
        /// </summary>
        private string _RDW_Partner;
        public string RDW_Partner
        {
            get { return _RDW_Partner; }
            set { _RDW_Partner = value; }
        }

        /// <summary>
        /// 参与人
        /// </summary>
        private string _RDW_PartnerName;
        public string RDW_PartnerName
        {
            get { return _RDW_PartnerName; }
            set { _RDW_PartnerName = value; }
        }

        /// <summary>
        /// 步骤完成日期
        /// </summary>
        private string _RDW_StepFinishDate;
        public string RDW_StepFinishDate
        {
            get { return _RDW_StepFinishDate; }
            set { _RDW_StepFinishDate = value; }
        }
    }
}
