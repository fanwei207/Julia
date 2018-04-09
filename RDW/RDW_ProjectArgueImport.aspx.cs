using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class RDW_RDW_ProjectArgueImport : BasePage
{

    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lkbModle_Click(object sender, EventArgs e)
    {
        string title = "100^<b>Project Name</b>~^100^<b>Project Code</b>~^100^<b>Massage</b>~^";

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
        ImportExcelFile();
    }

    public void ImportExcelFile()
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
                filename.PostedFile.SaveAs(strFileName);//上传 文件
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
                        ltlAlert.Text = "alert('导入文件必须是Excel格式a');";

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
                        success = importProductArgue(dt, out message, strUID,struName, out errDt);//插入，
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

                        string title = "100^<b>Project Name</b>~^100^<b>Project Code</b>~^100^<b>Massage</b>~^100^<b>error</b>~^";
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式a');";
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

    private bool importProductArgue(DataTable dt, out string message, string strUID, string struName, out DataTable errorDt)
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
                        dt.Columns[0].ColumnName != "Project Name" ||
                        dt.Columns[1].ColumnName != "Project Code" ||
                        dt.Columns[2].ColumnName != "Massage" 

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
                TempColumn.ColumnName = "ProjectName";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "ProjectCode";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Massage";
                TempTable.Columns.Add(TempColumn);

             

                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //TempRow["cost"] 
                        TempRow = TempTable.NewRow();//创建新的行

                        TempRow["ProjectName"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["ProjectCode"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                        TempRow["Massage"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                     



                        TempTable.Rows.Add(TempRow);
                    }

                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();

                    string sqlstr = "sp_RDW_importProductArgue";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",strUID)
                            , new SqlParameter("@uName",struName)
                                 };

                    errorDt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
                    if (errorDt.Rows.Count > 0)
                    {

                        if (errorDt.Rows[0][0].ToString().Equals("1"))
                        {
                            success = true;
                            message = "导入文件成功!";
                        }
                        else
                        {
                            message = "导入文件失败!";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "导入文件失败!";
                        success = false;
                    }

                }
            }
            catch
            {
                message = "导入文件失败!";
                success = false;
                throw new Exception();
            }

        }
        return success;
    }


}