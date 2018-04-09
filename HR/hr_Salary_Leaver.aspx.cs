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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Data.OleDb;
using Wage;
/// <summary>
/// 离职人员工资暂扣
/// </summary>
public partial class hr_Salary_Leaver : BasePage
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //导出数据校验后的错误信息
            if (Request.QueryString["ty"] == "ErrorExport")
            {
                DataSet ds = SelectImportErrorList(Session["uID"].ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                    doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("SalaryLeaverUsers错误信息");

                    #region 设置列宽
                    AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col1.ColumnIndexStart = 0;
                    col1.ColumnIndexEnd = 0;
                    col1.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col1);

                    AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col2.ColumnIndexStart = 1;
                    col2.ColumnIndexEnd = 1;
                    col2.Width = 60 * 6000 / 164;
                    sheet.AddColumnInfo(col2);

                    AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col3.ColumnIndexStart = 2;
                    col3.ColumnIndexEnd = 2;
                    col3.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col3);

                    AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col4.ColumnIndexStart = 3;
                    col4.ColumnIndexEnd = 3;
                    col4.Width = 90 * 6000 / 164;
                    sheet.AddColumnInfo(col4);

                    AppLibrary.WriteExcel.ColumnInfo col5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col5.ColumnIndexStart = 4;
                    col5.ColumnIndexEnd = 4;
                    col5.Width = 180 * 6000 / 164;
                    sheet.AddColumnInfo(col5);

                    #endregion

                    int rowIndex = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PrintErrorExcel(doc, sheet, rowIndex, row["userNo"], row["userName"], row["department"], row["slfDate"], row["errMsg"]);
                        if (rowIndex == 1)
                        {
                            rowIndex += 2;
                        }
                        else
                        {
                            rowIndex++;
                        }
                    }

                    doc.Save(Server.MapPath("/Excel/"), true);
                    ds.Dispose();

                    ltlAlert.Text = "window.open('/Excel/" + doc.FileName + "','_blank');";
                }

            }

            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            dropDeptBind();
            btnLock.Visible = this.Security["6050041"].isValid;
            btnUnLock.Visible = this.Security["6050041"].isValid;
        }
        
    }

    protected void GridViewBind()
    {
        btnLock.Enabled = !IsLeaverSalaryLocked(txtYear.Text, txtMonth.Text);
        BtnRouting.Enabled = btnLock.Enabled;
        gv.DataSource = selectHrSalaryLeaverList();
        gv.DataBind();
    }

    /// <summary>
    /// 检验指定年月的暂存工资是否被锁定
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    protected bool IsLeaverSalaryLocked(string year, string month)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@year", txtYear.Text);
            param[1] = new SqlParameter("@month", txtMonth.Text);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_isSalaryLeaverLocked", param);

            return Convert.ToBoolean(param[2].Value);
        }
        catch
        {
            return false;
        }
    }


    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[7].Enabled = btnLock.Enabled;
        
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!this.IsDate(txtYear.Text + "-" + txtMonth.Text + "-1"))
        {
            this.Alert("请正确填写年月！");
            return;
        }

        GridViewBind();
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@year", txtYear.Text);
            param[1] = new SqlParameter("@month", txtMonth.Text);
            param[2] = new SqlParameter("@uID", Session["uID"].ToString());
            param[3] = new SqlParameter("@uName", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_lockSalaryLeaver", param);

            if (Convert.ToBoolean(param[4].Value))
            {
                this.Alert("已锁定！");

                GridViewBind();
            }
            else
            {
                this.Alert("锁定失败，请联系管理员！");
            }
        }
        catch
        {
            this.Alert("锁定操作失败！刷新后重新操作一次！");
        }
    }
    protected void btnUnLock_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@year", txtYear.Text);
            param[1] = new SqlParameter("@month", txtMonth.Text);
            param[2] = new SqlParameter("@uID", Session["uID"].ToString());
            param[3] = new SqlParameter("@uName", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_unlockSalaryLeaver", param);

            if (Convert.ToBoolean(param[4].Value))
            {
                this.Alert("已解锁！");

                GridViewBind();
            }
            else
            {
                this.Alert("解锁失败，请联系管理员！");
            }
        }
        catch
        {
            this.Alert("解锁操作失败！刷新后重新操作一次！");
        }
    }
    protected void BtnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (!CheckValidity(Session["uID"].ToString()))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
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
                //ltlAlert.Text = "window.open('hr_Salary_Leaver.aspx?ty=export&rt=" + DateTime.Now.ToString() + "', '_blank');";
                Response.Redirect("/HR/hr_Salary_Leaver.aspx?ty=ErrorExport");
            }
        }
    }

    protected bool ImportExcelFile()
    {
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
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
                ltlAlert.Text = "alert('Failed to upload files.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    //ds = GetExcelContents(strFileName);
                    dt = this.GetExcelContents(strFileName);
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
                     *      1、至少应该有4列
                     *      2、工号必须存在
                    */
                    if (dt.Columns.Count != 4)
                    {
                        //ds.Reset();
                        ltlAlert.Text = "alert('该文件必须有4列！');";
                        return false;
                    }

                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {

                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "工号")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 工号！');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "姓名")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 姓名！');";
                            return false;
                        }

                        if (col == 2 && dt.Columns[col].ColumnName.Trim() != "部门")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 部门！');";
                            return false;
                        }

                        if (col == 3 && dt.Columns[col].ColumnName.Trim() != "暂存年月")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 暂存年月！');";
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
                    column.ColumnName = "userNo";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "userName";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Department";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "slfDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
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
                            row = table.NewRow();

                            #region 赋值、长度判定
                            row["userNo"] = r[0].ToString().Trim();
                            row["userName"] = r[1].ToString().Trim();
                            row["Department"] = r[2].ToString().Trim();
                            row["slfDate"] = r[3].ToString().Trim();
                            #endregion

                            row["createdBy"] = _uID;
                            row["errMsg"] = string.Empty;

                            table.Rows.Add(row);
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsnx()))
                            {
                                bulkCopy.DestinationTableName = "dbo.hr_salary_leaverTemp";
                                bulkCopy.ColumnMappings.Add("userNo", "userNo");
                                bulkCopy.ColumnMappings.Add("userName", "userName");
                                bulkCopy.ColumnMappings.Add("Department", "department");
                                bulkCopy.ColumnMappings.Add("slfDate", "slfDate");
                                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
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

                //ds.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }

    protected bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_deleteSalaryLeaverTemp", param);

            return true;
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
            string strSql = "sp_hr_checkSalaryLeaverValidity";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[1].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool InsertBatchTemp(string uID, string uName)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_insertSalaryLeaver", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }

    protected DataSet SelectImportErrorList(string userid)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", userid);
            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_selectSalaryLeaverTempError", param);

        }
        catch (Exception ex)
        {
            return null;
        }
    
    
    }

    protected void PrintErrorExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex, Object userNo, Object userName, Object department, Object slfDate, Object errMsg)
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 13;

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        if (rowIndex == 1)
        {
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            sheet.Cells.Add(rowIndex, 1, "工号", xf);
            sheet.Cells.Add(rowIndex, 2, "姓名", xf);
            sheet.Cells.Add(rowIndex, 3, "部门", xf);
            sheet.Cells.Add(rowIndex, 4, "暂存年月", xf);
            sheet.Cells.Add(rowIndex, 5, "错误信息", xf);
            rowIndex++;
        }

        sheet.Cells.Add(rowIndex, 1, userNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, userName.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, department.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, slfDate.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, errMsg.ToString(), xf);
        
    }

    protected void PrintDataExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex, Object slDate, Object userNo, Object userName, Object deptName, Object salary, Object leavDate, Object lockDate, Object slRelDate)
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 13;

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        if (rowIndex == 1)
        {
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            sheet.Cells.Add(rowIndex, 1, "暂存年月", xf);
            sheet.Cells.Add(rowIndex, 2, "工号", xf);
            sheet.Cells.Add(rowIndex, 3, "姓名", xf);
            sheet.Cells.Add(rowIndex, 4, "部门", xf);
            sheet.Cells.Add(rowIndex, 5, "工资", xf);
            sheet.Cells.Add(rowIndex, 6, "离职日期", xf);
            sheet.Cells.Add(rowIndex, 7, "锁定日期", xf);
            sheet.Cells.Add(rowIndex, 8, "发放年月", xf);
            rowIndex++;
        }

            sheet.Cells.Add(rowIndex, 1, slDate.ToString(), xf);
            sheet.Cells.Add(rowIndex, 2, userNo.ToString(), xf);
            sheet.Cells.Add(rowIndex, 3, userName.ToString(), xf);
            sheet.Cells.Add(rowIndex, 4, deptName.ToString(), xf);
            sheet.Cells.Add(rowIndex, 5, salary.ToString(), xf);
            sheet.Cells.Add(rowIndex, 6, leavDate.ToString(), xf);
            sheet.Cells.Add(rowIndex, 7, lockDate.ToString(), xf);
            sheet.Cells.Add(rowIndex, 8, slRelDate.ToString(), xf);

    }

    #region Import Excel Data To a DataSet
    /*
    protected DataSet GetExcelContents(string filePath)
    {
        string conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 8.0;" + "Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();

        try
        {
            OleDbConnection excelConnection = new OleDbConnection(conString);

            excelConnection.Open();

            DataTable dtSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });

            String excelSchemaName = dtSchema.Rows[0].ItemArray[2].ToString();

            OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + excelSchemaName + "]", excelConnection);

            DataSet ds = new DataSet();

            excelDataAdapter.AcceptChangesDuringFill = false;

            excelDataAdapter.Fill(ds);

            excelConnection.Close();

            return ds;
        }
        catch (Exception ee)
        {
            string msg = ee.ToString();
        }
        return null;
    }
    */
    #endregion
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strYear = txtYear.Text.Trim();
        string strMonth = txtMonth.Text.Trim();
        string userid = gv.DataKeys[e.RowIndex].Value.ToString();
        if (Deletehr_salary_leaver(strYear, strMonth, userid, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            GridViewBind();
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程中出错！');";
            GridViewBind();
            return;
        }
    }

    protected bool Deletehr_salary_leaver(string year, string month, string userID, string uID, string uName)
    {
        try
        {
            string strSql = "sp_hr_deleteSalaryLeaver";
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@year", year);
            sqlParam[1] = new SqlParameter("@month", month);
            sqlParam[2] = new SqlParameter("@userID", userID);
            sqlParam[3] = new SqlParameter("@uID", uID);
            sqlParam[4] = new SqlParameter("@uName", uName);
            sqlParam[5] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
            return Convert.ToBoolean(sqlParam[5].Value);
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
    }

    protected void dropDeptBind()
    {
        ListItem item;
        item = new ListItem("--", "-1");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
    }

    protected DataSet selectHrSalaryLeaverList()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@year", txtYear.Text);
            param[1] = new SqlParameter("@month", txtMonth.Text == "" ? "0":txtMonth.Text);
            param[2] = new SqlParameter("@userNo", txtUserNo.Text);
            param[3] = new SqlParameter("@deptNo", dropDept.SelectedValue);
            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_hr_selectSalaryLeaver", param);
            
        }
        catch
        {
            return null;
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = selectHrSalaryLeaverList();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("工资暂存人员列表");

            #region 设置列宽
            AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col1.ColumnIndexStart = 0;
            col1.ColumnIndexEnd = 0;
            col1.Width = 100 * 6000 / 164;
            sheet.AddColumnInfo(col1);

            AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col2.ColumnIndexStart = 1;
            col2.ColumnIndexEnd = 1;
            col2.Width = 60 * 6000 / 164;
            sheet.AddColumnInfo(col2);

            AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col3.ColumnIndexStart = 2;
            col3.ColumnIndexEnd = 2;
            col3.Width = 100 * 6000 / 164;
            sheet.AddColumnInfo(col3);

            AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col4.ColumnIndexStart = 3;
            col4.ColumnIndexEnd = 3;
            col4.Width = 90 * 6000 / 164;
            sheet.AddColumnInfo(col4);

            AppLibrary.WriteExcel.ColumnInfo col5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col5.ColumnIndexStart = 4;
            col5.ColumnIndexEnd = 4;
            col5.Width = 100 * 6000 / 164;
            sheet.AddColumnInfo(col5);

            AppLibrary.WriteExcel.ColumnInfo col6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col6.ColumnIndexStart = 5;
            col6.ColumnIndexEnd = 5;
            col6.Width = 100 * 6000 / 164;
            sheet.AddColumnInfo(col6);

            AppLibrary.WriteExcel.ColumnInfo col7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col7.ColumnIndexStart = 6;
            col7.ColumnIndexEnd = 6;
            col7.Width = 100 * 6000 / 164;
            sheet.AddColumnInfo(col7);

            AppLibrary.WriteExcel.ColumnInfo col8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col8.ColumnIndexStart = 7;
            col8.ColumnIndexEnd = 7;
            col8.Width = 100 * 6000 / 164;
            sheet.AddColumnInfo(col8);

            #endregion

            int rowIndex = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                PrintDataExcel(doc, sheet, rowIndex, row["slDate"], row["userNo"], row["userName"], row["deptName"],row["salary"], row["leavDate"], row["lockDate"], row["slRelDate"]);
                if (rowIndex == 1)
                {
                    rowIndex += 2;
                }
                else
                {
                    rowIndex++;
                }
            }

            doc.Save(Server.MapPath("/Excel/"), true);
            ds.Dispose();

            ltlAlert.Text = "window.open('/Excel/" + doc.FileName + "','_blank');";
        }
        else
        {
            this.Alert("没有数据需要导出！");
        }
    }
}
