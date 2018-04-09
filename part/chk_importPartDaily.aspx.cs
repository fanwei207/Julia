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
using System.IO;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class part_chk_importPartDaily : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected bool IsDate(string val)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(val);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected bool GetUser(string userNo)
    {
        bool IsCorrect = false;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userNo", userNo);
            param[1] = new SqlParameter("@orgID", Session["orgID"].ToString());

            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_chk_selectUser", param);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lblUserID.Text = reader["userID"].ToString();
                    lblUserName.Text = reader["userName"].ToString();
                }
                reader.Dispose();
                IsCorrect = true;
            }
            else
            {
                IsCorrect = false;
            }
        }
        catch
        {
            IsCorrect = false;
        }
        return IsCorrect;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"].ToString() == string.Empty)
        {
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else if (txbGenerateDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('生成日期不能为空！')";
        }
        else if (!IsDate(txbGenerateDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('生成日期格式不对！')";
        }
        else if (txbCheckedDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('盘点日期不能为空！')";
        }
        else if (!IsDate(txbCheckedDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('盘点日期格式不对！')";
        }
        else if (txbGenerateDate.Text.Trim() != txbCheckedDate.Text.Trim())
        {
            ltlAlert.Text = "alert('盘点日期与生成日期必须是同一天！')";
        }
        else if (txbFinance.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('工号不能为空！')";
        }
        else if (!GetUser(txbFinance.Text.Trim()))
        {
            ltlAlert.Text = "alert('工号不正确！')";
        }
        else if (ImportExcelFile())
        {
            if (CheckPartDailyError(Session["uID"].ToString()))
            {
                if (ImportParts(Session["uID"].ToString()))
                {
                    ltlAlert.Text = "alert('导入成功!');";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!\\nFail to import partDaily.');";
                }
            }
            else
            {
                ltlAlert.Text = "window.open('/part/chk_exportPartDailyError.aspx?generateDate=" + txbGenerateDate.Text.Trim() + "&rm=" + DateTime.Now.ToString() + "', '_blank');";
            }
        }
    }

    protected bool ImportExcelFile()
    {
        #region       判断是否选择文件，以及选择的是否是excel文件
        if (excelFile.Value.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择要导入的Excel文件！')";
            return false;
        }
        string excelFileSuffix = excelFile.Value.Trim().Substring(excelFile.Value.Trim().LastIndexOf('.') + 1);
        if (excelFileSuffix != "xls" && excelFileSuffix != "xlsx")
        {
            ltlAlert.Text = "alert('请选择Excel文件！')";
            return false;
        }
        #endregion

        #region       检查Excel文件大小，创建Excel文件服务器目录，上传Excel文件至服务器
        string strServerPath = Server.MapPath("/import");       /*服务器存放Excel文件的路径*/
        string strPostedFileName = excelFile.PostedFile.FileName.Trim();      /*Excel文件的名字*/
        string strFileName = strPostedFileName.Substring(strPostedFileName.LastIndexOf('\\') + 1).Trim();
        if (excelFile.PostedFile.ContentLength > 8388608)
        {
            ltlAlert.Text = "alert('文件大小不能超过8 MB！')";
            return false;
        }
        if (!Directory.Exists(strServerPath))
        {
            try
            {
                Directory.CreateDirectory(strServerPath);
            }
            catch
            {
                ltlAlert.Text = "alert('Excel文件上传失败！请联系管理员！\\nError：Fail to create server directory.')";
                return false;
            }
        }

        //上传Excel文件至服务器
        string strFullName = strServerPath + "\\" + strFileName;
        try
        {
            excelFile.PostedFile.SaveAs(strFullName);
        }
        catch
        {
            ltlAlert.Text = "alert('Excel文件上传失败！请联系管理员！\\nError：Fail to upload the excel file.')";
            return false;
        }
        #endregion

        if (File.Exists(strFullName))
        {
            DataTable dtblExcel = new DataTable();
            #region   加载Excel数据
            try
            {
                dtblExcel = this.GetExcelContents(strFullName);
            }
            catch
            {
                ltlAlert.Text = "alert('加载Excel数据失败！请稍后重试！\\nError：Fail to load the excel file.')";
                return false;
            }
            finally
            {
                if (File.Exists(strFullName))
                {
                    try
                    {
                        File.Delete(strFullName);
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('加载Excel数据失败！请稍后重试！\\nError：Fail to delete the excel file.')";
                    }
                }
            }
            #endregion

            if (dtblExcel.Rows.Count > 0)
            {
                string strCreatedDate = txbGenerateDate.Text.Trim();
                string strCheckedDate = txbCheckedDate.Text.Trim();
                string strCheckedBy = lblUserID.Text.Trim();
                string strCheckedName = lblUserName.Text.Trim();
                string strSite = string.Empty;
                string strLoc = string.Empty;
                string strQAD = string.Empty;
                string strLot = string.Empty;
                string strSysQty = string.Empty;
                string strRelQty = string.Empty;
                string strDiffs = string.Empty;

                if (dtblExcel.Columns[0].ColumnName != "地点" || dtblExcel.Columns[1].ColumnName != "库位" || dtblExcel.Columns[2].ColumnName != "QAD号" || dtblExcel.Columns[3].ColumnName != "批次" || dtblExcel.Columns[4].ColumnName != "系统库存" || dtblExcel.Columns[5].ColumnName != "盘点库存" || dtblExcel.Columns[6].ColumnName != "差异原因")
                {
                    ltlAlert.Text = "alert('模板不正确！请确认前7列的名称是：\\n地点、库位、QAD号、批次\\n系统库存、盘点库存、差异原因')";
                    return false;
                }

                #region 创建临时数据源表
                DataTable table = new DataTable("temp");
                DataColumn column;
                DataRow row;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "generateDate";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "checkedDate";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "checkedBy";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "checkedName";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "site";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "loc";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "qad";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "lot";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "sysQty";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "relQty";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "diff";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "createdBy";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "createdDate";
                table.Columns.Add(column);
                #endregion

                if (ClearPartDailyTemp(Session["uID"].ToString()))
                {
                    for (int i = 0; i < dtblExcel.Rows.Count; i++)
                    {
                        strSite = dtblExcel.Rows[i][0].ToString();
                        strLoc = dtblExcel.Rows[i][1].ToString();
                        strQAD = dtblExcel.Rows[i][2].ToString();
                        strLot = dtblExcel.Rows[i][3].ToString();
                        strSysQty = dtblExcel.Rows[i][4].ToString();
                        strRelQty = dtblExcel.Rows[i][5].ToString();
                        strDiffs = dtblExcel.Rows[i][6].ToString();

                        row = table.NewRow();
                        row["generateDate"] = strCreatedDate;
                        row["checkedDate"] = strCheckedDate;
                        row["checkedBy"] = strCheckedBy;
                        row["checkedName"] = strCheckedName;
                        row["site"] = strSite;
                        row["loc"] = strLoc;
                        row["qad"] = strQAD;
                        row["lot"] = strLot;
                        row["sysQty"] = strSysQty;
                        row["relQty"] = strRelQty;
                        row["diff"] = strDiffs;
                        row["createdBy"] = Session["uID"].ToString();
                        row["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        table.Rows.Add(row);
                    }
                    if (table != null && table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsnx()))
                        {
                            bulkCopy.DestinationTableName = "dbo.chk_pt_mstrTemp";
                            bulkCopy.ColumnMappings.Add("generateDate", "pt_createdDate");
                            bulkCopy.ColumnMappings.Add("checkedDate", "pt_checkedDate");
                            bulkCopy.ColumnMappings.Add("checkedBy", "pt_checkedBy");
                            bulkCopy.ColumnMappings.Add("checkedName", "pt_checkedName");
                            bulkCopy.ColumnMappings.Add("site", "pt_site");
                            bulkCopy.ColumnMappings.Add("loc", "pt_loc");
                            bulkCopy.ColumnMappings.Add("qad", "pt_part");
                            bulkCopy.ColumnMappings.Add("lot", "pt_lot");
                            bulkCopy.ColumnMappings.Add("sysQty", "pt_sys_qty");
                            bulkCopy.ColumnMappings.Add("relQty", "pt_rel_qty");
                            bulkCopy.ColumnMappings.Add("diff", "pt_diff");
                            bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                            bulkCopy.ColumnMappings.Add("createdDate", "createdDate");

                            try
                            {
                                bulkCopy.WriteToServer(table);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入时出错，请联系管理员！\\nError：Fail to write to the server.\\n" + ex.Message + "')";
                                return false;
                            }
                            finally
                            {
                                table.Dispose();
                            }
                        }
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('临时表数据清空失败！')";
                    return false;
                }
            }
            dtblExcel.Dispose();
        }
        else
        {
            ltlAlert.Text = "alert('要加载的Excel文件不存在！请联系管理员！\\nError：Fail to find the uploaded excel file.')";
            return false;
        }
        return true;
    }

    protected bool ClearPartDailyTemp(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_clearPartDailyTemp", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckPartDailyError(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_checkPartDailyTempError", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool ImportParts(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_importPartDaily", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
}
