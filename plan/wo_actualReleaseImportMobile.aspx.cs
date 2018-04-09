using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;




public partial class plan_wo_actualReleaseImportMobile : BasePage
{
    private wo.Wo_ActualRelease helper = new wo.Wo_ActualRelease();
    private adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFileType.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            ddlFileType.Items.Add(item1);
        }
    }

    protected void uploadPartBtn_ServerClick(object sender, EventArgs e)
    {
        ImportExcelFile();
    }

    public void ImportExcelFile()
    {    
        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;


        string strPlant = "";
        switch (Session["PlantCode"].ToString())
        {
            case "1": strPlant = "SZX";
                break;
            case "2": strPlant = "ZQL";
                break;
            case "5": strPlant = "YQL";
                break;
            case "8": strPlant = "HQL";
                break;
        }


        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

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
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    string message="";
                    bool success = Import(strFileName, strUID, strPlant, out message);
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {
                        DataTable errDt = helper.GetImportError(strUID);
                        string title = "100^<b>加工单</b>~^100^<b>ID</b>~^100^<b>实际下达日期</b>~^100^<b>操作</b>~^100^<b>生产线</b>~^100^<b>成本中心</b>~^100^<b>出错</b>~^";
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }
          
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
    }
    public bool Import(string filePath, string uId, string plant, out string message)
    {
        message = "";
        DataTable dt = null;
        bool success = true;
        try
        {
            dt = this.GetExcelContents(filePath);
        }
        catch (Exception ex)
        {
            message = "导入文件必须是Excel格式'" + ex.Message.ToString() + "'.";
            success = false;
        }
        finally
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        if (success)
        {
            try
            {
                if (
                    dt.Columns[0].ColumnName != "加工单" ||
                    dt.Columns[1].ColumnName != "ID" ||
                    dt.Columns[2].ColumnName != "QAD" ||
                    dt.Columns[3].ColumnName != "QAD下达日期" ||
                    dt.Columns[4].ColumnName != "计划日期" ||
                    dt.Columns[5].ColumnName != "评审日期" ||
                    dt.Columns[6].ColumnName != "上线日期" ||
                    dt.Columns[7].ColumnName != "地点" ||
                    dt.Columns[8].ColumnName != "生产线" ||
                    dt.Columns[9].ColumnName != "成本中心" ||
                    dt.Columns[10].ColumnName != "工厂"
                    )
                {
                    dt.Reset();
                    message = "导入文件的模版不正确，请更新模板再导入!";
                    success = false;
                }

                DataTable TempTable = new DataTable("TempTable");
                DataColumn TempColumn;
                DataRow TempRow;

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wo_nbr";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wo_lot";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.DateTime");
                TempColumn.ColumnName = "wo_rel_date_act";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wo_mark";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wo_line";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wo_ctr";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wo_domain";
                TempTable.Columns.Add(TempColumn);



                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "wo_error";
                TempTable.Columns.Add(TempColumn);



                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Int32");
                TempColumn.ColumnName = "wo_createdby";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.DateTime");
                TempColumn.ColumnName = "wo_createdDate";

                TempTable.Columns.Add(TempColumn);

                if (dt.Rows.Count > 0)
                {
                    string wo_nbr = string.Empty;
                    string wo_lot = string.Empty;
                    string wo_rel_date = string.Empty;
                    string wo_mark = string.Empty;
                    string wo_error = string.Empty;
                    string wo_createdBy = uId;
                    string wo_createdDate = DateTime.Now.ToString();
                    string wo_line = string.Empty;
                    string wo_ctr = string.Empty;

                    DateTime dateFormat = DateTime.Now;

                    //先清空临时表中该上传员工的记录
                    if (ClearTempTable(Convert.ToInt32(uId)))
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            if (dt.Rows[i].IsNull(0)) wo_nbr = "";
                            else wo_nbr = dt.Rows[i].ItemArray[0].ToString().Trim();

                            if (dt.Rows[i].IsNull(1)) wo_lot = "";
                            else wo_lot = dt.Rows[i].ItemArray[1].ToString().Trim();

                            if (dt.Rows[i].IsNull(2)) wo_rel_date = "";
                            else wo_rel_date = dt.Rows[i].ItemArray[2].ToString().Trim();


                            if (dt.Rows[i].IsNull(3)) wo_mark = "";
                            else wo_mark = dt.Rows[i].ItemArray[10].ToString().Trim();

                            if (dt.Rows[i].IsNull(4)) wo_line = "";
                            else wo_line = dt.Rows[i].ItemArray[8].ToString().Trim();

                            if (dt.Rows[i].IsNull(5)) wo_ctr = "";
                            else wo_ctr = dt.Rows[i].ItemArray[9].ToString().Trim();

                            wo_error = "";
                            TempRow = TempTable.NewRow();
                            if (wo_nbr.Length == 0 || wo_lot.Length == 0)
                            {
                                wo_error += "加工单、ID不能为空;";
                            }

                            if (wo_line.Length == 0)
                            {
                                wo_error += "生产线不能为空;";
                            }

                           
                            if (wo_mark.Length != 0 && wo_mark != "删除")
                            {
                                wo_mark=string.Empty;
                            }


                            TempRow["wo_nbr"] = wo_nbr;
                            TempRow["wo_lot"] = wo_lot;
                            TempRow["wo_mark"] = wo_mark;
                            TempRow["wo_domain"] = plant;
                            TempRow["wo_line"] = wo_line;
                            TempRow["wo_ctr"] = wo_ctr;
                            TempRow["wo_createdby"] = wo_createdBy;
                            TempRow["wo_createdDate"] = wo_createdDate;
                            TempRow["wo_error"] = wo_error;

                            TempTable.Rows.Add(TempRow);
                        }
                        //TempTable有数据的情况下批量复制到数据库里
                        if (TempTable != null && TempTable.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopy.DestinationTableName = "wo_actRel_Temp";

                                bulkCopy.ColumnMappings.Clear();

                                bulkCopy.ColumnMappings.Add("wo_nbr", "wo_nbr");
                                bulkCopy.ColumnMappings.Add("wo_lot", "wo_lot");
                                bulkCopy.ColumnMappings.Add("wo_rel_date_act", "wo_rel_date_act");

                                bulkCopy.ColumnMappings.Add("wo_mark", "wo_mark");
                                bulkCopy.ColumnMappings.Add("wo_line", "wo_line");
                                bulkCopy.ColumnMappings.Add("wo_ctr", "wo_ctr");
                                bulkCopy.ColumnMappings.Add("wo_domain", "wo_domain");
                                bulkCopy.ColumnMappings.Add("wo_error", "wo_error");
                                bulkCopy.ColumnMappings.Add("wo_createdby", "wo_createdby");
                                bulkCopy.ColumnMappings.Add("wo_createdDate", "wo_createdDate");

                                try
                                {
                                    bulkCopy.WriteToServer(TempTable);
                                }
                                catch (Exception ex)
                                {
                                    message = "导入时出错，请联系系统管理员A！";
                                    success = false;
                                }
                                finally
                                {
                                    TempTable.Dispose();
                                    bulkCopy.Close();
                                }
                            }
                        }
                        dt.Reset();
                        if (success)
                        {
                            //数据库端验证
                            if (CheckTempTable(Convert.ToInt32(uId)))
                            {
                                //判断上传内容能否通过验证
                                if (JudgeTempTable(Convert.ToInt32(uId)))
                                {
                                    if (TransTempTable(Convert.ToInt32(uId)))
                                    {
                                        message = "导入文件成功";
                                        success = true;
                                    }
                                    else
                                    {
                                        message = "导入时出错，请联系管理员C!";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    message = "导入文件结束，有错误!";
                                    success = false;
                                }
                            }
                            else
                            {
                                message = "导入时出错，请联系管理员B!";
                                success = false;
                            }
                        }
                    }
                }
            }
            catch
            {
                message = "导入文件失败!";
                success = false;
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
        return success;
    }
    private bool ClearTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_deleteWoActRelTemp", param));
        }
        catch
        {
            return false;
        }
    }
    private bool CheckTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_checkWoActRelTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 判断临时表中该上传者能否通过
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private bool JudgeTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_judgeWoActRelTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 将大订单临时表更新到正式表里
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private bool TransTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_updateWoActRelLine", param));
        }
        catch
        {
            return false;
        }
    }
 
}