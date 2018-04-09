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
using System.Data.Odbc;
using TCPCHINA.ODBCHelper;
public partial class part_part_ItemUpdateImport : BasePage
{
    adamClass chk = new adamClass();
    String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (CheckValidity(Session["uID"].ToString()))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString(), txtremark.Text.Trim()))
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
                string title = "100^<b>QAD</b>~^100^<b>物料号</b>~^100^<b>描述</b>~^100^<b>QAD描述1 </b>~^100^<b>QAD描述2</b>~^100^<b>错误信息</b>~^";

                string sql = " 	SELECT QAD,part,describe,desc1,desc2,errMsg	from Part_ItemsUpdateTemp where createBy =" + Session["uID"].ToString();

                DataTable dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('导入失败!');";
                ExportExcel(title, dt, false);

            }
        }
    }
    private bool InsertBatchTemp(string uID, string uName, string remark)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@remark", remark);
            sqlParam[3] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_part_insertItemUpdate", sqlParam);

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
            string strSql = "sp_part_checkItemsUpdate";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[1].Value);
        }
        catch
        {
            return false;
        }
    }
    public Boolean ImportExcelFile()
    {
        DataTable dt;
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
            string error = "";
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

                    if (dt.Columns.Count != 5)
                    {
                        dt.Reset();

                        ltlAlert.Text = "alert('The file must have 5 columns！');";

                        return false;
                    }

                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "QAD")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD！');";
                            return false;
                        }
                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "物料号")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 物料号！');";
                            return false;
                        }

                        if (col == 2 && dt.Columns[col].ColumnName.Trim() != "描述")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 描述！');";
                            return false;
                        }

                        if (col == 3 && dt.Columns[col].ColumnName.Trim() != "QAD描述1")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD描述1 ！');";
                            return false;
                        }

                        if (col == 4 && dt.Columns[col].ColumnName.Trim() != "QAD描述2")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD描述2！');";
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
                    column.ColumnName = "QAD";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "part";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "describe";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "desc1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "desc2";
                    table.Columns.Add(column);

                   

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createBy";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createname";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
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

                                if (r[0].ToString().Trim() == string.Empty)
                                {
                                    error += "QAD号不能为空；";
                                }
                                else
                                {
                                    row["QAD"] = r[0].ToString().Trim();
                                }


                                row["part"] = r[1].ToString().Trim();


                                //custPo的长度允许最长20个字符，否则截取

                                row["describe"] = r[2].ToString().Trim();
                                if (r[3].ToString().Length > 24)
                                {
                                    error += "描述1不能超过24个字符；";
                                }
                                else
                                {
                                    row["desc1"] = r[3].ToString().Trim();
                                }
                                if (r[4].ToString().Length > 24)
                                {
                                    error += "描述2不能超过24个字符；";
                                }
                                else
                                {

                                    row["desc2"] = r[4].ToString().Trim();

                                }
                                



                                #endregion

                                row["createBy"] = _uID;
                                row["createname"] = Session["uName"].ToString();
                                if (error == "")
                                {
                                    row["errMsg"] = string.Empty;
                                }
                                else
                                {
                                    row["errMsg"] = error;
                                }

                                table.Rows.Add(row);
                            }
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.Part_ItemsUpdateTemp";
                                bulkCopy.ColumnMappings.Add("QAD", "QAD");
                                bulkCopy.ColumnMappings.Add("part", "part");
                                bulkCopy.ColumnMappings.Add("describe", "describe");
                                bulkCopy.ColumnMappings.Add("desc1", "desc1");
                                bulkCopy.ColumnMappings.Add("desc2", "desc2");
                                bulkCopy.ColumnMappings.Add("createBy", "createBy");
                                bulkCopy.ColumnMappings.Add("createname", "createname");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");
                               



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
    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_Part_clearItemsUpdateTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }
}