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
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using QADSID;

public partial class SID_DocumentInfo : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle1"] = "500^<b>错误原因</b>~^";
                Session["EXHeader1"] = "";
                Session["EXSQL1"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "' And plantID='" + Session["plantCode"] + "' Order By Id ";
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            txtDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string strDate = txtDate.Text.Trim();
        string strDocuNo = txtShipNo.Text.Trim();

        gvDocument.DataSource = sid.SelectDocumentInfo(strDocuNo, strDate, chkIsCabin.Checked);
        gvDocument.DataBind();

        Session["EXTitle"] = "~^";
        Session["EXSQL"] = sid.SelectDocumentInfoExcel(strDocuNo, strDate, chkIsCabin.Checked);
        Session["EXHeader"] = "/SID/ExportSIDDocumentInfo.aspx^~^";
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocument_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvDocument_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDocument.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void ImportExcelFile()
    {
        String strSQL = "";
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        Boolean boolError = false;
        Boolean boolError1 = false;
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
                catch
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件必须是Excel格式或者模板及内容正确!');";
                    return;
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
                    if (dt.Columns[0].ColumnName != "发票号码" || dt.Columns[1].ColumnName != "生效日期" || dt.Columns[2].ColumnName != "提单号码")
                    {
                        dt.Reset();
                        ltlAlert.Text = "alert('导入文件的模版不正确!');";
                        return;
                    }

                    strSQL = " Delete From ImportError Where userID='" + Session["uID"] + "' And plantID='" + Session["plantCode"] + "'";
                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);
                    ErrorRecord = 0;

                    string strInvNo = "";
                    string strComm = "";
                    string strBL = "";

                    #region 按SID_det_temp和ImportError的结构构建表
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
                    DataTable tblTemp = new DataTable("det_temp");

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Invoice";
                    tblTemp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "BL";
                    tblTemp.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Commencement";
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
                        strInvNo = "";
                        strComm = "";
                        strBL = "";
                        boolError = false;

                        if (dt.Rows[i].IsNull(0))
                        {
                            strInvNo = "";
                        }
                        else
                        {
                            strInvNo = dt.Rows[i].ItemArray[0].ToString().Trim();
                        }

                        if (dt.Rows[i].IsNull(1))
                        {
                            strComm = "";
                        }
                        else
                        {
                            strComm = dt.Rows[i].ItemArray[1].ToString().Trim();
                        }

                        if (dt.Rows[i].IsNull(2))
                        {
                            strBL = "";
                        }
                        else
                        {
                            strBL = dt.Rows[i].ItemArray[2].ToString().Trim();
                        }


                        if (strInvNo.Length > 0)
                        {


                            if (strComm.Length <= 0)
                            {
                                ErrorRecord += 1;
                                boolError = true;

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，生效日期不能为空！";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            if (strComm.Length > 20)
                            {
                                ErrorRecord += 1;
                                boolError = true;

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，生效日期长度不能超过20！";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            if (strBL.Length <= 0)
                            {
                                ErrorRecord += 1;
                                boolError = true;

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，提单号码不能为空！";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            if (strBL.Length > 20)
                            {
                                ErrorRecord += 1;
                                boolError = true;

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "行 " + Convert.ToString(i + 2) + "，提单号码长度不能超过20！";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }

                            if (!boolError)
                            {
                                rowTemp = tblTemp.NewRow();

                                rowTemp["Invoice"] = strInvNo;
                                rowTemp["Commencement"] = strComm;
                                rowTemp["BL"] = strBL;
                                rowTemp["createdBy"] = Convert.ToInt32(Session["uID"]);
                                rowTemp["createdDate"] = DateTime.Now;

                                tblTemp.Rows.Add(rowTemp);
                            }
                        }
                    }

                    SqlParameter param = new SqlParameter("@uid", Session["uID"]);
                    boolError = Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_SID_DeleteDetTempInfo", param));

                    if (!boolError)
                    {
                        if (tblTemp != null && tblTemp.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopyTemp = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopyTemp.DestinationTableName = "SID_det_temp";

                                bulkCopyTemp.ColumnMappings.Clear();

                                bulkCopyTemp.ColumnMappings.Add("Invoice", "SID_Invoice");
                                bulkCopyTemp.ColumnMappings.Add("Commencement", "SID_Commencement");
                                bulkCopyTemp.ColumnMappings.Add("BL", "SID_BL");
                                bulkCopyTemp.ColumnMappings.Add("createdBy", "SID_createdby");
                                bulkCopyTemp.ColumnMappings.Add("createdDate", "SID_createddate");

                                try
                                {
                                    bulkCopyTemp.WriteToServer(tblTemp);
                                }
                                catch (Exception ex)
                                {
                                    boolError1 = true;
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
                            using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
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
                                    boolError1 = true;
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

                if (!boolError1)
                {
                    if (ErrorRecord == 0)
                    {

                        SqlParameter param = new SqlParameter("@uid", Session["uID"]);
                        boolError = Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_SID_UpdateDetInfo", param));

                        if (!boolError)
                        {
                            ltlAlert.Text = "alert('数据更新成功！'); window.location.href='" + chk.urlRand("/SID/SID_DocumentInfo.aspx") + "';";
                        }
                        else
                        {
                            ltlAlert.Text = "alert('数据更新失败，请重试！'); window.location.href='" + chk.urlRand("/SID/SID_DocumentInfo.aspx") + "';";
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('数据更新导入结束，有错误！'); window.location.href='" + chk.urlRand("/SID/SID_DocumentInfo.aspx?err=y") + "';";
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('导入错误,请联系系统管理员!'); window.location.href='" + chk.urlRand("/SID/SID_DocumentInfo.aspx") + "';";
                }
            }
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        ImportExcelFile();
    }
}
