using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using CommClass;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

namespace SampleManagement
{
    /// <summary>
    /// 系统角色 -- 引用供应链系统上的
    /// </summary>
    public enum SysRole
    {
        /// <summary>
        /// 员工
        /// </summary>
        Owner = 65,
        /// <summary>
        /// 供应商
        /// </summary>
        Supplier = 64,
        /// <summary>
        /// 客户
        /// </summary>
        Customer = 63
    }
    /// <summary>
    /// Summary description for Sample
    /// </summary>
    public class Sample
    {

        string strConn = ConfigurationManager.AppSettings["SqlConn.TCPC_Supplier"];
        public Sample()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 单据号
        /// </summary>
        private string _bos_Nbr;
        public string bos_Nbr
        {
            get { return _bos_Nbr; }
            set { _bos_Nbr = value; }
        }

        /// <summary>
        /// 单据 行号
        /// </summary>
        private int _bos_detLine;
        public int bos_detLine
        {
            get { return _bos_detLine; }
            set { _bos_detLine = value; }
        }

        ///// <summary>
        ///// 单据行 Item Code
        ///// </summary>
        //private int _bos_detCode;
        //public int _bos_detCode
        //{
        //    get { return _bos_detLine; }
        //    set { _bos_detLine = value; }
        //}

        /// <summary>
        /// 获取样品单据号
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <returns></returns>
        public DataTable getBosMstr(string strBosnbr, string strVend)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosMstr", param).Tables[0];

        }

