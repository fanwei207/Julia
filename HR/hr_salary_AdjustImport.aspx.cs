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
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class HR_hr_salary_AdjustImport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"].ToString() == string.Empty)
        {
            ltlAlert.Text = "alert('请重新登录！')";
            return;
        }
        if (ImportExcelFile())
        {
            if (CheckSalaryAdjustTempError(Convert.ToInt32(Session["uID"])))
            {
                if (ImportSalaryAdjust(Convert.ToInt32(Session["uID"])))
                {
                    ltlAlert.Text = "alert('导入成功！')";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败！请联系管理员！\\nFail to import SalaryAdjust.')";
                }
            }
            else
            {
                ltlAlert.Text = "window.open('hr_salary_AdjustImportError.aspx?rt=" + DateTime.Now.ToString() + "', '_blank');";
            }
        }
    }

    protected bool ImportSalaryAdjust(int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@operateID", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_importSalaryAdjust", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckSalaryAdjustTempError(int uID)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@createdBy", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_checkSalaryAdjustTempError", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool ClearSalaryAdjustTemp(int uID)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            param[0] = new SqlParameter("@createdBy", uID);
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_clearSalaryAdjustTemp2", param);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected bool ImportExcelFile()
    {
        DataTable dtExcel = new DataTable();

        #region 加载Excel文件
        string excelFileSuffix = excelFile.Value.Trim().Substring(excelFile.Value.Trim().IndexOf('.') + 1);
        if (excelFileSuffix != "xls" && excelFileSuffix != "xlsx")
        {
            ltlAlert.Text = "alert('请选择Excel文件！')";
            return false;
        }
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
        if (File.Exists(strFullName))
        {
            try
            {
                dtExcel = this.GetExcelContents(strFullName);
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
        }
        else
        {
            ltlAlert.Text = "alert('要加载的Excel文件不存在！请联系管理员！\\nError：Fail to find the uploaded excel file.')";
            return false;
        }
        #endregion

        if (dtExcel.Rows.Count > 0)
        {
            /*
             *  导入Excel文件必须满足：
             *      列名必须依次为“年份、月份、公司名称、部门、工号、工段、班组、工种、调整比、调整金额、调整原因”
             */
            if (dtExcel.Columns[0].ColumnName != "年份" || dtExcel.Columns[1].ColumnName != "月份"  || dtExcel.Columns[2].ColumnName != "部门" 
                    || dtExcel.Columns[3].ColumnName != "工号" || dtExcel.Columns[4].ColumnName != "工段" || dtExcel.Columns[5].ColumnName != "班组" 
                    || dtExcel.Columns[6].ColumnName != "工种" || dtExcel.Columns[7].ColumnName != "调整比"   || dtExcel.Columns[8].ColumnName != "调整金额" 
                    || dtExcel.Columns[9].ColumnName != "调整原因")
            {
                dtExcel.Dispose();
                ltlAlert.Text = "alert('Excel模版不正确！\\nExcel文件前11列列名应依次如下：\\n年份、月份、部门、工号、工段、班组、工种、调整比、调整金额、调整原因')";
                return false;
            }

            //转换成模版格式
            DataTable table = new DataTable("temp");
            DataColumn column;
            DataRow row;

            #region 定义表列
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "year";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "month";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "plantCode";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "departmentName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "userNO";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "workShopName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "workGroupName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "workTypeName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "percent";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "money";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "reason";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "createdBy";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "createdDate";
            table.Columns.Add(column);
            #endregion

            if (ClearSalaryAdjustTemp(Convert.ToInt32(Session["uID"])))
            {
                foreach (DataRow r in dtExcel.Rows)
                {
                    row = table.NewRow();

                    #region 赋值、长度判定
                    if (r[0].ToString() == string.Empty)
                    {
                        row["year"] = DateTime.Now.Year;
                    }
                    else
                    {
                        row["year"] = IsNumeric(r[0]) ? Convert.ToInt32(r[0]) : 0;
                    }

                    if (r[1].ToString() == string.Empty)
                    {
                        row["month"] = DateTime.Now.Month;
                    }
                    else
                    {
                        row["month"] = IsNumeric(r[1]) ? Convert.ToInt32(r[1]) : 0;
                    }

                    row["plantCode"] = Convert.ToInt32(Session["plantCode"]);

                    if (r[2].ToString().Length > 20)
                    {
                        row["departmentName"] = r[2].ToString().Trim().Substring(0, 20);
                    }
                    else
                    {
                        row["departmentName"] = r[2].ToString().Trim();
                    }

                    if (r[3].ToString().Length > 50)
                    {
                        row["userNO"] = r[3].ToString().Trim().Substring(0, 50);
                    }
                    else
                    {
                        row["userNO"] = r[3].ToString().Trim();
                    }

                    if (r[4].ToString().Length > 20)
                    {
                        row["workShopName"] = r[4].ToString().Trim().Substring(0, 20);
                    }
                    else
                    {
                        row["workShopName"] = r[4].ToString().Trim();
                    }

                    if (r[5].ToString().Length > 20)
                    {
                        row["workGroupName"] = r[5].ToString().Trim().Substring(0, 20);
                    }
                    else
                    {
                        row["workGroupName"] = r[5].ToString().Trim();
                    }

                    if (r[6].ToString().Length > 20)
                    {
                        row["workTypeName"] = r[6].ToString().Trim().Substring(0, 20);
                    }
                    else
                    {
                        row["workTypeName"] = r[6].ToString().Trim();
                    }

                    if (r[7].ToString().Length > 9)
                    {
                        row["percent"] = r[7].ToString().Trim().Substring(0,9);
                    }
                    else
                    {
                        row["percent"] = r[7].ToString();
                    }

                    if (r[8].ToString().Length > 9)
                    {
                        row["money"] = r[8].ToString().Trim().Substring(0,9);
                    }
                    else
                    {
                        row["money"] = r[8].ToString();
                    }

                    if (r[9].ToString().Length > 255)
                    {
                        row["reason"] = r[9].ToString().Trim().Substring(0, 255);
                    }
                    else
                    {
                        row["reason"] = r[9].ToString().Trim();
                    }

                    row["createdBy"] = Convert.ToInt32(Session["uID"]);
                    row["createdDate"] = DateTime.Now.ToString();

                    #endregion

                    table.Rows.Add(row);
                }

                if (table != null && table.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsnx()))
                    {
                        bulkCopy.DestinationTableName = "dbo.SalaryAdjustTemp";
                        bulkCopy.ColumnMappings.Add("year", "adjust_year");
                        bulkCopy.ColumnMappings.Add("month", "adjust_month");
                        bulkCopy.ColumnMappings.Add("plantCode", "adjust_plantCode");
                        bulkCopy.ColumnMappings.Add("departmentName", "adjust_departmentName");
                        bulkCopy.ColumnMappings.Add("userNO", "adjust_userNO");
                        bulkCopy.ColumnMappings.Add("workShopName", "adjust_workShopName");
                        bulkCopy.ColumnMappings.Add("workGroupName", "adjust_workGroupName");
                        bulkCopy.ColumnMappings.Add("workTypeName", "adjust_workTypeName");
                        bulkCopy.ColumnMappings.Add("percent", "adjust_percent");
                        bulkCopy.ColumnMappings.Add("money", "adjust_money");
                        bulkCopy.ColumnMappings.Add("reason", "adjust_reason");
                        bulkCopy.ColumnMappings.Add("createdBy", "adjust_createdBy");
                        bulkCopy.ColumnMappings.Add("createdDate", "adjust_createdDate");

                        try
                        {
                            bulkCopy.WriteToServer(table);
                        }
                        catch
                        {
                            ltlAlert.Text = "alert('导入时出错，请联系管理员！\\nError：Fail to write to the server.')";
                            return false;
                        }
                        finally
                        {
                            dtExcel.Dispose();
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
        return true;
    }

    protected bool IsNumeric(object val)
    {
        try
        {
            double d = Convert.ToDouble(val);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
