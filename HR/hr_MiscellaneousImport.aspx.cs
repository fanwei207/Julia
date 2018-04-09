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
using Wage;

public partial class hr_MiscellaneousImport : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

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

            filetypeDDL.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            filetypeDDL.Items.Add(item1);
        }
    }

    protected void BtnMiscellaneousImport_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (MiscellaneousDDL.SelectedItem.Value.ToString() == "0")
        {
            ltlAlert.Text = "alert('请选择杂项类型');";
            return;
        }

        ImportExcelFile();
    }

    public void ImportExcelFile()
    {
        String strSQL = "";
        DataTable dst = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;
        string strPlant = Convert.ToString(Session["PlantCode"]);
        string strImporter = Convert.ToString(Session["uID"]);

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
                    dst = this.GetExcelContents(strFileName);
                }
                catch (Exception ex)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
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

                try
                {
                    if (dst.Rows.Count > 0)
                    {
                        string strUID = string.Empty;
                        string strUserNo = string.Empty;
                        string strDate = string.Empty;
                        string strAmount = string.Empty;
                        string strMemo = string.Empty;
                        DateTime date = DateTime.Now;
                        Decimal amount = 0.0M;
                        bool isError = false;

                        strSQL = " Delete From ImportError Where userID = '" + strImporter + "'";
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);

                        int intType = Convert.ToInt32(MiscellaneousDDL.SelectedItem.Value);

                        i = 0;

                        for (i = 0; i <= dst.Rows.Count - 1; i++)
                        {
                            isError = false;
                            if (dst.Rows[i].IsNull(0)) strUserNo = "";
                            else strUserNo = dst.Rows[i].ItemArray[0].ToString().Trim();

                            if (dst.Rows[i].IsNull(2)) strDate = "";
                            else strDate = dst.Rows[i].ItemArray[2].ToString().Trim();

                            if (dst.Rows[i].IsNull(3)) strAmount = "";
                            else strAmount = dst.Rows[i].ItemArray[3].ToString().Trim();

                            if (dst.Rows[i].IsNull(4)) strMemo = "";
                            else strMemo = dst.Rows[i].ItemArray[4].ToString().Trim();

                            if (strUserNo.Length > 0)
                            {
                                if (strUserNo.Length == 0)
                                {
                                    ErrorRecord++;
                                    isError = true;
                                    hr.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 员工工号不能为空", strImporter);
                                }

                                if (strDate.Length == 0)
                                {
                                    ErrorRecord++;
                                    isError = true;
                                    hr.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 生效日期不能为空", strImporter);
                                }
                                else
                                {
                                    try
                                    {
                                        date = Convert.ToDateTime(strDate);

                                        strUID = hr.CheckMiscellaneousUserIsValid(strUserNo, strDate, strPlant);
                                        if (strUID.Length == 0)
                                        {
                                            ErrorRecord++;
                                            isError = true;
                                            hr.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 员工工号" + strUserNo + "不存在或者已删除或者已离职或者无效或者已离职员工生效日期跨月", strImporter);
                                        }
                                    }
                                    catch
                                    {
                                        ErrorRecord++;
                                        isError = true;
                                        hr.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 生效日期非法", strImporter);
                                    }
                                }

                                if (strAmount.Length == 0)
                                {
                                    ErrorRecord++;
                                    isError = true;
                                    hr.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 发生金额不能为空", strImporter);
                                }
                                else
                                {
                                    try
                                    {
                                        amount = Convert.ToDecimal(strAmount);
                                    }
                                    catch
                                    {
                                        ErrorRecord++;
                                        isError = true;
                                        hr.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 发生金额非法", strImporter);
                                    }
                                }

                                if (strMemo.Length > 50)
                                {
                                    ErrorRecord++;
                                    isError = true;
                                    hr.InsertErrorInfo("行:" + (i + 2).ToString().Trim() + ", 备注信息不能超过50", strImporter);
                                }

                                if (!isError)
                                {
                                    hr.ImportMiscellaneousInfo(strUID, strDate, strAmount, strMemo, strPlant, strImporter, intType);                                    
                                }
                            }
                        }
                    }
                    dst.Reset();

                    if (ErrorRecord == 0)
                    {
                        ltlAlert.Text = "alert('导入文件成功!');";
                    }
                    else
                    {
                        ltlAlert.Text = "alert('导入文件结束，有错误!'); window.location.href='/HR/hr_MiscellaneousImport.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
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
