using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Portal.Fixas
{
    /*
     这里主要是每个模块相关类定义的集合
     */

    #region 基本的类

    /// <summary>
    /// User类。
    /// </summary>
    public class User
    {
        private int _ID;
        /// <summary>
        /// User的ID号
        /// </summary>
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        private string _Name;
        /// <summary>
        /// User的名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        private DateTime _Date;
        /// <summary>
        /// CreatedDate或者ModifiedDate。可以不初始化，直接赋值、调用。
        /// </summary>
        public DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                this._Date = value;
            }
        }

        public User()
        {
            this._ID = 0;
            this._Name = string.Empty;
            this._Date = DateTime.Now;
        }
    }

    #endregion

    #region 固定资产

    #region 固定资产类 Added By Chenyb
    /// <summary>
    /// 固定资产类，包含固定资产的基本信息
    /// </summary>
    public class FixedAssets
    {
        private string fixasNo;
        /// <summary>
        /// 固定资产编号
        /// </summary>
        public string FixasNo
        {
            get
            {
                return this.fixasNo;
            }
            set
            {
                this.fixasNo = value;
            }
        }

        private string fixasName;
        /// <summary>
        /// 固定资产名称
        /// </summary>
        public string FixasName
        {
            get
            {
                return this.fixasName;
            }
            set
            {
                this.fixasName = value;
            }
        }

        private string fixasDesc;
        /// <summary>
        /// 固定资产规格
        /// </summary>
        public string FixasDesc
        {
            get
            {
                return this.fixasDesc;
            }
            set
            {
                this.fixasDesc = value;
            }
        }

        private int fixasTypeID;
        /// <summary>
        /// 固定资产类型ID
        /// </summary>
        public int FixasTypeID
        {
            get
            {
                return this.fixasTypeID;
            }
            set
            {
                this.fixasTypeID = value;
            }
        }

        private string fixasType;
        /// <summary>
        /// 固定资产类型
        /// </summary>
        public string FixasType
        {
            get
            {
                return this.fixasType;
            }
            set
            {
                this.fixasType = value;
            }
        }

        private int fixasSubTypeID;
        /// <summary>
        /// 固定资产的子类ID
        /// </summary>
        public int FixasSubTypeID
        {
            get
            {
                return this.fixasSubTypeID;
            }
            set
            {
                this.fixasSubTypeID = value;
            }
        }

        private string fixasSubType;
        /// <summary>
        /// 固定资产的子类
        /// </summary>
        public string FixasSubType
        {
            get
            {
                return this.fixasSubType;
            }
            set
            {
                this.fixasSubType = value;
            }
        }

        private string fixasEntity;
        /// <summary>
        /// 固定资产入账公司
        /// </summary>
        public string FixasEntity
        {
            get
            {
                return this.fixasEntity;
            }
            set
            {
                this.fixasEntity = value;
            }
        }

        private string fixasVouDate;
        /// <summary>
        /// 固定资产入账时间
        /// </summary>
        public string FixasVouDate
        {
            get
            {
                return this.fixasVouDate;
            }
            set
            {
                this.fixasVouDate = value;
            }
        }

        private string fixasSupplier;
        /// <summary>
        /// 固定资产供应商
        /// </summary>
        public string FixasSupplier
        {
            get
            {
                return this.fixasSupplier;
            }
            set
            {
                this.fixasSupplier = value;
            }
        }

        private string domain;
        /// <summary>
        /// 固定资产当前域
        /// </summary>
        public string Domain
        {
            get
            {
                return this.domain;
            }
            set
            {
                this.domain = value;
            }
        }

        private string cc;
        /// <summary>
        /// 固定资产当前成本中心
        /// </summary>
        public string CC
        {
            get
            {
                return this.cc;
            }
            set
            {
                this.cc = value;
            }
        }

        private string fixasSerialNumber;
        /// <summary>
        /// 固定资产流水号
        /// </summary>
        public string FixasSerialNumber
        {
            get
            {
                return this.fixasSerialNumber;
            }
            set
            {
                this.fixasSerialNumber = value;
            }
        }

        private bool isExists;
        /// <summary>
        /// 该固定资产是否存在
        /// </summary>
        public bool IsExists
        {
            get
            {
                return this.isExists;
            }
        }

        public FixedAssets()
        {
            this.fixasNo = string.Empty;
            this.fixasName = string.Empty;
            this.fixasDesc = string.Empty;
            this.fixasTypeID = 0;
            this.fixasType = string.Empty;
            this.fixasSubTypeID = 0;
            this.fixasSubType = string.Empty;
            this.fixasEntity = string.Empty;
            this.fixasVouDate = string.Empty;
            this.fixasSupplier = string.Empty;
            this.isExists = false;
        }

        public FixedAssets(string _fixasNo, int _plantCode)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fixasNo", _fixasNo);
                param[1] = new SqlParameter("@plantCode", _plantCode);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectFixedAssets", param))
                {
                    while (reader.Read())
                    {
                        this.fixasNo = reader["fixasNo"].ToString();
                        this.fixasName = reader["fixasName"].ToString();
                        this.fixasDesc = reader["fixasDesc"].ToString();
                        this.fixasTypeID = reader["fixasTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["fixasTypeID"]);
                        this.fixasType = reader["fixasType"].ToString();
                        this.fixasSubTypeID = reader["fixasSubTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["fixasSubTypeID"]);
                        this.fixasSubType = reader["fixasSubType"].ToString();
                        this.fixasEntity = reader["fixasEntity"].ToString();
                        if (reader["fixasVouDate"] != DBNull.Value)
                        {
                            this.fixasVouDate = string.Format("{0:yyyy-MM-dd}", reader["fixasVouDate"]);
                        }
                        else
                        {
                            this.fixasVouDate = string.Empty;
                        }
                        this.fixasSupplier = reader["fixasSupplier"].ToString();
                        this.domain = reader["domain"].ToString();
                        this.cc = reader["ccDesc"].ToString();
                        this.fixasSerialNumber = reader["fixasSerialNumber"].ToString();
                        this.isExists = this.fixasNo == string.Empty ? false : true;
                    }
                }
            }
            catch
            {
                this.fixasNo = string.Empty;
                this.fixasName = string.Empty;
                this.fixasDesc = string.Empty;
                this.fixasTypeID = 0;
                this.fixasType = string.Empty;
                this.fixasSubTypeID = 0;
                this.fixasSubType = string.Empty;
                this.fixasEntity = string.Empty;
                this.fixasVouDate = string.Empty;
                this.fixasSupplier = string.Empty;
                this.domain = string.Empty;
                this.cc = string.Empty;
                this.isExists = false;
            }
        }
    }
    #endregion

    #region 固定资产类型操作类 Added By Chenyb
    /// <summary>
    /// FixasType操作类
    /// </summary>
    public class FixasTypeHelper
    {
        /// <summary>
        /// 获取所有固定资产类型
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectFixasTypeList()
        {
            adamClass adam = new adamClass();
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectFixasTypeList").Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取固定资产某类型的所有子类型
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public static DataTable SelectFixasSubTypeList(string typeID)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@typeID", typeID);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectFixasSubTypeList", param).Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    #endregion

    #region 固定资产维修 Added by Chenyb
    /// <summary>
    /// 固定资产维修
    /// </summary>
    public class FixasRepair
    {
        private adamClass adam = new adamClass();

        private FixedAssets fixasInfo;
        /// <summary>
        /// 固定资产基本信息
        /// </summary>
        public FixedAssets FixasInfo
        {
            get
            {
                return this.fixasInfo;
            }
            set
            {
                this.fixasInfo = value;
            }
        }

        private string repairOrder;
        /// <summary>
        /// 维修工单号
        /// </summary>
        public string RepairOrder
        {
            get
            {
                return this.repairOrder;
            }
            set
            {
                this.repairOrder = value;
            }
        }

        private string applyRepairDate;
        /// <summary>
        /// 申请维修时间
        /// </summary>
        public string ApplyRepairDate
        {
            get
            {
                return this.applyRepairDate;
            }
            set
            {
                this.applyRepairDate = value;
            }
        }

        private string problemDesc;
        /// <summary>
        /// 维修问题描述
        /// </summary>
        public string ProblemDesc
        {
            get
            {
                return this.problemDesc;
            }
            set
            {
                this.problemDesc = value;
            }
        }

        private string repairStatus;
        /// <summary>
        /// 该维修申请的状态
        /// </summary>
        public string RepairStatus
        {
            get
            {
                return this.repairStatus;
            }
            set
            {
                this.repairStatus = value;
            }
        }

        private User repairAssigner;
        /// <summary>
        /// 该维修任务指派人
        /// </summary>
        public User RepairAssigner
        {
            get
            {
                return this.repairAssigner;
            }
            set
            {
                this.repairAssigner = value;
            }
        }

        private string repairAssignedToName;
        /// <summary>
        /// 接受该维修任务的人
        /// </summary>
        public string RepairAssignedToName
        {
            get
            {
                return this.repairAssignedToName;
            }
            set
            {
                this.repairAssignedToName = value;
            }
        }

        private string repairAssignedRemark;
        /// <summary>
        /// 指派维修人时的备注
        /// </summary>
        public string RepairAssignedRemark
        {
            get
            {
                return this.repairAssignedRemark;
            }
            set
            {
                this.repairAssignedRemark = value;
            }
        }

        private User repairConfirmer;
        /// <summary>
        /// 维修最终结果验收人
        /// </summary>
        public User RepairConfirmer
        {
            get
            {
                return this.repairConfirmer;
            }
            set
            {
                this.repairConfirmer = value;
            }
        }

        private string repairConfirmRemark;
        /// <summary>
        /// 最终验收人意见
        /// </summary>
        public string RepairConfirmRemark
        {
            get
            {
                return this.repairConfirmRemark;
            }
            set
            {
                this.repairConfirmRemark = value;
            }
        }

        private User applyCreator;
        /// <summary>
        /// 维修申请创建人
        /// </summary>
        public User ApplyCreator
        {
            get
            {
                return this.applyCreator;
            }
            set
            {
                this.applyCreator = value;
            }
        }

        private User applyModifier;
        /// <summary>
        /// 维修申请修改人
        /// </summary>
        public User ApplyModifier
        {
            get
            {
                return this.applyModifier;
            }
            set
            {
                this.applyModifier = value;
            }
        }

        private string repairedName;
        /// <summary>
        /// 维修人
        /// </summary>
        public string RepairedName
        {
            get
            {
                return this.repairedName;
            }
            set
            {
                this.repairedName = value;
            }
        }

        private string repairBeginDate;
        /// <summary>
        /// 维修开始时间
        /// </summary>
        public string RepairBeginDate
        {
            get
            {
                return this.repairBeginDate;
            }
            set
            {
                this.repairBeginDate = value;
            }
        }

        private string repairEndDate;
        /// <summary>
        /// 维修结束时间
        /// </summary>
        public string RepairEndDate
        {
            get
            {
                return this.repairEndDate;
            }
            set
            {
                this.repairEndDate = value;
            }
        }

        private string repairRecord;
        /// <summary>
        /// 维修记录
        /// </summary>
        public string RepairRecord
        {
            get
            {
                return this.repairRecord;
            }
            set
            {
                this.repairRecord = value;
            }
        }

        private User recordModifier;
        /// <summary>
        /// 维修记录修改人
        /// </summary>
        public User RecordModifier
        {
            get
            {
                return this.recordModifier;
            }
            set
            {
                this.recordModifier = value;
            }
        }

        /// <summary>
        /// 检查该固定资产维修申请是否存在
        /// </summary>
        public bool IsExists
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@repairOrder", this.repairOrder);
                    param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_checkRepairApplyExists", param);
                    return Convert.ToBoolean(param[1].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "IsExists：" + ex.Message;
                    return false;
                }
            }
        }

        private string exceptionMsg;
        /// <summary>
        /// 固定资产维修操作中产生的一些异常
        /// </summary>
        public string ExceptionMsg
        {
            get
            {
                return this.exceptionMsg;
            }
        }

        public FixasRepair()
        {
            this.fixasInfo = new FixedAssets();

            this.repairOrder = string.Empty;
            this.applyRepairDate = string.Empty;
            this.problemDesc = string.Empty;
            this.repairStatus = string.Empty;
            this.repairAssigner = new User();
            this.repairAssigner.Date = DateTime.MinValue;
            this.repairAssignedToName = string.Empty;
            this.repairConfirmer = new User();
            this.repairConfirmer.Date = DateTime.MinValue;
            this.repairConfirmRemark = string.Empty;
            this.repairAssignedRemark = string.Empty;

            this.applyCreator = new User();
            this.applyCreator.Date = DateTime.MinValue;
            this.applyModifier = new User();
            this.applyModifier.Date = DateTime.MinValue;

            this.repairedName = string.Empty;
            this.repairBeginDate = string.Empty;
            this.repairEndDate = string.Empty;
            this.repairRecord = string.Empty;
            this.recordModifier = new User();
            this.recordModifier.Date = DateTime.MinValue;

            this.exceptionMsg = string.Empty;
        }

        /// <summary>
        /// 插入新固定资产维修申请
        /// </summary>
        public bool InsertApply
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[15];
                    param[0] = new SqlParameter("@repairOrder", this.repairOrder);
                    param[1] = new SqlParameter("@fixasNo", this.fixasInfo.FixasNo);
                    param[2] = new SqlParameter("@fixasName", this.fixasInfo.FixasName);
                    param[3] = new SqlParameter("@fixasDesc", this.fixasInfo.FixasDesc);
                    param[4] = new SqlParameter("@fixasTypeID", this.fixasInfo.FixasTypeID);
                    param[5] = new SqlParameter("@fixasType", this.fixasInfo.FixasType);
                    param[6] = new SqlParameter("@fixasSubTypeID", this.fixasInfo.FixasSubTypeID);
                    param[7] = new SqlParameter("@fixasSubType", this.fixasInfo.FixasSubType);
                    param[8] = new SqlParameter("@fixasEntity", this.fixasInfo.FixasEntity);
                    param[9] = new SqlParameter("@fixasVouDate", this.fixasInfo.FixasVouDate);
                    param[10] = new SqlParameter("@fixasSupplier", this.fixasInfo.FixasSupplier);
                    param[11] = new SqlParameter("@createdBy", this.applyCreator.ID);
                    param[12] = new SqlParameter("@createdName", this.applyCreator.Name);
                    param[13] = new SqlParameter("@problemDesc", this.problemDesc);
                    param[14] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[14].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_insertRepairApply", param);
                    return Convert.ToBoolean(param[14].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "InsertApply：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新维修申请
        /// </summary>
        public bool UpdateApply
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@repairOrder", this.repairOrder);
                    param[1] = new SqlParameter("@modifiedBy", this.applyModifier.ID);
                    param[2] = new SqlParameter("@modifiedName", this.applyModifier.Name);
                    param[3] = new SqlParameter("@problemDesc", this.problemDesc);
                    param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[4].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_updateRepairApply", param);
                    return Convert.ToBoolean(param[4].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "UpdateApply：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 指派该维修单的负责人
        /// </summary>
        public bool AssignApply
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@repairOrder", this.repairOrder);
                    param[1] = new SqlParameter("@fixasNO", this.fixasInfo.FixasNo);
                    param[2] = new SqlParameter("@assignedBy", this.repairAssigner.ID);
                    param[3] = new SqlParameter("@assignedName", this.repairAssigner.Name);
                    param[4] = new SqlParameter("@assignedToName", this.repairAssignedToName);
                    param[5] = new SqlParameter("@assignedRemark", this.repairAssignedRemark);
                    param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_assignRepairApply", param);
                    return Convert.ToBoolean(param[6].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "AssignApply：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 维修记录人点击“完成维修”按钮时，表示已经在维修单上得到车间的书面签字确认信息
        /// </summary>
        public bool ConfirmApply
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@repairOrder", this.repairOrder);
                    param[1] = new SqlParameter("@confirmedName", this.repairConfirmer.Name);
                    param[2] = new SqlParameter("@endDate", this.repairEndDate);
                    param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[3].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_confirmRepairApply", param);
                    return Convert.ToBoolean(param[3].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "ConfirmApply：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除固定资产维修申请
        /// </summary>
        public bool DeleteApply
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@repairOrder", this.repairOrder);
                    param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_deleteRepairApply", param);
                    return Convert.ToBoolean(param[1].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "DeleteApply：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新固定资产维修记录
        /// </summary>
        public bool UpdateRecord
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@repairOrder", this.repairOrder);
                    param[1] = new SqlParameter("@repairedName", this.repairedName);
                    param[2] = new SqlParameter("@repairRecord", this.repairRecord);
                    param[3] = new SqlParameter("@modifiedBy", this.recordModifier.ID);
                    param[4] = new SqlParameter("@modifiedName", this.recordModifier.Name);
                    param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[5].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_updateRepairRecord", param);
                    return Convert.ToBoolean(param[5].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "UpdateRecord：" + ex.Message;
                    return false;
                }
            }
        }
    }

    /// <summary>
    /// 固定资产维修操作
    /// </summary>
    public class FixasRepairHelper
    {
        /// <summary>
        /// 返回的结果数量
        /// </summary>
        public static int Count = 0;
        /// <summary>
        /// 异常情况
        /// </summary>
        public static string ExceptionMsg = string.Empty;

        /// <summary>
        /// 获取固定资产维修单的所有信息
        /// </summary>
        /// <param name="fixasNo">资产编号</param>
        /// <param name="repairOrder">维修单</param>
        /// <param name="applyRepairDate1">申请维修日期1</param>
        /// <param name="applyRepairDate2">申请维修日期2</param>
        /// <param name="repairBeginDate1">维修开始时间1</param>
        /// <param name="repairBeginDate2">维修开始时间2</param>
        /// <returns></returns>
        public static IList<FixasRepair> SelectRepairOrder(string fixasNo, string repairOrder, string applyRepairDate1, string applyRepairDate2, string repairBeginDate1, string repairBeginDate2, string repairStatus, int type, int subType, int plantCode, string cc)
        {
            adamClass adam = new adamClass();
            IList<FixasRepair> fixasRepairList = new List<FixasRepair>();
            Count = 0;
            int index = 0;
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@fixasNo", fixasNo);
                param[1] = new SqlParameter("@repairOrder", repairOrder);
                param[2] = new SqlParameter("@applyRepairDate1", applyRepairDate1);
                param[3] = new SqlParameter("@applyRepairDate2", applyRepairDate2);
                param[4] = new SqlParameter("@repairBeginDate1", repairBeginDate1);
                param[5] = new SqlParameter("@repairBeginDate2", repairBeginDate2);
                param[6] = new SqlParameter("@repairStatus", repairStatus);
                param[7] = new SqlParameter("@type", type);
                param[8] = new SqlParameter("@subType", subType);
                param[9] = new SqlParameter("@plantCode", plantCode);
                param[10] = new SqlParameter("@cc", cc);

                SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectRepairOrder", param);
                while (reader.Read())
                {
                    FixasRepair item = new FixasRepair();

                    item.RepairOrder = reader["plan_No"].ToString();

                    item.FixasInfo.FixasNo = reader["plan_fixasNo"].ToString();
                    item.FixasInfo.FixasName = reader["plan_fixasName"].ToString();
                    item.FixasInfo.FixasDesc = reader["plan_fixasDesc"].ToString();
                    item.FixasInfo.FixasType = reader["plan_fixasType"].ToString();
                    item.FixasInfo.FixasEntity = reader["plan_fixasEntity"].ToString();
                    item.FixasInfo.FixasVouDate = string.Format("{0:yyyy-MM-dd}", reader["plan_fixasVouDate"]);
                    item.FixasInfo.FixasSupplier = reader["plan_fixasSupplier"].ToString();
                    item.FixasInfo.FixasType = reader["plan_fixasType"].ToString();
                    item.FixasInfo.FixasSubType = reader["plan_fixasSubtype"].ToString();
                    item.FixasInfo.Domain = reader["domain"].ToString();
                    item.FixasInfo.CC = reader["ccDesc"].ToString();

                    item.ApplyRepairDate = string.Format("{0:yyyy-MM-dd HH:mm}", reader["plan_date"]);
                    item.ProblemDesc = reader["plan_problemDesc"].ToString();
                    item.RepairStatus = reader["plan_repairStatus"].ToString();
                    if (reader["plan_assignedBy"] != DBNull.Value)
                    {
                        item.RepairAssigner.ID = Convert.ToInt32(reader["plan_assignedBy"]);
                    }
                    item.RepairAssigner.Name = reader["plan_assignedName"].ToString();
                    if (reader["plan_assignedDate"] != DBNull.Value)
                    {
                        item.RepairAssigner.Date = Convert.ToDateTime(reader["plan_assignedDate"]);
                    }

                    item.RepairAssignedToName = reader["plan_assignedToName"].ToString();
                    item.RepairAssignedRemark = reader["plan_assignedRemark"].ToString();
                    if (reader["plan_confirmedBy"] != DBNull.Value)
                    {
                        item.RepairConfirmer.ID = Convert.ToInt32(reader["plan_confirmedBy"]);
                    }
                    item.RepairConfirmer.Name = reader["plan_confirmedName"].ToString();
                    if (reader["plan_confirmedDate"] != DBNull.Value)
                    {
                        item.RepairConfirmer.Date = Convert.ToDateTime(reader["plan_confirmedDate"]);
                    }  
                    item.RepairConfirmRemark = reader["plan_confirmedRemark"].ToString();

                    if (reader["plan_createdBy"] != DBNull.Value)
                    {
                        item.ApplyCreator.ID = Convert.ToInt32(reader["plan_createdBy"]);
                    }
                    item.ApplyCreator.Name = reader["plan_createdName"].ToString();
                    if (reader["plan_createdDate"] != DBNull.Value)
                    {
                        item.ApplyCreator.Date = Convert.ToDateTime(reader["plan_createdDate"]);
                    }
                    item.RepairedName = reader["rec_repairedName"].ToString();
                    item.RepairBeginDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader["rec_beginDate"]);
                    item.RepairEndDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader["rec_endDate"]);
                    item.RepairRecord = reader["rec_record"].ToString();
                    if (reader["rec_modifiedBy"] != DBNull.Value)
                    {
                        item.RecordModifier.ID = Convert.ToInt32(reader["rec_modifiedBy"]);
                    }
                    item.RecordModifier.Name = reader["rec_modifiedName"].ToString();
                    if (reader["rec_modifiedDate"] != DBNull.Value)
                    {
                        item.RecordModifier.Date = Convert.ToDateTime(reader["rec_modifiedDate"]);
                    }

                    index++;
                    fixasRepairList.Add(item);
                }

                reader.Close();
                Count = index;
                return fixasRepairList;
            }
            catch (Exception ex)
            {
                ExceptionMsg = "SelectRepair：" + ex.Message;
                return fixasRepairList;
            }
        }

        /// <summary>
        /// 一定时间区间内，所有维修时长汇总
        /// </summary>
        /// <param name="fixasNo">资产编号</param>
        /// <param name="fixasName">资产名称</param>
        /// <param name="repairDate1">维修时间区间1</param>
        /// <param name="repairDate2">维修时间区间2</param>
        /// <returns></returns>
        public static DataSet SelectRepairTimeCost(string fixasNo, string fixasName, string repairDate1, string repairDate2, int plantCode)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@fixasNo", fixasNo);
                param[1] = new SqlParameter("@fixasName", fixasName);
                param[2] = new SqlParameter("@repairDate1", repairDate1);
                param[3] = new SqlParameter("@repairDate2", repairDate2);
                param[4] = new SqlParameter("@plantCode", plantCode);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectRepairTimeCost", param);
            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 一定时间区间内，所有已完成维修的时长汇总
        /// </summary>
        /// <param name="fixasNo">资产编号</param>
        /// <param name="fixasName">资产名称</param>
        /// <param name="repairDate1">维修时间区间1</param>
        /// <param name="repairDate2">维修时间区间2</param>
        /// <returns></returns>
        public static DataSet SelectRepairTimeCost2(string fixasNo, string fixasName, string repairDate1, string repairDate2, int plantCode)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@fixasNo", fixasNo);
                param[1] = new SqlParameter("@fixasName", fixasName);
                param[2] = new SqlParameter("@repairDate1", repairDate1);
                param[3] = new SqlParameter("@repairDate2", repairDate2);
                param[4] = new SqlParameter("@plantCode", plantCode);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectRepairTimeCost2", param);
            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                return null;
            }
        }
    }
    #endregion

    #region 固定资产保养 Added By Chenyb
    /// <summary>
    /// 固定资产保养
    /// </summary>
    public class FixasMaintain
    {
        private adamClass adam = new adamClass();

        private string maintainOrder;
        /// <summary>
        /// 保养工单号
        /// </summary>
        public string MaintainOrder
        {
            get
            {
                return this.maintainOrder;
            }
            set
            {
                this.maintainOrder = value;
            }
        }

        private FixedAssets fixasInfo;
        /// <summary>
        /// 固定资产基本信息
        /// </summary>
        public FixedAssets FixasInfo
        {
            get
            {
                return this.fixasInfo;
            }
            set
            {
                this.fixasInfo = value;
            }
        }

        private string planMaintainDate;
        /// <summary>
        /// 计划保养日期
        /// </summary>
        public string PlanMaintainDate
        {
            get
            {
                return this.planMaintainDate;
            }
            set
            {
                this.planMaintainDate = value;
            }
        }

        private string maintainDesc;
        /// <summary>
        /// 保养描述
        /// </summary>
        public string MaintainDesc
        {
            get
            {
                return this.maintainDesc;
            }
            set
            {
                this.maintainDesc = value;
            }
        }

        private string maintainStatus;
        /// <summary>
        /// 保养状态
        /// </summary>
        public string MaintainStatus
        {
            get
            {
                return this.maintainStatus;
            }
            set
            {
                this.maintainStatus = value;
            }
        }

        private User planCreator;
        /// <summary>
        /// 保养计划创建人
        /// </summary>
        public User PlanCreator
        {
            get
            {
                return this.planCreator;
            }
            set
            {
                this.planCreator = value;
            }
        }

        private User planModifier;
        /// <summary>
        /// 保养计划修改人
        /// </summary>
        public User PlanModifier
        {
            get
            {
                return this.planModifier;
            }
            set
            {
                this.planModifier = value;
            }
        }

        private User panConfirmer;
        /// <summary>
        /// 保养计划确认人
        /// </summary>
        public User PlanConfirmer
        {
            get
            {
                return this.panConfirmer;
            }
            set
            {
                this.panConfirmer = value;
            }
        }

        private string maintainedName;
        /// <summary>
        /// 保养人姓名
        /// </summary>
        public string MaintainedName
        {
            get
            {
                return this.maintainedName;
            }
            set
            {
                this.maintainedName = value;
            }
        }

        private string maintainBeginDate;
        /// <summary>
        /// 保养开始时间
        /// </summary>
        public string MaintainBeginDate
        {
            get
            {
                return this.maintainBeginDate;
            }
            set
            {
                this.maintainBeginDate = value;
            }
        }

        private string maintainEndDate;
        /// <summary>
        /// 保养结束时间
        /// </summary>
        public string MaintainEndDate
        {
            get
            {
                return this.maintainEndDate;
            }
            set
            {
                this.maintainEndDate = value;
            }
        }

        private string maintainedRecord;
        /// <summary>
        /// 保养记录
        /// </summary>
        public string MaintainedRecord
        {
            get
            {
                return this.maintainedRecord;
            }
            set
            {
                this.maintainedRecord = value;
            }
        }

        private User maintainModifier;
        /// <summary>
        /// 保养记录修改人
        /// </summary>
        public User MaintainModifier
        {
            get
            {
                return this.maintainModifier;
            }
            set
            {
                this.maintainModifier = value;
            }
        }

        private string exceptionMsg;
        /// <summary>
        /// 异常描述
        /// </summary>
        public string ExceptionMsg
        {
            get
            {
                return this.exceptionMsg;
            }
            set
            {
                this.exceptionMsg = value;
            }
        }

        /// <summary>
        /// 判断当前的保养计划是否存在
        /// </summary>
        public bool IsExists
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@maintainOrder", this.maintainOrder);
                    param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_checkMaintainPlanExists", param);
                    return Convert.ToBoolean(param[1].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = ex.Message;
                    return false;
                }
            }
        }

        public FixasMaintain()
        {
            this.fixasInfo = new FixedAssets();

            this.maintainOrder = string.Empty;
            this.planMaintainDate = string.Empty;
            this.maintainDesc = string.Empty;
            this.maintainStatus = string.Empty;
            this.planCreator = new User();
            this.planCreator.Date = DateTime.MinValue;
            this.planModifier = new User();
            this.planModifier.Date = DateTime.MinValue;
            this.panConfirmer = new User();
            this.panConfirmer.Date = DateTime.MinValue;

            this.maintainedName = string.Empty;
            this.maintainBeginDate = string.Empty;
            this.MaintainBeginDate = string.Empty;
            this.maintainedRecord = string.Empty;
            this.maintainModifier = new User();
            this.maintainModifier.Date = DateTime.MinValue;

            this.exceptionMsg = string.Empty;
        }

        /// <summary>
        /// 添加一个保养计划
        /// </summary>
        public bool InsertPlan
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[16];
                    param[0] = new SqlParameter("@maintainOrder", this.maintainOrder);
                    param[1] = new SqlParameter("@fixasNo", this.fixasInfo.FixasNo);
                    param[2] = new SqlParameter("@fixasName", this.fixasInfo.FixasName);
                    param[3] = new SqlParameter("@fixasDesc", this.fixasInfo.FixasDesc);
                    param[4] = new SqlParameter("@fixasTypeID", this.fixasInfo.FixasTypeID);
                    param[5] = new SqlParameter("@fixasType", this.fixasInfo.FixasType);
                    param[6] = new SqlParameter("@fixasSubTypeID", this.fixasInfo.FixasSubTypeID);
                    param[7] = new SqlParameter("@fixasSubType", this.fixasInfo.FixasSubType);
                    param[8] = new SqlParameter("@fixasEntity", this.fixasInfo.FixasEntity);
                    param[9] = new SqlParameter("@fixasVouDate", this.fixasInfo.FixasVouDate);
                    param[10] = new SqlParameter("@fixasSupplier", this.fixasInfo.FixasSupplier);
                    param[11] = new SqlParameter("@planMaintainDate", this.planMaintainDate);
                    param[12] = new SqlParameter("@createdBy", this.planCreator.ID);
                    param[13] = new SqlParameter("@createdName", this.planCreator.Name);
                    param[14] = new SqlParameter("@maintainDesc", this.maintainDesc);
                    param[15] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[15].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_insertMaintainPlan", param);
                    return Convert.ToBoolean(param[15].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "InsertPlan：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新保养计划
        /// </summary>
        public bool UpdatePlan
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@maintainOrder", this.maintainOrder);
                    param[1] = new SqlParameter("@planMaintainDate", this.planMaintainDate);
                    param[2] = new SqlParameter("@modifiedBy", this.planModifier.ID);
                    param[3] = new SqlParameter("@modifiedName", this.planModifier.Name);
                    param[4] = new SqlParameter("@maintainDesc", this.maintainDesc);
                    param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[5].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_updateMaintainPlan", param);
                    return Convert.ToBoolean(param[5].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "UpdatePlan：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除一个保养计划
        /// </summary>
        public bool DeletePlan
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@maintainOrder", this.maintainOrder);
                    param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_deleteMaintainPlan", param);
                    return Convert.ToBoolean(param[1].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "DeletePlan：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新保养记录
        /// </summary>
        public bool UpdateRecord
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@maintainOrder", this.maintainOrder);
                    param[1] = new SqlParameter("@maintainedName", this.maintainedName);
                    param[2] = new SqlParameter("@maintainedBeginDate", this.maintainBeginDate);
                    param[3] = new SqlParameter("@maintainedEndDate", this.maintainEndDate);
                    param[4] = new SqlParameter("@maintainedRecord", this.maintainedRecord);
                    param[5] = new SqlParameter("@modifiedBy", this.maintainModifier.ID);
                    param[6] = new SqlParameter("@modifiedName", this.maintainModifier.Name);
                    param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[7].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_updateMaintainRecord", param);
                    return Convert.ToBoolean(param[7].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "UpdateRecord：" + ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 完成保养
        /// </summary>
        public bool ConfirmPlan
        {
            get
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@maintainOrder", this.maintainOrder);
                    param[1] = new SqlParameter("@confirmedName", this.panConfirmer.Name);
                    param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[2].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_confirmMaintainPlan", param);
                    return Convert.ToBoolean(param[2].Value);
                }
                catch (Exception ex)
                {
                    this.exceptionMsg = "ConfirmPlan：" + ex.Message;
                    return false;
                }
            }
        }
    }

    /// <summary>
    /// 固定资产保养操作
    /// </summary>
    public class FixasMaintainHelper
    {
        /// <summary>
        /// 查找到的记录数
        /// </summary>
        public static int Count = 0;
        /// <summary>
        /// 异常情况
        /// </summary>
        public static string ExceptionMsg = string.Empty;

        /// <summary>
        /// 获取固定资产保养单
        /// </summary>
        /// <param name="fixasNo">资产编号</param>
        /// <param name="maintainOrder">保养单</param>
        /// <param name="planMaintainDate1">计划保养日期1</param>
        /// <param name="planMaintainDate2">计划保养日期2</param>
        /// <param name="maintainedDate1">保养时间1</param>
        /// <param name="maintainedDate2">保养时间2</param>
        /// <returns></returns>
        public static IList<FixasMaintain> SelectMaintainOrder(string fixasNo, string maintainOrder, string planMaintainDate1, string planMaintainDate2, string maintainedDate1, string maintainedDate2, string maintainStatus, int type, int subType, int plantCode, string cc)
        {
            adamClass adam = new adamClass();
            IList<FixasMaintain> fixasMaintainList = new List<FixasMaintain>();
            Count = 0;
            int index = 0;
            try
            {
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@fixasNo", fixasNo);
                param[1] = new SqlParameter("@maintainOrder", maintainOrder);
                param[2] = new SqlParameter("@planMaintainDate1", planMaintainDate1);
                param[3] = new SqlParameter("@planMaintainDate2", planMaintainDate2);
                param[4] = new SqlParameter("@maintainedDate1", maintainedDate1);
                param[5] = new SqlParameter("@maintainedDate2", maintainedDate2);
                param[6] = new SqlParameter("@maintainStatus", maintainStatus);
                param[7] = new SqlParameter("@type", type);
                param[8] = new SqlParameter("@subType", subType);
                param[9] = new SqlParameter("@plantCode", plantCode);
                param[10] = new SqlParameter("@cc", cc);

                SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectMaintainOrder", param);
                while (reader.Read())
                {
                    FixasMaintain item = new FixasMaintain();

                    item.MaintainOrder = reader["plan_No"].ToString();

                    item.FixasInfo.FixasNo = reader["plan_fixasNo"].ToString();
                    item.FixasInfo.FixasName = reader["plan_fixasName"].ToString();
                    item.FixasInfo.FixasDesc = reader["plan_fixasDesc"].ToString();
                    item.FixasInfo.FixasType = reader["plan_fixasType"].ToString();
                    item.FixasInfo.FixasEntity = reader["plan_fixasEntity"].ToString();
                    item.FixasInfo.FixasVouDate = string.Format("{0:yyyy-MM-dd}", reader["plan_fixasVouDate"]);
                    item.FixasInfo.FixasSupplier = reader["plan_fixasSupplier"].ToString();
                    item.FixasInfo.FixasType = reader["plan_fixasType"].ToString();
                    item.FixasInfo.FixasSubType = reader["plan_fixasSubtype"].ToString();
                    item.FixasInfo.Domain = reader["domain"].ToString();
                    item.FixasInfo.CC = reader["ccDesc"].ToString();

                    item.PlanMaintainDate = string.Format("{0:yyyy-MM-dd HH:mm}", reader["plan_date"]);
                    item.MaintainDesc = reader["plan_maintainDesc"].ToString();
                    item.MaintainStatus = reader["plan_maintainStatus"].ToString();
                    if (reader["plan_createdBy"] != DBNull.Value)
                    {
                        item.PlanCreator.ID = Convert.ToInt32(reader["plan_createdBy"]);
                    }
                    item.PlanCreator.Name = reader["plan_createdName"].ToString();
                    if (reader["plan_createdDate"] != DBNull.Value)
                    {
                        item.PlanCreator.Date = Convert.ToDateTime(reader["plan_createdDate"]);
                    }
                    item.PlanConfirmer.Name = reader["plan_confirmedName"].ToString();
                    if (reader["plan_confirmedDate"] != DBNull.Value)
                    {
                        item.PlanConfirmer.Date = Convert.ToDateTime(reader["plan_confirmedDate"]);
                    }

                    item.MaintainedName = reader["rec_maintainedName"].ToString();
                    item.MaintainBeginDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader["rec_beginDate"]);
                    item.MaintainEndDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader["rec_endDate"]);
                    item.MaintainedRecord = reader["rec_record"].ToString();
                    if (reader["rec_modifiedBy"] != DBNull.Value)
                    {
                        item.MaintainModifier.ID = Convert.ToInt32(reader["rec_modifiedBy"]);
                    }
                    item.MaintainModifier.Name = reader["rec_modifiedName"].ToString();
                    if (reader["rec_modifiedDate"] != DBNull.Value)
                    {
                        item.MaintainModifier.Date = Convert.ToDateTime(reader["rec_modifiedDate"]);
                    }

                    index++;
                    fixasMaintainList.Add(item);
                }

                reader.Close();
                Count = index;
                return fixasMaintainList;
            }
            catch (Exception ex)
            {
                ExceptionMsg = "SelectMaintainOrder：" + ex.Message;
                return fixasMaintainList;
            }
        }

        /// <summary>
        /// 一定时间区间内，所有保养时长汇总
        /// </summary>
        /// <param name="fixasNo">资产编号</param>
        /// <param name="fixasName">资产名称</param>
        /// <param name="maintainDate1">保养时间区间1</param>
        /// <param name="maintainDate2">保养时间区间2</param>
        /// <returns></returns>
        public static DataSet SelectMaintainTimeCost(string fixasNo, string fixasName, string maintainDate1, string maintainDate2, int plantCode)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@fixasNo", fixasNo);
                param[1] = new SqlParameter("@fixasName", fixasName);
                param[2] = new SqlParameter("@maintainDate1", maintainDate1);
                param[3] = new SqlParameter("@maintainDate2", maintainDate2);
                param[4] = new SqlParameter("@plantCode", plantCode);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectMaintainTimeCost", param);

            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        ///一定时间区间内，已完成保养的时长汇总
        /// </summary>
        /// <param name="fixasNo">资产编号</param>
        /// <param name="fixasName">资产名称</param>
        /// <param name="maintainDate1">保养时间区间1</param>
        /// <param name="maintainDate2">保养时间区间2</param>
        /// <returns></returns>
        public static DataSet SelectMaintainTimeCost2(string fixasNo, string fixasName, string maintainDate1, string maintainDate2, int plantCode)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@fixasNo", fixasNo);
                param[1] = new SqlParameter("@fixasName", fixasName);
                param[2] = new SqlParameter("@maintainDate1", maintainDate1);
                param[3] = new SqlParameter("@maintainDate2", maintainDate2);
                param[4] = new SqlParameter("@plantCode", plantCode);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectMaintainTimeCost2", param);

            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        ///一定时间区间内，固定资产的保养状况
        /// </summary>
        /// <param name="fixasNo">资产编号</param>
        /// <param name="fixasName">资产名称</param>
        /// <param name="maintainDate1">保养时间区间1</param>
        /// <param name="maintainDate2">保养时间区间2</param>
        /// <returns></returns>
        public static DataSet SelectMaintainTrack(string fixasNo, string fixasName, string maintainDate1, string maintainDate2, int type, int subType, int plantCode)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@fixasNo", fixasNo);
                param[1] = new SqlParameter("@fixasName", fixasName);
                param[2] = new SqlParameter("@maintainDate1", maintainDate1);
                param[3] = new SqlParameter("@maintainDate2", maintainDate2);
                param[4] = new SqlParameter("@type", type);
                param[5] = new SqlParameter("@subType", subType);
                param[6] = new SqlParameter("@plantCode", plantCode);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectMaintainTrack", param);

            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 清理fixas_maintain_mstrTemp临时表的数据
        /// </summary>
        /// <param name="uID">userID</param>
        /// <returns></returns>
        public static bool ClearFixasMaintainMstrTemp(string uID)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@uID", uID);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_clearMaintainMstrTemp", param);

                return Convert.ToBoolean(param[1].Value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查fixas_maintain_mstrTemp临时表中的数据是否有误
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public static int CheckFixasMaintainMstrTempError(string uID, int plantCode)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@uID", uID);
                param[1] = new SqlParameter("@plantCode", plantCode);
                param[2] = new SqlParameter("@retValue", SqlDbType.Int);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_checkMaintainMstrTempError", param);

                return Convert.ToInt32(param[2].Value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将fixas_maintain_mstrTemp临时表中无误的数据导入到正式表fixas_maintain_mstr
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public static bool ImportFixasMaintainMstrTemp(string uID)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@uID", uID);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_importMaintainMstrTemp", param);

                return Convert.ToBoolean(param[1].Value);
            }
            catch
            {
                return false;
            }
        }

        public static DataSet SelectFixasMaintainMstrTempError(string uID)
        {
            adamClass adam = new adamClass();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_fixas_selectMaintainMstrTempError", param);
            }
            catch
            {
                return null;
            }
        }
    }
    #endregion

    #region 维护&保养项目
    /// <summary>
    /// 维护项目
    /// </summary>
    public class RepairItem
    {
        private adamClass adam = new adamClass();

        private int _ID;
        /// <summary>
        /// 维修项目ID
        /// </summary>
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        private string _Name;
        /// <summary>
        /// 维修项目名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        private string _Description;
        /// <summary>
        /// 维修项目备注
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        private User _Creator;
        /// <summary>
        /// 创建人
        /// </summary>
        public User Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }

        private User _Modifier;
        /// <summary>
        /// 修改人
        /// </summary>
        public User Modifier
        {
            get
            {
                return this._Modifier;
            }
            set
            {
                this._Modifier = value;
            }
        }

        /// <summary>
        /// 调用这个构造函数来创建一个空白记录。
        /// </summary>
        public RepairItem()
        {
            this._ID = 0;
            this._Name = string.Empty;
            this._Description = string.Empty;
            this._Creator = new User();
            this._Modifier = new User();
        }

        /// <summary>
        /// 调用这个函数，根据已知ID创建对象
        /// </summary>
        /// <param name="id"></param>
        public RepairItem(int id)
        {
            try
            {
                string strSql = "sp_Fixas_selectRepairItems";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["item_id"].ToString());
                    this._Name = reader["item_name"].ToString();
                    this._Description = reader["item_desc"].ToString();

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["item_createdby"].ToString());
                    this._Creator.Name = reader["item_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["item_createddate"].ToString());

                    this._Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["item_modifiedby"].ToString().Trim()))
                    {
                        this._Modifier.ID = Convert.ToInt32(reader["item_modifiedby"].ToString());
                        this._Modifier.Name = reader["item_modifier"].ToString();
                        this._Modifier.Date = Convert.ToDateTime(reader["item_modifieddate"].ToString());
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 插入新纪录
        /// </summary>
        public bool Insert
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_insertRepairItem";
                    SqlParameter[] sqlParam = new SqlParameter[6];
                    sqlParam[0] = new SqlParameter("@name", this._Name);
                    sqlParam[1] = new SqlParameter("@desc", this._Description);
                    sqlParam[2] = new SqlParameter("@createdby", this._Creator.ID);
                    sqlParam[3] = new SqlParameter("@createddate", this._Creator.Date);
                    sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[4].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[4].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 更新记录。如果该项目已经被使用，则项目名称不能被修改。
        /// </summary>
        public bool Update
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_updateRepairItem";
                    SqlParameter[] sqlParam = new SqlParameter[6];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@name", this._Name);
                    sqlParam[2] = new SqlParameter("@desc", this._Description);
                    sqlParam[3] = new SqlParameter("@modifiedby", this._Modifier.ID);
                    sqlParam[4] = new SqlParameter("@modifieddate", this._Modifier.Date);
                    sqlParam[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[5].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[5].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 删除记录。如果该项目已经被使用，则不能被删除。只需RepairItem.ID被赋值即可。
        /// </summary>
        public bool Delete
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_deleteRepairItem";
                    SqlParameter[] sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[1].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 系统不允许RepairItem.Name重复。必须指定ID（0表示新增，非0表示更新）和Name。
        /// </summary>
        /// <returns></returns>
        public bool IsExist
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_checkRepairItemIsAreadyExist";
                    SqlParameter[] sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@name", this._Name);
                    sqlParam[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[2].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[2].Value.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
    /// <summary>
    /// RepairItem索引
    /// </summary>
    public class RepairItemCollector : IListSource
    {
        private IList<RepairItem> _items;

        public RepairItemCollector(IList<RepairItem> items)
        {
            this._items = items;
        }

        /// <summary>
        /// 所包含元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        public RepairItem this[int index]
        {
            get
            {
                return this._items[index];
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList()
        {
            BindingList<RepairItem> items = new BindingList<RepairItem>();

            foreach (RepairItem item in this._items)
            {
                items.Add(item);
            }

            return items;
        }
    }
    /// <summary>
    /// RepairItem的主要类。通常从这个类开始引用
    /// </summary>
    public class RepairItemHelper
    {
        /// <summary>
        /// 按筛选条件选出记录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static RepairItemCollector SelectRepairItems(string name, string desc)
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_selectRepairItems";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@name", name);
                sqlParam[1] = new SqlParameter("@desc", desc);

                IList<RepairItem> list = new List<RepairItem>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                while (reader.Read())
                {
                    RepairItem item = new RepairItem();

                    item.ID = Convert.ToInt32(reader["item_id"].ToString());
                    item.Name = reader["item_name"].ToString();
                    item.Description = reader["item_desc"].ToString();

                    item.Creator = new User();
                    item.Creator.ID = Convert.ToInt32(reader["item_createdby"].ToString());
                    item.Creator.Name = reader["item_creator"].ToString();
                    item.Creator.Date = Convert.ToDateTime(reader["item_createddate"].ToString());

                    //如果item_createdby有零值，表示尚没有修改历史
                    item.Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["item_modifiedby"].ToString().Trim()))
                    {
                        item.Modifier.ID = Convert.ToInt32(reader["item_modifiedby"].ToString());
                        item.Modifier.Name = reader["item_modifier"].ToString();
                        item.Modifier.Date = Convert.ToDateTime(reader["item_modifieddate"].ToString());
                    }

                    list.Add(item);
                }

                reader.Close();

                return new RepairItemCollector(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 选出所有的记录
        /// </summary>
        public static RepairItemCollector SelectAllRepairItems()
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_selectRepairItems";

                IList<RepairItem> list = new List<RepairItem>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql);

                while (reader.Read())
                {
                    RepairItem item = new RepairItem();

                    item.ID = Convert.ToInt32(reader["item_id"].ToString());
                    item.Name = reader["item_name"].ToString();
                    item.Description = reader["item_desc"].ToString();

                    item.Creator = new User();
                    item.Creator.ID = Convert.ToInt32(reader["item_createdby"].ToString());
                    item.Creator.Name = reader["item_creator"].ToString();
                    item.Creator.Date = Convert.ToDateTime(reader["item_createddate"].ToString());

                    //如果item_createdby有零值，表示尚没有修改历史
                    item.Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["item_modifiedby"].ToString().Trim()))
                    {
                        item.Modifier.ID = Convert.ToInt32(reader["item_modifiedby"].ToString());
                        item.Modifier.Name = reader["item_modifier"].ToString();
                        item.Modifier.Date = Convert.ToDateTime(reader["item_modifieddate"].ToString());
                    }

                    list.Add(item);
                }

                reader.Close();

                return new RepairItemCollector(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
    #endregion

    #region 维护计划
    /// <summary>
    /// 维护计划
    /// </summary>
    public class Plan
    {
        #region 属性
        private adamClass adam = new adamClass();

        private int _ID;
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        private string _fixasNo;
        /// <summary>
        /// 资产编码
        /// </summary>
        public string FixasNo
        {
            get
            {
                return this._fixasNo;
            }
            set
            {
                this._fixasNo = value;
            }
        }

        private string _fixasName;
        /// <summary>
        /// 资产名称
        /// </summary>
        public string FixasName
        {
            get
            {
                return this._fixasName;
            }
            set
            {
                this._fixasName = value;
            }
        }

        private string _fixasDesc;
        /// <summary>
        /// 固定资产规格
        /// </summary>
        public string FixasDesc
        {
            get
            {
                return this._fixasDesc;
            }
            set
            {
                this._fixasDesc = value;
            }
        }

        private string _fixasType;
        /// <summary>
        /// 固定资产类型
        /// </summary>
        public string FixasType
        {
            get
            {
                return this._fixasType;
            }
            set
            {
                this._fixasType = value;
            }
        }

        private string _fixasEntity;
        /// <summary>
        /// 入账公司
        /// </summary>
        public string FixasEntity
        {
            get
            {
                return this._fixasEntity;
            }
            set
            {
                this._fixasEntity = value;
            }
        }

        private DateTime _fixasVouDate;
        /// <summary>
        /// 入账日期
        /// </summary>
        public DateTime FixasVouDate
        {
            get
            {
                return this._fixasVouDate;
            }
            set
            {
                this._fixasVouDate = value;
            }
        }

        private string _fixasSupplier;
        /// <summary>
        /// 供应商
        /// </summary>
        public string FixasSupplier
        {
            get
            {
                return this._fixasSupplier;
            }
            set
            {
                this._fixasSupplier = value;
            }
        }

        private DateTime _Date;
        /// <summary>
        /// 计划日期
        /// </summary>
        public DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                this._Date = value;
            }
        }

        public RepairItem _RepairItem;
        /// <summary>
        /// 维护项目
        /// </summary>
        public RepairItem RepairItem
        {
            get
            {
                return this._RepairItem;
            }
            set
            {
                this._RepairItem = value;
            }
        }

        private User _Creator;
        /// <summary>
        /// 创建人
        /// </summary>
        public User Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }

        private User _Modifier;
        /// <summary>
        /// 修改人
        /// </summary>
        public User Modifier
        {
            get
            {
                return this._Modifier;
            }
            set
            {
                this._Modifier = value;
            }
        }
        #endregion

        public Plan()
        {
            this._ID = 0;
            this._fixasNo = string.Empty;
            this._fixasName = string.Empty;
            this._fixasDesc = string.Empty;
            this._fixasType = string.Empty;
            this._fixasEntity = string.Empty;
            this._fixasVouDate = DateTime.Now;
            this._fixasSupplier = string.Empty;
            this._Date = DateTime.Now;
            this._RepairItem = new RepairItem();

            this._Creator = new User();
            this._Modifier = new User();
        }
        public Plan(int id)
        {
            try
            {
                string strSql = "sp_Fixas_selectPlans";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["plan_id"].ToString());
                    this._fixasNo = reader["plan_fixasNo"].ToString();
                    this._fixasName = reader["plan_fixasName"].ToString();
                    this._fixasDesc = reader["plan_fixasDesc"].ToString();
                    this._fixasType = reader["plan_fixasType"].ToString();
                    this._fixasEntity = reader["plan_fixasEntity"].ToString();
                    this._fixasVouDate = Convert.ToDateTime(reader["plan_fixasVouDate"].ToString());
                    this._fixasSupplier = reader["plan_fixasSupplier"].ToString();
                    this._Date = Convert.ToDateTime(reader["plan_date"].ToString());

                    this._RepairItem = new RepairItem();
                    this._RepairItem.ID = Convert.ToInt32(reader["plan_item_id"].ToString());
                    this._RepairItem.Name = reader["plan_item_name"].ToString();

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["plan_createdby"].ToString());
                    this._Creator.Name = reader["plan_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["plan_createddate"].ToString());

                    this._Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["plan_modifiedby"].ToString().Trim()))
                    {
                        this._Modifier.ID = Convert.ToInt32(reader["plan_modifiedby"].ToString());
                        this._Modifier.Name = reader["plan_modifier"].ToString();
                        this._Modifier.Date = Convert.ToDateTime(reader["plan_modifieddate"].ToString());
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 通过资产编码
        /// </summary>
        /// <param name="fixasno"></param>
        public Plan(string fixasno)
        {
            try
            {
                string strSql = "sp_Fixas_selectPlans";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@fixasNo", fixasno);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["plan_id"].ToString());
                    this._fixasNo = reader["plan_fixasNo"].ToString();
                    this._fixasName = reader["plan_fixasName"].ToString();
                    this._fixasDesc = reader["plan_fixasDesc"].ToString();
                    this._fixasType = reader["plan_fixasType"].ToString();
                    this._fixasEntity = reader["plan_fixasEntity"].ToString();
                    this._fixasVouDate = Convert.ToDateTime(reader["plan_fixasVouDate"].ToString());
                    this._fixasSupplier = reader["plan_fixasSupplier"].ToString();
                    this._Date = Convert.ToDateTime(reader["plan_date"].ToString());

                    this._RepairItem = new RepairItem();
                    this._RepairItem.ID = Convert.ToInt32(reader["plan_item_id"].ToString());
                    this._RepairItem.Name = reader["plan_item_name"].ToString();

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["plan_createdby"].ToString());
                    this._Creator.Name = reader["plan_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["plan_createddate"].ToString());

                    this._Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["plan_modifiedby"].ToString().Trim()))
                    {
                        this._Modifier.ID = Convert.ToInt32(reader["plan_modifiedby"].ToString());
                        this._Modifier.Name = reader["plan_modifier"].ToString();
                        this._Modifier.Date = Convert.ToDateTime(reader["plan_modifieddate"].ToString());
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 插入新纪录
        /// </summary>
        public bool Insert
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_insertPlan";
                    SqlParameter[] sqlParam = new SqlParameter[7];
                    sqlParam[0] = new SqlParameter("@fixasNo", this._fixasNo);
                    sqlParam[1] = new SqlParameter("@date", this._Date);
                    sqlParam[2] = new SqlParameter("@item_id", this._RepairItem.ID);
                    sqlParam[3] = new SqlParameter("@item_name", this._RepairItem.Name);
                    sqlParam[4] = new SqlParameter("@createdby", this._Creator.ID);
                    sqlParam[5] = new SqlParameter("@createddate", this._Creator.Date);
                    sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[6].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 更新计划。只能更新计划日期
        /// </summary>
        public bool Update
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_updatePlan";
                    SqlParameter[] sqlParam = new SqlParameter[7];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@item_id", this._RepairItem.ID);
                    sqlParam[2] = new SqlParameter("@item_name", this._RepairItem.Name);
                    sqlParam[3] = new SqlParameter("@date", this._Date);
                    sqlParam[4] = new SqlParameter("@modifiedby", this._Modifier.ID);
                    sqlParam[5] = new SqlParameter("@modifieddate", this._Modifier.Date);
                    sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[6].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Delete
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_deletePlan";
                    SqlParameter[] sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[1].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 系统不允许重复.当FixasNo、Date和RepairItem都重复是，方为重复。必须指定ID（0表示新增，非0表示更新）、FixasNo、Date和RepairItem。
        /// </summary>
        /// <returns></returns>
        public bool IsExist
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_checkPlanIsAreadyExist";
                    SqlParameter[] sqlParam = new SqlParameter[5];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@fixasNo", this._fixasNo);
                    sqlParam[2] = new SqlParameter("@planDate", this._Date);
                    sqlParam[3] = new SqlParameter("@item_id", this._RepairItem.ID);
                    sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[4].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[4].Value.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
    /// <summary>
    /// Plan的集合类。内置索引器
    /// </summary>
    public class PlanCollector : IListSource
    {
        private IList<Plan> _items;

        public PlanCollector(IList<Plan> items)
        {
            this._items = items;
        }

        /// <summary>
        /// 所包含元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        public Plan this[int index]
        {
            get
            {
                return this._items[index];
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList()
        {
            BindingList<Plan> items = new BindingList<Plan>();

            foreach (Plan item in this._items)
            {
                items.Add(item);
            }

            return items;
        }
    }
    /// <summary>
    /// Plan的主要类。通常从这个类开始引用
    /// </summary>
    public class PlanHelper
    {
        public static PlanCollector SelectPlans(string fixasNo, string fixasName, string fixasDesc, string fixasType, string fixasEntity, string fixasVouDate, string fixasSupplier, string planDate)
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_selectPlans";
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@fixasNo", fixasNo);
                sqlParam[1] = new SqlParameter("@fixasName", fixasName);
                sqlParam[2] = new SqlParameter("@fixasDesc", fixasDesc);
                sqlParam[3] = new SqlParameter("@fixasType", fixasType);
                sqlParam[4] = new SqlParameter("@fixasEntity", fixasEntity);
                sqlParam[5] = new SqlParameter("@fixasVouDate", fixasVouDate);
                sqlParam[6] = new SqlParameter("@fixasSupplier", fixasSupplier);
                sqlParam[7] = new SqlParameter("@planDate", planDate);

                IList<Plan> list = new List<Plan>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                while (reader.Read())
                {
                    Plan item = new Plan();

                    item.ID = Convert.ToInt32(reader["plan_id"].ToString());
                    item.FixasNo = reader["plan_fixasNo"].ToString();
                    item.FixasName = reader["plan_fixasName"].ToString();
                    item.FixasDesc = reader["plan_fixasDesc"].ToString();
                    item.FixasType = reader["plan_fixasType"].ToString();
                    item.FixasEntity = reader["plan_fixasEntity"].ToString();
                    item.FixasVouDate = Convert.ToDateTime(reader["plan_fixasVouDate"]);
                    item.FixasSupplier = reader["plan_fixasSupplier"].ToString();
                    item.Date = Convert.ToDateTime(reader["plan_date"]);

                    item.RepairItem = new RepairItem();
                    item.RepairItem.ID = Convert.ToInt32(reader["plan_item_id"].ToString());
                    item.RepairItem.Name = reader["plan_item_name"].ToString();

                    item.Creator = new User();
                    item.Creator.ID = Convert.ToInt32(reader["plan_createdby"].ToString());
                    item.Creator.Name = reader["plan_creator"].ToString();
                    item.Creator.Date = Convert.ToDateTime(reader["plan_createddate"].ToString());

                    item.Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["plan_modifiedby"].ToString().Trim()))
                    {
                        item.Modifier.ID = Convert.ToInt32(reader["plan_modifiedby"].ToString());
                        item.Modifier.Name = reader["plan_modifier"].ToString();
                        item.Modifier.Date = Convert.ToDateTime(reader["plan_modifieddate"].ToString());
                    }

                    list.Add(item);
                }

                reader.Close();

                return new PlanCollector(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 查看输入的资产编码是否存在
        /// </summary>
        /// <param name="fixasno"></param>
        /// <returns></returns>
        public static bool IsFixasAlreadyExist(string fixasno)
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_checkFixasIsAreadyExist";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@fixasNo", fixasno);
                sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                return Convert.ToBoolean(sqlParam[1].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
    #endregion

    #region 维修保养记录维护
    /// <summary>
    /// 维修保养记录维护
    /// </summary>
    public class Record
    {
        #region 属性
        private adamClass adam = new adamClass();

        private int _ID;
        /// <summary>
        /// Record的ID
        /// </summary>
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        private string _fixasNo;
        /// <summary>
        /// 资产编码
        /// </summary>
        public string FixasNo
        {
            get
            {
                return this._fixasNo;
            }
            set
            {
                this._fixasNo = value;
            }
        }

        private string _fixasName;
        /// <summary>
        /// 资产名称
        /// </summary>
        public string FixasName
        {
            get
            {
                return this._fixasName;
            }
            set
            {
                this._fixasName = value;
            }
        }

        private string _fixasDesc;
        /// <summary>
        /// 固定资产规格
        /// </summary>
        public string FixasDesc
        {
            get
            {
                return this._fixasDesc;
            }
            set
            {
                this._fixasDesc = value;
            }
        }

        private string _fixasType;
        /// <summary>
        /// 固定资产类型
        /// </summary>
        public string FixasType
        {
            get
            {
                return this._fixasType;
            }
            set
            {
                this._fixasType = value;
            }
        }

        private string _fixasEntity;
        /// <summary>
        /// 入账公司
        /// </summary>
        public string FixasEntity
        {
            get
            {
                return this._fixasEntity;
            }
            set
            {
                this._fixasEntity = value;
            }
        }

        private DateTime _fixasVouDate;
        /// <summary>
        /// 入账日期
        /// </summary>
        public DateTime FixasVouDate
        {
            get
            {
                return this._fixasVouDate;
            }
            set
            {
                this._fixasVouDate = value;
            }
        }

        private string _fixasSupplier;
        /// <summary>
        /// 供应商
        /// </summary>
        public string FixasSupplier
        {
            get
            {
                return this._fixasSupplier;
            }
            set
            {
                this._fixasSupplier = value;
            }
        }

        private RepairItem _RepairItem;
        /// <summary>
        /// 维护项目
        /// </summary>
        public RepairItem RepairItem
        {
            get
            {
                return this._RepairItem;
            }
            set
            {
                this._RepairItem = value;
            }
        }

        private User _Maintor;
        /// <summary>
        /// 维修人
        /// </summary>
        public User Maintor
        {
            get
            {
                return this._Maintor;
            }
            set
            {
                this._Maintor = value;
            }
        }

        private double _Money;
        /// <summary>
        /// 维修金额
        /// </summary>
        public double Money
        {
            get
            {
                return this._Money;
            }
            set
            {
                this._Money = value;
            }
        }

        private User _Creator;
        /// <summary>
        /// 创建人
        /// </summary>
        public User Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }

        private User _Modifier;
        /// <summary>
        /// 修改人
        /// </summary>
        public User Modifier
        {
            get
            {
                return this._Modifier;
            }
            set
            {
                this._Modifier = value;
            }
        }
        #endregion

        public Record()
        {
            this._ID = 0;
            this._fixasNo = string.Empty;
            this._fixasName = string.Empty;
            this._fixasDesc = string.Empty;
            this._fixasType = string.Empty;
            this._fixasEntity = string.Empty;
            this._fixasVouDate = DateTime.Now;
            this._fixasSupplier = string.Empty;
            this._RepairItem = new RepairItem();
            this._Maintor = new User();
            this._Money = 0.0;

            this._Creator = new User();
            this._Modifier = new User();
        }
        public Record(int id)
        {
            try
            {
                string strSql = "sp_Fixas_selectRecords";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["rec_id"].ToString());
                    this._fixasNo = reader["rec_fixasNo"].ToString();
                    this._fixasName = reader["rec_fixasName"].ToString();
                    this._fixasDesc = reader["rec_fixasDesc"].ToString();
                    this._fixasType = reader["rec_fixasType"].ToString();
                    this._fixasEntity = reader["rec_fixasEntity"].ToString();
                    this._fixasVouDate = Convert.ToDateTime(reader["rec_fixasVouDate"].ToString());
                    this._fixasSupplier = reader["rec_fixasSupplier"].ToString();

                    this._RepairItem = new RepairItem();
                    this._RepairItem.ID = Convert.ToInt32(reader["rec_item_id"].ToString());
                    this._RepairItem.Name = reader["rec_item_name"].ToString();

                    this._Maintor = new User();
                    this._Maintor.ID = Convert.ToInt32(reader["rec_maintedby"].ToString());
                    this._Maintor.Name = reader["rec_maintor"].ToString();
                    this._Maintor.Date = Convert.ToDateTime(reader["rec_mainteddate"].ToString());

                    this._Money = Convert.ToDouble(reader["rec_money"].ToString());

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["rec_createdby"].ToString());
                    this._Creator.Name = reader["rec_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["rec_createddate"].ToString());

                    this._Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["item_modifiedby"].ToString().Trim()))
                    {
                        this._Modifier.ID = Convert.ToInt32(reader["rec_modifiedby"].ToString());
                        this._Modifier.Name = reader["rec_modifier"].ToString();
                        this._Modifier.Date = Convert.ToDateTime(reader["rec_modifieddate"].ToString());
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 插入新纪录
        /// </summary>
        public bool Insert
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_insertRecord";
                    SqlParameter[] sqlParam = new SqlParameter[9];
                    sqlParam[0] = new SqlParameter("@fixasNo", this._fixasNo);
                    sqlParam[1] = new SqlParameter("@item_id", this._RepairItem.ID);
                    sqlParam[2] = new SqlParameter("@item_name", this._RepairItem.Name);
                    sqlParam[3] = new SqlParameter("@maintedby", this._Maintor.ID);
                    sqlParam[4] = new SqlParameter("@mainteddate", this._Maintor.Date);
                    sqlParam[5] = new SqlParameter("@money", this.Money);
                    sqlParam[6] = new SqlParameter("@createdby", this._Creator.ID);
                    sqlParam[7] = new SqlParameter("@createddate", this._Creator.Date);
                    sqlParam[8] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[8].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[8].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 更新计划。只能更新维护金额
        /// </summary>
        public bool Update
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_updateRecord";
                    SqlParameter[] sqlParam = new SqlParameter[5];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@money", this.Money);
                    sqlParam[2] = new SqlParameter("@modifiedby", this._Modifier.ID);
                    sqlParam[3] = new SqlParameter("@modifieddate", this._Modifier.Date);
                    sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[4].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[4].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Delete
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_deleteRecord";
                    SqlParameter[] sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[1].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 必须指定ID（0表示新增，非0表示更新）、FixasNo、Maintor和RepairItem。当资产编码、维修项目、维修人和金额都相同时，系统提示是否重复，由操作人决定！
        /// </summary>
        /// <returns></returns>
        public bool IsExist
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_checkRecordIsAreadyExist";
                    SqlParameter[] sqlParam = new SqlParameter[7];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@fixasNo", this._fixasNo);
                    sqlParam[2] = new SqlParameter("@item_id", this._RepairItem.ID);
                    sqlParam[3] = new SqlParameter("@maintedby", this._Maintor.ID);
                    sqlParam[4] = new SqlParameter("@mainteddate", this._Maintor.Date);
                    sqlParam[5] = new SqlParameter("@money", this.Money);
                    sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[6].Value.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
    /// <summary>
    /// Plan的集合类。内置索引器
    /// </summary>
    public class RecordCollector : IListSource
    {
        private IList<Record> _items;

        public RecordCollector(IList<Record> items)
        {
            this._items = items;
        }

        /// <summary>
        /// 所包含元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        public Record this[int index]
        {
            get
            {
                return this._items[index];
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList()
        {
            BindingList<Record> items = new BindingList<Record>();

            foreach (Record item in this._items)
            {
                items.Add(item);
            }

            return items;
        }
    }
    /// <summary>
    /// Record的主要类。通常从这个类开始引用
    /// </summary>
    public class RecordHelper
    {
        public static RecordCollector SelectRecords(string fixasNo, string fixasName, string fixasDesc, string fixasType, string fixasEntity, string fixasVouDate, string fixasSupplier, string maintor, string maintedDate)
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_selectRecords";
                SqlParameter[] sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@fixasNo", fixasNo);
                sqlParam[1] = new SqlParameter("@fixasName", fixasName);
                sqlParam[2] = new SqlParameter("@fixasDesc", fixasDesc);
                sqlParam[3] = new SqlParameter("@fixasType", fixasType);
                sqlParam[4] = new SqlParameter("@fixasEntity", fixasEntity);
                sqlParam[5] = new SqlParameter("@fixasVouDate", fixasVouDate);
                sqlParam[6] = new SqlParameter("@fixasSupplier", fixasSupplier);
                sqlParam[7] = new SqlParameter("@maintor", maintor);
                sqlParam[8] = new SqlParameter("@maintedDate", maintedDate);

                IList<Record> list = new List<Record>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                while (reader.Read())
                {
                    Record item = new Record();

                    item.ID = Convert.ToInt32(reader["rec_id"].ToString());
                    item.FixasNo = reader["rec_fixasNo"].ToString();
                    item.FixasName = reader["rec_fixasName"].ToString();
                    item.FixasDesc = reader["rec_fixasDesc"].ToString();
                    item.FixasType = reader["rec_fixasType"].ToString();
                    item.FixasEntity = reader["rec_fixasEntity"].ToString();
                    item.FixasVouDate = Convert.ToDateTime(reader["rec_fixasVouDate"].ToString());
                    item.FixasSupplier = reader["rec_fixasSupplier"].ToString();

                    item.RepairItem = new RepairItem();
                    item.RepairItem.ID = Convert.ToInt32(reader["rec_item_id"].ToString());
                    item.RepairItem.Name = reader["rec_item_name"].ToString();

                    item.Maintor = new User();
                    item.Maintor.ID = Convert.ToInt32(reader["rec_maintedby"].ToString());
                    item.Maintor.Name = reader["rec_maintor"].ToString();
                    item.Maintor.Date = Convert.ToDateTime(reader["rec_mainteddate"].ToString());

                    if (reader["rec_money"].ToString().Trim() == string.Empty)
                    {
                        item.Money = 0;
                    }
                    else
                    {
                        item.Money = Convert.ToDouble(reader["rec_money"].ToString());
                    }

                    item.Creator = new User();
                    item.Creator.ID = Convert.ToInt32(reader["rec_createdby"].ToString());
                    item.Creator.Name = reader["rec_creator"].ToString();
                    item.Creator.Date = Convert.ToDateTime(reader["rec_createddate"].ToString());

                    item.Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["rec_modifiedby"].ToString().Trim()))
                    {
                        item.Modifier.ID = Convert.ToInt32(reader["rec_modifiedby"].ToString());
                        item.Modifier.Name = reader["rec_modifier"].ToString();
                        item.Modifier.Date = Convert.ToDateTime(reader["rec_modifieddate"].ToString());
                    }

                    list.Add(item);
                }

                reader.Close();

                return new RecordCollector(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
    #endregion

    #region 备品备件编号
    /// <summary>
    /// 备品备件库存
    /// </summary>
    public class SpareItem
    {
        #region 属性
        private adamClass adam = new adamClass();

        private int _ID;
        /// <summary>
        /// 备品备件项目ID
        /// </summary>
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        private string _No;
        /// <summary>
        /// 备品编号
        /// </summary>
        public string No
        {
            get
            {
                return this._No;
            }
            set
            {
                this._No = value;
            }
        }

        private string _Description;
        /// <summary>
        /// 备品描述
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        private string _Device;
        /// <summary>
        /// 用于设备
        /// </summary>
        public string Device
        {
            get
            {
                return this._Device;
            }
            set
            {
                this._Device = value;
            }
        }

        private string _Floor;
        /// <summary>
        /// 库位
        /// </summary>
        public string Floor
        {
            get
            {
                return this._Floor;
            }
            set
            {
                this._Floor = value;
            }
        }

        private User _Creator;
        /// <summary>
        /// 创建人
        /// </summary>
        public User Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }

        private User _Modifier;
        /// <summary>
        /// 修改人
        /// </summary>
        public User Modifier
        {
            get
            {
                return this._Modifier;
            }
            set
            {
                this._Modifier = value;
            }
        }
        #endregion
        /// <summary>
        /// 调用这个构造函数来创建一个空白记录。
        /// </summary>
        public SpareItem()
        {
            this._ID = 0;
            this._No = string.Empty;
            this._Description = string.Empty;
            this._Device = string.Empty;
            this._Floor = string.Empty;
            this._Creator = new User();
            this._Modifier = new User();
        }

        /// <summary>
        /// 调用这个函数，根据已知ID创建对象
        /// </summary>
        /// <param name="id"></param>
        public SpareItem(int id)
        {
            try
            {
                string strSql = "sp_Fixas_selectSpareItems";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["spri_id"].ToString());
                    this._No = reader["spri_no"].ToString();
                    this._Description = reader["spri_desc"].ToString();
                    this._Device = reader["spri_device"].ToString();
                    this._Floor = reader["spri_floor"].ToString();

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["spri_createdby"].ToString());
                    this._Creator.Name = reader["spri_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["spri_createddate"].ToString());

                    this._Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["spri_modifiedby"].ToString().Trim()))
                    {
                        this._Modifier.ID = Convert.ToInt32(reader["spri_modifiedby"].ToString());
                        this._Modifier.Name = reader["spri_modifier"].ToString();
                        this._Modifier.Date = Convert.ToDateTime(reader["spri_modifieddate"].ToString());
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 调用这个函数，根据已知no创建对象
        /// </summary>
        /// <param name="no"></param>
        public SpareItem(string no)
        {
            try
            {
                string strSql = "sp_Fixas_selectSpareItems";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@no", no);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["spri_id"].ToString());
                    this._No = reader["spri_no"].ToString();
                    this._Description = reader["spri_desc"].ToString();
                    this._Device = reader["spri_device"].ToString();
                    this._Floor = reader["spri_floor"].ToString();

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["spri_createdby"].ToString());
                    this._Creator.Name = reader["spri_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["spri_createddate"].ToString());

                    this._Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["spri_modifiedby"].ToString().Trim()))
                    {
                        this._Modifier.ID = Convert.ToInt32(reader["spri_modifiedby"].ToString());
                        this._Modifier.Name = reader["spri_modifier"].ToString();
                        this._Modifier.Date = Convert.ToDateTime(reader["spri_modifieddate"].ToString());
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 调用这个函数，获取当前库存总量
        /// </summary>
        public int Inventory
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_selectSpareInventory";
                    SqlParameter[] sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@no", this._No);
                    sqlParam[1] = new SqlParameter("@floor", this._Floor);
                    sqlParam[2] = new SqlParameter("@inventory", SqlDbType.BigInt);
                    sqlParam[2].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToInt32(sqlParam[2].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 插入新的记录
        /// </summary>
        public bool Insert
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_insertSpareItem";
                    SqlParameter[] sqlParam = new SqlParameter[7];
                    sqlParam[0] = new SqlParameter("@no", this._No);
                    sqlParam[1] = new SqlParameter("@desc", this._Description);
                    sqlParam[2] = new SqlParameter("@device", this._Device);
                    sqlParam[3] = new SqlParameter("@floor", this._Floor);
                    sqlParam[4] = new SqlParameter("@createdby", this._Creator.ID);
                    sqlParam[5] = new SqlParameter("@createddate", this._Creator.Date);
                    sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[6].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        public bool Update
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_updateSpareItem";
                    SqlParameter[] sqlParam = new SqlParameter[7];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@desc", this._Description);
                    sqlParam[2] = new SqlParameter("@device", this._Device);
                    sqlParam[3] = new SqlParameter("@floor", this._Floor);
                    sqlParam[4] = new SqlParameter("@modifiedby", this._Modifier.ID);
                    sqlParam[5] = new SqlParameter("@modifieddate", this._Modifier.Date);
                    sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[6].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 删除记录。如果该项目已经被使用，则不能被删除。只需SpareItem.ID被赋值即可。
        /// </summary>
        public bool Delete
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_deleteSpareItem";
                    SqlParameter[] sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[1].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 必须指定ID（0表示新增，非0表示更新）、No！
        /// </summary>
        /// <returns></returns>
        public bool IsExist
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_checkSpareItemIsAreadyExist";
                    SqlParameter[] sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@id", this._ID);
                    sqlParam[1] = new SqlParameter("@no", this._No);
                    sqlParam[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[2].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[2].Value.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
    /// <summary>
    /// SpareItem的集合类。内置索引器
    /// </summary>
    public class SpareItemCollector : IListSource
    {
        private IList<SpareItem> _items;

        public SpareItemCollector(IList<SpareItem> items)
        {
            this._items = items;
        }

        /// <summary>
        /// 所包含元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        public SpareItem this[int index]
        {
            get
            {
                return this._items[index];
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList()
        {
            BindingList<SpareItem> items = new BindingList<SpareItem>();

            foreach (SpareItem item in this._items)
            {
                items.Add(item);
            }

            return items;
        }
    }
    #endregion

    #region 备品备件库存
    /// <summary>
    /// 备品备件库存
    /// </summary>
    public class SpareStock
    {
        #region 属性
        private adamClass adam = new adamClass();

        private int _ID;
        /// <summary>
        /// 备品备件项目ID
        /// </summary>
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        private string _No;
        /// <summary>
        /// 备品编号
        /// </summary>
        public string No
        {
            get
            {
                return this._No;
            }
            set
            {
                this._No = value;
            }
        }

        private string _Description;
        /// <summary>
        /// 备品描述
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        private string _Device;
        /// <summary>
        /// 用于设备
        /// </summary>
        public string Device
        {
            get
            {
                return this._Device;
            }
            set
            {
                this._Device = value;
            }
        }

        private int _Stock;
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Stock
        {
            get
            {
                return this._Stock;
            }
            set
            {
                this._Stock = value;
            }
        }

        private string _Floor;
        /// <summary>
        /// 出库时是领用车间；入库时是入库车间。
        /// </summary>
        public string Floor
        {
            get
            {
                return this._Floor;
            }
            set
            {
                this._Floor = value;
            }
        }

        private User _Creator;
        /// <summary>
        /// 创建人
        /// </summary>
        public User Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }
        #endregion
        /// <summary>
        /// 调用这个构造函数来创建一个空白记录。Type默认是入库
        /// </summary>
        public SpareStock()
        {
            this._ID = 0;
            this._No = string.Empty;
            this._Description = string.Empty;
            this._Device = string.Empty;
            this._Stock = 0;
            this._Floor = string.Empty;
            this._Creator = new User();
        }

        /// <summary>
        /// 调用这个函数，根据已知ID创建对象
        /// </summary>
        /// <param name="id"></param>
        public SpareStock(int id)
        {
            try
            {
                string strSql = "sp_Fixas_selectSpareStocks";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["spr_id"].ToString());
                    this._No = reader["spr_no"].ToString();
                    this._Description = reader["spr_desc"].ToString();
                    this._Device = reader["spr_device"].ToString();
                    this._Stock = Convert.ToInt32(reader["spr_qty_stock"].ToString());
                    this._Floor = reader["spr_floor"].ToString();

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["spr_createdby"].ToString());
                    this._Creator.Name = reader["spr_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["spr_createddate"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
    /// <summary>
    /// SpareStock的集合类。内置索引器
    /// </summary>
    public class SpareStockCollector : IListSource
    {
        private IList<SpareStock> _items;

        public SpareStockCollector(IList<SpareStock> items)
        {
            this._items = items;
        }

        /// <summary>
        /// 所包含元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        public SpareStock this[int index]
        {
            get
            {
                return this._items[index];
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList()
        {
            BindingList<SpareStock> items = new BindingList<SpareStock>();

            foreach (SpareStock item in this._items)
            {
                items.Add(item);
            }

            return items;
        }
    }
    #endregion

    #region 备品备件出入库
    public enum SpareType
    {
        /// <summary>
        /// 入库
        /// </summary>
        StockIn = 0,
        /// <summary>
        /// 出库
        /// </summary>
        StockOut = 1
    }

    /// <summary>
    /// 备品备件出入库
    /// </summary>
    public class SpareHist
    {
        #region 属性
        private adamClass adam = new adamClass();

        private int _ID;
        /// <summary>
        /// 备品备件项目ID
        /// </summary>
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        private string _No;
        /// <summary>
        /// 备品编号
        /// </summary>
        public string No
        {
            get
            {
                return this._No;
            }
            set
            {
                this._No = value;
            }
        }

        private string _Description;
        /// <summary>
        /// 备品描述
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        private string _Device;
        /// <summary>
        /// 用于设备
        /// </summary>
        public string Device
        {
            get
            {
                return this._Device;
            }
            set
            {
                this._Device = value;
            }
        }

        private int _Qty;
        /// <summary>
        /// 出入库数量
        /// </summary>
        public int Qty
        {
            get
            {
                return this._Qty;
            }
            set
            {
                this._Qty = value;
            }
        }

        private User _Holder;
        /// <summary>
        /// 出库时是领用人、出库日期；入库时是入库人、入库日期。
        /// </summary>
        public User Holder
        {
            get
            {
                return this._Holder;
            }
            set
            {
                this._Holder = value;
            }
        }

        private string _Floor;
        /// <summary>
        /// 出库时是领用车间；入库时是入库车间。
        /// </summary>
        public string Floor
        {
            get
            {
                return this._Floor;
            }
            set
            {
                this._Floor = value;
            }
        }

        public SpareType _Type;
        /// <summary>
        /// 出入库类型
        /// </summary>
        public SpareType Type
        {
            set
            {
                this._Type = value;
            }
            get
            {
                return this._Type;
            }
        }

        private User _Creator;
        /// <summary>
        /// 创建人
        /// </summary>
        public User Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }
        #endregion
        /// <summary>
        /// 调用这个构造函数来创建一个空白记录。Type默认是入库
        /// </summary>
        public SpareHist()
        {
            this._ID = 0;
            this._No = string.Empty;
            this._Description = string.Empty;
            this._Device = string.Empty;
            this._Qty = 0;
            this._Holder = new User();
            this._Floor = string.Empty;
            this._Type = SpareType.StockIn;//默认入库
            this._Creator = new User();
        }

        /// <summary>
        /// 调用这个函数，根据已知ID创建对象
        /// </summary>
        /// <param name="id"></param>
        public SpareHist(int id)
        {
            try
            {
                string strSql = "sp_Fixas_selectSpareHists";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    this._ID = Convert.ToInt32(reader["sprh_id"].ToString());
                    this._No = reader["sprh_no"].ToString();
                    this._Description = reader["sprh_desc"].ToString();
                    this._Device = reader["sprh_device"].ToString();
                    this._Qty = Convert.ToInt32(reader["sprh_qty"].ToString());
                    this._Floor = reader["sprh_floor"].ToString();

                    this._Holder = new User();
                    this._Holder.ID = Convert.ToInt32(reader["sprh_holdedby"].ToString());
                    this._Holder.Name = reader["sprh_holder"].ToString();
                    this._Holder.Date = Convert.ToDateTime(reader["sprh_holdeddate"].ToString());

                    if (Convert.ToBoolean(reader["sprh_type"].ToString()) == false)
                    {
                        this._Type = SpareType.StockIn;
                    }
                    else
                    {
                        this._Type = SpareType.StockOut;
                    }

                    this._Creator = new User();
                    this._Creator.ID = Convert.ToInt32(reader["sprh_createdby"].ToString());
                    this._Creator.Name = reader["sprh_creator"].ToString();
                    this._Creator.Date = Convert.ToDateTime(reader["sprh_createddate"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 插入新的出|入库记录
        /// </summary>
        public bool Insert
        {
            get
            {
                try
                {
                    string strSql = "sp_Fixas_insertSpareHist";
                    SqlParameter[] sqlParam = new SqlParameter[10];
                    sqlParam[0] = new SqlParameter("@no", this._No);
                    sqlParam[1] = new SqlParameter("@qty", this._Qty);
                    sqlParam[2] = new SqlParameter("@device", this._Device);
                    sqlParam[3] = new SqlParameter("@floor", this._Floor);
                    sqlParam[4] = new SqlParameter("@holdedby", this._Holder.ID);
                    sqlParam[5] = new SqlParameter("@holdeddate", this._Holder.Date);
                    sqlParam[6] = new SqlParameter("@type", SqlDbType.Bit);

                    if (this._Type == SpareType.StockIn)
                    {
                        sqlParam[6].Value = false;
                    }
                    else
                    {
                        sqlParam[6].Value = true;
                    }

                    sqlParam[7] = new SqlParameter("@createdby", this._Creator.ID);
                    sqlParam[8] = new SqlParameter("@createddate", this._Creator.Date);
                    sqlParam[9] = new SqlParameter("@retValue", SqlDbType.Bit);
                    sqlParam[9].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                    return Convert.ToBoolean(sqlParam[9].Value);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
    /// <summary>
    /// Spare的集合类。内置索引器
    /// </summary>
    public class SpareHistCollector : IListSource
    {
        private IList<SpareHist> _items;

        public SpareHistCollector(IList<SpareHist> items)
        {
            this._items = items;
        }

        /// <summary>
        /// 所包含元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        public SpareHist this[int index]
        {
            get
            {
                return this._items[index];
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList()
        {
            BindingList<SpareHist> items = new BindingList<SpareHist>();

            foreach (SpareHist item in this._items)
            {
                items.Add(item);
            }

            return items;
        }
    }

    #endregion

    #region Spare的主要类。通常从这个类开始引用
    /// <summary>
    /// Spare的主要类。通常从这个类开始引用
    /// </summary>
    public class SpareHelper
    {
        public static SpareItemCollector SelectSpareItems(string no, string desc, string device, string floor)
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_selectSpareItems";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@no", no);
                sqlParam[1] = new SqlParameter("@desc", desc);
                sqlParam[2] = new SqlParameter("@device", device);
                sqlParam[3] = new SqlParameter("@floor", floor);

                IList<SpareItem> list = new List<SpareItem>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                while (reader.Read())
                {
                    SpareItem item = new SpareItem();

                    item.ID = Convert.ToInt32(reader["spri_id"].ToString());
                    item.No = reader["spri_no"].ToString();
                    item.Description = reader["spri_desc"].ToString();
                    item.Device = reader["spri_device"].ToString();
                    item.Floor = reader["spri_floor"].ToString();

                    item.Creator = new User();
                    item.Creator.ID = Convert.ToInt32(reader["spri_createdby"].ToString());
                    item.Creator.Name = reader["spri_creator"].ToString();
                    item.Creator.Date = Convert.ToDateTime(reader["spri_createddate"].ToString());

                    item.Modifier = new User();
                    Regex regInt = new Regex(@"^[1-9][0-9]*$");
                    if (regInt.IsMatch(reader["spri_modifiedby"].ToString().Trim()))
                    {
                        item.Modifier.ID = Convert.ToInt32(reader["spri_modifiedby"].ToString());
                        item.Modifier.Name = reader["spri_modifier"].ToString();
                        item.Modifier.Date = Convert.ToDateTime(reader["spri_modifieddate"].ToString());
                    }

                    list.Add(item);
                }

                reader.Close();

                return new SpareItemCollector(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static SpareHistCollector SelectSpareHists(string no1, string no2, string device, string holderdate1, string holderdate2
                                        , string floor, string holder, string createddate1, string createddate2, string type)
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_selectSpareHists";
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@no1", no1);
                sqlParam[1] = new SqlParameter("@no2", no2);
                sqlParam[2] = new SqlParameter("@device", device);
                sqlParam[3] = new SqlParameter("@holderdate1", holderdate1);
                sqlParam[4] = new SqlParameter("@holderdate2", holderdate2);
                sqlParam[5] = new SqlParameter("@floor", floor);
                sqlParam[6] = new SqlParameter("@holder", holder);
                sqlParam[7] = new SqlParameter("@createddate1", createddate1);
                sqlParam[8] = new SqlParameter("@createddate2", createddate2);
                sqlParam[9] = new SqlParameter("@type", type);

                IList<SpareHist> list = new List<SpareHist>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                while (reader.Read())
                {
                    SpareHist item = new SpareHist();

                    item.ID = Convert.ToInt32(reader["sprh_id"].ToString());
                    item.No = reader["sprh_no"].ToString();
                    item.Description = reader["sprh_desc"].ToString();
                    item.Device = reader["sprh_device"].ToString();
                    item.Qty = Convert.ToInt32(reader["sprh_qty"].ToString());
                    item.Floor = reader["sprh_floor"].ToString();

                    item.Holder = new User();
                    item.Holder.ID = Convert.ToInt32(reader["sprh_holdedby"].ToString());
                    item.Holder.Name = reader["sprh_holder"].ToString();
                    item.Holder.Date = Convert.ToDateTime(reader["sprh_holdeddate"].ToString());

                    if (Convert.ToBoolean(reader["sprh_type"].ToString()) == false)
                    {
                        item.Type = SpareType.StockIn;
                    }
                    else
                    {
                        item.Type = SpareType.StockOut;
                    }

                    item.Creator = new User();
                    item.Creator.ID = Convert.ToInt32(reader["sprh_createdby"].ToString());
                    item.Creator.Name = reader["sprh_creator"].ToString();
                    item.Creator.Date = Convert.ToDateTime(reader["sprh_createddate"].ToString());

                    list.Add(item);
                }

                reader.Close();

                return new SpareHistCollector(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static SpareStockCollector SelectSpareStocks(string no, string desc, string device, string floor)
        {
            adamClass adam = new adamClass();

            try
            {
                string strSql = "sp_Fixas_selectSpareStocks";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@no", no);
                sqlParam[1] = new SqlParameter("@desc", desc);
                sqlParam[2] = new SqlParameter("@device", device);
                sqlParam[3] = new SqlParameter("@floor", floor);

                IList<SpareStock> list = new List<SpareStock>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

                while (reader.Read())
                {
                    SpareStock item = new SpareStock();

                    item.ID = Convert.ToInt32(reader["spr_id"].ToString());
                    item.No = reader["spr_no"].ToString();
                    item.Description = reader["spr_desc"].ToString();
                    item.Device = reader["spr_device"].ToString();
                    item.Stock = Convert.ToInt32(reader["spr_qty_stock"].ToString());
                    item.Floor = reader["spr_floor"].ToString();

                    item.Creator = new User();
                    item.Creator.ID = Convert.ToInt32(reader["spr_createdby"].ToString());
                    item.Creator.Name = reader["spr_creator"].ToString();
                    item.Creator.Date = Convert.ToDateTime(reader["spr_createddate"].ToString());

                    list.Add(item);
                }

                reader.Close();

                return new SpareStockCollector(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
    #endregion
    #endregion
}