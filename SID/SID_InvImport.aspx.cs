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
using System.IO;
using QADSID;
using System.Data.SqlClient;

public partial class SID_SID_InvImport : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "80^<b>���˵���</b>~^80^<b>��֤��Ʊ</b>~^100^<b>�ͻ��ɹ���</b>~^30^<b>��</b>~^120^<b>�ͻ����</b>~^200^<b>����</b>~^80^<b>�۸�</b>~^60^<b>����</b>~^100^<b>�۸�����(PCS/SETS)</b>~^200^<b>������Ϣ</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select sid_nbr,sid_invoice,sid_po,sid_line,sid_cust_part,sid_cust_partdesc,sid_price,sid_ptype,sid_currency,sid_errinfo From tcpc0.dbo.SID_invTemp Where sid_createdby='" + Convert.ToInt32(Session["uID"]) + "'  Order By sid_vid ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);

        }
    }
    protected void BtnInv_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        sid.DelTempShip(Convert.ToInt32(Session["uID"]));
        sid.DelImportError(Convert.ToInt32(Session["uID"]));

        if (ImportExcelFile())
        {
            Int32 Ierr = 0;

            Ierr = sid.ImportShipInvData(Convert.ToInt32(Session["uID"]));
            if (Ierr < 0)
            {
                Response.Redirect(chk.urlRand("/SID/SID_Invimport.aspx?err=y"));
            }
            else
            {
                ltlAlert.Text = "alert('����ɹ�!');";
            }

        }
    }

    public Boolean ImportExcelFile()
    {
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
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

                try
                {
                    //ds = chk.getExcelContents(strFileName);
                    dt = GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ'" + e.ToString() + "'.');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns[0].ColumnName != "���˵���" ||
                        dt.Columns[1].ColumnName != "��֤��Ʊ" ||
                        dt.Columns[2].ColumnName != "�ͻ��ɹ���" ||
                        dt.Columns[3].ColumnName != "��" ||
                        dt.Columns[4].ColumnName != "�ͻ����" ||
                        dt.Columns[5].ColumnName != "����" ||
                        dt.Columns[6].ColumnName != "�۸�" ||
                        dt.Columns[8].ColumnName != "����" ||
                        dt.Columns[9].ColumnName != "�۸�����(PCS/SETS)")
                    {
                        //ds.Reset();
                        ltlAlert.Text += "alert('�����ļ���ģ�治��ȷ!');";
                        return false;
                    }

                    String _Nbr = "";
                    String _So_line = "";
                    String _PO = "";
                    String _Cust_part = "";
                    String _Desc = "";
                    String _Price = "";
                    String _Price1 = "";
                    String _Currency = "";
                    String _Invoice = "";
                    String _Ptype = "";

                    i = 0;

                    //ת����SID_InvTemp��ʽ
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Nbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "Line";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "PO";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Part";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "PartDesc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Decimal");
                    column.ColumnName = "Price";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Decimal");
                    column.ColumnName = "Price1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Curr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Invoice";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Ptype";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.DateTime");
                    column.ColumnName = "createdDate";
                    table.Columns.Add(column);

                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        _Nbr = "";
                        _So_line = "";
                        _PO = "";
                        _Cust_part = "";
                        _Desc = "";
                        _Price = "";
                        _Price1 = "";
                        _Currency = "";
                        _Invoice = "";
                        _Ptype = "";

                        //Price Type
                        if (dt.Rows[i].IsNull(9))
                        {
                            //ds.Reset();
                            ltlAlert.Text += "alert('�۸����ͱ���Ҫ��!');";
                            return false;
                        }
                        else
                        {
                            _Ptype = dt.Rows[i].ItemArray[9].ToString().Trim();
                            if ((_Ptype != "PCS") && (_Ptype != "SETS"))
                            {
                                //ds.Reset();
                                ltlAlert.Text += "alert('�۸����ͱ���Ҫ��!');";
                                return false;
                            }

                        }

                        //Price
                        try
                        {
                            Convert.ToDecimal(dt.Rows[i].ItemArray[6].ToString().Trim());
                        }
                        catch
                        {
                            //ds.Reset();
                            ltlAlert.Text += "alert('�۸����ֵ����!');";
                            return false;
                        }

                        //Price1
                        try
                        {
                            Convert.ToDecimal(dt.Rows[i].ItemArray[7].ToString().Trim());
                        }
                        catch
                        {
                            //ds.Reset();
                            ltlAlert.Text += "alert('TCP�۸����ֵ����!');";
                            return false;
                        }

                        //_So_line Add By Shanzm 2012-12-31
                        try
                        {
                            _So_line = Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString().Trim()).ToString();
                        }
                        catch
                        {
                            _So_line = "-1";
                        }

                        _Nbr = dt.Rows[i].ItemArray[0].ToString().Trim();
                        _Invoice = dt.Rows[i].ItemArray[1].ToString().Trim();
                        _PO = dt.Rows[i].ItemArray[2].ToString().Trim();

                        _Cust_part = dt.Rows[i].ItemArray[4].ToString().Trim();
                        _Desc = dt.Rows[i].ItemArray[5].ToString().Trim();
                        _Price = dt.Rows[i].ItemArray[6].ToString().Trim();
                        _Price1 = dt.Rows[i].ItemArray[7].ToString().Trim();
                        _Currency = dt.Rows[i].ItemArray[8].ToString().Trim();

                        row = table.NewRow();

                        row["Nbr"] = _Nbr;
                        row["Invoice"] = _Invoice;
                        row["PO"] = _PO;
                        row["Line"] = Convert.ToInt32(_So_line);
                        row["Part"] = _Cust_part;
                        row["PartDesc"] = _Desc;
                        row["Price"] = Convert.ToDecimal(_Price);
                        row["Price1"] = Convert.ToDecimal(_Price1);
                        row["Curr"] = _Currency;
                        row["Ptype"] = _Ptype;
                        row["createdBy"] = Convert.ToInt32(Session["uID"].ToString());
                        row["createdDate"] = DateTime.Now;

                        table.Rows.Add(row);

                        if (_Nbr.Trim() == "" && _So_line.Trim() == "")
                        {
                            break; ;
                        }
                    }

                    //table�����ݵ������
                    if (table != null && table.Rows.Count > 0)
                    {

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                        {
                            bulkCopy.DestinationTableName = "SID_InvTemp";

                            bulkCopy.ColumnMappings.Clear();

                            bulkCopy.ColumnMappings.Add("Nbr", "SID_nbr");
                            bulkCopy.ColumnMappings.Add("Line", "SID_line");
                            bulkCopy.ColumnMappings.Add("PO", "SID_PO");
                            bulkCopy.ColumnMappings.Add("Invoice", "SID_Invoice");
                            bulkCopy.ColumnMappings.Add("Part", "SID_cust_part");
                            bulkCopy.ColumnMappings.Add("PartDesc", "SID_cust_partdesc");
                            bulkCopy.ColumnMappings.Add("Price", "SID_price");
                            bulkCopy.ColumnMappings.Add("Price1", "SID_price1");
                            bulkCopy.ColumnMappings.Add("Curr", "SID_currency");
                            bulkCopy.ColumnMappings.Add("Ptype", "SID_Ptype");
                            bulkCopy.ColumnMappings.Add("createdBy", "SID_createdby");
                            bulkCopy.ColumnMappings.Add("createdDate", "SID_createddate");

                            try
                            {
                                bulkCopy.WriteToServer(table);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('����ʱ��������ϵϵͳ����Ա��');";
                                return false;
                            }
                            finally
                            {
                                table.Dispose();
                                bulkCopy.Close();
                            }
                        }
                    }
                } //dt.Rows.Count > 0                           

                //ds.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }
        return true;
    }
}
