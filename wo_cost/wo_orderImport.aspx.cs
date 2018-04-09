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

public partial class wo_cost_wo_orderImport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_wo_clearOrderTemp", param);

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

                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "地点")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 地点！');";
                            return false;
                        }
                        else if (col == 1 && dt.Columns[col].ColumnName.Trim() != "提出部门")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 提出部门！');";
                            return false;
                        }
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "加工部门")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 加工部门！');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "承担部门")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 承担部门！');";
                            return false;
                        }
                        else if (col == 4 && dt.Columns[col].ColumnName.Trim() != "承担供应商")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 承担供应商！');";
                            return false;
                        }
                        else if (col == 5 && dt.Columns[col].ColumnName.Trim() != "加工单号")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 加工单号！');";
                            return false;
                        }
                        else if (col == 6 && dt.Columns[col].ColumnName.Trim() != "类型")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 类型！');";
                            return false;
                        }
                        else if (col == 7 && dt.Columns[col].ColumnName.ToLower().Trim() != "数量")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 数量！');";
                            return false;
                        }
                        else if (col == 8 && dt.Columns[col].ColumnName.ToLower().Trim() != "工艺要求")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 工艺要求！');";
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
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "site";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cc_from";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cc_to";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "nbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "type";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qty";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "req";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cc_duty";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "supplier";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cc_from_n";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cc_to_n";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cc_duty_n";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "supplier_n";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "rtfile";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createdDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createdName";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);
                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);
                    string _uName = Session["uName"].ToString();
                    string error = string.Empty;
                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定

                            if (r["地点"].ToString().Trim().Length == 0)
                            {
                                error += "地点不能为空";
                            }
                            row["site"] = r["地点"].ToString().Trim();


                            if (r["提出部门"].ToString().Trim().Length == 0)
                            {


                                error += "提出部门不能为空";

                            }

                            row["cc_from"] = r["提出部门"].ToString().Trim();

                            if (r["加工部门"].ToString().Trim().Length == 0)
                            {

                                error += "加工部门不能为空";


                            }

                            row["cc_to"] = r["加工部门"].ToString().Trim();


                            if (r["加工单号"].ToString().Trim().Length == 0)
                            {

                                error += "加工单号不能为空";

                            }
                            else if (r["加工单号"].ToString().Trim().Length > 12)
                            {
                                error += "加工单号不能超过12位";
                            }
                            else
                            {
                                row["nbr"] = r["加工单号"].ToString().Trim();
                            }
                            if (r["加工单号"].ToString().Trim().Length == 0)
                            {

                                error += "加工单号不能为空";

                            }

                            if (r["数量"].ToString().Trim().Length == 0)
                            {

                                error += "数量不能为空";

                            }
                            else
                            {
                                try
                                {
                                    int num = Convert.ToInt32(r["数量"].ToString().Trim());
                                   
                                }
                                catch (Exception)
                                {

                                    error += "数量必须为整数";
                                }
                            }
                            row["qty"] = r["数量"].ToString().Trim();
                            row["nbr"] = r["加工单号"].ToString().Trim();


                            row["type"] = "A";



                            #endregion
                            row["req"] = r["工艺要求"].ToString().Trim();
                            row["createdBy"] = _uID;
                            row["createdName"] = _uName;
                            row["cc_duty"] = r["承担部门"].ToString().Trim();
                            row["supplier"] = r["承担供应商"].ToString().Trim();
                            row["cc_from_n"] = r["提出部门"].ToString().Trim();
                            row["cc_to_n"] = r["加工部门"].ToString().Trim();
                            row["cc_duty_n"] = r["承担部门"].ToString().Trim();
                            row["supplier_n"] = r["承担供应商"].ToString().Trim();
                            row["rtfile"] = string.Empty;


                            row["errMsg"] = error;

                            table.Rows.Add(row);
                        }


                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsnx()))
                            {
                                bulkCopy.DestinationTableName = "dbo.wo_order_temp";
                                bulkCopy.ColumnMappings.Add("site", "wot_site");
                                bulkCopy.ColumnMappings.Add("cc_from", "wot_cc_from");
                                bulkCopy.ColumnMappings.Add("cc_to", "wot_cc_to");
                                bulkCopy.ColumnMappings.Add("nbr", "wot_nbr");
                                bulkCopy.ColumnMappings.Add("type", "wot_type");
                                bulkCopy.ColumnMappings.Add("qty", "wot_qty");
                                bulkCopy.ColumnMappings.Add("req", "wot_req");
                                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                                bulkCopy.ColumnMappings.Add("cc_duty", "wot_cc_duty");
                                bulkCopy.ColumnMappings.Add("supplier", "wot_supplier");
                                bulkCopy.ColumnMappings.Add("cc_from_n", "wot_cc_from_n");
                                bulkCopy.ColumnMappings.Add("cc_to_n", "wot_cc_to_n");
                                bulkCopy.ColumnMappings.Add("cc_duty_n", "wot_cc_duty_n");
                                bulkCopy.ColumnMappings.Add("supplier_n", "wot_supplier_n");
                                bulkCopy.ColumnMappings.Add("rtfile", "wot_rtfile");
                                bulkCopy.ColumnMappings.Add("createdName", "createdName");
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
    protected bool CheckValidity(string uID)
    {
        try
        {
            string strSql = "sp_Order_check";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[1].Value);
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

            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_Order_Insert", sqlParam);

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
            if (!CheckValidity(Session["uID"].ToString()))
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
            else
            {
                string title = "120^<b>地点</b>~^120^<b>提出部门</b>~^120^<b>加工部门</b>~^80^<b>承担部门</b>~^80^<b>承担供应商</b>~^100^<b>加工单号</b>~^100^<b>类型</b>~^50^<b>数量</b>~^80^<b>工艺要求</b>~^500^<b>错误信息</b>~^";

                string sql = " select wot_site,wot_cc_from,wot_cc_to,wot_cc_duty,wot_supplier,wot_nbr,wot_type,wot_qty,wot_req,errMsg from wo_order_temp Where createdBy='" + Convert.ToInt32(Session["uID"]) + "' ";

                DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, sql).Tables[0];
               
                ExportExcel(title, dt, false);
                //ltlAlert.Text = "alert('导入失败!');";
            }
        }
    }
}