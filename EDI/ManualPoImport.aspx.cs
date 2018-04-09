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


public partial class ManualPoImport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    private bool InsertBatchTemp(string uID, string uName, string plantCode)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@plantCode", plantCode);
            sqlParam[3] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertbatchManualPO", sqlParam);

            return Convert.ToBoolean(sqlParam[3].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckValidity(string uID)
    {
        try
        {
            string strSql = "sp_edi_checkManualPOValidity";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[1].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_clearMaualPoTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean ImportExcelFile()
    {
        DataTable dt ;
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

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
                ltlAlert.Text = "alert('Fail to upload file.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please select a file.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum upload file is 8 MB.');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('Failed to upload file.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dt = this.GetExcelContents(strFileName); //chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('Import file must be in Excel format.');";
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
                    if (dt.Columns.Count != 22)
                    {
                        dt.Reset();
                        ltlAlert.Text = "alert('The file must have 22 columns！');";
                        return false;
                    }

                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim().ToLower() != "cust code")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Cust Code！');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim().ToLower() != "cust po")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Cust PO！');";
                            return false;
                        }

                        if (col == 2 && dt.Columns[col].ColumnName.Trim().ToLower() != "req date")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Req Date！');";
                            return false;
                        }

                        if (col == 3 && dt.Columns[col].ColumnName.Trim().ToLower() != "due date")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Due Date！');";
                            return false;
                        }

                        if (col == 4 && dt.Columns[col].ColumnName.Trim().ToLower() != "ship to")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Ship To！');";
                            return false;
                        }

                        if (col == 5 && dt.Columns[col].ColumnName.Trim().ToLower() != "ship via")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Ship Via！');";
                            return false;
                        }

                        if (col == 6 && dt.Columns[col].ColumnName.Trim().ToLower() != "channel")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Channel！');";
                            return false;
                        }
                        if (col == 7 && dt.Columns[col].ColumnName.Trim().ToLower() != "remark1")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Remark1！');";
                            return false;
                        }

                        if (col == 8 && dt.Columns[col].ColumnName.Trim().ToLower() != "line")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Line！');";
                            return false;
                        }

                        if (col == 9 && dt.Columns[col].ColumnName.Trim().ToLower() != "cust part")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Cust Part！');";
                            return false;
                        }

                        if (col == 10 && dt.Columns[col].ColumnName.Trim().ToLower() != "qad")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD！');";
                            return false;
                        }

                        if (col == 11 && dt.Columns[col].ColumnName.Trim().ToLower() != "qty")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Qty！');";
                            return false;
                        }

                        if (col == 12 && dt.Columns[col].ColumnName.Trim().ToLower() != "um")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be UM！');";
                            return false;
                        }

                        if (col == 13 && dt.Columns[col].ColumnName.Trim().ToLower() != "price")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Price！');";
                            return false;
                        }
                        //同名的，自动加1
                        if (col == 14 && dt.Columns[col].ColumnName.Trim().ToLower() != "req date1")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Req Date！');";
                            return false;
                        }

                        if (col == 15 && dt.Columns[col].ColumnName.Trim().ToLower() != "due date1")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Due Date！');";
                            return false;
                        }

                        if (col == 16 && dt.Columns[col].ColumnName.Trim().ToLower() != "description")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Description！');";
                            return false;
                        }

                        if (col == 17 && dt.Columns[col].ColumnName.Trim().ToLower() != "remark2")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Remark2！');";
                            return false;
                        }
                        if (col == 18 && dt.Columns[col].ColumnName.Trim().ToLower() != "sku")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be SKU！');";
                            return false;
                        }

                        if (col == 19 && dt.Columns[col].ColumnName.Trim() != "IsSample(N/Y)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be IsSample(N/Y)！');";
                            return false;
                        }

                        if (col == 20 && dt.Columns[col].ColumnName.Trim() != "Sale Domain")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Sale Domain！');";
                            return false;
                        }
                        if (col == 21 && dt.Columns[col].ColumnName.Trim() != "Curr")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be Curr！');";
                            return false;
                        }

                    }																
                    #endregion

                    //转换成模板格式
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "custCode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "custPo";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "hrdReqDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "hrdDueDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "shipTo";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "shipVia";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "hrdRmks";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "line";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "custPart";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Qad";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ordQty";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "um";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "price";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "detReqDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "detDueDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "detDesc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "detRmks";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "channel";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "SKU";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "IsSample";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Sale_Domain";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Curr";
                    table.Columns.Add(column);

                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[0].ToString().Trim() != string.Empty)
                            {
                                row = table.NewRow();

                                #region 赋值、长度判定
                                //custCode的长度允许最长15个字符，否则截取
                                if (r[0].ToString().Trim().Length > 15)
                                {
                                    row["custCode"] = r[0].ToString().Trim().Substring(0, 15);
                                }
                                else
                                {
                                    row["custCode"] = r[0].ToString().Trim();
                                }

                                //custPo的长度允许最长20个字符，否则截取
                                if (r[1].ToString().Trim().Length > 20)
                                {
                                    row["custPo"] = r[1].ToString().Trim().Substring(0, 20);
                                }
                                else
                                {
                                    row["custPo"] = r[1].ToString().Trim();
                                }

                                //hrdReqDate的长度允许最长10个字符，否则截取
                                if (r[2].ToString().Trim().Length > 10)
                                {
                                    try
                                    {
                                        row["hrdReqDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[2]));
                                    }
                                    catch
                                    {
                                        row["hrdReqDate"] = r[2].ToString().Trim().Substring(0, 10);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        row["hrdReqDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[2]));
                                    }
                                    catch
                                    {
                                        row["hrdReqDate"] = r[2].ToString().Trim();
                                    }
                                }

                                //hrdDueDate的长度允许最长10个字符，否则截取
                                if (r[3].ToString().Trim().Length > 10)
                                {
                                    try
                                    {
                                        row["hrdDueDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[3]));
                                    }
                                    catch
                                    {
                                        row["hrdDueDate"] = r[3].ToString().Trim().Substring(0, 10);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        row["hrdDueDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[3]));
                                    }
                                    catch
                                    {
                                        row["hrdDueDate"] = r[3].ToString().Trim();
                                    }
                                }

                                //shipTo的长度允许最长20个字符，否则截取
                                if (r[4].ToString().Trim().Length > 20)
                                {
                                    row["shipTo"] = r[4].ToString().Trim().Substring(0, 20);
                                }
                                else
                                {
                                    row["shipTo"] = r[4].ToString().Trim();
                                }

                                //shipTo的长度允许最长20个字符，否则截取
                                if (r[5].ToString().Trim().Length > 20)
                                {
                                    row["shipVia"] = r[5].ToString().Trim().Substring(0, 20);
                                }
                                else
                                {
                                    row["shipVia"] = r[5].ToString().Trim();
                                }

                                //channel的长度允许最长8个字符，否则截取
                                if (r[6].ToString().Trim().Length > 8)
                                {
                                    row["channel"] = r[6].ToString().Trim().Substring(0, 8);
                                }
                                else
                                {
                                    row["channel"] = r[6].ToString().Trim();
                                }
                                //hrdRmks的长度允许最长50个字符，否则截取
                                if (r[7].ToString().Trim().Length > 50)
                                {
                                    row["hrdRmks"] = r[7].ToString().Trim().Substring(0, 50);
                                }
                                else
                                {
                                    row["hrdRmks"] = r[7].ToString().Trim();
                                }

                                //line的长度允许最长4个字符，否则截取
                                if (r[8].ToString().Trim().Length > 4)
                                {
                                    row["line"] = r[8].ToString().Trim().Substring(0, 4);
                                }
                                else
                                {
                                    row["line"] = r[8].ToString().Trim();
                                }

                                //custPart的长度允许最长20个字符，否则截取
                                if (r[9].ToString().Trim().Length > 30)
                                {
                                    row["custPart"] = r[9].ToString().Trim().Substring(0, 30);
                                }
                                else
                                {
                                    row["custPart"] = r[9].ToString().Trim();
                                }

                                //Qad的长度允许最长15个字符，否则截取
                                if (r[10].ToString().Trim().Length > 15)
                                {
                                    row["Qad"] = r[10].ToString().Trim().Substring(0, 15);
                                }
                                else
                                {
                                    row["Qad"] = r[10].ToString().Trim();
                                }

                                //ordQty的长度允许最长15个字符，否则截取
                                if (r[11].ToString().Trim().Length > 15)
                                {
                                    row["ordQty"] = r[11].ToString().Trim().Substring(0, 15);
                                }
                                else
                                {
                                    row["ordQty"] = r[11].ToString().Trim();
                                }

                                //um的长度允许最长5个字符，否则截取
                                if (r[12].ToString().Trim().Length > 5)
                                {
                                    row["um"] = r[12].ToString().Trim().Substring(0, 5);
                                }
                                else
                                {
                                    row["um"] = r[12].ToString().Trim();
                                }

                                //price的长度允许最长15个字符，否则截取
                                if (r[13].ToString().Trim().Length > 15)
                                {
                                    row["price"] = r[13].ToString().Trim().Substring(0, 15);
                                }
                                else
                                {
                                    row["price"] = r[13].ToString().Trim();
                                }

                                //detReqDate的长度允许最长10个字符，否则截取
                                if (r[14].ToString().Trim().Length > 10)
                                {
                                    try
                                    {
                                        row["detReqDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[14]));
                                    }
                                    catch
                                    {
                                        row["detReqDate"] = r[14].ToString().Trim().Substring(0, 10);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        row["detReqDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[14]));
                                    }
                                    catch
                                    {
                                        row["detReqDate"] = r[14].ToString().Trim();
                                    }
                                }

                                //detDueDate的长度允许最长10个字符，否则截取
                                if (r[15].ToString().Trim().Length > 10)
                                {
                                    try
                                    {
                                        row["detDueDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[15]));
                                    }
                                    catch
                                    {
                                        row["detDueDate"] = r[15].ToString().Trim().Substring(0, 10);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        row["detDueDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[15]));
                                    }
                                    catch
                                    {
                                        row["detDueDate"] = r[15].ToString().Trim();
                                    }
                                }

                                if (r[16].ToString().Trim().Length > 100)
                                {
                                    row["detDesc"] = r[16].ToString().Trim().Substring(0, 100);
                                }
                                else
                                {
                                    row["detDesc"] = r[16].ToString().Trim();
                                }

                                //detRmks的长度允许最长50个字符，否则截取
                                if (r[17].ToString().Trim().Length > 50)
                                {
                                    row["detRmks"] = r[17].ToString().Trim().Substring(0, 50);
                                }
                                else
                                {
                                    row["detRmks"] = r[17].ToString().Trim();
                                }

                                if (r[18].ToString().Trim().Length > 50)
                                {
                                    row["SKU"] = r[18].ToString().Trim().Substring(0, 50);
                                }
                                else
                                {
                                    row["SKU"] = r[18].ToString().Trim();
                                }
                                if (r[19].ToString().Trim().ToLower() == "y")
                                {
                                    row["IsSample"] = "True";
                                }
                                else
                                {
                                    row["IsSample"] = string.Empty;
                                }
                                if (r[20].ToString().Trim().Length > 5)
                                {
                                    row["Sale_Domain"] = r[20].ToString().Trim().Substring(0, 5);
                                }
                                else
                                {
                                    row["Sale_Domain"] = r[20].ToString().Trim();
                                }
                                if (r[21].ToString().Trim().Length > 5)
                                {
                                    row["Curr"] = r[21].ToString().Trim().Substring(0, 5);
                                }
                                else
                                {
                                    row["Curr"] = r[21].ToString().Trim();
                                }
                                #endregion

                                row["createdBy"] = _uID;
                                row["errMsg"] = string.Empty;

                                table.Rows.Add(row);
                            }
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopy.DestinationTableName = "dbo.ManualPoTemp";
                                bulkCopy.ColumnMappings.Add("custCode", "hrd_cust");
                                bulkCopy.ColumnMappings.Add("custPo", "hrd_nbr");
                                bulkCopy.ColumnMappings.Add("hrdReqDate", "hrd_req_date");
                                bulkCopy.ColumnMappings.Add("hrdDueDate", "hrd_due_date");
                                bulkCopy.ColumnMappings.Add("shipTo", "hrd_shipto");
                                bulkCopy.ColumnMappings.Add("shipVia", "hrd_shipvia");
                                bulkCopy.ColumnMappings.Add("hrdRmks", "hrd_rmks");
                                bulkCopy.ColumnMappings.Add("line", "det_line");
                                bulkCopy.ColumnMappings.Add("custPart", "det_cust_part");
                                bulkCopy.ColumnMappings.Add("Qad", "det_qad");
                                bulkCopy.ColumnMappings.Add("ordQty", "det_ord_qty");
                                bulkCopy.ColumnMappings.Add("um", "det_um");
                                bulkCopy.ColumnMappings.Add("price", "det_price");
                                bulkCopy.ColumnMappings.Add("detReqDate", "det_req_date");
                                bulkCopy.ColumnMappings.Add("detDueDate", "det_due_date");
                                bulkCopy.ColumnMappings.Add("detDesc", "det_desc");
                                bulkCopy.ColumnMappings.Add("detRmks", "det_rmks");
                                bulkCopy.ColumnMappings.Add("channel", "channel");
                                bulkCopy.ColumnMappings.Add("createdBy", "hrd_createdBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "hrd_errMsg");
                                bulkCopy.ColumnMappings.Add("SKU", "SKU");
                                bulkCopy.ColumnMappings.Add("IsSample", "IsSample");
                                bulkCopy.ColumnMappings.Add("Sale_Domain", "saleDomain");
                                bulkCopy.ColumnMappings.Add("Curr", "curr");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('Operation fails!Please try again!');";

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
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (!CheckValidity(Session["uID"].ToString()))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString(), Session["plantCode"].ToString()))
                {
                    ltlAlert.Text = "alert('success!');";
                }
                else
                {
                    ltlAlert.Text = "alert('fail!');";
                }
            }
            else
            {
                ltlAlert.Text = "window.open('ManualPoImportError.aspx?rt=" + DateTime.Now.ToString() + "', '_blank');";
            }
        }
    }
}
