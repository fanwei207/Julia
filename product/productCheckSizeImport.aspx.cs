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

public partial class product_productCheckSizeImport : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropFileType.Items.Clear();
            dropFileType.Items.Add(new ListItem("Excel (.xls) file", "0"));
            dropFileType.SelectedValue = "0";
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"].ToString() == string.Empty)
        {
            ltlAlert.Text = "alert('�����µ�¼��')";
        }
        else if (ImportExcelFile())
        {
            if (CheckItemsTempError(Session["uID"].ToString()))
            {
                if (ImportItems(Session["uID"].ToString()))
                {
                    ltlAlert.Text = "alert('����ɹ���')";
                }
                else
                {
                    ltlAlert.Text = "alert('����ʧ��!\\nFail to import items.');";
                }
            }
            else
            {
                ltlAlert.Text = "window.open('/product/productCheckSizeError.aspx?rm=" + DateTime.Now.ToString() + "', '_blank');";
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
                if (dtblExcel.Columns[0].ColumnName != "��Ʒ�ͺ�" || dtblExcel.Columns[1].ColumnName != "����(kg)" || dtblExcel.Columns[2].ColumnName != "���(m3)" || dtblExcel.Columns[3].ColumnName != "��(cm)" || dtblExcel.Columns[4].ColumnName != "��(cm)" || dtblExcel.Columns[5].ColumnName != "��(cm)")
                {
                    ltlAlert.Text = "alert('ģ�岻��ȷ����ȷ��ǰ6�е������ǣ�\\n��Ʒ�ͺš�����(kg)�����(m3)\\n��(cm)����(cm)����(cm)')";
                    return false;
                }

                #region ������ʱ����Դ��
                DataTable table = new DataTable("temp");
                DataColumn column;
                DataRow row;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "code";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "weight";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "size";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "length";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "width";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "height";
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

                if (ClearItemsTemp(Session["uID"].ToString()))
                {
                    for (int i = 0; i < dtblExcel.Rows.Count; i++)
                    {
                        row = table.NewRow();
                        row["code"] = dtblExcel.Rows[i][0].ToString();
                        row["weight"] = dtblExcel.Rows[i][1].ToString();
                        row["size"] = dtblExcel.Rows[i][2].ToString();
                        row["length"] = dtblExcel.Rows[i][3].ToString();
                        row["width"] = dtblExcel.Rows[i][4].ToString();
                        row["height"] = dtblExcel.Rows[i][5].ToString();
                        row["createdBy"] = Session["uID"].ToString();
                        row["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        table.Rows.Add(row);
                    }
                    if (table != null && table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopy.DestinationTableName = "dbo.Items_temp";
                            bulkCopy.ColumnMappings.Add("code", "code");
                            bulkCopy.ColumnMappings.Add("weight", "box_chk_weight");
                            bulkCopy.ColumnMappings.Add("size", "box_chk_size");
                            bulkCopy.ColumnMappings.Add("length", "box_chk_length");
                            bulkCopy.ColumnMappings.Add("width", "box_chk_width");
                            bulkCopy.ColumnMappings.Add("height", "box_chk_depth");
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

    protected bool ClearItemsTemp(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "Item_clearItemsTemp", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckItemsTempError(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "Items_checkItemsTempError", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool ImportItems(string uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@retValue", DbType.Boolean);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "Item_importItemsCheckSize", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
}
