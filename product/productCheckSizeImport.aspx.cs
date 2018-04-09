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
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else if (ImportExcelFile())
        {
            if (CheckItemsTempError(Session["uID"].ToString()))
            {
                if (ImportItems(Session["uID"].ToString()))
                {
                    ltlAlert.Text = "alert('导入成功！')";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!\\nFail to import items.');";
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
        #region       判断是否选择文件，以及选择的是否是excel文件
        if (excelFile.Value.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择要导入的Excel文件！')";
            return false;
        }
        string excelFileSuffix = excelFile.Value.Trim().Substring(excelFile.Value.Trim().IndexOf('.') + 1);
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
                if (dtblExcel.Columns[0].ColumnName != "产品型号" || dtblExcel.Columns[1].ColumnName != "重量(kg)" || dtblExcel.Columns[2].ColumnName != "体积(m3)" || dtblExcel.Columns[3].ColumnName != "长(cm)" || dtblExcel.Columns[4].ColumnName != "宽(cm)" || dtblExcel.Columns[5].ColumnName != "高(cm)")
                {
                    ltlAlert.Text = "alert('模板不正确！请确认前6列的名称是：\\n产品型号、重量(kg)、体积(m3)\\n长(cm)、宽(cm)、高(cm)')";
                    return false;
                }

                #region 创建临时数据源表
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
