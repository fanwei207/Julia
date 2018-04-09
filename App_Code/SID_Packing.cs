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
using System.Text;
using System.Reflection;
using System.Diagnostics;



namespace QADSID
{
    /// <summary>
    /// Summary description for Packing
    /// </summary>
    public class SID_Packing
    {
        adamClass adam = new adamClass();
        String strSQL = "";

        /// <summary>
        /// Ĭ�Ϲ��췽��.
        /// </summary>
        public SID_Packing()
        {
            //
            // TODO: Add constructor logic here
            //
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
        /// ��֤���ػ���
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable SelectPackingListInfo1(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectPackingInfo";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                //string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                //DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strResult).Tables[0];
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ȷ�ϳ��˶��������۶����Ƿ����
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable CheckExportInfo(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_CheckExportInfo";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strResult).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ��֤ά����Ϣ ��š��ᵥ������
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable SelectExportInfo(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectExportInfo";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strResult).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// װ�䵥��ϸ
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable SelectPackingDetailsInfo(string strShipNo, Int32 uid)
        {
            try
            {
                string strSql = "sp_sid_SelectPackingDetailsInfo";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@uid", uid);
                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strResult).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ��Ʊ��ϸ
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable SelectInvoiceDetailsInfo(string strShipNo, Int32 uid, Int32 plantcode, string checkpricedate)
        {
            try
            {
                string strSql = "sp_sid_SelectInvoiceDetailsInfo";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@uid", uid);
                sqlParam[2] = new SqlParameter("@plantcode", plantcode);
                sqlParam[3] = new SqlParameter("@checkpricedate", checkpricedate);
                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strResult).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// ��Ʊ��ϸ
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable SelectInvoiceDetailsInfoByWM(string strShipNo, Int32 uid, Int32 plantcode, string checkpricedate)
        {
            try
            {
                string strSql = "sp_sid_SelectInvoiceDetailsByWMInfo";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@uid", uid);
                sqlParam[2] = new SqlParameter("@plantcode", plantcode);
                sqlParam[3] = new SqlParameter("@checkpricedate", checkpricedate);
                //string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        /// <summary>
        /// ��֤���ػ���
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public IList<SID_PackingListInfo> SelectPackingListInfo(string strShipNo, Int32 uid, Int32 plantcode, string checkpricedate)
        {
            try
            {
                string strSql = "sp_sid_SelectPackingDetailsInfo1";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@uid", uid);
                sqlParam[2] = new SqlParameter("@plantcode", plantcode);
                sqlParam[3] = new SqlParameter("@checkpricedate", checkpricedate);

                IList<SID_PackingListInfo> DeclarationInfo = new List<SID_PackingListInfo>();

                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    SID_PackingListInfo di = new SID_PackingListInfo();
                    di.No = reader["sid_no"].ToString();
                    di.PO = reader["SID_PO"].ToString();
                    di.QAD = reader["SID_QAD"].ToString();
                    di.Part = reader["sid_cust_part"].ToString();
                    di.Descriptions = reader["sid_cust_partdesc"].ToString();
                    di.Qty = reader["sid_qty_pcs"].ToString();
                    di.Unit = reader["PI_UM"].ToString();
                    if (string.IsNullOrEmpty(reader["Pi_price1"].ToString()))
                    {
                        di.Price1 = decimal.Zero;
                    }
                    else
                    {
                        di.Price1 = Convert.ToDecimal(reader["Pi_price1"].ToString());
                    }
                    if (string.IsNullOrEmpty(reader["Pi_price2"].ToString()))
                    {
                        di.Price2 = decimal.Zero;
                    }
                    else
                    {
                        di.Price2 = Convert.ToDecimal(reader["Pi_price2"].ToString());
                    }
                    if (string.IsNullOrEmpty(reader["Pi_price3"].ToString()))
                    {
                        di.Price3 = decimal.Zero;
                    }
                    else
                    {
                        di.Price3 = Convert.ToDecimal(reader["Pi_price3"].ToString());
                    }
                    di.Currency = reader["Pi_Currency"].ToString();
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
        /// ��֤���ػ���
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public IList<SID_PackingListInfo> SelectPackingListInfo1(string strShipNo, Int32 uid, Int32 plantcode, string checkpricedate)
        {
            try
            {
                string strSql = "sp_sid_SelectPackingDetailsInfo2";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@uid", uid);
                sqlParam[2] = new SqlParameter("@plantcode", plantcode);
                sqlParam[3] = new SqlParameter("@checkpricedate", checkpricedate);

                IList<SID_PackingListInfo> DeclarationInfo = new List<SID_PackingListInfo>();

                string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));

                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strResult);
                while (reader.Read())
                {
                    SID_PackingListInfo di = new SID_PackingListInfo();
                    di.No = reader["sid_so_line"].ToString();
                    di.PO = reader["SID_PO"].ToString();
                    di.nbr = reader["SID_nbr"].ToString();
                    di.QAD = reader["SID_QAD"].ToString();
                    di.Part = reader["sid_cust_part"].ToString();
                    di.Descriptions = reader["sid_cust_partdesc"].ToString();
                    di.Qty = reader["sid_qty_pcs"].ToString();
                    di.Unit = reader["PI_UM"].ToString();
                    if (string.IsNullOrEmpty(reader["Pi_price1"].ToString()))
                    {
                        di.Price1 = decimal.Zero;
                    }
                    else
                    {
                        di.Price1 = Convert.ToDecimal(reader["Pi_price1"].ToString());
                    }
                    if (string.IsNullOrEmpty(reader["Pi_price2"].ToString()))
                    {
                        di.Price2 = decimal.Zero;
                    }
                    else
                    {
                        di.Price2 = Convert.ToDecimal(reader["Pi_price2"].ToString());
                    }
                    if (string.IsNullOrEmpty(reader["Pi_price3"].ToString()))
                    {
                        di.Price3 = decimal.Zero;
                    }
                    else
                    {
                        di.Price3 = Convert.ToDecimal(reader["Pi_price3"].ToString());
                    }
                    di.Currency = reader["Pi_Currency"].ToString();
                    di.CustPoDocPath = reader["doc_path"].ToString();
                    di.CustPoDocName = reader["doc_name"].ToString();
                    di.PriceIsNull = reader["PriceIsNull"].ToString();
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
        /// ����ά�� ���/�ᵥ
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public bool UpdateInvoiceInfo(string strShipNo, string boxno, string bl)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@boxno", boxno);
                sqlParam[2] = new SqlParameter("@bl", bl);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_UpdateInvoiceInfo", sqlParam));
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }



        /// <summary>
        /// ��Ϣά�� ���/�ᵥ/SHIP TO/��ӡ��¼
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable SaveInfo(string strShipNo, string boxno, string bl, string shipto, int uid, string lcno, int status, string nbrno,string shipdate, string PackingType)
        {
            try
            {
                //string strSql = "sp_sid_SaveInvoiceInfo";
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                sqlParam[1] = new SqlParameter("@boxno", boxno);
                sqlParam[2] = new SqlParameter("@bl", bl);
                sqlParam[3] = new SqlParameter("@shipto", shipto);
                sqlParam[4] = new SqlParameter("@uid", uid);
                sqlParam[5] = new SqlParameter("@lcno", lcno);
                sqlParam[6] = new SqlParameter("@status", status);
                sqlParam[7] = new SqlParameter("@nbrno", nbrno);
                sqlParam[8] = new SqlParameter("@shipdate", shipdate);
                sqlParam[9] = new SqlParameter("@PackingType", PackingType);

                //string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                //DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strResult).Tables[0];

                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_SaveInvoiceInfo", sqlParam).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// �����δ���ص�ϵͳ���˵���Ϣ Modified by Shanzm 2011.2.14 Add params shipdate1,shipdate2
        /// </summary>
        /// <param name="strPK"></param>
        /// <param name="strRef"></param>
        /// <param name="strNo"></param>
        /// <param name="strDomain"></param>
        /// <returns>������δ���ص�ϵͳ���˵������б�</returns>
        public IList<SID_PackingInfo> SelectSIDList(string strPK, string strRef, string strNbr, string strDomain, string shipdate1, string shipdate2)
        {
            try
            {
                string strSql = "sp_sid_SelectPackingMstr";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@pk", strPK);
                sqlParam[1] = new SqlParameter("@ref", strRef);
                sqlParam[2] = new SqlParameter("@nbr", strNbr);
                sqlParam[3] = new SqlParameter("@domain", strDomain);
                sqlParam[4] = new SqlParameter("@shipdate1", shipdate1);
                sqlParam[5] = new SqlParameter("@shipdate2", shipdate2);

                IList<SID_PackingInfo> SIDInfo = new List<SID_PackingInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_PackingInfo si = new SID_PackingInfo();
                    si.Container = reader["Container"].ToString();
                    si.Domain = reader["Domain"].ToString();
                    si.Nbr = reader["Nbr"].ToString();
                    si.Shipno = reader["Shipno"].ToString();
                    si.PK = reader["PK"].ToString();
                    si.PKRef = reader["PKRef"].ToString();
                    si.ShipTo = reader["ShipTo"].ToString();
                    si.SID = Convert.ToInt32(reader["SID"]);
                    si.Site = reader["Site"].ToString();
                    si.Via = reader["Via"].ToString();
                    si.ShipDate = reader["ShipDate"].ToString();
                    si.OutDate = reader["OutDate"].ToString();
                    si.PackingDate = reader["PackingDate"].ToString();
                    si.SID_org1_con = Convert.ToBoolean(reader["SID_org1_con"].ToString());
                    si.SID_org1_uid = reader["SID_org1_uid"].ToString();
                    si.SID_org2_con = Convert.ToBoolean(reader["SID_org2_con"].ToString());
                    si.SID_org2_uid = reader["SID_org2_uid"].ToString();
                    si.SID_org3_con = Convert.ToBoolean(reader["SID_org3_con"].ToString());
                    si.SID_org3_uid = reader["SID_org3_uid"].ToString();
                    si.NbrCombine = reader["NbrCombine"].ToString();

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
        /// ���Ӵ�ӡ���ݵ���ʱ��
        /// </summary>
        /// <param name="strSID"></param>
        /// <param name="uID"></param>
        /// <returns>���ر������ݵ���ʱ��</returns>
        public bool InsertPrintPackingTemp(string strSID, string struID)
        {
            try
            {
                string strSql = "sp_sid_InsertPrintPackingemp";

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
        /// ��������󶨳��˵�����PK��
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>���ص�֤���ػ��ܶ����б�</returns>
        public DataTable GetPackingInfo(Int32 uid)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@uid", uid);
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_sid_GetNbrPkInfo", sqlParam).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Import Dec PriceUpdate
        /// </summary>
        public int ImportShipCheckPriceStatus(Int32 uID)
        {
            strSQL = "sp_SID_shipCheckPriceStatus";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// Import InvoiceNO Check
        /// </summary>
        public int ImportShipInvNOCheck(Int32 uID, string invno)
        {
            strSQL = "sp_SID_shipinvCheckInvoiceNo";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@invno", invno);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// Import Price Check
        /// </summary>
        public int ImportShipInvDataCheck(Int32 uID)
        {
            strSQL = "sp_SID_shipinvimportCheck";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// Import Dec Check
        /// </summary>
        public int ImportShipInvDataCheckDec(Int32 uID)
        {
            strSQL = "sp_SID_shipinvimportCheckDec";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm));

        }

        /// <summary>
        /// Import Dec Check
        /// </summary>
        public DataTable CheckEDIWithShipPrice(Int32 uID)
        {
            strSQL = "sp_SID_CheckEDIWithShipPrice";
            SqlParameter parm = new SqlParameter("@uID", uID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];

        }
        /// <summary>
        /// Price of det with pi Check
        /// </summary>
        public int ShipInvDataCheckDetPriceWithPiPrice(Int32 uID, string checkpricedate)
        {
            strSQL = "sp_SID_ShipPrintInvCheckDetPriceWithPiPrice";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@checkpricedate", checkpricedate);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));

        }

        /// <summary>
        /// Price of det with pi Check
        /// </summary>
        public int ShipInvDataCheckDetPrice3WithPiPrice3(Int32 uID, string checkpricedate)
        {
            strSQL = "sp_SID_ShipInvCheckDetWithPiPrice3";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@checkpricedate", checkpricedate);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));

        }

        /// <summary>
        /// Import Price By Packing
        /// </summary>
        public int ImportShipInvData(Int32 uID, string checkpricedate)
        {
            strSQL = "sp_SID_shipinvimportBypacking";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@checkpricedate", checkpricedate);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));

        }

        /// <summary>
        /// Import Printer Info
        /// </summary>
        public int ImportPrintData(string nbr, Int32 uID)
        {
            strSQL = "sp_SID_shipPrintimport";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@nbr", nbr);
            sqlParam[1]  = new SqlParameter("@uID", uID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));

        }


        /// <summary>
        /// send email by bl
        /// </summary>
        public int sendemailbybl(string nbr, Int32 uID, string filepath)
        {
            strSQL = "sp_sid_sendemail_BL";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@nbr", nbr);
            sqlParam[1] = new SqlParameter("@uID", uID);
            sqlParam[2] = new SqlParameter("@filepath", filepath);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));

        }
        
        /// <summary>
        /// ��ֹExcel
        /// </summary>
        /// <param name="processName"></param>
        private void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //�õ����д򿪵Ľ���
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex
            }
        }

                ///   <summary> 
        /// ���캯������ָ��ģ���ļ�������ļ�����·��
        ///   </summary> 
        ///   <param name="templetFilePath"> Excelģ���ļ�·�� </param> 
        ///   <param name="outputFilePath"> ���Excel�ļ�·�� </param> 
        public SID_Packing(string templetFilePath, string outputFilePath)
        {
            if (templetFilePath == null)
                throw new Exception(" Excelģ���ļ�·������Ϊ�գ� ");

            if (outputFilePath == null)
                throw new Exception(" ���Excel�ļ�·������Ϊ�գ� ");

            if (!File.Exists(templetFilePath))
                throw new Exception(" ָ��·����Excelģ���ļ������ڣ� ");

            this.templetFile = templetFilePath;
            this.outputFile = outputFilePath;

            try
            {
                Process[] myProcesses;
                myProcesses = Process.GetProcessesByName("Excel");

                foreach (Process myProcess in myProcesses)
                {
                    myProcess.Kill();
                }
            }
            catch { }
            finally
            {
                KillProcess("Excel");
            }
        }


        protected string templetFile = null;
        protected string outputFile = null;
        protected object missing = Missing.Value;

        /// <summary>
        /// ������ص�
        /// </summary>
        /// <param name="sheetPrefixName"></param>
        /// <param name="strShipNo"></param>
        /// <param name="isPkgs"></param>
        public void DeclarationToExcel(string sheetPrefixName, string strShipNo)
        {
            if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
                sheetPrefixName = " Sheet1";

            // ����һ��Application����ʹ��ɼ� 
            Excel.Application app = new Excel.ApplicationClass();
            app.Visible = false;

            // ��ģ���ļ����õ�WorkBook���� 
            Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            // �õ�WorkSheet���� 
            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);
            workSheet.Name = sheetPrefixName;

            ////ȡ��������������
            //workSheet.Cells.Locked = false;

            SID sid = new SID();

            //�������
            //��ǰҳ
            int curP = 0;
            //ÿҳ����
            int cntP = 5;
            //������
            int cntT = Convert.ToInt32(sid.SelectDeclarationCount(strShipNo));
            //��ҳ��
            int totP = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(cntT) / Convert.ToDecimal(cntP)));
            //�������
            int cnt = 0;
            //ÿҳ����
            int rowP = 46;
            //��ǰ��
            int curR = 20;

            if (cntT < 0)
                return;

            //���ͷ����Ϣ
            SID_DeclarationInfo sdi = sid.SelectDeclarationHeader(strShipNo);
            IDataReader readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);

            //���ͷ����Ϣ
            workSheet.Cells[rowP * curP + 2, 2] = "SHANGHAI";
            workSheet.Cells[rowP * curP + 2, 7] = sdi.ShipDate;
            workSheet.Cells[rowP * curP + 4, 5] = sdi.ShipVia;
            workSheet.Cells[rowP * curP + 4, 1] = "3118941529";
            workSheet.Cells[rowP * curP + 4, 7] = sdi.Conveyance;
            workSheet.Cells[rowP * curP + 4, 10] = sdi.BL;
            workSheet.Cells[rowP * curP + 5, 1] = "�Ϻ�ǿ��������޹�˾";
            workSheet.Cells[rowP * curP + 6, 5] = sdi.Trade;
            workSheet.Cells[rowP * curP + 6, 12] = "T/T";
            workSheet.Cells[rowP * curP + 9, 5] = sdi.Country;
            workSheet.Cells[rowP * curP + 9, 8] = sdi.Harbor;
            workSheet.Cells[rowP * curP + 9, 11] = "SONGJIANG";
            workSheet.Cells[rowP * curP + 11, 1] = sdi.ShipNo;
            workSheet.Cells[rowP * curP + 11, 3] = "FOB";

            while (readerH.Read())
            {

                workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyBox"]);
                workSheet.Cells[rowP * curP + 13, 7] = "CTNS";
                workSheet.Cells[rowP * curP + 13, 10] = string.Format("{0:#0.00}", readerH["Weight"]);
                workSheet.Cells[rowP * curP + 13, 12] = string.Format("{0:#0.00}", readerH["Net"]);

                ////������Ԫ��
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
            }
            readerH.Close();

            //�����ϸ��Ϣ
            IDataReader readerD = (IDataReader)sid.SelectDeclarationExcel(strShipNo);
            while (readerD.Read())
            {
                if (cnt >= 5)
                {
                    cnt = 0;
                    curP += 1;
                    curR = 20;

                    //���ͷ����Ϣ
                    workSheet.Cells[rowP * curP + 2, 2] = "SHANGHAI";
                    workSheet.Cells[rowP * curP + 2, 7] = sdi.ShipDate;
                    workSheet.Cells[rowP * curP + 4, 5] = sdi.ShipVia;
                    workSheet.Cells[rowP * curP + 4, 1] = "3118941529";
                    workSheet.Cells[rowP * curP + 4, 7] = sdi.Conveyance;
                    workSheet.Cells[rowP * curP + 4, 10] = sdi.BL;
                    workSheet.Cells[rowP * curP + 5, 1] = "�Ϻ�ǿ��������޹�˾";
                    workSheet.Cells[rowP * curP + 6, 5] = sdi.Trade;
                    workSheet.Cells[rowP * curP + 6, 12] = "T/T";
                    workSheet.Cells[rowP * curP + 9, 5] = sdi.Country;
                    workSheet.Cells[rowP * curP + 9, 8] = sdi.Harbor;
                    workSheet.Cells[rowP * curP + 9, 11] = "SHANGHAI";
                    workSheet.Cells[rowP * curP + 11, 1] = sdi.Verfication;
                    workSheet.Cells[rowP * curP + 11, 3] = "FOB";

                    readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
                    while (readerH.Read())
                    {
                        workSheet.Cells[rowP * curP + 13, 4] = string.Format("{0:#0}", readerH["QtyBox"]);
                        workSheet.Cells[rowP * curP + 13, 7] = "CTNS";
                        workSheet.Cells[rowP * curP + 13, 10] = string.Format("{0:#0.00}", readerH["Weight"]);
                        workSheet.Cells[rowP * curP + 13, 12] = string.Format("{0:#0.00}", readerH["Net"]);

                        ////������Ԫ��
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 4], workSheet.Cells[rowP * curP + 13, 4]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 7], workSheet.Cells[rowP * curP + 13, 7]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 10], workSheet.Cells[rowP * curP + 13, 10]).Locked = true;
                        //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + 13, 12], workSheet.Cells[rowP * curP + 13, 12]).Locked = true;
                    }
                    readerH.Close();
                }

                string[] strCode = readerD["SCode"].ToString().Split(',');
                workSheet.Cells[rowP * curP + curR, 1] = strCode[2];
                workSheet.Cells[rowP * curP + curR, 2] = strCode[1];
                workSheet.Cells[rowP * curP + curR + 1, 2] = strCode[0];
                workSheet.Cells[rowP * curP + curR, 5] = string.Format("{0:#0}", readerD["QtyPcs"]);
                workSheet.Cells[rowP * curP + curR, 6] = "PCS/ֻ";
                //workSheet.Cells[rowP * curP + curR, 7] = "USD";
                workSheet.Cells[rowP * curP + curR, 8] = string.Format("{0:#0.00}", readerD["AvgPrice"]);
                workSheet.Cells[rowP * curP + curR, 9] = "USD";
                workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#0.00}", readerD["Amount"]);
                workSheet.Cells[rowP * curP + curR, 11] = "USD";

                ////������Ԫ��
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 1], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;
                //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR + 1, 2], workSheet.Cells[rowP * curP + curR + 1, 2]).Locked = true;

                cnt += 1;
                curR += 2;
            }
            readerD.Close();
            readerD.Dispose();

            curR += 1;

            //���Total:
            workSheet.Cells[rowP * curP + curR, 9] = "Total:";
            //workSheet.Cells[rowP * curP + curR, 9] = "USD";

            readerH = (IDataReader)sid.SelectDeclarationWNVBPA(strShipNo);
            while (readerH.Read())
            {
                workSheet.Cells[rowP * curP + curR, 10] = string.Format("{0:#,###,##0.00}", readerH["Amount"]);
            }
            workSheet.Cells[rowP * curP + curR, 11] = "USD";
            readerH.Close();
            readerH.Dispose();

            ////������Ԫ��
            //workSheet.Cells.get_Range(workSheet.Cells[rowP * curP + curR, 8], workSheet.Cells[rowP * curP + curR, 10]).Locked = true;

            ////���������
            //Random rnd = new Random();
            //string strRnd = rnd.Next().ToString();

            //workSheet.Protect(strRnd, false, true, true, true, true, true, true, false, false, false, false, false, false, false, false);

            // ���Excel�ļ����˳� 
            try
            {
                //workBook.Protect(strRnd, true, false);
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                workBook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workSheet = null;
                workBook = null;
                app = null;

                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                KillProcess("Excel");
            }

        }

        /// <summary>
        /// ����嵥
        /// </summary>
        /// <param name="strShipNo"></param>
        /// <returns>����嵥</returns>
        public DataTable SelectPackingStorageList(string strShipNo)
        {
            try
            {
                string strSql = "sp_sid_SelectPackingStorageList";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@shipno", strShipNo);
                //string strResult = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        ///Packing Combine Nbr Info
        /// </summary>
        public int PackingCombineNbr(string nbr, Int32 uID, string id)
        {
            strSQL = "sp_SID_PackingCombineNbr";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@nbr", nbr);
            sqlParam[1] = new SqlParameter("@uID", uID);
            sqlParam[2] = new SqlParameter("@id", id);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));
        }

        /// <summary>
        ///Packing Combine Nbr Info delete
        /// </summary>
        public int PackingCombineNbrDelete(string id, Int32 uID, string nbr, string nbrCombine)
        {
            strSQL = "sp_SID_PackingCombineNbrDelete";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@id", id);
            sqlParam[1] = new SqlParameter("@uID", uID);
            sqlParam[2] = new SqlParameter("@nbr", nbr);
            sqlParam[3] = new SqlParameter("@nbrCombine", nbrCombine);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));
        }

