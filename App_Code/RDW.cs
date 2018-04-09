using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace RD_WorkFlow
{
    /// <summary>
    /// Summary description for RDW
    /// </summary>
    public class RDW
    {
        string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
        String strSupp = ConfigurationManager.AppSettings["SqlConn.TCPC_Supplier"];

        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public RDW()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //绑定项目类型
        public DataSet SelectProjType()
        {
            try
            {
                string sql = "select RDW_typeID,RDW_typeName from RD_Workflow.dbo.RDW_Type";

                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql);
            }
            catch
            {
                return null;
            }
         }
            
        /// <summary>
        /// 获得RDW清单
        /// </summary>
        /// <param name="strProj"></param>
        /// <param name="strProd"></param>
        /// <param name="strStart"></param>
        /// <param name="strStatus"></param>
        /// <param name="strUID"></param>
        /// <param name="ViewAll"></param>
        /// <returns>返回RDW清单对象列表</returns>
        public IList<RDW_Header> SelectRDWList(string cateid, string strProj, string strProd, string strSku, string strStart, string strMessage, string strStatus, string strUID, string keyword, string region, string type, bool ViewAll, string LampType)
        {
            //try
            //{
            string strSql = "sp_RDW_SelectRdwHeaderList";
            SqlParameter[] sqlParam = new SqlParameter[13];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@prod", strProd);   
            sqlParam[2] = new SqlParameter("@sku", strSku);
            sqlParam[3] = new SqlParameter("@start", strStart);
            sqlParam[4] = new SqlParameter("@msg", strMessage);
            sqlParam[5] = new SqlParameter("@status", strStatus);
            sqlParam[6] = new SqlParameter("@uid", strUID);
            sqlParam[7] = new SqlParameter("@all", ViewAll == true ? 1 : 0);
            sqlParam[8] = new SqlParameter("@cateid", cateid);
            sqlParam[9] = new SqlParameter("@keyword", keyword);
            sqlParam[10] = new SqlParameter("@region", region);
            sqlParam[11] = new SqlParameter("@type", type);
            sqlParam[12] = new SqlParameter("@LampType", LampType);
            IList<RDW_Header> RDWHeader = new List<RDW_Header>();
            IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            
            while (reader.Read())
            {
                RDW_Header rh = new RDW_Header();
                rh.RDW_Project = reader["Project"].ToString();
                rh.RDW_ProdCode = reader["ProdCode"].ToString();
                rh.RDW_ProdSKU = reader["ProdSku"].ToString();
                rh.RDW_ProdDesc = reader["ProdDesc"].ToString();
                rh.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StartDate"]));
                rh.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["EndDate"]));
                rh.RDW_Status = reader["Status"].ToString();
                rh.RDW_Stage = reader["stage"].ToString();
                rh.RDW_Creater = reader["Creater"].ToString();
                rh.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                rh.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                rh.RDW_Memo = reader["Memo"].ToString();
                rh.RDW_Partner = reader["Partner"].ToString();
                rh.RDW_Standard = reader["Standard"].ToString();
                rh.RDW_MstrID = Convert.ToInt32(reader["ID"]);
                rh.RDW_OldID = Convert.ToInt32(reader["OldID"]);
                rh.RDW_Type = reader["Type"].ToString();
                rh.RDW_TypeName = reader["TypeName"].ToString();
                rh.RDW_EcnCode = reader["ecnCode"].ToString();
                rh.RDW_PPA = reader["ppa"].ToString();
                rh.RDW_Priority = reader["priority"].ToString();
                rh.RDW_LampType = reader["RDW_LampType"].ToString();
                if (reader["RDW_EStarDLC"].ToString() == "--")
                    rh.RDW_EStarDLC = "";
                else
                    rh.RDW_EStarDLC = reader["RDW_EStarDLC"].ToString();
                if (reader["FinishDate"] == DBNull.Value)
                {
                    rh.RDW_FinishDate = "";
                }
                else
                {
                    rh.RDW_FinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["FinishDate"]));
                }
                rh.RDW_Category = reader["cate_name"].ToString();
                rh.RDW_Remark = reader["RDW_Remark"].ToString();
                rh.RDW_PPAMstrid = reader["RDW_PPAMstrID"].ToString();
                //if (rh.RDW_FinishDate == "1900-01-01")
                //{
                //    rh.RDW_FinishDate = "";
                //}
                RDWHeader.Add(rh);
            }
            reader.Close();
            return RDWHeader;
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return null;
            //}
        }

        public DataSet GetTemplateByType(int type)
        {
            try
            {
                string sql = "sp_RDW_SelectTemplateByType";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@type", type);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, sqlParam);
            }
            catch
            {
                return null;
            }

        }
        public DataSet GetTemplateByType(int type, int category)
        {
            try
            {
                string sql = "sp_RDW_SelectTemplateByType";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@type", type);
                sqlParam[1] = new SqlParameter("@category", category);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, sqlParam);
            }
            catch
            {
                return null;
            }

        }
        public bool IsProject(string oldprodName, string oldprodCode)
        {
            try
            {
                string sql = @"select *  from RDW_Mstr  where RDW_Project = '" + oldprodName + "' and RDW_ProdCode = '" + oldprodCode + "'";

                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql).Tables[0];
                string ecnCode;
                if (dt.Rows.Count > 0)
                    return true;
                else
                    ecnCode = oldprodCode + "-1";
                return false;
            }
            catch
            {
                return false;
            }
        }

        //通过老的code算出新的ecn的code
        public string GetNewProdEcnCode(string oldprodName, string oldprodCode)
        {
            try
            {
                string sql = "sp_RDW_GetNewProdCode";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@oldprodName", oldprodName);
                sqlParam[1] = new SqlParameter("@oldprodCode", oldprodCode);

                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql ,sqlParam).Tables[0];
                string ecnCode="";
                if(dt.Rows.Count > 0)
                    ecnCode = dt.Rows[0][0].ToString() + "-" + (Convert.ToInt32( dt.Rows[0][1].ToString() ) + 1).ToString();

                return ecnCode;
            }
            catch
            {
                return "";
            }
        }
        //找出oldProd 的ID
        public string GetOldProdID( string oldprodName, string oldprodCode )
        {
            try
            {
                string sql = @"select RDW_MstrID from RDW_Mstr Where RDW_Project = '" + oldprodName + "' and RDW_ProdCode = '" + oldprodCode + "'";

                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql).Tables[0];

                return dt.Rows[0][0].ToString();
            }
            catch
            {
                return "0";
            }
        }

        public string GetOldCategory(string oldprodName, string oldprodCode)
        {
            try
            {
                string sql = @"select RDW_Category from RDW_Mstr Where RDW_Project = '" + oldprodName + "' and RDW_ProdCode = '" + oldprodCode + "'";

                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql).Tables[0];

                return dt.Rows[0][0].ToString();
            }
            catch
            {
                return "0";
            }
        }
        /// <summary>
        /// 删除已有信息
        /// </summary>
        /// <param name="strMstrID"></param>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteRDWHeader(string strMstrID)
        {
            try
            {
                string strSql = "sp_RDW_DeleteRDWHeader";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strMstrID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <param name="strMID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckDeleteRDWHeader(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_DeleteRDWHeaderCheck";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 新增RDW
        /// </summary>
        /// <param name="rm"></param>
        /// <returns>ID</returns>
        public int InsertRDWHeader(RDW_Header rh)
        {
            try
            {
                string strSql = "sp_RDW_InsertRDWHeader";

                SqlParameter[] sqlParam = new SqlParameter[21];
                sqlParam[0] = new SqlParameter("@proj", rh.RDW_Project.Trim());
                sqlParam[1] = new SqlParameter("@code", rh.RDW_ProdCode.Trim());
                sqlParam[2] = new SqlParameter("@desc", rh.RDW_ProdDesc.Trim());
                sqlParam[3] = new SqlParameter("@sku", rh.RDW_ProdSKU.Trim());
                sqlParam[4] = new SqlParameter("@stand", rh.RDW_Standard.Trim());
                sqlParam[5] = new SqlParameter("@start", rh.RDW_StartDate.Trim());
                sqlParam[6] = new SqlParameter("@end", rh.RDW_EndDate.Trim());
                sqlParam[7] = new SqlParameter("@memo", rh.RDW_Memo.Trim());
                sqlParam[8] = new SqlParameter("@template", rh.RDW_Template);
                sqlParam[9] = new SqlParameter("@uid", rh.RDW_CreatedBy.ToString());
                sqlParam[10] = new SqlParameter("@cate", rh.RDW_Category);
                sqlParam[11] = new SqlParameter("@oldID", rh.RDW_OldID);
                sqlParam[12] = new SqlParameter("@type", rh.RDW_Type);
                sqlParam[13] = new SqlParameter("@ecnCode", rh.RDW_EcnCode);                
                sqlParam[14] = new SqlParameter("@customer", rh.RDW_Customer);
                sqlParam[15] = new SqlParameter("@engineerteam", rh.RDW_EngineerTeam);
                sqlParam[16] = new SqlParameter("@lamptype", rh.RDW_LampType);
                sqlParam[17] = new SqlParameter("@priority", rh.RDW_Priority);
                sqlParam[18] = new SqlParameter("@Tier", rh.RDW_Tier);
                sqlParam[19] = new SqlParameter("@EStarDLC", rh.RDW_EStarDLC);
                sqlParam[20] = new SqlParameter("@PPA", rh.RDW_PPA);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }
        public int IsPPAEnabled(string uID,int mID)
        {
            try
            {
                string strSql = "sp_rdw_IsPPAEnabled";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@uID", uID);
                sqlParam[1] = new SqlParameter("@mID", mID);
                sqlParam[2] = new SqlParameter("@retValue", DbType.Int32);
                sqlParam[2].Direction = System.Data.ParameterDirection.Output;

                SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam);

                return Convert.ToInt32(sqlParam[2].Value);
            }
            catch
            {
                return 0;
            }
        }
        public DataSet SelectOldProject(int oldProjectID , int newProjectID)
        {
            string strSql = "sp_RDW_SelectOldRdwHeader";
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@oldid", oldProjectID);
                sqlParam[1] = new SqlParameter("@newid", newProjectID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                return null;
            }
            

        }

        public string IsEnabledEcnCode( string ecnCode)
        {
            string sql = "sp_RDW_IsenabledECN";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@ecnCode", ecnCode);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Int32);
            sqlParam[1].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, sqlParam);

            return sqlParam[1].Value.ToString();
        }
        /// <summary>
        /// 获得RDW抬头
        /// </summary>
        /// <param name="strID"></param>
        /// <returns>返回RDW抬头对象列表</returns>
        public RDW_Header SelectRDWHeader(string strID)
        {
            //try
            //{
            string strSql = "sp_RDW_SelectRdwHeader";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", strID);

            RDW_Header rh = new RDW_Header();
            IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                rh.RDW_Project = reader["Project"].ToString();
                rh.RDW_ProdCode = reader["ProdCode"].ToString();
                rh.RDW_ProdSKU = reader["ProdSku"].ToString();
                rh.RDW_ProdDesc = reader["ProdDesc"].ToString();
                rh.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StartDate"]));
                rh.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["EndDate"]));
                rh.RDW_Status = reader["Status"].ToString();
                rh.RDW_Memo = reader["Memo"].ToString();
                rh.RDW_Standard = reader["Standard"].ToString();
                rh.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                rh.RDW_PartnerName = "Project Viewer:" + reader["PartnerName"].ToString();
                rh.RDW_Partner = reader["Partner"].ToString();
                rh.RDW_PMID = reader["PMID"].ToString();
                rh.RDW_PM = reader["PM"].ToString();
                rh.RDW_Category = reader["RDW_Category"].ToString();
                rh.RDW_MGR = reader["RDW_MgrName"].ToString();
                rh.RDW_EcnCode = reader["RDW_ecnCode"].ToString();
                rh.RDW_Type = reader["Type"].ToString();
                rh.RDW_TypeName = reader["TypeName"].ToString();
                rh.RDW_Tier = reader["Tier"].ToString();
                rh.RDW_LampType = reader["LampType"].ToString();
                rh.RDW_Priority = reader["Priority"].ToString();
                rh.RDW_EngineerTeam = reader["EngineerTeam"].ToString();
                rh.RDW_Customer = reader["Customer"].ToString();
                rh.RDW_EStarDLC = reader["EStarDLC"].ToString();
                rh.RDW_PPA = reader["ppa"].ToString();
                rh.RDW_PPAMstrid = reader["RDW_PPAMstrID"].ToString();
            }
            reader.Close();
            return rh;
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return null;
            //}
        }

        /// <summary>
        /// 更新RDW抬头
        /// </summary>
        /// <param name="rh"></param>
        /// <returns>返回是否更新成功</returns>
        public bool UpdateRDWHeader(RDW_Header rh)
        {
            try
            {
                string strSql = "sp_RDW_UpdateRDWHeader";

                SqlParameter[] sqlParam = new SqlParameter[18];
                sqlParam[0] = new SqlParameter("@mid", rh.RDW_MstrID.ToString());
                sqlParam[1] = new SqlParameter("@code", rh.RDW_ProdCode.Trim());
                sqlParam[2] = new SqlParameter("@desc", rh.RDW_ProdDesc.Trim());
                sqlParam[3] = new SqlParameter("@sku", rh.RDW_ProdSKU.Trim());
                sqlParam[4] = new SqlParameter("@memo", rh.RDW_Memo.Trim());
                sqlParam[5] = new SqlParameter("@start", rh.RDW_StartDate.Trim());
                sqlParam[6] = new SqlParameter("@end", rh.RDW_EndDate.Trim());
                sqlParam[7] = new SqlParameter("@stand", rh.RDW_Standard.Trim());
                sqlParam[8] = new SqlParameter("@proj", rh.RDW_Project.Trim());
                sqlParam[9] = new SqlParameter("@cate", rh.RDW_Category.Trim());
                sqlParam[10] = new SqlParameter("@status", rh.RDW_Status.Trim());
                sqlParam[11] = new SqlParameter("@customer", rh.RDW_Customer);
                sqlParam[12] = new SqlParameter("@engineerteam", rh.RDW_EngineerTeam);
                sqlParam[13] = new SqlParameter("@lamptype", rh.RDW_LampType);
                sqlParam[14] = new SqlParameter("@priority", rh.RDW_Priority);
                sqlParam[15] = new SqlParameter("@Tier", rh.RDW_Tier.Trim());
                sqlParam[16] = new SqlParameter("@EStarDLC", rh.RDW_EStarDLC.Trim());
                sqlParam[17] = new SqlParameter("@PPA", rh.RDW_PPA.Trim());

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得清单明细
        /// </summary>
        /// <param name="strID"></param>
        /// <returns>返回明细对象列表</returns>
        public IList<RDW_Detail> SelectRDWDetailList(string strID,bool isClosed=false)
        {
            string strEvaluator = string.Empty;
            string strEvaluatorName = string.Empty;
            string strPartner = string.Empty;
            string strPartnerName = string.Empty;

            string strSql = "sp_RDW_SelectRdwDetailList";
            if (isClosed)
            {
                strSql = "sp_RDW_SelectClosedRdwDetailList";
            }
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@mid", strID);

            IList<RDW_Detail> RDWDetail = new List<RDW_Detail>();
            IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                RDW_Detail rd = new RDW_Detail();
                rd.RDW_MstrID = Convert.ToInt32(reader["MstrID"]);
                rd.RDW_DetID = Convert.ToInt32(reader["DetID"]);
                rd.RDW_StepName = reader["StepName"].ToString();
                rd.RDW_StepDesc = reader["StepDesc"].ToString();
                rd.RDW_StepNo = reader["Step"].ToString();
                rd.RDW_Sort = Convert.ToInt32(reader["sort"].ToString());
                rd.RDW_isActive = Convert.ToBoolean(reader["isActive"]);
                rd.RDW_isTemp = Convert.ToBoolean(reader["RDW_isTemp"].ToString());
                rd.RDW_Duration = Convert.ToInt32(reader["Duration"].ToString());
                rd.RDW_Predecessor = reader["Predecessor"].ToString();
                rd.RDW_Status = Convert.ToInt32(reader["Status"]);
                rd.RDW_Creater = reader["Creater"].ToString();
                rd.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                rd.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                rd.RDW_StepStartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepStartDate"]));
                rd.RDW_StepEndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepEndDate"]));
                rd.RDW_StepFinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepFinishDate"]));
                rd.RDW_ParentDetID = Convert.ToInt32(reader["RDW_ParentID"].ToString());
                rd.RDW_PredtaskID = reader["RDW_PredtaskID"].ToString();
                rd.RDW_Extra = (bool)reader["RDW_extraStep"];
                rd.RDW_StepActEndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepActEndDate"]));
                //string sqlStr = "sp_RDW_SelectPredecessor";
                //sqlParam[0] = new SqlParameter("@StepPreId", rd.RDW_PredecessorId);
                //rd.RDW_PredecessorName = (string)SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlStr
                //    , sqlParam);

                if (rd.RDW_StepStartDate == "1900-01-01")
                {
                    rd.RDW_StepStartDate = "";
                }
                if (rd.RDW_StepEndDate == "1900-01-01")
                {
                    rd.RDW_StepEndDate = "";
                }
                if (rd.RDW_StepFinishDate == "1900-01-01")
                {
                    rd.RDW_StepFinishDate = "";
                }

                if (rd.RDW_ParentDetID == 0)
                {
                    strSql = "sp_RDW_SelectRdwDetailEvaluater";
                    sqlParam[0] = new SqlParameter("@did", rd.RDW_DetID.ToString());

                    strEvaluator = "";
                    strEvaluatorName = "";
                    SqlDataReader readerEval = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                    while (readerEval.Read())
                    {
                        strEvaluator += readerEval["EvaluateID"].ToString() + ";";
                        //readerEval["Result"].ToString() != "0"
                        if (Convert.ToBoolean(readerEval["Result"]) == false)
                        {
                            strEvaluatorName += "*" + readerEval["Evaluater"].ToString() + "<br>";
                        }
                        else
                            strEvaluatorName += readerEval["Evaluater"].ToString() + "<br>";


                    }
                    readerEval.Close();

                    if (strEvaluator.Length > 0)
                    {
                        rd.RDW_Evaluater = strEvaluatorName.Substring(0, strEvaluatorName.Length - 4);
                        rd.RDW_EvaluaterID = ";" + strEvaluator;
                    }
                    else
                    {
                        rd.RDW_Evaluater = strEvaluatorName;
                        rd.RDW_EvaluaterID = strEvaluator;
                    }
                }
                #region 当子步骤无审核人，大步骤的审批者即为子步骤的审核者wangcaixia 20140221
                if (rd.RDW_ParentDetID != 0)
                {
                    strSql = "sp_RDW_SelectRdwDetailEvaluater";
                    sqlParam[0] = new SqlParameter("@did", rd.RDW_ParentDetID.ToString());

                    strEvaluator = "";
                    strEvaluatorName = "";
                    SqlDataReader readerEval = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                    while (readerEval.Read())
                    {
                        strEvaluator += readerEval["EvaluateID"].ToString() + ";";
                        strEvaluatorName += readerEval["Evaluater"].ToString() + "<br>";
                    }
                    readerEval.Close();

                    if (strEvaluator.Length > 0)
                    {
                        rd.RDW_Evaluater = strEvaluatorName.Substring(0, strEvaluatorName.Length - 4);
                        rd.RDW_EvaluaterID = ";" + strEvaluator;
                    }
                    else
                    {
                        rd.RDW_Evaluater = strEvaluatorName;
                        rd.RDW_EvaluaterID = strEvaluator;
                    }
                }
                #endregion

                strSql = "sp_RDW_SelectRdwDetailPartner";
                sqlParam[0] = new SqlParameter("@did", rd.RDW_DetID.ToString());

                strPartner = "";
                strPartnerName = "";
                SqlDataReader readerPartner = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (readerPartner.Read())
                {
                    strPartner += readerPartner["Partner"].ToString() + ";";

                    if (Convert.ToBoolean(readerPartner["RDW_Result"]) == false)
                    {
                        strPartnerName += "*" + readerPartner["PartnerName"].ToString() + "<BR>";
                    }
                    else
                        strPartnerName += readerPartner["PartnerName"].ToString() + "<BR>";
                }
                readerPartner.Close();

                if (strPartner.Length > 0)
                {
                    rd.RDW_Partner = ";" + strPartner;
                    rd.RDW_PartnerName = strPartnerName.Substring(0, strPartnerName.Length - 4);
                }
                else
                {
                    rd.RDW_Partner = strPartner;
                    rd.RDW_PartnerName = strPartnerName;
                }

                RDWDetail.Add(rd);
            }
            reader.Close();
            return RDWDetail;
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return null;
            //}
        }

        /// <summary>
        /// 判断清单明细中是否有已完成审批步骤
        /// </summary>
        /// <param name="strID"></param>
        /// <returns>返回明细对象列表</returns>
        public DataTable CheckRDWDetIsCanUpdateDate(string strID, bool isClosed = false)
        {
            string strEvaluator = string.Empty;
            string strEvaluatorName = string.Empty;
            string strPartner = string.Empty;
            string strPartnerName = string.Empty;

            string strSql = "sp_RDW_CheckRdwDetailListUpdateDate";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@mid", strID);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
          
        }

        public int HasPPA(string ppaID)
        {
            try
            {
                string sql = "Select * From ppa_docs d left join ppa_mstr m On d.ppa_mstrId = m.ppa_mstrId Where m.ppa_mstrId = '" + ppaID + "'";

                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql).Tables[0].Rows.Count;
            }
            catch
            {
                return 0;
            }
            
        }
        public bool IsOldOrECNProject(string mID)
        {
            try
            {
                string sql = "Select RDW_Code From RDW_Det Where RDW_MstrID =  " + mID + " Order By RDW_Sort";
                string sql2 = "select isnull(RDW_oldProjectID,'0') from RDW_Mstr Where RDW_MstrID =  " + mID;
                if (SqlHelper.ExecuteScalar(strConn, CommandType.Text, sql) == DBNull.Value || Convert.ToInt32( SqlHelper.ExecuteScalar(strConn, CommandType.Text, sql2) ) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除已有步骤
        /// </summary>
        /// <param name="strDetID"></param>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteRDWDetail(string strMsrID, string strDetID)
        {
            try
            {
                //string strSql = "sp_RDW_DeleteRDWDetail";
                string strSql = "sp_RDW_DeleteSteps";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@mid", strMsrID);
                sqlParam[1] = new SqlParameter("@id", strDetID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <param name="strDetID"></param>
        /// <returns></returns>
        public bool CheckDeleteRDWDetail(string strDetID, out bool canAddSubStep)
        {
            canAddSubStep = false;
            try
            {
                string strSql = "sp_RDW_DeleteRDWDetailCheck";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDetID);
                sqlParam[1] = new SqlParameter("@canAddSubStep", System.Data.SqlDbType.Bit, 1);
                sqlParam[1].Direction = System.Data.ParameterDirection.Output;
                bool canDel = Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
                canAddSubStep = (bool)sqlParam[1].Value;
                return canDel;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以查看明细
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckViewRDWDetail(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_ViewRDWDetailCheck";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以查看明细
        /// </summary>
        /// <param name="strMID"></param>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckViewRDWDetailEdit(string strMID, string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_ViewRDWDetailEditCheck";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@did", strDID);
                sqlParam[2] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以评审明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckEvaluateRDWDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_EvaluateRDWDetailCheck";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以取消评审明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckCancelEvaluateRDWDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_CancelEvaluateRDWDetailCheck";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得可操作明细最大序号
        /// </summary>
        /// <param name="strMID"></param>
        /// <returns>返回最大序号</returns>
        public int SelectDetailCurrent(string strMID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDetailCurrent";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strMID);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -999;
            }
        }
        /// <summary>
        /// 获得延期信息
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public DataTable SelectRDWDelay(string strDID)
        {
            try
            {
                 string strSql = "sp_RDW_selectdelay";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        public SqlDataReader SelectRDWDelay(string strDID,string id)
        {
            try
            {
                string strSql = "sp_RDW_selectdelay";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@id", id);
                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception)
            {

                return null;
            }
        }
        public SqlDataReader SelectRDWDelay(string strDID, int sut)
        {
            try
            {
                string strSql = "sp_RDW_selectdelay";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@id", "0");
                sqlParam[2] = new SqlParameter("@stu", sut.ToString());
                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 新增延期信息
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int InsertRDWDelay(string strDID,string name,string time,string remark,string id,string uname)
        {
            try
            {
                string strSql = "sp_RDW_insertdelay";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@StepName", name);
                sqlParam[2] = new SqlParameter("@delaytime", time);
                sqlParam[3] = new SqlParameter("@delayrmk", remark);
                sqlParam[4] = new SqlParameter("@createby", id);
                sqlParam[5] = new SqlParameter("@createname", uname);
                sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToInt32(sqlParam[6].Value);
            }
            catch (Exception)
            {

                return -1;
            }
        }
        /// <summary>
        /// 删除延期信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteRDWDelay(string id)
        {
            try
            {
                string strSql = "sp_RDW_deletedelay";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@detID", id);
          
                sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToBoolean(sqlParam[1].Value);
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// 更改延期信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int UpdateRDWDelay(string id,string date,string remark,string uid,string uname)
        {
            try
            {
                string strSql = "sp_RDW_updatedelay";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@time", date);
                sqlParam[2] = new SqlParameter("@rmk", remark);
                sqlParam[3] = new SqlParameter("@modifyby", uid);
                sqlParam[4] = new SqlParameter("@modifyname", uname);
                sqlParam[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[5].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToInt32(sqlParam[5].Value);
            }
            catch (Exception)
            {

                return -1;
            }
        }
        /// <summary>
        /// 获得评审明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns>返回评审明细对象列表</returns>
        public RDW_Detail SelectRDWDetailEdit(string strDID,bool isClosed=false)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDetailEdit";
                if (isClosed)
                {
                    strSql = "sp_RDW_SelectClosedRdwDetailEdit";
                }
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                RDW_Detail rd = new RDW_Detail();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    rd.RDW_Project = reader["Project"].ToString();
                    rd.RDW_StepName = reader["StepName"].ToString();
                    rd.RDW_StepDesc = reader["StepDesc"].ToString().Replace("<br>", "\n").Replace("\n\n", "\n");
                    rd.RDW_ProdCode = reader["ProdCode"].ToString();
                    rd.RDW_ProdDesc = reader["ProdDesc"].ToString();
                    rd.RDW_Status = Convert.ToInt32(reader["Status"]);
                    rd.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", reader["StartDate"]);
                    rd.RDW_StepStartDate = string.Format("{0:yyyy-MM-dd}", reader["StepStartDate"]);
                    rd.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", reader["EndDate"]);
                    rd.RDW_StepEndDate = string.Format("{0:yyyy-MM-dd}", reader["StepEndDate"]);
                    rd.RDW_Sort = (int)reader["Sort"];
                    rd.RDW_Extra = (bool)reader["RDW_extraStep"];

                    if (isClosed)
                    {
                        rd.RDW_StepTitle = reader["RDW_StepTitle"].ToString();
                    }
                   
                    //Add By Shanzm 2015-05-17: 决定该步骤是否应显示Tracking按钮
                    rd.RDW_needTracking = Convert.ToBoolean(reader["RDW_needTracking"]);
                    if (rd.RDW_StartDate == "1900-01-01")
                       {
                        rd.RDW_StartDate = "";
                    }
                    if (rd.RDW_StepStartDate == "1900-01-01")
                    {
                        rd.RDW_StepStartDate = "";
                    }
                    if (rd.RDW_EndDate == "1900-01-01")
                    {
                        rd.RDW_EndDate = "";
                    }
                    if (rd.RDW_StepEndDate == "1900-01-01")
                    {
                        rd.RDW_StepEndDate = "";
                    }
                }
                reader.Close();
                return rd;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 保存评审
        /// </summary>
        /// <param name="strMID"></param>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <param name="strNotes"></param>
        /// <param name="strPass"></param>
        /// <returns>返回是否保存成功</returns>
        public bool UpdateEvaluateRDWDetail(string strMID, string strDID, string strUID, string strNotes, bool isPass, string strUName)
        {
            try
            {
                string strSql = "sp_RDW_UpdateEvaluateRDWDetail";

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@did", strDID);
                sqlParam[2] = new SqlParameter("@uid", strUID);
                sqlParam[3] = new SqlParameter("@notes", strNotes);
                sqlParam[4] = new SqlParameter("@isPass", isPass);
                sqlParam[5] = new SqlParameter("@uName", strUName);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="byteFile"></param>
        /// <param name="strType"></param>
        /// <param name="strUID"></param>
        /// <returns>返回是否上传成功</returns>
        public bool UploadFile(string fileName, string newFileName, string uID, string uName, string stepID)
        {
            try
            {
                string strSql = "sp_RDW_UploadFile";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@fileName", fileName);
                sqlParam[1] = new SqlParameter("@newFileName", newFileName);
                sqlParam[2] = new SqlParameter("@uID", uID);
                sqlParam[3] = new SqlParameter("@uName", uName);
                sqlParam[4] = new SqlParameter("@stepID", stepID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        ///<summary>
        ///UpLoad Template File
        ///return success or failed
        ///</summary>
        public bool UploadTemplateFile(string fileName, string newFileName, string uID, string uName, string cateID,string docPath)
        {
            try
            {
                string strSql = "sp_RDW_UploadTemplateFile";

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@fileName", fileName);
                sqlParam[1] = new SqlParameter("@newFileName", newFileName);
                sqlParam[2] = new SqlParameter("@uID", uID);
                sqlParam[3] = new SqlParameter("@uName", uName);
                sqlParam[4] = new SqlParameter("@cateID", cateID);
                sqlParam[5] = new SqlParameter("@cate_docPath", docPath);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public bool UpdateCategoryTemplate(int cateID, int templateID)
        {
            string strSql = "sp_RDW_UpdateCategoryTemplate";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@cateID", cateID);
            sqlParam[1] = new SqlParameter("@templateID", templateID);

            if (SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam) == 1)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 获得上传文件清单
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns>返回上传文件对象列表</returns>
        public IList<RDW_Detail_Docs> SelectRDWDetailDocs(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDetailDocs";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                IList<RDW_Detail_Docs> DetailDocs = new List<RDW_Detail_Docs>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RDW_Detail_Docs rdd = new RDW_Detail_Docs();
                    rdd.RDW_DocsID = Convert.ToInt32(reader["DocsID"]);
                    rdd.RDW_DetID = Convert.ToInt32(reader["DetID"]);
                    rdd.RDW_FileName = reader["FileName"].ToString();
                    rdd.RDW_PhysicalName = reader["PhysicalName"].ToString();
                    rdd.RDW_FileType = reader["FileType"].ToString();
                    rdd.RDW_Uploader = reader["Uploader"].ToString();
                    rdd.RDW_UploaderID = reader["UploaderID"].ToString();
                    rdd.RDW_TransferStatus = Convert.ToBoolean(reader["RDW_transferStatus"]);
                    rdd.RDW_UploadDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["UploadDate"]));
                    rdd.RDW_Path = reader["RDW_Path"].ToString();
                    rdd.RDW_fromDocID = reader["RDW_fromDocID"].ToString();
                    rdd.RDW_isDelete = Convert.ToBoolean(reader["RDW_isDelete"]);
                    DetailDocs.Add(rdd);
                }
                reader.Close();
                return DetailDocs;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得明细消息
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns>返回明细消息对象列表</returns>
        public IList<RDW_Detail> SelectRDWDetailMessage(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDetailMessage";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                IList<RDW_Detail> RDWDetail = new List<RDW_Detail>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RDW_Detail rd = new RDW_Detail();
                    rd.RDW_Message = reader["Message"].ToString();
                    RDWDetail.Add(rd);
                }
                reader.Close();
                return RDWDetail;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        public DataTable SelectProdMassage(string no)
        {
            string str = "sp_prod_SelectProdMassage";
            SqlParameter param = new SqlParameter("@no",no);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, str, param).Tables[0];
        }

        /// <summary> 
        /// 获得整个项目上传文件清单 
        /// </summary> 
        /// <param name="strDID"></param> 
        /// <param name="strUID"></param> 
        /// <returns>返回上传文件对象和消息对象列表</returns> 
        public IList<RDW_Detail_All> SelectRDWMessageAndDocs(string strMID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwMessageDocs";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strMID);

                IList<RDW_Detail_All> DetailAll = new List<RDW_Detail_All>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RDW_Detail_All rda = new RDW_Detail_All();
                    if (reader["DocsID"] != DBNull.Value)
                    {
                        rda.RDW_DocsID = Convert.ToInt32(reader["DocsID"]);
                        rda.RDW_FileName = reader["FileName"].ToString();
                        rda.RDW_PhysicalName = reader["RDW_PhysicalName"].ToString();
                        rda.RDW_FileType = reader["FileType"].ToString();
                        rda.RDW_Uploader = reader["Uploader"].ToString();
                        rda.RDW_UploaderID = reader["UploaderID"].ToString();
                        rda.RDW_UploadDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["UploadDate"]));
                        rda.RDW_Path = reader["RDW_path"].ToString();
                        rda.RDW_Message = reader["Message"].ToString();
                        rda.RDW_MstrID = Convert.ToInt32(reader["RDW_MstrID"]);
                    }

                    else
                    {
                        rda.RDW_DocsID = 0;
                        rda.RDW_FileName = "";
                        rda.RDW_PhysicalName = "";
                        rda.RDW_FileType = "";
                        rda.RDW_Uploader = "";
                        rda.RDW_UploaderID = "";
                        rda.RDW_UploadDate = string.Empty;
                        rda.RDW_Path = "";
                        rda.RDW_Message = "";
                    }

                    if (reader["DetID"] != DBNull.Value)
                        rda.RDW_DetID = Convert.ToInt32(reader["DetID"]);
                    else
                        rda.RDW_DetID = 0;
                    rda.RDW_StepName = reader["stepname"].ToString();
                    rda.RDW_StepNo = reader["TaskId"].ToString();

                    DetailAll.Add(rda);
                }
                reader.Close();
                return DetailAll;
            }
            catch (Exception ex)
            {
                //throw ex; 
                return null;
            }
        }

        /// <summary>
        /// 获得整个项目上传文件清单
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns>返回上传文件对象列表</returns>
        public IList<RDW_Detail_Docs> SelectRDWDocs(string strMID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDocs";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strMID);

                IList<RDW_Detail_Docs> DetailDocs = new List<RDW_Detail_Docs>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RDW_Detail_Docs rdd = new RDW_Detail_Docs();
                    rdd.RDW_DocsID = Convert.ToInt32(reader["DocsID"]);
                    rdd.RDW_DetID = Convert.ToInt32(reader["DetID"]);
                    rdd.RDW_StepName = reader["stepname"].ToString();
                    rdd.RDW_StepNo = reader["TaskId"].ToString();
                    rdd.RDW_FileName = reader["FileName"].ToString();
                    rdd.RDW_PhysicalName = reader["RDW_PhysicalName"].ToString();
                    rdd.RDW_FileType = reader["FileType"].ToString();
                    rdd.RDW_Uploader = reader["Uploader"].ToString();
                    rdd.RDW_UploaderID = reader["UploaderID"].ToString();
                    rdd.RDW_UploadDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["UploadDate"]));
                    rdd.RDW_Path = reader["RDW_path"].ToString();
                    DetailDocs.Add(rdd);
                }
                reader.Close();
                return DetailDocs;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        /// <summary>
        /// 获得整个项目消息
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns>返回项目消息对象列表</returns>
        public IList<RDW_Detail> SelectRDWMessage(string strMID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwMessage";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strMID);

                IList<RDW_Detail> RDWDetail = new List<RDW_Detail>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RDW_Detail rd = new RDW_Detail();
                    rd.RDW_Message = reader["Message"].ToString();
                    rd.RDW_StepName = reader["StepName"].ToString();
                    rd.RDW_DetID = Convert.ToInt32(reader["RDW_DetID"]);

                    RDWDetail.Add(rd);
                }
                reader.Close();
                return RDWDetail;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        /// <summary>
        /// 保存明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strNotes"></param>
        /// <param name="strUID"></param>
        /// <returns>返回是否保存成功</returns>
        public bool SaveDetailEdit(string strDID, string strNotes, string strUID, string strUName)
        {
            try
            {
                string strSql = "sp_RDW_SaveDetailEdit";

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@notes", strNotes);
                sqlParam[2] = new SqlParameter("@uid", strUID);
                sqlParam[3] = new SqlParameter("@uName", strUName);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获取指定文档
        /// </summary>
        /// <param name="strDocID"></param>
        /// <returns></returns>
        public SqlDataReader SelectRDWDetailDocView(string strDocID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwDetailDocView";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@docid", strDocID);

                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 删除上传文档
        /// </summary>
        /// <param name="strDocID"></param>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteRDWDetailDocs(string strDocID)
        {
            try
            {
                string strSql = "sp_RDW_DeleteRdwDetailDocs";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@docid", strDocID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以取消
        /// </summary>
        /// <param name="strMID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckCancelRDWHeader(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_CheckRDWHeaderCancel";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 更新抬头状态Cancel
        /// </summary>
        /// <param name="strMID"></param>
        /// <returns>返回是否更新成功</returns>
        public bool UpdateRDWHeaderCancel(string strMID)
        {
            try
            {
                string strSql = "sp_RDW_UpdateRDWHeaderCancel";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strMID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 更新抬头状态Cancel或SUSPEND
        /// </summary>
        /// <param name="strMID"></param>
        /// <returns>返回是否更新成功</returns>
        public bool UpdateRDWHeaderCancel(string strMID,string Status,string Remark)
        {
            try
            {
                string strSql = "sp_rdw_UpdateRDWHeaderCancelOrSuspend";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@Status", Status);
                sqlParam[2] = new SqlParameter("@Remark", Remark);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable selectRDWHeaderCancelOrSuspend(string mid)
        {
            string str = "sp_rdw_selectRDWHeaderCancelOrSuspend";
            SqlParameter param = new SqlParameter("@mid", mid);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, str, param).Tables[0];
        }

        /// <summary>
        /// 判断是否可以完成明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckFinishRDWDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_FinishRDWDetailCheck";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获取指定步骤参与者数据集
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public DataSet getPartner(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRDWDetailPartner";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获取用户邮箱
        /// </summary>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public string getUserEmail(string strUID)
        {
            try
            {
                string strSql = "sp_RDW_SelectSenderEmail";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@uid", strUID);

                return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return "";
            }
        }

        /// <summary>
        /// 取消评审
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool UpdateCancelEvaluateRDWDetail(string strDID, string strUserID, string strUID, string strUname, bool isUpdate, string strReasons)
        {
            try
            {
                string strSql = string.Empty;

                if (isUpdate)
                {
                    strSql = "sp_RDW_UpdateCancelEvaluateRDWDetailMsg";
                }
                else
                {
                    strSql = "sp_RDW_UpdateCancelEvaluateRDWDetail";
                }

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@userid", strUserID);
                sqlParam[2] = new SqlParameter("@uid", strUID);
                sqlParam[3] = new SqlParameter("@uname", strUname);
                sqlParam[4] = new SqlParameter("@CancelReason", strReasons);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得该步骤所有涉及用户Email
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public IDataReader SelectRDWDetailMemberEmail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRDWDetailMemberEmail";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 完成步骤任务
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <param name="strNotes"></param>
        /// <returns></returns>
        public int UpdateFinishRDWDetail(string strDID, string strUID, string strNotes, string strUName)
        {
            try
            {
                string strSql = "sp_RDW_UpdateFinishRDWDetail";

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);
                sqlParam[2] = new SqlParameter("@notes", strNotes);
                sqlParam[3] = new SqlParameter("@uName", strUName);


                return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }


        /// <summary>
        /// 发送邮件至审核者
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public bool EvaluateEmailRDWDetailCheck(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_EvaluateEmailRDWDetailCheck";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获取审核者Email
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public IDataReader SelectRDWDetailEvaluateEmail(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRDWDetailEvaluateEmail";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Ilist<T> 转换成 DataSet
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataSet ConvertToDataSet<T>(IList<T> i_objlist)
        {
            if (i_objlist == null || i_objlist.Count <= 0)
            {
                return null;
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (T t in i_objlist)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// Ilist<T> 转换成 DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable ConvertToDataTable<T>(IList<T> i_objlist)
        {
            if (i_objlist == null || i_objlist.Count <= 0)
            {
                return null;
            }
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (T t in i_objlist)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 获得RDW清单DataTable
        /// </summary>
        /// <param name="strProd"></param>
        /// <param name="strStart"></param>
        /// <param name="strStatus"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public DataTable SelectRDWListDataTable(string strProd, string strStart, string strStatus, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwHeaderList";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@prod", strProd);
                sqlParam[1] = new SqlParameter("@start", strStart);
                sqlParam[2] = new SqlParameter("@status", strStatus);
                sqlParam[3] = new SqlParameter("@uid", strUID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得RDW清单
        /// </summary>
        /// <param name="strProj"></param>
        /// <param name="strProd"></param>
        /// <param name="strStart"></param>
        /// <param name="strStatus"></param>
        /// <param name="strUID"></param>
        /// <param name="ViewAll"></param>
        /// <returns>返回RDW清单对象列表</returns>
        public IList<RDW_Header> SelectRDWRptList(string strProj, string strProd, string strStart, string strStatus, string strUID, bool ViewAll, string qad)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwRptList";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@proj", strProj);
                sqlParam[1] = new SqlParameter("@prod", strProd);
                sqlParam[2] = new SqlParameter("@start", strStart);
                sqlParam[3] = new SqlParameter("@status", strStatus);
                sqlParam[4] = new SqlParameter("@uid", strUID);
                sqlParam[5] = new SqlParameter("@all", ViewAll == true ? 1 : 0);
                sqlParam[6] = new SqlParameter("@qad", qad);

                IList<RDW_Header> RDWHeader = new List<RDW_Header>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    //RDW_Header rh = new RDW_Header();
                    //rh.RDW_Project = reader["Project"].ToString();
                    //rh.RDW_ProdCode = reader["ProdCode"].ToString();
                    ////rh.RDW_TaskID = reader["TaskID"].ToString();
                    //rh.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StartDate"]));
                    //rh.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["EndDate"]));
                    //rh.RDW_Status = reader["Status"].ToString();
                    //rh.RDW_Creater = reader["Creater"].ToString();
                    //rh.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                    //rh.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    //rh.RDW_Memo = reader["Memo"].ToString();
                    //rh.RDW_Partner = reader["Partner"].ToString();
                    //rh.RDW_Standard = reader["Standard"].ToString();
                    //rh.RDW_MstrID = Convert.ToInt32(reader["ID"]);
                    //rh.RDW_FinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["FinishDate"]));
                    //rh.RDW_CurrStep = reader["CurrStep"].ToString();
                    //rh.RDW_DalayDays = Convert.ToInt64(reader["DelayDays"].ToString());
                    //rh.RDW_Mbr = reader["RDW_Mbr"].ToString();
                    //rh.RDW_Ptr = reader["RDW_Ptr"].ToString();
                    //rh.RDW_ProdDesc = reader["ProdDesc"].ToString();
                    //if (rh.RDW_FinishDate == "1900-01-01")
                    //{
                    //    rh.RDW_FinishDate = "";
                    //}
                    //if (rh.RDW_CreatedDate == "1900-01-01")
                    //{
                    //    rh.RDW_CreatedDate = "";
                    //}


                    RDW_Header rh = new RDW_Header();
                    rh.RDW_Project = reader["Project"].ToString();
                    rh.RDW_ProdCode = reader["ProdCode"].ToString();
                    rh.RDW_ProdDesc = reader["ProdDesc"].ToString();
                    rh.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StartDate"]));
                    rh.RDW_Status = reader["Status"].ToString();
                    rh.RDW_Creater = reader["Creater"].ToString();
                    rh.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                    rh.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    rh.RDW_Memo = reader["Memo"].ToString();
                    rh.RDW_Partner = reader["Partner"].ToString();
                    rh.RDW_Standard = reader["Standard"].ToString();
                    rh.RDW_MstrID = Convert.ToInt32(reader["ID"]);
                    rh.RDW_FinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["FinishDate"]));
                    rh.RDW_CurrStep = reader["CurrStep"].ToString();
                    rh.RDW_DalayDays = Convert.ToInt64(reader["DelayDays"].ToString());
                    rh.RDW_Mbr = reader["RDW_Mbr"].ToString();
                    rh.RDW_Ptr = reader["RDW_Ptr"].ToString();
                    rh.RDW_CurrStep = reader["CurrStep"].ToString();
                    rh.RDW_TaskID = reader["RDW_TaskID"].ToString();
                    rh.RDW_ProdSKU = reader["RDW_ProdSKU"].ToString();
                    rh.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["EndDate"]));
                    if (rh.RDW_StartDate == "1900-01-01")
                    {
                        rh.RDW_StartDate = "";
                    }
                    if (rh.RDW_FinishDate == "1900-01-01")
                    {
                        rh.RDW_FinishDate = "";
                    }
                    if (rh.RDW_CreatedDate == "1900-01-01")
                    {
                        rh.RDW_CreatedDate = "";
                    }
                    if (rh.RDW_EndDate == "1900-01-01")
                    {
                        rh.RDW_EndDate = "";
                    }
                    rh.RDW_DetID = Convert.ToInt32(reader["RDW_DetID"]);
                    RDWHeader.Add(rh);
                }
                reader.Close();
                return RDWHeader;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得RDW清单
        /// </summary>
        /// <param name="strProj"></param>
        /// <param name="strProd"></param>
        /// <param name="strStart"></param>
        /// <param name="strStatus"></param>
        /// <param name="strUID"></param>
        /// <param name="ViewAll"></param>
        /// <returns>返回RDW清单对象列表</returns>
        public IList<RDW_Header> SelectRDWQueryList(string cateid, string strProj, string strProd, string strStart, string strStatus, string strUID, bool ViewAll, int ShowAll, string qad, string sku)
        {
            //try
            //{
            string strSql = "sp_RDW_SelectRdwQueryList";
            SqlParameter[] sqlParam = new SqlParameter[10];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@prod", strProd);
            sqlParam[2] = new SqlParameter("@start", strStart);
            sqlParam[3] = new SqlParameter("@status", strStatus);
            sqlParam[4] = new SqlParameter("@uid", strUID);
            sqlParam[5] = new SqlParameter("@viewall", ViewAll == true ? 1 : 0);
            //sqlParam[6] = new SqlParameter("@showall", ShowAll == true ? 1 : 0);
            sqlParam[6] = new SqlParameter("@showall", ShowAll);
            sqlParam[7] = new SqlParameter("@qad", qad);
            sqlParam[8] = new SqlParameter("@cateid", cateid);
            sqlParam[9] = new SqlParameter("@sku", sku);
            IList<RDW_Header> RDWHeader = new List<RDW_Header>();
            IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                RDW_Header rh = new RDW_Header();
                rh.RDW_Project = reader["Project"].ToString();
                rh.RDW_ProdCode = reader["ProdCode"].ToString();
                rh.RDW_ProdDesc = reader["ProdDesc"].ToString();
                rh.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StartDate"]));
                rh.RDW_Status = reader["Status"].ToString();
                rh.RDW_Creater = reader["Creater"].ToString();
                rh.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                rh.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                rh.RDW_Memo = reader["Memo"].ToString();
                rh.RDW_Partner = reader["Partner"].ToString();
                rh.RDW_Standard = reader["Standard"].ToString();
                rh.RDW_MstrID = Convert.ToInt32(reader["ID"]);
                rh.RDW_FinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["FinishDate"]));
                rh.RDW_CurrStep = reader["CurrStep"].ToString();
                rh.RDW_DalayDays = Convert.ToInt64(reader["DelayDays"].ToString());
                rh.RDW_Mbr = reader["RDW_Mbr"].ToString();
                rh.RDW_Ptr = reader["RDW_Ptr"].ToString();
                rh.RDW_TaskID = reader["RDW_TaskID"].ToString();
                rh.RDW_ProdSKU = reader["RDW_ProdSKU"].ToString();
                rh.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["EndDate"]));
                rh.RDW_Category = reader["cate_name"].ToString();
                rh.RDW_isActive = Convert.ToBoolean(reader["isActive"]);
                if (rh.RDW_FinishDate == "1900-01-01")
                {
                    rh.RDW_FinishDate = "";
                }
                if (rh.RDW_CreatedDate == "1900-01-01")
                {
                    rh.RDW_CreatedDate = "";
                }
                if (rh.RDW_EndDate == "1900-01-01")
                {
                    rh.RDW_EndDate = "";
                }

                //string sqlStr = "sp_RDW_SelectFinishDate";
                //SqlParameter[] sqlParam2 = new SqlParameter[2];
                //sqlParam2[0] =new SqlParameter("@project", rh.RDW_Project);
                //sqlParam2[1] = new SqlParameter("@currstep", rh.RDW_CurrStep);

                //DateTime FinishTime = (DateTime)SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlStr, sqlParam2);
                //string FinishDate = string.Format("{0:yyyy-MM-dd}", FinishTime);
                //if (FinishDate != "1900-01-01")
                //{
                //    RDWHeader.Add(rh);
                //}
                RDWHeader.Add(rh);

            }
            reader.Close();
            return RDWHeader;
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return null;
            //}
        }
        public DataTable SelectRDWQueryTable(string cateid, string strProj, string strProd, string strStart, string strStatus, string strUID, bool ViewAll, int ShowAll, string qad, string sku)
        {
            try
            { 
            string strSql = "sp_RDW_SelectRdwQueryList";
            SqlParameter[] sqlParam = new SqlParameter[10];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@prod", strProd);
            sqlParam[2] = new SqlParameter("@start", strStart);
            sqlParam[3] = new SqlParameter("@status", strStatus);
            sqlParam[4] = new SqlParameter("@uid", strUID);
            sqlParam[5] = new SqlParameter("@viewall", ViewAll == true ? 1 : 0);
            //sqlParam[6] = new SqlParameter("@showall", ShowAll == true ? 1 : 0);
            sqlParam[6] = new SqlParameter("@showall", ShowAll);
            sqlParam[7] = new SqlParameter("@qad", qad);
            sqlParam[8] = new SqlParameter("@cateid", cateid);
            sqlParam[9] = new SqlParameter("@sku", sku);
           
            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        #region Add by shanzm 2011.12.13
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public DataSet SelectOwner()
        {
            try
            {
                string strName = "sp_RDW_SelectOwner";

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得RDW模板清单
        /// </summary>
        /// <param name="strProj"></param>
        /// <param name="strProd"></param>
        /// <param name="strStart"></param>
        /// <param name="strStatus"></param>
        /// <param name="strUID"></param>
        /// <param name="ViewAll"></param>
        /// <returns>返回RDW模板清单对象列表</returns>
        public IList<RDW_Header> SelectTepmlateMstr(string strProj, string strSKU, string strLeader, Int32 strUID)
        {
            //try
            //{
            string strSql = "sp_RDW_SelectTepmlateMstr";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@sku", strSKU);
            sqlParam[2] = new SqlParameter("@leader", strLeader);
            sqlParam[3] = new SqlParameter("@uid", strUID);

            IList<RDW_Header> RDWHeader = new List<RDW_Header>();
            IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                RDW_Header rh = new RDW_Header();
                rh.RDW_Project = reader["RDW_Project"].ToString();
                rh.RDW_ProdCode = reader["RDW_ProdCode"].ToString();
                rh.RDW_Memo = reader["RDW_Memo"].ToString();
                rh.RDW_Creater = reader["RDW_Creater"].ToString();
                rh.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["RDW_CreatedDate"]));
                rh.RDW_CreatedBy = Convert.ToInt32(reader["RDW_CreatedBy"]);
                rh.RDW_MstrID = Convert.ToInt32(reader["RDW_MstrID"]);
                RDWHeader.Add(rh);
            }
            reader.Close();
            return RDWHeader;
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return null;
            //}
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="cateid"></param>
        public int SelectRelatedTepmlate(int cateid)
        {
             
            int ID = 0;
            //string Name = "";
            string strSql = "sp_RDW_SelectRelatedTepmlateMstr";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@cateid", cateid);
            //sqlParam[1] = new SqlParameter("@templateID",ID );
            //sqlParam[1].Direction=ParameterDirection.Output;
            //sqlParam[2] = new SqlParameter("@templateName", Name);
            //sqlParam[2].Direction = ParameterDirection.Output;

            DataSet dataset = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            if (dataset.Tables[0].Rows[0]["tempid"] == DBNull.Value)
            {
                ID = 0;
            }
            else
            {
                ID = Convert.ToInt32(dataset.Tables[0].Rows[0]["tempid"]);
            }
            return ID;
            //tempName = dataset.Tables[0].Rows[0]["tempname"].ToString();

        }
        /// <summary>
        /// 根据ID查到MStr记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RDW_Header SelectTepmlateMstr(string id)
        {
            try
            {
                string strSql = "sp_RDW_SelectTepmlateMstr";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);

                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                RDW_Header rh = new RDW_Header();

                while (reader.Read())
                {
                    rh.RDW_Project = reader["RDW_Project"].ToString();
                    rh.RDW_ProdCode = reader["RDW_ProdCode"].ToString();
                    rh.RDW_Memo = reader["RDW_Memo"].ToString();
                    rh.RDW_Creater = reader["RDW_Creater"].ToString();
                    rh.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["RDW_CreatedDate"]));
                    rh.RDW_CreatedBy = Convert.ToInt32(reader["RDW_CreatedBy"]);
                    rh.RDW_MstrID = Convert.ToInt32(reader["RDW_MstrID"]);
                }
                reader.Close();
                return rh;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="strMstrID"></param>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteTemplate(string strMstrID)
        {
            try
            {
                string strSql = "sp_RDW_DeleteTemplate";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strMstrID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 新增RDW模板
        /// </summary>
        /// <param name="rm"></param>
        /// <returns>ID</returns>
        public int InsertTemplateMstr(RDW_Header rh)
        {
            try
            {
                string strSql = "sp_RDW_InsertTemplateMstr";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@proj", rh.RDW_Project.Trim());
                sqlParam[1] = new SqlParameter("@code", rh.RDW_ProdCode.Trim());
                sqlParam[2] = new SqlParameter("@memo", rh.RDW_Memo.Trim());
                sqlParam[3] = new SqlParameter("@leader", rh.RDW_LeaderID.ToString());
                sqlParam[4] = new SqlParameter("@uid", rh.RDW_CreatedBy.ToString());

                return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }
        /// <summary>
        /// 更新RDW模板
        /// </summary>
        /// <param name="rm"></param>
        /// <returns>ID</returns>
        public void UpdateTemplateMstr(RDW_Header rh)
        {
            try
            {
                string strSql = "sp_RDW_UpdateTemplateMstr";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@mstrID", rh.RDW_MstrID);
                sqlParam[1] = new SqlParameter("@proj", rh.RDW_Project.Trim());
                sqlParam[2] = new SqlParameter("@code", rh.RDW_ProdCode.Trim());
                sqlParam[3] = new SqlParameter("@memo", rh.RDW_Memo.Trim());
                sqlParam[4] = new SqlParameter("@uid", rh.RDW_CreatedBy.ToString());

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                //throw ex;
            }
        }

        /// <summary>
        /// 判断该步骤的子步骤（如果有的话），是否全部完成，否则不能审核该步骤
        /// </summary>
        /// <param name="stepid"></param>
        /// <returns></returns>
        public bool CheckIsAllSubTasksCompleted(string stepid)
        {
            try
            {
                string strSql = "sp_RDW_checkIsAllSubTasksCompleted";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@stepid", stepid);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }


        public bool CheckModifiedDate(string stepid)
        {
            try
            {
                string strSql = "sp_RDW_checkModifiedDateLimited";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Detid", stepid);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        #endregion


        /// <summary>
        /// 获得该项目所有涉及的用户Email
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public IDataReader SelectRDWProjectAllMemberEmail(string strMID, string strUId)
        {
            try
            {
                string strSql = "sp_RDW_SelectRDWAllMemberEmail";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@uid", strUId);
                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 步骤审批员获取该步骤的保存\上传文件权限
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckApproverSave(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_CheckApproverSavePermission";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));

            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// Leader获取该步骤的保存\上传文件权限
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckLeaderSave(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_CheckLeaderSavePermission";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));

            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获取步骤的审核者
        /// </summary>
        /// <param name="strDID">项目步骤ID</param>
        /// <returns></returns>
        public DataSet getStepApprover(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_SelectRdwStepApprover";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获取步骤后续完成的步骤
        /// </summary>
        /// <param name="strMID">项目ID</param>
        /// <param name="strDID">步骤Id</param>
        /// <returns></returns>
        public DataSet getSubsequentCompSteps(string strMID, string strDID)
        {
            try
            {
                string strSql = "sp_RDW_SelectSubsequentCompSteps";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@did", strDID);

                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        ///  取消已完成步骤的后续步骤
        /// </summary>
        /// <param name="SubCompStepsId">取消完成的步骤ID</param>
        /// <param name="CurStepID">当前步骤ID</param>
        /// <param name="strMID">项目ID</param>
        /// <param name="strUID">用户ID</param>
        /// <param name="strUname">UserName</param>
        /// <returns></returns>
        public bool CancelSubsequentCompStep(string SubCompStepsId, string CurStepID, string strMID, string strUID, string strUname)
        {
            //try
            //{
            string strSql = "sp_RDW_CancelSubsequentCompStep";

            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@cancelDetID", SubCompStepsId);
            sqlParam[1] = new SqlParameter("@curDetId", CurStepID);
            sqlParam[2] = new SqlParameter("@mid", strMID);
            sqlParam[3] = new SqlParameter("@uid", strUID);
            sqlParam[4] = new SqlParameter("@uname", strUname);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return false;
            //}
        }

        /// <summary>
        ///  成员取消完成
        /// </summary>
        /// <param name="strMID"></param>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <param name="strUName"></param>
        /// <returns></returns>
        public bool CancelMemberFinish(string strMID, string strDID, string strUID, string strUName)
        {
            string strSql = "sp_RDW_CancelMemberFinish";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@mid", strMID);
            sqlParam[1] = new SqlParameter("@did", strDID);
            sqlParam[2] = new SqlParameter("@uId", strUID);
            sqlParam[3] = new SqlParameter("@uName", strUName);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
        }

        /// <summary>
        /// 成员取消完成
        /// </summary>
        /// <param name="strMID"></param>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <param name="strUName"></param>
        /// <param name="strReason"></param>
        /// <returns></returns>
        public bool CancelMemberFinish(string strMID, string strDID, string strUID, string strUName, string strReason)
        {
            string strSql = "sp_RDW_CancelMemberFinish";
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@mid", strMID);
            sqlParam[1] = new SqlParameter("@did", strDID);
            sqlParam[2] = new SqlParameter("@uId", strUID);
            sqlParam[3] = new SqlParameter("@uName", strUName);
            sqlParam[4] = new SqlParameter("@cancelReason", strReason);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
        }

        /// <summary>
        /// 确定当前用户有权限取消步骤完成，当步骤成员还未Approve时
        /// </summary>
        /// <param name="strDID">项目步骤IDeas</param>
        /// <param name="strUID">用户</param>
        /// <returns></returns>
        public bool CheckCancleMemberFinish(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RDW_CancelMemberFinishCheck";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获取项目分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataSet SelectProjectCategory(string name)
        {
            try
            {
                string strSql = "sp_RDW_selectCategory";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@name", name);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取项目分类的区域
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataSet SelectProjectRegion(string name)
        {
            try
            {
                string strSql = "sp_RDW_selectRegion";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@name", name);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 检查项目的Code，保证其唯一性
        /// </summary>
        /// <param name="prodcode"></param>
        /// <returns></returns>
        public bool CheckIsHavethisProdCode(string prodcode)
        {
            try
            {
                string strSql = "sp_RDW_checkIsHaveProdcode";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@prodcode", prodcode);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam).ToString());
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 上传文件时，必须验证文件的类型
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public bool CheckFileExtension(string extension)
        {
            try
            {
                string strSql = "sp_RDW_checkFileExtension";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@extension", extension);
                sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

                return Convert.ToBoolean(sqlParam[1].Value);
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// 步骤是否需要链接到打样单
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public bool CheckLinkSample(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_CheckLinkSample";

                SqlParameter parm = new SqlParameter("@strdid", strDID);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, parm));

            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 是否延期
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public string Checkdelay(string strDID)
        {
            try
            {
                string strSql = "sp_RDW_Checkdelay";

                SqlParameter parm = new SqlParameter("@did", strDID);
                string st= (SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, parm)).ToString();
                return st;
            }
            catch (Exception ex)
            {
                //throw ex;
                return "0";
            }
        }
        /// <summary>
        /// 获取文档分类
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public DataTable getTypeInfo()
        {
            String strSql = "Select typeid, typename From qaddoc.dbo.DocumentType where isDeleted Is Null  order by typename";
            return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 步骤是否需要链接到打样单
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns></returns>
        public DataTable getCategoryInfo(string strTypeID)
        {
            String strSql = "Select  cateid,  catename From qaddoc.dbo.DocumentCategory  Where  catename Is Not Null And typeId = '" + strTypeID + "' order by catename ";
            return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql).Tables[0];
        }

        public DataTable getDocbyType(int iTypeid, int iCategoryid, string strKeyWordSearch)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@typeid", iTypeid);
            param[1] = new SqlParameter("@cateid", iCategoryid);
            param[2] = new SqlParameter("@keyword", strKeyWordSearch);

            return SqlHelper.ExecuteDataset(strSupp, CommandType.StoredProcedure, "sp_bos_selectBosallDoc", param).Tables[0];
        }
        /// <summary>
        /// 获取供应商针对打样单上传的文档信息
        /// </summary>
        /// <param name="bosNbr"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public DataTable getBosSuppImportDocs(string strDID)
        {
            SqlParameter param = new SqlParameter("@strdid", strDID);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_RDW_selectBosSuppImportDocs", param).Tables[0];
        }

        /// <summary>
        /// add document from doc moudle
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        public Boolean addBosDetDocLink(string docid, string did, string uID)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@docid", docid);
            param[1] = new SqlParameter("@did", did);
            param[2] = new SqlParameter("@uID", uID);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_RDW_CopyFileFromDocs", param));
        }

        /// <summary>
        ///  获取项目Code的流水号,通过类别
        /// </summary>
        /// <param name="cateid"></param>
        /// <param name="RDW_CreatedBy"></param>
        /// <returns>项目名称</returns>
        public string getProjectCodebyCateCode(int cateid, int RDW_CreatedBy)
        {
            string strSql = "sp_RDW_selectProjectCodebyCategory";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@cateid", cateid);
            sqlParam[1] = new SqlParameter("@uID", RDW_CreatedBy);
            sqlParam[2] = new SqlParameter("@retValue", SqlDbType.VarChar, 10);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            return Convert.ToString(sqlParam[2].Value);
        }

        /// <summary>
        /// 项目导出
        /// </summary>
        /// <param name="strProj"></param>
        /// <param name="strProd"></param>
        /// <param name="strSku"></param>
        /// <param name="strStart"></param>
        /// <param name="strMessage"></param>
        /// <param name="strStatus"></param>
        /// <param name="strUID"></param>
        /// <param name="canViewAll"></param>
        /// <returns></returns>
        public IList<RDW_Header> SelectRDWListExport(string strCateid, string strProj, string strProd, string strSku, string strStart, string strMessage, string strStatus, string strUID, bool canViewAll)
        {
            //try
            //{
            string strSql = "sp_RDW_SelectRdwHeaderListReport";
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@prod", strProd);
            sqlParam[2] = new SqlParameter("@sku", strSku);
            sqlParam[3] = new SqlParameter("@start", strStart);
            sqlParam[4] = new SqlParameter("@msg", strMessage);
            sqlParam[5] = new SqlParameter("@status", strStatus);
            sqlParam[6] = new SqlParameter("@uid", strUID);
            sqlParam[7] = new SqlParameter("@all", canViewAll == true ? 1 : 0);
            sqlParam[8] = new SqlParameter("@cateid", strCateid);

            IList<RDW_Header> RDWHeader = new List<RDW_Header>();
            IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                RDW_Header rh = new RDW_Header();
                rh.RDW_Category = reader["cate_name"].ToString();
                rh.RDW_Project = reader["Project"].ToString();
                rh.RDW_ProdCode = reader["ProdCode"].ToString();
                rh.RDW_ProdSKU = reader["ProdSku"].ToString();
                rh.RDW_ProdDesc = reader["ProdDesc"].ToString();
                rh.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StartDate"]));
                rh.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["EndDate"]));
                rh.RDW_Status = reader["Status"].ToString();
                rh.RDW_Creater = reader["Creater"].ToString();
                rh.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                rh.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                rh.RDW_Memo = reader["Memo"].ToString();
                rh.RDW_PartnerName = reader["PartnerName"].ToString();
                rh.RDW_Standard = reader["Standard"].ToString();
                rh.RDW_MstrID = Convert.ToInt32(reader["ID"]);
                rh.RDW_PM = reader["Leader"].ToString();
                rh.RDW_FinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["FinishDate"]));
                //if (rh.RDW_FinishDate == "1900-01-01")
                //{
                //    rh.RDW_FinishDate = "";
                //}
                RDWHeader.Add(rh);
            }
            reader.Close();
            return RDWHeader;
        }

        public DataTable SelectRDWListExport1(string strCateid, string strProj, string strProd, string strSku, string strStart, string strMessage, string strStatus, string strUID, string keyword,string region,string type, bool canViewAll)
        {
            //try
            //{
            string strSql = "sp_RDW_SelectRdwHeaderList";
            SqlParameter[] sqlParam = new SqlParameter[12];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@prod", strProd);
            sqlParam[2] = new SqlParameter("@sku", strSku);
            sqlParam[3] = new SqlParameter("@start", strStart);
            sqlParam[4] = new SqlParameter("@msg", strMessage);
            sqlParam[5] = new SqlParameter("@status", strStatus);
            sqlParam[6] = new SqlParameter("@uid", strUID);
            sqlParam[7] = new SqlParameter("@all", canViewAll == true ? 1 : 0);
            sqlParam[8] = new SqlParameter("@cateid", strCateid);
            sqlParam[9] = new SqlParameter("@keyword", keyword);
            sqlParam[10] = new SqlParameter("@region", region);
            sqlParam[11] = new SqlParameter("@type", type);

            IList<RDW_Header> RDWHeader = new List<RDW_Header>();
            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            return dt;
        }

        public DataTable SelectRDWListWithQADExport(string strCateid, string strProj, string strProd, string strSku, string strStart, string strMessage, string strStatus, string strUID, string keyword,string region,string type, bool canViewAll)
        {
            //try
            //{
            string strSql = "sp_RDW_SelectRdwHeaderListWithQad";
            SqlParameter[] sqlParam = new SqlParameter[12];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@prod", strProd);
            sqlParam[2] = new SqlParameter("@sku", strSku);
            sqlParam[3] = new SqlParameter("@start", strStart);
            sqlParam[4] = new SqlParameter("@msg", strMessage);
            sqlParam[5] = new SqlParameter("@status", strStatus);
            sqlParam[6] = new SqlParameter("@uid", strUID);
            sqlParam[7] = new SqlParameter("@all", canViewAll == true ? 1 : 0);
            sqlParam[8] = new SqlParameter("@cateid", strCateid);
            sqlParam[9] = new SqlParameter("@keyword", keyword);
            sqlParam[10] = new SqlParameter("@region", region);
            sqlParam[11] = new SqlParameter("@type", type);
            IList<RDW_Header> RDWHeader = new List<RDW_Header>();
            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            return dt;
        }

        public DataTable SelectProjectSummary(string cateid, string strProj, string strProd, string strStart, string strStatus, string strUID, bool ViewAll, bool ShowAll, string qad)
        {
            //try
            //{
            string strSQL = "sp_RDW_SelectRdwQueryList";
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@proj", strProj);
            sqlParam[1] = new SqlParameter("@prod", strProd);
            sqlParam[2] = new SqlParameter("@start", strStart);
            sqlParam[3] = new SqlParameter("@status", strStatus);
            sqlParam[4] = new SqlParameter("@uid", strUID);
            sqlParam[5] = new SqlParameter("@viewall", ViewAll == true ? 1 : 0);
            sqlParam[6] = new SqlParameter("@showall", ShowAll == true ? 1 : 0);
            sqlParam[7] = new SqlParameter("@qad", qad);
            sqlParam[8] = new SqlParameter("@cateid", cateid);

            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSQL, sqlParam).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取所有申请的记录
        /// </summary>
        /// <param name="strProjName"></param>
        /// <param name="strProjCode"></param>
        /// <param name="strApplyDate"></param>
        /// <param name="pendingToAppr"></param>
        /// <returns></returns>
        public DataTable getProjQadApplyMstr(int strmID, string strProjName, string strProjCode, string strApplyDate, bool pendingToAppr, int userId, int roleId)
        {
            string strSql = "sp_RDW_selectProjQadApplyList";
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@projName", strProjName);
            sqlParam[1] = new SqlParameter("@projCode", strProjCode);
            sqlParam[2] = new SqlParameter("@applyDate", strApplyDate);
            sqlParam[3] = new SqlParameter("@pendingToAppr", pendingToAppr);
            sqlParam[4] = new SqlParameter("@strmID", strmID);
            sqlParam[5] = new SqlParameter("@uid", userId);
            sqlParam[6] = new SqlParameter("@roleid", roleId);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        }

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="iApplyId"></param>
        public void deleteProjQadApply(int iApplyId)
        {
            string strSql = "sp_RDW_deleteProjQadApply";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@rdw_pqid", iApplyId);
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
        }

        public DataTable getProjQadLink(string strID, int pqapplyId)
        {
            //try
            //{
            string strSql = "sp_RDW_selectProjQadApply";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@mid", strID);
            sqlParam[1] = new SqlParameter("@applyid", pqapplyId);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return null;
            //}
        }

        /// <summary>
        /// 新建QAD申请
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="strApprover"></param>
        /// <param name="strApproverName"></param>
        /// <param name="applyReason"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public int AddProjQadLink(string mid, string strApprover, string strApproverName, string applyReason, int uID, string uName, DataTable qadDetail)
        {
            string strSql = "sp_RDW_InsertProjQadApply";
            StringWriter writer = new StringWriter();
            qadDetail.WriteXml(writer);
            string detail = writer.ToString();

            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@mid", mid);
            sqlParam[1] = new SqlParameter("@approverBy", strApprover);
            sqlParam[2] = new SqlParameter("@approverName", strApproverName);
            sqlParam[3] = new SqlParameter("@applyReason", applyReason);
            sqlParam[4] = new SqlParameter("@uId", uID);
            sqlParam[5] = new SqlParameter("@uName", uName);
            sqlParam[6] = new SqlParameter("@qaddetail", detail);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
        }

        /// <summary>
        /// 审批结果
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="approveBy"></param>
        /// <param name="approveOpoin"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool UpdateProjQadLink(int pqid, string strApprover, string strApproverName, int uID, string uName, string approveOpoin, bool result)
        {
            string strSql = "sp_RDW_UpdateProjQadApprove";

            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@pqid", pqid);
            if (!string.IsNullOrEmpty(strApproverName))
            {
                sqlParam[1] = new SqlParameter("@approverBy", strApprover);
                sqlParam[2] = new SqlParameter("@approverName", strApproverName);
            }
            sqlParam[3] = new SqlParameter("@uId", uID);
            sqlParam[4] = new SqlParameter("@uName", uName);
            sqlParam[5] = new SqlParameter("@approveOpinion", approveOpoin);
            sqlParam[6] = new SqlParameter("@appresult", result);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));

        }

        public bool IsExistRdwQad(string mid, string qad)
        {
            try
            {
                string strName = "sp_rdw_checkRdwQadList";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@qad", qad);
                param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[2].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
                return Convert.ToBoolean(param[2].Value);

            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public bool IsExistQad(string qad, out DataTable dt)
        {
            try
            {
                string strName = "sp_rdw_checkQad";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@qad", qad);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;
                dt = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param).Tables[0];
                return Convert.ToBoolean(param[1].Value);

            }
            catch (Exception ex)
            {
                dt = null;
                return false;
            }
        }

        public bool IsExistQad(string qad)
        {
            try
            {
                string strName = "sp_rdw_checkQad";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@qad", qad);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
                return Convert.ToBoolean(param[1].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable SelectRdwQad(string id, int rdw_pqapplyId)
        {

            try
            {
                string strName = "sp_rdw_selectRdwQadList";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@rdw_pqapplyId", rdw_pqapplyId);
                return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }

        }

        public DataTable SelectRdwQad(int rdw_pqapplyId)
        {

            try
            {
                string strName = "sp_rdw_selectRdwApplyQadList";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@rdw_pqapplyId", rdw_pqapplyId);
                return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }

        }

        public DataTable SelectApprove(string id, int rdw_pqapplyId)
        {

            try
            {
                string strName = "sp_RDW_selectProjQadApprove";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@mid", id);
                sqlParam[1] = new SqlParameter("@applyid", rdw_pqapplyId);
                return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, sqlParam).Tables[0];
            }
            catch
            {
                return null;
            }

        }

        public bool DeleteRdwQad(string id)
        {
            try
            {
                string strName = "sp_rdw_deleteRdwQadList";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
                return Convert.ToBoolean(param[1].Value);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        ///   插入项目与QAD的关联
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="qad"></param>
        /// <param name="createBy"></param>
        /// <param name="rdw_pqapplyId">申请序号</param>
        /// <returns></returns>
        public bool InsertRdwQad(string mid, string qad, int createBy)
        {
            try
            {
                string strName = "sp_rdw_insertRdwQadList";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@mid", mid);
                param[1] = new SqlParameter("@qad", qad);
                param[2] = new SqlParameter("@createBy", createBy);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
                return Convert.ToBoolean(param[3].Value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 选择人员
        /// </summary>
        /// <param name="plantid"></param>
        /// <param name="departmentId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataSet selectAllUsers(int plantid, int departmentId, string userName)
        {
            //try
            //{
            string strSql = "sp_RDW_selectAllUsers";

            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@plantid", plantid);
            sqlParam[1] = new SqlParameter("@departmentId", departmentId);
            sqlParam[2] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return null;
            //}
        }

        public DataTable GetApplyQadList(string pqApplyId)
        {
            DataTable dt;
            string strSql = "sp_RDW_selectProjApplyQadListLinkExport";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pqapplyid", pqApplyId);

            dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            return dt;
        }


        public bool SendMail(string mid, string projectName, string projectCode, string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string applyReason, string applyId, out string returnMessage)
        {
            StringBuilder sb = new StringBuilder();

            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
            MailAddress to;
            MailMessage mail = new MailMessage();
            mail.From = from;
            Boolean isSuccess = false;
            try
            {
                to = new MailAddress(toEmailAddress, strApproverName);
                mail.To.Add(to);
            }
            catch
            {
                returnMessage = "the email address of " + toEmailAddress + "is incorrect.Pls correct!";
                return false;
            }

            if (applyEmailAddress != null && applyEmailAddress != "")
            {
                MailAddress cc = new MailAddress(applyEmailAddress, applyName);
                mail.CC.Add(cc);
            }

            try
            {
                mail.Subject = "[Notify]Project Add QAD Link Application -- Approve QAD";
                sb.Append("<html>");
                sb.Append("<body>");
                sb.Append("<form>");
                sb.Append(" Dear  Approve  <br>");
                sb.Append("     Project Name:" + projectName + "<br>");
                sb.Append("     Product Code:" + projectCode + "<br>");
                sb.Append("     Apply Reasons:" + applyReason + "<br>");
                sb.Append("     中文译-- 申请者: " + applyName + "在此项目添加了与QAD呈关联变更更的申请，请链接以下地址时查看并审批申请<br><br>");
                sb.Append("     Please see detail information.<br>");
                sb.Append("     Please click follow link to view:<br>");
                sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/RDW/RDW_ProjQadApply.aspx?type=new&mid=" + mid + "&rm=" + DateTime.Now.ToString() + "' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/RDW/RDW_ProjQadApply.aspx?type=new&mid=" + mid + "</a>");
                sb.Append("</form>");
                sb.Append("</html>");

                mail.Body = Convert.ToString(sb);
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                sb.Remove(0, sb.Length);
                isSuccess = true;
                returnMessage = "Send Mail Success!";
            }
            catch (Exception ex)
            {
                isSuccess = false;
                returnMessage = "Send Mail Failed!";
            }

            return isSuccess;


        }

        /// <summary>
        /// Get ExcelContent Via OLEDB
        /// </summary>
        public DataSet GetExcelContents(String pFile, String sSheet)
        {
            OleDbConnection myOleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + pFile + "; Extended Properties = Excel 8.0;");
            OleDbCommand myOleDbCommand = new OleDbCommand("SELECT * FROM [" + sSheet + "]", myOleDbConnection);
            OleDbDataAdapter myData = new OleDbDataAdapter(myOleDbCommand);

            DataSet myDS = new DataSet();
            myData.Fill(myDS);

            myOleDbConnection.Close();
            return myDS;
        }

        public string[] GetExcelSheetName(string strFileName)
        {
            List<string> arrTable = new List<string>();

            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet." +
                           "OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + strFileName))
            {
                conn.Open();
                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][2].ToString().Trim().Substring(dt.Rows[j][2].ToString().Trim().Length - 1, 1) == "$")
                    {
                        arrTable.Add(dt.Rows[j][2].ToString().Trim());
                    }

                }
                conn.Close();
            }
            return arrTable.ToArray();
        }
        //判断该用户能否取消该步骤（disapprove能否使用）
        public bool CanDisapprove(int did ,int mid ,int uID)
        {
            try
            {
                string sql = "sp_RDW_CanDisapprove";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@did", did);
                sqlParam[1] = new SqlParameter("@mid", mid);
                sqlParam[2] = new SqlParameter("@uid", uID);
                sqlParam[3] = new SqlParameter("@retValue", DbType.Int32);
                sqlParam[3].Direction = System.Data.ParameterDirection.Output;

                SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, sqlParam);
                return Convert.ToBoolean(sqlParam[3].Value);
            }
            catch
            {
                return false;
            }
        }
        //判断该步骤是否为审批步骤 
        public bool IsApproveStep(int did)
        {
            try
            {
                string sql = "sp_RDW_IsApproveStep";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", did);
                sqlParam[1] = new SqlParameter("@retValue", DbType.Int32);
                sqlParam[1].Direction = System.Data.ParameterDirection.Output;

                SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, sqlParam);
                return Convert.ToBoolean(sqlParam[1].Value);
            }
            catch
            {
                return false;
            }
        }

        //得到立项模板带路径的全文件名
        public string GetTemplateDocName(int mid)
        {
            string strSql = "sp_rdw_DownTemplateDoc";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@mid", mid);

            string fileURL = SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam).ToString();

            return fileURL;
        }

        public void InitDet_Ptr_Mbr(int strID,int tempID)
        {
            string strSql = "sp_RDW_SelectRdwDetailList";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@mid", strID);

            IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            while (reader.Read())
            {
                RDW_Detail rd = new RDW_Detail();
                rd.RDW_MstrID = Convert.ToInt32(reader["MstrID"]);
                rd.RDW_DetID = Convert.ToInt32(reader["DetID"]);
                rd.RDW_StepName = reader["StepName"].ToString();
                rd.RDW_StepDesc = reader["StepDesc"].ToString();
                rd.RDW_StepNo = reader["Step"].ToString();
                rd.RDW_Sort = Convert.ToInt32(reader["sort"].ToString());
                rd.RDW_isActive = Convert.ToBoolean(reader["isActive"]);
                rd.RDW_isTemp = Convert.ToBoolean(reader["RDW_isTemp"].ToString());
                rd.RDW_Duration = Convert.ToInt32(reader["Duration"].ToString());
                rd.RDW_Predecessor = reader["Predecessor"].ToString();
                rd.RDW_Status = Convert.ToInt32(reader["Status"]);
                rd.RDW_Creater = reader["Creater"].ToString();
                rd.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                rd.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                rd.RDW_StepStartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepStartDate"]));
                rd.RDW_StepEndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepEndDate"]));
                rd.RDW_StepFinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepFinishDate"]));
                rd.RDW_ParentDetID = Convert.ToInt32(reader["RDW_ParentID"].ToString());
                rd.RDW_PredtaskID = reader["RDW_PredtaskID"].ToString();

                /////////////////////////////////////////////////////////////////////////////////////////
                string streadPrt = "sp_RDW_read_Ptr";
                SqlParameter[] pararms = new SqlParameter[2];
                pararms[0] = new SqlParameter("@StepName", rd.RDW_StepName);
                pararms[1] = new SqlParameter("@tempID", tempID);
                IDataReader readPtr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, streadPrt, pararms);
                while (readPtr.Read())
                {
                    string initDet_Ptr = "sp_RDW_InitDet_Prt";
                    SqlParameter[] InitParam = new SqlParameter[3];
                    InitParam[0] = new SqlParameter("@DetID", rd.RDW_DetID);
                    if (!DBNull.Value.Equals(readPtr["UserID"]) && !DBNull.Value.Equals(readPtr["UserName"]))
                    {
                        InitParam[1] = new SqlParameter("@PartnerID", Convert.ToInt32(readPtr["UserID"]));
                        InitParam[2] = new SqlParameter("@PartnerName", readPtr["UserName"].ToString());
                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, initDet_Ptr, InitParam);
                    }
                }
                readPtr.Close();
                /////////////////////////////////////////////////////////////////////////////////////////
                string streadMbr = "sp_RDW_read_Mbr";
                pararms[0] = new SqlParameter("@StepName", rd.RDW_StepName);
                pararms[1] = new SqlParameter("@tempID", tempID);
                IDataReader readMbr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, streadMbr, pararms);
                while (readMbr.Read())
                {
                    string initDet_Mbr = "sp_RDW_InitDet_Mbr";
                    SqlParameter[] InitParam = new SqlParameter[3];
                    InitParam[0] = new SqlParameter("@DetID", rd.RDW_DetID);
                    if (!DBNull.Value.Equals(readMbr["UserID"]) && !DBNull.Value.Equals(readMbr["UserName"]))
                    {
                        InitParam[1] = new SqlParameter("@MbrID", Convert.ToInt32(readMbr["UserID"]));
                        InitParam[2] = new SqlParameter("@MbrName", readMbr["UserName"].ToString());
                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, initDet_Mbr, InitParam);
                    }
                }
                readMbr.Close();
            }
            reader.Close();
        }

        public bool CloseRdwDet(string mstrId, string detId, string userId)
        {
            string strSql = "sp_RDW_CloseDet";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@mid", mstrId);
            sqlParam[1] = new SqlParameter("@DetId", detId);
            sqlParam[2] = new SqlParameter("@UserId", userId);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
        }

        #region UL
        public bool insertUL(string project, string model, string LEDJXL, string LEDLV, string DriverJXL, string DriverLV, string createby, string createyname)
        {
            try
            {
                string strSql = "sp_UL_insertUL";
                SqlParameter[] sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@project", project);
                sqlParam[1] = new SqlParameter("@model", model);
                sqlParam[2] = new SqlParameter("@LEDJXL", LEDJXL);
                sqlParam[3] = new SqlParameter("@LEDLV", LEDLV);
                sqlParam[4] = new SqlParameter("@DriverJXL", DriverJXL);
                sqlParam[5] = new SqlParameter("@DriverLV", DriverLV);
                sqlParam[6] = new SqlParameter("@createby", createby);
                sqlParam[7] = new SqlParameter("@createyname", createyname);
                sqlParam[8] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToBoolean(sqlParam[8].Value);
            }
            catch
            {
                return false;
            }
        }
        public bool selectULmodel(string model)
        {
            try
            {
                string strSql = "sp_UL_selectULmodel";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@model", model);
                sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToBoolean(sqlParam[1].Value);
            }
            catch
            {
                return false;
            }
        }


        public bool insertULnew(string id, string UL_model, string Ul_Enumber, string UL_Section, string UL_DriverJXL, string UL_LEDJXL, string UL_DriverLv, string UL_LEDLv, string UL_pic, string UL_NOTE, string createby, string createyname)
        {
            try
            {
                string strSql = "sp_UL_insertULNew";
                SqlParameter[] sqlParam = new SqlParameter[12];
               
                sqlParam[0] = new SqlParameter("@UL_model", UL_model);

                sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);

                sqlParam[2] = new SqlParameter("@Ul_Enumber", Ul_Enumber);
                sqlParam[3] = new SqlParameter("@UL_Section", UL_Section);
                sqlParam[4] = new SqlParameter("@UL_DriverJXL", UL_DriverJXL);
                sqlParam[5] = new SqlParameter("@UL_LEDJXL", UL_LEDJXL);
                sqlParam[6] = new SqlParameter("@UL_DriverLv", UL_DriverLv);
                sqlParam[7] = new SqlParameter("@UL_LEDLv", UL_LEDLv);
                sqlParam[8] = new SqlParameter("@UL_pic", UL_pic);
                sqlParam[9] = new SqlParameter("@UL_NOTE", UL_NOTE);
                sqlParam[10] = new SqlParameter("@createby", createby);
                sqlParam[11] = new SqlParameter("@createyname", createyname);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToBoolean(sqlParam[1].Value);
            }
            catch
            {
                return false;
            }
        }
        public bool insertULQAD(string id,string QAD, string createby, string createyname)
        {
            try
            {
                string strSql = "sp_UL_insertULQAD";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@QAD", QAD);
                sqlParam[2] = new SqlParameter("@createby", createby);
                sqlParam[3] = new SqlParameter("@createyname", createyname);
                sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToBoolean(sqlParam[4].Value);
            }
            catch
            {
                return false;
            }
        }
        public bool updateULDoc(string id, string UL_docSentTo, string UL_docSentName, string UL_docDate, string UL_docSentEmail)
        {
            try
            {
                string strSql = "sp_UL_updateDoc";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@UL_docSentTo", UL_docSentTo);
                sqlParam[2] = new SqlParameter("@UL_docSentName", UL_docSentName);
                sqlParam[3] = new SqlParameter("@UL_docDate", UL_docDate);
                sqlParam[4] = new SqlParameter("@UL_docSentEmail", UL_docSentEmail);
                sqlParam[5] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToBoolean(sqlParam[5].Value);
            }
            catch
            {
                return false;
            }
        }

        public bool updateULModel(string id, string UL_model, string Ul_Enumber, string UL_Section, string UL_DriverJXL, string UL_LEDJXL, string UL_DriverLv, string UL_LEDLv, string UL_pic, string UL_NOTE)
        {
            try
            {
                string strSql = "sp_UL_updateModel";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@UL_model", UL_model);
               
                sqlParam[2] = new SqlParameter("@retValue", SqlDbType.Bit);

                sqlParam[3] = new SqlParameter("@Ul_Enumber", Ul_Enumber);
                sqlParam[4] = new SqlParameter("@UL_Section", UL_Section);
                sqlParam[5] = new SqlParameter("@UL_DriverJXL", UL_DriverJXL);
                sqlParam[6] = new SqlParameter("@UL_LEDJXL", UL_LEDJXL);
                sqlParam[7] = new SqlParameter("@UL_DriverLv", UL_DriverLv);
                sqlParam[8] = new SqlParameter("@UL_LEDLv", UL_LEDLv);
                sqlParam[9] = new SqlParameter("@UL_pic", UL_pic);
                sqlParam[10] = new SqlParameter("@UL_NOTE", UL_NOTE);
                sqlParam[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToBoolean(sqlParam[2].Value);
            }
            catch
            {
                return false;
            }
        }
        public DataTable selectUL(string project, string findate1, string findate2, string model)
        {
            try
            {
                string strSql = "sp_UL_selectUL";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@project", project);
                sqlParam[1] = new SqlParameter("@findate1", findate1);
                sqlParam[2] = new SqlParameter("@findate2", findate2);
                sqlParam[3] = new SqlParameter("@model", model);


                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
                
            }
            catch
            {
                return null;
            }
        }

        public DataTable selectULView(string project, string findate1, string findate2, string model)
        {
            try
            {
                string strSql = "sp_UL_selectULView";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@project", project);
                sqlParam[1] = new SqlParameter("@findate1", findate1);
                sqlParam[2] = new SqlParameter("@findate2", findate2);
                sqlParam[3] = new SqlParameter("@model", model);


                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            }
            catch
            {
                return null;
            }
        }
        public DataTable selectULEXCEL(string project, string findate1, string findate2, string model)
        {
            try
            {
                string strSql = "sp_UL_selectULEXCEL";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@project", project);
                sqlParam[1] = new SqlParameter("@findate1", findate1);
                sqlParam[2] = new SqlParameter("@findate2", findate2);
                sqlParam[3] = new SqlParameter("@model", model);


                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            }
            catch
            {
                return null;
            }
        }
        public DataTable selectULTemp(string project, string findate1, string findate2, string model)
        {
            try
            {
                string strSql = "sp_UL_selectULTemp";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@project", project);
                sqlParam[1] = new SqlParameter("@findate1", findate1);
                sqlParam[2] = new SqlParameter("@findate2", findate2);
                sqlParam[3] = new SqlParameter("@model", model);


                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            }
            catch
            {
                return null;
            }
        }
        public DataTable selectULSample(string id)
        {
            try
            {
                string strSql = "sp_UL_selectSample";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            }
            catch
            {
                return null;
            }
        }

        public DataTable selectULQAD(string id)
        {
            try
            {
                string strSql = "sp_UL_selectULQAD";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            }
            catch
            {
                return null;
            }
        }
        public DataTable selectULDoc(string id)
        {
            try
            {
                string strSql = "sp_UL_selectDoc";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            }
            catch
            {
                return null;
            }
        }
        public string selectULproject(string id,string mid)
        {
            try
            {
                string strSql = "sp_UL_selectULmstr";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@mid", mid);
               string project = "";

               SqlDataReader read =  SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
               while (read.Read())
               {
                   project = read["UL_Project"].ToString().Trim();
               }
               read.Close();

               return project;

            }
            catch
            {
                return null;
            }
        }


        public SqlDataReader selectULdet(string id)
        {
            try
            {
                string strSql = "sp_UL_selectULdet";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);
                

                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                

            }
            catch
            {
                return null;
            }
        }

        public SqlDataReader selectULid(string project)
        {
            try
            {
                string strSql = "sp_UL_selectULid";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@project", project);


                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);


            }
            catch
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 记录每个进入RDW_DetailEdit的日志表（哪个人在什么时候进过这个步骤）
        /// </summary>
        /// <param name="did"></param>
        /// <param name="mid"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        public void InsertEnterLog(string did,string mid ,string uID,string uName)
        {
            string sql = "sp_RDW_insertEnterLog";
            try
            {
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@did", did);
                parm[1] = new SqlParameter("@uID", uID);
                parm[2] = new SqlParameter("@uName", uName);
                parm[3] = new SqlParameter("@mid", mid);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, parm);
            }
            catch
            {
                return ;
            }
        }

        public string InitialPPAInfo(string strMID,int uID,string uName)
        {
            //根据strMID初始化PPA相关数据
            string sql = "sp_RDW_initailPPAInfo";
            try
            {
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@mstrID", strMID);
                parm[1] = new SqlParameter("@uID", uID);
                parm[2] = new SqlParameter("@uName", uName);
                parm[3] = new SqlParameter("@retValue", DbType.Int32);
                parm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, parm);
                return parm[3].Value.ToString();
            }
            catch
            {
                return "0";
            }
            
        }

        public bool existsProdUser(string uid)
        {
            string sql = "sp_proc_existsProdUser";
            SqlParameter param = new SqlParameter("@uid", uid);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,sql,param));
        }

        public bool CheckMessageExists(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_rdw_checkRdwDetMassageExist";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@userId", strUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public int CheckExistsPPA(string ppa)
        {
            string str = "sp_rdw_CheckExistsPPA";
            SqlParameter param = new SqlParameter("@ppa", ppa);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
        }

        public bool CheckParentID(string did, string uid)
        {
            string str = "sp_prod_CheckParentID";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@did", did);
            param[1] = new SqlParameter("@uid", uid);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
        }

    }
}