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
using System.Data.OleDb;
using adamFuncs;

public partial class hr_AttendanceImport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Access (.mdb) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);
        }
    }

    protected void BtnAttImport_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        ImportAccessFile();
    }

    public void ImportAccessFile()
    {
        string strsql = "";
        string strFileName = "";
        string strBackFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

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

        if(strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower().Trim() != "mdb")
        {
            ltlAlert.Text = "alert('导入文件必须是Access数据库(*.mdb).');";
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
            //if (filename1.PostedFile.ContentLength > 8388608)
            //{
            //    ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
            //    return;
            //}

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch (Exception ex)
            {
                ltlAlert.Text = "alert('上传文件失败.');"; 
                return;
            }

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName;
            int intLogID = 0;

            if (File.Exists(strFileName))
            {
                OleDbConnection conn = new OleDbConnection(strConn);

                try
                {                    
                    conn.Open();

                    //DataTable dtb = AccessHelper.getAllTables(conn);
                    if (AccessHelper.chkTableName(conn, "checkinout"))
                    {
                        OleDbDataReader reader1 = AccessHelper.ExecuteReader(conn, " Select Distinct Sensorid From checkinout ", null);
                        
                        while (reader1.Read())
                        {
                            strsql = " Select Top(1) Isnull(logID, 0) From hr_AttendanceInfo Where Sensorid='" + reader1[0] + "' And PlantID = '" + Session["PlantCode"] + "' Order By logID Desc ";
                            if (SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strsql) != null)
                            {
                                intLogID = Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strsql));
                            }

                            OleDbDataReader reader = AccessHelper.ExecuteReader(conn, " Select logID, userID, CheckTime, CheckType, Sensorid From checkinout Where Sensorid = '" + reader1[0] + "' And LogID > " + intLogID, null);
                            while (reader.Read())
                            {
                                strsql = " Insert Into hr_AttendanceInfo(logID, AttendanceUserNo, CheckTime, CheckType, Sensorid, ImportedBy, ImportedDate, PlantID)";
                                strsql += " Values('" + reader[0] + "','" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + Session["uID"];
                                strsql += "', getdate(), '" + Session["PlantCode"] + "')";

                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strsql);
                            }
                            reader.Close();
                        }
                        reader1.Close();
                    }
                    else
                    {
                        ltlAlert.Text = "alert('导入文件不是考勤机上所使用的数据库格式.');";
                        return;
                    }
                    conn.Close();
                }
                catch(Exception ex)
                {
                    conn.Close();

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件不是考勤机上所使用的数据库格式." + ex.Message + "');";
                    return;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_hr_UpdateAttendanceInfo");

                ltlAlert.Text = "alert('考勤信息导入成功.'); window.location.href='/HR/hr_AttendanceImport.aspx?rm=" + DateTime.Now.ToString() + "';";
            } 
        } 
    }
}
