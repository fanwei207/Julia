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
using adamFuncs;


/// <summary>
/// Summary description for RT
/// </summary>
/// 
namespace RT_WorkFlow
{
    public class RT
    {
        adamClass adam = new adamClass();
        string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];

        public RT()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public IList<RT_Header> SelectRDWList(string strProj, string strProd, string strStart, string strMessage, string strStatus, string strUID, bool ViewAll)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwHeaderList";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@proj", strProj);
                sqlParam[1] = new SqlParameter("@prod", strProd);
                sqlParam[2] = new SqlParameter("@start", strStart);
                sqlParam[3] = new SqlParameter("@msg", strMessage);
                sqlParam[4] = new SqlParameter("@status", strStatus);
                sqlParam[5] = new SqlParameter("@uid", strUID);
                sqlParam[6] = new SqlParameter("@all", ViewAll == true ? 1 : 0);

                IList<RT_Header> RDWHeader = new List<RT_Header>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RT_Header rh = new RT_Header();
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
                    if (rh.RDW_FinishDate == "1900-01-01")
                    {
                        rh.RDW_FinishDate = "";
                    }
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
        /// 删除已有信息
        /// </summary>
        /// <param name="strMstrID"></param>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteRDWHeader(string strMstrID)
        {
            try
            {
                string strSql = "sp_RT_DeleteRDWHeader";

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
                string strSql = "sp_RT_DeleteRDWHeaderCheck";

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
        public int InsertRDWHeader(RT_Header rh)
        {
            try
            {
                string strSql = "sp_RT_InsertRDWHeader";

                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@proj", rh.RDW_Project.Trim());
                sqlParam[1] = new SqlParameter("@code", rh.RDW_ProdCode.Trim());
                sqlParam[2] = new SqlParameter("@desc", rh.RDW_ProdDesc.Trim());
                sqlParam[3] = new SqlParameter("@stand", rh.RDW_Standard.Trim());
                sqlParam[4] = new SqlParameter("@start", rh.RDW_StartDate.Trim());
                sqlParam[5] = new SqlParameter("@end", rh.RDW_EndDate.Trim());
                sqlParam[6] = new SqlParameter("@memo", rh.RDW_Memo.Trim());
                sqlParam[7] = new SqlParameter("@uid", rh.RDW_CreatedBy.ToString());

                return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }

        /// <summary>
        /// 获得RDW抬头
        /// </summary>
        /// <param name="strID"></param>
        /// <returns>返回RDW抬头对象列表</returns>
        public RT_Header SelectRDWHeader(string strID)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwHeader";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", strID);

                RT_Header rh = new RT_Header();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    rh.RDW_Project = reader["Project"].ToString();
                    rh.RDW_ProdCode = reader["ProdCode"].ToString();
                    rh.RDW_ProdDesc = reader["ProdDesc"].ToString();
                    rh.RDW_StartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StartDate"]));
                    rh.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["EndDate"]));
                    rh.RDW_Status = reader["Status"].ToString();
                    rh.RDW_Memo = reader["Memo"].ToString();
                    rh.RDW_Standard = reader["Standard"].ToString();
                    rh.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    rh.RDW_PartnerName = "Project Leader:" + reader["PartnerName"].ToString();
                    rh.RDW_Partner = reader["Partner"].ToString();
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
        /// 更新RDW抬头
        /// </summary>
        /// <param name="rh"></param>
        /// <returns>返回是否更新成功</returns>
        public bool UpdateRDWHeader(RT_Header rh)
        {
            try
            {
                string strSql = "sp_RT_UpdateRDWHeader";

                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@mid", rh.RDW_MstrID.ToString());
                sqlParam[1] = new SqlParameter("@code", rh.RDW_ProdCode.Trim());
                sqlParam[2] = new SqlParameter("@desc", rh.RDW_ProdDesc.Trim());
                sqlParam[3] = new SqlParameter("@memo", rh.RDW_Memo.Trim());
                sqlParam[4] = new SqlParameter("@start", rh.RDW_StartDate.Trim());
                sqlParam[5] = new SqlParameter("@end", rh.RDW_EndDate.Trim());
                sqlParam[6] = new SqlParameter("@stand", rh.RDW_Standard.Trim());
                sqlParam[7] = new SqlParameter("@proj", rh.RDW_Project.Trim());

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
        public IList<RT_Detail> SelectRDWDetailList(string strID)
        {
            try
            {
                string strEvaluator = string.Empty;
                string strEvaluatorName = string.Empty;
                string strPartner = string.Empty;
                string strPartnerName = string.Empty;

                string strSql = "sp_RT_SelectRdwDetailList";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@mid", strID);

                IList<RT_Detail> RDWDetail = new List<RT_Detail>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RT_Detail rd = new RT_Detail();
                    rd.RDW_MstrID = Convert.ToInt32(reader["MstrID"]);
                    rd.RDW_DetID = Convert.ToInt32(reader["DetID"]);
                    rd.RDW_StepName = reader["StepName"].ToString();
                    rd.RDW_StepDesc = reader["StepDesc"].ToString();
                    rd.RDW_Sort = Convert.ToInt32(reader["Sort"]);
                    rd.RDW_Status = Convert.ToInt32(reader["Status"]);
                    rd.RDW_Creater = reader["Creater"].ToString();
                    rd.RDW_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    rd.RDW_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                    rd.RDW_StepStartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepStartDate"]));
                    rd.RDW_StepEndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepEndDate"]));
                    rd.RDW_StepFinishDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepFinishDate"]));
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

                    strSql = "sp_RT_SelectRdwDetailEvaluater";
                    sqlParam[0] = new SqlParameter("@did", rd.RDW_DetID.ToString());

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

                    strSql = "sp_RT_SelectRdwDetailPartner";
                    sqlParam[0] = new SqlParameter("@did", rd.RDW_DetID.ToString());

                    strPartner = "";
                    strPartnerName = "";
                    SqlDataReader readerPartner = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                    while (readerPartner.Read())
                    {
                        strPartner += readerPartner["Partner"].ToString() + ";";
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
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 删除已有步骤
        /// </summary>
        /// <param name="strDetID"></param>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteRDWDetail(string strDetID)
        {
            try
            {
                string strSql = "sp_RT_DeleteRDWDetail";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDetID);

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
        public bool CheckDeleteRDWDetail(string strDetID)
        {
            try
            {
                string strSql = "sp_RT_DeleteRDWDetailCheck";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDetID);

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
        /// <param name="strID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckViewRDWDetail(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_RT_ViewRDWDetailCheck";

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
                string strSql = "sp_RT_ViewRDWDetailEditCheck";

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
                string strSql = "sp_RT_EvaluateRDWDetailCheck";

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
                string strSql = "sp_RT_CancelEvaluateRDWDetailCheck";

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
                string strSql = "sp_RT_SelectRdwDetailCurrent";

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
        /// 获得评审明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns>返回评审明细对象列表</returns>
        public RT_Detail SelectRDWDetailEdit(string strDID)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwDetailEdit";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                RT_Detail rd = new RT_Detail();
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
                    rd.RDW_EndDate = string.Format("{0:yyyy-MM-dd}", reader["EndDate"]);
                    rd.RDW_StepStartDate = string.Format("{0:yyyy-MM-dd}", reader["StepStartDate"]);
                    rd.RDW_StepEndDate = string.Format("{0:yyyy-MM-dd}", reader["StepEndDate"]);
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
        public bool UpdateEvaluateRDWDetail(string strMID, string strDID, string strUID, string strNotes)
        {
            try
            {
                string strSql = "sp_RT_UpdateEvaluateRDWDetail";

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@mid", strMID);
                sqlParam[1] = new SqlParameter("@did", strDID);
                sqlParam[2] = new SqlParameter("@uid", strUID);
                sqlParam[3] = new SqlParameter("@notes", strNotes);

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
        public bool UploadFile(string strFile, byte[] byteFile, string strType, string strUID, string strUname, string strDID)
        {
            try
            {
                string strSql = "sp_RT_UploadFile";

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@name", strFile);
                sqlParam[1] = new SqlParameter("@data", byteFile);
                sqlParam[2] = new SqlParameter("@type", strType);
                sqlParam[3] = new SqlParameter("@uid", strUID);
                sqlParam[4] = new SqlParameter("@uname", strUname);
                sqlParam[5] = new SqlParameter("@did", strDID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得上传文件清单
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns>返回上传文件对象列表</returns>
        public IList<RT_Detail_Docs> SelectRDWDetailDocs(string strDID)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwDetailDocs";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                IList<RT_Detail_Docs> DetailDocs = new List<RT_Detail_Docs>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RT_Detail_Docs rdd = new RT_Detail_Docs();
                    rdd.RDW_DocsID = Convert.ToInt32(reader["DocsID"]);
                    rdd.RDW_DetID = Convert.ToInt32(reader["DetID"]);
                    rdd.RDW_FileName = reader["FileName"].ToString();
                    rdd.RDW_FileType = reader["FileType"].ToString();
                    rdd.RDW_Uploader = reader["Uploader"].ToString();
                    rdd.RDW_UploaderID = reader["UploaderID"].ToString();
                    rdd.RDW_UploadDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["UploadDate"]));
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
        public IList<RT_Detail> SelectRDWDetailMessage(string strDID)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwDetailMessage";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@did", strDID);

                IList<RT_Detail> RDWDetail = new List<RT_Detail>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RT_Detail rd = new RT_Detail();
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

        /// <summary>
        /// 保存明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strNotes"></param>
        /// <param name="strUID"></param>
        /// <returns>返回是否保存成功</returns>
        public bool SaveDetailEdit(string strDID, string strNotes, string strUID)
        {
            try
            {
                string strSql = "sp_RT_SaveDetailEdit";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@notes", strNotes);
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
        /// 获取指定文档
        /// </summary>
        /// <param name="strDocID"></param>
        /// <returns></returns>
        public SqlDataReader SelectRDWDetailDocView(string strDocID)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwDetailDocView";

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
                string strSql = "sp_RT_DeleteRdwDetailDocs";

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
                string strSql = "sp_RT_CheckRDWHeaderCancel";

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
                string strSql = "sp_RT_UpdateRDWHeaderCancel";

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
        /// 判断是否可以完成明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        public bool CheckFinishRDWDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_RT_FinishRDWDetailCheck";

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
                string strSql = "sp_RT_SelectRDWDetailPartner";

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
                string strSql = "sp_RT_SelectSenderEmail";

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
        public bool UpdateCancelEvaluateRDWDetail(string strDID, string strUserID, string strUID, string strUname, bool isUpdate)
        {
            try
            {
                string strSql = string.Empty;

                if (isUpdate)
                {
                    strSql = "sp_RT_UpdateCancelEvaluateRDWDetailMsg";
                }
                else
                {
                    strSql = "sp_RT_UpdateCancelEvaluateRDWDetail";
                }

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@userid", strUserID);
                sqlParam[2] = new SqlParameter("@uid", strUID);
                sqlParam[3] = new SqlParameter("@uname", strUname);

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
                string strSql = "sp_RT_SelectRDWDetailMemberEmail";

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
        public bool UpdateFinishRDWDetail(string strDID, string strUID, string strNotes)
        {
            try
            {
                string strSql = "sp_RT_UpdateFinishRDWDetail";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@uid", strUID);
                sqlParam[2] = new SqlParameter("@notes", strNotes);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
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
                string strSql = "sp_RT_EvaluateEmailRDWDetailCheck";

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
                string strSql = "sp_RT_SelectRDWDetailEvaluateEmail";

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
                string strSql = "sp_RT_SelectRdwHeaderList";
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
        public IList<RT_Header> SelectRDWRptList(string strProj, string strProd, string strStart, string strStatus, string strUID, bool ViewAll)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwRptList";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@proj", strProj);
                sqlParam[1] = new SqlParameter("@prod", strProd);
                sqlParam[2] = new SqlParameter("@start", strStart);
                sqlParam[3] = new SqlParameter("@status", strStatus);
                sqlParam[4] = new SqlParameter("@uid", strUID);
                sqlParam[5] = new SqlParameter("@all", ViewAll == true ? 1 : 0);

                IList<RT_Header> RDWHeader = new List<RT_Header>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RT_Header rh = new RT_Header();
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
                    if (rh.RDW_FinishDate == "1900-01-01")
                    {
                        rh.RDW_FinishDate = "";
                    }
                    if (rh.RDW_CreatedDate == "1900-01-01")
                    {
                        rh.RDW_CreatedDate = "";
                    }
                    rh.RDW_CurrMstrStep = "id=" + reader["ID"].ToString() + "&mid=" + reader["ID"].ToString() + "&did=" + reader["CurrStepID"].ToString();
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
        public IList<RT_Header> SelectRDWQueryList(string strProj, string strProd, string strStart, string strStatus, string strUID, bool ViewAll, bool ShowAll)
        {
            try
            {
                string strSql = "sp_RT_SelectRdwQueryList";
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@proj", strProj);
                sqlParam[1] = new SqlParameter("@prod", strProd);
                sqlParam[2] = new SqlParameter("@start", strStart);
                sqlParam[3] = new SqlParameter("@status", strStatus);
                sqlParam[4] = new SqlParameter("@uid", strUID);
                sqlParam[5] = new SqlParameter("@viewall", ViewAll == true ? 1 : 0);
                sqlParam[6] = new SqlParameter("@showall", ShowAll == true ? 1 : 0);

                IList<RT_Header> RDWHeader = new List<RT_Header>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    RT_Header rh = new RT_Header();
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
                    if (rh.RDW_FinishDate == "1900-01-01")
                    {
                        rh.RDW_FinishDate = "";
                    }
                    if (rh.RDW_CreatedDate == "1900-01-01")
                    {
                        rh.RDW_CreatedDate = "";
                    }
                    rh.RDW_CurrMstrStep = "id=" + reader["ID"].ToString() + "&mid=" + reader["ID"].ToString();

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
    }
}
