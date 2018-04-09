using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class part_NPart_importPartVendorToAppv : BasePage
{

    PC_price pc = new PC_price();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lkbModle_Click(object sender, EventArgs e)
    {
        string title = "100^<b>QAD</b>~^100^<b>物料号</b>~^100^<b>供应商</b>~^160^<b>单位</b>~^100^<b>技术参考价</b>~^100^<b>需求规格</b>~^100^<b>备注</b>~^";

        string[] titleSub = title.Split(new char[]{'~'});

        DataTable dtExcel = new DataTable("temp");
        DataColumn col;

        foreach(string colName in titleSub)
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
                         success = pc.importPartVendorToAppv(dt, out message, strUID, out errDt);//插入，
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

                        string title = "100^<b>QAD</b>~^100^<b>物料号</b>~^100^<b>供应商</b>~^160^<b>单位</b>~^100^<b>技术参考价</b>~^100^<b>需求规格</b>~^100^<b>备注</b>~^100^<b>错误信息</b>~^";
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
}