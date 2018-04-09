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
            DataTable dtExcel = new DataTable();
            #region   加载Excel数据
            try
            {
                dtExcel = this.GetExcelContents(strFullName);//chk.getExcelContents(strFullName).Tables[0];
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

            if (dtExcel.Rows.Count > 0)
            {
                if (dtExcel.Columns[0].ColumnName != "工号" || dtExcel.Columns[1].ColumnName != "工序名称" || dtExcel.Columns[2].ColumnName != "岗位名称")
                {
                    ltlAlert.Text = "alert('" + dtExcel.Columns[0].ColumnName + "\\n" + dtExcel.Columns[1].ColumnName + "\\n" + dtExcel.Columns[2].ColumnName + "')";
                    // ltlAlert.Text = "alert('模板不正确！请确认前3列的名称是：\\n工号、工序名称和岗位名称')";
                    return false;
                }

                #region   转换成usersPostTemp格式
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
                                    ltlAlert.Text = "alert('导入时出错，请联系管理员！\\nError：Fail to write to the server.')";
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
            }
        }
        else
        {
            ltlAlert.Text = "alert('要加载的Excel文件不存在！请联系管理员！\\nError：Fail to find the uploaded excel file.')";
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
                        ltlAlert.Text = "alert('用户岗位更新成功！')";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('用户岗位更新失败！\\nFail to import the userspost.')";
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
            ltlAlert.Text = "alert('请重新登录！')";
        }
    }
}
