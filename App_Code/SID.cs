using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using adamFuncs;
using CommClass;
using System.Data.OleDb;
using System.Text.RegularExpressions;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Data;
using System.Text;
using System.Text;



namespace QADSID
{
    /// <summary>
    /// Summary description for SID
    /// </summary>
    public class SID
    {
        adamClass adam = new adamClass();
        String strSQL = "";

        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public SID()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region 报关

        /// <summary>
        /// 获得尚未报关的系统货运单信息 Modified by Shanzm 2011.2.14 Add params shipdate1,shipdate2
        /// </summary>
        /// <param name="strPK"></param>
        /// <param name="strRef"></param>
        /// <param name="strNo"></param>
        /// <param name="strDomain"></param>
        /// <returns>返回尚未报关的系统货运单对象列表</returns>
        public IList<SID_Info> SelectSIDList(string strPK, string strRef, string strNbr, string strDomain, string shipdate1, string shipdate2)
        {
            try
            {
                string strSql = "sp_sid_SelectSidMstr";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@pk", strPK);
                sqlParam[1] = new SqlParameter("@ref", strRef);
                sqlParam[2] = new SqlParameter("@nbr", strNbr);
                sqlParam[3] = new SqlParameter("@domain", strDomain);
                sqlParam[4] = new SqlParameter("@shipdate1", shipdate1);
                sqlParam[5] = new SqlParameter("@shipdate2", shipdate2);

                IList<SID_Info> SIDInfo = new List<SID_Info>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_Info si = new SID_Info();
                    si.Container = reader["Container"].ToString();
                    si.Domain = reader["Domain"].ToString();
                    si.Nbr = reader["Nbr"].ToString();
                    si.PK = reader["PK"].ToString();
                    si.PKRef = reader["PKRef"].ToString();
                    si.ShipTo = reader["ShipTo"].ToString();
                    si.SID = Convert.ToInt32(reader["SID"]);
                    si.Site = reader["Site"].ToString();
                    si.Via = reader["Via"].ToString();
                    si.ShipDate = reader["ShipDate"].ToString();
                    si.OutDate = reader["OutDate"].ToString();

                    SIDInfo.Add(si);
                }
                reader.Close();
                return SIDInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        /// <summary>
        /// 查询添加照片
        /// </summary>
        /// <param name="strPK"></param>
        /// <param name="strRef"></param>
        /// <param name="strNbr"></param>
        /// <param name="strDomain"></param>
        /// <param name="shipdate1"></param>
        /// <param name="shipdate2"></param>
        /// <returns></returns>
        public DataTable SelectSIDListpicture(string strPK, string strRef, string strNbr, string strDomain, string shipdate1, string shipdate2,string status,string picdata1,string picdata2,string site,string outdate)
        {
            try
            {
                string strSql = "sp_sid_SelectSidMstrpicture";
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = new SqlParameter("@pk", strPK);
                sqlParam[1] = new SqlParameter("@ref", strRef);
                sqlParam[2] = new SqlParameter("@nbr", strNbr);
                sqlParam[3] = new SqlParameter("@domain", strDomain);
                sqlParam[4] = new SqlParameter("@shipdate1", shipdate1);
                sqlParam[5] = new SqlParameter("@shipdate2", shipdate2);
                sqlParam[6] = new SqlParameter("@status", status);
                sqlParam[7] = new SqlParameter("@picdate1", picdata1);
                sqlParam[8] = new SqlParameter("@picdate2", picdata2);
                sqlParam[9] = new SqlParameter("@Site", site);
                sqlParam[10] = new SqlParameter("@OutDate", outdate);
                 return  SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
               
               
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        /// <summary>
        /// 查看关联出运单
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public DataSet SelectSIDListpictureship(string sid)
        {
            try
            {
                string strSql = "sp_sid_Selectpictureshipid";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sid", sid);
              

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);


            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        /// <summary>
        /// 增加报关数据到临时表
        /// </summary>
        /// <param name="strSID"></param>
        /// <param name="uID"></param>
        /// <returns>返回报关数据到临时表</returns>
        public bool InsertDeclarationTemp(string strSID, string struID)
        {
            try
            {
                string strSql = "sp_sid_insertDeclarationtemp";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@sid", strSID);
                sqlParam[1] = new SqlParameter("@uid", struID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        /// <summary>
        /// 未报关出运单报关时，获取详细信息。
        /// </summary>
        /// <param name="strRet"></param>
        /// <returns></returns>
        public SqlDataReader GetSIDmstr(string strRet)
        {
            try
            {
                return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, "select top 1 SID_nbr,CONVERT(nvarchar(10),SID_shipdate,120) as SID_shipdate ,SID_shipto,SID_via from SID_mstr where SID_id=" + strRet);
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 获得报关临时表信息
        /// </summary>
        /// <param name="uID"></param>
        /// <returns>返回报关临时表对象</returns>
        public IList<SID_DeclarationInfo> SelectDeclarationTemp(int uID)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationTemp";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@uid", uID);

                IList<SID_DeclarationInfo> DeclarationInfo = new List<SID_DeclarationInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_DeclarationInfo di = new SID_DeclarationInfo();
                    di.SNO = reader["SNO"].ToString();
                    di.SCode = reader["SCode"].ToString();
                    di.QtySet = Convert.ToDecimal(reader["QtySet"]);
                    di.QtyPcs = Convert.ToDecimal(reader["QtyPcs"]);
                    di.QtyBox = Convert.ToDecimal(reader["QtyBox"]);
                    di.QtyPkgs = Convert.ToDecimal(reader["QtyPkgs"]);
                    di.Volume = Convert.ToDecimal(reader["Volume"]);
                    di.Weight = Convert.ToDecimal(reader["Weight"]);
                    di.Net = Convert.ToDecimal(reader["Net"]);
                    di.FixAmount = Convert.ToDecimal(reader["FixAmount"]);
                    di.Amount = Convert.ToDecimal(reader["Amount"]);
                    di.Diff = Convert.ToDecimal(reader["Diff"]);
                    di.AvgPrice = Convert.ToDecimal(reader["AvgPrice"]);
                    DeclarationInfo.Add(di);
                }
                reader.Close();
                return DeclarationInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得指定报关明细
        /// </summary>
        /// <param name="uID"></param>
        /// <returns>返回指定报关明细对象</returns>
        public IList<SID_DeclarationInfo> SelectDeclaration(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclaration";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                IList<SID_DeclarationInfo> DeclarationInfo = new List<SID_DeclarationInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_DeclarationInfo di = new SID_DeclarationInfo();
                    di.SNO = reader["SNO"].ToString();
                    di.SCode = reader["Code"].ToString();
                    di.QtySet = Convert.ToDecimal(reader["QtySet"]);
                    di.QtyPcs = Convert.ToDecimal(reader["QtyPcs"]);
                    di.QtyBox = Convert.ToDecimal(reader["QtyBox"]);
                    di.QtyPkgs = Convert.ToDecimal(reader["QtyPkgs"]);
                    di.Volume = Convert.ToDecimal(reader["Volume"]);
                    di.Weight = Convert.ToDecimal(reader["Weight"]);
                    di.Net = Convert.ToDecimal(reader["Net"]);
                    di.FixAmount = Convert.ToDecimal(reader["FixAmount"]);
                    di.Amount = Convert.ToDecimal(reader["Amount"]);
                    di.Diff = Convert.ToDecimal(reader["Diff"]);
                    di.AvgPrice = Convert.ToDecimal(reader["AvgPrice"]);
                    //di.CodeCmmt = reader["code_cmmt"].ToString();
                    DeclarationInfo.Add(di);
                }
                reader.Close();
                return DeclarationInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        ///  获得指定报关明细(导出EXCEL)
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectDeclarationExcel(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationExcel";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得指定报关明细记录条数
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回指定报关明细记录条数</returns>
        public int SelectDeclarationCount(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationCount";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -1;
            }
        }

        /// <summary>
        /// 获得指定报关明细(毛重W,净重N,体积V,箱数B,件数P,总金额A
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns></returns>
        public SqlDataReader SelectDeclarationWNVBPA(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationWNVBPA";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得指定报关检疫明细(净重N,箱数B,件数P)
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns></returns>
        public SqlDataReader SelectDeclarationNBPA(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationNBPA";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得指定报关信息
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回指定报关信息对象</returns>
        public SID_DeclarationInfo SelectDeclarationHeader(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationHeader";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                SID_DeclarationInfo di = new SID_DeclarationInfo();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    di.ShipNo = reader["ShipNo"].ToString();
                    di.SDate = Convert.ToDateTime(reader["ShipDate"]);
                    di.ShipDate = reader["ShipDate"].ToString();
                    di.Customer = reader["Customer"].ToString();
                    di.Harbor = reader["Harbor"].ToString();
                    di.ShipVia = reader["ShipVia"].ToString();
                    di.Trade = reader["Trade"].ToString();
                    di.Conveyance = reader["Conveyance"].ToString();
                    di.BL = reader["BL"].ToString();
                    di.Verfication = reader["Verfication"].ToString();
                    di.Contact = reader["Contact"].ToString();
                    di.Country = reader["Country"].ToString();
                    di.PO = reader["PO"].ToString();
                    di.Status = reader["Status"].ToString();
                    di.Tax = reader["Tax"].ToString();
                    di.FOB = reader["FOB"].ToString();
                }
                reader.Close();
                return di;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 保存报关单
        /// </summary>
        /// <param name="sdi"></param>
        /// <returns></returns>
        public bool UpdateDeclarationHeader(SID_DeclarationInfo sdi, string strOriShipNo)
        {
            try
            {
                string strSql = "sp_sid_UpdateDeclarationHeader";

                SqlParameter[] sqlParam = new SqlParameter[14];
                sqlParam[0] = new SqlParameter("@orishipno", strOriShipNo);
                sqlParam[1] = new SqlParameter("@shipno", sdi.ShipNo);
                sqlParam[2] = new SqlParameter("@shipdate", sdi.ShipDate);
                sqlParam[3] = new SqlParameter("@customer", sdi.Customer);
                sqlParam[4] = new SqlParameter("@harbor", sdi.Harbor);
                sqlParam[5] = new SqlParameter("@shipvia", sdi.ShipVia);
                sqlParam[6] = new SqlParameter("@trade", sdi.Trade);
                sqlParam[7] = new SqlParameter("@conveyance", sdi.Conveyance);
                sqlParam[8] = new SqlParameter("@bl", sdi.BL);
                sqlParam[9] = new SqlParameter("@verfication", sdi.Verfication);
                sqlParam[10] = new SqlParameter("@contact", sdi.Contact);
                sqlParam[11] = new SqlParameter("@country", sdi.Country);
                sqlParam[12] = new SqlParameter("@tax", sdi.Tax);
                sqlParam[13] = new SqlParameter("@uid", sdi.uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 保存临时报关单明细
        /// </summary>
        /// <param name="sdi"></param>
        /// <returns></returns>
        public bool UpdateDeclarationDetailTemp(SID_DeclarationInfo sdi)
        {
            try
            {
                string strSql = "sp_sid_UpdateDeclarationDetailTemp";

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@sno", sdi.SNO);
                sqlParam[1] = new SqlParameter("@net", sdi.Net);
                sqlParam[2] = new SqlParameter("@amount", sdi.Amount);
                sqlParam[3] = new SqlParameter("@uid", sdi.uID);
                
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 保存报关单明细
        /// </summary>
        /// <param name="sdi"></param>
        /// <returns></returns>
        public bool UpdateDeclarationDetail(SID_DeclarationInfo sdi)
        {
            try
            {
                string strSql = "sp_sid_UpdateDeclarationDetail";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@shipno", sdi.ShipNo);
                sqlParam[1] = new SqlParameter("@sno", sdi.SNO);
                sqlParam[2] = new SqlParameter("@net", sdi.Net);
                sqlParam[3] = new SqlParameter("@amount", sdi.Amount);
                sqlParam[4] = new SqlParameter("@uid", sdi.uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 更新报关单状态
        /// </summary>
        /// <param name="sdi"></param>
        /// <returns></returns>
        public bool UpdateDeclarationStatus(SID_DeclarationInfo sdi)
        {
            try
            {
                string strSql = "sp_sid_UpdateDeclarationStatus";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@shipno", sdi.ShipNo);
                sqlParam[1] = new SqlParameter("@uid", sdi.uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 增加报关单
        /// </summary>
        /// <param name="sdi"></param>
        /// <returns></returns>
        public bool InsertDeclarationHeader(SID_DeclarationInfo sdi)
        {
            try
            {
                string strSql = "sp_sid_InsertDeclaration";

                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@shipno", sdi.ShipNo);
                sqlParam[1] = new SqlParameter("@shipdate", sdi.ShipDate);
                sqlParam[2] = new SqlParameter("@customer", sdi.Customer);
                sqlParam[3] = new SqlParameter("@harbor", sdi.Harbor);
                sqlParam[4] = new SqlParameter("@shipvia", sdi.ShipVia);
                sqlParam[5] = new SqlParameter("@trade", sdi.Trade);
                sqlParam[6] = new SqlParameter("@conveyance", sdi.Conveyance);
                sqlParam[7] = new SqlParameter("@bl", sdi.BL);
                sqlParam[8] = new SqlParameter("@verfication", sdi.Verfication);
                sqlParam[9] = new SqlParameter("@contact", sdi.Contact);
                sqlParam[10] = new SqlParameter("@country", sdi.Country);
                sqlParam[11] = new SqlParameter("@tax", sdi.Tax);
                sqlParam[12] = new SqlParameter("@uid", sdi.uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得出运编号
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回出运编号条数</returns>
        public int CheckShipNo(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectShipNoInfo";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -1;
            }
        }

        /// <summary>
        /// 删除已有报关信息
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns></returns>
        public bool DeleteDeclaration(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_DeleteDeclaration";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 报关绑定时CUST为C0000035同时确认报关价格.
        /// </summary>
        /// <param name="strSID"></param>
        /// <returns></returns>
        public bool UpdateCustPrice(string strSID, Int32 uID)
        {
            try
            {
                string strSql = "sp_sid_UpdateCustPrice";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@sid", strSID);
                sqlParam[1] = new SqlParameter("@uID", uID);
                Int32 i = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否存在0价格的数据
        /// </summary>
        /// <param name="strSID"></param>
        /// <returns></returns>
        public bool chkPriceIsZero(string strSID)
        {
            try
            {
                string strSql = "sp_sid_CheckPriceIsZero";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sid", strSID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        /// <summary>
        /// 检查是否存与表中
        /// </summary>
        /// <param name="strSID"></param>
        /// <returns></returns>
        public bool chkPicture(string strSID)
        {
            try
            {
                string strSql = "sp_sid_CheckPicture";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sid", strSID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        /// <summary>
        /// 出运单关联
        /// </summary>
        /// <param name="shipid"></param>
        /// <param name="sidid"></param>
        /// <param name="createby"></param>
        /// <returns></returns>
        public bool insertPicture(string shipid,string sidid,string createby)
        {
            try
            {
                string strSql = "sp_sid_insertPicture";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@shipid", shipid);
                sqlParam[1] = new SqlParameter("@sid", sidid);
                sqlParam[2] = new SqlParameter("@createby", createby);
               SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
               return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="shipid"></param>
        /// <param name="fname"></param>
        /// <param name="fpath"></param>
        /// <param name="remark"></param>
        /// <param name="createby"></param>
        /// <returns></returns>
        public bool insertPictureurl(string shipid, string fname,string fpath,string remark, string createby)
        {
            try
            {
                string strSql = "sp_sid_insertPictureurl";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@shipid", shipid);
                sqlParam[1] = new SqlParameter("@fname", fname);
                sqlParam[2] = new SqlParameter("@fpath", fpath);
                sqlParam[3] = new SqlParameter("@remark", remark);
                sqlParam[4] = new SqlParameter("@createby", createby);
                SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        /// <summary>
        /// 已做报关单信息
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <param name="strVerfication"></param>
        /// <returns>返回报关信息对象</returns>
        public IList<SID_DeclarationInfo> SelectDeclarationList(string strShipNo, string strVerfication)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationList";
                string strStatus = string.Empty;

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@verfication", strVerfication);

                IList<SID_DeclarationInfo> DeclarationInfo = new List<SID_DeclarationInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_DeclarationInfo di = new SID_DeclarationInfo();
                    di.ShipNo = reader["ShipNo"].ToString();
                    di.ShipDate = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                    di.Customer = reader["Customer"].ToString();
                    di.Harbor = reader["Harbor"].ToString();
                    di.ShipVia = reader["ShipVia"].ToString();
                    di.Trade = reader["Trade"].ToString();
                    di.Conveyance = reader["Conveyance"].ToString();
                    di.BL = reader["BL"].ToString();
                    di.Verfication = reader["Verfication"].ToString();
                    di.Contact = reader["Contact"].ToString();
                    di.Country = reader["Country"].ToString();
                    if (reader["Status"].ToString().IndexOf("A") >= 0)
                    {
                        strStatus += "新增,";
                    }
                    if (reader["Status"].ToString().IndexOf("C") >= 0)
                    {
                        strStatus += "修改,";
                    }
                    if (reader["Status"].ToString().IndexOf("D") >= 0)
                    {
                        strStatus += "删除,";
                    }
                    if (reader["Status"].ToString().IndexOf("N") >= 0)
                    {
                        strStatus += "正常,";
                    }
                    di.Status = strStatus.Substring(0, strStatus.Length - 1);
                    strStatus = "";
                    //switch(reader["Status"].ToString())
                    //{
                    //    case "A":
                    //        di.Status += "新增,";
                    //        goto case "C";

                    //    case "C":
                    //        di.Status += "修改,";
                    //        goto case "D";

                    //    case "D":
                    //        di.Status += "删除,";
                    //        goto case "N";

                    //    case "N":
                    //        di.Status += "正常";
                    //        break;
                    //}
                    DeclarationInfo.Add(di);
                }
                reader.Close();
                return DeclarationInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        ///  获得指定报关检疫明细
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectQuarantine(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectQuarantine";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得总的差异
        /// </summary>
        /// <returns></returns>
        public decimal SelectTotalDiff()
        {
            try
            {
                string strSql = "sp_sid_SelectTotalDiff";

                return Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }

        /// <summary>
        /// 获得各客户差异汇总
        /// </summary>
        /// <returns>返回各客户差异汇总列表</returns>
        public IList<SID_DeclarationInfo> SelectCustomerDiffList()
        {
            try
            {
                string strSql = "sp_sid_SelectCustomerDiffList";

                IList<SID_DeclarationInfo> DeclarationInfo = new List<SID_DeclarationInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql);
                while (reader.Read())
                {
                    SID_DeclarationInfo di = new SID_DeclarationInfo();
                    di.Customer = reader["Customer"].ToString();
                    di.Diff = Convert.ToDecimal(reader["Diff"]);
                    DeclarationInfo.Add(di);
                }
                reader.Close();
                return DeclarationInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得拆分前汇总金额
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回拆分前汇总金额</returns>
        public decimal SelectSplitAmount(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectSplitAmount";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }

        /// <summary>
        /// 获得临时表拆分前汇总金额
        /// </summary>
        /// <param name="struID"></param>
        /// <returns>返回拆分前汇总金额</returns>
        public decimal SelectSplitAmountTemp(int uID)
        {
            try
            {
                string strSql = "sp_sid_SelectSplitAmountTemp";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@uid", @uID);

                return Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return 0;
            }
        }

        /// <summary>
        /// 判断是否存在拆分数据
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns></returns>
        public bool chkSplitIsExist(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectSplitIsExist";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断临时表是否存在拆分数据
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns></returns>
        public bool chkSplitIsExistTemp(int intuID)
        {
            try
            {
                string strSql = "sp_sid_SelectSplitIsExistTemp";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@uid", intuID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 单证发票信息汇总
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns>返回单证发票信息汇总对象列表</returns>
        public IList<SID_DocumentInfo> SelectDocumentInfo(string strShipNo, string strDate, bool IsCabin)
        {
            try
            {
                string strSql = "sp_sid_SelectDocumentInfo";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@date", strDate);
                sqlParam[2] = new SqlParameter("@IsCabin", IsCabin);

                IList<SID_DocumentInfo> DocumentInfo = new List<SID_DocumentInfo>();

                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    SID_DocumentInfo di = new SID_DocumentInfo();
                    di.ItemNo = reader["ItemNo"].ToString();
                    di.ItemDesc = reader["ItemDesc"].ToString();
                    di.OrderNo = reader["OrderNo"].ToString();
                    di.ShipDate = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                    di.DocumentNo = reader["DocumentNo"].ToString();
                    di.Qty = Convert.ToInt32(reader["Qty"]);
                    di.Sets_Qty = Convert.ToInt32(reader["Sets_Qty"]);
                    di.SID_Ptype = reader["SID_Ptype"].ToString();
                    di.ATLPrice = Convert.ToDecimal(reader["ATLPrice"]);
                    di.ATLAmount = Convert.ToDecimal(reader["ATLAmount"]);
                    di.TCPPrice = Convert.ToDecimal(reader["TCPPrice"]);
                    di.TCPAmount = Convert.ToDecimal(reader["TCPAmount"]);
                    di.Commencement = reader["Commencement"].ToString();
                    di.IsCabin = reader["IsCabin"].ToString();
                    DocumentInfo.Add(di);
                }
                reader.Close();
                return DocumentInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 单证发票信息汇总Excel
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns>返回单证发票信息汇总SQL</returns>
        public string SelectDocumentInfoExcel(string strShipNo, string strDate, bool IsCabin)
        {
            try
            {
                string strSql = "sp_sid_SelectDocumentInfo";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@date", strDate);
                sqlParam[2] = new SqlParameter("@IsCabin", IsCabin);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 判断是否存在必须填写商检号而未填的数据
        /// </summary>
        /// <param name="strSID"></param>
        /// <returns></returns>
        public bool chkQAIsEmpty(string strSID)
        {
            try
            {
                string strSql = "sp_sid_CheckQAIsEmpty";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sid", strSID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判断是否必须填写商检号
        /// </summary>
        /// <param name="strSID"></param>
        /// <returns></returns>
        public bool chkQAIsRequired(string strSNO)
        {
            try
            {
                string strSql = "sp_sid_chkQAIsRequired";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sno", strSNO);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 获得需要商检号的系列信息
        /// </summary>
        /// <param name="uID"></param>
        /// <returns>返回需要商检号的系列对象</returns>
        public IList<QADSID.SID_QA> SelectNeedQASNO(string strSNO)
        {
            try
            {
                string strSql = "sp_sid_SelectNeedQASNO";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sno", strSNO);

                IList<QADSID.SID_QA> QA = new List<QADSID.SID_QA>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    QADSID.SID_QA sq = new QADSID.SID_QA();
                    sq.SNO = reader["SNO"].ToString();
                    sq.SDESC = reader["SDESC"].ToString();
                    QA.Add(sq);
                }
                reader.Close();
                return QA;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 增加需要填写商检号的系列号
        /// </summary>
        /// <param name="strSNO"></param>
        /// <returns></returns>
        public bool InsertNeedQASNO(string strSNO)
        {
            try
            {
                string strSql = "sp_sid_InsertNeedQASNO";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sno", strSNO.Trim());

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 删除需要填写商检号的系列号
        /// </summary>
        /// <param name="strSNO"></param>
        /// <returns></returns>
        public bool DeleteNeedQASNO(string strSNO)
        {
            try
            {
                string strSql = "sp_sid_DeleteNeedQASNO";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sno", strSNO.Trim());

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 报关统计汇总数据
        /// </summary>
        /// <param name="intYear">年</param>
        /// <param name="intMonth">月</param>
        /// <param name="nozero">不包含0</param>
        /// <param name="isSum">是否汇总</param>
        /// <returns>返回报关统计汇总对象列表</returns>
        public IList<SID_DeclarationRpt> SelectDeclarationReport(int intYear, int intMonth, bool nozero, bool isSum)
        {
            try
            {
                string strSql = "sp_sid_DeclarationReport";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@year", intYear);
                sqlParam[1] = new SqlParameter("@month", intMonth);
                sqlParam[2] = new SqlParameter("@nozero", nozero == true ? "1" : "0");
                sqlParam[3] = new SqlParameter("@isSum", isSum==true ? "1" : "0");

                IList<SID_DeclarationRpt> DeclarationRpt = new List<SID_DeclarationRpt>();

                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    SID_DeclarationRpt sdr = new SID_DeclarationRpt();
                    sdr.DeclarationPrefix = reader["SID_DeclarationPrefix"].ToString();
                    if (!isSum)
                    {
                        sdr.DeclarationNo = reader["SID_ShipNo"].ToString();
                        sdr.DeclarationShipDate = string.Format("{0:yyyy-MM-dd}", reader["SID_ShipDate"]);

                    }
                    sdr.DeclarationAmount = Convert.ToDecimal(reader["Amount"]);
                    if (isSum) sdr.DeclarationCount = Convert.ToInt32(reader["Cnt"]);
                    DeclarationRpt.Add(sdr);
                }
                reader.Close();
                return DeclarationRpt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 报关统计汇总数据Excel
        /// </summary>
        /// <param name="intYear">年</param>
        /// <param name="intMonth">月</param>
        /// <param name="nozero">不包含0</param>
        /// <param name="isSum">是否汇总</param>
        /// <returns>返回报关统计汇总对象</returns>
        public string SelectDeclarationReportExcel(int intYear, int intMonth, bool nozero, bool isSum)
        {
            try
            {
                string strSql = "sp_sid_DeclarationReport";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@year", intYear);
                sqlParam[1] = new SqlParameter("@month", intMonth);
                sqlParam[2] = new SqlParameter("@nozero", nozero == true ? "1" : "0");
                sqlParam[3] = new SqlParameter("@isSum", isSum == true ? "1" : "0");

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        #endregion

        #region 财务报表

        /// <summary>
        /// 单证报关差异
        /// </summary>
        /// <param name="strDocumentStart"></param>
        /// <param name="strDocumentEnd"></param>
        /// <param name="strDeclarationStart"></param>
        /// <param name="strDeclarationEnd"></param>
        /// <returns>返回单证报关差异对象列表</returns>
        public IList<SID_DocumentDeclarationDiff> SelectDiffAmountList(string strDocumentStart, string strDocumentEnd, string strDeclarationStart, string strDeclarationEnd)
        {
            try
            {
                string strSql = "sp_sid_SelectDocumentDeclarationDiff";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@docstart", strDocumentStart);
                sqlParam[1] = new SqlParameter("@docend", strDocumentEnd);
                sqlParam[2] = new SqlParameter("@decstart", strDeclarationStart);
                sqlParam[3] = new SqlParameter("@decend", strDeclarationEnd);

                IList<SID_DocumentDeclarationDiff> DocumentDeclarationDiff = new List<SID_DocumentDeclarationDiff>();

                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    SID_DocumentDeclarationDiff ddd = new SID_DocumentDeclarationDiff();
                    ddd.DocumentNo = reader["DocumentNo"].ToString();
                    ddd.DocumentShipDate = string.Format("{0:yyyy-MM-dd}", reader["DocumentShipDate"]);
                    ddd.DeclarationNo = reader["DeclarationNo"].ToString();
                    ddd.DeclarationShipDate = string.Format("{0:yyyy-MM-dd}", reader["DeclarationShipDate"]);
                    ddd.DocumentAmount = Convert.ToDecimal(reader["DocumentAmount"]);
                    ddd.DeclarationAmount = Convert.ToDecimal(reader["DeclarationAmount"]);
                    ddd.DiffAmount = Convert.ToDecimal(reader["DiffAmount"]);
                    DocumentDeclarationDiff.Add(ddd);
                }
                reader.Close();
                return DocumentDeclarationDiff;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 单证报关差异Excel
        /// </summary>
        /// <param name="strDocumentStart"></param>
        /// <param name="strDocumentEnd"></param>
        /// <param name="strDeclarationStart"></param>
        /// <param name="strDeclarationEnd"></param>
        /// <returns>返回单证报关差异SQL</returns>
        public string SelectDiffAmountExcel(string strDocumentStart, string strDocumentEnd, string strDeclarationStart, string strDeclarationEnd)
        {
            try
            {
                string strSql = "sp_sid_SelectDocumentDeclarationDiff";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@docstart", strDocumentStart);
                sqlParam[1] = new SqlParameter("@docend", strDocumentEnd);
                sqlParam[2] = new SqlParameter("@decstart", strDeclarationStart);
                sqlParam[3] = new SqlParameter("@decend", strDeclarationEnd);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 单证报关汇总
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回单证报关汇总对象列表</returns>
        public IList<SID_DeclarationInfo> SelectDeclarationInfo(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationInfo";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                IList<SID_DeclarationInfo> DeclarationInfo = new List<SID_DeclarationInfo>();

                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    SID_DeclarationInfo di = new SID_DeclarationInfo();
                    di.ShipNo = reader["ShipNo"].ToString();
                    di.Tax = reader["Tax"].ToString();
                    di.ShipDate = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                    di.Verfication = reader["Verfication"].ToString();
                    di.SNO = reader["SNO"].ToString();
                    di.SCode = reader["SCode"].ToString();
                    di.Code = reader["Code"].ToString();
                    di.QtyPcs = Convert.ToDecimal(reader["QtyPcs"]);
                    di.AvgPrice = Convert.ToDecimal(reader["AvgPrice"]);
                    di.Amount = Convert.ToDecimal(reader["Amount"]);
                    DeclarationInfo.Add(di);
                }
                reader.Close();
                return DeclarationInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 单证报关汇总Excel
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>返回单证报关汇总SQL</returns>
        public string SelectDeclarationInfoExcel(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationInfo";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Export Finance Report
        /// </summary>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public IList<SID_Finance> SelectFinanceReport(string strStart, string strEnd)
        {
            try
            {
                string strSql = "sp_sid_SelectFinanceReport";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@start", strStart);
                sqlParam[1] = new SqlParameter("@end", strEnd);

                IList<SID_Finance> SID_Finance = new List<SID_Finance>();

                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    SID_Finance fin = new SID_Finance();
                    fin.Flag = reader["Flag"].ToString();
                    fin.Domain = reader["Domain"].ToString();
                    fin.Invoice = reader["Invoice"].ToString();
                    fin.EffDate = string.Format("{0:yyyy-MM-dd}", reader["EffDate"]);
                    fin.Bill = reader["Bill"].ToString();
                    fin.Sell = reader["Sell"].ToString();
                    fin.Ship = reader["Ship"].ToString();
                    fin.Curr = reader["Curr"].ToString();
                    fin.ATLAmount = Convert.ToDecimal(reader["ATLAmount"]);
                    fin.TCPAmount = Convert.ToDecimal(reader["TCPAmount"]);
                    fin.TaxNo = reader["Tax"].ToString();
                    fin.Invoice2 = reader["Invoice2"].ToString();

                    SID_Finance.Add(fin);
                }
                reader.Close();
                return SID_Finance;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Export Finance Report Excel
        /// </summary>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns>Finance Report SQL</returns>
        public string SelectFinanceReportExcel(string strStart, string strEnd)
        {
            try
            {
                string strSql = "sp_sid_SelectFinanceReport";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@start", strStart);
                sqlParam[1] = new SqlParameter("@end", strEnd);

                return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        #endregion

        #region TCP Stage 856
        /// <summary>
        /// 插入错误信息
        /// </summary>
        /// <param name="ErrInfo"></param>
        /// <param name="uID"></param>
        public void InsertErrorInfo(string ErrInfo, int uID)
        {
            strSQL = "Insert into ImportError(ErrorInfo,userid) values(N'" + ErrInfo + "','" + uID + "')";
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
        }

        /// <summary>
        /// 判断发票是否存在
        /// </summary>
        /// <param name="strInv">发票号</param>
        /// <returns>True/False</returns>
        public bool CheckMasterInvoiceExist(string strInv)
        {
            try
            {
                string strSql = "sp_edi_CheckInvoiceNo";
                SqlParameter sqlParam = new SqlParameter("@inv", strInv);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        /// <summary>
        /// 插入装箱单数据
        /// </summary>
        /// <param name="intUID">Session["uID"]</param>
        /// <returns>True/False</returns>
        public bool InsertStage856(int intUID)
        {
            try
            {
                string strSql = "sp_edi_InsertStage856";
                SqlParameter sqlParam = new SqlParameter("@uid", intUID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        /// <summary>
        /// 判断订单号QAD是否存在
        /// </summary>
        /// <param name="strPoType">PoType</param>
        /// <param name="strPoNo">PoNo</param>
        /// <param name="strPoLine">PoLine</param>
        /// <returns>True/False</returns>
        public bool CheckPOLine(string strPoType, string strPoNo, string strPoLine)
        {
            try
            {
                string strSql = "sp_edi_CheckPOLineExist";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@type", strPoType);
                sqlParam[1] = new SqlParameter("@no", strPoNo);
                sqlParam[2] = new SqlParameter("@line", strPoLine);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获得已导入的装箱单
        /// </summary>
        /// <param name="strInvNo">发票号</param>
        /// <param name="strShipDest">目的港</param>
        /// <param name="strShipDate">出运日期</param>
        /// <returns>返回已导入的装箱单对象列表</returns>
        public IList<SID_Stage856> SelectPackList(string strInvNo, string strShipDest, string strShipDateFrom, string strShipDateTo)
        {
            try
            {
                string strSql = "sp_sid_SelectPackList";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@invno", strInvNo);
                sqlParam[1] = new SqlParameter("@dest", strShipDest);
                sqlParam[2] = new SqlParameter("@datefr", strShipDateFrom);
                sqlParam[3] = new SqlParameter("@dateto", strShipDateTo);
               
                IList<SID_Stage856> SID_Stage856 = new List<SID_Stage856>();

                IDataReader reader = SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_Stage856 stage = new SID_Stage856();
                    stage.MasterInvoice = reader["MasterInvoice"].ToString();
                    stage.ShipDate = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                    stage.ShipDest = reader["Destination"].ToString();
                    stage.ShipMeth = reader["ShipMethod"].ToString();
                    stage.DetailCount = Convert.ToInt32(reader["DetailCount"]);
                    stage.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);
                    stage.TotalCtns = Convert.ToInt32(reader["TotalCtns"]);
                    stage.isFinish = Convert.ToBoolean(reader["isFinish"]);
                    SID_Stage856.Add(stage);
                }
                reader.Close();
                return SID_Stage856;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得已导入的装箱单明细
        /// </summary>
        /// <param name="strInvNo">发票号</param>
        /// <returns>返回已导入的装箱单对象明细列表</returns>
        public IList<SID_Stage856> SelectPackDetail(string strInvNo, string strShipDate)
        {
            try
            {
                string strSql = "sp_sid_SelectPackDetail";
                 SqlParameter[] sqlParam = new SqlParameter[2];
                 sqlParam[0] = new SqlParameter("@inv", strInvNo);
                 sqlParam[1] = new SqlParameter("@Shipdate", Convert.ToDateTime(strShipDate));

                IList<SID_Stage856> SID_Stage856 = new List<SID_Stage856>();

                IDataReader reader = SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_Stage856 stage = new SID_Stage856();
                    stage.Container = reader["Container"].ToString();
                    stage.ContainerInvoice = reader["ContainerInvoice"].ToString();
                    stage.Item = reader["Item"].ToString();
                    stage.ItemDesc = reader["ItemDesc"].ToString();
                    stage.PO = reader["PO"].ToString();
                    stage.PoLine = reader["Line"].ToString();
                    stage.QtyShip = Convert.ToInt32(reader["ShipQty"]);
                    stage.ItemUom = reader["ItemUom"].ToString();
                    stage.QtyCtn = Convert.ToInt32(reader["CtnQty"]);
                    stage.CtnUom = reader["CtnUom"].ToString();
                    stage.LineNo = reader["LineNum"].ToString();
                    stage.UnitPrice = Convert.ToDecimal(reader["UnitPrice"].ToString());
                    stage.ExtdPrice = Convert.ToDecimal(reader["ExtdPrice"].ToString());
                    SID_Stage856.Add(stage);
                }
                reader.Close();
                return SID_Stage856;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获取指定装箱单信息
        /// </summary>
        /// <param name="strInvNo">发票号</param>
        /// <returns>返回已导入的装箱单对象</returns>
        public SID_Stage856 SelectPackList(string strInvNo)
        {
            try
            {
                string strSql = "sp_sid_SelectPackList";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@invno", strInvNo);
                sqlParam[1] = new SqlParameter("@dest", "");
                sqlParam[2] = new SqlParameter("@datefr", "");
                sqlParam[3] = new SqlParameter("@dateto", "");
               
                SID_Stage856 stage = new SID_Stage856();

                IDataReader reader = SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    stage = new SID_Stage856();
                    stage.MasterInvoice = reader["MasterInvoice"].ToString();
                    stage.ShipDate = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                    stage.ShipDest = reader["Destination"].ToString();
                    stage.ShipMeth = reader["ShipMethod"].ToString();
                }
                reader.Close();
                return stage;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 获得未同步Stage856的出运单
        /// </summary>
        /// <param name="strShipNo">出运单号</param>
        /// <param name="strShipDateFrom">出运起始日期</param>
        /// <param name="strShipDateTo">出运结束日期</param>
        /// <returns>返回未同步Stage856的出运单对象</returns>
        public IList<SID_SynStageASN> SelectSynStageList(string strInvNo, string strShipNo, string strDate, bool isChkAll)
        {
            try
            {
                string strSql = "sp_sid_SelectSynStage856List";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@invno", strInvNo);
                sqlParam[1] = new SqlParameter("@shipno", strShipNo);
                sqlParam[2] = new SqlParameter("@date", strDate);
                sqlParam[3] = new SqlParameter("@all", isChkAll);

                IList<SID_SynStageASN> SID_SynStageASN = new List<SID_SynStageASN>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_SynStageASN synstage = new SID_SynStageASN();
                    synstage.InvoiceNo = reader["InvoiceNo"].ToString().Trim();
                    synstage.ShipNo = reader["ShipNo"].ToString();
                    synstage.ShipDate = string.Format("{0:yyyy-MM-dd}", reader["ShipDate"]);
                    synstage.Container = reader["Container"].ToString().Trim();
                    synstage.ZhangQiZhang = reader["ZhangQiZhang"].ToString().Trim();
                    SID_SynStageASN.Add(synstage);
                }
                reader.Close();
                return SID_SynStageASN;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 同步到Stage856
        /// </summary>
        /// <param name="strInvNo">发票号</param>
        /// <returns>True/False</returns>
        public bool SynStage(string strInvNo, string strShipDate)
        {
            try
            {
                string strSql = "sp_sid_SynStage856";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@inv", strInvNo);
                sqlParam[1] = new SqlParameter("@shipDate", Convert.ToDateTime(strShipDate));
               

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否能同步到Stage856
        /// </summary>
        /// <param name="strInvNo">发票号</param>
        /// <returns>True/False</returns>
        public bool JudgeSynStage856(string strInvNo)
        {
            try
            {
                SqlParameter param = new SqlParameter("@SID_Invoice", strInvNo);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_JudgeSynStage856", param));
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 显示sid_det里面物料的重量、长、宽、高
        /// </summary>
        /// <param name="strInvNo">发票号</param>
        /// <returns>dataset</returns>
        public DataTable GetPartDataFromSID(string strInvNo)
        {
            SqlParameter param = new SqlParameter("@SID_Invoice", strInvNo);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_selectPartDataFromSID", param).Tables[0];
        }

        /// <summary>
        /// 删除Stage856错误数据
        /// </summary>
        /// <param name="strInvNo">strInvNo</param>
        /// <returns>True/False</returns>
        public bool DeleteStage856(string strInvNo, string strShipDate)
        {
            try
            {
                string strSql = "sp_sid_DeleteStage856";
                 SqlParameter[] sqlParam = new SqlParameter[2];
                 sqlParam[0] = new SqlParameter("@inv", strInvNo);
                 sqlParam[1] = new SqlParameter("@shipDate", Convert.ToDateTime(strShipDate));

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region TCP HTS
        public IList<SID_HTS> SelectHtsList(string strItem, string strHts)
        {
            try
            {
                string strSql = "sp_sid_SelectHtsList";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@item", strItem);
                sqlParam[1] = new SqlParameter("@hts", strHts);

                IList<SID_HTS> SID_HTS = new List<SID_HTS>();

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_HTS hts = new SID_HTS();
                    hts.TcpItem = reader["IMLITM"].ToString();
                    hts.HtsCode = reader["IIHSCD"].ToString();
                    hts.HtsDesc = reader["DRDL01"].ToString();
                    SID_HTS.Add(hts);
                }
                reader.Close();
                return SID_HTS;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        #endregion      

        #region Created by Liu Yi
        /// <summary>
        /// Delete the ship detail  data
        /// </summary>
        public Int32 DelShipDetails(String uID, String SDID)
        {
            strSQL = "sp_SID_DelShipDetails";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@SDID", SDID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }

        /// <summary>
        /// Add Shipdetails
        /// </summary>
        public Int32 InsertShipDetails(String uID, String DID, String SNO, String QAD, String Set, String Box, String Qa, String Nbr, String Line, String Wo, String Po,
    String CustPart, String Weight, String Volume, String Pkgs, String QtyPcs, String Fedx, String Fob, String Memo)
        {
            strSQL = "sp_SID_InsertShipDetails";
            SqlParameter[] parm = new SqlParameter[19];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@DID", DID);
            parm[2] = new SqlParameter("@SNO", SNO);
            parm[3] = new SqlParameter("@QAD", QAD);
            parm[4] = new SqlParameter("@Set", Set);
            parm[5] = new SqlParameter("@Box", Box);
            parm[6] = new SqlParameter("@Qa", Qa);
            parm[7] = new SqlParameter("@Nbr", Nbr);
            parm[8] = new SqlParameter("@Line", Line);
            parm[9] = new SqlParameter("@Wo", Wo);
            parm[10] = new SqlParameter("@Po", Po);
            parm[11] = new SqlParameter("@CustPart", CustPart);
            parm[12] = new SqlParameter("@Weight", Weight);
            parm[13] = new SqlParameter("@Volume", Volume);
            parm[14] = new SqlParameter("@Pkgs", Pkgs);
            parm[15] = new SqlParameter("@QtyPcs", QtyPcs);
            parm[16] = new SqlParameter("@Fedx", Fedx);
            parm[17] = new SqlParameter("@Fob", Fob);
            parm[18] = new SqlParameter("@Memo", Memo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }
        /// <summary>
        /// Get the shipdata details
        /// </summary>
        public DataTable GetShipDetails(String DID)
        {
            strSQL = "sp_SID_shipdetails";
            SqlParameter parma = new SqlParameter("@DID", DID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        /// <summary>
        /// Whether this String Number or Not
        /// </summary>
        public bool IsNumber(string str)
        {
            if (str == null || str == "") return false;
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(str) &&
            !objTwoDotPattern.IsMatch(str) &&
            !objTwoMinusPattern.IsMatch(str) &&
            objNumberPattern.IsMatch(str);
        }

        /// <summary>
        /// delete temp table
        /// </summary>
        public void DelTempShip(Int32 uID)
        {
            strSQL = "sp_SID_deltempship";
            SqlParameter parm = new SqlParameter("@uID", uID);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);

        }
        
        /// <summary>
        /// delete ImportError table
        /// </summary>
        public void DelImportError(Int32 uID)
        {
            strSQL = " Delete From ImportError Where userID='" + uID + "'";
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);

        }
        
        /// <summary>
        /// Insert Error information to ImportError table
        /// </summary>
        public void InsertErrorInfo(String ErrInfo, String Esheet, Int32 uID)
        {
            strSQL = "Insert into ImportError(ErrorInfo,userid) values(N'" + ErrInfo + "," + Esheet + "','" + uID + "')";
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
        }

        public DataTable SelectHtsListTb(string strItem, string strHts)
        {
            try
            {
                string strSql = "sp_sid_SelectHtsList";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@item", strItem);
                sqlParam[1] = new SqlParameter("@hts", strHts);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        ///  Insert Error information to ImportError table
        /// </summary>
        /// <param name="ErrInfo"></param>
        /// <param name="Esheet"></param>
        /// <param name="uID"></param>
        /// <param name="i"></param>
        public void InsertErrorInfo(String ErrInfo, String Esheet, Int32 uID, String i)
        {
            strSQL = "Insert into ImportError(ErrorInfo,userid) values(N'" + ErrInfo + "," + Esheet + "行" + i + "','" + uID + "')";
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
        }
        
        /// <summary>
        /// delete ImportError table
        /// </summary>
        public int ImportShipData(Int32 uID)
        {
            strSQL = "sp_SID_shipimport";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }
        
        /// <summary>
        /// delete ImportError table
        /// </summary>
        public int ImportShipInvData(Int32 uID)
        {
            strSQL = "sp_SID_shipinvimport";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }
        
        /// <summary>
        /// Insert the shipdata mstr
        /// </summary>
        public Int32 InsertTempMstr(Int32 uID, String PK, String Nbr, String OutDate, String Via, String Ctype, String ShipDate, String ShipTo, String Site, String Domain, String PkRef, String LCL)
        {

            strSQL = "sp_SID_InsertTempMstr";
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@pk", adam.sqlEncode(PK));
            parm[2] = new SqlParameter("@nbr", adam.sqlEncode(Nbr));
            parm[3] = new SqlParameter("@outdate", adam.sqlEncode(OutDate));
            parm[4] = new SqlParameter("@via", adam.sqlEncode(Via));
            parm[5] = new SqlParameter("@ctype", adam.sqlEncode(Ctype));
            parm[6] = new SqlParameter("@shipdate", adam.sqlEncode(ShipDate));
            parm[7] = new SqlParameter("@shipto", adam.sqlEncode(ShipTo));
            parm[8] = new SqlParameter("@site", adam.sqlEncode(Site));
            parm[9] = new SqlParameter("@domain", adam.sqlEncode(Domain));
            parm[10] = new SqlParameter("@pkref", adam.sqlEncode(PkRef));
            parm[11] = new SqlParameter("@LCL", adam.sqlEncode(LCL));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }
        
        /// <summary>
        /// Update the shipdata mstr
        /// </summary>
        public void UpdateShipMstr(Int32 uID, Int32 DID, String PK, String Nbr, String OutDate, String Via, String Ctype, String ShipDate, String ShipTo, String Site, String Domain, String PkRef)
        {

            strSQL = "sp_SID_UpdateShipMstr";
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@DID", DID);
            parm[2] = new SqlParameter("@pk", adam.sqlEncode(PK));
            parm[3] = new SqlParameter("@nbr", adam.sqlEncode(Nbr));
            parm[4] = new SqlParameter("@outdate", adam.sqlEncode(OutDate));
            parm[5] = new SqlParameter("@via", adam.sqlEncode(Via));
            parm[6] = new SqlParameter("@ctype", Ctype);
            parm[7] = new SqlParameter("@shipdate", adam.sqlEncode(ShipDate));
            parm[8] = new SqlParameter("@shipto", adam.sqlEncode(ShipTo));
            parm[9] = new SqlParameter("@site", adam.sqlEncode(Site));
            parm[10] = new SqlParameter("@domain", adam.sqlEncode(Domain));
            parm[11] = new SqlParameter("@pkref", adam.sqlEncode(PkRef));
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }

        /// <summary>
        /// 设置验货日期和决定是否免检
        /// </summary>
        public void UpdateInspectInfo(int uID, string uName, int DID, string InspDate, string InspSite, bool MianJian,string InspMatchDate,bool IsCabin)
        {
            strSQL = "sp_SID_UpdateInspectInfo";
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@uName", uName);
            parm[2] = new SqlParameter("@DID", DID);
            parm[3] = new SqlParameter("@insp_date", InspDate);
            parm[4] = new SqlParameter("@insp_site", InspSite);
            parm[5] = new SqlParameter("@mj", MianJian);
            parm[6] = new SqlParameter("@InspMatchDate", InspMatchDate);
            parm[7] = new SqlParameter("@IsCabin", IsCabin);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }

        /// <summary>
        /// 清空验货日期和免检信息
        /// </summary>
        public void ClearInspectInfo(int uID, string uName, int DID)
        {
            strSQL = "sp_SID_ClearInspectInfo";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@uName", uName);
            parm[2] = new SqlParameter("@DID", DID);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }

        /// <summary>
        /// Insert the shipdata detail
        /// </summary>
        public void InsertTempDetail(Int32 uID, Int32 DID, String QAD, decimal Qty_set, decimal Qty_box, String QA, String Memo, String So_nbr, String So_line, String WO, String PO, String Cust_part, decimal Weight, decimal Volume, String SNO, Decimal Qty_pcs, Decimal Qty_pkgs, String Fob, String Fedx, String NO, String ATL)
        {
            strSQL = "sp_SID_InsertTempDetail";
            SqlParameter[] parm = new SqlParameter[21];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@SID", DID);
            parm[2] = new SqlParameter("@QAD", QAD);
            parm[3] = new SqlParameter("@qty_set", Qty_set);
            parm[4] = new SqlParameter("@qty_box", Qty_box);
            parm[5] = new SqlParameter("@qa", QA);
            parm[6] = new SqlParameter("@memo", Memo);
            parm[7] = new SqlParameter("@so_nbr", So_nbr);
            parm[8] = new SqlParameter("@So_line", So_line);
            parm[9] = new SqlParameter("@wo", WO);
            parm[10] = new SqlParameter("@PO", PO);
            parm[11] = new SqlParameter("@cust_part", Cust_part);
            parm[12] = new SqlParameter("@weight", Weight);
            parm[13] = new SqlParameter("@volume", Volume);
            parm[14] = new SqlParameter("@SNO", SNO);
            parm[15] = new SqlParameter("@qty_pcs", Qty_pcs);
            parm[16] = new SqlParameter("@qty_pkgs", Qty_pkgs);
            parm[17] = new SqlParameter("@fob", Fob);
            parm[18] = new SqlParameter("@fedx", Fedx);
            parm[19] = new SqlParameter("@NO", NO);
            parm[20] = new SqlParameter("@ATL", ATL);


            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);

        }
        /// <summary>
        /// 价格导入
        /// </summary>
        /// <param name="SID_QAD"></param>
        /// <param name="SID_cust_part"></param>
        /// <param name="SID_Ptype"></param>
        /// <param name="SID_Cust"></param>
        /// <param name="SID_ShipTo"></param>
        /// <param name="SID_ShipName"></param>
        /// <param name="SID_BillTo"></param>
        /// <param name="SID_BillName"></param>
        /// <param name="SID_price1"></param>
        /// <param name="SID_price2"></param>
        /// <param name="SID_price3"></param>
        public bool InsertPriceList(string Pi_Cust, string Pi_QAD, string Pi_ShipTo, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate, string Pi_price1, string Pi_price2, string Pi_price3, string Pi_createdBy, string Pi_createdName)

        {
            try
            {
                strSQL = "sp_SID_insert_PriceList";
                SqlParameter[] parm = new SqlParameter[12];
                parm[0] = new SqlParameter("@Pi_Cust", Pi_Cust);
                parm[1] = new SqlParameter("@Pi_QAD", Pi_QAD);
                parm[2] = new SqlParameter("@Pi_ShipTo", Pi_ShipTo);
                parm[3] = new SqlParameter("@Pi_Currency", Pi_Currency);
                parm[4] = new SqlParameter("@Pi_UM", Pi_UM);
                parm[5] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
                parm[6] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
                parm[7] = new SqlParameter("@Pi_price1", Pi_price1);
                parm[8] = new SqlParameter("@Pi_price2", Pi_price2);
                parm[9] = new SqlParameter("@Pi_price3", Pi_price3);
                parm[10] = new SqlParameter("@Pi_createdBy", Pi_createdBy);
                parm[11] = new SqlParameter("@Pi_createdName", Pi_createdName);

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        /// <summary>
        /// Insert the shipdata price
        /// </summary>
        public void InsertTempInvData(String uID, String Nbr, String Invoice, String Line, String PO, String Part, String Desc, String Price, String Currency, String Ptype,String Price1)
        {

            strSQL = "sp_SID_InsertTempInvData";
            SqlParameter[] parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@Nbr", Nbr);
            parm[2] = new SqlParameter("@Invoice", Invoice);
            parm[3] = new SqlParameter("@Line", Line);
            parm[4] = new SqlParameter("@PO", PO);
            parm[5] = new SqlParameter("@part", Part);
            parm[6] = new SqlParameter("@desc", Desc);
            parm[7] = new SqlParameter("@price", Convert.ToDecimal(Price));
            parm[8] = new SqlParameter("@currency", Currency);
            parm[9] = new SqlParameter("@Ptype", Ptype);
            parm[10] = new SqlParameter("@price1", Price1);

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);

        }

        /// <summary>
        /// Get the shipdata mstr
        /// </summary>
        public DataTable GetShip()
        {
            strSQL = "sp_SID_ship";
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL).Tables[0];
        }
        /// <summary>
        /// Get the shipdata mstr
        /// </summary>
        public DataTable GetShip(String PK, String Nbr, String OutDate, String Via, String Ctype, String ShipDate, String ShipTo, String Site, String Domain, String Time, String Time1, String Rad)
        {
            strSQL = "sp_SID_ship1";
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@pk", PK);
            parm[1] = new SqlParameter("@nbr", Nbr);
            parm[2] = new SqlParameter("@outdate", OutDate);
            parm[3] = new SqlParameter("@via", Via);
            parm[4] = new SqlParameter("@ctype", Ctype);
            parm[5] = new SqlParameter("@shipdate", ShipDate);
            parm[6] = new SqlParameter("@shipto", ShipTo);
            parm[7] = new SqlParameter("@site", Site);
            parm[8] = new SqlParameter("@domain", Domain);
            parm[9] = new SqlParameter("@time", Time);
            if (Time1 != "")
            {
                parm[10] = new SqlParameter("@time1", Convert.ToDateTime(Time1).AddDays(1));
            }
            else
            {
                parm[10] = new SqlParameter("@time1", Time1);
            }
            parm[11] = new SqlParameter("@Rad", Rad);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }

        /// <summary>
        /// Get the shipdata mstr
        /// </summary>
        public DataTable GetShip(String PK, String Nbr, String OutDate, String Via, String Ctype, String ShipDate, String ShipTo, String Site, String Domain, String Time, String Time1, String Rad,string sonbr,string soline)
        {
            strSQL = "sp_SID_ship1";
            SqlParameter[] parm = new SqlParameter[14];
            parm[0] = new SqlParameter("@pk", PK);
            parm[1] = new SqlParameter("@nbr", Nbr);
            parm[2] = new SqlParameter("@outdate", OutDate);
            parm[3] = new SqlParameter("@via", Via);
            parm[4] = new SqlParameter("@ctype", Ctype);
            parm[5] = new SqlParameter("@shipdate", ShipDate);
            parm[6] = new SqlParameter("@shipto", ShipTo);
            parm[7] = new SqlParameter("@site", Site);
            parm[8] = new SqlParameter("@domain", Domain);
            parm[9] = new SqlParameter("@time", Time);
            if (Time1 != "")
            {
                parm[10] = new SqlParameter("@time1", Convert.ToDateTime(Time1).AddDays(1));
            }
            else
            {
                parm[10] = new SqlParameter("@time1", Time1);
            }
            parm[11] = new SqlParameter("@Rad", Rad);
            parm[12] = new SqlParameter("@sonbr", sonbr);
            parm[13] = new SqlParameter("@soline", soline);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }


        /// <summary>
        /// Get the shipdata mstr
        /// </summary>
        public DataTable GetShip(string nbr,string Line,string Rad)
        {
            strSQL = "sp_SID_shipEn";
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@line", Line);
            parm[2] = new SqlParameter("@Rad", Rad);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }
        /// <summary>
        /// Get the shipdata detail
        /// </summary>
        public DataTable GetShipDetail(String DID)
        {
            strSQL = "sp_SID_shipdetail";
            SqlParameter parma = new SqlParameter("@DID", DID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }
        /// <summary>
        /// Get the shipdata detail
        /// </summary>
        public DataTable GetShipDetail1(String DID)
        {
            strSQL = "sp_SID_shipdetail1";
            SqlParameter parma = new SqlParameter("@DID", DID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parma).Tables[0];
        }

        /// <summary>
        /// Delete the shipdata
        /// </summary>
        public Int32 DelShipData(String uID, String DID)
        {
            strSQL = "sp_SID_Delshipdata";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@DID", DID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }

        /// <summary>
        /// Delete the shipdata
        /// </summary>
        public Int32 DelShipDetail(String uID, String DID)
        {
            strSQL = "sp_SID_DelShipDetail";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@DID", DID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }

        /// <summary>
        /// Update the ship Detail Data
        /// </summary>
        public Int32 UpdateShipDetail(String uID, String DID, String SNO, String Box, String Qa, String Wo, String Weight, String Volume,
            String Price, String Price1, String Memo, String Qtyset, String Qtypcs, String Pkgs, String Ptype, String TcpPo)
        {
            strSQL = "sp_SID_UpdateShipDetail";
            SqlParameter[] parm = new SqlParameter[16];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@DID", DID);
            parm[2] = new SqlParameter("@SNO", SNO);
            parm[3] = new SqlParameter("@Box", Box);
            parm[4] = new SqlParameter("@Qa", Qa);
            parm[5] = new SqlParameter("@Wo", Wo);
            parm[6] = new SqlParameter("@Weight", Convert.ToDecimal(Weight));
            parm[7] = new SqlParameter("@Volume", Convert.ToDecimal(Volume));
            parm[8] = new SqlParameter("@Price", Convert.ToDecimal(Price));
            parm[9] = new SqlParameter("@Price1", Convert.ToDecimal(Price1));
            parm[10] = new SqlParameter("@Memo", Memo);
            parm[11] = new SqlParameter("@qtyset", Qtyset);
            parm[12] = new SqlParameter("@qtypcs", Qtypcs);
            parm[13] = new SqlParameter("@pkgs", Pkgs);
            parm[14] = new SqlParameter("@Ptype", Ptype);
            parm[15] = new SqlParameter("@TcpPo", TcpPo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }

        /// <summary>
        /// Get ExcelContent Via OLEDB
        /// </summary>
        public DataSet getExcelContents(String pFile, String sSheet)
        {
            OleDbConnection myOleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + pFile + "; Extended Properties = Excel 8.0;");
            OleDbCommand myOleDbCommand = new OleDbCommand("SELECT * FROM [" + sSheet + "]", myOleDbConnection);
            OleDbDataAdapter myData = new OleDbDataAdapter(myOleDbCommand);

            DataSet myDS = new DataSet();
            myData.Fill(myDS);

            myOleDbConnection.Close();
            return myDS;
        }


        /// <summary>
        /// Get ExcelContent Via OLEDB
        /// </summary>
        public DataSet getExcelContents1(String pFile, String sSheet)
        {
            //FileStream fs1 = new FileStream((@"D:\TCP-File\Julia\import\20140805103212531TCP14667bak.xls"), FileMode.Open, FileAccess.Read);
            //HSSFWorkbook hssfworkbook1 = new HSSFWorkbook(fs1, true);
            //ISheet sheet2 = hssfworkbook1.GetSheet("A");
            //IRow dataRow1 = sheet2.GetRow(1); //should be row 2
            //dataRow1.GetCell(1);


            IWorkbook workbook = null;
            FileStream fs = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(pFile, FileMode.Open, FileAccess.Read);
                if (pFile.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new HSSFWorkbook(fs);
                else if (pFile.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs,true);

                //HSSFSheet sheet1 = workbook.GetSheetAt(0);

                if (sSheet != null)
                {
                    //sheet = workbook.GetRow(0);

                    sheet = workbook.GetSheet("excel");
                    //HSSFRow headerRow = sheet.GetRow(0);
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    bool isFirstRowColumn = true;
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);
                            data.Columns.Add(column);
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                //return data;
                DataSet myDS = new DataSet();
                myDS.Tables.Add(data);
                return myDS;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }



            //OleDbConnection myOleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + pFile + "; Extended Properties = Excel 8.0;");
            //OleDbCommand myOleDbCommand = new OleDbCommand("SELECT * FROM [" + sSheet + "]", myOleDbConnection);
            //OleDbDataAdapter myData = new OleDbDataAdapter(myOleDbCommand);

            //DataSet myDS = new DataSet();
            //myData.Fill(myDS);

            //myOleDbConnection.Close();
            //return myDS;
        }




        /// <summary>
        /// 
        /// </summary>
        public bool IsDate(string input)
        {
            try
            {
                System.DateTime.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 判断出运单是否在空运列表里
        /// Created By Liuqj
        /// </summary>
        public bool IsAirShipImport(string po, string line)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@po", po);
                param[1] = new SqlParameter("@line", line);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_JudgeIsAirShipImport", param));
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 判断出运单是物料号是否存在edi_db..cp_mstr表里
        /// Created By Liuqj
        /// </summary>
        public bool ExistsQADShipImport(string qad, string so_nbr, string so_line, string po, int uID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@qad", qad);
                param[1] = new SqlParameter("@so_nbr", so_nbr);
                param[2] = new SqlParameter("@so_line", so_line);
                param[3] = new SqlParameter("@po", po);
                param[4] = new SqlParameter("@uID", uID);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_JudgeExistsQADShipImport", param));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断出运单是否在空运列表里
        /// Created By Liuqj
        /// </summary>
        public bool IsAirShipInsert(string id, string po, string line)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@po", po);
                param[2] = new SqlParameter("@line", line);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_JudgeIsAirShipInsert", param));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断错误列表里是否存在提示
        /// </summary>
        /// <param name="uID"></param>
        public bool IsExistsAirShip(int uID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@uID", uID);
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_JudegExistsAirShip", param));
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region create by Shan Zhiming

        public Int32 InsertShipDetail(String uID, String DID, String SNO, String QAD, String Set, String Box, String Qa, String Nbr, String Line, String Wo, String Po,
    String CustPart, String Weight, String Volume, String Pkgs, String QtyPcs, String Fedx, String Fob, String Memo, string Atl)
        {
            strSQL = "sp_SID_InsertShipDetail";
            SqlParameter[] parm = new SqlParameter[20];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@DID", DID);
            parm[2] = new SqlParameter("@SNO", SNO);
            parm[3] = new SqlParameter("@QAD", QAD);
            parm[4] = new SqlParameter("@Set", Set);
            parm[5] = new SqlParameter("@Box", Box);
            parm[6] = new SqlParameter("@Qa", Qa);
            parm[7] = new SqlParameter("@Nbr", Nbr);
            parm[8] = new SqlParameter("@Line", Line);
            parm[9] = new SqlParameter("@Wo", Wo);
            parm[10] = new SqlParameter("@Po", Po);
            parm[11] = new SqlParameter("@CustPart", CustPart);
            parm[12] = new SqlParameter("@Weight", Weight);
            parm[13] = new SqlParameter("@Volume", Volume);
            parm[14] = new SqlParameter("@Pkgs", Pkgs);
            parm[15] = new SqlParameter("@QtyPcs", QtyPcs);
            parm[16] = new SqlParameter("@Fedx", Fedx);
            parm[17] = new SqlParameter("@Fob", Fob);
            parm[18] = new SqlParameter("@Memo", Memo);
            parm[19] = new SqlParameter("@atl", Atl);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }


        public DataTable GetShipDetail(String DID, string rad)
        {
            strSQL = "sp_SID_shipdetail2";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@DID", DID);
            parm[1] = new SqlParameter("@rad", rad);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }

        //报关和单证
        public void ConfirmShipInfo(String SID, string org, string uID)
        {
            strSQL = "sp_SID_ConfirmShipInfo";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@SID", SID);
            parm[1] = new SqlParameter("@ORG", org);
            parm[2] = new SqlParameter("@UID", uID);

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }
        #endregion



        #region 客户物料

        public DataTable showCustDiscription(string part, string cust, string HST)
        {
            try
            {
                string strSQL = "sp_SID_select_CustDiscription";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@part", part);
                parm[1] = new SqlParameter("@cust", cust);
                parm[2] = new SqlParameter("@HST", HST);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }

        }




        public SqlDataReader selectCustDiscription(string id)
        {
            try
            {
                string strSQL = "sp_SID_select_CustDiscription";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@id", id);


                return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public string saveCustDiscription(string SID_id, string SID_cust, string SID_partID, string SID_HST, string SID_description, string uID, string uName)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@SID_id", SID_id);
                param[1] = new SqlParameter("@SID_cust", SID_cust);
                param[2] = new SqlParameter("@SID_partID", SID_partID);
                param[3] = new SqlParameter("@SID_HST", SID_HST);
                param[4] = new SqlParameter("@SID_description", SID_description);
                param[5] = new SqlParameter("@uID", uID);
                param[6] = new SqlParameter("@uName", uName);
                param[7] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
                param[7].Direction = ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_SID_saveCustPart", param);

                if (param[7].Value.ToString().Trim().Length == 0)
                {
                    return "保存成功！";
                   
                }
                else
                {
                    return param[7].Value.ToString().Trim();
                }
            }
            catch
            {
                return "保存失败！请刷新后重新操作一次！";
            }
        }

        #endregion

        //导出出运明细计划确认出运日期

        public DataTable SidExportExcel(String PK, String Nbr, String OutDate, String Via, String Ctype, String ShipDate, String ShipTo, String Site, String Domain, String Time, String Time1, String Rad)
        {
            try
            {
                string strSQL = "sp_sid_selectexportexcel";
                SqlParameter[] parm = new SqlParameter[12];
                parm[0] = new SqlParameter("@pk", PK);
                parm[1] = new SqlParameter("@nbr", Nbr);
                parm[2] = new SqlParameter("@outdate", OutDate);
                parm[3] = new SqlParameter("@via", Via);
                parm[4] = new SqlParameter("@ctype", Ctype);
                parm[5] = new SqlParameter("@shipdate", ShipDate);
                parm[6] = new SqlParameter("@shipto", ShipTo);
                parm[7] = new SqlParameter("@site", Site);
                parm[8] = new SqlParameter("@domain", Domain);
                parm[9] = new SqlParameter("@time", Time);
                if (Time1 != "")
                {
                    parm[10] = new SqlParameter("@time1", Convert.ToDateTime(Time1).AddDays(1));
                }
                else
                {
                    parm[10] = new SqlParameter("@time1", Time1);
                }
                parm[11] = new SqlParameter("@Rad", Rad);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }

        }

        //导出出运明细计划确认出运日期

        public DataTable SidExportExcel1(String PK, String Nbr, String OutDate1, String OutDate2, String Via, String Ctype, String ShipDate1, String ShipDate2, String ShipTo, String Site, String Domain, String Rad, string CreateDate1, string CreateDate2)
        {
            try
            {
                string strSQL = "sp_sid_selectexportexcel1";
                SqlParameter[] parm = new SqlParameter[14];
                parm[0] = new SqlParameter("@pk", PK);
                parm[1] = new SqlParameter("@nbr", Nbr);
                parm[2] = new SqlParameter("@outdate1", OutDate1);
                parm[3] = new SqlParameter("@outdate2", OutDate2);
                parm[4] = new SqlParameter("@via", Via);
                parm[5] = new SqlParameter("@ctype", Ctype);
                parm[6] = new SqlParameter("@shipdate1", ShipDate1);
                parm[7] = new SqlParameter("@shipdate2", ShipDate2);
                parm[8] = new SqlParameter("@shipto", ShipTo);
                parm[9] = new SqlParameter("@site", Site);
                parm[10] = new SqlParameter("@domain", Domain);
                //parm[11] = new SqlParameter("@time", Time);
                //if (Time1 != "")
                //{
                //    parm[11] = new SqlParameter("@time1", Convert.ToDateTime(Time1).AddDays(1));
                //}
                //else
                //{
                //    parm[11] = new SqlParameter("@time1", Time1);
                //}
                parm[11] = new SqlParameter("@Rad", Rad);
                parm[12] = new SqlParameter("@CreateDate1", CreateDate1);
                parm[13] = new SqlParameter("@CreateDate2", CreateDate2);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }

        }



        /// <summary>
        /// 确认送货时间
        /// </summary>
        public int ImportCheckData(Int32 uID, bool finishedaccess, bool arrivedaccess)
        {
            strSQL = "sp_SID_shipinvcheckdate";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@finishedaccess", finishedaccess);
            parm[2] = new SqlParameter("@arrivedaccess", arrivedaccess);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// 送货时间已确认提示
        /// </summary>
        public string ImportCheckDataExsit(Int32 uID)
        {
            strSQL = "sp_SID_shipinvcheckdateExsit";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, param);
            if (param[1].Value.ToString().Trim().Length == 0)
            {
                return "保存成功！";
            }
            else
            {
                return param[1].Value.ToString().Trim();
            }

        }

        /// <summary>
        /// delete temp table
        /// </summary>
        public void DelTempCheckedDateInfo(Int32 uID)
        {
            strSQL = "sp_SID_deltempCheckedDateInfo";
            SqlParameter parm = new SqlParameter("@uID", uID);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }

        /// <summary>
        /// 判断是否已存在
        /// </summary>
        public int CheckImportDataExist(String _Nbr, String _id, String _So_nbr, String _So_line)  //Int32 uID)
        {
            strSQL = "sp_SID_shipcheckdateexist";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@nbr", _Nbr);
            param[1] = new SqlParameter("@sid", _id);
            param[2] = new SqlParameter("@so_nbr", _So_nbr);
            param[3] = new SqlParameter("@so_line", _So_line);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, param));

        }

        /// <summary>
        ///  出运单明细维护完全，显示最晚一个时间
        /// </summary>
        public int CheckImportDataNotMax(String _Nbr, String _id, String _So_nbr, String _So_line, String _checkedate )  //Int32 uID)
        {
            strSQL = "sp_SID_shipcheckdateMax";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@nbr", _Nbr);
            param[1] = new SqlParameter("@sid", _id);
            param[2] = new SqlParameter("@so_nbr", _So_nbr);
            param[3] = new SqlParameter("@so_line", _So_line);
            param[4] = new SqlParameter("@checkeddate", _checkedate);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, param));

        }

        /// <summary>
        /// 判断更新完工日期与获取抵达时间权限
        /// </summary>
        public int CheckImportDataUserAccess(Int32 uID)
        {
            strSQL = "sp_SID_shipcheckdateUserAccess";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uid", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, param));

        }


        /// <summary>
        /// Delete the temp inv import data
        /// </summary>
        public void DelInvTemp(Int32 uID)
        {
            strSQL = "sp_sid_DelInvTemp";
            SqlParameter parm = new SqlParameter("@uID", uID);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }

        //财务发票号导入检查
        public int CheckInvTempError(int uID)
        {
            string sqlstr = "sp_sid_CheckInvTempError";
            SqlParameter[] param = new SqlParameter[]{new SqlParameter("@uID",uID)
        };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
        }

        //财务发票号导入
        public bool ImportInvShip(Int32 uID)
        {
            strSQL = "sp_sid_ImportInvShip";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }
        //根据单号查询
        public DataTable SelectInvShipNoInfo(string inv, string shipno)
        {
            strSQL = "sp_sid_SelectInvShipNo";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@inv", inv);
            parm[1] = new SqlParameter("@shipno", shipno);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }
        //编辑查询
        public DataTable SelectInvShipNoByID(int sid_id)
        {
            strSQL = "sp_sid_SelectInvShipNoByID";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@sid_id", sid_id);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }
        //编辑新增保存
        public bool SaveInvShipNo(int id, string inv, string shipno, int uID)
        {
            strSQL = "sp_sid_SaveInvShipNo";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@id", id);
            parm[1] = new SqlParameter("@inv", inv);
            parm[2] = new SqlParameter("@shipno", shipno);
            parm[3] = new SqlParameter("@uID", uID);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }
        //判断财务发票号，报关单号
        public int CheckInvShipNo(string inv, string shipno)
        {
            strSQL = "sp_sid_CheckInvShipNo";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@inv", inv);
            parm[1] = new SqlParameter("@shipno", shipno);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));
        }


        public DataTable GetSidNbrVar(string nbr, bool checkstatus)
        {
            try
            {
                string strSql = "sp_sid_SelectSidNbrVar";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@nbr", nbr);
                sqlParam[1] = new SqlParameter("@checkstatus", checkstatus);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        //获取邮件地址
        public DataTable GetSidUpdateEmailAdr(string TYPE, string domain, int uID)
        {
            try
            {
                string strSql = "sp_sid_SelectSUpdateEmailAdr";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@TYPE", TYPE);
                sqlParam[1] = new SqlParameter("@domain", domain);
                sqlParam[2] = new SqlParameter("@uID", uID);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        //确认提交
        public int SidVarSubmit(string sidnbr, Int32 Uid)
        {
            try
            {
                string strSql = "sp_sid_SidVarSubmit";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@sidnbr", sidnbr);
                sqlParam[1] = new SqlParameter("@Uid", Uid);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return -1;
            }
        }



        /// <summary>
        /// Get the shipdata mstr By Fin
        /// </summary>
        public DataTable GetShip(String PK, String Nbr, String Via, String OutDate, String OutDate1, String Site, String Time, String Time1, String Tradetype)
        {

            strSQL = "sp_SID_shipForFin";
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@pk", PK);
            parm[1] = new SqlParameter("@nbr", Nbr);
            parm[2] = new SqlParameter("@outdate", OutDate);
            parm[3] = new SqlParameter("@outdate1", OutDate1);
            //parm[3] = new SqlParameter("@shipdate", ShipDate);
            parm[4] = new SqlParameter("@site", Site);
            parm[5] = new SqlParameter("@time", Time);
            if (Time1 != "")
            {
                parm[6] = new SqlParameter("@time1", Convert.ToDateTime(Time1).AddDays(1));
            }
            else
            {
                parm[6] = new SqlParameter("@time1", Time1);
            }
            parm[7] = new SqlParameter("@Tradetype", Tradetype);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }
        public DataTable GetShipDetailForFin(String DID, string rad)
        {
            strSQL = "sp_SID_shipdetailForFin";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@DID", DID);
            parm[1] = new SqlParameter("@rad", rad);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
        }

        //内销
        public void ConfirmShipFINInfo(String SID, string org, string uID)
        {
            strSQL = "sp_SID_ConfirmShipFINInfo";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@SID", SID);
            parm[1] = new SqlParameter("@ORG", org);
            parm[2] = new SqlParameter("@UID", uID);

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
        }


        /// <summary>
        /// 获得报关临时表信息
        /// </summary>
        /// <param name="uID"></param>
        /// <returns>返回报关临时表对象</returns>
        /// 

        public DataTable SelectDeclarationEditTemp(int uID, bool type)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationEditTemp";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@uid", uID);
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        public IList<SID_DeclarationInfo> SelectDeclarationEditTemp(int uID)
        {
            try
            {
                string strSql = "sp_sid_SelectDeclarationEditTemp";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@uid", uID);

                IList<SID_DeclarationInfo> DeclarationInfo = new List<SID_DeclarationInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_DeclarationInfo di = new SID_DeclarationInfo();
                    di.SID_DID = Convert.ToInt32(reader["SID_DID"].ToString());
                    di.SNBR = reader["SNBR"].ToString();
                    di.SNO = reader["SNO"].ToString();
                    di.SCode = reader["SCode"].ToString();
                    di.QtySet = Convert.ToDecimal(reader["QtySet"]);
                    di.QtyPcs = Convert.ToDecimal(reader["QtyPcs"]);
                    di.QtyBox = Convert.ToDecimal(reader["QtyBox"]);
                    di.QtyPkgs = Convert.ToDecimal(reader["QtyPkgs"]);
                    di.Volume = Convert.ToDecimal(reader["Volume"]);
                    di.Weight = Convert.ToDecimal(reader["Weight"]);
                    di.Net = Convert.ToDecimal(reader["Net"]);
                    di.FixAmount = Convert.ToDecimal(reader["FixAmount"]);
                    di.Amount = Convert.ToDecimal(reader["Amount"]);
                    di.Diff = Convert.ToDecimal(reader["Diff"]);
                    di.AvgPrice = Convert.ToDecimal(reader["AvgPrice"]);
                    di.Old_Price = Convert.ToDecimal(reader["Old_Price"]);
                    di.New_Price = Convert.ToDecimal(reader["New_Price"]);
                    di.TotalNet = Convert.ToDecimal(reader["TotalNet"]);
                    DeclarationInfo.Add(di);
                }
                reader.Close();
                return DeclarationInfo;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// 明细修改保存临时报关单明细
        /// </summary>
        /// <param name="sdi"></param>
        /// <returns></returns>
        public bool UpdateDeclarationDetailEditTemp(SID_DeclarationInfo sdi)
        {
            try
            {
                string strSql = "sp_sid_UpdateDeclarationDetailEditTemp";

                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@SID_DID", sdi.SID_DID);
                sqlParam[1] = new SqlParameter("@Weight", sdi.Weight);
                sqlParam[2] = new SqlParameter("@New_Price", sdi.New_Price);
                sqlParam[3] = new SqlParameter("@uid", sdi.uID);
                sqlParam[4] = new SqlParameter("@SNO", sdi.SNO);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 判定是否已绑定报关。
        /// </summary>
        /// <param name="strRet"></param>
        /// <returns></returns>
        public DataTable CheckEistsCuster(string strShipNo)
        {
            try
            {
                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, "select * from SID_Declaration where sid_shipno=" + "'" + strShipNo + "'").Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }


        /// <summary>
        /// delete ImportError table
        /// </summary>
        public int GetPartInfoData(Int32 uID)
        {
            strSQL = "sp_SID_GetPartInfoData";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// delete temp table
        /// </summary>
        public void DelGetPartInfoTemp(Int32 uID)
        {
            strSQL = "sp_SID_delGetPartInfoTemp";
            SqlParameter parm = new SqlParameter("@uID", uID);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);

        }

    }
}