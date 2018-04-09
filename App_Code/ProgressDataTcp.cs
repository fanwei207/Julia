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
using System.Data.SqlClient;
using adamFuncs;

/// <summary>
/// Summary description for ProgressDataTcp
/// </summary>
/// 
namespace TCPNEW
{
    public class ProgressDataTcp
    {
        static adamClass adam = new adamClass();

        private ProgressDataTcp()
        {
            //
            // TODO: Add constructor logic here
            //   
        }



        public static DataTable GetWorkproType()
        {
            try
            {
                DataSet dstWorkpro = null;
                string strName = "sp_New_GetWorkproType";
                dstWorkpro = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName);

                return dstWorkpro.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        //summary
        //Process for work procedure, including save, update and delete
        //summary

        public static void UpdateWorkpro(int systemCodeID, string systemCodeName)
        {
            try
            {

                string strName = "sp_New_UpdateWorkpro";
                SqlParameter[] parmWorkpro = new SqlParameter[2];
                parmWorkpro[0] = new SqlParameter("@systemCodeID", systemCodeID);
                parmWorkpro[1] = new SqlParameter("@systemCodeName", systemCodeName);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmWorkpro);

            }
            catch
            {
                return;
            }
        }

        public static void DeleteWorkpro(int systemCodeID)
        {
            try
            {
                string strName = "sp_New_DelWorkpro";
                SqlParameter parmWorkpro = new SqlParameter("@systemCodeID", systemCodeID);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmWorkpro);
            }
            catch
            {
                return;
            }
        }


