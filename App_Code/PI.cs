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



namespace PIInfo
{
    /// <summary>
    /// Summary description for PI
    /// </summary>
    public class PI
    {
        adamClass adam = new adamClass();
        String strSQL = "";

        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public PI()
        {
            //
            // TODO: Add constructor logic here
            //
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
            strSQL = "sp_SID_deltempPricList";
            SqlParameter parm = new SqlParameter("@uID", uID);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);

        }

        /// <summary>
        /// delete temp table
        /// </summary>
        public void DelTempPriceList(Int32 uID)
        {
            strSQL = "sp_PI_deltempPricList";
            SqlParameter parm = new SqlParameter("@uID", uID);
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);

        }

        /// <summary>
        /// Update temp table to use
        /// </summary>
        public string UpddatePriceList(Int32 uID)
        {
            strSQL = "sp_Pi_InsertPricListBytemp";
            SqlParameter parm = new SqlParameter("@uID", uID);
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
            while (reader.Read())
            {
                return reader["num"].ToString().Trim();
            }
            return "0";
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
        public int ImportPriceData(Int32 uID ,Int32 plantcode)
        {
            strSQL = "sp_pi_priceimport";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@uID", uID);
            parm[1] = new SqlParameter("@plantcode", plantcode);
            parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            parm[2].Direction = ParameterDirection.Output;
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

                    sheet = workbook.GetSheet(sSheet);
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


        #region 补充


        

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
        public bool InsertPriceList(string Pi_Cust, string Pi_QAD, string Pi_ShipTo, string pi_DoMain, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate, string Pi_price1, string Pi_price2, string Pi_price3, string Pi_createdBy, string Pi_createdName,string Pi_Remark)
        {
            try
            {
                Pi_price1 = (Pi_price1 == string.Empty) ? "0" : Pi_price1;
                Pi_price2 = (Pi_price2 == string.Empty) ? "0" : Pi_price2;
                Pi_price3 = (Pi_price3 == string.Empty) ? "0" : Pi_price3;
                string strSQL = "sp_Pi_insert_PriceList";
                SqlParameter[] parm = new SqlParameter[14];
                parm[0] = new SqlParameter("@Pi_Cust", Pi_Cust);
                parm[1] = new SqlParameter("@Pi_QAD", Pi_QAD);
                parm[2] = new SqlParameter("@Pi_ShipTo", Pi_ShipTo);
                parm[3] = new SqlParameter("@pi_DoMain", pi_DoMain);
                parm[4] = new SqlParameter("@Pi_Currency", Pi_Currency);
                parm[5] = new SqlParameter("@Pi_UM", Pi_UM);
                parm[6] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
                parm[7] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
                parm[8] = new SqlParameter("@Pi_price1", Pi_price1);
                parm[9] = new SqlParameter("@Pi_price2", Pi_price2);
                parm[10] = new SqlParameter("@Pi_price3", Pi_price3);
                parm[11] = new SqlParameter("@Pi_createdBy", Pi_createdBy);
                parm[12] = new SqlParameter("@Pi_createdName", Pi_createdName);
                parm[13] = new SqlParameter("@Pi_Remark", Pi_Remark);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 修改价格
        /// </summary>
        /// <param name="Pi_id"></param>
        /// <param name="Pi_Currency"></param>
        /// <param name="Pi_UM"></param>
        /// <param name="Pi_StartDate"></param>
        /// <param name="Pi_EndDate"></param>
        /// <param name="Pi_price1"></param>
        /// <param name="Pi_price2"></param>
        /// <param name="Pi_price3"></param>
        /// <param name="Pi_remark"></param>
        /// <param name="Pi_ModifyBy"></param>
        /// <param name="Pi_ModifyName"></param>
        /// <returns></returns>
        public bool UpdatePriceList(string Pi_id, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate, string Pi_price1, string Pi_price2, string Pi_price3,string Pi_remark , string Pi_ModifyBy, string Pi_ModifyName)
        {
            try
            {
               
                Pi_price1 = (Pi_price1 == string.Empty) ? "0" : Pi_price1;
                Pi_price2 = (Pi_price2 == string.Empty) ? "0" : Pi_price2;
                Pi_price3 = (Pi_price3 == string.Empty) ? "0" : Pi_price3;
                string strSQL = "sp_Pi_update_PriceList";
                SqlParameter[] parm = new SqlParameter[11];
                parm[0] = new SqlParameter("@Pi_id", Pi_id);
                parm[1] = new SqlParameter("@Pi_remark", Pi_remark);
                parm[2] = new SqlParameter("@Pi_Currency", Pi_Currency);
                parm[3] = new SqlParameter("@Pi_UM", Pi_UM);
                parm[4] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
                parm[5] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
                parm[6] = new SqlParameter("@Pi_price1", Pi_price1);
                parm[7] = new SqlParameter("@Pi_price2", Pi_price2);
                parm[8] = new SqlParameter("@Pi_price3", Pi_price3);
                parm[9] = new SqlParameter("@Pi_ModifyBy", Pi_ModifyBy);
                parm[10] = new SqlParameter("@Pi_ModifyName", Pi_ModifyName);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdatePriceList(string Pi_id, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate, string Pi_price1, string Pi_price2, string Pi_price3, string Pi_remark, string Pi_ModifyBy, string Pi_ModifyName,string His)
        {
            try
            {
                Pi_price1 = (Pi_price1 == string.Empty || Pi_price1 == @"&nbsp;") ? "" : Pi_price1;
                Pi_price2 = (Pi_price2 == string.Empty || Pi_price2 == @"&nbsp;") ? "" : Pi_price2;
                Pi_price3 = (Pi_price3 == string.Empty || Pi_price3 == @"&nbsp;") ? "" : Pi_price3;
                Pi_price1 = (Pi_price1 == string.Empty) ? "0" : Pi_price1;
                Pi_price2 = (Pi_price2 == string.Empty) ? "0" : Pi_price2;
                Pi_price3 = (Pi_price3 == string.Empty) ? "0" : Pi_price3;
                string isHis = (His == "Yes") ? "1" : "0";
                string strSQL = "sp_Pi_update_PriceList";
                SqlParameter[] parm = new SqlParameter[12];
                parm[0] = new SqlParameter("@Pi_id", Pi_id);
                parm[1] = new SqlParameter("@Pi_remark", Pi_remark);
                parm[2] = new SqlParameter("@Pi_Currency", Pi_Currency);
                parm[3] = new SqlParameter("@Pi_UM", Pi_UM);
                parm[4] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
                parm[5] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
                parm[6] = new SqlParameter("@Pi_price1", Pi_price1);
                parm[7] = new SqlParameter("@Pi_price2", Pi_price2);
                parm[8] = new SqlParameter("@Pi_price3", Pi_price3);
                parm[9] = new SqlParameter("@Pi_ModifyBy", Pi_ModifyBy);
                parm[10] = new SqlParameter("@Pi_ModifyName", Pi_ModifyName);
                parm[11] = new SqlParameter("@Pi_IsHis", isHis);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DeletePriceList(string Pi_id, string uID, string uName)
        {
            try
            {
                string strSQL = "sp_Pi_delete_PriceList";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@Pi_id", Pi_id);
                parm[1] = new SqlParameter("@id", uID);
                parm[2] = new SqlParameter("@name", uName);
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 检验价格是否存在
        /// </summary>
        /// <param name="Pi_Cust"></param>
        /// <param name="Pi_QAD"></param>
        /// <param name="Pi_ShipTo"></param>
        /// <param name="Pi_Currency"></param>
        /// <param name="Pi_UM"></param>
        /// <param name="Pi_StartDate"></param>
        /// <param name="Pi_EndDate"></param>
        /// <returns></returns>
        public int CheckPriceLists(string Pi_Cust, string Pi_QAD, string Pi_ShipTo, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate,string Pi_DoMain)
        {
            try
            {
                string strSQL = "sp_Pi_Check_PriceList";
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@Pi_Cust", Pi_Cust);
                parm[1] = new SqlParameter("@Pi_QAD", Pi_QAD);
                parm[2] = new SqlParameter("@Pi_ShipTo", Pi_ShipTo);
                parm[3] = new SqlParameter("@Pi_Currency", Pi_Currency);
                parm[4] = new SqlParameter("@Pi_UM", Pi_UM);
                parm[5] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
                parm[6] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
                parm[7] = new SqlParameter("@Pi_DoMain", Pi_DoMain);
                DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
                DataTable dt1 = ds.Tables[0];

                if (dt1.Rows.Count > 0)
                {
                    return 0;
                }

                return 1;
            }
            catch (Exception)
            {
                return 2;
            }

        }
        /// <summary>
        /// 查询价格
        /// </summary>
        /// <param name="Pi_Cust"></param>
        /// <param name="Pi_QAD"></param>
        /// <param name="Pi_ShipTo"></param>
        /// <returns></returns>
        public DataTable showPriceList(string Pi_Cust, string Pi_QAD, string Pi_ShipTo, string startdate1, string enddate1, string startdate2, string enddate2, string status, string domain)
        {
            try
            {
                string strSQL = "sp_Pi_select_PriceList";
                SqlParameter[] parm = new SqlParameter[9];
                parm[0] = new SqlParameter("@cust", Pi_Cust);
                parm[1] = new SqlParameter("@QAD", Pi_QAD);
                parm[2] = new SqlParameter("@shipto", Pi_ShipTo);
                parm[3] = new SqlParameter("@Pi_StartDate1", startdate1);
                parm[4] = new SqlParameter("@Pi_EndDate1", enddate1);
                parm[5] = new SqlParameter("@Pi_StartDate2", startdate2);
                parm[6] = new SqlParameter("@Pi_EndDate2", enddate2);
                parm[7] = new SqlParameter("@Pi_Status", status);
                parm[8] = new SqlParameter("@Pi_Domain", domain);

                return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }

        }
       
        /// <summary>
        /// 检验价格是否存在
        /// </summary>
        /// <param name="Pi_id"></param>
        /// <param name="Pi_Cust"></param>
        /// <param name="Pi_QAD"></param>
        /// <param name="Pi_ShipTo"></param>
        /// <param name="Pi_Currency"></param>
        /// <param name="Pi_UM"></param>
        /// <param name="Pi_StartDate"></param>
        /// <param name="Pi_EndDate"></param>
        /// <returns></returns>
        public int CheckPriceList(string Pi_id, string Pi_Cust, string Pi_QAD, string Pi_ShipTo, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate, string Pi_DoMain)
        {
            try
            {
                string strSQL = "sp_Pi_Check_PriceList";
                SqlParameter[] parm = new SqlParameter[9];
                parm[0] = new SqlParameter("@Pi_Cust", Pi_Cust);
                parm[1] = new SqlParameter("@Pi_QAD", Pi_QAD);
                parm[2] = new SqlParameter("@Pi_ShipTo", Pi_ShipTo);
                parm[3] = new SqlParameter("@Pi_Currency", Pi_Currency);
                parm[4] = new SqlParameter("@Pi_UM", Pi_UM);
                parm[5] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
                parm[6] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
                parm[7] = new SqlParameter("@Pi_id", Pi_id);
                parm[8] = new SqlParameter("@Pi_DoMain", Pi_DoMain);
                DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
                 DataTable dt2 = ds.Tables[1];
                if (dt2.Rows.Count > 0)
                {
                    return 3;
                }
                DataTable dt1 = ds.Tables[0];
                if(dt1.Rows.Count>0)
                {
                    return 0;
                }

                DataTable dt3 = ds.Tables[2];
                string row="0";
                foreach (DataRow item in dt3.Rows)
                {
                    row = item[0].ToString();
                }
                if (row == "1")
                {
                    return 4;
                }

                return 1;
            }
            catch (Exception)
            {
                return 2;
            }

        }
        #endregion



    }
}