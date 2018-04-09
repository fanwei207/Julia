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
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;

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
    protected void BtnShip_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        sid.DelTempShip(Convert.ToInt32(Session["uID"]));
        sid.DelImportError(Convert.ToInt32(Session["uID"]));

        int ErrorRecord = 0;

        if (!ImportExcelFile())
        {
            ErrorRecord += 1;

        }


        if (ErrorRecord == 0)
        {
            Int32 Ierr = 0;

            Ierr = sid.ImportShipData(Convert.ToInt32(Session["uID"]));
            if (Ierr < 0)
            {
                ltlAlert.Text = "alert('�ظ�����������');";

            }
            else
            {
                if (sid.IsExistsAirShip(Convert.ToInt32(Session["uID"])))
                {
                    Session["EXTitle"] = "500^<b>ϵͳ��ʾ</b>~^";
                    Session["EXHeader"] = "";
                    Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                    ltlAlert.Text = "alert('����ɹ�!���о���!');window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
                }
                else
                {
                    ltlAlert.Text = "alert('����ɹ�!');";
                }
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

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if(strFileName.Trim().Length <= 0)
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
        DataTable tblTemp = new DataTable("det_temp");
        
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sno";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qad";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_set";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_box";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qa";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "memo";
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
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PO";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "cust_part";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "weight";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "volume";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pcs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pkgs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fob";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fedx";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "no";
        tblTemp.Columns.Add(column); 
        
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "atl";
        tblTemp.Columns.Add(column);
        
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "createdBy";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "createdDate";
        tblTemp.Columns.Add(column);
        #endregion

        if (filename1.PostedFile != null)
        {
           if(filename1.PostedFile.ContentLength > 8388608)
           {
               ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
               return false;
           }

           try
           {
               filename1.PostedFile.SaveAs(strFileName);
           }
           catch
           {
               ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
               return false ;
           }

         

           if (File.Exists(strFileName))
           {
               //�Ȱ��̼��ȡ����
               IList<QADSID.SID_QA> qaList = sid.SelectNeedQASNO(string.Empty);

               if (qaList == null)
               {
                   ltlAlert.Text = "alert('�̼�Ż�ȡʧ��!��ˢ�º����²���һ�Σ�');";
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
                           if (dt.Rows[j][2].ToString().Trim().Substring(dt.Rows[j][2].ToString().Trim().Length - 1,1) == "$")
                           {
                               arrTable[j] = dt.Rows[j][2].ToString().Trim();
                           }
                           
                       }
                       conn.Close();
                   }

                   foreach (string aa in arrTable)
                   {
                       if (aa != null )
                       {
                           
                           try
                           {
                               ds = sid.getExcelContents(strFileName, aa);
                           }
                           catch
                           {
                               if (File.Exists(strFileName))
                               {
                                   File.Delete(strFileName);
                               }

                               ltlAlert.Text = "alert('�����ļ�������Excel��ʽ����ģ�弰������ȷ!" + aa + "');";
                               return false;
                           }

                           if (ds.Tables[0].Rows.Count > 0)
                           {
                               if (ds.Tables[0].Columns[0].ColumnName != "��Ʒ���˵�")
                               {
                                   ds.Reset();
                                   ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ!');";
                                   return false;
                               }

                               DataRow rowError;//��������
                               DataRow rowTemp;//��ʱ�����

                               
                               rowTemp = tblTemp.NewRow();

                               String _PK = "";
                               String _PKref = "";
                               String _Nbr = "";
                               String _OutDate = "";
                               String _Via = "";
                               String _Ctype = "";
                               String _ShipDate = "";
                               String _ShipTo = "";
                               String _Site = "";
                               String _SNO = "";
                               String _QAD = "";
                               Decimal _Qty_set = 0;
                               Decimal _Qty_box = 0;
                               String _QA = "";
                               String _Memo = "";
                               String _So_nbr = "";
                               String _So_line = "";
                               String _WO = "";
                               String _PO = "";
                               String _Cust_part = "";
                               Decimal _Weight = 0;
                               Decimal _Volume = 0;
                               Decimal _qty_pkgs = 0;
                               Decimal _qty_pcs = 0;
                               String _domain = "";
                               String _Fob = "";
                               String _Fedx = "";
                               Int32 SID = 0;
                               String _NO = "";
                               String _ATL = "";
                               String _LCL = "";

                               string strErrMsg = string.Empty;//ǰ�˵Ĵ�����Ϣ
                               ErrorRecord = 0;


                               for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                               {
                                   _SNO = "";
                                   _QAD = "";
                                   _Qty_set = 0;
                                   _Qty_box = 0;
                                   _QA = "";
                                   _Memo = "";
                                   _So_nbr = "";
                                   _So_line = "";
                                   _WO = "";
                                   _PO = "";
                                   _Cust_part = "";
                                   _Weight = 0;
                                   _Volume = 0;
                                   _qty_pcs = 0;
                                   _qty_pkgs = 0;
                                   _NO = "";
                                   _ATL = "";

                                   #region ����ͷ��
                                   if (i == 0)
                                   {
                                       if (ds.Tables[0].Rows[i].IsNull(0) || ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() != "���˵���:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "���˵�λ�ò���,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Nbr = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                           if (_Nbr.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "���˵�����Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(3) || ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim() != "ϵͳ���˵���:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "ϵͳ���˵�λ�ò���,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _PK = ds.Tables[0].Rows[i].ItemArray[5].ToString().Trim();
                                           if (_PK.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "ϵͳ���˵�����Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(7) || ds.Tables[0].Rows[i].ItemArray[7].ToString().Trim() != "��������:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "��������λ�ò���,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _OutDate = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                           if (_OutDate.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "�������ڲ���Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }

                                           Int32 strdate = 0;
                                           Int32 str2mon = 0;
                                           Int32 str3day = 0;
                                           Int32 str4hour = 0;
                                           try
                                           {
                                               strdate = Convert.ToInt32(_OutDate.Trim().Substring(0, 2));
                                               str2mon = Convert.ToInt32(_OutDate.Trim().Substring(3, 2));
                                               str3day = Convert.ToInt32(_OutDate.Trim().Substring(6, 2));
                                               str4hour = Convert.ToInt32(_OutDate.Trim().Substring(11, 2));
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "�������ڸ�ʽ����ȷ, ����Ϊ14-12-01   05��װ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }


                                           //if (Convert.ToInt32(_OutDate.Trim().Substring(0, 2)) < 14 || Convert.ToInt32(_OutDate.Trim().Substring(3, 2)) > 12 || Convert.ToInt32(_OutDate.Trim().Substring(6, 2)) > 31 || Convert.ToInt32(_OutDate.Trim().Substring(11, 2)) > 24)
                                           if (strdate < 14 || str2mon > 12 || str3day > 31 || str4hour > 24)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "�������ڲ���Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                           _LCL = ds.Tables[0].Rows[i].ItemArray[13].ToString().Trim(); //dt.Rows[i].ItemArray[13].ToString().Trim();
                                       }
                                   }

                                   if (i == 1)
                                   {
                                       if (ds.Tables[0].Rows[i].IsNull(0) || ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() != "���䷽ʽ:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "���䷽ʽλ�ò���,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Via = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                           if (_Via.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "���䷽ʽ����Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(3) || ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim() != "��������:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "��������λ�ò���,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Ctype = ds.Tables[0].Rows[i].ItemArray[5].ToString().Trim();
                                           if (_Ctype.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "�������Ͳ���Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(7) || ds.Tables[0].Rows[i].ItemArray[7].ToString().Trim() != "��������:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "��������λ�ò���,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _ShipDate = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                           if (_ShipDate.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "�������ڲ���Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                           else
                                           {
                                               if (!sid.IsDate(_ShipDate.Trim()))
                                               {
                                                   ErrorRecord += 1;

                                                   rowError = tblError.NewRow();

                                                   rowError["errInfo"] = "�������ڱ���Ϊ��������,����" + aa;
                                                   rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                   rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                   tblError.Rows.Add(rowError);
                                               }
                                           }
                                       }

                                   }

                                   if (i == 2)
                                   {
                                       if (ds.Tables[0].Rows[i].IsNull(0) || ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim() != "����:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "����λ�ò���,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _ShipTo = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                           if (_ShipTo.Trim().Length <= 0)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "����λ�ò���Ϊ��,����" + aa;
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(7) || ds.Tables[0].Rows[i].ItemArray[7].ToString() != "װ��ص�:")
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "װ��ص㲻��,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Site = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                       }

                                       if (ds.Tables[0].Rows[i].IsNull(11))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "�������ڵĹ�˾,����" + aa;
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _domain = ds.Tables[0].Rows[i].ItemArray[11].ToString().Trim();
                                       }

                                       if (_Nbr.Length > 0)
                                       {
                                           if (Char.IsLetter(_Nbr, _Nbr.Length - 1))
                                           {
                                               _PKref = _Nbr.Substring(_Nbr.Length - 1, 1);
                                           }
                                           else
                                           {
                                               _PKref = "";
                                           }
                                       }

                                       if (ErrorRecord <= 0)
                                       {
                                           SID = sid.InsertTempMstr(Convert.ToInt32(Session["uID"]), _PK, _Nbr, _OutDate, _Via, _Ctype, _ShipDate, _ShipTo, _Site, _domain, _PKref,_LCL);
                                           if (SID <= 0)
                                           {
                                               ds.Reset();
                                               ltlAlert.Text = "alert('�������,����ϵϵͳ����Ա!');";
                                               break;//�����������Ե���������ʾ
                                           }
                                       }
                                   }
                                   #endregion
                                   
                                   #region ������ϸ��
                                   if (i >= 5)
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

                                           rowError["errInfo"] = "��Ų���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _NO = ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim();
                                       }
                                       //ϵ��
                                       if (ds.Tables[0].Rows[i].IsNull(1))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "ϵ�в���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _SNO = ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim();
                                       }


                                       //QAD
                                       if (ds.Tables[0].Rows[i].IsNull(2))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "QAD����Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _QAD = ds.Tables[0].Rows[i].ItemArray[2].ToString().Trim();
                                       }

                                       //�ͻ�����
                                       if (ds.Tables[0].Rows[i].IsNull(3))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "�ͻ����ϲ���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Cust_part = ds.Tables[0].Rows[i].ItemArray[3].ToString().Trim();
                                       }

                                       //����
                                       if (ds.Tables[0].Rows[i].IsNull(4))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "������������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Qty_set = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[4]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //ֻ��
                                       if (ds.Tables[0].Rows[i].IsNull(5))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "ֻ������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _qty_pcs = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[5]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "ֻ������������,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //����
                                       if (ds.Tables[0].Rows[i].IsNull(6))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "��������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Qty_box = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[6]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //����
                                       if (ds.Tables[0].Rows[i].ItemArray[7].ToString().Trim().Length <= 0)
                                       {
                                           _qty_pkgs = 0;
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _qty_pkgs = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[7]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //�̼�� 
                                       if (ds.Tables[0].Rows[i].IsNull(8))
                                       {
                                           _QA = "";
                                       }
                                       else
                                       {
                                           _QA = ds.Tables[0].Rows[i].ItemArray[8].ToString().Trim();
                                       }
                                       
                                       bool bQa = false;//��ʶ�̼���Ƿ���qaList�б���
                                       foreach (QADSID.SID_QA sqa in qaList)//_SNO.Trim()) && )
                                       {
                                           if (sqa.SNO == _SNO.Trim())
                                           {
                                               bQa = true;
                                           }
                                       }

                                       if (bQa && _QA.Trim().Length == 0)
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = _SNO.Trim() + "ϵ�е��̼�Ų���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }

                                       //��ע
                                       if (ds.Tables[0].Rows[i].IsNull(9))
                                       {
                                           _Memo = "";
                                       }
                                       else
                                       {
                                           _Memo = ds.Tables[0].Rows[i].ItemArray[9].ToString().Trim();
                                       }

                                       //���۶���
                                       if (ds.Tables[0].Rows[i].IsNull(10))
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
                                           _So_nbr = ds.Tables[0].Rows[i].ItemArray[10].ToString().Trim();
                                       }

                                       //�к�
                                       if (ds.Tables[0].Rows[i].IsNull(11))
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
                                               _So_line = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[11]).ToString();
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

                                       //�����/�ӹ���
                                       if (ds.Tables[0].Rows[i].IsNull(12))
                                       {
                                           _WO = "";
                                       }
                                       else
                                       {
                                           _WO = ds.Tables[0].Rows[i].ItemArray[12].ToString().Trim();
                                       }

                                       //TCP������
                                       if (ds.Tables[0].Rows[i].IsNull(13))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "�ͻ������Ų���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _PO = ds.Tables[0].Rows[i].ItemArray[13].ToString().Trim();
                                       }

                                       //QAD�Ƿ����EDI_DB..CP_PART
                                       if (!sid.ExistsQADShipImport(_QAD, _So_nbr, _So_line, _PO, Convert.ToInt32(Session["uID"])))
                                       {
                                           //ErrorRecord += 1;
                                           sid.InsertErrorInfo("�ö����ͻ����������ϱ��в����ڣ�����ϵ���������ӣ��˴�������ʾ��", aa, Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                       }

                                       //����
                                       if (ds.Tables[0].Rows[i].IsNull(14))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "��������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Weight = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[14]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //���
                                       if (ds.Tables[0].Rows[i].IsNull(15))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "�������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           try
                                           {
                                               _Volume = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[15]);
                                           }
                                           catch
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "�������������,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //�ͻ�������
                                       if (ds.Tables[0].Rows[i].IsNull(16))
                                       {
                                           _Fob = "";
                                       }
                                       else
                                       {
                                           _Fob = ds.Tables[0].Rows[i].ItemArray[16].ToString().Trim();
                                       }

                                       //ATL������
                                       if (ds.Tables[0].Rows[i].IsNull(17))
                                       {
                                           _ATL = "";
                                       }
                                       else
                                       {
                                           _ATL = ds.Tables[0].Rows[i].ItemArray[17].ToString().Trim();
                                       }

                                       //Fedx���ٺ�
                                       if (ds.Tables[0].Rows[i].IsNull(18))
                                       {
                                           _Fedx = "";
                                       }
                                       else
                                       {
                                           _Fedx = ds.Tables[0].Rows[i].ItemArray[18].ToString().Trim();
                                       }

                                       //����
                                       if (_Via.ToUpper() != "A")
                                       {
                                           if (sid.IsAirShipImport(_PO, _So_line))
                                           {
                                               //ErrorRecord += 1;
                                               sid.InsertErrorInfo("�ö��������Ѿ�����Ϊ���ˣ�ϵͳ�Ѿ�Ϊ�������ȥ���˴�������ʾ��", aa, Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                           }
                                       }

                                       if (ErrorRecord <= 0)
                                       {
                                           rowTemp = tblTemp.NewRow();

                                           rowTemp["id"] = SID;
                                           rowTemp["SNO"] = _SNO;
                                           rowTemp["QAD"] = _QAD;
                                           rowTemp["qty_set"] = _Qty_set;
                                           rowTemp["qty_box"] = _Qty_box;
                                           rowTemp["qa"] = _QA;
                                           rowTemp["memo"] = _Memo;
                                           rowTemp["so_nbr"] = _So_nbr;
                                           rowTemp["so_line"] = _So_line;
                                           rowTemp["wo"] = _WO;
                                           rowTemp["PO"] = _PO;
                                           rowTemp["cust_part"] = _Cust_part;
                                           rowTemp["weight"] = _Weight;
                                           rowTemp["volume"] = _Volume;
                                           rowTemp["qty_pcs"] = _qty_pcs;
                                           rowTemp["qty_pkgs"] = _qty_pkgs;
                                           rowTemp["fob"] = _Fob;
                                           rowTemp["fedx"] = _Fedx;
                                           rowTemp["no"] = _NO;
                                           rowTemp["atl"] = _ATL;
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
                           bulkCopyTemp.DestinationTableName = "dbo.SID_det_temp";
                           bulkCopyTemp.ColumnMappings.Add("id", "SID_id");
                           bulkCopyTemp.ColumnMappings.Add("sno", "SID_SNO");
                           bulkCopyTemp.ColumnMappings.Add("qad", "SID_QAD");
                           bulkCopyTemp.ColumnMappings.Add("qty_set", "SID_qty_set");
                           bulkCopyTemp.ColumnMappings.Add("qty_box", "SID_qty_box");
                           bulkCopyTemp.ColumnMappings.Add("qa", "SID_qa");
                           bulkCopyTemp.ColumnMappings.Add("memo", "SID_memo");
                           bulkCopyTemp.ColumnMappings.Add("so_nbr", "SID_so_nbr");
                           bulkCopyTemp.ColumnMappings.Add("so_line", "SID_so_line");
                           bulkCopyTemp.ColumnMappings.Add("wo", "SID_wo");
                           bulkCopyTemp.ColumnMappings.Add("PO", "SID_PO");
                           bulkCopyTemp.ColumnMappings.Add("cust_part", "SID_cust_part");
                           bulkCopyTemp.ColumnMappings.Add("weight", "SID_weight");
                           bulkCopyTemp.ColumnMappings.Add("volume", "SID_volume");
                           bulkCopyTemp.ColumnMappings.Add("qty_pcs", "SID_qty_pcs");
                           bulkCopyTemp.ColumnMappings.Add("qty_pkgs", "SID_qty_pkgs");
                           bulkCopyTemp.ColumnMappings.Add("fob", "SID_fob");
                           bulkCopyTemp.ColumnMappings.Add("fedx", "SID_fedx");
                           bulkCopyTemp.ColumnMappings.Add("no", "SID_no");
                           bulkCopyTemp.ColumnMappings.Add("atl", "SID_atl");
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
        } //filename1.PostedFile != null
        
        if (ErrorRecord <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public Boolean ImportExcelFile()
    {
        //String strSQL = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
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

        strUserFileName = filename1.PostedFile.FileName;
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
        DataTable tblTemp = new DataTable("det_temp");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sno";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qad";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_set";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_box";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qa";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "memo";
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
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PO";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "cust_part";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "weight";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "volume";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pcs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "qty_pkgs";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fob";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fedx";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "no";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "atl";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qadpo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "qadline";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "createdBy";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "createdDate";
        tblTemp.Columns.Add(column);
        #endregion

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return false;
            }



            if (File.Exists(strFileName))
            {
                //�Ȱ��̼��ȡ����
                IList<QADSID.SID_QA> qaList = sid.SelectNeedQASNO(string.Empty);

                if (qaList == null)
                {
                    ltlAlert.Text = "alert('�̼�Ż�ȡʧ��!��ˢ�º����²���һ�Σ�');";
                    return false;
                }

                // Get the WorkSheet Name
                int[] arrTable;

                using (FileStream sr = new FileStream(strFileName, FileMode.OpenOrCreate))
                {   //����·��ͨ���Ѵ��ڵ�excel������HSSFWorkbook��������excel�ĵ�   
                    HSSFWorkbook workbook = new HSSFWorkbook(sr);
                    int x = workbook.Workbook.NumSheets;
                    arrTable = new int[x];
                    List<string> sheetNames = new List<string>();
                    for (int j = 0; j < x; j++)
                    {
                        arrTable[j] = j;//workbook.Workbook.GetSheetName(j);
                    }
                }


                #region //ѭ������Sheet
                foreach (int aa in arrTable)
                {
                    if (aa != null)
                    {

                        try
                        {
                            //ds = sid.getExcelContents(strFileName, aa);
                            dt = this.GetExcelContents(strFileName, aa);
                        }
                        catch
                        {
                            if (File.Exists(strFileName))
                            {
                                File.Delete(strFileName);
                            }

                            ltlAlert.Text = "alert('�����ļ�������Excel��ʽ����ģ�弰������ȷ!" + aa + "');";
                            return false;
                        }

                        #region //��ȡTable����
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Columns[0].ColumnName != "��Ʒ���˵�")
                            {
                                ds.Reset();
                                ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ!');";
                                return false;
                            }

                            DataRow rowError;//��������
                            DataRow rowTemp;//��ʱ�����


                            rowTemp = tblTemp.NewRow();

                            String _PK = "";
                            String _PKref = "";
                            String _Nbr = "";
                            String _OutDate = "";
                            String _Via = "";
                            String _Ctype = "";
                            String _ShipDate = "";
                            String _ShipTo = "";
                            String _Site = "";
                            String _SNO = "";
                            String _QAD = "";
                            Decimal _Qty_set = 0;
                            Decimal _Qty_box = 0;
                            String _QA = "";
                            String _Memo = "";
                            String _So_nbr = "";
                            String _So_line = "";
                            String _WO = "";
                            String _PO = "";
                            String _Cust_part = "";
                            Decimal _Weight = 0;
                            Decimal _Volume = 0;
                            Decimal _qty_pkgs = 0;
                            Decimal _qty_pcs = 0;
                            String _domain = "";
                            String _Fob = "";
                            String _Fedx = "";
                            Int32 SID = 0;
                            String _NO = "";
                            String _ATL = "";
                            String _QADPO = "";
                            String _QADLine = "";
                            String _LCL = "";

                            string strErrMsg = string.Empty;//ǰ�˵Ĵ�����Ϣ
                            ErrorRecord = 0;


                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                _SNO = "";
                                _QAD = "";
                                _Qty_set = 0;
                                _Qty_box = 0;
                                _QA = "";
                                _Memo = "";
                                _So_nbr = "";
                                _So_line = "";
                                _WO = "";
                                _PO = "";
                                _Cust_part = "";
                                _Weight = 0;
                                _Volume = 0;
                                _qty_pcs = 0;
                                _qty_pkgs = 0;
                                _NO = "";
                                _ATL = "";
                                _QADPO = "";
                                _QADLine = "";

                                #region ����ͷ��
                                if (i == 0)
                                {
                                    if (dt.Rows[i].IsNull(0) || dt.Rows[i].ItemArray[0].ToString().Trim() != "���˵���:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "���˵�λ�ò���,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Nbr = dt.Rows[i].ItemArray[2].ToString().Trim();
                                        if (_Nbr.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "���˵�����Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(3) || dt.Rows[i].ItemArray[3].ToString().Trim() != "ϵͳ���˵���:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "ϵͳ���˵�λ�ò���,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _PK = dt.Rows[i].ItemArray[5].ToString().Trim();
                                        if (_PK.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "ϵͳ���˵�����Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(7) || dt.Rows[i].ItemArray[7].ToString().Trim() != "��������:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "��������λ�ò���,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _OutDate = dt.Rows[i].ItemArray[9].ToString().Trim();
                                        if (_OutDate.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�������ڲ���Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }

                                        Int32 strdate = 0;
                                        Int32 str2mon = 0;
                                        Int32 str3day = 0;
                                        Int32 str4hour = 0;
                                        try
                                        {
                                            strdate = Convert.ToInt32(_OutDate.Trim().Substring(0, 2));
                                            str2mon = Convert.ToInt32(_OutDate.Trim().Substring(3, 2));
                                            str3day = Convert.ToInt32(_OutDate.Trim().Substring(6, 2));
                                            str4hour = Convert.ToInt32(_OutDate.Trim().Substring(11, 2));
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�������ڸ�ʽ����ȷ, ����Ϊ14-12-01   05��װ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }


                                        //if (Convert.ToInt32(_OutDate.Trim().Substring(0, 2)) < 14 || Convert.ToInt32(_OutDate.Trim().Substring(3, 2)) > 12 || Convert.ToInt32(_OutDate.Trim().Substring(6, 2)) > 31 || Convert.ToInt32(_OutDate.Trim().Substring(11, 2)) > 24)
                                        if (strdate < 14 || str2mon > 12 || str3day > 31 || str4hour > 24)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�������ڲ���Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                        _LCL = dt.Rows[i].ItemArray[13].ToString().Trim();

                                    }
                                }

                                if (i == 1)
                                {
                                    if (dt.Rows[i].IsNull(0) || dt.Rows[i].ItemArray[0].ToString().Trim() != "���䷽ʽ:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "���䷽ʽλ�ò���,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Via = dt.Rows[i].ItemArray[2].ToString().Trim();
                                        if (_Via.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "���䷽ʽ����Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(3) || dt.Rows[i].ItemArray[3].ToString().Trim() != "��������:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "��������λ�ò���,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Ctype = dt.Rows[i].ItemArray[5].ToString().Trim();
                                        if (_Ctype.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�������Ͳ���Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(7) || dt.Rows[i].ItemArray[7].ToString().Trim() != "��������:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "��������λ�ò���,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _ShipDate = dt.Rows[i].ItemArray[9].ToString().Trim();
                                        if (_ShipDate.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�������ڲ���Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                        else
                                        {
                                            if (!sid.IsDate(_ShipDate.Trim()))
                                            {
                                                ErrorRecord += 1;

                                                rowError = tblError.NewRow();

                                                rowError["errInfo"] = "�������ڱ���Ϊ��������,����" + aa;
                                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                tblError.Rows.Add(rowError);
                                            }
                                        }
                                    }

                                }

                                if (i == 2)
                                {
                                    if (dt.Rows[i].IsNull(0) || dt.Rows[i].ItemArray[0].ToString().Trim() != "����:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "����λ�ò���,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _ShipTo = dt.Rows[i].ItemArray[2].ToString().Trim();
                                        if (_ShipTo.Trim().Length <= 0)
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "����λ�ò���Ϊ��,����" + aa;
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    if (dt.Rows[i].IsNull(7) || dt.Rows[i].ItemArray[7].ToString() != "װ��ص�:")
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "װ��ص㲻��,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Site = dt.Rows[i].ItemArray[9].ToString().Trim();
                                    }

                                    if (dt.Rows[i].IsNull(11))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "�������ڵĹ�˾,����" + aa;
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _domain = dt.Rows[i].ItemArray[11].ToString().Trim();
                                    }

                                    if (_Nbr.Length > 0)
                                    {
                                        if (Char.IsLetter(_Nbr, _Nbr.Length - 1))
                                        {
                                            _PKref = _Nbr.Substring(_Nbr.Length - 1, 1);
                                        }
                                        else
                                        {
                                            _PKref = "";
                                        }
                                    }

                                    if (ErrorRecord <= 0)
                                    {
                                        SID = sid.InsertTempMstr(Convert.ToInt32(Session["uID"]), _PK, _Nbr, _OutDate, _Via, _Ctype, _ShipDate, _ShipTo, _Site, _domain, _PKref, _LCL);
                                        if (SID <= 0)
                                        {
                                            ds.Reset();
                                            ltlAlert.Text = "alert('�������,����ϵϵͳ����Ա!');";
                                            break;//�����������Ե���������ʾ
                                        }
                                    }
                                }
                                #endregion

                                #region ������ϸ��
                                if (i >= 5)
                                {
                                    rowTemp = tblTemp.NewRow();

                                    //first three column is null, break
                                    if (dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1) && dt.Rows[i].IsNull(2))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (dt.Rows[i].ItemArray[0].ToString().Trim() == "" && dt.Rows[i].ItemArray[1].ToString().Trim() == "" && dt.Rows[i].ItemArray[2].ToString().Trim() == "")
                                        {
                                            break;
                                        }
                                    }

                                    //���
                                    if (dt.Rows[i].IsNull(0))
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
                                        _NO = dt.Rows[i].ItemArray[0].ToString().Trim();
                                    }
                                    //ϵ��
                                    if (dt.Rows[i].IsNull(1))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "ϵ�в���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _SNO = dt.Rows[i].ItemArray[1].ToString().Trim();
                                    }


                                    //QAD
                                    if (dt.Rows[i].IsNull(2))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "QAD����Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _QAD = dt.Rows[i].ItemArray[2].ToString().Trim();
                                    }

                                    //�ͻ�����
                                    if (dt.Rows[i].IsNull(3))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "�ͻ����ϲ���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _Cust_part = dt.Rows[i].ItemArray[3].ToString().Trim();
                                    }

                                    //����
                                    if (dt.Rows[i].IsNull(4))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "������������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Qty_set = Convert.ToDecimal(dt.Rows[i].ItemArray[4]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //ֻ��
                                    if (dt.Rows[i].IsNull(5))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "ֻ������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _qty_pcs = Convert.ToDecimal(dt.Rows[i].ItemArray[5]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "ֻ������������,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //����
                                    if (dt.Rows[i].IsNull(6))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "��������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Qty_box = Convert.ToDecimal(dt.Rows[i].ItemArray[6]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //����
                                    if (dt.Rows[i].ItemArray[7].ToString().Trim().Length <= 0)
                                    {
                                        _qty_pkgs = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _qty_pkgs = Convert.ToDecimal(dt.Rows[i].ItemArray[7]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //�̼�� 
                                    if (dt.Rows[i].IsNull(8))
                                    {
                                        _QA = "";
                                    }
                                    else
                                    {
                                        _QA = dt.Rows[i].ItemArray[8].ToString().Trim();
                                    }

                                    bool bQa = false;//��ʶ�̼���Ƿ���qaList�б���
                                    foreach (QADSID.SID_QA sqa in qaList)//_SNO.Trim()) && )
                                    {
                                        if (sqa.SNO == _SNO.Trim())
                                        {
                                            bQa = true;
                                        }
                                    }

                                    if (bQa && _QA.Trim().Length == 0)
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = _SNO.Trim() + "ϵ�е��̼�Ų���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }

                                    //��ע
                                    if (dt.Rows[i].IsNull(9))
                                    {
                                        _Memo = "";
                                    }
                                    else
                                    {
                                        _Memo = dt.Rows[i].ItemArray[9].ToString().Trim();
                                    }

                                    //���۶���
                                    if (dt.Rows[i].IsNull(10))
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
                                        _So_nbr = dt.Rows[i].ItemArray[10].ToString().Trim();
                                    }

                                    //�к�
                                    if (dt.Rows[i].IsNull(11))
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
                                            _So_line = Convert.ToInt32(dt.Rows[i].ItemArray[11]).ToString();
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

                                    //�����/�ӹ���
                                    if (dt.Rows[i].IsNull(12))
                                    {
                                        _WO = "";
                                    }
                                    else
                                    {
                                        _WO = dt.Rows[i].ItemArray[12].ToString().Trim();
                                    }

                                    //TCP������
                                    if (dt.Rows[i].IsNull(13))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "�ͻ������Ų���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        _PO = dt.Rows[i].ItemArray[13].ToString().Trim();
                                    }

                                    //QAD�Ƿ����EDI_DB..CP_PART
                                    if (!sid.ExistsQADShipImport(_QAD, _So_nbr, _So_line, _PO, Convert.ToInt32(Session["uID"])))
                                    {
                                        //ErrorRecord += 1;
                                        sid.InsertErrorInfo("�ö����ͻ����������ϱ��в����ڣ�����ϵ���������ӣ��˴�������ʾ��", aa.ToString(), Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                    }

                                    //����
                                    if (dt.Rows[i].IsNull(14))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "��������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Weight = Convert.ToDecimal(dt.Rows[i].ItemArray[14]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "��������������,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //���
                                    if (dt.Rows[i].IsNull(15))
                                    {
                                        ErrorRecord += 1;

                                        rowError = tblError.NewRow();

                                        rowError["errInfo"] = "�������Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                        rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                        rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                        tblError.Rows.Add(rowError);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _Volume = Convert.ToDecimal(dt.Rows[i].ItemArray[15]);
                                        }
                                        catch
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�������������,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                    }

                                    //�ͻ�������
                                    if (dt.Rows[i].IsNull(16))
                                    {
                                        _Fob = "";
                                    }
                                    else
                                    {
                                        _Fob = dt.Rows[i].ItemArray[16].ToString().Trim();
                                    }

                                    //ATL������
                                    if (dt.Rows[i].IsNull(17))
                                    {
                                        _ATL = "";
                                    }
                                    else
                                    {
                                        _ATL = dt.Rows[i].ItemArray[17].ToString().Trim();
                                    }

                                    //Fedx���ٺ�
                                    if (dt.Rows[i].IsNull(18))
                                    {
                                        _Fedx = "";
                                    }
                                    else
                                    {
                                        _Fedx = dt.Rows[i].ItemArray[18].ToString().Trim();
                                    }

                                    //QAD�ͻ�����
                                    if (dt.Rows[i].IsNull(19))
                                    {
                                        _QADPO = "";
                                    }
                                    else
                                    {
                                        _QADPO = dt.Rows[i].ItemArray[19].ToString().Trim();
                                    }

                                    //QAD�ͻ�������
                                    if (dt.Rows[i].IsNull(20))
                                    {
                                        _QADLine = "";
                                    }
                                    else
                                    {
                                        _QADLine = dt.Rows[i].ItemArray[20].ToString().Trim();//Convert.ToInt32(dt.Rows[i].ItemArray[18].ToString());
                                    }

                                    //����
                                    if (_Via.ToUpper() != "A")
                                    {
                                        if (sid.IsAirShipImport(_PO, _So_line))
                                        {
                                            //ErrorRecord += 1;
                                            sid.InsertErrorInfo("�ö��������Ѿ�����Ϊ���ˣ�ϵͳ�Ѿ�Ϊ�������ȥ���˴�������ʾ��", aa.ToString(), Convert.ToInt32(Session["uID"]), Convert.ToString(i + 2)); ;
                                        }
                                    }

                                    if (ErrorRecord <= 0)
                                    {
                                        rowTemp = tblTemp.NewRow();

                                        rowTemp["id"] = SID;
                                        rowTemp["SNO"] = _SNO;
                                        rowTemp["QAD"] = _QAD;
                                        rowTemp["qty_set"] = _Qty_set;
                                        rowTemp["qty_box"] = _Qty_box;
                                        rowTemp["qa"] = _QA;
                                        rowTemp["memo"] = _Memo;
                                        rowTemp["so_nbr"] = _So_nbr;
                                        rowTemp["so_line"] = _So_line;
                                        rowTemp["wo"] = _WO;
                                        rowTemp["PO"] = _PO;
                                        rowTemp["cust_part"] = _Cust_part;
                                        rowTemp["weight"] = _Weight;
                                        rowTemp["volume"] = _Volume;
                                        rowTemp["qty_pcs"] = _qty_pcs;
                                        rowTemp["qty_pkgs"] = _qty_pkgs;
                                        rowTemp["fob"] = _Fob;
                                        rowTemp["fedx"] = _Fedx;
                                        rowTemp["no"] = _NO;
                                        rowTemp["atl"] = _ATL;
                                        rowTemp["qadpo"] = _QADPO;
                                        if (string.IsNullOrEmpty(_QADLine))
                                        {
                                            rowTemp["qadline"] = 0;
                                        }
                                        else
                                        {
                                            rowTemp["qadline"] = Convert.ToInt32(_QADLine);
                                        }
                                        rowTemp["createdby"] = Convert.ToInt32(Session["uID"]);
                                        rowTemp["createddate"] = DateTime.Now;

                                        tblTemp.Rows.Add(rowTemp);
                                    }
                                }
                                #endregion
                            }
                        } //ds.Tables[0].Rows.Count > 0    
                        #endregion

                    } // a != null)
                    ds.Reset();
                } //foreach
                #endregion

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
                        bulkCopyTemp.DestinationTableName = "dbo.SID_det_temp";
                        bulkCopyTemp.ColumnMappings.Add("id", "SID_id");
                        bulkCopyTemp.ColumnMappings.Add("sno", "SID_SNO");
                        bulkCopyTemp.ColumnMappings.Add("qad", "SID_QAD");
                        bulkCopyTemp.ColumnMappings.Add("qty_set", "SID_qty_set");
                        bulkCopyTemp.ColumnMappings.Add("qty_box", "SID_qty_box");
                        bulkCopyTemp.ColumnMappings.Add("qa", "SID_qa");
                        bulkCopyTemp.ColumnMappings.Add("memo", "SID_memo");
                        bulkCopyTemp.ColumnMappings.Add("so_nbr", "SID_so_nbr");
                        bulkCopyTemp.ColumnMappings.Add("so_line", "SID_so_line");
                        bulkCopyTemp.ColumnMappings.Add("wo", "SID_wo");
                        bulkCopyTemp.ColumnMappings.Add("PO", "SID_PO");
                        bulkCopyTemp.ColumnMappings.Add("cust_part", "SID_cust_part");
                        bulkCopyTemp.ColumnMappings.Add("weight", "SID_weight");
                        bulkCopyTemp.ColumnMappings.Add("volume", "SID_volume");
                        bulkCopyTemp.ColumnMappings.Add("qty_pcs", "SID_qty_pcs");
                        bulkCopyTemp.ColumnMappings.Add("qty_pkgs", "SID_qty_pkgs");
                        bulkCopyTemp.ColumnMappings.Add("fob", "SID_fob");
                        bulkCopyTemp.ColumnMappings.Add("fedx", "SID_fedx");
                        bulkCopyTemp.ColumnMappings.Add("no", "SID_no");
                        bulkCopyTemp.ColumnMappings.Add("atl", "SID_atl");
                        bulkCopyTemp.ColumnMappings.Add("qadpo", "SID_qadpo");
                        bulkCopyTemp.ColumnMappings.Add("qadline", "SID_qadline");
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
        } //filename1.PostedFile != null

        if (ErrorRecord <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    #region ����NPOI��ȡExcel
    /// <summary>
    /// ����NPOI��ȡExcel
    /// </summary>
    /// <param name="excelPath">Ҫ��ȡ��Excel·��</param>
    /// <param name="header">����Ϊ�գ���֤Excel��ͷ����ʽ�ǣ��ͻ�,���Ϻ�,�۸�</param>
    /// <returns></returns>
    public DataTable GetExcelContents(string excelPath, int Num)
    {
        string ext = Path.GetExtension(excelPath);
        DataTable dt = null;
        if (ext == ".xls")
        {
            dt = GetExcelContent2003(excelPath, Num);
        }
        else
        {
            dt = GetExcelContent2007(excelPath, Num);
        }
        return dt;
    }
    #endregion

    /// <summary>
    /// ����NPOI��ȡExcel2003
    /// </summary>
    /// <param name="excelPath">Ҫ��ȡ��Excel·��</param>
    /// <param name="header">����Ϊ�գ���֤Excel��ͷ����ʽ�ǣ��ͻ�,���Ϻ�,�۸�</param>
    /// <returns></returns>
    public DataTable GetExcelContent2003(string excelPath, int count)
    {
        if (File.Exists(excelPath))
        {
            //����·��ͨ���Ѵ��ڵ�excel������HSSFWorkbook��������excel�ĵ�
            FileStream fileStream = new FileStream(excelPath, FileMode.Open);
            IWorkbook workbook = new HSSFWorkbook(fileStream);

            //��ȡexcel�ĵ�һ��sheet
            ISheet sheet = workbook.GetSheetAt(count);

            DataTable table = new DataTable();
            //��ȡsheet������
            IRow headerRow = sheet.GetRow(0);

            //һ�����һ������ı�� ���ܵ�����
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //���һ�еı��  ���ܵ�����
            int rowCount = sheet.LastRowNum + 1;

            for (int i = (sheet.FirstRowNum + 1); i < rowCount; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    dataRow[j] = "";
                                    break;
                                case CellType.String:
                                    dataRow[j] = cell.StringCellValue;
                                    break;
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = cell.DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = cell.NumericCellValue;
                                    }
                                    break;
                                case CellType.Formula:
                                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                                    dataRow[j] = e.Evaluate(cell).StringValue;
                                    break;
                                default:
                                    dataRow[j] = cell.ToString();
                                    break;
                            }
                        }
                    }

                    table.Rows.Add(dataRow);
                }
                catch
                {
                    continue;
                }
            }

            workbook = null;
            sheet = null;

            return table;
        }
        else
        {
            return null;
        }
    }

    /// <summary>  
    /// ����NPOI��ȡExcel2007
    /// ��Excel�ļ��е����ݶ�����DataTable��(xlsx)  
    /// </summary>  
    /// <param name="file"></param>  
    /// <returns></returns>  
    public DataTable GetExcelContent2007(string file, int count)
    {
        DataTable dt = new DataTable();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
            ISheet sheet = xssfworkbook.GetSheetAt(count);

            //��ͷ  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    //continue;  
                }
                else
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //����  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    /// <summary>  
    /// ��ȡ��Ԫ������(xlsx)  
    /// </summary>  
    /// <param name="cell"></param>  
    /// <returns></returns>  
    private static object GetValueTypeForXLSX(XSSFCell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank:
                return null;
            case CellType.Boolean:
                return cell.BooleanCellValue;
            case CellType.Numeric:
                return cell.NumericCellValue;
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Error:
                return cell.ErrorCellValue;
            case CellType.Formula:
            default:
                return "=" + cell.CellFormula;
        }
    }

}
