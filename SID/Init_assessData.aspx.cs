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
using TCPCHINA.ODBCHelper;
using System.Text.RegularExpressions;

public partial class SID_Init_assessData :BasePage
{
    String strConn = System.Configuration.ConfigurationSettings.AppSettings["sqlConn.Conn9C"];
    adamClass chk = new adamClass();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            int nRet = chk.securityCheck(Convert.ToString(Session["uID"]), Convert.ToString(Session["uRole"]), Convert.ToString(Session["orgID"]), "4573300", false, false);
            if (nRet <= 0)
            {
                Response.Redirect("/public/Message.aspx?type=" + nRet.ToString(), true);
            }

            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "500^<b>错误原因</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);

        }
    }

    protected void BtnAss_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        int ErrorRecord = 0;

        if (!ImportExcelFile())
        {
            ErrorRecord += 1;

        }


        if (ErrorRecord == 0)
        {
                ltlAlert.Text = "alert('导入成功!');";
         
        }
        else
        {
            ltlAlert.Text = "alert('导入结束，有错误！'); window.location.href='" + chk.urlRand("/SID/Init_assessData.aspx?err=y") + "';";

        }

    }
    public Boolean ImportExcelFile()
    {
        String strSQL = "";
        DataSet ds = new DataSet();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
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
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return false;
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
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }



            if (File.Exists(strFileName))
            {

                try
                {
                    ds = chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + e.ToString() + "'.');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Columns[0].ColumnName != "零件号" ||
                        ds.Tables[0].Columns[1].ColumnName != "加工时间" ||
                        ds.Tables[0].Columns[2].ColumnName != "难度系数" 
                       )
                    {
                        ds.Reset();
                        ltlAlert.Text += "alert('导入文件的模版不正确!');";
                        return false;
                    }

                    String _Part = "";
                    String _RunTime = "";
                    String _Rate = "";

                    i = 0;

                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        _Part = "";
                        _RunTime = "";
                        _Rate = "";
 

                        //Price Type
                        if (ds.Tables[0].Rows[i].IsNull(0))
                        {
                            ds.Reset();
                            ltlAlert.Text += "alert('零件必须要填!');";
                            return false;
                        }
                        else
                        {
                            _Part = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                            if (_Part.Length != 14)
                            {
                                ds.Reset();
                                ltlAlert.Text += "alert('零件不正确!');";
                                return false;
                            }

                        }

                        if (ds.Tables[0].Rows[i].IsNull(1))
                        {
                            ds.Reset();
                            ltlAlert.Text += "alert('加工时间必须要填!');";
                            return false;
                        }
                        else
                        {
                            _RunTime = ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                            if (!IsNumber(_RunTime))
                            {
                                ds.Reset();
                                ltlAlert.Text += "alert('加工时间不正确!');";
                                return false;
                            }
                        }

                        if (ds.Tables[0].Rows[i].IsNull(2))
                        {
                            ds.Reset();
                            ltlAlert.Text += "alert('难度系数必须要填!');";
                            return false;
                        }
                        else
                        {
                            _Rate = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                            if (!IsNumber(_RunTime))
                            {
                                ds.Reset();
                                ltlAlert.Text += "alert('难度系数不正确!');";
                                return false;
                            }
                        }

                        if (_Part.Trim() == "" && _RunTime.Trim() == "")
                        {
                            break; ;
                        }


                        strSQL = "update PUB.pt_mstr set pt__qad01 = '" + _RunTime.Trim() + "',pt__qad02 = '" + _Rate.Trim() + "' where pt_domain = 'szx' and pt_part ='" + _Part.Trim() + "' ";
                       OdbcHelper.ExecuteNonQuery(strConn, CommandType.Text, strSQL);


                    } //for
                } //ds.Tables[0].Rows.Count > 0                           

                ds.Reset();


                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            } //File.Exists(strFileName)
        } //filename1.PostedFile != null
        return true;

    }
    public bool IsNumber(string str)
    {
        if (str == null || str == "") return false;
        Regex objNotNumberPattern = new Regex("[^0-9.-]");
        Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
        String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
        String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
        Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

        return !objNotNumberPattern.IsMatch(str) &&
        !objTwoDotPattern.IsMatch(str) &&
        !objTwoMinusPattern.IsMatch(str) &&
        objNumberPattern.IsMatch(str);
    }

}
