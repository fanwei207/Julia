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

public partial class EDI_StockingImport : BasePage
{
    adamClass chk = new adamClass();
    private string strFileName1;

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

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_SK_insertStocking", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool checkTemp()
    {
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_SK_CheckStockingtemp");

        string strSql2 = "select * from Stocking_Temp where isnull( errMsg ,'') <>''";
        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "100^<b>备货单号</b>~^100^<b>客户</b>~^100^<b>客户物料</b>~^100^<b>QAD</b>~^100^<b>价格</b>~^100^<b>数量</b>~^100^<b>单位</b>~^100^<b>备注</b>~^100^<b>错误信息</b>~^";

                string sql = " select sk_nbr,[sk_vent],[sk_part],[sk_QAD],[sk_price],[sk_num],[sk_EA],[sk_remark],errMsg from Stocking_Temp  ";

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

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_SK_clearStockingtemp", param);

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

        #region 上传文档例行处理
        strCatFolder = Server.MapPath("/TecDocs/BH");

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

        strUserFileName = filePo.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        int intExt = strUserFileName.LastIndexOf(".");
        strFileName1 = strUserFileName.Substring(intLastBackslash + 1, intExt - intLastBackslash);
        string ext = strUserFileName.Substring(intExt + 1);
        if (strFileName1.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入凭证文件.');";
            return false;
        }

        strUserFileName = strFileName1+"."+ext;
        string phyFileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + ext;
        strFileName1 = strCatFolder + "\\" + phyFileName;
        #endregion

        if (filename1.PostedFile != null && filePo.PostedFile != null) 
        {
            if (filename1.PostedFile.ContentLength > 8388608 || filePo.PostedFile.ContentLength > 8388608) 
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
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "备货单号")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 备货单号！');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "客户")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 客户！');";
                            return false;
                        }
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "客户物料")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 客户物料！');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "QAD")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 QAD！');";
                            return false;
                        }
                        else if (col == 4 && dt.Columns[col].ColumnName.Trim() != "价格")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 价格！');";
                            return false;
                        }
                        else if (col == 5 && dt.Columns[col].ColumnName.Trim() != "数量")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 数量！');";
                            return false;
                        }
                        else if (col == 6 && dt.Columns[col].ColumnName.Trim() != "单位")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 单位！');";
                            return false;
                        }
                        else if (col == 7 && dt.Columns[col].ColumnName.Trim() != "备注")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 备注！');";
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

                    //DataRow rowError;//错误表的行

                    //转换成模板格式
                    DataTable table = new DataTable("Stocking_Temp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_nbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_vent";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_part";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_QAD";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_price";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_num";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_EA";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sk_remark";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CreateBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CreateName";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "fileName";
                    table.Columns.Add(column);
                    
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "filePath";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);
                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);
                    string strerror = "";

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定
                            if (r["备货单号"].ToString().Trim().Length == 0)
                            {
                                strerror += "备货单号不能为空.";
                            }


                            if (r["QAD"].ToString().Trim().Length == 0)
                            {
                                strerror += "QAD不能为空.";
                            }
                            else
                            {
                                string strSql2 = " Select top 1 * From tcpc0..Items where item_qad ='" + r["QAD"].ToString().Trim() + "'";
                                DataSet ds2;
                                ds2 = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql2);
                                if (!(ds2 != null && ds2.Tables[0].Rows.Count > 0))
                                {
                                    strerror += "QAD不存在.";
                                }

                                if (r["QAD"].ToString().Trim().Substring(0, 1) == "1")
                                {
                                    string strSql3 = " SELECT * FROM QAD_Data..xxwkf_mstr WHERE xxwkf_log01 = 1 and xxwkf_chr01 ='" + r["QAD"].ToString().Trim() + "'";
                                    ds2 = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql3);
                                    if (!(ds2 != null && ds2.Tables[0].Rows.Count > 0))
                                    {
                                        strerror += "QAD未锁定.";
                                    }

                                }

                            }
                            if (r["数量"].ToString().Trim().Length == 0)
                            {
                                strerror += "数量不能为空.";
                                r["数量"] = "0";
                            }

                            if (r["价格"].ToString().Trim().Length != 0)
                            {
                                try
                                {
                                    r["价格"] = Convert.ToDouble(r["价格"].ToString().Trim()).ToString("0.0000");
                                }
                                catch (Exception)
                                {
                                    ltlAlert.Text = "alert('价格格式不正确,见表" + line + "行');";
                                    return false;
                                }
                            }

                            row["sk_nbr"] = r["备货单号"].ToString().Trim();
                            row["sk_vent"] = r["客户"].ToString().Trim();
                            row["sk_part"] = r["客户物料"].ToString().Trim();
                            row["sk_QAD"] = r["QAD"].ToString().Trim();
                            row["sk_price"] = r["价格"].ToString().Trim();
                            row["sk_num"] = r["数量"].ToString().Trim();
                            row["sk_EA"] = r["单位"].ToString().Trim();
                            row["sk_remark"] = r["备注"].ToString().Trim();
                            row["CreateBy"] = Session["uID"].ToString();
                            row["CreateName"] = Session["uName"].ToString();
                            row["fileName"] = strUserFileName;
                            row["filePath"] = "/TecDocs/BH/" + phyFileName;

                            #endregion

                            if (strerror != "")
                            {
                                row["errMsg"] = strerror;
                            }
                            else
                            {
                                row["errMsg"] = string.Empty;
                            }
                            table.Rows.Add(row);
                            strerror = "";
                        }


                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.Stocking_Temp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("sk_nbr", "sk_nbr");
                                bulkCopy.ColumnMappings.Add("sk_vent", "sk_vent");
                                bulkCopy.ColumnMappings.Add("sk_part", "sk_part");
                                bulkCopy.ColumnMappings.Add("sk_QAD", "sk_QAD");
                                bulkCopy.ColumnMappings.Add("sk_price", "sk_price");
                                bulkCopy.ColumnMappings.Add("sk_num", "sk_num");
                                bulkCopy.ColumnMappings.Add("sk_EA", "sk_EA");
                                bulkCopy.ColumnMappings.Add("sk_remark", "sk_remark");
                                bulkCopy.ColumnMappings.Add("CreateBy", "CreateBy");
                                bulkCopy.ColumnMappings.Add("CreateName", "CreateName");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");
                                bulkCopy.ColumnMappings.Add("fileName", "fileName");
                                bulkCopy.ColumnMappings.Add("filePath", "filePath");
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
            if (checkTemp())
            {
                filePo.PostedFile.SaveAs(strFileName1);
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('导入成功!');";
                }
                else
                {
                    if (File.Exists(strFileName1))
                    {
                        File.Delete(strFileName1);
                    }
                    ltlAlert.Text = "alert('导入失败!');";
                }
            }

        }
    }
}