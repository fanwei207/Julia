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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_wo2_updateUserPost : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    { }

    protected bool ImportExcelFile()
    {
        #region       �ж��Ƿ�ѡ���ļ����Լ�ѡ����Ƿ���excel�ļ�
        if (excelFile.Value.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��ѡ��Ҫ�����Excel�ļ���')";
            return false;
        }
        string excelFileSuffix = excelFile.Value.Trim().Substring(excelFile.Value.Trim().IndexOf('.') + 1);
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
            DataTable dtExcel = new DataTable();
            #region   ����Excel����
            try
            {
                dtExcel = this.GetExcelContents(strFullName);//chk.getExcelContents(strFullName).Tables[0];
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

            if (dtExcel.Rows.Count > 0)
            {
                if (dtExcel.Columns[0].ColumnName != "����" || dtExcel.Columns[1].ColumnName != "��������" || dtExcel.Columns[2].ColumnName != "��λ����")
                {
                    ltlAlert.Text = "alert('" + dtExcel.Columns[0].ColumnName + "\\n" + dtExcel.Columns[1].ColumnName + "\\n" + dtExcel.Columns[2].ColumnName + "')";
                    // ltlAlert.Text = "alert('ģ�岻��ȷ����ȷ��ǰ3�е������ǣ�\\n���š��������ƺ͸�λ����')";
                    return false;
                }

                #region   ת����usersPostTemp��ʽ
                DataTable table = new DataTable("temp");
                DataColumn column;
                DataRow row;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "plantCode";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "userNo";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "processName";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "postName";
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

                if (Session["uID"] != null)
                {
                    if (ClearUserPostTemp(Session["uID"].ToString()))
                    {
                        foreach (DataRow dRow in dtExcel.Rows)
                        {
                            row = table.NewRow();

                            row["plantCode"] = Convert.ToInt32(Session["plantCode"]);
                            if (dRow[0].ToString().Length > 50)
                            {
                                row["userNo"] = dRow[0].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["userNo"] = dRow[0].ToString();
                            }

                            if (dRow[1].ToString().Length > 50)
                            {
                                row["processName"] = dRow[1].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["processName"] = dRow[1].ToString();
                            }

                            if (dRow[2].ToString().Length > 50)
                            {
                                row["postName"] = dRow[2].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["postName"] = dRow[2].ToString();
                            }

                            row["createdBy"] = Convert.ToInt32(Session["uID"]);
                            row["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm");

                            table.Rows.Add(row);
                        }

                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopy.DestinationTableName = ".dbo.usersPostTemp";
                                bulkCopy.ColumnMappings.Add("plantCode", "plantCode");
                                bulkCopy.ColumnMappings.Add("userNo", "userNo");
                                bulkCopy.ColumnMappings.Add("processName", "processName");
                                bulkCopy.ColumnMappings.Add("postName", "postName");
                                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                                bulkCopy.ColumnMappings.Add("createdDate", "createdDate");

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
            }
        }
        else
        {
            ltlAlert.Text = "alert('Ҫ���ص�Excel�ļ������ڣ�����ϵ����Ա��\\nError��Fail to find the uploaded excel file.')";
            return false;
        }
        return true;
    }

    protected bool ClearUserPostTemp(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_clearUsersPostTemp", param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckUserPostTemp(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_checkUsersPostTemp", param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool ImportUsersPost(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_importUsersPost", param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Session["uID"] != null || Session["plantCode"] != null)
        {
            if (ImportExcelFile())
            {
                if (CheckUserPostTemp(Session["uID"].ToString()))
                {
                    if (ImportUsersPost(Session["uID"].ToString()))
                    {
                        ltlAlert.Text = "alert('�û���λ���³ɹ���')";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('�û���λ����ʧ�ܣ�\\nFail to import the userspost.')";
                    }
                }
                else
                {
                    ltlAlert.Text = "window.open('wo2_updateUserPostError.aspx','_blank')";
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('�����µ�¼��')";
        }
    }
}