        /// <summary>
        /// 获取样品单据号
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <returns></returns>
        public DataTable getBosMstr(string strBosnbr, string strVend, string vendconfim)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@bos_vendconfim", vendconfim);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosMstr", param).Tables[0];
        }

        /// <summary>
        /// 获取样品单据号,加上生成日期选择
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <param name="createdDate1"></param>
        /// <param name="createdDate2"></param>
        /// <returns></returns>
        public DataTable getBosMstr(string strBosnbr, string strVend, DateTime createdDate1, DateTime createdDate2)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@createdDate1", createdDate1);
            param[3] = new SqlParameter("@createdDate2", createdDate2);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosMstr", param).Tables[0];
        }

        /// <summary>
        /// 获取样品单据号,加上生成日期选择
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <param name="vendconfim"></param>
        /// <param name="createdDate1"></param>
        /// <param name="createdDate2"></param>
        /// <returns></returns>
        public DataTable getBosMstr(string strBosnbr, string strVend, string vendconfim, DateTime createdDate1, DateTime createdDate2)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@bos_vendconfim", vendconfim);
            param[3] = new SqlParameter("@createdDate1", createdDate1);
            param[4] = new SqlParameter("@createdDate2", createdDate2);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosMstr", param).Tables[0];
        }

        /// <summary>
        /// 获取样品单据号，通过项目步骤id号
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <param name="createdDate1"></param>
        /// <param name="createdDate2"></param>
        /// <param name="strDid">项目步骤Id号</param>
        /// <returns></returns>
        public DataTable getBosMstrByDid(string strBosnbr, string strVend, DateTime createdDate1, DateTime createdDate2, int strDid)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@createdDate1", createdDate1);
            param[3] = new SqlParameter("@createdDate2", createdDate2);
            param[4] = new SqlParameter("@rdw_Did", strDid);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosMstrByDid", param).Tables[0];
        }

        /// <summary>
        /// 某一单据号的样品清单
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <returns></returns>
        public DataTable getBosDet(string strBosnbr)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosDetMstr", param).Tables[0];
        }

        /// <summary>
        /// 新增打样单
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <param name="strVendName"></param>
        /// <param name="strRmks"></param>
        /// <param name="UserId"></param>
        /// <returns>新生成的打样单号</returns>
        public String addBosNbr(string strBosnbr, string strVend, string strVendName, string strRmks, int UserId)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@bos_vendName", strVendName);
            param[3] = new SqlParameter("@bos_rmks", strRmks);
            param[4] = new SqlParameter("@bos_createdby", UserId);

            return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_insertBosMstr", param));
        }

        /// <summary>
        ///  新增打样单,与项目管理关联的
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <param name="strVendName"></param>
        /// <param name="strRmks"></param>
        /// <param name="UserId"></param>
        /// <param name="strRDWDid">项目管理步骤 id</param>
        /// <returns>新生成的打样单号</returns>
        public string addBosNbr(string strBosnbr, string strVend, string strVendName, string strRmks, int UserId, int RDWDid)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@bos_vendName", strVendName);
            param[3] = new SqlParameter("@bos_rmks", strRmks);
            param[4] = new SqlParameter("@bos_createdby", UserId);
            param[5] = new SqlParameter("@bos_RDWDid", RDWDid);

            return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_insertBosMstr", param));
        }

        public bool updateBosNbr(string strBosnbr, string strVend, string strVendName, string strRmks)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@bos_vendName", strVendName);
            param[3] = new SqlParameter("@bos_rmks", strRmks);

            return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_updateBosMstr", param));
        }

        public bool addbosDet(string strbosnbr, int ibosdetline, string strbosdetCode, string strbosdetQad, float strbosdetQty, string strbosdetRmks, string strbosdetReqDate)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@bos_nbr", strbosnbr);
            param[1] = new SqlParameter("@bos_detline", ibosdetline);
            param[2] = new SqlParameter("@bos_detCode", strbosdetCode);
            param[3] = new SqlParameter("@bos_detQad", strbosdetQad);
            param[4] = new SqlParameter("@bos_detQty", strbosdetQty);
            param[5] = new SqlParameter("@bos_detRmks", strbosdetRmks);
            param[6] = new SqlParameter("@bos_detReqDate", strbosdetReqDate);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_insertBosDetMstr", param));
        }

        public bool deleteBos(string strBosnbr)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_deleteBosMstr", param));
        }

        public bool deleteBosDet(string strBosnbr, string strBosdetLine)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_detline", strBosdetLine);

            return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_deleteBosdetMstr", param));
        }

        public bool updateBosdet(string strbosnbr, int bosLine, string strbosdetQty, string strbosdetDoc, string strbosdetrmks, string strbosdetrequireDate)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@bos_nbr", strbosnbr);
            param[1] = new SqlParameter("@bos_detline", bosLine);
            param[2] = new SqlParameter("@bos_detqty", strbosdetQty);
            param[3] = new SqlParameter("@bos_detdoc", strbosdetDoc);
            param[4] = new SqlParameter("@bos_detrmks", strbosdetrmks);
            param[5] = new SqlParameter("@bos_detreqDate", strbosdetrequireDate);

            return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_updateBosdetMstr", param));
        }

        public bool updateBosdet(string strbosnbr, int bosLine, string strbosdetcode)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@bos_nbr", strbosnbr);
            param[1] = new SqlParameter("@bos_detline", bosLine);
            param[2] = new SqlParameter("@bos_detcode", strbosdetcode);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_updateBosdetMstrcode", param));
        }

        public string ConfirmExistsCode(string strCode)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@strCode", strCode);
            param[1] = new SqlParameter("@strQAD", SqlDbType.VarChar, 16);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_ConfirmExistsCode", param);
            return param[1].Value.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable getTypeInfo()
        {
            String strSql = "Select typeid, typename From QADDOC.qaddoc.dbo.DocumentType where isDeleted Is Null  order by typename";
            return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql).Tables[0];
        }

        public DataTable getCategoryInfo(string strTypeID)
        {
            String strSql = "Select  cateid,  catename From QADDOC.qaddoc.dbo.DocumentCategory  Where  catename Is Not Null And typeId = '" + strTypeID + "' order by catename ";
            return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql).Tables[0];
        }

        public DataTable getDocbyType(int iTypeid, int iCategoryid, string strKeyWordSearch)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@typeid", iTypeid);
            param[1] = new SqlParameter("@cateid", iCategoryid);
            param[2] = new SqlParameter("@keyword", strKeyWordSearch);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosallDoc", param).Tables[0];
        }

        public DataTable getBosDetDoc(string strbosNbr, int ibosDetLine)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bos_nbr", strbosNbr);
            param[1] = new SqlParameter("@bos_det_line", ibosDetLine);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosDetDoc", param).Tables[0];
        }

        public bool addBosDetDocLink(string strbosNbr, int ibosDetLine, string docid, string docVersion)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@bos_nbr", strbosNbr);
            param[1] = new SqlParameter("@bos_det_line", ibosDetLine);
            param[2] = new SqlParameter("@bos_docid", docid);
            param[3] = new SqlParameter("@bos_docVersipn", docVersion);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_insertBosDetDoc", param));

        }

        public bool deleteBosDoc(int id)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_deleteBosdetDoc", param));
        }

        public bool confirmBosbyVend(string strbos_nbr, string bosVendRmks)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bos_nbr", strbos_nbr);
            param[1] = new SqlParameter("@bos_vendMessage", bosVendRmks);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_confirmBosbyVend", param));
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="strbos_nbr"></param>
        /// <param name="receiveRmks"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public bool confirmRecieveBos(string strbos_nbr, string receiveRmks, int uId)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@bos_nbr", strbos_nbr);
            param[1] = new SqlParameter("@receiveRmks", receiveRmks);
            param[2] = new SqlParameter("@uID", uId);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_confirmBosRecieve", param));
        }

        /// <summary>
        ///  获取样品单据号, 通过收货
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <param name="IsReceive"></param>
        /// <returns></returns>
        public DataTable getBosMstrForRecieve(string strBosnbr, string strVend, string IsReceive)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@bos_receiptIsConfirm", IsReceive);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosMstrForRecieve", param).Tables[0];
        }

        /// <summary>
        /// 获取打样的供应商
        /// </summary>
        /// <param name="sRole"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet getBosSuppliers(SysRole sRole)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@sysRole", sRole.ToString());

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectSupplier", param);
            return ds;
        }

        /// <summary>
        /// 对打样单的列表分别收货
        /// </summary>
        /// <param name="strBosNbr"></param>
        /// <param name="strBosdetline"></param>
        /// <returns></returns>
        public bool updateBosdetReciept(string strBosNbr, string strBosdetline, int uID)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@bos_nbr", strBosNbr);
            param[1] = new SqlParameter("@bos_detline", strBosdetline);
            param[2] = new SqlParameter("@uid", uID);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_confirmBosDetRecieve", param));
        }

        /// <summary>
        /// 技术验收
        /// </summary>
        /// <param name="strbosnbr"></param>
        /// <param name="bosLine"></param>
        /// <param name="strbosdetcode"></param>
        /// <param name="uID"></param>
        /// <param name="techChkResult"></param>
        /// <returns></returns>
        public bool updateBosdetTechResult(string strbosnbr, int bosLine, string strbosdetcode, string uID, string chkResult, string reason)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@bos_nbr", strbosnbr);
            param[1] = new SqlParameter("@bos_detline", bosLine);
            param[3] = new SqlParameter("@bos_detcode", strbosdetcode);
            param[4] = new SqlParameter("@uID", uID);
            param[5] = new SqlParameter("@chkResult", chkResult);
            param[6] = new SqlParameter("@reason", reason);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_updateBosDetTechResult", param));
        }
        /// <summary>
        /// 质检验收
        /// </summary>
        /// <param name="strbosnbr"></param>
        /// <param name="bosLine"></param>
        /// <param name="strbosdetcode"></param>
        /// <param name="uID"></param>
        /// <param name="chkResult"></param>
        /// <returns></returns>
        public bool updateBosdetQCResult(string strbosnbr, int bosLine, string strbosdetcode, string uID, string chkResult, string reason)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@bos_nbr", strbosnbr);
            param[1] = new SqlParameter("@bos_detline", bosLine);
            param[3] = new SqlParameter("@bos_detcode", strbosdetcode);
            param[4] = new SqlParameter("@uID", uID);
            param[5] = new SqlParameter("@chkResult", chkResult);
            param[6] = new SqlParameter("@reason", reason);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_updateBosDetQCResult", param));
        }
        /// <summary>
        /// 获取打样单供检验
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <param name="strVend"></param>
        /// <param name="createdDate1"></param>
        /// <param name="createdDate2"></param>
        /// <param name="checkValue"></param>
        /// <param name="checkType">类型：Tech，技术验收； Qual：质检验收。</param>
        /// <returns></returns>
        public DataTable getBosMstrToCheck(string strBosnbr, string strVend, DateTime createdDate1, DateTime createdDate2, string checkValue, string checkType)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr);
            param[1] = new SqlParameter("@bos_vend", strVend);
            param[2] = new SqlParameter("@createdDate1", createdDate1);
            param[3] = new SqlParameter("@createdDate2", createdDate2);
            param[4] = new SqlParameter("@checkValue", checkValue);
            param[5] = new SqlParameter("@checkType", checkType);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosMstrForCheck", param).Tables[0];
        }

        /// <summary>
        /// 获取供应商针对打样单上传的文档信息
        /// </summary>
        /// <param name="bosNbr"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public DataTable getBosSuppImportDocs(string bosNbr, int line)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bos_nbr", bosNbr);
            param[1] = new SqlParameter("@bos_line", line);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosSuppImportDocs", param).Tables[0];
        }

        /// <summary>
        /// 供应商上传文档
        /// </summary>
        /// <param name="bosNbr"></param>
        /// <param name="vender"></param>
        /// <param name="line"></param>
        /// <param name="attachName"></param>
        /// <param name="docId"></param>
        /// <param name="docDescs"></param>
        /// <param name="uploadby"></param>
        /// <returns></returns>
        public bool insertBosImportDocs(string bosNbr, string vender, int line, string bosCode, string bosQad, string attachName, string docId, string docDescs, string uploadby, string type, string typeName)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@bos_nbr", bosNbr);
            param[1] = new SqlParameter("@bos_vender", vender);
            param[2] = new SqlParameter("@bos_line", line);
            param[3] = new SqlParameter("@bos_code", bosCode);
            param[4] = new SqlParameter("@bos_qad", bosQad);
            param[5] = new SqlParameter("@bos_docfileName", attachName);
            param[6] = new SqlParameter("@bos_docId", docId);
            param[7] = new SqlParameter("@bos_docDescs", docDescs);
            param[8] = new SqlParameter("@bos_uploadby", uploadby);
            param[9] = new SqlParameter("@bos_docType", type);
            param[10] = new SqlParameter("@bos_docTypeName", typeName);

            return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_insertsuppImportDocs", param));
        }
        /// <summary>
        /// 删除供应商上传的文档 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteUploadDocs(int id)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_deleteSuppImportDocs", param);
            return Convert.ToBoolean(param[1].Value);

        }

        /// <summary>
        /// 获取打样单的供应商联系Email
        /// </summary>
        /// <param name="vendcode"></param>
        /// <returns></returns>
        public string getBosVendEmail(string vendcode)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@vendcode", vendcode);
            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosVendEmail", param).Tables[0];
            return dt.Rows[0]["usr_email"].ToString();

        }

        /// <summary>
        /// 已发送邮件通知供应商，进行标识
        /// </summary>
        /// <param name="bosNbr"></param>
        public void updateBosMstrSendEmail(string bosNbr)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bos_nbr", bosNbr);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_updateBosMstrSendVendEmail", param);
        }
        /// <summary>
        /// 获取打样创建人的邮箱
        /// </summary>
        /// <param name="bosnbr"></param>
        /// <returns></returns>
        public DataTable getBosnbrCreatedbyEmail(string bosnbr)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bos_nbr", bosnbr);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_bos_selectBosCreatedbyEmail", param).Tables[0];
        }

        /// <summary>
        /// 验证打样单是否有供应商上传的文档
        /// </summary>
        /// <param name="nbr"></param>
        /// <returns></returns>
        public bool IsExistDoc(string nbr, int line)
        {

            try
            {

                string strName = "sp_bos_CheckVendImportDoc";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@nbr", nbr);
                param[1] = new SqlParameter("@line", line);
                param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[2].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
                return Convert.ToBoolean(param[2].Value);

            }
            catch (Exception ex)
            {
                return false;
            }


        }
        /// <summary>
        /// 质检停止采购
        /// </summary>
        /// <param name="nbr"></param>
        /// <param name="line"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public bool QCApprove(string nbr, int line, string uID, string uName)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@nbr", nbr);
                param[1] = new SqlParameter("@line", line);
                param[2] = new SqlParameter("@uID", uID);
                param[3] = new SqlParameter("@uName", uName);
                param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_bos_QCApprove", param);
                return Convert.ToBoolean(param[4].Value);
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="strBosnbr"></param>
        /// <returns></returns>
        public bool updateBosNbrToCancel(string strBosnbr)
        {
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@bos_nbr", strBosnbr); 
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_bos_updateBosNbrToCancel", param));
        } 
    }
}