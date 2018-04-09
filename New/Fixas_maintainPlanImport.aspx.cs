using System;
using System.Data;
using System.IO;
using Portal.Fixas;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class new_Fixas_maintainPlanImport : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ty"] == "error")
        {
            DataTable dt = FixasMaintainHelper.SelectFixasMaintainMstrTempError(Session["uID"].ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("保养计划导入错误");

                #region 设置列宽
                AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                col1.ColumnIndexStart = 0;
                col1.ColumnIndexEnd = 0;
                col1.Width = 120 * 6000 / 164;
                sheet.AddColumnInfo(col1);

                AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                col2.ColumnIndexStart = 1;
                col2.ColumnIndexEnd = 1;
                col2.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(col2);

                AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                col3.ColumnIndexStart = 2;
                col3.ColumnIndexEnd = 2;
                col3.Width = 200 * 6000 / 164;
                sheet.AddColumnInfo(col3);

                AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                col4.ColumnIndexStart = 3;
                col4.ColumnIndexEnd = 3;
                col4.Width = 350 * 6000 / 164;
                sheet.AddColumnInfo(col4);
                #endregion

                int rowIndex = 1;
                PrintExcel(doc, sheet, rowIndex, "资产编号", "计划保养日期", "保养描述", "错误描述");
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    PrintExcel(doc, sheet, rowIndex, row["plan_fixasNo"], row["plan_date"], row["plan_maintainDesc"], row["errorDesc"]);
                }

                doc.Send();
                Response.Flush();
                Response.End();
            }
            dt.Dispose();
        }
    }

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
            Object fixasNo, Object planDate, Object maintainDesc, Object errorDesc)
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

        sheet.Cells.Add(rowIndex, 1, fixasNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, planDate, xf);
        sheet.Cells.Add(rowIndex, 3, maintainDesc, xf);
        sheet.Cells.Add(rowIndex, 4, errorDesc, xf);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else if (ImportExcelFile())
        {
            int retValue = FixasMaintainHelper.CheckFixasMaintainMstrTempError(Session["uID"].ToString(), Convert.ToInt32(Session["plantCode"]));
            if (retValue == 1)
            {
                if (FixasMaintainHelper.ImportFixasMaintainMstrTemp(Session["uID"].ToString()))
                {
                    ltlAlert.Text = "window.location.href='/new/Fixas_maintainPlan.aspx?rt=" + DateTime.Now.ToString() + "'";
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!\\nFail to import.')";
                }
            }
            else if (retValue == -1)
            {
                Response.Redirect("/new/Fixas_maintainPlanImport.aspx?ty=error&rt=" + DateTime.Now.ToString());
            }
            else
            {
                ltlAlert.Text = "alert('导入异常，请联系管理员！')";
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
        string excelFileSuffix = excelFile.Value.Trim().Substring(excelFile.Value.Trim().LastIndexOf('.') + 1);
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

                if (dtblExcel.Columns[0].ColumnName != "资产编号" || dtblExcel.Columns[1].ColumnName != "计划保养日期" || dtblExcel.Columns[2].ColumnName != "保养描述")
                {
                    ltlAlert.Text = "alert('模版不正确！\\n请确认前三列是资产编号、计划保养日期、保养描述')";
                }

                #region 创建临时表
                DataTable table = new DataTable("temp");
                DataColumn column;
                DataRow row;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "plan_fixasNo";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "plan_date";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "plan_maintainDesc";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "plan_createdBy";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "plan_createdName";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.DateTime");
                column.ColumnName = "plan_createdDate";
                table.Columns.Add(column);
                #endregion

                if (FixasMaintainHelper.ClearFixasMaintainMstrTemp(Session["uID"].ToString()))
                {
                    foreach (DataRow rw in dtblExcel.Rows)
                    {
                        row = table.NewRow();
                        row["plan_fixasNo"] = rw[0].ToString();
                        row["plan_date"] = rw[1].ToString();
                        row["plan_maintainDesc"] = rw[2].ToString();
                        row["plan_createdBy"] = Session["uID"].ToString();
                        row["plan_createdName"] = Session["uName"].ToString();
                        row["plan_createdDate"] = DateTime.Now.ToString();
                        table.Rows.Add(row);
                    }
                    if (table != null && table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopy.DestinationTableName = "dbo.fixas_maintain_mstrTemp";
                            bulkCopy.ColumnMappings.Add("plan_fixasNo", "plan_fixasNo");
                            bulkCopy.ColumnMappings.Add("plan_date", "plan_date");
                            bulkCopy.ColumnMappings.Add("plan_maintainDesc", "plan_maintainDesc");
                            bulkCopy.ColumnMappings.Add("plan_createdBy", "plan_createdBy");
                            bulkCopy.ColumnMappings.Add("plan_createdName", "plan_createdName");
                            bulkCopy.ColumnMappings.Add("plan_createdDate", "plan_createdDate");

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
}