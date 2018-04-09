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
            ltlAlert.Text = "alert('�����µ�¼��')";
        }
        else if (txbGenerateDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڲ���Ϊ�գ�')";
        }
        else if (!IsDate(txbGenerateDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('�������ڸ�ʽ���ԣ�')";
        }
        else if (txbCheckedDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�̵����ڲ���Ϊ�գ�')";
        }
        else if (!IsDate(txbCheckedDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('�̵����ڸ�ʽ���ԣ�')";
        }
        else if (txbGenerateDate.Text.Trim() != txbCheckedDate.Text.Trim())
        {
            ltlAlert.Text = "alert('�̵��������������ڱ�����ͬһ�죡')";
        }
        else if (txbFinance.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���Ų���Ϊ�գ�')";
        }
        else if (!GetUser(txbFinance.Text.Trim()))
        {
            ltlAlert.Text = "alert('���Ų���ȷ��')";
        }
        else if (ImportExcelFile())
        {
            if (CheckPartDailyError(Session["uID"].ToString()))
            {
                if (ImportParts(Session["uID"].ToString()))
                {
                    ltlAlert.Text = "alert('����ɹ�!');";
                }
                else
                {
                    ltlAlert.Text = "alert('����ʧ��!\\nFail to import partDaily.');";
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
        #region       �ж��Ƿ�ѡ���ļ����Լ�ѡ����Ƿ���excel�ļ�
        if (excelFile.Value.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��ѡ��Ҫ�����Excel�ļ���')";
            return false;
        }
        string excelFileSuffix = excelFile.Value.Trim().Substring(excelFile.Value.Trim().LastIndexOf('.') + 1);
        if (excelFileSuffix != "xls" && excelFileSuffix != "xlsx")
        {
            ltlAlert.Text = "alert('��ѡ��Excel�ļ���')";
            return false;
        }
        #endregion

        #region       ���Excel�ļ���С������Excel�ļ�������Ŀ¼���ϴ�Excel�ļ���������
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

        //�ϴ�Excel�ļ���������
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
        #endregion

        if (File.Exists(strFullName))
        {
            DataTable dtblExcel = new DataTable();
            #region   ����Excel����
            try
            {
                dtblExcel = this.GetExcelContents(strFullName);
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

                if (dtblExcel.Columns[0].ColumnName != "�ص�" || dtblExcel.Columns[1].ColumnName != "��λ" || dtblExcel.Columns[2].ColumnName != "QAD��" || dtblExcel.Columns[3].ColumnName != "����" || dtblExcel.Columns[4].ColumnName != "ϵͳ���" || dtblExcel.Columns[5].ColumnName != "�̵���" || dtblExcel.Columns[6].ColumnName != "����ԭ��")
                {
                    ltlAlert.Text = "alert('ģ�岻��ȷ����ȷ��ǰ7�е������ǣ�\\n�ص㡢��λ��QAD�š�����\\nϵͳ��桢�̵��桢����ԭ��')";
                    return false;
                }

                #region ������ʱ����Դ��
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
                                ltlAlert.Text = "alert('����ʱ��������ϵ����Ա��\\nError��Fail to write to the server.\\n" + ex.Message + "')";
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
                    ltlAlert.Text = "alert('��ʱ���������ʧ�ܣ�')";
                    return false;
                }
            }
            dtblExcel.Dispose();
        }
        else
        {
            ltlAlert.Text = "alert('Ҫ���ص�Excel�ļ������ڣ�����ϵ����Ա��\\nError��Fail to find the uploaded excel file.')";
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
