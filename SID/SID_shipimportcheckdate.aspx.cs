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
using QADSID;
using System.Collections.Generic;


public partial class SID_shipimport1 : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);
        
        }
    }

    public Boolean ImportExcelFile1()
    {
        //String strSQL = "";
        DataSet ds = new DataSet();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        //Boolean boolError = false;
        int ErrorRecord = 0;


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
                return false;
            }

        }

        strUserFileName = filename2.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('��ѡ�����ļ�.');";
            return false;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27��Ψһ�ַ��������趨Ϊ��������ʱ������롱
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        #region ��SID_det_temp��ImportError�Ľṹ������
        DataColumn column;

        //����ImportError
        DataTable tblError = new DataTable("import_error");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "errInfo";
        tblError.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "uID";
        tblError.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "plantCode";
        tblError.Columns.Add(column);

        //����SID_det_temp
        DataTable tblTemp = new DataTable("checkdet_temp");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "nbr";
        tblTemp.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "so_nbr";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "so_line";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");//System.Type.GetType("System.DateTime");
        column.ColumnName = "finishedDate";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");//System.Type.GetType("System.DateTime");
        column.ColumnName = "checkedDate";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "createdby";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "createddate";
        tblTemp.Columns.Add(column);


        #endregion

        if (filename2.PostedFile != null)
        {
            if (filename2.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return false;
            }

            try
            {
                filename2.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return false;
            }



            if (File.Exists(strFileName))
            {
                DataRow rowError;//��������
                DataRow rowTemp;//��ʱ�����
                string ext = ShareDocument.GetFileExtension(strFileName);
                if (ext!="208207")
                {
                    //ltlAlert.Text = "alert('�����ļ�������Excel��ʽ����ģ�弰������ȷ!');";
                    //return false;

                    ErrorRecord += 1;

                    rowError = tblError.NewRow();

                    rowError["errInfo"] = "�����ļ�������Excel��ʽ����ģ�弰���ݲ���ȷ!";
                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                    tblError.Rows.Add(rowError);
                    
                    if (tblError != null && tblError.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopyError.DestinationTableName = "dbo.ImportError";
                            bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                            bulkCopyError.ColumnMappings.Add("uID", "userID");
                            bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                            try
                            {
                                bulkCopyError.WriteToServer(tblError);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('�������,����ϵϵͳ����Ա!');";
                                return false;
                            }
                            finally
                            {
                                bulkCopyError.Close();
                                tblError.Dispose();
                            }
                        }
                    }
                    return false;
                }
                // Get the WorkSheet Name
                String[] arrTable;
                arrTable = new string[20];

                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet." +
                               "OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + strFileName))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][2].ToString().Trim().Substring(dt.Rows[j][2].ToString().Trim().Length - 1, 1) == "$")
                        {
                            arrTable[j] = dt.Rows[j][2].ToString().Trim();
                        }

                    }
                    conn.Close();
                }

                foreach (string aa in arrTable)
                {
                    if (aa != null)
                    {
                        try
                        {
                            ds = sid.getExcelContents1(strFileName, aa.Replace("$", ""));
                        }
                        catch
                        {
                            if (File.Exists(strFileName))
                            {
                                File.Delete(strFileName);
                            }

                            ltlAlert.Text = "alert('�����ļ�������Excel��ʽ����ģ�弰������ȷ!');";
                            return false;
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Columns[0].ColumnName != "���˵���")
                            {
                                ds.Reset();
                                ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ!');";
                                return false;
                            }




                            rowTemp = tblTemp.NewRow();

                            String _Nbr = "";
                            String _id = "";
                            String _So_nbr = "";
                            String _So_line = "";
                            String _FinishedDate = "";
                            String _CheckDate = "";
          

                            string strErrMsg = string.Empty;//ǰ�˵Ĵ�����Ϣ
                            ErrorRecord = 0;


                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                _Nbr = "";
                                _id = "";
                                _So_nbr = "";
                                _So_line = "";
                                _FinishedDate = "";
                                _CheckDate = "";

                                #region ����ƻ�ȷ������
                                if (i >= 0)
                                {
                                    rowTemp = tblTemp.NewRow();

                                    //first three column is null, break
                                    if (ds.Tables[0].Rows[i].IsNull(0) && ds.Tables[0].Rows[i].IsNull(1) && ds.Tables[0].Rows[i].IsNull(2))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() == "" && ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim() == "" && ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim() == "")
                                        {
                                            break;
                                        }
                                    }

                                    //���
                                    if (ds.Tables[0].Rows[i].IsNull(0))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "���˵��Ų���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Nbr = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                                    }

                                    //�ͻ�����
                                    if (ds.Tables[0].Rows[i].IsNull(5))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "��Ų���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _id = ds.Tables[0].Rows[i].ItemArray[5].ToString().Trim();
                                    }

                                    //���۶���
                                    if (ds.Tables[0].Rows[i].IsNull(12))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "���۶�������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _So_nbr = ds.Tables[0].Rows[i].ItemArray[12].ToString().Trim();
                                    }

                                    //�к�
                                    if (ds.Tables[0].Rows[i].IsNull(13))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "�кŲ���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _So_line = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[13]).ToString();
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�кű���������,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //int useraccess = 0;
                                    //useraccess = sid.CheckImportDataUserAccess(Convert.ToInt32(Session["uID"]));


                                    //�깤ʱ��
                                    if (ds.Tables[0].Rows[i].IsNull(16) || string.IsNullOrEmpty(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()))
                                    {
                                        if (this.Security["550010021"].isValid)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�깤ʱ�����ڸ�ʽ����ȷ,�뽫��λ����Ϊ���ڸ�ʽ���ı���ʽ�����������硰2014-09-12 13:30��,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (this.Security["550010021"].isValid)
                                            {
                                                string stryear = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("yyyy");
                                                string strmonth = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("MM");
                                                string strday = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("dd");
                                                string strhour = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim()).ToString("HH:mm");
                                                //string str35 = str33 + "/" + str31.Substring(2, 2) + "/" + str32 +  " " + str34;
                                                string str37 = stryear.Substring(2, 2) + "/" + strmonth + "/" + strday + " " + strhour;
                                                string str36 = Convert.ToDateTime(str37).ToString("yyyy/MM/dd HH:mm");
                                                if (str36.Substring(0, 2) != "20" || str36.Substring(4, 1) != "/" || Convert.ToInt32(str36.Substring(0, 4)) < 2014 || strhour == "00:00")
                                                {
                                                    ErrorRecord += 1;

                                                    rowError = tblError.NewRow();

                                                    rowError["errInfo"] = "�깤ʱ���ʽ����ȷ,�뽫������λ����Ϊ���ڸ�ʽ���ı���ʽ�����������硰2014-09-12 13:30������" + aa + "��" + Convert.ToString(i + 2);
                                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                    tblError.Rows.Add(rowError);
                                                }
                                                //Convert.ToDateTime(dts.Rows[i].ItemArray[8].ToString().Trim()).ToString();
                                                _FinishedDate = str36;//Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[15].ToString().Trim()).ToString();
                                            }
                                            else
                                            {
                                                _FinishedDate = null ;
                                            }
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�깤ʱ���ʽ����ȷ,�뽫������λ����Ϊ���ڸ�ʽ���ı���ʽ�����������硰2014-09-12 13:30������" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                            //ltlAlert.Text = "alert('���ڸ�ʽ����ȷ,��������д!');";
                                            //return false;
                                        }
                                    }
                                    //ȷ�����ڲ��ô��ڳ�������
                                    int Ierr1 = 0;
                                    if (_FinishedDate == null)
                                    {
                                        Ierr1 = 1;
                                    }
                                    else
                                    {
                                        Ierr1 = sid.CheckImportDataNotMax(_Nbr, _id, _So_nbr, _So_line, _FinishedDate);
                                    }
                                    if (Ierr1 < 0)
                                    {
                                        if (Ierr1 == -1)
                                        {
                                            //ErrorRecord += 1;

                                            //rowError = tblError.NewRow();

                                            //rowError["errInfo"] = "������ȷ�ϲ����ظ�����,����" + aa + "��" + Convert.ToString(i + 2);
                                            //rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            //rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            //tblError.Rows.Add(rowError);
                                        }
                                        else
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�깤ʱ�䲻�ô��ڳ���ʱ��,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }

                                    }

                                    //����ִ�ʱ��
                                    if (ds.Tables[0].Rows[i].IsNull(17) || string.IsNullOrEmpty(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()))
                                    {
                                        if (this.Security["550010022"].isValid)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "����ִ�ʱ���ʽ����ȷ,�뽫��λ����Ϊ���ڸ�ʽ���ı���ʽ�����������硰2014-09-12 13:30��,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (this.Security["550010022"].isValid)
                                            {
                                                string str31 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("yyyy");
                                                string str32 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("MM");
                                                string str33 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("dd");
                                                string str34 = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim()).ToString("HH:mm");
                                                //string str35 = str33 + "/" + str31.Substring(2, 2) + "/" + str32 +  " " + str34;
                                                string str37 = str31.Substring(2, 2) + "/" + str32 + "/" + str33 + " " + str34;
                                                string str36 = Convert.ToDateTime(str37).ToString("yyyy/MM/dd HH:mm");
                                                if (str36.Substring(0, 2) != "20" || str36.Substring(4, 1) != "/" || Convert.ToInt32(str36.Substring(0, 4)) < 2014 || str34 == "00:00")
                                                {
                                                    ErrorRecord += 1;

                                                    rowError = tblError.NewRow();

                                                    rowError["errInfo"] = "����ִ�ʱ���ʽ����ȷ,�뽫��λ����Ϊ���ڸ�ʽ���ı���ʽ�����������硰2014-09-12 13:30������" + aa + "��" + Convert.ToString(i + 2);
                                                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                    tblError.Rows.Add(rowError);
                                                }
                                                //Convert.ToDateTime(dts.Rows[i].ItemArray[8].ToString().Trim()).ToString();
                                                _CheckDate = str36;//Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[15].ToString().Trim()).ToString();
                                            }
                                            else
                                            {
                                                _CheckDate = null;
                                            }
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "����ִ�ʱ���ʽ����ȷ,�뽫��λ����Ϊ���ڸ�ʽ���ı���ʽ�����������硰2014-09-12 13:30������" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                            //ltlAlert.Text = "alert('���ڸ�ʽ����ȷ,��������д!');";
                                            //return false;
                                        }
                                        if (this.Security["550010021"].isValid && this.Security["550010022"].isValid && !string.IsNullOrEmpty(_FinishedDate) && !string.IsNullOrEmpty(_CheckDate))
                                        {
                                            if (Convert.ToDateTime(_FinishedDate) > Convert.ToDateTime(_CheckDate))
                                            {
                                                ErrorRecord += 1;

                                                rowError = tblError.NewRow();

                                                rowError["errInfo"] = "�깤ʱ�䲻�ô��ڻ���ִ�ʱ�䡱����" + aa + "��" + Convert.ToString(i + 2);
                                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                tblError.Rows.Add(rowError);
                                            }
                                        }
                                    }

                                    //�ж������Ƿ���ά��
                                    int Ierr = 0;
                                    if (this.Security["550010022"].isValid)
                                    {
                                        Ierr = 1;
                                    }
                                    else
                                    {
                                        Ierr = sid.CheckImportDataExist(_Nbr, _id, _So_nbr, _So_line);
                                    }
                                    if (Ierr < 0)
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "������ȷ�ϲ����ٵ���,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);

                                    }

                                    //ȷ�����ڲ��ô��ڳ�������
                                    Ierr = 0;
                                    Ierr = sid.CheckImportDataNotMax(_Nbr, _id, _So_nbr, _So_line, _CheckDate);
                                    if (Ierr < 0)
                                    {
                                        if (Ierr == -1)
                                        {
                                            //ErrorRecord += 1;

                                            //rowError = tblError.NewRow();

                                            //rowError["errInfo"] = "������ȷ�ϲ����ظ�����,����" + aa + "��" + Convert.ToString(i + 2);
                                            //rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            //rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            //tblError.Rows.Add(rowError);
                                        }
                                        else
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "����ִ�ʱ�䲻�ô��ڳ���ʱ��,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }

                                    }

                                    if (ErrorRecord <= 0)
                                    {
                                        rowTemp = tblTemp.NewRow();

                                        rowTemp["nbr"] = _Nbr;
                                        rowTemp["id"] = _id;
                                        rowTemp["so_nbr"] = _So_nbr;
                                        rowTemp["so_line"] = _So_line;
                                        rowTemp["finishedDate"] = _FinishedDate;
                                        rowTemp["checkedDate"] = _CheckDate;
                                        rowTemp["createdby"] = Convert.ToInt32(Session["uID"]);
                                        rowTemp["createddate"] = DateTime.Now;

                                        tblTemp.Rows.Add(rowTemp);
                                    }
                                }
                                #endregion
                            }
                        } //ds.Tables[0].Rows.Count > 0                           
                    } // a != null)
                    ds.Reset();
                } //foreach

                //�ϴ�
                if (tblError != null && tblError.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                    {
                        bulkCopyError.DestinationTableName = "dbo.ImportError";
                        bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                        bulkCopyError.ColumnMappings.Add("uID", "userID");
                        bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                        try
                        {
                            bulkCopyError.WriteToServer(tblError);
                        }
                        catch (Exception ex)
                        {
                            ltlAlert.Text = "alert('�������,����ϵϵͳ����Ա!');";
                            return false;
                        }
                        finally
                        {
                            bulkCopyError.Close();
                            tblError.Dispose();
                        }
                    }
                }

                if (tblTemp != null && tblTemp.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopyTemp = new SqlBulkCopy(chk.dsn0()))
                    {
                        bulkCopyTemp.DestinationTableName = "dbo.SID_checkdate_temp";
                        bulkCopyTemp.ColumnMappings.Add("nbr", "SID_nbr");
                        bulkCopyTemp.ColumnMappings.Add("id", "SID_id");
                        bulkCopyTemp.ColumnMappings.Add("so_nbr", "SID_so_nbr");
                        bulkCopyTemp.ColumnMappings.Add("so_line", "SID_so_line");
                        bulkCopyTemp.ColumnMappings.Add("finishedDate", "SID_finsheddate");
                        bulkCopyTemp.ColumnMappings.Add("checkedDate", "SID_checkeddate");
                        bulkCopyTemp.ColumnMappings.Add("createdBy", "SID_createdby");
                        bulkCopyTemp.ColumnMappings.Add("createdDate", "SID_createddate");

                        try
                        {
                            bulkCopyTemp.WriteToServer(tblTemp);
                        }
                        catch (Exception ex)
                        {
                            ltlAlert.Text = "alert('�������,����ϵϵͳ����Ա!');";
                            return false;
                        }
                        finally
                        {
                            bulkCopyTemp.Close();
                            tblTemp.Dispose();
                        }
                    }
                }

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            } //File.Exists(strFileName)
        } //filename2.PostedFile != null

        if (ErrorRecord <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    protected void BtnShip1_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(filename2.PostedFile.FileName))
        {
            ltlAlert.Text = "alert('�����ϴ����ĵ�!');";
            return;
        }
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        string strCatFolder = Server.MapPath("/import");
        string strUserFileName = filename2.PostedFile.FileName;
        string strFileName = strCatFolder + "\\" + strKey + strUserFileName;
        try
        {
            filename2.PostedFile.SaveAs(strFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
            return;
        }
        string ext = ShareDocument.GetFileExtension(strFileName);
        if (ext != "208207")
        {
            ltlAlert.Text = "alert('�ĵ���ʽ����ȷ������Ϊ.xsl�ļ�!');";
            return;
        }


        //int useraccess = 0;
        //useraccess = sid.CheckImportDataUserAccess(Convert.ToInt32(Session["uID"]));
        if (!this.Security["550010021"].isValid && !this.Security["550010022"].isValid)
        {
            ltlAlert.Text = "alert('�޲���Ȩ�ޣ���������Ȩ�ޣ�');";
            return;
        }
        sid.DelTempCheckedDateInfo(Convert.ToInt32(Session["uID"]));
        sid.DelImportError(Convert.ToInt32(Session["uID"]));

        int ErrorRecord = 0;

        if (!ImportExcelFile1())
        {
            ErrorRecord += 1;
        }

        if (ErrorRecord == 0)
        {
            Int32 Ierr = 0;
            bool finished = this.Security["550010021"].isValid;
            bool arrived = this.Security["550010022"].isValid;
            Ierr = sid.ImportCheckData(Convert.ToInt32(Session["uID"]),finished,arrived);
            if (Ierr < 0)
            {
                string Ierr1 = "";
                Ierr1 = sid.ImportCheckDataExsit(Convert.ToInt32(Session["uID"]));
                ltlAlert.Text = "alert('�ͻ�����"+Ierr1+"�ͻ�ʱ����ȷ��,�����޸�����ϵ������ƻ���');";
            }
            else
            {
                ltlAlert.Text = "alert('����ɹ�!');";
            }

        }
        else
        {
            Session["EXTitle"] = "500^<b>����ԭ��</b>~^";
            Session["EXHeader"] = "";
            Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
    }
}
