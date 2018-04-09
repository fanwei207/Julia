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

public partial class Test_Test_Ques_import : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_clearquesmstr", param);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public Boolean ImportExcelFile()
    {
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int line = 0;
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
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dt = this.GetExcelContents(strFileName);
                    //ds = chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('导入文件必须是Excel格式!');";
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



                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "题目类型")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 题目类型！');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "所属模块")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 所属模块！');";
                            return false;
                        }
                        //else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "开始时间")
                        //{
                        //    dt.Reset();
                        //    ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 开始时间！');";
                        //    return false;
                        //}
                        //else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "截止时间")
                        //{
                        //    dt.Reset();
                        //    ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 截止时间！');";
                        //    return false;
                        //}
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "题目")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 题目！');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "答案")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 答案！');";
                            return false;
                        }
                        else if (col == 4 && dt.Columns[col].ColumnName.Trim() != "A")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 A！');";
                            return false;
                        }
                        else if (col == 5 && dt.Columns[col].ColumnName.Trim() != "B")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 B！');";
                            return false;
                        }
                          else if (col == 6 && dt.Columns[col].ColumnName.Trim() != "C")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 C！');";
                            return false;
                        }
                          else if (col == 7 && dt.Columns[col].ColumnName.Trim() != "D")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 D！');";
                            return false;
                        }
                          else if (col == 8 && dt.Columns[col].ColumnName.Trim() != "E")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 E！');";
                            return false;
                        }
                          else if (col == 9 && dt.Columns[col].ColumnName.Trim() != "F")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 F！');";
                            return false;
                        }
                        else if (col == 10 && dt.Columns[col].ColumnName.Trim() != "必考")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 必考！');";
                            return false;
                        }

                    }
                    #endregion
                    //构建ImportError
                    DataColumn column;
                    DataTable tblError = new DataTable("import_error");

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errInfo";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "plantCode";
                    tblError.Columns.Add(column);

                    //DataRow rowError;//错误表的行

                    //转换成模板格式
                    DataTable table = new DataTable("Ques_temp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "type";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "startdate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "enddate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "title";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "answer";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "A";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "B";
                    table.Columns.Add(column);
                     column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "C";
                    table.Columns.Add(column);
                     column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "D";
                    table.Columns.Add(column);
                     column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "E";
                    table.Columns.Add(column);

                     column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "F";
                    table.Columns.Add(column);

                   
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "queskey";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CreateBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CreateName";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);
                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);
                    string strerror = "";

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定
                            if (r["题目类型"].ToString().Trim().Length == 0)
                            {
                                strerror += "题目类型不能为空.";
                            }


                            if (r["所属模块"].ToString().Trim().Length == 0)
                            {
                                strerror += "所属模块不能为空.";
                            }

                            //if (r["开始时间"].ToString().Trim().Length == 0)
                            //{
                            //    strerror += "开始时间不能为空.";
                              
                            //}
                            if (r["题目"].ToString().Trim().Length == 0)
                            {
                                strerror += "题目不能为空.";

                            }
                            if (r["答案"].ToString().Trim().Length == 0)
                            {
                                strerror += "答案不能为空.";

                            }


                            row["type"] = r["题目类型"].ToString().Trim();
                            row["cate"] = r["所属模块"].ToString().Trim();
                            row["startdate"] = "1900-1-1";
                            row["enddate"] = "";
                            row["title"] = r["题目"].ToString().Trim();
                            row["answer"] = r["答案"].ToString().Trim();
                            row["A"] = r["A"].ToString().Trim();
                            row["B"] = r["B"].ToString().Trim();
                            row["C"] = r["C"].ToString().Trim();
                            row["D"] = r["D"].ToString().Trim();
                            row["E"] = r["E"].ToString().Trim();
                            row["F"] = r["F"].ToString().Trim();
                            row["queskey"] = r["必考"].ToString().Trim();
                            row["CreateBy"] = Session["uID"].ToString();
                            row["CreateName"] = Session["uName"].ToString();

                            #endregion

                            if (strerror != "")
                            {
                                row["errMsg"] = strerror;
                            }
                            else
                            {
                                row["errMsg"] = string.Empty;
                            }
                            table.Rows.Add(row);
                            strerror = "";
                        }


                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
                            {
                                bulkCopy.DestinationTableName = "dbo.Ques_temp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("type", "type");
                                bulkCopy.ColumnMappings.Add("cate", "cate");
                                bulkCopy.ColumnMappings.Add("startdate", "startdate");
                                bulkCopy.ColumnMappings.Add("enddate", "enddate");
                                bulkCopy.ColumnMappings.Add("title", "title");
                                bulkCopy.ColumnMappings.Add("answer", "answer");
                                bulkCopy.ColumnMappings.Add("A", "A");
                                bulkCopy.ColumnMappings.Add("B", "B");
                                bulkCopy.ColumnMappings.Add("C", "C");
                                bulkCopy.ColumnMappings.Add("D", "D");
                                bulkCopy.ColumnMappings.Add("E", "E");
                                bulkCopy.ColumnMappings.Add("F", "F");
                                bulkCopy.ColumnMappings.Add("queskey", "queskey");
                                bulkCopy.ColumnMappings.Add("CreateBy", "CreateBy");
                                bulkCopy.ColumnMappings.Add("CreateName", "CreateName");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('导入临时表时出错，请联系系统管理员！');";
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
    private bool checkTemp()
    {
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_Checkquestemp");

        string strSql2 = "select * from Ques_temp where isnull( errMsg ,'') <>'' and CreateBy = " + Session["uID"].ToString();
        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "100^<b>题目类型</b>~^100^<b>所属模块</b>~^100^<b>开始时间</b>~^100^<b>截止时间</b>~^100^<b>题目</b>~^100^<b>答案</b>~^100^<b>错误信息</b>~^";

                string sql = " select type,[cate],[startdate],[enddate],[title],[answer],errMsg from Ques_temp  ";

                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('导入失败!');";
                ExportExcel(title, dt, false);
                return false;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed! \\n 提交信息验证失败！');Form1.usercode.focus();";
            return false;
        }
        return true;
    }
    private bool InsertBatchTemp(string uID, string uName)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_test_insertques", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (checkTemp())
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('导入成功!');";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!');";
                }
            }

        }
    }
}