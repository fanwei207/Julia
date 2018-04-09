using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RD_WorkFlow
{
    public enum SKUType
    { 
        /// <summary>
        /// Stock
        /// </summary>
        STK,
        /// <summary>
        /// Make to Order
        /// </summary>
        MTO
    }

    /// <summary>
    /// the params of table sku_mstr
    /// </summary>
    public class SKU
    {
        private string _SKU;
        public string Sku
        {
            get { return this._SKU; }
        }

        private string _UPC;
        public string UPC
        {
            get { return this._UPC; }
            set
            {
                if (value.Length > 12)
                {
                    throw new Exception(string.Format("The length of UPC({0}) must be less then 12 characters.Please check it.", value));
                }
                else
                {
                    this._UPC = value;
                }
            }
        }

        private Int32 _Voltage;
        public Int32 Voltage
        {
            get { return this._Voltage; }
            set { this._Voltage = value; }
        }

        private Int32 _Wattage;
        public Int32 Wattage
        {
            get { return this._Wattage; }
            set { this._Wattage = value; }
        }

        private Int32 _Lumens;
        public Int32 Lumens
        {
            get { return this._Lumens; }
            set { this._Lumens = value; }
        }

        private Decimal _LPW;
        public Decimal LPW
        {
            get { return this._LPW; }
            set { this._LPW = value; }
        }

        private Int32 _CBCPest;
        public Int32 CBCPest
        {
            get { return this._CBCPest; }
            set { this._CBCPest = value; }
        }

        private Int32 _BeamAngle;
        public Int32 BeamAngle
        {
            get { return this._BeamAngle; }
            set { this._BeamAngle = value; }
        }

        private Int32 _CCT;
        public Int32 CCT
        {
            get { return this._CCT; }
            set { this._CCT = value; }
        }

        private Int32 _CRI;
        public Int32 CRI
        {
            get { return this._CRI; }
            set { this._CRI = value; }
        }

        private SKUType _STKorMTO;
        public SKUType STKorMTO
        {
            get { return this._STKorMTO; }
            set { this._STKorMTO = value; }
        }

        private Int32 _CaseQty;
        public Int32 CaseQty
        {
            get { return this._CaseQty; }
            set { this._CaseQty = value; }
        }

        //Added By Liuqj
        private string _UL;
        public string UL
        {
            get { return this._UL; }
            set
            {
                if (value.Length > 60)
                {
                    throw new Exception(string.Format("The length of UL({0}) must be less then 60 characters.Please check it.", value));
                }
                else
                {
                    this._UL = value;
                }
            }
        }

        private string _ProductCategory;
        public string ProductCategory
        {
            get { return this._ProductCategory; }
            set { this._ProductCategory = value; }
        }

        private string _LEDChipType;
        public string LEDChipType
        {
            get { return this._LEDChipType; }
            set
            {
                if (value.Length > 30)
                {
                    throw new Exception(string.Format("The length of LED Chip Type({0}) must be less then 30 characters.Please check it.", value));
                }
                else
                {
                    this._LEDChipType = value;
                }
            }
        }

        private Int32 _LEDChipQty;
        public Int32 LEDChipQty
        {
            get { return this._LEDChipQty; }
            set { this._LEDChipQty = value; }
        }

        private string _DriverType;
        public string DriverType
        {
            get { return this._DriverType; }
            set 
            {
                if (value.Length > 30)
                {
                    throw new Exception(string.Format("The length of Driver Type({0}) must be less then 30 characters.Please check it.", value));
                }
                else
                {
                    this._DriverType = value;
                }
            }
        }

        private string _CustomerName;
        public string CustomerName
        {
            get { return this._CustomerName; }
            set
            {
                if (value.Length > 30)
                {
                    throw new Exception(string.Format("The length of Customer Name({0}) must be less then 30 characters.Please check it.", value));
                }
                else
                {
                    this._CustomerName = value;
                }
            }
        }
        //End By Liuqj

        private string _CreateUser;
        public string CreateUser
        {
            get { return this._CreateUser; }
            set 
            {
                if (value == string.Empty)
                {
                    throw new Exception("The parameter CreateUser should not be empty.Please check it.");
                }
                else
                {
                    this._CreateUser = value;
                }
            }
        }

        private DateTime _CreateDate;
        public DateTime CreateDate
        {
            get { return this._CreateDate; }
            set { this._CreateDate = value; }
        }

        private string _ModifiedUser;
        public string ModifiedUser
        {
            get { return this._ModifiedUser; }
            set
            {
                if (value == string.Empty)
                {
                    throw new Exception("The parameter ModifiedUser should not be empty.Please check it.");
                }
                else
                {
                    this._ModifiedUser = value;
                }
            }
        }

        private DateTime _ModifiedDate;
        public DateTime ModifiedDate
        {
            get { return this._ModifiedDate; }
            set {this._ModifiedDate = value; }
        }

        public SKU(string sku)
        {
            if (sku.Length > 30)
            {
                throw new Exception(string.Format("The length of SKU({0}) must be less then 30 characters.Please check it.", sku));
            }
            else
            {
                this._SKU = sku;
            }
        }
    }

    /// <summary>
    /// the functions of sku_mstr
    /// </summary>
    public class SKUHelper
    {
        private string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];

        public SKUHelper()
        {
        }

        public bool IsAlreadyExist(SKU sku)
        {
            try
            {
                string strSql = "sp_rdw_isSKUAlreadyExist";

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@sku", sku.Sku);
                sqlParam[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);

                return Convert.ToBoolean(sqlParam[1].Value.ToString());
            }
            catch
            {
                return false;
            }
        }

        public IList<SKU> Items(string code)
        {
            try
            {
                string strSql = "sp_rdw_selectSKU";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sku", code);

                DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

                if (table == null || (table != null && table.Rows.Count < 0))
                {
                    throw new Exception("There are no SKU records or the operation failed.");
                }
                else
                {
                    IList<SKU> SKUList = new List<SKU>();

                    foreach (DataRow row in table.Rows)
                    {
                        SKU sku = new SKU(row["SKU"].ToString());

                        sku.UPC = row["UPC"].ToString();
                        sku.Voltage = Convert.ToInt32(row["Voltage"].ToString());
                        sku.Wattage = Convert.ToInt32(row["Wattage"].ToString());
                        sku.Lumens = Convert.ToInt32(row["Lumens"].ToString());
                        sku.LPW = Convert.ToDecimal(row["LPW"].ToString());
                        sku.CBCPest = Convert.ToInt32(row["CBCPest"].ToString());
                        sku.BeamAngle = Convert.ToInt32(row["BeamAngle"].ToString());
                        sku.CCT = Convert.ToInt32(row["CCT"].ToString());
                        sku.CRI = Convert.ToInt32(row["CRI"].ToString());
                        sku.STKorMTO = (SKUType)Enum.Parse(typeof(SKUType), row["STKorMTO"].ToString());
                        sku.CaseQty = Convert.ToInt32(row["CaseQty"].ToString());
                        sku.UL = row["UL"].ToString();
                        sku.ProductCategory = row["ProductCategory"].ToString();
                        sku.LEDChipType = row["LEDChipType"].ToString();
                        sku.LEDChipQty = Convert.ToInt32(row["LEDChipQty"].ToString());
                        sku.DriverType = row["DriverType"].ToString();
                        sku.CustomerName = row["CustomerName"].ToString();
                        sku.CreateUser = row["CreateUser"].ToString();
                       
                        SKUList.Add(sku);
                    }

                    return SKUList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        public SKU Item(string code)
        {
            try
            {
                string strSql = "sp_rdw_selectSKU";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@sku", code);

                DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

                if (table == null || (table != null && table.Rows.Count <= 0))
                {
                    SKU sku = new SKU(code);

                    return sku;
                }
                else
                {
                    SKU sku = new SKU(table.Rows[0]["SKU"].ToString());

                    sku.UPC = table.Rows[0]["UPC"].ToString();
                    sku.Voltage = Convert.ToInt32(table.Rows[0]["Voltage"].ToString());
                    sku.Wattage = Convert.ToInt32(table.Rows[0]["Wattage"].ToString());
                    sku.Lumens = Convert.ToInt32(table.Rows[0]["Lumens"].ToString());
                    sku.LPW = Convert.ToDecimal(table.Rows[0]["LPW"].ToString());
                    sku.CBCPest = Convert.ToInt32(table.Rows[0]["CBCPest"].ToString());
                    sku.BeamAngle = Convert.ToInt32(table.Rows[0]["BeamAngle"].ToString());
                    sku.CCT = Convert.ToInt32(table.Rows[0]["CCT"].ToString());
                    sku.CRI = Convert.ToInt32(table.Rows[0]["CRI"].ToString());
                    sku.STKorMTO = (SKUType)Enum.Parse(typeof(SKUType), table.Rows[0]["STKorMTO"].ToString());
                    sku.CaseQty = Convert.ToInt32(table.Rows[0]["CaseQty"].ToString());
                    sku.UL = table.Rows[0]["UL"].ToString();
                    sku.ProductCategory = table.Rows[0]["ProductCategory"].ToString();
                    sku.LEDChipType = table.Rows[0]["LEDChipType"].ToString();
                    sku.LEDChipQty = Convert.ToInt32(table.Rows[0]["LEDChipQty"].ToString());
                    sku.DriverType = table.Rows[0]["DriverType"].ToString();
                    sku.CustomerName = table.Rows[0]["CustomerName"].ToString();
                    sku.CreateUser = table.Rows[0]["CreateUser"].ToString();  
                    return sku;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public void Add(SKU sku)
        {
            try
            {
                string strSql = "sp_rdw_updateSKU";
                SqlParameter[] sqlParam = new SqlParameter[20];
                sqlParam[0] = new SqlParameter("@SKU", sku.Sku);
                sqlParam[1] = new SqlParameter("@UPC", sku.UPC);
                sqlParam[2] = new SqlParameter("@Voltage", sku.Voltage);
                sqlParam[3] = new SqlParameter("@Wattage", sku.Wattage);
                sqlParam[4] = new SqlParameter("@Lumens", sku.Lumens);
                sqlParam[5] = new SqlParameter("@LPW", sku.LPW);
                sqlParam[6] = new SqlParameter("@CBCPest", sku.CBCPest);
                sqlParam[7] = new SqlParameter("@BeamAngle", sku.BeamAngle);
                sqlParam[8] = new SqlParameter("@CCT", sku.CCT);
                sqlParam[9] = new SqlParameter("@CRI", sku.CRI);
                sqlParam[10] = new SqlParameter("@STKorMTO", sku.STKorMTO);
                sqlParam[11] = new SqlParameter("@CaseQty", sku.CaseQty);
                sqlParam[12] = new SqlParameter("@UL", sku.UL);
                sqlParam[13] = new SqlParameter("@ProductCategory", sku.ProductCategory);
                sqlParam[14] = new SqlParameter("@LEDChipType", sku.LEDChipType);
                sqlParam[15] = new SqlParameter("@LEDChipQty", sku.LEDChipQty);
                sqlParam[16] = new SqlParameter("@DriverType", sku.DriverType);
                sqlParam[17] = new SqlParameter("@CustomerName", sku.CustomerName);
                sqlParam[18] = new SqlParameter("@CreateUser", sku.CreateUser);
                sqlParam[19] = new SqlParameter("@CreateDate", sku.CreateDate);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                throw new Exception("The operation failed.");
            }
        }

        public void Update(SKU sku)
        {
            try
            {
                string strSql = "sp_rdw_updateSKU";
                SqlParameter[] sqlParam = new SqlParameter[20];
                sqlParam[0] = new SqlParameter("@SKU", sku.Sku);
                sqlParam[1] = new SqlParameter("@UPC", sku.UPC);
                sqlParam[2] = new SqlParameter("@Voltage", sku.Voltage);
                sqlParam[3] = new SqlParameter("@Wattage", sku.Wattage);
                sqlParam[4] = new SqlParameter("@Lumens", sku.Lumens);
                sqlParam[5] = new SqlParameter("@LPW", sku.LPW);
                sqlParam[6] = new SqlParameter("@CBCPest", sku.CBCPest);
                sqlParam[7] = new SqlParameter("@BeamAngle", sku.BeamAngle);
                sqlParam[8] = new SqlParameter("@CCT", sku.CCT);
                sqlParam[9] = new SqlParameter("@CRI", sku.CRI);
                sqlParam[10] = new SqlParameter("@STKorMTO", sku.STKorMTO);
                sqlParam[11] = new SqlParameter("@CaseQty", sku.CaseQty);
                sqlParam[12] = new SqlParameter("@UL", sku.UL);
                sqlParam[13] = new SqlParameter("@ProductCategory", sku.ProductCategory);
                sqlParam[14] = new SqlParameter("@LEDChipType", sku.LEDChipType);
                sqlParam[15] = new SqlParameter("@LEDChipQty", sku.LEDChipQty);
                sqlParam[16] = new SqlParameter("@DriverType", sku.DriverType);
                sqlParam[17] = new SqlParameter("@CustomerName", sku.CustomerName);
                sqlParam[18] = new SqlParameter("@ModifiedUser", sku.ModifiedUser);
                sqlParam[19] = new SqlParameter("@ModifiedDate", sku.ModifiedDate);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                throw new Exception("The operation failed.");
            }
        }

        public void Delete(SKU sku)
        {
            try
            {
                string strSql = "sp_rdw_deleteSKU";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@SKU", sku.Sku);

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch (Exception ex)
            {
                throw new Exception("The operation failed.");
            }
        }
    }
}