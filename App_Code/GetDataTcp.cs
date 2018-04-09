using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GetDataTcp
/// </summary>
/// 
namespace TCPNEW
{
    public class GetDataTcp
    {
        static adamClass adam = new adamClass();
        public GetDataTcp()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region GetData and delete data for Entity in fix asset module
        public static DataTable GetEntityFixAsset()
        {
            try
            {
                DataSet dstEntity = null;
                string strName = "sp_Fix_GetEntity";
                dstEntity = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName);

                return dstEntity.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static int DeleteEntity(int enti_id, int enti_by)
        {
            try
            {
                string strName = "sp_Fix_DeleteEntity";
                SqlParameter[] parmEntity = new SqlParameter[2];
                parmEntity[0] = new SqlParameter("@enti_id", enti_id);
                parmEntity[1] = new SqlParameter("@enti_by", enti_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmEntity);
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region /* GetData and delete data for Status in fix asset module */
        public static DataTable GetStatusFixAsset()
        {
            try
            {
                DataSet dstStatus = null;
                string strName = "sp_Fix_GetStatus";
                dstStatus = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName);

                return dstStatus.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static int DeleteStatus(int fixsta_id, int fixsta_by)
        {
            try
            {
                string strName = "sp_Fix_DeleteStatus";
                SqlParameter[] parmStatus = new SqlParameter[2];
                parmStatus[0] = new SqlParameter("@fixsta_id", fixsta_id);
                parmStatus[1] = new SqlParameter("@fixsta_by", fixsta_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmStatus);
            }
            catch
            {
                return -1;
            }

        }


        public static int SaveOrModifiedStatus(int fixsta_id, string fixsta_name, int fixsta_by)
        {
            try
            {
                string strName = "sp_Fix_SaveOrModifyStatus";
                SqlParameter[] parmStatus = new SqlParameter[3];
                parmStatus[0] = new SqlParameter("@fixsta_id", fixsta_id);
                parmStatus[1] = new SqlParameter("@fixsta_name", fixsta_name);
                parmStatus[2] = new SqlParameter("@fixsta_by", fixsta_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmStatus);
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region /* GetData and delete data for Type in fix asset module */
        public static DataTable GetTypeFixAsset()
        {
            try
            {
                DataSet dstType = null;
                string strName = "sp_Fix_GetType";
                dstType = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName);

                return dstType.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static int DeleteType(int fixtp_id, int fixtp_by)
        {
            try
            {
                string strName = "sp_Fix_DeleteType";
                SqlParameter[] parmType = new SqlParameter[2];
                parmType[0] = new SqlParameter("@fixtp_id", fixtp_id);
                parmType[1] = new SqlParameter("@fixtp_by", fixtp_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmType);
            }
            catch
            {
                return -1;
            }
        }

        public static int SaveOrModifyType(int fixtp_id, string fixtp_name, int fixtp_lefttime, int fixtp_by)
        {

            try
            {
                string strName = "sp_Fix_SaveOrModifyType";
                SqlParameter[] parmType = new SqlParameter[4];
                parmType[0] = new SqlParameter("@fixtp_id", fixtp_id);
                parmType[1] = new SqlParameter("@fixtp_name", fixtp_name);
                parmType[2] = new SqlParameter("@fixtp_lefttime", fixtp_lefttime);
                parmType[3] = new SqlParameter("@fixtp_by", fixtp_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmType);
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region /* GetData and delete data for TypeDetail in fix asset module */

        public static int SaveOrModifyTypeDetail(int parentID, int fixtp_det_id, string fixtp_det_name, int fixtp_det_lefttime, int fixtp_by)
        {

            try
            {
                string strName = "sp_Fix_SaveOrModifyTypeDetail";
                SqlParameter[] parmType = new SqlParameter[5];
                parmType[0] = new SqlParameter("@parentID", parentID);
                parmType[1] = new SqlParameter("@fixtp_det_id", fixtp_det_id);
                parmType[2] = new SqlParameter("@fixtp_det_name", fixtp_det_name);
                parmType[3] = new SqlParameter("@fixtp_det_lefttime", fixtp_det_lefttime);
                parmType[4] = new SqlParameter("@fixtp_by", fixtp_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmType);
            }
            catch
            {
                return -1;

            }
        }


        public static int DeleteTypeDetail(int @fixtp_det_id, int fixtp_by)
        {
            try
            {
                string strName = "sp_Fix_DeleteTypeDetail";
                SqlParameter[] parmType = new SqlParameter[2];
                parmType[0] = new SqlParameter("@fixtp_det_id", @fixtp_det_id);
                parmType[1] = new SqlParameter("@fixtp_by", fixtp_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmType);
            }
            catch
            {
                return -1;
            }
        }

        public static DataSet selectTypeDetailFixAsset(int parentID)
        {
            try
            {
                string strName = "sp_Fix_selectTypeDetail";
                SqlParameter parm = new SqlParameter("@parentID", parentID);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }


        }
        public static SqlDataReader GetParentType(int parentID)
        {
            try
            {
                string strName = "sp_Fix_selectParentType";
                SqlParameter parm = new SqlParameter("@parentID", parentID);
                return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region /* GetData and delete data for Cost Center in fix asset module */
        public static DataTable GetCostCenterFixAsset(int EntityID)
        {
            try
            {
                DataSet dstCostCenter = null;
                string strName = "sp_Fix_GetCostCenter";
                SqlParameter parm = new SqlParameter("@EntityID", EntityID);
                dstCostCenter = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                return dstCostCenter.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetLineFixAsset(int EntityID)
        {
            try
            {
                DataSet dstLine = null;
                string strName = "sp_Fix_selectLine";
                SqlParameter parm = new SqlParameter("@EntityID", EntityID);
                dstLine = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                return dstLine.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static int DeleteCostCenter(int fixctc_id, int fixctc_by)
        {
            try
            {
                string strName = "sp_Fix_DeleteCostCenter";
                SqlParameter[] parmCtC = new SqlParameter[2];
                parmCtC[0] = new SqlParameter("@fixctc_id", fixctc_id);
                parmCtC[1] = new SqlParameter("@fixctc_by", fixctc_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmCtC);
            }
            catch
            {
                return -1;
            }
        }

        public static int SaveOrModifyCostCenter(int fixctc_id, int fixctc_enti_id, string fixctc_no, string fixctc_name, int fixctc_by)
        {

            try
            {
                string strName = "sp_Fix_SaveOrModifyCostCenter";
                SqlParameter[] parmCtC = new SqlParameter[5];
                parmCtC[0] = new SqlParameter("@fixctc_id", fixctc_id);
                parmCtC[1] = new SqlParameter("@fixctc_enti_id", fixctc_enti_id);
                parmCtC[2] = new SqlParameter("@fixctc_no", fixctc_no);
                parmCtC[3] = new SqlParameter("@fixctc_name", fixctc_name);
                parmCtC[4] = new SqlParameter("@fixctc_by", fixctc_by);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmCtC);
            }
            catch
            {
                return -1;
            }
        }
        #endregion



        public static DataTable GetAssetDetail(int ParentID)
        {
            try
            {
                DataSet dsDetail = null;
                string strName = "sp_Fix_GetAssetDetail";
                SqlParameter parm = new SqlParameter("@fixas_det_parent", ParentID);
                dsDetail = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
                return dsDetail.Tables[0];
            }
            catch
            {
                return null;
            }

        }

        public static DataTable GetAssetHistDetail(string AssetNo)
        {
            try
            {
                DataSet dsDetail = null;
                string strName = "sp_Fix_GetAssetHistDetail";
                SqlParameter parm = new SqlParameter("@AssetNo", AssetNo);
                dsDetail = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
                return dsDetail.Tables[0];
            }
            catch
            {
                return null;
            }

        }

        public static int DelAssetDetailRecord(int fixas_det_id, int fixas_det_createdBy)
        {
            try
            {
                string strName = "sp_Fix_DelAssetDetailRecord";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fixas_det_id", fixas_det_id);
                param[1] = new SqlParameter("@fixas_det_createdBy", fixas_det_createdBy);

                return SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return -1;
            }

        }


        //public static DataTable GetFixAssetSearch(string AssetNo, string AssetDate, string VouDate, string rate)
        //{
        //    try
        //    {
        //        DataSet dsDetail = null;
        //        string strName = "sp_Fix_GetFixAssetInfo";
        //        //SqlParameter[] param = new SqlParameter[4];
        //        //param[0] = new SqlParameter("@AssetNo", AssetNo);
        //        //param[1] = new SqlParameter("@AssetDate", AssetDate);
        //        //param[2] = new SqlParameter("@VouDate", VouDate);
        //        //param[3] = new SqlParameter("@rate", rate);

        //        dsDetail = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName);
        //        return dsDetail.Tables[0];
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public static DataTable GetFixAsset(string AssetNo, string AssetName, string AssetSpec, int TypeID, int EntityID,
                                            string StdDate, string EndDate, string Voucher, decimal ACost, string ASupplier, string AReference,
                                            string ACode, string AComment, int DetailTypeID)
        {
            try
            {
                DataSet dsDetail = null;
                string strName = "sp_Fix_GetFixAsset";
                SqlParameter[] param = new SqlParameter[14];
                param[0] = new SqlParameter("@AssetNo", AssetNo);
                param[1] = new SqlParameter("@AssetName", AssetName);
                param[2] = new SqlParameter("@AssetSpec", AssetSpec);
                param[3] = new SqlParameter("@TypeID", TypeID);
                param[4] = new SqlParameter("@EntityID", EntityID);
                param[5] = new SqlParameter("@StdDate", StdDate);
                param[6] = new SqlParameter("@EndDate", EndDate);
                param[7] = new SqlParameter("@Voucher", Voucher);
                param[8] = new SqlParameter("@ACost", ACost);
                param[9] = new SqlParameter("@ASupplier", ASupplier);
                param[10] = new SqlParameter("@AReference", AReference);
                param[11] = new SqlParameter("@ACode", ACode);
                param[12] = new SqlParameter("@AComment", AComment);
                param[13] = new SqlParameter("@DetailTypeID", DetailTypeID);

                dsDetail = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param);
                return dsDetail.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetFixAssetHist(string AssetNo, string AssetName, string AssetSpec, int TypeID, int EntityID,
                                    string StdDate, string EndDate, string Voucher, decimal ACost, string ASupplier, string AReference,
                                    string ACode, string AComment)
        {
            try
            {
                DataSet dsDetail = null;
                string strName = "sp_Fix_GetFixAssetHist";
                SqlParameter[] param = new SqlParameter[13];
                param[0] = new SqlParameter("@AssetNo", AssetNo);
                param[1] = new SqlParameter("@AssetName", AssetName);
                param[2] = new SqlParameter("@AssetSpec", AssetSpec);
                param[3] = new SqlParameter("@TypeID", TypeID);
                param[4] = new SqlParameter("@EntityID", EntityID);
                param[5] = new SqlParameter("@StdDate", StdDate);
                param[6] = new SqlParameter("@EndDate", EndDate);
                param[7] = new SqlParameter("@Voucher", Voucher);
                param[8] = new SqlParameter("@ACost", ACost);
                param[9] = new SqlParameter("@ASupplier", ASupplier);
                param[10] = new SqlParameter("@AReference", AReference);
                param[11] = new SqlParameter("@ACode", ACode);
                param[12] = new SqlParameter("@AComment", AComment);

                dsDetail = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param);
                return dsDetail.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static string GetDiscountByTypeID(int ID)
        {
            try
            {
                string sqlStr = "sp_Fix_GetDiscountByTypeID";

                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter ("@ID",ID)
                
                };

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlStr, param).ToString();

            }
            catch
            {
                return string.Empty;
            }
        }

     public static DataTable GetFixAssetReserve(string AssetNo, string AssetDate, string VouDate, string rate)
        {
            try
            {
                DataSet dsDetail = null;
                string strName = "sp_Fix_GetFixAssetInfo";
                SqlParameter param = new SqlParameter("@AssetNo", AssetNo);

                dsDetail = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName,param);
                return dsDetail.Tables[0];
            }
            catch
            {
                return null;
            }
        }
        }
      
}
