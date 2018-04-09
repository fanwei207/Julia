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

public partial class RDW_UL_QADImport : BasePage
{
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnRouting_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (CheckValidity(Session["uID"].ToString()))
            {


                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    // Importcustpart(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));

                    ltlAlert.Text = "alert('导入成功!');";
                    //string strID = Convert.ToString(Request.QueryString["mid"]);
                    //Response.Redirect("/RDW/UL_QAD.aspx?t=&mid=" + strID + "&rm=" + DateTime.Now.ToString(), true); 
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!');";
                }
            }
            else
            {
                // ltlAlert.Text = "window.open('CustPartImportError.aspx?rt=" + DateTime.Now.ToString() + "', '_blank');";
            }
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
                    if (dt.Columns.Count != 3)
                    {
                        dt.Reset();
                        ltlAlert.Text = "alert('该文件必须有3列！');";
                        return false;
                    }





                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {

                        if (col == 2 && dt.Columns[col].ColumnName.Trim() != "Project")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 Project！');";
                            return false;
                        }
                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "Model")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 Model！');";
                            return false;
                        }
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "QAD")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 QAD！');";
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

                    DataRow rowError;//错误表的行

                    //转换成模板格式
                    DataTable table = new DataTable("temp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "ID";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Project";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Model";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qad";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createname";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
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
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定
                            //custCode的长度允许最长15个字符，否则截取
                            //if (r[0].ToString().Trim().Length == 0)
                            //{

                            //    rowError = tblError.NewRow();

                            //    rowError["errInfo"] = "Project不能为空,见表" + line + "行";
                            //    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                            //    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                            //    tblError.Rows.Add(rowError);
                            //}
                           
                            //else
                            //{
                                row["Project"] = r[0].ToString().Trim();
                            //}

                            if (r[1].ToString().Trim().Length == 0)
                            {

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "Model不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            else
                            {
                                row["Model"] = r[1].ToString().Trim();
                            }
                            if (r[2].ToString().Trim().Length == 0)
                            {

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "QAD不能为空,见表" + line + "行";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            else
                            {
                                row["qad"] = r[2].ToString().Trim();
                            }

                            #endregion

                            row["createdBy"] = _uID;
                            row["createname"] = Convert.ToString(Session["uID"]);
                           // row["ID"] = Convert.ToString(Request.QueryString["mid"]);
                           // row["Project"] = Convert.ToString(Request.QueryString["Project"]);
                            row["errMsg"] = string.Empty;

                            table.Rows.Add(row);
                        }

                        if (tblError != null && tblError.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(strConn))
                            {
                                bulkCopyError.DestinationTableName = "dbo.UL_ImportError";
                                bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                                bulkCopyError.ColumnMappings.Add("uID", "userID");
                                bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                                try
                                {
                                    bulkCopyError.WriteToServer(tblError);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('错误表导入错误,请联系系统管理员!');";
                                    return false;
                                }
                                finally
                                {
                                    bulkCopyError.Close();
                                    tblError.Dispose();
                                }
                            }
                        }
                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
                            {
                                bulkCopy.DestinationTableName = "dbo.UL_QADTemp";
                                bulkCopy.ColumnMappings.Add("ID", "ULT_ID");
                                bulkCopy.ColumnMappings.Add("Project", "ULT_Project");
                                bulkCopy.ColumnMappings.Add("qad", "ULT_QAD");
                                bulkCopy.ColumnMappings.Add("createname", "ULT_createName");
                                bulkCopy.ColumnMappings.Add("createdBy", "ULT_createBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "ULT_errMsg");
                                bulkCopy.ColumnMappings.Add("Model", "ULT_model");

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
    protected bool CheckValidity(string uID)
    {
        Importcustpart(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));
        string newid_no = Request.Params["newid_no"];
        string strSql2 = " Select top 1 * From UL_ImportError where userID ='" + Convert.ToInt32(Session["uID"]) + "' and  plantID= '" + Convert.ToInt32(Session["PlantCode"]) + "'";

        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                Session["EXTitle"] = "500^<b>系统提示</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From RD_Workflow.dbo.UL_ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                ltlAlert.Text = "alert('导入失败!');window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
                return false;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed! \\n 提交信息验证失败！');Form1.usercode.focus();";
            return false;
        }
        return true;
        //try
        //{
        //    string strSql = "sp_edi_checkCustPartValidity";

        //    SqlParameter[] sqlParam = new SqlParameter[2];
        //    sqlParam[0] = new SqlParameter("@uID", uID);
        //    sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
        //    sqlParam[1].Direction = ParameterDirection.Output;

        //    SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);

        //    return Convert.ToBoolean(sqlParam[1].Value);
        //}
        //catch
        //{
        //    return false;
        //}

    }
    public int Importcustpart(Int32 uID, Int32 plantcode)
    {
        string strSQL = "sp_UL_QADimport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@plantcode", plantcode);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSQL, parm));

    }
    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_UL_clearQADTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
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

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_UL_insertQAD", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        Response.Redirect("/RDW/UL_QAD.aspx?t=&mid=" + strID + "&rm=" + DateTime.Now.ToString(), true); 
    }
}