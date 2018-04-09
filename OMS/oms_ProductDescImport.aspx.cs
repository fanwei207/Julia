using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class oms_ProductDescImport : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFileType.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            ddlFileType.Items.Add(item1);
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
       this.ImportExcelFile();
    }

    public void ImportExcelFile()
    {
        string strFileName = "";
        string strCatFolder = "";//服务器对应的物理路径
        string strUserFileName = "";
        int intLastBackslash = 0;//最后的反斜杠的位置
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
                ltlAlert.Text = "alert('Upload  failed');";
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;//PostedFile返回要上载的文件
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);//获取到文件名
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please select file.');";
            return;
        }
        strUserFileName = strFileName;

        //给文件命名
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

        //
        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum amount of data is 8MB !');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//文件(上传了）
            }
            catch
            {
                ltlAlert.Text = "alert('Upload  failed');";
                return;
            }
            //如果文件已经存在
            if (File.Exists(strFileName))
            {
                try
                {
                    string message = "";
                    bool success = OMSHelper.Import(strFileName, strUID, struName, out message);
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {
                        DataTable errDt = OMSHelper.GetImportError(strUID);

                        StringBuilder title = new StringBuilder();
                        for (int ic = 0; ic < errDt.Columns.Count; ic++)
                        { 
                           title.Append("100^<b>"+errDt.Columns[ic].ToString().Trim()+"</b>~^");
                        }

                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title.ToString(), errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('The upload file must be Excel'" + ex.Message.ToString() + "'.');";
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