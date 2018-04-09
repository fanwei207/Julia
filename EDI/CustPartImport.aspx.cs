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


public partial class CustPartImport : BasePage
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

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertBatchCustPart", sqlParam);

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
        string strSql2 = " Select top 1 * From ImportError where userID ='" + Convert.ToInt32(Session["uID"]) + "' and  plantID= '" + Convert.ToInt32(Session["PlantCode"]) + "'";

        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "120^<b>客户/货物发往</b>~^120^<b>客户物料</b>~^120^<b>物料号</b>~^80^<b>生效日期</b>~^80^<b>截止日期</b>~^100^<b>说明</b>~^100^<b>显示客户物料</b>~^50^<b>SKU</b>~^80^<b>历史开始时间</b>~^80^<b>历史截止时间</b>~^500^<b>错误信息</b>~^";
                
                string sql = " select cpt_cust,cpt_cust_part,cpt_part,cpt_start_date,cpt_end_date,cpt_comment,cpt_cust_partd,cpt_sku,cpt_his_start_date,cpt_his_end_date,cpt_errMsg from cp_temp Where cpt_createdBy='" + Convert.ToInt32(Session["uID"]) + "' ";

                DataTable dt = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, sql).Tables[0];
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

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_clearCustPartTemp", param);

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

                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "客户/货物发往")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 客户/货物发往！');";
                            return false;
                        }
                        else if (col == 1 && dt.Columns[col].ColumnName.Trim() != "客户物料")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 客户物料！');";
                            return false;
                        }
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "物料号")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 物料号！');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "生效日期")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 生效日期！');";
                            return false;
                        }
                        else if (col == 4 && dt.Columns[col].ColumnName.Trim() != "截止日期")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 截止日期！');";
                            return false;
                        }
                        else if (col == 5 && dt.Columns[col].ColumnName.Trim() != "说明")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 说明！');";
                            return false;
                        }
                        else if (col == 6 && dt.Columns[col].ColumnName.Trim() != "显示客户物料")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 显示客户物料！');";
                            return false;
                        }
                        else if (col == 7 && dt.Columns[col].ColumnName.ToLower().Trim() != "sku")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 SKU！');";
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
                    DataTable table = new DataTable("temp");
                   
                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "domain";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "custCode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "custPart";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qad";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "stdDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "endDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "comment";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "partd";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sku";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
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
                            if (r[0].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "域不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                           
                                row["domain"] = "SZX";
                           

                            //custCode的长度允许最长15个字符，否则截取
                                if (r["客户/货物发往"].ToString().Trim().Length == 0)
                            {

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "客户/货物发往不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                                else if (r["客户/货物发往"].ToString().Trim().Length > 8)
                            {
                                row["custCode"] = r["客户/货物发往"].ToString().Trim().Substring(0, 8);
                            }
                            else
                            {
                                row["custCode"] = r["客户/货物发往"].ToString().Trim();
                            }

                            //custPart的长度允许最长20个字符，否则截取
                                if (r["客户物料"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "客户物料不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                                else if (r["客户物料"].ToString().Trim().Length > 50)
                            {
                                row["custPart"] = r["客户物料"].ToString().Trim().Substring(0, 50);
                            }
                            else
                            {
                                row["custPart"] = r["客户物料"].ToString().Trim();
                            }

                            //qad的长度允许最长18个字符，否则截取
                                if (r["物料号"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "物料号不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                                else if (r["物料号"].ToString().Trim().Length > 18)
                            {
                                row["qad"] = r["物料号"].ToString().Trim().Substring(0, 18);
                            }
                            else
                            {
                                row["qad"] = r["物料号"].ToString().Trim();
                            }

                            //stdDate的长度允许最长10个字符，否则截取
                                if (r["生效日期"].ToString().Trim().Length > 10)
                            {
                                try
                                {
                                    row["stdDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["生效日期"]));
                                }
                                catch
                                {
                                    row["stdDate"] = r["生效日期"].ToString().Trim().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["stdDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["生效日期"]));
                                }
                                catch
                                {
                                    row["stdDate"] = r["生效日期"].ToString().Trim();
                                }
                            }

                            //endDate的长度允许最长10个字符，否则截取
                                if (r["截止日期"].ToString().Trim().Length > 10)
                            {
                                try
                                {
                                    row["endDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["截止日期"]));
                                }
                                catch
                                {
                                    row["endDate"] = r["截止日期"].ToString().Trim().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["endDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["截止日期"]));
                                }
                                catch
                                {
                                    row["endDate"] = r["截止日期"].ToString().Trim();
                                }
                            }

                            //comment的长度允许最长40个字符，否则截取
                                if (r["说明"].ToString().Trim().Length > 20)
                            {
                                row["comment"] = r["说明"].ToString().Trim().Substring(0, 40);
                            }
                            else
                            {
                                row["comment"] = r["说明"].ToString().Trim();
                            }

                            //partd的长度允许最长40个字符，否则截取
                                if (r["显示客户物料"].ToString().Trim().Length > 20)
                            {
                                row["partd"] = r["显示客户物料"].ToString().Trim().Substring(0, 40);
                            }
                            else
                            {
                                row["partd"] = r["显示客户物料"].ToString().Trim();
                            }
                            //vnum的长度允许最长50个字符，否则截取
                            if (r[7].ToString().Trim().Length > 50)
                            {
                                row["sku"] = r["SKU"].ToString().Trim().Substring(0, 50);
                            }
                            else
                            {
                                row["sku"] = r["SKU"].ToString().Trim();
                            }
                            #endregion

                            row["createdBy"] = _uID;
                            row["errMsg"] = string.Empty;

                            table.Rows.Add(row);
                        }

                        if (tblError != null && tblError.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopyError.DestinationTableName = "dbo.ImportError";
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
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopy.DestinationTableName = "dbo.cp_temp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("custCode", "cpt_cust");
                                bulkCopy.ColumnMappings.Add("custPart", "cpt_cust_part");
                                bulkCopy.ColumnMappings.Add("qad", "cpt_part");
                                bulkCopy.ColumnMappings.Add("stdDate", "cpt_start_date");
                                bulkCopy.ColumnMappings.Add("endDate", "cpt_end_date");
                                bulkCopy.ColumnMappings.Add("comment", "cpt_comment");
                                bulkCopy.ColumnMappings.Add("partd", "cpt_cust_partd");
                                bulkCopy.ColumnMappings.Add("sku", "cpt_sku");
                                bulkCopy.ColumnMappings.Add("createdBy", "cpt_createdBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "cpt_errMsg");
                                
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
        string strSQL = "sp_edi_CustPartimport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@plantcode", plantcode);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        return Convert.ToInt32(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSQL, parm));

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
