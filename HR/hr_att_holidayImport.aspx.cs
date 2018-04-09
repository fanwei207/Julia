using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Wage;

public partial class HR_hr_att_holidayImport : BasePage
{
    HR helper = new HR();


    protected void Page_Load(object sender, EventArgs e)
    {

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
                    string message = "";
                    bool success = helper.holidayImport(strFileName, strUID, Convert.ToInt32(Session["PlantCode"]), out message);//插入，
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {
                        DataTable errDt = helper.GetImportError(strUID);//输出错误信息
                        string title = "100^<b>日期</b>~^100^<b>工号</b>~^100^<b>姓名</b>~^100^<b>小时</b>~^100^<b>错误信息</b>~^";
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
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

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("/hr/hr_att_holiday.aspx");
    }
}