        public static void SaveWorkPro(string strWorkPro)
        {
            try
            {
                string strName = "sp_New_SaveWorkpro";
                SqlParameter parmWorkpro = new SqlParameter("@Name", strWorkPro);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmWorkpro);
            }
            catch
            {
                return;
            }
        }
        // End work procedure type  

        #region Process for PieceWorkPro procedure, including save, update and delete

        public static DataTable GetWorkKinds() 
        {
            try
            {
                DataSet dstWorkKinds = null;
                string strName = "select ID,Name from workkinds order by ID";
                dstWorkKinds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, strName);

                return dstWorkKinds.Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetSystemCode()
        {
            try
            {
                DataSet dstSystemCode = null;
                string strName = "SELECT systemCodeID,systemCodeName FROM SystemCode WHERE systemCodeTypeID='41' ORDER BY systemCodeID";
                dstSystemCode = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName);

                return dstSystemCode.Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetPieceWorkPro(string str)
        {
            try
            {
                DataSet dstPieceWorkPro = null;
                string strName = str;
                dstPieceWorkPro = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strName);

                return dstPieceWorkPro.Tables[0];
            }
            catch
            {
                return null;
            }
        }
        public static void DeletePieceWorkPro(string wid)
        {
            try
            {
                string strName = "delete from PieceWorkPro where id =" + wid;
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.Text, strName);
            }
            catch
            {
                return;
            }
        }
        public static int ModifyPieceWorkPro(string id,string sType,string wKinds,string price,string coeff,string saprice,string comment,string workdate,string modifiedBy) 
        {
            int result = -1;

            try
            {
                string strName = "sp_New_PieceWorkProModified";
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@wType", sType);
                param[2] = new SqlParameter("@wkinds", wKinds);
                param[3] = new SqlParameter("@price", price);
                param[4] = new SqlParameter("@coeff", coeff);
                param[5] = new SqlParameter("@sdprice", saprice);
                param[6] = new SqlParameter("@comment", comment);
                param[7] = new SqlParameter("@workdate", workdate);
                param[8] = new SqlParameter("@modifiedBy", modifiedBy);

                result = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        public static int SavePieceWorkPro(string sType, string wKinds, string price, string coeff, string saprice, string comment, string workdate, string createBy) 
        {
            int result = -1;

            try
            {
                string strName = "sp_New_PieceWorkProAddNew";

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@wType", sType);
                param[1] = new SqlParameter("@wkinds", wKinds);
                param[2] = new SqlParameter("@price", price);
                param[3] = new SqlParameter("@coeff", coeff);
                param[4] = new SqlParameter("@sdprice", saprice);
                param[5] = new SqlParameter("@comment", comment);
                param[6] = new SqlParameter("@workdate", workdate);
                param[7] = new SqlParameter("@createBy", createBy);

                result = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, param);

            }
            catch
            {
                result = -1; ;
            }
            return result;
        }
        #endregion

    //summary
    // Save a new record or modified record for entity in fix asset module
    //summary
    public static int SaveAndModifyEntity(string strEntity, string  strComment, int intEntityID, int intUserID)
    {
        try
        {
            string strName ="sp_Fix_EntityMaintenance";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@enti_name", strEntity);
            param[1] = new SqlParameter("@enti_comment", strComment);
            param[2] = new SqlParameter("@enti_id", intEntityID);
            param[3] = new SqlParameter("@enti_by", intUserID);
        
            return  SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return -1;
        }
    }


        public static DataTable CheckAssetNo(string strAssetNo)
        {
            try
            {
                DataSet dstAsset = null;
                string strName = "sp_Fix_CheckAssetNo";
                SqlParameter parmAsset = new SqlParameter("@AssetNo", strAssetNo);
                dstAsset = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parmAsset);
                return dstAsset.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataSet CheckAssetIncrementNo(string strAssetIncrementNo)
        {
            try
            {
                DataSet dstAsset = null;
                string strName = "sp_Fix_CheckAssetIncrementNo";
                SqlParameter parmAsset = new SqlParameter("@AssetNo", strAssetIncrementNo);
                dstAsset = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parmAsset);
                return dstAsset;
            }
            catch
            {
                return null;
            }
        }

        public static int DelAssetRecord(int intAssetID,int modifiedBy)
        {
            try
            {
                string strName = "sp_Fix_DelAssetRecord";
                SqlParameter[] parmAsset = new SqlParameter[2];
                parmAsset[0] = new SqlParameter("@AssetID", intAssetID);
                parmAsset[1] = new SqlParameter("@modifiedBy", modifiedBy);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmAsset);
                return 0;
            }
            catch
            {
                return -1;
            }

        }

        public static int DelAssetIncrementRecord(int intAssetIncrementID, int modifiedBy)
        {
            try
            {
                string strName = "sp_Fix_DelAssetIncrementRecord";
                SqlParameter[] parmAsset = new SqlParameter[2];
                parmAsset[0] = new SqlParameter("@AssetID", intAssetIncrementID);
                parmAsset[1] = new SqlParameter("@modifiedBy", modifiedBy);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parmAsset);
                return 0;
            }
            catch
            {
                return -1;
            }

        }

        public static int SaveAndModifyAsset(string AssetNum,string AssetName,string AssetSpec, int AssetType,string Cost, int Entity, string MachineCode,
                                             string Voucher, string VoucherDate, string Supplier, string Comment, string Reference, Byte[] AssetPhoto, int intAssetID,
                                              int createdBy, int modifiedBy, string ImgType, int flag, string discount, int TypeDetail ,string maintainPeriod,string photoPath)
        {
            try
            {
                string strName = "sp_Fix_SaveAndModifyAsset";
                SqlParameter[] param = new SqlParameter[22];
                param[0] = new SqlParameter("@fixas_id", intAssetID);
                param[1] = new SqlParameter("@fixas_no", AssetNum);
                param[2] = new SqlParameter("@fixas_name", AssetName);
                param[3] = new SqlParameter("@fixas_spec", AssetSpec);
                param[4] = new SqlParameter("@fixas_tp_id", AssetType);
                param[5] = new SqlParameter("@fixas_ent_id", Entity);
                param[6] = new SqlParameter("@fixas_voucher", Voucher);
                param[7] = new SqlParameter("@fixas_vou_date",VoucherDate);
                param[8] = new SqlParameter("@fixas_cost", Convert.ToDecimal(Cost));
                param[9] = new SqlParameter("@fixas_supplier", Supplier);
                param[10] = new SqlParameter("@fixas_reference", Reference);
                param[11] = new SqlParameter("@fixas_comment", Comment);
                param[12] = new SqlParameter("@fixas_code", MachineCode);
                param[13] = new SqlParameter("@fixas_photo", SqlDbType.Binary);
                param[14] = new SqlParameter("@fixas_createdBy", createdBy);
                param[15] = new SqlParameter("@fixas_modifiedBy", modifiedBy);
                param[16] = new SqlParameter("@fixas_phototype", ImgType);
                param[17] = new SqlParameter("@flag", flag);
                param[18] = new SqlParameter("@discount", discount);
                param[19] = new SqlParameter("@typeDetail", TypeDetail);
                param[20] = new SqlParameter("@maintainPeriod", maintainPeriod);
                param[21] = new SqlParameter("@fixas_photopath", photoPath);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, param));
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int SaveAndModifyAssetIncrement(int AssetID,string AssetIncNo,int IncrementEntity,string IncrementVoucher, string IncrementDate,
                                                      decimal IncrementCost, string IncrementSupplier, string IncrementComment, string IncrementName, int AssetIncrementID, int createdBy, int modifiedBy, string discount)
        {
            try
            {
                string strName = "sp_Fix_SaveAndModifyAssetIncrement";
                SqlParameter[] param = new SqlParameter[13];
                param[0] = new SqlParameter("@fixas_inc_id", AssetIncrementID);
                param[1] = new SqlParameter("@fixas_inc_code", AssetIncNo);
                param[2] = new SqlParameter("@fixas_inc_entity", IncrementEntity);
                param[3] = new SqlParameter("@fixas_inc_voucher", IncrementVoucher);
                param[4] = new SqlParameter("@fixas_inc_date", IncrementDate);
                param[5] = new SqlParameter("@fixas_inc_cost", IncrementCost);
                param[6] = new SqlParameter("@fixas_inc_supplier", IncrementSupplier);
                param[7] = new SqlParameter("@fixas_inc_comment", IncrementComment);
                param[8] = new SqlParameter("@fixas_inc_createdBy", createdBy);
                param[9] = new SqlParameter("@fixas_id", AssetID);
                param[10] = new SqlParameter("@fixas_inc_modifiedBy", modifiedBy);
                param[11] = new SqlParameter("@fixas_inc_name", IncrementName);
                param[12] = new SqlParameter("@discount", discount);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, param));
            }
            catch
            {
                return -1;
            }
        }




        public static int SaveAndModifiyAssetDetail(string fixid, string StartDate, int DetailEntity, int DetailCenter, int DetailStatus,
                                                    string DetailSite, string DetailComment, string Responsibler, int ParentID, int createdBy,string line)
        {
            try
            {
                string strName = "sp_Fix_SaveAndModifyAssetDetail";
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@fixas_det_id", fixid);
                param[1] = new SqlParameter("@fixas_det_parent", ParentID);
                param[2] = new SqlParameter("@fixas_det_startDate", StartDate);
                param[3] = new SqlParameter("@fixas_det_entity", DetailEntity);
                param[4] = new SqlParameter("@fixas_det_cosCenter", DetailCenter);
                param[5] = new SqlParameter("@fixas_det_status", DetailStatus);
                param[6] = new SqlParameter("@fixas_det_site", DetailSite);
                param[7] = new SqlParameter("@fixas_det_responsibler", Responsibler);
                param[8] = new SqlParameter("@fixas_det_comment", DetailComment);
                param[9] = new SqlParameter("@fixas_det_createdBy", createdBy);
                param[10] = new SqlParameter("@line", line);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, param));
            }
            catch
            {
                return -1;
            }
        }
       

    }
}
