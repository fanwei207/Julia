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

public partial class EDI_FIFOCostDetimport : BasePage
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

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_insertFIFODet", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckValidity(string uID)
    {
       // Importcustpart(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));
        string newid_no = Request.Params["newid_no"];
        string strSql2 = " Select top 1 * From FIFOError where userID ='" + Convert.ToInt32(Session["uID"]) + "' and  plantID= '" + Convert.ToInt32(Session["PlantCode"]) + "'";

        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "120^<b>错误信息</b>~^";

                string sql = " select ErrorInfo from FIFOError Where userID='" + Convert.ToInt32(Session["uID"]) + "' ";

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

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_clearFIFODet", param);

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

                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "产品")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 产品！');";
                            return false;
                        }
                        else if (col == 1 && dt.Columns[col].ColumnName.Trim() != "零件")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 零件！');";
                            return false;
                        }
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "描述")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 描述！');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "单位")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 单位！');";
                            return false;
                        }
                        else if (col == 4 && dt.Columns[col].ColumnName.Trim() != "用量")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 用量！');";
                            return false;
                        }
                        else if (col == 5 && dt.Columns[col].ColumnName.Trim() != "损耗率")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 损耗率！');";
                            return false;
                        }
                        else if (col == 6 && dt.Columns[col].ColumnName.Trim() != "实用量")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 实用量！');";
                            return false;
                        }
                        else if (col == 7 && dt.Columns[col].ColumnName.ToLower().Trim() != "标准本层")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 标准本层！');";
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
                    DataTable table = new DataTable("ChinaFIFOCostDetTemp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Product";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Part";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Description";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "EA";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Consumption";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "attrition";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Practical";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "layerStandard";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "StandardLower";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "GLPrice";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "GLAmount";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CurrentLayer";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CurrentLowLevel";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CUPrice";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CUAmount";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Difference";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "gradation";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "PurchasePriceDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Supplier";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "domain";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "GrantingPrinciples";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ComponentType";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ExchangeRate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Hours";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "LaborCosts";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "SOPO";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ReleaseDate";
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
                            if (r["产品"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "产品不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                            else
                            {
                                row["Product"] = r["产品"].ToString().Trim();
                            }
                            //custCode的长度允许最长15个字符，否则截取
                            if (r["零件"].ToString().Trim().Length == 0)
                            {

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "零件不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            else
                            {
                                row["Part"] = r["零件"].ToString().Trim();
                            }

                            //custPart的长度允许最长20个字符，否则截取
                            if (r["描述"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "描述不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            else
                            {
                                row["Description"] = r["描述"].ToString().Trim();
                            }

                            //qad的长度允许最长18个字符，否则截取
                            if (r["单位"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "单位号不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            else
                            {
                                row["EA"] = r["单位"].ToString().Trim();
                            }

                            row["Consumption"] = r["用量"].ToString().Trim();
                            row["attrition"] = r["损耗率"].ToString().Trim();
                            row["Practical"] = r["实用量"].ToString().Trim();
                            row["layerStandard"] = r["标准本层"].ToString().Trim();
                            row["StandardLower"] = r["标准低层"].ToString().Trim();
                            row["GLPrice"] = r["GL单价"].ToString().Trim();
                            row["GLAmount"] = r["GL金额"].ToString().Trim();
                            row["CurrentLayer"] = r["当前本层"].ToString().Trim();
                            row["CurrentLowLevel"] = r["当前低层"].ToString().Trim();
                            row["CUPrice"] = r["CU单价"].ToString().Trim();
                            row["CUAmount"] = r["CU金额"].ToString().Trim();
                            row["Difference"] = r["差异"].ToString().Trim();
                            row["gradation"] = r["层次"].ToString().Trim();
                            row["PurchasePriceDate"] = r["采购价日期"].ToString().Trim();
                            row["Supplier"] = r["供应商"].ToString().Trim();
                            row["domain"] = r["域"].ToString().Trim();
                            row["GrantingPrinciples"] = r["发放原则"].ToString().Trim();
                            row["ComponentType"] = r["组件类型"].ToString().Trim();
                            row["ExchangeRate"] = r["汇率"].ToString().Trim();
                            row["Hours"] = r["工时"].ToString().Trim();
                            row["LaborCosts"] = r["工费(委外)"].ToString().Trim();
                            row["SOPO"] = r["工单"].ToString().Trim();
                            row["ReleaseDate"] = r["下达日期"].ToString().Trim();

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
                                bulkCopy.DestinationTableName = "dbo.ChinaFIFOCostDetTemp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("Product", "Product");
                                bulkCopy.ColumnMappings.Add("Part", "Part");
                                bulkCopy.ColumnMappings.Add("Description", "Description");
                                bulkCopy.ColumnMappings.Add("EA", "EA");
                                bulkCopy.ColumnMappings.Add("Consumption", "Consumption");
                                bulkCopy.ColumnMappings.Add("attrition", "attrition");
                                bulkCopy.ColumnMappings.Add("Practical", "Practical");
                                bulkCopy.ColumnMappings.Add("layerStandard", "layerStandard");
                                bulkCopy.ColumnMappings.Add("StandardLower", "StandardLower");
                                bulkCopy.ColumnMappings.Add("GLPrice", "GLPrice");
                                bulkCopy.ColumnMappings.Add("GLAmount", "GLAmount");
                                bulkCopy.ColumnMappings.Add("CurrentLayer", "CurrentLayer");
                                bulkCopy.ColumnMappings.Add("CurrentLowLevel", "CurrentLowLevel");
                                bulkCopy.ColumnMappings.Add("CUPrice", "CUPrice");
                                bulkCopy.ColumnMappings.Add("CUAmount", "CUAmount");
                                bulkCopy.ColumnMappings.Add("Difference", "Difference");
                                bulkCopy.ColumnMappings.Add("gradation", "gradation");
                                bulkCopy.ColumnMappings.Add("PurchasePriceDate", "PurchasePriceDate");
                                bulkCopy.ColumnMappings.Add("Supplier", "Supplier");
                                bulkCopy.ColumnMappings.Add("domain", "domain");
                                bulkCopy.ColumnMappings.Add("GrantingPrinciples", "GrantingPrinciples");
                                bulkCopy.ColumnMappings.Add("ComponentType", "ComponentType");
                                bulkCopy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
                                bulkCopy.ColumnMappings.Add("Hours", "Hours");
                                bulkCopy.ColumnMappings.Add("LaborCosts", "LaborCosts");
                                bulkCopy.ColumnMappings.Add("SOPO", "SOPO");
                                bulkCopy.ColumnMappings.Add("ReleaseDate", "ReleaseDate");
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

    //public int Importcustpart(Int32 uID, Int32 plantcode)
    //{
    //    string strSQL = "sp_FIFO_checkimport";
    //    SqlParameter[] parm = new SqlParameter[3];
    //    parm[0] = new SqlParameter("@uID", uID);
    //    parm[1] = new SqlParameter("@plantcode", plantcode);
    //    parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
    //    parm[2].Direction = ParameterDirection.Output;
    //    return Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm));

    //}
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