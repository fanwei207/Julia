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
using CommClass;

public partial class EDIPOImport :BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ÿ������Ҫ����Ȩ��
            foreach (ListItem item in chkList.Items)
            {
                if (item.Value == "10")
                {
                    item.Enabled = this.Security["10000101"].isValid;
                }
                else if (item.Value == "20")
                {
                    item.Enabled = this.Security["10000102"].isValid;
                }
                else if (item.Value == "30")
                {
                    item.Enabled = this.Security["10000103"].isValid;
                }
                else if (item.Value == "40")
                {
                    item.Enabled = this.Security["10000104"].isValid;
                }
                else if (item.Value == "120")
                {
                    item.Enabled = this.Security["10000104"].isValid;
                }
                else if (item.Value == "50")
                {
                    item.Enabled = this.Security["10000105"].isValid;
                }
                else if (item.Value == "60")
                {
                    item.Enabled = this.Security["10000106"].isValid;
                }
                else if (item.Value == "70")
                {
                    item.Enabled = this.Security["10000107"].isValid;
                }
                else if (item.Value == "90")
                {
                    item.Enabled = this.Security["10000108"].isValid;
                }
                else if (item.Value == "100")
                {
                    item.Enabled = this.Security["10000109"].isValid;
                }
                else if (item.Value == "110")
                {
                    item.Enabled = this.Security["10000111"].isValid;
                }
                else if (item.Value == "120")
                {
                    item.Enabled = this.Security["10000112"].isValid;
                }
            }
        }
    }

    private bool InsertBatchTemp(string uID, bool bPlanDate, bool bDueDate, bool bUnitPrice, bool bSite, bool bCust, bool bPart, bool bCustPart, bool bSoNbr, bool bQty, bool bSample, bool bremark, bool bDomain, bool bPromisedDeliveryDate)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[15];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@bPlanDate", bPlanDate);
            sqlParam[2] = new SqlParameter("@bDueDate", bDueDate);
            sqlParam[3] = new SqlParameter("@bUnitPrice", bUnitPrice);
            sqlParam[4] = new SqlParameter("@bSite", bSite);
            sqlParam[5] = new SqlParameter("@bCust", bCust);
            sqlParam[6] = new SqlParameter("@bPart", bPart);
            sqlParam[7] = new SqlParameter("@bCustPart", bCustPart);
            sqlParam[8] = new SqlParameter("@bSoNbr", bSoNbr);
            sqlParam[9] = new SqlParameter("@bQty", bQty);

            sqlParam[10] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[10].Direction = ParameterDirection.Output;
            sqlParam[11] = new SqlParameter("@bSample", bSample);
            sqlParam[12] = new SqlParameter("@bremark", bremark);
            sqlParam[13] = new SqlParameter("@bDomain", bDomain);
            sqlParam[14] = new SqlParameter("@bPromisedDeliveryDate", bPromisedDeliveryDate);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertbatchEDIPO", sqlParam);

            return Convert.ToBoolean(sqlParam[10].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckValidity(string uID, bool bPlanDate, bool bDueDate, bool bUnitPrice, bool bSite, bool bCust, bool bPart, bool bCustPart, bool bSoNbr, bool bQty, bool bSample, bool bremark, bool bDomain, bool bPromisedDeliveryDate)
    {
        try
        {
            string strSql = "sp_edi_checkEDIPOValidity";

            SqlParameter[] sqlParam = new SqlParameter[14];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@bPlanDate", bPlanDate);
            sqlParam[2] = new SqlParameter("@bDueDate", bDueDate);
            sqlParam[3] = new SqlParameter("@bUnitPrice", bUnitPrice);
            sqlParam[4] = new SqlParameter("@bSite", bSite);
            sqlParam[5] = new SqlParameter("@bCust", bCust);
            sqlParam[6] = new SqlParameter("@bPart", bPart);
            sqlParam[7] = new SqlParameter("@bCustPart", bCustPart);
            sqlParam[8] = new SqlParameter("@bSoNbr", bSoNbr);
            sqlParam[9] = new SqlParameter("@bQty", bQty);

            sqlParam[10] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[10].Direction = ParameterDirection.Output;
            sqlParam[11] = new SqlParameter("@bSample", bSample);
            sqlParam[12] = new SqlParameter("@bDomain", bDomain);
            sqlParam[13] = new SqlParameter("@bPromisedDeliveryDate", bPromisedDeliveryDate);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[10].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_clearTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean ImportExcelFile()
    {
        DataTable ds = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

        #region �ϴ��ĵ����д���
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

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
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
                try
                {
                    ds = this.GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ!');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (ds.Rows.Count > 0)
                {
                    /*
                     *  �����Excel�ļ��������㣺
                     *      1������Ӧ��������
                     *      2���ӵ����п�ʼ����Ϊ����
                     *      3���������Ʊ�����wo2_mop�д���
                    */
                    if (ds.Columns.Count != 24)
                    {
                        ds.Reset();
                        ltlAlert.Text = "alert('���ļ�������24�У�');";
                        return false;
                    }

                    #region Excel���������뱣��һ��
                    for (int col = 0; col < ds.Columns.Count;col ++ )
                    {
                        if (col == 0 && ds.Columns[col].ColumnName.Trim() != "����")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ���ڣ�');";
                            return false;
                        }

                        if (col == 1 && ds.Columns[col].ColumnName.Trim() != "�ͻ�����")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �ͻ����룡');";
                            return false;
                        }

                        if (col == 2 && ds.Columns[col].ColumnName.Trim() != "�ۿ�")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �ۿڣ�');";
                            return false;
                        }

                        if (col == 3 && ds.Columns[col].ColumnName.Trim() != "TCP������")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� TCP�����ţ�');";
                            return false;
                        }

                        if (col == 4 && ds.Columns[col].ColumnName.Trim() != "�ͻ�������")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �ͻ������ţ�');";
                            return false;
                        }

                        if (col == 5 && ds.Columns[col].ColumnName.Trim() != "SW1")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� SW1��');";
                            return false;
                        }

                        if (col == 6 && ds.Columns[col].ColumnName.Trim() != "SW2")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� SW2��');";
                            return false;
                        }

                        if (col == 7 && ds.Columns[col].ColumnName.Trim() != "��ֹ����")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��ֹ���ڣ�');";
                            return false;
                        }

                        if (col == 8 && ds.Columns[col].ColumnName.Trim() != "���")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��ţ�');";
                            return false;
                        }

                        if (col == 9 && ds.Columns[col].ColumnName.Trim() != "SZX���۶���")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� SZX���۶�����');";
                            return false;
                        }

                        if (col == 10 && ds.Columns[col].ColumnName.Trim() != "ATL���۶���")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ATL���۶�����');";
                            return false;
                        }

                        if (col == 11 && ds.Columns[col].ColumnName.Trim() != "QAD�ű���")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� QAD�ű��룡');";
                            return false;
                        }

                        if (col == 12 && ds.Columns[col].ColumnName.Trim() != "��Ʒ�ͺ�")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��Ʒ�ͺţ�');";
                            return false;
                        }

                        if (col == 13 && ds.Columns[col].ColumnName.Trim() != "��������(��)")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��������(��)��');";
                            return false;
                        }

                        if (col == 14 && ds.Columns[col].ColumnName.Trim() != "����(ֻ)")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ����(ֻ)��');";
                            return false;
                        }

                        if (col == 15 && ds.Columns[col].ColumnName.Trim() != "���۵��ص�")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ���۵��ص㣡');";
                            return false;
                        }

                        if (col == 16 && ds.Columns[col].ColumnName.Trim() != "��ע")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��ע��');";
                            return false;
                        }

                        if (col == 17 && ds.Columns[col].ColumnName.Trim() != "�ɹ��۸�")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �ɹ��۸�');";
                            return false;
                        }

                        if (col == 18 && ds.Columns[col].ColumnName.Trim() != "����")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ������');";
                            return false;
                        }

                        if (col == 19 && ds.Columns[col].ColumnName.Trim() != "�ƻ�����")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �ƻ����ڣ�');";
                            return false;
                        }

                        if (col == 20 && ds.Columns[col].ColumnName.Trim() != "��Ʒ")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��Ʒ��');";
                            return false;
                        }
                        if (col == 21 && ds.Columns[col].ColumnName.Trim() != "BOM���ڱ�ע")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� BOM���ڱ�ע��');";
                            return false;
                        }
                        if (col == 22 && ds.Columns[col].ColumnName.Trim() != "�Ƶ�")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �Ƶأ�');";
                            return false;
                        }
                        if (col == 23 && ds.Columns[col].ColumnName.Trim() != "��ŵ��������")
                        {
                            ds.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��ŵ�������ڣ�');";
                            return false;
                        }
                    }																
                    #endregion

                    //ת����ģ���ʽ
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    #region �������
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "date";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cust";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "port";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "tcp_po";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "cust_po";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sw1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sw2";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "due_date";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "line";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "szx_so";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "atl_so";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qad";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "item";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qty_ord";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qty_ord1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "site";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "rmks";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "price";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sample";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "planDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "IsSample";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "remarks";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "domain";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "promisedDeliveryDate";
                    table.Columns.Add(column);
                    #endregion
                  
                    int _uID = Convert.ToInt32(Session["uID"].ToString());

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in ds.Rows)
                        {
                            row = table.NewRow();

                            #region ��ֵ�������ж�
                            //date�ĳ��������10���ַ��������ȡ
                            if (r[0].ToString().Length > 10)
                            {
                                try
                                {
                                    row["date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[0]));
                                }
                                catch
                                {
                                    row["date"] = r[0].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[0]));
                                }
                                catch
                                {
                                    row["date"] = r[0].ToString();
                                }
                            }

                            //cust�ĳ��������50���ַ��������ȡ
                            if (r[1].ToString().Length > 50)
                            {
                                row["cust"] = r[1].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["cust"] = r[1].ToString();
                            }

                            //port�ĳ��������50���ַ��������ȡ
                            if (r[2].ToString().Length > 50)
                            {
                                row["port"] = r[2].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["port"] = r[2].ToString();
                            }

                            //tcp_po�ĳ��������20���ַ��������ȡ
                            if (r[3].ToString().Length > 20)
                            {
                                row["tcp_po"] = r[3].ToString().Substring(0, 20);
                            }
                            else
                            {
                                row["tcp_po"] = r[3].ToString();
                            }

                            //tcp_po�ĳ��������20���ַ��������ȡ
                            if (r[4].ToString().Length > 20)
                            {
                                row["cust_po"] = r[4].ToString().Substring(0, 20);
                            }
                            else
                            {
                                row["cust_po"] = r[4].ToString();
                            }

                            //sw1�ĳ��������10���ַ��������ȡ
                            if (r[5].ToString().Length > 10)
                            {
                                try
                                {
                                    row["sw1"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[5]));
                                }
                                catch
                                {
                                    row["sw1"] = r[5].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["sw1"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[5]));
                                }
                                catch
                                {
                                    row["sw1"] = r[5].ToString();
                                }
                            }

                            //sw2�ĳ��������10���ַ��������ȡ
                            if (r[6].ToString().Length > 10)
                            {
                                try
                                {
                                    row["sw2"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[6]));
                                }
                                catch
                                {
                                    row["sw2"] = r[6].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["sw2"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[6]));
                                }
                                catch
                                {
                                    row["sw2"] = r[6].ToString();
                                }
                            }

                            //due_date�ĳ��������10���ַ��������ȡ
                            if (r[7].ToString().Length > 10)
                            {
                                try
                                {
                                    row["due_date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[7]));
                                }
                                catch
                                {
                                    row["due_date"] = r[7].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["due_date"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[7]));
                                }
                                catch
                                {
                                    row["due_date"] = r[7].ToString();
                                }
                            }

                            //line�ĳ��������4���ַ��������ȡ
                            if (r[8].ToString().Length > 4)
                            {
                                row["line"] = r[8].ToString().Substring(0, 4);
                            }
                            else
                            {
                                row["line"] = r[8].ToString();
                            }

                            //szx_so�ĳ��������8���ַ��������ȡ
                            if (r[9].ToString().Length > 8)
                            {
                                row["szx_so"] = r[9].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["szx_so"] = r[9].ToString();
                            }

                            //atl_so�ĳ��������8���ַ��������ȡ
                            if (r[10].ToString().Length > 8)
                            {
                                row["atl_so"] = r[10].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["atl_so"] = r[10].ToString();
                            }

                            //qad�ĳ��������14���ַ��������ȡ
                            if (r[11].ToString().Length > 14)
                            {
                                row["qad"] = r[11].ToString().Substring(0, 14);
                            }
                            else
                            {
                                row["qad"] = r[11].ToString();
                            }

                            //item�ĳ��������50���ַ��������ȡ
                            if (r[12].ToString().Length > 50)
                            {
                                row["item"] = r[12].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["item"] = r[12].ToString();
                            }

                            //qty_ord�ĳ��������8���ַ��������ȡ
                            if (r[13].ToString().Length > 8)
                            {
                                row["qty_ord"] = r[13].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["qty_ord"] = r[13].ToString();
                            }

                            //qty_ord1�ĳ��������8���ַ��������ȡ
                            if (r[14].ToString().Length > 8)
                            {
                                row["qty_ord1"] = r[14].ToString().Substring(0, 8);
                            }
                            else
                            {
                                row["qty_ord1"] = r[14].ToString();
                            }

                            //site�ĳ��������4���ַ��������ȡ
                            if (r[15].ToString().Length > 4)
                            {
                                row["site"] = r[15].ToString().Substring(0, 4);
                            }
                            else
                            {
                                row["site"] = r[15].ToString();
                            }

                            //rmks�ĳ��������10���ַ��������ȡ
                            if (r[16].ToString().Length > 10)
                            {
                                row["rmks"] = r[16].ToString().Substring(0, 10);
                            }
                            else
                            {
                                row["rmks"] = r[16].ToString();
                            }

                            //price�ĳ��������10���ַ��������ȡ
                            if (r[17].ToString().Length > 10)
                            {
                                row["price"] = r[17].ToString().Substring(0, 10);
                            }
                            else
                            {
                                row["price"] = r[17].ToString();
                            }

                            //sample�ĳ��������50���ַ��������ȡ
                            if (r[18].ToString().Length > 50)
                            {
                                row["sample"] = r[18].ToString().Substring(0, 50);
                            }
                            else
                            {
                                row["sample"] = r[18].ToString();
                            }

                            //planDate�ĳ��������10���ַ��������ȡ
                            if (r[19].ToString().Length > 10)
                            {
                                try
                                {
                                    row["planDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[19]));
                                }
                                catch
                                {
                                    row["planDate"] = r[19].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["planDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[19]));
                                }
                                catch
                                {
                                    row["planDate"] = r[19].ToString();
                                }
                            }

                            if (r[20].ToString().Length > 10)
                            {
                                try
                                {
                                    row["IsSample"] = Convert.ToBoolean(r[20]).ToString();
                                }
                                catch
                                {
                                    row["IsSample"] = "error";
                                }
                            
                            }
                            else
                            {
                                try
                                {
                                    row["IsSample"] = Convert.ToBoolean(r[20]).ToString();
                                }
                                catch
                                {
                                    row["IsSample"] = "error";
                                }
                            
                            }
                            try
                            {
                                row["remarks"] = r[21].ToString();
                            }
                            catch
                            {
                                row["remarks"] = "error";
                            }

                            row["domain"] = r[22].ToString();

                            try
                            {
                                row["promisedDeliveryDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[23]));
                            }
                            catch
                            {
                                row["promisedDeliveryDate"] = r[23].ToString();
                            }
                           
                            #endregion

                            row["createdBy"] = _uID;
                            row["errMsg"] = string.Empty;

                            table.Rows.Add(row);
                        }

                        //table�����ݵ������
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopy.DestinationTableName = "dbo.ediTemp";
                                bulkCopy.ColumnMappings.Add("date", "et_date");
                                bulkCopy.ColumnMappings.Add("cust", "et_cust");
                                bulkCopy.ColumnMappings.Add("port", "et_port");
                                bulkCopy.ColumnMappings.Add("tcp_po", "et_tcp_po");
                                bulkCopy.ColumnMappings.Add("cust_po", "et_cust_po");
                                bulkCopy.ColumnMappings.Add("sw1", "et_sw1");
                                bulkCopy.ColumnMappings.Add("sw2", "et_sw2");
                                bulkCopy.ColumnMappings.Add("due_date", "et_due_date");
                                bulkCopy.ColumnMappings.Add("line", "et_line");
                                bulkCopy.ColumnMappings.Add("szx_so", "et_szx_so");
                                bulkCopy.ColumnMappings.Add("atl_so", "et_atl_so");
                                bulkCopy.ColumnMappings.Add("qad", "et_qad");
                                bulkCopy.ColumnMappings.Add("item", "et_item");
                                bulkCopy.ColumnMappings.Add("qty_ord", "et_qty_ord");
                                bulkCopy.ColumnMappings.Add("qty_ord1", "et_qty_ord1");
                                bulkCopy.ColumnMappings.Add("site", "et_site");
                                bulkCopy.ColumnMappings.Add("rmks", "et_rmks");
                                bulkCopy.ColumnMappings.Add("price", "et_price");
                                bulkCopy.ColumnMappings.Add("sample", "et_sample");
                                bulkCopy.ColumnMappings.Add("planDate", "et_planDate");
                                bulkCopy.ColumnMappings.Add("createdBy", "et_createdBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "et_errMsg");
                                bulkCopy.ColumnMappings.Add("IsSample", "et_IsSample");
                                bulkCopy.ColumnMappings.Add("remarks", "et_remark");
                                bulkCopy.ColumnMappings.Add("domain", "et_domain");
                                bulkCopy.ColumnMappings.Add("promisedDeliveryDate", "et_promisedDeliveryDate");
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
                                }
                            }
                        }
                    }
                }                            

                ds.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        #region ��������
        bool bPlanDate = false;
        bool bDueDate = false;
        bool bUnitPrice = false;
        bool bSite = false;
        bool bCust = false;
        bool bPart = false;
        bool bCustPart = false;
        bool bSoNbr = false;
        bool bQty = false;
        bool bIsSample = false;
        bool bRemark = false;
        bool bPromisedDeliveryDate = false;
        bool bDomain = false;
        foreach (ListItem item in chkList.Items)
        {
            //�ƻ�����
            if (Convert.ToInt32(item.Value) == 10 && item.Selected)
            {
                bPlanDate = true;
            }
            //��ֹ����
            if (Convert.ToInt32(item.Value) == 20 && item.Selected)
            {
                bDueDate = true;
            }
            //����
            if (Convert.ToInt32(item.Value) == 30 && item.Selected)
            {
                bUnitPrice = true;
            }
            //�Ƶ�
            if (Convert.ToInt32(item.Value) == 40 && item.Selected)
            {
                bSite = true;
            }
            //�ͻ�
            if (Convert.ToInt32(item.Value) == 50 && item.Selected)
            {
                bCust = true;
            }
            //QAD��
            if (Convert.ToInt32(item.Value) == 60 && item.Selected)
            {
                bPart = true;
            }
            //�ͻ����
            if (Convert.ToInt32(item.Value) == 70 && item.Selected)
            {
                bCustPart = true;
            }
            //���۶���
            if (Convert.ToInt32(item.Value) == 80 && item.Selected)
            {
                bSoNbr = true;
            }
            //����
            if (Convert.ToInt32(item.Value) == 90 && item.Selected)
            {
                bQty = true;
            }
            //��Ʒ
            if (Convert.ToInt32(item.Value) == 100 && item.Selected)
            {
                bIsSample = true;
            }
            //BOM����ԭ��
            if (Convert.ToInt32(item.Value) == 110 && item.Selected)
            {
                bRemark = true;
            }
            //��ŵ��������
            if (Convert.ToInt32(item.Value) == 120 && item.Selected)
            {
                bPromisedDeliveryDate = true;
            }

            //��
            if (Convert.ToInt32(item.Value) == 130 && item.Selected)
            {
                bDomain = true;
            }
        }

        if (!bPlanDate && !bDueDate && !bUnitPrice && !bSite && !bCust && !bPart && !bCustPart && !bSoNbr && !bQty && !bIsSample && !bRemark && !bDomain && !bPromisedDeliveryDate)
        {
            ltlAlert.Text = "alert('������ѡ��һ���!');";
            return;
        }
        #endregion
        if (ImportExcelFile())
        {
            if (!CheckValidity(Session["uID"].ToString(), bPlanDate, bDueDate, bUnitPrice, bSite, bCust, bPart, bCustPart, bSoNbr, bQty, bIsSample, bRemark, bDomain, bPromisedDeliveryDate))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), bPlanDate, bDueDate, bUnitPrice, bSite, bCust, bPart, bCustPart, bSoNbr, bQty, bIsSample, bRemark, bDomain, bPromisedDeliveryDate))
                {
                    ltlAlert.Text = "alert('����ɹ�!');";
                }
                else
                {
                    ltlAlert.Text = "alert('����ʧ��!');";
                }
            }
            else
            {
                ltlAlert.Text = "window.open('EDIPOImportError.aspx?rt=" + DateTime.Now.ToString() + "', '_blank');";
            }
        }
    }
}
