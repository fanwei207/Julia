using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

/// <summary>
/// TCP-CHINA Operating Management System
/// </summary>
public class OMSHelper
{
    private static adamClass adm = new adamClass();

    private OMSHelper() { }

    #region Factory Status
    public static DataTable GetFSCategory(string name, string desc)
    {
        string sp = "sp_OMS_selectFSCatagory";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@name", name);
        parms[1] = new SqlParameter("@desc", desc);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    public static bool CheckFSCatagory(string name, int id)
    {
        string sp = "sp_OMS_checkFSCatagory";
        SqlParameter[] parms = new SqlParameter[3];
        parms[0] = new SqlParameter("@name", name);
        parms[1] = new SqlParameter("@id", id);
        parms[2] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[2].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);
        return Convert.ToBoolean(parms[2].Value);
    }
    public static bool InsertFSCatagory(string typeName, string desc, int uId, string uName)
    {
        string sp = "sp_OMS_insertFSCatagory";
        SqlParameter[] parms = new SqlParameter[5];
        parms[0] = new SqlParameter("@name", typeName);
        parms[1] = new SqlParameter("@desc", desc);
        parms[2] = new SqlParameter("@uId", uId);
        parms[3] = new SqlParameter("@uName", uName);
        parms[4] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[4].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);

