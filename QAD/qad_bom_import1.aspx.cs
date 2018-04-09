using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using System.IO;

public partial class QAD_qad_bom_import1 : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "150^<b>���ٵ���</b>~^200^<b>��Ʒ����</b>~^100^<b>������</b>~^100^<b>����</b>~^400^<b>������Ϣ</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select trackOrder, item, code, qty, errorMessage From tcpc0.dbo.PartImport_temp Where createdBy='" + Convert.ToInt32(Session["uID"]) + "'" + " And ErrorMessage <> ''";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx?ymd=a','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            filetypeDDL.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            filetypeDDL.Items.Add(item1);
        }
    }

    protected void BtnImport_ServerClick(object sender, EventArgs e)
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
        //DataSet dst = new DataSet();
        DataTable dt = new DataTable();
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
                try
                {
                    //dst = chk.getExcelContents(strFileName);
                    dt = this.GetExcelContents(strFileName);
                }
                catch (Exception ex)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ'" + ex.Message.ToString() + "'.');";
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
                    if (dt.Columns[0].ColumnName != "���ٵ���" ||
                        dt.Columns[1].ColumnName != "��Ʒ����" ||
                        dt.Columns[2].ColumnName != "������" ||
                        dt.Columns[3].ColumnName != "����")
                    {
                        //dst.Reset();
                        ltlAlert.Text += "alert('�����ļ���ģ�治��ȷ!');";
                        return;
                    }

                    //�½�TempTable�ڴ��
                    DataTable TempTable = new DataTable("TempTable");
                    DataColumn TempColumn;
                    DataRow TempRow;

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "trackOrder";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "item";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "code";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "qty";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.Int32");
                    TempColumn.ColumnName = "createdBy";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "createdDate";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "errorMessage";
                    TempTable.Columns.Add(TempColumn);

                    if (dt.Rows.Count > 0)
                    {
                        string strTrackOrder = string.Empty;
                        string strItem = string.Empty;
                        string strCode = string.Empty;
                        string strQty = string.Empty;
                        string strError = string.Empty;

                        strSQL = " Delete From PartImport_temp Where createdBy = '" + strUID + "'";
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);

                        i = 0;

                        for (i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            if (dt.Rows[i].IsNull(0)) strTrackOrder = "";
                            else strTrackOrder = dt.Rows[i].ItemArray[0].ToString().Trim();

                            if (dt.Rows[i].IsNull(1)) strItem = "";
                            else strItem = dt.Rows[i].ItemArray[1].ToString().Trim();

                            if (dt.Rows[i].IsNull(2)) strCode = "";
                            else strCode = dt.Rows[i].ItemArray[2].ToString().Trim();

                            if (dt.Rows[i].IsNull(3)) strQty = "";
                            else strQty = dt.Rows[i].ItemArray[3].ToString().Trim();

                            strError = "";

                            //if (strTrackOrder.Length == 0)
                            //{
                            //    strError += "���ٵ��Ų���Ϊ��;";
                            //}

                            if (strItem.Length == 0)
                            {
                                strError += "��Ʒ���Ʋ���Ϊ��;";
                            }

                            //if (strCode.Length == 0)
                            //{
                            //    strError += "�����Ų���Ϊ��;";
                            //}

                            if (strQty.Length == 0)
                            {
                                strError += "��������Ϊ��;";
                            }
                            else
                            {
                                try
                                {
                                    int IntQty = Convert.ToInt32(strQty);
                                    if (IntQty <= 0)
                                    {
                                        strError += "�����������0;";
                                    }
                                }
                                catch
                                {
                                    strError += "��������Ϊ����;";
                                }
                            }

                            TempRow = TempTable.NewRow();
                            TempRow["trackOrder"] = strTrackOrder;
                            TempRow["item"] = strItem;
                            TempRow["code"] = strCode;
                            TempRow["qty"] = strQty;
                            TempRow["createdBy"] = strUID;
                            TempRow["createdDate"] = DateTime.Now;
                            TempRow["errorMessage"] = strError;

                            TempTable.Rows.Add(TempRow);
                        }

                        //TempTable�����ݵ������
                        if (TempTable != null && TempTable.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopy.DestinationTableName = "PartImport_temp";

                                bulkCopy.ColumnMappings.Clear();

                                bulkCopy.ColumnMappings.Add("trackOrder", "trackOrder");
                                bulkCopy.ColumnMappings.Add("item", "item");
                                bulkCopy.ColumnMappings.Add("code", "code");
                                bulkCopy.ColumnMappings.Add("qty", "qty");
                                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                                bulkCopy.ColumnMappings.Add("createdDate", "createdDate");
                                bulkCopy.ColumnMappings.Add("errorMessage", "errorMessage");

                                try
                                {
                                    bulkCopy.WriteToServer(TempTable);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('����ʱ��������ϵϵͳ����Ա��');";
                                    return;
                                }
                                finally
                                {
                                    TempTable.Dispose();
                                    bulkCopy.Close();
                                }
                            }
                        }
                        checkItemExists(strUID, strPlant);
                    }
                    //dst.Reset();

                    if (judgeItemError(strUID))
                    {
                        int nRet = 0;
                        nRet = ExtendStru(Convert.ToInt32(strUID), Convert.ToInt32(strPlant));
                        if (nRet == -1)
                        {
                            ltlAlert.Text = "alert('չ���ṹʱ��������ϵϵͳ����Ա��');";
                            return;
                        }
                        else
                        {
                            Response.Redirect("qad_bom_exportexcel1.aspx?uID=" + strUID + "&plantID=" + strPlant + "&rm=" + DateTime.Now, true);
                        }
                    }
                    else
                    {
                        ltlAlert.Text = "alert('�����ļ��������д���!'); window.location.href='/QAD/qad_bom_import1.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('�����ļ�ʧ��!" + ex.Message + "');";
                    return;
                }

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
            }
        }
    }

    //������ʱ������
    public void checkItemExists(string userID, string plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@createdBy", userID);
        param[1] = new SqlParameter("@plantCode", plantCode);

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_item_checkExists", param);
    }

    //�ж���ʱ�������Ƿ��д���
    public bool judgeItemError(string userID)
    {
        SqlParameter param = new SqlParameter("@createdBy", userID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_item_judgeError", param));
    }

    //չ���ṹ
    public int ExtendStru(int userID, int plantID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@userID", userID);
        param[1] = new SqlParameter("@PlantID", plantID);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Export_Stru_QAD1", param));
    }
}
