using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.Collections.Generic;
using System.Configuration;
using ProdApp;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QCProgress;
using IT;
using System.IO;
using System.Text;
using adamFuncs;
using System.Data.SqlClient;

public partial class IT_Page_ImportViewer : BasePage
{
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hidPageID.Value = Request["pageid"].ToString();
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            this.Alert("请重新登录！");
            return;
        }
        else if (ImportExcelFile())
        {
            this.Alert("批量导入成功！");
        }
        else
        {
            this.Alert("Excel表中的数据有错误！");
            ExcelError(hidPageID.Value,Session["uID"].ToString());
            return;
        }
    }

    protected bool ImportExcelFile()
    {
        #region   判断是否选择文件，以及选择的是否是excel文件
        if (fileExcel.Value.Trim() == string.Empty)
        {
            this.Alert("请选择要导入的Excel文件！");
            return false;
        }
        string excelFileSuffix = fileExcel.Value.Trim().Substring(fileExcel.Value.Trim().LastIndexOf('.') + 1);
        if (excelFileSuffix != "xls" && excelFileSuffix != "xlsx")
        {
            this.Alert("请选择Excel文件！");
            return false;
        }
        #endregion
        #region       检查Excel文件大小，创建Excel文件服务器目录，上传Excel文件至服务器
        string strServerPath = Server.MapPath("/import");       /*服务器存放Excel文件的路径*/
        string strPostedFileName = fileExcel.PostedFile.FileName.Trim();      /*Excel文件的名字*/
        string strFileName = strPostedFileName.Substring(strPostedFileName.LastIndexOf('\\') + 1).Trim();
        if (fileExcel.PostedFile.ContentLength > 5 * 1024 * 1024)
        {
            this.Alert("文件大小不能超过5 MB！");
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
                this.Alert("Excel文件上传失败！请联系管理员！\\nError：Fail to create server directory.");
                return false;
            }
        }

        //上传Excel文件至服务器
        string strFullName = strServerPath + "\\" + strFileName;
        try
        {
            fileExcel.PostedFile.SaveAs(strFullName);
        }
        catch
        {
            this.Alert("Excel文件上传失败！请联系管理员！\\nError：Fail to upload the excel file.");
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
                this.Alert("加载Excel数据失败！请稍后重试！\\nError：Fail to load the excel file.");
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
                        this.Alert("加载Excel数据失败！请稍后重试！\\nError：Fail to delete the excel file.");
                    }
                }
            }
            #endregion
            DataTable dtImportField = PageMakerHelper.GetImportField(hidPageID.Value);
            DataTable dtPageMstr = PageMakerHelper.GetPageMstr(hidPageID.Value);

            if (dtblExcel.Rows.Count > 0)
            {
                #region 验证excel模板是否正确
                if (dtblExcel.Columns.Count < dtImportField.Rows.Count)
                {
                    this.Alert("Excel列数少于模板列数，请重新下载模板！");
                    return false;
                }
                for (int c = 0; c < dtImportField.Rows.Count; c++)
                {
                    if (dtImportField.Rows[c]["pd_label"].ToString().ToLower() != dtblExcel.Columns[c].ColumnName.ToLower()) //判断字段的大小写
                    {
                        this.Alert("Excel列跟模板列不对应，请重新下载新模板！");
                        return false;
                    }
                }
                #endregion

                #region 创建临时表
                DataTable table = new DataTable("temp");
                DataColumn column;
                DataRow row;

                //添加Guid列
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Guid");
                column.ColumnName = "pageID";
                table.Columns.Add(column);


                for (int c = 1; c <= dtImportField.Rows.Count; c++)
                {
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = dtImportField.Rows[c - 1]["pd_colName"].ToString();
                    table.Columns.Add(column);
                }

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "uID";
                table.Columns.Add(column);


                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "uName";
                table.Columns.Add(column);

                #endregion

                if (PageMakerHelper.ClearImportTemp(hidPageID.Value, Session["uID"].ToString()))
                {
                    foreach (DataRow rw in dtblExcel.Rows)
                    {
                        row = table.NewRow();
                        //Guid 方便对临时表的操作
                        row["pageID"] = hidPageID.Value;
                        row["uID"] = Session["uID"].ToString();
                        row["uName"] = Session["uName"].ToString();
                        for (int c = 0; c < dtImportField.Rows.Count; c++)
                        {
                            row[dtImportField.Rows[c]["pd_colName"].ToString()] = rw[c].ToString();
                        }
                        table.Rows.Add(row);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn))
                        {
                            bulkCopy.DestinationTableName = "WorkFlow.dbo.Page_ImportTemp";

                            for (int c = 0; c < dtImportField.Rows.Count; c++)
                            {
                                bulkCopy.ColumnMappings.Add(dtImportField.Rows[c]["pd_colName"].ToString(), "col" + c);
                            }
                            bulkCopy.ColumnMappings.Add("pageID", "pageID");
                            bulkCopy.ColumnMappings.Add("uID", "uID");
                            bulkCopy.ColumnMappings.Add("uName", "uName");
                            try
                            {
                                bulkCopy.WriteToServer(table);
                                #region 验证数据
                                if (PageMakerHelper.ExcelValidate(hidPageID.Value, Session["uID"].ToString()))
                                {
                                    string ImportProc = string.Empty;
                                    DataTable dt = PageMakerHelper.GetPageMstr(hidPageID.Value);
                                    foreach (DataRow rows in dt.Rows)
                                    {
                                        ImportProc = rows["pm_importProc"].ToString();
                                    }
                                    if (string.IsNullOrEmpty(ImportProc))
                                    {
                                        if (PageMakerHelper.ExcelToTable(hidPageID.Value, Session["uID"].ToString()))
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        if (PageMakerHelper.ExcelToTableBySelf(hidPageID.Value, Session["uID"].ToString(), ImportProc))
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                this.Alert("表配置有误，请联系管理员！");
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
                    this.Alert("临时表数据清空失败！");
                    return false;
                }
            }
            else
            {
                this.Alert("Excel数据为空！");
                return false;            
            }
            dtblExcel.Dispose();
        }
        else
        {
            this.Alert("要加载的Excel文件不存在！请联系管理员！");
            return false;
        }
        return true;
    }
    protected void btnTemp_Click(object sender, EventArgs e)
    {
        string EXTitle = string.Empty;
        DataTable dtExcel = new DataTable("temp");
        DataColumn col;
        DataTable dtImportField = PageMakerHelper.GetImportField(hidPageID.Value);
        foreach (DataRow row in dtImportField.Rows)
        {
            EXTitle += "100^<b>" + row["pd_label"].ToString() + "</b>~^";
                      
            col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = row["pd_label"].ToString();
            dtExcel.Columns.Add(col);
        }
        if (dtImportField.Rows.Count > 0)
        {
            this.ExportExcel(EXTitle, dtExcel, false,ExcelVersion.Excel2003);
        }
        else
        {
            this.Alert("请先配置要导入的字段！");
            return;
        }        
    }


    /// <summary>
    /// 导出有错误的Excel数据
    /// </summary>
    /// <param name="pageid"></param>
    /// <param name="uid"></param>
    private void ExcelError(string pageid, string uid)
    {
        string EXTitle = string.Empty;
        DataTable dtExcel = new DataTable("temp");
        DataTable dtExcelerror = PageMakerHelper.GetExcelError(pageid,uid);
        DataTable dtImportField = PageMakerHelper.GetImportField(hidPageID.Value);
        foreach (DataRow row in dtImportField.Rows)
        {
            EXTitle += "100^<b>" + row["pd_label"].ToString() + "</b>~^";
        }
        if (dtImportField.Rows.Count > 0)
        {
            EXTitle += "200^<b>错误提示</b>~^";
            this.ExportExcel(EXTitle, dtExcelerror, false);
        }
        else
        {
            this.Alert("无错误数据，请联系管理员！");
            return;
        }      
    }
}