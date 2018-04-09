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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using adamFuncs;

public partial class plan_bigOrderBhToSo : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "100^<b>销售单</b>~^100^<b>行号</b>~^100^<b>原备货号</b>~^100^<b>原备货行</b>~^100^<b>加工单</b>~^100^<b>ID</b>~^500^<b>错误信息</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select bo_so, bo_line, bo_so_parent, bo_line_parent, bo_wo_nbr, bo_wo_lot, bo_error From tcpc0.dbo.bigOrder_BH Where bo_createdBy='" + Convert.ToInt32(Session["uID"]) + "'" + " And bo_error <> ''";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx?ymd=a','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            ddlFileType.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            ddlFileType.Items.Add(item1);
        }
    }

    protected void uploadPartBtn_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        ImportExcelFile();
    }

    public void ImportExcelFile()
    {
        String strSQL = "";
        DataTable dst = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

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
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dst = this.GetExcelContents(strFileName);
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                try
                {
                    if (dst.Columns[0].ColumnName != "销售单" &&
                        dst.Columns[1].ColumnName != "行号" &&
                        dst.Columns[2].ColumnName != "原备货号" &&
                        dst.Columns[3].ColumnName != "原备货行" &&
                        dst.Columns[4].ColumnName != "加工单" &&
                        dst.Columns[5].ColumnName != "ID"
                        )
                    {
                        dst.Reset();
                        ltlAlert.Text = "alert('导入文件的模版不正确!');";
                        return;
                    }

                    #region//新建TempTable内存表
                    DataTable TempTable = new DataTable("TempTable");
                    DataColumn TempColumn;
                    DataRow TempRow;

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_so";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_line";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_so_parent";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_line_parent";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_wo_nbr";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_wo_lot";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_error";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.Int32");
                    TempColumn.ColumnName = "bo_createdBy";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "bo_createdName";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "bo_createdDate";

                    TempTable.Columns.Add(TempColumn);
                    #endregion

                    if (dst.Rows.Count > 0)
                    {
                        string bo_so = string.Empty;
                        string bo_line = string.Empty;
                        string bo_so_parent = string.Empty;
                        string bo_line_parent = string.Empty;
                        string bo_wo_nbr = string.Empty;
                        string bo_wo_lot = string.Empty;
                        string bo_error = string.Empty;
                        string bo_createdBy = strUID;
                        string bo_createdName = struName;
                        string bo_createdDate = DateTime.Now.ToString();

                        DateTime dateFormat = DateTime.Now;
                        int intFormat = 0;

                        //先清空临时表中该上传员工的记录
                        if (ClearTempTable(Convert.ToInt32(strUID)))
                        {
                            i = 0;
                            for (i = 0; i <= dst.Rows.Count - 1; i++)
                            {
                                if (dst.Rows[i].IsNull(0)) bo_so = "";
                                else bo_so = dst.Rows[i].ItemArray[0].ToString().Trim();

                                if (dst.Rows[i].IsNull(1)) bo_line = "";
                                else bo_line = dst.Rows[i].ItemArray[1].ToString().Trim();

                                if (dst.Rows[i].IsNull(2)) bo_so_parent = "";
                                else bo_so_parent = dst.Rows[i].ItemArray[2].ToString().Trim();

                                if (dst.Rows[i].IsNull(3)) bo_line_parent = "";
                                else bo_line_parent = dst.Rows[i].ItemArray[3].ToString().Trim();

                                if (dst.Rows[i].IsNull(4)) bo_wo_nbr = "";
                                else bo_wo_nbr = dst.Rows[i].ItemArray[4].ToString().Trim();

                                if (dst.Rows[i].IsNull(5)) bo_wo_lot = "";
                                else bo_wo_lot = dst.Rows[i].ItemArray[5].ToString().Trim();

                                bo_error = "";

                                if (bo_so.Length == 0)
                                {
                                    bo_error += "销售单不能为空;";
                                }

                                if (bo_line.Length == 0)
                                {
                                    bo_error += "行号不能为空;";
                                }
                                else
                                {
                                    try
                                    {
                                        intFormat = Convert.ToInt32(bo_line);
                                    }
                                    catch
                                    {
                                        bo_error += "行号必须为整数;";
                                    }
                                }

                                if (bo_so_parent.Length == 0)
                                {
                                    bo_error += "原备货号不能为空;";
                                }

                                if (bo_line_parent.Length == 0)
                                {
                                    bo_error += "原备货行不能为空;";
                                }
                                else
                                {
                                    try
                                    {
                                        intFormat = Convert.ToInt32(bo_line_parent);
                                    }
                                    catch
                                    {
                                        bo_error += "原备货行必须为整数;";
                                    }
                                }

                                if(bo_wo_nbr.Length == 0)
                                {
                                    bo_error += "加工单不能为空;";
                                }

                                if(bo_wo_lot.Length == 0)
                                {
                                    bo_error += "ID不能为空;";
                                }

                                TempRow = TempTable.NewRow();
                                TempRow["bo_so"] = bo_so;
                                TempRow["bo_line"] = bo_line;
                                TempRow["bo_so_parent"] = bo_so_parent;
                                TempRow["bo_line_parent"] = bo_line_parent;
                                TempRow["bo_wo_nbr"] = bo_wo_nbr;
                                TempRow["bo_wo_lot"] = bo_wo_lot;
                                TempRow["bo_error"] = bo_error;
                                TempRow["bo_createdBy"] = bo_createdBy;
                                TempRow["bo_createdName"] = bo_createdName;
                                TempRow["bo_createdDate"] = bo_createdDate;

                                TempTable.Rows.Add(TempRow);
                            }

                            //TempTable有数据的情况下批量复制到数据库里
                            if (TempTable != null && TempTable.Rows.Count > 0)
                            {
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                {
                                    bulkCopy.DestinationTableName = "bigOrder_BH";

                                    bulkCopy.ColumnMappings.Clear();

                                    bulkCopy.ColumnMappings.Add("bo_so", "bo_so");
                                    bulkCopy.ColumnMappings.Add("bo_line", "bo_line");
                                    bulkCopy.ColumnMappings.Add("bo_so_parent", "bo_so_parent");
                                    bulkCopy.ColumnMappings.Add("bo_line_parent", "bo_line_parent");
                                    bulkCopy.ColumnMappings.Add("bo_wo_nbr", "bo_wo_nbr");
                                    bulkCopy.ColumnMappings.Add("bo_wo_lot", "bo_wo_lot");
                                    bulkCopy.ColumnMappings.Add("bo_error", "bo_error");
                                    bulkCopy.ColumnMappings.Add("bo_createdBy", "bo_createdBy");
                                    bulkCopy.ColumnMappings.Add("bo_createdName", "bo_createdName");
                                    bulkCopy.ColumnMappings.Add("bo_createdDate", "bo_createdDate");

                                    try
                                    {
                                        bulkCopy.WriteToServer(TempTable);
                                    }
                                    catch (Exception ex)
                                    {
                                        ltlAlert.Text = "alert('导入时出错，请检查文件格式！');";
                                        return;
                                    }
                                    finally
                                    {
                                        TempTable.Dispose();
                                        bulkCopy.Close();
                                    }
                                }
                            }

                            dst.Reset();

                            //数据库端验证
                            if (CheckTempTable(Convert.ToInt32(strUID)))
                            {
                                //判断上传内容能否通过验证
                                if (JudgeTempTable(Convert.ToInt32(strUID)))
                                {
                                    if (TransTempTable(Convert.ToInt32(strUID)))
                                    {
                                        ltlAlert.Text = "alert('导入文件成功!'); window.location.href='/plan/bigOrder.aspx?rm=" + DateTime.Now.ToString() + "';";
                                    }
                                    else
                                    {
                                        ltlAlert.Text = "alert('导入时出错，请联系管理员A!');";
                                        return;
                                    }
                                }
                                else
                                {
                                    ltlAlert.Text = "alert('导入文件结束，有错误!'); window.location.href='/plan/bigOrderBhToSo.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                                    return;
                                }
                            }
                            else
                            {
                                ltlAlert.Text = "alert('导入时出错，请联系管理员B!');";
                                return;
                            }
                        }
                        else
                        {
                            ltlAlert.Text = "alert('清空临时表数据失败!');";
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件失败!');";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
    }

    public bool ClearTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteBhToSo", param));
        }
        catch
        {
            return false;
        }
    }

    public bool CheckTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkBhToSo", param));
        }
        catch
        {
            return false;
        }
    }

    public bool JudgeTempTable(int createdBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", createdBy);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_judgeBhToSo", param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    public bool TransTempTable(int createdBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", createdBy);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_changeBhToSo", param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
}