        return Convert.ToBoolean(parms[4].Value);
    }
    public static bool ModifyFSCatagory(string typeName, string desc, int fs_caId)
    {
        string sp = "sp_OMS_modifyFSCatagory";
        SqlParameter[] parms = new SqlParameter[4];
        parms[0] = new SqlParameter("@name", typeName);
        parms[1] = new SqlParameter("@desc", desc);
        parms[2] = new SqlParameter("@fsCaId", fs_caId);
        parms[3] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[3].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);

        return Convert.ToBoolean(parms[3].Value);
    }
    public static bool DeleteFSCatagory(int fs_caId)
    {
        string sp = "sp_OMS_deleteFSCatagory";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@fsCaId", fs_caId);
        parms[1] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);
        return Convert.ToBoolean(parms[1].Value);
    }
    public static bool CheckFDeleteFSCatagory(int id)
    {
        string sp = "sp_OMS_checkdeleteFSCatagory";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@id", id);
        parms[1] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);
        return Convert.ToBoolean(parms[1].Value);
    }

    public static DataTable GetFactoryStatus(string cu, int fsc_id, string docName, int domain, int impt)
    {
        string sp = "sp_OMS_selectFSDocuments";
        SqlParameter[] parms = new SqlParameter[5];
        parms[0] = new SqlParameter("@cust", cu);
        parms[1] = new SqlParameter("@fsc_id", fsc_id);
        parms[2] = new SqlParameter("@docName", docName);
        parms[3] = new SqlParameter("@fsd_domain", domain);
        parms[4] = new SqlParameter("@impt", impt);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    public static DataTable GetCustomer()
    {
        string sp = "sp_OMS_selectCustomer";
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sp).Tables[0];
    }

    public static bool InsertFSDocuments(string path, string name, string customer, string domain, int FS_Catagory_Id, int uid, string uName, int impt)
    {
        string sp = "sp_OMS_InsertFSDocuments";

        SqlParameter[] prams = new SqlParameter[9];
        prams[0] = new SqlParameter("@FS_Documents_Path", path);
        prams[1] = new SqlParameter("@FS_Documents_Name", name);
        prams[2] = new SqlParameter("@FS_Documents_Cu_ad_addr", customer);
        prams[3] = new SqlParameter("@FS_cuDomain", domain);
        prams[4] = new SqlParameter("@FS_Category_Id", FS_Catagory_Id);
        prams[5] = new SqlParameter("@FS_Documents_UpLoadBy", uid);
        prams[6] = new SqlParameter("@FS_UpLoadName", uName);
        prams[7] = new SqlParameter("@impt", impt);
        prams[8] = new SqlParameter("@reValue", SqlDbType.Bit);
        prams[8].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, prams);
        return Convert.ToBoolean(prams[8].Value);
    }
    public static bool CheckFSDocumentsExists(string path, int FS_Cata_Id)
    {
        string sp = "sp_OMS_checkFSDocExists";
        SqlParameter[] prams = new SqlParameter[3];
        prams[0] = new SqlParameter("@filePath", path);
        prams[1] = new SqlParameter("@FS_Catagory_Id", FS_Cata_Id);
        prams[2] = new SqlParameter("@reValue", SqlDbType.Bit);
        prams[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, prams);
        return Convert.ToBoolean(prams[2].Value);
    }

    public static bool DeleteFSDocument(int id)
    {
        string sp = "sp_OMS_deleteFSDocument";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@fsId", id);
        parms[1] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);

        return Convert.ToBoolean(parms[1].Value);
    }

    public static string GetFSDocumentPath(int id)
    {
        string sp = "sp_OMS_getFSDocPath";
        SqlParameter parms = new SqlParameter("@fsd_id", id);
        DataTable dt = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sp, parms).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
            return "";
    }

    #endregion

    #region Forecast
    public static DataTable GetCustomerPart(string customer, string domain)
    {
        string sp = "sp_FS_Fc_selectCuPart";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@cu", customer);
        parms[1] = new SqlParameter("@domain", domain);

        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    /// <summary>
    /// 获取QAD号
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public static string GetQADPart(string product, string customer)
    {
        string sp = "sp_OMS_SelectQADPart";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@product", product);
        parms[1] = new SqlParameter("@customer", customer);
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sp, parms).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
            return "";
    }
    public static DataTable GetForecast(string customer, string cuPart, DateTime dt)
    {
        string sp = "sp_OMS_selectForecast";
        SqlParameter[] parms = new SqlParameter[3];
        parms[0] = new SqlParameter("@fc_cu", customer);
        parms[1] = new SqlParameter("@fc_pr_nbr", cuPart);
        parms[2] = new SqlParameter("@fc_date", dt);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sp, parms).Tables[0];
    }
    public static bool InsertForecast(string cu, string cuPart, string part, DateTime dt, int qty, string unit, int uId)
    {
        string sp = "sp_OMS_insertForecast";
        SqlParameter[] parms = new SqlParameter[8];
        parms[0] = new SqlParameter("@cu", cu);
        parms[1] = new SqlParameter("@cuPart", cuPart);
        parms[2] = new SqlParameter("@part", part);
        parms[3] = new SqlParameter("@date", dt);
        parms[4] = new SqlParameter("@qty", qty);
        parms[5] = new SqlParameter("@unit", unit);
        parms[6] = new SqlParameter("@uId", uId);
        parms[7] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[7].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);
        return Convert.ToBoolean(parms[7].Value);
    }
    public static bool ModifyForecast(int id, string cuPart, string part, DateTime dt, int qty, string unit, int uId)
    {
        string sp = "sp_OMS_modifyForecast";
        SqlParameter[] parms = new SqlParameter[8];
        parms[0] = new SqlParameter("@fc_id", id);
        parms[1] = new SqlParameter("@cuPart", cuPart);
        parms[2] = new SqlParameter("@part", part);
        parms[3] = new SqlParameter("@date", dt);
        parms[4] = new SqlParameter("@qty", qty);
        parms[5] = new SqlParameter("@unit", unit);
        parms[6] = new SqlParameter("@uId", uId);
        parms[7] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[7].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);
        return Convert.ToBoolean(parms[7].Value);
    }
    public static bool DeleteForecast(int fc_id)
    {
        string sp = "sp_OMS_deleteForecast";
        SqlParameter[] parms = new SqlParameter[2];
        parms[0] = new SqlParameter("@fc_id", fc_id);
        parms[1] = new SqlParameter("@reValue", SqlDbType.Bit);
        parms[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, sp, parms);
        return Convert.ToBoolean(parms[1].Value);
    }
    #endregion

    #region Project Tracking
    //
    /// <summary>
    /// 查询主题页
    /// </summary>
    /// <returns></returns>
    public static DataSet SelectTaskGVMessage(string cust, string keywords, string status, string me)
    {
        try
        {
            string strSql = "sp_oms_selectGVMessage";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@cust", cust);
            parms[1] = new SqlParameter("@keywords", keywords);
            parms[1] = new SqlParameter("@status", status);
            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, parms);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 查询留言回复
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public static DataSet SelectTaskGVMessage(string parentID, string cust, string keywords)
    {
        try
        {
            string strSql = "sp_oms_selectGVMessage";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@parentID", parentID);
            param[1] = new SqlParameter("@cust", cust);
            param[2] = new SqlParameter("@keywords", keywords);
            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 插入主题或留言
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="message"></param>
    /// <param name="fpath"></param>
    /// <param name="fname"></param>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public static bool insertTaskMessage(string id, string name, string message, string fpath, string fname, string parentID, string det, string cust)
    {
        try
        {
            string strSql = "sp_oms_insertMessage";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@name", name);
            param[2] = new SqlParameter("@desc", message);
            param[3] = new SqlParameter("@fpath", fpath);
            param[4] = new SqlParameter("@fname", fname);
            param[5] = new SqlParameter("@parentID", parentID);
            param[6] = new SqlParameter("@detdesc", det);
            param[7] = new SqlParameter("@reValue", SqlDbType.Bit);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@cust", cust);
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param);
            return Convert.ToBoolean(param[7].Value);
        }
        catch
        {
            return false;
        }
    }
    public static bool closeTaskMessage(string id)
    {
        try
        {
            string strSql = "sp_OMS_closeGVMessage";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param);
            return true;

        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// OrderTracking查询
    /// </summary>
    /// <param name="po1"></param>
    /// <param name="po2"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <param name="region"></param>
    /// <param name="customer"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public static DataTable GetOrderTracking(string po1, string po2, string dateFrom, string dateTo, string region, string customer, string status)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@po1", po1);
        param[1] = new SqlParameter("@po2", po2);
        param[2] = new SqlParameter("@ordDate1", dateFrom);
        param[3] = new SqlParameter("@ordDate2", dateTo);
        param[4] = new SqlParameter("@region", region);
        param[5] = new SqlParameter("@customer", customer);
        param[6] = new SqlParameter("@status", status);
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_selectOrderTrackingNew", param).Tables[0];
    }
    public static DataTable GetRegions()
    {
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_selectCmRegion").Tables[0];
    }
    public static void RefreshOrderTracking()
    {
        SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_job_OrderTracking");
    }
    #endregion

    #region Products
    public static DataTable GetProducts(string custCode, string custPart, string part)
    {
        string strName = "sp_oms_selectCustomerProduct";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@custCode", custCode);
        parm[1] = new SqlParameter("@custPart", custPart);
        parm[2] = new SqlParameter("@part", part);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);

        return _dataset.Tables[0];
    }
    public static DataTable GetProductDescDetail(string id, string itemNumber)
    {
        string sqlstr = "sp_oms_selectProductDescByIdOrCode ";
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@id",id),
            new SqlParameter("@code",itemNumber)
            };
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public static DataTable GetProductDesc(string ItemNumber, string UPCNumber)
    {
        string sqlstr = "sp_oms_selectProductDesc";
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@ItemNumber",ItemNumber),
            new SqlParameter("@UPCNumber",UPCNumber)
            };
        return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }
    ///<summary>
    ///绑定数据到GridView，当表格数据为空时显示表头
    ///</summary>
    ///<param name="gridview"></param>
    ///<param name="table"></param>
    public static void GridViewDataBind(GridView gridview, DataTable table)
    {
        //记录为空重新构造Gridview
        if (table.Rows.Count == 0)
        {
            table = table.Clone();
            table.Rows.Add(table.NewRow());
            gridview.DataSource = table;
            gridview.DataBind();
            int columnCount = gridview.Rows[0].Cells.Count;
            gridview.Rows[0].Cells.Clear();
            gridview.Rows[0].Cells.Add(new TableCell());
            gridview.Rows[0].Cells[0].ColumnSpan = columnCount;
            gridview.Rows[0].Cells[0].Text = "No Record";
            gridview.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            //数据不为空直接绑定
            gridview.DataSource = table;
            gridview.DataBind();
        }

        //重新绑定取消选择
        gridview.SelectedIndex = -1;
        //释放表，是放表所占的空间
        table.Dispose();
    }
    public static bool DeleteProductDeseByID(int id)
    {
        string sqlstr = "sp_oms_deleteProductDesc";
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@id",id)
            };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    /////////////////////////////////////////////////////////////
    //Excel功能块
    ////////////////////////////////////////////////////////////


    /// <summary>
    /// 清空临时表中上传者所有记录
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private static bool ClearTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_deleteProductImportTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summmary>
    /// 数据库端对临时表进行检查
    /// </summmary>
    private static bool CheckTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_checkProductDescTemp", param));
        }
        catch
        {
            return false;
        }
    }



    /// <summary>
    /// 判断临时表中该上传者导入是否有错误
    /// 对error进行检测
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private static bool JudgeTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_checkProductImportTempError", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 将产品目录临时表更新到正式表里
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    /// 
    private static bool TransTempTable(int createdBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]{
                
                new SqlParameter("@createdBy", createdBy),
                
            };

            bool a = Convert.ToBoolean(SqlHelper.ExecuteScalar(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_insertProductDescFromTemp", param));
            return a;
        }
        catch
        {
            return false;
        }
    }


    public static bool Import(string filePath, string uId, string uName, out string message)
    {

        message = "";
        DataTable dt = null;
        bool success = true;
        try
        {
            dt = adm.getExcelContents(filePath).Tables[0];//获取excel的内容
        }
        catch (Exception ex)
        {
            message = "'The upload file must be Excel'" + ex.Message.ToString() + "'.";
            success = false;
        }
        finally
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        if (success)
        {
            try
            {

                if (
                    dt.Columns[0].ColumnName != "Item Number" ||
                    dt.Columns[1].ColumnName != "UPC Number" ||
                    dt.Columns[2].ColumnName != "Description" ||
                    dt.Columns[3].ColumnName != "Wattage" ||
                    dt.Columns[4].ColumnName != "Equiv" ||
                    dt.Columns[5].ColumnName != "Lumens" ||
                    dt.Columns[6].ColumnName != "LPW" ||
                    dt.Columns[7].ColumnName != "CBCPest" ||
                    dt.Columns[8].ColumnName != "BeamAngle" ||
                    dt.Columns[9].ColumnName != "CCT" ||
                    dt.Columns[10].ColumnName != "CRI" ||
                    dt.Columns[11].ColumnName != "MOL" ||
                    dt.Columns[12].ColumnName != "Dia" ||
                    dt.Columns[13].ColumnName != "IP" ||
                    dt.Columns[14].ColumnName != "MP" ||
                    dt.Columns[15].ColumnName != "List" ||
                    dt.Columns[16].ColumnName != "A4 Price" ||
                    dt.Columns[17].ColumnName != "Price" ||
                    dt.Columns[18].ColumnName != "STK/MTO" ||
                    dt.Columns[19].ColumnName != "LM79/Life" ||
                    dt.Columns[20].ColumnName != "IES" ||
                    dt.Columns[21].ColumnName != "UL" ||
                    dt.Columns[22].ColumnName != "LDL" ||
                    dt.Columns[23].ColumnName != "Energy Star" ||
                    dt.Columns[24].ColumnName != "Model #"
                    ||
                    dt.Columns[25].ColumnName != "Caution Statement" ||
                    dt.Columns[26].ColumnName != "Country of Origin" ||
                    dt.Columns[27].ColumnName != "pf" ||
                    dt.Columns[28].ColumnName != "Date Code"
                    ||
                    dt.Columns[29].ColumnName != "UL File #" ||
                    dt.Columns[30].ColumnName != "UL Control #" ||
                    dt.Columns[31].ColumnName != "UL Group" ||
                    dt.Columns[32].ColumnName != "Voltage"
                    ||
                    dt.Columns[33].ColumnName != "Frequency" ||
                    dt.Columns[34].ColumnName != "Amperage"


                    )
                {
                    dt.Reset();
                    message = "The upload file template is error，Please update your template!";
                    success = false;
                }

                DataTable TempTable = new DataTable("TempTable");
                DataColumn TempColumn;
                DataRow TempRow;
                // DataRow TempRow;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = dt.Columns[i].ColumnName;
                    TempTable.Columns.Add(TempColumn);
                }
                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "error";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Int32");
                TempColumn.ColumnName = "createBy";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.DateTime");
                TempColumn.ColumnName = "createDate";
                TempTable.Columns.Add(TempColumn);

                if (dt.Rows.Count > 0)
                {

                    string ItemNumber = string.Empty;
                    
                    string error = string.Empty;


                    string createBy = uId;
                    string createDate = DateTime.Now.ToString();

                    DateTime dateFormat = DateTime.Now;

                    //先清空临时表中该上传员工的记录
                    if (ClearTempTable(Convert.ToInt32(uId)))
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        { 
                            if (dt.Rows[i].IsNull(0))
                            {
                                error += "Item ItemNumber can not be empty!";
                            }

                            TempRow = TempTable.NewRow();
                            int jj = 0;
                            while (jj < dt.Columns.Count)
                            {

                                TempRow[(dt.Columns[jj].ColumnName.ToString())] = dt.Rows[i].ItemArray[jj].ToString().Trim();
                                jj++;
                            }
                            TempRow["createBy"] = Convert.ToInt32(createBy);
                            TempRow["createDate"] = createDate;
                            TempRow["error"] = error;
                            TempTable.Rows.Add(TempRow);
                        }
                        //TempTable有数据的情况下批量复制到数据库里
                        if (TempTable != null && TempTable.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adm.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopy.DestinationTableName = "product_Import_temp";

                                bulkCopy.ColumnMappings.Clear();

                                bulkCopy.ColumnMappings.Add(dt.Columns[0].ColumnName.ToString().Trim(), "Item_Number");
                                bulkCopy.ColumnMappings.Add(dt.Columns[1].ColumnName.ToString().Trim(), "UPC_Number");
                                bulkCopy.ColumnMappings.Add(dt.Columns[2].ColumnName.ToString().Trim(), "Description");
                                bulkCopy.ColumnMappings.Add(dt.Columns[3].ColumnName.ToString().Trim(), "Wattage");
                                bulkCopy.ColumnMappings.Add(dt.Columns[4].ColumnName.ToString().Trim(), "Equiv");
                                bulkCopy.ColumnMappings.Add(dt.Columns[5].ColumnName.ToString().Trim(), "Lumens");
                                bulkCopy.ColumnMappings.Add(dt.Columns[6].ColumnName.ToString().Trim(), "LPW");
                                bulkCopy.ColumnMappings.Add(dt.Columns[7].ColumnName.ToString().Trim(), "CBCPest");
                                bulkCopy.ColumnMappings.Add(dt.Columns[8].ColumnName.ToString().Trim(), "BeamAngle");
                                bulkCopy.ColumnMappings.Add(dt.Columns[9].ColumnName.ToString().Trim(), "CCT");
                                bulkCopy.ColumnMappings.Add(dt.Columns[10].ColumnName.ToString().Trim(), "CRI");
                                bulkCopy.ColumnMappings.Add(dt.Columns[11].ColumnName.ToString().Trim(), "MOL");
                                bulkCopy.ColumnMappings.Add(dt.Columns[12].ColumnName.ToString().Trim(), "Dia");
                                bulkCopy.ColumnMappings.Add(dt.Columns[13].ColumnName.ToString().Trim(), "IP");
                                bulkCopy.ColumnMappings.Add(dt.Columns[14].ColumnName.ToString().Trim(), "MP");
                                bulkCopy.ColumnMappings.Add(dt.Columns[15].ColumnName.ToString().Trim(), "List");
                                bulkCopy.ColumnMappings.Add(dt.Columns[16].ColumnName.ToString().Trim(), "A4");
                                bulkCopy.ColumnMappings.Add(dt.Columns[17].ColumnName.ToString().Trim(), "Price");
                                bulkCopy.ColumnMappings.Add(dt.Columns[18].ColumnName.ToString().Trim(), "[STK/MTO]");
                                bulkCopy.ColumnMappings.Add(dt.Columns[19].ColumnName.ToString().Trim(), "[LM79/Life]");
                                bulkCopy.ColumnMappings.Add(dt.Columns[20].ColumnName.ToString().Trim(), "IES");
                                bulkCopy.ColumnMappings.Add(dt.Columns[21].ColumnName.ToString().Trim(), "UL");
                                bulkCopy.ColumnMappings.Add(dt.Columns[22].ColumnName.ToString().Trim(), "LDL");
                                bulkCopy.ColumnMappings.Add(dt.Columns[23].ColumnName.ToString().Trim(), "EnergyStar");
                                bulkCopy.ColumnMappings.Add(dt.Columns[24].ColumnName.ToString().Trim(), "Model#");
                                bulkCopy.ColumnMappings.Add(dt.Columns[25].ColumnName.ToString().Trim(), "CautionStatement");
                                bulkCopy.ColumnMappings.Add(dt.Columns[26].ColumnName.ToString().Trim(), "CountryOfOrigin");
                                bulkCopy.ColumnMappings.Add(dt.Columns[27].ColumnName.ToString().Trim(), "pf");
                                bulkCopy.ColumnMappings.Add(dt.Columns[28].ColumnName.ToString().Trim(), "DateCode");
                                bulkCopy.ColumnMappings.Add(dt.Columns[29].ColumnName.ToString().Trim(), "UL_File#");
                                bulkCopy.ColumnMappings.Add(dt.Columns[30].ColumnName.ToString().Trim(), "UL_Control#");
                                bulkCopy.ColumnMappings.Add(dt.Columns[31].ColumnName.ToString().Trim(), "UL_Group");
                                bulkCopy.ColumnMappings.Add(dt.Columns[32].ColumnName.ToString().Trim(), "Voltage");
                                bulkCopy.ColumnMappings.Add(dt.Columns[33].ColumnName.ToString().Trim(), "Frequency");
                                bulkCopy.ColumnMappings.Add(dt.Columns[34].ColumnName.ToString().Trim(), "Amperage");

                                bulkCopy.ColumnMappings.Add("createBy", "CreatedBy");
                                bulkCopy.ColumnMappings.Add("createDate", "CreatedDate");
                                bulkCopy.ColumnMappings.Add("error", "error");

                                try
                                {
                                    bulkCopy.WriteToServer(TempTable);
                                }
                                catch (Exception ex)
                                {
                                    message = "uploading Exception，please submit to admin！";
                                    success = false;
                                }
                                finally
                                {
                                    TempTable.Dispose();
                                    bulkCopy.Close();
                                }
                            }
                        }
                        dt.Reset();
                        if (success)
                        {
                            //数据库端验证
                            if (CheckTempTable(Convert.ToInt32(uId)))
                            {
                                //判断上传内容能否通过验证
                                if (JudgeTempTable(Convert.ToInt32(uId)))
                                {
                                    if (TransTempTable(Convert.ToInt32(uId)))
                                    {
                                        message = "upload success";
                                        success = true;
                                    }
                                    else
                                    {
                                        message = "uploading Exception，please submit to admin!";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    message = "upload success,but exist error!";
                                    success = false;
                                }
                            }
                            else
                            {
                                message = "uploading Exception，please submit to admin!";
                                success = false;
                            }
                        }
                    }
                    else
                    {
                        message = "uploading Exception，please submit to admin!";
                        success = false;

                    }
                }
            }
            catch
            {
                message = "Upload  failed!";
                success = false;
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
        return success;
    }

    public static DataTable GetImportError(string createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);
            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, "sp_oms_getProductImportTempByUID", param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    #endregion
}