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
using PIInfo;
using System.Collections.Generic;

using CommClass;
using System.Data;
using System.Text.RegularExpressions;


public partial class SID_PriceList : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
    PI pi = new PI();

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

        pi.DelTempPriceList(Convert.ToInt32(Session["uID"]));
        pi.DelImportError(Convert.ToInt32(Session["uID"]));
        

        int ErrorRecord = 0;

        if (!ImportExcelFile())
        {
            ErrorRecord += 1;

        }


        if (ErrorRecord == 0)
        {
            pi.ImportPriceData(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));

            string newid_no = Request.Params["newid_no"];
            string strSql2 = " Select top 1 * From ImportError where userID ='" + Convert.ToInt32(Session["uID"]) + "' and  plantID= '" + Convert.ToInt32(Session["PlantCode"]) + "'";

            DataSet ds2; 
            try
            {
                ds2 = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strSql2);
                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                {
                    Session["EXTitle"] = "500^<b>ϵͳ��ʾ</b>~^";
                    Session["EXHeader"] = "";
                    Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                    ltlAlert.Text = "alert('����ʧ��!');window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('Submission information verification failed! \\n �ύ��Ϣ��֤ʧ�ܣ�');Form1.usercode.focus();";
                return;
            }
            string num = pi.UpddatePriceList(Convert.ToInt32(Session["uID"]));
            ltlAlert.Text = "alert('�ɹ�����" + num + "������!');";
        }
        else
        {
            Session["EXTitle"] = "500^<b>����ԭ��</b>~^";
            Session["EXHeader"] = "";
            Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }

    }

    public Boolean ImportExcelFile()
    {
        //String strSQL = "";
       // DataSet ds = new DataSet();
        DataTable dts =new DataTable();
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

        #region ��Pi_PriceList_temp��ImportError�Ľṹ������
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

        //����Pi_PriceList_temp
        DataTable tblTemp = new DataTable("PriceList_temp");

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Cust";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qad";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ShipTo";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "DoMain";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Currency";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Ptype";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "price1";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "price2";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Decimal");
        column.ColumnName = "price3";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "StartDate";
        tblTemp.Columns.Add(column); 

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "EndDate";
        tblTemp.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "remark";
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
                           
                           try
                           {
                              // ds = pi.getExcelContents(strFileName, aa);
                               dts = this.GetExcelContents(strFileName);
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

                           if (dts.Rows.Count > 0)
                           {
                               if (dts.Columns[0].ColumnName != "�۸��")
                               {
                                   dts.Reset();
                                   ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ!');";
                                   return false;
                               }

                               DataRow rowError;//��������
                               DataRow rowTemp;//��ʱ�����

                               rowTemp = tblTemp.NewRow();

                               String _Cust = "";
                               String _QAD = "";
                               String _ShipTo = "";
                               String _DoMain = "";
                               String _Currency = "";
                               String _Ptype = "";
                               Decimal _price1 = 0;
                               Decimal _price2 = 0;
                               Decimal _price3 = 0;
                               String _StartDate = "";
                               String _EndDate = "";
                               String _Remark = "";


                               string strErrMsg = string.Empty;//ǰ�˵Ĵ�����Ϣ
                               ErrorRecord = 0;


                               for (int i = 0; i <= dts.Rows.Count - 1; i++)
                               {

                                   string aa = string.Format(i.ToString());
                                   _Cust = "";
                                   _QAD = "";
                                   _ShipTo = "";
                                   _DoMain = "";
                                   _Currency = "";
                                   _Ptype = "";
                                   _price1 = 0;
                                   _price2 = 0;
                                   _price3 = 0;
                                   _StartDate = "";
                                   _EndDate = "";
                                   _Remark = "";

                                   #region ������ϸ��
                                   if (i >= 1)
                                   {
                                       rowTemp = tblTemp.NewRow();

                                       //first three column is null, break
                                       if (dts.Rows[i].IsNull(0) && dts.Rows[i].IsNull(1) && dts.Rows[i].IsNull(2))
                                       {
                                           break;
                                       }
                                       else
                                       {
                                           if (dts.Rows[i].ItemArray[0].ToString().Trim() == "" && dts.Rows[i].ItemArray[1].ToString().Trim() == "" && dts.Rows[i].ItemArray[2].ToString().Trim() == "")
                                           {
                                               break;
                                           }
                                       }

                                       //�ͻ�
                                       if (dts.Rows[i].IsNull(0))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "Item����Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _Cust = dts.Rows[i].ItemArray[0].ToString().Trim();
                                       }

                                       //QAD��
                                       if (dts.Rows[i].IsNull(1))
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
                                           _QAD = dts.Rows[i].ItemArray[1].ToString().Trim();
                                       }

                                       //���� ���ǵ����ü۸񣬽��˶���ע��
                                       //if (dts.Rows[i].IsNull(2))
                                       //{
                                       //    ErrorRecord += 1;

                                       //    rowError = tblError.NewRow();

                                       //    rowError["errInfo"] = "����(ShipTo)����Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                       //    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                       //    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                       //    tblError.Rows.Add(rowError);
                                       //}
                                       //else
                                       //{
                                           _ShipTo = dts.Rows[i].ItemArray[2].ToString().Trim();
                                       //}

                                       //�۸���
                                        if (string.IsNullOrEmpty(dts.Rows[i].ItemArray[3].ToString().Trim()))
                                        {
                                            ErrorRecord += 1;

                                            rowError = tblError.NewRow();

                                            rowError["errInfo"] = "�۸�����Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                            rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                            rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                            tblError.Rows.Add(rowError);
                                        }
                                        else
                                        {
                                            _DoMain = dts.Rows[i].ItemArray[3].ToString().Trim();
                                        }

                                       //�Ƽ۵�λ
                                       if (dts.Rows[i].IsNull(4))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "�Ƽ۵�λ����Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           if (dts.Rows[i].ItemArray[4].ToString().Trim() == "SETS" || dts.Rows[i].ItemArray[4].ToString().Trim() == "PCS")
                                           {
                                               _Ptype = dts.Rows[i].ItemArray[4].ToString().Trim();
                                           }
                                           else
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "�Ƽ۵�λ����ȷ,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);
                                               tblError.Rows.Add(rowError); 
                                           }
                                       }

                                       //����
                                       if (dts.Rows[i].IsNull(5))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "���ֲ���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           if (dts.Rows[i].ItemArray[5].ToString().Trim()== "USD" || dts.Rows[i].ItemArray[5].ToString().Trim() == "RMB")
                                           {
                                               _Currency = dts.Rows[i].ItemArray[5].ToString().Trim();
                                           }
                                           else
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "���ֲ���ȷ,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }

                                       //�۸�1(ATL�۸�)
                                       if (dts.Rows[i].IsNull(6) || dts.Rows[i].ItemArray[6]==string.Empty)
                                       {
                                           _price1 = 0;
                                       }
                                       else
                                       {
                                           _price1 = Convert.ToDecimal(dts.Rows[i].ItemArray[6]);
                                       }

                                       //�۸�2(TCP�۸�)
                                       if (dts.Rows[i].IsNull(7) || dts.Rows[i].ItemArray[7] == string.Empty)
                                       {
                                           _price2 = 0;
                                       }
                                       else
                                       {
                                           _price2 = Convert.ToDecimal(dts.Rows[i].ItemArray[7]);
                                       }


                                       //�۸�3(CUST�۸�)
                                       if (dts.Rows[i].IsNull(8) || dts.Rows[i].ItemArray[8] == string.Empty)
                                       {
                                           _price3 = 0;
                                       }
                                       else
                                       {
                                           _price3 = Convert.ToDecimal(dts.Rows[i].ItemArray[8]);
                                       }

                                       //��Ч��ʼ����
                                       if (dts.Rows[i].IsNull(9))
                                       {
                                           ErrorRecord += 1;

                                           rowError = tblError.NewRow();

                                           rowError["errInfo"] = "��Ч��ʼ���ڲ���Ϊ��,����" + aa + "��" + Convert.ToString(i + 2);
                                           rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                           rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                           tblError.Rows.Add(rowError);
                                       }
                                       else
                                       {
                                           _StartDate = Convert.ToDateTime(dts.Rows[i].ItemArray[9].ToString().Trim()).ToString();
                                       }
                                       //��Ч��ֹ����
                                       if (dts.Rows[i].IsNull(10) || dts.Rows[i].ItemArray[10].ToString().Trim()=="")
                                       {
                                           _EndDate = null;
                                       }
                                       else
                                       {

                                           _EndDate = Convert.ToDateTime(dts.Rows[i].ItemArray[10].ToString().Trim()).ToString();
                                       }
                                       if (_EndDate != string.Empty && _EndDate!=null)
                                       {
                                           try
                                           {
                                               if (Convert.ToDateTime(dts.Rows[i].ItemArray[9].ToString().Trim()) > Convert.ToDateTime(dts.Rows[i].ItemArray[10].ToString().Trim()))
                                               {
                                                   ErrorRecord += 1;

                                                   rowError = tblError.NewRow();

                                                   rowError["errInfo"] = "QAD:" + dts.Rows[i].ItemArray[1].ToString().Trim() + ":��Ч��ʼ���ڲ��ܴ��ڽ�ֹ����,����" + aa + "��" + Convert.ToString(i + 2);
                                                   rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                                   rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                                   tblError.Rows.Add(rowError);
                                               }
                                           }
                                           catch (Exception)
                                           {
                                               ErrorRecord += 1;

                                               rowError = tblError.NewRow();

                                               rowError["errInfo"] = "QAD:" + dts.Rows[i].ItemArray[1].ToString().Trim() + ":���ڸ�ʽ����ȷ,����" + aa + "��" + Convert.ToString(i + 2);
                                               rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                               rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                               tblError.Rows.Add(rowError);
                                           }
                                       }
                                      
                                       //��ע
                                       if (dts.Rows[i].IsNull(11))
                                       {
                                           _Remark = "";
                                       }
                                       else
                                       {
                                           _Remark = dts.Rows[i].ItemArray[11].ToString().Trim();
                                       }


                                       if (ErrorRecord <= 0)
                                       {
                                           rowTemp = tblTemp.NewRow();

                                           rowTemp["Cust"] = _Cust;
                                           rowTemp["QAD"] = _QAD;
                                           rowTemp["ShipTo"] = _ShipTo;
                                           rowTemp["DoMain"] = _DoMain;
                                           rowTemp["Currency"] = _Currency;
                                           rowTemp["Ptype"] = _Ptype;
                                           rowTemp["price1"] = _price1;
                                           rowTemp["price2"] = _price2;
                                           rowTemp["price3"] = _price3;
                                           rowTemp["StartDate"] = _StartDate;
                                           rowTemp["EndDate"] = _EndDate;
                                           rowTemp["Remark"] = _Remark;
                                           rowTemp["createdby"] = Convert.ToInt32(Session["uID"]);
                                           rowTemp["createddate"] = DateTime.Now;

                                           tblTemp.Rows.Add(rowTemp);
                                       }
                                   }
                                   #endregion
                               }
                           } //dts.Rows.Count > 0                           
                       dts.Reset();

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
                           bulkCopyTemp.DestinationTableName = "dbo.Pi_PriceListTemp";
                           bulkCopyTemp.ColumnMappings.Add("Cust", "Pi_Cust");
                           bulkCopyTemp.ColumnMappings.Add("qad", "Pi_QAD");
                           bulkCopyTemp.ColumnMappings.Add("ShipTo", "Pi_ShipTo");
                           bulkCopyTemp.ColumnMappings.Add("DoMain", "Pi_Domain");
                           bulkCopyTemp.ColumnMappings.Add("Currency", "Pi_Currency");
                           bulkCopyTemp.ColumnMappings.Add("Ptype", "Pi_UM");
                           bulkCopyTemp.ColumnMappings.Add("price1", "Pi_price1");
                           bulkCopyTemp.ColumnMappings.Add("price2", "Pi_price2");
                           bulkCopyTemp.ColumnMappings.Add("price3", "Pi_price3");
                           bulkCopyTemp.ColumnMappings.Add("StartDate", "Pi_StartDate");
                           bulkCopyTemp.ColumnMappings.Add("EndDate", "Pi_EndDate");
                           bulkCopyTemp.ColumnMappings.Add("Remark", "Pi_Remark");
                           bulkCopyTemp.ColumnMappings.Add("createdBy", "Pi_createdby");
                           bulkCopyTemp.ColumnMappings.Add("createdDate", "Pi_createddate");

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

}