        /// <summary>
        /// ��ó��˵��źϲ����� Modified by Shanzm 2011.2.14 Add params shipdate1,shipdate2
        /// </summary>
        /// <param name="strPK"></param>
        /// <param name="strRef"></param>
        /// <param name="strNo"></param>
        /// <param name="strDomain"></param>
        /// <returns>������δ���ص�ϵͳ���˵������б�</returns>
        public IList<SID_PackingInfo> SelectSIDNbrCombineList(string strNbr)
        {
            try
            {
                string strSql = "sp_sid_SelectPackingNbrCombine";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@nbr", strNbr);

                IList<SID_PackingInfo> SIDInfo = new List<SID_PackingInfo>();
                IDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    SID_PackingInfo si = new SID_PackingInfo();
                    si.Container = reader["Container"].ToString();
                    si.Domain = reader["Domain"].ToString();
                    si.Nbr = reader["Nbr"].ToString();
                    si.Shipno = reader["Shipno"].ToString();
                    si.PK = reader["PK"].ToString();
                    si.PKRef = reader["PKRef"].ToString();
                    si.ShipTo = reader["ShipTo"].ToString();
                    si.SID = Convert.ToInt32(reader["SID"]);
                    si.Site = reader["Site"].ToString();
                    si.Via = reader["Via"].ToString();
                    si.ShipDate = reader["ShipDate"].ToString();
                    si.OutDate = reader["OutDate"].ToString();
                    si.SID_org1_con = Convert.ToBoolean(reader["SID_org1_con"].ToString());
                    si.SID_org1_uid = reader["SID_org1_uid"].ToString();
                    si.SID_org2_con = Convert.ToBoolean(reader["SID_org2_con"].ToString());
                    si.SID_org2_uid = reader["SID_org2_uid"].ToString();
                    si.SID_org3_con = Convert.ToBoolean(reader["SID_org3_con"].ToString());
                    si.SID_org3_uid = reader["SID_org3_uid"].ToString();
                    si.NbrCombine = reader["NbrCombine"].ToString();

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
        /// send email by bl
        /// </summary>
        public string GetPackingNbrCombineNo(string nbr, string NbrCombine)
        {
            strSQL = "sp_sid_GetPackingNbrCombineNo";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@nbr", nbr);
            sqlParam[1] = new SqlParameter("@NbrCombine", NbrCombine);
            return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));

        }
        /// <summary>
        /// send email by bl
        /// </summary>
        public string GetPackingNbrNo(string nbr)
        {
            strSQL = "sp_sid_GetPackingNbrNo";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@nbr", nbr);
            return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam));

        }
    }
}