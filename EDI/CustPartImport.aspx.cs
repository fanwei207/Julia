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


public partial class CustPartImport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    private bool InsertBatchTemp(string uID, string uName)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertBatchCustPart", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }

    protected bool CheckValidity(string uID)
    {
        Importcustpart(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));
        string newid_no = Request.Params["newid_no"];
        string strSql2 = " Select top 1 * From ImportError where userID ='" + Convert.ToInt32(Session["uID"]) + "' and  plantID= '" + Convert.ToInt32(Session["PlantCode"]) + "'";

        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "120^<b>�ͻ�/���﷢��</b>~^120^<b>�ͻ�����</b>~^120^<b>���Ϻ�</b>~^80^<b>��Ч����</b>~^80^<b>��ֹ����</b>~^100^<b>˵��</b>~^100^<b>��ʾ�ͻ�����</b>~^50^<b>SKU</b>~^80^<b>��ʷ��ʼʱ��</b>~^80^<b>��ʷ��ֹʱ��</b>~^500^<b>������Ϣ</b>~^";
                
                string sql = " select cpt_cust,cpt_cust_part,cpt_part,cpt_start_date,cpt_end_date,cpt_comment,cpt_cust_partd,cpt_sku,cpt_his_start_date,cpt_his_end_date,cpt_errMsg from cp_temp Where cpt_createdBy='" + Convert.ToInt32(Session["uID"]) + "' ";

                DataTable dt = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('����ʧ��!');";
                ExportExcel(title, dt, false);
                return false;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed! \\n �ύ��Ϣ��֤ʧ�ܣ�');Form1.usercode.focus();";
            return false;
        }
        return true;
        
    }

    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_clearCustPartTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean ImportExcelFile()
    {
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int line = 0;
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
                    dt = this.GetExcelContents(strFileName);
                    //ds = chk.getExcelContents(strFileName);
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

                if (dt.Rows.Count > 0)
                {
                    /*
                     *  �����Excel�ļ��������㣺
                     *      1������Ӧ��������
                     *      2���ӵ����п�ʼ����Ϊ����
                     *      3���������Ʊ�����wo2_mop�д���
                    */
                  
          

                    #region Excel���������뱣��һ��
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {

                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "�ͻ�/���﷢��")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �ͻ�/���﷢����');";
                            return false;
                        }
                        else if (col == 1 && dt.Columns[col].ColumnName.Trim() != "�ͻ�����")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� �ͻ����ϣ�');";
                            return false;
                        }
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "���Ϻ�")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ���Ϻţ�');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "��Ч����")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��Ч���ڣ�');";
                            return false;
                        }
                        else if (col == 4 && dt.Columns[col].ColumnName.Trim() != "��ֹ����")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��ֹ���ڣ�');";
                            return false;
                        }
                        else if (col == 5 && dt.Columns[col].ColumnName.Trim() != "˵��")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ˵����');";
                            return false;
                        }
                        else if (col == 6 && dt.Columns[col].ColumnName.Trim() != "��ʾ�ͻ�����")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� ��ʾ�ͻ����ϣ�');";
                            return false;
                        }
                        else if (col == 7 && dt.Columns[col].ColumnName.ToLower().Trim() != "sku")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('���ļ��ĵ�" + col.ToString() + "�б����� SKU��');";
                            return false;
                        }
                    }																
                    #endregion
                    //����ImportError
                    DataColumn column;
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

                    DataRow rowError;//��������
                  
                    //ת����ģ���ʽ
                    DataTable table = new DataTable("temp");
                   
                    DataRow row;

                    #region �������
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "domain";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "custCode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "custPart";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "qad";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "stdDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "endDate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "comment";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "partd";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "sku";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createdBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);
                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);
                   
                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region ��ֵ�������ж�
                            //domain�ĳ��������5���ַ��������ȡ
                            if (r[0].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "����Ϊ��,����" + line + "��";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                           
                                row["domain"] = "SZX";
                           

                            //custCode�ĳ��������15���ַ��������ȡ
                                if (r["�ͻ�/���﷢��"].ToString().Trim().Length == 0)
                            {

                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "�ͻ�/���﷢������Ϊ��,����" + line + "��";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                                else if (r["�ͻ�/���﷢��"].ToString().Trim().Length > 8)
                            {
                                row["custCode"] = r["�ͻ�/���﷢��"].ToString().Trim().Substring(0, 8);
                            }
                            else
                            {
                                row["custCode"] = r["�ͻ�/���﷢��"].ToString().Trim();
                            }

                            //custPart�ĳ��������20���ַ��������ȡ
                                if (r["�ͻ�����"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "�ͻ����ϲ���Ϊ��,����" + line + "��";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                                else if (r["�ͻ�����"].ToString().Trim().Length > 50)
                            {
                                row["custPart"] = r["�ͻ�����"].ToString().Trim().Substring(0, 50);
                            }
                            else
                            {
                                row["custPart"] = r["�ͻ�����"].ToString().Trim();
                            }

                            //qad�ĳ��������18���ַ��������ȡ
                                if (r["���Ϻ�"].ToString().Trim().Length == 0)
                            {
                                rowError = tblError.NewRow();

                                rowError["errInfo"] = "���ϺŲ���Ϊ��,����" + line + "��";
                                rowError["uID"] = Convert.ToInt32(Session["uID"]);
                                rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                                tblError.Rows.Add(rowError);
                            }
                                else if (r["���Ϻ�"].ToString().Trim().Length > 18)
                            {
                                row["qad"] = r["���Ϻ�"].ToString().Trim().Substring(0, 18);
                            }
                            else
                            {
                                row["qad"] = r["���Ϻ�"].ToString().Trim();
                            }

                            //stdDate�ĳ��������10���ַ��������ȡ
                                if (r["��Ч����"].ToString().Trim().Length > 10)
                            {
                                try
                                {
                                    row["stdDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["��Ч����"]));
                                }
                                catch
                                {
                                    row["stdDate"] = r["��Ч����"].ToString().Trim().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["stdDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["��Ч����"]));
                                }
                                catch
                                {
                                    row["stdDate"] = r["��Ч����"].ToString().Trim();
                                }
                            }

                            //endDate�ĳ��������10���ַ��������ȡ
                                if (r["��ֹ����"].ToString().Trim().Length > 10)
                            {
                                try
                                {
                                    row["endDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["��ֹ����"]));
                                }
                                catch
                                {
                                    row["endDate"] = r["��ֹ����"].ToString().Trim().Substring(0, 10);
                                }
                            }
                            else
                            {
                                try
                                {
                                    row["endDate"] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r["��ֹ����"]));
                                }
                                catch
                                {
                                    row["endDate"] = r["��ֹ����"].ToString().Trim();
                                }
                            }

                            //comment�ĳ��������40���ַ��������ȡ
                                if (r["˵��"].ToString().Trim().Length > 20)
                            {
                                row["comment"] = r["˵��"].ToString().Trim().Substring(0, 40);
                            }
                            else
                            {
                                row["comment"] = r["˵��"].ToString().Trim();
                            }

                            //partd�ĳ��������40���ַ��������ȡ
                                if (r["��ʾ�ͻ�����"].ToString().Trim().Length > 20)
                            {
                                row["partd"] = r["��ʾ�ͻ�����"].ToString().Trim().Substring(0, 40);
                            }
                            else
                            {
                                row["partd"] = r["��ʾ�ͻ�����"].ToString().Trim();
                            }
                            //vnum�ĳ��������50���ַ��������ȡ
                            if (r[7].ToString().Trim().Length > 50)
                            {
                                row["sku"] = r["SKU"].ToString().Trim().Substring(0, 50);
                            }
                            else
                            {
                                row["sku"] = r["SKU"].ToString().Trim();
                            }
                            #endregion

                            row["createdBy"] = _uID;
                            row["errMsg"] = string.Empty;

                            table.Rows.Add(row);
                        }

                        if (tblError != null && tblError.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
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
                                    ltlAlert.Text = "alert('����������,����ϵϵͳ����Ա!');";
                                    return false;
                                }
                                finally
                                {
                                    bulkCopyError.Close();
                                    tblError.Dispose();
                                }
                            }
                        }
                        //table�����ݵ������
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopy.DestinationTableName = "dbo.cp_temp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("custCode", "cpt_cust");
                                bulkCopy.ColumnMappings.Add("custPart", "cpt_cust_part");
                                bulkCopy.ColumnMappings.Add("qad", "cpt_part");
                                bulkCopy.ColumnMappings.Add("stdDate", "cpt_start_date");
                                bulkCopy.ColumnMappings.Add("endDate", "cpt_end_date");
                                bulkCopy.ColumnMappings.Add("comment", "cpt_comment");
                                bulkCopy.ColumnMappings.Add("partd", "cpt_cust_partd");
                                bulkCopy.ColumnMappings.Add("sku", "cpt_sku");
                                bulkCopy.ColumnMappings.Add("createdBy", "cpt_createdBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "cpt_errMsg");
                                
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('������ʱ��ʱ��������ϵϵͳ����Ա��');";
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

                dt.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }

    public int Importcustpart(Int32 uID, Int32 plantcode)
    {
        string strSQL = "sp_edi_CustPartimport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@uID", uID);
        parm[1] = new SqlParameter("@plantcode", plantcode);
        parm[2] = new SqlParameter("@retValue", SqlDbType.Bit);
        parm[2].Direction = ParameterDirection.Output;
        return Convert.ToInt32(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSQL, parm));

    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (CheckValidity(Session["uID"].ToString()))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
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
                // ltlAlert.Text = "window.open('CustPartImportError.aspx?rt=" + DateTime.Now.ToString() + "', '_blank');";
            }
        }
    }
}
