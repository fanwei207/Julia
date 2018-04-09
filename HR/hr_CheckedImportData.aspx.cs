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
using ImportDT;
using NPOI.SS.Formula.Functions;

public partial class HR_hr_CheckedImportData : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();
    ImportData ipdt = new ImportData();
   
     protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "500^<b>������Ϣ</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "' Order by id ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            dropType.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            dropType.Items.Add(item1);


            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            Bindholiday();     

            btnDelete.Attributes.Add("onclick", "return confirm('ȷ��Ҫɾ��������');");

            btnDelNight.Attributes.Add("onclick", "return confirm('ȷ��Ҫɾ��ҹ�����������');");
            btnDelHoliday.Attributes.Add("onclick", "return confirm('ȷ��Ҫɾ���������������');");

            btnAttendance.Attributes.Add("onclick", "return confirm('ȷ��Ҫת����������ѡ���µĿ��ڽ���������');");
        }
   
    }

    protected void BtnMiscellaneousImport_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (dropImport.SelectedValue  == "0")
        {
            ltlAlert.Text = "alert('��ѡ����������');";
            return;
        }

        ImportExcelFile();
    }

    public void ImportExcelFile()
    {

        String strSQL = "";
        
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;
        string strPlant = Convert.ToString(Session["PlantCode"]);
        string strImporter = Convert.ToString(Session["uID"]);
        bool blInsert;
        blInsert = true;

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return;
            }

        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('��ѡ�����ļ�.');";
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
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                DataSet dst = new DataSet();
                try
                {
                    //dst = chk.getExcelContents(strFileName);
                    dst.Tables.Add(GetExcelContents(strFileName));
                }
                catch (Exception ex)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ.');";
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
                   
          
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        /*
                         ����	    1       hr_CheckSalary
                         ��Ϣ	    2       hr_ChSpDate
                         ���	    3       hr_ChLeaveDate   
                         ����	    4       hr_ChRestTime  
                         ����	    5       hr_ChAttendence
                         �鳧��Ա	6       Users.isAttendance = 1
                         ����1	    7
                         ����1	    8
                         */

                        strSQL = " Delete From ImportError Where userID = '" + Convert.ToString(Session["uID"]) + "'";
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);

                        switch (dropImport.SelectedValue)
                        {
                            case "1":
                                blInsert=ImportSalary(dst, Convert.ToString(Session["uID"]));
                                break ;
                            case "2":
                                blInsert = ImportRest(dst, Convert.ToString(Session["uID"]));
                                break;
                            case "3":
                                blInsert = ImportLeave(dst, Convert.ToString(Session["uID"]));
                                break;
                            case "4":
                                blInsert = ImportLunchTime(dst, Convert.ToString(Session["uID"]));
                                break;
                            case "5":
                                blInsert = ImportAttDay(dst, Convert.ToString(Session["uID"]));
                                break;
                            case "6":
                                blInsert = ImportAttUsers(dst, Convert.ToString(Session["uID"]));
                                break;
                            case "7":
                                blInsert = ImportSalary1(dst, Convert.ToString(Session["uID"]));
                                break;
                            case "8":
                                blInsert = ImportAttDay1(dst, Convert.ToString(Session["uID"]));
                                break;
                        }
                        
                    }

                    dst.Reset();

                    if (blInsert)
                    {
                        if (dropImport.SelectedValue.ToString() != "5")
                        {
                            ltlAlert.Text = "alert('����ɹ����д����¼!'); window.location.href='/HR/hr_CheckedImportData.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('�����ļ��ɹ�!'); window.location.href='/HR/hr_CheckedImportData.aspx?rm=" + DateTime.Now.ToString() + "';";
                    }
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('�����ļ�ʧ��!');";
                    return;
                }

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            } // End If(File.Exists(strFileName))
        } // End if (filename.PostedFile != null)

    }

    #region ��������
    private bool  ImportSalary(DataSet dsSalary,string strUid)
    {
        int i = 0;
        bool blflag ;
        bool blSalary = false;

        for (i = 0; i <= dsSalary.Tables[0].Rows.Count - 1; i++)
        {
            blflag = false;

            string strDept;
            string strNum,strUser,strName,strUserID,strWp,strDate,strkind;
            DateTime dtDate;
            decimal[] decOthers;
            decOthers = new decimal[30];

            strUser = "";
            strName = "";
            strDept = "";
            strNum = "";
            strWp = ""; 
            strDate = ""; 
            strkind = "";
            dtDate =DateTime.Now;

            // Validate department infor
            if (dsSalary.Tables[0].Rows[i].IsNull(0))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
            {
                strDept = ipdt.CheckDeptName(dsSalary.Tables[0].Rows[i].ItemArray[0].ToString().Trim());
                if (strDept.Trim().Length == 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��������򲻴���", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }

            //Validate Seri Number
            if (dsSalary.Tables[0].Rows[i].IsNull(1))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��Ų���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
                strNum = dsSalary.Tables[0].Rows[i].ItemArray[1].ToString().Trim();


            //Validate user number and name
            if (dsSalary.Tables[0].Rows[i].IsNull(2))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
                strUser = dsSalary.Tables[0].Rows[i].ItemArray[2].ToString().Trim();


            if (dsSalary.Tables[0].Rows[i].IsNull(3))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��������Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
                strName = dsSalary.Tables[0].Rows[i].ItemArray[3].ToString().Trim();

            if (strUser.Trim().Length != 0 && strName.Trim().Length != 0)
            {
                strUserID = ipdt.CheckUserInfo(strUser, strName, Convert.ToString(Session["plantcode"]));
                if (strUserID.Trim().Length == 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ź�������ƥ��򲻴���", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }
            else
                strUserID = "";

            // Work group Name
            strWp = dsSalary.Tables[0].Rows[i].ItemArray[4].ToString().Trim();


            //Validate other datas
            if (dsSalary.Tables[0].Rows[i].IsNull(30))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ڲ���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
            {
                strDate = dsSalary.Tables[0].Rows[i].ItemArray[30].ToString().Trim();
                try
                {
                    DateTime date = Convert.ToDateTime(strDate);
                }
                catch
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������ڷǷ�", strUid);
                    blflag = true;
                    blSalary = true;
                }
                dtDate = Convert.ToDateTime(dsSalary.Tables[0].Rows[i].ItemArray[30].ToString().Trim());
            }

            for (int j = 5; j <= 29; j++)
            {
                string strjudge;
                if (Convert.ToString(dsSalary.Tables[0].Rows[i].ItemArray[j]).Trim().Length == 0)
                    strjudge = "0";
                else
                    strjudge = Convert.ToString(dsSalary.Tables[0].Rows[i].ItemArray[j]);

                //Response.Write(strjudge+"<br>");

                if (!CheckDecimal(strjudge))
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", [" + strjudge + "]������ݷǷ�", strUid);
                    blflag = true;
                    blSalary = true;
                }
                else
                {
                    decOthers[j] = Convert.ToDecimal(strjudge);
                    //Response.Write(decOthers[j].ToString() + "<br>");
                }
                    //decOthers[j] = (Convert.ToString(dsSalary.Tables[0].Rows[i].ItemArray[j]).Trim().Length == 0) ? 0 : Convert.ToDecimal(dsSalary.Tables[0].Rows[i].ItemArray[j]);

            }
            
            strkind = dsSalary.Tables[0].Rows[i].ItemArray[31].ToString().Trim();
            if (strkind.Length == 0)
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ͳ���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
            {
                try
                {
                    int intTT = Convert.ToInt32(strkind);
                }
                catch
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������ݷǷ�", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }

            //PrintWrite(decOthers);
            if (!blflag)
            {
                if (ipdt.InsertSalary(strDept, strNum, strUser, strName, strWp, decOthers, dtDate, strkind, strUserID, Convert.ToString(Session["uid"])) < 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }

        }
        dsSalary.Reset();
        return blSalary;
    }
    #endregion

    #region ��������1
    private bool ImportSalary1(DataSet dsSalary, string strUid)
    {
        int i = 0;
        bool blflag;
        bool blSalary = false;

        for (i = 0; i <= dsSalary.Tables[0].Rows.Count - 1; i++)
        {
            blflag = false;

            string strDept;
            string strNum, strUser, strName, strUserID, strWp, strDate, strkind;
            decimal[] decOthers;
            decOthers = new decimal[30];

            strUser = "";
            strName = "";
            strDept = "";
            strNum = "";
            strWp = "";
            strDate = "";
            strkind = "";

            // Validate department infor
            if (dsSalary.Tables[0].Rows[i].IsNull(0))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
            {
                strDept = ipdt.CheckDeptName(dsSalary.Tables[0].Rows[i].ItemArray[0].ToString().Trim());
                if (strDept.Trim().Length == 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��������򲻴���", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }

            //Validate Seri Number
            if (dsSalary.Tables[0].Rows[i].IsNull(1))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��Ų���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
                strNum = dsSalary.Tables[0].Rows[i].ItemArray[1].ToString().Trim();


            //Validate user number and name
            if (dsSalary.Tables[0].Rows[i].IsNull(2))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
                strUser = dsSalary.Tables[0].Rows[i].ItemArray[2].ToString().Trim();


            if (dsSalary.Tables[0].Rows[i].IsNull(3))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��������Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
                strName = dsSalary.Tables[0].Rows[i].ItemArray[3].ToString().Trim();

            if (strUser.Trim().Length != 0 && strName.Trim().Length != 0)
            {
                strUserID = ipdt.CheckUserInfo(strUser, strName, Convert.ToString(Session["plantcode"]));
                if (strUserID.Trim().Length == 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ź�������ƥ��򲻴���", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }
            else
                strUserID = "";

            // Work group Name
            strWp = dsSalary.Tables[0].Rows[i].ItemArray[4].ToString().Trim();


            //Validate other datas
            if (dsSalary.Tables[0].Rows[i].IsNull(30))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ڲ���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
            {
                strDate = dsSalary.Tables[0].Rows[i].ItemArray[30].ToString().Trim();
                try
                {
                    DateTime date = Convert.ToDateTime(strDate);
                }
                catch
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������ڷǷ�", strUid);
                    blflag = true;
                    blSalary = true;
                }

            }

            for (int j = 5; j <= 29; j++)
            {
                string strjudge;
                if (Convert.ToString(dsSalary.Tables[0].Rows[i].ItemArray[j]).Trim().Length == 0)
                    strjudge = "0";
                else
                    strjudge = Convert.ToString(dsSalary.Tables[0].Rows[i].ItemArray[j]);

                //Response.Write(strjudge+"<br>");

                if (!CheckDecimal(strjudge))
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", [" + strjudge + "]������ݷǷ�", strUid);
                    blflag = true;
                    blSalary = true;
                }
                else
                {
                    decOthers[j] = Convert.ToDecimal(strjudge);
                    //Response.Write(decOthers[j].ToString() + "<br>");
                }
                //decOthers[j] = (Convert.ToString(dsSalary.Tables[0].Rows[i].ItemArray[j]).Trim().Length == 0) ? 0 : Convert.ToDecimal(dsSalary.Tables[0].Rows[i].ItemArray[j]);

            }

            strkind = dsSalary.Tables[0].Rows[i].ItemArray[31].ToString().Trim();
            if (strkind.Length == 0)
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ͳ���Ϊ��", strUid);
                blflag = true;
                blSalary = true;
            }
            else
            {
                try
                {
                    int intTT = Convert.ToInt32(strkind);
                }
                catch
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������ݷǷ�", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }

            //PrintWrite(decOthers);
            if (!blflag)
            {
                if (ipdt.InsertSalary1(strDept, strNum, strUser, strName, strWp, decOthers, strDate, strkind, strUserID, Convert.ToString(Session["uid"])) < 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���", strUid);
                    blflag = true;
                    blSalary = true;
                }
            }

        }
        dsSalary.Reset();
        return blSalary;
    }
    #endregion

    #region ����
    private bool ImportRest(DataSet dsRest, string strUid)
    {
        int i = 0;
        bool blRest = false;
        bool blflag ;

        for (i = 0; i <= dsRest.Tables[0].Rows.Count - 1; i++)
        {
            blflag = false;
            string strDp,strDate,strType;
            strDp = "";
            strDate = "";
            strType = "";

            if (dsRest.Tables[0].Rows[i].IsNull(0))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blRest = true;
            }
            else
            {
                strDp = dsRest.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                  if (strDp == "����")
                      strDp="0";
                  else
                  {
                    string str = "SELECT departmentID FROM tcpc"+ Convert.ToString(Session["plantcode"])+".dbo.departments where name=N'" + strDp + "' And active=1 ";
                    strDp = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, str).ToString();

                    if (strDp.Length == 0)
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������Ʋ����ڻ��д���", strUid);
                        blflag = true;
                        blRest = true;
                    }
                  }

            }


            if (dsRest.Tables[0].Rows[i].IsNull(1))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ڲ���Ϊ��", strUid);
                blflag = true;
                blRest = true;
            }
            else
            {
                
                try
                {
                    DateTime date = Convert.ToDateTime(dsRest.Tables[0].Rows[i].ItemArray[1].ToString().Trim());
                }
                catch
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ڷǷ�", strUid);
                    blflag = true;
                    blRest = true;
                }

                strDate = Convert.ToDateTime(dsRest.Tables[0].Rows[i].ItemArray[1].ToString().Trim()).ToShortDateString();
            }
            
            if (dsRest.Tables[0].Rows[i].IsNull(2))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ͳ���Ϊ��", strUid);
                blflag = true;
                blRest = true;
            }
            else
                strType = dsRest.Tables[0].Rows[i].ItemArray[2].ToString().Trim();

            if (!blflag)
            {
                if (ipdt.InsertRest(strDp, strDate, strType, Convert.ToString(Session["uid"])) < 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���", strUid);
                    blflag = true;
                    blRest = true;
                }
            }
            
        }
        dsRest.Reset();
        return blRest;
    }
    #endregion

    #region ���
    private bool ImportLeave(DataSet dsLeave, string strUid)
    {
        int i = 0;
        bool blLeave = false;
        bool blflag ;

        
        for (i = 0; i <= dsLeave.Tables[0].Rows.Count - 1; i++)
        {
            blflag = false;
            string strUser,strName,strStart,strEnd,strType,strDate,strUserID,strTypeName ;
            strUser = "";
            strName = "";
            strStart = "";
            strEnd = "";
            strType = "";
            strDate = "";
            strUserID = "";
            strTypeName = "";

            if (dsLeave.Tables[0].Rows[i].IsNull(4))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ͳ���Ϊ��", strUid);
                blflag = true;
                blLeave = true;
            }
            else
            {
                strType = dsLeave.Tables[0].Rows[i].ItemArray[4].ToString().Trim();

                switch (strType)
                {

                    case "��ְ":
                        strTypeName = "0";
                        break;
                    case "�¼�":
                        strTypeName = "1";
                        break;
                    case "����":
                        strTypeName = "2";
                        break;
                    case "����":
                        strTypeName = "3";
                        break;
                    case "���":
                        strTypeName = "4";
                        break;
                    case "����":
                        strTypeName = "5";
                        break;
                    case "ɥ��":
                        strTypeName = "6";
                        break;
                    case "����":
                        strTypeName = "7";
                        break;
                    default:
                        strTypeName = "-1";
                        break;

                }

                if (dsLeave.Tables[0].Rows[i].ItemArray[5].ToString().Trim().Length > 0)
                {
                    strDate = dsLeave.Tables[0].Rows[i].ItemArray[5].ToString().Trim();
                    try
                    {
                        DateTime date = Convert.ToDateTime(strDate);
                    }
                    catch
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ҹ�����ڷǷ�", strUid);
                        blflag = true;
                        blLeave = true;
                    }
                }
                else
                    strDate = "";


                if (strTypeName != "-1")
                {
                    //Validate user number and name
                    if (dsLeave.Tables[0].Rows[i].IsNull(0))
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                        blflag = true;
                        blLeave = true;
                    }
                    else
                        strUser = dsLeave.Tables[0].Rows[i].ItemArray[0].ToString().Trim();


                    if (dsLeave.Tables[0].Rows[i].IsNull(1))
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��������Ϊ��", strUid);
                        blflag = true;
                        blLeave = true;
                    }
                    else
                        strName = dsLeave.Tables[0].Rows[i].ItemArray[1].ToString().Trim();

                    if (strUser.Trim().Length != 0 && strName.Trim().Length != 0)
                    {
                        strUserID = ipdt.CheckUserInfo(strUser, strName, Convert.ToString(Session["plantcode"]));
                        if (strUserID.Trim().Length == 0)
                        {
                            hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ź�������ƥ��򲻴���", strUid);
                            blflag = true;
                            blLeave = true;
                        }
                    }

                    //Validate other datas
                    if (dsLeave.Tables[0].Rows[i].IsNull(2))
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��ʼ���ڲ���Ϊ��", strUid);
                        blflag = true;
                        blLeave = true;
                    }
                    else
                    {
                        try
                        {
                            DateTime date = Convert.ToDateTime(dsLeave.Tables[0].Rows[i].ItemArray[2].ToString().Trim());
                        }
                        catch
                        {
                            hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������ڷǷ�", strUid);
                            blflag = true;
                            blLeave = true;
                        }
                        strStart = Convert.ToDateTime(dsLeave.Tables[0].Rows[i].ItemArray[2].ToString().Trim()).ToShortDateString();
                    }

                    if (dsLeave.Tables[0].Rows[i].IsNull(3))
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������ڲ���Ϊ��", strUid);
                        blflag = true;
                        blLeave = true;
                    }
                    else
                    {

                        try
                        {
                            DateTime edate = Convert.ToDateTime(dsLeave.Tables[0].Rows[i].ItemArray[3].ToString().Trim());
                        }
                        catch
                        {
                            hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������ڷǷ�", strUid);
                            blflag = true;
                            blLeave = true;
                        }
                        strEnd = Convert.ToDateTime(dsLeave.Tables[0].Rows[i].ItemArray[3].ToString().Trim()).ToShortDateString();

                    }
                } // End if type <> -1
                else
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ͳ���ȷ", strUid);
                    blflag = true;
                    blLeave = true;
                }
                
                if (!blflag)
                {
                    
                    if (ipdt.InsertLeave(strUser, strUserID, strStart, strEnd, strTypeName, strType, strDate, Convert.ToString(Session["uid"])) < 0)
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���", strUid);
                        blflag = true;
                        blLeave = true;
                    }
                }

              
            }  //End if Type
           
        }
        dsLeave.Reset();
        return blLeave;
    }
    #endregion

    #region ����
    private bool ImportLunchTime(DataSet dsLunch, string strUid)
    {
        int i = 0;
        bool blLunch = false;
        bool blflag;

       

        for (i = 0; i <= dsLunch.Tables[0].Rows.Count - 1; i++)
        {
            blflag = false;
            string strDp,strDate,strTime;

            string[] strOthers;
            strOthers = new string[6];
            strDp = "";
            strDate ="";
       

            //judge department
            if (dsLunch.Tables[0].Rows[i].IsNull(0))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blLunch = true;
            }
            else
            {
                strDp = dsLunch.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
             
                string str = "SELECT departmentID FROM tcpc" + Convert.ToString(Session["plantcode"]) + ".dbo.departments where name=N'" + strDp + "' And active=1 ";
                object deptId = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, str);

                if (deptId == null)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������Ʋ����ڻ��д���", strUid);
                    blflag = true;
                    blLunch = true;
                }
                else
                {
                    strDp = deptId.ToString();
                }
              }

            //judge effietdate

              if (dsLunch.Tables[0].Rows[i].IsNull(1))
              {
                  hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��Ч���ڲ���Ϊ��", strUid);
                  blflag = true;
                  blLunch = true;
              }
              else
              {
                  strDate = dsLunch.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                  try
                  {
                      DateTime date = Convert.ToDateTime(strDate);
                  }
                  catch
                  {
                      hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",��Ч���ڷǷ�", strUid);
                      blflag = true;
                      blLunch = true;
                  }
              }


            //judge time
              for (int j = 2; j < 6; j++)
              {
                  if (dsLunch.Tables[0].Rows[i].IsNull(j))
                  {
                      hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",ʱ�䲻��Ϊ��", strUid);
                      blflag = true;
                      blLunch = true;
                  }
                  else
                  {
                      strTime = dsLunch.Tables[0].Rows[i].ItemArray[j].ToString().Trim();

                      if (!CheckTime("1999-09-09", strTime))
                      {
                          hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",ʱ��Ƿ�", strUid);
                          blflag = true;
                          blLunch = true;
                      }
                      else
                      {
                          if (strTime.Trim().Length >8)
                            strOthers[j] = Convert.ToDateTime(strTime).ToLongTimeString();
                          else
                            strOthers[j] = strTime;
                      }
                  }
              }


              if (!blflag)
              {

                  if (ipdt.InsertLunchTime(strDp, strDate,strOthers, Convert.ToInt32(Session["uid"])) < 0)
                  {
                      hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���" , strUid);
                      blflag = true;
                      blLunch = true;
                  }
              }

         }

         dsLunch.Reset();
         return blLunch;
     }
    #endregion

    #region �����鳧��Ա���б�
    /// <summary>
    /// �����鳧��Ա���б�
    /// </summary>
    /// <param name="dsAtt"></param>
    /// <param name="strUid"></param>
    /// <returns></returns>
    private bool ImportAttUsers(DataSet dsAtt, string strUid)
    {
        bool blAttday = false;
        int i = 0;
        bool blflag;
        string[] strOthers;
        strOthers = new string[4];

        if (ipdt.ClearAttUsers(Session["PlantCode"].ToString()))
        {
            for (i = 0; i <= dsAtt.Tables[0].Rows.Count - 1; i++)
            {
                blflag = false;
                string strUserNo = string.Empty;

                //����
                if (dsAtt.Tables[0].Rows[i].IsNull(0))
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                    blflag = true;
                    blAttday = true;
                }

                strUserNo = dsAtt.Tables[0].Rows[i][0].ToString().Trim();
                //End workshop

                if (!blflag)
                {
                    if (!ipdt.InsertAttUsers(strUserNo, Session["uName"].ToString(), Session["PlantCode"].ToString()))
                    {
                        hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���", strUid);

                        blflag = true;
                        blAttday = true;
                    }
                }
            }
        }
        else
        {
            hr.InsertErrorInfo("ע��:��������ݵ�ʱ��������!����ϵ����Ա!", strUid);
        }

        dsAtt.Reset();
        return blAttday;
    }

    #endregion

    #region ����
    private bool ImportAttDay(DataSet dsAtt, string strUid)
    {
        bool blAttday = false;
        int i = 0;
        bool blflag;
        string[] strOthers;
        strOthers = new string[4];

        DataTable dtblExcel = new DataTable();
        dtblExcel = dsAtt.Tables[0];
        #region ��������

        #region ������ʱ��
        DataTable table = new DataTable("temp");
        DataColumn column;
        DataRow row;

        //���Guid��
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "deptname";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "workgroup";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "userno";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "username";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "userid";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "date";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "time1";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "time2";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "time3";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "time4";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "type";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "typename";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "creatBy";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "creatdate";
        table.Columns.Add(column);
        #endregion

        

        

        foreach (DataRow rw in dtblExcel.Rows)
        {
            row = table.NewRow();

            row["deptname"] = rw[0].ToString();
            row["workgroup"] = rw[1].ToString();
            row["userno"] = rw[2].ToString();
            row["username"] = rw[3].ToString();
            row["date"] = rw[4].ToString();
            row["time1"] = rw[5].ToString();
            row["time2"] = rw[6].ToString();
            row["time3"] = rw[7].ToString();
            row["time4"] = rw[8].ToString();
            row["type"] = rw[9].ToString();
            row["typename"] = rw[10].ToString();

            row["creatBy"] = Session["uID"].ToString();
            row["creatdate"] = DateTime.Now.ToString();

             
            table.Rows.Add(row);

        }
        if (table != null && table.Rows.Count > 0)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsnx()))
            {
                bulkCopy.DestinationTableName = "tcpc" + Convert.ToString(Session["plantcode"]) + ".dbo.hr_ChAttendence_temp";

                bulkCopy.ColumnMappings.Add("deptname", "departmentName");
                bulkCopy.ColumnMappings.Add("workgroup", "WorkGroup");
                bulkCopy.ColumnMappings.Add("userno", "userNo");
                bulkCopy.ColumnMappings.Add("username", "userName");
                bulkCopy.ColumnMappings.Add("userid", "userID");
                bulkCopy.ColumnMappings.Add("date", "workdate");
                bulkCopy.ColumnMappings.Add("time1", "starttime");
                bulkCopy.ColumnMappings.Add("time2", "endtime");
                bulkCopy.ColumnMappings.Add("time3", "starttime1");
                bulkCopy.ColumnMappings.Add("time4", "endtime1");
                bulkCopy.ColumnMappings.Add("type", "SalaryType");
                bulkCopy.ColumnMappings.Add("typename", "typename");
                bulkCopy.ColumnMappings.Add("creatBy", "creatBy");
                bulkCopy.ColumnMappings.Add("creatdate", "creatdate");

                try
                {
                    //�����ʱ��
                    if (ipdt.ClearTempTable(Session["uID"].ToString()))
                    { 
                        bulkCopy.WriteToServer(table);

                        #region ��֤����
                        ipdt.ValidTempTable(Session["uID"].ToString());
                        #endregion
                        #region ��������
                        if (ipdt.ExceltoTable(Session["uID"].ToString()))
                        {
                            DataTable td = ipdt.exportChAttendenceErrMsg(Session["uID"].ToString());
                            if (td.Rows.Count != 0)
                            {
                                this.Alert("�鳧���ݵ����������޸�Excel�����µ��룡");
                                return true;
                            }
                            else
                            {
                                this.Alert("�鳧���ݵ���ɹ���");
                                return false;
                            }
                        }
                        else
                        {
                            this.Alert("�鳧���ݱ�����������ϵ����Ա��");
                            return true;
                        }
                        #endregion
                    }
                    else
                    {
                        this.Alert("��ʱ���������ʧ��,�����µ��룡");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    this.Alert("�鳧���ݵ�����������ϵ����Ա��");
                    return false;
                }
                finally
                {
                    table.Dispose();
                    //�����������д�����Ϣ�����赼��������Ϣ

                    DataTable td = ipdt.exportChAttendenceErrMsg(Session["uID"].ToString());
                    if (td.Rows.Count != 0)
                    {
                        string EXTitle = "100^<b>����</b>~^100^<b>����</b>~^60^<b>����</b>~^80^<b>����</b>~^100^<b>����</b>~^100^<b>�ϰ�ʱ��</b>~^100^<b>�°�ʱ��</b>~^100^<b>�ϰ�ʱ��1</b>~^100^<b>�°�ʱ��1</b>~^200^<b>����(A��Ϊ1����֮Ϊ0)1</b>~^200^<b>�������ƣ����ϰ�ʱ��ʱ�ÿգ���֮���硰��١���1</b>~^300^<b>������Ϣ</b>~^";
                        try
                        {
                            this.ExportExcel(EXTitle, td, false);
                        }
                        catch
                        {
                            this.Alert("Excel�������ݵ���ʧ�ܣ�����ϵ����Ա��");
                        }
                    }
                }
            }
        
        }
        
        #endregion

        return false;
        
    }
    #endregion

    #region ����1
    private bool ImportAttDay1(DataSet dsAtt, string strUid)
    {
        bool blAttday = false;
        int i = 0;
        bool blflag;
        string[] strOthers;
        strOthers = new string[4];

        for (i = 0; i <= dsAtt.Tables[0].Rows.Count - 1; i++)
        {
            blflag = false;
            string strDp, strWork, strUser, strName, strDate, strUserID, strTime;
            strUser = "";
            strName = "";
            strUserID = "";
            strDp = "";
            strWork = "";
            strDate = "";

            //judge department
            if (dsAtt.Tables[0].Rows[i].IsNull(0))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blAttday = true;
            }
            else
            {
                strDp = dsAtt.Tables[0].Rows[i].ItemArray[0].ToString().Trim();

                string str = "SELECT departmentID FROM tcpc" + Convert.ToString(Session["plantcode"]) + ".dbo.departments where name=N'" + strDp + "' And active=1 ";
                strDp = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, str).ToString();

                if (strDp.Length == 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", �������Ʋ����ڻ��д���", strUid);
                    blflag = true;
                    blAttday = true;
                }
            }
            //End department

            //judge workshop
            //if (dsAtt.Tables[0].Rows[i].IsNull(1))
            //{
            //    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",  ���鲻��Ϊ��", strUid);
            //    blflag = true;
            //    blAttday = true;
            //}   
            strWork = dsAtt.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
            //End workshop


            //judge User
            if (dsAtt.Tables[0].Rows[i].IsNull(2))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���Ų���Ϊ��", strUid);
                blflag = true;
                blAttday = true;
            }
            else
                strUser = dsAtt.Tables[0].Rows[i].ItemArray[2].ToString().Trim();


            if (dsAtt.Tables[0].Rows[i].IsNull(3))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ��������Ϊ��", strUid);
                blflag = true;
                blAttday = true;
            }
            else
                strName = dsAtt.Tables[0].Rows[i].ItemArray[3].ToString().Trim();

            if (strUser.Trim().Length != 0 && strName.Trim().Length != 0)
            {
                strUserID = ipdt.CheckUserInfo(strUser, strName, Convert.ToString(Session["plantcode"]));
                if (strUserID.Trim().Length == 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ź�������ƥ��򲻴���", strUid);
                    blflag = true;
                    blAttday = true;
                }
            }
            else
                strUserID = "";

            //End user

            //judge efficent date
            if (dsAtt.Tables[0].Rows[i].IsNull(4))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ڲ���Ϊ��", strUid);
                blflag = true;
                blAttday = true;
            }
            else
            {
                strDate = dsAtt.Tables[0].Rows[i].ItemArray[4].ToString().Trim();
                try
                {
                    DateTime date = Convert.ToDateTime(strDate);
                }
                catch
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",���ڷǷ�", strUid);
                    blflag = true;
                    blAttday = true;
                }
            }
            //End date


            //judge time for work
            bool bltime = false;
            bool blmatch = false;
            for (int j = 5; j < 9; j++)
            {
                if (!dsAtt.Tables[0].Rows[i].IsNull(j))
                    bltime = true;
            }

            if (bltime == true)
            {
                for (int j = 5; j < 9; j++)
                {
                    if (dsAtt.Tables[0].Rows[i].IsNull(j))
                    {
                        if (j % 2 == 0 && !blmatch)    // Find the matched number group
                        {
                            hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",ʱ�䲻��Ϊ��", strUid);
                            blflag = true;
                            blAttday = true;
                            blmatch = false;
                        }
                        else
                        {
                            if (j % 2 == 0)
                                blmatch = false;
                            else
                                blmatch = true;

                            strOthers[j - 5] = "";
                        }
                    }
                    else
                    {
                        if (j % 2 == 0 && blmatch)
                        {
                            hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",ʱ�䲻��Ϊ��", strUid);
                            blflag = true;
                            blAttday = true;
                            blmatch = false;
                        }
                        else
                        {
                            strTime = dsAtt.Tables[0].Rows[i].ItemArray[j].ToString().Trim();

                            if (!CheckTime("1999-09-09", strTime))
                            {
                                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",ʱ��Ƿ�", strUid);
                                blflag = true;
                                blAttday = true;
                            }
                            else
                            {
                                if (strTime.Trim().Length > 8)
                                    strOthers[j - 5] = Convert.ToDateTime(strTime).ToLongTimeString();
                                else
                                    strOthers[j - 5] = strTime;
                            }
                        }
                    }
                }
            }
            else
            {
                strOthers[0] = "";
                strOthers[1] = "";
                strOthers[2] = "";
                strOthers[3] = "";

                if (dsAtt.Tables[0].Rows[i].IsNull(10))
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",  ���Ʋ���Ϊ��", strUid);
                    blflag = true;
                    blAttday = true;
                }

            }
            // End judge time


            if (dsAtt.Tables[0].Rows[i].IsNull(9))
            {
                hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ",  ���಻��Ϊ��", strUid);
                blflag = true;
                blAttday = true;
            }



            if (!blflag)
            {

                if (ipdt.InsertAtt1(strDp, strWork, strDate, strUserID, strUser, strName, strOthers, dsAtt.Tables[0].Rows[i].ItemArray[9].ToString().Trim(), Convert.ToInt32(Session["uid"]), dsAtt.Tables[0].Rows[i].ItemArray[10].ToString().Trim()) < 0)
                {
                    hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���" + strOthers[0].ToString() + "/" + strOthers[1].ToString() + "/" + dsAtt.Tables[0].Rows[i].ItemArray[9].ToString().Trim(), strUid);
                    //hr.InsertErrorInfo("��:" + (i + 2).ToString().Trim() + ", ���ݵ������������µ���", strUid);
                    blflag = true;
                    blAttday = true;
                }
            }

        }

        dsAtt.Reset();
        return blAttday;
    }
    #endregion
    
    private bool CheckTime(string strDate, string strTime)
    {
        try
        {
            DateTime dtDate;
            if (strTime.Length >8)
                dtDate = Convert.ToDateTime(strTime);
            else
                dtDate = Convert.ToDateTime(strDate + " " + strTime);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool CheckDecimal(string strDec)
    {
        try
        {
            decimal decCh = Convert.ToDecimal(strDec);
            return true;
        }
        catch
        {
            return false;
        }
    }

    //private void PrintWrite(decimal[] decprint)
    //{
    //    for (int i = 5; i < decprint.Length; i++)
    //    {
    //        Response.Write(decprint[i].ToString() + "<br>");
    //    }
    //    decprint = new decimal[29]; 
    //}

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (dropDelete.SelectedIndex == 0)
        //{
        //    ltlAlert.Text = "alert('����ѡ�����!');";
        //    return;
        //}
        if (txtYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('������д���!');";
            return;
        }

        if (ipdt.DeleteCheckSalaryData(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), txtUserNo.Text.Trim(), Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(dropDelete.SelectedValue)) <= 0)
        {
            ltlAlert.Text = "alert('ɾ�����������²���!');";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('ɾ���ɹ�!');";
            txtUserNo.Text = "";
            dropDelete.SelectedIndex = 0;
        }
    }

    #region Setup Night Date

    protected void btnSchNight_Click(object sender, EventArgs e)
    {
        string strDate;
        if (txtYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('������д���!');";
            return;
        }

        Search();
    }


    protected void btnAddNight_Click(object sender, EventArgs e)
    {
        if (txtNightDate.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ҹ�����ڲ���Ϊ��!');";
            return;
        }
        else
        {
            try
            {
                DateTime date = Convert.ToDateTime(txtNightDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('ҹ�����ڸ�ʽ����Ӧ��Ϊ 1900-01-01 !');";
                return;
            }
        }

        Search();

        if (ipdt.DelADDNight(lblNight.Text.Trim(),txtNightDate.Text.Trim(),Convert.ToInt32(Session["uid"]),Convert.ToInt32(Session["plantcode"]),0,txtYear.Text,dropMonth.SelectedValue)<0 )
        {
            ltlAlert.Text = "alert('ҹ�������������������²��� !');";
            return;
        }

        ltlAlert.Text = "alert('ҹ���������ӳɹ�!');";
        txtNightDate.Text = "";

        Search();

    }

    protected void btnDelNight_Click(object sender, EventArgs e)
    {
        Search();
        if (ipdt.DelADDNight(lblNight.Text.Trim(), txtNightDate.Text.Trim(), Convert.ToInt32(Session["uid"]), Convert.ToInt32(Session["plantcode"]), 1, txtYear.Text, dropMonth.SelectedValue) < 0)
        {
            ltlAlert.Text = "alert('ҹ�������������������²��� !');";
            return;
        }

        Search();
    }



    private void Search()
    {
        string strDate;

        string str = "SELECT NightDate FROM tcpc" + Convert.ToString(Session["plantcode"]) + ".dbo.hr_ChLeaveDate WHERE ntype=-1 and Year(NightDate)='" + txtYear.Text.Trim() + "' AND Month(NightDate)='" + dropMonth.SelectedValue + "' ";
        strDate = Convert.ToString(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, str));

        if (strDate.Trim().Length == 0)
            lblNight.Text = "��ǰ������û��ȷ��ҹ���������";
        else
            lblNight.Text = "����ҹ���ֹ��"+Convert.ToDateTime(strDate).ToShortDateString();
    }

    #endregion
    
    #region Holiday Mantanance

    private void Bindholiday()
    {
        dropholiday.Items.Clear();

        ListItem item;
        item = new ListItem("--", "0");
        dropholiday.Items.Add(item);

        string str = "SELECT workdate FROM hr_ChDate Where Cyear='" + txtYear.Text.Trim() + "' AND cmonth='" + dropMonth.SelectedValue + "' And type =1 ";
        DataTable dtDropHoliday = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, str).Tables[0];
        if (dtDropHoliday.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropHoliday.Rows.Count; i++)
            {
                item = new ListItem(Convert.ToDateTime(dtDropHoliday.Rows[i].ItemArray[0]).ToShortDateString(),Convert.ToString(i+1));       
                dropholiday.Items.Add(item);
            }
        }
        dropholiday.SelectedIndex = 0;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Bindholiday();
    }

    protected void btnAddHoliday_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtHoliday.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('ҹ�����ڲ���Ϊ��!');";
                return;
            }
            else
            {
                try
                {
                    DateTime date = Convert.ToDateTime(txtHoliday.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('ҹ�����ڸ�ʽ����Ӧ��Ϊ 1900-01-01 !');";
                    return;
                }
            }

            Bindholiday();


            string str,strproc;

            //----------------------------------------------------------------------------------//
            //Get date in a month
            strproc = "sp_Hr_Chdate";
            SqlParameter[] parmArray = new SqlParameter[2];
            parmArray[0] = new SqlParameter("@Year", Convert.ToDateTime(txtHoliday.Text.Trim()).Year );
            parmArray[1] = new SqlParameter("@Month", Convert.ToDateTime(txtHoliday.Text.Trim()).Month );
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, strproc, parmArray);
            //----------------------------------------------------------------------------------//
            str = " IF NOT EXISTS (SELECT * FROM hr_ChDate WHERE type =1 AND workdate ='" + txtHoliday.Text.Trim() + "' )  UPDATE hr_ChDate SET type =1 WHERE workdate ='" + txtHoliday.Text.Trim() + "' ";
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, str);

            Bindholiday();
            txtHoliday.Text = "";
            ltlAlert.Text = "alert('�������ӳɹ�!');";
        }
        catch
        {
            ltlAlert.Text = "alert('�����������������²���!');";
            return;
        }
    }

    protected void btnDelHoliday_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "UPDATE hr_ChDate  SET type =0 WHERE workdate ='" + txtHoliday.Text.Trim() + "' ";
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, str);
            Bindholiday();
            txtHoliday.Text = "";
        }
        catch
        {
            ltlAlert.Text = "alert('����ɾ��ʧ�ܣ������²���!');";
            return;
        }
    }


    #endregion

    #region Attendance

    protected void btnAttendance_Click(object sender, EventArgs e)
    {
        string strFirstTime,strFirstMin, strSecondTime,strSecondMin,strThirdTime,strThirdMin,strFourTime,strFourMin;

        //----First Time -------------------------
        if (txtHour1.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ��Сʱ����Ϊ��!');Form1.txtHour1.focus();";
            return;
        }
        else
        {
            strFirstTime = txtHour1.Text.Trim();
            try
            {
                int intFirst = Convert.ToInt32(strFirstTime);

                if (intFirst > 24 || intFirst < 0)
                {
                    ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour1.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour1.focus();";
                return;
            }

        }

        if (txtMin1.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ����Ӳ���Ϊ��!');Form1.txtMin1.focus();";
            return;
        }
        else
        {
            strFirstMin = txtMin1.Text.Trim();
            try
            {
                int intFirstMin = Convert.ToInt32(strFirstMin);

                if (intFirstMin > 60 || intFirstMin < 0)
                {
                    ltlAlert.Text = "alert('ʱ��������벻�淶��Ӧ����0-60֮��!');Form1.txtMin1.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���Ӳ��淶��Ӧ����0-60֮��!');Form1.txtMin1.focus();";
                return;
            }

        }

        strFirstTime = strFirstTime + ":" + strFirstMin;


        //------Second Time --------------------------------
        if (txtHour2.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ��Сʱ����Ϊ��!');Form1.txtHour2.focus();";
            return;
        }
        else
        {
            strSecondTime = txtHour2.Text.Trim();
            try
            {
                int intSecond = Convert.ToInt32(strSecondTime);

                if (intSecond > 24 || intSecond < 0)
                {
                    ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour2.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour2.focus();";
                return;
            }

        }

        if (txtMin2.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ����Ӳ���Ϊ��!');Form1.txtMin2.focus();";
            return;
        }
        else
        {
            strSecondMin = txtMin2.Text.Trim();
            try
            {
                int intSecondMin = Convert.ToInt32(strSecondMin);

                if (intSecondMin > 60 || intSecondMin < 0)
                {
                    ltlAlert.Text = "alert('ʱ��������벻�淶��Ӧ����0-60֮��!');Form1.txtMin2.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���Ӳ��淶��Ӧ����0-60֮��!');Form1.txtMin2.focus();";
                return;
            }

        }

        strSecondTime = strSecondTime + ":" + strSecondMin;


        //--------Third Time----------------------------------------
        if (txtHour3.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ��Сʱ����Ϊ��!');Form1.txtHour3.focus();";
            return;
        }
        else
        {
            strThirdTime = txtHour3.Text.Trim();
            try
            {
                int intThird= Convert.ToInt32(strThirdTime);

                if (intThird > 24 || intThird < 0)
                {
                    ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour3.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour3.focus();";
                return;
            }

        }

        if (txtMin3.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ����Ӳ���Ϊ��!');Form1.txtMin3.focus();";
            return;
        }
        else
        {
            strThirdMin = txtMin3.Text.Trim();
            try
            {
                int intThirdMin = Convert.ToInt32(strThirdMin);

                if (intThirdMin > 60 || intThirdMin < 0)
                {
                    ltlAlert.Text = "alert('ʱ��������벻�淶��Ӧ����0-60֮��!');Form1.txtMin3.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���Ӳ��淶��Ӧ����0-60֮��!');Form1.txtMin3.focus();";
                return;
            }

        }

        strThirdTime = strThirdTime + ":" + strThirdMin;


        //------------Four Time-------------------------------------------
         if (txtHour4.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ��Сʱ����Ϊ��!');Form1.txtHour4.focus();";
            return;
        }
        else
        {
            strFourTime = txtHour4.Text.Trim();
            try
            {
                int intFour= Convert.ToInt32(strFourTime);

                if (intFour > 24 || intFour < 0)
                {
                    ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour4.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���벻�淶��Ӧ����0-24֮��!');Form1.txtHour4.focus();";
                return;
            }

        }

        if (txtMin4.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('ʱ����Ӳ���Ϊ��!');Form1.txtMin4.focus();";
            return;
        }
        else
        {
            strFourMin = txtMin4.Text.Trim();
            try
            {
                int intFourMin = Convert.ToInt32(strFourMin);

                if (intFourMin > 60 || intFourMin < 0)
                {
                    ltlAlert.Text = "alert('ʱ��������벻�淶��Ӧ����0-60֮��!');Form1.txtMin4.focus();";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('ʱ��Сʱ���Ӳ��淶��Ӧ����0-60֮��!');Form1.txtMin4.focus();";
                return;
            }

        }

        strFourTime = strFourTime + ":" + strFourMin;




        try
        {
            SqlParameter[] parmArray;
            //Get date in a month
            string strproc = "sp_Hr_Chdate";
            parmArray = new SqlParameter[2];
            parmArray[0] = new SqlParameter("@Year", Convert.ToInt32(txtYear.Text.Trim()));
            parmArray[1] = new SqlParameter("@Month", Convert.ToInt32(dropMonth.SelectedValue));
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, strproc, parmArray);

            string str = "sp_Hr_chSalary";
            parmArray = new SqlParameter[8];
            parmArray[0] = new SqlParameter("@Year",  Convert.ToInt32(txtYear.Text.Trim()));
            parmArray[1] = new SqlParameter("@Month", Convert.ToInt32(dropMonth.SelectedValue) );
            parmArray[2] = new SqlParameter("@start1", strFirstTime);
            parmArray[3] = new SqlParameter("@end1", strSecondTime);
            parmArray[4] = new SqlParameter("@start2",strThirdTime);
            parmArray[5] = new SqlParameter("@end2", strFourTime);
            parmArray[6] = new SqlParameter("@creat", Convert.ToInt32(Session["uid"]));
            parmArray[7] = new SqlParameter("@type", chkNormal.Checked ? 1 : 0);
            
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, str, parmArray);


            ltlAlert.Text = "alert('ת���������!');";
        }
        catch
        {
            string strsql = "DELETE FROM hr_ChAttendence Where Year(workdate)='" + txtYear.Text.Trim() + "' AND Month(workdate)='" + dropMonth.SelectedValue + "' ";
            SqlHelper.ExecuteNonQuery(chk.dsnx(),CommandType.Text,strsql);

            ltlAlert.Text = "alert('ת�����ڳ��������²���!');";
            return;
        }


        
    }

   
    #endregion
    
}
