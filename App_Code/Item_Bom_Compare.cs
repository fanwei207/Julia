using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using CommClass;
using adamFuncs;
using TCPCHINA.ODBCHelper;
using System.ComponentModel;
using Microsoft.ApplicationBlocks.Data;
using System.Data.Odbc;

/// <summary>
/// Summary description for Item_Bom_Compare
/// </summary>
namespace Item
{
    public class Item_Bom_Compare
    {
        public static void DelItemTempTable(int uID)//删除tcpc0..item_bom_temp表里当前用户的记录
        {
            adamClass chk = new adamClass();
            string strSql = "Delete From tcpc0..item_bom_temp where item_user_id='" + uID + "'";
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql);
        }
        /// <summary>
        /// 初始化tcpc0..item_bom_temp。这个用作展开ps_mstr中间表
        /// </summary>
        /// <param name="par"></param>
        /// <param name="comp"></param>
        /// <param name="uID"></param>
        public static void InsertOrigin(string par, string comp, int uID)//往tcpc0..item_bom_temp插入当前初始数据
        {
            adamClass chk = new adamClass();
            string strSql = "Insert Into tcpc0..item_bom_temp(item_bom_par,item_bom_comp,item_user_id,item_bom_qty,item_bom_lel)";
            strSql += "Values('" + par + "', '" + comp + "','" + uID + "',1,0)";
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSql);
        }
        /// <summary>
        /// 从tcpc0..item_bom_temp中获取子健
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="lel"></param>
        /// <param name="Qad"></param>
        /// <returns></returns>
        public static SqlDataReader GetOrigin(int uID, int lel, string Qad)
        {
            adamClass chk = new adamClass();
            string strSql = "Select item_bom_id, item_bom_par, item_bom_comp, isnull(item_bom_qty,0) From tcpc0..item_bom_temp";
            strSql += " Where item_user_id='" + uID + "' and item_bom_lel='" + lel + "' and item_bom_comp='" + Qad + "'";
            return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSql);
        }
        /// <summary>
        /// 创建100库结构临时表。结构取自表Product_stru；存入表tcpc0..product_bom_temp
        /// </summary>
        /// <param name="product_bom_code"></param>
        /// <param name="product_user_id"></param>
        public void CreateProductTempTable(string product_bom_code, int product_user_id)//创建100库里结构
        {
            adamClass chk = new adamClass();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@product_bom_code", product_bom_code);
            param[1] = new SqlParameter("@product_user_id", product_user_id);
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_product_extendstru", param);
        }
        public void CreateProductTempTable1(string product_bom_code, int product_user_id, bool onlyPakage = false)//创建100库里结构
        {
            adamClass chk = new adamClass();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@product_bom_code", product_bom_code);
            param[1] = new SqlParameter("@product_user_id", product_user_id);
            param[2] = new SqlParameter("@onlyPakage", onlyPakage);
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_product_extendstru1", param);
        }
        public DataTable CompareItemStru(int uId, string lel)
        {
            adamClass chk = new adamClass();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uId);
            param[1] = new SqlParameter("@lel", lel);
            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_product_CompareBomStru", param).Tables[0];
        }

        public DataTable CompareItemStru1(int uId, string lel, bool onlyPakage = false)
        {
            adamClass chk = new adamClass();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uId);
            param[1] = new SqlParameter("@lel", lel);
            param[2] = new SqlParameter("@onlyPakage", onlyPakage);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_product_CompareBomStru1", param).Tables[0];
        }

        public DataTable CompareItemStru2(int uId, string lel)
        {
            adamClass chk = new adamClass();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uId);
            param[1] = new SqlParameter("@lel", lel);
            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_product_CompareBomStruByBomUpdate", param).Tables[0];
        }

        /// <summary>
        /// 直接从PUB.ps_mstr中获取父键
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="par"></param>
        /// <returns></returns>
        public static OdbcDataReader GetQadOrigin(string domain, string par)
        {
            adamClass chk = new adamClass();
            String strSQL = "";
            String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
            strSQL = "Select ps_comp,ps_qty_per from PUB.ps_mstr Where ps_domain='" + domain + "'And ps_ps_code='' And ps_par='" + par + "' And (ps_start is null or ps_start <= '" + DateTime.Now + "') And (ps_end is null or ps_end >= '" + DateTime.Now + "')";
            return OdbcHelper.ExecuteReader(strConn, CommandType.Text, strSQL);
        }

        /// <summary>
        /// 直接从PUB.xxpsquery_hist中获取修改后父键
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="par"></param>
        /// <returns></returns>
        public static OdbcDataReader GetQadOrigin1(string domain, string par, string uid)
        {
            adamClass chk = new adamClass();
            String strSQL = "";
            String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
            strSQL = "Select xxpsquery_comp,xxpsquery_per,xxpsquery_crud from PUB.xxpsquery_hist Where xxpsquery_domain='" + domain + "' And xxpsquery_par='" + par + "' And (xxpsquery_start is null or xxpsquery_start <= '" + DateTime.Now + "') And (xxpsquery_end is null or xxpsquery_end >= '" + DateTime.Now + "')";
            return OdbcHelper.ExecuteReader(strConn, CommandType.Text, strSQL);

            //String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.QAD_DATA"];
            //strSQL = "Select xxpsquery_comp,xxpsquery_per,xxpsquery_crud from QAD_DATA..xxpsquery_hist Where xxpsquery_domain='" + domain + "' And xxpsquery_par='" + par + "' And (xxpsquery_start is null or xxpsquery_start <= '" + DateTime.Now + "') And (xxpsquery_end is null or xxpsquery_end >= '" + DateTime.Now + "')";
            //return SqlHelper.ExecuteReader(strConn, CommandType.Text, strSQL);
        }


        /// <summary>
        /// 将子健插入表tcpc0..item_bom_temp
        /// </summary>
        /// <param name="par"></param>
        /// <param name="comp"></param>
        /// <param name="uID"></param>
        /// <param name="qty"></param>
        /// <param name="lel"></param>
        public static void InsertChild(string par, string comp, int uID, decimal qty, int lel)
        {
            adamClass chk = new adamClass();
            string str = "Insert Into tcpc0..item_bom_temp(item_bom_par,item_bom_comp,item_user_id,item_bom_qty,item_bom_lel)";
            str += "Values('" + par + "','" + comp + "','" + uID + "','" + qty + "','" + lel + "')";
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, str);
        }


        /// <summary>
        /// 将修改后子健插入表tcpc0..item_bom_temp
        /// </summary>
        /// <param name="par"></param>
        /// <param name="comp"></param>
        /// <param name="uID"></param>
        /// <param name="qty"></param>
        /// <param name="lel"></param>
        public static void InsertChild1(string par, string comp, int uID, decimal qty, string crud, int lel)
        {
            adamClass chk = new adamClass();
            string str = "Insert Into tcpc0..item_bom_temp(item_bom_par,item_bom_comp,item_user_id,item_bom_qty,item_bom_crud,item_bom_lel)";
            str += "Values('" + par + "','" + comp + "','" + uID + "','" + qty + "','" + crud + "','" + lel + "')";
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, str);
        }

        /// <summary>
        /// 将QAD未审批的项，同步至100库
        /// </summary>
        /// <param name="partQad"></param>
        /// <param name="uID"></param>
        public static bool SysnTempBomFromQad(string domain, string partQad, string uID)
        {
            /*
             *  1、先将未审批数据同步至100
             *  2、过滤掉不属于该结构的申请
             *  3、对这些申请做虚拟审批
             *  4、将结果存入临时ps_mstr
             */
            adamClass chk = new adamClass();
            String strSQL = "";
            //String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
            String strConn = chk.dsn0();

            strSQL = "Select xxpsapy_par, xxpsapy_comp, xxpsapy_recomp, xxpsapy_start, xxpsapy_end, xxpsapy_ref, xxpsapy_per, xxpsapy_scrp, xxpsapy_fcst, xxpsapy_op, xxpsapy_rmks, xxpsapy_day, ";
            strSQL += "xxpsapy_user1, xxpsapy_user2, xxpsapy_confi1, xxpsapy_confi2, xxpsapy_confi3, xxpsapy_domain, xxpsapy_isok, xxpsapy_refuse, xxpsapy_pln, xxpsapy_pur, xxpsapy_cst, xxpsapy_coner1, ";
            strSQL += "xxpsapy_coner2, xxpsapy_coner3, xxpsapy_condate1, xxpsapy_condate2, xxpsapy_condate3, xxpsapy_okdate, xxpsapy_refusedate, xxpsapy_pscode, xxpsapy_ltoff, xxpsapy_itemno, ";
            strSQL += "xxpsapy_group, xxpsapy_process, xxpsapy_crud, xxpsapy_bomst, xxpsapy_lot ";
            strSQL += "From MFGPRO..PUB.xxpsapy_mstr ";
            strSQL += "Where xxpsapy_domain = '" + domain + "' ";
            strSQL += " And xxpsapy_isok = 0 AND xxpsapy_refuse = 0 AND xxpsapy_pln = 0";

            try
            {
                //DataTable table = OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL).Tables[0];
                DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSQL).Tables[0];

                if (table != null && table.Rows.Count > 0)
                {
                    //追加一列createBy
                    table.Columns.Add("createBy");
                    foreach (DataRow row in table.Rows)
                    {
                        row["createBy"] = uID;
                    }

                    #region 将PUB.xxpsapy_mstr同步至70.product_xxpsapy_mstr
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                    {
                        bulkCopy.DestinationTableName = "dbo.product_xxpsapy_mstr";
                        bulkCopy.ColumnMappings.Add("xxpsapy_par", "xxpsapy_par");
                        bulkCopy.ColumnMappings.Add("xxpsapy_comp", "xxpsapy_comp");
                        bulkCopy.ColumnMappings.Add("xxpsapy_recomp", "xxpsapy_recomp");
                        bulkCopy.ColumnMappings.Add("xxpsapy_start", "xxpsapy_start");
                        bulkCopy.ColumnMappings.Add("xxpsapy_end", "xxpsapy_end");
                        bulkCopy.ColumnMappings.Add("xxpsapy_ref", "xxpsapy_ref");
                        bulkCopy.ColumnMappings.Add("xxpsapy_per", "xxpsapy_per");
                        bulkCopy.ColumnMappings.Add("xxpsapy_scrp", "xxpsapy_scrp");
                        bulkCopy.ColumnMappings.Add("xxpsapy_fcst", "xxpsapy_fcst");
                        bulkCopy.ColumnMappings.Add("xxpsapy_op", "xxpsapy_op");
                        bulkCopy.ColumnMappings.Add("xxpsapy_rmks", "xxpsapy_rmks");
                        bulkCopy.ColumnMappings.Add("xxpsapy_day", "xxpsapy_day");
                        bulkCopy.ColumnMappings.Add("xxpsapy_user1", "xxpsapy_user1");
                        bulkCopy.ColumnMappings.Add("xxpsapy_user2", "xxpsapy_user2");
                        bulkCopy.ColumnMappings.Add("xxpsapy_confi1", "xxpsapy_confi1");
                        bulkCopy.ColumnMappings.Add("xxpsapy_confi2", "xxpsapy_confi2");
                        bulkCopy.ColumnMappings.Add("xxpsapy_confi3", "xxpsapy_confi3");
                        bulkCopy.ColumnMappings.Add("xxpsapy_domain", "xxpsapy_domain");
                        bulkCopy.ColumnMappings.Add("xxpsapy_isok", "xxpsapy_isok");
                        bulkCopy.ColumnMappings.Add("xxpsapy_refuse", "xxpsapy_refuse");
                        bulkCopy.ColumnMappings.Add("xxpsapy_pln", "xxpsapy_pln");
                        bulkCopy.ColumnMappings.Add("xxpsapy_pur", "xxpsapy_pur");
                        bulkCopy.ColumnMappings.Add("xxpsapy_cst", "xxpsapy_cst");
                        bulkCopy.ColumnMappings.Add("xxpsapy_coner1", "xxpsapy_coner1");
                        bulkCopy.ColumnMappings.Add("xxpsapy_coner2", "xxpsapy_coner2");
                        bulkCopy.ColumnMappings.Add("xxpsapy_coner3", "xxpsapy_coner3");
                        bulkCopy.ColumnMappings.Add("xxpsapy_condate1", "xxpsapy_condate1");
                        bulkCopy.ColumnMappings.Add("xxpsapy_condate2", "xxpsapy_condate2");
                        bulkCopy.ColumnMappings.Add("xxpsapy_condate3", "xxpsapy_condate3");
                        bulkCopy.ColumnMappings.Add("xxpsapy_okdate", "xxpsapy_okdate");
                        bulkCopy.ColumnMappings.Add("xxpsapy_refusedate", "xxpsapy_refusedate");
                        bulkCopy.ColumnMappings.Add("xxpsapy_pscode", "xxpsapy_pscode");
                        bulkCopy.ColumnMappings.Add("xxpsapy_ltoff", "xxpsapy_ltoff");
                        bulkCopy.ColumnMappings.Add("xxpsapy_itemno", "xxpsapy_itemno");
                        bulkCopy.ColumnMappings.Add("xxpsapy_group", "xxpsapy_group");
                        bulkCopy.ColumnMappings.Add("xxpsapy_process", "xxpsapy_process");
                        bulkCopy.ColumnMappings.Add("xxpsapy_crud", "xxpsapy_crud");
                        bulkCopy.ColumnMappings.Add("xxpsapy_bomst", "xxpsapy_bomst");
                        bulkCopy.ColumnMappings.Add("xxpsapy_lot", "xxpsapy_lot");
                        bulkCopy.ColumnMappings.Add("createBy", "createBy");

                        try
                        {
                            bulkCopy.WriteToServer(table);
                            table.Dispose();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            table.Dispose();
                            return false;
                        }
                    }
                    #endregion
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 虚拟审批程序
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="lel"></param>
        /// <returns></returns>
        public static bool VirtualApprove(string uID, string part, string domain)
        {
            try
            {
                adamClass chk = new adamClass();
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@uID", uID);
                param[1] = new SqlParameter("@part", part);
                param[2] = new SqlParameter("@domain", domain);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_product_virtualApprove", param);

                return Convert.ToBoolean(param[3].Value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 区别于GetQadOrigin，是从tcpc0.product_ps_mstr中获取父键
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="par"></param>
        /// <returns></returns>
        public static SqlDataReader GetQadOrigin4(string domain, string par, string uID)
        {
            adamClass chk = new adamClass();
            String strSQL = "Select ps_comp, ps_qty_per from product_ps_mstr where ps_createBy = " + uID + " And ps_domain='" + domain + "'And ps_ps_code='' And ps_par='" + par + "' And (ps_start is null or ps_start <= '" + DateTime.Now + "') And (ps_end is null or ps_end >= '" + DateTime.Now + "')";
            return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
        }
        /// <summary>
        /// 删除临时申请数据（product_xxpsapy_mstr）
        /// </summary>
        /// <param name="uID"></param>
        public static bool DeletepTempApplyData(string uID)
        {
            try
            {
                adamClass chk = new adamClass();
                string strSql = "Delete From product_xxpsapy_mstr where createBy = " + uID;
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

