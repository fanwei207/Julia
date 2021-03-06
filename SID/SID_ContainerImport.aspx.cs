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
using System.Text.RegularExpressions;
using adamFuncs;
using CommClass;
using System.IO;
using QADSID;

public partial class SID_ContainerImport : BasePage
{
    adamClass adam = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "500^<b>错误信息</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);

        }
    }

    protected void BtnImport_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        ImportExcelFile();
    }

    public void ImportExcelFile()
    {
        String strSQL = "";
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;
        bool chkPass = true;
        bool boolError = false;

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

        strUserFileName = filename1.PostedFile.FileName;
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

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
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
                    dt = this.GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + e.ToString() + "'.');";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        string strShipNo = string.Empty;
                        string strContainer = string.Empty;
                        string strInvoiceNo = string.Empty;
                        i = 0;

                        strSQL = " Delete From ImportError Where userID = '" + Session["uID"] + "'";
                        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);


                        strSQL = " Delete From SID_mstr_temp Where SID_createdby = '" + Session["uID"] + "'";
                        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);

                        #region 按SID_mstr_temp和ImportError的结构构建表
                        DataColumn column;
                        DataRow rowError;//错误表的行
                        DataRow rowTemp;//临时表的行

                        //构建ImportError
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

                        //构建SID_det_temp
                        DataTable tblTemp = new DataTable("mstr_temp");

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "Invoice";
                        tblTemp.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "Container";
                        tblTemp.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "Nbr";
                        tblTemp.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.Int32");
                        column.ColumnName = "createdBy";
                        tblTemp.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.DateTime");
                        column.ColumnName = "createdDate";
                        tblTemp.Columns.Add(column);
                        #endregion

                        for (i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            if (!(dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1) && dt.Rows[i].IsNull(2)))
                            {
                                chkPass = true;

                                if (dt.Rows[i].IsNull(0)) strShipNo = "";
                                else strShipNo = dt.Rows[i].ItemArray[0].ToString().Trim();

                                if (dt.Rows[i].IsNull(1)) strContainer = "";
                                else strContainer = dt.Rows[i].ItemArray[1].ToString().Trim();

                                if (dt.Rows[i].IsNull(2)) strInvoiceNo = "";
                                else strInvoiceNo = dt.Rows[i].ItemArray[2].ToString().Trim();

                                if (strShipNo.Length == 0)
                                {
                                    ErrorRecord++;
                                    chkPass = false;

                                    rowError = tblError.NewRow();

                                    rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，出运单号不能为空！";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                    tblError.Rows.Add(rowError);

                                    //sid.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 出运单号不能为空", Convert.ToInt32(Session["uID"]));
                                }

                                if (strContainer.Length == 0)
                                {
                                    ErrorRecord++;
                                    chkPass = false;

                                    rowError = tblError.NewRow();

                                    rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，集装箱号不能为空！";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                    tblError.Rows.Add(rowError);

                                    //sid.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 集装箱号不能为空", Convert.ToInt32(Session["uID"]));
                                }
                                else
                                {
                                    if (strContainer.Length > 20)
                                    {
                                        ErrorRecord++;
                                        chkPass = false;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，集装箱号长度超过20！";
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);

                                        //sid.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 集装箱号长度超过20", Convert.ToInt32(Session["uID"]));
                                    }
                                }

                                if (strInvoiceNo.Length == 0)
                                {
                                    ErrorRecord++;
                                    chkPass = false;

                                    rowError = tblError.NewRow();

                                    rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，发票号不能为空！";
                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                    tblError.Rows.Add(rowError);

                                    //sid.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 发票号不能为空", Convert.ToInt32(Session["uID"]));
                                }
                                else
                                {
                                    if (strInvoiceNo.Length > 20)
                                    {
                                        ErrorRecord++;
                                        chkPass = false;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，发票号长度超过20！";
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);

                                        //sid.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 发票号长度超过20", Convert.ToInt32(Session["uID"]));
                                    }
                                }

                                //if (chkPass)
                                //{
                                //    strSQL = " Update SID_Mstr Set SID_Container = '" + strContainer + "', SID_Invoice ='" + strInvoiceNo + "' Where SID_nbr = '" + strShipNo + "'";
                                //    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
                                //}
                                if (chkPass)
                                {
                                    rowTemp = tblTemp.NewRow();

                                    rowTemp["Invoice"] = strInvoiceNo;
                                    rowTemp["Container"] = strContainer;
                                    rowTemp["Nbr"] = strShipNo;
                                    rowTemp["createdBy"] = Convert.ToInt32(Session["uID"]);
                                    rowTemp["createdDate"] = DateTime.Now;

                                    tblTemp.Rows.Add(rowTemp);
                                }
                            }
                            else
                                break;
                        }

                        if (chkPass)
                        {
                            if (tblTemp != null && tblTemp.Rows.Count > 0)
                            {
                                using (SqlBulkCopy bulkCopyTemp = new SqlBulkCopy(adam.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                {
                                    bulkCopyTemp.DestinationTableName = "SID_mstr_temp";

                                    bulkCopyTemp.ColumnMappings.Clear();

                                    bulkCopyTemp.ColumnMappings.Add("Invoice", "SID_Invoice");
                                    bulkCopyTemp.ColumnMappings.Add("Container", "SID_Container");
                                    bulkCopyTemp.ColumnMappings.Add("Nbr", "SID_nbr");
                                    bulkCopyTemp.ColumnMappings.Add("createdBy", "SID_createdby");
                                    bulkCopyTemp.ColumnMappings.Add("createdDate", "SID_createddate");

                                    try
                                    {
                                        bulkCopyTemp.WriteToServer(tblTemp);
                                    }
                                    catch (Exception ex)
                                    {
                                        boolError = true;
                                    }
                                    finally
                                    {
                                        bulkCopyTemp.Close();
                                        tblTemp.Dispose();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (tblError != null && tblError.Rows.Count > 0)
                            {
                                using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(adam.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                {
                                    bulkCopyError.DestinationTableName = "ImportError";

                                    bulkCopyError.ColumnMappings.Clear();

                                    bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                                    bulkCopyError.ColumnMappings.Add("uID", "userID");
                                    bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                                    try
                                    {
                                        bulkCopyError.WriteToServer(tblError);
                                    }
                                    catch (Exception ex)
                                    {
                                        boolError = true;
                                    }
                                    finally
                                    {
                                        bulkCopyError.Close();
                                        tblError.Dispose();
                                    }
                                }
                            }

                        }
                    }
                    dt.Reset();

                    if (!boolError)
                    {
                        if (ErrorRecord == 0)
                        {
                            SqlParameter param = new SqlParameter("@uid", Session["uID"]);
                            boolError = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, "sp_SID_UpdateMstrInfo", param));

                            if (!boolError)
                            {
                                ltlAlert.Text = "alert('数据更新成功！'); window.location.href='/SID/SID_ContainerImport.aspx?rm=" + DateTime.Now.ToString() + "';";
                            }
                            else
                            {
                                ltlAlert.Text = "alert('数据更新失败，请重试！'); window.location.href='/SID/SID_ContainerImport.aspx?rm=" + DateTime.Now.ToString() + "';";
                            }
                        }
                        else
                        {
                            ltlAlert.Text = "alert('导入文件结束，有错误!'); window.location.href='/SID/SID_ContainerImport.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                            return;
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('导入错误,请联系系统管理员!'); window.location.href='/SID/SID_ContainerImport.aspx?rm=" + DateTime.Now.ToString() + "';";
                    }
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件失败!" + ex.Message + "');";
                    return;
                }

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }
    }
}