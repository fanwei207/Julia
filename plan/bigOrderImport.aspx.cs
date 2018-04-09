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

public partial class plan_bigOrderImport : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Security["44000320"].isValid)
            {
                tb2.Visible = false;
                gvPerson.Columns[3].Visible = false;
            }
            else
            {
                tb2.Visible = true;
                gvPerson.Columns[3].Visible = true;
            }


            if (Request.QueryString["err"] == "y")
            {
                Session["EXTitle"] = "100^<b>���۵�</b>~^100^<b>�к�</b>~^100^<b>�ӹ���</b>~^100^<b>ID</b>~^100^<b>����</b>~^100^<b>����</b>~^100^<b>������</b>~^100^<b>�ƻ�����</b>~^300^<b>ԭ��</b>~^100^<b>����</b>~^100^<b>ԭ�ӹ���</b>~^100^<b>ԭID</b>~^100^<b>������ɢ��</b>~^100^<b>��ע1</b>~^100^<b>��ע2</b>~^500^<b>������Ϣ</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select bo_so, bo_line, wo_nbr, wo_lot, bo_type, case isDate(wo_plandate) when 0 then wo_plandate Else Convert(varchar(10), cast(wo_plandate as datetime), 126) End as wo_plandate, bo_reason, wo_mark, bo_Sample, bo_SampleNbr, wo_nbr_parent, wo_lot_parent, bo_undefine1, bo_undefine2, bo_undefine3, bo_error From tcpc0.dbo.bigOrder_temp Where bo_createdBy='" + Convert.ToInt32(Session["uID"]) + "'" + " And bo_error <> ''";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx?ymd=a','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }

            ddlFileType.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            ddlFileType.Items.Add(item1);

            ddl_plant.SelectedValue = Session["PlantCode"].ToString();

            bindData();
        }
    }

    protected void uploadPartBtn_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ddlImportType.SelectedItem.Value.ToString() == "0")
        {
            ltlAlert.Text = "alert('��ѡ��������!');";
            return;
        }
        else
        {
            ImportExcelFile();
        }
        bindData();
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

        string strPlant = "";
        switch (Session["PlantCode"].ToString())
        {
            case "1": strPlant = "SZX";
                break;
            case "2": strPlant = "ZQL";
                break;
            case "5": strPlant = "YQL";
                break;
            case "8": strPlant = "HQL";
                break;
        }


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

                if (ddlImportType.SelectedItem.Value.ToString().Trim() == "1")
                {
                    try
                    {
                        if (dt.Columns[0].ColumnName != "���۵�" &&
                            dt.Columns[1].ColumnName != "�к�" &&
                            dt.Columns[2].ColumnName != "�ӹ���" &&
                            dt.Columns[3].ColumnName != "ID" &&
                            dt.Columns[4].ColumnName != "����" &&
                            dt.Columns[5].ColumnName != "�ƻ�����" &&
                            dt.Columns[6].ColumnName != "ԭ��" &&
                            dt.Columns[7].ColumnName != "����" &&
                            dt.Columns[8].ColumnName != "����(Y/N)" &&
                            dt.Columns[9].ColumnName != "������" &&
                            dt.Columns[10].ColumnName != "ԭ�ӹ���" &&
                            dt.Columns[11].ColumnName != "ԭID" &&
                            dt.Columns[12].ColumnName != "������ɢ��" &&
                            dt.Columns[13].ColumnName != "��ע1" &&
                            dt.Columns[14].ColumnName != "��ע2"

                            )
                        {
                            //dst.Reset();
                            ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ�������ģ���ٵ���!');";
                            return;
                        }

                        #region//�½�TempTable�ڴ��
                        DataTable TempTable = new DataTable("TempTable");
                        DataColumn TempColumn;
                        DataRow TempRow;

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_so";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_line";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_nbr";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_lot";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_type";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_plandate";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_reason";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_mark";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_nbr_parent";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "wo_lot_parent";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.Int32");
                        TempColumn.ColumnName = "bo_status";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_domain";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine1";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine2";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine3";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_Sample";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_SampleNbr";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_error";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.Int32");
                        TempColumn.ColumnName = "bo_createdBy";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_createdName";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.DateTime");
                        TempColumn.ColumnName = "bo_createdDate";

                        TempTable.Columns.Add(TempColumn);
                        #endregion

                        if (dt.Rows.Count > 0)
                        {
                            string bo_so = string.Empty;
                            string bo_line = string.Empty;
                            string wo_nbr = string.Empty;
                            string wo_lot = string.Empty;
                            string bo_type = string.Empty;
                            string wo_plandate = string.Empty;
                            string bo_reason = string.Empty;
                            string wo_mark = string.Empty;
                            string wo_nbr_par = string.Empty;
                            string wo_lot_par = string.Empty;
                            int bo_status = 0;
                            string bo_domain = strPlant;
                            string bo_undefine1 = string.Empty;
                            string bo_undefine2 = string.Empty;
                            string bo_undefine3 = string.Empty;
                            bool bo_Sample = false;
                            string bo_SampleNbr = string.Empty;
                            string bo_error = string.Empty;
                            string bo_createdBy = strUID;
                            string bo_createdName = struName;
                            string bo_createdDate = DateTime.Now.ToString();

                            DateTime dateFormat = DateTime.Now;
                            int intFormat = 0;

                            //�������ʱ���и��ϴ�Ա���ļ�¼
                            if (ClearTempTable(Convert.ToInt32(strUID)))
                            {
                                i = 0;
                                for (i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    if (dt.Rows[i].IsNull(0)) bo_so = "";
                                    else bo_so = dt.Rows[i].ItemArray[0].ToString().Trim();

                                    if (dt.Rows[i].IsNull(1)) bo_line = "";
                                    else bo_line = dt.Rows[i].ItemArray[1].ToString().Trim();

                                    if (dt.Rows[i].IsNull(2)) wo_nbr = "";
                                    else wo_nbr = dt.Rows[i].ItemArray[2].ToString().Trim();

                                    if (dt.Rows[i].IsNull(3)) wo_lot = "";
                                    else wo_lot = dt.Rows[i].ItemArray[3].ToString().Trim();

                                    if (dt.Rows[i].IsNull(4)) bo_type = "";
                                    else bo_type = dt.Rows[i].ItemArray[4].ToString().Trim();

                                    if (dt.Rows[i].IsNull(5)) wo_plandate = "";
                                    else wo_plandate = dt.Rows[i].ItemArray[5].ToString().Trim();

                                    if (dt.Rows[i].IsNull(6)) bo_reason = "";
                                    else bo_reason = dt.Rows[i].ItemArray[6].ToString().Trim();

                                    if (dt.Rows[i].IsNull(7)) wo_mark = "";
                                    else wo_mark = dt.Rows[i].ItemArray[7].ToString().Trim();

                                    if (dt.Rows[i].IsNull(13)) wo_nbr_par = "";
                                    else wo_nbr_par = dt.Rows[i].ItemArray[13].ToString().Trim();

                                    if (dt.Rows[i].IsNull(14)) wo_lot_par = "";
                                    else wo_lot_par = dt.Rows[i].ItemArray[14].ToString().Trim();

                                    if (dt.Rows[i].IsNull(10)) bo_undefine1 = "";
                                    else bo_undefine1 = dt.Rows[i].ItemArray[10].ToString().Trim();

                                    if (dt.Rows[i].IsNull(11)) bo_undefine2 = "";
                                    else bo_undefine2 = dt.Rows[i].ItemArray[11].ToString().Trim();

                                    if (dt.Rows[i].IsNull(12)) bo_undefine3 = "";
                                    else bo_undefine3 = dt.Rows[i].ItemArray[12].ToString().Trim();

                                    if (dt.Rows[i].IsNull(8)) bo_Sample = false;
                                    else bo_Sample = dt.Rows[i].ItemArray[8].ToString().Trim().ToUpper() == "Y" ? true : false;

                                    if (dt.Rows[i].IsNull(9)) bo_SampleNbr = "";
                                    else bo_SampleNbr = dt.Rows[i].ItemArray[9].ToString().Trim();

                                    bo_error = "";

                                    if (bo_so.Length == 0)
                                    {
                                        bo_error += "���۵�����Ϊ��;";
                                    }

                                    if (bo_line.Length == 0)
                                    {
                                        bo_error += "�кŲ���Ϊ��;";
                                    }
                                    else
                                    {
                                        try
                                        {
                                            intFormat = Convert.ToInt32(bo_line);
                                        }
                                        catch
                                        {
                                            bo_error += "�кű���Ϊ����;";
                                        }
                                    }

                                    if (wo_nbr.Length == 0 || wo_lot.Length == 0)
                                    {
                                        bo_error += "�ӹ�����ID����Ϊ��;";
                                    }

                                    if ( wo_lot.Trim().Length > 8)
                                    {
                                        bo_error += "ID���Ȳ��ܳ���8λ;";
                                    }

                                    if (bo_type.Length == 0)
                                    {
                                        bo_error += "���Ͳ���Ϊ��;";
                                    }
                                    else
                                    {
                                        if (bo_type != "LED" && bo_type != "CFL")
                                        {
                                            bo_error += "����ֻ��ΪLED����CFL;";
                                        }
                                    }

                                    if (wo_plandate.Length == 0)
                                    {
                                        bo_error += "�ƻ����ڲ���Ϊ��;";
                                    }
                                    else
                                    {
                                        try
                                        {
                                            DateTime dtFormat = Convert.ToDateTime(wo_plandate);
                                        }
                                        catch
                                        {
                                            bo_error += "�ƻ����ڸ�ʽ����ȷ;";
                                        }
                                    }

                                    if (bo_reason.Length > 100)
                                    {
                                        bo_error += "ԭ���ܳ���100����;";
                                    }

                                    if (wo_mark.Length != 0 && wo_mark != "ɾ��")
                                    {
                                        bo_error += "����ֻ�����ջ���ɾ��;";
                                    }

                                    TempRow = TempTable.NewRow();
                                    TempRow["bo_so"] = bo_so;
                                    TempRow["bo_line"] = bo_line;
                                    TempRow["wo_nbr"] = wo_nbr;
                                    TempRow["wo_lot"] = wo_lot;
                                    TempRow["bo_type"] = bo_type;
                                    TempRow["wo_plandate"] = wo_plandate;
                                    TempRow["bo_reason"] = (bo_reason == "" ? "" : bo_createdName + ":" + bo_reason);
                                    TempRow["wo_mark"] = wo_mark;
                                    TempRow["wo_nbr_parent"] = wo_nbr_par;
                                    TempRow["wo_lot_parent"] = wo_lot_par;
                                    TempRow["bo_status"] = bo_status;
                                    TempRow["bo_domain"] = strPlant;
                                    TempRow["bo_undefine1"] = bo_undefine1;
                                    TempRow["bo_undefine2"] = bo_undefine2;
                                    TempRow["bo_undefine3"] = bo_undefine3;
                                    TempRow["bo_Sample"] = bo_Sample;
                                    TempRow["bo_SampleNbr"] = bo_SampleNbr;
                                    TempRow["bo_error"] = bo_error;
                                    TempRow["bo_createdBy"] = bo_createdBy;
                                    TempRow["bo_createdName"] = bo_createdName;
                                    TempRow["bo_createdDate"] = bo_createdDate;

                                    TempTable.Rows.Add(TempRow);
                                }

                                //TempTable�����ݵ�������������Ƶ����ݿ���
                                if (TempTable != null && TempTable.Rows.Count > 0)
                                {
                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                    {
                                        bulkCopy.DestinationTableName = "bigOrder_temp";

                                        bulkCopy.ColumnMappings.Clear();

                                        bulkCopy.ColumnMappings.Add("bo_so", "bo_so");
                                        bulkCopy.ColumnMappings.Add("bo_line", "bo_line");
                                        bulkCopy.ColumnMappings.Add("wo_nbr", "wo_nbr");
                                        bulkCopy.ColumnMappings.Add("wo_lot", "wo_lot");
                                        bulkCopy.ColumnMappings.Add("bo_type", "bo_type");
                                        bulkCopy.ColumnMappings.Add("wo_plandate", "wo_plandate");
                                        bulkCopy.ColumnMappings.Add("bo_reason", "bo_reason");
                                        bulkCopy.ColumnMappings.Add("wo_mark", "wo_mark");
                                        bulkCopy.ColumnMappings.Add("wo_nbr_parent", "wo_nbr_parent");
                                        bulkCopy.ColumnMappings.Add("wo_lot_parent", "wo_lot_parent");
                                        bulkCopy.ColumnMappings.Add("bo_status", "bo_status");
                                        bulkCopy.ColumnMappings.Add("bo_domain", "bo_domain");
                                        bulkCopy.ColumnMappings.Add("bo_undefine1", "bo_undefine1");
                                        bulkCopy.ColumnMappings.Add("bo_undefine2", "bo_undefine2");
                                        bulkCopy.ColumnMappings.Add("bo_undefine3", "bo_undefine3");
                                        bulkCopy.ColumnMappings.Add("bo_Sample", "bo_Sample");
                                        bulkCopy.ColumnMappings.Add("bo_SampleNbr", "bo_SampleNbr");
                                        bulkCopy.ColumnMappings.Add("bo_error", "bo_error");
                                        bulkCopy.ColumnMappings.Add("bo_createdBy", "bo_createdBy");
                                        bulkCopy.ColumnMappings.Add("bo_createdName", "bo_createdName");
                                        bulkCopy.ColumnMappings.Add("bo_createdDate", "bo_createdDate");

                                        try
                                        {
                                            bulkCopy.WriteToServer(TempTable);
                                        }
                                        catch (Exception ex)
                                        {
                                            ltlAlert.Text = "alert('����ʱ��������ϵϵͳ����ԱA��');";
                                            return;
                                        }
                                        finally
                                        {
                                            TempTable.Dispose();
                                            bulkCopy.Close();
                                        }
                                    }
                                }

                                //dst.Reset();

                                //���ݿ����֤
                                if (CheckTempTable(Convert.ToInt32(strUID)))
                                {
                                    //�ж��ϴ������ܷ�ͨ����֤
                                    if (JudgeTempTable(Convert.ToInt32(strUID)))
                                    {
                                        if (TransTempTable(Convert.ToInt32(strUID)))
                                        {
                                            ltlAlert.Text = "alert('�����ļ��ɹ�!'); window.location.href='/plan/bigOrder.aspx?rm=" + DateTime.Now.ToString() + "';";
                                        }
                                        else
                                        {
                                            ltlAlert.Text = "alert('����ʱ��������ϵ����ԱC!');";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ltlAlert.Text = "alert('�����ļ��������д���!'); window.location.href='/plan/bigOrderImport.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                                        return;
                                    }
                                }
                                else
                                {
                                    ltlAlert.Text = "alert('����ʱ��������ϵ����ԱB!');";
                                    return;
                                }
                            }
                            else
                            {
                                ltlAlert.Text = "alert('�����ʱ������ʧ��!');";
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('�����ļ�ʧ��!');";
                        return;
                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }
                }
                else if (ddlImportType.SelectedItem.Value.ToString().Trim() == "2")
                {
                    try
                    {
                        if
                        (
                            dt.Columns[0].ColumnName != "���۵�" &&
                            dt.Columns[1].ColumnName != "������ɢ��"
                        )
                        {
                            //dst.Reset();
                            ltlAlert.Text = "alert('�����ļ���ģ�治��ȷ�������ģ���ٵ���!');";
                            return;
                        }

                        #region//�½�TempTable�ڴ��
                        DataTable TempTable = new DataTable("TempTable");
                        DataColumn TempColumn;
                        DataRow TempRow;

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_so";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_undefine1";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_error";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.Int32");
                        TempColumn.ColumnName = "bo_createdBy";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.String");
                        TempColumn.ColumnName = "bo_createdName";
                        TempTable.Columns.Add(TempColumn);

                        TempColumn = new DataColumn();
                        TempColumn.DataType = System.Type.GetType("System.DateTime");
                        TempColumn.ColumnName = "bo_createdDate";

                        TempTable.Columns.Add(TempColumn);
                        #endregion

                        if (dt.Rows.Count > 0)
                        {
                            string bo_so = string.Empty;
                            string bo_undefine1 = string.Empty;
                            string bo_error = string.Empty;
                            string bo_createdBy = strUID;
                            string bo_createdName = struName;
                            string bo_createdDate = DateTime.Now.ToString();

                            //�������ʱ���и��ϴ�Ա���ļ�¼
                            if (ClearZSTempTable(Convert.ToInt32(strUID)))
                            {
                                i = 0;
                                for (i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    if (dt.Rows[i].IsNull(0)) bo_so = "";
                                    else bo_so = dt.Rows[i].ItemArray[0].ToString().Trim();

                                    if (dt.Rows[i].IsNull(1)) bo_undefine1 = "";
                                    else bo_undefine1 = dt.Rows[i].ItemArray[1].ToString().Trim();

                                    bo_error = "";

                                    if (bo_so.Length == 0)
                                    {
                                        bo_error += "���۵�����Ϊ��;";
                                    }

                                    if (bo_undefine1.Length == 0)
                                    {
                                        bo_error += "������ɢ������Ϊ��;";
                                    }

                                    TempRow = TempTable.NewRow();
                                    TempRow["bo_so"] = bo_so;
                                    TempRow["bo_undefine1"] = bo_undefine1;
                                    TempRow["bo_error"] = bo_error;
                                    TempRow["bo_createdBy"] = bo_createdBy;
                                    TempRow["bo_createdName"] = bo_createdName;
                                    TempRow["bo_createdDate"] = bo_createdDate;

                                    TempTable.Rows.Add(TempRow);
                                }

                                //TempTable�����ݵ�������������Ƶ����ݿ���
                                if (TempTable != null && TempTable.Rows.Count > 0)
                                {
                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                    {
                                        bulkCopy.DestinationTableName = "bigOrder_ZS";

                                        bulkCopy.ColumnMappings.Clear();

                                        bulkCopy.ColumnMappings.Add("bo_so", "bo_so");
                                        bulkCopy.ColumnMappings.Add("bo_undefine1", "bo_undefine1");
                                        bulkCopy.ColumnMappings.Add("bo_error", "bo_error");
                                        bulkCopy.ColumnMappings.Add("bo_createdBy", "bo_createdBy");
                                        bulkCopy.ColumnMappings.Add("bo_createdName", "bo_createdName");
                                        bulkCopy.ColumnMappings.Add("bo_createdDate", "bo_createdDate");

                                        try
                                        {
                                            bulkCopy.WriteToServer(TempTable);
                                        }
                                        catch (Exception ex)
                                        {
                                            ltlAlert.Text = "alert('����ʱ��������ϵϵͳ����ԱA��');";
                                            return;
                                        }
                                        finally
                                        {
                                            TempTable.Dispose();
                                            bulkCopy.Close();
                                        }
                                    }
                                }

                                //dst.Reset();

                                //���ݿ����֤
                                if (CheckZSTempTable(Convert.ToInt32(strUID)))
                                {
                                    //�ж��ϴ������ܷ�ͨ����֤
                                    if (JudgeZSTempTable(Convert.ToInt32(strUID)))
                                    {
                                        if (TransZSTempTable(Convert.ToInt32(strUID)))
                                        {
                                            ltlAlert.Text = "alert('�����ļ��ɹ�!'); window.location.href='/plan/bigOrder.aspx?rm=" + DateTime.Now.ToString() + "';";
                                        }
                                        else
                                        {
                                            ltlAlert.Text = "alert('����ʱ��������ϵ����ԱC!');";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ltlAlert.Text = "alert('�����ļ��������д���!'); window.location.href='/plan/bigOrderImport.aspx?err=y&rm=" + DateTime.Now.ToString() + "';";
                                        return;
                                    }
                                }
                                else
                                {
                                    ltlAlert.Text = "alert('����ʱ��������ϵ����ԱB!');";
                                    return;
                                }
                            }
                            else
                            {
                                ltlAlert.Text = "alert('�����ʱ������ʧ��!');";
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('�����ļ�ʧ��!');";
                        return;
                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��մ󶩵���ʱ�����ϴ������м�¼
    /// </summary>
    /// <param name="createdBy">�ϴ��ߵ�ID��</param>
    private bool ClearTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteBoTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summmary>
    /// ���ݿ�˶���ʱ����м��
    /// </summmary>
    private bool CheckTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkBoTemp1", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// �жϴ󶩵���ʱ���и��ϴ����ܷ�ͨ��
    /// </summary>
    /// <param name="createdBy">�ϴ��ߵ�ID��</param>
    private bool JudgeTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_judgeBoTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// ���󶩵���ʱ����µ���ʽ����
    /// </summary>
    /// <param name="createdBy">�ϴ��ߵ�ID��</param>
    private bool TransTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_insertBo1", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// ��ʾ�󶩵���������Ա
    /// </summary>
    /// <param name="bo_domain">��</param>
    private DataTable GetBoPerson()
    {
        try
        {
            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_selectBoPerson").Tables[0];
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// ��Ӵ󶩵���������Ա
    /// </summary>
    /// <param name="plantcode">plantcode</param>
    /// <param name="userNo">userNo</param>
    private bool AddBoPerson(int plantcode, string userNo)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@plantcode", plantcode);
            param[1] = new SqlParameter("@userNo", userNo);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_insertBoPerson", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// ɾ���󶩵���������Ա
    /// </summary>
    /// <param name="bo_id">ID</param>
    private bool DeleteBoPerson(int bo_id)
    {
        try
        {
            SqlParameter param = new SqlParameter("@bo_id", bo_id);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteBoPerson", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// �󶨴󶩵���������Ա
    /// </summary>
    private void bindData()
    {
        DataTable dt = GetBoPerson();

        gvPerson.DataSource = dt;
        gvPerson.DataBind();
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txb_userno.Text.Length == 0)
        {
            ltlAlert.Text = "alert('���Ų���Ϊ��!');";
            return;
        }
        else
        {
            if (AddBoPerson(Convert.ToInt32(ddl_plant.SelectedItem.Value), txb_userno.Text.Trim()))
            {
                bindData();
            }
            else
            {
                ltlAlert.Text = "alert('����Ա�����ڻ�������ԭ�����ʧ�ܣ�����ϵ����Ա!');";
                return;
            }
        }
    }

    protected void gvPerson_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
    }

    protected void gvPerson_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DeleteBoPerson(Convert.ToInt32(gvPerson.DataKeys[e.RowIndex].Value)))
        {
            bindData();
        }
        else
        {
            ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
            return;
        }
    }

    /// <summmary>
    /// ���ݿ�˶���ʱ����м��
    /// </summmary>
    private bool CheckZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkZSBoTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// �жϴ󶩵���ʱ���и��ϴ����ܷ�ͨ��
    /// </summary>
    /// <param name="createdBy">�ϴ��ߵ�ID��</param>
    private bool JudgeZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_judgeZSBoTemp", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// ���󶩵���ʱ����µ���ʽ����
    /// </summary>
    /// <param name="createdBy">�ϴ��ߵ�ID��</param>
    private bool TransZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_insertZSBo", param));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// ��մ󶩵���ʱ�����ϴ������м�¼
    /// </summary>
    /// <param name="createdBy">�ϴ��ߵ�ID��</param>
    private bool ClearZSTempTable(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteZSBoTemp", param));
        }
        catch
        {
            return false;
        }
    }
}