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
using System.IO;
using adamFuncs;

public partial class plan_bigOrderImport : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Security["44000320"].isValid)
            {
                tb2.Visible = false;
                gvPerson.Columns[3].Visible = false;
            }
            else
            {
                tb2.Visible = true;
                gvPerson.Columns[3].Visible = true;
            }


            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "100^<b>销售单</b>~^100^<b>行号</b>~^100^<b>加工单</b>~^100^<b>ID</b>~^100^<b>类型</b>~^100^<b>留样</b>~^100^<b>留样单</b>~^100^<b>计划日期</b>~^300^<b>原因</b>~^100^<b>操作</b>~^100^<b>原加工单</b>~^100^<b>原ID</b>~^100^<b>整箱与散货</b>~^100^<b>备注1</b>~^100^<b>备注2</b>~^500^<b>错误信息</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select bo_so, bo_line, wo_nbr, wo_lot, bo_type, case isDate(wo_plandate) when 0 then wo_plandate Else Convert(varchar(10), cast(wo_plandate as datetime), 126) End as wo_plandate, bo_reason, wo_mark, bo_Sample, bo_SampleNbr, wo_nbr_parent, wo_lot_parent, bo_undefine1, bo_undefine2, bo_undefine3, bo_error From tcpc0.dbo.bigOrder_temp Where bo_createdBy='" + Convert.ToInt32(Session["uID"]) + "'" + " And bo_error <> ''";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx?ymd=a','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            ddlFileType.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            ddlFileType.Items.Add(item1);

            ddl_plant.SelectedValue = Session["PlantCode"].ToString();

            bindData();
        }
    }

    protected void uploadPartBtn_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ddlImportType.SelectedItem.Value.ToString() == "0")
        {
            ltlAlert.Text = "alert('请选择导入类型!');";
            return;
        }
        else
        {
            ImportExcelFile();
        }
        bindData();
    }

    public void ImportExcelFile()
    {
        String strSQL = "";
        //DataSet dst = new DataSet();
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;

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
                    //dst = chk.getExcelContents(strFileName);
                    dt = this.GetExcelContents(strFileName);
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (ddlImportType.SelectedItem.Value.ToString().Trim() == "1")
                {
                    try
                    {
                        if (dt.Columns[0].ColumnName != "销售单" &&
                            dt.Columns[1].ColumnName != "行号" &&
                            dt.Columns[2].ColumnName != "加工单" &&
                            dt.Columns[3].ColumnName != "ID" &&
                            dt.Columns[4].ColumnName != "类型" &&
                            dt.Columns[5].ColumnName != "计划日期" &&
                            dt.Columns[6].ColumnName != "原因" &&
                            dt.Columns[7].ColumnName != "操作" &&
                            dt.Columns[8].ColumnName != "留样(Y/N)" &&
                            dt.Columns[9].ColumnName != "留样单" &&
                            dt.Columns[10].ColumnName != "原加工单" &&
                            dt.Columns[11].ColumnName != "原ID" &&
                            dt.Columns[12].ColumnName != "整箱与散货" &&
                            dt.Columns[13].ColumnName != "备注1" &&
                            dt.Columns[14].ColumnName != "备注2"

                            )
                        {
                            //dst.Reset();
                            ltlAlert.Text = "alert('导入文件的模版不正确，请更新模板再导入!');";
                            return;
                        }

                        #region//新建TempTable内存表
                        DataTable TempTable = new DataTable("TempTable");
                        DataColumn TempColumn;
                        DataRow TempRow;

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_so";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_line";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_nbr";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_lot";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_type";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_plandate";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_reason";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_mark";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_nbr_parent";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_lot_parent";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.Int32");
                        TempColumn.ColumnName = "bo_status";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_domain";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine1";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine2";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine3";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_Sample";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_SampleNbr";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_error";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.Int32");
                        TempColumn.ColumnName = "bo_createdBy";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_createdName";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.DateTime");
                        TempColumn.ColumnName = "bo_createdDate";

                        TempTable.Columns.Add(TempColumn);
                        #endregion

                        if (dt.Rows.Count > 0)
                        {
                            string bo_so = string.Empty;
                            string bo_line = string.Empty;
                            string wo_nbr = string.Empty;
                            string wo_lot = string.Empty;
                            string bo_type = string.Empty;
                            string wo_plandate = string.Empty;
                            string bo_reason = string.Empty;
                            string wo_mark = string.Empty;
                            string wo_nbr_par = string.Empty;
                            string wo_lot_par = string.Empty;
                            int bo_status = 0;
                            string bo_domain = strPlant;
                            string bo_undefine1 = string.Empty;
                            string bo_undefine2 = string.Empty;
                            string bo_undefine3 = string.Empty;
                            bool bo_Sample = false;
                            string bo_SampleNbr = string.Empty;
                            string bo_error = string.Empty;
                            string bo_createdBy = strUID;
                            string bo_createdName = struName;
                            string bo_createdDate = DateTime.Now.ToString();

                            DateTime dateFormat = DateTime.Now;
                            int intFormat = 0;

                            //先清空临时表中该上传员工的记录
                            if (ClearTempTable(Convert.ToInt32(strUID)))
                            {
                                i = 0;
                                for (i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    if (dt.Rows[i].IsNull(0)) bo_so = "";
                                    else bo_so = dt.Rows[i].ItemArray[0].ToString().Trim();

                                    if (dt.Rows[i].IsNull(1)) bo_line = "";
                                    else bo_line = dt.Rows[i].ItemArray[1].ToString().Trim();

                                    if (dt.Rows[i].IsNull(2)) wo_nbr = "";
                                    else wo_nbr = dt.Rows[i].ItemArray[2].ToString().Trim();

                                    if (dt.Rows[i].IsNull(3)) wo_lot = "";
                                    else wo_lot = dt.Rows[i].ItemArray[3].ToString().Trim();

                                    if (dt.Rows[i].IsNull(4)) bo_type = "";
                                    else bo_type = dt.Rows[i].ItemArray[4].ToString().Trim();

                                    if (dt.Rows[i].IsNull(5)) wo_plandate = "";
                                    else wo_plandate = dt.Rows[i].ItemArray[5].ToString().Trim();

                                    if (dt.Rows[i].IsNull(6)) bo_reason = "";
                                    else bo_reason = dt.Rows[i].ItemArray[6].ToString().Trim();

                                    if (dt.Rows[i].IsNull(7)) wo_mark = "";
                                    else wo_mark = dt.Rows[i].ItemArray[7].ToString().Trim();

                                    if (dt.Rows[i].IsNull(13)) wo_nbr_par = "";
                                    else wo_nbr_par = dt.Rows[i].ItemArray[13].ToString().Trim();

                                    if (dt.Rows[i].IsNull(14)) wo_lot_par = "";
                                    else wo_lot_par = dt.Rows[i].ItemArray[14].ToString().Trim();

                                    if (dt.Rows[i].IsNull(10)) bo_undefine1 = "";
                                    else bo_undefine1 = dt.Rows[i].ItemArray[10].ToString().Trim();

                                    if (dt.Rows[i].IsNull(11)) bo_undefine2 = "";
                                    else bo_undefine2 = dt.Rows[i].ItemArray[11].ToString().Trim();

                                    if (dt.Rows[i].IsNull(12)) bo_undefine3 = "";
                                    else bo_undefine3 = dt.Rows[i].ItemArray[12].ToString().Trim();

                                    if (dt.Rows[i].IsNull(8)) bo_Sample = false;
                                    else bo_Sample = dt.Rows[i].ItemArray[8].ToString().Trim().ToUpper() == "Y" ? true : false;

                                    if (dt.Rows[i].IsNull(9)) bo_SampleNbr = "";
                                    else bo_SampleNbr = dt.Rows[i].ItemArray[9].ToString().Trim();

                                    bo_error = "";

                                    if (bo_so.Length == 0)
                                    {
                                        bo_error += "销售单不能为空;";
                                    }

                                    if (bo_line.Length == 0)
                                    {
                                        bo_error += "行号不能为空;";
                                    }
                                    else
                                    {
                                        try
                                        {
                                            intFormat = Convert.ToInt32(bo_line);
                                        }
                                        catch
                                        {
                                            bo_error += "行号必须为整数;";
                                        }
                                    }

                                    if (wo_nbr.Length == 0 || wo_lot.Length == 0)
                                    {
                                        bo_error += "加工单、ID不能为空;";
                                    }

                                    if ( wo_lot.Trim().Length > 8)
                                    {
                                        bo_error += "ID长度不能超过8位;";
                                    }

                                    if (bo_type.Length == 0)
                                    {
                                        bo_error += "类型不能为空;";
                                    }
                                    else
                                    {
                                        if (bo_type != "LED" && bo_type != "CFL")
                                        {
                                            bo_error += "类型只能为LED或者CFL;";
                                        }
                                    }

                                    if (wo_plandate.Length == 0)
                                    {
                                        bo_error += "计划日期不能为空;";
                                    }
                                    else
                                    {
                                        try
                                        {
                                            DateTime dtFormat = Convert.ToDateTime(wo_plandate);
                                        }
                                        catch
                                        {
                                            bo_error += "计划日期格式不正确;";
                                        }
                                    }

                                    if (bo_reason.Length > 100)
                                    {
                                        bo_error += "原因不能超过100文字;";
                                    }

                                    if (wo_mark.Length != 0 && wo_mark != "删除")
                                    {
                                        bo_error += "操作只能留空或者删除;";
                                    }

                                    TempRow = TempTable.NewRow();
                                    TempRow["bo_so"] = bo_so;
                                    TempRow["bo_line"] = bo_line;
                                    TempRow["wo_nbr"] = wo_nbr;
                                    TempRow["wo_lot"] = wo_lot;
                                    TempRow["bo_type"] = bo_type;
                                    TempRow["wo_plandate"] = wo_plandate;
                                    TempRow["bo_reason"] = (bo_reason == "" ? "" : bo_createdName + ":" + bo_reason);
                                    TempRow["wo_mark"] = wo_mark;
                                    TempRow["wo_nbr_parent"] = wo_nbr_par;
                                    TempRow["wo_lot_parent"] = wo_lot_par;
                                    TempRow["bo_status"] = bo_status;
                                    TempRow["bo_domain"] = strPlant;
                                    TempRow["bo_undefine1"] = bo_undefine1;
                                    TempRow["bo_undefine2"] = bo_undefine2;
                                    TempRow["bo_undefine3"] = bo_undefine3;
                                    TempRow["bo_Sample"] = bo_Sample;
                                    TempRow["bo_SampleNbr"] = bo_SampleNbr;
                                    TempRow["bo_error"] = bo_error;
                                    TempRow["bo_createdBy"] = bo_createdBy;
                                    TempRow["bo_createdName"] = bo_createdName;
                                    TempRow["bo_createdDate"] = bo_createdDate;

                                    TempTable.Rows.Add(TempRow);
                                }

                                //TempTable有数据的情况下批量复制到数据库里
                                if (TempTable != null && TempTable.Rows.Count > 0)
                                {
                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                    {
                                        bulkCopy.DestinationTableName = "bigOrder_temp";

                                        bulkCopy.ColumnMappings.Clear();

                                        bulkCopy.ColumnMappings.Add("bo_so", "bo_so");
                                        bulkCopy.ColumnMappings.Add("bo_line", "bo_line");
                                        bulkCopy.ColumnMappings.Add("wo_nbr", "wo_nbr");
                                        bulkCopy.ColumnMappings.Add("wo_lot", "wo_lot");
                                        bulkCopy.ColumnMappings.Add("bo_type", "bo_type");
                                        bulkCopy.ColumnMappings.Add("wo_plandate", "wo_plandate");
                                        bulkCopy.ColumnMappings.Add("bo_reason", "bo_reason");
                                        bulkCopy.ColumnMappings.Add("wo_mark", "wo_mark");
                                        bulkCopy.ColumnMappings.Add("wo_nbr_parent", "wo_nbr_parent");
                                        bulkCopy.ColumnMappings.Add("wo_lot_parent", "wo_lot_parent");
                                        bulkCopy.ColumnMappings.Add("bo_status", "bo_status");
                                        bulkCopy.ColumnMappings.Add("bo_domain", "bo_domain");
                                        bulkCopy.ColumnMappings.Add("bo_undefine1", "bo_undefine1");
                                        bulkCopy.ColumnMappings.Add("bo_undefine2", "bo_undefine2");
                                        bulkCopy.ColumnMappings.Add("bo_undefine3", "bo_undefine3");
                                        bulkCopy.ColumnMappings.Add("bo_Sample", "bo_Sample");
                                        bulkCopy.ColumnMappings.Add("bo_SampleNbr", "bo_SampleNbr");
                                        bulkCopy.ColumnMappings.Add("bo_error", "bo_error");
                                        bulkCopy.ColumnMappings.Add("bo_createdBy", "bo_createdBy");
                                        bulkCopy.ColumnMappings.Add("bo_createdName", "bo_createdName");
                                        bulkCopy.ColumnMappings.Add("bo_createdDate", "bo_createdDate");

                                        try
                                        {
                                            bulkCopy.WriteToServer(TempTable);
                                        }
                                        catch (Exception ex)
                                        {
                                            ltlAlert.Text = "alert('导入时出错，请联系系统管理员A！');";
                                            return;
                                        }
                                        finally
                                        {
                                            TempTable.Dispose();
                                            bulkCopy.Close();
                                        }
                                    }
                                }

                                //dst.Reset();

                                //数据库端验证
                                if (CheckTempTable(Convert.ToInt32(strUID)))
                                {
                                    //判断上传内容能否通过验证
                                    if (JudgeTempTable(Convert.ToInt32(strUID)))
                                    {
                                        if (TransTempTable(Convert.ToInt32(strUID)))
                                        {
                                            ltlAlert.Text = "alert('导入文件成功!'); window.location.href='/plan/bigOrder.aspx?rm=" + DateTime.Now.ToString() + "';";
                                        }
                                        else
                                        {
                                            ltlAlert.Text = "alert('导入时出错，请联系管理员C!');";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ltlAlert.Text = "alert('导入文件结束，有错误!'); window.location.href='/plan/bigOrderImport.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                                        return;
                                    }
                                }
                                else
                                {
                                    ltlAlert.Text = "alert('导入时出错，请联系管理员B!');";
                                    return;
                                }
                            }
                            else
                            {
                                ltlAlert.Text = "alert('清空临时表数据失败!');";
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('导入文件失败!');";
                        return;
                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }
                }
                else if (ddlImportType.SelectedItem.Value.ToString().Trim() == "2")
                {
                    try
                    {
                        if
                        (
                            dt.Columns[0].ColumnName != "销售单" &&
                            dt.Columns[1].ColumnName != "整箱与散货"
                        )
                        {
                            //dst.Reset();
                            ltlAlert.Text = "alert('导入文件的模版不正确，请更新模板再导入!');";
                            return;
                        }

                        #region//新建TempTable内存表
                        DataTable TempTable = new DataTable("TempTable");
                        DataColumn TempColumn;
                        DataRow TempRow;

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_so";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine1";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_error";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.Int32");
                        TempColumn.ColumnName = "bo_createdBy";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_createdName";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.DateTime");
                        TempColumn.ColumnName = "bo_createdDate";

                        TempTable.Columns.Add(TempColumn);
                        #endregion

                        if (dt.Rows.Count > 0)
                        {
                            string bo_so = string.Empty;
                            string bo_undefine1 = string.Empty;
                            string bo_error = string.Empty;
                            string bo_createdBy = strUID;
                            string bo_createdName = struName;
                            string bo_createdDate = DateTime.Now.ToString();

                            //先清空临时表中该上传员工的记录
                            if (ClearZSTempTable(Convert.ToInt32(strUID)))
                            {
                                i = 0;
                                for (i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    if (dt.Rows[i].IsNull(0)) bo_so = "";
                                    else bo_so = dt.Rows[i].ItemArray[0].ToString().Trim();

                                    if (dt.Rows[i].IsNull(1)) bo_undefine1 = "";
                                    else bo_undefine1 = dt.Rows[i].ItemArray[1].ToString().Trim();

                                    bo_error = "";

                                    if (bo_so.Length == 0)
                                    {
                                        bo_error += "销售单不能为空;";
                                    }

                                    if (bo_undefine1.Length == 0)
                                    {
                                        bo_error += "整箱与散货不能为空;";
                                    }

                                    TempRow = TempTable.NewRow();
                                    TempRow["bo_so"] = bo_so;
                                    TempRow["bo_undefine1"] = bo_undefine1;
                                    TempRow["bo_error"] = bo_error;
                                    TempRow["bo_createdBy"] = bo_createdBy;
                                    TempRow["bo_createdName"] = bo_createdName;
                                    TempRow["bo_createdDate"] = bo_createdDate;

                                    TempTable.Rows.Add(TempRow);
                                }

                                //TempTable有数据的情况下批量复制到数据库里
                                if (TempTable != null && TempTable.Rows.Count > 0)
                                {
                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                    {
                                        bulkCopy.DestinationTableName = "bigOrder_ZS";

                                        bulkCopy.ColumnMappings.Clear();

                                        bulkCopy.ColumnMappings.Add("bo_so", "bo_so");
                                        bulkCopy.ColumnMappings.Add("bo_undefine1", "bo_undefine1");
                                        bulkCopy.ColumnMappings.Add("bo_error", "bo_error");
                                        bulkCopy.ColumnMappings.Add("bo_createdBy", "bo_createdBy");
                                        bulkCopy.ColumnMappings.Add("bo_createdName", "bo_createdName");
                                        bulkCopy.ColumnMappings.Add("bo_createdDate", "bo_createdDate");

                                        try
                                        {
                                            bulkCopy.WriteToServer(TempTable);
                                        }
                                        catch (Exception ex)
                                        {
                                            ltlAlert.Text = "alert('导入时出错，请联系系统管理员A！');";
                                            return;
                                        }
                                        finally
                                        {
                                            TempTable.Dispose();
                                            bulkCopy.Close();
                                        }
                                    }
                                }

                                //dst.Reset();

                                //数据库端验证
                                if (CheckZSTempTable(Convert.ToInt32(strUID)))
                                {
                                    //判断上传内容能否通过验证
                                    if (JudgeZSTempTable(Convert.ToInt32(strUID)))
                                    {
                                        if (TransZSTempTable(Convert.ToInt32(strUID)))
                                        {
                                            ltlAlert.Text = "alert('导入文件成功!'); window.location.href='/plan/bigOrder.aspx?rm=" + DateTime.Now.ToString() + "';";
                                        }
                                        else
                                        {
                                            ltlAlert.Text = "alert('导入时出错，请联系管理员C!');";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ltlAlert.Text = "alert('导入文件结束，有错误!'); window.location.href='/plan/bigOrderImport.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                                        return;
                                    }
                                }
                                else
                                {
                                    ltlAlert.Text = "alert('导入时出错，请联系管理员B!');";
                                    return;
                                }
                            }
                            else
                            {
                                ltlAlert.Text = "alert('清空临时表数据失败!');";
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('导入文件失败!');";
                        return;
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
    }

    /// <summary>
    /// 清空大订单临时表中上传者所有记录
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private bool ClearTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteBoTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summmary>
    /// 数据库端对临时表进行检查
    /// </summmary>
    private bool CheckTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkBoTemp1", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 判断大订单临时表中该上传者能否通过
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private bool JudgeTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_judgeBoTemp", param));
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

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_insertBo1", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 显示大订单的审批人员
    /// </summary>
    /// <param name="bo_domain">域</param>
    private DataTable GetBoPerson()
    {
        try
        {
            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_selectBoPerson").Tables[0];
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 添加大订单的审批人员
    /// </summary>
    /// <param name="plantcode">plantcode</param>
    /// <param name="userNo">userNo</param>
    private bool AddBoPerson(int plantcode, string userNo)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@plantcode", plantcode);
            param[1] = new SqlParameter("@userNo", userNo);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_insertBoPerson", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 删除大订单的审批人员
    /// </summary>
    /// <param name="bo_id">ID</param>
    private bool DeleteBoPerson(int bo_id)
    {
        try
        {
            SqlParameter param = new SqlParameter("@bo_id", bo_id);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteBoPerson", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 绑定大订单的审批人员
    /// </summary>
    private void bindData()
    {
        DataTable dt = GetBoPerson();

        gvPerson.DataSource = dt;
        gvPerson.DataBind();
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txb_userno.Text.Length == 0)
        {
            ltlAlert.Text = "alert('工号不能为空!');";
            return;
        }
        else
        {
            if (AddBoPerson(Convert.ToInt32(ddl_plant.SelectedItem.Value), txb_userno.Text.Trim()))
            {
                bindData();
            }
            else
            {
                ltlAlert.Text = "alert('该人员不存在或者其它原因，添加失败，请联系管理员!');";
                return;
            }
        }
    }

    protected void gvPerson_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
    }

    protected void gvPerson_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeleteBoPerson(Convert.ToInt32(gvPerson.DataKeys[e.RowIndex].Value)))
        {
            bindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败,请联系管理员!');";
            return;
        }
    }

    /// <summmary>
    /// 数据库端对临时表进行检查
    /// </summmary>
    private bool CheckZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkZSBoTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 判断大订单临时表中该上传者能否通过
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private bool JudgeZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_judgeZSBoTemp", param));
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
    private bool TransZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_insertZSBo", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 清空大订单临时表中上传者所有记录
    /// </summary>
    /// <param name="createdBy">上传者的ID号</param>
    private bool ClearZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteZSBoTemp", param));
        }
        catch
        {
            return false;
        }
    }
}