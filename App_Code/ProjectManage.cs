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

namespace PM
{
    /// <summary>
    /// Summary description for ProjectManage
    /// </summary>
    public class ProjectManage
    {
        adamClass adam = new adamClass();
        string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_PM"];

        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public ProjectManage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 获得PM清单
        /// </summary>
        /// <param name="strProjName">项目名称</param>
        /// <param name="strProjCode">项目编号</param>
        /// <param name="strStart">开始日期</param>
        /// <param name="strStatus">项目状态</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>返回PM清单对象列表</returns>
        public IList<PM_Header> SelectPMList(string strProjName, string strProjCode, string strProjectDate, string strStatus, string struID, string strPlant)
        {
            try
            {
                string strSql = "sp_PM_SelectPMHeaderList";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@name", strProjName);
                sqlParam[1] = new SqlParameter("@code", strProjCode);
                sqlParam[2] = new SqlParameter("@date", strProjectDate);
                sqlParam[3] = new SqlParameter("@status", strStatus);
                sqlParam[4] = new SqlParameter("@uid", struID);
                sqlParam[5] = new SqlParameter("@plant", strPlant);

                IList<PM_Header> PMHeader = new List<PM_Header>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    PM_Header ph = new PM_Header();
                    ph.PM_ProjName = reader["ProjName"].ToString();
                    ph.PM_ProjCode = reader["ProjCode"].ToString();
                    ph.PM_ProjDesc = reader["ProjDesc"].ToString();
                    ph.PM_ProjectDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["ProjectDate"]));
                    ph.PM_CloseDate = reader["CloseDate"].ToString().Length == 0 ? "" : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CloseDate"]));
                    ph.PM_Status = reader["Status"].ToString();
                    ph.PM_Creater = reader["Creater"].ToString();
                    ph.PM_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                    ph.PM_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    ph.PM_Memo = reader["Memo"].ToString();
                    ph.PM_Partner = reader["Partner"].ToString();
                    ph.PM_Content = reader["Content"].ToString();
                    ph.PM_MstrID = Convert.ToInt32(reader["ID"]);
                    ph.PM_Dept = reader["Dept"].ToString();
                    PMHeader.Add(ph);
                }
                reader.Close();
                return PMHeader;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 更新抬头状态Cancel
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>True/False</returns>
        public bool UpdatePMHeaderCancel(string strMID)
        {
            try
            {
                string strSql = "sp_PM_UpdatePMHeaderCancel";

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
        /// 判断是否可以删除
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckDeletePMHeader(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_DeletePMHeaderCheck";

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
        /// 判断是否可以取消
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckCancelPMHeader(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_CheckPMHeaderCancel";

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
        /// 删除已有信息
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>True/False</returns>
        public bool DeletePMHeader(string strMID)
        {
            try
            {
                string strSql = "sp_PM_DeletePMHeader";

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
        /// 新增PM_Mstr
        /// </summary>
        /// <param name="ph">PM_Header</param>
        /// <returns>ID</returns>
        public int InsertPMHeader(PM_Header ph)
        {
            try
            {
                string strSql = "sp_PM_InsertPMHeader";

                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@name", ph.PM_ProjName.Trim());
                sqlParam[1] = new SqlParameter("@code", ph.PM_ProjCode.Trim());
                sqlParam[2] = new SqlParameter("@desc", ph.PM_ProjDesc.Trim());
                sqlParam[3] = new SqlParameter("@content", ph.PM_Content.Trim());
                sqlParam[4] = new SqlParameter("@date", ph.PM_ProjectDate.Trim());
                sqlParam[5] = new SqlParameter("@fin", ph.PM_FinDate.ToString());
                sqlParam[6] = new SqlParameter("@human", ph.PM_HumanFee.ToString());
                sqlParam[7] = new SqlParameter("@equip", ph.PM_EquipFee.ToString());
                sqlParam[8] = new SqlParameter("@soft", ph.PM_SoftFee.ToString());
                sqlParam[9] = new SqlParameter("@memo", ph.PM_Memo.Trim());
                sqlParam[10] = new SqlParameter("@uid", ph.PM_CreatedBy.ToString());


                return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }

        /// <summary>
        /// 判断是否可以查看明细
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckViewPMDetail(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_ViewPMDetailCheck";

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
        /// 获得PM抬头
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>返回PM抬头对象列表</returns>
        public PM_Header SelectPMHeader(string strMID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMHeader";
                SqlParameter sqlParam = new SqlParameter("@mid", strMID);

                PM_Header ph = new PM_Header();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    ph.PM_ProjName = reader["ProjName"].ToString();
                    ph.PM_ProjCode = reader["ProjCode"].ToString();
                    ph.PM_ProjDesc = reader["ProjDesc"].ToString();
                    ph.PM_ProjectDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["ProjectDate"]));
                    ph.PM_Status = reader["Status"].ToString();
                    ph.PM_Memo = reader["Memo"].ToString();
                    ph.PM_Content = reader["Content"].ToString();
                    ph.PM_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    ph.PM_PartnerName = "责任人:" + reader["PartnerName"].ToString();
                    ph.PM_Partner = reader["Partner"].ToString();
                    ph.PM_FinDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["FinishDate"]));
                    ph.PM_HumanFee = reader["HumanFee"].ToString();
                    ph.PM_EquipFee = reader["EquipFee"].ToString();
                    ph.PM_SoftFee = reader["SoftFee"].ToString();
                }
                reader.Close();
                return ph;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得清单明细
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>返回明细对象列表</returns>
        public IList<PM_Detail> SelectPMDetailList(string strMID)
        {
            try
            {
                string strEvaluator = string.Empty;
                string strEvaluatorName = string.Empty;
                string strPartner = string.Empty;
                string strPartnerName = string.Empty;

                string strSql = "sp_PM_SelectPMDetailList";
                SqlParameter sqlParam = new SqlParameter("@mid", strMID);

                IList<PM_Detail> PMDetail = new List<PM_Detail>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    PM_Detail pd = new PM_Detail();
                    pd.PM_MstrID = Convert.ToInt32(reader["MstrID"]);
                    pd.PM_DetID = Convert.ToInt32(reader["DetID"]);
                    pd.PM_StepName = reader["StepName"].ToString();
                    pd.PM_StepDesc = reader["StepDesc"].ToString();
                    pd.PM_Sort = Convert.ToInt32(reader["Sort"]);
                    pd.PM_Status = Convert.ToInt32(reader["Status"]);
                    pd.PM_Creater = reader["Creater"].ToString();
                    pd.PM_CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                    pd.PM_CreatedDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                    pd.PM_StepStartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepStartDate"]));
                    pd.PM_StepEndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["StepEndDate"]));
                    if (pd.PM_StepStartDate == "1900-01-01")
                    {
                        pd.PM_StepStartDate = "";
                    }
                    if (pd.PM_StepEndDate == "1900-01-01")
                    {
                        pd.PM_StepEndDate = "";
                    }

                    strSql = "sp_PM_SelectPMDetailEvaluater";
                    sqlParam = new SqlParameter("@did", pd.PM_DetID.ToString());

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
                        pd.PM_Evaluater = strEvaluatorName.Substring(0, strEvaluatorName.Length - 4);
                        pd.PM_EvaluaterID = ";" + strEvaluator;
                    }
                    else
                    {
                        pd.PM_Evaluater = strEvaluatorName;
                        pd.PM_EvaluaterID = strEvaluator;
                    }

                    strSql = "sp_PM_SelectPMDetailPartner";
                    sqlParam = new SqlParameter("@did", pd.PM_DetID.ToString());

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
                        pd.PM_Partner = ";" + strPartner;
                        pd.PM_PartnerName = strPartnerName.Substring(0, strPartnerName.Length - 4);
                    }
                    else
                    {
                        pd.PM_Partner = strPartner;
                        pd.PM_PartnerName = strPartnerName;
                    }

                    PMDetail.Add(pd);
                }
                reader.Close();
                return PMDetail;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 更新PM抬头
        /// </summary>
        /// <param name="ph">PM_Header</param>
        /// <returns>True/False</returns>
        public bool UpdatePMHeader(PM_Header ph)
        {
            try
            {
                string strSql = "sp_PM_UpdatePMHeader";

                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@mid", ph.PM_MstrID.ToString());
                sqlParam[1] = new SqlParameter("@code", ph.PM_ProjCode.Trim());
                sqlParam[2] = new SqlParameter("@desc", ph.PM_ProjDesc.Trim());
                sqlParam[3] = new SqlParameter("@memo", ph.PM_Memo.Trim());
                sqlParam[4] = new SqlParameter("@projdate", ph.PM_ProjectDate.Trim());
                sqlParam[5] = new SqlParameter("@findate", ph.PM_FinDate.Trim());
                sqlParam[6] = new SqlParameter("@content", ph.PM_Content.Trim());
                sqlParam[7] = new SqlParameter("@name", ph.PM_ProjName.Trim());
                sqlParam[8] = new SqlParameter("@human", ph.PM_HumanFee.Trim());
                sqlParam[9] = new SqlParameter("@equip", ph.PM_EquipFee.Trim());
                sqlParam[10] = new SqlParameter("@soft", ph.PM_SoftFee.Trim());

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
        /// <param name="strDetID">PM_DetID</param>
        /// <returns>True/False</returns>
        public bool CheckDeletePMDetail(string strDetID)
        {
            try
            {
                string strSql = "sp_PM_DeletePMDetailCheck";

                SqlParameter sqlParam = new SqlParameter("@did", strDetID);

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
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>返回最大序号</returns>
        public int SelectDetailCurrent(string strMID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailCurrent";

                SqlParameter sqlParam = new SqlParameter("@mid", strMID);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -999;
            }
        }

        /// <summary>
        /// 删除已有步骤
        /// </summary>
        /// <param name="strDetID">PM_DetID</param>
        /// <returns>True/False</returns>
        public bool DeletePMDetail(string strDetID)
        {
            try
            {
                string strSql = "sp_PM_DeletePMDetail";

                SqlParameter sqlParam = new SqlParameter("@did", strDetID);

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
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckViewPMDetailEdit(string strMID, string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_ViewPMDetailEditCheck";

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
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckEvaluatePMDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_EvaluatePMDetailCheck";

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
        /// 判断是否可以完成明细
        /// </summary>
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckFinishPMDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_FinishPMDetailCheck";

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
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckCancelEvaluatePMDetail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_CancelEvaluatePMDetailCheck";

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
        /// 获得评审明细
        /// </summary>
        /// <param name="strDID"></param>
        /// <returns>返回评审明细对象列表</returns>
        public PM_Detail SelectPMDetailEdit(string strDID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailEdit";
                SqlParameter sqlParam = new SqlParameter("@did", strDID);

                PM_Detail pd = new PM_Detail();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    pd.PM_ProjName = reader["ProjName"].ToString();
                    pd.PM_StepName = reader["StepName"].ToString();
                    pd.PM_StepDesc = reader["StepDesc"].ToString().Replace("<br>", "\n");
                    pd.PM_ProjCode = reader["ProjCode"].ToString();
                    pd.PM_ProjDesc = reader["ProjDesc"].ToString();
                    pd.PM_Status = Convert.ToInt32(reader["Status"]);
                    pd.PM_ProjectDate = string.Format("{0:yyyy-MM-dd}", reader["ProjectDate"]);
                    pd.PM_StepStartDate = string.Format("{0:yyyy-MM-dd}", reader["StepStartDate"]);
                    pd.PM_StepEndDate = string.Format("{0:yyyy-MM-dd}", reader["StepEndDate"]);
                }
                reader.Close();
                return pd;
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
        /// <param name="strDID">PM_DetID</param>
        /// <returns>返回明细消息对象列表</returns>
        public IList<PM_Detail> SelectPMDetailMessage(string strDID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailMessage";
                SqlParameter sqlParam = new SqlParameter("@did", strDID);

                IList<PM_Detail> PMDetail = new List<PM_Detail>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    PM_Detail pd = new PM_Detail();
                    pd.PM_Message = reader["Message"].ToString();
                    PMDetail.Add(pd);
                }
                reader.Close();
                return PMDetail;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得上传文件清单
        /// </summary>
        /// <param name="strDID">PM_DetID</param>
        /// <returns>返回上传文件对象列表</returns>
        public IList<PM_Detail_Docs> SelectPMDetailDocs(string strDID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailDocs";
                SqlParameter sqlParam = new SqlParameter("@did", strDID);

                IList<PM_Detail_Docs> DetailDocs = new List<PM_Detail_Docs>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    PM_Detail_Docs pdd = new PM_Detail_Docs();
                    pdd.PM_DocsID = Convert.ToInt32(reader["DocsID"]);
                    pdd.PM_DetID = Convert.ToInt32(reader["DetID"]);
                    pdd.PM_FileName = reader["FileName"].ToString();
                    pdd.PM_FileType = reader["FileType"].ToString();
                    pdd.PM_Uploader = reader["Uploader"].ToString();
                    pdd.PM_UploaderID = reader["UploaderID"].ToString();
                    pdd.PM_UploadDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["UploadDate"]));
                    DetailDocs.Add(pdd);
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
        /// 删除上传文档
        /// </summary>
        /// <param name="strDocID">PM_DocID</param>
        /// <returns>True/False</returns>
        public bool DeletePMDetailDocs(string strDocID)
        {
            try
            {
                string strSql = "sp_PM_DeletePMDetailDocs";

                SqlParameter sqlParam = new SqlParameter("@docid", strDocID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 保存明细
        /// </summary>
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strNotes">备注</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool SaveDetailEdit(string strDID, string strNotes, string strUID)
        {
            try
            {
                string strSql = "sp_PM_SaveDetailEdit";

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
        /// 获得该步骤所有涉及用户Email
        /// </summary>
        /// <param name="strDID">PM_DetID</param>
        /// <returns>IDataReader</returns>
        public IDataReader SelectPMDetailMemberEmail(string strDID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailMemberEmail";

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
        /// 获取用户邮箱
        /// </summary>
        /// <param name="strUID">PM_DetID</param>
        /// <returns>string</returns>
        public string getUserEmail(string strUID)
        {
            try
            {
                string strSql = "sp_PM_SelectSenderEmail";

                SqlParameter sqlParam = new SqlParameter("@uid", strUID);

                return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return "";
            }
        }

        /// <summary>
        /// 保存评审
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <param name="strNotes">备注</param>
        /// <returns>True/False</returns>
        public bool UpdateEvaluatePMDetail(string strMID, string strDID, string strUID, string strNotes)
        {
            try
            {
                string strSql = "sp_PM_UpdateEvaluatePMDetail";

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
        /// <param name="strFile">文件名</param>
        /// <param name="byteFile">文件大小</param>
        /// <param name="strType">文件类型</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <param name="strDID">PM_DetID</param>
        /// <returns>True/False</returns>
        public bool UploadFile(string strFile, byte[] byteFile, string strType, string strUID, string strUname, string strDID)
        {
            try
            {
                string strSql = "sp_PM_UploadFile";

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
        /// 完成步骤任务
        /// </summary>
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <param name="strNotes">备注</param>
        /// <returns>True/False</returns>
        public bool UpdateFinishPMDetail(string strDID, string strUID, string strNotes)
        {
            try
            {
                string strSql = "sp_PM_UpdateFinishPMDetail";

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
        /// <param name="strDID">PM_DetID</param>
        /// <returns>True/False</returns>
        public bool EvaluateEmailPMDetailCheck(string strDID)
        {
            try
            {
                string strSql = "sp_PM_EvaluateEmailPMDetailCheck";

                SqlParameter sqlParam = new SqlParameter("@did", strDID);

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
        /// <param name="strDID">PM_DetID</param>
        /// <returns>IDataReader</returns>
        public IDataReader SelectPMDetailEvaluateEmail(string strDID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailEvaluateEmail";

                SqlParameter sqlParam = new SqlParameter("@did", strDID);

                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获取指定文档
        /// </summary>
        /// <param name="strDocID">PM_DocID</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectPMDetailDocView(string strDocID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailDocView";

                SqlParameter sqlParam = new SqlParameter("@docid", strDocID);

                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得进展情况说明
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>进展情况说明对象列表</returns>
        public IList<PM_Message> SelectMessageList(string strMID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMMessageList";
                SqlParameter sqlParam = new SqlParameter("@mid", strMID);

                IList<PM_Message> Message = new List<PM_Message>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    PM_Message msg = new PM_Message();
                    msg.PM_ReportDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["CreatedDate"]));
                    msg.PM_CreatedBy = Convert.ToInt64(reader["CreatedBy"]);
                    msg.PM_Reporter = reader["Reporter"].ToString();
                    msg.Message = reader["Message"].ToString();
                    Message.Add(msg);
                }
                reader.Close();
                return Message;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得上传文件清单
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>返回上传文件对象列表</returns>
        public IList<PM_Docs> SelectPMDocs(string strMID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDocs";
                SqlParameter sqlParam = new SqlParameter("@mid", strMID);

                IList<PM_Docs> Docs = new List<PM_Docs>();
                IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    PM_Docs pd = new PM_Docs();
                    pd.PM_DocsID = Convert.ToInt32(reader["DocsID"]);
                    pd.PM_MstrID = Convert.ToInt32(reader["MstrID"]);
                    pd.PM_FileName = reader["FileName"].ToString();
                    pd.PM_FileType = reader["FileType"].ToString();
                    pd.PM_Uploader = reader["Uploader"].ToString();
                    pd.PM_UploaderID = reader["UploaderID"].ToString();
                    pd.PM_UploadDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(reader["UploadDate"]));
                    Docs.Add(pd);
                }
                reader.Close();
                return Docs;
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
        /// <param name="strDocID">PM_DocID</param>
        /// <returns>True/False</returns>
        public bool DeletePMDocs(string strDocID)
        {
            try
            {
                string strSql = "sp_PM_DeletePMDocs";

                SqlParameter sqlParam = new SqlParameter("@docid", strDocID);

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
        /// <param name="strFile">文件名</param>
        /// <param name="byteFile">文件大小</param>
        /// <param name="strType">文件类型</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <param name="strMID">PM_MstrID</param>
        /// <returns>True/False</returns>
        public bool UploadDoc(string strFile, byte[] byteFile, string strType, string strUID, string strUname, string strMID)
        {
            try
            {
                string strSql = "sp_PM_UploadDoc";

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@name", strFile);
                sqlParam[1] = new SqlParameter("@data", byteFile);
                sqlParam[2] = new SqlParameter("@type", strType);
                sqlParam[3] = new SqlParameter("@uid", strUID);
                sqlParam[4] = new SqlParameter("@uname", strUname);
                sqlParam[5] = new SqlParameter("@mid", strMID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 保存明细
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strNotes">备注</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool SavePMMessage(string strMID, string strNotes, string strUID)
        {
            try
            {
                string strSql = "sp_PM_SavePMMessage";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@mid", strMID);
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

        public bool CloseProject(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_CloseProject";

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
        /// 判断是否可以删除
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckDeleteProject(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_CheckDeleteProject";

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
        /// 判断是否可以取消
        /// </summary>
        /// <param name="strMID">PM_MstrID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool CheckCancelProject(string strMID, string strUID)
        {
            try
            {
                string strSql = "sp_PM_CheckCancelProject";

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
        /// 获取指定文档
        /// </summary>
        /// <param name="strDocID">PM_DocID</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectPMDocView(string strDocID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDocView";

                SqlParameter sqlParam = new SqlParameter("@docid", strDocID);

                return SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获取指定步骤参与者数据集
        /// </summary>
        /// <param name="strDID">PM_DetID</param>
        /// <returns>DataSet</returns>
        public DataSet SelectPMDetailPartner(string strDID)
        {
            try
            {
                string strSql = "sp_PM_SelectPMDetailPartner";

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
        /// 
        /// </summary>
        /// <param name="strDID"></param>
        /// <param name="strUID"></param>
        /// <returns></returns>
        //

        /// <summary>
        /// 取消评审
        /// </summary>
        /// <param name="strDID">PM_DetID</param>
        /// <param name="strUserID">PartnerID</param>
        /// <param name="strUID">Session["uID"]</param>
        /// <param name="strUname">PartnerName</param>
        /// <param name="isUpdate"></param>
        /// <returns>True/False</returns>
        public bool UpdateCancelEvaluatePMDetail(string strDID, string strUserID, string strUID, string strUname, string strPartner, bool isUpdate)
        {
            try
            {
                string strSql = string.Empty;

                if (isUpdate)
                {
                    strSql = "sp_PM_UpdateCancelEvaluatePMDetailMsg";
                }
                else
                {
                    strSql = "sp_PM_UpdateCancelEvaluatePMDetail";
                }

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@did", strDID);
                sqlParam[1] = new SqlParameter("@userid", strUserID);
                sqlParam[2] = new SqlParameter("@uid", strUID);
                sqlParam[3] = new SqlParameter("@uname", strUname);
                sqlParam[4] = new SqlParameter("@partner", strPartner);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
    }
}