using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using CommClass;


public partial class EDI_EDIPOExceptionImport : BasePage
{
    //adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lkbModle_Click(object sender, EventArgs e)
    {
        string title = "100^<b>Customer</b>~^100^<b>Cust Part</b>~^100^<b>Owner</b>~^100^<b>Reason Code</b>~^100^<b>Start Date</b>~^";

        string[] titleSub = title.Split(new char[] { '~' });

        DataTable dtExcel = new DataTable("temp");
        DataColumn col;

        foreach (string colName in titleSub)
        {
            col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = colName;
            dtExcel.Columns.Add(col);
        }

        ExportExcel(title, dtExcel, false);
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

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
                ltlAlert.Text = "alert('Upload file failed.');";
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please，Select import file');";
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
                ltlAlert.Text = "alert('The maximum upload file is 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('Upload file failed.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {


                    DataTable errDt = null;
                    DataTable dt = null;
                    bool success = false;
                    try
                    {
                        //dt = adam.getExcelContents(filePath).Tables[0];
                        //NPOIHelper helper = new NPOIHelper();
                        dt = GetExcelContents(strFileName);
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('Import file must be Excel format A');";

                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }

                    string message = "";
                    try
                    {
                        success = importEDIPoException(dt, out message, strUID, struName, out errDt);//插入，
                    }
                    catch { }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }

                    }
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {
                        string title = "100^<b>Customer</b>~^100^<b>Cust Part</b>~^100^<b>Owner</b>~^100^<b>Reason Code</b>~^100^<b>Start Date</b>~^180^<b>Error</b>~^";
                      
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('Import file must be Excel format B');";
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

    private bool importEDIPoException(DataTable dt, out string message, string strUID, string struName, out DataTable errorDt)
    {
        message = "";
        errorDt = null;
        dt.TableName = "TempTable";
        bool success = true;

        if (success)
        {
            try
            {
                if (
                    //dt.Columns[0].ColumnName != "QAD" ||
                    dt.Columns[0].ColumnName != "Customer" ||
                    dt.Columns[1].ColumnName != "Cust Part" ||
                    dt.Columns[2].ColumnName != "Owner" ||
                    dt.Columns[3].ColumnName != "Reason Code" ||
                     dt.Columns[4].ColumnName != "Start Date" 
                    )
                {
                    dt.Reset();
                    message = "Import file template is not correct, please update the template and then import!";
                    success = false;
                }

                DataTable TempTable = new DataTable("TempTable");
                DataColumn TempColumn;
                DataRow TempRow;


                //TempColumn = new DataColumn();
                //TempColumn.DataType = System.Type.GetType("System.String");
                //TempColumn.ColumnName = "QAD";
                //TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "custPart";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "shipTo";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "ownerName";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "exceptionCode";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "starDate";
                TempTable.Columns.Add(TempColumn);

                

                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //TempRow["cost"] 
                        TempRow = TempTable.NewRow();//创建新的行

                       // TempRow["QAD"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["custPart"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                        TempRow["shipTo"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["ownerName"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                        TempRow["exceptionCode"] = dt.Rows[i].ItemArray[3].ToString().Trim();
                        TempRow["starDate"] = dt.Rows[i].ItemArray[4].ToString().Trim();

                        TempTable.Rows.Add(TempRow);
                    }

                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();

                    string sqlstr = "sp_EDI_POexceptionImport";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",strUID)
                             , new SqlParameter("@uName",struName)
                        
                                 };

                    errorDt = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sqlstr, param).Tables[0];
                    if (errorDt.Rows.Count > 0)
                    {

                        if (errorDt.Rows[0][0].ToString().Equals("1"))
                        {
                            success = true;
                            message = "Import file successfully!";
                        }
                        else
                        {
                            message = "Import file failed!";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "Import file failed!";
                        success = false;
                    }

                }
            }
            catch
            {
                message = "Import file failed!";
                success = false;
                throw new Exception();
            }

        }
        return success;
    }
}