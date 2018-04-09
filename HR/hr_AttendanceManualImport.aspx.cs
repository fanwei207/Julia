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

public partial class hr_AttendanceManualImport : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("14020116", "A类考勤数据维护"); 
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "100^<b>成本中心</b>~^100^<b>工号</b>~^150^<b>上班时间</b>~^150^<b>下班时间</b>~^500^<b>错误信息</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select Center,Code,Convert(varchar(20),Starttime,120) as Starttime,Convert(varchar(20),Endtime,120) as Endtime,ErrorMessage From tcpc0.dbo.hr_AttendanceManualImport_Temp Where createdBy='" + Convert.ToInt32(Session["uID"]) + "'" + " And ErrorMessage <> ''";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx?ymd=a','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            filetypeDDL.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            filetypeDDL.Items.Add(item1);
        }
        
    }

    protected void BtnAttendanceManualImport_ServerClick(object sender, EventArgs e)
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
        DataTable dst = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;
        string strPlant = Convert.ToString(Session["PlantCode"]);
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

        strFileName = strCatFolder + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + strUserFileName;

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
                    if (dst.Columns[0].ColumnName != "成本中心" ||
                        dst.Columns[1].ColumnName != "工号" ||
                        dst.Columns[2].ColumnName != "上班时间" ||
                        dst.Columns[3].ColumnName != "下班时间")
                    {
                        dst.Reset();
                        ltlAlert.Text += "alert('导入文件的模版不正确!');";
                        return;
                    }

                    //新建TempTable内存表
                    DataTable TempTable = new DataTable("TempTable");
                    DataColumn TempColumn;
                    DataRow TempRow;

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "Center";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "Code";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "Starttime";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "Endtime";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.Int32");
                    TempColumn.ColumnName = "createdBy";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "ErrorMessage";
                    TempTable.Columns.Add(TempColumn);

                    if (dst.Rows.Count > 0)
                    {
                        string strCenter = string.Empty;
                        string strUserNo = string.Empty;
                        string strStart = string.Empty;
                        string strEnd = string.Empty;
                        string strError = string.Empty;

                        DateTime start = DateTime.Now;
                        DateTime end = DateTime.Now;

                        strSQL = " Delete From hr_AttendanceManualImport_Temp Where createdBy = '" + strUID + "'";
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);

                        for (int i = 0; i <= dst.Rows.Count - 1; i++)
                        {
                            TempRow = TempTable.NewRow();

                            if (dst.Rows[i].IsNull(0)) strCenter = "";
                            else strCenter = dst.Rows[i].ItemArray[0].ToString().Trim();

                            if (dst.Rows[i].IsNull(1)) strUserNo = "";
                            else strUserNo = dst.Rows[i].ItemArray[1].ToString().Trim();

                            if (dst.Rows[i].IsNull(2)) strStart = "";
                            else strStart = dst.Rows[i].ItemArray[2].ToString().Trim();

                            if (dst.Rows[i].IsNull(3)) strEnd = "";
                            else strEnd = dst.Rows[i].ItemArray[3].ToString().Trim();

                            strError = "";

                            if (strCenter.Length == 0)
                            {
                                strError += "成本中心不能为空;";
                            }
                            
                            if (strStart.Length == 0)
                            {
                                strError += "上班时间不能为空;";
                            }
                            else
                            {
                                try
                                {
                                    start = Convert.ToDateTime(strStart);

                                    TempRow["Starttime"] = strStart;
                                }
                                catch
                                {
                                    strError += "上班时间非法;";
                                }
                            }

                            if (strEnd.Length == 0)
                            {
                                strError += "下班时间不能为空;";
                            }
                            else
                            {
                                try
                                {
                                    end = Convert.ToDateTime(strEnd);

                                    TempRow["Endtime"] = strEnd;
                                }
                                catch
                                {
                                    strError += "下班时间非法;";
                                }
                            }

                            if (DateTime.Compare(start, end) > 0)
                            {
                                strError += "下班时间不能早于上班时间;";
                            }

                            if (strUserNo.Length == 0)
                            {
                                strError += "员工工号不能为空;";
                            }
                            else
                            {
                                if (hr.ChecklimitedUser(strUserNo, strPlant))
                                {
                                    if (!this.Security["14020116"].isValid)
                                    {
                                        strError += "员工工号" + strUserNo + "没有维护A类员工考勤权限;";
                                    }
                                }
                            }
                            
                            TempRow["Center"] = strCenter;
                            TempRow["Code"] = strUserNo;    
                            TempRow["createdBy"] = strUID;
                            TempRow["ErrorMessage"] = strError;

                            TempTable.Rows.Add(TempRow);
                        }

                        //TempTable有数据的情况下
                        if (TempTable != null && TempTable.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopy.DestinationTableName = "hr_AttendanceManualImport_Temp";

                                bulkCopy.ColumnMappings.Clear();

                                bulkCopy.ColumnMappings.Add("Center", "Center");
                                bulkCopy.ColumnMappings.Add("Code", "Code");
                                bulkCopy.ColumnMappings.Add("Starttime", "Starttime");
                                bulkCopy.ColumnMappings.Add("Endtime", "Endtime");
                                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                                bulkCopy.ColumnMappings.Add("ErrorMessage", "ErrorMessage");

                                try
                                {
                                    bulkCopy.WriteToServer(TempTable);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('导入时出错，请联系系统管理员！');";
                                    return;
                                }
                                finally
                                {
                                    TempTable.Dispose();
                                    bulkCopy.Close();
                                }
                            }
                        }
                        hr.CheckAttendanceManual(strPlant, strUID);
                    }
                    dst.Reset();

                    if (hr.JudgeAttendanceManual(strUID))
                    {
                        switch (hr.ImportAttendanceManual(strUID, struName, strPlant))
                        {
                            case 0:
                                ltlAlert.Text = "alert('导入文件成功!'); window.location.href='/HR/hr_AttendanceManualImport.aspx?rm=" + DateTime.Now.ToString() + "';";
                                return;
                                break;

                            case 1:
                                ltlAlert.Text = "alert('导入文件考勤开始日期跨月!'); window.location.href='/HR/hr_AttendanceManualImport.aspx?rm=" + DateTime.Now.ToString() + "';";
                                return;
                                break;

                            case 2:
                                ltlAlert.Text = "alert('有员工考勤编号没有导入，请更新考勤编号后再导入!'); window.location.href='/HR/hr_AttendanceManualImport.aspx?rm=" + DateTime.Now.ToString() + "';";
                                return;
                                break;

                            case -1:
                                ltlAlert.Text = "alert('导入文件失败!'); window.location.href='/HR/hr_AttendanceManualImport.aspx?rm=" + DateTime.Now.ToString() + "';";
                                return;
                                break;
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('导入文件结束，有错误!'); window.location.href='/HR/hr_AttendanceManualImport.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                        return;
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