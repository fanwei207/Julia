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
using QADSID;
using System.Collections.Generic;


public partial class SID_shipimport1 : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);
        
        }
    }

    public Boolean ImportExcelFile1()
    {
        //String strSQL = "";
        DataSet ds = new DataSet();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        //Boolean boolError = false;
        int ErrorRecord = 0;


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

        strUserFileName = filename2.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return false;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27：唯一字符串可以设定为“年月日时分秒毫秒”
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        #region 按SID_det_temp和ImportError的结构构建表
        DataColumn column;

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
        DataTable tblTemp = new DataTable("checkdet_temp");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "nbr";
        tblTemp.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "so_nbr";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "so_line";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");//System.Type.GetType("System.DateTime");
        column.ColumnName = "finishedDate";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");//System.Type.GetType("System.DateTime");
        column.ColumnName = "checkedDate";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "createdby";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "createddate";
        tblTemp.Columns.Add(column);


        #endregion

        if (filename2.PostedFile != null)
        {
            if (filename2.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }

            try
            {
                filename2.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }



            if (File.Exists(strFileName))
            {
                DataRow rowError;//错误表的行
                DataRow rowTemp;//临时表的行
                string ext = ShareDocument.GetFileExtension(strFileName);
                if (ext!="208207")
                {
                    //ltlAlert.Text = "alert('导入文件必须是Excel格式或者模板及内容正确!');";
                    //return false;

                    ErrorRecord += 1;

                    rowError = tblError.NewRow();

                    rowError["errInfo"] = "导入文件必须是Excel格式或者模板及内容不正确!";
                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                    tblError.Rows.Add(rowError);
                    
                    if (tblError != null && tblError.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopyError.DestinationTableName = "dbo.ImportError";
                            bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                            bulkCopyError.ColumnMappings.Add("uID", "userID");
                            bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                            try
                            {
                                bulkCopyError.WriteToServer(tblError);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                                return false;
                            }
                            finally
                            {
                                bulkCopyError.Close();
                                tblError.Dispose();
                            }
                        }
                    }
                    return false;
                }
                // Get the WorkSheet Name
                String[] arrTable;
                arrTable = new string[20];

                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet." +
                               "OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + strFileName))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][2].ToString().Trim().Substring(dt.Rows[j][2].ToString().Trim().Length - 1, 1) == "$")
                        {
                            arrTable[j] = dt.Rows[j][2].ToString().Trim();
                        }

                    }
                    conn.Close();
                }

                foreach (string aa in arrTable)
                {
                    if (aa != null)
                    {
                        try
                        {
                            ds = sid.getExcelContents1(strFileName, aa.Replace("$", ""));
                        }
                        catch
                        {
                            if (File.Exists(strFileName))
                            {
                                File.Delete(strFileName);
                            }

                            ltlAlert.Text = "alert('导入文件必须是Excel格式或者模板及内容正确!');";
                            return false;
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Columns[0].ColumnName != "出运单号")
                            {
                                ds.Reset();
                                ltlAlert.Text = "alert('导入文件的模版不正确!');";
                                return false;
                            }




                            rowTemp = tblTemp.NewRow();

                            String _Nbr = "";
                            String _id = "";
                            String _So_nbr = "";
                            String _So_line = "";
                            String _FinishedDate = "";
                            String _CheckDate = "";
          

                            string strErrMsg = string.Empty;//前端的错误信息
                            ErrorRecord = 0;


                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                _Nbr = "";
                                _id = "";
                                _So_nbr = "";
                                _So_line = "";
                                _FinishedDate = "";
                                _CheckDate = "";

                                #region 导入计划确认日期
                                if (i >= 0)
                                {
                                    rowTemp = tblTemp.NewRow();

                                    //first three column is null, break
                                    if (ds.Tables[0].Rows[i].IsNull(0) && ds.Tables[0].Rows[i].IsNull(1) && ds.Tables[0].Rows[i].IsNull(2))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() == "" && ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim() == "" && ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim() == "")
                                        {
                                            break;
                                        }
                                    }

                                    //序号
                                    if (ds.Tables[0].Rows[i].IsNull(0))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "出运单号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Nbr = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                                    }

                                    //客户物料
                                    if (ds.Tables[0].Rows[i].IsNull(5))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "序号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _id = ds.Tables[0].Rows[i].ItemArray[5].ToString().Trim();
                                    }

                                    //销售订单
                                    if (ds.Tables[0].Rows[i].IsNull(12))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "销售订单不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _So_nbr = ds.Tables[0].Rows[i].ItemArray[12].ToString().Trim();
                                    }

                                    //行号
                                    if (ds.Tables[0].Rows[i].IsNull(13))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "行号不能为空,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _So_line = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[13]).ToString();
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "行号必须是数字,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //int useraccess = 0;
                                    //useraccess = sid.CheckImportDataUserAccess(Convert.ToInt32(Session["uID"]));


                                    //完工时间
                                    if (ds.Tables[0].Rows[i].IsNull(16) || string.IsNullOrEmpty(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()))
                                    {
                                        if (this.Security["550010021"].isValid)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "完工时间日期格式不正确,请将栏位设置为日期格式或文本格式，并输入例如“2014-09-12 13:30”,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (this.Security["550010021"].isValid)
                                            {
                                                string stryear = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("yyyy");
                                                string strmonth = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("MM");
                                                string strday = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("dd");
                                                string strhour = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("HH:mm");
                                                //string str35 = str33 + "/" + str31.Substring(2, 2) + "/" + str32 +  " " + str34;
                                                string str37 = stryear.Substring(2, 2) + "/" + strmonth + "/" + strday + " " + strhour;
                                                string str36 = Convert.ToDateTime(str37).ToString("yyyy/MM/dd HH:mm");
                                                if (str36.Substring(0, 2) != "20" || str36.Substring(4, 1) != "/" || Convert.ToInt32(str36.Substring(0, 4)) < 2014 || strhour == "00:00")
                                                {
                                                    ErrorRecord += 1;

                                                    rowError = tblError.NewRow();

                                                    rowError["errInfo"] = "完工时间格式不正确,请将日期栏位设置为日期格式或文本格式，并输入例如“2014-09-12 13:30”，表" + aa + "行" + Convert.ToString(i + 2);
                                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                    tblError.Rows.Add(rowError);
                                                }
                                                //Convert.ToDateTime(dts.Rows[i].ItemArray[8].ToString().Trim()).ToString();
                                                _FinishedDate = str36;//Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[15].ToString().Trim()).ToString();
                                            }
                                            else
                                            {
                                                _FinishedDate = null ;
                                            }
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "完工时间格式不正确,请将日期栏位设置为日期格式或文本格式，并输入例如“2014-09-12 13:30”，表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                            //ltlAlert.Text = "alert('日期格式不正确,请重新填写!');";
                                            //return false;
                                        }
                                    }
                                    //确认日期不得大于出运日期
                                    int Ierr1 = 0;
                                    if (_FinishedDate == null)
                                    {
                                        Ierr1 = 1;
                                    }
                                    else
                                    {
                                        Ierr1 = sid.CheckImportDataNotMax(_Nbr, _id, _So_nbr, _So_line, _FinishedDate);
                                    }
                                    if (Ierr1 < 0)
                                    {
                                        if (Ierr1 == -1)
                                        {
                                            //ErrorRecord += 1;

                                            //rowError = tblError.NewRow();

                                            //rowError["errInfo"] = "日期已确认不能重复导入,见表" + aa + "行" + Convert.ToString(i + 2);
                                            //rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            //rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            //tblError.Rows.Add(rowError);
                                        }
                                        else
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "完工时间不得大于出厂时间,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }

                                    }

                                    //货物抵达时间
                                    if (ds.Tables[0].Rows[i].IsNull(17) || string.IsNullOrEmpty(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()))
                                    {
                                        if (this.Security["550010022"].isValid)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "货物抵达时间格式不正确,请将栏位设置为日期格式或文本格式，并输入例如“2014-09-12 13:30”,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (this.Security["550010022"].isValid)
                                            {
                                                string str31 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("yyyy");
                                                string str32 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("MM");
                                                string str33 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("dd");
                                                string str34 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("HH:mm");
                                                //string str35 = str33 + "/" + str31.Substring(2, 2) + "/" + str32 +  " " + str34;
                                                string str37 = str31.Substring(2, 2) + "/" + str32 + "/" + str33 + " " + str34;
                                                string str36 = Convert.ToDateTime(str37).ToString("yyyy/MM/dd HH:mm");
                                                if (str36.Substring(0, 2) != "20" || str36.Substring(4, 1) != "/" || Convert.ToInt32(str36.Substring(0, 4)) < 2014 || str34 == "00:00")
                                                {
                                                    ErrorRecord += 1;

                                                    rowError = tblError.NewRow();

                                                    rowError["errInfo"] = "货物抵达时间格式不正确,请将栏位设置为日期格式或文本格式，并输入例如“2014-09-12 13:30”，表" + aa + "行" + Convert.ToString(i + 2);
                                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                    tblError.Rows.Add(rowError);
                                                }
                                                //Convert.ToDateTime(dts.Rows[i].ItemArray[8].ToString().Trim()).ToString();
                                                _CheckDate = str36;//Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[15].ToString().Trim()).ToString();
                                            }
                                            else
                                            {
                                                _CheckDate = null;
                                            }
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "货物抵达时间格式不正确,请将栏位设置为日期格式或文本格式，并输入例如“2014-09-12 13:30”，表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                            //ltlAlert.Text = "alert('日期格式不正确,请重新填写!');";
                                            //return false;
                                        }
                                        if (this.Security["550010021"].isValid && this.Security["550010022"].isValid && !string.IsNullOrEmpty(_FinishedDate) && !string.IsNullOrEmpty(_CheckDate))
                                        {
                                            if (Convert.ToDateTime(_FinishedDate) > Convert.ToDateTime(_CheckDate))
                                            {
                                                ErrorRecord += 1;

                                                rowError = tblError.NewRow();

                                                rowError["errInfo"] = "完工时间不得大于货物抵达时间”，表" + aa + "行" + Convert.ToString(i + 2);
                                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                tblError.Rows.Add(rowError);
                                            }
                                        }
                                    }

                                    //判断日期是否已维护
                                    int Ierr = 0;
                                    if (this.Security["550010022"].isValid)
                                    {
                                        Ierr = 1;
                                    }
                                    else
                                    {
                                        Ierr = sid.CheckImportDataExist(_Nbr, _id, _So_nbr, _So_line);
                                    }
                                    if (Ierr < 0)
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "日期已确认不可再导入,见表" + aa + "行" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);

                                    }

                                    //确认日期不得大于出运日期
                                    Ierr = 0;
                                    Ierr = sid.CheckImportDataNotMax(_Nbr, _id, _So_nbr, _So_line, _CheckDate);
                                    if (Ierr < 0)
                                    {
                                        if (Ierr == -1)
                                        {
                                            //ErrorRecord += 1;

                                            //rowError = tblError.NewRow();

                                            //rowError["errInfo"] = "日期已确认不能重复导入,见表" + aa + "行" + Convert.ToString(i + 2);
                                            //rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            //rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            //tblError.Rows.Add(rowError);
                                        }
                                        else
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "货物抵达时间不得大于出厂时间,见表" + aa + "行" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }

                                    }

                                    if (ErrorRecord <= 0)
                                    {
                                        rowTemp = tblTemp.NewRow();

                                        rowTemp["nbr"] = _Nbr;
                                        rowTemp["id"] = _id;
                                        rowTemp["so_nbr"] = _So_nbr;
                                        rowTemp["so_line"] = _So_line;
                                        rowTemp["finishedDate"] = _FinishedDate;
                                        rowTemp["checkedDate"] = _CheckDate;
                                        rowTemp["createdby"] = Convert.ToInt32(Session["uID"]);
                                        rowTemp["createddate"] = DateTime.Now;

                                        tblTemp.Rows.Add(rowTemp);
                                    }
                                }
                                #endregion
                            }
                        } //ds.Tables[0].Rows.Count > 0                           
                    } // a != null)
                    ds.Reset();
                } //foreach

                //上传
                if (tblError != null && tblError.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                    {
                        bulkCopyError.DestinationTableName = "dbo.ImportError";
                        bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                        bulkCopyError.ColumnMappings.Add("uID", "userID");
                        bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                        try
                        {
                            bulkCopyError.WriteToServer(tblError);
                        }
                        catch (Exception ex)
                        {
                            ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                            return false;
                        }
                        finally
                        {
                            bulkCopyError.Close();
                            tblError.Dispose();
                        }
                    }
                }

                if (tblTemp != null && tblTemp.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopyTemp = new SqlBulkCopy(chk.dsn0()))
                    {
                        bulkCopyTemp.DestinationTableName = "dbo.SID_checkdate_temp";
                        bulkCopyTemp.ColumnMappings.Add("nbr", "SID_nbr");
                        bulkCopyTemp.ColumnMappings.Add("id", "SID_id");
                        bulkCopyTemp.ColumnMappings.Add("so_nbr", "SID_so_nbr");
                        bulkCopyTemp.ColumnMappings.Add("so_line", "SID_so_line");
                        bulkCopyTemp.ColumnMappings.Add("finishedDate", "SID_finsheddate");
                        bulkCopyTemp.ColumnMappings.Add("checkedDate", "SID_checkeddate");
                        bulkCopyTemp.ColumnMappings.Add("createdBy", "SID_createdby");
                        bulkCopyTemp.ColumnMappings.Add("createdDate", "SID_createddate");

                        try
                        {
                            bulkCopyTemp.WriteToServer(tblTemp);
                        }
                        catch (Exception ex)
                        {
                            ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                            return false;
                        }
                        finally
                        {
                            bulkCopyTemp.Close();
                            tblTemp.Dispose();
                        }
                    }
                }

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            } //File.Exists(strFileName)
        } //filename2.PostedFile != null

        if (ErrorRecord <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    protected void BtnShip1_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(filename2.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('不能上传空文档!');";
            return;
        }
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        string strCatFolder = Server.MapPath("/import");
        string strUserFileName = filename2.PostedFile.FileName;
        string strFileName = strCatFolder + "\\" + strKey + strUserFileName;
        try
        {
            filename2.PostedFile.SaveAs(strFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('上传文件失败.');";
            return;
        }
        string ext = ShareDocument.GetFileExtension(strFileName);
        if (ext != "208207")
        {
            ltlAlert.Text = "alert('文档格式不正确，必须为.xsl文件!');";
            return;
        }


        //int useraccess = 0;
        //useraccess = sid.CheckImportDataUserAccess(Convert.ToInt32(Session["uID"]));
        if (!this.Security["550010021"].isValid && !this.Security["550010022"].isValid)
        {
            ltlAlert.Text = "alert('无操作权限，请先申请权限！');";
            return;
        }
        sid.DelTempCheckedDateInfo(Convert.ToInt32(Session["uID"]));
        sid.DelImportError(Convert.ToInt32(Session["uID"]));

        int ErrorRecord = 0;

        if (!ImportExcelFile1())
        {
            ErrorRecord += 1;
        }

        if (ErrorRecord == 0)
        {
            Int32 Ierr = 0;
            bool finished = this.Security["550010021"].isValid;
            bool arrived = this.Security["550010022"].isValid;
            Ierr = sid.ImportCheckData(Convert.ToInt32(Session["uID"]),finished,arrived);
            if (Ierr < 0)
            {
                string Ierr1 = "";
                Ierr1 = sid.ImportCheckDataExsit(Convert.ToInt32(Session["uID"]));
                ltlAlert.Text = "alert('送货单："+Ierr1+"送货时间已确认,如需修改请联系报关与计划！');";
            }
            else
            {
                ltlAlert.Text = "alert('导入成功!');";
            }

        }
        else
        {
            Session["EXTitle"] = "500^<b>错误原因</b>~^";
            Session["EXHeader"] = "";
            Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
    }
}
