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
            ltlAlert.Text = "alert('�����µ�¼��')";
            return;
        }
        if (ImportExcelFile())
        {
            if (CheckSalaryAdjustTempError(Convert.ToInt32(Session["uID"])))
            {
                if (ImportSalaryAdjust(Convert.ToInt32(Session["uID"])))
                {
                    ltlAlert.Text = "alert('����ɹ���')";
                }
                else
                {
                    ltlAlert.Text = "alert('����ʧ�ܣ�����ϵ����Ա��\\nFail to import SalaryAdjust.')";
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

        #region ����Excel�ļ�
        string excelFileSuffix = excelFile.Value.Trim().Substring(excelFile.Value.Trim().IndexOf('.') + 1);
        if (excelFileSuffix != "xls" && excelFileSuffix != "xlsx")
        {
            ltlAlert.Text = "alert('��ѡ��Excel�ļ���')";
            return false;
        }
        string strServerPath = Server.MapPath("/import");       /*���������Excel�ļ���·��*/
        string strPostedFileName = excelFile.PostedFile.FileName.Trim();      /*Excel�ļ�������*/
        string strFileName = strPostedFileName.Substring(strPostedFileName.LastIndexOf('\\') + 1).Trim();
        if (excelFile.PostedFile.ContentLength > 8388608)
        {
            ltlAlert.Text = "alert('�ļ���С���ܳ���8 MB��')";
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
                ltlAlert.Text = "alert('Excel�ļ��ϴ�ʧ�ܣ�����ϵ����Ա��\\nError��Fail to create server directory.')";
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
            ltlAlert.Text = "alert('Excel�ļ��ϴ�ʧ�ܣ�����ϵ����Ա��\\nError��Fail to upload the excel file.')";
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
                ltlAlert.Text = "alert('����Excel����ʧ�ܣ����Ժ����ԣ�\\nError��Fail to load the excel file.')";
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
                        ltlAlert.Text = "alert('����Excel����ʧ�ܣ����Ժ����ԣ�\\nError��Fail to delete the excel file.')";
                    }
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('Ҫ���ص�Excel�ļ������ڣ�����ϵ����Ա��\\nError��Fail to find the uploaded excel file.')";
            return false;
        }
        #endregion

        if (dtExcel.Rows.Count > 0)
        {
            /*
             *  ����Excel�ļ��������㣺
             *      ������������Ϊ����ݡ��·ݡ���˾���ơ����š����š����Ρ����顢���֡������ȡ�����������ԭ��
             */
            if (dtExcel.Columns[0].ColumnName != "���" || dtExcel.Columns[1].ColumnName != "�·�"  || dtExcel.Columns[2].ColumnName != "����" 
                    || dtExcel.Columns[3].ColumnName != "����" || dtExcel.Columns[4].ColumnName != "����" || dtExcel.Columns[5].ColumnName != "����" 
                    || dtExcel.Columns[6].ColumnName != "����" || dtExcel.Columns[7].ColumnName != "������"   || dtExcel.Columns[8].ColumnName != "�������" 
                    || dtExcel.Columns[9].ColumnName != "����ԭ��")
            {
                dtExcel.Dispose();
                ltlAlert.Text = "alert('Excelģ�治��ȷ��\\nExcel�ļ�ǰ11������Ӧ�������£�\\n��ݡ��·ݡ����š����š����Ρ����顢���֡������ȡ�����������ԭ��')";
                return false;
            }

            //ת����ģ���ʽ
            DataTable table = new DataTable("temp");
            DataColumn column;
            DataRow row;

            #region �������
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

                    #region ��ֵ�������ж�
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
                            ltlAlert.Text = "alert('����ʱ��������ϵ����Ա��\\nError��Fail to write to the server.')";
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
                ltlAlert.Text = "alert('��ʱ���������ʧ�ܣ�')";
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
