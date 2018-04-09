using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using CommClass;


public partial class EDI_FIFOCostimport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    private bool InsertBatchTemp(string uID, string uName)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_insertFIFO", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckValidity(string uID)
    {
        Importcustpart(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));
        string newid_no = Request.Params["newid_no"];
        string strSql2 = " Select top 1 * From ChinaFIFOCostTemp where errMsg <> ''";

        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "<b>SOPO</b>~^<b>CUST</b>~^<b>SO</b>~^<b>LINE</b>~^<b>WO</b>~^200^<b>QAD</b>~^200^<b>CUSTITEM</b>~^200^<b>QTY</b>~^200^<b>SHIPDATE</b>~^<b>AluminumPCBCostRMB</b>~^<b>ConnectorCostRMB</b>~^<b>DriverCostRMB</b>~^<b>HeatSinkCostRMB</b>~^<b>LampBaseCostRMB</b>~^<b>LampShadeCostRMB</b>~^<b>LEDChipCostRMB</b>~^<b>OtherCostRMB</b>~^<b>PackageCostRMB</b>~^<b>PlasticCostRMB</b>~^<b>MaterialCostRMB</b>~^<b>LaborCostRMB</b>~^<b>OVERHEAD(RMB)</b>~^<b>Inv Price</b>~^<b>INV NO</b>~^120^<b>错误信息</b>~^";

                string sql = "  SELECT  [CustomerPO],[CustomerPOType],[SOPO] ,[Customer] ,[QADSalesOrder] ,[QADSalesOrderLine] ,[QADWO]  ,[QADItem] ,[CustomerItem] ,[Quantity],[ShipDate]    ,[AluminumPCBCostRMB],[ConnectorCostRMB],[DriverCostRMB] ,[HeatSinkCostRMB],[LampBaseCostRMB],[LampShadeCostRMB],[LEDChipCostRMB],[OtherCostRMB],[PackageCostRMB] ,[PlasticCostRMB]  ,[MaterialCostRMB] ,[LaborCostRMB],[OverheadCostRMB],[InvoicePriceUSD] ,[InvoiceNumber],errMsg FROM [tcpc0].[dbo].[ChinaFIFOCostTemp] ";

                DataTable dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('导入失败!');";
                ExportExcel(title, dt, false);
                return false;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed! \\n 提交信息验证失败！');Form1.usercode.focus();";
            return false;
        }
        return true;

    }

    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_clearFIFO", param);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean ImportExcelFile()
    {
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int line = 0;
        #region 上传文档例行处理
        strCatFolder = Server.MapPath("/import");

        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dt = this.GetExcelContents(strFileName);
                    //ds = chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('导入文件必须是Excel格式!');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    /*
                     *  导入的Excel文件必须满足：
                     *      1、至少应该有五列
                     *      2、从第五列开始即视为工序
                     *      3、工序名称必须在wo2_mop中存在
                    */



                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {

                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "SOPO")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 SOPO！');";
                            return false;
                        }
                        else if (col == 1 && dt.Columns[col].ColumnName.Trim() != "CUST")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 CUST！');";
                            return false;
                        }
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "SO")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 SO！');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "LINE")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 LINE！');";
                            return false;
                        }
                        else if (col == 4 && dt.Columns[col].ColumnName.Trim() != "WO")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 WO！');";
                            return false;
                        }
                        else if (col == 5 && dt.Columns[col].ColumnName.Trim() != "QAD")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 QAD！');";
                            return false;
                        }
                        else if (col == 6 && dt.Columns[col].ColumnName.Trim() != "CUSTITEM")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 CUSTITEM！');";
                            return false;
                        }
                        else if (col == 7 && dt.Columns[col].ColumnName.ToLower().Trim() != "qty")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 QTY！');";
                            return false;
                        }
                    }
                    #endregion
                    //构建ImportError
                    DataColumn column;
                    DataTable tblError = new DataTable("import_error");

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errInfo";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "plantCode";
                    tblError.Columns.Add(column);

                    DataRow rowError;//错误表的行

                    //转换成模板格式
                    DataTable table = new DataTable("ChinaFIFOCostTemp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "SOPO";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CustomerPO";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CustomerPOType";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Customer";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "QADSalesOrder";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "QADSalesOrderLine";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "QADWO";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "QADItem";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CustomerItem";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "Quantity";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ShipDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "AluminumPCBCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ConnectorCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "DriverCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "HeatSinkCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "LampBaseCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "LampShadeCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "LEDChipCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "OtherCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "PackageCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "PlasticCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "MaterialCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "LaborCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "OverheadCostRMB";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "MTL(RMB)";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "LBR(RMB)";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "InvoicePriceUSD";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "InvoiceNumber";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);
                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定
                            //domain的长度允许最长5个字符，否则截取
                            if (r["SOPO"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "SPOP不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                            else
                            {
                                row["SOPO"] = r["SOPO"].ToString().Trim();
                                row["CustomerPO"] = r["SOPO"].ToString().Trim().Substring(2);
                                row["CustomerPOType"] = r["SOPO"].ToString().Trim().Substring(0, 2);
                            }
                            //custCode的长度允许最长15个字符，否则截取
                            if (r["CUST"].ToString().Trim().Length == 0)
                            {

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "CUST不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                            
                            else
                            {
                                row["Customer"] = r["CUST"].ToString().Trim();
                            }

                            //custPart的长度允许最长20个字符，否则截取
                            if (r["SO"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "SO不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                           
                            else
                            {
                                row["QADSalesOrder"] = r["SO"].ToString().Trim();
                            }

                            //qad的长度允许最长18个字符，否则截取
                            if (r["LINE"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "物料号不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                          
                            else
                            {
                                row["QADSalesOrderLine"] = r["LINE"].ToString().Trim();
                            }
                            if (r["AluminumPCBCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["AluminumPCBCostRMB"] = Convert.ToDouble(r["AluminumPCBCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "AluminumPCBCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["ConnectorCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["ConnectorCostRMB"] = Convert.ToDouble(r["ConnectorCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "ConnectorCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["DriverCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["DriverCostRMB"] = Convert.ToDouble(r["DriverCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "DriverCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["HeatSinkCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["HeatSinkCostRMB"] = Convert.ToDouble(r["HeatSinkCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "HeatSinkCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["LampBaseCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["LampBaseCostRMB"] = Convert.ToDouble(r["LampBaseCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "LampBaseCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["AluminumPCBCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["AluminumPCBCostRMB"] = Convert.ToDouble(r["AluminumPCBCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "AluminumPCBCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["LampShadeCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["LampShadeCostRMB"] = Convert.ToDouble(r["LampShadeCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "LampShadeCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["LEDChipCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["LEDChipCostRMB"] = Convert.ToDouble(r["LEDChipCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "LEDChipCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["OtherCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["OtherCostRMB"] = Convert.ToDouble(r["OtherCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "OtherCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["PackageCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["PackageCostRMB"] = Convert.ToDouble(r["PackageCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "PackageCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["PlasticCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["PlasticCostRMB"] = Convert.ToDouble(r["PlasticCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "PlasticCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["MaterialCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["MaterialCostRMB"] = Convert.ToDouble(r["MaterialCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "MaterialCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["LaborCostRMB"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["LaborCostRMB"] = Convert.ToDouble(r["LaborCostRMB"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "LaborCostRMB格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            if (r["OVERHEAD(RMB)"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["OVERHEAD(RMB)"] = Convert.ToDouble(r["OVERHEAD(RMB)"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "OVERHEAD(RMB)格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }
                            //if (r["MTL(RMB)"].ToString().Trim().Length != 0)
                            //{
                            //    try
                            //    {
                            //        r["MTL(RMB)"] = Convert.ToDouble(r["MTL(RMB)"].ToString().Trim()).ToString("0.0000");
                            //    }
                            //    catch (Exception)
                            //    {
                            //        rowError = tblError.NewRow();
                            //        rowError["errInfo"] = "MTL(RMB)格式不正确,见表" + line + "行";
                            //        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                            //        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                            //        tblError.Rows.Add(rowError);
                            //    }
                            //}
                            //if (r["LBR(RMB)"].ToString().Trim().Length != 0)
                            //{
                            //    try
                            //    {
                            //        r["LBR(RMB)"] = Convert.ToDouble(r["LBR(RMB)"].ToString().Trim()).ToString("0.0000");
                            //    }
                            //    catch (Exception)
                            //    {
                            //        rowError = tblError.NewRow();
                            //        rowError["errInfo"] = "LBR(RMB)格式不正确,见表" + line + "行";
                            //        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                            //        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                            //        tblError.Rows.Add(rowError);
                            //    }
                            //}
                            if (r["Inv Price"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["Inv Price"] = Convert.ToDouble(r["Inv Price"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    rowError = tblError.NewRow();
                                    rowError["errInfo"] = "Inv Price格式不正确,见表" + line + "行";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                    tblError.Rows.Add(rowError);
                                }
                            }

                            row["QADWO"] = r["WO"].ToString().Trim();
                            row["QADItem"] = r["QAD"].ToString().Trim();
                            row["CustomerItem"] = r["CUSTITEM"].ToString().Trim();
                            try
                            {
                                int qty = Convert.ToInt32(r["QTY"].ToString().Trim());
                                row["Quantity"] = r["QTY"].ToString().Trim();
                            }
                            catch (Exception)
                            {
                                rowError = tblError.NewRow();
                                rowError["errInfo"] = "QTY不为整数,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                tblError.Rows.Add(rowError);
                            }

                            try
                            {
                                DateTime dates = Convert.ToDateTime(r["SHIPDATE"].ToString().Trim());
                                row["ShipDate"] = r["SHIPDATE"].ToString().Trim();
                            }
                            catch (Exception)
                            {
                                rowError = tblError.NewRow();
                                rowError["errInfo"] = "SHIPDATE格式不正确,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                tblError.Rows.Add(rowError);
                            }
                           

                            row["AluminumPCBCostRMB"] = r["AluminumPCBCostRMB"].ToString().Trim();
                            row["ConnectorCostRMB"] = r["ConnectorCostRMB"].ToString().Trim();
                            row["DriverCostRMB"] = r["DriverCostRMB"].ToString().Trim();
                            row["HeatSinkCostRMB"] = r["HeatSinkCostRMB"].ToString().Trim();
                            row["LampBaseCostRMB"] = r["LampBaseCostRMB"].ToString().Trim();
                            row["LampShadeCostRMB"] = r["LampShadeCostRMB"].ToString().Trim();
                            row["LEDChipCostRMB"] = r["LEDChipCostRMB"].ToString().Trim();
                            row["OtherCostRMB"] = r["OtherCostRMB"].ToString().Trim();
                            row["PackageCostRMB"] = r["PackageCostRMB"].ToString().Trim();
                            row["PlasticCostRMB"] = r["PlasticCostRMB"].ToString().Trim();
                            row["MaterialCostRMB"] = r["MaterialCostRMB"].ToString().Trim();
                            row["LaborCostRMB"] = r["LaborCostRMB"].ToString().Trim();
                            //row["MTL(RMB)"] = r["MTL(RMB)"].ToString().Trim();
                            //row["LBR(RMB)"] = r["LBR(RMB)"].ToString().Trim();
                            row["OverheadCostRMB"] = r["OVERHEAD(RMB)"].ToString().Trim();
                            row["InvoicePriceUSD"] = r["Inv Price"].ToString().Trim();
                            row["InvoiceNumber"] = r["INV NO"].ToString().Trim();
                            
                            
                            #endregion

                          
                            row["errMsg"] = string.Empty;

                            table.Rows.Add(row);
                        }

                        if (tblError != null && tblError.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopyError.DestinationTableName = "dbo.FIFOError";
                                bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                                bulkCopyError.ColumnMappings.Add("uID", "userID");
                                bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                                try
                                {
                                    bulkCopyError.WriteToServer(tblError);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('错误表导入错误,请联系系统管理员!');";
                                    return false;
                                }
                                finally
                                {
                                    bulkCopyError.Close();
                                    tblError.Dispose();
                                }
                            }
                        }
                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.ChinaFIFOCostTemp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("CustomerPO", "CustomerPO");
                                bulkCopy.ColumnMappings.Add("CustomerPOType", "CustomerPOType");
                                bulkCopy.ColumnMappings.Add("SOPO", "SOPO");
                                bulkCopy.ColumnMappings.Add("Customer", "Customer");
                                bulkCopy.ColumnMappings.Add("QADSalesOrder", "QADSalesOrder");
                                bulkCopy.ColumnMappings.Add("QADSalesOrderLine", "QADSalesOrderLine");
                                bulkCopy.ColumnMappings.Add("QADWO", "QADWO");
                                bulkCopy.ColumnMappings.Add("QADItem", "QADItem");
                                bulkCopy.ColumnMappings.Add("CustomerItem", "CustomerItem");
                                bulkCopy.ColumnMappings.Add("Quantity", "Quantity");
                                bulkCopy.ColumnMappings.Add("ShipDate", "ShipDate");
                                bulkCopy.ColumnMappings.Add("AluminumPCBCostRMB", "AluminumPCBCostRMB");
                                bulkCopy.ColumnMappings.Add("ConnectorCostRMB", "ConnectorCostRMB");
                                bulkCopy.ColumnMappings.Add("DriverCostRMB", "DriverCostRMB");
                                bulkCopy.ColumnMappings.Add("HeatSinkCostRMB", "HeatSinkCostRMB");
                                bulkCopy.ColumnMappings.Add("LampBaseCostRMB", "LampBaseCostRMB");
                                bulkCopy.ColumnMappings.Add("LampShadeCostRMB", "LampShadeCostRMB");
                                bulkCopy.ColumnMappings.Add("LEDChipCostRMB", "LEDChipCostRMB");
                                bulkCopy.ColumnMappings.Add("OtherCostRMB", "OtherCostRMB");
                                bulkCopy.ColumnMappings.Add("PackageCostRMB", "PackageCostRMB");
                                bulkCopy.ColumnMappings.Add("PlasticCostRMB", "PlasticCostRMB");
                                bulkCopy.ColumnMappings.Add("MaterialCostRMB", "MaterialCostRMB");
                                bulkCopy.ColumnMappings.Add("LaborCostRMB", "LaborCostRMB");
                                bulkCopy.ColumnMappings.Add("OverheadCostRMB", "OverheadCostRMB");
                                //bulkCopy.ColumnMappings.Add("MTL(RMB)", "MTL(RMB)");
                                //bulkCopy.ColumnMappings.Add("LBR(RMB)", "LBR(RMB)");
                                bulkCopy.ColumnMappings.Add("InvoicePriceUSD", "InvoicePriceUSD");
                                bulkCopy.ColumnMappings.Add("InvoiceNumber", "InvoiceNumber");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('导入临时表时出错，请联系系统管理员！');";
                                    return false;
                                }
                                finally
                                {
                                    table.Dispose();
                                }
                            }
                        }
                    }
                }

                dt.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }

    public int Importcustpart(Int32 uID, Int32 plantcode)
    {
        string strSQL = "sp_FIFO_checkimport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@plantcode", plantcode);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        return Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm));

    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (CheckValidity(Session["uID"].ToString()))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('导入成功!');";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!');";
                }
            }
            else
            {
                // ltlAlert.Text = "window.open('CustPartImportError.aspx?rt=" + DateTime.Now.ToString() + "', '_blank');";
            }
        }
    }
}