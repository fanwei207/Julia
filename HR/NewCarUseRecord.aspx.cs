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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;

public partial class HR_NewCarUseRecord : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPlant.SelectedValue = Session["PlantCode"].ToString();
        }
    }
    private void BindGvCarUsesReacord(string TruckLoading)
    {
        DataTable dt = SelectCarUsesReacord(TruckLoading);
        gvCarUseReacord.DataSource = dt;
        gvCarUseReacord.DataBind();
    }
    private DataTable SelectCarUsesReacord(string TruckLoading)
    {
        string str = "sp_carRecord_SelectCarUsesReacord";
        
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@UsesDate",txtUsrDate.Text);
        param[1] = new SqlParameter("@CarNumber",txtCarNumner.Text);
        param[2] = new SqlParameter("@DepartureTime",txtDepartureTime.Text);
        param[3] = new SqlParameter("@PlaceOfDeparture",txtPlaceOfDeparture.Text);
        param[4] = new SqlParameter("@ArrivalTime",txtArrivalTime.Text);
        param[5] = new SqlParameter("@Destination",txtdestination.Text);
        param[6] = new SqlParameter("@TruckLoading", TruckLoading);
        param[7] = new SqlParameter("@plant", ddlPlant.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string TruckLoading = "4";
        TruckLoading = ddlTruckLoading.SelectedValue.ToString();
        //if (rbTruckLoading1.Checked)
        //{
        //    TruckLoading = 0;
        //}
        //else if (rbTruckLoading2.Checked)
        //{
        //    TruckLoading = 1;
        //}
        //else if (rbTruckLoading3.Checked)
        //{
        //    TruckLoading = 2;
        //}
        //else if (rbTruckLoading4.Checked)
        //{
        //    TruckLoading = 4;
        //}
        BindGvCarUsesReacord(TruckLoading);
    }
    protected void gvCarUseReacord_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvCarUseReacord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gvCarUseReacord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 0)
            {
                e.Row.Cells[9].Text = "空车";
            }
            else if (Convert.ToInt32(gvCarUseReacord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 1)
            {
                e.Row.Cells[9].Text = "满载";
            }
            else if (Convert.ToInt32(gvCarUseReacord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 2)
            {
                e.Row.Cells[9].Text = "未满载";
            }
            else if (Convert.ToInt32(gvCarUseReacord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 3)
            {
                e.Row.Cells[9].Text = "满员";
            }
            else if (Convert.ToInt32(gvCarUseReacord.DataKeys[e.Row.RowIndex].Values["TruckLoading"]) == 4)
            {
                e.Row.Cells[9].Text = "未满员";
            }
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        #region 验证数据
        if (txtUsrDate.Text == string.Empty)
        {
            this.Alert("日期不能为空！");
            return;
        }
        else if (txtDepartureTime.Text == string.Empty)
        {
            this.Alert("出发时间不能为空！");
            return;
        }
        else if (txtPlaceOfDeparture.Text == string.Empty)
        {
            this.Alert("出发地不能为空！");
            return;
        }
        else if (txtCarNumner.Text == string.Empty)
        {
            this.Alert("车号不能为空！");
            return;
        }
        else if (txtArrivalTime.Text == string.Empty)
        {
            this.Alert("到达时间不能为空！");
            return;
        }
        else if (txtdestination.Text == string.Empty)
        {
            this.Alert("到达地不能为空！");
            return;
        }
        else if (txtUses.Text == string.Empty)
        {
            this.Alert("用途不能为空！");
            return;
        }
        #endregion
        string TruckLoading = "0";
        TruckLoading = ddlTruckLoading.SelectedValue.ToString();
        if (TruckLoading == "4")
        {
            TruckLoading = "0";
        }
        //if (rbTruckLoading1.Checked)
        //{
        //    TruckLoading = 0;
        //}
        //else if (rbTruckLoading2.Checked)
        //{
        //    TruckLoading = 1;
        //}
        //else if (rbTruckLoading3.Checked)
        //{
        //    TruckLoading = 2;
        //}
        //else if (rbTruckLoading4.Checked)
        //{
        //    TruckLoading = 0;
        //}
        if (InsertCarUsesRecord(TruckLoading))
        {
            this.Alert("新增失败，请联系管理员！");
            return;
        }
        else
        {
            BindGvCarUsesReacord("0");
        }
    }
    /// <summary>
    /// 新增用车记录
    /// </summary>
    /// <param name="TruckLoading"></param>
    /// <returns></returns>
    private bool InsertCarUsesRecord(string  TruckLoading)
    {
        string str = "sp_carRecord_InsertCarUsesReacord";

        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@UsesDate", txtUsrDate.Text);
        param[1] = new SqlParameter("@CarNumber", txtCarNumner.Text);
        param[2] = new SqlParameter("@DepartureTime", txtDepartureTime.Text);
        param[3] = new SqlParameter("@PlaceOfDeparture", txtPlaceOfDeparture.Text);
        param[4] = new SqlParameter("@ArrivalTime", txtArrivalTime.Text);
        param[5] = new SqlParameter("@Destination", txtdestination.Text);
        param[6] = new SqlParameter("@TruckLoading", TruckLoading);
        param[7] = new SqlParameter("@Uses", txtUses.Text);
        param[8] = new SqlParameter("@uID", Session["uID"].ToString());
        param[9] = new SqlParameter("@uName", Session["uName"].ToString());
        param[10] = new SqlParameter("@PlantCode", Session["PlantCode"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(),CommandType.StoredProcedure, str, param));
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (UpLoadFile.Value.Trim() != string.Empty)
        {
            try
            {
                if (!ImportExcelFile())
                {
                    this.Alert("批量导入失败！请联系管理员！");
                    return;
                }
                else
                {
                    ValidateAndInsertCarUsesReacord();
                    if (!CheckCarUsesRecordError())
                    {
                        this.Alert("有错误数据！");
                        //下载错误数据模板
                        ExcelError(Session["uID"].ToString());
                    }
                    BindGvCarUsesReacord("0");
                }

            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!')";
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择文件!!')";
        }
    }
    /// <summary>
    /// 导出有错误的Excel数据
    /// </summary>
    /// <param name="pageid"></param>
    /// <param name="uid"></param>
    private void ExcelError(string uid)
    {
        string EXTitle = string.Empty;
        DataTable dtExcel = new DataTable("temp");
        DataTable dtExcelerror = SelectCarUsesRecordError();
        EXTitle = "100^<b>公司（SQL、ZQL、YQL、HQL）</b>~^100^<b>日期</b>~^100^<b>车号</b>~^100^<b>用途</b>~^100^<b>出发时间</b>~^100^<b>出发地</b>~^100^<b>到达时间</b>~^100^<b>到达地</b>~^100^<b>货车装载情况(0-空车，1-满载，2-没有满载）</b>~^200^<b>错误</b>~^";

        this.ExportExcel(EXTitle, dtExcelerror, false);
        //DataTable dtImportField = PageMakerHelper.GetImportField(hidPageID.Value);
        //foreach (DataRow row in dtImportField.Rows)
        //{
        //    EXTitle += "100^<b>" + row["pd_label"].ToString() + "</b>~^";
        //}
        //if (dtImportField.Rows.Count > 0)
        //{
        //    EXTitle += "200^<b>错误提示</b>~^";
        //    this.ExportExcel(EXTitle, dtExcelerror, false);
        //}
        //else
        //{
        //    this.Alert("无错误数据，请联系管理员！");
        //    return;
        //}
    }
    protected bool ImportExcelFile()
    {
        #region   判断是否选择文件，以及选择的是否是excel文件
        if (UpLoadFile.Value.Trim() == string.Empty)
        {
            this.Alert("请选择要导入的Excel文件！");
            return false;
        }
        string excelFileSuffix = UpLoadFile.Value.Trim().Substring(UpLoadFile.Value.Trim().LastIndexOf('.') + 1);
        if (excelFileSuffix != "xls" && excelFileSuffix != "xlsx")
        {
            this.Alert("请选择Excel文件！");
            return false;
        }
        #endregion
        #region       检查Excel文件大小，创建Excel文件服务器目录，上传Excel文件至服务器
        string strServerPath = Server.MapPath("/import");       /*服务器存放Excel文件的路径*/
        string strPostedFileName = UpLoadFile.PostedFile.FileName.Trim();      /*Excel文件的名字*/
        string strFileName = strPostedFileName.Substring(strPostedFileName.LastIndexOf('\\') + 1).Trim();
        if (UpLoadFile.PostedFile.ContentLength > 5 * 1024 * 1024)
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
            UpLoadFile.PostedFile.SaveAs(strFullName);
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
            //DataTable dtImportField = PageMakerHelper.GetImportField(hidPageID.Value);
            //DataTable dtPageMstr = PageMakerHelper.GetPageMstr(hidPageID.Value);

            if (dtblExcel.Rows.Count > 0)
            {
                #region 验证excel模板是否正确
                if (dtblExcel.Columns.Count != 8)
                {
                    this.Alert("Excel列数与模板列数不相同，请重新下载模板！");
                    return false;
                }
                if (dtblExcel.Columns[0].ColumnName.ToString() != "日期" &&
                    dtblExcel.Columns[1].ColumnName.ToString() != "车号" &&
                    dtblExcel.Columns[2].ColumnName.ToString() != "用途" &&
                    dtblExcel.Columns[3].ColumnName.ToString() != "出发时间" &&
                    dtblExcel.Columns[4].ColumnName.ToString() != "出发地" &&
                    dtblExcel.Columns[5].ColumnName.ToString() != "到达时间" &&
                    dtblExcel.Columns[6].ColumnName.ToString() != "到达地" &&
                    dtblExcel.Columns[7].ColumnName.ToString() != "货车装载情况(0-空车，1-满载，2-没有满载）") //判断模板是否正确
                {
                    this.Alert("Excel列跟模板列不对应，请重新下载新模板！");
                    return false;
                }
                #endregion

                #region 创建临时表
                DataTable table = new DataTable("CarUseRecord_temp");
                DataColumn column;
                DataRow row;

                //添加Guid列
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Plant";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "UsesDate";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "CarNumber";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Uses";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "DepartureTime";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "ArrivalTime";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "PlaceOfDeparture";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Destination";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "TruckLoading";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "createBy";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "createName";
                table.Columns.Add(column);
                #endregion

                if (!ClearImportTemp())
                {
                    string PlantCode = Session["PlantCode"].ToString();    
                    if (PlantCode == "1")
                    {
                        PlantCode = "SQL";
                    }
                    else if (PlantCode == "2")
                    {
                        PlantCode = "ZQL";
                    }
                    else if (PlantCode == "5")
                    {
                        PlantCode = "YQL";
                    }
                    else if (PlantCode == "8")
                    {
                        PlantCode = "HQL";
                    }
                    foreach (DataRow rw in dtblExcel.Rows)
                    {
                        row = table.NewRow();
                        //Guid 方便对临时表的操作
                        row["Plant"] = PlantCode;
                        row["UsesDate"] = rw[0].ToString();
                        row["CarNumber"] = rw[1].ToString();
                        row["Uses"] = rw[2].ToString();
                        row["DepartureTime"] = rw[3].ToString();
                        row["ArrivalTime"] = rw[5].ToString();
                        row["PlaceOfDeparture"] = rw[4].ToString();
                        row["Destination"] = rw[6].ToString();
                        row["TruckLoading"] = rw[7].ToString();
                        row["createBy"] = Session["uID"].ToString();
                        row["createName"] = Session["uName"].ToString();

                        table.Rows.Add(row);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopy.DestinationTableName = "tcpc0.dbo.CarUseRecord_temp";

                            bulkCopy.ColumnMappings.Add("Plant", "Plant");
                            bulkCopy.ColumnMappings.Add("UsesDate", "UsesDate");
                            bulkCopy.ColumnMappings.Add("CarNumber", "CarNumber");
                            bulkCopy.ColumnMappings.Add("Uses", "Uses");
                            bulkCopy.ColumnMappings.Add("DepartureTime", "DepartureTime");
                            bulkCopy.ColumnMappings.Add("ArrivalTime", "ArrivalTime");
                            bulkCopy.ColumnMappings.Add("PlaceOfDeparture", "PlaceOfDeparture");
                            bulkCopy.ColumnMappings.Add("Destination", "Destination");
                            bulkCopy.ColumnMappings.Add("TruckLoading", "TruckLoading");
                            bulkCopy.ColumnMappings.Add("createBy", "createBy");
                            bulkCopy.ColumnMappings.Add("createName", "createName");
                            try
                            {
                                bulkCopy.WriteToServer(table);
                                #region 同步数据
                                
                                #endregion
                                //#region 同步数据
                                //if (PageMakerHelper.ExcelValidate(hidPageID.Value, Session["uID"].ToString()))
                                //{
                                //    string ImportProc = string.Empty;
                                //    DataTable dt = PageMakerHelper.GetPageMstr(hidPageID.Value);
                                //    foreach (DataRow rows in dt.Rows)
                                //    {
                                //        ImportProc = rows["pm_importProc"].ToString();
                                //    }
                                //    if (string.IsNullOrEmpty(ImportProc))
                                //    {
                                //        if (PageMakerHelper.ExcelToTable(hidPageID.Value, Session["uID"].ToString()))
                                //        {
                                //            return true;
                                //        }
                                //        else
                                //        {
                                //            return false;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        if (PageMakerHelper.ExcelToTableBySelf(hidPageID.Value, Session["uID"].ToString(), ImportProc))
                                //        {
                                //            return true;
                                //        }
                                //        else
                                //        {
                                //            return false;
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    return false;
                                //}
                                //#endregion
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
    private bool ClearImportTemp()
    {
        string sql = "delete from CarUseRecord_temp Where createBy = " + Session["uID"].ToString();
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql));
    }
    private bool CheckCarUsesRecordError()
    {
        DataTable dt = SelectCarUsesRecordError();
        if (dt == null || dt.Rows.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private DataTable SelectCarUsesRecordError()
    {
        string sql = "select Plant,UsesDate,CarNumber,Uses,DepartureTime,PlaceOfDeparture,ArrivalTime,Destination,TruckLoading,error from CarUseRecord_temp where error is not null And createBy = " + Session["uID"].ToString();

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private void ValidateAndInsertCarUsesReacord()
    {
        string str = "sp_carRecord_ValidateAndInsertCarUsesReacord";

        SqlParameter param = new SqlParameter("@uID",Session["uID"].ToString());
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, str, param);
    }
    protected void gvCarUseReacord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //定义参数
        string ID = gvCarUseReacord.DataKeys[e.RowIndex].Values["id"].ToString();
        //写入历史表
        string str = "sp_carRecord_InsertCarUseRecordHis";
        string sql = "Delete From CarUseRecord Where id = '" + ID + "'";
        SqlParameter[] Param = new SqlParameter[10];
        Param[0] = new SqlParameter("@id", ID);
        Param[1] = new SqlParameter("@uID", Session["uID"].ToString());
        Param[2] = new SqlParameter("@uName", Session["uName"].ToString());

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, str, Param);
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, sql);

        BindGvCarUsesReacord("0");
    }
    protected void gvCarUseReacord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCarUseReacord.PageIndex = e.NewPageIndex;
        BindGvCarUsesReacord(ddlTruckLoading.SelectedValue.ToString());
    }
}